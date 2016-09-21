var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    tick();
    $("#ScrapTime").val("");
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#RecordDate").val("");
    $("#CreateTime").val("");
    $("#btnSave").click(function () {
        var TSID = $("#TSID").val();
        var UntiID = $("#UntiID").val();//
        //  alert("部门" + UntiID);
        var CustomerID = $("#CustomerID").val();//
        var Customer = $("#CustomerID").val();//客户名称
        // var Customer = $("#CustomerID option:selected").text();//客户名称
        // alert("客户名称" + Customer + CustomerID);
        var RecordDate = $("#RecordDate").val();
        var ComplaintDate = $("#ComplaintDate").val();
        var EmergencyDegree = $('#EmergencyDegree  option:selected').text();//紧急       ID都是.val
        // alert("紧急程度" + EmergencyDegree);
        var ComplaintTheme = $("#ComplaintTheme").val();
        var ComplaintCategory = $('#ComplaintCategory  option:selected').text();//分类
        // alert("分类" + ComplaintCategory);
        var FirstDealUser = $("#FirstDealUser").val();//首次处理人ID
        // var FirstDealUserq = $('#FirstDealUser  option:selected').text();// 首次处理人
        //  alert("首次处理人" + FirstDealUser + FirstDealUserq);
        var ComplainContent = $("#ComplainContent").val();
        var Remark = $("#Remark").val();
        var CreateTime = $("#localtime").text();
        var CreateUser = $("#CreateUser").val();
     
        var HandleProcess = $("#HandleProcess").val();
        var HandleState = $('#HandleState  option:selected').text();//处理结果
        // alert("处理结果" + HandleState);
        var HandleDate = $("#HandleDate").val();
        var CostDate = $("#CostDate").val();
        var CustomerFeedback = $("#CustomerFeedback").val();

       // var UserName = $("#UserName").val();
        var UserName = $('#UserName  option:selected').text();
        //  alert("投诉人员" + UserName);


        var Tel = $("#Tel").val();
        var Adderss = $("#Adderss").val();

        var HandleUser = $("#HandleUser").val();

        var Type = "2";//添加


        if (RecordDate == "" || RecordDate == null) {
            alert("投诉日期不能为空");
            return;
        }
        //产品信息
        var ID = "";
        var ProductID = "";
        var MainContent = "";
        var SpecsModels = "";
        var Unit = "";
        var Amount = "";
        var PContractDate = "";
        var OrderID = "";

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var Productid = tbody.getElementsByTagName("tr")[i].cells[7].innerText;//document.getElementById("ProductID" + i).innerHTML;
            var mainContent = tbody.getElementsByTagName("tr")[i].cells[1].innerText;//document.getElementById("ProName" + i).innerHTML;
            var specsModels = tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("Spec" + i).innerHTML;
            var unit = tbody.getElementsByTagName("tr")[i].cells[3].innerText;//document.getElementById("Units" + i).innerHTML;
            var salesNum = tbody.getElementsByTagName("tr")[i].cells[4].innerText;//document.getElementById("Amount" + i).innerHTML;
            var strOrderID = tbody.getElementsByTagName("tr")[i].cells[5].innerText;//document.getElementById("OrderID" + i).innerHTML;


            ID += parseInt(i + 1);
            ProductID += Productid;
            MainContent += mainContent;
            SpecsModels += specsModels;
            Unit += unit;
            Amount += salesNum;
            OrderID += strOrderID;
            if (i < tbody.rows.length - 1) {
                ID += ",";
                ProductID += ",";
                MainContent += ",";
                SpecsModels += ",";
                Unit += ",";
                Amount += ",";
                OrderID += ",";
            }
            else {
                ID += "";
                ProductID += "";
                MainContent += "";
                SpecsModels += "";
                Unit += "";
                Amount += "";
                OrderID += "";
            }
        }
        $.ajax({
            url: "SaveUserComplaints",
            type: "Post",
            data: {
                CreateTime: CreateTime, TSID: TSID, RecordDate: RecordDate, ComplaintTheme: ComplaintTheme, ComplaintDate: ComplaintDate, ComplainContent: ComplainContent,
                HandleProcess: HandleProcess, HandleState: HandleState, CustomerID: CustomerID, Customer: Customer, CreateUser: CreateUser, Remark: Remark,
                HandleDate: HandleDate, CostDate: CostDate, CustomerFeedback: CustomerFeedback,HandleUser:HandleUser,
                EmergencyDegree: EmergencyDegree, ComplaintCategory: ComplaintCategory,OrderID:OrderID,
                FirstDealUser: FirstDealUser, UntiID: UntiID, UserName: UserName, Type: Type,Tel:Tel,Adderss:Adderss,
                ID: ID, ProductID: ProductID, MainContent: MainContent, SpecsModels: SpecsModels, Unit: Unit, Amount: Amount
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

    var listtypes = new Array();
    listtypes[0] = "text";
    listtypes[1] = "text";
    listtypes[2] = "text";
    listtypes[3] = "text";
    listtypes[4] = "text";
    listtypes[5] = "text";
    listtypes[6] = "text";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";
    listNewElements[4] = "input";
    listNewElements[5] = "input";
    listNewElements[6] = "input";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "ID";
    listNewElementsID[1] = "WID";
    listNewElementsID[2] = "MainContent";
    listNewElementsID[3] = "SpecsModels";
    listNewElementsID[4] = "Unit";
    listNewElementsID[5] = "Amount";
    listNewElementsID[6] = "OrderTime";

    var listCheck = new Array();
    listCheck[0] = "n";
    listCheck[1] = "n";
    listCheck[2] = "n";
    listCheck[3] = "n";
    listCheck[4] = "n";
    listCheck[5] = "n";


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
   // alert(OrderContactor);
    ShowIframe1("选择订单信息", "../CustomerService/OrderList?OrderContactor=" + OrderContactor, 500, 400);
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
  
    $.ajax({
        url: "GetCustomerSatisfactionSurvey",
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
                   // html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable class="labAmount' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].OrderNum + '</lable> </td>';
                    html += '<td><lable class="labOrderID' + rowCount + ' " id="OrderID' + rowCount + '">' + json[i].OrderID + '</lable> </td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
                    html += '<td style="display:none;"><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                }
            }
        }
    })
}
function deleteTr1(date) {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var newCount = date.id;
        var strRow = newCount.charAt(newCount.length - 1);
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
        var typeNames = ["RowNumber", "ProName", "Spec", "Units", "Amount", "DetailInfo", "ProductID"];

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
    var strRow = newCount.charAt(newCount.length - 1);

    var strU = "#UnitPrice" + strRow;
    var strUnitPrice = $(strU).text();
   // var strUnitPrice = document.getElementById("DetailInfo").getElementsByTagName("tr")[0].cells[5].innerText;
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
function DepName() {
    var DeptId = $("#UntiID").val();
    $.ajax({
        url: "GetUserName",
        type: "post",
        data: { DeptId: DeptId },
        dataType: "json",
        success: function (data) {
            var items = "";
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    items += "<option value='" + json[i].UserId + "'>" + json[i].UserName + "</option>";
                }
            }
            $("#UserName").html(items);
        }
    })
}

