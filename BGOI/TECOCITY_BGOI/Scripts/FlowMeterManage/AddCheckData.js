var isConfirm = false;
$(document).ready(function () {
    var Request = GetRequest();
    var RepairType = Request["RepairType"];
  
    $("#typetitle").html(RepairType);
    var type = Request["type"];

    if (type == "腰轮") {
        $("#trStrQmax25")[0].style.display = "none";
    }
    var RID = Request["RID"];
    $("#StrRID").val(RID);
    $("#RepairType").val(RepairType);
    $("#pageContent").height($(window).height());

    $("#QD").click(function () {
        isConfirm = confirm("确定要建新项目吗")
        if (isConfirm == false) {
            return false;
        }
        else
            submitInfo();
    })
})

function returnConfirm() {
    return false;
}
function submitInfo() {
    $("#Strvalidate").val($('input:radio[name="IsRepair"]:checked').val());
    var options = {
        url: "InsertCheckData",
        data: { StrRepairType: $("#RepairType").val() },
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 10);
            }
            else {
                // alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}
function GetRequest() {

    var url = location.search; //获取url中"?"符后的字串

    var theRequest = new Object();

    if (url.indexOf("?") != -1) {

        var str = url.substr(1);

        strs = str.split("&");

        for (var i = 0; i < strs.length; i++) {

            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);

        }

    }

    return theRequest;

}
function Q1() {
    var r1 = parseInt($("#StrQmin").val() == "" ? 0 : $("#StrQmin").val());
    var r2 = parseInt($("#StrQmax1").val() == "" ? 0 : $("#StrQmax1").val());
    var r3 = parseInt($("#StrQmax2").val() == "" ? 0 : $("#StrQmax2").val());
    var r4 = parseInt($("#StrQmax25").val() == "" ? 0 : $("#StrQmax25").val());

    var r5 = parseInt($("#StrQmax4").val() == "" ? 0 : $("#StrQmax4").val());
    var r6 = parseInt($("#StrQmax7").val() == "" ? 0 : $("#StrQmax7").val());
    var r7 = parseInt($("#StrQmax").val() == "" ? 0 : $("#StrQmax").val());
    var r = 0;
    var q1 = 0;
    if (r1 > r) {
        r = r1;
        q1 = $("#StrAvg_Qmin").val();
    }
    if (r2 > r) {
        r = r2;
        q1 = $("#StrAvg_Qmax1").val();
    }
    if (r3 > r) {
        r = r3;
        q1 = $("#StrAvg_Qmax2").val();
    }


    if (r4 > r) {
        r = r4;
        q1 = $("#StrAvg_Qmax25").val();
    }

    if (r5 > r) {
        r = r5;
        q1 = $("#StrAvg_Qmax4").val();
    }



    if (r6 > r) {
        r = r6;
        q1 = $("#StrAvg_Qmax7").val();
    }


    if (r7 > r) {
        r = r7;
        q1 = $("#StrAvg_Qmax").val();
    }



    var q2 = 0;
    if (r1 < r) {
        r = r1;
        q2 = $("#StrAvg_Qmin").val();
    }
    if (r2 < r) {
        r = r2;
        q2 = $("#StrAvg_Qmax1").val();
    }
    if (r3 < r) {
        r = r3;
        q2 = $("#StrAvg_Qmax2").val();
    }
    if (r4 < r) {
        r = r4;
        q2 = $("#StrAvg_Qmax25").val();
    }
    if ($("#StrQmax4tr")[0].style.display != "none") {

        if (r5 < r) {
            r = r5;
            q2 = $("#StrAvg_Qmax4").val();
        }
    }
    if ($("#StrQmax7tr")[0].style.display != "none") {
        if (r6 < r) {
            r = r6;
            q2 = $("#StrAvg_Qmax7").val();
        }
    }
    if ($("#StrQmaxtr")[0].style.display != "none") {

        if (r7 < r) {
            r = r7;
            q2 = $("#StrAvg_Qmax").val();
        }
    }
    $("#Strq1").val(q1);
    $("#Strq2").val(q2);
}

function AvgRepeat() {
    var r1 = parseFloat($("#StrRepeat_Qmin").val() == "" ? 0 : $("#StrRepeat_Qmin").val());
    var r2 = parseFloat($("#StrRepeat_Qmax1").val() == "" ? 0 : $("#StrRepeat_Qmax1").val());
    var r3 = parseFloat($("#StrRepeat_Qmax2").val() == "" ? 0 : $("#StrRepeat_Qmax2").val());
    var r4 = parseFloat($("#StrRepeat_Qmax25").val() == "" ? 0 : $("#StrRepeat_Qmax25").val());

    var r5 = parseFloat($("#StrRepeat_Qmax4").val() == "" ? 0 : $("#StrRepeat_Qmax4").val());
    var r6 = parseFloat($("#StrRepeat_Qmax7").val() == "" ? 0 : $("#StrRepeat_Qmax7").val());
    var r7 = parseFloat($("#StrRepeat_Qmax").val() == "" ? 0 : $("#StrRepeat_Qmax").val());
    var r = 0;
    if (r1 > r) {
        r = r1;
    }
    if (r2 > r) {
        r = r2;
    }
    if (r3 > r) {
        r = r3;
    }
    if (r4 > r) {
        r = r4;
    }
    if (r5 > r) {
        r = r5;
    }
    if (r6 > r) {
        r = r6;
    }
    if (r7 > r) {
        r = r7;
    }
    $("#StrAvgRepeat").val(r);

}
function addContent() {

    if ($("#StrQmax4tr")[0].style.display == "none") {
        $("#StrQmax4tr")[0].style.display = "";
        return;
    }
    if ($("#StrQmax7tr")[0].style.display == "none") {
        $("#StrQmax7tr")[0].style.display = "";
        return;
    }
    if ($("#StrQmaxtr")[0].style.display == "none") {
        $("#StrQmaxtr")[0].style.display = "";
        return;
    }
}
function remContent() {
    if ($("#StrQmaxtr")[0].style.display == "") {
        $("#StrQmaxtr")[0].style.display = "none";
        return;
    }
    if ($("#StrQmax7tr")[0].style.display == "") {
        $("#StrQmax7tr")[0].style.display = "none";
        return;
    }
    if ($("#StrQmax4tr")[0].style.display == "") {
        $("#StrQmax4tr")[0].style.display = "none";
        return;
    }


}

function HighorLow() {
    var r = $("#StrRepairMethod").val().split('-')[1];
    if (r == "高频")
        document.getElementById("HighorLowtd").innerHTML = "平均仪表系数1/m³ %";


    else
        document.getElementById("HighorLowtd").innerHTML = "示值误差 %";

}