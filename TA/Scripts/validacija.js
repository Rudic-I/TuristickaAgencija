//fokus za obavezna polja
function provera(e) {
    var element = document.getElementById(e.target.id);
    if (element.value == null || element.value == "" || !element.value.match(/[A-Za-z0-9]+/)) {
        e.target.style.outline = "2px solid #CD5C5C";
        e.target.placeholder = "Ovo polje je obavezno";
        element.focus();
    }
    else {
        e.target.style.outline = "none";
    }
}

//provera forme za rezervaciju
function proveraRezervacije() {
    var ispravnaForma = true;

    //provera da li su sva input polja popunjena
    var input = document.getElementsByTagName('input');
    for (let i = 0; i < input.length; i++) {
        if (input[i].value == "" || input[i].value == null) {
            ispravnaForma = false;
            input[i].style.outline = "2px solid #CD5C5C";
            input[i].placeholder = "Ovo polje je obavezno";
            input[i].focus();
            
        }
    }

    //provera da li je RezervacijaID broj od osam cifara
    var rezervacijaID = document.getElementById("RezervacijaID").value;
    if (!rezervacijaID.match(/^\d{8}$/)) {
        document.getElementById('lblRezervacijaID').innerHTML = "Šifra rezervacije mora da sadrži osam cifara!";
        ispravnaForma = false;
    }
    else {
        document.getElementById('lblRezervacijaID').innerHTML = "";
    }

    //provera da li ime klijenta sadrzi samo slova
    var imeKlijenta = document.getElementById("Ime").value;
    if (!imeKlijenta.match(/[a-zA-Z]/)) {
        document.getElementById('lblIme').innerHTML = "Ime klijenta sadrži samo slova!";
        ispravnaForma = false;
    }
    else {
        document.getElementById('lblIme').innerHTML = "";
    }

    //provera da li korisnicko ime klijenta sadrzi slova i brojeve
    var korisnickoIme = document.getElementById("KorisnickoIme").value;
    if (!korisnickoIme.match(/^[a-zA-Z][a-zA-Z0-9]+/)) {
        document.getElementById('lblKorisnickoIme').innerHTML = "Korisničko ime počinje slovom. Može da sadrži samo slova i cifre.";
        ispravnaForma = false;
    }
    else {
        document.getElementById('lblKorisnickoIme').innerHTML = "";
    }

    //ako je forma ispravno popunjena
    if (ispravnaForma) {
        var potvrda = confirm("Da li ste sigurni?");
        if (potvrda) {
            document.getElementById("btnSubmit").setAttribute("type", "submit");
        }
        else {
            return false;
        }
    }
}

//provera logovanja
function proveraLogovanja() {
    var ispravnaForma = true;

    //provera da li su sva input polja popunjena
    var input = document.getElementsByTagName('input');
    for (let i = 0; i < input.length; i++) {
        if (input[i].value == "" || input[i].value == null) {
            ispravnaForma = false;
            input[i].style.outline = "2px solid #CD5C5C";
            input[i].placeholder = "Ovo polje je obavezno";
            input[i].focus();
        }
    }

    //ako je forma ispravno popunjena
    if (ispravnaForma) {
        document.getElementById("btnSubmit").setAttribute("type", "submit");
    }
    else {
        return false;
    }
}

//prikaz modala / provera za brisanje aranzmana i otkazivanje rezervacije
function show(e) {
    var id = $(e).attr("data-id");
    var modal = $(".modal");
    var modalContentBtn = $(".modal-content-btn");
    var showButton;

    var linkovi = $(".linkovi");
    linkovi.each(function() {
        if ($(this).attr("id") == id) {
            modal.css("display", "block");
            showButton = $(this).parent().clone();
            showButton.css("display", "block");
            showButton.appendTo(modalContentBtn);
        }
    });

    var closeBtn = $('.cursor');
    closeBtn.click(function () {
        modal.css("display", "none");
        modalContentBtn.empty();
    });
}

