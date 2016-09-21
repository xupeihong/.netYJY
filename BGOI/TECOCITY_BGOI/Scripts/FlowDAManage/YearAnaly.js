var curPage = 1;
var OnePageCount = 20;
var oldSelID = 0;


$(document).ready(function () {

    $("#pageContent").height($(window).height());
    LoadUserAnaly();
    var myDate = new Date();
    OnePageCount = myDate.getFullYear() - 1997
})



function LoadUserAnaly() {

    jQuery("#list").jqGrid({
        url: 'LoadYearAnaly',
        datatype: 'json',
        postData: {
            name: $("#CustomerName").val(), sdate: $("#SS_Date").val(), edate: $("#ES_Date").val()
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
        colNames: ['贸易', '总表数', '总问题表数', '总故障率', '大问题表数', '大问题故障率', '影响计量问题表数', '影响计量故障率'],
        colModel: [
        { name: '年份', index: '年份', width: 250 },
        { name: '总表数', index: '总表数', width: 100 },
        { name: '总问题表数', index: '总问题表数', width: 100 },
        { name: '总故障率', index: '总故障率', width: 100 },
        { name: '大问题表数', index: '大问题表数', width: 100 },
         { name: '大问题故障率', index: '大问题故障率', width: 100 },
        { name: '影响计量问题表数', index: '影响计量问题表数', width: 100 },
{ name: '影响计量故障率', index: '影响计量故障率', width: 100 }


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
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).RepairID + "' name='cb'/>";
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

function reload() {

    $("#list").jqGrid('setGridParam', {
        url: 'LoadYearAnaly',
        datatype: 'json',
        postData: {
            name: $("#CustomerName").val(), sdate: $("#SS_Date").val(), edate: $("#ES_Date").val()
        },

    }).trigger("reloadGrid");

}