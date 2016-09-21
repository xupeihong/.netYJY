
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);
    LoadCheckDataList();
});

function LoadCheckDataList() {

    jQuery("#list").jqGrid({
        url: 'LoadCheckDataList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: $("#StrRID").val(), RepairType: $("#StrRepairType").val(),
            RepairMethod: $("#StrRepairMethod").val(), CheckDate: $("#StrCheckDate").val(), CheckUser: $("#StrCheckUser").val()
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
        colNames: ['登记卡号', '检测人', '检测时间', '检测类型', '检测方式', ],
        colModel: [
        { name: 'RID', index: 'RID', width: 250 },
        { name: 'CheckUser', index: 'CheckUser', width: 100 },
        { name: 'CheckDate', index: 'CheckDate', width: 100 },
        { name: 'RepairType', index: 'RepairType', width: 100 },
        { name: 'RepairMethod', index: 'RepairMethod', width: 100 },



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
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).RepairID + "' name='cb'/>";
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function reload() {
    if ($('.field-validation-error').length == 0) {
        $("#list").jqGrid('setGridParam', {
            url: 'LoadCheckDataList',
            datatype: 'json',
            postData: {
                curpage: curPage, rownum: OnePageCount,
                RID: $("#StrRID").val(), RepairType: $("#StrRepairType").val(),
                RepairMethod: $("#StrRepairMethod").val(), CheckDate: $("#StrCheckDate").val(), CheckUser: $("#StrCheckUser").val()
            },

        }).trigger("reloadGrid");
    }
}

