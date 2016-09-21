$(document).ready(function () {
    if (location.search != "") {
        EID = location.search.split('&')[0].split('=')[1];
        DID = location.search.split('&')[1].split('=')[1];
     var  id = "";
        var Dnum = DID.split(',');
        for (var i = 0; i <Dnum. length-1; i++) {
            id += "'" + Dnum[i] + "'";
            if (i < Dnum.length-1) {
                id += ",";
            } else
            {
                id += "";
            }
        }
        if (id != "") { DID = id.substr(0, id.length - 1); }
       
    }
    LoadOrderDetail();
  
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
        var ID="";
        for (var i = 0; i < arr1.length - 1; i++) {
            //salesNum += document.getElementById("Amount" + arr1[i]).value + ",";
            var Did = document.getElementById("DID" + arr1[i]).innerHTML;
          ID +="'"+ Did+"'";
            if (i < arr1.length - 1) {
                ID += ",";
            } else {
                ID += "";
            }
        }
        ID = ID.substr(0,ID.length-1);
            window.parent.addBasicDetail(ID);
            window.parent.ClosePop();

        //}


    })
});
var DID = 0;
function LoadOrderDetail()
{
    var ID = DID;
    $.ajax({
        url: "GetOrdersDetailBYDID",
        type: "post",
        data: { DID: ID },
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
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="Units' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable  class="labSupplier' + rowCount + '"  id="Supplier' + rowCount + '">' + json[i].Manufacturer + '</lable></td>';
                    html += '<td ><input type="text"  readonly="readonly" id="Amount' + rowCount + '" value="' + json[i].OrderNum + '" /></td>';
                    html += '<td ><input type="text"  readonly="readonly"  id="UnitPrice' + rowCount + '" value="' + json[i].Price + '"/></td>';
                    html += '<td ><input type="text"  readonly="readonly" id="Subtotal' + rowCount + '" value="' + json[i].Subtotal + '"/></td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + ' " id="DeliveryTime' + rowCount + '">' + json[i].DeliveryTime + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labShipGoodsID' + rowCount + ' " id="ShipGoodsID' + rowCount + '">' + json[i].OrderID + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID+ '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                }


            }
        }
    })
}

function SaveExcGoods() {
    var OID = OrderID;
    var EID = $("#EID").val();
    var EXCType = $("#EXCType").val();
    var EXCWay = $("#EXCWay").val();
    var EXCYd = $("#EXCYd").val();
    var EXCWhy = $("#EXCWhy").val();

    var ProductID = "";
    var OrderContent = "";
    var SpecsModels = "";
    var Unit = "";
    var Amount = "";
    var Supplier = "";
    var UnitPrice = "";
    var Subtotal = "";

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
    for (var i = 0; i < arr1.length - 1; i++) {
        //salesNum += document.getElementById("Amount" + arr1[i]).value + ",";
        var Productid = document.getElementById("ProductID" + arr1[i]).innerHTML;
        var mainContent = document.getElementById("OrderContent" + arr1[i]).innerHTML;
        var specsModels = document.getElementById("Spec" + arr1[i]).innerHTML;
        var unit = document.getElementById("Units" + arr1[i]).innerHTML;
        var salesNum = document.getElementById("Amount" + arr1[i]).innerHTML;
        var supplier = document.getElementById("Supplier" + arr1[i]).innerHTML;
        var uitiprice = document.getElementById("UnitPrice" + arr1[i]).innerHTML;
        var subtotal = document.getElementById("Subtotal" + arr1[i]).innerHTML;
        ProductID += Productid;
        OrderContent += mainContent;
        SpecsModels += specsModels;
        Unit += unit;
        Amount += salesNum;
        Supplier += supplier;
        UnitPrice += uitiprice;
        Subtotal += subtotal;
        if (i < arr1.length - 1) {
            ProductID += ",";
            OrderContent += ",";
            SpecsModels += ",";
            Unit += ",";
            Amount += ",";
            Supplier += ",";
            UnitPrice += ",";
            Subtotal += ",";
        } else {
            ProductID += "";
            OrderContent += "";
            SpecsModels += "";
            Unit += "";
            Amount += "";
            Supplier += "";
            UnitPrice += "";
            Subtotal += "";
        }
    }
    //  var tbody = document.getElementById("DetailInfo");
    //for (var i = 0; i < tbody.rows.length; i++) {
    //    //if ($('input[id=Check' + i + ']:checked'))// ($('input[id=Check' + i + ']').prop("checked") == 'checked') 
    //    //{//($('input[id=c' + rowid + ']').prop("checked") == 'checked') 
    //    //'input[name=chuli]:checkbox:checked'
    //    if ($('input[name=cb]:checkbox:checked')) {

    //        var Productid = document.getElementById("ProductID" + i).innerHTML;
    //        var mainContent = document.getElementById("OrderContent" + i).innerHTML;
    //        var specsModels = document.getElementById("Spec" + i).innerHTML;
    //        var unit = document.getElementById("Units" + i).innerHTML;
    //        var salesNum = document.getElementById("Amount" + i).innerHTML;
    //        var supplier = document.getElementById("Supplier" + i).innerHTML;
    //        var uitiprice = document.getElementById("UnitPrice" + i).innerHTML;
    //        var subtotal = document.getElementById("Subtotal" + i).innerHTML;

    //        //ID += parseInt(i + 1);
    //        ProductID += Productid;
    //        OrderContent += mainContent;
    //        SpecsModels += specsModels;
    //        Unit += unit;
    //        Amount += salesNum;
    //        Supplier += supplier;
    //        UnitPrice += uitiprice;
    //        Subtotal += subtotal;
    //    }
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
        url: "SaveExcGoods",
        type: "Post",
        data: {
            OID: OID, EID: EID, EXCType: EXCType, EXCWay: EXCWay, EXCYd: EXCYd, EXCWhy: EXCWhy,
            ProductID: ProductID, OrderContent: OrderContent, SpecsModels: SpecsModels, Unit: Unit,
            Amount: Amount, Supplier: Supplier, UnitPrice: UnitPrice, Subtotal: Subtotal
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                alert("生成换货！");
                window.parent.ClosePop();
            }
            else {
                alert("生成换货失败-" + data.Msg);
            }
        }
    });
}

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

function XJ(rowPrice) {
    var newPrice = rowPrice.id;
    var strRow = newPrice.charAt(newPrice.length - 1);
    var strUnitPrice = $("#UnitPrice" + strRow).val();
    var Count = $("#Amount" + strRow).val();
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);

    $("#Subtotal" + strRow).val(strTotal);

    //GetAmount();//
        //var Total = 0;
        //var tbody = document.getElementById("DetailInfo");
        //for (var i = 0; i < tbody.rows.length; i++) {
        //    var Amount = document.getElementById("Amount" + i).value;
        //    var UnitPrice = document.getElementById("UnitPrice" + i).value;
        //    if (Amount == "" || Amount == null) {
        //        Amount = "0";
        //    }
        //    if (UnitPrice == "" || UnitPrice == null) {
        //        UnitPrice = "0.00";
        //    }
        //    Total = parseFloat(Amount) * parseFloat(UnitPrice);

        //    $("#Subtotal" + i).val(Total);}
        //}
    
}
