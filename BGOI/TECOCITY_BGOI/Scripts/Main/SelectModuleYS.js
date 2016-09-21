var Role;
var wfor;
$(document).ready(function () {
    Role = $("#Role").val();
    $("#pageSelectAll").height($(window).height());
    tick();
    var UserID = $("#UserID").val();
    if (UserID == "1") {
        $("#rightMake").css({ width: "500px;" });
        $("#ReImage").show();
        $("#ReA").show();
        //document.getElementById("rightMake").style.width = "500px;"
        //document.getElementById("ReImage").style.display = ""
        //document.getElementById("ReA").style.display = ""
    }
    else {
        $("#rightMake").css({ width: "300px;" });
        $("#ReImage").hide();
        $("#ReA").hide();
        //document.getElementById("rightMake").style.width = "300px;"
        //document.getElementById("ReImage").style.display = "none"
        //document.getElementById("ReA").style.display = "none"
    }
    wfor = setInterval("selectValidate()", 60000);
})



//function turnTest()
//{
//    var name = "Left_ProjectManage$AppExamine";
//    window.open("../Main/Main?Url=" + name);
//}

function selectM(name, obj) {
    var userRole = "";
    userRole = "," + name + ",";
    if (Role.indexOf(userRole) >= 0) {
        window.location.href = "../Main/Main?id=" + name;
        //window.open("../Main/Main?id=" + name);
    }
    else {
        alert("您没有访问该模块的权限");
    }
}

function Mover(obj) {
    obj.className = "btnSelectW";
}

function Mout(obj) {
    obj.className = "btnSelect";
    $("#hint").html("");
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
    if (ww == 0) colorhead = "<font color=\"white\">";
    if (ww > 0 && ww < 6) colorhead = "<font color=\"white\">";
    if (ww == 6) colorhead = "<font color=\"white\">";
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
    document.getElementById("ltime").innerHTML = showLocale(today);
    window.setTimeout("tick()", 1000);
}


function UpdatePwd() {
    ShowIframe1("修改密码", "UpdatePwd", 400, 150, '');
}

function RestPwd() {
    ShowIframe1("重置密码", "ResetPwd", 400, 150, '');
}

function selectValidate() {
    $.ajax({
        type: "post",
        url: "ValidateJudge",
        data: {},
        dataType: "Json",
        success: function (data) {
            if (data.success == "false") {
                clearInterval(wfor);
                return;
            }
            else {
                alert("您的密码经过重置后,只能使用一天,到期时间为 " + data.Msg + " ,请立刻修改密码");
            }
        }
    })
}