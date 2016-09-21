$(document).ready(function () {
    $("#hole").height($(window).height());
    $("#ProjectformInfo").submit(function () {
        var options = {
            url: "UpRativeSource",
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
                    alert(data.Msg);
                }
            }
        };
        $("#ProjectformInfo").ajaxSubmit(options);
        return false;
    })

    $("#charge").click(function () {
        if ($("#StrRID").val() == "") {
            alert("内部编号不能为空");
            return;
        }
        if ($("#StrEquipID").val() == "") {
            alert("设备编号不能为空");
            return;
        }
        if ($("#StrProModel").val() == "") {
            alert("产品型号不能为空");
            return;
        }
        if ($("#StrSource").val() == "") {
            alert("源项不能为空");
            return;
        }
        if ($("#StrManufacturer").val() == "") {
            alert("生产厂家不能为空");
            return;
        }
        if ($("#StrNominal").val() == "") {
            alert("标称活度不能为空");
            return;
        }
        if ($("#StrSourceNumber").val() == "") {
            alert("放射源编码不能为空");
            return;
        }
        var a = confirm("确定要修改放射源吗")
        if (a == false)
            return;
        else {
            $("#ProjectformInfo").submit();
        }
    })
})