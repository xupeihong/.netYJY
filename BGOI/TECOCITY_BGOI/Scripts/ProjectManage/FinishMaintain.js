
var curPage = 1;
var OnePageCount = 15;
var curPage1 = 1;
var OnePageCount1 = 15;
var curPage2 = 1;
var OnePageCount2 = 15;
var curPage3 = 1;
var OnePageCount3 = 15;
var Type = "工程项目";
var PID;
var ProID;
var ProID;
var Pname;
var StartDate;
var EndDate;
var Principal;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    $("#ProCompletions").width($("#search").width());
    $("#ProCompletions").height($("#pageContent").height() / 2 - 75);
    $("#Finish").width($("#search").width());
    $("#Finish").height($("#pageContent").height() / 2 - 75);
    $("#UserLog").width($("#search").width());
    $("#UserLog").height($("#pageContent").height() / 2 - 75);
    jq();
    jq1();
    jq2();
    jq3();

    $('#JGYSQdiv').click(function () {
        this.className = "btnTw";
        $('#XMJXdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");

        $("#ProCompletions").css("display", "");
        $("#Finish").css("display", "none");
        $("#UserLog").css("display", "none");
        reload1();
    })

    $('#XMJXdiv').click(function () {
        this.className = "btnTw";
        $('#JGYSQdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");

        $("#Finish").css("display", "");
        $("#ProCompletions").css("display", "none");
        $("#UserLog").css("display", "none");
        reload2();
    })

    $('#CZRZdiv').click(function () {
        this.className = "btnTw";
        $('#JGYSQdiv').attr("class", "btnTh");
        $('#XMJXdiv').attr("class", "btnTh");

        $("#UserLog").css("display", "");
        $("#ProCompletions").css("display", "none");
        $("#Finish").css("display", "none");
        reload3();
    })

    $('#JGYS').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行竣工验收的项目单");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State < "3") {
                alert("该项目还没有立项审批，不能进行投标操作");
                return;
            }
            var IsContract = jQuery("#list").jqGrid('getRowData', rowid).IsContract;
            if (IsContract == "有") {
                var ConState = jQuery("#list").jqGrid('getRowData', rowid).ConState;
                if (ConState < 2) {
                    alert("该项目合同还没有通过审批，不能进行操作");
                    return;
                }
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID + "@" + jQuery("#list").jqGrid('getRowData', rowid).Pname;
            window.parent.OpenDialog("竣工验收", "../ProjectManage/addProCompletions?id=" + texts, 600, 400, '');
        }
    })

    $('#XMJX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行结项的项目单");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State < "3") {
                alert("该项目还没有立项审批，不能进行投标操作");
                return;
            }
            if (State == "8") {
                alert("该项目已结项，不能重复操作");
                return;
            }
            var IsContract = jQuery("#list").jqGrid('getRowData', rowid).IsContract;
            if (IsContract == "有") {
                var ConState = jQuery("#list").jqGrid('getRowData', rowid).ConState;
                if (ConState < 2) {
                    alert("该项目合同还没有通过审批，不能进行操作");
                    return;
                }
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID + "@" + jQuery("#list").jqGrid('getRowData', rowid).Pname + "@" + jQuery("#list").jqGrid('getRowData', rowid).CID;
            window.parent.OpenDialog("项目结项", "../ProjectManage/addProFinish?id=" + texts, 700, 570, '');
        }
    })
})

function reload() {
    if ($('.field-validation-error').length == 0) {
        ProID = $('#ProID').val();
        Pname = $('#Pname').val();
        StartDate = $('#StartDate').val();
        EndDate = $('#EndDate').val();
        Principal = $('#Principal').val();
        $("#list").jqGrid('setGridParam', {
            url: 'FinishMaintainGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, ProID: ProID, Pname: Pname, StartDate: StartDate, EndDate: EndDate, principal: Principal },

        }).trigger("reloadGrid");
    }
}

