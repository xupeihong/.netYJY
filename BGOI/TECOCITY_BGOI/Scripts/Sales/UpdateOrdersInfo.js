$(document).ready(function () {
    if (location.search != "") {
        OrderID = location.search.split('&')[0].split('=')[1];
    }
    LoadOrderInfo();
    $("#Total").click(function () {
        HJ();
    });

    $("#btnSaveOrder").click(function () {
        //提交审批页面
        if (SP !=0)
        {
              DID= OrderID;
              var texts = DID + "@" + "订单修改其他部门审批";
              ShowIframe1("提交审批", "../SalesManage/SubmitUpdateApproval?id=" + texts, 700, 400, '');
              }
        else   if (SP ==0 && SP2 == 1)
        {
                DID = OrderID;
                var texts = DID + "@" + "订单修改审批";
                ShowIframe1("提交审批", "../SalesManage/SubmitUpdateApproval?id=" + texts, 700, 400, '');
              //  window.parent.OpenDialog("提交审批", "../SalesManage/SubmitUpdateApproval?id=" + texts, 700, 500, '')
        }
      else{  SaveUpdateOrderInfo();}
    })
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});

var RowId = 0;
function LoadOrderInfo() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetOrdersDetail",
        type: "post",
        data: { OrderID: OrderID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><input  class="labProName' + rowCount + ' " id="ProName' + rowCount + '" value="' + json[i].OrderContent + '"  style="width:90px;" onchange=ChangeP() /> </td>';
                    html += '<td ><input class="labSpec' + rowCount + ' " id="Spec' + rowCount + '" value="' + json[i].SpecsModels + '"  style="width:90px;" onchange=ChangeP() /> </td>';
                    html += '<td ><input type="text" class="labUnits' + rowCount + ' " id="Units' + rowCount + '" vlaue="' + json[i].OrderUnit + '" style="width:90px;"/> </td>';
                    html += '<td ><input type="text" onchange=ChangeP()  onblur=XJ(this) id="Amount' + rowCount + '" style="width:90px;" value="' + json[i].OrderNum + '"/></td>';
                    html += '<td ><input type="text" style="width:90px;" readonly="readonly"  id="Supplier' + rowCount + '" value="' + json[i].Manufacturer + '"  style="width:90px;" /> </td>';//onclick=CheckSupplier(this) 
                    html += '<td ><input type="text" style="width:90px;"  onblur=XJ(this) id="UnitPrice' + rowCount + '" onchange=ChangeP2() value="' + json[i].Price + '"  /> </td>';
                    html += '<td ><input type="text" style="width:90px;" readonly="readonly" id="Subtotal' + rowCount + '" onchange=ChangeP2()   value="' + json[i].Subtotal + '" /> </td>';
                    html += '<td ><input type="text"  id="UnitCost' + rowCount + '"  style="width:90px;"  value="' + json[i].UnitCost + '" onblur=TotalCost(this) /></td>';//单位成本';
                    html += '<td ><input type="text"  id="TotalCost' + rowCount + '" style="width:90px;" value="' + json[i].TotalCost + '" /></td>';//累计成本';
                    html += '<td ><input type="text" style="width:100px;" id="Technology' + rowCount + '" onchange=ChangeP() value="' + json[i].Technology + '"  /> </td>';
                    html += '<td ><input type="text"  id="SaleNo' + rowCount + '" style="width:90px;"  value="' + json[i].SaleNo + '" /></td>';//销售单号';
                    html += '<td ><input type="text"  id="ProjectNo' + rowCount + '" style="width:90px;" value="' + json[i].ProjectNO + '" /></td>';//工程项目编号
                    html += '<td ><input type="text"  id="JNAME' + rowCount + '" style="width:90px;" value="' + json[i].JNAME + '" /></td>';//工程项目名称';
                    //html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate" style="width:170px;" id="DeliveryTime' + rowCount + '" value="' + json[i].DeliveryTime + '"> </td>';
                    //html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                  //  html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows= CountRows + 1;
                }


            }
        }
    })
}



//小计
function XJ(rowid) {
    RowId = rowid.id;
    var a = RowId.split('Amount');
    var b = RowId.split('UnitPrice');
    var Total = 0;
    var s = "";

    if (a.length == 2) {
        s =a[1];
    }
    if (b.length == 2) {
        s = b[1];
    }
    var Total = 0;
    var tbody = document.getElementById("DetailInfo");
   // for (var i = 0; i < tbody.rows.length; i++) {
        var Amount = document.getElementById("Amount" + s).value;
        var UnitPrice = document.getElementById("UnitPrice" + s).value;
        if (Amount == "" || Amount == null) {
            Amount = "0";
        }
        if (UnitPrice == "" || UnitPrice == null) {
            UnitPrice = "0.00";
        }
        Total = parseFloat(Amount) * parseFloat(UnitPrice);

        $("#Subtotal" + s).val(Total);
        HJ();
        TotalCost(rowid);
  //  }
}
//合计
function HJ() {
    var Total = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
         // var Subtotal = document.getElementById("Subtotal" + i).value;
        var Subtotal = $("#Subtotal" + i).val();
        if (Subtotal == "" || Subtotal == null) {
            Subtotal = "0";
        }
        Total = Total + parseFloat(Subtotal);
    }
    $("#OrderActualTotal").val(Total);
}


