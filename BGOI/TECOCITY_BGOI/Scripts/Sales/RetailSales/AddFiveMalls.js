$(document).ready(function () {
    $("#btnSave").click(function () {
        var HigherUnitID = $("#HigherUnitID").val();
        var Malls = $("#Malls").val();

        $.ajax({
            url: "InsertMalls",
            type: "post",
            data: { HigherUnitID: HigherUnitID, Malls: Malls },
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