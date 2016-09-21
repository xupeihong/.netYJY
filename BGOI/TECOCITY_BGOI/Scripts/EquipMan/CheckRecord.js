$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "UpdateDCheckInfo",
            data: {},
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    window.parent.frames["iframeRight"].reload();
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
        if ($("#StrFinishDate").val() == "") {
            alert("完成时间不能为空");
            return;
        }
        if ($("#StrActualCharge").val() == 0) {
            alert("实际费用不能0");
            return;
        }
        if (isNaN($("#StrActualCharge").val())) {
            alert("实际费用请填写数字");
            return;
        }
        if ($("#StrCalibrationResults").val() == "") {
            alert("结果不能为空");
            return;
        }
        var a = confirm("确定要对该设备进行校准记录吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})