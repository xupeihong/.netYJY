$(document).ready(function () {
    //退货完成
    $("#btnSaveExChangeFinish").click(function () {
        SaveExChangeFinish();
    });

    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});
function SaveExChangeFinish()
{
    var EID=$("#EID").val();
    var OrderID=$("#OrderID").val();
    var ExFinishDate = $("#ExFinishDate").val();
    var ExFinishDescription = $("#ExFinishDescription").val();
    var ExFinishDealPeo = $("#ExFinishDealPeo").val();
    $.ajax({
        url: "SaveExchangeGoodsFinish",
        type: "Post",
        data: {
            EID: EID, OrderID: OrderID, ExFinishDate: ExFinishDate, ExFinishDescription: ExFinishDescription, ExFinishDealPeo: ExFinishDealPeo
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("退货完成！");
                window.parent.ClosePop();
            }
            else {
                alert("退货失败-" + data.msg);
            }
        }
    });
}
