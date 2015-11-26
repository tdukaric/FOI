xquery version "3.0";
declare namespace order="urn:gs1:ecom:order:xsd:3";
declare namespace sh="http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader";
declare namespace xsi="http://www.w3.org/2001/XMLSchema-instance" ;

let $datoteka := request:get-parameter("datoteka", 0)

let $myDoc := doc($datoteka)

return
if (request:get-parameter("datoteka", "0") = "0")
    then <span>Neuspjesno citanje naziva datoteke!</span>
    else
        if (exists($myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation))
            then
            (
                <narudzba>
                    <oznaka>1</oznaka>
                    <oznakaPosiljatelja></oznakaPosiljatelja>
                    <nazivPosiljatelja>{$myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation/sh:Contact/text()}</nazivPosiljatelja>
                    <emailPosiljatelja>{$myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation/sh:EmailAddress/text()}</emailPosiljatelja>
                    <faxPosiljatelja>{$myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation/sh:FaxNumber/text()}</faxPosiljatelja>
                    <telPosiljatelja>{$myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation/sh:TelephoneNumber/text()}</telPosiljatelja>
                    <oznakaPrimatelja></oznakaPrimatelja>
                    <nazivPrimatelja>{$myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation/sh:Contact/text()}</nazivPrimatelja>
                    <emailPrimatelja>{$myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation/sh:EmailAddress/text()}</emailPrimatelja>
                    <faxPrimatelja>{$myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation/sh:FaxNumber/text()}</faxPrimatelja>
                    <telPrimatelja>{$myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation/sh:TelephoneNumber/text()}</telPrimatelja>
                    <datumIzdavanja>{$myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:DocumentIdentification/sh:CreationDateAndTime/text()}</datumIzdavanja>
                    <items>
                    {
                    for $artikl in $myDoc/order:orderMessage/order/orderLineItem
                        return
                        <item>
                            <oznaka>{$artikl/lineItemNumber/text()}</oznaka>
                            <gtin>{$artikl/transactionalTradeItem/gtin/text()}</gtin>
                            <kolicina>{$artikl/tradeItemDescription/text()}</kolicina>
                            <iznos>{$artikl/netPrice/text()}</iznos>
                        </item>
                    }
                    </items>
                </narudzba>
            )
            else
                <error>Dokument nije u GS1 formatu! </error>