$(document).ready(function () {
    $("#btnPrint").click(function () {
        $("table td").attr("style", "border: 1px solid #000;");
        document.getElementById("btnPrint").className = "Noprint";
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
    });
});