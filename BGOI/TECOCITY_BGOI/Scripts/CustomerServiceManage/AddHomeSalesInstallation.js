var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    tick();
    jq();
    //if (location.search != "") {
    //    BAIDold = location.search.split('&')[0].split('=')[1];
    //}
    // $("#BAIDold").val(BAIDold);
    $("#ScrapTime").val("");
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#RecordDate").val("");
    $("#CreateTime").val("");
    $("#btnSave").click(function () {

        var CreateTime = $("#localtime").text();
        var AZID = $("#AZID").val();
        var BAIDold = $("#BZID").val();
        var InstallName = $("#InstallName").val();
        var InstallTime = $("#InstallTime").val();
        if (InstallTime == "") {
            alert("安装时间不能为空！");
            return;
        }
        var IsCharge = $("input[name='IsCharge']:checked").val();
        var IsInvoice = $("input[name='IsInvoice']:checked").val();
        var ReceiptType = $("input[name='ReceiptType']:checked").val();
        var SureSatisfied = $("input[name='SureSatisfied']:checked").val();
        var IsProContent = $("input[name='IsProContent']:checked").val();
        var IsTeaching = $("input[name='IsTeaching']:checked").val();
        var IsWearClothes = $("input[name='IsWearClothes']:checked").val();
        var IsGifts = $("input[name='IsGifts']:checked").val();
        var IsClean = $("input[name='IsClean']:checked").val();
        var IsUserSign = $("input[name='IsUserSign']:checked").val();
        var Remark = $("#Remark").val();
        var CreateUser = $("#CreateUser").val();
        var AZCompany = $("#AZCompany").val();

        //产品信息
        var ID = "";
        var ProductID = "";
        var MainContent = "";
        var SpecsModels = "";
        var Unit = "";
        var Amount = "";
        var PRemark = "";
        var Total = "";
        var UnitPrice = "";
        var OrderID = "";
        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = tbody.getElementsByTagName("tr")[i].cells[0].innerText;// document.getElementById("RowNumber" + i).innerHTML;
            var Productid = tbody.getElementsByTagName("tr")[i].cells[1].innerText;// document.getElementById("ProductID" + i).innerHTML;
            var mainContent = tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("OrderContent" + i).innerHTML;
            var specsModels = tbody.getElementsByTagName("tr")[i].cells[3].innerText;// document.getElementById("SpecsModels" + i).innerHTML;
            var unit = tbody.getElementsByTagName("tr")[i].cells[4].innerText;// document.getElementById("OrderUnit" + i).innerHTML;
            var salesNum = document.getElementById("OrderNum" + i).value;
            var unitprice = document.getElementById("Price" + i).value;// tbody.getElementsByTagName("tr")[i].cells[6].innerText;// document.getElementById("Price" + i).innerHTML;
            var total = document.getElementById("Subtotal" + i).value;
            var orderId = tbody.getElementsByTagName("tr")[i].cells[9].innerText;// document.getElementById("OrderID" + i).innerHTML;

            ID += parseInt(i + 1);
            ProductID += Productid;
            MainContent += mainContent;
            SpecsModels += specsModels;
            Unit += unit;
            Amount += salesNum;
            // PRemark += remark;
            Total += total;
            UnitPrice += unitprice;
            OrderID += orderId;
            if (i < tbody.rows.length - 1) {
                ID += ",";
                ProductID += ",";
                MainContent += ",";
                SpecsModels += ",";
                Unit += ",";
                Amount += ",";
                //  PRemark += ",";
                Total += ",";
                UnitPrice += ",";
                OrderID += ",";
            }
            else {
                ID += "";
                ProductID += "";
                MainContent += "";
                SpecsModels += "";
                Unit += "";
                Amount += "";
                // PRemark += "";
                Total += "";
                UnitPrice += "";
                OrderID += "";
            }
        }
        $.ajax({
            url: "SaveHomeSalesInstallation",
            type: "Post",
            data: {
                CreateTime: CreateTime, AZID: AZID, BAIDold: BAIDold, InstallName: InstallName, IsCharge: IsCharge, IsInvoice: IsInvoice,
                ReceiptType: ReceiptType, SureSatisfied: SureSatisfied, IsProContent: IsProContent, IsWearClothes: IsWearClothes, IsTeaching: IsTeaching,
                IsGifts: IsGifts, IsClean: IsClean, IsUserSign: IsUserSign, Remark: Remark, CreateUser: CreateUser, InstallTime: InstallTime,
                ID: ID, ProductID: ProductID, MainContent: MainContent, SpecsModels: SpecsModels, Unit: Unit, Amount: Amount,
                Total: Total, UnitPrice: UnitPrice, AZCompany: AZCompany, OrderID: OrderID
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("添加成功！");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert("添加失败-" + data.msg);
                }
            }
        });
    });
});
function jq() {
    var BZID = "";
    $.ajax({
        url: "UpProductReportList",
        type: "post",
        data: { BZID: BZID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            for (var i = 0; i < json.length; i++) {
                rowCount = document.getElementById("DetailInfo").rows.length;
                var CountRows = parseInt(rowCount) + 1;
                var html = "";
                html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                html += '<td ><input type="text"  style="width:110px;" id="ProductID' + rowCount + '"value=' + json[i].PID + '\ /></td>';
                //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                html += '<td ><input type="text"  style="width:110px;" id="ProName' + rowCount + '" value=' + json[i].OrderContent + '\ /></td>';
                html += '<td ><input type="text"  style="width:170px;" id="Spec' + rowCount + '" value=' + json[i].SpecsModels + '\ /></td>';
                html += '<td ><input type="text"  style="width:30px;" id="Units' + rowCount + '" value=' + json[i].Unit + '\ /></td>';
                html += '<td ><input type="text"  style="width:30px;" id="Amount' + rowCount + '" onblur="OnBlurAmount(this);" value=' + json[i].Num + ' \/></td>';
                html += '<td ><input type="text"  style="width:30px;" id="UnitPrice' + rowCount + '" value=' + json[i].UnitPrice + '\ /></td>';
                html += '<td ><input type="text"  style="width:30px;" id="Total' + rowCount + '" style="width:60px;" value=' + json[i].Price + '\ /></td>';
                html += '<td style="display:none;"><lable class="labOrderID' + rowCount + ' " id="OrderID' + rowCount + '">' + json[i].OrderID + '</lable> </td>';
                html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
                html += '</tr>'
                $("#DetailInfo").append(html);
            }
        }
    })
}
function addProDetail(DID) {
    $.ajax({
        url: "GetHomeSalesInstallation",
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
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><input type="text" id="OrderNum' + rowCount + '" value="' + json[i].OrderNum + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
                    html += '<td ><input type="text" id="Price' + rowCount + '" style="width:30px;" onblur="OnPriceTotal(this);" value="' + json[i].Price + '"/></td>';
                    html += '<td ><input type="text" id="Subtotal' + rowCount + '" value="' + json[i].Subtotal + '" style="width:60px;"/></td>';
                    html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labOrderID' + rowCount + ' " id="OrderID' + rowCount + '">' + json[i].OrderID + '</lable> </td>';
                    html += '<td  style="display:none;"><lable class="labCount' + rowCount + ' " id="Count' + rowCount + '">' + json[i].OrderNum + '</lable></td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
                //GetAmount();
                //GetCount();
            }
        }
    })
    //GetAmount();
}
function addKonghang() { //增加空行
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var html = "";
    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
    html += '<td ><input type="text" style="width:110px;" id="ProductID' + rowCount + '"></td>';
    //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
    html += '<td ><input type="text" style="width:110px;" id="ProName' + rowCount + '"></td>';
    html += '<td ><input type="text"  style="width:170px;" id="Spec' + rowCount + '"></td>';
    html += '<td ><input type="text" style="width:30px;" id="Units' + rowCount + '"></td>';
    html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;" onblur="OnBlurAmount(this);"></td>';
    html += '<td ><input type="text" id="UnitPrice' + rowCount + '" style="width:30px;" onblur="OnPriceTotal(this);"></td>';
    html += '<td ><input type="text" style="width:30px;" id="Total' + rowCount + '" style="width:60px;" ></td>';
    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
    html += '</tr>'
    $("#DetailInfo").append(html);

}
function deleteTr1(date) {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var newCount = date.id;
        var strRow = newCount.charAt(newCount.length - 1);
        // $("#DetailInfo" + strRow).parent().parent().remove();
        $("#DetailInfo" + strRow).closest('tr').remove();
    }
}
function deleteTr(date) {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var tbodyID = "DetailInfo";
        var newRowIDnew = date.id;
        var rowIndex = -1;
        var typeNames = ["RowNumber", "ProName", "Spec", "Units", "Amount", "UnitPrice", "Total", "DetailInfo", "ProductID"];

        if (newRowIDnew != "")
            rowIndex = newRowIDnew.replace(tbodyID, '');
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
        //GetAmount();
        $("#DetailInfo tr").removeAttr("class");
    }
}
function IsWhetherO() {
    var IsWhether = $("input[name='IsWhether']:checked").val();
    if (IsWhether == "1") {
        $('#WarehouseOne').hide();
        $('#OneHouse').hide();
    } else {
        $('#WarehouseOne').show();
        $('#OneHouse').show();
    }
}
function AddRow() {

    var listids = new Array();
    listids[0] = "0";
    listids[1] = "1";
    listids[2] = "2";
    listids[3] = "3";
    listids[4] = "4";
    listids[5] = "5";
    listids[6] = "6";
    //  listids[7] = "7";
    //listids[8] = "8";
    //listids[9] = "9";

    var listtypes = new Array();
    listtypes[0] = "text";
    listtypes[1] = "text";
    listtypes[2] = "text";
    listtypes[3] = "text";
    listtypes[4] = "text";
    listtypes[5] = "text";
    listtypes[6] = "text";
    //listtypes[7] = "text";
    //listtypes[8] = "text";
    //listtypes[9] = "text";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";
    listNewElements[4] = "input";
    listNewElements[5] = "input";
    listNewElements[6] = "input";
    //listNewElements[7] = "input";
    //listNewElements[8] = "input";
    ////listNewElements[9] = "input";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "ID";
    listNewElementsID[1] = "WID";
    listNewElementsID[2] = "MainContent";
    listNewElementsID[3] = "SpecsModels";
    listNewElementsID[4] = "Unit";
    listNewElementsID[5] = "Amount";
    listNewElementsID[6] = "OrderTime";
    //listNewElementsID[7] = "ChannelsFrom";
    //listNewElementsID[8] = "OrderTime";
    //listNewElementsID[9] = "ChannelsFrom";

    var listCheck = new Array();
    listCheck[0] = "n";
    listCheck[1] = "n";
    listCheck[2] = "n";
    listCheck[3] = "n";
    listCheck[4] = "n";
    listCheck[5] = "n";
    // listCheck[6] = "n";
    //listCheck[7] = "y";
    // listCheck[8] = "n";
    //listCheck[7] = "n";


    var listcontent = new Array();

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.addNewRow('myTable', listtypes, listNewElements, listcontent, 'DetailInfo', listNewElementsID, listCheck);
    var tbody = document.getElementById("DetailInfo");
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
    for (var i = 0; i < rowCount; i++) {
        document.getElementById("myTable").rows[i + 1].cells[0].style.display = "none";
    }
}
function CheckDetail() {
    ShowIframe1("选择家用销售", "../CustomerService/ChangeHomeSalesInstallation", 700, 350);
}
function DelRow() {
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
    if (rowCount == 0) {
        alert("当前没有可删除的行！");
        return;
    }

    var listtypeNames = new Array();
    listtypeNames[0] = "MainContent";
    listtypeNames[1] = "DeviceName";
    listtypeNames[2] = "SpecsModels";
    listtypeNames[3] = "SalesNum";
    listtypeNames[4] = "WorkChief";
    listtypeNames[5] = "Constructor";
    listtypeNames[6] = "Tel";
    listtypeNames[7] = "BelongArea";
    listtypeNames[8] = "OrderTime";
    listtypeNames[9] = "ChannelsFrom";

    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.removeRow('myTable', 'DetailInfo', listtypeNames);
}
function CheckSpecial(op) {
    var special = $("#txtSpecial").val();
    var arr = new Array();
    arr = special.split(',');
    var msg = $("#" + op + "").val();
    for (var i = 0; i < arr.length; i++) {
        if (msg.indexOf(arr[i]) > 0) {
            alert("有非法字符" + arr[i] + ",请重新输入");
            $("#" + op + "").val('');

            return false;
        }
    }
}
function InitPage() {
    //TaskID_DX = document.getElementById("TaskID_DX").value;

    var listids = new Array();
    listids[0] = "0";
    listids[1] = "1";
    listids[2] = "2";
    listids[3] = "3";

    var listtypes = new Array();
    listtypes[0] = "text";
    listtypes[1] = "text";
    listtypes[2] = "text";
    listtypes[3] = "text";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "GDDepth";
    listNewElementsID[1] = "SDDepth";
    listNewElementsID[2] = "BDDepth";
    listNewElementsID[3] = "GDIDepth";


    var listcontent = new Array();
    var TaskID = $("#TaskID").val();
    var AccidentPoint = $("#AccidentPoint").val();
    $.post("GetFSInfo?TaskID=" + TaskID + "&AccidentPoint=" + encodeURI(AccidentPoint) + "&tabName=FSInfo", function (data1) {
        var objGXInfo = JSON.parse(data1);
        var tableGX = new Table(objGXInfo, 'oddRow', 'evenRow', 'selRow', listids, listtypes, listNewElements, listcontent, listNewElementsID);
        tableGX.LoadTableTBody('GXInfo');
        var tbody = document.getElementById("GXInfo");
        var rowCount = tbody.rows.length;
        for (var i = 0; i < rowCount; i++) {
            document.getElementById("myTable").rows[i + 1].cells[0].style.display = "none";
        }
    });

    var tbody = document.getElementById("GXInfo");
    var rowCount = tbody.rows.length;
    for (var i = 0; i < rowCount; i++) {
        document.getElementById("myTable").rows[i + 1].cells[0].style.display = "none";
    }
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
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    //html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
                    html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                    // html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
                    html += '<td style="display:none;"><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    //html += '<td style="display:none;"><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                }
            }
        }
    })
}

function OnPriceTotal(rowcount)//(改变单价调用)求金额合
{

    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    //var strRow = newCount.charAt(newCount.length - 1);
    var strRow = newCount.substring(5);

    var strUnitPrice = document.getElementById("OrderNum" + strRow).value;// document.getElementById("DetailInfo").getElementsByTagName("tr")[0].cells[5].innerText;
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    if (isNaN(strTotal)) {
        $("#Subtotal" + strRow).val("");
    }
    else {
        $("#Subtotal" + strRow).val(strTotal);
    }
}

function OnBlurAmount(rowcount) //求金额和 (改变数量调用)
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    //var strRow = newCount.charAt(newCount.length - 1);
    var strRow = newCount.substring(8);

    var strUnitPrice = document.getElementById("Price" + strRow).value;// document.getElementById("DetailInfo").getElementsByTagName("tr")[0].cells[5].innerText;
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    if (isNaN(strTotal)) {
        $("#Subtotal" + strRow).val("");
    }
    else {
        $("#Subtotal" + strRow).val(strTotal);
    }
}

function selRow(curRow) {
    newRowID = curRow.id;
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
    document.getElementById("localtime").innerHTML = showLocale(today);
    window.setTimeout("tick()", 1000);
}




