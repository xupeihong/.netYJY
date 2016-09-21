
$(document).ready(function () {
    jq();//加载数据
    // 打印 
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";
        $("#ReportContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#ReportContent").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });
})

function jq() {
    if (location.search != "") {
        Info = location.search.split('&')[0].split('=')[1];
        type = location.search.split('&')[1].split('=')[1];
    }
    if (type == "1") {
        $("#dayin").hide();
        $("#jc").hide();
    }
    $.ajax({
        url: "PrintWarrCard",
        type: "Post",
        data: {
            Info: Info
        },
        async: false,
        success: function (data) {
        }
    });
}

