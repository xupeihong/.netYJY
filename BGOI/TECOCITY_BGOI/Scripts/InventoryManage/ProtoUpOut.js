var curPage = 1;
var OnePageCount = 6;
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
    // 打印
    $("#btnPrint").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).ListOutID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).ListOutID;
            var url = "PrintProtoUpOut?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
    // 打印发货单
    $("#btnPrint2").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).ListOutID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).ListOutID;
            var url = "PrintBasicOutInvoiceList?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    $("#Fin").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {

            var ListOutID = Model.ListOutID;
            $.ajax({
                type: "POST",
                url: "UpOutDateState",
                data: { ListOutID: ListOutID },
                success: function (data) {
                    alert(data.Msg);
                    reload();
                },
                dataType: 'json'
            });
            reload1("");
        }
    });
})

function AddProtoUpOut() {

    window.parent.parent.OpenDialog("新增出库单", "../InventoryManage/AddProtoUpOut", 800, 450);

}

function jq() {
    var ListOutID = $('#ListOutID').val();
    var HouseID = $('#HouseID').val();

    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $("input[name='State']:checked").val();


    jQuery("#list").jqGrid({
        url: 'ProtoUpOutList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ListOutID: ListOutID, HouseID: HouseID, Begin: Begin, End: End, State: State },
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
        colNames: ['','序号', '出库单号', '科目', '出库时间', '经手人', '总金额', '所属仓库', '备注', '状态'],
        colModel: [
            { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'ListOutID', index: 'ListOutID', width: 120, align: "center" },
        { name: 'SubjectName', index: 'SubjectName', width: 150, align: "center" },
        { name: 'ProOutTime', index: 'ProOutTime', width: 100, align: "center" },
        { name: 'ProOutUser', index: 'ProOutUser', width: 100, align: "center" },
        { name: 'Amount', index: 'Amount', width: 80, align: "center" },
        { name: 'HouseName', index: 'HouseName', width: 80, align: "center" },
        { name: 'Remark', index: 'Remark', width: 80, align: "center" },
        { name: 'State', index: 'State', width: 80, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        multiselect: true,
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
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

            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {

                return;
            }
            else {

                var ListOutID = Model.ListOutID;
                reload1(ListOutID);

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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    var ListOutID = $('#ListOutID').val();
    var HouseID = $('#HouseID').val();

    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $("input[name='State']:checked").val();

    $("#list").jqGrid('setGridParam', {
        url: 'ProtoUpOutList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ListOutID: ListOutID, HouseID: HouseID, Begin: Begin, End: End, State: State },

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

    var ListOutID = $('#ListOutID').val();
    var HouseID = $('#HouseID').val();

    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $("input[name='State']:checked").val();
    if (State == "1") {
        $('#Fin').hide();
    } else {
        $('#Fin').show();
    }

    $("#list").jqGrid('setGridParam', {
        url: 'ProtoUpOutList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ListOutID: ListOutID, HouseID: HouseID, Begin: Begin, End: End, State: State },
        loadonce: false

    }).trigger("reloadGrid");//重新载入

}

function jq1(ListOutID) {

    jQuery("#list1").jqGrid({
        url: 'StockOutDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, ListOutID: ListOutID },
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
        colNames: ['序号', '产品编号', '产品名称', '规格型号', '单位', '申请数量','实际数量', '单价', '厂家','批号', '备注', '上样机编号'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'ProductID', index: 'PIDame', width: 100, align: "center" },
        { name: 'ProName', index: 'ProName', width: 120, align: "center" },
        { name: 'Spec', index: 'Spec', width: 120, align: "center" },
        { name: 'Units', index: 'Units', width: 80, align: "center" },
        { name: 'StockOutCount', index: 'StockOutCount', width: 80, align: "center" },
        { name: 'StockOutCountActual', index: 'StockOutCountActual', width: 80, align: "center" },
        { name: 'UnitPrice', index: 'UnitPrice', width: 80, align: "center" },
        { name: 'Manufacturer', index: 'Manufacturer', width: 120, align: "center" },
        { name: 'BatchID', index: 'BatchID', width: 120, align: "center" },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" },
        { name: 'OrderID', index: 'OrderID', width: 120, align: "center" }
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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}

function reload1(ListOutID) {

    var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'StockOutDetialList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount1, ListOutID: ListOutID },

    }).trigger("reloadGrid");
}
function FX() {
    var str = "";
    var rowid = $("#list").jqGrid('getGridParam', 'selarrrow');
    for (var i = 0; i < rowid.length; i++) {
        var m = jQuery("#list").jqGrid('getRowData', rowid[i]).ListOutID;
        str += "'" + m + "',";
    }
    $("#ListOutIDN").val(str);
}


