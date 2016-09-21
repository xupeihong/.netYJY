$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
    var s = ID.substr(0, 2);
    if (s == "BA")
    {
        LoadBAPayment();
        LoadBAPaymentDetail();
    }
    if (s == "BJ") {
        LoadBJPayment();
        LoadBJPaymentDetail();
    }
    if (s == "DH") {
        LoadDHPayment();
        LoadDHPaymentDetail();
    }
    if (s == "FH") {
        LoadFHPayment();
        LoadFHPaymentDetail();
    }
    if (s == "HK") {
        LoadHKPayment();
        LoadHKPaymentDetail();
    }
    if (s == "TH") {
        LoadTHPayment();
        LoadTHPaymentDetail();
    }
   

});

var curPage = 1;
var OnePageCount = 15;
var DID = 0;
var oldSelID = 0;
function LoadBAPayment()
{

        jQuery("#list").jqGrid({
            url: 'GetRecordInfoByPID',
            datatype: 'json',
            postData: {PID:ID, curpage: curPage, rownum: OnePageCount },
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
            colNames: ['', '信息日期', '项目名称', '工程编号', '内容', '规格型号', '业务负责人', '所属区域', '渠道来源', '进度'],
            colModel: [
            //{ name: 'IDCheck', index: 'Id', width: 20 },
            { name: 'PID', index: 'PID', width: 90, hidden: true },
            { name: 'RecordDate', index: 'RecordDate', width: 100 },
            { name: 'PlanName', index: 'PlanName', width: 80 },
            { name: 'PlanID', index: 'PlanID', width: 80 },
            { name: 'MainContent', index: 'MainContent', width: 150 },
            { name: 'Specifications', index: 'Specifications', width: 150 },
            { name: 'WorkChief', index: 'WorkChief', width: 70 },
            { name: 'BelongArea', index: 'BelongArea', width: 50 },
            { name: 'ChannelsFrom', index: 'ChannelsFrom', width: 100 },
            { name: 'State', index: 'State', width: 100 }
            ],
            pager: jQuery('#pager'),
            pgbuttons: true,
            rowNum: OnePageCount,
            viewrecords: true,
            imgpath: 'themes/basic/images',
            //caption: '销售表',

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
                if (oldSelID != 0) {
                    $('input[id=c' + oldSelID + ']').prop("checked", false);

                }
                $('input[id=c' + rowid + ']').prop("checked", true);
                oldSelID = rowid;
                ID = jQuery("#list").jqGrid('getRowData', rowid).PID//0812k
                // reload1(DID);
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
                $("#list").jqGrid("setGridHeight",150, false);
                $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
            }
        });
    
}

