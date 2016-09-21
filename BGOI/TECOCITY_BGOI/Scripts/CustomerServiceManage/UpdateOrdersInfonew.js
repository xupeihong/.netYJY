$(document).ready(function () {
    if (location.search != "") {
        OrderID = location.search.split('&')[0].split('=')[1];
    }
    LoadOrderInfo();
    $("#Total").click(function () {
        HJ();
    });

    $("#btnSaveOrder").click(function () {
        SaveUpdateOrderInfo();
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
        url: "GetOrdersDetailnew",
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
                    //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';//序号
                    //html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';//物品编号
                    //html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';//物品名称
                    //html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';//规格型号
                    //html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';//单位

                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><input type="text" value=' + json[i].ProductID + ' id="ProductID' + rowCount + '" style="width:83px;"/></td>';
                    html += '<td ><input type="text" value=' + json[i].OrderContent + ' id="ProName' + rowCount + '" style="width:83px;"/></td>';
                    html += '<td ><input type="text" value=' + json[i].SpecsModels + ' id="Spec' + rowCount + '" style="width:100px;"/></td>';
                    html += '<td ><input type="text" value=' + json[i].OrderUnit + ' id="Units' + rowCount + '" style="width:28px;"/></td>';



                    html += '<td ><input type="text"   onblur=XJ() id="Amount' + rowCount + '" style="width:30px;" value="' + json[i].OrderNum + '"/></td>';//数量
                    html += '<td ><input type="text" onclick=CheckSupplier(this) style="width:30px;" readonly="readonly"  id="Supplier' + rowCount + '" value="' + json[i].Manufacturer + '"> </td>';//供应商
                    html += '<td ><input type="text" style="width:30px;"  onblur=XJ() id="UnitPrice' + rowCount + '" value="' + json[i].Price + '"> </td>';//单价

                    html += '<td><input type="text" onblur=XJ(this)  id="TaxRate' + rowCount + '"  style="width:30px;" value="' + json[i].TaxRate + '">%</td>';//税率

                    html += '<td ><input type="text" style="width:30px;" readonly="readonly" id="Subtotal' + rowCount + '" value="' + json[i].Subtotal + '"> </td>';//小计
                    html += '<td ><input type="text" style="width:300px;" id="Technology' + rowCount + '" value="' + json[i].Technology + '"  > </td>';//技术要求或参数
                    html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate" style="width:50px;" id="DeliveryTime' + rowCount + '" value="' + json[i].DeliveryTime + '"> </td>';// 交货时间
                    //html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    //html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
               
                }
            }
        }
    })
}
//小计
function XJ() {
    var Total = 0;
    var s = "";
    if (RowId == 0) {
        s = "0";
    } else {
        s = RowId.substr(RowId.length - 1, RowId.length);
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
    //  }
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
function CheckDetail() {
    //  ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 350);
    ShowIframe1("选择货品信息", "../InventoryManage/ChangeBasic", 500, 500);
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
                    //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    //html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    //html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    //html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    //html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Spec + '</lable> </td>';



                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><input type="text" value=' + json[i].ProductID + ' id="ProductID' + rowCount + '" style="width:83px;"/></td>';
                    html += '<td ><input type="text" value=' + json[i].ProName + ' id="ProName' + rowCount + '" style="width:83px;"/></td>';
                    html += '<td ><input type="text" value=' + json[i].Spec + ' id="Spec' + rowCount + '" style="width:100px;"/></td>';
                    html += '<td ><input type="text" value=' + json[i].Units + ' id="Units' + rowCount + '" style="width:28px;"/></td>';



                    html += '<td ><input type="text" onblur=XJ() id="Amount' + rowCount + '" style="width:30px;"/></td>';
                    html += '<td ><input type="text" onclick=CheckSupplier(this) style="width:100px;" readonly="readonly"  id="Supplier' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:30px;"  onblur=XJ() id="UnitPrice' + rowCount + '"> </td>';

                    html += '<td><input type="text" onblur=XJ(this)  id="TaxRate' + rowCount + '"  style="width:30px;">%</td>';

                    html += '<td ><input type="text" style="width:30px;" readonly="readonly" id="Subtotal' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:300px;" id="Technology' + rowCount + '"> </td>';
                    html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate" style="width:50px;" id="DeliveryTime' + rowCount + '"> </td>';
                    //html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    //html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    //html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="DID' + rowCount + '">add</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                }
            }
        }
    })
}

function addKonghang() { //增加空行
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var html = "";
    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
    html += '<td ><input type="text" id="ProductID' + rowCount + '" style="width:83px;"/></td>';
    html += '<td ><input type="text" id="ProName' + rowCount + '" style="width:83px;"/></td>';
    html += '<td ><input type="text" id="Spec' + rowCount + '" style="width:100px;"/></td>';
    html += '<td ><input type="text" id="Units' + rowCount + '" style="width:28px;"/></td>';
    html += '<td><input type="text"  onblur=XJ(this)  id="Amount' + rowCount + '"  style="width:30px;" /></td>';
    html += '<td><input type="text" onclick=CheckSupplier(this) readonly="readonly"  id="Supplier' + rowCount + '"style="width:30px;"> </td>';
    html += '<td><input type="text" onblur=XJ(this)  id="UnitPrice' + rowCount + '" style="width:30px;"> </td>';
    html += '<td><input type="text" onblur=XJ(this)  id="TaxRate' + rowCount + '"  style="width:30px;">%</td>';
    html += '<td><input type="text"  readonly="readonly" id="Subtotal' + rowCount + '"  style="width:30px;"> </td>';
    html += '<td><input type="text" style="width:300px;" id="Technology' + rowCount + '"> </td>';
    html += '<td><input type="text" onclick="WdatePicker()" class="Wdate" style="width:50px;" id="DeliveryTime' + rowCount + '"> </td>';
    html += '</tr>'
    $("#DetailInfo").append(html);

   



}

