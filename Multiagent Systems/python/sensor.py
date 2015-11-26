#!/usr/bin/env python
# -*- coding: utf-8 -*-
import spade
import sys
import socket
from spade.DF import ServiceDescription, DfAgentDescription

class Posiljatelj( spade.Agent.Agent ):
    class InformBehav( spade.Behaviour.OneShotBehaviour ):
        def _process( self ):
            primatelj = spade.AID.aid( name=self.server,  addresses=[self.serverAdresa])
            self.msg = spade.ACLMessage.ACLMessage()
            self.msg.setPerformative( "inform" )
            self.msg.setOntology( "IDS" )
            self.msg.setLanguage( "english" )
            self.msg.addReceiver( primatelj )
            self.msg.setContent( 'service:' + self.service + '#warning:' + self.warning + '#localIP:' + self.localIP + '#remoteIP:' + self.ip + '#level:' + self.level)
            print self.msg
            self.myAgent.send( self.msg )
            print "Message send!"
    
    def _setup(self):
        print "Senzor: starting . . ."
        b = self.InformBehav()
        b.serverAdresa = self.serverAdresa
        b.server = self.server
        b.warning = self.warning
        b.service = self.service
        b.localIP = self.localIP
        b.ip = self.ip
        b.level = self.level
        
        self.agnt = DfAgentDescription()
        self.agnt.setAID( self.getAID() )
        SD = ServiceDescription()
        SD.setName( self.server )
        SD.setType( 'sensor' )
        self.agnt.addService( SD )
        self.registerService( self.agnt )
        
        self.addBehaviour( b, None )

if __name__ == "__main__":
    for arg in sys.argv[ 1: ]:
        naziv, sadrzaj = arg.split( '=' )
        if naziv == 'ime':
            ime = sadrzaj + '@127.0.0.1'
        if naziv == 'server':
            server = sadrzaj + '@127.0.0.1'
        if naziv == 'warning':
            warning = sadrzaj
        if naziv == 'service':
            service = sadrzaj
        if naziv == 'ip':
            remoteIP = sadrzaj
        if naziv == 'level':
            level = sadrzaj

    a = Posiljatelj(ime, "tajna" )
    a.server = server
    a.serverAdresa = 'xmpp://' + server
    a.warning = warning
    a.service = service
    a.localIP = socket.gethostbyname(socket.gethostname())
    a.ip = remoteIP
    a.level = level

    a.start()
    a._kill()