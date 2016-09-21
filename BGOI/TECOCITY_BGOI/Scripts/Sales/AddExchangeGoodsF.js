$(document).ready(function () {
    if (location.search != "") {
        OrderID = location.search.split('&')[0].split('=')[1];
        ContractID = location.search.split('&')[1].split('=')[1];
    }
    LoadShipmentsDetail();
    //  addBasicDetail(DID);
    document.getElementById('HH').style.display = 'none';
    document.getElementById('AddHH').style.display = 'none';
    document.getElementById('btnDel').style.display = 'none';
    // $("#AddHH").val("enable", "false");
    $("#btnSaveExcGoods").click(function () {
        SaveExcGoods();
    });
    $("#ISEXc").click(function () {
        Checkbol = document.getElementById('ISEXc').checked
         if (Checkbol == false)
         {
             $("#HH").css("display", "none");
             document.getElementById('AddHH').style.display = 'none';
             document.getElementById('btnDel').style.display = 'none';
             return;
         }

        var cbNum = document.getElementsByName("cb");
        //  var cbNum = document.getElementsByName("cb");
        var aa = "";
        for (var i = 0; i < cbNum.length; i++) {
            var cbid = "";
            if (cbNum[i].checked == true) {
                cbid = cbNum[i].id;
                aa += cbid.substring(5) + ",";
                if (document.getElementById('ISEXc').checked) {
                    $("#HH").css("display", "block");
                    document.getElementById('AddHH').style.display = 'block';
                    document.getElementById('btnDel').style.display = 'block';

                }
            }
        }
        var salesNum = "";
        var arr1 = aa.split(',');
        if (arr1.length > 1) {
            if (document.getElementById('ISEXc').checked) {
                $("#HH").css("display", "block");
                document.getElementById('AddHH').style.display = 'block';
                document.getElementById('btnDel').style.display = 'block';

            }
        } else {
            $("#HH").css("display", "none");
            $("#AddHH").css("display", "none");
            document.getElementById('btnDel').style.display = 'none';
        }
    });


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
            if (document.getElementById('ISEXc').checked) {
                $("#HH").css("display", "block");
                document.getElementById('AddHH').style.display = 'block';
                document.getElementById('btnDel').style.display = 'block';

            }
        } else {
            $("#HH").css("display", "none");
            $("#AddHH").css("display", "none");
            document.getElementById('btnDel').style.display = 'none';
        }
    })

    $("#AddHH").click(function () {
        // CheckDetail();
        FDetail();
    })

    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});
