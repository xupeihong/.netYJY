$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor1").width() - 30);


    $("#StartDate").val("");
    $("#EndDate").val("");
    LoadOrderInfo();
    jq1();
    //LoadOrderDetail();
    //LoadOrderBill();

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
            if (location.search.split('&')[0].split('=')[0] == "") {
                parent.frames["iframeRight"].Getid(id);
            }
            else {
                window.parent.Getid(id);
            }



            //window.parent.parent.Getid(id);


            window.parent.ClosePop();
        }
    });


});
var curPage = 1;
var OnePageCount = 15;
var curPage1 = 1;
var OnePageCount1 = 15;
var DID = 0;
var oldSelID = 0;

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'GetOrderInfo',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },

    }).trigger("reloadGrid");
}

function LoadOrderInfo() {
    jQuery('#list').jqGrid({
        url: 'GetOrderInfo',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
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
        colNames: ['合同编号', '订单编号', '联系人', '联系人电话', '单位地址', '订单状态'],
        colModel: [

        { name: 'ContractID', index: 'ContractID', width: 100 },
        { name: 'OrderID', index: 'OrderID', width: 100 },
        { name: 'OrderContactor', index: 'OrderContactor', width: 100 },
         { name: 'OrderTel', index: 'OrderTel', width: 100 },
        { name: 'OrderAddress', index: 'OrderAddress', width: 100 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 }

        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '订单主表',
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

            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {

                return;
            }
            else {
             var id=   Model.OrderID;
             reload1(id);
            }
            oldSelID = rowid;
            DID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
            OOstate = jQuery("#list").jqGrid('getRowData', rowid).OOstate;
            State = jQuery("#list").jqGrid('getRowData', rowid).State;
            OrderJE = jQuery("#list").jqGrid('getRowData', rowid).Total;//订单金额
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
            $("#list").jqGrid("setGridHeight", 150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}


function jq1(OrderID) {

    jQuery("#list1").jqGrid({
        url: 'GetOrderInfoGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, OrderID: OrderID },
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
        colNames: ['订货内容', '规格型号', '厂家', '单位', '数量', '单价', '总计', '技术要求或参数', '交（提）货时间'],
        colModel: [

        { name: 'ordercontent', index: 'ordercontent', width: 180 },
        { name: 'SpecsModels', index: 'SpecsModels', width: 110 },
        { name: 'manufacturer', index: 'manufacturer', width: 80 },
        { name: 'orderunit', index: 'orderunit', width: 60 },
        { name: 'ActualAmount', index: 'ActualAmount', width: 80 },
        { name: 'unitprice', index: 'unitprice', width: 80 },
        { name: 'actualsubtotal', index: 'actualsubtotal', width: 90 },
        { name: 'technology', index: 'technology', width: 90 },

         { name: 'deliverytime', index: 'deliverytime', width: 90 }
          //{ name: 'RKState', index: 'RKState', width: 90 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '物品详细',
        //multiselect: true,
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
            var dataSel = jQuery("#list1").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list1").jqGrid('getRowData', ids);
            var DDID = Model.DDID;
            reload1(DDID);
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height()/2 - 150, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}

function reload1(id) {
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'GetOrderInfoGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, OrderID: id },

    }).trigger("reloadGrid");
}


function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    reload1();
    reload2();
}
//function reload1() {
//    $("#Detaillist").jqGrid('setGridParam', {
//        url: 'LoadOrderDetail',
//        datatype: 'json',
//        postData: { OrderID: DID, curpage: curPage, rownum: OnePageCount }, //,jqType:JQtype

//    }).trigger("reloadGrid");
//}
function reload2() {
    $("#Billlist").jqGrid('setGridParam', {
        url: 'LoadOrderBill',
        datatype: 'json',
        postData: { OrderID: DID, curpage: curPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}


function LoadSerachOrderInfo() {
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    if (StartDate == "" && EndDate == "") {
        GetSearchData();
    }
    else {
        var strSeparator = "-"; //日期分隔符
        var strDateArrayStart;
        var strDateArrayEnd;
        var intDay;
        strDateArrayStart = StartDate.split(strSeparator);
        strDateArrayEnd = EndDate.split(strSeparator);
        var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
        var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);
        if (strDateS <= strDateE) {
            GetSearchData();
        }
        else {
            alert("截止日期不能小于开始日期！");
            $("#End").val("");
            return false;
        }
    }


}
function GetSearchData() {
    var ContractID = $('#ContractID').val();
    var OrderUnit = $('#OrderUnit').val();
    var UseUnit = $('#UseUnit').val();
    var OrderContent = $('#OrderContent').val(); //$('#SpecsModels').click.Text();
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    var State = $("input[name='State']:checked").val();
    var HState = $("input[name='HState']:checked").val();
    $("#list").jqGrid('setGridParam', {
        url: 'GetSearchOrderInfo',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, ContractID: ContractID, OrderUnit: OrderUnit,
            UseUnit: UseUnit, OrderContent: OrderContent, StartDate: StartDate,
            EndDate: EndDate, State: State, HState: HState
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入


}


