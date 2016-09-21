
$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadMan();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#sure").click(function () {
        if ($("#CName").val() == "") {
            alert("联系人姓名不能为空"); return;
        }
        if ($("#Job").val() == "") {
            alert("职位不能为空"); return;
        }
        var res = confirm("确定要保存联系人信息吗？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }
    });
})