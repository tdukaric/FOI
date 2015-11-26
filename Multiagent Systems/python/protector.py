#!/usr/bin/env python
# -*- coding: utf-8 -*-
import spade
import sys
from spade.DF import ServiceDescription, DfAgentDescription

class Primatelj( spade.Agent.Agent ):
    
    class SvePoruke( spade.Behaviour.Behaviour ):
        def _process( self ):
            self.msg = None
            self.msg = self._receive( True, 10 )
            if self.msg:
                print "Message received!"
            else:
                print "Waiting for message..."

    class Minion( spade.Behaviour.Behaviour ):
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

                print self.msg.content
                aad = spade.AMS.AmsAgentDescription()
                aad.ownership = "FREE"
                result = self.myAgent.modifyAgent(aad)
                if result:
                    print "FREE"
    
    
    def _setup(self):
        self.iznos = 0;
        rb = self.SvePoruke()
        self.setDefaultBehaviour( rb )
        
        self.agnt = DfAgentDescription()
        self.agnt.setAID( self.getAID() )
        SD = ServiceDescription()
        SD.setName( self.naziv )
        SD.setType( 'minion' )
        self.agnt.addService( SD )
        self.registerService( self.agnt )
        print "registriran!"
        
        minion_template = spade.Behaviour.ACLTemplate()
        minion_template.setOntology( "minion" )
        mt=spade.Behaviour.MessageTemplate(minion_template)
        
        ab = self.Minion()
        self.addBehaviour( ab, mt )

if __name__ == "__main__":
    for arg in sys.argv[ 1: ]:
        naziv, sadrzaj = arg.split( '=' )
        if naziv == 'ime':
            ime = sadrzaj + '@127.0.0.1'
        if naziv == 'server':
            server = sadrzaj + '@127.0.0.1'
    a = Primatelj( server, "secret" )
    a.naziv = ime
    a.start()