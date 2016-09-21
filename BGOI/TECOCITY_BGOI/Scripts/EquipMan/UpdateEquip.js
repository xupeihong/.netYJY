$(document).ready(function () {
    $("#hole").height($(window).height());

    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "UpdateNewDeviceBas",
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
        if ($("#StrControlCode").val() == "") {
            alert("控制编号不能为空");
            return;
        }
        if ($("#StrEname").val() == "") {
            alert("设备名称不能为空");
            return;
        }
        if ($("#StrSpecification").val() == "") {
            alert("规格型号不能为空");
            return;
        }
        if ($("#StrTracingType").val() == "") {
            alert("请选择溯源类型");
            return;
        }
        if ($("#StrCycleType").val() == "") {
            alert("请选择检定/校准周期类型");
            return;
        }
        if ($("#StrCycle").val() == "") {
            alert("检定/校准周期不能空");
            return;
        }
        if (isNaN($("#StrCycle").val())) {
            alert("检定/校准周期请填写数字");
            return;
        }
        var a = confirm("确定要修改设备信息吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})