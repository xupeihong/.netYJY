var isConfirm = false;
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    $("#charge").click(function () {
        isConfirm = confirm("确定要修改分包设计信息吗")
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
        url: "upSubPackage",
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