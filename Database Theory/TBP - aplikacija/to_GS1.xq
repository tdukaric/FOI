xquery version "3.0";
declare namespace item_data_notification ="urn:gs1:ecom:item_data_notification:xsd:3";
declare namespace sh="http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader";
declare namespace xsi="http://www.w3.org/2001/XMLSchema-instance" ;
declare option exist:serialize "omit-xml-declaration=no encoding=utf-8"; 

let $datoteka := request:get-parameter("datoteka", 0)
let $x := request:get-parameter("racun", 1)
let $myDoc := doc($datoteka)

return
if (request:get-parameter("datoteka", "0") = "0")
    then <span>Neuspjesno citanje naziva datoteke!</span>
    else
        if (exists($myDoc/racuni/racun))
            then (
                for $racun in $myDoc/racuni/racun[1] return (: Umjesto "1" mo?e se staviti varijabla $x.4:)
                    <item_data_notification:itemDataNotificationMessage xsi:schemaLocation="urn:gs1:ecom:item_data_notification:xsd:3 Scheme/ItemDataNotification.xsd">
                        <sh:StandardBusinessDocumentHeader>
                        	<sh:HeaderVersion>1.0</sh:HeaderVersion>
                    		<sh:Sender>
                    			<sh:Identifier Authority="GS1"/>
                    			<sh:ContactInformation>
                    				<sh:Contact>{$racun/nazivPlatitelja/text()}</sh:Contact>
                                    <sh:EmailAddress>{$racun/emailPlatitelja/text()}</sh:EmailAddress>
                                    <sh:FaxNumber>{$racun/faxPlatitelja/text()}</sh:FaxNumber>
                                    <sh:TelephoneNumber>{$racun/telPlatitelja/text()}</sh:TelephoneNumber>
                                    <sh:ContactTypeIdentifier>Buyer</sh:ContactTypeIdentifier>
                                </sh:ContactInformation>
                            </sh:Sender>
                            <sh:Receiver>
                                <sh:Identifier Authority="GS1"/>
                                <sh:ContactInformation>
                                    <sh:Contact>{$racun/naziv/text()}</sh:Contact>
                                    <sh:EmailAddress>{$racun/email/text()}</sh:EmailAddress>
                                    <sh:FaxNumber>{$racun/fax/text()}</sh:FaxNumber>
                                    <sh:TelephoneNumber>{$racun/tel/text()}</sh:TelephoneNumber>
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
                        <itemDataNotification>
                            <creationDateTime>{current-dateTime()}</creationDateTime>
                            <documentStatusCode>ORIGINAL</documentStatusCode>
                            <itemDataNotificationIdentification>
                                <entityIdentification>44337788</entityIdentification>
                            </itemDataNotificationIdentification>
                            <datasource>
                                <gln>4322311000004</gln>
                            </datasource>
                            <dataRecipient>
                                <gln>5412345000013</gln>
                            </dataRecipient>
                            {
                            for $item in $racun/items/item return
                            <itemDataNotificationLineItem>
                                <lineItemNumber>{$item/oznaka/text()}</lineItemNumber>
                                <tradeItemDescription languageCode="en">{$item/Naziv/text()}</tradeItemDescription>
                                <tradeItemIdentification>
                                    <gtin>{$item/gtin/text()}</gtin>
                                </tradeItemIdentification>
                            </itemDataNotificationLineItem>
                            }
                        </itemDataNotification>
                    </item_data_notification:itemDataNotificationMessage>)
        else <error>Dokument nije u lokalnom formatu!</error>


