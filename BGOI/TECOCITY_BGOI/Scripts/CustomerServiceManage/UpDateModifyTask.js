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
        var BXID = $("#BXID").val();
        var RepairID = $("#RepairID").val();
        var CreateTime = $("#localtime").text();
       // var CustomerID = $("#Customer").text();
       // var Customer = $("#Customer option:selected").text();
        var Customer = $("#Customer").val();
        var ContactName = $("#ContactName").val();
        var Tel = $("#Tel").val();
        var Address = $("#Address").val();
        //var DeviceType = $('#DeviceType  option:selected').text();
        //var DeviceName = $('#DeviceType  option:selected').text();

        var DeviceType = $('#DeviceType').val();//产品型号
        var DeviceName = $('#DeviceName').val();//产品名称

        var DeviceID = $("#DeviceID").val();
        var BXKNum = $("#BXKNum").val();
        var GuaranteePeriod = $("input[name='GuaranteePeriod']:checked").val();
        var RepairDate = $("#RepairDate").val();
        var EnableDate = $("#EnableDate").val();
        var RepairTheUser = $("#RepairTheUser").val();
        var Remark = $("#Remark").val();
        var CreateUser = $("#CreateUser").val();
        var Type = "1";//修改
        $.ajax({
            url: "SaveMaintenanceTask",
            type: "Post",
            data: {
                BXID: BXID, RepairID: RepairID, CreateTime: CreateTime, Customer: Customer, ContactName: ContactName,
                Tel: Tel, Address: Address, DeviceType: DeviceType, DeviceID: DeviceID, BXKNum: BXKNum, DeviceName: DeviceName, GuaranteePeriod: GuaranteePeriod,
                RepairDate: RepairDate, RepairTheUser: RepairTheUser, Remark: Remark, CreateUser: CreateUser, Type: Type, EnableDate: EnableDate
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("修改成功！");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert("修改失败-" + data.msg);
                }
            }
        });
    });
});
function jq() {
    if (location.search != "") {
        BXID = location.search.split('&')[0].split('=')[1];
    }
   // alert(BXID);
    $.ajax({
        url: "UpDateModifyTaskComplaintsa",
        type: "post",
        data: { BXID: BXID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            for (var i = 0; i < json.length; i++) {
                $("#BXID").val(json[i].BXID);
                $("#RepairID").val(json[i].RepairID);
                $("#localtime").val(json[i].localtime);
             //   $("#Customer option:contains('" + json[i].Customer + "')").prop('selected', true);
                //$("#DeviceID option:contains('" + json[i].DeviceID + "')").prop('selected', true);
                //$("#DeviceType option:contains('" + json[i].DeviceType + "')").prop('selected', true);
                $("#Customer").val(json[i].Customer);
                $("#ContactName").val(json[i].ContactName);
                $("#DeviceID").val(json[i].DeviceID);
                $("#DeviceType").val(json[i].DeviceType);
                $("#Tel").val(json[i].Tel);
                $("#Address").val(json[i].Address);
                $("#DeviceName").val(json[i].DeviceID);
                //changenew(json[i].DeviceID);
               // $("#DeviceID").val(json[i].DeviceID);
                $("#EnableDate").val(json[i].EnableDate);
                $("#BXKNum").val(json[i].BXKNum);
                $("#RepairDate").val(json[i].RepairDate);
                $("#RepairTheUser").val(json[i].RepairTheUser);
                $("#Remark").val(json[i].Remark);
                $("#CreateUser").val(json[i].CreateUser);

                if (json[i].GuaranteePeriod == "0") {
                    $(':radio[name=GuaranteePeriod][value=0]').attr('checked', true);
                } else {
                    $(':radio[name=GuaranteePeriod][value=1]').attr('checked', true);
                }
            }
        }
    })
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
function GaiBian() {
    var DDL = $("#Customer").val();
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
function changenew(pid) {
    $("#DeviceID").val(pid);
    $.ajax({
        url: "GetPronewSpec",
        type: "post",
        data: { pid: pid },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#DeviceType").val(json[i].Spec);
                }
            }
        }
    })
}



