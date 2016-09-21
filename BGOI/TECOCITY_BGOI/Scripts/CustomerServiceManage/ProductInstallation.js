﻿var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ListOutID;
var curPage1 = 1;
var OnePageCount1 = 4;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    jq1("");
    $("#De").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {

            var msg = "您真的确定要撤销吗?";
            if (confirm(msg) == true) {
                var AZID = Model.AZID;
                $.ajax({
                    type: "POST",
                    url: "DeProductInstallation",
                    data: { AZID: AZID },
                    success: function (data) {
                        alert(data.Msg);
                        reload();
                    },
                    dataType: 'json'
                });
                return true;
            } else {
                return false;
            }
        }
    });
    // 打印
    $("#btnPrint").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).BXKID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).AZID;
            var url = "PrintProductInstallation?Info=" + escape(texts)+"&type=0";
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})
function jq() {
    var PID = $('#PID').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $("#SpecsModels").val();
    var InstallName = $("#InstallName").val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();

    jQuery("#list").jqGrid({
        url: 'ProductInstallationList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, OrderContent: OrderContent, SpecsModels: SpecsModels, Begin: Begin, End: End, InstallName: InstallName },
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
        colNames: ['序号', '报装编号', '安装编号', '安装时间','安装人员', '是否收费', '是否开票', '开票类型', '客户满意度', '是否向用户说明包装内所含物品', '是否穿工作服', '是否指导用户使用及指导事项', '是否接收用户赠与的物品', '工作完成后是否做好清洁工作', '客户是否阅读安装单并签字', '备注', '记录人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'BZID', index: 'BZID', width: 150, align: "center" },
        { name: 'AZID', index: 'AZID', width: 150, align: "center" },
        { name: 'InstallTime', index: 'InstallTime', width: 150, align: "center" },
        { name: 'InstallName', index: 'InstallName', width: 150, align: "center" },
        { name: 'IsCharge', index: 'IsCharge', width: 100, align: "center" },
        { name: 'IsInvoice', index: 'IsInvoice', width: 100, align: "center" },
        { name: 'ReceiptType', index: 'ReceiptType', width: 80, align: "center" },
        { name: 'SureSatisfied', index: 'SureSatisfied', width: 80, align: "center" },
        { name: 'IsProContent', index: 'IsProContent', width: 80, align: "center" },
        { name: 'IsWearClothes', index: 'IsWearClothes', width: 150, align: "center" },
        { name: 'IsTeaching', index: 'IsTeaching', width: 150, align: "center" },
        { name: 'IsGifts', index: 'IsGifts', width: 100, align: "center" },
        { name: 'IsClean', index: 'IsClean', width: 100, align: "center" },
        { name: 'IsUserSign', index: 'IsUserSign', width: 80, align: "center" },
        { name: 'Remark', index: 'Remark', width: 80, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 80, align: "center" }
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

                var AZID = Model.AZID;
                reload1(AZID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()/2 - 115, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function reload() {
    var PID = $('#PID').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $("#SpecsModels").val();
    var InstallName = $("#InstallName").val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();

    $("#list").jqGrid('setGridParam', {
        url: 'ProductInstallationList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, OrderContent: OrderContent, InstallName: InstallName, SpecsModels: SpecsModels, Begin: Begin, End: End },

    }).trigger("reloadGrid");
}
//查询
function SearchOut() {
    var strDateStart = $('#Begin').val();
    var strDateEnd = $('#End').val();
    if (strDateStart == "" && strDateEnd == "") {
        getSearch();
    }
    else {
        var strSeparator = "-"; //日期分隔符
        var strDateArrayStart;
        var strDateArrayEnd;
        var intDay;
        strDateArrayStart = strDateStart.split(strSeparator);
        strDateArrayEnd = strDateEnd.split(strSeparator);
        var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
        var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);
        if (strDateS <= strDateE) {
            getSearch();
        }
        else {
            alert("截止日期不能小于开始日期！");
            $("#End").val("");
            return false;
        }
    }
}
function getSearch() {
    curRow = 0;
    curPage = 1;
    var PID = $('#PID').val();
    var OrderContent = $('#OrderContent').val();
   // var SpecsModels = $("#SpecsModels").val();
    var InstallName = $("#InstallName").val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();

    $("#list").jqGrid('setGridParam', {
        url: 'ProductInstallationList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, OrderContent: OrderContent, InstallName: InstallName, Begin: Begin, End: End },
        loadonce: false
    }).trigger("reloadGrid");//重新载入
}
function jq1(AZID) {

    jQuery("#list1").jqGrid({
        url: 'ProductInstallationDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, AZID: AZID },
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
        //AZID, BZDID, DID, PID, OrderContent, SpecsModels, Unit, Num, UnitPrice, Total
        colNames: ['序号', '产品编号', '产品名称', '规格型号', '单位', '数量', '单价'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'PID', index: 'PID', width: 100, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 120, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 120, align: "center" },
        { name: 'Unit', index: 'Unit', width: 80, align: "center" },
        { name: 'Num', index: 'Num', width: 80, align: "center" },
        { name: 'UnitPrice', index: 'UnitPrice', width: 80, align: "center" }
        //{ name: 'Manufacturer', index: 'Manufacturer', width: 120, align: "center" },
        //{ name: 'Remark', index: 'Remark', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '产品安装详细',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
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
                if (curPage == $("#list1").getGridParam("lastpage"))
                    return;
                curPage = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage == 1)
                    return;
                curPage = $("#list1").getGridParam("page") - 1;
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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height()/2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}
function reload1(AZID) {

  
    $("#list1").jqGrid('setGridParam', {
        url: 'ProductInstallationDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, AZID: AZID },

    }).trigger("reloadGrid");
}
//修改
function ScrapManagementUP() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        window.parent.parent.OpenDialog("修改产品安装", "../CustomerService/UpProductInstallationList?AZID=" + Model.AZID, 800, 550);
    }
   
}


