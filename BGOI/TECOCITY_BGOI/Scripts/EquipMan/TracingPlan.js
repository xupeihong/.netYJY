
var curPage = 1;
var OnePageCount = 2000;
var XStarTime;
var XEndTime;
var JStarTime;
var JEndTime;
var OrderDate;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
})

function Towod() {
    var record = $("#list").getGridParam("reccount");
    if (record == 0) {
        alert("列表内容为空，没有要导出的数据，不能进行导出操作");
        return false;
    }
    else {
        var one = confirm("确定将列表内容导出吗？")
        if (one == false) {
            return false;
        }
        else {
            return true;
        }
    }
}

function reload() {
    XStarTime = $('#XStarTime').val();
    XEndTime = $('#XEndTime').val();
    JStarTime = $('#JStarTime').val();
    JEndTime = $('#JEndTime').val();
    $("#list").jqGrid('setGridParam', {
        url: 'TracingGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, xstarTime: XStarTime, xendTime: XEndTime, JStarTime: JStarTime, JEndTime: JEndTime, OrderDate: OrderDate },

    }).trigger("reloadGrid");
}

function jq() {
    XStarTime = $('#XStarTime').val();
    XEndTime = $('#XEndTime').val();
    JStarTime = $('#JStarTime').val();
    JEndTime = $('#JEndTime').val();
    jQuery("#list").jqGrid({
        url: 'TracingGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, xstarTime: XStarTime, xendTime: XEndTime, JStarTime: JStarTime, JEndTime: JEndTime, OrderDate: OrderDate },
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
        colNames: ['', "序号", '设备名称', '控制编号', '规格型号', '制造商', '校准服务机构', '校准周期', '上次校准时间', '计划校准时间', '结果', '负责人'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'xu', index: 'xu', width: 30 },
        { name: 'Ename', index: 'Ename', width: 70 },
        { name: 'ControlCode', index: 'ControlCode', width: 70 },
        { name: 'Specification', index: 'Specification', width: 100 },
        { name: 'Manufacturer', index: 'Manufacturer', width: $("#bor").width() - 980 },
        { name: 'CheckCompany', index: 'CheckCompany', width: 200 },
        { name: 'Cycle', index: 'Cycle', width: 50 },
        { name: 'LastDate', index: 'LastDate', width: 120 },
        { name: 'PlanDate', index: 'PlanDate', width: 120 },
        { name: 'CalibrationResults', index: 'CalibrationResults', width: 100 },
        { name: 'Principal', index: 'Principal', width: 50 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '设备溯源计划表',
        sortable: true,
        optionloadonce: true,
        sortname: 'LastDate',
        sortname: 'PlanDate',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).TaskID + "' name='cb'/>";
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        },
        onSortCol: function (index, iCol, sortorder) {
            OrderDate = index + "@" + sortorder;
            $("#Order").val(OrderDate);
            reload();
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