

$(document).ready(function () {

})

function DownLoadFile(id, filename) {
    if (filename == "") {
        alert("没有文件可以查看"); return;
    }
    else {
        window.open("../SuppliesManage/DownLoadNewFile?sid=" + id);
    }
}
function DownLoadAwards(id, name) {
    if (name == "") {
        alert("没有文件可以查看"); return;
    }
    else {
        window.open("../SuppliesManage/DownLoadAward?sid=" + id);
    }
}
function DownLoadjiage(id, name) {
    if (name == "") {
        alert("没有文件可以查看"); return;
    }
    else {
        window.open("../SuppliesManage/DownLoadPrice?sid=" + id);
    }
}
function DownLoadCertify(id, name) {
    if (name == "") {
        alert("没有文件可以查看"); return;
    }
    else {
        window.open("../SuppliesManage/DownLoadCertify?sid=" + id);
    }
}
function DownLoadproduct(id, name) {
    if (name == "") {
        alert("没有文件可以查看"); return;
    }
    else {
        window.open("../SuppliesManage/DownLoadProduct?id=" + id);
    }
}
function DownLoadserver(id, name) {
    if (name == "") {
        alert("没有文件可以查看"); return;
    }
    else {
        window.open("../SuppliesManage/DownLoadServer?id=" + id);
    }
}