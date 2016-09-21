
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var OrderDate;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    // 新增 完成
    $("#XZDJK").click(function () {
        window.parent.OpenDialog("新增登记卡", "../FlowMeterManage/AddCardNew", 900, 650, '');
    });

    // 修改 完成
    $("#XGDJK").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var Types = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RID + "@" + "iframe";
            if (Types == "超声波")
                window.parent.OpenDialog("修改登记卡", "../FlowMeterManage/EditRepairCardUT?Info=" + escape(texts), 900, 600, '');
            else
                window.parent.OpenDialog("修改登记卡", "../FlowMeterManage/EditRepairCard?Info=" + escape(texts), 900, 600, '');
        }
    });

    // 下发任务 完成
    $("#XFRW").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (state != "已收货确认") {
                alert("请选择‘已收货确认’状态下的数据进行任务下发操作");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RID + "@" + jQuery("#list").jqGrid('getRowData', rowid).ModelType +"@" + "iframe";
            window.parent.OpenDialog("下发任务", "../FlowMeterManage/SendTaskCard?Info=" + escape(texts), 600, 400, '');
        }
    });

    // 查看详细 完成
    $("#CKXX").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var Types = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RID + "@" + "iframe";
            if (Types == "超声波")
                window.parent.OpenDialog("查看超声波登记详细", "../FlowMeterManage/DetailCardUT?Info=" + escape(texts), 700, 600, '');
            else
                window.parent.OpenDialog("查看详细", "../FlowMeterManage/DetailCard?Info=" + escape(texts), 700, 600, '');

        }
    });

    // 打印登记卡 完成 
    $("#DYDJK").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var Types = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RID + "@" + "iframe";
            var url = "../FlowMeterManage/PrintCard?Info=" + escape(texts);
            var url1 = "../FlowMeterManage/PrintCardUT?Info=" + escape(texts);
            if (Types == "超声波")
                window.showModalDialog(url1, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
            else
                window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    // 打印随工单 完成
    $("#DYSGD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var Types = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RID + "@" + "iframe";
            var url = "../FlowMeterManage/PrintWorkCard?Info=" + escape(texts);
            var url1 = "../FlowMeterManage/PrintWorkCardUT?Info=" + escape(texts);
            if (Types == "超声波")
                window.showModalDialog(url1, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
            else
                window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }

    });

    // 打印流转单 完成
    $("#DYLZD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var Types = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            if (Types != "超声波") {
                var texts = jQuery("#list").jqGrid('getRowData', rowid).RID + "@" + "iframe";
                var url = "../FlowMeterManage/PrintTransCard?Info=" + escape(texts);
                window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
            }
            else {
                alert("超声波类型仪表没有流转卡");
                return;
            }
        }
    });

    // 确认收货 完成
    $("#QRSH").click(function () {
        window.parent.OpenDialog("确认收货", "../FlowMeterManage/TakeDelivery", 700, 450, '');
    });

    // 上传附件 完成
    $("#SCFJ").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RID + "@" + "iframe";
            window.parent.OpenDialog("上传附件", "../FlowMeterManage/UpLoadManage?Info=" + escape(texts), 500, 400, '');
        }
    });


    LoadCardList();// 根据查询条件加载登记卡列表
    LoadWorkCardList();// 加载随工单列表
    LoadTransCardList();// 加载流转卡列表
    //
    LoadWorkCardListUT();// 加载超声波随工单列表
    LoadTransCardListUT();// 加载超声波随工单列表
    //
    LoadFileList();// 加载已上传的附件列表

    $('#SGDdiv').click(function () {
        this.className = "btnTw";
        $('#LZKdiv').attr("class", "btnTh");
        $('#FJdiv').attr("class", "btnTh");

        $("#FJList").css("display", "none");
        //
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var modelType = "";
        if (rowid != null) {
            modelType = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
        }
        if (modelType == "超声波") {
            $("#LZKList").css("display", "none");
            $("#LZKListUT").css("display", "none");
            $("#SGDListUT").css("display", "");
            $("#SGDList").css("display", "none");
            reloadUT1();
        }
        else {
            $("#LZKList").css("display", "none");
            $("#LZKListUT").css("display", "none");
            $("#SGDList").css("display", "");
            $("#SGDListUT").css("display", "none");
            reload1();
        }
    })

    $('#LZKdiv').click(function () {
        this.className = "btnTw";
        $('#SGDdiv').attr("class", "btnTh");
        $('#FJdiv').attr("class", "btnTh");

        $("#FJList").css("display", "none");
        //
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var modelType = "";
        if (rowid != null) {
            modelType = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
        }
        if (modelType == "超声波") {
            $("#SGDListUT").css("display", "none");
            $("#SGDList").css("display", "none");
            $("#LZKListUT").css("display", "");
            $("#LZKList").css("display", "none");
            reloadUT2();
        }
        else {
            $("#SGDListUT").css("display", "none");
            $("#SGDList").css("display", "none");
            $("#LZKList").css("display", "");
            $("#LZKListUT").css("display", "none");
            reload2();
        }
    })

    $('#FJdiv').click(function () {
        this.className = "btnTw";
        $('#SGDdiv').attr("class", "btnTh");
        $("#LZKdiv").attr("class", "btnTh");

        $("#SGDList").css("display", "none");
        $("#SGDListUT").css("display", "none");
        $("#LZKListUT").css("display", "none");
        $("#LZKList").css("display", "none");
        $("#FJList").css("display", "");
        reload3();
    })

});

