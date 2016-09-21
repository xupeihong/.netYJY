var curPage = 1;
var RcurPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var id = 0;
var ID = 0;
var DID = 0;
var LogID = 0;
var OID = 0;
var objData = '';
var Malls = "";
var ApplyReason = "";
var MakeType = "";
var Applyer = "";
var Customer = "";
var StartDate = "";
var EndDate = "";
$(document).ready(function () {
    var userRole = $("#UserRole").val();
    var ExJob = $("#ExJob").val();
    if (userRole.indexOf(",4,") != "-1" && ExJob == "") {
        $("#divOperate").css("display", "block");
    }
    else {
        $("#divOperate").css("display", "none");
    }

    $("#pageContent").height($(window).height());
    document.getElementById('div1').style.display = 'block';
    document.getElementById('div2').style.display = 'none';
    document.getElementById('RZDIV').style.display = 'none';
    LoadBasInfo();
    LoadSPInfo('');
    LoadLog();
    $("#btnRecord").click(function () {
        window.parent.OpenDialog("专柜申请", "../SalesRetail/ApplyShoppe", 900, 500, '');
    });

    //$('#btnDetail').click(function () {
    //    var rowId = $("#list").jqGrid("getGridParam", "selrow");
    //    if (rowId == null) {
    //        alert("请选择专柜申请记录...");
    //        return;
    //    }
    //    else {
    //        var SIID = jQuery("#list").jqGrid("getRowData", rowId).SIID;
    //        LoadDetail(SIID);
    //        //reload1(orderID);
    //        this.className = "btnTw";
    //        $("#div1").css("display", "");
    //    }
    //})

    $("#btnDetail").click(function () {
        this.className = "btnTw";
        $('#btnSPInfo').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'none';

    });

    $("#btnSPInfo").click(function () {
        this.className = "btnTw";
        $('#btnDetail').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'block';
        document.getElementById('RZDIV').style.display = 'none';
    });

    $("#RZXX").click(function () {
        this.className = "btnTw";
        $('#btnDetail').attr("class", "btnTh");
        $('#btnSPInfo').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'block';
    })
    $("#btnPrintSP").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要打印的申请记录...");
            return;
        }
        else {
            var PID = jQuery("#list").jqGrid("getRowData", rowId).SIID;
            window.showModalDialog("../SalesRetail/PrintSPInfo?PID=" + PID + "&TaskType=Shoppe", window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    $("#btnUpdate").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要修改的专柜申请记录...");
            return;
        }
        else {
            var SIID = jQuery("#list").jqGrid("getRowData", rowId).SIID;
            window.parent.OpenDialog("修改专柜申请记录", "../SalesRetail/UpdateShoppe?SIID=" + SIID, 900, 500, '');
        }
    });

    $("#btnSP").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要进行审批的专柜申请单");
            return false;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowId).SIID + "@" + "专柜审批" + "@" + "专柜制作";
            var SIID = jQuery("#list").jqGrid('getRowData', rowId).SIID;
            var State = jQuery("#list").jqGrid('getRowData', rowId).State;
            if (State == "审批中")
            {
                alert("审批中不能重复提交审批");
                return;
            }
            if (State == "审批完成")
            {
                alert("审批完成不能提交审批");
                return;
            }
            window.parent.OpenDialog("提交审批", "../SalesRetail/ApprovalCommon?PID=" + SIID + "&id=" + texts, 700, 500, '');
            //window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
        }
    });

    $("#btnSearch").click(function () {
        reload();
    });

    $("#btnDetail").click(function () {
        this.className = "btnTw";
        $('#btnSPInfo').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
    });

    $("#btnSPInfo").click(function () {
        this.className = "btnTw";
        $('#btnDetail').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'block';
    });

    $("#btnPrint").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要打印的申请记录...");
            return;
        }
        else {
            var SIID = jQuery("#list").jqGrid("getRowData", rowId).SIID;
            window.showModalDialog("../SalesRetail/PrintShoppe?SIID=" + SIID, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    $("#btnCancel").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        var State = $("#list").jqGrid("getRowData", rowId).State;
        if (State == "审批完成" || State=="审批中") {
            alert("审批完成或审批中不能撤销");
            return;
        }
        if (rowId == null) {
            alert("请选择要撤销的专柜申请记录...");
            return;
        }
        else {
            var SIID = jQuery("#list").jqGrid("getRowData", rowId).SIID;
            if (confirm("是否确定要撤销编号为" + SIID + "的申请记录?")) {
                $.ajax({
                    url: "DeleteShoppe",
                    type: "post",
                    data: { SIID: SIID },
                    dataType: "Json",
                    success: function (data) {
                        if (data.success == "true") {
                            alert(data.Msg);
                            reload();
                        }
                        else {
                            alert(data.Msg);
                            return;
                        }
                    }
                });
            }
        }
    });
});


