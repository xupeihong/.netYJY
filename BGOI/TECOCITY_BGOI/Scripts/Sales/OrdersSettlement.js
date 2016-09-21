var isConfirm = false;
$(document).ready(function () {
    if (location.search != "") {
        OrderID = location.search.split('&')[0].split('=')[1];
        PID = location.search.split('&')[1].split('=')[1];
        DebtAmount = location.search.split('&')[2].split('=')[1];
    }
    $("#hole").height($(window).height());
    var amount = $("#DebtAmount").val();
    if (amount > 0) {
        $("input[name=Debt]:eq(0)").attr("checked", 'checked');
        //document.getElementById("reason").style.display = "none";
    }
    else {
        $("input[name=Debt]:eq(1)").attr("checked", 'checked');
        //document.getElementById("reason").style.display = "";
    }
    $("#charge").click(function () {
        var DebtAmount1 = $("#DebtAmount").val();
        //if (DebtAmount1 == DebtAmount) {
        //    $("input[name=Debt]:eq(0)").attr("checked", 'checked');
        //}
        //else {
        //    $("input[name=Debt]:eq(1)").attr("checked", 'checked');
        //}

        if (DebtAmount1 > DebtAmount) {
            alert("结算金额不能大于欠款金额");
            return;
        }
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
    var PID = $("#PID").val();
    
    var options = {
        url: "InsertProFinish",
        data: {PID:PID},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}