function selChange(rowid) {
    if ($('input[id=c' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSelID != 0) {
            $('input[id=c' + oldSelID + ']').prop("checked", false);
        }
        $('input[id=c' + rowid + ']').prop("checked", true);
        $("#list").setSelection(rowid)
    }
}

// 登记卡
function LoadCardList() {
    jQuery("#list").jqGrid({
        url: 'LoadCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: $("#RepairID").val(), CustomerName: $("#CustomerName").val(), CustomerAddr: $("#CustomerAddr").val(),
            MeterID: $("#MeterID").val(), MeterName: $("#MeterName").val(), Model: $("#Model").val(),
            SS_Date: $("#SS_Date").val(), ES_Date: $("#ES_Date").val(), State: $("#State").val(),
            OrderDate: $("#Order").val(), CardType: $("#CardType").val()
        },
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
        colNames: ['', '登记卡号', '', '维修编号', '', '客户名称', '客户地址', '联系电话', '送表日期', '仪表编号', '隶属单位', '仪表型号', '仪表名称', '是否送检', '第三方检定单位', '状态', ''],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'RID', index: 'RID', width: 120 },
        { name: 'RIDShow', index: 'RIDShow', width: 90, hidden: true },
        { name: 'RepairID', index: 'RepairID', width: 110 },
        { name: 'RepairShow', index: 'RepairShow', width: 90, hidden: true },
        { name: 'CustomerName', index: 'CustomerName', width: 200 },
        { name: 'CustomerAddr', index: 'CustomerAddr', width: 200 },
        { name: 'S_Tel', index: 'S_Tel', width: 100 },
        { name: 'S_Date', index: 'S_Date', width: 120 },
        { name: 'MeterID', index: 'MeterID', width: 100 },
        { name: 'SubUnit', index: 'SubUnit', width: 100 },
        { name: 'Model', index: 'Model', width: 100 },
        { name: 'MeterName', index: 'MeterName', width: 120 },
        { name: 'IsOut', index: 'IsOut', width: 100 },
        { name: 'OutUnit', index: 'OutUnit', width: 100 },
        { name: 'State', index: 'State', width: 100 },
        { name: 'ModelType', index: 'ModelType', width: 100, hidden: true }

        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        sortable: true,
        optionloadonce: true,
        sortname: 'State',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).RID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                var Subunit = jQuery("#list").jqGrid('getRowData', id).SubUnit;
                if (Subunit == "" || Subunit == null)
                    $("#" + ids[i]).find("td").css("background-color", "#00c8ff");//设置背景颜色
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            select();
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        },
        onSortCol: function (index, iCol, sortorder) {
            OrderDate = index + "@" + sortorder;
            $("#Order").val(OrderDate);
            reload();
        }
    });
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: $("#RepairID").val(), CustomerName: $("#CustomerName").val(), CustomerAddr: $("#CustomerAddr").val(),
            MeterID: $("#MeterID").val(), MeterName: $("#MeterName").val(), Model: $("#Model").val(),
            SS_Date: $("#SS_Date").val(), ES_Date: $("#ES_Date").val(), State: $("#State").val(),
            OrderDate: $("#Order").val(), CardType: $("#CardType").val()
        },

    }).trigger("reloadGrid");
}

