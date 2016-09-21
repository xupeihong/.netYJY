$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reload();
        window.parent.ClosePop();
    }
    ck_function();
    $("#hole").height($(window).height());
    $("#sure1").click(function () {
        var res = confirm("确定要提交审批吗？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }
    })

})

function ck_function() {
    $('.ckb').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("IsPass").value = res;
            $('.ckb').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb').attr("disabled", false);
        }
    })
}