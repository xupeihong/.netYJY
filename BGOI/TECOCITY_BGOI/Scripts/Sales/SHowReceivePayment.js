$(document).ready(function () {
    LoadReceivePayment();
    //GetReceiveDetail();
    //LoadReceiveBill();
    //LoadFJ();
    //document.getElementById('div1').style.display = 'block';
    //document.getElementById('div2').style.display = 'none';
    //document.getElementById('FJ').style.display = 'none';
 
    $("#btnSearch").click(function () {
        reload();
    })



   
});
var ID = 0;
var OrderID = '';
var curPage = 1;
var FcunPage = 1;
var DcurPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
function LoadReceivePayment() {
    jQuery("#list").jqGrid({
        url: 'GetShowReceivePayment',
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
        colNames: ['回款单号', '合同编号', '关联订单', '关联订单内容', '回款金额', '回款方式', '回款日期'],
        colModel: [
        { name: 'RID', index: 'RID', width: 200 },
        { name: 'ContractID', index: 'ContractID', width: 200 },
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
         //  select(rowid);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function reload() {
    var OrderID = $("#OrderID").val();
    var ContractID = $("#ContractID").val();
    $("#list").jqGrid('setGridParam', {
        url: 'GetShowReceivePayment',
        datatype: 'json',
        postData: { curpage: 1, rownum: OnePageCount, ContractID: ContractID, OrderID: OrderID }, //,jqType:JQtype

    }).trigger("reloadGrid");
}