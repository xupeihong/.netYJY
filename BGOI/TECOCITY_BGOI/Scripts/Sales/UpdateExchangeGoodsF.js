$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
        OrderID = location.search.split('&')[1].split('=')[1];
    }
    LoadExchangeGoods();
    LoadExchangeDetail();
    $("#btnSaveExcGoods").click(function () {
        SaveUpdateExcGoods();
    });
    $("#ISEXc").click(function () {
        var Eid = $("#EID").val();
        GetDID();
        ShowIframe1("修改换货信息", "../SalesManage/UpdateReturnDetail?EID=" + Eid + "&DID=" + DID, 700, 350, '');
    });

    $("#AddHH").click(function () {
       // CheckDetail();
          FDetail();
    })
    $("#cb").click(function () {
        var cbNum = document.getElementsByName("Rcb");
        //  var cbNum = document.getElementsByName("cb");
        var aa = "";
        for (var i = 0; i < cbNum.length; i++) {
            var cbid = "";
            if (cbNum[i].checked == true) {
                cbid = cbNum[i].id;
                aa += cbid.substring(5) + ",";
            }
        }
        var salesNum = "";
        var arr1 = aa.split(',');
        if (arr1.length > 1) {
            //if (document.getElementById('ISEXc').checked) {
            $("#HH").css("display", "block");
            document.getElementById('AddHH').style.display = 'block';
            document.getElementById('btnDel').style.display = 'block';

            // }
        } else {
            $("#HH").css("display", "none");
            $("#AddHH").css("display", "none");
            document.getElementById('btnDel').style.display = 'none';
        }
    })
    $("#btnExit").click(function () {
        window.parent.ClosePop();

    })
});

