
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    // 生成曲线图
    $("#CKQXT").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var mID = jQuery("#list").jqGrid('getRowData', rowid).MeterID;
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = rID + "@" + mID + "@" + jQuery("#list").jqGrid('getRowData', rowid).RepairMethod;
            window.parent.OpenDialog("查看对比曲线图", "../FlowDAManage/ShowImgs?Info=" + escape(texts), 700, 400, '');
        }
    });

    jq();// 加载检测仪表列表

})

//
function jq() {
    jQuery("#list").jqGrid({
        url: 'LoadDetecList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairMethod: $("#RepairMethod").val(), Caliber: $("#Caliber").val(),
            CustomerName: $("#CustomerName").val(), Model: $("#Model").val()
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
        colNames: ['', '表号', '表型号', '隶属单位', '', '检测方式', ''],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'MeterID', index: 'MeterID', width: 130 },
        { name: 'Model', index: 'Model', width: 150 },
        { name: 'SubUnit', index: 'SubUnit', width: 100 },
        { name: 'Method', index: 'Method', width: 100, hidden: true },
        { name: 'RepairMethod', index: 'RepairMethod', width: 100 },
        { name: 'RID', index: 'RID', width: 100, hidden: true }

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
                var curChk = "<input id='c" + id + "' type='checkbox' onclick='selChange(" + id + ")' value='" +
                    jQuery("#list").jqGrid('getRowData', id).MeterID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

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
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadDetecList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RepairMethod: $("#RepairMethod").val(), Caliber: $("#Caliber").val(),
            CustomerName: $("#CustomerName").val(), Model: $("#Model").val()
        },
    }).trigger("reloadGrid");
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

