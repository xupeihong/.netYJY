
$(document).ready(function () {
    if (location.search != "") {
        DDID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "SelectGoodsDDID",
        type: "post",
        data: { DDID: DDID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="TotalNoTax' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="TotalNoTax' + rowCount + '">' + json[i].Total + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                
                }
                document.getElementById("DDID").innerHTML = location.search.split('&')[0].split('=')[1];
                document.getElementById("OrderDate").innerHTML = json[0].OrderDate;
                document.getElementById("xzrwlx").innerHTML = json[0].BusinessTypess;
                document.getElementById("TaskNum").innerHTML = json[0].PID;
                document.getElementById("Begin").innerHTML = json[0].DeliveryLimit;
                document.getElementById("OrderContacts").innerHTML = json[0].OrderContacts;
                document.getElementById("DeliveryMethod").innerHTML = json[0].JHFS;
                document.getElementById("IsInvoice").innerHTML = json[0].FP;
                document.getElementById("PaymentMethod").innerHTML = json[0].ZFFS;
                document.getElementById("PaymentAgreement").innerHTML = json[0].FKYD;
                document.getElementById("ContractNO").innerHTML = json[0].ContractNO;
                document.getElementById("TheProject").innerHTML = json[0].TheProject;
            }

        }

    });


});

