﻿$(document).ready(function () {
    $("#PrintArea table td").attr("style", "border: 1px solid #000;");
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";
        //$("#PrintArea").attr("style", "width: 100%;margin-top:10px")
        $("#PrintArea").attr("style", "width: 100%;margin-top:10px;page-break-after: always;height :1000px;");
        $("table td").attr("style", "border: 1px solid #000;");
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        //wb.ExecWB(7, 1)
        document.getElementById("btnPrint").className = "btn";
        //$("#PrintArea").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });
});