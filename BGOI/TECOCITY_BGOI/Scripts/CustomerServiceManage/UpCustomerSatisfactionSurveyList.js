var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    jq();
    tick();
    $("#ScrapTime").val("");
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#RecordDate").val("");
    $("#CreateTime").val("");
    $("#btnSave").click(function () {
        var CreateTime = $("#localtime").text();
        var DCID = $("#DCID").val();
        var CustomerID = $("#CustomerID").val();
        var Customer = $("#CustomerID option:selected").text();
        var Address = $("#Address").val();
        var Tel = $("#Tel").val();

        var ContactPerson = $("#ContactPerson").val();
        var Begin = $("#Begin").val();
        var ProductQuality = $("input[name='ProductQuality']:checked").val();
        var ProductQrice = $("input[name='ProductQrice']:checked").val();
        var ProductDelivery = $("input[name='ProductDelivery']:checked").val();

        var ProductSurvey = $("#ProductSurvey").val();
        var CustomerServiceSurvey = $("input[name='CustomerServiceSurvey']:checked").val();
        var SupplySurvey = $("input[name='SupplySurvey']:checked").val();
        var LeakSurvey = $("input[name='LeakSurvey']:checked").val();
        var ServiceSurvey = $("#ServiceSurvey").val();

        var AgencySales = $("input[name='AgencySales']:checked").val();
        var AgencyConsultation = $("input[name='AgencyConsultation']:checked").val();
        var AgencySpareParts = $("input[name='AgencySpareParts']:checked").val();
        var AgencySurvey = $("#AgencySurvey").val();
        var Remark = $("#Remark").val();

        var CreateUser = $("#CreateUser").val();
        var Type = "1";//修改
      
        //产品信息
        var ID = "";
        var ProductID = "";
        var MainContent = "";
        var SpecsModels = "";
        var Unit = "";
        var Amount = "";
        var PContractDate = "";
        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var Productid = tbody.getElementsByTagName("tr")[i].cells[7].innerText;//document.getElementById("ProductID" + i).innerHTML;
            var mainContent = tbody.getElementsByTagName("tr")[i].cells[1].innerText;//document.getElementById("ProName" + i).innerHTML;
            var specsModels = tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("Spec" + i).innerHTML;
            var unit = tbody.getElementsByTagName("tr")[i].cells[3].innerText;//document.getElementById("Units" + i).innerHTML;
            var salesNum = tbody.getElementsByTagName("tr")[i].cells[4].innerText;//document.getElementById("Amount" + i).innerHTML;
            var ContractDate = tbody.getElementsByTagName("tr")[i].cells[5].innerText;//document.getElementById("ContractDate" + i).innerHTML;//订购时间
            //alert(ContractDate);
            ID += parseInt(i + 1);
            ProductID += Productid;
            MainContent += mainContent;
            SpecsModels += specsModels;
            Unit += unit;
            Amount += salesNum;
            PContractDate += ContractDate;
            if (i < tbody.rows.length - 1) {
                ID += ",";
                ProductID += ",";
                MainContent += ",";
                SpecsModels += ",";
                Unit += ",";
                Amount += ",";
                PContractDate += ",";
            }
            else {
                ID += "";
                ProductID += "";
                MainContent += "";
                SpecsModels += "";
                Unit += "";
                Amount += "";
                PContractDate += "";
            }
        }
        $.ajax({
            url: "SaveCustomerSatisfactionSurvey",
            type: "Post",
            data: {
                CreateTime: CreateTime, DCID: DCID, CustomerID: CustomerID, Address: Address, Tel: Tel, Customer: Customer,
                ContactPerson: ContactPerson, Begin: Begin, ProductQuality: ProductQuality, ProductQrice: ProductQrice, ProductDelivery: ProductDelivery,
                ProductSurvey: ProductSurvey, CustomerServiceSurvey: CustomerServiceSurvey, SupplySurvey: SupplySurvey, LeakSurvey: LeakSurvey, ServiceSurvey: ServiceSurvey,
                AgencySales: AgencySales, AgencyConsultation: AgencyConsultation, AgencySpareParts: AgencySpareParts, AgencySurvey: AgencySurvey, Remark: Remark,
                CreateUser: CreateUser, Type: Type,
                ID: ID, ProductID: ProductID, MainContent: MainContent, SpecsModels: SpecsModels, Unit: Unit, Amount: Amount, PContractDate: PContractDate
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("操作成功！");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert("操作失败-" + data.msg);
                }
            }
        });
    });
});
function jq() {
    if (location.search != "") {
        DCID = location.search.split('&')[0].split('=')[1];
    }
    $("#Hidden1").val(DCID);
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "UpSurveyList",
        type: "post",
        data: { DCID: DCID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            for (var i = 0; i < json.length; i++) {
                var html = "";
                html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                html += '<td ><lable class="labAmount' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Num + '</lable> </td>';
                html += '<td ><lable class="labContractDate' + rowCount + ' " id="ContractDate' + rowCount + '">' + json[i].OrderDate + '</lable> </td>';
                html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
                html += '<td style="display:none;"><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                html += '</tr>'
                $("#DetailInfo").append(html);
                GaiBian(json[i].Customer);
               // $("#DCID").val(json[i].DCID);
                $("#CustomerID option:contains('" + json[i].Customer + "')").prop('selected', true);

                $("#Begin").val(json[i].SurveyDate);
                $("#ProductSurvey").val(json[i].ProductSurvey);
                $("#ServiceSurvey").val(json[i].ServiceSurvey);
                $("#AgencySurvey").val(json[i].AgencySurvey);

                var ProductQuality = json[i].ProductQuality;
                if (ProductQuality == "0") {
                    $(':radio[name=ProductQuality][value=0]').attr('checked', true);
                } else if (ProductQuality == "1") {
                    $(':radio[name=ProductQuality][value=1]').attr('checked', true);
                } else if (ProductQuality == "2") {
                    $(':radio[name=ProductQuality][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=ProductQuality][value=3]').attr('checked', true);
                }

                var ProductQrice = json[i].ProductQrice;
                if (ProductQrice == "0") {
                    $(':radio[name=ProductQrice][value=0]').attr('checked', true);
                } else if (ProductQrice == "1") {
                    $(':radio[name=ProductQrice][value=1]').attr('checked', true);
                } else if (ProductQrice == "2") {
                    $(':radio[name=ProductQrice][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=ProductQrice][value=3]').attr('checked', true);
                }

                var ProductDelivery = json[i].ProductDelivery;
                if (ProductDelivery == "0") {
                    $(':radio[name=ProductDelivery][value=0]').attr('checked', true);
                } else if (ProductDelivery == "1") {
                    $(':radio[name=ProductDelivery][value=1]').attr('checked', true);
                } else if (ProductDelivery == "2") {
                    $(':radio[name=ProductDelivery][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=ProductDelivery][value=3]').attr('checked', true);
                }

                var CustomerServiceSurvey = json[i].CustomerServiceSurvey;
                if (CustomerServiceSurvey == "0") {
                    $(':radio[name=CustomerServiceSurvey][value=0]').attr('checked', true);
                } else if (CustomerServiceSurvey == "1") {
                    $(':radio[name=CustomerServiceSurvey][value=1]').attr('checked', true);
                } else if (CustomerServiceSurvey == "2") {
                    $(':radio[name=CustomerServiceSurvey][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=CustomerServiceSurvey][value=3]').attr('checked', true);
                }

                var SupplySurvey = json[i].SupplySurvey;
                if (SupplySurvey == "0") {
                    $(':radio[name=SupplySurvey][value=0]').attr('checked', true);
                } else if (SupplySurvey == "1") {
                    $(':radio[name=SupplySurvey][value=1]').attr('checked', true);
                } else if (SupplySurvey == "2") {
                    $(':radio[name=SupplySurvey][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=SupplySurvey][value=3]').attr('checked', true);
                }

                var LeakSurvey = json[i].LeakSurvey;
                if (LeakSurvey == "0") {
                    $(':radio[name=LeakSurvey][value=0]').attr('checked', true);
                } else if (LeakSurvey == "1") {
                    $(':radio[name=LeakSurvey][value=1]').attr('checked', true);
                } else if (LeakSurvey == "2") {
                    $(':radio[name=LeakSurvey][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=LeakSurvey][value=3]').attr('checked', true);
                }

                var AgencySales = json[i].AgencySales;
                if (AgencySales == "0") {
                    $(':radio[name=AgencySales][value=0]').attr('checked', true);
                } else if (AgencySales == "1") {
                    $(':radio[name=AgencySales][value=1]').attr('checked', true);
                } else if (AgencySales == "2") {
                    $(':radio[name=AgencySales][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=AgencySales][value=3]').attr('checked', true);
                }

                var AgencyConsultation = json[i].AgencyConsultation;
                if (AgencyConsultation == "0") {
                    $(':radio[name=AgencyConsultation][value=0]').attr('checked', true);
                } else if (AgencyConsultation == "1") {
                    $(':radio[name=AgencyConsultation][value=1]').attr('checked', true);
                } else if (AgencyConsultation == "2") {
                    $(':radio[name=AgencyConsultation][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=AgencyConsultation][value=3]').attr('checked', true);
                }

                var AgencySpareParts = json[i].AgencySpareParts;
                if (AgencySpareParts == "0") {
                    $(':radio[name=AgencySpareParts][value=0]').attr('checked', true);
                } else if (AgencySpareParts == "1") {
                    $(':radio[name=AgencySpareParts][value=1]').attr('checked', true);
                } else if (AgencySpareParts == "2") {
                    $(':radio[name=AgencySpareParts][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=AgencySpareParts][value=3]').attr('checked', true);
                }

                $("#Remark").val(json[i].Remark);
                $("#CreateUser").val(json[i].CreateUser);
            }
        }
    })
}

