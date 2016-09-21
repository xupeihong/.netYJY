$(document).ready(function () {
    if (location.search != "") {
        EID = location.search.split('&')[0].split('=')[1];
        DID = location.search.split('&')[1].split('=')[1];
        var id = "";
        var Dnum = DID.split(',');
        for (var i = 0; i < Dnum.length - 1; i++) {
            id += "'" + Dnum[i] + "'";
            if (i < Dnum.length - 1) {
                id += ",";
            } else {
                id += "";
            }
        }
        if (id != "") { DID = id.substr(0, id.length - 1); }
        
    }
   LoadReturnDetail();

    $("#btnSaveExcGoods").click(function () {
        var aa = "";
        var cbNum = document.getElementsByName("cb");
        for (var i = 0; i < cbNum.length; i++) {

            var cbid = "";
            if (cbNum[i].checked == true) {
                cbid = cbNum[i].id;
                aa += cbid.substring(5) + ",";
            }

        }
        //var dataSel = jQuery("#list").jqGrid('getGridParam');
        //var ids = dataSel.selrow;
        //var Model = jQuery("#list").jqGrid('getRowData', ids);
        //if (ids == null) {
        //    alert("请选择要修改的行");
        //    return;
        //}
        //else {

        // var PID = aa;
        var salesNum = "";
        var arr1 = aa.split(',');
        var ID = "";
        for (var i = 0; i < arr1.length - 1; i++) {
            //salesNum += document.getElementById("Amount" + arr1[i]).value + ",";
            var Did = document.getElementById("DID" + arr1[i]).innerHTML;
            // ID += "'" + Did + "'";
            ID += Did;
            if (i < arr1.length - 1) {
                ID += ",";
            } else {
                ID += "";
            }
        }
        ID = ID.substr(0, ID.length - 1);
        window.parent.addBasicDetail(ID);
        window.parent.ClosePop();

        //}


    })

    $("#btnExit").click(function () {
        window.parent.ClosePop();
    })
});
var DID = 0;
function LoadReturnDetail() {
    var ID = DID;
    $.ajax({
        url: "GetReturnDetailByDID",
        type: "post",
        data: { ID: ID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><input id="Check' + rowCount + '" type="checkbox" name="cb"/><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable></td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + '" id="Spec' + rowCount + '">' + json[i].Specifications + '</lable></td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable></td>';
                    html += '<td ><lable  class="labSupplier' + rowCount + '"  id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable></td>';
                    html += '<td ><input type="text"  readonly="readonly" id="Amount' + rowCount + '" value="' + json[i].Amount + '" /></td>';
                    html += '<td ><input type="text"  readonly="readonly"  id="UnitPrice' + rowCount + '" value="' + json[i].ExUnit + '"/></td>';
                    html += '<td ><input type="text"  readonly="readonly" id="Subtotal' + rowCount + '" value="' + json[i].ExTotal + '"/></td>';
                    //html += '<td ><lable class="labDeliveryTime' + rowCount + ' " id="DeliveryTime' + rowCount + '">' + json[i].DeliveryTime + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labShipGoodsID' + rowCount + ' " id="ShipGoodsID' + rowCount + '">' + json[i].OrderID + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                }


            }
        }
    })
}

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}