var Amount = "";
var Price = "";
var RAmount = "";
var RPrice = "";
var Subtotal = 0;
var DID = "";
function LoadExchangeGoods() {
   var orderID = OrderID;
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetShipmentOrdersDetail",
        type: "post",
        // data: { EID: ID },
        data: { orderID: orderID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><input id="Check' + rowCount + '" type="checkbox" onclick="CheckRow(this)" value="' + json[i].DID + '" name="cb"/><lable class="labRowNumber' + rowCount + ' "  id="RowNumber' + rowCount + '">' + (i + 1) + '</lable></td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable></td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable></td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].SpecsModels + '</lable></td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="Units' + rowCount + '">' + json[i].OrderUnit + '</lable></td>';
                    html += '<td ><lable  class="labSupplier' + rowCount + '"  id="Supplier' + rowCount + '">' + json[i].Manufacturer + '</lable></td>';
                    html += '<td ><lable   id="Amount' + json[i].DID + '"  >' + json[i].ActualAmount + '</lable></td>';

                    html += '<td ><lable  id="UnitPrice' + json[i].DID + '" >' + json[i].Price + '</lable></td>';
                    html += '<td ><lable id="Subtotal' + json[i].DID + '" >' + json[i].ActualSubTotal + '</lable></td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + ' " id="DeliveryTime' + rowCount + '">' + json[i].DeliveryTime + '</lable></td>';
                    html += '<td style="display:none;"><lable class="labOrderID' + rowCount + ' " id="OrderID' + rowCount + '">' + json[i].OrderID + '</lable></td>';
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


function LoadExchangeDetail() {
    rowCount2 = document.getElementById("ReturnDetailInfo").rows.length;
    var CountRows2 = parseInt(rowCount2) + 1;
    $.ajax({
        url: "GetExchangeGoodsDetail",
        type: "post",
        data: { EID: ID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    var ED = json[i].EDID;
                    var cbNum = document.getElementsByName("cb");
                    // cbNum[t].checked == true;


                    $("input[name='cb']").each(function () {
                        if ($(this).val() == ED) {
                            $(this).attr("checked", "true");
                            //Scount++;
                        }
                        else {

                        }
                    })


                    rowCount2 = i;
                    var html = "";
                    html = '<tr  id ="ReturnDetailInfo' + rowCount2 + '" onclick="RselRow(this)">'
                    //<input id="RCheck' + rowCount + '"  type="checkbox" name="Rcb"/>
                    html += '<td ><lable style="width:60px;" class="labRowNumber' + rowCount2 + '"  id="RowNumber' + rowCount2 + '">' + CountRows2 + '</lable></td>';
                    html += '<td ><input type="text" style="width:90px;" class="labProductID' + rowCount2 + ' " id="RProductID' + rowCount2 + '" value="' + json[i].ProductID + '" /></td>';
                    html += '<td ><input type="text" class="labProName' + rowCount2 + '" id="ROrderContent' + rowCount2 + '" style="width:90px;" vlaue="' + json[i].OrderContent + '"/></td>';
                    html += '<td ><input type="text" class="labSpec' + rowCount2 + '" id="RSpec' + rowCount2 + '" style="width:90px;" value="' + json[i].Specifications + '"/></td>';
                    html += '<td ><input type="text"  class="labUnits' + rowCount2 + ' " id="RUnits' + rowCount2 + '"  style="width:90px;"  value="' + json[i].Unit + '" /></td>';
                    html += '<td ><input type="text"    id="RSupplier' + rowCount2 + '" style="width:90px;" value="' + json[i].Supplier + '" /></td>';//readonly="readonly"
                    html += '<td ><input type="text"  onblur=RXJ(this)  id="RAmount' + rowCount2 + '"  style="width:90px;" value="' + json[i].Amount + '"  /></td>';
                    html += '<td ><input type="text" onblur=RXJ(this)  id="RUnitPrice' + rowCount2 + '" style="width:90px;" value="' + json[i].Price + '" /></td>';
                    html += '<td ><input type="text"  readonly="readonly" id="RSubtotal' + rowCount2 + '"  style="width:90px;" value="' + json[i].Subtotal + '" /></td>';
                    html += '<td ><input type="text"  id="RUnitCost' + rowCount2 + '" onblur=TotalCost(this) style="width:90px;" value="' + json[i].UnitCost + '" /></td>';//单位成本';
                    html += '<td ><input type="text" readonly="readonly"  id="RTotalCost' + rowCount2 + '" style="width:90px;" value="' + json[i].TotalCost + '" /></td>';//累计成本';
                    html += '<td ><input type="text"  id="RTechnology' + rowCount2 + '" style="width:90px;"  value="' + json[i].Technology + '" /></td>';
                    html += '<td ><input type="text"  id="RSaleNo' + rowCount2 + '" value="' + json[i].SaleNo + '" style="width:90px;" /></td>';//销售单号';
                    html += '<td ><input type="text"  id="RProjectNo' + rowCount2 + '" style="width:90px;" value="' + json[i].ProjectNO + '" /></td>';//工程项目编号
                    html += '<td ><input type="text"  id="RJNAME' + rowCount2 + '" style="width:90px;" value="' + json[i].JNAME + '" /></td>';//工程项目名称';
                    // html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate" style="width:170px;" id="RDeliveryTime' + rowCount + '"> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount2 + ' " id="REID' + rowCount2 + '">' + json[i].EID + '</lable></td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount2 + ' " id="RDID' + rowCount2 + '">' + json[i].DID + '</lable></td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount2 + ' " id="REDID' + rowCount2 + '">' + json[i].EDID + '</lable></td>';
                    html += '</tr>'
                    //  $("#ReturnDetailInfo").append(html);
                    CountRows2 = CountRows2 + 1;
                    rowCount2 += 1;
                    $("#ReturnDetailInfo").append(html);

                }


            }
        }
    })

}

function addBasicDetail(ID) { //增加货品信息行
    var ID = ID;
    document.getElementById("ReturnTable").style.display = 'block';
    rowCount = document.getElementById("ReturnDetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetReturnDetailByDID",
        type: "post",
        data: { ID: ID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="RselRow(this)">'
                    html += '<td ><input id="RCheck' + rowCount + '" type="checkbox" name="Rcb"/><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="RProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ROrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="RSpec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="RUnits' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable  class="labSupplier' + rowCount + '"  id="RSupplier' + rowCount + '">' + json[i].Supplier + '</lable></td>';
                    html += '<td ><input type="text" onblur=RXJ(this)  id="RAmount' + rowCount + '" value="' + json[i].Amount + '" /></td>';
                    html += '<td ><input type="text" onblur=RXJ(this)  id="RUnitPrice' + rowCount + '" value="' + json[i].ExUnit + '"/></td>';
                    html += '<td ><input type="text"   readonly="readonly" id="RSubtotal' + rowCount + '" value="' + json[i].ExTotal + '"/></td>';
                    //html += '<td ><lable class="labDeliveryTime' + rowCount + ' " id="RDeliveryTime' + rowCount + '">' + json[i].DeliveryTime + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labShipGoodsID' + rowCount + ' " id="RShipGoodsID' + rowCount + '">' + json[i].OrderID + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="RDID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '</tr>'
                    $("#ReturnDetailInfo").append(html);

                }


            }
        }
    })
}


