
var DID;
var rowCount;
$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    tick();
    $("#StockInTime").val("");
    $("#btnSave").click(function () {
        var UnitID = $("#UnitIDnew").val();
        var HandwrittenAccount = $("#HandwrittenAccount").val();

        var ListInID = $("#ListInID").val();
        var SubjectID = $("#SubjectID").val();
        var BatchID = $("#BatchID").val();
        // var StockInTime = $("#StockInTime").val();
        var StockInTime = $("#StockInTime").text();
        var HouseID = $("#HouseID").val();
        var ProPlace = $("#ProPlace").val();
        var LoginName = $("#StockInUser").val();
        //var Amount = $("#AmountM").val();
        var DrawID = $("#DrawID").val();
        var Remark1 = $("#Remark").val();
        //if (SubjectID == "" || SubjectID == null) {
        //    alert("科目不能为空");
        //    return;
        //}
        if (BatchID == "" || BatchID == null) {
            alert("入库批号不能为空");
            return;
        }
        if (StockInTime == "" || StockInTime == null) {
            alert("入库时间不能为空");
            return;
        }
        if (HouseID == "" || HouseID == null) {
            alert("仓库不能为空");
            return;
        }
        if (LoginName == "" || LoginName == null) {
            alert("经手人不能为空");
            return;
        }

        var MainContent = "";
        var PID = "";
        var ProName = "";
        var SpecsModels = "";
        var UnitName = "";
        var StockInCount = "";
         var UnitPrice = "";
         var TotalAmount = "";
        var Manufacturer = "";
        var Remark = "";
        var DID = "";
        var OrderID = "";
        var RKID = "";

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = tbody.getElementsByTagName("tr")[i].cells[0].innerText;//document.getElementById("RowNumber" + i).innerHTML;
            var pID = tbody.getElementsByTagName("tr")[i].cells[1].innerText;//document.getElementById("PID" + i).innerHTML;
            var rKID = tbody.getElementsByTagName("tr")[i].cells[11].innerText;//document.getElementById("RKID" + i).innerHTML;
            var proName = tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("OrderContent" + i).innerHTML;
            var specsModels = tbody.getElementsByTagName("tr")[i].cells[3].innerText;//document.getElementById("Specifications" + i).innerHTML;
            var unitName = tbody.getElementsByTagName("tr")[i].cells[4].innerText;//document.getElementById("Unit" + i).innerHTML;
            var stockInCount = document.getElementById("Amount" + i).value;
            var unitPrice = document.getElementById("UnitPrice" + i).value;// tbody.getElementsByTagName("tr")[0].cells[0].innerText;//document.getElementById("UnitPrice" + i).innerHTML;
            var totalAmount = document.getElementById("Total" + i).value;
            var manufacturer = tbody.getElementsByTagName("tr")[i].cells[8].innerText;//document.getElementById("Supplier" + i).innerHTML;
            var remark = tbody.getElementsByTagName("tr")[i].cells[9].innerText;//document.getElementById("Remark" + i).innerHTML;
            var did = tbody.getElementsByTagName("tr")[i].cells[10].innerText;//document.getElementById("DID" + i).innerHTML;
            var orderId = tbody.getElementsByTagName("tr")[0].cells[11].innerText;//document.getElementById("OrderID" + i).innerHTML;

            MainContent += mainContent;
            PID += pID;
            ProName += proName;
            SpecsModels += specsModels;
            UnitName += unitName;
            StockInCount += stockInCount;
             UnitPrice += unitPrice;
            TotalAmount += totalAmount;
            Manufacturer += manufacturer;
            Remark += remark;
            DID += did;
            OrderID += orderId;
            RKID += rKID;

            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                PID += ",";
                ProName += ",";
                SpecsModels += ",";
                UnitName += ",";
                StockInCount += ",";
                UnitPrice += ",";
                TotalAmount += ",";
                Manufacturer += ",";
                Remark += ",";
                DID += ",";
                OrderID += ",";
                RKID += ",";

            }
            else {
                MainContent += "";
                PID += "";
                ProName += "";
                SpecsModels += "";
                UnitName += "";
                StockInCount += "";
                 UnitPrice += "";
                 TotalAmount += "";
                Manufacturer += "";
                Remark += "";
                DID += "";
                OrderID += "";
                RKID += "";

            }
        }

        $.ajax({
            url: "SaveProductionStockIn",
            type: "Post",
            data: {
                ListInID: ListInID, SubjectID: SubjectID, BatchID: BatchID, StockInTime: StockInTime, HouseID: HouseID, ProPlace: ProPlace, DID: DID,
                LoginName: LoginName, PID: PID, ProName: ProName, SpecsModels: SpecsModels, MainContent: MainContent, DrawID: DrawID,Remark1:Remark1,
                UnitName: UnitName, StockInCount: StockInCount, Manufacturer: Manufacturer, Remark: Remark, RKID: RKID, OrderID: OrderID,
                HandwrittenAccount: HandwrittenAccount, UnitPrice: UnitPrice, TotalAmount: TotalAmount
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("添加成功");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert(data.msg);
                }
            }
        });
    });

});

