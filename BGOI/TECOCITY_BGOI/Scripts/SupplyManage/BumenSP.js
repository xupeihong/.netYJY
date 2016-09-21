$(document).ready(function () {
    GetTime();
    $("#charge").click(function () {
        var OpinionsD = document.getElementById("OpinionsD").value;
        var agree = $("input:radio[name='ISAgree']:checked").val();
        if (agree != "0" && agree != "1") { alert("是否同意通过部门年审不能为空"); return; }
        if (agree == "1" && OpinionsD == "") { alert("部门负责人意见不能为空"); return; }
        var res = confirm("确定提交部门级审批？");
        if (res) {
            $("#BumenSPInfo").submit();
        } else {
            return;
        }
    })
    $("#BumenSPInfo").submit(function () {
        var options = {
            url: "InsertBumenInfo",
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
        $("#BumenSPInfo").ajaxSubmit(options);
        return false;
    })
})

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