//累计成本
function TotalCost(rowrid) {
    RowId = rowrid.id;
    var a = RowId.split('Amount');
    var b = RowId.split('UnitCost');
    var c = RowId.split('UnitPrice');
    var Total = 0;
    var s = "";
    if (a.length == 2) {
        s = a[1];
    }
    if (b.length == 2) {
        s = b[1];
    }
    if (c.length == 2) {
        s = c[1];
    }
    var tbody = document.getElementById("DetailInfo");
    //for (var i = 0; i < tbody.rows.length; i++) {
    var Amount = document.getElementById("Amount" + s).value;
    var UnitCost = document.getElementById("UnitCost" + s).value;
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
    $("#TotalCost" + s).val(Total);
    HJ();
}



function CheckDetail() {
    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 450);
}
function addBasicDetail(PID) { //增加货品信息行

    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //var strPID = $("#ProductID").val();
    //$("#ProductID").val(strPID + "," + ProductID);
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
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><input class="labProName' + rowCount + ' " id="ProName' + rowCount + '" value="' + json[i].ProName + '" style="width:90px;" /> </td>';
                    html += '<td ><input class="labSpec' + rowCount + '" id="Spec' + rowCount + '"value="' + json[i].Spec + '" style="width:90px;" /> </td>';
                    html += '<td ><input type="text" class="labUnits' + rowCount + ' " id="Units' + rowCount + '" style="width:90px;" value="' + json[i].Units + '"/></td>';
                    html += '<td ><input type="text" onblur=XJ(this) id="Amount' + rowCount + '" style="width:90px;"/></td>';
                    html += '<td ><input type="text" style="width:90px;" readonly="readonly"  id="Supplier' + rowCount + '" /> </td>';
                    html += '<td ><input type="text" style="width:90px;"  onblur=XJ(this) id="UnitPrice' + rowCount + '" /> </td>';//onclick=CheckSupplier(this) 
                    html += '<td ><input type="text" style="width:90px;" readonly="readonly" id="Subtotal' + rowCount + '" /> </td>';
                    html += '<td ><input type="text"  id="UnitCost' + rowCount + '" onblur=TotalCost(this) style="width:90px;" /></td>';//单位成本';
                    html += '<td ><input type="text"  readonly="readonly"  id="TotalCost' + rowCount + '" style="width:90px;" /></td>';//累计成本';
                    html += '<td ><input type="text" style="width:90px;" id="Technology' + rowCount + '" /> </td>';
               
                    html += '<td ><input type="text"  id="SaleNo' + rowCount + '" style="width:90px;" /></td>';//销售单号';
                    html += '<td ><input type="text"  id="ProjectNo' + rowCount + '" style="width:90px;" /></td>';//工程项目编号
                    html += '<td ><input type="text"  id="JNAME' + rowCount + '" style="width:90px;"/></td>';//工程项目名称';

                    //html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate" style="width:170px;" id="DeliveryTime' + rowCount + '"> </td>';
                    //html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                  //  html += '<td style="display:none;"><lable class="labPID' + rowCount + '" id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                  //  html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="DID' + rowCount + '"></lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
                    rowCount += 1;
                }


            }
        }
    })
}

function DeleteRow() {
    var tbodyID = "DetailInfo";
    var rowIndex = -1;
   var typeNames = ["RowNumber", "ProductID", "ProName", "Spec", "Units", "Amount", "Supplier", "UnitPrice", "Subtotal", "UnitCost", "TotalCost", "Technology", "SaleNo","ProjectNo","JNAME"];

    var rowIndex = -1;
    if (newRowID != "")
        rowIndex = newRowID.replace(tbodyID, '');
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
                selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
            else
                selRow(document.getElementById(tbodyID + rowIndex), '');
        }
    }
   HJ();
}

//选择供应商
function CheckSupplier(rowid) {
    RowId = rowid.id;
    ShowIframe1("选择货品信息", "../SalesManage/Supplier",850,350);
}

