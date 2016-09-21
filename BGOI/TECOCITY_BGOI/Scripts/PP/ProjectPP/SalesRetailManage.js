var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var id = 0;
var ID = 0;
var DID = 0;
var OID = 0;
var objData = '';
var SalesProduct = "";
var SpecsModels = "";
var SalesMan = "";
var StartDate = "";
var EndDate = "";
$(document).ready(function () {
    LoadBasInfo();

    $("#QD").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的项");
            return;
        }
        else {
            var id = Model.OrderID;
            parent.frames["iframeRight"].Getid(id);
            window.parent.ClosePop();
        }
    });

    $("#btnSearch").click(function () {
        reload();
    });
});



function reload() {
    if ($('.field-validation-error').length == 0) {
        SalesProduct = $("#SalesProduct").val();
        SpecsModels = $("#SpecsModels").val();
        SalesMan = $("#SalesMan").val();
        StartDate = $("#StartDate").val();
        EndDate = $("#EndDate").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetSalesGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, SalesProduct: SalesProduct, SpecsModels: SpecsModels, SalesMan: SalesMan, StartDate: StartDate, EndDate: EndDate },

        }).trigger("reloadGrid");
    }
}

function LoadBasInfo() {
    SalesProduct = $("#SalesProduct").val();
    SpecsModels = $("#SpecsModels").val();
    SalesMan = $("#SalesMan").val();
    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();
    jQuery("#list").jqGrid({
        url: 'GetSalesGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, SalesProduct: SalesProduct, SpecsModels: SpecsModels, SalesMan: SalesMan, StartDate: StartDate, EndDate: EndDate },
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
        colNames: ['销售编号', '销售产品', '销售人员', '销售日期', '成交总价', '', '备注'],
        colModel: [
        { name: 'OrderID', index: 'OrderID', width: 140 },
        { name: 'OrderContent', index: 'OrderContent', width: 200 },
        { name: 'ProvidManager', index: 'ProvidManager', width: 130 },
        { name: 'ContractDate', index: 'ContractDate', width: 150 },
        { name: 'DTotalPrice', index: 'DTotalPrice', width: 150 },
        { name: 'ProductID', index: 'ProductID', width: 200, hidden: true },
        { name: 'Remark', index: 'Remark', width: 200 }
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
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            ID = jQuery("#list").jqGrid('getRowData', rowid).PID//0812k
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