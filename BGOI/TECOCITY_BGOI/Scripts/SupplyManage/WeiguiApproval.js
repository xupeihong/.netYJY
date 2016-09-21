$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reaload();
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
            document.getElementById("Approval1").value = res;
            $('.ckb').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb').attr("disabled", false);
        }
    })
    $('.ckb1').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("Approval2").value = res;
            $('.ckb1').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb1').attr("disabled", false);
        }
    })
    $('.ckb2').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("Approval3").value = res;
            $('.ckb2').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb2').attr("disabled", false);
        }
    })
    $('.ckb3').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("Approval4").value = res;
            $('.ckb3').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb3').attr("disabled", false);
        }
    })
    $('.ckb4').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("Approval5").value = res;
            $('.ckb4').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb4').attr("disabled", false);
        }
    })
    
}