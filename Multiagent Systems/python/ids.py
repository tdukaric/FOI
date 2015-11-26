#!/usr/bin/env python
# -*- coding: utf-8 -*-
import spade
import sys
from spade.DF import ServiceDescription, DfAgentDescription
import smtplib

class Primatelj( spade.Agent.Agent ):
    
    class SvePoruke( spade.Behaviour.Behaviour ):
        def _process( self ):
            self.msg = None
            self.msg = self._receive( True, 2 )
            if self.msg:
                print "Message received!"
            else:
                print "Waiting for message..."
    class PorukeIDSa( spade.Behaviour.Behaviour ):
        pravila = []
        
        def _process( self ):
            
            self.msg = None
            self.msg = self._receive( True )
            if self.msg:
                aad = spade.AMS.AmsAgentDescription()
                aad.ownership = "BUSY"
                result = self.myAgent.modifyAgent(aad)
                if result:
                    print "BUSY"

                lista = self.msg.content.split("#")
                for elem in lista[0:]:
                    naziv, sadrzaj = elem.split( ':' )
                    if naziv == 'warning':
                        warning = sadrzaj
                    #print warning
                    if naziv == 'service':
                        service = sadrzaj
                    #print service
                    if naziv == 'remoteIP':
                        remoteIP = sadrzaj
                    #print remoteIP
                    if naziv == 'level':
                        level = sadrzaj
                    #print level
                    if naziv == 'localIP':
                        localIP = sadrzaj
                    #print localIP
                    if naziv == 'level':
                        level = sadrzaj
                    #print level
                    
                #print str(lista)
                print self.msg.content
                self.pravila.append(self.msg.content)
                
                print self.pravila
                
                if(int(level) > 3):
                    print 'High threat!'
                    print 'Sending email...'
                    fromaddr = 'tdukaric.vas@gmail.com'
                    toaddrs  = 'tdukaric.vas@gmail.com'
                    msg = "Service= %s \nWarning= %s \nRemote IP= %s \nLocal IP= %s \nLevel= %s " % (service, warning, remoteIP, localIP, level)
                    username = 'tdukaric.vas'
                    password = 'nikolina20'
                    server = smtplib.SMTP('smtp.gmail.com:587')
                    server.starttls()
                    server.login(username,password)
                    server.sendmail(fromaddr, toaddrs, msg)
                    server.quit()
                    print 'Mail sent!'
            
                print self.msg.content
                
                a = spade.Agent.Agent('trazi@127.0.0.1', 'tajna')
                a.start()
                
                sd = spade.DF.ServiceDescription()
                sd.setType('minion')
                dad = spade.DF.DfAgentDescription()
                dad.addService(sd)
                rezultat = a.searchService( dad )
                for i in rezultat:
                    primatelj = i.getAID()
                    inf = spade.ACLMessage.ACLMessage()
                    inf.setPerformative( "inform" )
                    inf.setOntology( "minion" )
                    inf.setLanguage( "english" )
                    inf.addReceiver( primatelj )
                    inf.setContent( 'service:' + service + '#warning:' + warning + '#localIP:' + localIP + '#remoteIP:' + remoteIP + '#level:' + level)
                    a.send( inf )
                    print "Message send!"
                


                a._kill()
            
                            
                aad = spade.AMS.AmsAgentDescription()
                aad.ownership = "FREE"
                result = self.myAgent.modifyAgent(aad)
                if result:
                    print "FREE"
    
    def _setup(self):
        rb = self.SvePoruke()
        self.setDefaultBehaviour( rb )
        
        
        self.agnt = DfAgentDescription()
        self.agnt.setAID( self.getAID() )
        SD = ServiceDescription()
        SD.setName( 'coordinator' )
        SD.setType( 'ids' )
        self.agnt.addService( SD )
        self.registerService( self.agnt )
        
        ids_template = spade.Behaviour.ACLTemplate()
        ids_template.setOntology( "IDS" )
        mt=spade.Behaviour.MessageTemplate(ids_template)
        
        ab = self.PorukeIDSa()
        self.addBehaviour( ab, mt )

if __name__ == "__main__":
    a = Primatelj( "idsserver@127.0.0.1", "secret" )
    a.start()