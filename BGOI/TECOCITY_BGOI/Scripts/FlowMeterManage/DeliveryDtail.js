
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
        url: "GetDeliveryInfo",
        type: "post",
        data: { TakeID: $("#strTakeID").val() },
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
    if (data.DeliveryInfo != "") {
        var tab = $("#DetailInfo");
        tab.html('');
        var datas = eval("(" + data.DeliveryInfo + ")");
        $.each(datas.DeliveryInfo, function (i, n) {
            var tr = $('<tr style="height:30px;"></tr>');
            var td = '';
            for (var key in n) {
                var keyValue = n[key];
                if (key == 'FaceOther')
                    td = $('<td style="width:200px;">' + keyValue + '</td>');
                else
                    td = $('<td>' + keyValue + '</td>');
                td.appendTo(tr);
            }
            tr.appendTo(tab);
        });
        if (datas.DeliveryInfo.length < 10) {
            for (var row = 12; row > datas.DeliveryInfo.length; row--) {
                var tr = $('<tr style="height:30px;"></tr>');
                var td1 = $('<td>仪表基本信息</td>');
                td1.appendTo(tr);
                for (var col = 1; col < 10; col++) {
                    var td = $('<td></td>');
                    td.appendTo(tr);
                }
                tr.appendTo(tab);
            }
        }

        $("#DetailInfo").width = $("#pageContent").width - 20;
        mc("DetailInfo", 0, tab[0].rows.length - 1, 0);
    }
}

// 合并单元格
function mc(tableId, startRow, endRow, col) {
    var tb = document.getElementById(tableId);
    if (col >= tb.rows[0].cells.length) {
        return;
    }
    if (col == 0) {
        endRow = tb.rows.length - 1;
    }
    for (var i = startRow; i < endRow; i++) {
        if (tb.rows[startRow].cells[col].innerHTML == tb.rows[i + 1].cells[col].innerHTML) {
            tb.rows[i + 1].removeChild(tb.rows[i + 1].cells[col]);
            tb.rows[startRow].cells[col].rowSpan = (tb.rows[startRow].cells[col].rowSpan | 0) + 1;
            //if (i == endRow - 1 && startRow != endRow) {
            //    mc(tableId, startRow, endRow, col + 1);
            //}
        }
        //else {
        //    mc(tableId, startRow, i + 0, col + 1);
        //    startRow = i + 1;
        //}
    }
}

