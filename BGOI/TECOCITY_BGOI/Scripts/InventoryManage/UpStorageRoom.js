
$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    jq();
    $("#btnSave").click(function () {
        var IsHouseID = $("input[name='IsHouseID']:checked").val();
        var HouseID = $("#HouseID").val();
        var Adress = $("#Adress").val();
        var HouseName = $("#HouseName").val();
        var UnitID = $("#UnitID").val();
        //var UnitIDnew = $("#UnitIDnew").val();
        var UnitIDnew = $("#UnitIDo").val();
        var TypeID = $("#TypeID").val();
        if (HouseName == "" || HouseName == null) {
            alert("仓库名称不能为空");
            return;
        }
        $.ajax({
            url: "SaveUpStorageRoom",
            type: "Post",
            data: {
                IsHouseID: IsHouseID, HouseID: HouseID, Adress: Adress, HouseName: HouseName, UnitID: UnitID, TypeID: TypeID
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
function jq() {
    if (location.search != "") {
        HouseID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "UpUpStorageRoom",
        type: "Post",
        data: {
            HouseID: HouseID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        $("#UnitID option:contains('" + json[i].UnitID + "')").prop('selected', true);
                        $("#TypeID option:contains('" + json[i].TypeID + "')").prop('selected', true);
                        if (json[i].IsHouseID == "1") {
                            $(':radio[name=IsHouseID][value=1]').attr('checked', true);
                        } else {
                            $(':radio[name=IsHouseID][value=2]').attr('checked', true);
                        }

                    }
                }
            }
            else {
                alert(data.Msg);
            }
        }
    });
}
