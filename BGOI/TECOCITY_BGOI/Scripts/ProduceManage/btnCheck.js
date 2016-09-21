var curPage = 1;
var OnePageCount = 10;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;


$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#searchOut").width($("#bor").width() - 30);
    jq("");

    $("#btnSave").click(function () {
        var M = "";
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var EID = jQuery("#list").jqGrid('getRowData', rowid).EID;
        if (rowid == null) {
            alert("请选择一行数据");
            return;
        }
        else {
            window.parent.LoadExCheck(EID);
            window.parent.ClosePop();

        }


    })

})

function reload() {
    var EID = $('#EID').val();

    $("#list").jqGrid('setGridParam', {
        url: 'getCHECK',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, EID: EID },

    }).trigger("reloadGrid");
}

function jq() {
    var EID = $('#EID').val();

    jQuery("#list").jqGrid({
        url: 'getCHECK',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, EID: EID },
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
        colNames: ['订单编号', '合同编号', '退货单号', '退换货日期', '退货方式', '状态', '', ''],
        colModel: [
        { name: 'OrderID', index: 'OrderID', width: 200 },
        { name: 'ContractID', index: 'ContractID', width: 200 },
        { name: 'EID', index: 'EID', width: 200 },
        { name: 'ChangeDate', index: 'ChangeDate', width: 200 },
        { name: 'ReturnWay', index: 'ReturnWay', width: 100 },
        { name: 'State', index: 'State', width: 200 },
        { name: 'ISF', index: 'ISF', width: 200, hidden: true },
        { name: 'ISEXR', index: 'ISEXR', width: 200, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

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
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function SearchOut() {
    curRow = 0;
    curPage = 1;

    var EID = $('#EID').val();

    $("#list").jqGrid('setGridParam', {
        url: 'getCHECK',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, EID: EID },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}



