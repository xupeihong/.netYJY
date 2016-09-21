
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    // 修改
    $("#XGLZK").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RID + "@" + "iframe";
            window.parent.OpenDialog("修改流转卡信息", "../FlowMeterManage/EditTransCard?Info=" + escape(texts), 700, 600, '');
        }
    });

    // 查看详细 
    $("#CKXX").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RID + "@" + "iframe";
            window.parent.OpenDialog("查看流转卡详细", "../FlowMeterManage/DetailTransCard?Info=" + escape(texts), 700, 600, '');
        }
    });

    LoadCardList();// 根据查询条件加载列表

});

//
function LoadCardList() {
    jQuery("#list").jqGrid({
        url: 'LoadTransCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: $("#RepairID").val(), CustomerName: $("#CustomerName").val(), MeterID: $("#MeterID").val()
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
        colNames: ['', '', '', '流转卡编号', '', '维修编号', '', '客户名称', '送修初检测', '送修','修后一次检测','一次返修','修后二次检测','二次送修','修后三次检测','三次返修','备注'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'RID', index: 'RID', width: 110, hidden: true },
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
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).TID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });

            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
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

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadTransCardList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairID: $("#RepairID").val(), CustomerName: $("#CustomerName").val(), MeterID: $("#MeterID").val()
        },
    }).trigger("reloadGrid");
}
