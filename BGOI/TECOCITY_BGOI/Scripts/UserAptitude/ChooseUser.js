var curPage = 1;
var OnePageCount = 15;
var UserName;
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
        var texts = jQuery("#list").jqGrid('getRowData', rowid).UserId;
        var name = jQuery("#list").jqGrid('getRowData', rowid).UserName;
        var one = confirm("确定选择 " + name + "吗")
        if (one == false)
            return;
        else {
            window.parent.document.getElementById("StrUserID").value = texts;
            window.parent.document.getElementById("StrUserName").value = name;
            alert("选择人员成功");
            parent.ClosePop();
        }
    })
})

function reload() {
    UserName = $('#UserName').val();
    $("#list").jqGrid('setGridParam', {
        url: 'UserGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, uaskname: UserName },

    }).trigger("reloadGrid");
}

function jq() {
    UserName = $('#UserName').val();
    TaskPlace = $('#TaskPlace').val();
    jQuery("#list").jqGrid({
        url: 'UserGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, uaskname: UserName },
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
        colNames: ['', '用户编号', '用户姓名', '单位id'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'UserId', index: 'UserId', width: 150 },
        { name: 'UserName', index: 'UserName', width: 150 },
        { name: 'DeptId', index: 'DeptId', width: 50, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '用户表',

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