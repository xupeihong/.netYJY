
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    // 获取明细列表
    GetDetailInfo();

    // 打印 
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";

        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";

    });
})

function GetDetailInfo() {
    $.ajax({
        url: "GetDetailInfo",
        type: "post",
        data: { SID: $("#strSID").val() },
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "false") {
                return;
            }
            else {
                FillData(data);
            }
        }
    });
}

// 填充表格 
function FillData(data) {
    if ($("#content_tableLayout").length != 0) {
        $("#content_tableLayout").before($("#content"));
        $("#content_tableLayout").empty();
    }
    if (data.DetailInfo != "") {
        var tab = $("#DetailInfo");
        tab.html('');
        var datas = eval("(" + data.DetailInfo + ")");
        $.each(datas.DetailInfo, function (i, n) {
            var tr = $('<tr style="height:30px;"></tr>');
            var td = '';
            for (var key in n) {
                var keyValue = n[key];
                td = $('<td style="width:100px;">' + keyValue + '</td>');
                td.appendTo(tr);
            }
            tr.appendTo(tab);
        });
        if (datas.DetailInfo.length < 10) {
            for (var row = 13; row > datas.DetailInfo.length; row--) {
                var tr = $('<tr style="height:30px;"></tr>');
                for (var col = 0; col < 10; col++) {
                    var td = $('<td></td>');
                    td.appendTo(tr);
                }
                tr.appendTo(tab);
            }
        }

        $("#DetailInfo").width = $("#pageContent").width - 20;

    }

}