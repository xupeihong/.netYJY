$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
    var s = ID.substr(0, 2);
    if (s == "RW") {
        $("#RW").show();
        LoadRWPayment();
        LoadRWPaymentDetail();
    }
    if (s == "LL") {
        $("#LL").show();
        LoadLLPayment();
        LoadLLPaymentDetail();
    }
    if (s == "SG") {
        $("#SG").show();
        LoadSGPayment();
        LoadSGPaymentDetail();
    }
    if (s == "BG") {
        $("#BG").show();
        LoadBGPayment();
        LoadBGPaymentDetail();
    }
    if (s == "RK") {
        $("#RK").show();
        LoadRKPayment();
        LoadRKPaymentDetail();
    }
});

var curPage = 1;
var curPage1 = 1;
var OnePageCount = 15;
var DID = 0;
var oldSelID = 0;
function LoadRWPayment() {
    jQuery("#list").jqGrid({
        url: 'LoadRWPayment',
        datatype: 'json',
        postData: { RWID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '任务单号', '订货单位', '联系人', '联系方式', '开单日期', '生产说明', '状态'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'RWID', index: 'RWID', width: 160, align: "center" },
        { name: 'OrderUnit', index: 'OrderUnit', width: 120, align: "center" },
        { name: 'OrderContactor', index: 'OrderContactor', width: 150, align: "center" },
        { name: 'OrderTel', index: 'OrderTel', width: 100, align: "center" },
        { name: 'CreateTime', index: 'CreateTime', width: 80, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'Remark', index: 'Remark', width: 80, align: "center" },
        { name: 'State', index: 'State', width: 80, align: "center" }
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
            reloadT();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });

}
function reloadT()
{
    var OnePageCount1 = 4;
    $("#list").jqGrid('setGridParam', {
        url: 'LoadRWPayment',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, RWID: ID },

    }).trigger("reloadGrid");
}
function LoadRWPaymentDetail() {
    jQuery("#list1").jqGrid({
        url: 'LoadRWPaymentDetail',
        datatype: 'json',
        postData: { RWID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '物品名称', '规格型号', '单位', '数量', '技术要求及参数', '完成日期', '备注', '状态'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 100, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 120, align: "center" },
        { name: 'OrderUnit', index: 'OrderUnit', width: 120, align: "center" },
        { name: 'OrderNum', index: 'OrderNum', width: 80, align: "center" },
        { name: 'Technology', index: 'Technology', width: 80, align: "center" },
        { name: 'DeliveryTime', index: 'DeliveryTime', width: 80, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" },
        { name: 'State', index: 'State', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '产品详细',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage1"))
                    return;
                curPage1 = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            reloadT1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}
function reloadT1()
{
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'LoadRWPaymentDetail',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount, RWID: ID },

    }).trigger("reloadGrid");
}

function LoadLLPayment() {
    //GetOrderInfoGrid
    jQuery("#list").jqGrid({
        url: 'LoadLLPayment',
        datatype: 'json',
        postData: { LLID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '编号', '领料部门', '开单日期', '产品编号', '产品名称', '规格型号', '数量', '状态'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'LLID', index: 'LLID', width: 160, align: "center" },
        { name: 'MaterialDepartment', index: 'MaterialDepartment', width: 120, align: "center" },
        { name: 'MaterialTime', index: 'MaterialTime', width: 150, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'PID', index: 'PID', width: 100, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 100, align: "center" },
        { name: 'Specifications', index: 'Specifications', width: 80, align: "center" },
        { name: 'Amount', index: 'Amount', width: 80, align: "center" },
        { name: 'State', index: 'State', width: 80, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;

            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {

                return;
            }
            else {
                var LLID = Model.LLID;
                //reload1(LLID);
            }
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
            reloadll();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() +100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function reloadll()
{
    var OnePageCount1 = 4;
    $("#list").jqGrid('setGridParam', {
        url: 'LoadLLPayment',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, LLID: ID },

    }).trigger("reloadGrid");
}
function LoadLLPaymentDetail() {
    jQuery("#list1").jqGrid({
        url: 'LoadLLPaymentDetail',//'GetOfferInfoGrid'
        datatype: 'json',
        postData: { LLID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '零件名称', '规格型号', '单台数量','单位', '数量', '进货批次', '交货日期', '备注'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 100, align: "center" },
        { name: 'Specifications', index: 'Specifications', width: 120, align: "center" },
        { name: 'Manufacturer', index: 'Manufacturer', width: 120, align: "center" },
        { name: 'OrderUnit', index: 'OrderUnit', width: 80, align: "center" },
        { name: 'OrderNum', index: 'OrderNum', width: 80, align: "center" },
        { name: 'Technology', index: 'Technology', width: 80, align: "center" },
        { name: 'DeliveryTime', index: 'DeliveryTime', width: 120, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" }

        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
           
        },
       

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage1"))
                    return;
                curPage1 = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            reloadll1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 15, false);
        }
    });
}
function reloadll1()
{
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'LoadLLPaymentDetail',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount, LLID: ID },

    }).trigger("reloadGrid");
}

