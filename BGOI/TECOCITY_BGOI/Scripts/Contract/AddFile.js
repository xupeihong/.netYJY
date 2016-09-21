$(document).ready(function () {
    loadFile();
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        //document.getElementById("unit").innerHTML = "";
        //loadFile();
    }
    $("#hole").height($(window).height());
    $("#upLoad").click(function () {
        var FileName = $("#UploadFile").val();
        if (FileName.replace(/(\s*$)/g, '') == "") {
            alert("请选择要上传的文件");
            return;
        }
        var one = confirm("确定上传文件吗");
        if (one == false)
            return;
        else {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('pdf') < 0 && filetype.toLowerCase().indexOf('doc') < 0) {
                alert("不支持该类型文件的上传，请上传jpg,doc,pdf格式文件");
                return;
            }
            document.forms[0].submit();
        }
    })
})

function loadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("StrCID").value;
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