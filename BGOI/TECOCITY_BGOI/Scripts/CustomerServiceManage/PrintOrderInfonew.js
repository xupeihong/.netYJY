$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
        // OrderID = location.search.split('&')[1].split('=')[1];
    }
    //  LoadOrderDetial();
    //  LoadShipments();
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";
        $("#PrintArea").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        //wb.ExecWB(7, 1)
        document.getElementById("btnPrint").className = "btn";
        $("#PrintArea").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });

});
var curPage = 1;
var OnePageCount = 15;
function LoadOrderDetial() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetOrdersDetailnew",
        type: "post",
        data: { OrderID: ID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';

                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labAmount' + rowCount + '  id="Unit' + rowCount + '">' + json[i].OrderUnit + '</lable></td>';
                    html += '<td ><lable class="labAmount' + rowCount + '  id="Amount' + rowCount + '">' + json[i].OrderNum + '</lable></td>';
                    html += '<td ><lable class="labAmount' + rowCount + '  id="Suplier' + rowCount + '">' + json[i].Manufacturer + '</lable></td>';
                    html += '<td ><lable class="labAmount' + rowCount + '  id="Price' + rowCount + '">' + json[i].Price + '</lable></td>';
                    html += '<td ><lable class="labAmount' + rowCount + '  id="Subtotal' + rowCount + '">' + json[i].Subtotal + '</lable></td>';
                    html += '<td ><lable class="labAmount' + rowCount + '  id="Subtotal' + rowCount + '">' + json[i].Technology + '</lable></td>';
                    html += '<td ><lable class="labAmount' + rowCount + '  id="Subtotal' + rowCount + '">' + json[i].DeliveryTime + '</lable></td>';


                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows += 1;
                }


            }
        }
    })
}

function LoadShipments() {//GetShipment
    rowCount = document.getElementById("Shipment").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetOrdersInfonew",
        type: "post",
        data: { Orderid: ID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            //if (json.length > 0) {


            //    for (var i = 0; i < json.length; i++) {
            var html = "";
            html = '<tr  id ="DetailInfo' + rowCount + '">'
            html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">合计：人民币（大写）   </lable> </td>';
            html += '<td colspan="3"><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].Total + '</lable> </td>';
            html += '</tr>'

            html = '<tr  id ="DetailInfo' + rowCount + '">'
            html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">付款方式：   </lable> </td>';
            html += '<td colspan="3"><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].PayWay + '</lable> </td>';
            html += '</tr>'
            html = '<tr  id ="DetailInfo' + rowCount + '">'
            html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">产品保修期：   </lable> </td>';
            html += '<td colspan="3"><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].Guarantee + '</lable> </td>';
            html += '</tr>'
            html += '<tr rowspan="2">'
            html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">供方</lable> </td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">单  位(章)</lable> </td>';
            html += '<td ><lable class="labSpec' + rowCount + '  id="Amount' + rowCount + '">' + json[i].Provider + '</lable></td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">负责人</lable> </td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].ProvidManager + '</lable> </td>';
            html += '</tr>'
            html += '<tr>'
            html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">需方</lable> </td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">单  位(章)</lable> </td>';
            html += '<td ><lable class="labSpec' + rowCount + '  id="Amount' + rowCount + '">' + json[i].Demand + '</lable></td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">负责人</lable> </td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].DemandManager + '</lable> </td>';
            html += '</tr>'
            $("#Shipment").append(html);

            //    }


            //}
        }
    })
}