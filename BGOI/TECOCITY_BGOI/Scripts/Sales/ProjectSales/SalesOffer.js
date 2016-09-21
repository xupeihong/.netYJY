$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
     LoadRecordOffer(ID);
    LoadRecordOfferInfo(ID);

});
var curPage = 1;
var OnePageCount = 15;
function LoadRecordOffer(ID) {
    var Xid = ID;
    jQuery('#BJlist').jqGrid({
        url: 'GetRecordOffer',
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
        colNames: ['报价单号', '项目编号', '报价单位', '报价人', '审批人1', '审批人2', '报价说明', '报价日期', '联系人', '联系人电话', '客户', '客户联系人', '交货方式', '金额', '状态'],
        colModel: [
        { name: 'BJID', index: 'BJID', width: 150 },
        { name: 'PID', index: 'PID', width: 100 },
        { name: 'OfferUnit', index: 'OfferUnit', width: 100 },
        { name: 'OfferContacts', index: 'OfferContacts', width: 100 },
        { name: 'Approvaler1', index: 'Approvaler1', width: 100 },
        { name: 'Approvaler2', index: 'Approvaler2', width: 100 },
        { name: 'Description', index: 'Description', width: 100 },
        { name: 'OfferTime', index: 'OfferTime', width: 100 },
        { name: 'People', index: 'People', width: 100 },
        { name: 'Tel', index: 'Tel', width: 100 },
        { name: 'Customer', index: 'Customer', width: 100 },
        { name: 'CustomerPeople', index: 'CustomerPeople', width: 100 },
        { name: 'Delivery', index: 'Delivery', width: 100 },
        { name: 'Total', index: 'Total', width: 100 },
        { name: 'State', index: 'State', width: 100 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '报价主表',
        gridComplete: function () {
            var ids = jQuery("#BJlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var Model = jQuery("#BJlist").jqGrid('getRowData', id);
                Up_Down = "<a href='#' style='color:blue' onclick='DownloadFile(" + id + ")'  >详情</a>";
                jQuery("#BJlist").jqGrid('setRowData', ids[i], { DID: Up_Down });

            }
        },
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //    //DID = jQuery("#loadlist").jqGrid('getRowData', rowid).PID;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#BJlist").getGridParam("lastpage"))
                    return;
                curPage = $("#BJlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#BJlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#BJlist").getGridParam("page") - 1;
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
            $("#BJlist").jqGrid("setGridHeight",150, false);
            $("#BJlist").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function LoadRecordOfferInfo(ID) {
    var Xid = ID;
    jQuery('#BJInfo').jqGrid({
        url: 'GetRecordOfferInfo',
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
        colNames: ['报价单号', '细项编号', '物料编码', '产品名称', '规格型号', '生成厂家', '单位', '进货数量', '含税定价', '含税金额', '时间', '报价人'],
        colModel: [
        { name: 'BJID', index: 'BJID', width: 150 },
        { name: 'XID', index: 'XID', width: 100 },
        { name: 'ProductID', index: 'ProductID', width: 100 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'Unit', index: 'Unit', width: 100 },
        { name: 'Amount', index: 'Amount', width: 100 },
        { name: 'UnitPrice', index: 'UnitPrice', width: 100 },
        { name: 'Total', index: 'Total', width: 100 },
        { name: 'CreateTime', index: 'CreateTime', width: 100 },
        { name: 'CreateUser', index: 'CreateUser', width: 100 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '报价详细数据',
        gridComplete: function () {
            var ids = jQuery("#BJlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var Model = jQuery("#BJlist").jqGrid('getRowData', id);
                Up_Down = "<a href='#' style='color:blue' onclick='DownloadFile(" + id + ")'  >详情</a>";
                jQuery("#BJlist").jqGrid('setRowData', ids[i], { DID: Up_Down });

            }
        },
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //    //DID = jQuery("#loadlist").jqGrid('getRowData', rowid).PID;
        //},

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
            $("#BJInfo").jqGrid("setGridHeight", 150, false);
            $("#BJInfo").jqGrid("setGridWidth", $("#bottom").width() - 30, false);
        }
    });
}
