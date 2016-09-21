$(document).ready(function () {
    $("#btnSave").click(function () {
        var ID = $("#ID").val();
        var HigherUnitID = $("#HigherUnitID").val();
        var Text = $("#Text").val();

        $.ajax({
            url: "AlterMallsInfo",
            type: "post",
            data: { ID: ID, HigherUnitID: HigherUnitID, Text: Text },
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
})