function reload() {
    if ($('.field-validation-error').length == 0) {
        Malls = $("#Malls").val();
        ApplyReason = $("#ApplyReason").val();
        MakeType = $("#MakeType").val();
        Applyer = $("#Applyer").val();
        Customer = $("#Customer").val();
        StartDate = $("#StartDate").val();
        EndDate = $("#EndDate").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetShoppeGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, Malls: Malls, ApplyReason: ApplyReason, MakeType: MakeType, Applyer: Applyer, Customer: Customer, StartDate: StartDate, EndDate: EndDate },

        }).trigger("reloadGrid");
    }
}

function LoadBasInfo() {
    Malls = $("#Malls").val();
    ApplyReason = $("#ApplyReason").val();
    MakeType = $("#MakeType").val();
    Applyer = $("#Applyer").val();
    Customer = $("#Customer").val();
    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();
    jQuery("#list").jqGrid({
        url: 'GetShoppeGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, Malls: Malls, ApplyReason: ApplyReason, MakeType: MakeType, Applyer: Applyer, Customer: Customer, StartDate: StartDate, EndDate: EndDate },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['申请编号', '商场名称', '所属代理商', '申请事由', '制作类型', '使用年限', '费用预算', '申请人', '申请日期', '状态', '所属单位'],
        colModel: [
        { name: 'SIID', index: 'SIID', width: 140, align: 'center' },
        { name: 'Customer', index: 'Customer', width: 120, align: 'center' },
        { name: 'Malls', index: 'Malls', width: 130, align: 'center' },
        { name: 'ApplyReason', index: 'ApplyReason', width: 150, align: 'center' },
        { name: 'MakeType', index: 'MakeType', width: 100, align: 'center' },
        { name: 'UseYear', index: 'UseYear', width: 110, align: 'center' },
        { name: 'Budget', index: 'Budget', width: 90, align: 'center' },
        { name: 'Applyer', index: 'Applyer', width: 90, align: 'center' },
        { name: 'ApplyTime', index: 'ApplyTime', width: 120, align: 'center' },
        { name: 'State', index: 'State', width: 90, align: 'center' },
        { name: 'UnitName', index: 'UnitName', width: 120, align: 'center' }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var SIID = jQuery("#list").jqGrid("getRowData", rowid).SIID;
            LogID = SIID;
            LoadDetail(SIID);
            reload3(SIID);
            reload7();
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 146, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadDetail(SIID) {

    $.post("../SalesRetail/GetMallInfo?SIID=" + SIID, function (data) {
        $("#loadlist").html('');

        var objTask = JSON.parse(data);
        var jsonHTML = "";
        jsonHTML = "<tr><td style='width:16%;'>所属代理商：</td>"
                 + "<td colspan='2'>" + objTask[0]["Malls"] + "</td>"
                 + "<td>商场地址：</td>"
                 + "<td colspan='2'>" + objTask[0]["Address"] + "</td></tr>";
        jsonHTML += "<tr><td style='width:16%;'>商场名称：</td>"
                 + "<td style='width:18%;'>" + objTask[0]["Customer"] + "</td>"
                 + "<td style='width:16%;'>商场联系人：</td>"
                 + "<td style='width:17%;'>" + objTask[0]["MallType"] + "</td>"
                 + "<td style='width:16%;'>联系电话：</td><td style='width:17%;'>" + objTask[0]["Phone"] + "</td></tr>";
        jsonHTML += "<tr><td style='width:16%;'>竞品1：名称：</td>"
                 + "<td style='width:18%;'>" + objTask[0]["ProductsOneName"] + "</td>"
                 + "<td style='width:16%;'>专柜尺寸：</td>"
                 + "<td style='width:17%;'>" + objTask[0]["ShoppeSize"] + "</td>"
                 + "<td style='width:16%;'>出样数量：</td><td style='width:17%;'>" + objTask[0]["SampleOneNum"] + "</td></tr>";
        jsonHTML += "<tr><td style='width:16%;'>竞品2：名称：</td>"
              + "<td style='width:18%;'>" + objTask[0]["ProductsTwoName"] + "</td>"
              + "<td style='width:16%;'>专柜尺寸：</td>"
              + "<td style='width:17%;'>" + objTask[0]["ShoppeTwoSize"] + "</td>"
              + "<td style='width:16%;'>出样数量：</td><td style='width:17%;'>" + objTask[0]["SampleNum"] + "</td></tr>";
        jsonHTML += "<tr><td style='width:16%;'>我司专柜位置：</td>"
            + "<td style='width:18%;'>" + objTask[0]["ShoppePosition"] + "</td>"
            + "<td style='width:16%;'>进驻后预计月均销量（台）：</td>"
            + "<td style='width:17%;'>" + objTask[0]["MonthSalesNum"] + "</td>"
            + "<td style='width:16%;'>金额：</td><td style='width:17%;'>" + objTask[0]["SalesAmount"] + "</td></tr>";

        $("#loadlist").append(jsonHTML);
    });

}

function LoadSPInfo(PID) {
    var webkey = $('#webkey').val();
    var folderBack = $('#folderBack').val();
    jQuery("#list2").jqGrid({
        url: 'ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['', '职务', '姓名', '审批方式', '人数', '审批情况', '审批意见', '审批时间', '备注'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'Job', index: 'Job', width: 100 },
        { name: 'UserName', index: 'UserName', width: 100 },
        { name: 'AppTypeDesc', index: 'AppTypeDesc', width: 100 },
        { name: 'Num', index: 'Num', width: 100 },
        { name: 'stateDesc', index: 'stateDesc', width: 100 },
        { name: 'Opinion', index: 'Opinion', width: $("#bor").width() - 920 },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 150 },
        { name: 'Remark', index: 'Remark', width: 200 },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                //var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                //jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage == $("#list2").getGridParam("lastpage"))
                    return;
                curPage = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reload3(PID);
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

function reload3(PID) {
    var webkey = $('#webkey').val();
    var folderBack = $('#folderBack').val();
    $("#list2").jqGrid('setGridParam', {
        url: 'ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}

function LoadLog() {
    jQuery("#RZlist").jqGrid({
        url: 'GetLogGrid',
        datatype: 'json',
        postData: { curpage: RcurPage, rownum: OnePageCount, ID: LogID },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['', '日志名称', '日志类型', '操作人', '操作单位', '操作时间'],
        colModel: [
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'LogContent', index: 'LogContent', width: 90 },
        { name: 'ProductType', index: 'ProductType', width: 90 },
        { name: 'Actor', index: 'Actor', width: 90 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'LogTime', index: 'LogTime', width: 100 }
        ],
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#FJXXlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                //var curRowData = jQuery("#FJXXlist").jqGrid('getRowData', id);
                //var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#FJXXlist").jqGrid('getRowData', id).PID + "' name='cb'/>";
                //jQuery("#FJXXlist").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            //ID = jQuery("#list").jqGrid('getRowData', rowid).EID//0812k
            //OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
            //select(rowid);
            //$("#Billlist tbody").html("");
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager6") {
                if (RcurPage == $("#RZlist").getGridParam("lastpage"))
                    return;
                RcurPage = $("#RZlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager6") {
                RcurPage = $("#RZlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager6") {
                if (RcurPage == 1)
                    return;
                RcurPage = $("#RZlist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager6") {
                RcurPage = 1;
            }
            else {
                RcurPage = $("#pager6 :input").val();
            }
            reload7();
        },
        loadComplete: function () {
            $("#RZlist").jqGrid("setGridHeight", $("#pageContent").height() + 80, false);
            $("#RZlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload7() {
    $("#RZlist").jqGrid('setGridParam', {
        url: 'GetLogGrid',
        datatype: 'json',
        postData: {
            curpage: RcurPage, rownum: OnePageCount, ID: LogID
        },
        //loadonce: false

    }).trigger("reloadGrid");//重新载入
}