var DID = "";
var Amount = "";
var Price = "";
var RAmount = "";
var RPrice = "";
var Subtotal = 0;
var Checkbol = false;
var OderderAmount = 0;//存订单的数量用于和退货数量和换货数量作比较
var Scount = 0;
function LoadShipmentsDetail() {
    var orderID = OrderID;
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetShipmentOrdersDetail",
        type: "post",
        data: { orderID: orderID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><input id="Check' + rowCount + '" type="checkbox" onclick="CheckRow(this)" name="cb"/><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
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
                    OderderAmount += parseInt(json[i].OrderNum);
                    $("#DetailInfo").append(html);

                }


            }
        }
    })
}
function SaveExcGoods() {
    if ($('.field-validation-error').length == 0) {
        var OID = OrderID;
        var EID = $("#EID").val();
        var EXCType = $("#EXCType").val();
        var ISF = $("#ISF").val();
        var ISEXR = $("#ISEXR").val();
        if (EXCType == "") {
            alert("退货类型不能为空");
            return;
        }
        var EXCWay = $("#EXCWay").val();
        if (EXCWay == "") {
            alert("退货方式不能为空");
            return;
        }
        var EXCYd = $("#ReturnContract").val();

        var EXCWhy = $("#ReturnReason").val();
        if (EXCWhy == "") {
            alert("退货原因不能为空");
            return;
        }
        var EXCSM = $("#ReturnInstructions").val();
        var ExcAount = 0;//退货的数量
        var ProductID = "";
        var OrderContent = "";
        var SpecsModels = "";
        var Unit = "";
        var Amount = "";
        var Supplier = "";
        var UnitPrice = "";
        var Subtotal = "";
        var ExcTotal = 0;
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
            //salesNum += document.getElementById("Amount" + arr1[i]).value + ",";
            //\
            var Did = document.getElementById("EDID" + arr1[i]).innerHTML;
            var salesNum = document.getElementById("Amount" + Did).innerHTML;
           // var uitiprice = document.getElementById("UnitPrice" + Did).innerHTML;
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
            }


        }
        //DId = DID
        //alert(DID);
        //alert(ProductID);
        //换货信息
        //var RProductID = "";
        //var ROrderContent = "";
        //var RSpecsModels = "";
        //var RUnit = "";
        //var RAmount = "";
        //var RSupplier = "";
        //var RUnitPrice = "";
        //var RSubtotal = "";
        //var RExcTotal = 0;
        //var RDID = "";
        //var Raa = "";
        //var Runitcost = "";
        //var Rtotalcost = "";
        //var Rsaleno = "";
        //var Rprojectno = "";
        //var Rjname = "";
        //var Rtechnology = "";
        //var RExcAmount = 0;//换货的数量
        //var RcbNum = document.getElementsByName("Rcb");
        //var tbody = document.getElementById("ReturnDetailInfo");
        //for (var i = 0; i < RcbNum.length; i++) {
        //    var Rcbid = "";
        //    if (RcbNum[i].checked == true) {
        //        Rcbid = RcbNum[i].id;
        //        Raa += Rcbid.substring(6) + ",";
        //    }
        //}
        //var RsalesNum = "";
        //var Rarr1 = Raa.split(',');
        ////for (var i = 0; i < Rarr1.length - 1; i++) {
        //for (var i = 0; i < tbody.rows.length; i++) {
        //    //salesNum += document.getElementById("Amount" + arr1[i]).value + ",";
        //    // var RDid = document.getElementById("RDID" + i).value;
        //    var RProductid = document.getElementById("RProductID" + i).value;
        //    var RmainContent = document.getElementById("ROrderContent" + i).value;
        //    var RspecsModels = document.getElementById("RSpec" + i).value;
        //    var Runit = document.getElementById("RUnits" + i).value;
        //    var RsalesNum = document.getElementById("RAmount" + i).value;
        //    var Rsupplier = document.getElementById("RSupplier" + i).value;
        //    var Ruitiprice = document.getElementById("RUnitPrice" + i).value;
        //    var Rsubtotal = document.getElementById("RSubtotal" + i).value;
        //    var runitcost = document.getElementById("RUnitCost" + i).value;
        //    var rtotalcost = document.getElementById("RTotalCost" + i).value;
        //    var rsaleno = document.getElementById("RSaleNo" + i).value;
        //    var rprojetno = document.getElementById("RProjectNo" + i).value;
        //    var rjname = document.getElementById("RJNAME" + i).value;
        //    var rtechnology = document.getElementById("RTechnology" + i).value;
        //    // RDeliveryTime
        //    //   var deli
        //    //  RDID += RDid;
        //    RProductID += RProductid;
        //    ROrderContent += RmainContent;
        //    SpecsModels += RspecsModels;
        //    RUnit += Runit;
        //    RAmount += RsalesNum;
        //    RExcAmount += parseInt(RsalesNum);
        //    RSupplier += Rsupplier;
        //    RUnitPrice += Ruitiprice;
        //    RSubtotal += Rsubtotal;
        //    RExcTotal += parseFloat(Rsubtotal);
        //    Runitcost += runitcost;
        //    Rtotalcost += rtotalcost;
        //    Rsaleno += rsaleno;
        //    Rprojectno += rprojetno;
        //    Rjname += rjname;
        //    Rtechnology += rtechnology;
        //    if (i < tbody.rows.length - 1) {
        //        //RDID += ",";
        //        RProductID += ",";
        //        ROrderContent += ",";
        //        RSpecsModels += ",";
        //        RUnit += ",";
        //        RAmount += ",";
        //        RSupplier += ",";
        //        RUnitPrice += ",";
        //        RSubtotal += ",";
        //        Runitcost += ",";
        //        Rtotalcost += ",";
        //        Rsaleno += ",";
        //        Rprojectno += ",";
        //        Rjname += ",";
        //        Rtechnology += ",";

        //    } else {
        //        // RDID += "";
        //        RProductID += "";
        //        ROrderContent += "";
        //        RSpecsModels += "";
        //        RUnit += "";
        //        RAmount += "";
        //        RSupplier += "";
        //        RUnitPrice += "";
        //        RSubtotal += "";
        //        Runitcost += "";
        //        Rtotalcost += "";
        //        Rsaleno += "";
        //        Rprojectno += "";
        //        Rjname += "";
        //        Rtechnology += "";
        //    }
        //}

        //if (ExcAount > OderderAmount || RExcAmount > OderderAmount)//|| (parseInt(RExcAmount) + parseInt(ExcAount) > OderderAmount)
        //{
        //    alert("退货的数量/换货的总数量不能超过订单之前的数量");
        //    return;
        //}
        //if (ROrderContent == "" && RSpecsModels == "") {
        //    alert("物品名称和规格型号不能为空");
        //    return;
        //}
        //if (Rtechnology == "") {
        //    alert("技术参数不能为空");
        //    return;
        //}
        isConfirm = confirm("确定要换货吗？")
        if (isConfirm == false) {
            return false;
        } else {
            $.ajax({
                url: "SaveExcGoods",
                type: "Post",
                data: {
                    DID: DID, OrderID: OID, EID: EID, ReturnType: EXCType, ReturnWay: EXCWay, ReturnContract: EXCYd, ReturnReason: EXCWhy, ReturnInstructions: EXCSM,
                    ContractID: ContractID,ISEXR:ISEXR,ISF:ISF,
                    Amount: Amount, Supplier: Supplier, UnitPrice: UnitPrice, Subtotal: Subtotal,
                    //换货信息
                    RProductID: RProductID, ROrderContent: ROrderContent, RSpecsModels: RSpecsModels, RUnit: RUnit,
                    RAmount: RAmount, RSupplier: RSupplier, RUnitPrice: RUnitPrice, RSubtotal: RSubtotal, Runitcost: Runitcost, Rtotalcost: Rtotalcost, Rsaleno: Rsaleno, Rprojectno: Rprojectno, Rjname: Rjname, Rtechnology: Rtechnology,
                    //退货和换货的数量和金额
                   // ExcAount: ExcAount, RExcAmount: RExcAmount, ExcTotal: ExcTotal, RExcTotal: RExcTotal
                },
                async: false,
                success: function (data) {
                    if (data.success == true) {
                        window.parent.frames["iframeRight"].reload();
                        alert("生成退换货！");
                        window.parent.ClosePop();
                    }
                    else {
                        alert("生成退换货失败-" + data.msg);
                    }
                }
            });
        }
    }
}

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    var strRow = newRowID.charAt(newRowID.length - 1);
    $("#" + newRowID).attr("class", "RowClass");
    //Amount = $("#Amount" + strRow).val();
    //Price = $("#UnitPrice" + strRow).val();
    //var cbNum = document.getElementsByName("cb");
    //   cbNum[strRow].checked = true;

    var ID = document.getElementById("EDID" + strRow).innerHTML;
    ID = "'" + ID + "'";
    //获取所在行的数量和单价
    $.ajax({
        url: "GetOrdersDetailBYDID",
        type: "post",
        data: { DID: ID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                Amount = json[0].OrderNum;
                Price = json[0].Price;
            }
        }
    });
}
function CheckRow(curCheck) {
    newRowID = curCheck.id;
    $("#DetailInfo tr").removeAttr("class");
    var strRow = newRowID.charAt(newRowID.length - 1);
    $("#" + newRowID).attr("class", "RowClass");
    var aa = "";
    DID = "";
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
        var Did = document.getElementById("EDID" + arr1[i]).innerHTML;
        DID += "'" + Did + "'";
        if (i < arr1.length - 1) {
            DID += ",";
        } else {
            DID += "";
        }
    }
    if (arr1.length > 1) {
        if (document.getElementById('ISEXc').checked) {
            $("#HH").css("display", "block");
            document.getElementById('AddHH').style.display = 'block';
            document.getElementById('btnDel').style.display = 'block';
            FDetail();
        }
    } else {
        document.getElementById('ISEXc').checked = false;
        $("#HH").css("display", "none");
        $("#AddHH").css("display", "none");
        document.getElementById('btnDel').style.display = 'none';
    }
    // alert(DID);
   // $("#ReturnDetailInfo").html("");
    //addBasicDetail(DID);
}

