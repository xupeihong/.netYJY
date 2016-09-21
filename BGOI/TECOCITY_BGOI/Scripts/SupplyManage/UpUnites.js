$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#updateUnit").submit(function () {
        var options = {
            url: "UpdateUniteInfo",
            data: {},
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    window.parent.frames["iframeRight"].reloadShare();
                    window.parent.ClosePop();
                }
                else {
                    alert(data.Msg);
                }
            }
        };
        $("#updateUnit").ajaxSubmit(options);
        return false;
    })
    $("#UPUnite").click(function () {
        var shareunite = document.getElementById("ShareUnits").value;
        //var WType = document.getElementById("WType").value;
        //var DeclareUser = document.getElementById("DeclareUser").value;
        //var CreateUser = document.getElementById("CreateUser").value;
        //var CreateTime = document.getElementById("CreateTime").value;
        //var Validate = document.getElementById("Validate").value;
        var one = confirm("确定保存共享部门信息吗？");
        if (one) {
            $("#updateUnit").submit();
        }
        else { return; }
    })
})