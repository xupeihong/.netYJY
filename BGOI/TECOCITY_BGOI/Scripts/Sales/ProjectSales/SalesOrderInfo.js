$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
    LoadOrderInfo(ID);
    LoadOrderInfoDetail(ID);

});
var curPage = 1;
var OnePageCount = 15;
function LoadOrderInfo(ID)
{
    var Xid = ID;
    jQuery('#OrderInfolist').jqGrid({
        url: 'GetOrderInfoGrid',
        datatype: 'json',
        postData: { Xid: Xid, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['备案编号', '订货编号', '合同编号', '销售类型', '签订日期', '订货单位', '联系人', '联系电话', '合计', '付款方式', '保修期', '状态'],
        colModel: [
        { name: 'PID', index: 'PID', width: 150 },
        { name: 'OrderID', index: 'OrderID', width: 100 },
        { name: 'ContractID', index: 'ContractID', width: 100 },
        { name: 'SalesType', index: 'SalesType', width: 100 },
        { name: 'ContractDate', index: 'ContractDate', width: 100 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 100 },
        { name: 'OrderContactor', index: 'OrderContactor', width: 100 },
        { name: 'OrderTel', index: 'OrderTel', width: 100 },
        { name: 'Total', index: 'Total', width: 100 },
        { name: 'PayWay', index: 'PayWay', width: 100 },
        { name: 'Guarantee', index: 'Guarantee', width: 100 },
        { name: 'State', index: 'State', width: 100 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '销售主表',
        //gridComplete: function () {
        //    var ids = jQuery("#DHlist").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var Model = jQuery("#DHlist").jqGrid('getRowData', id);
        //        Up_Down = "<a href='#' style='color:blue' onclick='DownloadFile(" + id + ")'  >详情</a>";
        //        jQuery("#DHlist").jqGrid('setRowData', ids[i], { DID: Up_Down });

        //    }
        //},
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#OrderInfolist").getGridParam("lastpage"))
                    return;
                curPage = $("#loadlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#OrderInfolist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#OrderInfolist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager5 :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#OrderInfolist").jqGrid("setGridHeight", 150, false);
            $("#OrderInfolist").jqGrid("setGridWidth", $("#bor").width()+250, false);
        }
    });
}
function LoadOrderInfoDetail(ID) {
    var Xid = ID;
    jQuery('#OrderInfoDeatilList').jqGrid({
        url: 'GetOrderInfoDetailGrid',
        datatype: 'json',
        postData: { Xid: Xid, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['订货单编号', '发货内容编号', '物料编码', '产品名称', '规格型号', '生成厂家', '单位', '数量', '含税单价', '含税金额', '时间', '状态'],
        colModel: [
        { name: 'OrderID', index: 'OrderID', width: 150 },
        { name: 'DID', index: 'DID', width: 100 },
        { name: 'ProductID', index: 'ProductID', width: 100 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'SpecsModels', index: 'SpecsModels', width: 100 },
        { name: 'Manufacturer', index: 'Manufacturer', width: 100 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 100 },
        { name: 'OrderNum', index: 'OrderNum', width: 100 },
        { name: 'Price', index: 'Price', width: 100 },
        { name: 'Subtotal', index: 'Subtotal', width: 100 },
        { name: 'DeliveryTime', index: 'DeliveryTime', width: 100 },
        { name: 'State', index: 'State', width: 100 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '订单详细数据',
        //gridComplete: function () {
        //    var ids = jQuery("#DHlist").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var Model = jQuery("#DHlist").jqGrid('getRowData', id);
        //        Up_Down = "<a href='#' style='color:blue' onclick='DownloadFile(" + id + ")'  >详情</a>";
        //        jQuery("#DHlist").jqGrid('setRowData', ids[i], { DID: Up_Down });

        //    }
        //},
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#OrderInfoDeatilList").getGridParam("lastpage"))
                    return;
                curPage = $("#OrderInfoDeatilList").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#OrderInfoDeatilList").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#OrderInfoDeatilList").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager1 :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#OrderInfoDeatilList").jqGrid("setGridHeight",150, false);
            $("#OrderInfoDeatilList").jqGrid("setGridWidth", $("#bor").width() + 250, false);
        }
    });
}