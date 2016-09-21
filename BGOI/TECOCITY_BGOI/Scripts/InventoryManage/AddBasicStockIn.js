var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    // tick();
    // ticknew();
    //$("#StockInTime").val("");
    $("#btnSave").click(function () {
        var UnitID = $("#UnitIDnew").val();
        var HandwrittenAccount = $("#HandwrittenAccount").val();
        var SubjectID = $("#SubjectID").val();
        var ListInID = $("#ListInID").val();
        var BatchID = $("#BatchID").val();
        var StockInTime = $("#StockInTime").val();
        //var StockInTime = $("#StockInTime").text();

        //var UnitIDnew = $("#UnitIDnew").val();
        var UnitIDnew = $("#UnitIDo").val();
        if (UnitIDnew.indexOf(".49.") > 0) {
            var HouseID = $("#HouseIFZ").val();
        } else {
            var HouseID = $("#HouseID").val();
        }
        var ProPlace = $("#ProPlace").val();
        var LoginName = $("#StockInUser").val();
        var Amount = $("#AmountM").val();
        var DrawID = $("#DrawID").val();
        var Remarkzhu = $("#Remark").val();
        //if (SubjectID == "" || SubjectID == null) {
        //    alert("科目不能为空");
        //    return;
        //}
        //if (BatchID == "" || BatchID == null) {
        //    alert("入库批号不能为空");
        //    return;
        //}
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
        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            //var strUnitPrice = tbody.getElementsByTagName("tr")[0].cells[6].innerText;
            var mainContent = tbody.getElementsByTagName("tr")[i].cells[0].innerText;//document.getElementById("RowNumber" + i).innerHTML;
            var pID = tbody.getElementsByTagName("tr")[i].cells[1].innerText;//document.getElementById("PID" + i).innerHTML;
            var proName = tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("ProName" + i).innerHTML;
            var specsModels = tbody.getElementsByTagName("tr")[i].cells[3].innerText;//document.getElementById("Spec" + i).innerHTML;
            var unitName = tbody.getElementsByTagName("tr")[i].cells[4].innerText;//document.getElementById("Units" + i).innerHTML;
            var stockInCount = document.getElementById("Amount" + i).value;
            var unitPrice = document.getElementById("UnitPrice" + i).value;//tbody.getElementsByTagName("tr")[i].cells[6].innerText;//
            var totalAmount = document.getElementById("Total" + i).value;
            var manufacturer = tbody.getElementsByTagName("tr")[i].cells[8].innerText;//document.getElementById("Manufacturer" + i).innerHTML;
            var remark = tbody.getElementsByTagName("tr")[i].cells[9].innerText;//document.getElementById("Remark" + i).innerHTML;
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

            }
        }
        $.ajax({
            url: "SaveBasicStockIn",
            type: "Post",
            data: {
                ListInID: ListInID, SubjectID: SubjectID, BatchID: BatchID, StockInTime: StockInTime, HouseID: HouseID, ProPlace: ProPlace,
                LoginName: LoginName, PID: PID, ProName: ProName, SpecsModels: SpecsModels, MainContent: MainContent, DrawID: DrawID, Remarkzhu: Remarkzhu,
                UnitName: UnitName, StockInCount: StockInCount, UnitPrice: UnitPrice, TotalAmount: TotalAmount, Manufacturer: Manufacturer, Remark: Remark, Amount: Amount,
                HandwrittenAccount: HandwrittenAccount
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("添加成功");
                    //window.parent.reload();
                    //window.parent.ClosePop();
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
function addBasicDetail(PID) { //增加货品信息行
    //判断重复数据
    var tbody = document.getElementById("DetailInfo");
    if (tbody.rows.length == 0) {

        //var strPID = $("#PID").val();
        //$("#PID").val(strPID + "," + PID);
        $.ajax({
            url: "GetBasicDetail",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
                        html += '<td ><input type="text" id="UnitPrice' + rowCount + '" style="width:30px;" onblur="OnBlurUnitPrice(this);" value="' + json[i].Price2 + '"/></td>';
                        //html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                        html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                        html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                    GetAmount();
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
        //rowCount = document.getElementById("DetailInfo").rows.length;
        //var CountRows = parseInt(rowCount) + 1;
        //var strPID = $("#PID").val();
        //$("#PID").val(strPID + "," + PID);
        $.ajax({
            url: "GetBasicDetail",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
                        html += '<td ><input type="text" id="UnitPrice' + rowCount + '" style="width:30px;" onblur="OnBlurUnitPrice(this);" value="' + json[i].Price2 + '"/></td>';
                        //html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                        html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                        html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                    GetAmount();
                }
            }
        })
    }

}
function AddNewBasic() { //弹出选择货品信息页面
    ShowIframe1("选择货品信息", "../InventoryManage/ChangeBasic", 760, 400);
}
function GetAmount() {  //获取总数金额
    $("#AmountM").val("");
    var Amount = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var totalAmount = document.getElementById("Total" + i).value;
        if (totalAmount == "" || totalAmount == null) {
            totalAmount = "0";
        }
        Amount = Amount + parseFloat(totalAmount);

        $("#AmountM").val(Amount);
    }
}
function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function DelRow() { //删除货品信息行
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var tbodyID = "DetailInfo";
        var rowIndex = -1;
        var typeNames = ["RowNumber", "PID", "ProName", "Spec", "Units", "Amount", "UnitPrice", "Total", "Manufacturer", "Remark"];
        if (newRowID != "")
            rowIndex = newRowID.replace(tbodyID, '');
        if (rowIndex != -1) {
            document.getElementById(tbodyID).deleteRow(rowIndex);
            //var a = $("#" + tbodyID + " tr").length;
            if (rowIndex < $("#" + tbodyID + " tr").length) {
                for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
                    // var b = parseInt(i);
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
                        tr1.html(html);//.toLocaleLowerCase());//replace('TD','td'));
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
    var strRow = newCount.substring(6);

    //var strRow = newCount.charAt(newCount.length - 1);
    //var strU = "#UnitPrice" + strRow;
    //var strUnitPrice = $(strU).text();
    var strUnitPrice = document.getElementById("UnitPrice" + strRow).value;// document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[6].innerText;
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    $("#Total" + strRow).val(strTotal);
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
function LingJian(typrid) {
    $("#HouseIFZ").val(typrid);
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

function ticknew() {
    var today;
    today = new Date();
    $("#StockInTime").val(showLocale(today));
    window.setTimeout("ticknew()", 1000);
}