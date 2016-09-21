var id;
$(document).ready(function () {
    if (location.search != "") {
        id = location.search.split('&')[0].split('=')[1];
    }
    $("#btnSave").click(function () {
        var XJID = $("#XJID").val();
        var CID = id;
        var OrderNumber = $("#OrderNumber").val();
        //var OrderUnit = $("#OrderUnit").val();
        var InquiryTitle = $("#InquiryTitle").val();
        var OrderContacts = $("#OrderContacts").val();
        var Approver1 = $("#Approver1").val();
        var Approver2 = $("#Approver2").val();
        var State = $("#State").val();
        var BusinessTypes = $("#BusinessTypes").val();
        var InquiryExplain = $("#InquiryExplain").val();
        var InquiryDate = $("#InquiryDate").val();
        var DeliveryLimit = $("#DeliveryLimit").val();
        var DeliveryMethod = $("#DeliveryMethod").val();
        var IsInvoice = $("#IsInvoice").val();
        var PaymentMethod = $("#PaymentMethod").val();
        var PaymentAgreement = $("#PaymentAgreement").val();
        var TotalTax = $("#TotalTax").val();
        var TotalNoTax = $("#TotalNoTax").val();
        $.ajax({
            url: "insertinquiry",
            type: "Post",
            data: {
                XJID: XJID, CID: CID, OrderNumber: OrderNumber,  InquiryTitle: InquiryTitle, OrderContacts: OrderContacts, Approver1: Approver1,
                Approver2: Approver2, State: State, BusinessTypes: BusinessTypes, InquiryExplain: InquiryExplain, InquiryDate: InquiryDate, DeliveryLimit: DeliveryLimit,
                DeliveryMethod: DeliveryMethod, IsInvoice: IsInvoice, PaymentMethod: PaymentMethod, PaymentAgreement: PaymentAgreement, TotalTax: TotalTax, TotalNoTax: TotalNoTax
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