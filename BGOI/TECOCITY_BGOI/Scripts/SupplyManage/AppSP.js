$(document).ready(function () {
    GetTime();
    $("#charge").click(function () {
        var ReviewDate = document.getElementById("ReviewDate").value;
        var Reason = document.getElementById("Reason").value;
        var DeclareUser = document.getElementById("DeclareUser").value;
        // var Opinions = $("input[name='Opinions']:checked").val();
        var Opinions = $("input:radio[name='Opinions']:checked").val();
        if (Opinions != "1" && Opinions != "2" && Opinions != "0") { alert("准出意见不能为空"); return; }
        //if (ReviewDate == "") { alert("申请时间不能为空"); return; }
        if (Reason == "") { alert("申请准出原因不能为空"); return; }
        if (DeclareUser == "") { alert("采购/供应商管理员签字不能为空"); return; }

        var res = confirm("确定提交处理申请？");
        if (res) {
            $("#AppInfo").submit();
        } else {
            return;
        }


    })
    $("#AppInfo").submit(function () {
        var options = {
            url: "InsertDetailInfo",
            data: {},
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    window.parent.frames["iframeRight"].reload();
                    window.parent.ClosePop();
                }
                else {
                    alert(data.Msg);
                }
            }
        };
        $("#AppInfo").ajaxSubmit(options);
        return false;
    })
    //ck_function();
})
//function change(op) {
//    var res = "";
//    if (op == "CLYJ") {
//        $('input[name=chuli]:checkbox:checked').each(function () {
//            res += $(this).val();
//        });
//    }
//    document.getElementById("Opinions").value = res;
//}
//function ck_function() {
//    $('.ckb2').click(function () {
//        var ckbname = this.name;
//        if (this.checked) {
//            var getId = document.getElementById(this.name);
//            var res = ckbname;
//            document.getElementById("Opinions").value = res;
//            $('.ckb2').each(function () {
//                if (this.name != ckbname) {
//                    $(this).attr("disabled", true);
//                }
//            });
//        } else {
//            $('.ckb2').attr("disabled", false);
//        }
//    })
//}
function GetTime() {
    var mydate = new Date();

    var s = mydate.getFullYear() + "-";
    if (mydate.getMonth() < 10) {
        s += '0' + (mydate.getMonth() + 1) + "-";
    }
    else {
        s += (mydate.getMonth() + 1) + "-";
    }
    if (mydate.getDate() < 10) {
        s += '0' + mydate.getDate();


    } else {
        s += mydate.getDate();

    }
    document.getElementById("ReviewDate").value = s;

}