function addProDetail(DID) {
   
    $.ajax({
        url: "GetProductionDetail",
        type: "post",
        data: { did: DID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                   // html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Specifications' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + ' " id="Unit' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text" id="Amount' + rowCount + '" value="' + json[i].Amount + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
                    // html += '<td ><input type="text" id="UnitPrice' + rowCount + '"  style="width:30px;" onblur="OnBlurUnitPrice(this);" value="' + json[i].UnitPrice + '"/></td>';
                    //html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].Price2 + '</lable> </td>';
                    html += '<td ><input type="text" id="UnitPrice' + rowCount + '" style="width:30px;" onblur="OnBlurUnitPrice(this);" value="' + json[i].Price2 + '"/></td>';

                    html += '<td ><input type="text" id="Total' + rowCount + '" value="" style="width:60px;"/></td>';
                    html += '<td ><lable class="labSupplier' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td  style="display:none;"><lable class="labRKID' + rowCount + ' " id="RKID' + rowCount + '">' + json[i].RKID + '</lable></td>';
                    html += '<td ><lable class="labRKID' + rowCount + ' " id="RKID' + rowCount + '">' + json[i].HouseName + '</lable></td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);


                    $("#HouseID option:contains('" + json[i].HouseName + "')").prop('selected', true);
                }
                // GetAmount();
            }
        }
    })
    //GetAmount();
}

function AddNewTest() {
    ShowIframe1("选择生产组装货品", "../InventoryManage/ChangeProduction", 500, 350);
}

function GetAmount() {
    $("#AmountM").val("");
    var Amount = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var totalAmount = document.getElementById("Total" + i).value;
        if (totalAmount == "" || totalAmount == null) {
            totalAmount = "0";
        }
        Amount = Amount + parseInt(totalAmount);

        if (Amount == 0) {
            $("#AmountM").val("0");
        }
        else {
            $("#AmountM").val(Amount);
        }
    }
}

var newRowID;
function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");

}

function DelRow() {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var tbodyID = "DetailInfo";
        var rowIndex = -1;
        var typeNames = ["RowNumber", "PID", "OrderContent", "Specifications", "Unit", "Amount", "UnitPrice", "Total", "Supplier", "Remark", "DID", 'OrderID', 'Count'];

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

                            var oldid = typeNames[k] + (parseInt(i) + 1);
                            var newid = typeNames[k] + i;

                            var reg = new RegExp(oldid, "g");

                            html = html.replace(reg, newid);

                        }
                        tr1.html(html);
                    }
                    document.getElementById("RowNumber" + i).innerHTML = parseInt(i) + 1;
                    // $("#RowNumber" + i).html(parseInt(i) + 1);
                }
            }
            if (document.getElementById(tbodyID).rows.length > 0) {
                if (rowIndex == document.getElementById(tbodyID).rows.length)
                    selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
                else
                    selRow(document.getElementById(tbodyID + rowIndex), '');;
            }
        }
        GetAmount();
        $("#DetailInfo tr").removeAttr("class");
    }
}

