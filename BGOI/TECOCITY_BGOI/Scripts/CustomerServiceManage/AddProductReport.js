var PID;
var rowCount;
var newRowID;
$(document).ready(function () {
    tick();
    jq();
    $("#ScrapTime").val("");
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#RecordDate").val("");
    $("#CreateTime").val("");
    $("#btnSave").click(function () {
        var CreateTime = $("#localtime").text();
        var BZID = $("#BZID").val();

        //var UnitID = $("#CustomerName").val();//客户ID
        //var CustomerName = $("#CustomerName option:selected").text();//客户名称
        var CustomerName = $("#CustomerName").val();
        var UnitID = "";//确定在处理
        var Tel = $("#Tel").val();
        var Address = $("#Address").val();
        var InstallTime = $("#InstallTime").val();
        var WarehouseTwo = $("#WarehouseTwo").val();
        var IsWhether = $("input[name='IsWhether']:checked").val();
        if (IsWhether == '0') {
            var WarehouseOne = $("#WarehouseOne").val();
        }
        var Remark = $("#Remark").val();
        var CreateUser = $("#CreateUser").val();

        var BZCompany = $("#BZCompany").val();

        var DiPer = $("#DiPer").val();

        var Type = "2";//添加

        var TRID = $("#TRID").val();

        if (InstallTime == "" || InstallTime == null) {
            alert("报装日期不能为空");
            return;
        }

        //产品信息
        var ID = "";
        var ProductID = "";
        var MainContent = "";
        var SpecsModels = "";
        var Unit = "";
        var Amount = "";
        // var PRemark = "";
        var Total = "";
        var UnitPrice = "";
        var SalesChannel = "";
        var IsPendingCollection = "";


        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var Productid = document.getElementById("ProductID" + i).value;// tbody.getElementsByTagName("tr")[i].cells[7].innerText;//document.getElementById("ProductID" + i).innerHTML;
            var mainContent = document.getElementById("ProName" + i).value;// tbody.getElementsByTagName("tr")[i].cells[1].innerText;//document.getElementById("ProName" + i).innerHTML;
            var specsModels = document.getElementById("Spec" + i).value;// tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("Spec" + i).innerHTML;
            var unit = document.getElementById("Units" + i).value;//tbody.getElementsByTagName("tr")[i].cells[3].innerText;//document.getElementById("Units" + i).innerHTML;
            var salesNum = document.getElementById("Amount" + i).value;
            var unitprice = document.getElementById("UnitPrice" + i).value;//tbody.getElementsByTagName("tr")[i].cells[5].innerText;//document.getElementById("UnitPrice" + i).innerHTML;
            var total = document.getElementById("Total" + i).value;
            var salesChannel = document.getElementById("SalesChannel" + i).value;

            var isPendingCollection = "";
            var radio = document.getElementsByName("Fruit" + i);
            for (var j = 0; j < radio.length; j++) {
                if (radio[j].checked == true) {
                    isPendingCollection = radio[j].value;
                    break;
                }
            }
            // var remark = tbody.getElementsByTagName("tr")[i].cells[0].innerText;//document.getElementById("Remark" + i).innerHTML;
            ID += parseInt(i + 1);
            ProductID += Productid;
            MainContent += mainContent;
            SpecsModels += specsModels;
            Unit += unit;
            Amount += salesNum;
            //  PRemark += remark;
            Total += total;
            UnitPrice += unitprice;
            SalesChannel += salesChannel;
            IsPendingCollection += isPendingCollection;

            if (i < tbody.rows.length - 1) {
                ID += ",";
                ProductID += ",";
                MainContent += ",";
                SpecsModels += ",";
                Unit += ",";
                Amount += ",";
                // PRemark += ",";
                Total += ",";
                UnitPrice += ","
                SalesChannel += ",";
                IsPendingCollection += ",";
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
                SalesChannel += "";
                IsPendingCollection += "";
            }
        }
        $.ajax({
            url: "SaveProductReport",
            type: "Post",
            data: {
                BZID: BZID, CustomerName: CustomerName, Tel: Tel, Address: Address, CreateTime: CreateTime, Type: Type,
                InstallTime: InstallTime, WarehouseTwo: WarehouseTwo, WarehouseOne: WarehouseOne, Remark: Remark,
                CreateUser: CreateUser, ProductID: ProductID, MainContent: MainContent, SpecsModels: SpecsModels,
                SalesChannel: SalesChannel, IsPendingCollection: IsPendingCollection, BZCompany: BZCompany,
                Unit: Unit, ID: ID, Amount: Amount, Total: Total, UnitPrice: UnitPrice, IsWhether: IsWhether, UnitID: UnitID,
                TRID: TRID, DiPer: DiPer
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
  //  ShowIframe1("选择货品信息", "../CustomerService/ChangeProduct", 500, 400);
    ShowIframe1("选择货品信息", "../InventoryManage/ChangeBasic", 500, 500);
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
                  //  html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    html += '<td ><input type="text" id="UnitPrice' + rowCount + '" style="width:30px;" readonly="true" value="' + json[i].UnitPrice + '"/></td>';
                    html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                    //html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><input type="text" id="SalesChannel' + rowCount + '" style="width:60px;"/></td>';
                    html += '<td><label><input id="IsPendingCollection' + rowCount + '" name="Fruit' + rowCount + '" type="radio" value="0" />是 </label> <label><input id="IsPendingCollection' + rowCount + '" name="Fruit' + rowCount + '" type="radio" value="1" />否 </label></td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
                    //html += '<td style="display:none;"><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                }
            }
        }
    })
    //加载保修卡
    $.ajax({
        url: "GetPIDBasicDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                $("#bxk").show();
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfoi").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfoi' + rowCount + '" onclick="ChaKan(this)">'
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                   // html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labBXKID' + rowCount + ' " id="BXKID' + rowCount + '">' + json[i].BXKID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfoi").append(html);
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
    html += '<td ><input type="text" id="ProductID' + rowCount + '" style="width:50px;"/></td>';
    html += '<td ><input type="text" id="ProName' + rowCount + '" style="width:50px;"/></td>';
    html += '<td ><input type="text" id="Spec' + rowCount + '" style="width:50px;"/></td>';
    html += '<td ><input type="text" id="Units' + rowCount + '" style="width:30px;"/></td>';
    html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
    html += '<td ><input type="text" id="UnitPrice' + rowCount + '" style="width:30px;"onblur="OnPriceTotal(this);"/></td>';
    html += '<td ><input type="text" id="Total' + rowCount + '" style="width:50px;"/></td>';
    html += '<td ><input type="text" id="SalesChannel' + rowCount + '" style="width:50px;"/></td>';
    html += '<td><label><input id="IsPendingCollection' + rowCount + '" name="Fruit' + rowCount + '" type="radio" value="0" />是 </label> <label><input id="IsPendingCollection' + rowCount + '" name="Fruit' + rowCount + '" type="radio" value="1" />否 </label></td>';
    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
    html += '</tr>'
    $("#DetailInfo").append(html);


    var html1 = "";
    html1 = '<tr  id ="DetailInfoi' + rowCount + '" onclick="ChaKan(this)">'
    html1 += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
    html1 += '<td ><input type="text" id="OrderContent' + rowCount + '" style="width:60px;"/></td>';
    html1 += '<td ><input type="text" id="BXKID' + rowCount + '" style="width:60px;"/></td>';

    //html1 += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
    //html1 += '<td ><lable class="labBXKID' + rowCount + ' " id="BXKID' + rowCount + '">' + json[i].BXKID + '</lable> </td>';
    html1 += '</tr>'
    $("#DetailInfoi").append(html1);

}
//查看保修卡详细
function ChaKan(va) {
    var type = 1;//查看
    newRowID = va.id;
    var tbody = document.getElementById("DetailInfoi");
    var BXKID = tbody.getElementsByTagName("tr")[newRowID].cells[2].innerText;
    var url = "PrintWarrCard?Info='" + BXKID + "'&type=" + type;
    window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
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
    var typeNames = ["RowNumber", "ProName", "Spec", "Units", "Amount", "UnitPrice", "Total", "ProductID", "SalesChannel", "IsPendingCollection", "DetailInfo", ];

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
function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    //var strRow = newCount.charAt(newCount.length - 1);
    var strRow = newCount.substring(6);
    var strUnitPrice = document.getElementById("UnitPrice" + strRow).value;//  document.getElementById("DetailInfo").getElementsByTagName("tr")[0].cells[5].innerText;
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    if (isNaN(strTotal)) {
        $("#Total" + strRow).val("");
    }
    else {
        $("#Total" + strRow).val(strTotal);
    }
}
function OnPriceTotal(rowcount)//(改变单价调用)求金额合
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    //var strRow = newCount.charAt(newCount.length - 1);
    var strRow = newCount.substring(9);
    var strUnitPrice = document.getElementById("Amount" + strRow).value;// document.getElementById("DetailInfo").getElementsByTagName("tr")[0].cells[5].innerText;
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    if (isNaN(strTotal)) {
        $("#Total" + strRow).val("");
    }
    else {
        $("#Total" + strRow).val(strTotal);
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
function GaiBian() {
    var DDL = $("#CustomerName").val();
    $.ajax({
        url: "GetKClientBas",
        type: "post",
        data: { DDL: DDL },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#Address").val(json[i].ComAddress);
                    $("#Tel").val(json[i].Phone);
                }
            }
        }
    })
}
function jq() {
    if (location.search != "") {
        type = location.search.split('&')[0].split('=')[1];
        if (type != "") {
            UserName = location.search.split('&')[1].split('=')[1];
            Tel = location.search.split('&')[2].split('=')[1];
            Address = location.search.split('&')[3].split('=')[1];
        }
    }
    if (type != "") {
        $("#CustomerName").val(UserName);
        $("#Tel").val(Tel);
        $("#Address").val(Address);

        $("#bzpg").hide();
        $("#idbh").show();
        $("#pgd").show();
        $("#pgd1").show();
        $("#pgd2").show();
        $("#pgry").show();
        $("#bzdj").hide();
        $("#bzdj1").hide();
        $("#bzdj2").hide();
        $("#TRID").val(type);
    }
}
