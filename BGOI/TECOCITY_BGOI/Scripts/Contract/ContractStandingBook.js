var curPage = 1;
var OnePageCount = 15;
var Cname;
var ContractID;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
})

function reload() {
    if ($('.field-validation-error').length == 0) {
        Cname = $('#Cname').val();
        ContractID = $('#ContractID').val();
        $("#list").jqGrid('setGridParam', {
            url: 'ContractStandingBookGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, cname: Cname, contractID: ContractID },

        }).trigger("reloadGrid");
    }
}

function jq() {
    Cname = $('#Cname').val();
    ContractID = $('#ContractID').val();
    jQuery("#list").jqGrid({
        url: 'ContractStandingBookGrid',
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
        colNames: ['序号', '合同编号', '合同名称', '内容', '签署日期', '甲方单位', '乙方单位', '合同金额', '页数', '经办人', '备注'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 30 },
        { name: 'ContractID', index: 'ContractID', width: 70 },
        { name: 'Cname', index: 'Cname', width: $("#bor").width() - 950 },
        { name: 'ContractContent', index: 'ContractContent', width: 150 },
        { name: 'Ctime', index: 'Ctime', width: 80 },
        { name: 'PartyA', index: 'PartyA', width: 120 },
        { name: 'PartyB', index: 'PartyB', width: 120 },
        { name: 'Amount', index: 'Amount', width: 70 },
        { name: 'PageCount', index: 'PageCount', width: 70 },
        { name: 'Principal', index: 'Principal', width: 50 },
        { name: 'Rmark', index: 'Rmark', width: 120 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '合同台账表',

        gridComplete: function () {

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