function selChange(id) {
    if ($('input[id=Check' + id + ']').prop("checked") == 'checked') {
        alert(id);
        return;
    }
    else {
        if (id != 0) {
            $('input[id=c' + id + ']').prop("checked", false);
        }
        $('input[id=c' + id + ']').prop("checked", true);
        $("#list").setSelection(id)
    }
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
        var Did = document.getElementById("EDID" + arr1[i]).innerHTML;
        DID += Did;
        if (i < arr1.length - 1) {
            DID += ",";
        } else {
            DID += "";
        }
    }
}


function CheckDetail() {
    //this.className = "btnTw";
    //$('#btnAddF').attr("class", "btnTh");
    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 450);
}
////
function addBasicDetail(PID) { //增加货品信息行
    document.getElementById("ReturnTable").style.display = 'block';
    rowCount = document.getElementById("ReturnDetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetBasicDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        ansyc: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="ReturnDetailInfo' + rowCount + '" onclick="RselRow(this)">'
                    html += '<td ><input id="RCheck' + rowCount + '"  type="checkbox" name="Rcb"/><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '"/>' + CountRows + '</td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="RProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ROrderContent' + rowCount + '"  style="width:60px;">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable  class="labSpec' + rowCount + ' " id="RSpec' + rowCount + '" style="width:50px;"  >' + json[i].Spec + '  </lable></td>';
                    html += '<td ><lable  class="labUnits' + rowCount + ' " id="RUnits' + rowCount + '"  style="width:50px;"  >' + json[i].Units + '</lable></td>';
                    html += '<td ><input type="text" onclick=CheckSupplier(this)   id="RSupplier' + rowCount + '" style="width:50px;" /> </td>';//readonly="readonly"
                    html += '<td ><input type="text"  onblur=RXJ(this)  id="RAmount' + rowCount + '"  style="width:50px;" /></td>';
                    html += '<td ><input type="text" onblur=RXJ(this)  id="RUnitPrice' + rowCount + '" style="width:60px;" /> </td>';
                    html += '<td ><input type="text"  readonly="readonly" id="RSubtotal' + rowCount + '"  style="width:50px;" /> </td>';
                    html += '<td ><input type="text"  id="RUnitCost' + rowCount + '" onblur=TotalCost(this) style="width:50px;" /></td>';//单位成本';
                    html += '<td ><input type="text" readonly="readonly"  id="RTotalCost' + rowCount + '" style="width:50px;" /></td>';//累计成本';
                    html += '<td ><input type="text"  id="RTechnology' + rowCount + '" style="width:50px;" /> </td>';
                    html += '<td ><input type="text"  id="RSaleNo' + rowCount + '" style="width:50px;" /></td>';//销售单号';
                    html += '<td ><input type="text"  id="RProjectNo' + rowCount + '" style="width:50px;" /></td>';//工程项目编号
                    html += '<td ><input type="text"  id="RJNAME' + rowCount + '" style="width:50px;"/></td>';//工程项目名称';
                    // html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate" style="width:170px;" id="RDeliveryTime' + rowCount + '"> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="RDID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    $("#ReturnDetailInfo").append(html);
                    CountRows = CountRows + 1;
                    rowCount += 1;
                }


            }
        }
    })
}

