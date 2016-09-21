
var isConfirm = false;
var i = 0;
var j = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    var ChangePlace = $("#StrChangePlace").val();
    if (ChangePlace == 0) {
        $("input[name=ChangePlace]:eq(0)").attr("checked", 'checked');
    }
    else if (ChangePlace == 1) {
        $("input[name=ChangePlace]:eq(1)").attr("checked", 'checked');
    }
    else {
        $("input[name=ChangePlace]:eq(2)").attr("checked", 'checked');
    }
    //
    var Check1 = $("#StrCheck1").val();
    if (Check1 == 0) {
        $("input[name=Check1]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check1]:eq(1)").attr("checked", 'checked');
    }
    var Check2 = $("#StrCheck2").val();
    if (Check2 == 0) {
        $("input[name=Check2]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check2]:eq(1)").attr("checked", 'checked');
    }
    var Check3 = $("#StrCheck3").val();
    if (Check3 == 0) {
        $("input[name=Check3]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check3]:eq(1)").attr("checked", 'checked');
    }
    var Check4 = $("#StrCheck4").val();
    if (Check4 == 0) {
        $("input[name=Check4]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check4]:eq(1)").attr("checked", 'checked');
    }
    var Check5 = $("#StrCheck5").val();
    if (Check5 == 0) {
        $("input[name=Check5]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check5]:eq(1)").attr("checked", 'checked');
    }
    var Check6 = $("#StrCheck6").val();
    if (Check6 == 0) {
        $("input[name=Check6]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check6]:eq(1)").attr("checked", 'checked');
    }
    var Check7 = $("#StrCheck7").val();
    if (Check7 == 0) {
        $("input[name=Check7]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check7]:eq(1)").attr("checked", 'checked');
    }
    var Check8 = $("#StrCheck8").val();
    if (Check8 == 0) {
        $("input[name=Check8]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check8]:eq(1)").attr("checked", 'checked');
    }
    //
    var RepairContent1 = $("#StrRepairContent1").val();
    if (RepairContent1 == 0) {
        $("input[name=RepairContent1]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=RepairContent1]:eq(1)").attr("checked", 'checked');
    }
    var RepairContent2 = $("#StrRepairContent2").val();
    if (RepairContent2 == 0) {
        $("input[name=RepairContent2]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=RepairContent2]:eq(1)").attr("checked", 'checked');
    }
    var RepairContent3 = $("#StrRepairContent3").val();
    if (RepairContent3 == 0) {
        $("input[name=RepairContent3]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=RepairContent3]:eq(1)").attr("checked", 'checked');
    }

    // 确定修改
    $("#QRXG").click(function () {
        isConfirm = confirm("确定要修改随工单吗")
        if (isConfirm == false) {
            return false;
        }
        else {

            $("#StrChangePlace").val($('input:radio[name="ChangePlace"]:checked').val());
            $("#StrCheck1").val($('input:radio[name="Check1"]:checked').val());
            $("#StrCheck2").val($('input:radio[name="Check2"]:checked').val());
            $("#StrCheck3").val($('input:radio[name="Check3"]:checked').val());
            $("#StrCheck4").val($('input:radio[name="Check4"]:checked').val());
            $("#StrCheck5").val($('input:radio[name="Check5"]:checked').val());
            $("#StrCheck6").val($('input:radio[name="Check6"]:checked').val());
            $("#StrCheck7").val($('input:radio[name="Check7"]:checked').val());
            $("#StrCheck8").val($('input:radio[name="Check8"]:checked').val());

            $("#StrRepairContent1").val($('input:radio[name="RepairContent1"]:checked').val());
            $("#StrRepairContent2").val($('input:radio[name="RepairContent2"]:checked').val());
            $("#StrRepairContent3").val($('input:radio[name="RepairContent3"]:checked').val());

            submitInfo();
        }
    });

})

// 界面提交
function submitInfo() {
    var options = {
        url: "UpdateWorkCardUT",
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
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

function returnConfirm() {
    return false;
}
