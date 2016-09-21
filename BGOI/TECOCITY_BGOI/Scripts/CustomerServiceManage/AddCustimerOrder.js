$(document).ready(function () {
    //$("#Showlist").hidde();
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
        //  Type = location.search.split('&')[1].split('=')[1];
    }
    //   Jq();
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    })
    LoadProjectDetail();
    $("#BXtxt").val("");
    //document.getElementById('div1').style.display = 'none';
    //document.getElementById('div2').style.display = 'none';

    //if (ID != "" && ID != null) {
    //    document.getElementById('div1').style.display = 'block';
    //    document.getElementById('div2').style.display = 'none';
    //}
    //else {
    //    document.getElementById('div2').style.display = 'block';
    //    document.getElementById('div1').style.display = 'none';
    //}
    // document.getElementById('div1').style.display = 'none';
    // document.getElementById('div2').style.display = 'none';
    $("#btnSaveOrder").click(function () {
        SaveOrderInfo();
    })
    $("#Total").click(function () {
        HJ();
    });
});
var ID;
var curPage = 1;
var OnePageCount = 15;
//var PID;
var RowId = 0;
var isConfirm = false;
var Type;
//加载项目的物品数据
function LoadProjectDetail() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var PID = ID;
    $.ajax({
        url: "getProjectDetailGrid",
        type: "post",
        data: { PID: PID },
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
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><input type="text" onblur=XJ() id="Amount' + rowCount + '" value="' + json[i].Amount + '" style="width:30px;"/></td>';
                    html += '<td ><input type="text" onclick=CheckSupplier(this) id="Supplier' + rowCount + '"> </td>';
                    html += '<td ><input type="text"  id="UnitPrice' + rowCount + '"  onblur=XJ() > </td>';
                    html += '<td  style="width:60px;" ><input type="text" onblur=XJ(this)  id="TaxRate' + rowCount + '"  style="width:30px;" />%</td>';
                    html += '<td ><input type="text"  id="Subtotal' + rowCount + '" style="width:60px;"/></td>';
                    html += '<td ><input type="text" style="width:100px;" id="Technology' + rowCount + '"> </td>';
                    html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate" style="width:170px;" id="DeliveryTime' + rowCount + '"> </td>';
                    html += '<td  td style="display:none;"><lable class="labYPrice' + rowCount + ' " id="YPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
}
//添加物品

function CheckDetail() {
    //this.className = "btnTw";
    //$('#btnAddF').attr("class", "btnTh");
    //ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 350);
    ShowIframe1("选择货品信息", "../InventoryManage/ChangeBasic", 500, 500);
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
    html += '<td><input type="text"  onblur=SL(this)  id="Amount' + rowCount + '"  style="width:30px;" value=0 \/></td>';
    html += '<td><input type="text" onclick=CheckSupplier(this) readonly="readonly"  id="Supplier' + rowCount + '"style="width:30px;"> </td>';
    html += '<td><input type="text" onblur=DJ(this)  id="UnitPrice' + rowCount + '" style="width:30px;" value=0 \/> </td>';
    html += '<td><input type="text" onblur=ZK(this)  id="TaxRate' + rowCount + '"  style="width:30px;" value=0 \/>%</td>';
    html += '<td><input type="text"  readonly="readonly" id="Subtotal' + rowCount + '"  style="width:30px;" > </td>';
    html += '<td><input type="text" style="width:300px;" id="Technology' + rowCount + '"> </td>';
    html += '<td><input type="text" onclick="WdatePicker()" class="Wdate" style="width:50px;" id="DeliveryTime' + rowCount + '"> </td>';
    html += '</tr>'
    $("#DetailInfo").append(html);

}

function SL(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Amount = $("#" + newCount).val();
    var strRow = newCount.substring(6);

    //var strRow = newCount.charAt(newCount.length - 1);
    //var strU = "#UnitPrice" + strRow;
    //var strUnitPrice = $(strU).text();

    var strTaxRate = document.getElementById("TaxRate" + strRow).value / 100;
    var strUnitPrice = document.getElementById("UnitPrice" + strRow).value;// document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[6].innerText;
    if (strTaxRate != 0) {
        var strTotal = parseFloat(Amount) * parseFloat(strUnitPrice) * parseFloat(strTaxRate);
        $("#Subtotal" + strRow).val(strTotal);
    } else {
        var strTotal = parseFloat(Amount) * parseFloat(strUnitPrice);
        $("#Subtotal" + strRow).val(strTotal);
    }
    GetAmount();
}
function DJ(rowcount) {
    var newCount = rowcount.id;
    var UnitPrice = $("#" + newCount).val();
    var strRow = newCount.substring(9);
    // var strRow = newCount.charAt(newCount.length - 1);
    //var strU = "#UnitPrice" + strRow;
    //var strUnitPrice = $(strU).text();
    var strAmount = document.getElementById("Amount" + strRow).value;// document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[5].innerText;
    var strTaxRate = document.getElementById("TaxRate" + strRow).value / 100;
    if (strTaxRate != 0) {
        var strTotal = parseFloat(strAmount) * parseFloat(UnitPrice) * parseFloat(strTaxRate);
        $("#Subtotal" + strRow).val(strTotal);
    } else {
        var strTotal = parseFloat(strAmount) * parseFloat(UnitPrice);
        $("#Subtotal" + strRow).val(strTotal);
    }

    GetAmount();
}
function ZK(rowcount) {
    var newCount = rowcount.id;
    var TaxRate = $("#" + newCount).val() / 100;
    var strRow = newCount.substring(7);
    // var strRow = newCount.charAt(newCount.length - 1);
    //var strU = "#UnitPrice" + strRow;
    //var strUnitPrice = $(strU).text();
    var strAmount = document.getElementById("Amount" + strRow).value;// document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[5].innerText;
    var strUnitPrice = document.getElementById("UnitPrice" + strRow).value;
    if (TaxRate != 0) {
        var strTotal = parseFloat(strAmount) * parseFloat(strUnitPrice) * parseFloat(TaxRate);
        $("#Subtotal" + strRow).val(strTotal);
    } else {
        var strTotal = parseFloat(strAmount) * parseFloat(strUnitPrice);
        $("#Subtotal" + strRow).val(strTotal);
    }

    GetAmount();
}
function GetAmount() {  //获取总数金额
    $("#Total").val("");
    var Amount = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var totalAmount = document.getElementById("Subtotal" + i).value;
        if (totalAmount == "" || totalAmount == null) {
            totalAmount = "0";
        }
        Amount = Amount + parseFloat(totalAmount);

        $("#Total").val(Amount);
    }
}





