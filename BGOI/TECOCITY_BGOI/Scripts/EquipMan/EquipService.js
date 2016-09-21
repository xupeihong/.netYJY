$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "InsertDRepairInfo",
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
        if ($("#StrServiceReason").val() == "") {
            alert("维修原因不能为空");
            return;
        }
        if ($("#StrServiceTime").val() == "") {
            alert("维修时间不能为空");
            return;
        }
        if ($("#StrServiceCompany").val() == "") {
            alert("维修单位不能为空");
            return;
        }
        var a = confirm("确定要对该设备进行维修吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})