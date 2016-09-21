$(document).ready(function () {
    loadFile();
})
function loadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("Sid").value;
    var fid = $("#FID").val();//唯一编号
    var Mfilename = $("#FFileName").val();//资质证明文件名称

    $.ajax({
        url: "GetMFile",
        type: "post",
        data: { data1: InforNo },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var id = data.id.split('@');
                var Code = data.File.split('@');
                var Name = data.Name.split('@');
                var Banding = document.getElementById("unit");
                if (Code == "" && Name == "") {
                    Banding.innerHTML = "";
                    return;
                }
                for (var i = 0; i < Code.length; i++) {
                    var cross = id[i] + "/" + Name[i] + "/" + Code[i];
                    Banding.innerHTML += Name[i] + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='DownloadFile(\"" + cross + "\")'>下载</a><br/>";
                }//<a style='color:blue;cursor:hand;' onclick='deleteFile(\"" + cross + "\")'>删除</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            }
            else {
                alert(data.Msg);
                return;
            }
        }
    });


}
function DownloadFile(id) {
    window.open("DownLoadUnit?id=" + id);
}
function deleteFile(id) {
    var one = confirm("确实要删除这条条目吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deleteMFile",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    loadFile();
                    window.parent.frames["iframeRight"].reloadPlanPro();
                    // document.getElementById("unit").innerHTML = "";
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