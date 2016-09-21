var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    tick();
    $("#ScrapTime").val("");
    $("#btnSave").click(function () {
        var KHID = $("#KHID").val();
        var CusName = $("#CusName").val();
        var CusAdd = $("#CusAdd").val();
        var CusTel = $("#CusTel").val();
        var CusEmail = $("#CusEmail").val();
        var CusUnit = $("#CusUnit").val();
        var CreateUser = $("#CreateUser").val();
        var CreateTime = $("#localtime").text();
        var Remark = $("#Remark").val();
        var type = 2;//修改
        if (CusName == "" || CusName == null) {
            alert("客户名称不能为空");
            return;
        }
        if (CusAdd == "" || CusAdd == null) {
            alert("客户地址不能为空");
            return;
        }
        if (CusTel == "" || CusTel == null) {
            alert("客户电话不能为空");
            return;
        }
        $.ajax({
            url: "SaveCusService",
            type: "Post",
            data: {
                KHID: KHID, CusName: CusName, CusAdd: CusAdd, CusTel: CusTel, CusEmail: CusEmail, CusUnit: CusUnit,
                CreateUser: CreateUser, CreateTime: CreateTime, type: type, Remark: Remark
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