function addSupplier(SID) {
    var rownumber = RowId.substr(8, RowId.length - 8);
    var ProID = document.getElementById("ProductID" + rownumber).innerHTML;
    $.ajax({
        url: "GetSupplier",
        type: "post",
        data: { SID: SID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
              //  for (var i = 0; i < json.length; i++) {
                $("#Supplier" + rownumber).val(json[0].COMNameC);
                $("#UnitPrice" + rownumber).val(json[0].Price);
                XJ();
               // }
            }
        }
    });
}
//
//保存订单
function SaveUpdateOrderInfo() {
    var PID =$("#PID").val();
    var OrderID = $("#OrderID").val();
    var OrderUnit = $("#OrderUnit").val();
    if (OrderUnit == "" || OrderUnit == null) {
        alert("订货单位不能为空");
        return false;
    }
    var OrderContactor = $("#OrderContactor").val();
    var OrderTel = $("#OrderTel").val();
    var OrderAddress = $("#OrderAddress").val();
    var UseUnit = $("#UseUnit").val();
    var UseContactor = $("#UseContactor").val();
    var UseTel = $("#UseTel").val();
    var UseAddress = $("#UseAddress").val();
    var OrderActualTotal = $("#OrderActualTotal").val();
    var PayWay = $("#PayWay").val();
    var Guarantee = $("#Guarantee").val();
    if (Guarantee == "" || Guarantee == null) {
        alert("保修日期不能为空");
        return false;
    }
    var DeliveryTime = $("#DeliveryTime").val();
    var Provider = $("#Provider").val();
    var ProvidManager = $("#ProvidManager").val();
    if (ProvidManager == "" || ProvidManager == null) {
        alert("供应方不能为空");
        return false;
    }
    var Demand = $("#Demand").val();
    var ChannelsFrom = $("#ChannelsFrom").val();
    var DemandManager = $("#DemandManager").val();
    var Remark = $("#Remark").val();
    //订单详细表
    var ProductID = "";
    var OrderContent = "";
    var SpecsModels = "";
    var Supplier = "";
    var Unit = "";
    var Amount = "";
    var UnitPrice = "";
    var Subtotal = "";//小计
    var Technology = "";
    var UnitCost = "";
    var TotalCost = "";
    var SaleNo = "";
    var ProjectNo = "";
    var JNAME = "";
    //var DeliveryTime = "";
   // var DID = "";
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Productid = document.getElementById("ProductID" + i).innerHTML;
        var mainContent = document.getElementById("ProName" + i).value;
        var specsModels = document.getElementById("Spec" + i).value;
        var unit = document.getElementById("Units" + i).value;
        var salesNum = document.getElementById("Amount" + i).value;
        var supplier = document.getElementById("Supplier" + i).value;
        var uitiprice = document.getElementById("UnitPrice" + i).value;
        var subtotal = document.getElementById("Subtotal" + i).value;
        var technology = document.getElementById("Technology" + i).value;
        var unitcost = document.getElementById("UnitCost" + i).value;
        var totalcost = document.getElementById("TotalCost" + i).value;
        var saleno = document.getElementById("SaleNo" + i).value;
        var projetno = document.getElementById("ProjectNo" + i).value;
        var jname = document.getElementById("JNAME" + i).value;
      //  var technology = document.getElementById("Technology" + i).value;
       // var deliverytime = document.getElementById("DeliveryTime" + i).value;
       // var did = document.getElementById("DID" + i).innerHTML;

        //ID += parseInt(i + 1);
        ProductID += Productid;
        OrderContent += mainContent;
        SpecsModels += specsModels;
        Unit += unit;
        Amount += salesNum;
        Supplier += supplier;
        UnitPrice += uitiprice;
        Subtotal += subtotal;
        Technology += technology;
        UnitCost += unitcost;
        TotalCost += totalcost;
        SaleNo += saleno;
        ProjectNo += projetno;
        JNAME += jname;
    //    DeliveryTime += deliverytime;
      //  DID+=did;
        if (i < tbody.rows.length - 1) {
            //ID += ",";
            ProductID += ",";
            OrderContent += ",";
            SpecsModels += ",";
            Unit += ",";
            Amount += ",";
            Supplier += ",";
            UnitPrice += ",";
            Subtotal += ",";
            Technology += ",";
            UnitCost += ",";
            TotalCost += ",";
            SaleNo += ",";
            ProjectNo += ",";
            JNAME += ",";
         //   DeliveryTime += ",";
         //   DID += ",";
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
            Subtotal += "";
            Technology += "";
            UnitCost += "";
            TotalCost += "";
            SaleNo += "";
            ProjectNo += "";
            JNAME += "";
            //DeliveryTime += "";
           // DID += "";
        }
    }
    $.ajax({
        url: "SaveUpdateOrderInfo",
        type: "Post",
        data: {
            PID:PID,OrderID: OrderID, OrderUnit: OrderUnit, OrderContactor: OrderContactor, OrderTel: OrderTel, OrderAddress: OrderAddress,DeliveryTime:DeliveryTime,
           UseUnit: UseUnit, UseContactor: UseContactor, UseTel: UseTel, UseAddress: UseAddress, OrderActualTotal: OrderActualTotal, PayWay: PayWay, Guarantee: Guarantee, Provider: Provider, ProvidManager: ProvidManager, Demand: Demand, DemandManager: DemandManager, Remark: Remark, ChannelsFrom: ChannelsFrom,
             ProductID: ProductID, OrderContent: OrderContent, SpecsModels: SpecsModels, Unit: Unit, Amount: Amount, Supplier: Supplier, UnitPrice: UnitPrice, Subtotal: Subtotal, Technology: Technology, UnitCost: UnitCost, TotalCost: TotalCost, SaleNo: SaleNo, ProjectNo: ProjectNo,JNAME:JNAME//, DID: DID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("修改完成！");
                window.parent.ClosePop();
            }
            else {
                alert("修改失败！");
            }
        }
    });
}

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

var SP = 0;
var SP2 = 0;
//订单单价改变
function ChangeP()
{
    SP = 1;
}
function ChangeP2()
{
    SP2 = 1;
}