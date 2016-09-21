$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "ReturnLendAptitude",
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
        if ($("#StrReturnDate").val() == "") {
            alert("归还时间不能为空");
            return;
        }
        var a = confirm("确定保存归还证书信息吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})