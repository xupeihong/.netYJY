
$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#btnSave").click(function () {
       
        var IsHouseID = $("input[name='IsHouseID']:checked").val();
        var HouseID = $("#HouseID").val();
        var Adress = $("#Adress").val();
        var HouseName = $("#HouseName").val();
        var UnitID = $("#UnitID").val();
        //var UnitIDnew = $("#UnitIDnew").val();
        var UnitIDnew = $("#UnitIDo").val();
        if (UnitIDnew.indexOf(".49.") > 0) {
            var TypeID = 1;
        } else {
            var TypeID = $("#TypeID").val();
        }

        if (HouseName == "" || HouseName == null) {
            alert("仓库名称不能为空");
            return;
        }
        $.ajax({
            url: "SaveInventoryAddFirstPage",
            type: "Post",
            data: {
                IsHouseID: IsHouseID, HouseID: HouseID, Adress: Adress, HouseName: HouseName, UnitID: UnitID, TypeID: TypeID
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("添加成功");
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
