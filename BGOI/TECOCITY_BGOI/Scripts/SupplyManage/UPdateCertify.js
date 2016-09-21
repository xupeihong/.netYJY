$(document).ready(function () {
    loadFile();
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadPSer();
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
        //var Cfilename = document.getElementById("Cfilename").value;
        //var FileType = document.getElementById("FileType").value;
        var Createuser = document.getElementById("Createuser").value;
        var FID = document.getElementById("FID").value;
        var Createtime = document.getElementById("Createtime").value;
        var Validate = document.getElementById("Validate").value;
        var FileName = $("#UploadFile").val();
        if (FileName.replace(/(\s*$)/g, '') != "") {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf('xlsx') < 0 && filetype.toLowerCase().indexOf('docx') < 0 && filetype.toLowerCase().indexOf('pic') < 0 && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('png') < 0 && filetype.toLowerCase().indexOf('gif') < 0) {
                alert("不支持该类型文件的上传，请上传word,pic,excel格式文件");
                return;
            }
        }
        var one = confirm("确定要更新证书吗？");
        if (one) {
            // $("input[name='Isplan']:checked").val();
            document.forms[0].submit();
        } else { return; }
    })
})
function loadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("Sid").value;
    //var filename = document.getElementById("Cfilename").value;
    var fid = $("#FID").val();
    var cfilename = $("#Cfilename").val();
    $.ajax({
        url: "GetCertifi",
        type: "post",
        data: { data1: InforNo, fid: fid, CFilename: cfilename },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var id = data.id.split('@');
                var Code = data.File.split('@');
                var Name = data.Name.split('@');
                var Type = data.Type.split('@');
                var Banding = document.getElementById("unit");
                if (Code == "") {
                    Banding.innerHTML = "";
                    return;
                }
                for (var i = 0; i < Code.length; i++) {
                    var cross = id[i] + "/" + Name[i] + "/" + Code[i] + "/" + Type[i];
                    Banding.innerHTML += Name[i] + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='deleteCerty(\"" + cross + "\")'>删除</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='DownLoadCerty(\"" + cross + "\")'>下载</a><br/>";
                }
            }
            else {
                alert(data.Msg);
                return;
            }
        }
    });


}
function DownLoadCerty(id) {
    window.open("DownLoadCerty?id=" + id);
}
function deleteCerty(id) {
    var one = confirm("确实要删除该文件吗？")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deleteCerty",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    //loadFile();
                    window.parent.frames["iframeRight"].reloadPSer();
                    document.getElementById("unit").innerHTML = "";
                    $("#Cfilename").val("");
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
    }
}