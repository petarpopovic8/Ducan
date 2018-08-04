//Pri svakom kliku kontrole koja ima css klasu delete zatraži potvrdu
$(function () {
    $(".delete").each(function () {
        SetConfirmDelete($(this));
    });
});

function SetConfirmDelete(btn){
    $(btn).click(function (event) {
        if (!confirm("Obrisati zapis?")) {
            event.preventDefault();
        }
    });
}

//btn je naziv ili kontrola kojoj se na click postavlja događaj učitavanja html-a za ažuriranje artikla
//Adresa je zapisana u editAjaxUrl, a parametar mora biti id i uzima se vrijednost koja stoji pod data-sifartikla kontrole btn
function SetEditAjax(btn, editAjaxUrl) {
    $(btn).click(function (event) {
        event.preventDefault();
        var sifartikla = $(this).data('sifartikla');
        var tr = $(this).parents("tr");

        $.get(editAjaxUrl, { id: sifartikla }, function (data) {
            tr.html(data);
        });
    });
}

function SetDeleteAjax(btn, deleteAjaxUrl) {
    $(btn).click(function () {
        var sifartikla = $(this).data('sifartikla');
        if (confirm('Obrisati artikl?')) {            
            $.post(deleteAjaxUrl, { id: sifartikla }, function (data) {
                if (data.Successful) {
                    var tr = $(btn).parents("tr");
                    $(tr).remove();
                }
                else {
                    alert(data.ErrorMessage);
                }
            });
        }
    });
}
