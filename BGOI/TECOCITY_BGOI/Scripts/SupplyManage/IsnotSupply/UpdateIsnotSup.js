$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#updateCustomer").submit(function () {
        var options = {
            url: "UpdateIsok",
            data: {},
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    window.parent.frames["iframeRight"].reload();
                    window.parent.ClosePop();
                }
                else {
                    alert(data.Msg);
                }
            }
        };
        $("#updateCustomer").ajaxSubmit(options);
        return false;
    })
    $("#UPCustome").click(function () {
        var COMNameC = document.getElementById("COMNameC").value;
        if (COMNameC == "") {
            alert("客户时间不能为空");
            return;
        }

        var res = confirm("确定修改非合格供应商信息吗？");
        if (res) {
            $("#updateCustomer").submit();

        } else {
            return;
        }
    });

})