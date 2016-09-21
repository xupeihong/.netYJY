$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadPSer();
        //window.parent.frames["iframeRight"].reload();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#sure1").click(function () {
        if ($("#Isplan").val() == "") {
            alert("是否是计划性认证证书"); return;
        }
        if ($("#Ctype").val() == "") {
            alert("证书类型不能为空"); return;
        }
        if ($("#Cname").val() == "") {
            alert("证书名称不能为空"); return;
        }
        if ($("#Ccode").val() == "") {
            alert("证书编号不能为空"); return;
        }
        if ($("#Corganization").val() == "") {
            alert("证书认证机构不能为空"); return;
        }
        if ($("#Cdate").val() == "") {
            alert("通过认证时间不能为空"); return;
        }
        if ($("#FileType").val() == "") {
            alert("文档类型不能为空"); return;
        }
        
        var FileName = $("#UploadCertifi").val();
        var Isplan = $("input[name='Isplan']:checked").val();
        if (FileName.replace(/(\s*$)/g, '') != "") {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf('xlsx') < 0 && filetype.toLowerCase().indexOf('docx') < 0 && filetype.toLowerCase().indexOf('pic') < 0 && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('png') < 0 && filetype.toLowerCase().indexOf('gif') < 0) {
                alert("不支持该类型文件的上传，请上传word,pic,excel,png,jpg,gif格式文件");
                return;
            }
        }
        var res = confirm("确定要保存证书信息吗？");
        if (res) {
            $("input[name='Isplan']:checked").val();
            document.forms[0].submit();
        }
        else {
            return;
        }
    })
});