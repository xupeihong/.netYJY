$(document).ready(function () {
    $("#cancel").click(function () {
        parent.ClosePop();
    });

    $("#savebtn").click(function () {
        var oldPwd = $("#OldPwd").val();
        var CFPwd = $("#CFPwd").val();
        var NewPwd = $("#NewPwd").val();
        if (oldPwd == "") {
            $("#oldAlert").show();
            return;
        }
        if (NewPwd == "") {
            $("#NewAlert").show();
            return;
        }
        if (CFPwd == "") {
            $("#CFAlert").show();
            return;
        }
        if (CFPwd != NewPwd) {
            $("#Alert").show();
            return;
        }


        $("#UpdatePwd").submit();
    })
    $("#UpdatePwd").submit(function () {
        var options = {
            url: "SavePwd",
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

    $("#NewPwd").blur(function () {
        var reg = /^(?![a-z]+$)(?!\d+$)[a-z0-9]{6,20}$/i;
        var CFPwd = $("#CFPwd").val();
        var NewPwd = $("#NewPwd").val();
        if (NewPwd != "") {
            $("#NewAlert").hide();
            if (reg.test(NewPwd) == false) {
                $("#AlertSix").show();
            }
            else {
                $("#AlertSix").hide();
                $("#NewAlert").hide();
            }
        } else {
            $("#AlertSix").hide();
            $("#NewAlert").show();
        }
        if (CFPwd != "" && NewPwd != "" && CFPwd != NewPwd) {
            $("#Alert").show();
        } else {
            $("#Alert").hide();

        }
    })

    $("#CFPwd").blur(function () {
        var CFPwd = $("#CFPwd").val();
        var NewPwd = $("#NewPwd").val();
        if (CFPwd != "") {
            $("#CFAlert").hide();
            $("#AlertSix").hide();
        } else
            $("#CFAlert").show();
        if (CFPwd != "" && NewPwd != "" && CFPwd != NewPwd) {
            $("#Alert").show();
        } else {
            $("#Alert").hide();
        }
    })

    $("#OldPwd").blur(function () {
        var OldPwd = $("#OldPwd").val();
        if (OldPwd != "") {
            $("#oldAlert").hide();
        } else
            $("#oldAlert").show();
    })
})