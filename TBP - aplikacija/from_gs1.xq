xquery version "3.0";
declare namespace item_data_notification ="urn:gs1:ecom:item_data_notification:xsd:3";
declare namespace sh="http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader";
declare namespace xsi="http://www.w3.org/2001/XMLSchema-instance" ;

let $datoteka := request:get-parameter("datoteka", 0)
let $myDoc := doc($datoteka)

return
if (request:get-parameter("datoteka", "0") = "0")
    then <span>Neuspjesno citanje naziva datoteke!</span>
    else
        if (exists($myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation/sh:Contact))
            then (<racun>
            <oznaka>1</oznaka>
            <naziv>{$myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation/sh:Contact/text()}</naziv>
            <email>{$myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation/sh:EmailAddress/text()}</email>
            <fax>{$myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation/sh:FaxNumber/text()}</fax>
            <tel>{$myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation/sh:TelephoneNumber/text()}</tel>
            <nazivPlatitelja>{$myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation/sh:Contact/text()}</nazivPlatitelja>
            <emailPlatitelja>{$myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation/sh:EmailAddress/text()}</emailPlatitelja>
            <faxPlatitelja>{$myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation/sh:FaxNumber/text()}</faxPlatitelja>
            <telPlatitelja>{$myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation/sh:TelephoneNumber/text()}</telPlatitelja>
            <datumIzdavanja>{$myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:DocumentIdentification/sh:CreationDateAndTime/text()}</datumIzdavanja>
            <items>
            {
            for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem
                return
                <item>
                    <oznaka>{$artikl/lineItemNumber/text()}</oznaka>
                    <gtin>{$artikl/tradeItemIdentification/gtin/text()}</gtin>
                    <Naziv>{$artikl/tradeItemDescription/text()}</Naziv>
                    <iznos></iznos>
                </item>
            }
            </items>
            
        </racun>)
        else <error>Dokument nije u GS1 formatu!</error>