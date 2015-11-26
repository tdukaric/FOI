xquery version "3.0";
declare namespace order="urn:gs1:ecom:order:xsd:3";
declare namespace sh="http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader";
declare namespace xsi="http://www.w3.org/2001/XMLSchema-instance" ;
declare option exist:serialize "method=xhtml media-type=text/html";

let $datoteka := request:get-parameter("datoteka", 0)

let $myDoc := doc($datoteka)

return
if (request:get-parameter("datoteka", "0") = "0")
    then <span>Neuspjesno citanje naziva datoteke!</span>
    else
        if (exists($myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation))
            then
            (
                <html>
                    <body>
                        <div style="width:600px;margin:10px;">
                        <div style="float:left; margin-right: 10px; border: solid 1px; width:270px; height:150px; padding:5px;">
                        
                        {
                            for $item in $myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation
                            return
                            <p>Podaci o posiljatelju:
                                 <span>Naziv: {$item/sh:Contact/text()}</span>
                                <br />
                                <span>Email adresa: {$item/sh:EmailAddress/text()}</span>
                                <br />
                                 <span>Broj telefaksa: {$item/sh:FaxNumber/text()}</span>
                                <br />
                                 <span>Broj telefona: {$item/sh:TelephoneNumber/text()}</span>
                            </p>
                        }
                        </div>
                        <div style="float:left;border: solid 1px; width:270px; height:150px; padding:5px;">
                        {
                            for $item in $myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation
                            return
                            <p>Podaci o primatelju:
                                 <span>Naziv: {$item/sh:Contact/text()}</span>
                                <br />
                                 <span>Email adresa: {$item/sh:EmailAddress/text()}</span>
                                <br />
                                 <span>Broj telefaksa: {$item/sh:FaxNumber/text()}</span>
                                <br />
                                 <span>Broj telefona: {$item/sh:TelephoneNumber/text()}</span>
                            </p>
                         }
                        </div>
                        
                        {
                            for $item in $myDoc/order:orderMessage/order
                            return
                            <p style="float:none;">
                                <p style=""><br />Datum kreacije dokumenta: {$item/creationDateTime/text()}
                                    <br />
                                    Posiljatelj: {$item/orderLogisticalInformation/shipFrom/gln/text()}<br />
                                    Primatelj: {$item/orderLogisticalInformation/shipTo/gln/text()}<br />
                                    Odrediste: {$item/orderLogisticalInformation/inventoryLocation/gln/text()}< br/><br />
                                </p>    
                            </p>
                        }        
                        {   
                            for $artikl in $myDoc/order:orderMessage/order/orderLineItem
                            return
                            <div>
                                <span>Oznaka artikla:</span>
                                
                                <span>{$artikl/lineItemNumber/text()}</span>
                                
                                <br />
                                <span>Kolicina: {$artikl/requestedQuantity/text()}</span>
                                <span>{$artikl/requestedQuantity/attribute::measurementUnitCode/text()}</span>
                                
                                <br />
                                <span>Oznaka transakcije: {$artikl/transactionalTradeItem/gtin/text()}</span><br /><br />
                        </div>
                        }
                        </div>
                    </body>
                </html>
            )
            else
                <html>
                    <body>
                        <span>Dokument nije u GS1 formatu!</span><br /><br />
                    </body>
                </html>