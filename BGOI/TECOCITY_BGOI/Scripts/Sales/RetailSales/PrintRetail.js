$(document).ready(function () {
    $("#btnPrint").click(function () {
        //document.getElementById("btnPrint").className = "Noprint";
        //pagesetup("0.3", "0", "0.3", "0.3");
        //window.print();
        //document.getElementById("btnPrint").className = "btn";
        ///
        document.getElementById("btnPrint").className = "Noprint";
        $("#PrintArea table td").attr("style", "border: 1px solid #000;");
        $(".PrintArea").attr("style", "width: 700px;margin-top:10px;page-break-after: always;height :1000px;")
        pagesetup("0", "0", "0", "0");
        window.print();
        //wb.ExecWB(7, 1)
        document.getElementById("btnPrint").className = "btn";
        $("#PrintArea").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });
});