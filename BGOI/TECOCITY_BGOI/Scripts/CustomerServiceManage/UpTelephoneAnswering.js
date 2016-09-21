var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    jq();
    tick();
    $("#ScrapTime").val("");
    $("#RecordDate").val("");
    $("#CreateTime").val("");
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })

    $("#btnSave").click(function () {
        var DHID = $("#DHID").val();
        var SchoolWork = $("#SchoolWork").val();
        var AnswerDate = $("#AnswerDate").val();
        var UserName = $("#UserName").val();
        var Tel = $("#Tel").val();
        var Address = $("#Address").val();
        var ProcessingResults = $("#ProcessingResults").val();
        var Remark = $("#Remark").val();
        var CreateUser = $("#CreateUser").val();
        var CreateTime = $("#localtime").text();
        var type = 2;//修改
        if (SchoolWork == "" || SchoolWork == null) {
            alert("派工单号不能为空");
            return;
        }
        if (UserName == "" || UserName == null) {
            alert("联系人不能为空");
            return;
        }
        if (Tel == "" || Tel == null) {
            alert("电话不能为空");
            return;
        }
        $.ajax({
            url: "SaveTelephoneAnswering",
            type: "Post",
            data: {
                DHID: DHID, SchoolWork: SchoolWork, AnswerDate: AnswerDate, UserName: UserName, Tel: Tel, Address: Address, ProcessingResults: ProcessingResults,
                Remark: Remark, CreateUser: CreateUser, CreateTime: CreateTime, type: type
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("修改成功");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert(data.Msg);
                }
            }
        });
    });

})
function jq() {
    if (location.search != "") {
        DHID = location.search.split('&')[0].split('=')[1];
    }
    // $("#HFID").val(HFID);
    $("#Hidden1").val(DHID);
    $.ajax({
        url: "UpTelDate",
        type: "post",
        data: { DHID: DHID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            for (var i = 0; i < json.length; i++) {
                $("#AnswerDate").val(json[i].AnswerDate);
            }
        }
    })
}

function tick() {
    var today;
    today = new Date();
    document.getElementById("localtime").innerHTML = showLocale(today);
    window.setTimeout("tick()", 1000);
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