function DeleteRow() {
    var tbodyID = "DetailInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "ProName", "Spec", "Units", "Amount", "Supplier", "Remark"];

    if (newRowID != "")
        rowIndex = newRowID.replace(tbodyID, '');
    if (rowIndex != -1) {
        document.getElementById(tbodyID).deleteRow(rowIndex);
        //alert(rowIndex);
        if (rowIndex < $("#" + tbodyID + " tr").length) {
            for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
                var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                var tr1 = $("#" + tbodyID + (parseInt(i) + 1));
                tr.id = tbodyID + i;
                for (var j = 0; j < tr.childNodes.length; j++) {
                    var html = tr1.html();
                    for (var k = 0; k < typeNames.length; k++) {

                        var olPID = typeNames[k] + (parseInt(i) + 1);
                        var newid = typeNames[k] + i;

                        var reg = new RegExp(olPID, "g");

                        html = html.replace(reg, newid);

                    }
                    tr1.html(html);
                }
                $("#RowNumber" + i).html(parseInt(i) + 1);
            }
        }
        if (document.getElementById(tbodyID).rows.length > 0) {
            if (rowIndex == document.getElementById(tbodyID).rows.length)
                selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
            else
                selRow(document.getElementById(tbodyID + rowIndex), '');;
        }
    }
}

//选择供应商
function CheckSupplier(rowid) {
    RowId = rowid.id;
    // ShowIframe1("选择货品信息", "../SalesManage/Supplier", 500, 520);
    ShowIframe1("选择供应商", "../CustomerService/GetSupplier", 850, 350);
}

function addSupplier(SID) {
    var rownumber = RowId.substr(8, RowId.length - 8);
    var ProID = document.getElementById("ProductID" + rownumber).innerHTML;
    $.ajax({
        url: "GetSupplierCot",
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
    var PID = $("#PID").val();
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
    var Total = $("#Total").val();
    var PayWay = $("#PayWay").val();
    var Guarantee = $("#Guarantee").val();
    if (Guarantee == "" || Guarantee == null) {
        alert("保修日期不能为空");
        return false;
    }
    var Provider = $("#Provider").val();
    var ProvidManager = $("#ProvidManager").val();
    if (ProvidManager == "" || ProvidManager == null) {
        alert("供应方不能为空");
        return false;
    }
    var Demand = $("#Demand").val();

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
    var DeliveryTime = "";
   // var DID = "";

    var TaxRate = "";
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        //var Productid = tbody.getElementsByTagName("tr")[i].cells[1].innerText;// document.getElementById("ProductID" + i).innerHTML;
        //var mainContent = tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("ProName" + i).innerHTML;
        //var specsModels = tbody.getElementsByTagName("tr")[i].cells[3].innerText;//document.getElementById("Spec" + i).innerHTML;
        //var unit = tbody.getElementsByTagName("tr")[i].cells[4].innerText;//document.getElementById("Units" + i).innerHTML;

        var Productid = document.getElementById("ProductID" + i).value;//document.getElementById("ProductID" + i).innerHTML;
        var mainContent = document.getElementById("ProName" + i).value;//document.getElementById("ProName" + i).innerHTML;
        var specsModels = document.getElementById("Spec" + i).value;
        var unit = document.getElementById("Units" + i).value;//document.getElementById("Units" + i).innerHTML;


        var salesNum = document.getElementById("Amount" + i).value;
        var supplier = document.getElementById("Supplier" + i).value;
        var uitiprice = document.getElementById("UnitPrice" + i).value;

        var taxrate = document.getElementById("TaxRate" + i).value;

        var subtotal = document.getElementById("Subtotal" + i).value;
        var technology = document.getElementById("Technology" + i).value;
        var deliverytime = document.getElementById("DeliveryTime" + i).value;
        //var did = document.getElementById("DID" + i).innerHTML;

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
        DeliveryTime += deliverytime;
      //  DID += did;
        TaxRate += taxrate;
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
            DeliveryTime += ",";
           // DID += ",";
            TaxRate += ",";
        }
        else {
            //ID += "";
            ProductID += "";
            OrderContent += "";
            SpecsModels += "";
            Unit += "";
            Amount += "";
            Supplier += "";
            UnitPrice += "";
            Subtotal += "";
            Technology += "";
            DeliveryTime += "";
           // DID += "";
            TaxRate += ",";
        }
    }
    $.ajax({
        url: "SaveUpdateOrderInfonew",
        type: "Post",
        data: {
            PID: PID, OrderID: OrderID, OrderUnit: OrderUnit, OrderContactor: OrderContactor, OrderTel: OrderTel, OrderAddress: OrderAddress,
            UseUnit: UseUnit, UseContactor: UseContactor, UseTel: UseTel, UseAddress: UseAddress, Total: Total, PayWay: PayWay, Guarantee: Guarantee,
            Provider: Provider, ProvidManager: ProvidManager, Demand: Demand, DemandManager: DemandManager, Remark: Remark,TaxRate:TaxRate,
            ProductID: ProductID, OrderContent: OrderContent, SpecsModels: SpecsModels, Unit: Unit, Amount: Amount, Supplier: Supplier,
            UnitPrice: UnitPrice, Subtotal: Subtotal, Technology: Technology, DeliveryTime: DeliveryTime
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                setTimeout('parent.ClosePop()', 10);
                window.parent.frames["iframeRight"].reload();
                alert("修改完成！");
              //  window.parent.ClosePop();
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