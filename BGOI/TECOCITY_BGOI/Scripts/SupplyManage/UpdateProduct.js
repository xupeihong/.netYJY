$(document).ready(function () {
    loadFile();
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadpro();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());

    $("#UPProduct").click(function () {

        var FileName = $("#UploadFile").val();
        if (FileName.replace(/(\s*$)/g, '') != "") {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf('xlsx') < 0 && filetype.toLowerCase().indexOf('docx') < 0 && filetype.toLowerCase().indexOf('pic') < 0 && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('png') < 0 && filetype.toLowerCase().indexOf('gif') < 0) {
                alert("不支持该类型文件的上传，请上传word,pic,excel,jpg,png,gif格式文件");
                return;
            }
        }
        var one = confirm("确定要更新产品吗？");
        if (one) {

            document.forms[0].submit();
        } else {
            return;
        }
    });
})

function loadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("Sid").value;
    //var timeout = document.getElementById("BYTtime").value;//拟购时间
    var proid = $("#ID").val();
    var filename = $("#FFileName").val();
    $.ajax({
        url: "GetProduct",
        type: "post",
        data: { data1: InforNo, timeOut: proid, filename: filename },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var id = data.id.split('@');
                var Code = data.File.split('@');
                var Name = data.Name.split('@');
                // var Type = data.Type.split('@');
                var Banding = document.getElementById("unit");
                if (Code == "") {
                    Banding.innerHTML = "";
                    return;
                }
                for (var i = 0; i < Code.length; i++) {
                    var cross = id[i] + "/" + Name[i] + "/" + Code[i] + "/";
                    Banding.innerHTML += Name[i] + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='deleteProduct(\"" + cross + "\")'>删除</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='DownloadProduct(\"" + cross + "\")'>下载</a><br/>";
                }
            }
            else {
                alert(data.Msg);
                return;
            }
        }
    });
}
function DownloadProduct(id) {
    window.open("DownLoadProduct?id=" + id);
}
function deleteProduct(id) {
    var one = confirm("确实要删除这条条目吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deleteProduct",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    //loadFile();
                    window.parent.frames["iframeRight"].reloadpro();
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