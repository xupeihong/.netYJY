
var curPage = 1;
var OnePageCount = 15;
var Type = "工程项目";
var PID;
var ProID;
var Pname;
var start;
var end;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    //$("#search").width($("#bor").width() - 30);
    //$("#Prepare").width($("#search").width());
    //$("#Prepare").height($("#pageContent").height() / 2 - 75);
    //$("#SetUp").width($("#search").width());
    //$("#SetUp").height($("#pageContent").height() / 2 - 75);
    jq();



    $("#QD").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的项");
            return;
        }
        else {
            var id = Model.PID;
            parent.frames["iframeRight"].Getid(id);
            window.parent.ClosePop();
        }
    });
})

function loadLX() {
    $("#SetUp").html("");
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var state = jQuery("#list").jqGrid('getRowData', rowid).State;
    if (PID != null) {
        $.ajax({
            url: "LoadSetUp",
            type: "post",
            data: { data1: PID },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    var html = data.html;
                    $("#SetUp").html(html);
                }
                else {
                    return;
                }
            }
        });
    }
}

function reload() {
    if ($('.field-validation-error').length == 0) {
        ProID = $('#ProID').val();
        Pname = $('#Pname').val();
        start = $('#StartDate').val();
        end = $('#EndDate').val();
        $("#list").jqGrid('setGridParam', {
            url: 'GeneralGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, ProID: ProID, Pname: Pname, start: start, end: end },

        }).trigger("reloadGrid");
    }
}

function jq() {
    ProID = $('#ProID').val();
    Pname = $('#Pname').val();
    start = $('#StartDate').val();
    end = $('#EndDate').val();
    jQuery("#list").jqGrid({
        url: 'GeneralGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProID: ProID, Pname: Pname, start: start, end: end },
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
        colNames: ['', '项目编号', '内部编号', '项目名称', '客户名称', '项目来源', '客户名称', '项目目标', '项目概述', '创建时间', '项目状态', 'State'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'ProID', index: 'ProID', width: 100 },
        { name: 'Pname', index: 'Pname', width: 150 },
        { name: 'CustomerName', index: 'CustomerName', width: 100 },
        { name: 'PsourceDesc', index: 'PsourceDesc', width: 70 },
        { name: 'CustomerName', index: 'CustomerName', width: 150 },
        { name: 'Goal', index: 'Goal', width: 150 },
        { name: 'MainContent', index: 'MainContent', width: $("#bor").width() - 900 },
        { name: 'CreateTime', index: 'CreateTime', width: 150 },
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 250, false);
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
    loadLX();
}



function reload1() {
    //JQtype = $("#JQtype").val();
    $("#list1").jqGrid('setGridParam', {
        url: 'ProjectQQGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, pid: PID }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

