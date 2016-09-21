$(document).ready(function () {
    $("#btnSave").click(function () {
        var TypeId = $("#Type").val();
        var TextDesc = $("#Text").val();

        $.ajax({
            url: "InsertBasicGrid",
            type: "post",
            data: { TypeId: TypeId, TextDesc: TextDesc },
            dataType: "Json",
            success: function (data) {
                if (data.success == true) {
                    window.parent.frames["iframeRight"].reload();
                    alert("添加完成！");
                    setTimeout('parent.ClosePop()', 100);
                }
                else {
                    alert("添加失败！");
                    return;
                }
            }
        });
    });
});