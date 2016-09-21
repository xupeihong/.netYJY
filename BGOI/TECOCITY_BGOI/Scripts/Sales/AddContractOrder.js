$(document).ready(function () {
    //$("#Showlist").hidde();
    if (location.search != "") {
        CID = location.search.split('&')[0].split('=')[1];
        //  Type = location.search.split('&')[1].split('=')[1];
    }
    //   Jq();
    //LoadProjectDetail();
    $("#BXtxt").val("");
    // document.getElementById('div1').style.display = 'none';
    // document.getElementById('div2').style.display = 'none';
    $("#btnSaveOrder").click(function () {

        SaveOrderInfo();
    })
    $("#Total").click(function () {
        HJ();
    });
});
var CID;
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
                    // 0826 // html += '<td ><lable class="labUnits' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';

                    html += '<td ><input type="text" onblur=XJ() id="Amount' + rowCount + '" value="' + json[i].Amount + '" style="width:30px;"/></td>';
                    //html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';

                    //html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                    //html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    // html += '<td ><lable class="labSupplier' + rowCount + ' " id="Supplier' + rowCount + '">' + "供应商" + '</lable> </td>';
                    //  html += '<td ><input type="button" id="Supplier' + rowCount + '" onclick=CheckSupplier(this) value="' + "供应商" + '" style="width:60px;"/><input type="text" onclick=CheckSupplier(this) id="txtSupplier' + rowCount + '"> </td>';
                    html += '<td ><input type="text" onclick=CheckSupplier(this) id="Supplier' + rowCount + '"> </td>';
                    html += '<td ><input type="text"  id="UnitPrice' + rowCount + '"  onblur=XJ() > </td>';
                    html += '<td ><input type="text"  id="Subtotal' + rowCount + '" style="width:60px;"/></td>';
                    html += '<td ><input type="text" style="width:100px;" id="Technology' + rowCount + '"> </td>';
                    html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate" style="width:170px;" id="DeliveryTime' + rowCount + '"> </td>';
                    // html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
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
    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 350);
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
        ansyc: false,
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';

                    html += '<td ><input type="text"  onblur=XJ()  id="Amount' + rowCount + '"  style="width:30px;" /></td>';
                    //html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';

                    //html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                    //html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><input type="text" onclick=CheckSupplier(this) readonly="readonly"  id="Supplier' + rowCount + '"> </td>';
                    html += '<td ><input type="text" onblur=XJ()  id="UnitPrice' + rowCount + '"> </td>';
                    html += '<td ><input type="text"  readonly="readonly" id="Subtotal' + rowCount + '"  style="width:60px;"> </td>';
                    html += '<td ><input type="text" style="width:100px;" id="Technology' + rowCount + '"> </td>';
                    html += '<td ><input type="text" onclick="WdatePicker()" class="Wdate"  id="DeliveryTime' + rowCount + '"> </td>';
                    // html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                }


            }
        }
    })
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
                            $("#UnitPrice" + rownumber).val(json[0].price);
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
    //var PID = ID;
    var OrderUnit = $("#OrderUnit").val();
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
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Productid = document.getElementById("ProductID" + i).innerHTML;
        var mainContent = document.getElementById("ProName" + i).innerHTML;
        var specsModels = document.getElementById("Spec" + i).innerHTML;
        var unit = document.getElementById("Units" + i).innerHTML;
        var salesNum = document.getElementById("Amount" + i).value;
        var supplier = document.getElementById("Supplier" + i).value;
        var uitiprice = document.getElementById("UnitPrice" + i).value;
        var subtotal = document.getElementById("Subtotal" + i).value;
        var technology = document.getElementById("Technology" + i).value;
        var deliverytime = document.getElementById("DeliveryTime" + i).value;


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
        }
    }
    var options = {
        url: "SaveOrderInfo",
        type: "Post",
        ansyc: false,
        data: {
            ContractID:CID,
            ProductID: ProductID, OrderContent: OrderContent, SpecsModels: SpecsModels, Unit: Unit, Amount: Amount, Supplier: Supplier, UnitPrice: UnitPrice, Subtotal: Subtotal, Technology: Technology, DeliveryTime: DeliveryTime
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                alert("生成订单！");
                window.parent.ClosePop();
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
function XJ() {

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
    if (Amount == "" || Amount == null) {
        Amount = "0";
    }
    if (UnitPrice == "" || UnitPrice == null) {
        UnitPrice = "0.00";
    }
    Total = parseFloat(Amount) * parseFloat(UnitPrice);

    $("#Subtotal" + s).val(Total);
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