function LoadSGPayment() {
    jQuery("#list").jqGrid({
        url: 'LoadSGPayment',
        datatype: 'json',
        postData: { SGID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '编号', '发单日期', '技术要求', '技术负责人', '状态'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'SGID', index: 'SGID', width: 160, align: "center" },
        { name: 'billing', index: 'billing', width: 150, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'OrderContent', index: 'OrderContent', width: 200, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 100, align: "center" },
        { name: 'State', index: 'State', width: 80, align: "center" }
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
            //LoadOrdersInfo();
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
            reloadSG();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function reloadSG()
{
    var OnePageCount1 = 4;
    $("#list").jqGrid('setGridParam', {
        url: 'LoadSGPayment',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, SGID: ID },

    }).trigger("reloadGrid");
}
function LoadSGPaymentDetail() {
    jQuery("#list1").jqGrid({
        url: 'LoadSGPaymentDetail',
        datatype: 'json',
        postData: { SGID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '工序', '班主', '预计完工日期', '责任人', '计划数量', '完成数量-合格', '完成数量-返修', '完成数量-变更', '完成数量-废品', '实际完工日期', '检验员', '原因'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'Process', index: 'Process', width: 100, align: "center" },
        { name: 'team', index: 'team', width: 120, align: "center" },
        { name: 'Estimatetime', index: 'Estimatetime', width: 120, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'person', index: 'person', width: 80, align: "center" },
        { name: 'plannumber', index: 'plannumber', width: 80, align: "center" },
        { name: 'Qualified', index: 'Qualified', width: 80, align: "center" },
        { name: 'number', index: 'number', width: 80, align: "center" },
        { name: 'numbers', index: 'numbers', width: 80, align: "center" },
        { name: 'Fnubers', index: 'Fnubers', width:80, align: "center" },
        { name: 'finishtime', index: 'finishtime', width: 80, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'people', index: 'people', width: 80, align: "center" },
        { name: 'reason', index: 'reason', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
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
            DID = jQuery("#list1").jqGrid('getRowData', rowid).PID;
            // LoadOrdersInfo();

        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage1"))
                    return;
                curPage1 = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            reloadSG1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() +100, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 15, false);
        }
    });
}
function reloadSG1()
{
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'LoadSGPaymentDetail',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount, SGID: ID },

    }).trigger("reloadGrid");
}

function LoadBGPayment() {
    jQuery('#list').jqGrid({
        url: 'LoadBGPayment',
        datatype: 'json',
        postData: { BGID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '报告单编号', '创建日期', '产品编号', '产品名称', '规格型号', '数量', '备注', '记录人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'BGID', index: 'BGID', width: 160, align: "center" },
        { name: 'CreateTime', index: 'CreateTime', width: 150, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'DID', index: 'DID', width: 200, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 100, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 80, align: "center" },
        { name: 'OrderNum', index: 'OrderNum', width: 200, align: "center" },
        { name: 'Remark', index: 'Remark', width: 100, align: "center" },
        { name: 'CreatePerson', index: 'CreatePerson', width: 80, align: "center" }
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
            //select(rowid);
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
            reloadBG();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", 150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function reloadBG() {
    var OnePageCount1 = 4;
    $("#list").jqGrid('setGridParam', {
        url: 'LoadBGPayment',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, BGID: ID },

    }).trigger("reloadGrid");
}
function LoadBGPaymentDetail() {
    jQuery('#list1').jqGrid({
        url: 'LoadBGPaymentDetail',
        datatype: 'json',
        postData: { BGID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号','报告编码', '文档类型', '文档名称', '操作人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'BGID', index: 'BGID', width: 100 },
        { name: 'Type', index: 'Type', width: 100 },
        { name: 'FileName', index: 'FileName', width: 100 },
        { name: 'CreatePerson', index: 'CreatePerson', width: 100 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '检测报告详细列表',
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            DID = jQuery("#list1").jqGrid('getRowData', rowid).OrderID;
            //select(rowid);
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage1"))
                    return;
                curPage1 = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage = $("#pager1:input").val();
            }
            reloadBG1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}
function reloadBG1() {
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'LoadBGPaymentDetail',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount, BGID: ID },

    }).trigger("reloadGrid");
}

function LoadRKPayment() {
    jQuery("#list").jqGrid({
        url: 'LoadRKPayment',
        datatype: 'json',
        postData: { RKID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '入库单号', '物品摘要', '入库人', '入库日期', '入库说明'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'RKID', index: 'RKID', width: 160, align: "center" },
        { name: 'Validate', index: 'Validate', width: 150, align: "center" },
        { name: 'StockInUser', index: 'StockInUser', width: 200, align: "center" },
        { name: 'StockInTime', index: 'StockInTime', width: 80, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'StockRemark', index: 'StockRemark', width: 80, align: "center" }
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
            //select(rowid);
            //$("#Billlist tbody").html("");
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
            reloadRK();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function reloadRK() {
    var OnePageCount1 = 4;
    $("#list").jqGrid('setGridParam', {
        url: 'LoadRKPayment',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, BGID: ID },

    }).trigger("reloadGrid");
}
function LoadRKPaymentDetail() {
    jQuery('#list1').jqGrid({
        url: 'LoadRKPaymentDetail',
        datatype: 'json',
        postData: { RKID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '产品编码', '产品名称', '产品规格', '单位', '数量', '备注'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'PID', index: 'PID', width: 100, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 120, align: "center" },
        { name: 'Specifications', index: 'Specifications', width: 120, align: "center" },
        { name: 'Unit', index: 'Unit', width: 80, align: "center" },
        { name: 'Amount', index: 'Amount', width: 80, align: "center" },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '产品详情',
        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            DID = jQuery("#list1").jqGrid('getRowData', rowid).OrderID;
            //select(rowid);
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage1"))
                    return;
                curPage1 = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            reloadRK1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() + 100,  false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}
function reloadRK1() {
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'LoadRKPaymentDetail',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount, BGID: ID },

    }).trigger("reloadGrid");
}
