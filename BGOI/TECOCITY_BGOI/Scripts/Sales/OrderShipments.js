$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
        ContractID= location.search.split('&')[1].split('=')[1];
    }
    LoadOrderDetail();
    $("#btnSaveOrderShip").click(function () {
        SaveOrderShip();
    });
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    })

});

var RAmount = "";
function LoadOrderDetail()
{
    var OrderID = ID;
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetOredersShipmentDetail",
        type: "post",
        data: { OrderID: ID },
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
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="Units' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    // 0826 // html += '<td ><lable class="labUnits' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';

                    html += '<td ><input type="text"  onblur=OldAmount(this)  class="labAmount' + rowCount + '" id="Amount' + rowCount + '" value="' + json[i].OrderNum + '"/></td>';
                   // html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].Price + '</lable> </td>';

                    //html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                    //html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    // html += '<td ><lable class="labSupplier' + rowCount + ' " id="Supplier' + rowCount + '">' + "供应商" + '</lable> </td>';
                    //  html += '<td ><input type="button" id="Supplier' + rowCount + '" onclick=CheckSupplier(this) value="' + "供应商" + '" style="width:60px;"/><input type="text" onclick=CheckSupplier(this) id="txtSupplier' + rowCount + '"> </td>';
                    html += '<td ><lable  class="labSupplier' + rowCount + '"  id="Supplier' + rowCount + '">' + json[i].Manufacturer + '</lable></td>';
                    html += '<td ><lable   class="labPrice' + rowCount + '" id="UnitPrice' + rowCount + '">' + json[i].Price + '</lable></td>';
                    html += '<td ><lable   class="labSubtotal' + rowCount + '" id="Subtotal' + rowCount + '">' + json[i].Subtotal + '</lable></td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + ' " id="DeliveryTime' + rowCount + '">' + json[i].DeliveryTime + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labOrderID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
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
    var strRow = newRowID.charAt(newRowID.length - 1);
    //var DID = document.getElementById("DID" + strRow).innerHTML;
    //DID = "'" + DID + "'";
    //$.ajax({
    //    url: "GetOrdersDetailBYDID",
    //    type: "post",
    //    data: { DID: DID },
    //    dataType: "json",
    //    success: function (data) {

    //        var json = eval(data.datas);
    //        if (json.length > 0) {
    //            RAmount = json[0].OrderNum;
    //           // RPrice = json[0].Price;
    //        }
    //    }
    //});
   // RAmount = $("#Amount" + strRow).val();

}

