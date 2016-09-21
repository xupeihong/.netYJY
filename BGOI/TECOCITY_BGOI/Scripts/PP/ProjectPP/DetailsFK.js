
if (location.search != "") {
    payid = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "SelectFKXQ",
        type: "post",
        data: { payid: payid },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Rate + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].Rate * json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '</tr>'
                    if (json[i].Rate != "0") {
                        $("#GXInfo").append(html);
                    }

                }
                document.getElementById("PayTime").innerHTML = json[0].PayTime;
                document.getElementById("PaymentMenthod").innerHTML = json[0].ZF;
                document.getElementById("State").innerHTML = json[0].FK;
                document.getElementById("OrderContacts").innerHTML = json[0].OrderContacts;
            }
        }


    })
}