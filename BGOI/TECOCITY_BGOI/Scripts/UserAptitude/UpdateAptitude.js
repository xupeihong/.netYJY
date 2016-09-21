$(document).ready(function () {
    loadFile();
    var msg = $("#msg").val();
    if (msg != "") {
        window.parent.frames["iframeRight"].reload();
        alert(msg);
        setTimeout('parent.ClosePop()', 100);
    }
    $("#hole").height($(window).height());
    $("#charge").click(function () {
        if ($("#StrUserID").val() == "") {
            alert("人员id不能为空");
            return;
        }
        if ($("#StrBusinessType").val() == "") {
            alert("业务类型不能为空");
            return;
        }
        if ($("#StrTecoClass").val() == "") {
            alert("请选择证书级别级别");
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
            if (filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('pdf') < 0) {
                alert("不支持该类型文件的上传，请上传jpg,pdf格式文件");
                return;
            }
        }
        var a = confirm("确定更新人员资质信息吗")
        if (a == false)
            return;
        else {
            document.forms[0].submit();
        }
    })
})

function loadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("ID").value;
    $.ajax({
        url: "GetFile",
        type: "post",
        data: { data1: InforNo },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var id = data.id.split('@');
                var Code = data.File.split('@');
                var Name = data.Name.split('@');
                var Banding = document.getElementById("unit");
                if (Code == "") {
                    Banding.innerHTML = "";
                    return;
                }
                for (var i = 0; i < Code.length; i++) {
                    var cross = id[i] + "/" + Name[i] + "/" + Code[i];
                    if (Name[i] != "")
                        Banding.innerHTML += Name[i] + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='deleteFile(\"" + cross + "\")'>删除</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='DownloadFile(\"" + cross + "\")'>下载</a><br/>";
                }
            }
            else {
                alert(data.Msg);
                return;
            }
        }
    });
}

function DownloadFile(id) {
    window.open("DownLoad?id=" + id);
}

function deleteFile(id) {
    var one = confirm("确实要删除这条条目吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deleteFile",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    loadFile();
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
    }
}