$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
        OrderID = location.search.split('&')[1].split('=')[1];
    }
    LoadExchangeGoods();
    LoadExchangeDetail();
  //  CheckOrderDetail();
    $("#btnSaveExcGoods").click(function () {
        SaveUpdateExcGoods();
    });
    $("#ISEXc").click(function () {
        var Eid = $("#EID").val();
        GetDID();
        ShowIframe1("修改换货信息", "../SalesManage/UpdateReturnDetail?EID=" + Eid + "&DID=" + DID, 700, 350, '');
    });

    $("#AddHH").click(function () {
        CheckDetail();
        //  FDetail();
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
var Scount = 0;
function LoadExchangeGoods()
{
    var orderID = OrderID;
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        // url: "GetExchangeGoodsDetail",
        //
        url: "GetShipmentOrdersDetail",
        type: "post",
        //   data: { EID: ID },
        data:{ orderID: orderID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length == 0) {
                $("#DetailInfo").append("没有可再进行修改的数量");
            }
            if (json.length > 0) {
        

                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    //html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    //html += '<td ><input id="Check' + rowCount + '" type="checkbox" name="cb"/><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                    //html += '<td ><lable class="labProductID' + rowCount + ' "  style="width:50px;" id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    //html += '<td ><lable class="labOrderContent' + rowCount + ' " sid="OrderContent' + rowCount + '"  style="width:50px;">' + json[i].OrderContent + '</lable> </td>';
                    //html += '<td ><lable class="labSpecifications' + rowCount + ' "  style="width:50px;" id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    //html += '<td ><lable class="labUnit' + rowCount + ' "  style="width:50px;" id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    //html += '<td ><lable  class="labSupplier' + rowCount + '"  style="width:50px;"  id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable></td>';
                    //html += '<td ><lable   style="width:50px;"  id="Amount' + rowCount + '" >' + json[i].Amount + '</lable></td>';
                    //html += '<td ><lable style="width:50px;" id="UnitPrice' + rowCount + '" >' + json[i].ExUnit + '</lable></td>';
                    //html += '<td ><lable  style="width:50px;" id="Subtotal' + rowCount + '" >' + json[i].ExTotal + '</lable></td>';
                    //html += '<td ><lable style="width:50px;"  id="Remark' + rowCount + '" >' + json[i].Remark + '</lable></td>';
                    //html += '<td style="display:none;"><lable class="labEID' + rowCount + ' " id="EID' + rowCount + '"  style="width:50px;">' + json[i].EID + '</lable> </td>';
                    //html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    //html += '</tr>'
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><input id="Check' + rowCount + '" type="checkbox" onclick="CheckRow(this)" value="'+ json[i].DID +'" name="cb"/><lable class="labRowNumber' + rowCount + ' "  id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="Units' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable  class="labSupplier' + rowCount + '"  id="Supplier' + rowCount + '">' + json[i].Manufacturer + '</lable></td>';
                    html += '<td ><lable   id="Amount' + json[i].DID + '"  >' + json[i].ActualAmount + '</lable></td>';

                    html += '<td ><lable  id="UnitPrice' + json[i].DID + '" >' + json[i].Price + '</lable></td>';
                    html += '<td ><lable id="Subtotal' + json[i].DID + '" >' + json[i].ActualSubTotal + '</lable></td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + ' " id="DeliveryTime' + rowCount + '">' + json[i].DeliveryTime + '</lable> </td>';
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



function LoadExchangeDetail()
{
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
                                Scount++;
                            }
                            else {
                               
                            }
                        })
                    
                 
                    rowCount2 = i;
                    var html = "";
                    html = '<tr  id ="ReturnDetailInfo' + rowCount2 + '" onclick="RselRow(this)">'
                    //<input id="RCheck' + rowCount + '"  type="checkbox" name="Rcb"/>
                    html += '<td ><lable style="width:60px;" class="labRowNumber' + rowCount2 + ' "  id="RowNumber' + rowCount2 + '">' + CountRows2 + '</lable></td>';
                    html += '<td ><lable style="width:90px;" class="labProductID' + rowCount2 + ' " id="RProductID' + rowCount2 + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount2 + ' " id="ROrderContent' + rowCount2 + '" style="width:90px;">' + json[i].OrderContent + '</lable></td>';
                    html += '<td ><lable  class="labSpec' + rowCount2 + ' " id="RSpec' + rowCount2 + '" style="width:90px;"  >' + json[i].Specifications + '</lable></td>';
                    html += '<td ><lable  class="labUnits' + rowCount2 + ' " id="RUnits' + rowCount2 + '"  style="width:90px;"  >' + json[i].Unit + '</lable></td>';
                    html += '<td ><input type="text"    id="RSupplier' + rowCount2 + '" style="width:90px;" value="' + json[i].Supplier + '" /> </td>';//readonly="readonly"
                    html += '<td ><input type="text"  onblur=RXJ(this)  id="RAmount' + rowCount2 + '"  style="width:90px;" value="'+json[i].Amount+'"  /></td>';
                    html += '<td ><input type="text" onblur=RXJ(this)  id="RUnitPrice' + rowCount2 + '" style="width:90px;" value="' + json[i].Price + '" /> </td>';
                    html += '<td ><input type="text"  readonly="readonly" id="RSubtotal' + rowCount2 + '"  style="width:90px;" value="' + json[i].Subtotal + '" /> </td>';
                    html += '<td ><input type="text"  id="RUnitCost' + rowCount2 + '" onblur=TotalCost(this) style="width:90px;" value="' + json[i].UnitCost + '" /></td>';//单位成本';
                    html += '<td ><input type="text" readonly="readonly"  id="RTotalCost' + rowCount2 + '" style="width:90px;" value="' + json[i].TotalCost + '" /></td>';//累计成本';
                    html += '<td ><input type="text"  id="RTechnology' + rowCount2 + '" style="width:90px;"  value="' + json[i].Technology + '" /> </td>';
                    html += '<td ><input type="text"  id="RSaleNo' + rowCount2 + '" value="' + json[i].SaleNo + '" style="width:90px;" /></td>';//销售单号';
                    html += '<td ><input type="text"  id="RProjectNo' + rowCount2 + '" style="width:90px;" value="' + json[i].ProjectNO + '" /></td>';//工程项目编号
                    html += '<td ><input type="text"  id="RJNAME' + rowCount2 + '" style="width:90px;" value="' + json[i].JNAME + '" /></td>';//工程项目名称';
                    // html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate" style="width:170px;" id="RDeliveryTime' + rowCount + '"> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount2 + ' " id="EID' + rowCount2 + '">' + json[i].EID + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount2 + ' " id="DID' + rowCount2 + '">' + json[i].DID + '</lable> </td>';
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

function CheckDetail() {
    //this.className = "btnTw";
    //$('#btnAddF').attr("class", "btnTh");
    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 450);
}
////

function addBasicDetail(PID) { //增加货品信息行
    document.getElementById("ReturnTable").style.display = 'block';
    rowCount3 = document.getElementById("ReturnDetailInfo").rows.length;
    //判断是否是与订单重复的编码
    var tbody = document.getElementById("DetailInfo");
    var s = PID.split(',');
    if (tbody.rows.length > 0) {
        for (var j = 0; j < s.length; j++) {
            for (var i = 0; i < tbody.rows.length; i++) {
                var Productid = document.getElementById("ProductID" + i).innerHTML;
                Productid = Productid.trim()
                Productid = "'" + Productid + "'";
                if (s[j] == Productid) {
                    if (s.length >= 2 && j == 0) {
                        Productid = Productid + ",";
                        PID = PID.replace(Productid, "");
                    }
                    else if (s.length > 2) {
                        Productid = "," + Productid;
                        PID = PID.replace(Productid, "");
                    }
                    else
                        PID = PID.replace(Productid, "");
                }
            }
        }
    }
    if (PID == "") {
        alert("不能与订单重复的物品编码");
        return;
    }

    //判断换货的编码是否重复



    var tbody2 = document.getElementById("ReturnDetailInfo");
    var s2 = PID.split(',');
    for (var j = 0; j < s.length; j++) {
        for (var i = 0; i < tbody2.rows.length; i++) {
            var Productid2 = document.getElementById("RProductID" + i).innerHTML;
            Productid2 = Productid2.trim()
            Productid2 = "'" + Productid2 + "'";
            if (s2[j] == Productid2) {
                if (s2.length >= 2 && j == 0) {
                    Productid2 = Productid2 + ",";
                    PID = PID.replace(Productid2, "");
                }
                else if (s.length > 2) {
                    Productid2 = "," + Productid2;
                    PID = PID.replace(Productid2, "");
                }
                else
                    PID = PID.replace(Productid2, "");
            }
        }
    }
    if (PID == "") {
        alert("不能重复的物品编码");
        return;
    }

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
    if (parseInt(rowCount3) == parseInt(arr1.length - 1)) {
        alert("不能添加换货");
        return;
    }
    if (parseInt(PID.split(',').length) + parseInt(rowCount3) > arr1.length - 1) {
        alert("对应项超过了已选订单");
        return;
    }



    //

    //var arr1 = aa.split(',');
    //for (var i = 0; i < arr1.length - 1; i++) {
    //    //salesNum += document.getElementById("Amount" + arr1[i]).value + ",";
    //    var Did = document.getElementById("EDID" + arr1[i]).innerHTML;
    //    DID += "'" + Did + "'";
    //    if (i < arr1.length - 1) {
    //        DID += ",";
    //    } else {
    //        DID += "";
    //    }
    //}

    //var addEDI = DID;
    //addEDI= addEDI.split(',');
    var id = newRowID.split('DetailInfo')[1];
    var AEDID = document.getElementById("EDID" + id).innerHTML;
    //var ADID = document.getElementById("DID" + id).innerHTML;
    //var AEID = document.getElementById("EID" + id).innerHTML;
    var ID = PID;
    document.getElementById("ReturnTable").style.display = 'block';
    rowCount3 = document.getElementById("ReturnDetailInfo").rows.length;
    var CountRows3= parseInt(rowCount3) + 1;
    $.ajax({
        url: "GetBasicDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="ReturnDetailInfo' + rowCount3 + '" onclick="RselRow(this)">'
                    //<input id="RCheck' + rowCount + '"  type="checkbox" name="Rcb"/>
                    html += '<td ><lable style="width:60px;" class="labRowNumber' + rowCount3 + ' "  id="RowNumber' + rowCount3 + '">' + CountRows3 + '</lable></td>';
                    html += '<td ><lable style="width:90px;" class="labProductID' + rowCount3 + ' " id="RProductID' + rowCount3 + '">' + json[i].ProductID + '</lable></td>';
                    html += '<td ><lable class="labProName' + rowCount3 + ' " id="ROrderContent' + rowCount3 + '" style="width:90px;">' + json[i].ProName + '</lable></td>';
                    html += '<td ><lable  class="labSpec' + rowCount3 + ' " id="RSpec' + rowCount3 + '" style="width:90px;"  >' + json[i].Spec + '  </lable></td>';
                    html += '<td ><lable  class="labUnits' + rowCount3 + ' " id="RUnits' + rowCount3 + '"  style="width:90px;"  >' + json[i].Units + '</lable></td>';
                    html += '<td ><input type="text" onclick=CheckSupplier(this)   id="RSupplier' + rowCount3 + '" style="width:90px;" /></td>';//readonly="readonly"
                    html += '<td ><input type="text"  onblur=RXJ(this)  id="RAmount' + rowCount3 + '"  style="width:90px;" /></td>';
                    html += '<td ><input type="text" onblur=RXJ(this)  id="RUnitPrice' + rowCount3 + '" style="width:90px;" /></td>';
                    html += '<td ><input type="text"  readonly="readonly" id="RSubtotal' + rowCount3 + '"  style="width:90px;" /> </td>';
                    html += '<td ><input type="text"  id="RUnitCost' + rowCount3 + '" onblur=TotalCost(this) style="width:90px;" /></td>';//单位成本';
                    html += '<td ><input type="text" readonly="readonly"  id="RTotalCost' + rowCount3 + '" style="width:90px;" /></td>';//累计成本';
                    html += '<td ><input type="text"  id="RTechnology' + rowCount3 + '" style="width:90px;" /></td>';
                    html += '<td ><input type="text"  id="RSaleNo' + rowCount3 + '" style="width:90px;" /></td>';//销售单号';
                    html += '<td ><input type="text"  id="RProjectNo' + rowCount3 + '" style="width:90px;" /></td>';//工程项目编号
                    html += '<td ><input type="text"  id="RJNAME' + rowCount3 + '" style="width:90px;"/></td>';//工程项目名称';
                    // html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate" style="width:170px;" id="RDeliveryTime' + rowCount + '"> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount2 + ' " id="EID' + rowCount3 + '"></lable></td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount2 + ' " id="DID' + rowCount3 + '"></lable></td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount3 + ' " id="REDID' + rowCount3 + '">' + AEDID + '</lable></td>';
                    html += '</tr>'
                    $("#ReturnDetailInfo").append(html);
                    CountRows3 = CountRows3 + 1;
                    rowCount3 += 1;
                }


            }
        }
    })
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

