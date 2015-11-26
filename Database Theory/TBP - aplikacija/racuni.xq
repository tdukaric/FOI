xquery version "3.0";
declare option exist:serialize "method=xhtml media-type=text/html";

let $datoteka := request:get-parameter("datoteka", 0)
let $x := request:get-parameter("racun", 1)
let $myDoc := doc($datoteka)

return
if (request:get-parameter("datoteka", "0") = "0")
    then <span>Neuspjesno citanje naziva datoteke!</span>
    else
        if (exists($myDoc/racuni/racun))
            then
            (
                <html>
                    <body>{
                        for $racun in $myDoc/racuni/racun[1] (:Umjesto 1 moze se staviti varijabla $x:)
                        return
                            <p>
                                <h2>Oznaka racuna: {$racun/oznaka/text()}</h2>
                                <p>Naziv platitelja: {$racun/nazivPlatitelja/text()}</p>
                                <p>Datum izdavanja: {$racun/datumIzdavanja/text()}</p>
                                <ul>
                                {
                                    for $items in $racun/items/item return
                                        <li>
                                            <p>Oznaka artikla: {$items/oznaka/text()}</p>
                                            <p>Naziv artikla: {$items/Naziv/text()}</p>
                                            <p>Cijena artikla: {$items/iznos/text()}</p>
                                        </li>
                                }
                                </ul>
                                <p>Ukupna cijena: {sum($racun/items/item/iznos/text())}</p>
                            </p>
                    }</body>
                </html>
            )
            else
                <error>Dokument nije u lokalnom formatu!</error>
