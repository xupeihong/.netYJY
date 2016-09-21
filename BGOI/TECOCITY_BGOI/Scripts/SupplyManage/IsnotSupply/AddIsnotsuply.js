$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reload();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#sure").click(function () {
        if ($("#COMNameC").val() == "") {
            alert("厂家名称不能为空"); return;
        }
        var res = confirm("确定要新增供应商信息吗？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }
    });
})