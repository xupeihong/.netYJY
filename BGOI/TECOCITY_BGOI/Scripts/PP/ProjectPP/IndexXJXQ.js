$(document).ready(function () {
    alert("-1");
    if (location.search != "") {
        alert(1);
        XJID = location.search.split('&')[0].split('=')[1];
        alert(XJID);
        addBasicDetail();
    }
});
function addBasicDetail() { //增加货品信息行
    XJID = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "SelectXJXQ",
        type: "post",
        data: { XJID: XJID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);

            var html = "";
            html = "<tr>";
            html = '<td ><lable class="labSpecifications" id="XJID">' + json[0].XJID + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="OrderUnit">' + json[0].OrderUnit + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="InquiryTitle">' + json[0].InquiryTitle + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="OrderContacts">' + json[0].OrderContacts + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Approver1">' + json[0].Approver1 + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Approver2">' + json[0].Approver2 + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="State">' + json[0].State + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="BusinessTypes">' + json[0].BusinessTypes + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="InquiryExplain">' + json[0].InquiryExplain + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="InquiryDate">' + json[0].InquiryDate + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="DeliveryLimit">' + json[0].DeliveryLimit + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="DeliveryMethod">' + json[0].DeliveryMethod + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="IsInvoice">' + json[0].IsInvoice + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="PaymentMethod">' + json[0].PaymentMethod + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="PaymentAgreement">' + json[0].PaymentAgreement + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="TotalTax">' + json[0].TotalTax + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="TotalNoTax">' + json[0].TotalNoTax + '</lable> </td>';
            html += '</tr>'
            $("#GXInfo").append(html);

            var html = "";
            html = "<tr>";
            html += '<td ><lable class="labSpecifications" id="OrderContent">' + json[0].OrderContent + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Specifications">' + json[0].Specifications + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Supplier">' + json[0].Supplier + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Unit">' + json[0].Unit + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Amount">' + json[0].Amount + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="NegotiatedPricingNoTax">' + json[0].NegotiatedPricingNoTax + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="TotalNegotiationNoTax">' + json[0].TotalNegotiationNoTax + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="UnitPriceTax">' + json[0].UnitPriceTax + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="TotalTax">' + json[0].TotalTax + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Rate">' + json[0].Rate + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Remark">' + json[0].Remark + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="PurchaseDate">' + json[0].PurchaseDate + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="ContractCondition">' + json[0].ContractCondition + '</lable> </td>';
            html += '</tr>'
            $("#GXInfo1").append(html);
        }

    })
}