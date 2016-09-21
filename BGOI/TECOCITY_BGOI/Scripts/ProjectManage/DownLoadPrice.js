﻿
$(document).ready(function () {
    loadFile();
})


function loadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("oid").value;
    $.ajax({
        url: "GetPriceFile",
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
                    Banding.innerHTML += Name[i] + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:pointer;' onclick='DownloadFile(\"" + id[i] + "\")'>下载</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<br/>";
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
    window.open("DownLoadPriceFile?id=" + id);
}