// 随工单
function LoadWorkCardList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
    }

    jQuery("#list1").jqGrid({
        url: 'LoadWorkCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: rID,
            SRepairDate: "", ERepairDate: "",
            RepairUser: ""
        },
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
        colNames: ['登记卡号', '', '随工单编号', '', '维修编号', '', '维修员', '检测员', '维修日期', '检测合格日期'],
        colModel: [
        { name: 'RID', index: 'RID', width: 120 },
        { name: 'RIDShow', index: 'RIDShow', width: 90, hidden: true },
        { name: 'WID', index: 'WID', width: 120 },
        { name: 'WIDShow', index: 'WIDShow', width: 90, hidden: true },
        { name: 'RepairID', index: 'RepairID', width: 110 },
        { name: 'RepairShow', index: 'RepairShow', width: 90, hidden: true },
        { name: 'RepairUser', index: 'RepairUser', width: 100 },
        { name: 'CheckUser', index: 'CheckUser', width: 100 },
        { name: 'RepairDate', index: 'RepairDate', width: 150 },
        { name: 'O_Date', index: 'O_Date', width: 150 }

        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage == $("#list1").getGridParam("lastpage"))
                    return;
                curPage = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage == 1)
                    return;
                curPage = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage = 1;
            }
            else {
                curPage = $("#pager1 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 162, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload1() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
    }
    $("#list1").jqGrid('setGridParam', {
        url: 'LoadWorkCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: rID,
            SRepairDate: "", ERepairDate: "",
            RepairUser: ""
        },

    }).trigger("reloadGrid");
}

// 超声波随工单
function LoadWorkCardListUT() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    var modelType = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
        modelType = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
    }

    jQuery("#list1UT").jqGrid({
        url: 'LoadWorkCardListUT',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: rID, SRepairDate: "", ERepairDate: "", ModelType: modelType,
            RepairUser: ""
        },
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
        colNames: ['登记卡号', '维修编号', '随工单编号', '', '仪表编号', '仪表型号', '维修人员', '验收人员', '验收日期'],
        colModel: [
        { name: 'RID', index: 'RID', width: 120, hidden: true },
        { name: 'RepairID', index: 'RepairID', width: 120 },
        { name: 'WID', index: 'WID', width: 120 },
        { name: 'WIDShow', index: 'WIDShow', width: 90, hidden: true },
        { name: 'MeterID', index: 'MeterID', width: 110 },
        { name: 'Model', index: 'Model', width: 120 },
        { name: 'RepairUser', index: 'RepairUser', width: 100 },
        { name: 'AcceptUser', index: 'AcceptUser', width: 100 },
        { name: 'AcceptDate', index: 'AcceptDate', width: 150 }

        ],
        pager: jQuery('#pager1UT'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1UT") {
                if (curPage == $("#list1UT").getGridParam("lastpage"))
                    return;
                curPage = $("#list1UT").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1UT") {
                curPage = $("#list1UT").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1UT") {
                if (curPage == 1)
                    return;
                curPage = $("#list1UT").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1UT") {
                curPage = 1;
            }
            else {
                curPage = $("#pager1UT :input").val();
            }
            reloadUT1();
        },
        loadComplete: function () {
            $("#list1UT").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 162, false);
            $("#list1UT").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reloadUT1() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    var modelType = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
        modelType = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
    }
    $("#list1UT").jqGrid('setGridParam', {
        url: 'LoadWorkCardListUT',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: rID, SRepairDate: "", ERepairDate: "", ModelType: modelType,
            RepairUser: ""
        },
        loadonce: false,
        mtype: 'POST',
        colNames: ['登记卡号', '维修编号', '随工单编号', '', '仪表编号', '仪表型号', '维修人员', '验收人员', '验收日期'],
        colModel: [
        { name: 'RID', index: 'RID', width: 120, hidden: true },
        { name: 'RepairID', index: 'RepairID', width: 120 },
        { name: 'WID', index: 'WID', width: 120 },
        { name: 'WIDShow', index: 'WIDShow', width: 90, hidden: true },
        { name: 'MeterID', index: 'MeterID', width: 110 },
        { name: 'Model', index: 'Model', width: 120 },
        { name: 'RepairUser', index: 'RepairUser', width: 100 },
        { name: 'AcceptUser', index: 'AcceptUser', width: 100 },
        { name: 'AcceptDate', index: 'AcceptDate', width: 150 }

        ],

    }).trigger("reloadGrid");
}

