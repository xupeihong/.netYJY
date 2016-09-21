
//$(document).ready(function () {
//    $("#Floor").height($(window).height());

$(document).ready(function () {
    $("#btnRecord").click(function () {
        window.parent.OpenDialog("新建请购单", "../PPManage/AddPurchase", 700, 580, '');
    });
});