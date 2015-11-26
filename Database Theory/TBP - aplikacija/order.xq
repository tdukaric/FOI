xquery version "3.0";
declare namespace order ="urn:gs1:ecom:order:xsd:3";
declare namespace sh="http://www.unece.org/cefact/namespaces/StandardBusinessDocumentHeader";
declare namespace xsi="http://www.w3.org/2001/XMLSchema-instance" ;
declare option exist:serialize "method=xhtml media-type=text/html";
let $myDoc := doc("order.xml")

return
<html>
<body>
    <div style="width:600px; border:solid 1px;">
    <div style="float:left;margin:10px;border: solid 1px; width:250px; height:150px; padding:5px;">
    <p>Podaci o po?iljatelju:<br />
    {
     for $item in $myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation
     return
     <span>Naziv: {$item/sh:Contact/text()}</span>
    }
    <br />
    {
     for $item in $myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation
     return
     <span>Email adresa: {$item/sh:EmailAddress/text()}</span>
    }
    <br />
    {
    for $item in $myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation
     return
     <span>Broj telefaksa: {$item/sh:FaxNumber/text()}</span>
    }
    <br />
    {
    for $item in $myDoc/order:orderMessage/sh:StandardBusinessDocumentHeader/sh:Sender/sh:ContactInformation
     return
     <span>Broj telefona: {$item/sh:TelephoneNumber/text()}</span>
    }
    </p>
    </div>
    <div style="float:left;margin:10px;border: solid 1px; width:250px; height:150px; padding:5px;">
    <p>Podaci o primatelju:<br />
    {
     for $item in $myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation
     return
     <span>Naziv: {$item/sh:Contact/text()}</span>
    }
    <br />
    {
     for $item in $myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation
     return
     <span>Email adresa: {$item/sh:EmailAddress/text()}</span>
    }
    <br />
    {
    for $item in $myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation
     return
     <span>Broj telefaksa: {$item/sh:FaxNumber/text()}</span>
    }
    <br />
    {
    for $item in $myDoc/item_data_notification:itemDataNotificationMessage/sh:StandardBusinessDocumentHeader/sh:Receiver/sh:ContactInformation
     return
     <span>Broj telefona: {$item/sh:TelephoneNumber/text()}</span>
    }
    </p>
    </div>
    
    <p style="float:none">Datum kreacije dokumenta:
    {
        for $item in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification
        return $item/creationDateTime/text()
    }
    </p>
    
    
    (:<span>Oznaka artikla:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/lineItemNumber/text()}</span>
    }
    <br />:)
    <span>Naziv artikla:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/tradeItemDescription/text()}</span>
    }
    <br />
    <span>Identifikacijska oznaka artikla:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/tradeItemIdentification/gtin/text()}</span>
    }
    <br />
    <span>Klasifikacijska oznaka artikla:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerNeutral/tradeItemClassificationCode/text()}</span>
    }
    <br />
    <br />
    <span>Dimenzije artikla:</span> <br />
    <span>Dubina artikla:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerNeutral/itemDataWeightAndDimension/depth/text()}</span>
    }
    <br />
    <span>Diametar artikla:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerNeutral/itemDataWeightAndDimension/diameter/text()}</span>
    }
    <br />
    <span>Te?ina artikla:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerNeutral/itemDataWeightAndDimension/grossWeight/text()}</span>
    }
    <br />
    <span>Visina artikla:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerNeutral/itemDataWeightAndDimension/height/text()}</span>
    }
    <br />
    <span>?irina artikla:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerNeutral/itemDataWeightAndDimension/width/text()}</span>
    }
    <br /><br />
    <span>Uvjeti narud?be:</span><br />
    <span>Maksimalna narud?ba:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerDependent/orderQuantityMaximum/text()}</span>
    }
    <br />
    <span>Minimalna narud?ba:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerDependent/orderQuantityMinimum/text()}</span>
    }
    <br />
    <span>Korak narud?be:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerDependent/orderQuantityMultiple/text()}</span>
    }
    <br />
    <span>Vrijeme izrade proizvoda:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerDependent/tradeItemProductionLeadTime/text()}</span>
    }
    <br />
    <span>Vrijeme isporuke proizvoda:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerDependent/tradeItemShipmentLeadTime/text()}</span>
    }
    <br />
    <span>Oznaka zamjenskog artikla:</span>
    {
        for $artikl in $myDoc/item_data_notification:itemDataNotificationMessage/itemDataNotification/itemDataNotificationLineItem[1]
        return <span>{$artikl/itemDataTradingPartnerDependent/substituteItem/gtin/text()}</span>
    }
    </div>
    
</body>
</html>