function FDetail() {
    $("#ReturnDetailInfo").html("");
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

    for (var i = 0; i < arr1.length - 1; i++) {
        //salesNum += document.getElementById("Amount" + arr1[i]).value + ",";
        var Did = document.getElementById("EDID" + arr1[i]).innerHTML;
        //DID += "'" + Did + "'";
        //if (i < arr1.length - 1) {
        //    DID += ",";
        //} else {
        //    DID += "";
        //}


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
        html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="REDID' + rowCount + '">' + Did + '</lable></td>';
        html += '</tr>'
        CountRows = CountRows + 1;
        rowCount += 1;
        $("#ReturnDetailInfo").append(html);
    }

  

}

//选择供应商
function CheckSupplier(rowid) {
    RowId = rowid.id;
    ShowIframe1("选择货品信息", "../SalesManage/Supplier", 850, 350);
}

function addSupplier(SID) {
    var rownumber = RowId.substr(RowId.length - 1, 1);
    var ProID = document.getElementById("ProductID" + rownumber).innerHTML;
    $.ajax({
        url: "GetSupplier",
        type: "post",
        data: { SID: SID },
        dataType: "json",
        ansyc: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                // for (var i = 0; i < json.length; i++) {
                $("#Supplier" + rownumber).val(json[0].COMNameC);
                //  $("#UnitPrice" + rownumber).val(json[0].price);
                var ProID = document.getElementById("ProductID" + rownumber).innerHTML;
                var SupID = document.getElementById("Supplier" + rownumber).value;
                $.ajax({
                    url: "GetProductPrice",
                    type: "post",
                    data: { ProID: ProID, SupID: SupID },
                    dataType: "json",
                    ansyc: false,
                    success: function (data) {

                        var json = eval(data.datas);
                        if (json.length > 0) {
                            //$("#Supplier" + rownumber).val(json[0].COMNameC);
                            $("#YPrice" + rownumber).val(json[0].price);
                            XJ();
                            HJ();
                        }
                    }
                });

            }
        }
    });
}

