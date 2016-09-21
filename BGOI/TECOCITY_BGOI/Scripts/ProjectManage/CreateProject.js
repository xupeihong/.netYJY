
var curPage = 1;
var OnePageCount = 15;
var curPage1 = 1;
var OnePageCount1 = 15;
var Type = "新建项目";
var PID;
var ProID;
var Pname;
var StartDate;
var EndDate;
var oldSelID = 0;
var newSelID = 0;

var objData = '';

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    //jqO();
    jq();
    jq1();

    $('#XJ').click(function () {
        window.parent.OpenDialog("新建项目", "../ProjectManage/CreateNewProject", 600, 500, '');
    })

    $('#XGXM').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行修改内容的项目单");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
            window.parent.OpenDialog("修改项目内容", "../ProjectManage/UpdateProjectBas?id=" + texts, 600, 500, '');
        }
    })

    $('#CXXM').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要撤销的项目");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var one = confirm("是否撤销编号为 " + texts + " 的项目吗");
            if (one == false)
                return;
            else {
                $.ajax({
                    url: "deleteBas",
                    type: "post",
                    data: { data1: texts },
                    dataType: "Json",
                    success: function (data) {
                        if (data.success == "true") {
                            alert(data.Msg);
                            PID = null;
                            reload();
                            reload1();
                        }
                        else {
                            alert(data.Msg);
                            return;
                        }
                    }
                });
            }
        }
    })
})

function search()
{
    PID = null;
    reload();
    reload1();
}

function reload() {
    if ($('.field-validation-error').length == 0) {
        ProID = $('#ProID').val();
        Pname = $('#Pname').val();
        StartDate = $('#StartDate').val();
        EndDate = $('#EndDate').val();
        $("#list").jqGrid('setGridParam', {
            url: 'ProjectGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, ProID: ProID, Pname: Pname, StartDate: StartDate, EndDate: EndDate },

        }).trigger("reloadGrid");
    }
}

function jq() {
    ProID = $('#ProID').val();
    Pname = $('#Pname').val();
    StartDate = $('#StartDate').val();
    EndDate = $('#EndDate').val();
    jQuery("#list").jqGrid({
        url: 'ProjectGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProID: ProID, Pname: Pname, StartDate: StartDate, EndDate: EndDate },
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
        colNames: ['', '项目编号', '内部编号', '项目名称', '客户名称', '项目来源', '客户名称', '项目目标', '项目主要内容', '创建时间', '所属单位','项目状态', 'State'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'ProID', index: 'ProID', width: 100 },
        { name: 'Pname', index: 'Pname', width: 150 },
        { name: 'CustomerName', index: 'CustomerName', width: 100 },
        { name: 'PsourceDesc', index: 'PsourceDesc', width: 70 },
        { name: 'CustomerName', index: 'CustomerName', width: 150 },
        { name: 'Goal', index: 'Goal', width: 150, hidden: true },
        { name: 'MainContent', index: 'MainContent', width: $("#bor").width() - 900 },
        { name: 'CreateTime', index: 'CreateTime', width: 150 },
        { name: 'DeptName', index: 'DeptName', width: 100 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 },
        { name: 'State', index: 'State', width: 50, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '项目表',

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
            select(rowid);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
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

function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    reload1();
}


function reload1() {
    $("#list1").jqGrid('setGridParam', {
        url: 'CreateProjectUserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, PID:PID,Type:Type },

    }).trigger("reloadGrid");
}

function jq1() {
    jQuery("#list1").jqGrid({
        url: 'CreateProjectUserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, PID: PID, Type: Type },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                //alert(obj);
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
        colNames: ['', '项目编号', '操作内容', '操作结果', '操作时间', '操作人'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RelevanceID', index: 'RelevanceID', width: 120 },
        { name: 'LogTitle', index: 'LogTitle', width: 200 },
        { name: 'LogContent', index: 'LogContent', width: $("#bor").width() - 700 },
        { name: 'LogTime', index: 'LogTime', width: 150 },
        { name: 'LogPerson', index: 'LogPerson', width: 70 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '操作日志记录表',

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
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
