$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        //window.parent.frames["iframeRight"].reloadPSer();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#UPCertify").click(function () {
        var IsPlan = document.getElementById("Isplan").value;
        var CType = document.getElementById("Ctype").value;
        var Cname = document.getElementById("Cname").value;
        var Ccode = document.getElementById("Ccode").value;
        var Corganization = document.getElementById("Corganization").value;
        var Cdate = document.getElementById("Cdate").value;
        var Createuser = document.getElementById("Createuser").value;
        var FID = document.getElementById("FID").value;
        var Createtime = document.getElementById("Createtime").value;
        var Validate = document.getElementById("Validate").value;
        var FileName = $("#UploadFile").val();
        if (FileName.replace(/(\s*$)/g, '') != "") {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf('xlsx') < 0 && filetype.toLowerCase().indexOf('docx') < 0 && filetype.toLowerCase().indexOf('pic') < 0 && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('doc') < 0) {
                alert("不支持该类型文件的上传，请上传word,pic,excel格式文件");
                return;
            }
        }
        var one = confirm("确定要更新证书吗？");
        if (one) {
            document.forms[0].submit();
        } else { return; }
    })
})