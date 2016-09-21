$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "UpNewCheckInfo",
            data: {},
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    parent.reload();
                    window.parent.parent.frames["iframeRight"].reload();
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
        if ($("#StrCheckDate").val() == "") {
            alert("检定日期不能为空");
            return;
        }
        if ($("#StrCheckWay").val() == "") {
            alert("请选择检定方式");
            return;
        }
        if ($("#StrCheckCompany").val() == "") {
            alert("检定/校准单位不能为空");
            return;
        }
        if ($("#StrCharge").val() == 0) {
            alert("费用不能0");
            return;
        }
        if (isNaN($("#StrCharge").val())) {
            alert("费用请填写数字");
            return;
        }
        var a = confirm("确定要修改记录吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})