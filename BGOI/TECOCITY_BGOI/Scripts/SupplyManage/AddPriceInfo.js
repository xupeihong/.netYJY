$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadPrice();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#sure1").click(function () {
        //var FileName = $("#UploadFile").val();
        //if (FileName.replace(/(\s*$)/g, '') != "") {
        //    var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
        //    if (filetype.toLowerCase().indexOf('xlsx') < 0 && filetype.toLowerCase().indexOf('docx') < 0 && filetype.toLowerCase().indexOf('pic') < 0 && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('png') < 0 && filetype.toLowerCase().indexOf('gif') < 0) {
        //        alert("不支持该类型文件的上传，请上传word,pic,excel,png,jpg,gif格式文件");
        //        return;
        //    }
        //}
        var res = confirm("确定要保存报价/比价单信息吗？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }
    });
})