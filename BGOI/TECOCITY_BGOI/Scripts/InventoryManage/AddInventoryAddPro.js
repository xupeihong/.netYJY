
$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#btnSave").click(function () {
        var PID = $("#PID").val();
        var MaterialNum = $("#MaterialNum").val();
        var Spec = $("#Spec").val();
        var ProName = $("#ProName").val();
        var ProTypeID = $("#ProTypeID").val();
        var HouseID = $("#HouseID").val();
        var Remark = $("#Remark").val();
        var FinishCount = $("#FinishCount").val();
        var UnitPrice = $("#UnitPrice").val();
        var Units = $("#Units").val();
        var Ptype = $("#Ptype").val();
        var Manufacturer = $("#Manufacturer").val();
        var Price2 = $("#Price2").val();
        var Detail = $("#Detail").val();
        if (PID == "" || PID == null) {
            alert("货品编号不能为空");
            return;
        }
        if (Ptype == "请选择" || Ptype == null) {
            alert("产品类型不能为空");
            return;
        }
        if (Manufacturer == "" || Manufacturer == null) {
            alert("厂家不能为空");
            return;
        }
        $.ajax({
            url: "SaveInventoryAddProNew",
            type: "Post",
            data: {
                PID: PID, MaterialNum: MaterialNum, Spec: Spec, ProName: ProName, ProTypeID: ProTypeID, UnitPrice: UnitPrice,
                Units: Units, Ptype: Ptype, Manufacturer: Manufacturer, Price2: Price2, Detail: Detail, Remark: Remark,
                FinishCount: FinishCount, HouseID: HouseID
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

