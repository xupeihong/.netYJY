var isConfirm = false;
$(document).ready(function () {
    $("#hole").height($(window).height());
    var Is = $("#StrIsReturn").val();
    if (Is == 0) {
        $("input[name=IsReturn]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=IsReturn]:eq(1)").attr("checked", 'checked');
    }

    $("#charge").click(function () {
        if ($('input:radio[name="IsReturn"]:checked').val() == null) {
            alert("请选择是否符合约定的回款进度");
            return;

        }
        isConfirm = confirm("确定保存回款记录吗")
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
    $("#StrIsReturn").val($('input:radio[name="IsReturn"]:checked').val());
    var options = {
        url: "upCCashBack",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload1();
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
