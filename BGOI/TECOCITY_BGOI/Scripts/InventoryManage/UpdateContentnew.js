$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#SupmageformInfo").submit(function () {
        var options = {
            url: "upNewContentnew",
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
        $("#SupmageformInfo").ajaxSubmit(options);
        return false;
    })
    $("#charge").click(function () {
        if ($("#Text").val() == "") {
            alert("内容不能为空");
            return;
        }
        var a = confirm("确定修改内容吗")
        if (a == false)
            return;
        else {
            $("#SupmageformInfo").submit();
        }
    })






})