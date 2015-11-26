xquery version "3.0";
declare namespace order="urn:gs1:ecom:order:xsd:3";
declare namespace sh="http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader";
declare namespace xsi="http://www.w3.org/2001/XMLSchema-instance" ;

let $datoteka := request:get-parameter("datoteka", 0)

let $myDoc := doc($datoteka)
let $doc := $myDoc/narudzba

return
if (request:get-parameter("datoteka", "0") = "0")
    then <span>Neuspjesno citanje naziva datoteke!</span>
    else
        if (exists($myDoc/narudzba))
            then
            (
                <order:orderMessage xmlns:order="urn:gs1:ecom:order:xsd:3" xmlns:sh="http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
                    <sh:StandardBusinessDocumentHeader>
                        <sh:HeaderVersion>1.0</sh:HeaderVersion>
                		<sh:Sender>
                			<sh:Identifier Authority="GS1"/>
                			<sh:ContactInformation>
                				<sh:Contact>{$doc/nazivPosiljatelja/text()}</sh:Contact>
                                <sh:EmailAddress>{$doc/emailPosiljatelja/text()}</sh:EmailAddress>
                                <sh:FaxNumber>{$doc/faxPosiljatelja/text()}</sh:FaxNumber>
                                <sh:TelephoneNumber>{$doc/telPosiljatelja/text()}</sh:TelephoneNumber>
                                <sh:ContactTypeIdentifier>Buyer</sh:ContactTypeIdentifier>
                            </sh:ContactInformation>
                        </sh:Sender>
                        <sh:Receiver>
                            <sh:Identifier Authority="GS1"/>
                            <sh:ContactInformation>
                                <sh:Contact>{$doc/nazivPrimatelja/text()}</sh:Contact>
                                <sh:EmailAddress>{$doc/emailPrimatelja/text()}</sh:EmailAddress>
                                <sh:FaxNumber>{$doc/faxPrimatelja/text()}</sh:FaxNumber>
                                <sh:TelephoneNumber>{$doc/telPrimatelja/text()}</sh:TelephoneNumber>
                                <sh:ContactTypeIdentifier>Seller</sh:ContactTypeIdentifier>
                            </sh:ContactInformation>
                        </sh:Receiver>
                        <sh:DocumentIdentification>
                    		<sh:Standard>GS1</sh:Standard>
                			<sh:TypeVersion>3.0</sh:TypeVersion>
                			<sh:InstanceIdentifier>100002</sh:InstanceIdentifier>
                			<sh:Type/>
                			<sh:MultipleType>false</sh:MultipleType>
                			<sh:CreationDateAndTime>{current-dateTime()}</sh:CreationDateAndTime>
                		</sh:DocumentIdentification>
                    </sh:StandardBusinessDocumentHeader>
                    <order>
                        <creationDateTime>{current-dateTime()}</creationDateTime>
                        <documentStatusCode>ORIGINAL</documentStatusCode>
                        <orderIdentification>
                            <entityIdentification>PO3352</entityIdentification>
                            <contentOwner>
                                <gln>0</gln>
                            </contentOwner>
                        </orderIdentification>
                        <buyer>
                            <gln>{$doc/nazivPrimatelja/text()}</gln>
                        </buyer>
                        <seller>
                            <gln>{$doc/nazivPosiljatelja/text()}</gln>
                        </seller>
                        <orderLogisticalInformation>
                            <shipFrom>
                                <gln>{$doc/oznakaPosiljatelja/text()}</gln>
                            </shipFrom>
                            <shipTo>
                                <gln>{$doc/oznakaPrimatelja/text()}</gln>
                            </shipTo>
                            <inventoryLocation>
                                <gln>{$doc/oznakaPosiljatelja/text()}</gln>
                            </inventoryLocation>
                            <ultimateConsignee>
                                <gln>{$doc/oznakaPrimatelja/text()}</gln>
                            </ultimateConsignee>
                        </orderLogisticalInformation>
                        {
                        for $item in $doc/items/item return
                        <orderLineItem>
                            <lineItemNumber>{$item/oznaka/text()}</lineItemNumber>
                            <requestedQuantity measurementUnitCode="EA">{$item/kolicina/text()}</requestedQuantity>
                            <transactionalTradeItem>
                                <gtin>{$item/oznaka/text()}</gtin>
                            </transactionalTradeItem>
                            <netPrice currencyCode="EUR">{$item/iznos/text()}</netPrice>
                        </orderLineItem>
                        }
                    </order>
                </order:orderMessage>)
            else
                <error>Dokument nije u lokalnom formatu!</error>