
$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "InsertGroupUser",
            data: {},
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    window.parent.frames["iframeRight"].reload1();
                    alert(data.Msg);
                    setTimeout('parent.ClosePop()', 10);
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
        if ($("#Text").val() == "") {
            alert("内容不能为空");
            return;
        }
        var a = confirm("确定添加新内容吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})