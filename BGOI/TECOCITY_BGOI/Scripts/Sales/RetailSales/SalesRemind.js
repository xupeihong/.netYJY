var curPage = 1;
var OnePageCount = 15;
var SalesType = "";
$(document).ready(function () {
    LoadInfo();
});

function reload() {
    SalesType = $("#SalesType").val();
    if ($('.field-validation-error').length == 0) {
        $("#list").jqGrid('setGridParam', {
            url: 'GetSalesRemindList',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, SalesType: SalesType },

        }).trigger("reloadGrid");
    }
}

function LoadInfo() {
    SalesType = $("#SalesType").val();
    jQuery("#list").jqGrid({
        url: 'GetSalesRemindList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, SalesType: SalesType },
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
        colNames: ['订单编号','合同编号', '预计回款日期', '记录内容', '销售类型', '创建人', '所属单位'],
        colModel: [
        { name: 'PID', index: 'PID', width: 140, align: 'center' },
         { name: 'ContractID', index: 'ContractID', width: 140, align: 'center' },
        { name: 'SignTime', index: 'SignTime', width: 180, align: 'center' },
        { name: 'LogContent', index: 'LogContent', width: 200, align: 'center' },
        { name: 'ProductType', index: 'ProductType', width: 150, align: 'center' },
        { name: 'Actor', index: 'Actor', width: 150, align: 'center' },
        { name: 'Unit', index: 'Unit', width: 150, align: 'center' }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
        },
        onSelectRow: function (rowid, status) {

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
            $("#list").jqGrid("setGridHeight",350, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}