// 流转卡 
function LoadTransCardList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
    }

    jQuery("#list2").jqGrid({
        url: 'LoadTransCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: rID, CustomerName: "", MeterID: ""
        },
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
        colNames: ['登记卡号', '', '流转卡编号', '', '维修编号', '', '客户名称', '送修初检测', '送修', '修后一次检测', '一次返修', '修后二次检测', '二次送修', '修后三次检测', '三次返修', '备注'],
        colModel: [
        { name: 'RID', index: 'RID', width: 120 },
        { name: 'RIDShow', index: 'RIDShow', width: 90, hidden: true },
        { name: 'TID', index: 'TID', width: 150 },
        { name: 'TIDShow', index: 'TIDShow', width: 90, hidden: true },
        { name: 'RepairID', index: 'RepairID', width: 150 },
        { name: 'RepairShow', index: 'RepairShow', width: 90, hidden: true },
        { name: 'CustomerName', index: 'CustomerName', width: 150 },
        { name: 'FirstCheck', index: 'FirstCheck', width: 100 },
        { name: 'SendRepair', index: 'SendRepair', width: 100 },
        { name: 'LastCheck', index: 'LastCheck', width: 100 },
        { name: 'OneRepair', index: 'OneRepair', width: 100 },
        { name: 'TwoCheck', index: 'TwoCheck', width: 100 },
        { name: 'TwoRepair', index: 'TwoRepair', width: 100 },
        { name: 'ThreeCheck', index: 'ThreeCheck', width: 100 },
        { name: 'ThreeRepair', index: 'ThreeRepair', width: 100 },
        { name: 'Comments', index: 'Comments', width: 200 }

        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage == $("#list2").getGridParam("lastpage"))
                    return;
                curPage = $("#list2").getGridParam("page2") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage == 1)
                    return;
                curPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 162, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload2() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
    }
    $("#list2").jqGrid('setGridParam', {
        url: 'LoadTransCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: rID, CustomerName: "", MeterID: ""
        },

    }).trigger("reloadGrid");
}

