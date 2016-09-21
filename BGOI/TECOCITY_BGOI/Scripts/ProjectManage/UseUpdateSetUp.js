var isConfirm = false;
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    var Is = $("#StrIsContract").val();
    if (Is == 0) {
        $("input[name=IsContract]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=IsContract]:eq(1)").attr("checked", 'checked');
    }
    $("#charge").click(function () {
        var isConfirm = confirm("确定修改立项内容吗")
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
    $("#StrIsContract").val($('input:radio[name="IsContract"]:checked').val());
    var options = {
        url: "UseUpNewSetUp",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

function LoadProfit() {
    var Amount = $("#StrContractAmount").val();
    var Cost = $("#StrCost").val();
    var Profit = Amount - Cost;
    if (Profit < 0) {
        alert("项目合同额不能小于项目成本");
        $("#StrProfit").val(0);
        return;
    }
    $("#StrProfit").val(Profit);
}