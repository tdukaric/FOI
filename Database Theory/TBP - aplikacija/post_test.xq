xquery version "1.0";
declare namespace request="http://exist-db.org/xquery/request";
declare namespace xs="http://www.w3.org/2001/XMLSchema";
declare option exist:serialize "method=xhtml media-type=text/html";

let $kolekcija := '/db/apps/TBP'
 
let $login := xmldb:login($kolekcija, 'admin', '')

let $datoteka := request:get-parameter("datoteka", 0)
let $oznaka := request:get-parameter("oznaka",0)
let $naziv := request:get-parameter("naziv",0)
let $email := request:get-parameter("email",0)
let $fax := request:get-parameter("fax",0)
let $tel := request:get-parameter("tel",0)
let $nazivPlatitelja := request:get-parameter("nazivPlatitelja",0)
let $emailPlatitelja := request:get-parameter("emailPlatitelja",0)
let $faxPlatitelja:= request:get-parameter("faxPlatitelja",0)
let $telPlatitelja:= request:get-parameter("telPlatitelja",0)
let $datumIzdavanja:= request:get-parameter("datumIzdavanja",0)
let $oznakaProizvoda:= request:get-parameter("oznakaProizvoda",0)
let $gtin:= request:get-parameter("gtin",0)
let $nazivProizvoda:= request:get-parameter("nazivProizvoda",0)
let $iznos:= request:get-parameter("iznos",0)

let $baza := doc($datoteka)
return (
update insert
<racun>
    <oznaka>{$oznaka}</oznaka>
    <naziv>{$naziv}</naziv>
    <email>{$email}</email>
    <fax>{$fax}</fax>
    <tel>{$tel}</tel>
    <nazivPlatitelja>{$nazivPlatitelja}</nazivPlatitelja>
    <emailPlatitelja>{$emailPlatitelja}</emailPlatitelja>
    <faxPlatitelja>{$faxPlatitelja}</faxPlatitelja>
    <telPlatitelja>{$telPlatitelja}</telPlatitelja>
    <datumIzdavanja>{$datumIzdavanja}</datumIzdavanja>
    <items>
        <item>
            <oznaka>{$oznakaProizvoda}</oznaka>
            <gtin>{$gtin}</gtin>
            <Naziv>{$nazivProizvoda}</Naziv>
            <iznos>{$iznos}</iznos>
        </item>
    </items>
</racun>
into $baza/racuni), (
    <html>
        <body>
            {
                if(request:get-parameter("datoteka", "0") = "0")
                    then <span>Neuspjesno citanje naziva datoteke!</span>
                    else <span>Unos uspjesno upisan u datoteku {request:get-parameter("datoteka", 0)}</span>
            }
        </body>
    </html>)