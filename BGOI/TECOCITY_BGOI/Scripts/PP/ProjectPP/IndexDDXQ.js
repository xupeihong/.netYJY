

$(document).ready(function () {
    alert("-1");
    if (location.search != "") {
        alert(1);
        DDID = location.search.split('&')[0].split('=')[1];
        alert(DDID);
        addBasicDetail();
    }
});

function addBasicDetail() { //增加货品信息行
    DDID = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "DDXQ",
        type: "post",
        data: { DDID: DDID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);

            var html = "";
            html = "<tr>";
            html = '<td ><lable class="labSpecifications" id="DDID">' + json[0].DDID + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="OrderUnit">' + json[0].OrderUnit + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="OrderContacts">' + json[0].OrderContacts + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Approver1">' + json[0].Approver1 + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Approver2">' + json[0].Approver2 + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="PayStatus">' + json[0].PayStatus + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="State">' + json[0].State + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="BusinessTypes">' + json[0].BusinessTypes + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="PleaseExplain">' + json[0].PleaseExplain + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="OrderDate">' + json[0].OrderDate + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="DeliveryLimit">' + json[0].DeliveryLimit + '</lable> </td>';
            html += '</tr>'
            $("#GXInfo").append(html);

            var html = "";
            html = "<tr>";
            html = '<td ><lable class="labSpecifications" id="DeliveryMethod">' + json[0].DeliveryMethod + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="IsInvoice">' + json[0].IsInvoice + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="PaymentMethod">' + json[0].PaymentMethod + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="PaymentAgreement">' + json[0].PaymentAgreement + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="ContractNO">' + json[0].ContractNO + '</lable> </td>';

            html += '<td ><lable class="labSpecifications" id="TheProject">' + json[0].TheProject + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="TotalTax">' + json[0].TotalTax + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="TotalNoTax">' + json[0].TotalNoTax + '</lable> </td>';
            html += '</tr>'
            $("#GXInfo1").append(html);


            var html = "";
            html = "<tr>";
            html += '<td ><lable class="labSpecifications" id="OrderContent">' + json[0].OrderContent + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Specifications">' + json[0].Specifications + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Supplier">' + json[0].Supplier + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Unit">' + json[0].Unit + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Amount">' + json[0].Amount + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="UnitPriceNoTax">' + json[0].UnitPriceNoTax + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="TotalNoTax">' + json[0].TotalNoTax + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="UnitPrice">' + json[0].UnitPrice + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Total ">' + json[0].Total + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Rate">' + json[0].Rate + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Use">' + json[0].Use + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Remark">' + json[0].Remark + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="PurchaseDate">' + json[0].PurchaseDate + '</lable> </td>';
            html += '</tr>'
            $("#GXInfo2").append(html);
        }

    })
}