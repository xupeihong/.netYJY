﻿$(document).ready(function () {
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";

    });
});