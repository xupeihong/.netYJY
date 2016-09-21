var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var newSelID = 0;
var PayOrIncome;
var Cname;
var ContractID;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $('#SZTXSJ').click(function () {
        window.parent.OpenDialog("设置提醒时间", "../Contract/SetWarnTime", 450, 200, '');
    })
})

function reload() {
    if ($('.field-validation-error').length == 0) {
        Cname = $('#Cname').val();
        ContractID = $('#ContractID').val();
        $("#list").jqGrid('setGridParam', {
            url: 'StayReturnCashGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, cname: Cname, contractID: ContractID },

        }).trigger("reloadGrid");
    }
}

function jq() {
    //PayOrIncome = $("#PayOrIncome").val();
    Cname = $('#Cname').val();
    ContractID = $('#ContractID').val();
    jQuery("#list").jqGrid({
        url: 'StayReturnCashGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, cname: Cname, contractID: ContractID },
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
        colNames: ['', '合同ID', '合同编号', '业务类型', '对应项目编号', '合同名称', '合同初始金额', '合同变更后金额', '合同签订日期', '预计完工时间', '合同签订回款次数', '已回款次数', '状态', 'State'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'CID', index: 'CID', width: 100 },
        { name: 'ContractID', index: 'ContractID', width: 70 },
        { name: 'BusinessTypeDesc', index: 'BusinessTypeDesc', width: 70 },
        { name: 'PID', index: 'PID', width: 100 },
        { name: 'Cname', index: 'Cname', width: $("#bor").width() - 1000 },
        { name: 'CBeginAmount', index: 'CBeginAmount', width: 70 },
        { name: 'CEndAmount', index: 'CEndAmount', width: 80 },
        { name: 'Ctime', index: 'Ctime', width: 80 },
        { name: 'CPlanEndTime', index: 'CPlanEndTime', width: 80 },
        { name: 'AmountNum', index: 'AmountNum', width: 100 },
        { name: 'CurAmountNum', index: 'CurAmountNum', width: 70 },
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        { name: 'State', index: 'State', width: 50, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '待回款提醒表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).CID + "' name='cb'/>";
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 220, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
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