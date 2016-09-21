
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var oldSelID1 = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    LoadCardListUT();
    LoadCardList();

    // 修改 完成 
    $("#XGSGD").click(function () {
        var Mtype = $("#Models").val();
        //
        if (Mtype == "CardType2") {// 超声波
            var rowid = $("#list1").jqGrid('getGridParam', 'selrow');
            var rID = jQuery("#list1").jqGrid('getRowData', rowid).RID;
            if (rowid == null) {
                alert("请在列表中选择一条数据");
                return;
            }
            else {
                var texts = jQuery("#list1").jqGrid('getRowData', rowid).RID + "@" + "iframe";
                window.parent.OpenDialog("修改超声波随工单信息", "../FlowMeterManage/EditWorkCardUT?Info=" + escape(texts), 900, 600, '');
            }
        }
        else {
            var rowid = $("#list").jqGrid('getGridParam', 'selrow');
            var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
            if (rowid == null) {
                alert("请在列表中选择一条数据");
                return;
            }
            else {
                var texts = jQuery("#list").jqGrid('getRowData', rowid).RID + "@" + "iframe";
                window.parent.OpenDialog("修改随工单信息", "../FlowMeterManage/EditWorkCard?Info=" + escape(texts), 900, 600, '');
            }
        }

    });

    // 查看详细 完成
    $("#CKXX").click(function () {
        var Mtype = $("#Models").val();
        //
        if (Mtype == "CardType2") {// 超声波
            var rowid = $("#list1").jqGrid('getGridParam', 'selrow');
            var rID = jQuery("#list1").jqGrid('getRowData', rowid).RID;
            if (rowid == null) {
                alert("请在列表中选择一条数据");
                return;
            }
            else {
                var texts = jQuery("#list1").jqGrid('getRowData', rowid).RID + "@" + "iframe";
                window.parent.OpenDialog("查看超声波随工单详细", "../FlowMeterManage/DetailWorkCardUT?Info=" + escape(texts), 800, 600, '');
            }
        }
        else {
            var rowid = $("#list").jqGrid('getGridParam', 'selrow');
            var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
            if (rowid == null) {
                alert("请在列表中选择一条数据");
                return;
            }
            else {
                var texts = jQuery("#list").jqGrid('getRowData', rowid).RID + "@" + "iframe";
                window.parent.OpenDialog("查看随工单详细", "../FlowMeterManage/DetailWorkCard?Info=" + escape(texts), 800, 600, '');
            }
        }

    });

    $("#CX").click(function () {
        var types = $("#CardType").val();
        if (types == "CardType2") {// 超声波
            $("#bor").hide();
            $("#bor1").show();
            reload1();
        }
        else {
            $("#bor1").hide();
            $("#bor").show();
            reload();// 根据查询条件加载列表
        }
    });

});
//
function LoadCardListUT() {
    jQuery("#list1").jqGrid({
        url: 'LoadWorkCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: $("#RepairID").val(),
            SRepairDate: $("#SRepairDate").val(), ERepairDate: $("#ERepairDate").val(),
            RepairUser: $("#RepairUser").val(), CardType: $("#CardType").val()
        }, loadonce: false,
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
        colNames: ['', '登记卡号', '随工单编号', '维修编号', '', '仪表编号', '仪表型号', '维修人员', '验收人员', '验收日期', '仪表类型'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'RID', index: 'RID', width: 120, hidden: true },
        { name: 'WID', index: 'WID', width: 120 },
        { name: 'RepairID', index: 'RepairID', width: 120 },
        { name: 'WIDShow', index: 'WIDShow', width: 90, hidden: true },
        { name: 'MeterID', index: 'MeterID', width: 110 },
        { name: 'Model', index: 'Model', width: 120 },
        { name: 'RepairUser', index: 'RepairUser', width: 100 },
        { name: 'AcceptUser', index: 'AcceptUser', width: 100 },
        { name: 'AcceptDate', index: 'AcceptDate', width: 150 },
        { name: 'ModelType', index: 'ModelType', width: 90, hidden: true }

        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange1(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).WID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });

            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID1 != 0) {
                $('input[id=c' + oldSelID1 + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID1 = rowid;
            var ModelTypes = jQuery("#list1").jqGrid('getRowData', rowid).ModelType;
            $("#Models").val(ModelTypes);
        },

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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function reload1() {

    $("#list1").jqGrid('setGridParam', {
        url: 'LoadWorkCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: $("#RepairID").val(),
            SRepairDate: $("#SRepairDate").val(), ERepairDate: $("#ERepairDate").val(),
            RepairUser: $("#RepairUser").val(), CardType: $("#CardType").val()
        },
        loadonce: false,
        mtype: 'POST',
        colNames: ['', '登记卡号', '随工单编号', '维修编号', '', '仪表编号', '仪表型号', '维修人员', '验收人员', '验收日期', ''],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'RID', index: 'RID', width: 120, hidden: true },
        { name: 'WID', index: 'WID', width: 120 },
        { name: 'RepairID', index: 'RepairID', width: 120 },
        { name: 'WIDShow', index: 'WIDShow', width: 90, hidden: true },
        { name: 'MeterID', index: 'MeterID', width: 110 },
        { name: 'Model', index: 'Model', width: 120 },
        { name: 'RepairUser', index: 'RepairUser', width: 100 },
        { name: 'AcceptUser', index: 'AcceptUser', width: 100 },
        { name: 'AcceptDate', index: 'AcceptDate', width: 150 },
        { name: 'ModelType', index: 'ModelType', width: 90, hidden: true }

        ],

    }).trigger("reloadGrid");
}

