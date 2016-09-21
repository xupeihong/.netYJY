
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    // 新增缴费记录 完成
    $("#XZJFJL").click(function () {
        window.parent.OpenDialog("新增缴费记录", "../FlowMeterManage/AddPayment", 600, 600, '');
    });

    // 修改缴费记录 完成
    $("#XGJFD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).PayID;
        if (rowid == null) {
            alert("请在列表中选择一条缴费记录");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PayID + "@" + "iframe";
            window.parent.OpenDialog("修改缴费单", "../FlowMeterManage/EditPayment?Info=" + escape(texts), 700, 600, '');
        }
    });

    // 撤销缴费记录 完成
    $("#CXJFD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).PayID;
        if (rowid == null) {
            alert("请在列表中选择一条缴费记录");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PayID + "@" + "iframe";
            window.parent.OpenDialog("撤销缴费记录单", "../FlowMeterManage/DelPayment?Info=" + escape(texts), 700, 550, '');
        }
    });

    // 查看详细 完成
    $("#CKXX").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).PayID;
        if (rowid == null) {
            alert("请在列表中选择一条缴费记录");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PayID + "@" + "iframe";
            window.parent.OpenDialog("缴费单详细", "../FlowMeterManage/PaymentDetail?Info=" + escape(texts), 500, 400, '');
        }
    });

    LoadPayList();// 缴费记录列表 
    LoadModelList();// 设备信息列表
    LoadBJDList();// 报价单列表 

    // 设备信息列表
    $('#SBXXdiv').click(function () {
        this.className = "btnTw";
        $('#BJDdiv').attr("class", "btnTh");

        $("#BJDList").css("display", "none");
        $("#SBXXList").css("display", "");
        reload1();
    })

    // 报价单列表
    $('#BJDdiv').click(function () {
        this.className = "btnTw";
        $('#SBXXdiv').attr("class", "btnTh");

        $("#BJDList").css("display", "");
        $("#SBXXList").css("display", "none");
        reload2();
    })

})

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

// 缴费记录列表
function LoadPayList() {
    jQuery("#list").jqGrid({
        url: 'LoadPayList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, PayUnit: $("#PayUnit").val()
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
        colNames: ['', '缴费单号', '缴费金额', '缴费单位', '缴费人', '缴费时间', '备注'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PayID', index: 'PayID', width: 120 },
        { name: 'PayMount', index: 'PayMount', width: 120 },
        { name: 'PayUnit', index: 'PayUnit', width: 200 },
        { name: 'PayPerson', index: 'PayPerson', width: 150 },
        { name: 'PayDate', index: 'PayDate', width: 150 },
        { name: 'Comments', index: 'Comments', width: 200 }

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
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PayID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            select();
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 130, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadPayList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, PayUnit: $("#PayUnit").val()
        },
    }).trigger("reloadGrid");
}

// 设备信息列表
function LoadModelList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).PayID;
    }
    jQuery("#list1").jqGrid({
        url: 'LoadModelList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, PayID: rID
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
        colNames: ['仪器编号', '仪器名称', '证书编号', '生产厂家', '仪表型号', '出厂日期',
            '流量范围', '承压等级', '口径', '客户地址','仪表类型'],
        colModel: [
        { name: 'MeterID', index: 'MeterID', width: 120 },
        { name: 'MeterName', index: 'MeterName', width: 120 },
        { name: 'CertifID', index: 'CertifID', width: 120 },
        { name: 'Manufacturer', index: 'Manufacturer', width: 150 },
        { name: 'Model', index: 'Model', width: 100 },
        { name: 'FactoryDate', index: 'FactoryDate', width: 120 },
        { name: 'FlowRange', index: 'FlowRange', width: 100 },
        { name: 'Pressure', index: 'Pressure', width: 100 },
        { name: 'Caliber', index: 'Caliber', width: 100 },
        { name: 'CustomerAddr', index: 'CustomerAddr', width: 180 },
        { name: 'ModelType', index: 'ModelType', width: 100 }

        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 130, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload1() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).PayID;
    }
    $("#list1").jqGrid('setGridParam', {
        url: 'LoadModelList',
        datatype: 'json',
        loadonce: false,
        postData: {
            curpage: curPage, rownum: OnePageCount, PayID: rID
        },

    }).trigger("reloadGrid");
}

// 报价单列表
function LoadBJDList() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).PayID;
    }
    jQuery("#list2").jqGrid({
        url: 'LoadBJDList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, PayID: rID
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
        colNames: ['报价单号', '报价金额', '登记号','', '备注'],
        colModel: [
        { name: 'QID', index: 'QID', width: 150 },
        { name: 'ConcesioPrice', index: 'ConcesioPrice', width: 120 },
        { name: 'RID', index: 'RID', width: 150 },
        { name: 'State', index: 'State', width: 150, hidden: true },
        { name: 'Comments', index: 'Comments', width: 200 }

        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage == $("#list2").getGridParam("lastpage"))
                    return;
                curPage = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage == 1)
                    return;
                curPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 130, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload2() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).PayID;
    }
    $("#list2").jqGrid('setGridParam', {
        url: 'LoadBJDList',
        datatype: 'json',
        loadonce: false,
        postData: {
            curpage: curPage, rownum: OnePageCount, PayID: rID
        },

    }).trigger("reloadGrid");
}

function select() {
    reload1();
    reload2();
}
