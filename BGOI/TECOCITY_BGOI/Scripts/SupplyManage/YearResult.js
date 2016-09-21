$(document).ready(function(){
    ck_function();
    GetTime();
    $("#charge").click(function () {
        var Opinions = document.getElementById("Opinions").value;
        var OpinionsD = document.getElementById("OpinionsD").value;
        var Approval1User = $("#Approval1User").val();
        var time = $("#ApprovalTime1").val();
        if (Opinions == "") { alert("处理意见不能为空"); return; }
        if (OpinionsD == "") { alert("处理原因不能为空"); return; }
        if (Approval1User == "") { alert("部门负责人不能为空"); return; }
        if (time == "") { alert("处理日期不能为空"); return; }
        var res = confirm("确定提交准出处理？");
        if (res) {
            $("#YearRes").submit();
        } else {
            return;
        }
    })
    $("#YearRes").submit(function () {
        var options = {
            url: "InsertYearResult",
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
        $("#YearRes").ajaxSubmit(options);
        return false;
    })

})
function ck_function() {
    $('.ckb2').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("Opinions").value = res;
            $('.ckb2').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb2').attr("disabled", false);
        }
    })
}
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
    document.getElementById("ApprovalTime1").value = s;

}


