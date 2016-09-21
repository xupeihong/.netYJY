$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reload();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#FZSugtion").click(function () {
        var state = $("input[name='SState']:checked").val();
        var content = $("#SContent").val();
        if (state != "0" && state != "1") {
            alert("内部评审意见不能为空");
            return;
        }
        else if (content == "") {
            alert("供应商初步评审综合评价不能为空"); return;
        }
        var res = confirm("确定要保存负责人内部评审意见吗？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }

    })
})