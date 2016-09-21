$(document).ready(function () {
    var OId = $("#OId").val();
    $.ajax({
        url: "GetFilesNew",
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
            //var regex = /\\/g;
            //var str = data.Msg.replace(regex, "/");
            for (var i = 0; i < items.length; i++) {
            img += '<img src="../' + str + '" style="width:400px"/>';
            }
            $("#Content").html(img);
        }
    });



})