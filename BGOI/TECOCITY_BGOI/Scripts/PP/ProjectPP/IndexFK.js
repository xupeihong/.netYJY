var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var CID;
var curPage1 = 1;
var OnePageCount1 = 20;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#pageContent").width($(window).width() - 5);
    $("#search").width($("#bor").width() - 15);
    jq();
    jq1();
    jq2();
    $("#XX").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var PayId = Model.PayId;
            window.parent.parent.OpenDialog("详情", "../PPManage/DetailsFK?PayIdXQ=" + PayId + "", 900, 550);
        }
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
            var PayId = Model.PayId;
            var state = Model.Remark;
            var DDID = Model.DDID;
            if (state == "C") {
                window.parent.parent.OpenDialog("修改", "../PPManage/UpdateFKXXer?PayIdXQ=" + PayId + "&DDID=" + DDID + "", 1100, 550);
            }
            else {
                window.parent.parent.OpenDialog("修改", "../PPManage/UpdateFKXX?PayIdXQ=" + PayId + "", 900, 550);
            }

        }
    });

    $("#DY").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要打印的请购单");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).PayId;
        var url = "PrintFK?id=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });
    $("#rzxq").click(function () {
        RZ();
        this.className = "btnTw";

        $('#WP').attr("class", "btnTh");
        $("#bor1").css("display", "none");
        $("#bor3").css("display", "none");
        $("#bor2").css("display", "");
    });
    $("#WP").click(function () {

        this.className = "btnTw";

        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var Remark = Model.Remark;
            if (Remark == 'L') {
                $("#bor1").css("display", "");
            }
            else {
                $("#bor3").css("display", "");
            }
        }

        $('#rzxq').attr("class", "btnTh");
        $("#bor1").css("display", "");

        $("#bor2").css("display", "none");

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
                var PayId = Model.PayId;
                $.ajax({
                    url: "DelectFK",
                    type: "Post",
                    data: {
                        PayId: PayId
                    },
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
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
    //$("#btnPrint").click(function () {
    //    var dataSel = jQuery("#list").jqGrid('getGridParam');
    //    var ids = dataSel.selrow;
    //    var Model = jQuery("#list").jqGrid('getRowData', ids);
    //    var payid = Model.PayId;
    //    alert(payid);
    //    $.ajax({
    //        url: "ShipmentsToExcel",
    //        type: "post",
    //        data: { payid: payid },
    //        dataType: "Json",
    //    });
    //});
});




function jq() {
    var PayId = $('#PayId').val();
    var State = $('#State').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();

    jQuery("#list").jqGrid({
        url: 'SelectFK',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PayId: PayId, Begin: Begin, End: End, State: State },
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
        colNames: ['', '编号', '关联订单号', '付款人', '单位', '付费时间', '付款状态'],
        colModel: [
               { name: 'Remark', index: 'Remark', width: 150, hidden: true },
        { name: 'PayId', index: 'PayId', width: 150 },
        { name: 'DDID', index: 'DDID', width: 150 },
        { name: 'OrderContacts', index: 'OrderContacts', width: 150 },
           { name: 'DeptName', index: 'DeptName', width: 100 },
        { name: 'PayTime', index: 'PayTime', width: 150 },
        { name: 'Text', index: 'Text', width: 150 }

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

                var PayId = Model.PayId;
                $('#WP').attr("class", "btnTw");
                $('#DJ').attr("class", "btnTh");
                $('#rzxq').attr("class", "btnTh");
                $("#danju").css("display", "none");
                $("#bor2").css("display", "none");

                var Remark = Model.Remark;
                if (Remark == 'L') {
                    $("#bor1").css("display", "");
                    reload1(PayId);
                }
                else {
                    $("#bor3").css("display", "");
                    reload2(PayId);
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
            //$("#list").jqGrid("setGridHeight", $("#pageContent").height() - 350, false);
            //$("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload() {
    var PayId = $('#PayId').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $('#State').val();
    $("#list").jqGrid('setGridParam', {
        url: 'SelectFK',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PayId: PayId, Begin: Begin, End: End, State: State },

    }).trigger("reloadGrid");
}

function jq1(PayId) {

    $("#bor3").css("display", "none")
    $("#bor1").css("display", "")

    jQuery("#list1").jqGrid({
        url: 'SelectFKGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, PayId: PayId },
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
        colNames: ['编号', '物料编码', '物品名称', '规格型号', '供应商', '单位', '数量', '预计单价', '预计金额', '实际付款'],
        colModel: [
        { name: 'PayId', index: 'PayId', width: 180 },
        { name: 'INID', index: 'INID', width: 120 },
        { name: 'OrderContent', index: 'OrderContent', width: 150 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'COMNameC', index: 'COMNameC', width: 100 },
        { name: 'Unit', index: 'Unit', width: 60 },
        { name: 'Amount', index: 'Amount', width: 60 },
        { name: 'UnitPriceNoTax', index: 'UnitPriceNoTax', width: 100 },
        { name: 'TotalNoTax', index: 'TotalNoTax', width: 100 },
        { name: 'Rate', index: 'Rate', width: 100 }
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
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            var PayId = Model.PayId;
            reload1(PayId);
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload1(PayId) {
    $("#bor3").css("display", "none")
    $("#bor1").css("display", "")
    $("#list1").jqGrid('setGridParam', {
        url: 'SelectFKGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, PayId: PayId },

    }).trigger("reloadGrid");
}


function jq2(PayId) {

    $("#bor3").css("display", "")
    $("#bor1").css("display", "none")
    jQuery("#list3").jqGrid({
        url: 'SelectFKCPXX',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, payid: PayId },
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
        colNames: ['', '', '物品名称', '规格型号', '单位', '数量', '税前单价', '税前总价', '税后单价', '税后总价'],
        colModel: [
 { name: 'ID', index: 'ID', width: 0, hidden: true },
        { name: 'PAYID', index: 'PAYID', width: 180, hidden: true },
        { name: 'Name', index: 'Name', width: 110 },
        { name: 'Spc', index: 'Spc', width: 80 },
        { name: 'Units', index: 'Units', width: 60 },
             { name: 'Num', index: 'Num', width: 80 },

                { name: 'Price2', index: 'Price2', width: 80 },
                   { name: 'Price2s', index: 'Price2s', width: 80 },
                      { name: 'UnitPrice', index: 'UnitPrice', width: 80 },
                         { name: 'UnitPrices', index: 'UnitPrices', width: 80 }
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

function reload2(PayId) {
    $("#bor3").css("display", "")
    $("#bor1").css("display", "none")
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list3").jqGrid('setGridParam', {
        url: 'SelectFKCPXX',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, payid: PayId },

    }).trigger("reloadGrid");
}



function RZ() {
    var Type = "付款";
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
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });

}

function list2() {

    if ($('.field-validation-error').length == 0) {
        var Type = "付款";
        $("#list2").jqGrid('setGridParam', {
            url: 'SelectRZ',
            datatype: 'json',
            postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },

        }).trigger("reloadGrid");
    }

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

    var PayId = $('#PayId').val();
    var State = $('#State').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();



    $("#list").jqGrid('setGridParam', {
        url: 'SelectFK',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PayId: PayId, Begin: Begin, End: End, State: State },
        loadonce: false,

    }).trigger("reloadGrid");//重新载入

}