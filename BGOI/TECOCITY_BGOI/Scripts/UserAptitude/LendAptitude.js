$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "InsertLendAptitude",
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
        if ($("#StrLendDate").val() == "") {
            alert("借出时间不能为空");
            return;
        }
        if ($("#StrLendUnit").val() == "") {
            alert("借出单位不能为空");
            return;
        }
        if ($("#StrLendReason").val() == "") {
            alert("借出原因不能为空");
            return;
        }
        var a = confirm("确定保存借出证书信息吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})