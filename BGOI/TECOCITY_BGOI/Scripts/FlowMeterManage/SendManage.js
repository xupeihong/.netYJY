
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    // 修改 完成
    $("#XGFHD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).DeliverID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).DeliverID + "@" + "iframe";
            window.parent.OpenDialog("修改收货单", "../FlowMeterManage/EditSendDelivery?Info=" + escape(texts), 600, 450, '');
        }
    });

    // 撤销 完成
    $("#CXFHD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).DeliverID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).DeliverID + "@" + "iframe";
            window.parent.OpenDialog("撤销发货单", "../FlowMeterManage/DelSendDelivery?Info=" + escape(texts), 700, 550, '');
        }
    });

    // 打印发货确认单 完成
    $("#DYQRD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).DeliverID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).DeliverID + "@" + "iframe";
            var url = "../FlowMeterManage/SendDeliveryDtail?Info=" + escape(texts);
            //window.parent.OpenDialog("查看发货确认单", "../FlowMeterManage/SendDeliveryDtail?Info=" + escape(texts), 800, 700, '');
            window.showModalDialog(url, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    jq();// 加载发货单列表 
    jq1();// 加载发货单仪表列表

});

//
function jq() {
    jQuery("#list").jqGrid({
        url: 'LoadSendList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            DeliverID: $("#DeliverID").val(), UnitName: $("#UnitName").val(),
            SReceiveDate: $("#SReceiveDate").val(), EReceiveDate: $("#EReceiveDate").val(),
            SSendDate: $("#SSendDate").val(), ESendDate: $("#ESendDate").val()
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
        colNames: ['', '发货单号', '货运单位', '收货人', '联系电话', '收货日期', '目的地', '发货人', '联系方式','发货日期'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'DeliverID', index: 'DeliverID', width: 120 },
        { name: 'UnitName', index: 'UnitName', width: 150 },
        { name: 'ReceiveName', index: 'ReceiveName', width: 100 },
        { name: 'ReceiveTel', index: 'ReceiveTel', width: 100 },
        { name: 'ReceiveDate', index: 'ReceiveDate', width: 120 },
        { name: 'ReceiveAddr', index: 'ReceiveAddr', width: 200 },
        { name: 'SendName', index: 'SendName', width: 100 },
        { name: 'SendTel', index: 'SendTel', width: 100 },
        { name: 'SendDate', index: 'SendDate', width: 120 }

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
        url: 'LoadSendList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            DeliverID: $("#DeliverID").val(), UnitName: $("#UnitName").val(),
            SReceiveDate: $("#SReceiveDate").val(), EReceiveDate: $("#EReceiveDate").val(),
            SSendDate: $("#SSendDate").val(), ESendDate: $("#ESendDate").val()
        },
    }).trigger("reloadGrid");
}

// 详细信息
function jq1() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).DeliverID;
    }
    jQuery("#list1").jqGrid({
        url: 'LoadSendDetail',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, DeliverID: rID
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
        colNames: ['登记卡号', '仪表编号', '仪表名称', '仪表型号', '口径', '客户地址', '随表附件', '外观检查项'],
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
        rID = jQuery("#list").jqGrid('getRowData', rowid).DeliverID;
    }
    $("#list1").jqGrid('setGridParam', {
        url: 'LoadSendDetail',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, DeliverID: rID
        },

    }).trigger("reloadGrid");
}
