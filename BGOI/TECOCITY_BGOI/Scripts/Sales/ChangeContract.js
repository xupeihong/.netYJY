var isConfirm = false;
$(document).ready(function () {
    $("#hole").height($(window).height());

    $("#charge").click(function () {
        isConfirm = confirm("确定要变更合同信息吗")
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
        if (type == "BT5") {
            ShowIframe1("工程项目", "BGOI_Project", 750, 450, '')
        }
        if (type == "BT1") {
            ShowIframe1("科安特检测业务", "BGOI_KAT", 700, 450, '')
        }
        if (type == "BT2") {
            ShowIframe1("四检所检测业务", "BGOI_SJS", 700, 450, '')
        }
    })
})


function returnConfirm() {
    return false;
}

function submitInfo() {
    var options = {
        url: "UpdateContract",
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