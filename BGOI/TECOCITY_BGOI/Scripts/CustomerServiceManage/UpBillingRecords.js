var PID;
var rowCount;
var newRowID;
$(document).ready(function () {
    tick();
    jq();
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
        var type = "2";
        //if (BXKID == "" || BXKID == null) {
        //    alert("保修卡编号不能为空");
        //    return;
        //}
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
function jq() {
    if (location.search != "") {
        BRDID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "UpUpUpBillingRecords",
        type: "Post",
        data: {
            BRDID: BRDID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        $("#BRDTime").val(json[i].BRDTime);
                        $("#ReceivablesTime").val(json[i].ReceivablesTime);
                    }
                }
            }
            else {
                alert(data.Msg);
            }
        }
    });
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
                    // $("#SpecsModels").val(json[i].Spec);
                    $("#SpecsModels option:contains('" + json[i].Spec + "')").prop('selected', true);
                    $("#PID").val(json[i].PID);
                }
            }
        }
    })
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




