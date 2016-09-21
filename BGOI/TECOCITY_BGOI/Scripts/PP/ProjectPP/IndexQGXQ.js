

$(document).ready(function () {
    alert("-1");
    if (location.search != "") {
        alert(1);
        CID = location.search.split('&')[0].split('=')[1];
        alert(CID);
        addBasicDetail();
    }
});

function addBasicDetail() { //增加货品信息行
    CID = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "QGXQ",
        type: "post",
        data: { CID: CID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            
                var html = "";
                html = "<tr>";
                html = '<td ><lable class="labSpecifications" id="CID">' + json[0].CID + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="OrderUnit">' + json[0].OrderUnit + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="OrderContacts">' + json[0].OrderContacts + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="Approver1">' + json[0].Approver1 + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="Approver2">' + json[0].Approver2 + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="State">' + json[0].Text + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="BusinessTypes">' + json[0].BusinessTypes + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="PleaseExplain">' + json[0].PleaseExplain + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="PleaseDate">' + json[0].PleaseDate + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="DeliveryDate">' + json[0].DeliveryDate + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="ExpectedTotal">' + json[0].ExpectedTotal + '</lable> </td>';
                html += '</tr>'
                $("#GXInfo").append(html);

                var html = "";
                html = "<tr>";
                html = '<td ><lable class="labSpecifications" id="CID">' + json[0].CID + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="OrderContent">' + json[0].OrderContent + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="Specifications">' + json[0].Specifications + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="Unit">' + json[0].Unit + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="Amount">' + json[0].Amount + '</lable> </td>';
                html += '</tr>'
                $("#GXInfo1").append(html);


                var html = "";
                html = "<tr>";
                html = '<td ><lable class="labSpecifications" id="UnitpriceNoTax">' + json[0].UnitpriceNoTax + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="TotalNoTax">' + json[0].TotalNoTax + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="Rate">' + json[0].Rate + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="Use">' + json[0].Use + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="Remark">' + json[0].Remark + '</lable> </td>';
                html += '<td ><lable class="labSpecifications" id="PurchaseDate">' + json[0].PurchaseDate + '</lable> </td>';
                html += '</tr>'
                $("#GXInfo2").append(html);
            }
       
    })
}