function jq() {
    ProID = $('#ProID').val();
    Pname = $('#Pname').val();
    StartDate = $('#StartDate').val();
    EndDate = $('#EndDate').val();
    Principal = $('#Principal').val();
    jQuery("#list").jqGrid({
        url: 'FinishMaintainGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProID: ProID, Pname: Pname, StartDate: StartDate, EndDate: EndDate, principal: Principal },
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
        colNames: ['', '项目编号', 'PID', '内部编号', '项目名称', '客户名称', '项目来源', '项目目标', '项目概述', '项目负责人', '立项时间', '是否设计', '是否报价', '是否预算', '有无合同', '回款情况', '所属单位', '项目状态', 'State', 'CID', 'ConState', '合同状态'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PIDShow', index: 'PIDShow', width: 120 },
        { name: 'PID', index: 'PID', width: 120, hidden: true },
        { name: 'ProID', index: 'ProID', width: 100 },
        { name: 'Pname', index: 'Pname', width: $("#bor").width() - 900 },
        { name: 'CustomerName', index: 'CustomerName', width: 100, hidden: true },
        { name: 'PsourceDesc', index: 'PsourceDesc', width: 70, hidden: true },
        { name: 'Goal', index: 'Goal', width: 150, hidden: true },
        { name: 'MainContent', index: 'MainContent', width: 150, hidden: true },
        { name: 'Principal', index: 'Principal', width: 70 },
        { name: 'AppDate', index: 'AppDate', width: 100 },
        { name: 'IsDesign', index: 'IsDesign', width: 60 },
        { name: 'IsPrice', index: 'IsPrice', width: 60 },
        { name: 'IsBudget', index: 'IsBudget', width: 60 },
        { name: 'IsContract', index: 'IsContract', width: 60 },
        { name: 'IsCBack', index: 'IsCBack', width: 70 },
        { name: 'DeptName', index: 'DeptName', width: 100 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 },
        { name: 'State', index: 'State', width: 50, hidden: true },
        { name: 'CID', index: 'CID', width: 50, hidden: true },
        { name: 'ConState', index: 'ConState', width: 50, hidden: true },
        { name: 'ConStateDesc', index: 'ConStateDesc', width: 100 },
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
                var str = "<a href='#' style='color:blue' onclick='ShowDetail(\"" + curRowData.PIDShow + "\")' >" + curRowData.PIDShow + "</a>";

                jQuery("#list").jqGrid('setRowData', ids[i], { PIDShow: str });
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function ShowDetail(id) {
    window.parent.OpenDialog("详细内容", "../ProjectManage/DetailApp?id=" + id, 700, 500, '');
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
    ProID = jQuery("#list").jqGrid('getRowData', rowid).ProID;
    reload1();
    reload2();
    reload3();
}



function reload1() {
    //JQtype = $("#JQtype").val();
    $("#list1").jqGrid('setGridParam', {
        url: 'ProCompletionsGrid',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, pid: PID }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

function jq1() {
    //JQtype = $("#JQtype").val();
    jQuery("#list1").jqGrid({
        url: 'ProCompletionsGrid',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, pid: PID }, //, jqType: JQtype
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
        colNames: ['ID', '项目编号', '竣工日期', '验收人','验收说明'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'ID', index: 'ID', width: 120, hidden: true },
        { name: 'PID', index: 'PID', width: 150 },
        { name: 'CompleteDate', index: 'CompleteDate', width: 200 },
        { name: 'CompletePerson', index: 'CompletePerson', width: 150},
        { name: 'CompleteRmark', index: 'CompleteRmark', width: $("#bor").width() - 680 },
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '项目竣工验收表',

        //gridComplete: function () {
        //    var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list1").jqGrid('getRowData', id);
        //        var curChk = "<input id='q" + id + "' onclick='selChangeq(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
        //        jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    }

        //},
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=q' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=q' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}


function reload2() {
    //JQtype = $("#JQtype").val();
    $("#list2").jqGrid('setGridParam', {
        url: 'ProFinishGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, pid: PID },

    }).trigger("reloadGrid");
}

function jq2() {
    jQuery("#list2").jqGrid({
        url: 'ProFinishGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, pid: PID },
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
        colNames: [ '项目编号', '结项日期', '是否欠款', '欠款金额','欠款原因','备注'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'FinishDate', index: 'FinishDate', width: 150 },
        { name: 'IsDebt', index: 'IsDebt', width: 100 },
        { name: 'DebtAmount', index: 'DebtAmount', width: 100 },
        { name: 'DebtReason', index: 'DebtReason', width:  $("#bor").width() - 700 },
        { name: 'Remark', index: 'Remark', width: 100 },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '项目结项表',

        //gridComplete: function () {
        //    var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list1").jqGrid('getRowData', id);
        //        var curChk = "<input id='q" + id + "' onclick='selChangeq(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
        //        jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    }

        //},
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=q' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=q' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

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
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}


function reload3() {
    //JQtype = $("#JQtype").val();
    $("#list3").jqGrid('setGridParam', {
        url: 'UserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage3, rownum: OnePageCount3, pid: PID, proid: ProID },

    }).trigger("reloadGrid");
}

function jq3() {
    jQuery("#list3").jqGrid({
        url: 'UserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage3, rownum: OnePageCount3, pid: PID, proid: ProID },
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
        colNames: ['项目编号', '操作内容', '操作结果', '操作时间', '操作人'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RelevanceID', index: 'RelevanceID', width: 120 },
        { name: 'LogTitle', index: 'LogTitle', width: 200 },
        { name: 'LogContent', index: 'LogContent', width: $("#bor").width() - 700 },
        { name: 'LogTime', index: 'LogTime', width: 150 },
        { name: 'LogPerson', index: 'LogPerson', width: 70 }
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '操作日志表',

        //gridComplete: function () {
        //    var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list1").jqGrid('getRowData', id);
        //        var curChk = "<input id='q" + id + "' onclick='selChangeq(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
        //        jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    }

        //},
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=q' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=q' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (curPage == $("#list3").getGridParam("lastpage"))
                    return;
                curPage = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPage = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPage == 1)
                    return;
                curPage = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPage = 1;
            }
            else {
                curPage = $("#pager3 :input").val();
            }
            reload3();
        },
        loadComplete: function () {
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

