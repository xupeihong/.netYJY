$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadShare();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#sure").click(function () {
        if ($("#ShareUnits").val() == "") {
            alert("共享部门不能为空"); return;
        }
        var res = confirm("确定要保存共享部门信息吗？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }
    });
})