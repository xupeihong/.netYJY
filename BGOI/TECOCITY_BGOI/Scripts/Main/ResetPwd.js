$(document).ready(function () {
    $("#cancel").click(function () {
        parent.ClosePop();
    });

    $("#savebtn").click(function () {
        var LoginName = $("#LoginName").val();
        var UserName = $("#UserName").val();
        if (LoginName == "") {
            $("#oldAlert").show();
            return;
        }
        if (UserName == "") {
            alert("没有该登录名对应的用户信息");
            $("#UserName").val("");
            return;
        }
        var one = confirm("是否要重置密码");
        if (one == false)
            return;
        else
            $("#UpdatePwd").submit();
    })

    $("#LoginName").blur(function () {
        var LoginName = $("#LoginName").val();
        $.ajax({
            url: "LoadUserName",
            type: "post",
            data: { data1: LoginName },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    $("#UserName").val(data.userName);
                    $("#UserID").val(data.userId);
                }
                else {
                    alert("没有该登录名对应的用户信息");
                    $("#UserName").val("");
                }
            }
        });
    })

    $("#UpdatePwd").submit(function () {
        var options = {
            url: "SaveRestPwd",
            data: {},
            type: "post",
            async: false,
            success: function (data) {
                if (data.success == true) {
                    //window.parent.frames["iframeRight"].reload();
                    alert("保存成功")
                    setTimeout('parent.ClosePop();', 100);
                }

                else {
                    alert(data.Msg);
                }
            }
        };
        $("#UpdatePwd").ajaxSubmit(options);
        return false;
    })

})