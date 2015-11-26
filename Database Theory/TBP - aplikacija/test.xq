xquery version "3.0";
declare namespace item_data_notification ="urn:gs1:ecom:item_data_notification:xsd:3";
declare namespace sh="http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader";
declare namespace xsi="http://www.w3.org/2001/XMLSchema-instance" ;
declare option exist:serialize "method=xhtml media-type=text/html";

let $datoteka := request:get-parameter("datoteka", 0)
let $myDoc := doc($datoteka)

return
if (request:get-parameter("datoteka", "0") = "0")
    then <span>Neuspjesno citanje naziva datoteke!</span>
    else
        if (exists($myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation))
            then (
                <html>
                    <body>
                        <div style="width:600px;">
                        <div style="float:left;margin:10px;border: solid 1px; width:250px; height:150px; padding:5px;">
                        
                        {
                            for $item in $myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation
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
                        <div style="float:left;margin:10px;border: solid 1px; width:250px; height:150px; padding:5px;">
                        {
                            for $item in $myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation
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
                            for $item in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification
                            return
                            <p style="float:none">Datum kreacije dokumenta: {$item/creationDateTime/text()}</p>
                        }        
                        {   
                            for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
                            return
                            <div>
                                <!--<span>Oznaka artikla:</span>
                                {
                                    for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
                                    return <span>{$artikl/lineItemNumber/text()}</span>
                                }
                                <br />-->
                                <span>Naziv artikla: {$artikl/tradeItemDescription/text()}</span>
                                
                                <br />
                                <span>Identifikacijska oznaka artikla: {$artikl/tradeItemIdentification/gtin/text()}</span>
                                
                                <br />
                                <span>Klasifikacijska oznaka artikla: {$artikl/itemDataTradingPartnerNeutral/tradeItemClassificationCode/text()}</span>
                                
                                <br />
                                <br />
                                <span>Dimenzije artikla:</span> <br />
                                <span>Dubina artikla: {$artikl/itemDataTradingPartnerNeutral/itemDataWeightAndDimension/depth/text()}</span>
                                
                                <br />
                                <span>Diametar artikla: {$artikl/itemDataTradingPartnerNeutral/itemDataWeightAndDimension/diameter/text()}</span>
                                
                                <br />
                                <span>Tezina artikla: {$artikl/itemDataTradingPartnerNeutral/itemDataWeightAndDimension/grossWeight/text()}</span>
                                
                                <br />
                                <span>Visina artikla: {$artikl/itemDataTradingPartnerNeutral/itemDataWeightAndDimension/height/text()}</span>
                                
                                <br />
                                <span>Sirina artikla: {$artikl/itemDataTradingPartnerNeutral/itemDataWeightAndDimension/width/text()}</span>
                                
                                <br /><br />
                                <span>Uvjeti narudzbe:</span><br />
                                <span>Maksimalna narudzba: {$artikl/itemDataTradingPartnerDependent/orderQuantityMaximum/text()}</span>
                                
                                <br />
                                <span>Minimalna narudzba: {$artikl/itemDataTradingPartnerDependent/orderQuantityMinimum/text()}</span>
                                
                                <br />
                                <span>Korak narudzbe: {$artikl/itemDataTradingPartnerDependent/orderQuantityMultiple/text()}</span>
                                
                                <br />
                                <span>Vrijeme izrade proizvoda: {$artikl/itemDataTradingPartnerDependent/tradeItemProductionLeadTime/text()}</span>
                                
                                <br />
                                <span>Vrijeme isporuke proizvoda: {$artikl/itemDataTradingPartnerDependent/tradeItemShipmentLeadTime/text()}</span>
                                
                                <br />
                                <span>Oznaka zamjenskog artikla: {$artikl/itemDataTradingPartnerDependent/substituteItem/gtin/text()}</span>
                                
                        </div>
                        }
                        </div><br/><br/>
                    </body>
                </html>)
            else
                <html>
                    <body>
                        <span>Dokument nije u GS1 formatu!</span><br /><br />
                    </body>
                </html>