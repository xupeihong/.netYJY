$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
    LoadShipment();
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});

var RowId = 0;
var curPage = 1;
var OnePageCount = 5;

function LoadShipment() {
    jQuery('#myTable').jqGrid({
        url: 'LoadOrderShipmentDetail',
        datatype: 'json',
        postData: { ShipGoodsID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['发货编码', '', '物品编码', '物品名称', '规格型号', '生成厂家', '单位', '单价', '数量', '备注'],
        colModel: [
        { name: 'ShipGoodsID', index: 'ShipGoodsID', width: 100 },
        { name: 'DID', index: 'DID', width: 100 ,hidden:true },
        { name: 'ProductID', index: 'ProductID', width: 100 },//
         { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'Unit', index: 'Unit', width: 100 },
        { name: 'Price', index: 'Price', width: 100 },
        { name: 'Amount', index: 'Amount', width: 100 },
        { name: 'Remark', index: 'Remark', width: 100 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '发货物品详细表',
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
            //if (oldSelID != 0) {
            //    $('input[id=c' + oldSelID + ']').prop("checked", false);
            //}
            //$('input[id=c' + rowid + ']').prop("checked", true);
            //oldSelID = rowid;
            //DID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
            //select(rowid);
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
            else if (pgButton == "prev_pager") {
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
            reload1();
        },
        loadComplete: function () {
            $("#myTable").jqGrid("setGridHeight", 150, false);
            $("#myTable").jqGrid("setGridWidth", 750, false);
        }
    });
}

function reload1() {
    $("#myTable").jqGrid('setGridParam', {
        url: 'LoadOrderShipmentDetail',
        datatype: 'json',
        postData: { ShipGoodsID: ID, curpage: curPage, rownum: OnePageCount },

    }).trigger("reloadGrid");
}