//换货的产品对呀的订单产品默认选中

function CheckOrderDetail() {

    var tbody = document.getElementById("DetailInfo");
    var tbody1 = document.getElementById("ReturnDetailInfo");
   // var s = PID.split(',');
    if (tbody.rows.length > 0 && tbody1.rows.length > 0)
    {
        if (tbody.rows.length > tbody1.rows.length)
        {
            for (var j = 0; j < tbody.length; j++) {
                for (var i = 0; i < tbody1.rows.length; i++) {
                    var DID = document.getElementById("EDID" + i).innerHTML;
                    var EDID = document.getElementById("REDID" + i).innerHTML;

                    if (DID == EDID) {

                        var cbNum = document.getElementsByName("cb");
                            cbNum[i].checked == true;
                      
                    }
                }
            }
        }

    }

    //if (tbody.rows.length > 0) {
    //    for (var j = 0; j < s.length; j++) {
    //        for (var i = 0; i < tbody.rows.length; i++) {
    //            var Productid = document.getElementById("ProductID" + i).innerHTML;
    //            Productid = Productid.trim()
    //            Productid = "'" + Productid + "'";
    //            if (s[j] == Productid) {
    //                if (s.length >= 2 && j == 0) {
    //                    Productid = Productid + ",";
    //                    PID = PID.replace(Productid, "");
    //                }
    //                else if (s.length > 2) {
    //                    Productid = "," + Productid;
    //                    PID = PID.replace(Productid, "");
    //                }
    //                else
    //                    PID = PID.replace(Productid, "");
    //            }
    //        }
    //    }
    //}
}

