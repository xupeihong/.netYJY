$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
    LoadShipmentDetail();
    $("#btnSaveOrderShip").click(function () {
        SaveShipment();
    });

    $("#btnExit").click(function () {
        window.parent.ClosePop();
    })
});
function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
    // $("#cb" + newRowID).checked("","");
    var strRow = newRowID.charAt(newRowID.length - 1);
    // $("#cb" + strRow).checked = true;
    var cbNum = document.getElementsByName("cb");
    
    //if (cbNum[strRow].checked == false) {
    //    cbNum[strRow].checked = !cbNum[strRow].checked;
    //}
    //else {
    //    cbNum[strRow].checked = !cbNum[strRow].checked;
    //}
    //if ($("#cb" + strRow).checked == true)
    //{ alert("T"); } else { alert("F");}
}
function LoadShipmentDetail()
{
    var OrderID = ID;
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
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';//<input id="Check' + rowCount + '" type="checkbox" name="cb"/>
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    //html += '<td ><input type="text"   onblur=XJ() id="Amount' + rowCount + '" style="width:100px;" value="' + json[i].Amount + '"/></td>';
                    //html += '<td ><input type="text" id="Supplier' + rowCount + '"  readonly="readonly" onclick=CheckSupplier(this) value="' + json[i].Supplier + '" style="width:60px;"/> </td>';
                    //html += '<td ><input type="text" style="width:100px;"  onblur=XJ() id="UnitPrice' + rowCount + '" value="' + json[i].Price + '"> </td>';
                    html += '<td ><lable   id="Amount' + rowCount + '" style="width:100px;" >' + json[i].Amount + '</lable></td>';
                    html += '<td ><lable id="Supplier' + rowCount + '"   style="width:60px;"/> ' + json[i].Supplier + '</lable></td>';
                    html += '<td ><lable  style="width:100px;"  id="UnitPrice' + rowCount + '" >' + json[i].Price + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labOrderID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                }


            }
        }
    })
}