function selChange1(rowid) {
    if ($('input[id=c' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSelID1 != 0) {
            $('input[id=c' + oldSelID1 + ']').prop("checked", false);
        }
        $('input[id=c' + rowid + ']').prop("checked", true);
        $("#list1").setSelection(rowid)
    }
}

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

//
function LoadCardList() {
    jQuery("#list").jqGrid({
        url: 'LoadWorkCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: $("#RepairID").val(),
            SRepairDate: $("#SRepairDate").val(), ERepairDate: $("#ERepairDate").val(),
            RepairUser: $("#RepairUser").val(), CardType: $("#CardType").val()
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
        colNames: ['', '', '', '随工单编号', '', '维修编号', '', '维修员', '检测员', '维修日期', '检测合格日期',''],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'RID', index: 'RID', width: 110, hidden: true },
        { name: 'RIDShow', index: 'RIDShow', width: 90, hidden: true },
        { name: 'WID', index: 'WID', width: 120 },
        { name: 'WIDShow', index: 'WIDShow', width: 90, hidden: true },
        { name: 'RepairID', index: 'RepairID', width: 110 },
        { name: 'RepairShow', index: 'RepairShow', width: 90, hidden: true },
        { name: 'RepairUser', index: 'RepairUser', width: 100 },
        { name: 'CheckUser', index: 'CheckUser', width: 100 },
        { name: 'RepairDate', index: 'RepairDate', width: 150 },
        { name: 'O_Date', index: 'O_Date', width: 150 },
        { name: 'ModelType', index: 'ModelType', width: 90, hidden: true }

        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).WID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });

            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var ModelTypes = jQuery("#list").jqGrid('getRowData', rowid).ModelType;
            $("#Models").val(ModelTypes);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadWorkCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: $("#RepairID").val(),
            SRepairDate: $("#SRepairDate").val(), ERepairDate: $("#ERepairDate").val(),
            RepairUser: $("#RepairUser").val(), CardType: $("#CardType").val()
        },
        loadonce: false,
        colNames: ['', '', '', '随工单编号', '', '维修编号', '', '维修员', '检测员', '维修日期', '出厂日期', ''],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'RID', index: 'RID', width: 110, hidden: true },
        { name: 'RIDShow', index: 'RIDShow', width: 90, hidden: true },
        { name: 'WID', index: 'WID', width: 120 },
        { name: 'WIDShow', index: 'WIDShow', width: 90, hidden: true },
        { name: 'RepairID', index: 'RepairID', width: 110 },
        { name: 'RepairShow', index: 'RepairShow', width: 90, hidden: true },
        { name: 'RepairUser', index: 'RepairUser', width: 100 },
        { name: 'CheckUser', index: 'CheckUser', width: 100 },
        { name: 'RepairDate', index: 'RepairDate', width: 150 },
        { name: 'O_Date', index: 'O_Date', width: 150 },
        { name: 'ModelType', index: 'ModelType', width: 90, hidden: true }

        ],
    }).trigger("reloadGrid");
}
