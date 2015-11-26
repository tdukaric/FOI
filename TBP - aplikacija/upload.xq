let $kolekcija := '/db/apps/TBP'
let $datoteka := request:get-uploaded-file-name('file')
 
let $login := xmldb:login($kolekcija, 'admin', '')
let $store := xmldb:store($kolekcija, 'temp.xml', $datoteka)
 
return
<results>
   <message>Datoteka {$datoteka} je spremljena na {$kolekcija}.</message>
</results>