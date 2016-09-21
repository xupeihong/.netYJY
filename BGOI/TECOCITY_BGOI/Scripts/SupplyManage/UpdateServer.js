$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadServer();
        window.parent.ClosePop();
        return;
    }
    loadFile();

    $("#hole").height($(window).height());

    $("#UPSer").click(function () {

        var FileName = $("#UploadFile").val();
        if (FileName.replace(/(\s*$)/g, '') != "") {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf('xlsx') < 0 && filetype.toLowerCase().indexOf('docx') < 0 && filetype.toLowerCase().indexOf('pic') < 0 && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('png') < 0 && filetype.toLowerCase().indexOf('gif') < 0) {
                alert("不支持该类型文件的上传，请上传word,pic,excel,jpg,png,gif格式文件");
                return;
            }
        }
        var one = confirm("确定要修改服务吗？");
        if (one) {

            document.forms[0].submit();
        } else { return; }
    })
})
function loadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("Sid").value;
    var filename = $("#FFileName").val();
    var id = $("#ServiceID").val();
    $.ajax({
        url: "GetServer",
        type: "post",
        data: { data1: InforNo, filename: filename, id: id },
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
                    var cross = id[i] + "/" + Name[i] + "/" + Code[i] + "/";
                    Banding.innerHTML += Name[i] + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='deleteServer(\"" + cross + "\")'>删除</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='DownloadServer(\"" + cross + "\")'>下载</a><br/>";
                }
            }
            else {
                alert(data.Msg);
                return;
            }
        }
    });
}
function DownloadServer(id) {
    window.open("DownLoadServer?id=" + id);
}
function deleteServer(id) {
    var one = confirm("确实要删除这条条目吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deleteServer",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    //loadFile();
                    window.parent.frames["iframeRight"].reloadServer();
                    document.getElementById("unit").innerHTML = "";
                    $("#FFileName").val("");
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
    }
}