function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    //var strRow = newCount.charAt(newCount.length - 1);
    var strRow = newCount.substring(6);//Amount
    var AmountC = $("#Count" + strRow).text();
    if (parseInt(AmountC) < parseInt(Count)) {
        alert("所填的数量已超过此物品数量，请重新填写...");
        $("#" + newCount).val("0");
        $("#Total" + strRow).val("0");
        return;
    }

    //var strU = "#UnitPrice" + strRow;
    //var strUnitPrice = $(strU).text();
    var strUnitPrice = document.getElementById("UnitPrice" + strRow).value;// 
    //var strUnitPrice =document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[6].innerText;// document.getElementById("UnitPrice" + i).value;
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);

    if (isNaN(strTotal)) {
        $("#Total" + strRow).val("");
    }
    else {
        $("#Total" + strRow).val(strTotal);
    }

    GetAmount();
}
function OnBlurUnitPrice(rowcount) {
    var newCount = rowcount.id;
    var UnitPrice = $("#" + newCount).val();
    var strRow = newCount.substring(9);
    // var strRow = newCount.charAt(newCount.length - 1);
    //var strU = "#UnitPrice" + strRow;
    //var strUnitPrice = $(strU).text();
    var strAmount = document.getElementById("Amount" + strRow).value;// document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[5].innerText;
    var strTotal = parseFloat(UnitPrice) * parseFloat(strAmount);
    $("#Total" + strRow).val(strTotal);
    GetAmount();
}
function OnBlurUnitPrice(rowcount) {
    var newCount = rowcount.id;
    var UnitPrice = $("#" + newCount).val();
    var strRow = newCount.substring(9);//UnitPrice
    // var strRow = newCount.charAt(newCount.length - 1);
    //var strU = "#UnitPrice" + strRow;
    //var strUnitPrice = $(strU).text();
    var strAmount = document.getElementById("Amount" + strRow).value;// document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[5].innerText;
    var strTotal = parseFloat(UnitPrice) * parseFloat(strAmount);
    $("#Total" + strRow).val(strTotal);
    GetAmount();
}

function showLocale(objD) {
    var str, colorhead, colorfoot;
    var yy = objD.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = objD.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = objD.getDate();
    if (dd < 10) dd = '0' + dd;
    var hh = objD.getHours();
    if (hh < 10) hh = '0' + hh;
    var mm = objD.getMinutes();
    if (mm < 10) mm = '0' + mm;
    var ss = objD.getSeconds();
    if (ss < 10) ss = '0' + ss;
    var ww = objD.getDay();
    if (ww == 0) colorhead = "<font color=\"#333333\">";
    if (ww > 0 && ww < 6) colorhead = "<font color=\"#333333\">";
    if (ww == 6) colorhead = "<font color=\"#333333\">";
    if (ww == 0) ww = "星期日";
    if (ww == 1) ww = "星期一";
    if (ww == 2) ww = "星期二";
    if (ww == 3) ww = "星期三";
    if (ww == 4) ww = "星期四";
    if (ww == 5) ww = "星期五";
    if (ww == 6) ww = "星期六";
    colorfoot = "</font>"
    str = colorhead + yy + "年" + MM + "月" + dd + "日" + " " + hh + ":" + mm + ":" + ss + " " + ww + colorfoot;
    return (str);
}
function tick() {
    var today;
    today = new Date();
    document.getElementById("StockInTime").innerHTML = showLocale(today);
    window.setTimeout("tick()", 1000);
}