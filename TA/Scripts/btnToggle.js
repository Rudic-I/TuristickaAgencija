//btn Prikazi aranzmane / Sakrij aranzmane
$('#btnToggle').click(function () {
    if ($(this).val() == "Dodaj aranžman") {
        sakrij();
    }
    else {
        $(this).val("Prikaži aranžmane");
        prikazi();
    }
});

function sakrij() {
    $('#btnToggle').val("Prikaži aranžmane");
    $('.prikaz-aranzmana').hide('slow');
}

function prikazi() {
    $('#btnToggle').val("Dodaj aranžman");
    $('.prikaz-aranzmana').show('slow');
}