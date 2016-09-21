$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "UpdateDRepairInfo",
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
        if ($("#StrServiceRecord").val() == "") {
            alert("维修情况记录不能为空");
            return;
        }
        if ($("#StrServiceResults").val() == "") {
            alert("维修结果不能为空");
            return;
        }
        if ($("#StrReturnTime").val() == "") {
            alert("取回时间不能为空");
            return;
        }
        var a = confirm("确定要对该设备进行维修记录吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})