
var isConfirm = false;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    
    // 确定
    $("#QD").click(function () {
        isConfirm = confirm("确定要提交入库信息吗")
        if (isConfirm == false) {
            return false;
        }
        else
            submitInfo();
    });

})

function submitInfo() {
    var options = {
        url: "AddStockInfo",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
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