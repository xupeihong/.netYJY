
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
    jq2();
    $("#DY").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要打印的请购单");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).RKID;
        var url = "PrintRK?id=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });

    $("#rzxq").click(function () {
        RZ();
        this.className = "btnTw";
        $('#DJ').attr("class", "btnTh");
        $('#WP').attr("class", "btnTh");
        $("#bor1").css("display", "none");
        $("#danju").css("display", "none");
        $("#bor2").css("display", "");
        $("#bor3").css("display", "none");
    });

    $("#WP").click(function () {
        this.className = "btnTw";
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);

        var RKType = Model.RKType;
        if (RKType == 'C') {
            $("#bor3").css("display", "");
        }
        else {
            $("#bor1").css("display", "");
        }

        $('#DJ').attr("class", "btnTh");
        $('#rzxq').attr("class", "btnTh");

        $("#danju").css("display", "none");
        $("#bor2").css("display", "none");

    });

    $("#XG").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {

            var RKID = Model.RKID;
            var RKType = Model.RKType;
            var SHID = Model.SHID;
            if (RKType == 'C') {
                window.parent.parent.OpenDialog("修改", "../PPManage/UpdateRKXXer?XGRKID=" + RKID + "&SHID=" + SHID + "", 900, 550);
            }
            else {
                window.parent.parent.OpenDialog("修改", "../PPManage/UpdateRKXX?XGRKID=" + RKID + "", 900, 550);
            }


        }
    });

    $("#XX").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var RKID = Model.RKID;
            window.parent.parent.OpenDialog("详情", "../PPManage/DetailsRK?RKIDXQ=" + RKID + "", 900, 550);

        }
    });

    $("#CX").click(function () {
        isConfirm = confirm("确定要撤销吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {
                alert("请选择要操作的行");
                return;
            }
            else {
                var RKID = Model.RKID;
                var RKType = Model.RKType;
                $.ajax({
                    url: "UpdateRKValidate",
                    type: "Post",
                    data: {
                        RKID: RKID,rktype:RKType
                    },
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            reload();
                            alert("成功");
                        }
                        else {
                            alert("失败");
                        }
                    }
                });

            }
        }

    });

});