function SaveOrderShip()
{
    var OrderID = ID;
    var ShipGoodeID = $("#ShipGoodsID").val();
    var Remark = $("#BZtxt").val();
    var Orderer = $("#DHtxt").val();
    if (Orderer == "")
    {
        alert("订货人不能为空");
        return;
    }

    var Shippers = $("#FHtxt").val();
    if (Shippers == "") {
        alert("发人不能为空");
        return;
    }
    var ProductID = "";
    var OrderContent = "";
    var SpecsModels = "";
    var Unit = "";
    var Amount = "";
    var Supplier = "";
    var UnitPrice = "";
    var Subtotal = "";
    var DID = "";
    var aa = "";
    var cbNum = document.getElementsByName("cb");
    for (var i = 0; i < cbNum.length; i++) {
        var cbid = "";
        if (cbNum[i].checked == true) {
            cbid = cbNum[i].id;
            aa += cbid.substring(5) + ",";
        }
    }
    var salesNum = "";
    var arr1 = aa.split(',');
    if (arr1.length <= 1) { alert("请选择发货数据"); return;}
    for (var i = 0; i < arr1.length-1; i++) {
        var Productid = document.getElementById("ProductID" + arr1[i]).innerHTML;
        var mainContent = document.getElementById("ProName" + arr1[i]).innerHTML;
        var specsModels = document.getElementById("Spec" + arr1[i]).innerHTML;
        var unit = document.getElementById("Units" + arr1[i]).innerHTML;
        var salesNum = document.getElementById("Amount" + arr1[i]).value;
        var supplier = document.getElementById("Supplier" + arr1[i]).innerHTML;
        var uitiprice = document.getElementById("UnitPrice" + arr1[i]).innerHTML;
        var subtotal = document.getElementById("Subtotal" + arr1[i]).innerHTML;
        var did = document.getElementById("DID" + arr1[i]).innerHTML;
        //ID += parseInt(i + 1);
        ProductID += Productid;
        OrderContent += mainContent;
        SpecsModels += specsModels;
        Unit += unit;
        Amount += salesNum;
        Supplier += supplier;
        UnitPrice += uitiprice;
        Subtotal += subtotal;
        DID += did;
        if (i < arr1.length - 1) {
            //ID += ",";
            ProductID += ",";
            OrderContent += ",";
            SpecsModels += ",";
            Unit += ",";
            Amount += ",";
            Supplier += ",";
            UnitPrice += ",";
            Subtotal += ",";
            DID += ",";
        }
        else {
            ProductID += "";
            OrderContent += "";
            SpecsModels += "";
            Unit += "";
            Amount += "";
            Supplier += "";
            UnitPrice += "";
            Subtotal += "";
            DID += "";
        }
    }
    //var tbody = document.getElementById("DetailInfo");
    //for (var i = 0; i < tbody.rows.length; i++) {
    //    var Productid = document.getElementById("ProductID" + i).innerHTML;
    //    var mainContent = document.getElementById("ProName" + i).innerHTML;
    //    var specsModels = document.getElementById("Spec" + i).innerHTML;
    //    var unit = document.getElementById("Units" + i).innerHTML;
    //    var salesNum = document.getElementById("Amount" + i).innerHTML;
    //    var supplier = document.getElementById("Supplier" + i).innerHTML;
    //    var uitiprice = document.getElementById("UnitPrice" + i).innerHTML;
    //    var subtotal = document.getElementById("Subtotal" + i).innerHTML;
    //    //ID += parseInt(i + 1);
    //    ProductID += Productid;
    //    OrderContent += mainContent;
    //    SpecsModels += specsModels;
    //    Unit += unit;
    //    Amount += salesNum;
    //    Supplier += supplier;
    //    UnitPrice += uitiprice;
    //    Subtotal += subtotal;
      
    //    if (i < tbody.rows.length - 1) {
    //        //ID += ",";
    //        ProductID += ",";
    //        OrderContent += ",";
    //        SpecsModels += ",";
    //        Unit += ",";
    //        Amount += ",";
    //        Supplier += ",";
    //        UnitPrice += ",";
    //        Subtotal += ",";
           
    //    }
    //    else {
    //        // ID += "";
    //        ProductID += "";
    //        OrderContent += "";
    //        SpecsModels += "";
    //        Unit += "";
    //        Amount += "";
    //        Supplier += "";
    //        UnitPrice += "";
    //        Subtotal += "";
           
    //    }
    //}
    $.ajax({
        url: "SaveOrderShip",
        type: "Post",
        data: {    OrderID:OrderID,ShipGoodeID:ShipGoodeID,ContractID:ContractID,Remark:Remark,Orderer:Orderer,Shippers:Shippers,
            ProductID: ProductID, OrderContent: OrderContent, SpecsModels: SpecsModels, Unit: Unit,
            Amount: Amount, Supplier: Supplier, UnitPrice: UnitPrice, Subtotal: Subtotal,DID:DID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("发货成功！");
                window.parent.ClosePop();
            }
            else {
                alert("发货失败-" + data.msg);
            }
        }
    });
}

function OldAmount(RowNum)
{
    var newRowNum = RowNum.id;
    var strRow = newRowNum.charAt(newRowNum.length - 1);
    var Count = $("#Amount" + strRow).val();



   //var strRow = newRowID.charAt(newRowID.length - 1);
    var DID = document.getElementById("DID" + strRow).innerHTML;
    DID = "'" + DID + "'";
    $.ajax({
        url: "GetOrdersDetailBYDID",
        type: "post",
        data: { DID: DID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                RAmount = json[0].OrderNum;
                // RPrice = json[0].Price;
            }
        }
    });
    if (parseFloat(RAmount) < parseFloat(Count) || Count == "") {
        alert("不能大于原数量或为空");
        return;
    }
}