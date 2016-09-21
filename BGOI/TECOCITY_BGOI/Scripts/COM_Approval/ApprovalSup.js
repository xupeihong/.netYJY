
$(document).ready(function () {
    $("#hole").height($(window).height());
    var webkey = $("#webkey").val();

    if (webkey == "准出停止供货评审" || webkey == "准出暂停供货评审" || webkey == "准出淘汰供应商评审" || webkey == "恢复供应商" || webkey == "恢复供货") {
        $("#one").hide();
        $("#two").hide();
        $("#three").hide();
        $("#four").hide();

    }
    else if (webkey == "准入评审") {
        $("#five").hide();
        $("#six").hide();
        $("#seven").show();
        $("#eight").show();
        $("#one").hide();
        $("#two").hide();
        $("#three").hide();
        $("#four").hide();
    }
    else {
        $("#one").show();
        $("#two").show();
        $("#three").show();
        $("#four").show();
    }
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "UpdateApproval",
            data: {},
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    window.parent.frames["iframeRight"].reload1();
                    // window.parent.frames["iframeRight"].reload();
                    setTimeout('parent.ClosePop()', 100);
                    //window.parent.ClosePop();
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
        var Opinion = document.getElementById("Opinion").value;
        if ($('input:radio[name="IsReturn"]:checked').val() == null) {
            alert("请选择是否通过审批");
            return;

        }
        if ($('input:radio[name="IsReturn"]:checked').val() == "否" && (Opinion == "" || Opinion == null)) {
            alert("审批意见不能为空"); return;
        }
        var option = $("#Opinion").val().length;
        if (option >= 100) {
            alert("审批意见不能超过100个字符");
            return;
        }
        //var Remark = $("#Remark").val().length;
        //if (Remark >= 100) {
        //    alert("备注不能超过100个字符");
        //    return;
        //}
        var a = confirm("确定保存审批记录吗")
        if (a == false)
            return;
        else {
            $("#IsPass").val($('input:radio[name="IsReturn"]:checked').val());
            $("#ProjectformInfo").submit();
        }
    })
})