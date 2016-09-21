
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var RKID;
var curPage1 = 1;
var OnePageCount1 = 20;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#pageContent").width($(window).width() - 5);
    $("#search").width($("#bor").width() - 15);
    jq();
    jq1();
    $("#JY").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var LJReturnDate = Model.LJReturnDate;

            var TransferNum = Model.TransferNum;
            if (LJReturnDate == "") {
                window.parent.parent.OpenDialog("检验", "../PPManage/InsertGoods?id=" + TransferNum + "", 900, 550);
            }
            else
            {
                alert("此订单以检验！！！")
                return;
            }
        }



    });
    $("#SC").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {

            var TransferNum = Model.TransferNum;
            var LJReturnDate = Model.LJReturnDate;
            if (LJReturnDate == "") {
                alert("交接单还没有检验！！！");
            }
            else {
                window.parent.parent.OpenDialog("签字", "../PPManage/Production?id=" + TransferNum + "", 450, 200);
            }

        }


    });
    $("#JH").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {

            var TransferNum = Model.TransferNum;
            var LJReturnDate = Model.LJReturnDate;
            if (LJReturnDate == "") {
                alert("交接单还没有检验！！！");
            }
            else {
                window.parent.parent.OpenDialog("签字", "../PPManage/Plan?id=" + TransferNum + "", 450, 200);
            }

        }


    });

    $("#KG").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {

            var TransferNum = Model.TransferNum;
            var productionPeople = Model.productionPeople;
            var planPeople = Model.planPeople;
            if (productionPeople == "" || planPeople == "") {
                alert("生产或计划负责人还没有检验！！！");
            }
            else {
                window.parent.parent.OpenDialog("签字", "../PPManage/Warehouse?id=" + TransferNum + "", 450, 200);
            }

        }


    });
});



function jq() {





    jQuery("#list").jqGrid({
        url: 'SelectJJD',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
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
        colNames: ['收货单号','交接单编号', '送检人', '送检日期', '要求完成日期', '零件返回日期', '收检人', '总结', '检验员', '生产部分负责人', '计划供应负责人', '库管员'],
        colModel: [
            { name: 'SHID', index: 'SHID', width: 150 },
            { name: 'TransferNum', index: 'TransferNum', width: 150 },
        { name: 'SJPeople', index: 'SJPeople', width: 150 },
        { name: 'Inspectiondate', index: 'Inspectiondate', width: 150 },
        { name: 'Gooddate', index: 'Gooddate', width: 100 },
         { name: 'LJReturnDate', index: 'LJReturnDate', width: 100 },
         { name: 'Bak', index: 'Bak', width: 100 },
        { name: 'Summary', index: 'Summary', width: 100 },
        { name: 'testPeople', index: 'testPeople', width: 150 },
         { name: 'productionPeople', index: 'productionPeople', width: 150 },
          { name: 'planPeople', index: 'planPeople', width: 150 },
           { name: 'Warehouse', index: 'Warehouse', width: 150 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',


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
                var TransferNum = Model.TransferNum;
                reload1(TransferNum);
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
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function reload() {

    $("#list").jqGrid('setGridParam', {
        url: 'SelectJJD',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },

    }).trigger("reloadGrid");

}
function jq1(TransferNum) {



    jQuery("#list1").jqGrid({
        url: 'SelectJJDGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TransferNum: TransferNum },
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
        colNames: ['零件编号', '零件名称', '规格型号', '单位', '数量', '供货商', '合格数量', '不合格数量'],
        colModel: [
        { name: 'GoodsNum', index: 'GoodsNum', width: 100 },
        { name: 'GoodsName', index: 'GoodsName', width: 150 },
        { name: 'GoodsSpe', index: 'GoodsSpe', width: 150 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'Amount', index: 'Amount', width: 100 },//width: $("#bor").width() - 800 
        { name: 'COMNameC', index: 'COMNameC', width: 200 },
        { name: 'YesAmount', index: 'YesAmount', width: 100 },
         { name: 'NoAmount', index: 'NoAmount', width: 100 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '物品详细',

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage"))
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
            else if (pgButton == "first_pager") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            var TransferNum = Model.TransferNum;
            reload1(TransferNum);

        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function reload1(TransferNum) {
    $("#list1").jqGrid('setGridParam', {
        url: 'SelectJJDGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TransferNum: TransferNum },

    }).trigger("reloadGrid");
}







