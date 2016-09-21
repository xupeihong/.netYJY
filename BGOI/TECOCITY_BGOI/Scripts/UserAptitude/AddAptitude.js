$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        window.parent.frames["iframeRight"].reloadU();
        alert(msg);
        setTimeout('parent.ClosePop()', 100);
    }
    $("#hole").height($(window).height());

    $("#charge").click(function () {
        if ($("#StrUserID").val() == "") {
            alert("人员id不能为空,请点击‘选择人员’进行操作");
            return;
        }
        if ($("#StrBusinessType").val() == "") {
            alert("业务类型不能为空");
            return;
        }
        if ($("#StrTecoClass").val() == "") {
            alert("请选择证书级别");
            return;
        }
        if ($("#StrGetTime").val() == "") {
            alert("取得资格时间不能为空");
            return;
        }
        if ($("#StrCertificatCode").val() == "") {
            alert("技术等级证书编号不能为空");
            return;
        }
        if ($("#StrCertificateName").val() == "") {
            alert("证书名称不能为空");
            return;
        }
        if ($("#StrLastCertificatDate").val() == "") {
            alert("最新批准日期不能为空");
            return;
        }
        if ($("#StrCertificatDate").val() == "") {
            alert("证书到期日期不能为空");
            return;
        }
        var FileName = $("#UploadFile").val();
        if (FileName.replace(/(\s*$)/g, '') != "") {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('pdf') < 0 && filetype.toLowerCase().indexOf('png') < 0) {
                alert("不支持该类型文件的上传，请上传jpg,pdf,png格式文件");
                return;
            }
        }
        var a = confirm("确定保存人员资质信息吗")
        if (a == false)
            return;
        else {
            document.forms[0].submit();
        }
    })

    $("#XZRY").click(function () {
        ShowIframe1("选择人员", "ChooseUser", 400, 450, '')
    })
})