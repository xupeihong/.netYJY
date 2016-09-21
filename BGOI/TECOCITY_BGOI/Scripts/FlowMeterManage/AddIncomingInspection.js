var isConfirm = false;
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    $("#QD").click(function () {
        isConfirm = confirm("确定要建新项目吗")
        if (isConfirm == false) {
            return false;
        }
        else
            submitInfo();
    })
})

function returnConfirm() {
    return isConfirm;
}
function submitInfo() {
    var options = {
        url: "InsertCleanRepair",
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