function LoadBAPaymentDetail()
{
    jQuery("#loadlist").jqGrid({
        url: 'GetDetailGrid',
        datatype: 'json',
        postData: { ID: ID, curpage: curPage, rownum: OnePageCount },
        loadonce: true,
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
        colNames: ['', '', '', '物品名称', '规格型号', '单位', '数量'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'XID', index: 'XID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'Amount', index: 'Amount', width: 80 }

        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        //gridComplete: function () {
        //    var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
        //        jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    }

        //},
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            DID = jQuery("#loadlist").jqGrid('getRowData', rowid).PID;
            // LoadOrdersInfo();

        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#loadlist").getGridParam("lastpage"))
                    return;
                curPage = $("#loadlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#loadlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#loadlist").getGridParam("page") - 1;
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
            $("#loadlist").jqGrid("setGridHeight", 150, false);
            $("#loadlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadBJPayment()
{
    //GetOrderInfoGrid
    jQuery("#list").jqGrid({
        url: 'GetOfferInfoByBJID',
        datatype: 'json',
        postData: {BJID:ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['报价单号', '项目单号', '报价标题', '报价说明', '报价人', '报价单位', '报价金额', '报价时间', '进度'],
        colModel: [
        { name: 'BJID', index: 'BJID', width: 90 },
        { name: 'offerPID', index: 'offerPID', width: 90 },
        { name: 'OfferTitle', index: 'OfferTitle', width: 90 },
        { name: 'Description', index: 'Description', width: 100 },
        { name: 'OfferContacts', index: 'OfferContacts', width: 80 },
        { name: 'OfferUnit', index: 'OfferUnit', width: 80 },
        { name: 'Total', index: 'Total', width: 150 },
        { name: 'OfferTime', index: 'OfferTime', width: 150 },
        { name: 'State', index: 'State', width: 70 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '报价表',

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
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            ID = jQuery("#list").jqGrid('getRowData', rowid).BJID//0812k
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
            $("#list").jqGrid("setGridHeight",150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });

}

function LoadBJPaymentDetail()
{
    jQuery("#loadlist").jqGrid({
        url: 'GetOfferDetailGrid',//'GetOfferInfoGrid'
        datatype: 'json',
        postData: { BJID: ID, curpage: curPage, rownum: OnePageCount },
        loadonce: true,
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
        colNames: ['', '', '', '物品名称', '规格型号', '单位', '数量'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'XID', index: 'XID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'Amount', index: 'Amount', width: 80 }

        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        //gridComplete: function () {
        //    var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
        //        jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    }

        //},
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            DID = jQuery("#Detaillist").jqGrid('getRowData', rowid).PID;
            // LoadOrdersInfo();

        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#Detaillist").getGridParam("lastpage"))
                    return;
                curPage = $("#Detaillist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#Detaillist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#Detaillist").getGridParam("page") - 1;
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
            $("#Detaillist").jqGrid("setGridHeight",150, false);
            $("#Detaillist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadDHPayment()
{
    jQuery("#list").jqGrid({
        url: 'GetOrderInfoGrid',
        datatype: 'json',
        postData: { Xid: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['', '订单编号', '合同', '订单所属单位部门', '签订日期', '订货单位', '订货单位联系人', '订货单位联系电话', '订货单位地址', ],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'OrderID', index: 'OrderID', width: 90},
        { name: 'ContractID', index: 'ContractID', width: 90},

        { name: 'UnitID', index: 'UnitID', width: 100 },
        { name: 'ContractDate', index: 'ContractDate', width: 80 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 80 },
        { name: 'OrderContactor', index: 'OrderContactor', width: 80 },//
        { name: 'OrderTel', index: 'OrderTel', width: 80 },// OrderAddress
        { name: 'OrderAddress', index: 'OrderAddress', width: 80 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        //gridComplete: function () {
        //    var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
        //        jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    }

        //},
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            OID = jQuery("#Orderlist").jqGrid('getRowData', rowid).PID;
           // LoadOrdersInfo();
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#loadlist").getGridParam("lastpage"))
                    return;
                curPage = $("#loadlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#loadlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#loadlist").getGridParam("page") - 1;
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
            $("#list").jqGrid("setGridHeight", 150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadDHPaymentDetail()
{
    jQuery("#loadlist").jqGrid({
        url: 'LoadOrderDetail',
        datatype: 'json',
        postData: { OrderID: ID, curpage: curPage, rownum: OnePageCount },
        loadonce: true,
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
        colNames: ['', '', '', '物品编码', '物品名称', '规格型号', '单位', '数量', '生产厂家', '单价', '合计', '技术参数', '提交时间', '状态', '备注'],
        colModel: [
        { name: 'PID', index: 'PID', width: 20, hidden: true },
        { name: 'OrderID', index: 'OrderID', width: 90, hidden: true },
        { name: 'DID', index: 'DID', width: 90, hidden: true },
        { name: 'ProductID', index: 'ProductID', width: 90 },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'SpecsModels', index: 'SpecsModels', width: 100 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 100 },
        { name: 'OrderNum', index: 'OrderNum', width: 100 },
        { name: 'Manufacturer', index: 'Manufacturer', width: 100 },
        { name: 'Price', index: 'Price', width: 100 },
        { name: 'Subtotal', index: 'Subtotal', width: 100 },
        { name: 'Technology', index: 'Technology', width: 80 },
        { name: 'DeliveryTime', index: 'DeliveryTime', width: 80 },
        { name: 'State', index: 'State', width: 80 },
        { name: 'Remark', index: 'Remark', width: 80 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        //gridComplete: function () {
        //    var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
        //        jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    }

        //},
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            DID = jQuery("#loadlist").jqGrid('getRowData', rowid).PID;
            // LoadOrdersInfo();

        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#loadlist").getGridParam("lastpage"))
                    return;
                curPage = $("#loadlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#loadlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#loadlist").getGridParam("page") - 1;
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
            $("#loadlist").jqGrid("setGridHeight", 150, false);
            $("#loadlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadFHPayment()
{
    jQuery('#list').jqGrid({
        url: 'LoadOrderShipment',
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
        colNames: ['订单编号', '发货单号', '发货日期', '订货人', '发货人', '备注', '创建时间'],
        colModel: [
        { name: 'OrderID', index: 'OrderID', width: 100 },
        { name: 'ShipGoodsID', index: 'ShipGoodsID', width: 100 },
        { name: 'ShipmentDate', index: 'ShipmentDate', width: 100 },
        { name: 'Orderer', index: 'Orderer', width: 100 },
        { name: 'Shippers', index: 'Shippers', width: 100 },
        { name: 'Remark', index: 'Remark', width: 100 },
        { name: 'CreateTime', index: 'CreateTime', width: 100 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '发货主表',
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
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            DID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
            select(rowid);
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
            $("#list").jqGrid("setGridHeight", 150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function LoadFHPaymentDetail()
{
    jQuery('#loadlist').jqGrid({
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
        { name: 'DID', index: 'DID', width: 100 },
        { name: 'ProductID', index: 'ProductID', width: 100 },//
         { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'Unit', index: 'Unit', width: 100 },
        { name: 'Price', index: 'Price', width: 100 },
        { name: 'Amount', index: 'Amount', width: 100 },
        { name: 'Remark', index: 'Remark', width: 100 }
        ],
        pager: jQuery('#pager'),
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
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            DID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
            select(rowid);
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
            $("#listDetail").jqGrid("setGridHeight", 150, false);
            $("#listDetail").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function LoadHKPayment()
{
    jQuery("#list").jqGrid({
        url: 'LoadReceivePayment',
        datatype: 'json',
        postData: {RID:ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['回款单号', '关联订单', '关联订单内容', '回款金额', '回款方式', '回款日期'],
        colModel: [
        { name: 'RID', index: 'RID', width: 200 },
        { name: 'OrderID', index: 'OrderID', width: 200 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Amount', index: 'Amount', width: 80 },
        { name: 'Mothods', index: 'Mothods', width: 80 },
        { name: 'PayTime', index: 'PayTime', width: 200 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

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
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            ID = jQuery("#list").jqGrid('getRowData', rowid).RID//0812k
            OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
            select(rowid);
            $("#Billlist tbody").html("");
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
            $("#list").jqGrid("setGridHeight",150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadTHPayment()
{

}
function LoadTHPaymentDetail()
{

}
