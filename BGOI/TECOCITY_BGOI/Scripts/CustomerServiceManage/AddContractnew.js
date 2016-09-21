var isConfirm = false;
$(document).ready(function () {
    $("#hole").height($(window).height());

    $("#charge").click(function () {
        isConfirm = confirm("确定要保存合同信息吗")
        if (isConfirm == false) {
            return false;
        }
        else
            submitInfo();
    })

    $("#XZXM").click(function () {
        var type = $("#StrBusinessType").val();
        if (type == "") {
            alert("请选择业务类型");
            return;
        }
        if (type == "BT8") {
            ShowIframe1("售后调压箱代管业务", "BGOI_TPRBE", 750, 450, '')
        }
        if (type == "BT9") {
            ShowIframe1("售后调压箱维保业务", "BGOI_STM", 700, 450, '')
        }
        if (type == "BT10") {
            ShowIframe1("售后调压箱技改、维修业务", "BGOI_TTM", 700, 450, '')
        }
    })
})


function returnConfirm() {
    return false;
}

function submitInfo() {
    var options = {
        url: "InsertContractBasnew",
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
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}