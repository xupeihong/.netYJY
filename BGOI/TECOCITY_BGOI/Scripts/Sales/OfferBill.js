$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
    LoadOffer();
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});

var RowId = 0;
var curPage = 1;
var OnePageCount = 5;

function LoadOffer() {
    jQuery("#myTable").jqGrid({
        url: 'GetOfferDetailGrid',
        datatype: 'json',
        postData: { BJID: ID, curpage: curPage, rownum: OnePageCount },
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
        //序号  物品编号  物品名称  规格型号  单位  数量  供应商  单价  金额  备注
        colNames: [ '物品编号', '产品名称', '规格型号', '单位', '数量', '供应商', '单价','金额','备注'],
        colModel: [
        { name: 'ProductID', index: 'ProductID', width: 20 },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'Amount', index: 'Amount', width: 80 },
        { name: 'Manufacturer', index: 'Manufacturer', width: 100 },
        { name: 'Price', index: 'Price', width: 100 },
        { name: 'ActualSubTotal', index: 'ActualSubTotal', width: 100 },
        { name: 'Remark', index: 'Remark', width: 80 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {
            var ids = jQuery("#myTable").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            //if (oldSelID != 0) {
            //    $('input[id=c' + oldSelID + ']').prop("checked", false);
            //}
            //$('input[id=c' + rowid + ']').prop("checked", true);
            //oldSelID = rowid;
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage == $("#myTable").getGridParam("lastpage"))
                    return;
                curPage = $("#myTable").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage = $("#myTable").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage == 1)
                    return;
                curPage = $("#myTable").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage = 1;
            }
            else {
                curPage = $("#pager1 :input").val();
            }
            reload1()
        },
        loadComplete: function () {
            $("#myTable").jqGrid("setGridHeight", 100, false);
            $("#myTable").jqGrid("setGridWidth",750, false);
        }
    });
}

function reload1() {
    $("#myTable").jqGrid('setGridParam', {
        url: 'GetOfferDetailGrid',
        datatype: 'json',
        postData: { BJID: ID, curpage: curPage, rownum: OnePageCount },

    }).trigger("reloadGrid");
}




