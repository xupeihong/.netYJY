﻿var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    $("#btnSave").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要修改的行");
            return;
        }
        else {
            var SHID = Model.SHID;
            var RKID = Model.RKID;
            window.parent.addProDetail(SHID, RKID);
            window.parent.ClosePop();
        }
    })
})

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'ChangeTestList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
    }).trigger("reloadGrid");
}

function jq() {
    jQuery("#list").jqGrid({
        url: 'ChangeProcureList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '采购入库单号','收货单号', '入库日期', '仓库', '入库说明', '处理人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'RKID', index: 'RKID', width: 150, align: "center" },
        { name: 'SHID', index: 'SHID', width: 150, align: "center" },
        { name: 'Rkdate', index: 'Rkdate', width: 120, align: "center" },
        { name: 'HouseName', index: 'HouseName', width: 150, align: "center" },
        { name: 'RKInstructions', index: 'RKInstructions', width: 150, align: "center" },
        { name: 'Handler', index: 'Handler', width: 80, align: "center" }
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()+250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}



