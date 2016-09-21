
$(document).ready(function () {
    $("#pageContent").height($(window).height());




    // 确定
    $("#QD").click(function () {

        var a = confirm("确定要提交修改吗")
        if (a == false)
            return;
        else {

            submit();
        }
    });


})

function returnConfirm() { return false; }
// 界面提交
function submit() {
    $("#Strvalidate").val($('input:radio[name="IsRepair"]:checked').val());
    var options = {
        url: "UpdateCheckData2Sure",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                alert(data.Msg);
                window.parent.frames["iframeRight"].reload();
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