function deleteTr(date) {
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
    var OrderContactor = $("#CustomerID").val();
    ShowIframe1("选择产品信息", "../CustomerService/OrderList?OrderContactor=" + OrderContactor, 500, 400);
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
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable class="labAmount' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].OrderNum + '</lable> </td>';
                    html += '<td ><lable class="labContractDate' + rowCount + ' " id="ContractDate' + rowCount + '">' + json[i].ContractDate + '</lable> </td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
                    html += '<td style="display:none;"><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
}
function deleteTr(date) {
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
function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    var strRow = newCount.charAt(newCount.length - 1);

    //var AmountC = $("#Count" + strRow).text();
    //if (parseInt(AmountC) < parseInt(Count)) {
    //    alert("所填的数量已超过此物品数量，请重新填写...");
    //    $("#" + newCount).val("0");
    //    $("#Total" + strRow).val("0");
    //    return;=
    //}

    var strU = "#UnitPrice" + strRow;
    var strUnitPrice = $(strU).text();
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
function GaiBian(DDL) {
    //var DDL = $("#CustomerID").val();
    //alert(DDL);
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
                    $("#ContactPerson").val(json[i].CName);
                }
            }
        }
    })
}
function GaiBianSen() {
    var DDL = $("#CustomerID").val();
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
                    $("#ContactPerson").val(json[i].CName);
                }
            }
        }
    })
}




