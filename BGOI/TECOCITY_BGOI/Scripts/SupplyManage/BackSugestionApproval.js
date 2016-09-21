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
        var Sperson = $("#Sperson").val();
        var SCreate = $("#SCreate").val();
        if (state != "0" && state != "1") {
            alert("恢复供应商意见不能为空");
            return;
        }
        else if (content == "") {
            alert("恢复供应商评价不能为空"); return;
        }
        else if (Sperson == "") {
            alert("恢复供应商评价人不能为空"); return;
        } else if (SCreate == "") {
            alert("恢复供应商评价创建时间不能为空"); return;
        }

        var res = confirm("确定要保存恢复供应商建议吗？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }

    })

})