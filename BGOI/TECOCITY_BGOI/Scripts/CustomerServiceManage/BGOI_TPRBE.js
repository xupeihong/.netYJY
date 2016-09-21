var curPage = 1;
var OnePageCount = 15;
var TaskName;
var ProjectNum;
var ProjectNum;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $("#QD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择项目");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ProjectNum;
        var one = confirm("确定选择编号为 " + texts + "的项目吗")
        if (one == false)
            return;
        else {
            window.parent.document.getElementById("StrPID").value = texts;
            alert("选择项目成功");
            parent.ClosePop();
        }
    })
})
function reload() {
    TaskName = $('#TaskName').val();
    ProjectNum = $('#ProjectNum').val();
    $("#list").jqGrid('setGridParam', {
        url: 'EntrustGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, taskname: TaskName, projectNum: ProjectNum },

    }).trigger("reloadGrid");
}

function jq() {
    TaskName = $('#TaskName').val();
    ProjectNum = $('#ProjectNum').val();
    jQuery("#list").jqGrid({
        url: 'EntrustGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, taskname: TaskName, projectNum: ProjectNum },
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
        colNames: ['', '委托单编号', '工程名称', '工程编号', '工程地点', '图纸编号', '交底时间', '项目状态', 'State', 'StartAEnd', 'WorkUnit', 'WorkUnitDesc'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'TaskID', index: 'TaskID', width: 100 },
        { name: 'TaskName', index: 'TaskName', width: 150 },
        { name: 'ProjectNum', index: 'ProjectNum', width: 100 },
        { name: 'TaskPlace', index: 'TaskPlace', width: 150 },
        { name: 'DrawingNum', index: 'DrawingNum', width: 100 },
        { name: 'TellsbTime', index: 'TellsbTime', width: 150 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 },
        { name: 'State', index: 'State', width: 50, hidden: true },
        { name: 'StartAEnd', index: 'StartAEnd', width: 50, hidden: true },
        { name: 'WorkUnit', index: 'WorkUnit', width: 50, hidden: true },
        { name: 'WorkUnitDesc', index: 'WorkUnitDesc', width: 50, hidden: true },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '委托单表',

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

