$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
    var s = ID.substr(0, 2);
    if (s == "SG") {
        $("#SG").show();
        LoadSGXG();
        LoadSGXGs();
        LoadSGXGDetail();
    }

});

var curPage = 1;
var curPage1 = 1;
var curPage2 = 1;
var OnePageCount = 15;
var DID = 0;
var oldSelID = 0;
function LoadSGXG() {
    jQuery("#list").jqGrid({
        url: 'LoadSGXG',
        datatype: 'json',
        postData: { SGID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '编号', '发单日期', '技术要求', '技术负责人', '状态'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'SGID', index: 'SGID', width: 160, align: "center" },
        { name: 'billing', index: 'billing', width: 150, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'OrderContent', index: 'OrderContent', width: 200, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 100, align: "center" },
        { name: 'State', index: 'State', width: 80, align: "center" }
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
function reload() {
    var OnePageCount1 = 4;
    $("#list").jqGrid('setGridParam', {
        url: 'LoadSGXG',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, SGID: ID },

    }).trigger("reloadGrid");
}

function LoadSGXGs() {
    jQuery("#list1").jqGrid({
        url: 'LoadSGXGs',
        datatype: 'json',
        postData: { SGID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '产品编码', '产品名称', '产品规格', '单位', '数量', '图纸号', '备注'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'DID', index: 'DID', width: 160, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 120, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 120, align: "center" },
        { name: 'OrderUnit', index: 'OrderUnit', width: 80, align: "center" },
        { name: 'OrderNum', index: 'OrderNum', width: 80, align: "center" },
        { name: 'photo', index: 'photo', width: 80, align: "center" },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
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
            ID = jQuery("#list1").jqGrid('getRowData', rowid).PID//0812k
            // reload1(DID);
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
        url: 'LoadSGXGs',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount, SGID: ID },

    }).trigger("reloadGrid");
}


function LoadSGXGDetail() {
    jQuery("#list2").jqGrid({
        url: 'LoadSGXGDetail',
        datatype: 'json',
        postData: { SGID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '工序', '班组', '预计完工日期', '责任人', '计划数量', '完成数量-合格', '完成数量-返修', '完成数量-变更', '完成数量-废品', '实际完工日期', '检验员'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'Process', index: 'Process', width: 100, align: "center" },
        { name: 'team', index: 'team', width: 120, align: "center" },
        { name: 'Estimatetime', index: 'Estimatetime', width: 120, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'person', index: 'person', width: 80, align: "center" },
        { name: 'plannumber', index: 'plannumber', width: 120, align: "center" },
        { name: 'Qualified', index: 'Qualified', width: 120, align: "center" },
        { name: 'number', index: 'number', width: 120, align: "center" },
        { name: 'numbers', index: 'numbers', width: 120, align: "center" },
        { name: 'Fnubers', index: 'Fnubers', width: 80, align: "center" },
        { name: 'finishtime', index: 'finishtime', width: 120, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'people', index: 'people', width: 120, align: "center" }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '产品详细',

        gridComplete: function () {
            var ids = jQuery("#list2").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list2").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list2").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list2").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            if (pgButton == "next_pager2") {
                if (curPage2 == $("#list2").getGridParam("lastpage2"))
                    return;
                curPage2 = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage2 = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage2 == 1)
                    return;
                curPage2 = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage2 = 1;
            }
            else {
                curPage2 = $("#pager2 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() +80, false);
            $("#list2").jqGrid("setGridWidth", $("#bor2").width() - 15, false);
        }
    });
}
function reload2()
{
    var OnePageCount1 = 4;
    $("#list2").jqGrid('setGridParam', {
        url: 'LoadSGXGDetail',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount, SGID: ID },

    }).trigger("reloadGrid");
}