function addBasicDetail(PID) { //增加货品信息行
    //判断重复数据
    var tbody = document.getElementById("DetailInfo");
    if (tbody.rows.length == 0) {

        //var strPID = $("#ProductID").val();
        //$("#ProductID").val(strPID + "," + ProductID);
        $.ajax({
            url: "GetBasicDetail",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            ansyc: false,
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    $("#myTable DetailInfo").html("");
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                        html += '<td><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td><input type="text"  class="labSpec' + rowCount + ' " id="Spec' + rowCount + '" value="' + json[i].Spec + '" /></td>';
                        html += '<td><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td><input type="text"  onblur=XJ(this)  id="Amount' + rowCount + '"  style="width:30px;" /></td>';
                        html += '<td><input type="text" onclick=CheckSupplier(this) readonly="readonly"  id="Supplier' + rowCount + '"> </td>';
                        html += '<td><input type="text" onblur=XJ(this)  id="UnitPrice' + rowCount + '" style="width:30px;"> </td>';
                        html += '<td><input type="text" onblur=XJ(this)  id="TaxRate' + rowCount + '"  style="width:30px;">%</td>';
                        html += '<td><input type="text"  readonly="readonly" id="Subtotal' + rowCount + '"  style="width:30px;"> </td>';
                        html += '<td><input type="text" style="width:30px;" id="Technology' + rowCount + '"> </td>';
                        html += '<td><input type="text" onclick="WdatePicker()" class="Wdate" style="width:150px;" id="DeliveryTime' + rowCount + '"> </td>';
                        html += '<td style="display:none;"><lable class="labYPrice' + rowCount + ' " id="YPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                        html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                }
            }
        })
    } else {
        var strPID = PID.replace("'", "").replace("'", "");
        var obj2 = strPID.split(",");
        for (var i = 0; i < tbody.rows.length; i++) {
            var pID = tbody.getElementsByTagName("tr")[i].cells[1].innerText;
            for (var j = 0; j < obj2.length; j++) {
                var newpid = obj2[j].replace("'", "").replace("'", "");
                if (newpid.replace(/[ ]/g, "") == pID.replace(/[ ]/g, "")) {
                    return;
                }
            }
        }
        //var strPID = $("#ProductID").val();
        //$("#ProductID").val(strPID + "," + ProductID);
        $.ajax({
            url: "GetBasicDetail",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            ansyc: false,
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    $("#myTable DetailInfo").html("");
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                        html += '<td><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td><input type="text"  class="labSpec' + rowCount + ' " id="Spec' + rowCount + '" value="' + json[i].Spec + '" /></td>';
                        html += '<td><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td><input type="text"  onblur=XJ(this)  id="Amount' + rowCount + '"  style="width:30px;" /></td>';
                        html += '<td><input type="text" onclick=CheckSupplier(this) readonly="readonly"  id="Supplier' + rowCount + '"> </td>';
                        html += '<td><input type="text" onblur=XJ(this)  id="UnitPrice' + rowCount + '" style="width:30px;"> </td>';
                        html += '<td><input type="text" onblur=XJ(this)  id="TaxRate' + rowCount + '"  style="width:30px;">%</td>';
                        html += '<td><input type="text"  readonly="readonly" id="Subtotal' + rowCount + '"  style="width:30px;"> </td>';
                        html += '<td><input type="text" style="width:30px;" id="Technology' + rowCount + '"> </td>';
                        html += '<td><input type="text" onclick="WdatePicker()" class="Wdate" style="width:150px;" id="DeliveryTime' + rowCount + '"> </td>';
                        html += '<td style="display:none;"><lable class="labYPrice' + rowCount + ' " id="YPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                        html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                }
            }
        })
    }




}
//选择供应商
function CheckSupplier(rowid) {
    RowId = rowid.id;
    ShowIframe1("选择供应商", "../CustomerService/GetSupplier", 850, 350);
}
//供应商
function addSupplier(SID) {
    var rownumber = RowId.substr(RowId.length - 1, 1);
    var ProID = document.getElementById("ProductID" + rownumber).innerHTML;
    $.ajax({
        url: "GetSupplierCot",
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
//
function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function DeleteRow() {
    var tbodyID = "DetailInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "ProName", "Spec", "Units", "Amount", "Supplier", "Remark"];

    if (newRowID != "")
        rowIndex = newRowID.replace(tbodyID, '');
    if (rowIndex != -1) {
        document.getElementById(tbodyID).deleteRow(rowIndex);

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
//保存订单
function SaveOrderInfo() {
    var PID = ID;
    var OrderUnit = $("#OrderUnit").val();
    var ContractID = $("#ContractID").val();
    //var 

    if (OrderUnit == "" || OrderUnit == null) {
        alert("订货单位不能为空");
        return false;
    }
    var Guarantee = $("#Guarantee").val();
    if (Guarantee == "" || Guarantee == null) {
        alert("保修日期不能为空");
        return false;
    }
    var ExpectedReturnDate = $("#ExpectedReturnDate").val
    if (ExpectedReturnDate == "" || ExpectedReturnDate == null) {
        alert("预计回款日期不能为空");
        return false;
    }
    var ProvidManager = $("#ProvidManager").val();
    if (ProvidManager == "" || ProvidManager == null) {
        alert("供应方不能为空");
        return false;
    }


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
    var YPrice = "";
    var TaxRate = "";
    var tbody = document.getElementById("DetailInfo");

    for (var i = 0; i < tbody.rows.length; i++) {
        //var Productid = tbody.getElementsByTagName("tr")[i].cells[1].innerText;//document.getElementById("ProductID" + i).innerHTML;
        //var mainContent = tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("ProName" + i).innerHTML;
        //var specsModels = document.getElementById("Spec" + i).value;
        //var unit = tbody.getElementsByTagName("tr")[i].cells[0].innerText;//document.getElementById("Units" + i).innerHTML;
        //var salesNum = document.getElementById("Amount" + i).value;
        //var supplier = document.getElementById("Supplier" + i).value;
        //var uitiprice = document.getElementById("UnitPrice" + i).value;
        //var subtotal = document.getElementById("Subtotal" + i).value;
        //var technology = document.getElementById("Technology" + i).value;
        //var deliverytime = document.getElementById("DeliveryTime" + i).value;
        //var yprice = tbody.getElementsByTagName("tr")[i].cells[12].innerText;//document.getElementById("YPrice" + i).innerHTML;
        //var taxrate = document.getElementById("TaxRate" + i).value;


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
        //var yprice = document.getElementById("YPrice" + i).value;//document.getElementById("YPrice" + i).innerHTML;
        //var taxrate = document.getElementById("TaxRate" + i).value;


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
        //YPrice += yprice;
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
            //YPrice += ",";
            TaxRate += ",";
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
            DeliveryTime += "";
            //YPrice += "";
            TaxRate += "";
        }
    }
    //, YPrice: YPrice
    var options = {
        url: "SaveOrderInfo",
        type: "Post",
        ansyc: false,
        data: {
            ID: ID, ContractID: ContractID, TaxRate: TaxRate,
            ProductID: ProductID, OrderContent: OrderContent, SpecsModels: SpecsModels, Unit: Unit, Amount: Amount, Supplier: Supplier, UnitPrice: UnitPrice, Subtotal: Subtotal, Technology: Technology, DeliveryTime: DeliveryTime
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("生成订单！");
                setTimeout('parent.ClosePop()', 10);
                window.parent.frames["iframeRight"].reload();

            }
            else {
                alert("生成订单失败-" + data.Msg);
            }
        }
    }
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}
function returnConfirm() {
    return isConfirm;
}
//从备案流转
function Jq() {
    var ID = ID;
    $.ajax({
        url: "GetProject",
        type: "post",
        data: { ID: ID },
        dataType: "json",
        ansyc: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#PID").val(json[i].PID);
                    $("#PlanID").val(json[i].PlanID);
                    $("#PlanName").val(json[i].PlanName);
                }
            }
        }
    });
}
//从合同流转

//小计
function XJ(rowrid) {
    // RowId = rowrid.id;

    var Total = 0;
    var s = "";
    if (RowId == 0) {
        s = "0";
    } else {
        s = RowId.substr(RowId.length - 1, RowId.length);
    }
    var tbody = document.getElementById("DetailInfo");
    //for (var i = 0; i < tbody.rows.length; i++) {
    var Amount = document.getElementById("Amount" + s).value;
    var UnitPrice = document.getElementById("UnitPrice" + s).value;
    var TaxRate = document.getElementById("TaxRate" + s).value + "";
    if (Amount == "" || Amount == null) {
        Amount = "0";
    }
    if (UnitPrice == "" || UnitPrice == null) {
        UnitPrice = "0.00";
    }
    TaxRate = parseFloat(TaxRate / 100);
    UnitPrice = parseFloat(UnitPrice) * parseFloat(1 + parseFloat(TaxRate));
    Total = parseFloat(Amount) * parseFloat(UnitPrice);
    Total = Total.toFixed(2);
    $("#Subtotal" + s).val(Total);
    HJ();
    // }
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


