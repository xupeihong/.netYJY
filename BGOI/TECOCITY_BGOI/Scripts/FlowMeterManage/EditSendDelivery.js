var isConfirm = false;

$(document).ready(function () {
    $("#pageContent").height($(window).height());

    // 确认修改 完成
    $("#QRXG").click(function () {
        isConfirm = confirm("确定要修改发货单信息吗")
        if (isConfirm == false) {
            return false;
        }
        else
            submitInfo();
    });

})

// 界面提交
function submitInfo() {
    var options = {
        url: "UpdateSendDelivery",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 10);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

function returnConfirm() {
    return false;
}

