$(document).ready(function () {
    if (location.search != "") {
        if (location.search.split('&')[0].split('=')[0] == "?THIDXQ") {
            var THID = location.search.split('&')[0].split('=')[1];
            $.ajax({
                url: "SelectTHXQ",
                type: "post",
                data: { THID: THID },
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
                            html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                            html += '<td ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                            html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                            html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';

                            html += '</tr>'
                            $("#GXInfo").append(html);
                        }

                        document.getElementById("Begin").innerHTML = json[0].ReturnDate;
                        document.getElementById("ReturnType").innerHTML = json[0].THLX;
                        document.getElementById("ReturnMode").innerHTML = json[0].THFS;
                        document.getElementById("ReturnAgreement").innerHTML = json[0].ReturnAgreement;
                        document.getElementById("TheProject").innerHTML = json[0].TheProject;
                        document.getElementById("ReturnDescription").innerHTML = json[0].ReturnDescription;
                        document.getElementById("OrderContacts").innerHTML = json[0].ReturnHandler;
                    }
                }
            })
        }
    }
});