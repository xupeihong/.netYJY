$(document).ready(function () {
    loadFile();
    $("#hole").height($(window).height());

})

function loadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("StrCID").value;
    $.ajax({
        url: "GetFileProject",
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
                    Banding.innerHTML += Name[i] + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='DownloadFile(\"" + cross + "\")'>下载</a><br/>";
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
    window.open("DownLoadProject?id=" + id);
}