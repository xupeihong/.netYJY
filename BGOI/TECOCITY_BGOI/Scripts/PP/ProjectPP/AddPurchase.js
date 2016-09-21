$(document).ready(function () {
    $("#btnSave").click(function () {
        var PID = $("#PID").val();
        var RecordDate = $("#RecordDate").val();
        var PlanID = $("#PlanID").val();
        var PlanName = $("#PlanName").val();
        var Remark = $("#Remark").val();

        var MainContent = "";
        var DeviceName = "";
        var SpecsModels = "";
        var SalesNum = "";
        var WorkChief = "";
        var Contructor = "";
        var Tel = "";
        var BelongArea = "";
        var OrderTime = "";
        var ChannelsFrom = "";
        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = document.getElementById("MainContent" + i).value;
            var deviceName = document.getElementById("DeviceName" + i).value;
            var specsModels = document.getElementById("SpecsModels" + i).value;
            var salesNum = document.getElementById("SalesNum" + i).value;
            var workChief = document.getElementById("WorkChief" + i).value;
            var contructor = document.getElementById("Constructor" + i).value;
            var tel = document.getElementById("Tel" + i).value;
            var belongArea = document.getElementById("BelongArea" + i).value;
            var orderTime = document.getElementById("OrderTime" + i).value;
            var channelsFrom = document.getElementById("ChannelsFrom" + i).value;
            MainContent += mainContent;
            DeviceName += deviceName;
            SpecsModels += specsModels;
            SalesNum += salesNum;
            WorkChief += workChief;
            Contructor += contructor;
            Tel += tel;
            BelongArea += belongArea;
            OrderTime += orderTime;
            ChannelsFrom += channelsFrom;

            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                DeviceName += ",";
                SpecsModels += ",";
                SalesNum += ",";
                WorkChief += ",";
                Contructor += ",";
                Tel += ",";
                BelongArea += ",";
                OrderTime += ",";
                ChannelsFrom += ",";
            }
            else {
                MainContent += "";
                DeviceName += "";
                SpecsModels += "";
                SalesNum += "";
                WorkChief += "";
                Contructor += "";
                Tel += "";
                BelongArea += "";
                OrderTime += "";
                ChannelsFrom += "";
            }
        }

        $.ajax({
            url: "SaveRecordInfo",
            type: "Post",
            data: {
                PID: PID, RecordDate: RecordDate, PlanID: PlanID, PlanName: PlanName, Remark: Remark, MainContent: MainContent,
                DeviceName: DeviceName, SpecsModels: SpecsModels, SalesNum: SalesNum, WorkChief: WorkChief, Constructor: Contructor,
                Tel: Tel, BelongArea: BelongArea, OrderTime: OrderTime, ChannelsFrom: ChannelsFrom
            },
            async: false,
            success: function (data) {
                if (data.success == true) {

                }
                else {

                }
            }
        });
    });
});

function AddRow() {

    var listids = new Array();
    listids[0] = "0";
    listids[1] = "1";
    listids[2] = "2";
    listids[3] = "3";
    listids[4] = "4";
    listids[5] = "5";
    listids[6] = "6";
    listids[7] = "7";
    listids[8] = "8";
    listids[9] = "9";

    var listtypes = new Array();
    listtypes[0] = "text";
    listtypes[1] = "text";
    listtypes[2] = "text";
    listtypes[3] = "text";
    listtypes[4] = "text";
    listtypes[5] = "text";
    listtypes[6] = "text";
    listtypes[7] = "text";
    listtypes[8] = "text";
    listtypes[9] = "text";

    var listNewElements = new Array();
    listNewElements[0] = "input";
    listNewElements[1] = "input";
    listNewElements[2] = "input";
    listNewElements[3] = "input";
    listNewElements[4] = "input";
    listNewElements[5] = "input";
    listNewElements[6] = "input";
    listNewElements[7] = "input";
    listNewElements[8] = "input";
    listNewElements[9] = "input";

    var listNewElementsID = new Array();
    listNewElementsID[0] = "MainContent";
    listNewElementsID[1] = "DeviceName";
    listNewElementsID[2] = "SpecsModels";
    listNewElementsID[3] = "SalesNum";
    listNewElementsID[4] = "WorkChief";
    listNewElementsID[5] = "Constructor";
    listNewElementsID[6] = "Tel";
    listNewElementsID[7] = "BelongArea";
    listNewElementsID[8] = "OrderTime";
    listNewElementsID[9] = "ChannelsFrom";

    var listCheck = new Array();
    listCheck[0] = "n";
    listCheck[1] = "y";
    listCheck[2] = "n";
    listCheck[3] = "n";
    listCheck[4] = "n";
    listCheck[5] = "n";
    listCheck[6] = "n";
    listCheck[7] = "n";
    listCheck[8] = "n";
    listCheck[9] = "n";


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

function DelRow() {
    var tbody = document.getElementById("DetailInfo");
    var rowCount = tbody.rows.length;
    if (rowCount == 0) {
        alert("当前没有可删除的行！");
        return;
    }

    var listtypeNames = new Array();
    listtypeNames[0] = "GDDepth";
    listtypeNames[1] = "SDDepth";
    listtypeNames[2] = "BDDepth";
    listtypeNames[3] = "GDIDepth";
    var tableGX = new Table(null, null, null, null, null, null, null, null);
    tableGX.removeRow('myTable', 'GXInfo', listtypeNames);
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
