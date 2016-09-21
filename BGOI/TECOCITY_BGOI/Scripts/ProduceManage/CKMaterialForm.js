$(document).ready(function () {
    var OId = $("#OId").val();

    $.ajax({
        url: "GetFiles",
        type: "post",
        data: { OId: OId },
        dataType: "json",
        async: false,
        success: function (data) {

            var items = eval(data);
            //alert(items.length+data)
            if (items.length <= 0) {
                alert("无图片可显示，请先上传图片！");
                setTimeout('parent.ClosePop();', 100);
                return;
            }
            var img = "";
            for (var i = 0; i < items.length; i++) {
                img += '<img src="../' + items[i].FilePath + '" style="width:400px"/>';
            }
            $("#Content").html(img);
        }
    });



})