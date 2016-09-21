var isConfirm = false;
$(document).ready(function () {
    $("#hole").height($(window).height());
    var amount = $("#StrIsDebt").val();
    if (amount == 0) {
        $("input[name=Debt]:eq(0)").attr("checked", 'checked');
        //document.getElementById("reason").style.display = "none";
    }
    else {
        $("input[name=Debt]:eq(1)").attr("checked", 'checked');
        //document.getElementById("reason").style.display = "";
    }
    $("#charge").click(function () {
        isConfirm = confirm("确定保存结算记录吗")
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
    var options = {
        url: "InsertSettlement",
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