$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "InsertCashBackConfigTime",
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
                    alert(data.Msg);
                }
            }
        };
        $("#ProjectformInfo").ajaxSubmit(options);
        return false;
    })

    $("#charge").click(function () {
        if ($("#num").val() == "") {
            alert("请填写提醒范围");
            return;
        }
        if (isNaN($("#num").val())) {
            alert("提醒范围请填写数字");
            return;
        }
        var a = confirm("确定设置提醒时间吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})