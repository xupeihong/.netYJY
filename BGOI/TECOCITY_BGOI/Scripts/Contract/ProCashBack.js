var isConfirm = false;
var type;
$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#charge").click(function () {

        $.ajax({
            url: "CheckMoney",
            type: "post",
            data: { data1: $("#StrCID").val(), data2: $("#StrCBMoney").val() },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    if ($('input:radio[name="IsReturn"]:checked').val() == null) {
                        alert("请选择是否符合约定的回款进度");
                        return;

                    }
                    isConfirm = confirm("确定保存回款记录吗")
                    if (isConfirm == false) {
                        return false;
                    }
                    else
                        submitInfo();
                }
                else {
                    alert("回款金额已超过合同额，不能进行回款");
                    return;
                }
            }
        });
    })

})

function returnConfirm() {
    return false;
}

function submitInfo() {
    $("#StrIsReturn").val($('input:radio[name="IsReturn"]:checked').val());
    type = $("#type").val();
    var options = {
        url: "InsertProCCashBack",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                if (type == "project") {
                    window.parent.frames["iframeRight"].reload7();
                }
                else {
                    window.parent.frames["iframeRight"].reload();
                    window.parent.frames["iframeRight"].reload1();
                }
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

function checkmoney()
{
}