function FDetail() {
    document.getElementById("ReturnTable").style.display = 'block';
    rowCount = document.getElementById("ReturnDetailInfo").rows.length;
    //判断是否是与订单重复的编码
    //var tbody = document.getElementById("DetailInfo");
    //var s = PID.split(',');
    //for (var j = 0; j < s.length; j++) {
    //    for (var i = 0; i < tbody.rows.length; i++) {
    //        var Productid = document.getElementById("ProductID" + i).innerHTML;
    //        Productid = Productid.trim()
    //        Productid = "'" + Productid + "'";
    //        if (s[j] == Productid) {
    //            if (s.length >= 2 && j == 0) {
    //                Productid = Productid + ",";
    //                PID = PID.replace(Productid, "");
    //            }
    //            else if (s.length > 2) {
    //                Productid = "," + Productid;
    //                PID = PID.replace(Productid, "");
    //            }
    //            else
    //                PID = PID.replace(Productid, "");
    //        }
    //    }
    //}
    //if (PID == "") {
    //    alert("不能与订单重复的物品编码");
    //    return;
    //}

    ////判断换货的编码是否重复



    //var tbody2 = document.getElementById("ReturnDetailInfo");
    //var s2 = PID.split(',');
    //for (var j = 0; j < s.length; j++) {
    //    for (var i = 0; i < tbody2.rows.length; i++) {
    //        var Productid2 = document.getElementById("RProductID" + i).innerHTML;
    //        Productid2 = Productid2.trim()
    //        Productid2 = "'" + Productid2 + "'";
    //        if (s2[j] == Productid2) {
    //            if (s2.length >= 2 && j == 0) {
    //                Productid2 = Productid2 + ",";
    //                PID = PID.replace(Productid2, "");
    //            }
    //            else if (s.length > 2) {
    //                Productid2 = "," + Productid2;
    //                PID = PID.replace(Productid2, "");
    //            }
    //            else
    //                PID = PID.replace(Productid2, "");
    //        }
    //    }
    //}
    //if (PID == "") {
    //    alert("不能重复的物品编码");
    //    return;
    //}

    //
    //
    //换货的数量和选择的订单的数量对应
    var cbNum = document.getElementsByName("cb");
    var aa = "";
    for (var i = 0; i < cbNum.length; i++) {
        var cbid = "";
        if (cbNum[i].checked == true) {
            cbid = cbNum[i].id;
            aa += cbid.substring(5) + ",";

        }
    }
    var salesNum = "";
    var arr1 = aa.split(',');
    //
    //if (parseInt(PID.split(',').length) > parseInt(arr1.length - 1)) {
    //    alert("换货的数量不能大于选择的数量");
    //    //  $("#ReturnDetailInfo").html("");
    //    return;

    //}
    if (parseInt(rowCount) == parseInt(arr1.length - 1)) {
        alert("不能添加换货");
        return;
    }
    //if (parseInt(PID.split(',').length) + parseInt(rowCount) > arr1.length - 1) {
    //    alert("对应项超过了已选订单");
    //    return;
    //}



    //







    document.getElementById("ReturnTable").style.display = 'block';
    rowCount = document.getElementById("ReturnDetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var html = "";
    html = '<tr  id ="ReturnDetailInfo' + rowCount + '" onclick=RselRow(this)>'
    // html = '<tr  id ="ReturnDetailInfo' + rowCount + '" onclick="RselRow(this)">';
    html += '<td style="width:90px;"><input id="RCheck' + rowCount + '"  type="checkbox" name="Rcb"/><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable></td>';
    html += '<td ><input type="text" class="labProductID' + rowCount + ' " id="RProductID' + rowCount + '"  style="width:90px;" /></td>';
    html += '<td ><input type="text"  class="labProName' + rowCount + ' " id="ROrderContent' + rowCount + '"  style="width:90px;"/></td>';
    html += '<td ><input type="text"  class="labSpec' + rowCount + ' " id="RSpec' + rowCount + '"  style="width:90px; "/></td>';
    html += '<td ><input type="text"  class="labUnits' + rowCount + ' " id="RUnits' + rowCount + '"  style="width:90px;"/> </td>';
    html += '<td ><input type="text"    id="RSupplier' + rowCount + '"  style="width:90px;"/> </td>';
    html += '<td ><input type="text"  onblur=RXJ(this)  id="RAmount' + rowCount + '"  style="width:90px;" /></td>';
    html += '<td><input type="text"  onblur=RXJ(this) id="RUnitPrice' + rowCount + '" style="width:90px;" /></td>';
    html += '<td ><input type="text"  readonly="readonly" id="RSubtotal' + rowCount + '"  style="width:90px;" /> </td>';
    html += '<td ><input type="text"  id="RUnitCost' + rowCount + '" onblur=TotalCost(this)    style="width:90px;"/></td>';//单位成本';
    html += '<td ><input type="text"  id="RTotalCost' + rowCount + '" readonly="readonly" style="width:90px;"/></td>';//累计成本';
    html += '<td ><input type="text"  id="RTechnology' + rowCount + '" style="width:90px;"> </td>';
    html += '<td ><input type="text"  id="RSaleNo' + rowCount + '" style="width:90px;"/></td>';//销售单号';
    html += '<td ><input type="text"  id="RProjectNo' + rowCount + '" style="width:90px;"/></td>';//工程项目编号
    html += '<td ><input type="text"  id="RJNAME' + rowCount + '" style="width:90px;"/></td>';//工程项目名称';

    html += '</tr>'
    CountRows = CountRows + 1;
    rowCount += 1;
    $("#ReturnDetailInfo").append(html);

}
//选择供应商
function CheckSupplier(rowid) {
    RowId = rowid.id;
    ShowIframe1("选择货品信息", "../SalesManage/Supplier", 500, 520);
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

//小计
function XJ(rowPrice) {
    //if (Amount == 0 || Amount == null)
    //{
    //    alert("不能为空");
    //    return;
    //}

    var newPrice = rowPrice.id;
    var strRow = newPrice.charAt(newPrice.length - 1);

    var strUnitPrice = $("#UnitPrice" + strRow).val();
    var Count = $("#Amount" + strRow).val();
    if (parseFloat(strUnitPrice) <= 0 || strUnitPrice == "" || parseFloat(Price) < parseFloat(strUnitPrice)) {
        alert("价格不能小于0为空或大于原单价");
        return;

    }
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

    //    $("#Subtotal" + i).val(Total);
    //}
}

function CheckAmount(amount) {
    var newamount = amount.id;
    var strRow = newamount.charAt(newamount.length - 1);
    var strUnitPrice = $("#UnitPrice" + strRow).val();
    var Count = $("#Amount" + strRow).val();
    if (parseFloat(Amount) < parseFloat(Count) || Count == "") {
        alert("不能大于原数量或为空");
        return;
    }
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    $("#Subtotal" + strRow).val(strTotal);
}

function RXJ(rowPrice) {
    var newPrice = rowPrice.id;
    var strRow = newPrice.charAt(newPrice.length - 1);
    var strUnitPrice = $("#RUnitPrice" + strRow).val();
    var Count = $("#RAmount" + strRow).val();
    if (parseFloat(RAmount) < parseFloat(Count) || Count == "") {
        alert("不能大于原数量或为空");
        //$("#Amount").focus();
        //$(this).focus();
        return;
    }
    if (parseFloat(strUnitPrice) <= 0 || strUnitPrice == "") {
        alert("价格不能小于0或为空");
        return;

    }
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);

    $("#RSubtotal" + strRow).val(strTotal);

}

//累计成本
function TotalCost(rowrid) {
    RowId = rowrid.id;//RUnitCostRTotalCost
    var a = RowId.split('RUnitCost');
    var b = RowId.split('RAmount');
    var s = a[1];
    if (a.length <= 1) {
        s = b[1];
    }
    var tbody = document.getElementById("DetailInfo");
    //for (var i = 0; i < tbody.rows.length; i++) {
    var Amount = document.getElementById("RAmount" + s).value;
    var UnitCost = document.getElementById("RUnitCost" + s).value;
    //var TaxRate=document.getElementById("TaxRate" + s).value+"";
    if (Amount == "" || Amount == null) {
        Amount = "0";
    }
    if (UnitCost == "" || UnitCost == null) {
        UnitCost = "0.00";
    }
    //  TaxRate = parseFloat(TaxRate / 100);
    //  UnitPrice = parseFloat(UnitPrice) * parseFloat(1 + parseFloat(TaxRate));
    Total = parseFloat(Amount) * parseFloat(UnitCost);
    Total = Total.toFixed(2);
    $("#RTotalCost" + s).val(Total);
    //HJ();
}

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
    var strRow = newRowID.charAt(newRowID.length - 1);
    var ID = document.getElementById("DID" + strRow).innerHTML;
    //ID = "'" + ID + "'";
    $.ajax({
        url: "GetExchangeDetailByDID",
        type: "post",
        data: { DID: ID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                Amount = json[0].Amount;
                Price = json[0].ExUnit;
            }
        }
    });
}


