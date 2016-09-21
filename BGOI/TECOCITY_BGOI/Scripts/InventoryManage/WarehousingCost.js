var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ListInID;
var curPage1 = 1;
var OnePageCount1 = 4;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
   // changcollege("");
    jq();
    var UnitIDnew = $("#UnitIDo").val();
    //if (UnitIDnew.indexOf(".49.") > 0) {
    //    var oid = 1;
    //    changcollege(oid);
    //}
})
function jq() {
    jQuery("#list").jqGrid({
        url: 'WarehousingCostList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount},
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
        colNames: [ '序号', '产品编号', '产品名称', '规格型号', '入库数量', '入库单价（税前）', '成本价','所属仓库'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'ProductID', index: 'ProductID', width: 120, align: "center" },
        { name: 'ProName', index: 'ProName', width: 120, align: "center" },
        { name: 'Spec', index: 'Spec', width: 150, align: "center" },
        { name: 's', index: 's', width: 100, align: "center" },
        { name: 'UnitPrice', index: 'UnitPrice', width: 100, align: "center" },
        { name: 'CBJG', index: 'CBJG', width: 80, align: "center" },
        { name: 'HouseName', index: 'HouseName', width: 80, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        //multiselect: true,
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
                var ListInID = Model.ListInID;
                $("#ListInIDnew").val(ListInID);
               // reload1(ListInID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()- 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'WarehousingCostList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount},

    }).trigger("reloadGrid");
}
