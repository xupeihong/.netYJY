$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#updateMan").submit(function () {
        var options = {
            url: "UpdateManInfo",
            data: {},
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    window.parent.frames["iframeRight"].reloadMan();
                    window.parent.ClosePop();
                }
                else {
                    alert(data.Msg);
                }
            }
        };
        $("#updateMan").ajaxSubmit(options);
        return false;
    })
    $("#UPPerson").click(function () {
        var CName = document.getElementById("CName").value;
        var Sex = document.getElementById("Sex").value;
        var Job = document.getElementById("Job").value;
        var Birthday = document.getElementById("Birthday").value;
        var Age = document.getElementById("Age").value;
        var Mobile = document.getElementById("Mobile").value;
        var FAX = document.getElementById("FAX").value;
        var Email = document.getElementById("Email").value;
        var QQ = document.getElementById("QQ").value;
        var WeiXin = document.getElementById("WeiXin").value;
        var Remark = document.getElementById("Remark").value;
        var CreateUser = document.getElementById("CreateUser").value;
        var CreateTime = document.getElementById("CreateTime").value;
        var Validate = document.getElementById("Validate").value;
        if (CName == "") {
            alert("联系人姓名不能"); return;
        }
        var one = confirm("确定保存联系人信息吗？");
        if (one) {
            $("#updateMan").submit();
        }
        else { return; }
    })
})