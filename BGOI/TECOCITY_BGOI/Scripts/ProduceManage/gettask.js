﻿var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#searchOut").width($("#bor").width() - 30);
    jq("");

    $("#btnSave").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var RWID = Model.RWID;
            window.parent.taskll(RWID);
            window.parent.ClosePop();
        }
    })
})


function reload() {
    var OrderContent = $('#OrderContent').val();
    
    $("#list").jqGrid('setGridParam', {
        url: 'gettaskll',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderContent: OrderContent},
    }).trigger("reloadGrid");
}

function jq() {
    var OrderContent = $('#OrderContent').val();
    
    jQuery("#list").jqGrid({
        url: 'gettaskll',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderContent: OrderContent },
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
        colNames: ['序号', '名称', '规格型号'],
        colModel: [
         { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'ProName', index: 'ProName', width: 140, align: "center" },
        { name: 'Spec', index: 'Spec', width: 120, align: "center" },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        multiselect: true,
        caption: '',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function SearchOut() {
    curRow = 0;
    curPage = 1;

    var OrderContent = $('#OrderContent').val();

    $("#list").jqGrid('setGridParam', {
        url: 'gettaskll',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderContent: OrderContent},
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}



