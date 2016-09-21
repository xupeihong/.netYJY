var curPage = 1;
var OnePageCount = 15;
var Ecode;
var StarTime;
var EndTime;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $('#XG').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要修改的条目");
            return;
        }
        var num = jQuery("#list").jqGrid('getRowData', rowid).xu;
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ID;
        ShowIframe1("修改送检校准信息", "../EquipMan/UpDCheckInfo?id=" + texts + "@" + num, 500, 450, '')
    })
})

function reload() {
    Ecode = $("#Ecode").val();
    StarTime = $('#StarTime').val();
    EndTime = $('#EndTime').val();
    $("#list").jqGrid('setGridParam', {
        url: 'DCheckInfoHistoryGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ecode: Ecode, starTime: StarTime, endTime: EndTime },

    }).trigger("reloadGrid");
}

function jq() {
    Ecode = $("#Ecode").val();
    StarTime = $('#StarTime').val();
    EndTime = $('#EndTime').val();
    jQuery("#list").jqGrid({
        url: 'DCheckInfoHistoryGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ecode: Ecode, starTime: StarTime, endTime: EndTime },
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
        colNames: ['', "序号", '编号', '控制编号', '设备名称', '日期', '检定单位', '方式', '费用', '精度', '负责人', '结果'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'xu', index: 'xu', width: 30 },
        { name: 'ID', index: 'ID', width: 100, hidden: true },
        { name: 'ControlCode', index: 'ControlCode', width: 70 },
        { name: 'Ename', index: 'Ename', width: 70 },
        { name: 'CheckDate', index: 'CheckDate', width: 100 },
        { name: 'CheckCompany', index: 'CheckCompany', width: 100 },
        { name: 'CheckWay', index: 'CheckWay', width: 50 },
        { name: 'Charge', index: 'Charge', width: 50 },
        { name: 'Precision', index: 'Precision', width: 70 },
        { name: 'Principal', index: 'Principal', width: 100 },
        { name: 'CalibrationResults', index: 'CalibrationResults', width: $("#bor").width() - 750 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '设备表',

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