// 超声波流转卡 
function LoadTransCardListUT() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
    }

    jQuery("#list2UT").jqGrid({
        url: 'LoadTransCardListUT',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: rID, CustomerName: "", MeterID: ""
        },
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
        colNames: ['登记卡号', '', '流转卡编号', '', '维修编号', '', '客户名称', '送修初检测', '送修', '修后一次检测', '一次返修', '修后二次检测', '二次送修', '修后三次检测', '三次返修', '备注'],
        colModel: [
        { name: 'RID', index: 'RID', width: 120 },
        { name: 'RIDShow', index: 'RIDShow', width: 90, hidden: true },
        { name: 'TID', index: 'TID', width: 150 },
        { name: 'TIDShow', index: 'TIDShow', width: 90, hidden: true },
        { name: 'RepairID', index: 'RepairID', width: 150 },
        { name: 'RepairShow', index: 'RepairShow', width: 90, hidden: true },
        { name: 'CustomerName', index: 'CustomerName', width: 150 },
        { name: 'FirstCheck', index: 'FirstCheck', width: 100 },
        { name: 'SendRepair', index: 'SendRepair', width: 100 },
        { name: 'LastCheck', index: 'LastCheck', width: 100 },
        { name: 'OneRepair', index: 'OneRepair', width: 100 },
        { name: 'TwoCheck', index: 'TwoCheck', width: 100 },
        { name: 'TwoRepair', index: 'TwoRepair', width: 100 },
        { name: 'ThreeCheck', index: 'ThreeCheck', width: 100 },
        { name: 'ThreeRepair', index: 'ThreeRepair', width: 100 },
        { name: 'Comments', index: 'Comments', width: 200 }

        ],
        pager: jQuery('#pager2UT'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        onPaging: function (pgButton) {
            if (pgButton == "next_pager2UT") {
                if (curPage == $("#list2UT").getGridParam("lastpage"))
                    return;
                curPage = $("#list2UT").getGridParam("page2UT") + 1;
            }
            else if (pgButton == "last_pager2UT") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2UT") {
                if (curPage == 1)
                    return;
                curPage = $("#list2UT").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2UT") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2UT :input").val();
            }
            reload2UT();
        },
        loadComplete: function () {
            $("#list2UT").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 162, false);
            $("#list2UT").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reloadUT2() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
    }
    $("#list2UT").jqGrid('setGridParam', {
        url: 'LoadTransCardListUT',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: rID, CustomerName: "", MeterID: ""
        },
    }).trigger("reloadGrid");
}

// 附件 
function LoadFileList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }

    jQuery("#list3").jqGrid({
        url: 'LoadFileList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, RID: rID
        },
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
        colNames: ['系统编号', '附件名称', '', '备注', '附件上传阶段', '', '系统操作人', '下载'],
        colModel: [
        { name: 'ID', index: 'ID', width: 100, align: 'center' },
        { name: 'FileName', index: 'FileName', width: 200 },
        { name: 'FileInfo', index: 'FileInfo', width: 200, hidden: true },
        { name: 'Comments', index: 'RIDShow', width: 300, hidden: true },
        { name: 'TypeText', index: 'TypeText', width: 150 },
        { name: 'Type', index: 'Type', width: 90, hidden: true },
        { name: 'CreatePerson', index: 'CreatePerson', width: 150 },
        { name: 'DownLoad', index: 'Id', width: 80, align: 'center' }
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        gridComplete: function () {
            var ids = jQuery("#list3").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list3").jqGrid('getRowData', id);
                var Down = "<a style='color:blue; text-align:center;' onclick='DownloadFile(\"" + curRowData.ID + "\")'>下 载</a>";
                jQuery("#list3").jqGrid('setRowData', ids[i], { DownLoad: Down });
            }
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (curPage == $("#list3").getGridParam("lastpage"))
                    return;
                curPage = $("#list3").getGridParam("page3") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPage == 1)
                    return;
                curPage = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reload3();
        },
        loadComplete: function () {
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 162, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload3() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    }
    $("#list3").jqGrid('setGridParam', {
        url: 'LoadFileList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, RID: rID
        },

    }).trigger("reloadGrid");
}

function select() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var modelType = "";
    if (rowid != null) {
        modelType = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
    }

    if (modelType == "超声波") {
        $("#SGDListUT").css("display", "");
        $("#SGDList").css("display", "none");
        $("#LZKListUT").css("display", "");
        $("#LZKList").css("display", "none");
        //
        $('#SGDdiv').attr("class", "btnTw");
        $("#LZKdiv").attr("class", "btnTh");
        $('#FJdiv').attr("class", "btnTh");

        reloadUT1();
        reloadUT2();
    }
    else {
        $("#SGDList").css("display", "");
        $("#SGDListUT").css("display", "none");
        $("#LZKList").css("display", "");
        $("#LZKListUT").css("display", "none");
        //
        $('#SGDdiv').attr("class", "btnTw");
        $("#LZKdiv").attr("class", "btnTh");
        $('#FJdiv').attr("class", "btnTh");

        reload1();
        reload2();
    }
    $("#FJList").css("display", "");
    reload3();
}

function DownloadFile(id) {
    window.open("DownLoad?id=" + id);
}
