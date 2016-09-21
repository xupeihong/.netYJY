
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    var msg = $("#msg").val();
    var TFileName = $("#FileName").val();
    if (msg == "上传已完成") {
        alert(msg);
        window.parent.frames["iframeRight"].reload();
        setTimeout('parent.ClosePop()', 10);
    }
    else if ( msg == "上传失败") {
        alert(msg);
    }

    // 上传按钮
    $("#charge").click(function () {
        var FileName = $("#UploadFile").val();
        if (FileName.replace(/(\s*$)/g, '') != "") {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('xls') < 0
                && filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('png') < 0) {
                alert("不支持该类型文件的上传，请上传doc,xl,jpg,png格式文件");
                return;
            }
        }
        isConfirm = confirm("确定要保存吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            document.forms[0].submit();
        }
    })
})

function returnConfirm() {
    return isConfirm;
}