function jq() {

    var RKID = $('#RKID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();



    jQuery("#list").jqGrid({
        url: 'SelectRK',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, RKID: RKID, Begin: Begin, End: End },
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
        colNames: ['', '收货单号', '采购入库单号','', '仓库', '入库人', '单位', '入库日期', '入库说明'],
        colModel: [
            { name: 'RKType', index: 'RKType', width: 150, hidden: true },
            { name: 'SHID', index: 'SHID', width: 150 },
        { name: 'RKID', index: 'RKID', width: 150 },
           
        { name: 'CKID', index: 'CKID', width: 150 , hidden: true },
        { name: 'HouseName', index: 'HouseName', width: 150},
        { name: 'Handler', index: 'Handler', width: 100 },
         { name: 'DeptName', index: 'DeptName', width: 100 },
        { name: 'Rkdate', index: 'Rkdate', width: 100 },
        { name: 'RKInstructions', index: 'RKInstructions', width: 150 }
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
                $('#WP').attr("class", "btnTw");
                $('#DJ').attr("class", "btnTh");
                $('#rzxq').attr("class", "btnTh");
                $("#danju").css("display", "none");
                $("#bor2").css("display", "none");
                var RKID = Model.RKID;
                var RKType = Model.RKType;
                if (RKType == 'C') {
                    $("#bor3").css("display", "");
                    reload2(RKID);
                }
                else {
                    $("#bor3").css("display", "none");
                    $("#bor1").css("display", "");
                    reload1(RKID);
                }

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
    if ($('.field-validation-error').length == 0) {
        var RKID = $('#RKID').val();
        var Begin = $('#Begin').val();
        var End = $('#End').val();
        $("#list").jqGrid('setGridParam', {
            url: 'SelectRK',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, RKID: RKID, Begin: Begin, End: End },

        }).trigger("reloadGrid");
    }
}
function jq1(RKID) {

    var RKID = $('#RKID').val();

    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var OrderContent = $('#OrderContent').val();
    var Supplier = $("#Supplier").val();

    jQuery("#list1").jqGrid({
        url: 'SelectRKGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, RKID: RKID, Begin: Begin, End: End, OrderContent: OrderContent, Supplier: Supplier },
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
        colNames: ['编号', '物品名称', '规格型号', '单位', '数量', '供货商', '备注', '入库数量'],
        colModel: [
        { name: 'RKID', index: 'RKID', width: 180 },
        { name: 'OrderContent', index: 'OrderContent', width: 150 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Unit', index: 'Unit', width: 200 },
        { name: 'Amount', index: 'Amount', width: 180 },//width: $("#bor").width() - 800 
        { name: 'COMNameC', index: 'COMNameC', width: 180 },
        { name: 'Remark', index: 'Remark', width: 50 },
         { name: 'SJAmount', index: 'SJAmount', width: 50 }
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
            var RKID = Model.RKID;
            reload1(RKID);

        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function reload1(RKID) {
    $("#list1").jqGrid('setGridParam', {
        url: 'SelectRKGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, RKID: RKID },

    }).trigger("reloadGrid");
}

function jq2(RKID) {

    $("#bor3").css("display", "")
    $("#bor1").css("display", "none")
    jQuery("#list3").jqGrid({
        url: 'SelectRKCPXX',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, rkid: RKID },
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
        colNames: ['', '', '物品名称', '规格型号', '单位', '数量', '税前单价', '税前总价', '税后单价', '税后总价', '入库数量'],
        colModel: [
 { name: 'ID', index: 'ID', width: 0, hidden: true },
        { name: 'RKID', index: 'RKID', width: 180, hidden: true },
        { name: 'Name', index: 'Name', width: 110 },
        { name: 'Spc', index: 'Spc', width: 80 },
        { name: 'Units', index: 'Units', width: 60 },
             { name: 'Num', index: 'Num', width: 80 },

                { name: 'Price2', index: 'Price2', width: 80 },
                   { name: 'Price2s', index: 'Price2s', width: 80 },
                      { name: 'UnitPrice', index: 'UnitPrice', width: 80 },
                         { name: 'UnitPrices', index: 'UnitPrices', width: 80 },

             { name: 'RKnum', index: 'RKnum', width: 80 }
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '物品详细',
        //multiselect: true,
        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list3").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager3 :input").val();
            }
            var dataSel = jQuery("#list3").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list3").jqGrid('getRowData', ids);
            var DDID = Model.DDID;
            reload2(DDID);
        },
        loadComplete: function () {
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload2(RKID) {
    $("#bor3").css("display", "")
    $("#bor1").css("display", "none")
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list3").jqGrid('setGridParam', {
        url: 'SelectRKCPXX',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, rkid: RKID },

    }).trigger("reloadGrid");
}

function SearchOut() {

    var strDateStart = $('#Begin').val();
    var strDateEnd = $('#End').val();
    //if (strDateStart == "" && strDateEnd == "" && strDateStart1 == "" && strDateEnd1 == "") {

    //    getSearch();
    //}
    //else {
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
    else if (strDateS >= strDateE) {
        alert("截止日期不可以小于或等于开始日期");
        $("#End").val("");
    }
    else (strDateS == "" || strDateE == "")
    {
        getSearch();
    }

}

function getSearch() {
    curRow = 0;
    curPage = 1;

    var RKID = $('#RKID').val();

    var Begin = $('#Begin').val();
    var End = $('#End').val();

    $("#list").jqGrid('setGridParam', {
        url: 'SelectRK',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, RKID: RKID, Begin: Begin, End: End },
        loadonce: false

    }).trigger("reloadGrid");//重新载入

}

function RZ() {
    var Type = "入库";
    jQuery("#list2").jqGrid({
        url: 'SelectRZ',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },
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
        colNames: ['操作ID', '操作', '状态', '时间', '操作人'],
        colModel: [
              { name: 'RelevanceID', index: 'RelevanceID', width: 150 },
        { name: 'LogTitle', index: 'LogTitle', width: 150 },
          { name: 'LogContent', index: 'LogContent', width: 150 },
        { name: 'LogTime', index: 'LogTime', width: 150 },
        { name: 'LogPerson', index: 'LogPerson', width: 100 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        onSelectRow: function (rowid, status) {
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage1 == $("#list2").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage1 = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager2 :input").val();
            }
            list2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });

}

function list2() {

    if ($('.field-validation-error').length == 0) {
        var Type = "入库";
        $("#list2").jqGrid('setGridParam', {
            url: 'SelectRZ',
            datatype: 'json',
            postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },

        }).trigger("reloadGrid");
    }

}