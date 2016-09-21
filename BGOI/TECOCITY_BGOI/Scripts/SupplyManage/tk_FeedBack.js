$(document).ready(function () {
    var msg = $("#msg").val();
    var state = $("#NState").val();
    if (state == "64") {
        $("#one").show();
        $("#two").show();
        $("#three").show();
    }
    else {
        $("#one").hide();
        $("#two").hide();
        $("#three").hide();
    }
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reload();
        window.parent.ClosePop();
    }
    //GetTime();
    $("#hole").height($(window).height());
    $("#sure1").click(function () {
        var Opinions = $("input[name='Opinions']:checked").val();
        //if ($("#FContent").val() == "") {
        //    alert("申请内容不能为空");
        //    return;
        //}
        if ($("#FReason").val() == "") {
            alert("申请原因不能为空");
            return;
        }
        //if ($("#FName").val() == "") {
        //    alert("申请人不能为空");
        //    return;
        //}
        //if ($("#FTime").val() == "") {
        //    alert("申请时间不能为空");
        //    return;
        //}
        //if (Opinions=="0") {

        //}
        var res = confirm("确定要保存申请信息？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }
    })
})
//function GetTime() {
//    var mydate = new Date();

//    var s = mydate.getFullYear() + "-";
//    if (mydate.getMonth() < 10) {
//        s += '0' + (mydate.getMonth() + 1) + "-";
//    }
//    else {
//        s += (mydate.getMonth() + 1) + "-";
//    }
//    if (mydate.getDate() < 10) {
//        s += '0' + mydate.getDate();


//    } else {
//        s += mydate.getDate();

//    }
//    document.getElementById("FTime").value = s;

//}