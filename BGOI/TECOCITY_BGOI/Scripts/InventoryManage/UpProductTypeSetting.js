
$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#btnSave").click(function () {
        var OID = $("#OID").val();
        var ID = $("#ID").val();
        var Text = $("#Text").val();
        if (Text == "" || Text == null) {
            alert("产品类型名称不能为空");
            return;
        }
        var type = 2;
        $.ajax({
            url: "SaveAddProductTypeSetting",
            type: "Post",
            data: {
                OID: OID, ID: ID, Text: Text, type: type
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("修改成功");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert(data.Msg);
                }
            }
        });
    });
});
