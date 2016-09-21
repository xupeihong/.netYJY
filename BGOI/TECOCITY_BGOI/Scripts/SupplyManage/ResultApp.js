$(document).ready(function () {
    GetTime();
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reload1();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#btnok").click(function () {
        var ResState = $("input[name='ResState']:checked").val();
        if (ResState == '1' && $("#ApprovalRes").val() == "") {
            alert("最终意见不能为空"); return;
        }
        //if ($("#ResState").val() == "") {
        //    alert("是否通过最终评审不能为空"); return;
        //}
        //if ($("#ApprovalRes").val() == "") {
        //    alert("最终意见不能为空"); return;
        //}
        if ($("#Approval4User").val() == "") {
            alert("签字人不能为空"); return;
        }
        var res = confirm("确定要提交最终意见吗？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }

    })
});
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
    document.getElementById("AppTime").value = s;

}