
$(document).ready(function () {
    //$("#RecordDate").val("");
    //$("#CreateTime").val("");
    LoadDetail();
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});


function returnConfirm() {
    //  return isConfirm;
}
var curpage = 1;
var OnePageCount = 3;
var PID = 0;
function LoadDetail() {
    PID = $("#PID").val();
    jQuery("#myTable").jqGrid({
        url: 'GetDetailGrid',
        datatype: 'json',
        postData: { ID: PID, curpage: curpage, rownum: OnePageCount },
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
        //序号 　　货品编号 　　货品名称 　　规格型号 　　单位 　　数量 　　备注 
        colNames: ['', '货品编号', '物品名称', '规格型号', '单位', '数量'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'ProductID', index: 'ProductID', width: 90, hidden: true },
       // { name: 'XID', index: 'XID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'Amount', index: 'Amount', width: 80 }

        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
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
                if (curpage == $("#myTable").getGridParam("lastpage"))
                    return;
                curpage = $("#myTable").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curpage = $("#myTable").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curpage == 1)
                    return;
                curpage = $("#myTable").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curpage = 1;
            }
            else {
                curpage = $("#pager1 :input").val();
            }
            reload1()
        },
        loadComplete: function () {
            $("#myTable").jqGrid("setGridHeight", 100, false);
            $("#myTable").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload1()
{
    $("#myTable").jqGrid('setGridParam', {
        url: 'GetDetailGrid',
        datatype: 'json',
        postData: {
            ID: PID, curpage: curpage, rownum: OnePageCount
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}











function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
