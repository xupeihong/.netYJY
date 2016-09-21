$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "InsertDCheckInfo",
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
        if ($("#StrCheckDate").val() == "") {
            alert("检定日期不能为空");
            return;
        }
        if ($("#StrCharge").val() == 0) {
            alert("费用不能0");
            return;
        }
        if (isNaN($("#StrCharge").val())) {
            alert("费用请填写数字");
            return;
        }
        var a = confirm("确定要对该设备进行记录吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})