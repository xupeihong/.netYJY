$(document).ready(function () {
    if (location.search != "") {
        DDID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "ErSelectGoodsDDID",
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
                    html = '<tr  id ="GXInfo' + rowCount + '"  >'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';;
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';

                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Suppliersss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td  style="display:none"><lable class="labOrderContent' + rowCount + ' " id="supplier' + rowCount + '">' + json[i].SID + '</lable> </td>';

                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Suppliersss' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Suppliersss' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Suppliersss' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Suppliersss' + rowCount + '">' + json[i].Total + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);

                }
                document.getElementById("DDID").innerHTML = location.search.split('&')[0].split('=')[1];
                document.getElementById("OrderDate").innerHTML = json[0].OrderDate;
                document.getElementById("GoodsType").innerHTML = json[0].GoodsTypes;
                if (json[0].GoodsType == "0") {
                    $("#tablelist").css("display", "");
                    $("#tablelists").css("display", "none");
                    document.getElementById("StockSituations").innerHTML = json[0].StockSituation;
                    if (json[0].DeliveryLimit != "0001/1/1 0:00:00") {
                        document.getElementById("Begins").innerHTML = json[0].DeliveryLimit;
                    }
                }
                else {
                    $("#tablelist").css("display", "none");
                    $("#tablelists").css("display", "");

                    if (json[0].DeliveryLimit != "0001/1/1 0:00:00") {
                        document.getElementById("Begin").innerHTML = json[0].DeliveryLimit;
                    }

                    document.getElementById("StockSituation").innerHTML = json[0].StockSituation;
                    document.getElementById("TheProject").innerHTML = json[0].TheProject;
                    document.getElementById("ProjectPeople").innerHTML = json[0].ProjectPeople;
                    document.getElementById("Contract").innerHTML = json[0].Contracts;
                    if (json[0].Contract == "0") {
                        $("#HTyes").css("display", "");
                        $("#HTno").css("display", "none");
                    }
                    else {
                        $("#HTyes").css("display", "none");
                        $("#HTno").css("display", "");
                        document.getElementById("ContractNoReason").innerHTML = json[0].ContractNoReason;
                    }
                    document.getElementById("Tsix").innerHTML = json[0].Tsixs;

                    document.getElementById("SaleUnitPrice").innerHTML = json[0].SaleUnitPrice;

                    document.getElementById("ContractTotal").innerHTML = json[0].ContractTotal;
                    document.getElementById("FKexplain").innerHTML = json[0].FKexplain;
                    document.getElementById("ProjectHK").innerHTML = json[0].ProjectHK;
                }

            }

        }

    });
});