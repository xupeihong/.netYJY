var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    tick();
    $("#ScrapTime").val("");
    $("#btnSave").click(function () {
        var BRDID = $("#BRDID").val();
        var DwName = $("#DwName").val();
        var Amount = $("#Amount").val();
        var Project = $("#Project").val();
        var PaymentMethod = $("#PaymentMethod").val();
        var PersonCharge = $("#PersonCharge").val();
        var BRDTime = $("#BRDTime").val();
        var ReceivablesTime = $("#ReceivablesTime").val();
        var Remark = $("#Remark").val();
        var CreateUser = $("#CreateUser").val();
        var CreateTime = $("#localtime").text();
        var type = "1";
        if (BRDTime == "" || BRDTime == null) {
            alert("时间不能为空");
            return;
        }
        if (ReceivablesTime == "" || ReceivablesTime == null) {
            alert("收款日期不能为空");
            return;
        }
        $.ajax({
            url: "SaveBillingRecords",
            type: "Post",
            data: {
                BRDID: BRDID, DwName: DwName, Amount: Amount, Project: Project,
                PaymentMethod: PaymentMethod, PersonCharge: PersonCharge, BRDTime: BRDTime, ReceivablesTime: ReceivablesTime,
                type: type, Remark: Remark, CreateUser: CreateUser, CreateTime: CreateTime
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("保存成功");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert(data.Msg);
                }
            }
        });
    });
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
});
function DelRow() { //删除货品信息行
    var tbodyID = "DetailInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "PID", "ProName", "Spec", "Units", "Amount", "UnitPrice", "Total", "Manufacturer", "Remark"];

    if (newRowID != "")
        rowIndex = newRowID.replace(tbodyID, '');
    if (rowIndex != -1) {
        document.getElementById(tbodyID).deleteRow(rowIndex);

        if (rowIndex < $("#" + tbodyID + " tr").length) {
            for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
                var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                var tr1 = $("#" + tbodyID + (parseInt(i) + 1));
                tr.id = tbodyID + i;
                for (var j = 0; j < tr.childNodes.length; j++) {
                    var html = tr1.html();
                    for (var k = 0; k < typeNames.length; k++) {

                        var olPID = typeNames[k] + (parseInt(i) + 1);
                        var newid = typeNames[k] + i;

                        var reg = new RegExp(olPID, "g");

                        html = html.replace(reg, newid);

                    }
                    tr1.html(html);
                }
                $("#RowNumber" + i).html(parseInt(i) + 1);
            }
        }
        if (document.getElementById(tbodyID).rows.length > 0) {
            if (rowIndex == document.getElementById(tbodyID).rows.length)
                selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
            else
                selRow(document.getElementById(tbodyID + rowIndex), '');;
        }
    }
    GetAmount();
}
function showLocale(objD) {
    var str, colorhead, colorfoot;
    var yy = objD.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = objD.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = objD.getDate();
    if (dd < 10) dd = '0' + dd;
    var hh = objD.getHours();
    if (hh < 10) hh = '0' + hh;
    var mm = objD.getMinutes();
    if (mm < 10) mm = '0' + mm;
    var ss = objD.getSeconds();
    if (ss < 10) ss = '0' + ss;
    var ww = objD.getDay();
    if (ww == 0) colorhead = "<font color=\"#333333\">";
    if (ww > 0 && ww < 6) colorhead = "<font color=\"#333333\">";
    if (ww == 6) colorhead = "<font color=\"#333333\">";
    if (ww == 0) ww = "星期日";
    if (ww == 1) ww = "星期一";
    if (ww == 2) ww = "星期二";
    if (ww == 3) ww = "星期三";
    if (ww == 4) ww = "星期四";
    if (ww == 5) ww = "星期五";
    if (ww == 6) ww = "星期六";
    colorfoot = "</font>"
    str = colorhead + yy + "年" + MM + "月" + dd + "日" + " " + hh + ":" + mm + ":" + ss + " " + ww + colorfoot;
    return (str);
}
function tick() {
    var today;
    today = new Date();
    document.getElementById("localtime").innerHTML = showLocale(today);
    window.setTimeout("tick()", 1000);
}
function GaiBian() {
    var DDL = $("#OrderContent").val();
    $.ajax({
        url: "GetProSpec",
        type: "post",
        data: { DDL: DDL },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#SpecsModels").val(json[i].Spec);
                    $("#PID").val(json[i].PID);
                }
            }
        }
    })
}




