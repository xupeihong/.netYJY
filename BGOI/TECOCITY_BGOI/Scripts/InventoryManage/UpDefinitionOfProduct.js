var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    // tick();
    jq();
    jq1();
    $("#StockInTime").val("");
    $("#btnSave").click(function () {
        //var ProductID = $("#ProductID").val();
        var ProductID = $("#ProductID").find("option:selected").text();
        if (ProductID == "请选择") {
            alert("产品编号不能为空");
            return;
        }
        var type = 2;
        var MainContent = "";
        var PID = "";
        var StockInCount = "";
        var strBiaoShi = "";
        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = tbody.getElementsByTagName("tr")[i].cells[0].innerText;//document.getElementById("RowNumber" + i).innerHTML;
            var pID = tbody.getElementsByTagName("tr")[i].cells[1].innerText;//document.getElementById("PID" + i).innerHTML;
            var stockInCount = document.getElementById("Amount" + i).value;
            var strbiaoShi = document.getElementById("BiaoShi" + i).value;
            MainContent += mainContent;
            PID += pID;
            StockInCount += stockInCount;
            strBiaoShi += strbiaoShi;
            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                PID += ",";
                StockInCount += ",";
                strBiaoShi += ",";
            }
            else {
                MainContent += "";
                PID += "";
                StockInCount += "";
                strBiaoShi += "";
            }
        }
        $.ajax({
            url: "SaveAddDefinitionOfProduct",
            type: "Post",
            data: {
                ProductID: ProductID, type: type,strBiaoShi:strBiaoShi,
                MainContent: MainContent, PID: PID, StockInCount: StockInCount
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("修改成功");
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
function jq() {
    if (location.search != "") {
        PID = location.search.split('&')[0].split('=')[1];
    }
    $("#ProductID option:contains('" + PID + "')").prop('selected', true);
    $.ajax({
        url: "GetUpDefinitionOfProduct",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                $("#ProductID option:contains('" + json[i].PID + "')").prop('selected', true);
                $("#ProName option:contains('" + json[i].ProName + "')").prop('selected', true);
                $("#Specnew option:contains('" + json[i].Spec + "')").prop('selected', true);
            }
        }
    })
}
function jq1() {
    if (location.search != "") {
        PID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "GetUpXian",
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
                    html = '<tr id ="DetailInfo' + rowCount + '" onclick="selRow(this)">';
                    //  html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PartPID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;"  value= "' + json[i].Number + '"/></td>';
                    html += '<td ><input type="text" id="BiaoShi' + rowCount + '" style="width:30px;" value= "' + json[i].IdentitySharing + '" /></td>';
                    html += '</tr>';
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
}
function addBasicDetail(PID) { //增加货品信息行
   
    //判断重复数据
    var tbody = document.getElementById("DetailInfo");
    if (tbody.rows.length == 0) {
        var strPID = $("#PID").val();
        $("#PID").val(strPID + "," + PID);
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
                        //  html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;" /></td>';
                        html += '<td ><input type="text" id="BiaoShi' + rowCount + '" style="width:30px;" /></td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                }
            }
        })
    } else {
        var strPID = PID.replace("'", "").replace("'", "");
        for (var i = 0; i < tbody.rows.length; i++) {
            var pID = tbody.getElementsByTagName("tr")[i].cells[1].innerText;
            if (strPID.replace(/[ ]/g, "") == pID.replace(/[ ]/g, "")) {
                return;
            }

        }

        var strPID = $("#PID").val();
        $("#PID").val(strPID + "," + PID);
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
                        //  html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;" /></td>';
                        html += '<td ><input type="text" id="BiaoShi' + rowCount + '" style="width:30px;" /></td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                }
            }
        })
    }
}
function AddNewBasic() { //弹出选择货品信息页面
    ShowIframe1("选择货品信息", "../InventoryManage/ChangeBasic", 550, 500);
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
        var typeNames = ["RowNumber", "PID", "ProName", "Spec", "Units", "Amount", "BiaoShi","DetailInfo"];
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
        // GetAmount();
        $("#DetailInfo tr").removeAttr("class");
    }
}



function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    var strRow = newCount.charAt(newCount.length - 1);
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
    var strRow = newCount.charAt(newCount.length - 1);
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

//根据产品名称加载规格
function getSpecNew() {
    //var ProductID = $("#ProductID").val();
    var ProductID = $("#ProductID").find("option:selected").text();
    $.ajax({
        url: "GetPIDToSpec",
        type: "post",
        data: { ProductID: ProductID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                $("#Specnew option:contains('" + json[i].Spec + "')").prop('selected', true);
            }
        }
    })
}
//根据产品名称加载规格
function getPidSpec() {

    var ProName = $("#ProName").find("option:selected").text();
    var Specnew = document.getElementById('Specnew');
    Specnew.options.length = 0;
    var op = new Option("请选择", "");
    Specnew.options.add(op);
    $.ajax({
        url: "GetProNameToSpec",
        type: "post",
        data: { ProName: ProName },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    //  $("#Specnew").val(json[i].Spec);
                    var op = new Option(json[i].Spec, "");
                    Specnew.options.add(op);
                }
            }
        }
    })
    var ProductID = document.getElementById('ProductID');
    ProductID.options.length = 0;
    var op = new Option("请选择", "");
    ProductID.options.add(op);
    $.ajax({
        url: "GetProNameToPID",
        type: "post",
        data: { ProName: ProName },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    //  $("#Specnew").val(json[i].Spec);
                    var op = new Option(json[i].PID, "");
                    ProductID.options.add(op);
                }
            }
        }
    })
}