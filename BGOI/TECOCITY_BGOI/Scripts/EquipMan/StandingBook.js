var curPage = 1;
var OnePageCount = 2000;
var TracingType;
var CheckCompany;
var StarTime;
var EndTime;
var OrderDate;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
})

function Towod() {
    var record = $("#list").getGridParam("reccount");
    if (record == 0) {
        alert("列表内容为空，没有要导出的数据，不能进行导出操作");
        return false;
    }
    else {
        var one = confirm("确定将列表内容导出吗？")
        if (one == false) {
            return false;
        }
        else {
            return true;
        }
    }
}

function reload() {
    TracingType = $('#TracingType').val();
    CheckCompany = $('#CheckCompany').val();
    StarTime = $('#StarTime').val();
    EndTime = $('#EndTime').val();
    $("#list").jqGrid('setGridParam', {
        url: 'StandingBookGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, starTime: StarTime, endTime: EndTime, TracingType: TracingType, CheckCompany: CheckCompany, OrderDate: OrderDate },

    }).trigger("reloadGrid");
}

function jq() {
    TracingType = $('#TracingType').val();
    CheckCompany = $('#CheckCompany').val();
    StarTime = $('#StarTime').val();
    EndTime = $('#EndTime').val();
    jQuery("#list").jqGrid({
        url: 'StandingBookGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, starTime: StarTime, endTime: EndTime, TracingType: TracingType, CheckCompany: CheckCompany, OrderDate: OrderDate },
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
        colNames: ['', "序号", '控制编号', '名称', '生产厂家', '检定单位', '出厂编号 ', '规格型号', '精度', '以检日期', '周期', '方式', '费用'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'xu', index: 'xu', width: 30 },
        { name: 'ControlCode', index: 'ControlCode', width: 70 },
        { name: 'Ename', index: 'Ename', width: 70 },
        { name: 'Manufacturer', index: 'Manufacturer', width: $("#bor").width() - 950 },
        { name: 'CheckCompany', index: 'CheckCompany', width: 100 },
        { name: 'FactoryNumber', index: 'FactoryNumber', width: 130 },
        { name: 'Specification', index: 'Specification', width: 70 },
        { name: 'Precision', index: 'Precision', width: 50 },
        { name: 'CheckDate', index: 'CheckDate', width: 120 },
        { name: 'Cycle', index: 'Cycle', width: 50 },
        { name: 'CheckWay', index: 'CheckWay', width: 100 },
        { name: 'Charge', index: 'Charge', width: 50 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '台账表',
        sortable: true,
        optionloadonce: true,
        sortname: 'CheckDate',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).TaskID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        },
        onSortCol: function (index, iCol, sortorder) {
            OrderDate = index + "@" + sortorder;
            $("#Order").val(OrderDate);
            reload();
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