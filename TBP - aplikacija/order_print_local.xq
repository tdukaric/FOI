xquery version "3.0";
declare option exist:serialize "method=xhtml media-type=text/html";

<html>
    <body>{
        for $doc in doc("XML/narudzba.xml")/narudzba
        return
            <p>
                <h2>Oznaka narudzbe: {$doc/oznaka/text()}</h2>
                <p>Naziv posiljatelja: {$doc/nazivPosiljatelja/text()}</p>
                <p>Naziv primatelja: {$doc/nazivPrimatelja/text()}</p>
                <p>Datum izdavanja: {$doc/datumIzdavanja/text()}</p>
                <ul>
                {
                    for $items in $doc/items/item return
                        <li>
                            <p>Oznaka artikla: {$items/oznaka/text()}</p>
                            <p>Kolicina artikla:{$items/kolicina/text()}</p>
                            <p>Cijena artikla: {$items/iznos/text()}</p>
                        </li>
                }
                </ul>
                <p>Ukupna cijena: {sum($doc/items/item/iznos/text())}</p>
                <p>Ukupno proizvoda: {sum($doc/items/item/kolicina/text())}</p>
            </p>
    }</body>
</html>