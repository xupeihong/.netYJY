var isConfirm = false;
$(document).ready(function () {
    
    $("#pageContent").height($(window).height());

    $("#charge").click(function () {
        if ($('input:radio[name="IsContract"]:checked').val() == null) {
            alert("请选择有无合同");
            return;

        }
        var isConfirm = confirm("确定要立项吗")
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
        url: "AppNewProject",
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

function loadContractAmount()
{
    if ($("#StrContractAmount").val() == "") {
        $("#StrContractAmount").val(0);
        document.getElementById("StrContractAmount").focus();
    }
}

function loadBudget() {
    if ($("#StrBudget").val() == "") {
        $("#StrBudget").val(0);
        document.getElementById("StrBudget").focus();
    }
}

function loadCost() {
    if ($("#StrCost").val() == "") {
        $("#StrCost").val(0);
        document.getElementById("StrCost").focus();
    }
}

function loadProfit() {
    if ($("#StrProfit").val() == "") {
        $("#StrProfit").val(0);
        document.getElementById("StrProfit").focus();
    }
}
