
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    // 修改 完成 
    $("#XGSHD").click(function () {        
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).TakeID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).TakeID + "@" + "iframe";
            window.parent.OpenDialog("修改收货单", "../FlowMeterManage/EditDelivery?Info=" + escape(texts), 600, 450, '');
        }
    });

    // 撤销 完成  
    $("#CXSHD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).TakeID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).TakeID + "@" + "iframe";
            window.parent.OpenDialog("撤销收货单", "../FlowMeterManage/DelDelivery?Info=" + escape(texts), 700, 550, '');
        }
    });

    // 打印收货确认单 完成
    $("#DYQRD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).TakeID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).TakeID + "@" + "iframe";
            var url = "../FlowMeterManage/DeliveryDtail?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }

    }); 

    jq();// 加载收货单列表 
    jq1();// 加载收货单仪表列表

});

//
function jq() {
    jQuery("#list").jqGrid({
        url: 'LoadDeliveryList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            TakeID: $("#TakeID").val(), UnitName: $("#UnitName").val(),
            SDeliverDate: $("#SDeliverDate").val(), EDeliverDate: $("#EDeliverDate").val(),
            ReceiveName: $("#ReceiveName").val(), SReceiveDate: $("#SReceiveDate").val(),
            EReceiveDate: $("#EReceiveDate").val()
        },
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
        colNames: ['', '收货单号', '送货单位', '送货人', '联系电话', '送货日期', '收货人', '联系电话', '收货日期'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'TakeID', index: 'TakeID', width: 130 },
        { name: 'UnitName', index: 'UnitName', width: 150 },
        { name: 'DeliverName', index: 'DeliverName', width: 100 },
        { name: 'DeliverTel', index: 'DeliverTel', width: 100 },
        { name: 'DeliverDate', index: 'DeliverDate', width: 150 },
        { name: 'ReceiveName', index: 'ReceiveName', width: 100 },
        { name: 'ReceiveTel', index: 'ReceiveTel', width: 100 },
        { name: 'ReceiveDate', index: 'ReceiveDate', width: 150 }

        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).TakeID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            reload1();
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 155, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function selChange(rowid) {
    if ($('input[id=c' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSelID != 0) {
            $('input[id=c' + oldSelID + ']').prop("checked", false);
        }
        $('input[id=c' + rowid + ']').prop("checked", true);
        $("#list").setSelection(rowid)
    }
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadDeliveryList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            TakeID: $("#TakeID").val(), UnitName: $("#UnitName").val(),
            SDeliverDate: $("#SDeliverDate").val(), EDeliverDate: $("#EDeliverDate").val(),
            ReceiveName: $("#ReceiveName").val(), SReceiveDate: $("#SReceiveDate").val(),
            EReceiveDate: $("#EReceiveDate").val()
        },
    }).trigger("reloadGrid");
}

// 详细信息
function jq1() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).TakeID;
    }
    jQuery("#list1").jqGrid({
        url: 'LoadDetailList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, TakeID: rID
        },
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
        colNames: ['登记卡号', '仪表编号', '仪表名称', '仪表型号', '口径', '客户地址', '随表附件','外观检查项'],
        colModel: [
        { name: 'RID', index: 'RID', width: 120 },
        { name: 'MeterID', index: 'MeterID', width: 120 },
        { name: 'MeterName', index: 'MeterName', width: 120 },
        { name: 'Model', index: 'Model', width: 90 },
        { name: 'Caliber', index: 'Caliber', width: 100 },
        { name: 'CustomerAddr', index: 'CustomerAddr', width: 180 },
        { name: 'Files', index: 'Files', width: 100 },
        { name: 'FaceText', index: 'FaceText', width: 250 }

        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '详细信息',

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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 155, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 20, false);
        }
    });
}

function reload1() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).TakeID;
    }
    $("#list1").jqGrid('setGridParam', {
        url: 'LoadDetailList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, TakeID: rID
        },

    }).trigger("reloadGrid");
}
