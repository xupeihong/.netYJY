$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
    var s = ID.substr(0, 2);
    if (s == "BG") {
        $("#BG").show();
        LoadBGXG();
        LoadBGXGDetail();
    }

});

var curPage = 1;
var curPage1 = 1;
var OnePageCount = 15;
var DID = 0;
var oldSelID = 0;
function LoadBGXG() {
    jQuery("#list").jqGrid({
        url: 'LoadBGXG',
        datatype: 'json',
        postData: { BGID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '编号', '上传日期', '操作人', '备注'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'BGID', index: 'BGID', width: 160, align: "center" },
        { name: 'uploadtime', index: 'uploadtime', width: 150, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'CreatePerson', index: 'CreatePerson', width: 100, align: "center" },
        { name: 'Remarks', index: 'Remarks', width: 80, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            ID = jQuery("#list").jqGrid('getRowData', rowid).PID//0812k
            // reload1(DID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });

}
function reload()
{
    var OnePageCount1 = 4;
    $("#list").jqGrid('setGridParam', {
        url: 'LoadBGXG',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, BGID: ID },

    }).trigger("reloadGrid");
}

function LoadBGXGDetail() {
    jQuery("#list1").jqGrid({
        url: 'LoadBGXGDetail',
        datatype: 'json',
        postData: { BGID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '编号', '文档类型', '文档名称', '操作人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'DID', index: 'DID', width: 160, align: "center" },
        { name: 'Type', index: 'Type', width: 120, align: "center" },
        { name: 'FileName', index: 'FileName', width: 120, align: "center" },
        { name: 'CreatePerson', index: 'CreatePerson', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '产品详细',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage1"))
                    return;
                curPage1 = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 15, false);
        }
    });
}
function reload1()
{
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'LoadBGXGDetail',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount, BGID: ID },

    }).trigger("reloadGrid");
}

