$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
    LoadExchangeDetail();
    LoadReturnDetail();
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});

var RowId = 0;
var curPage = 1;
var OnePageCount = 5;
//退货详细
function LoadExchangeDetail() {
    jQuery("#myTable").jqGrid({
        url: 'LoadExchangeDetail',
        datatype: 'json',
        postData: { Eid: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['退货单号', '物料编码', '产品名称', '规格型号', '生产厂家', '退货数量', '单位', '退货单价含税', '退货总结含税', '退货单价不含税', '退货总价不含税', '备注', ],
        colModel: [
        { name: 'EID', index: 'EID', width: 200 },
        { name: 'ProductID', index: 'ProductID', width: 200 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'Amount', index: 'Amount', width: 100 },
        { name: 'Unit', index: 'Unit', width: 100 },
        { name: 'ExUnit', index: 'ExUnit', width: 100 },
        { name: 'ExTotal', index: 'ExTotal', width: 100 },
         { name: 'ExUnitNo', index: 'ExUnit', width: 100 },
        { name: 'ExTotalNo', index: 'ExTotalNo', width: 100 },
        { name: 'Remark', index: 'Remark', width: 200 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '退货主表',

        gridComplete: function () {
            var ids = jQuery("#Detaillist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#Detaillist").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#Detaillist").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#Detaillist").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            //if (oldSelID != 0) {
            //    $('input[id=c' + oldSelID + ']').prop("checked", false);

            //}
            //$('input[id=c' + rowid + ']').prop("checked", true);
            //oldSelID = rowid;
            //ID = jQuery("#list").jqGrid('getRowData', rowid).RID//0812k
            //OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
            //select(rowid);
            //$("#Billlist tbody").html("");
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#myTable").getGridParam("lastpage"))
                    return;
                curPage = $("#myTable").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#myTable").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#myTable").getGridParam("page") - 1;
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
            $("#myTable").jqGrid("setGridHeight",100, false);
            $("#myTable").jqGrid("setGridWidth", 750, false);
        }
    });
}
//换货详细
function LoadReturnDetail() {
    jQuery("#ReturnTable").jqGrid({
        url: 'LoadReturnDetail',
        datatype: 'json',
        postData: { Eid: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['退货单号', '物料编码', '产品名称', '规格型号', '生产厂家', '退货数量', '单位', '退货单价含税', '退货总结含税', '退货单价不含税', '退货总价不含税', '备注', ],
        colModel: [
        { name: 'EID', index: 'EID', width: 200 },
        { name: 'ProductID', index: 'ProductID', width: 200 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'Amount', index: 'Amount', width: 100 },
        { name: 'Unit', index: 'Unit', width: 100 },
        { name: 'ExUnit', index: 'ExUnit', width: 100 },
        { name: 'ExTotal', index: 'ExTotal', width: 100 },
         { name: 'ExUnitNo', index: 'ExUnit', width: 100 },
        { name: 'ExTotalNo', index: 'ExTotalNo', width: 100 },
        { name: 'Remark', index: 'Remark', width: 200 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '换货详细',

        gridComplete: function () {
            var ids = jQuery("#DetailReturnlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#DetailReturnlist").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#DetailReturnlist").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#DetailReturnlist").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            //if (oldSelID != 0) {
            //    $('input[id=c' + oldSelID + ']').prop("checked", false);

            //}
            //$('input[id=c' + rowid + ']').prop("checked", true);
            //oldSelID = rowid;
            //ID = jQuery("#list").jqGrid('getRowData', rowid).RID//0812k
            //OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
            //select(rowid);
            //$("#Billlist tbody").html("");
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage == $("#ReturnTable").getGridParam("lastpage"))
                    return;
                curPage = $("#ReturnTable").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage = $("#ReturnTable").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage == 1)
                    return;
                curPage = $("#ReturnTable").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#ReturnTable").jqGrid("setGridHeight",100, false);
            $("#ReturnTable").jqGrid("setGridWidth",750, false);
        }
    });
}

function reload(){
    $("#myTable").jqGrid('setGridParam', {
        url: 'LoadExchangeDetail',
        datatype: 'json',
        postData: { Eid: ID, curpage: curPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

function reload2() {
    $("#ReturnTable").jqGrid('setGridParam', {
        url: 'LoadReturnDetail',
        datatype: 'json',
        postData: { Eid: ID, curpage: curPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");

    //LoadReceiveBill();
}



