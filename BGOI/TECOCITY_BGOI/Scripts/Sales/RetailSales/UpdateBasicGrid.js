$(document).ready(function () {
    $("#btnSave").click(function () {
        var TypeId = $("#Type").val();
        var TextDesc = $("#Text").val();
        var XID = $("#XID").val();

        $.ajax({
            url: "AlterBasicGrid",
            type: "post",
            data: { XID: XID, TypeId: TypeId, TextDesc: TextDesc },
            dataType: "Json",
            success: function (data) {
                if (data.success == true) {
                    window.parent.frames["iframeRight"].reload();
                    alert("修改完成！");
                    setTimeout('parent.ClosePop()', 100);
                }
                else {
                    alert("修改失败！");
                    return;
                }
            }
        });
    });
});