function func() {
    var vs = $('#ComplaintCategory  option:selected').val();
    if (vs == "0") {
        $("#CPFW").show();
        $("#FWTS").hide();
    } else if (vs == "1") {
        $("#FWTS").show();
        $("#CPFW").hide();
    } else {
        $("#CPFW").hide();
        $("#FWTS").hide();
    }
}
//根据客户名称加载报装和报修
function BZBXSelect(){
    var Customer = $("#CustomerID").val();//客户名称
    $.ajax({
        url: "GetBX",
        type: "post",
        data: { Customer: Customer },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                $("#BX").show();
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfobx").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfobx' + rowCount + '" onclick="ChaKanBX(this)">';
                    html += '<td ><lable class="labBXID' + rowCount + ' " id="BXID' + rowCount + '">' + json[i].BXID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfobx").append(html);
                }
            }
        }
    })
    $.ajax({
        url: "GetBZ",
        type: "post",
        data: { Customer: Customer },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                $("#BZ").show();
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfobz").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfobz' + rowCount + '" onclick="ChaKanBZ(this)">';
                    html += '<td ><lable class="labBZID' + rowCount + ' " id="BZID' + rowCount + '">' + json[i].BZID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfobz").append(html);
                }
            }
        }
    })
}
//查看报修详细
function ChaKanBX(va) {
    var type = 1;//查看
    newRowID = va.id;
    var tbody = document.getElementById("DetailInfobx");
    var BXID = tbody.getElementsByTagName("tr")[newRowID].cells[0].innerText;
    var url = "PrintMaintenanceTaskList?Info='" + BXID + "'&type=" + type;
    window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
}
//查看报修详细
function ChaKanBZ(va) {
    var type = 1;//查看
    newRowID = va.id;
    var tbody = document.getElementById("DetailInfobz");
    var BZID = tbody.getElementsByTagName("tr")[newRowID].cells[0].innerText;
    var url = "PrintProductReport?Info='" + BZID + "'&type=" + type + "'&a=''";

    window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
}





