﻿$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var result = confirm("Tem certeza que deseja excluir?");

        if (result == false) {
            e.preventDefault();
        }

    });
});