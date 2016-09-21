$(document).ready(function () {
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";
        $("table td").attr("style", "border: 1px solid #000;");
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
    });
});