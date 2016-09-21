var curPage = 1;
var OnePageCount = 20;
var oldSelID = 0;
var oldSelID2 = 0;
var isConfirm = false;

$(document).ready(function () {
    $("#pageContent").height($(window).height());

    jq();// 加载组
    jq2();// 加载人员

    // 选择完成 下发任务
    $('#QD').click(function () {
        isConfirm = confirm("确定要下发任务吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            var cbval = "";
            var cbval2 = "";
            $('input[name=cb]:checked').each(function () {
                cbval += $(this).val() + ",";
            });
            $('input[name=cb2]:checked').each(function () {
                cbval2 += $(this).val() + ",";
            });
            cbval = cbval.substr(0, cbval.length - 1);
            cbval2 = cbval2.substr(0, cbval2.length - 1);
            var texts = cbval + "&" + cbval2;
            if (texts == "" || texts == null) {
                alert("您还没有选择人员")
                return;
            }
            $("#SaveUser").val(texts);
            submitInfo();
        }
    })

})

// 界面提交
function submitInfo() {
    var options = {
        url: "SendTask",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 10);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

function returnConfirm() {
    return false;
}

//
function reload() {

    $("#list1").jqGrid('setGridParam', {
        url: 'getGroupList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },

    }).trigger("reloadGrid");
}

function jq() {

    jQuery("#list1").jqGrid({
        url: 'getGroupList',
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
        colNames: ['', '小组编号', '小组名称'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'GroupID', index: 'GroupID', width: 100 },
        { name: 'GroupName', index: 'GroupName', width: 150 }

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
                var curChk = "<input id='c" + id + "' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).GroupID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            reload2();
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
                curPage = $("#pager1 :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
            $("#list1").jqGrid("setGridWidth", $("#tabGroup").width() - 10, false);
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
        $("#list1").setSelection(rowid)
    }
}

function reload2() {
    var rowid = $("#list1").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list1").jqGrid('getRowData', rowid).GroupID;
    }
    $("#list2").jqGrid('setGridParam', {
        url: 'getPersonList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, GroupID: rID },
    }).trigger("reloadGrid");
}

function jq2() {
    var rowid = $("#list1").jqGrid('getGridParam', 'selrow');
    var rID = "";
    if (rowid != null) {
        rID = jQuery("#list1").jqGrid('getRowData', rowid).GroupID;
    }
    jQuery("#list2").jqGrid({
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
        colNames: ['', '人员编号', '人员姓名'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'UserID', index: 'UserID', width: 100 },
        { name: 'UserName', index: 'UserName', width: 150 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        gridComplete: function () {
            var ids = jQuery("#list2").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list2").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' type='checkbox' value='" + jQuery("#list2").jqGrid('getRowData', id).UserID + "' name='cb2'/>";
                jQuery("#list2").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage == $("#list2").getGridParam("lastpage"))
                    return;
                curPage = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage == 1)
                    return;
                curPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
            $("#list2").jqGrid("setGridWidth", $("#tabPerson").width() - 10, false);
        }
    });
}
