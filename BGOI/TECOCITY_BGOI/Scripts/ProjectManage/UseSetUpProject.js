var isConfirm = false;
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    $("#charge").click(function () {
        if ($('input:radio[name="IsContract"]:checked').val() == null) {
            alert("请选择有无合同");
            return;

        }
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
    $("#StrIsContract").val($('input:radio[name="IsContract"]:checked').val());
    var options = {
        url: "InsertUseProjectBas",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].loadLX();
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

function LoadProfit()
{
    var Amount = $("#StrContractAmount").val();
    var Cost = $("#StrCost").val();
    var Profit = Amount - Cost;
    if (Profit < 0)
    {
        alert("项目合同额不能小于项目成本");
        $("#StrProfit").val(0);
        return;
    }
    $("#StrProfit").val(Profit);
}