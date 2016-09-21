var curPage = 1;
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

        var M = "";
        var rowid = $("#list").jqGrid('getGridParam', 'selarrrow');
        for (var i = 0; i < rowid.length; i++) {
            if (i == 0) {
                var OrderID = jQuery("#list").jqGrid('getRowData', rowid[i]).OrderID;
                var ContractID = jQuery("#list").jqGrid('getRowData', rowid[i]).ContractID;
                var PID = jQuery("#list").jqGrid('getRowData', rowid[i]).PID;
            }
            else {
                var m = jQuery("#list").jqGrid('getRowData', rowid[i]).OrderID;
                var n = jQuery("#list").jqGrid('getRowData', rowid[i - 1]).OrderID;
                if (n != m) {
                    alert("请选择同一订货单");
                    return;
                }
            }
        }
        if (rowid.length == 0) {
            alert("请选择要生产的产品");
            return;
        }
        else {
            //var PID = Model.ProductID;
            //var OrderID = Model.OrderID;
            window.parent.customs(OrderID);
            window.parent.ClosePop();

        }


    })

})

function reload() {
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();

    $("#list").jqGrid('setGridParam', {
        url: 'ChangeSpecsModelsList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderContent: OrderContent, SpecsModels: SpecsModels },

    }).trigger("reloadGrid");
}

function jq() {
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();

    jQuery("#list").jqGrid({
        url: 'ChangeSpecsModelsList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderID: OrderContent, SpecsModels: SpecsModels },
        loadonce: false,
        mtype: 'POST',
        multiselect: true,
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
        colNames: ['序号', '订单号', '物品编号', '物品名称', '规格型号', '单位', '数量', '详细说明', '', ''],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'OrderID', index: 'OrderID', width: 150, align: "center" },
        { name: 'ProductID', index: 'ProductID', width: 150, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 120, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 120, align: "center" },
        { name: 'OrderUnit', index: 'OrderUnit', width: 150, align: "center" },
        { name: 'OrderNum', index: 'OrderNum', width: 150, align: "center" },
        { name: 'Remark', index: 'Remark', width: 150, align: "center" },
        { name: 'OrderID', index: 'OrderID', hidden: true },
        { name: 'ContractID', index: 'ContractID', hidden: true }
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function SearchOut() {
    curRow = 0;
    curPage = 1;

    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();

    $("#list").jqGrid('setGridParam', {
        url: 'ChangeSpecsModelsList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderID: OrderContent, SpecsModels: SpecsModels },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}



