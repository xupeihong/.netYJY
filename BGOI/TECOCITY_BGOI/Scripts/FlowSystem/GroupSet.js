
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    jq();// 组
    jq1();// 组员

    // 新增组
    $('#XZGroup').click(function () {
        window.parent.OpenDialog("新增组", "../FlowSystem/AddGroup", 400, 200, '');
    })

    // 新增组员
    $('#XZUser').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).GroupID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            window.parent.OpenDialog("新增组员", "../FlowSystem/AddGroupUser?Info=" + rID, 400, 200, '');
        }
    })
})

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: "getGroupList",
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
        caption: '基本信息配置表',
        loadonce: false,
        colNames: ['序号', '小组编号', '小组名称', '操作', '操作'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 90 },
        { name: 'GroupID', index: 'GroupID', width: $("#bor").width() - 250 },
        { name: 'GroupName', index: 'GroupName', width: 200 },
        { name: 'Pro', index: 'GroupID', width: 50 },
        { name: 'Prodel', index: 'GroupID', width: 50 }
        ],
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 120, false);
        },
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var GroupID;
                var modify;
                var del;
                var GroupName;
                GroupID = jQuery("#list").jqGrid('getRowData', id).GroupID;
                GroupName = jQuery("#list").jqGrid('getRowData', id).GroupName;
                modify = "<a  href='#' onclick='UpdateType(\"" + GroupID + "\",\"" + GroupName + "\")' style='color:blue'>修改</a>";
                del = "<a href='#' style='color:#f60' onclick='DeleteType(" + GroupID + ")' >删除</a>";
                jQuery("#list").jqGrid('setRowData', ids[i], { Pro: modify, Prodel: del });
            }
        }
    }).trigger("reloadGrid");
}

function jq() {
    jQuery("#list").jqGrid({
        url: "getGroupList",
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '小组编号', '小组名称', '操作', '操作'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 90 },
        { name: 'GroupID', index: 'GroupID', width: 100 },
        { name: 'GroupName', index: 'GroupName', width: 200 },
        { name: 'Pro', index: 'GroupID', width: 50 },
        { name: 'Prodel', index: 'GroupID', width: 50 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '基本信息配置表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var GroupID;
                var modify;
                var del;
                var GroupName;
                GroupID = jQuery("#list").jqGrid('getRowData', id).GroupID;
                GroupName = jQuery("#list").jqGrid('getRowData', id).GroupName;
                modify = "<a  href='#' onclick='UpdateType(\"" + GroupID + "\",\"" + GroupName + "\")' style='color:blue'>修改</a>";
                del = "<a href='#' style='color:#f60' onclick='DeleteType(" + GroupID + ")' >删除</a>";
                jQuery("#list").jqGrid('setRowData', ids[i], { Pro: modify, Prodel: del });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            reload1();
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 120, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function UpdateType(gid, text) {
    window.parent.OpenDialog("修改内容", "../FlowSystem/UpdateGroup?Info=" + gid + "@" + text, 400, 200);
}

function DeleteType(id) {
    var one = confirm("确定删除这条数据吗？");
    if (one == false)
        return
    else {
        $.ajax({
            type: "POST",
            url: "DeleteGroup",
            data: { data1: id },
            success: function (data) {
                alert(data.Msg);
                reload();
            },
            dataType: 'json'
        });
    }
}

function reload1() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).GroupID;
    }
    $("#list1").jqGrid('setGridParam', {
        url: 'getPersonList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, GroupID: rID },
        colNames: ['序号', '人员编号', '人员姓名', '操作', '操作'],
        colModel: [
        { name: 'ID', index: 'ID', width: 50 },
        { name: 'UserID', index: 'UserID', width: 100 },
        { name: 'UserName', index: 'UserName', width: 200 },
        { name: 'Pro', index: 'UserID', width: 50 },
        { name: 'Prodel', index: 'UserID', width: 50 }
        ],
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 120, false);
        },
        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var UserID;
                var modify;
                var del;
                var UserName;
                UserID = jQuery("#list1").jqGrid('getRowData', id).UserID;
                UserName = jQuery("#list1").jqGrid('getRowData', id).UserName;
                modify = "<a  href='#' onclick='UpdateType1(\"" + UserID + "\",\"" + UserName + "\")' style='color:blue'>修改</a>";
                del = "<a href='#' style='color:#f60' onclick='DeleteType1(\"" + UserID + "\")' >删除</a>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { Pro: modify, Prodel: del });
            }
        }
    }).trigger("reloadGrid");
}

function jq1() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list").jqGrid('getRowData', rowid).GroupID;
    }
    jQuery("#list1").jqGrid({
        url: 'getPersonList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, GroupID: rID },
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
        colNames: ['序号', '人员编号', '人员姓名', '操作', '操作'],
        colModel: [
        { name: 'ID', index: 'ID', width: 50 },
        { name: 'UserID', index: 'UserID', width: 100 },
        { name: 'UserName', index: 'UserName', width: 200 },
        { name: 'Pro', index: 'UserID', width: 50 },
        { name: 'Prodel', index: 'UserID', width: 50 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var UserID;
                var modify;
                var del;
                var UserName;
                UserID = jQuery("#list1").jqGrid('getRowData', id).UserID;
                UserName = jQuery("#list1").jqGrid('getRowData', id).UserName;
                modify = "<a  href='#' onclick='UpdateType1(\"" + UserID + "\",\"" + UserName + "\")' style='color:blue'>修改</a>";
                del = "<a href='#' style='color:#f60' onclick='DeleteType1(\"" + UserID + "\")' >删除</a>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { Pro: modify, Prodel: del });
            }
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
                curPage = $("#pager1:input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 120, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function UpdateType1(uid, text) {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = jQuery("#list").jqGrid('getRowData', rowid).GroupID;

    window.parent.OpenDialog("修改内容", "../FlowSystem/UpdateGroupUser?Info=" + uid + "@" + text + "@" + rID, 400, 200);
}

function DeleteType1(id) {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = jQuery("#list").jqGrid('getRowData', rowid).GroupID;

    var one = confirm("确定删除这条数据吗？");
    if (one == false)
        return
    else {
        $.ajax({
            type: "POST",
            url: "DeleteGroupUser",
            data: { data1: id, data2: rID },
            success: function (data) {
                alert(data.Msg);
                reload1();
            },
            dataType: 'json'
        });
    }
}