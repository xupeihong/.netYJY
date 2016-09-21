$(document).ready(function () {
    if (location.search != "") {
        OrderID = location.search.split('&')[0].split('=')[1];
    }
    LoadOrderInfo();
  
});

var RowId = 0;
var OrderID = 0;
function LoadOrderInfo() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetOrdersDetail",
        type: "post",
        data: { OrderID: OrderID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable  class="labProName' + rowCount + ' " id="ProName' + rowCount + '" >' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '" >' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable  class="labUnits' + rowCount + ' " id="Units' + rowCount + '" >' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable   id="Amount' + rowCount + '" style="width:30px;"  >' + json[i].OrderNum + '<lable/></td>';
                    html += '<td ><lable  style="width:30px;" readonly="readonly"  id="Supplier' + rowCount + '" >' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><lable style="width:60px;"  id="UnitPrice' + rowCount + '" onchange=ChangeP2() >' + json[i].Price + '</lable></td>';
                    html += '<td ><lable  style="width:60px;" readonly="readonly" id="Subtotal' + rowCount + '">' + json[i].Subtotal + '</lable> </td>';
                    html += '<td ><lable  id="UnitCost' + rowCount + '"  style="width:30px;"  >' + json[i].UnitCost + '</lable></td>';//单位成本';
                    html += '<td ><lable id="TotalCost' + rowCount + '" style="width:60px;" >' + json[i].TotalCost + '</lable></td>';//累计成本';
                    html += '<td ><lable style="width:100px;" id="Technology' + rowCount + '"  >' + json[i].Technology + '</lable> </td>';
                    html += '<td ><lable id="SaleNo' + rowCount + '" style="width:60px;"  >' + json[i].SaleNo + '</lable></td>';//销售单号';
                    html += '<td ><lable  id="ProjectNo' + rowCount + '" style="width:60px;" >' + json[i].ProjectNO + '</lable></td>';//工程项目编号
                    html += '<td ><lable id="JNAME' + rowCount + '" style="width:60px;" >' + json[i].JNAME + '</lable></td>';//工程项目名称';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows= CountRows + 1;
                }


            }
        }
    })
}

//退货详细
function LoadExchangeDetail() {
    jQuery("#Detaillist").jqGrid({
        url: 'LoadExchangeDetail',
        datatype: 'json',
        postData: { Eid: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['退货单号', '物料编码', '产品名称', '规格型号', '生产厂家', '退货数量', '单位', '退货单价含税', '退货总结含税', '退货单价不含税', '退货总价不含税', '备注', ],
        colModel: [
        { name: 'EID', index: 'EID', width: 200 },
        { name: 'ProductID', index: 'ProductID', width: 200 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'Amount', index: 'Amount', width: 100 },
        { name: 'Unit', index: 'Unit', width: 100 },
        { name: 'ExUnit', index: 'ExUnit', width: 100 },
        { name: 'ExTotal', index: 'ExTotal', width: 100 },
         { name: 'ExUnitNo', index: 'ExUnit', width: 100 },
        { name: 'ExTotalNo', index: 'ExTotalNo', width: 100 },
        { name: 'Remark', index: 'Remark', width: 200 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '退货主表',

        gridComplete: function () {
            var ids = jQuery("#Detaillist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#Detaillist").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#Detaillist").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#Detaillist").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
                curPage = $("#pager1 :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
//换货详细
function LoadReturnDetail() {
    jQuery("#DetailReturnlist").jqGrid({
        url: 'LoadReturnDetail',
        datatype: 'json',
        postData: { Eid: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['退货单号', '物料编码', '产品名称', '规格型号', '生产厂家', '退货数量', '单位', '退货单价含税', '退货总结含税', '退货单价不含税', '退货总价不含税', '备注', ],
        colModel: [
        { name: 'EID', index: 'EID', width: 200 },
        { name: 'ProductID', index: 'ProductID', width: 200 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'Amount', index: 'Amount', width: 100 },
        { name: 'Unit', index: 'Unit', width: 100 },
        { name: 'ExUnit', index: 'ExUnit', width: 100 },
        { name: 'ExTotal', index: 'ExTotal', width: 100 },
         { name: 'ExUnitNo', index: 'ExUnit', width: 100 },
        { name: 'ExTotalNo', index: 'ExTotalNo', width: 100 },
        { name: 'Remark', index: 'Remark', width: 200 }
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '换货详细',

        gridComplete: function () {
            var ids = jQuery("#DetailReturnlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#DetailReturnlist").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#DetailReturnlist").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#DetailReturnlist").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
                if (curPage == $("#DetailReturnlist").getGridParam("lastpage"))
                    return;
                curPage = $("#DetailReturnlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#DetailReturnlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#DetailReturnlist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager3 :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}