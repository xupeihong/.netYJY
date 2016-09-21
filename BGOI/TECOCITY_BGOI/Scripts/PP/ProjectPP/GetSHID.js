var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var SHID;
var curPage1 = 1;
var OnePageCount1 = 6;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $("#btnSave").click(function () {

        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {

            var selectedIds = $("#list").jqGrid("getGridParam", "selarrrow");
            var texts = "";
            for (var i = 0 ; i < selectedIds.length; i++) {
                texts += jQuery("#list").jqGrid('getCell', selectedIds[i], 'DID') + ",";
            }
            texts = texts.substr(0, texts.length - 1);
            var mycars = new Array()
            mycars = texts.split(',');
            for (var i = 0; i < mycars.length; i++) {
                var DID = mycars[i];
                window.parent.addBasicDetail(DID);
            }
            window.parent.ClosePop();
        }


    })


});

function jq() {
    var SHID = "";
    jQuery("#list").jqGrid({
        url: 'SelectSHGoods',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount,SHID:SHID},
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
        colNames: ['','编号', '物品摘要', '物品型号', '物品编码', '生产厂家', '单位', '数量', '单价', '总金额', '用途'],
        colModel: [
            { name: 'DID', index: 'DID', width: 0, hidden: true },
            { name: 'SHID', index: 'SHID', width: 100 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'INID', index: 'INID', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 90 },
        { name: 'Unit', index: 'Unit', width: 50 },
        { name: 'Amount', index: 'Amount', width: 50 },
        { name: 'UnitPriceNoTax', index: 'UnitPriceNoTax', width: 120 },
        { name: 'TotalNoTax', index: 'TotalNoTax', width: 120 },
        { name: 'Use', index: 'Use', width: 100 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        multiselect: true,
        gridComplete: function () {

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
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function reload() {


    //$("#list").jqGrid('setGridParam', {
    //    url: 'BasicStockInList',
    //    datatype: 'json',
    //    postData: { curpage: curPage, rownum: OnePageCount },

    //}).trigger("reloadGrid");
}