function DeleteRow() {
    var tbodyID = "ReturnDetailInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "RProductID", "ROrderContent", "RSpec", "RUnits", "RSupplier", "RAmount", "RUnitPrice", "RSubtotal", "RUnitCost", "RTotalCost", "RTechnology", "RSaleNo", "RProjectNo", "RJNAME", "EID", "DID", "REDID"];
    //var rowIndex = -1;
    if (newRowID1 != "")
        rowIndex = newRowID1.replace(tbodyID, '');
    if (rowIndex != -1) {
        document.getElementById(tbodyID).deleteRow(rowIndex);

        if (rowIndex < document.getElementById(tbodyID).rows.length) {
            for (var i = rowIndex; i < document.getElementById(tbodyID).rows.length; i++) {
                var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                tr.id = tbodyID + i;
                tr.childNodes[0].innerHTML = parseInt(i) + 1;
                for (var j = 1; j < tr.childNodes.length; j++) {
                    var td = tr.childNodes[j];
                    td.childNodes[0].id = typeNames[j] + i;
                    td.childNodes[0].name = typeNames[j] + i;

                }
            }
        }
        if (document.getElementById(tbodyID).rows.length > 0) {

            if (rowIndex == document.getElementById(tbodyID).rows.length)
                RselRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
            else
                RselRow(document.getElementById(tbodyID + rowIndex), '');
        }
    }
    //   HJ();
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
  //  Check
   // newRowID1 = "RowNumber" + curRow.id.splitt('Check')[0];
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
    var strRow = newRowID.charAt(newRowID.length - 1);

  //  newRowID1 = newRowID.split('DetailInfo')[1];
   // newRowID1 = "ReturnDetailInfo" + newRowID1;


    //var ID = document.getElementById("DID" + strRow).innerHTML;
    ////ID = "'" + ID + "'";
    //$.ajax({
    //    url: "GetExchangeDetailByDID",
    //    type: "post",
    //    data: { DID: ID },
    //    dataType: "json",
    //    success: function (data) {
    //        var json = eval(data.datas);
    //        if (json.length > 0) {
    //            Amount = json[0].Amount;
    //            Price = json[0].ExUnit;
    //        }
    //    }
    //});
}


