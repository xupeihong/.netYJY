
$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#btnSave").click(function () {
        var Spec = $("#Spec").val();
        var GGID = $("#GGID").val();
        if (Spec == "" || Spec == null) {
            alert("规格内容不能为空");
            return;
        }
        $.ajax({
            url: "SaveAddSpec",
            type: "Post",
            data: {
                Spec: Spec, GGID: GGID
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("操作成功");
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

