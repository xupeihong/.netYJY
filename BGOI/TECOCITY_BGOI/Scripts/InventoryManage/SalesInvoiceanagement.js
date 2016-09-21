var curPage = 1;
var OnePageCount = 10;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ListOutID;
var curPage1 = 1;
var OnePageCount1 = 6;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    jq1("");
    $("#Fin").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var ShipGoodsID = Model.ShipGoodsID;
            $.ajax({
                type: "POST",
                url: "UpSalesInvoDateState",
                data: { ShipGoodsID: ShipGoodsID },
                success: function (data) {
                    alert(data.Msg);
                    reload();
                },
                dataType: 'json'
            });

        }
    });
})
//function ScrapManagementOut() {

//    window.parent.parent.OpenDialog("新建发货单", "../InventoryManage/SalesInvoiceManagementOut", 800, 450);

//}
function jq() {
    var OrderID = $('#OrderID').val();
    var HouseID = $('#HouseID').val();
    var ShipGoodeID = $('#ShipGoodeID').val();
    var PID = $('#ProID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    //var State = $("input[name='State']:checked").val();
    var ContractID = $('#ContractID').val();
    jQuery("#list").jqGrid({
        url: 'SalesInvoiceManagementList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderID: OrderID, HouseID: HouseID, Begin: Begin, End: End, ShipGoodeID: ShipGoodeID, PID: PID, ContractID: ContractID },
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
        colNames: ['序号','订单编号','发货单号','创建日期'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RowNumber', index: 'RowNumber', width: 150, align: "center" },
        { name: 'OrderID', index: 'OrderID', width: 120, align: "center" },
        { name: 'ShipGoodsID', index: 'ShipGoodsID', width: 180, align: "center" },
        { name: 'CreateTime', index: 'CreateTime', width: 100, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
       // multiselect: true,
        gridComplete: function () {
            //var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            //for (var i = 0; i < ids.length; i++) {
            //    var id = ids[i];
            //    jq1(id);
            //    var curRowData = jQuery("#list").jqGrid('getRowData', id);
            //    var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
            //    jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            //}
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

                var ShipGoodsID = Model.ShipGoodsID;
                reload1(ShipGoodsID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()/2 - 130, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function reload() {

    
    var ShipGoodeID = $('#ShipGoodeID').val();
    var PID = $('#ProID').val();
    

    var OrderID = $('#OrderID').val();
    var HouseID = $('#HouseID').val();
    var ContractID = $('#ContractID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
   // var State = $("input[name='State']:checked").val();
  
    $("#list").jqGrid('setGridParam', {
        url: 'SalesInvoiceManagementList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderID: OrderID, ShipGoodeID: ShipGoodeID, PID: PID, HouseID: HouseID, Begin: Begin, End: End, ContractID: ContractID },

    }).trigger("reloadGrid");
}
function jq1(ShipGoodsID) {
    jQuery("#list1").jqGrid({
        url: 'SalesInvoiceManagementDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, ShipGoodsID: ShipGoodsID },
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
        colNames: ['序号','产品编号','产品名称','规格型号','单位','申请数量', '单价', '厂家', '备注'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'ProductID', index: 'ProductID', width: 100, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 120, align: "center" },
        { name: 'Specifications', index: 'Specifications', width: 120, align: "center" },
        { name: 'Unit', index: 'Unit', width: 80, align: "center" },
        { name: 'Amount', index: 'Amount', width: 80, align: "center" },
        { name: 'Price', index: 'Price', width: 80, align: "center" },
        { name: 'Supplier', index: 'Supplier', width: 120, align: "center" },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '物品详细',

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
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}
function reload1(ShipGoodsID) {
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'SalesInvoiceManagementDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, ShipGoodsID: ShipGoodsID },

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
    var OrderID = $('#OrderID').val();
    var ShipGoodeID = $('#ShipGoodeID').val();
    var PID = $('#ProID').val();
    var ContractID = $('#ContractID').val();
    var HouseID = $('#HouseID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    //var State = $("input[name='State']:checked").val();
    //if (State == "1") {
    //    $('#Fin').hide();
    //} else {
    //    $('#Fin').show();
    //}
    $("#list").jqGrid('setGridParam', {
        url: 'SalesInvoiceManagementList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderID: OrderID, PID: PID, HouseID: HouseID, ShipGoodeID: ShipGoodeID, Begin: Begin, End: End, ContractID: ContractID },
        loadonce: false

    }).trigger("reloadGrid");//重新载入

}