function RselRow(curRow) {
    newRowID1 = curRow.id;
    $("#ReturnDetailInfo tr").removeAttr("class");
    var strRow = newRowID1.charAt(newRowID1.length - 1);
    $("#" + newRowID1).attr("class", "RowClass");
    //Amount = $("#Amount" + strRow).val();
    //Price = $("#UnitPrice" + strRow).val();
    //var cbNum = document.getElementsByName("cb");
    //   cbNum[strRow].checked = true;

  //  var ID = document.getElementById("RDID" + strRow).innerHTML;
    //ID = "'" + ID + "'";
    //获取所在行的数量和单价
    //$.ajax({
    //    url: "GetExchangeDetailByDID",
    //    type: "post",
    //    data: { DID: ID },
    //    dataType: "json",
    //    success: function (data) {

    //        var json = eval(data.datas);
    //        if (json.length > 0) {
    //            RAmount = json[0].OrderNum;
    //            RPrice = json[0].Price;
    //        }
    //    }
    //});
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
      
        //DId = DID
        //alert(DID);
        //alert(ProductID);
        //换货信息
        //换货
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
        var EID = "";
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

            var RProductid = document.getElementById("RProductID" + i).innerHTML;
            var RmainContent = document.getElementById("ROrderContent" + i).innerHTML;
            var RspecsModels = document.getElementById("RSpec" + i).innerHTML;
            var Runit = document.getElementById("RUnits" + i).innerHTML;
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
            var edid=  document.getElementById("REDID" + i).innerHTML;
            var eid = document.getElementById("EID" + i).innerHTML;

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
            EID += eid;
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
        
        isConfirm = confirm("保存修改退货内容")
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
                RAmount: RAmount, RSupplier: RSupplier, RUnitPrice: RUnitPrice, RSubtotal: RSubtotal,Runitcost:Runitcost,Rtotalcost:Rtotalcost,Rsaleno:Rsaleno,Rprojectno:Rprojectno,
                Rjname:Rjname,Rtechnology:Rtechnology,EID:EID,EDID:EDID
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

function CheckRow(curCheck) {

    newRowID = curCheck.id;


   // var s = newRowID.split('Check');
   // newRowID1 = newRowID.split('Check')[1];
   // newRowID1 = "ReturnDetailInfo" + newRowID1;
    $("#DetailInfo tr").removeAttr("class");
    var strRow = newRowID.charAt(newRowID.length - 1);
    $("#" + newRowID).attr("class", "RowClass");
    var aa = "";
    var bb = "";
    DID = "";
    var cbNum = document.getElementsByName("cb");
    for (var i = 0; i < cbNum.length; i++) {
        var cbid = "";
        var bbid = "";
        if (cbNum[i].checked == true) {
            cbid = cbNum[i].id;
            aa += cbid.substring(5) + ",";
        } 
        if (cbNum[i].checked == false) {
            bbid = cbNum[i].id;
            bb += bbid.substring(5) + ",";
        }
    }
    var salesNum = "";
    var arr1 = aa.split(',');
    for (var i = 0; i < arr1.length - 1; i++) {
        //salesNum += document.getElementById("Amount" + arr1[i]).value + ",";
        var Did = document.getElementById("EDID" + arr1[i]).innerHTML;
        DID += "'" + Did + "'";
        if (i < arr1.length - 1) {
            DID += ",";
        } else {
            DID += "";
        }
    }

    if (parseInt(Scount) <=parseInt(arr1.length-1))
    {
        CheckDetail();
        Scount = arr1.length - 1;
    } 
    //if (arr2.length == cbNum.length)
    //{ }
    //else 
    //if (arr1.length > 1) {
    //    CheckDetail();
    //}
    else {

    }
    var arr2 = bb.split(',');
    rowCount4 = document.getElementById("ReturnDetailInfo").rows.length;


    for (var s = 0; s < rowCount4; s++) {
        var RDid = document.getElementById("REDID" + s).innerHTML;
        for (var w = 0; w < arr2.length - 1; w++) {
        var oDid = document.getElementById("EDID" + arr2[w]).innerHTML;
       // var e= $.inArray(EID, DID);
        if (oDid == RDid)
        {
            newRowID1 = "ReturnDetailInfo" + s;
            DeleteRow();
            rowCount4 = document.getElementById("ReturnDetailInfo").rows.length;
        }
        } 
       
    }





   
    //for (var t = 0; t < arr2.length - 1; t++) {
    //    var EID=document.getElementById("EDID" +arr2[t]).innerHTML;
    //    for (var i = 0; i < rowCount4; i++) {
    
    //      //  newRowID1 = "ReturnDetailInfo" + arr2[t];
    //        //newRowID1= document.getElementById("Row" + arr1[i]).innerHTML;

    //        var REDID = document.getElementById("REDID" + i).innerHTML;
    //       //返回 3,


    //        var p = $.inArray(REDID, arr2);
    //        if (p!=-1)
    //        {
    //            newRowID1 = "ReturnDetailInfo" + p;
    //            DeleteRow();
    //        }
            
    //    }
       
    //}
}