function addBasicDetailEX(ID) { //增加货品信息行
    var ID = ID;

    document.getElementById("ReturnTable").style.display = 'block';
    rowCount = document.getElementById("ReturnDetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetBasicDetail",//"GetOrdersDetailBYDID",
        type: "post",
        data: { PID: ID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json != null) {
                if (json.length > 0) {


                    for (var i = 0; i < json.length; i++) {
                        rowCount = i;
                        var html = "";
                        html = '<tr  id ="ReturnDetailInfo' + rowCount + '" onclick="RselRow(this)">'
                        html += '<td ><input id="RCheck' + rowCount + '" type="checkbox" name="Rcb"/><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="RProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ROrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="RSpec' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                        html += '<td ><lable class="labUnit' + rowCount + ' " id="RUnits' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                        html += '<td ><lable  class="labSupplier' + rowCount + '"  id="RSupplier' + rowCount + '">' + json[i].Manufacturer + '</lable></td>';
                        html += '<td ><input type="text" onblur=RXJ(this)  id="RAmount' + json[i].DID + '" /></td>';// value="' + (json[i].OrderNum-  document.getElementById("Amount" + json[i].DID).value) + '" 
                        html += '<td ><input type="text" onblur=RXJ(this)  id="RUnitPrice' + json[i].DID + '" value="' + json[i].Price + '"/></td>';
                        html += '<td ><input type="text"   readonly="readonly" id="RSubtotal' + json[i].DID + '" value="' + json[i].Subtotal + '"/></td>';
                        html += '<td ><lable class="labDeliveryTime' + rowCount + ' " id="RDeliveryTime' + rowCount + '">' + json[i].DeliveryTime + '</lable> </td>';
                        html += '<td style="display:none;"><lable class="labShipGoodsID' + rowCount + ' " id="RShipGoodsID' + rowCount + '">' + json[i].OrderID + '</lable> </td>';
                        html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="RDID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                        html += '</tr>'
                        $("#ReturnDetailInfo").append(html);

                    }
                }


            }
        }
    })
}

