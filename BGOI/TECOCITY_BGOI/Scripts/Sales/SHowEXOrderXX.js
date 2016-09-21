$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
       // OrderID = location.search.split('&')[1].split('=')[1];
    }
    LoadShipmentsDetail();
    LoadEXReturnDetail();
});

function LoadShipmentsDetail() {
    var EID = $("#EID").val();
   // var orderID = OrderID;
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetExchangeGoodsDetail",
        type: "post",
        data: { EID: EID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" >'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable  class="labSupplier' + rowCount + '"  id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable></td>';
                    html += '<td ><lable   id="Amount' + rowCount + '" >' + json[i].Amount + '</lable></td>';
                    html += '<td ><lable id="UnitPrice' + rowCount + '" >' + json[i].ExUnit + '</lable></td>';
                    html += '<td ><lable id="Subtotal' + rowCount + '" >' + json[i].ExTotal + '</lable></td>';
                    html += '<td ><lable id="Remark' + rowCount + '" >' + json[i].Remark + '</lable></td>';
                    html += '<td style="display:none;"><lable class="labOrderID' + rowCount + ' " id="OrderID' + rowCount + '">' + json[i].OrderID + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="EDID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '</tr>'
                    CountRows = CountRows + 1;
                    rowCount += 1;
                    $("#DetailInfo").append(html);

                }


            }
        }
    })
}


function LoadEXReturnDetail() { //增加货品信息行
    var EID = $("#EID").val();
    rowCount = document.getElementById("ReturnDetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetReturnDetailByEID",//"GetOrdersDetailBYDID",
        type: "post",
        data: { EID: EID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json != null) {
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = i;
                    
                            var html = "";
                            html = '<tr  id ="DetailInfo' + rowCount + '" >'
                            html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '" style="width:60px;">' + (i + 1) + '</lable> </td>';
                            html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '" style="width:60px;">' + json[i].ProductID + '</lable> </td>';
                            html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '" style="width:60px;">' + json[i].OrderContent + '</lable> </td>';
                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Spec' + rowCount + '" style="width:60px;">' + json[i].Specifications + '</lable> </td>';
                            html += '<td ><lable class="labUnit' + rowCount + ' " id="Units' + rowCount + '" style="width:60px;">' + json[i].Unit + '</lable> </td>';
                            html += '<td ><lable  class="labSupplier' + rowCount + '"  id="Supplier' + rowCount + '" style="width:60px;">' + json[i].Supplier + '</lable></td>';
                            html += '<td ><lable   id="Amount' + rowCount + '" style="width:60px;">' + json[i].Amount + '</lable></td>';
                            html += '<td ><lable id="UnitPrice' + rowCount + '" style="width:60px;">' + json[i].ExUnit + '</lable></td>';
                            html += '<td ><lable id="Subtotal' + rowCount + '" style="width:60px;">' + json[i].ExTotal + '</lable></td>';
                            html += '<td ><lable id="Remark' + rowCount + '" style="width:60px;">' + json[i].Remark + '</lable></td>';
                            html += '<td style="display:none;"><lable class="labOrderID' + rowCount + ' " id="OrderID' + rowCount + '" style="width:60px;">' + json[i].OrderID + '</lable> </td>';
                            html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="EDID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                            html += '</tr>'
                            CountRows = CountRows + 1;
                            rowCount += 1;
                      
                        $("#ReturnDetailInfo").append(html);

                    }
                }


            }
        }
    })
}
