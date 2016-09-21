$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "InsertDScrapInfo",
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
        if ($("#StrScrapTime").val() == "") {
            alert("报废时间不能为空");
            return;
        }
        if ($("#StrScrapResults").val() == "") {
            alert("报废原因不能为空");
            return;
        }
        var a = confirm("确定要对该设备进行报废吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})