function RselRow(curRow) {
    newRowID = curRow.id;
    $("#DetaiReturnDetailInfolInfo tr").removeAttr("class");
    var strRow = newRowID.charAt(newRowID.length - 1);
    $("#" + newRowID).attr("class", "RowClass");
    //Amount = $("#Amount" + strRow).val();
    //Price = $("#UnitPrice" + strRow).val();
    //var cbNum = document.getElementsByName("cb");
    //   cbNum[strRow].checked = true;

    var ID = document.getElementById("RDID" + strRow).innerHTML;
    //ID = "'" + ID + "'";
    //获取所在行的数量和单价
    $.ajax({
        url: "GetExchangeDetailByDID",
        type: "post",
        data: { DID: ID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                RAmount = json[0].OrderNum;
                RPrice = json[0].Price;
            }
        }
    });
}
function GetDID() {
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
        var Did = document.getElementById("DID" + arr1[i]).innerHTML;
        DID += Did;
        if (i < arr1.length - 1) {
            DID += ",";
        } else {
            DID += "";
        }
    }
    DID = DID.substr(0, DID.length - 1);
}

function SaveUpdateExcGoods() {
    if ($('.field-validation-error').length == 0) {
        var OID = OrderID;
        var EID = $("#EID").val();
        var EXCType = $("#ReturnType").val();
        var EXCWay = $("#ReturnWay").val();
        var EXCYd = $("#ReturnContract").val();
        var EXCWhy = $("#ReturnReason").val();
        var EXCSM = $("#ReturnInstructions").val();
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
        var RProductID = "";
        var ROrderContent = "";
        var RSpecsModels = "";
        var RUnit = "";
        var RAmount = "";
        var RSupplier = "";
        var RUnitPrice = "";
        var RSubtotal = "";
        var RExcTotal = 0;
        var RDID = "";
        var Raa = "";
        var Runitcost = "";
        var Rtotalcost = "";
        var Rsaleno = "";
        var Rprojectno = "";
        var Rjname = "";
        var Rtechnology = "";
       // var EID = "";
        var EDID = "";
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
        var tbody = document.getElementById("ReturnDetailInfo");
        if (tbody.rows.length == 0) {
            alert("请添加换货产品");
            return;
        }
        if (arr1.length - 1 > tbody.rows.length) {
            alert("换货的订单选择项不能小于选择订单的项");
        }
        for (var i = 0; i < arr1.length - 1; i++) {

            var Did = document.getElementById("EDID" + arr1[i]).innerHTML;
            var salesNum = document.getElementById("Amount" + Did).innerHTML;

            var uitiprice = document.getElementById("UnitPrice" + Did).innerHTML;

            var RProductid = document.getElementById("RProductID" + i).value;
            var RmainContent = document.getElementById("ROrderContent" + i).value;
            var RspecsModels = document.getElementById("RSpec" + i).value;
            var Runit = document.getElementById("RUnits" + i).value;
            var RsalesNum = document.getElementById("RAmount" + i).value;
            if (parseInt(RsalesNum) > parseInt(salesNum)) {
                alert("换货的数量不能大于订单数量");
                return;
            }
            var Rsupplier = document.getElementById("RSupplier" + i).value;
            var Ruitiprice = document.getElementById("RUnitPrice" + i).value;
            if (parseFloat(Ruitiprice) > parseFloat(uitiprice)) {
                alert("换货的价格不能大于订单价格");
                return;
            }
            var Rsubtotal = document.getElementById("RSubtotal" + i).value;
            var runitcost = document.getElementById("RUnitCost" + i).value;
            var rtotalcost = document.getElementById("RTotalCost" + i).value;
            var rsaleno = document.getElementById("RSaleNo" + i).value;
            var rprojetno = document.getElementById("RProjectNo" + i).value;
            var rjname = document.getElementById("RJNAME" + i).value;
            var rtechnology = document.getElementById("RTechnology" + i).value;
            var edid = document.getElementById("REDID" + i).innerHTML;
           // var eid = document.getElementById("REID" + i).innerHTML;

            DID += Did;
            RProductID += RProductid;
            ROrderContent += RmainContent;
            RSpecsModels += RspecsModels;
            RUnit += Runit;
            RAmount += RsalesNum;
            // RExcAmount += parseInt(RsalesNum);
            RSupplier += Rsupplier;
            RUnitPrice += Ruitiprice;
            RSubtotal += Rsubtotal;
            // RExcTotal += parseFloat(Rsubtotal);
            Runitcost += runitcost;
            Rtotalcost += rtotalcost;
            Rsaleno += rsaleno;
            Rprojectno += rprojetno;
            Rjname += rjname;
            Rtechnology += rtechnology;
           // EID += eid;
            EDID += edid;
            if (i < arr1.length - 1) {
                //RDID += ",";
                DID += ",";
                RProductID += ",";
                ROrderContent += ",";
                RSpecsModels += ",";
                RUnit += ",";
                RAmount += ",";
                RSupplier += ",";
                RUnitPrice += ",";
                RSubtotal += ",";
                Runitcost += ",";
                Rtotalcost += ",";
                Rsaleno += ",";
                Rprojectno += ",";
                Rjname += ",";
                Rtechnology += ",";
                EDID += ",";
                // EID += ",";
            } else {
                // RDID += "";
                RProductID += "";
                ROrderContent += "";
                RSpecsModels += "";
                RUnit += "";
                RAmount += "";
                RSupplier += "";
                RUnitPrice += "";
                RSubtotal += "";
                Runitcost += "";
                Rtotalcost += "";
                Rsaleno += "";
                Rprojectno += "";
                Rjname += "";
                Rtechnology += "";
                DID += "";
                EDID += "";
                // EID += "";
            }
        }
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
        //    //salesNum += document.getElementById("Amount" + arr1[i]).value + ",";
        //    var Did = document.getElementById("DID" + arr1[i]).innerHTML;
        //    var Productid = document.getElementById("ProductID" + arr1[i]).innerHTML;
        //    var mainContent = document.getElementById("OrderContent" + arr1[i]).innerHTML;
        //    var specsModels = document.getElementById("Spec" + arr1[i]).innerHTML;
        //    var unit = document.getElementById("Units" + arr1[i]).innerHTML;
        //    var salesNum = document.getElementById("Amount" + arr1[i]).innerHTML;
        //    var supplier = document.getElementById("Supplier" + arr1[i]).innerHTML;
        //    var uitiprice = document.getElementById("UnitPrice" + arr1[i]).innerHTML;
        //    var subtotal = document.getElementById("Subtotal" + arr1[i]).innerHTML;
        //    DID += Did;
        //    ProductID += Productid;
        //    OrderContent += mainContent;
        //    SpecsModels += specsModels;
        //    Unit += unit;
        //    Amount += salesNum;
        //    Supplier += supplier;
        //    UnitPrice += uitiprice;
        //    Subtotal += subtotal;
        //    if (i < arr1.length - 1) {
        //        DID += ",";
        //        ProductID += ",";
        //        OrderContent += ",";
        //        SpecsModels += ",";
        //        Unit += ",";
        //        Amount += ",";
        //        Supplier += ",";
        //        UnitPrice += ",";
        //        Subtotal += ",";
        //    } else {
        //        DID += "";
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
        ////DId = DID
        ////alert(DID);
        ////alert(ProductID);
        ////换货信息
        //var RProductID = "";
        //var ROrderContent = "";
        //var RSpecsModels = "";
        //var RUnit = "";
        //var RAmount = "";
        //var RSupplier = "";
        //var RUnitPrice = "";
        //var RSubtotal = "";
        //var RDID = "";
        //var Raa = "";
        //var RcbNum = document.getElementsByName("Rcb");
        //for (var i = 0; i < RcbNum.length; i++) {
        //    var Rcbid = "";
        //    if (RcbNum[i].checked == true) {
        //        Rcbid = RcbNum[i].id;
        //        Raa += Rcbid.substring(5) + ",";
        //    }
        //}
        //var RsalesNum = "";
        //var Rarr1 = Raa.split(',');
        //for (var i = 0; i < Rarr1.length - 1; i++) {
        //    //salesNum += document.getElementById("Amount" + arr1[i]).value + ",";
        //    var RDid = document.getElementById("RDID" + arr1[i]).innerHTML;
        //    var RProductid = document.getElementById("RProductID" + arr1[i]).innerHTML;
        //    var RmainContent = document.getElementById("ROrderContent" + arr1[i]).innerHTML;
        //    var RspecsModels = document.getElementById("RSpec" + arr1[i]).innerHTML;
        //    var Runit = document.getElementById("RUnits" + arr1[i]).innerHTML;
        //    var RsalesNum = document.getElementById("RAmount" + arr1[i]).value;
        //    var Rsupplier = document.getElementById("RSupplier" + arr1[i]).innerHTML;
        //    var Ruitiprice = document.getElementById("RUnitPrice" + arr1[i]).value;
        //    var Rsubtotal = document.getElementById("RSubtotal" + arr1[i]).value;
        //    RDID += RDid;
        //    RProductID += RProductid;
        //    ROrderContent += RmainContent;
        //    SpecsModels += RspecsModels;
        //    RUnit += Runit;
        //    RAmount += RsalesNum;
        //    RSupplier += Rsupplier;
        //    RUnitPrice += Ruitiprice;
        //    RSubtotal += Rsubtotal;
        //    if (i < Rarr1.length - 1) {
        //        RDID += ",";
        //        RProductID += ",";
        //        ROrderContent += ",";
        //        RSpecsModels += ",";
        //        RUnit += ",";
        //        RAmount += ",";
        //        RSupplier += ",";
        //        RUnitPrice += ",";
        //        RSubtotal += ",";
        //    } else {
        //        RDID += "";
        //        RProductID += "";
        //        ROrderContent += "";
        //        RSpecsModels += "";
        //        RUnit += "";
        //        RAmount += "";
        //        RSupplier += "";
        //        RUnitPrice += "";
        //        RSubtotal += "";
        //    }
        //}

        isConfirm = confirm("保存修改换货内容")
        if (isConfirm == false) {
            return false;
        }

        $.ajax({
            url: "SaveUpdateExcGoods",
            type: "Post",
            data: {
                DID: DID, OrderID: OID, EID: EID, ReturnType: EXCType, ReturnWay: EXCWay, ReturnContract: EXCYd, ReturnReason: EXCWhy, ReturnInstructions: EXCSM, EID: EID, EDID: EDID,
                //ProductID: ProductID, OrderContent: OrderContent, SpecsModels: SpecsModels, Unit: Unit,
                //Amount: Amount, Supplier: Supplier, UnitPrice: UnitPrice, Subtotal: Subtotal,
                //换货信息
                //RDID: RDID,
                RProductID: RProductID, ROrderContent: ROrderContent, RSpecsModels: RSpecsModels, RUnit: RUnit,
                RAmount: RAmount, RSupplier: RSupplier, RUnitPrice: RUnitPrice, RSubtotal: RSubtotal, Runitcost: Runitcost, Rtotalcost: Rtotalcost, Rsaleno: Rsaleno, Rprojectno: Rprojectno,
                Rjname: Rjname, Rtechnology: Rtechnology, EID: EID, EDID: EDID
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    window.parent.frames["iframeRight"].reload();
                    alert("修改退货成功！");

                    window.parent.ClosePop();
                }
                else {
                    alert("修改退换货失败-" + data.msg);
                }
            }
        });
    }
}

