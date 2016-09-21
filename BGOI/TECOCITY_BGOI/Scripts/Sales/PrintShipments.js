$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
        OrderID = location.search.split('&')[1].split('=')[1];
    }
    $("#PrintArea table td").attr("style", "border: 1px solid #000;");
    LoadShipmentsDetial();
    LoadShipments();
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
function LoadShipmentsDetial()
{
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetShipmentDetail",
        type: "post",
        data: { ShipGoodsID: ID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + '  id="Amount' + rowCount + '">'+json[i].Amount+'</lable></td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows += 1;
                }


            }
        }
    })
}

function LoadShipments()
{//GetShipment
    rowCount = document.getElementById("Shipment").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetShipments",
        type: "post",
        data: { ShipGoodsID: ID ,Orderid:OrderID},
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            //if (json.length > 0) {


            //    for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">备注 </lable> </td>';
                    html += '<td colspan=" 3"><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '</tr>'
                    html += '<tr>'
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">订货人</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + '  id="Amount' + rowCount + '">' + json[i].Orderer + '</lable></td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">发货人</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].Shippers + '</lable> </td>';
                    html += '</tr>'
                    html += '<tr>'
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">日期</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + '  id="Amount' + rowCount + '">' + json[i].ContractDate + '</lable></td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">日期</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].ShipmentDate + '</lable> </td>';
                    html += '</tr>'
                    $("#Shipment").append(html);

            //    }


            //}
        }
    })
}