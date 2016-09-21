$(document).ready(function () {
    if (location.search != "") {
        CID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "UpdatePurchaseGoods1",
        type: "post",
        data: { CID: CID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
                $("#OrderContent").val(json[0].ordercontent);
                $("#Specifications").val(json[0].specifications);
                $("#Unit").val(json[0].unit);
                $("#Amount").val(json[0].amount);
                $("#UnitpriceNoTax").val(json[0].unitpricenotax);
                $("#TotalNoTax").val(json[0].totalnotax);
                $("#Use").val(json[0].use);
                $("#Remark").val(json[0].remark);
                $("#PleaseExplain").val(json[0].pleaseexplain);
                $("#PurchaseDate").val(json[0].purchaseDate);
                $("#PleaseDate").val(json[0].pleasedate);
                $("#OrderContacts").val(json[0].ordercontacts);
        }
    });
    $("#btnSave").click(function () {
 
        var CID = location.search.split('&')[0].split('=')[1];
        var OrderContent = $("#OrderContent").val();
        var Specifications = $("#Specifications").val();
        var Unit = $("#Unit").val();
        var Amount = $("#Amount").val();
        var UnitpriceNoTax = $("#UnitpriceNoTax").val();
        var TotalNoTax = $("#TotalNoTax").val();
        var Use = $("#Use").val();
        var Remark = $("#Remark").val();
        var PleaseExplain = $("#PleaseExplain").val();
        var PurchaseDate = $("#PurchaseDate").val();
        var PleaseDate = $("#PleaseDate").val();
        var OrderContacts = $("#OrderContacts").val();


        $.ajax({
            url: "Updates",
            type: "Post",
            data: {
                CID: CID, OrderContent: OrderContent, Specifications: Specifications, Unit: Unit, Amount: Amount, UnitpriceNoTax: UnitpriceNoTax,
                TotalNoTax: TotalNoTax, Use: Use, Remark: Remark, PleaseExplain: PleaseExplain, PurchaseDate: PurchaseDate, PleaseDate: PleaseDate,
                OrderContacts: OrderContacts
            },

            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("成功");
                }
                else {
                    alert("失败");
                }
            }
        });
    });
});