function RselRow(curRow1) {
    newRowID1 = curRow1.id;
    $("#ReturnDetailInfo tr").removeAttr("class");
    var strRow = newRowID1.charAt(newRowID1.length - 1);
    $("#" + newRowID1).attr("class", "RowClass1");
    //Amount = $("#Amount" + strRow).val();
    //Price = $("#UnitPrice" + strRow).val();
    //var cbNum = document.getElementsByName("cb");
    //   cbNum[strRow].checked = true;

    //   var DID = document.getElementById("RDID" + strRow).innerHTML;
    //   DID = "'" + DID + "'";
    //获取所在行的数量和单价
    //$.ajax({
    //    url: "GetOrdersDetailBYDID",
    //    type: "post",
    //    data: { DID: DID },
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


function DeleteRow() {
    var tbodyID = "ReturnDetailInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "RProductID", "ROrderContent", "RSpec", "RUnits", "RSupplier", "RAmount", "RUnitPrice", "RSubtotal", "RUnitCost", "RTotalCost", "RTechnology", "RSaleNo", "RProjectNo", "RJNAME","REDID"];
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
    var newPrice = rowPrice.id;
    var a = newPrice.split('UnitPrice');
    var b = newPrice.split('Amount');
    var strRow = a[1];
    if (a.length <= 1) {
        strRow = b[1];
    }
    var strUnitPrice = $("#UnitPrice" + strRow).val();
    var Count = $("#Amount" + strRow).val();
    //if (parseFloat(strUnitPrice) <= 0 || strUnitPrice == "" || parseFloat(Price) < parseFloat(strUnitPrice)) {
    //    alert("价格不能小于0为空或大于原单价");
    //    return;
    //}
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    $("#Subtotal" + strRow).val(strTotal);
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

function CheckAmount(amount) {
    var newamount = amount.id;
    //  var strRow = newamount.charAt(7, newamount.length);
    var strRow = newamount.substr(6, newamount.length);
    var strUnitPrice = $("#UnitPrice" + strRow).val();
    var Count = $("#Amount" + strRow).val();
    if (parseFloat(Amount) < parseFloat(Count) || Count == "") {
        alert("不能大于原数量或为空");
        return;
    }
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    $("#Subtotal" + strRow).val(strTotal);
}

function RXJ(S) {


    var newPrice = S.id;
    var a = newPrice.split('RUnitPrice');
    var b = newPrice.split('RAmount');
    var strRow = a[1];
    if (a.length <= 1) {
        strRow = b[1];
    }
    // var strRow = newPrice.charAt(newPrice.length - 1);
    // var strRow = newPrice.substr(7, newPrice.length);
    var strUnitPrice = $("#RUnitPrice" + strRow).val();
    var XCount = $("#RAmount" + strRow).val();

    var EAmount = document.getElementById("RAmount" + strRow).value;
    var NowRAmount = parseInt(RAmount) - parseInt(EAmount);
    //if (NowRAmount == '0') {
    //    alert("退货数量等于订单数量不能进行换货操作");
    //    return;
    //}
    //if (parseFloat(NowRAmount) < parseFloat(XCount) || XCount == "") {
    //    alert("不能大于原数量或为空");
    //    return;
    //}
    //if (parseFloat(strUnitPrice) <= 0 || strUnitPrice == "") {
    //    alert("价格不能小于0或为空");
    //    return;

    //}
    var strTotal = parseFloat(XCount) * parseFloat(strUnitPrice);

    $("#RSubtotal" + strRow).val(strTotal);

}


//0813
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
    var arr1 = aa.split(',');
    if (arr1.length > 1) {
        if (document.getElementById('ISEXc').checked) {
            $("#HH").css("display", "block");
            document.getElementById('AddHH').style.display = 'block';
            document.getElementById('btnDel').style.display = 'block';

        }
    } else {
        $("#HH").css("display", "none");
        $("#AddHH").css("display", "none");
        document.getElementById('btnDel').style.display = 'none';
        document.getElementById('ISEXc').checked = false;
    }
    //if (parseInt(Scount) <= parseInt(arr1.length - 1)) {
    //    AddExchangeDetail();
    //    Scount = arr1.length - 1;
    //}
        //if (arr2.length == cbNum.length)
        //{ }
        //else 
        //if (arr1.length > 1) {
        //    CheckDetail();
        //}
    //else {

    //}
    var arr2 = bb.split(',');
    rowCount4 = document.getElementById("ReturnDetailInfo").rows.length;


    for (var s = 0; s < rowCount4; s++) {
        var RDid = document.getElementById("REDID" + s).innerHTML;
        for (var w = 0; w < arr2.length - 1; w++) {
            var oDid = document.getElementById("EDID" + arr2[w]).innerHTML;
            // var e= $.inArray(EID, DID);
            if (oDid == RDid) {
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