function SaveShipment()
{
    var ShipGoodeID = $("#ShipGoodsID").val();
    var OrderID = $("#OrderID").val();
    var Remark = $("#Remark").val();
    var Orderer = $("#Orderer").val();
    var Shippers = $("#Shippers").val();
    var Did = "";
    var ProductID = "";
    var OrderContent = "";
    var SpecsModels = "";
    var Unit = "";
    var Amount = "";
    var Supplier = "";
    var UnitPrice = "";
    //var Subtotal = "";
    //var aa = "";
    //var cbNum = document.getElementsByName("cb");
   
    //for (var i = 0; i < cbNum.length; i++) {
    //    var cbid = "";
    //    if (cbNum[i].checked == true) {
    //        cbid = cbNum[i].id;
    //        aa += cbid.substring(5) + ",";
    //    }
    //}
    //var salesNum = "";
    //var arr1 = aa.split(',');
    //for (var i = 0; i < arr1.length - 1; i++) {
    //    var Productid = document.getElementById("ProductID" + arr1[i]).innerHTML;
    //    var mainContent = document.getElementById("ProName" + arr1[i]).innerHTML;
    //    var specsModels = document.getElementById("Spec" + arr1[i]).innerHTML;
    //    var unit = document.getElementById("Units" + arr1[i]).innerHTML;
    //    var salesNum = document.getElementById("Amount" + arr1[i]).value;
    //    var supplier = document.getElementById("Supplier" + arr1[i]).value;
    //    var uitiprice = document.getElementById("UnitPrice" + arr1[i]).value;
    //    var did = document.getElementById("DID"+arr1[i]).innerHTML;
    //    var subtotal = document.getElementById("Subtotal" + arr1[i]).val();
    //    ID += parseInt(i + 1);
    //    ProductID += Productid;
    //    OrderContent += mainContent;
    //    SpecsModels += specsModels;
    //    Unit += unit;
    //    Amount += salesNum;
    //    Supplier += supplier;
    //    UnitPrice += uitiprice;
    //    Did += did;
    //    if (i < arr1.length - 1) {
    //        ID += ",";
    //        ProductID += ",";
    //        OrderContent += ",";
    //        SpecsModels += ",";
    //        Unit += ",";
    //        Amount += ",";
    //        Supplier += ",";
    //        UnitPrice += ",";
    //        Did += ",";
    //    }
    //    else {
    //        ProductID += "";
    //        OrderContent += "";
    //        SpecsModels += "";
    //        Unit += "";
    //        Amount += "";
    //        Supplier += "";
    //        UnitPrice += "";
    //        Subtotal += "";
    //        Did += "";
    //    }
    //}

    //
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Productid = document.getElementById("ProductID" + i).innerHTML;
        var mainContent = document.getElementById("ProName" + i).innerHTML;
        var specsModels = document.getElementById("Spec" + i).innerHTML;
        var unit = document.getElementById("Units" + i).innerHTML;
        var salesNum = document.getElementById("Amount" + i).innerHTML;
        var supplier = document.getElementById("Supplier" + i).innerHTML;
        var uitiprice = document.getElementById("UnitPrice" + i).innerHTML;
        var did = document.getElementById("DID" + i).innerHTML;
       // var subtotal = document.getElementById("Subtotal" + i).innerHTML;
        //ID += parseInt(i + 1);
        ProductID += Productid;
        OrderContent += mainContent;
        SpecsModels += specsModels;
        Unit += unit;
        Amount += salesNum;
        Supplier += supplier;
        UnitPrice += uitiprice;
        Did += did;
       // Subtotal += subtotal;

        if (i < tbody.rows.length - 1) {
            //ID += ",";
            ProductID += ",";
            OrderContent += ",";
            SpecsModels += ",";
            Unit += ",";
            Amount += ",";
            Supplier += ",";
            UnitPrice += ",";
            Did += ",";
          //  Subtotal += ",";

        }
        else {
            // ID += "";
            ProductID += "";
            OrderContent += "";
            SpecsModels += "";
            Unit += "";
            Amount += "";
            Supplier += "";
            UnitPrice += "";
            Did += "";
            //Subtotal += "";

        }
    }
    $.ajax({
        url: "SaveUpdateShipment",
        type: "Post",
        data: {
            OrderID:OrderID,ShipGoodsID: ShipGoodeID, Remark: Remark, Orderer: Orderer, Shippers: Shippers,
            ProductID: ProductID, OrderContent: OrderContent, SpecsModels: SpecsModels, Unit: Unit,
            Amount: Amount, Supplier: Supplier, UnitPrice: UnitPrice, Did: Did
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("发货单修改成功！");

                window.parent.ClosePop();
            }
            else {
                alert("发货单修改失败-" + data.msg);
            }
        }
    });
}

//选择供应商
function CheckSupplier(rowid) {
    RowId = rowid.id;
    ShowIframe1("选择货品信息", "../SalesManage/Supplier", 500, 450);
}

function addSupplier(SID) {
    //var rownumber = RowId.substr(8,RowId.length-8);
    $.ajax({
        url: "GetSupplier",
        type: "post",
        data: { SID: SID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#" + RowId).val(json[i].COMNameC);
                }
            }
        }
    });
}
//
function XJ() {
    var Total = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Amount = document.getElementById("Amount" + i).value;
        var UnitPrice = document.getElementById("UnitPrice" + i).value;
        if (Amount == "" || Amount == null) {
            Amount = "0";
        }
        if (UnitPrice == "" || UnitPrice == null) {
            UnitPrice = "0.00";
        }
        Total = parseFloat(Amount) * parseFloat(UnitPrice);

        $("#Subtotal" + i).val(Total);
    }
}
//合计
function HJ() {
    var Total = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Subtotal = document.getElementById("Subtotal" + i).value;
        if (Subtotal == "" || Subtotal == null) {
            Subtotal = "0";
        }
        Total = Total + parseFloat(Subtotal);

        $("#Total").val(Total);
    }
}