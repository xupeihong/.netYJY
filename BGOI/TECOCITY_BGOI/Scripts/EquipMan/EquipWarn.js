   var curPage = 1;
var OnePageCount = 15;
var Ename;
var StarTime;
var EndTime;
var OrderDate;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $('#SZTXSJ').click(function () {
        window.parent.OpenDialog("设置提醒时间", "../EquipMan/SetWarnTime", 450, 200, '');
    })
})

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'EquipWarnGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderDate: OrderDate },

    }).trigger("reloadGrid");
}

function jq() {
    Ename = $('#Ename').val();
    StarTime = $('#StarTime').val();
    EndTime = $('#EndTime').val();
    jQuery("#list").jqGrid({
        url: 'EquipWarnGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderDate: OrderDate },
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
        colNames: ['', "序号", '设备编号', '控制编号', '设备名称', '规格型号', '测量范围', '准确度等级/ 不确定度 ', '溯源方式', '上次检定/校准日期', '有效截止日期至', '检定/校准单位名称', '备注', '状态', 'State'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'xu', index: 'xu', width: 30 },
        { name: 'ECode', index: 'ECode', width: 100, hidden: true },
        { name: 'ControlCode', index: 'ControlCode', width: 70 },
        { name: 'Ename', index: 'Ename', width: 70 },
        { name: 'Specification', index: 'Specification', width: $("#bor").width() - 950 },
        { name: 'Clrange', index: 'Clrange', width: 100 },
        { name: 'Precision', index: 'Precision', width: 130 },
        { name: 'TracingTypeDesc', index: 'TracingTypeDesc', width: 70 },
        { name: 'LastDate', index: 'LastDate', width: 120 },
        { name: 'PlanDate', index: 'PlanDate', width: 120 },
        { name: 'CheckCompany', index: 'CheckCompany', width: 110 },
        { name: 'Remark', index: 'Remark', width: 100, hidden: true },
        { name: 'stateDesc', index: 'stateDesc', width: 50, hidden: true },
        { name: 'State', index: 'State', width: 50, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '设备表',
        sortable: true,
        optionloadonce: true,
        sortname: 'LastDate',
        //sortname: 'PlanDate',

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