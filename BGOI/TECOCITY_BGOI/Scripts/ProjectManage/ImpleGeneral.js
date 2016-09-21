
var curPage = 1;
var OnePageCount = 15;
var curPage1 = 1;
var OnePageCount1 = 15;
var curPage2 = 1;
var OnePageCount2 = 15;
var curPage3 = 1;
var OnePageCount3 = 15;
var curPage4 = 1;
var OnePageCount4 = 15;
var curPage5 = 1;
var OnePageCount5 = 15;
var curPage6 = 1;
var OnePageCount6 = 15;
var curPage7 = 1;
var OnePageCount7 = 15;
var curPage8 = 1;
var OnePageCount8 = 15;
var Type = "工程项目";
var PID;
var ProID;
var CID;
var Pname;
var StartDate;
var EndDate;
var Principal;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    $("#Schedule").width($("#search").width());
    $("#Schedule").height($("#pageContent").height() / 2 - 75);
    $("#SubWork").width($("#search").width());
    $("#SubWork").height($("#pageContent").height() / 2 - 75);
    $("#Purchase").width($("#search").width());
    $("#Purchase").height($("#pageContent").height() / 2 - 75);
    $("#SubPackage").width($("#search").width());
    $("#SubPackage").height($("#pageContent").height() / 2 - 75);
    $("#PayRecord").width($("#search").width());
    $("#PayRecord").height($("#pageContent").height() / 2 - 75);
    $("#Budget").width($("#search").width());
    $("#Budget").height($("#pageContent").height() / 2 - 75);
    $("#UserLog").width($("#search").width());
    $("#UserLog").height($("#pageContent").height() / 2 - 75);
    $("#Contract").width($("#search").width());
    $("#Contract").height($("#pageContent").height() / 2 - 75);
    jq();
    jq1();
    jq2();
    jq3();
    jq4();
    jq5();
    //jq6();
    jq7();
    jq8();

    $('#GCdiv').click(function () {
        this.className = "btnTw";
        $('#SGdiv').attr("class", "btnTh");
        $('#CGdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");
        $('#FYZCdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#Schedule").css("display", "");
        $("#SubWork").css("display", "none");
        $("#Purchase").css("display", "none");
        $("#SubPackage").css("display", "none");
        $("#PayRecord").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#Contract").css("display", "none");
        $("#CCashBack").css("display", "none");
        reload1();
    })

    $('#SGdiv').click(function () {
        this.className = "btnTw";
        $('#CGdiv').attr("class", "btnTh");
        $('#GCdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");
        $('#FYZCdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#SubWork").css("display", "");
        $("#Schedule").css("display", "none");
        $("#SubPackage").css("display", "none");
        $("#PayRecord").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#Contract").css("display", "none");
        $("#CCashBack").css("display", "none");
        $("#Purchase").css("display", "none");
        reload2();
    })

    $('#CGdiv').click(function () {
        this.className = "btnTw";
        $('#GCdiv').attr("class", "btnTh");
        $('#SGdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");
        $('#FYZCdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#Purchase").css("display", "");
        $("#SubWork").css("display", "none");
        $("#Schedule").css("display", "none");
        $("#SubPackage").css("display", "none");
        $("#PayRecord").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#Contract").css("display", "none");
        $("#CCashBack").css("display", "none");
        reload8();
    })

    $('#SJdiv').click(function () {
        this.className = "btnTw";
        $('#GCdiv').attr("class", "btnTh");
        $('#CGdiv').attr("class", "btnTh");
        $('#SGdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");
        $('#FYZCdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#SubPackage").css("display", "");
        $("#SubWork").css("display", "none");
        $("#Purchase").css("display", "none");
        $("#Schedule").css("display", "none");
        $("#PayRecord").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#Contract").css("display", "none");
        $("#CCashBack").css("display", "none");
        reload3();
    })

    $('#FYZCdiv').click(function () {
        this.className = "btnTw";
        $('#GCdiv').attr("class", "btnTh");
        $('#CGdiv').attr("class", "btnTh");
        $('#SGdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#PayRecord").css("display", "");
        $("#SubPackage").css("display", "none");
        $("#Purchase").css("display", "none");
        $("#SubWork").css("display", "none");
        $("#Schedule").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#Contract").css("display", "none");
        $("#CCashBack").css("display", "none");
        reload4();
    })

    $('#HKdiv').click(function () {
        this.className = "btnTw";
        $('#GCdiv').attr("class", "btnTh");
        $('#CGdiv').attr("class", "btnTh");
        $('#SGdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#FYZCdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");

        $("#CCashBack").css("display", "");
        $("#Purchase").css("display", "none");
        $("#PayRecord").css("display", "none");
        $("#SubPackage").css("display", "none");
        $("#SubWork").css("display", "none");
        $("#Schedule").css("display", "none");
        $("#Contract").css("display", "none");
        $("#UserLog").css("display", "none");
        reload7();
    })

    $('#CZRZdiv').click(function () {
        this.className = "btnTw";
        $('#GCdiv').attr("class", "btnTh");
        $('#CGdiv').attr("class", "btnTh");
        $('#SGdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");
        $('#FYZCdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#UserLog").css("display", "");
        $("#PayRecord").css("display", "none");
        $("#Purchase").css("display", "none");
        $("#SubPackage").css("display", "none");
        $("#SubWork").css("display", "none");
        $("#Schedule").css("display", "none");
        $("#Contract").css("display", "none");
        $("#CCashBack").css("display", "none");
        reload5();
    })

    $('#QDHTdiv').click(function () {
        this.className = "btnTw";
        $('#GCdiv').attr("class", "btnTh");
        $('#CGdiv').attr("class", "btnTh");
        $('#SGdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");
        $('#FYZCdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#Contract").css("display", "");
        $("#PayRecord").css("display", "none");
        $("#Purchase").css("display", "none");
        $("#SubPackage").css("display", "none");
        $("#SubWork").css("display", "none");
        $("#Schedule").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#CCashBack").css("display", "none");
        reload6();
    })

    $('#GCJL').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行过程记录的项目单");
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
            window.parent.OpenDialog("过程记录", "../ProjectManage/addSchedule?id=" + texts, 500, 350, '');
        }
    })

    $('#FBSG').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行分包施工记录的项目单");
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
            window.parent.OpenDialog("分包施工记录", "../ProjectManage/addSubWork?id=" + texts, 700, 570, '');
        }
    })

    $('#GCCG').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行工程采购的项目单");
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
            window.parent.OpenDialog("工程采购", "../ProjectManage/addPurchase?id=" + texts, 700, 470, '');
        }
    })

    $('#FBSJ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行分包设计记录的项目单");
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
            window.parent.OpenDialog("分包设计", "../ProjectManage/addSubPackage?id=" + texts, 700, 570, '');
        }
    })

    $('#FYZCJL').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行费用支出记录的项目单");
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
            window.parent.OpenDialog("费用支出记录", "../ProjectManage/addPayRecord?id=" + texts, 600, 400, '');
        }
    })

    $('#CBYS').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行成本预算的项目单");
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
            window.parent.OpenDialog("预算", "../ProjectManage/AddBudget?id=" + texts, 600, 400, '');
        }
    })

    $('#TB').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行投标的项目单");
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
            window.parent.OpenDialog("投标", "../ProjectManage/AddBidding?id=" + texts, 600, 400, '');
        }
    })

    $('#HKJL').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要进行回款记录的条目");
            return;
        }
        var State = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (State < "3") {
            alert("该项目还没有立项审批，不能进行投标操作");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
        var IsContract = jQuery("#list").jqGrid('getRowData', rowid).IsContract;
        if (IsContract == "有") {
            var ConState = jQuery("#list").jqGrid('getRowData', rowid).ConState;
            if (ConState < 2) {
                alert("该项目合同还没有通过审批，不能进行操作");
                return;
            }
        }
        var IsCBack = jQuery("#list").jqGrid('getRowData', rowid).IsCBack;
        if (IsCBack == "回款已完成") {
            alert("项目已完成回款，不能进行操作");
            return;
        }
        window.parent.OpenDialog("回款记录", "../ProjectManage/PCashBack?id=" + texts + "&type=project", 800, 600, '');
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
            url: 'ImpleGeneralGrid',
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
        url: 'ImpleGeneralGrid',
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
        colNames: ['', '项目编号', 'PID', '内部编号', '项目名称', '客户名称', '项目来源', '项目目标', '项目概述', '项目负责人', '立项时间', '是否设计', '是否报价', '是否预算', '有无合同', '回款情况', '所属单位', '项目状态', 'State', 'CID','ConState','合同状态'],
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
        { name: 'CID', index: 'CID', width: 120 },
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
    CID = jQuery("#list").jqGrid('getRowData', rowid).CID;
    reload1();
    reload2();
    reload3();
    reload4();
    reload5();
    //reload6();
    reload7();
    reload8();
}



function reload1() {
    //JQtype = $("#JQtype").val();
    $("#list1").jqGrid('setGridParam', {
        url: 'ScheduleGrid',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, pid: PID }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

function jq1() {
    //JQtype = $("#JQtype").val();
    jQuery("#list1").jqGrid({
        url: 'ScheduleGrid',
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
        colNames: ['ID', '项目编号', '进度概述', '时间'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'ID', index: 'ID', width: 120, hidden: true },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'Pview', index: 'Pview', width: $("#bor").width() - 500 },
        { name: 'CreateTime', index: 'CreateTime', width: 180 },
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '施工进度表',

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
        url: 'SubWorkGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, pid: PID },

    }).trigger("reloadGrid");
}

function jq2() {
    jQuery("#list2").jqGrid({
        url: 'SubWorkGrid',
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
        colNames: ['项目编号', '分包施工编号', '分包单位', '分包单位项目负责人', '分包费用', '预计分工施工周期', '分包施工原因', '分包施工主要内容', '是否签订安全施工协议', '状态', 'state'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'EID', index: 'EID', width: 150 },
        { name: 'SubUnit', index: 'SubUnit', width: 150 },
        { name: 'Principal', index: 'Principal', width: 100 },
        { name: 'SubPrice', index: 'SubPrice', width: 100 },
        { name: 'WorkCycle', index: 'WorkCycle', width: 100 },
        { name: 'SubWorkReason', index: 'SubWorkReason', width: 200 },
        { name: 'MainContent', index: 'MainContent', width:  $("#bor").width() - 900 },
        { name: 'IsSign', index: 'IsSign', width: 120 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 },
        { name: 'State', index: 'State', width: 100,hidden:true },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '分包施工表',

        gridComplete: function () {
            var ids = jQuery("#list2").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list2").jqGrid('getRowData', id);
                var str = "<a href='#' style='color:blue' onclick='ShowSubWork(\"" + curRowData.EID + "\")' >" + curRowData.EID + "</a>";

                jQuery("#list2").jqGrid('setRowData', ids[i], { EID: str });
            }

        },
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


function ShowSubWork(id) {
    window.parent.OpenDialog("分包施工文档下载", "../ProjectManage/DownLoadSubWork?id=" + id, 400, 200, '');
}

function reload3() {
    //JQtype = $("#JQtype").val();
    $("#list3").jqGrid('setGridParam', {
        url: 'SubPackageGrid',
        datatype: 'json',
        postData: { curpage: curPage3, rownum: OnePageCount3, pid: PID },

    }).trigger("reloadGrid");
}

function jq3() {
    jQuery("#list3").jqGrid({
        url: 'SubPackageGrid',
        datatype: 'json',
        postData: { curpage: curPage3, rownum: OnePageCount3, pid: PID },
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
        colNames: ['项目编号', '分包设计编号', '分包设计单位', '分包设计单位项目负责人', '分包设计费用', '预计设计周期', '分包设计原因', '分包设计主要内容', '状态', 'state'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'SID', index: 'SID', width: 150 },
        { name: 'DesignUnit', index: 'DesignUnit', width: 150 },
        { name: 'DesignPrincipal', index: 'DesignPrincipal', width: 100 },
        { name: 'DesignPrice', index: 'DesignPrice', width: 100 },
        { name: 'PredictCycle', index: 'PredictCycle', width: 100 },
        { name: 'SubReason', index: 'SubReason', width: 200 },
        { name: 'MainContent', index: 'MainContent', width: $("#bor").width() - 900 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 },
        { name: 'State', index: 'State', width: 100, hidden: true },
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '分包设计表',

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


function reload4() {
    //JQtype = $("#JQtype").val();
    $("#list4").jqGrid('setGridParam', {
        url: 'PayRecordGrid',
        datatype: 'json',
        postData: { curpage: curPage4, rownum: OnePageCount4, pid: PID },

    }).trigger("reloadGrid");
}

function jq4() {
    jQuery("#list4").jqGrid({
        url: 'PayRecordGrid',
        datatype: 'json',
        postData: { curpage: curPage4, rownum: OnePageCount4, pid: PID },
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
        colNames: ['项目编号', '费用支出编号', '支出类型', '支出金额', '支出日期', '支出人', '状态'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'PayID', index: 'PayID', width: 150 },
        { name: 'PayType', index: 'PayType', width: $("#bor").width() - 800 },
        { name: 'PayPrice', index: 'PayPrice', width: 120 },
        { name: 'PayDate', index: 'PayDate', width: 120 },
        { name: 'PayPerson', index: 'PayPerson', width: 120 },
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        ],
        pager: jQuery('#pager4'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '费用支出记录表',

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
            if (pgButton == "next_pager4") {
                if (curPage == $("#list4").getGridParam("lastpage"))
                    return;
                curPage = $("#list4").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager4") {
                curPage = $("#list4").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager4") {
                if (curPage == 1)
                    return;
                curPage = $("#list4").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager4") {
                curPage = 1;
            }
            else {
                curPage = $("#list4 :input").val();
            }
            reload4();
        },
        loadComplete: function () {
            $("#list4").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list4").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}


function reload5() {
    //JQtype = $("#JQtype").val();
    $("#list5").jqGrid('setGridParam', {
        url: 'UserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage5, rownum: OnePageCount5, pid: PID },

    }).trigger("reloadGrid");
}

function jq5() {
    jQuery("#list5").jqGrid({
        url: 'UserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage5, rownum: OnePageCount5, pid: PID },
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
        pager: jQuery('#pager5'),
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
            if (pgButton == "next_pager5") {
                if (curPage == $("#list5").getGridParam("lastpage"))
                    return;
                curPage = $("#list5").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager5") {
                curPage = $("#list5").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager5") {
                if (curPage == 1)
                    return;
                curPage = $("#list5").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager5") {
                curPage = 1;
            }
            else {
                curPage = $("#pager5 :input").val();
            }
            reload5();
        },
        loadComplete: function () {
            $("#list5").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list5").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}
function reload6() {
    $("#list6").jqGrid('setGridParam', {
        url: 'ContractGrid',
        datatype: 'json',
        postData: { curpage: curPage6, rownum: OnePageCount6, pid: CID },

    }).trigger("reloadGrid");
}

function jq6() {
    jQuery("#list6").jqGrid({
        url: 'ContractGrid',
        datatype: 'json',
        postData: { curpage: curPage6, rownum: OnePageCount6, pid: CID },
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
        colNames: ['合同ID', '合同编号', '业务类型', '对应项目编号', '合同名称', '合同初始金额', '合同变更后金额', '合同签订日期', '预计完工时间', '合同签订回款次数', '已回款次数', '状态', 'State'],
        colModel: [
        { name: 'CID', index: 'CID', width: 100 },
        { name: 'ContractID', index: 'ContractID', width: 70 },
        { name: 'BusinessTypeDesc', index: 'BusinessTypeDesc', width: 70 },
        { name: 'PID', index: 'PID', width: 100 },
        { name: 'Cname', index: 'Cname', width: $("#bor").width() - 1000 },
        { name: 'CBeginAmount', index: 'CBeginAmount', width: 70 },
        { name: 'CEndAmount', index: 'CEndAmount', width: 80 },
        { name: 'Ctime', index: 'Ctime', width: 80 },
        { name: 'CPlanEndTime', index: 'CPlanEndTime', width: 80 },
        { name: 'AmountNum', index: 'AmountNum', width: 100 },
        { name: 'CurAmountNum', index: 'CurAmountNum', width: 70 },
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        { name: 'State', index: 'State', width: 50, hidden: true }
        ],
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '合同表',

        //gridComplete: function () {
        //    var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).CID + "' name='cb'/>";
        //        jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    }

        //},
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager6") {
                if (curPage == $("#list6").getGridParam("lastpage"))
                    return;
                curPage = $("#list6").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager6") {
                curPage = $("#list6").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager6") {
                if (curPage == 1)
                    return;
                curPage = $("#list6").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager6") {
                curPage = 1;
            }
            else {
                curPage = $("#pager6 :input").val();
            }
            reload6();
        },
        loadComplete: function () {
            $("#list6").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list6").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}
function reload7() {
    //JQtype = $("#JQtype").val();
    $("#list7").jqGrid('setGridParam', {
        url: 'ProCashBackGrid',
        datatype: 'json',
        postData: { curpage: curPage7, rownum: OnePageCount7, pid: PID }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

function jq7() {
    //JQtype = $("#JQtype").val();
    jQuery("#list7").jqGrid({
        url: 'ProCashBackGrid',
        datatype: 'json',
        postData: { curpage: curPage7, rownum: OnePageCount7, pid: PID }, //, jqType: JQtype
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
        colNames: ['项目编号', '回款编号', '回款次数', '回款金额(万元)', '发票金额(万元)', '缴费单位', '回款日期'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'CID', index: 'CID', width: 120 },
        { name: 'CBID', index: 'CBID', width: 150 },
        { name: 'CurAmountNum', index: 'CurAmountNum', width: 120 },
        { name: 'CBMoney', index: 'CBMoney', width: 120 },
        { name: 'ReceiptMoney', index: 'ReceiptMoney', width: 120 },
        { name: 'PayCompany', index: 'PayCompany', width: $("#bor").width() - 850 },
        { name: 'CBDate', index: 'CBDate', width: 180 },
        ],
        pager: jQuery('#pager7'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '回款记录表',

        gridComplete: function () {
            var ids = jQuery("#list7").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list7").jqGrid('getRowData', id);
                var str = "<a href='#' style='color:blue' onclick='ShowCashBack(\"" + curRowData.CBID + "\")' >" + curRowData.CBID + "</a>";
                jQuery("#list7").jqGrid('setRowData', ids[i], { CBID: str });
            }

        },
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=q' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=q' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager7") {
                if (curPage == $("#list7").getGridParam("lastpage"))
                    return;
                curPage = $("#list7").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager7") {
                curPage = $("#list7").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager7") {
                if (curPage == 1)
                    return;
                curPage = $("#list7").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager7") {
                curPage = 1;
            }
            else {
                curPage = $("#pager7 :input").val();
            }
            reload7();
        },
        loadComplete: function () {
            $("#list7").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list7").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

function ShowCashBack(id) {
    window.parent.OpenDialog("回款记录文档下载", "../ProjectManage/DownLoadCashBack?id=" + id, 400, 200, '');
}

function reload8() {
    //JQtype = $("#JQtype").val();
    $("#list8").jqGrid('setGridParam', {
        url: 'PurchaseGrid',
        datatype: 'json',
        postData: { curpage: curPage8, rownum: OnePageCount8, pid: PID },

    }).trigger("reloadGrid");
}

function jq8() {
    jQuery("#list8").jqGrid({
        url: 'PurchaseGrid',
        datatype: 'json',
        postData: { curpage: curPage8, rownum: OnePageCount8, pid: PID },
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
        colNames: ['项目编号', '编号', '合同编号', '合同名称', '甲方', '乙方', '所属项目', '合同额', '合同类别', '状态', 'state'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'PcID', index: 'PcID', width: 150, hidden: true },
        { name: 'PcNum', index: 'PcNum', width: 100 },
        { name: 'PcName', index: 'PcName', width: 100 },
        { name: 'PartA', index: 'PartA', width: 100 },
        { name: 'PartB', index: 'PartB', width: 100 },
        { name: 'Pname', index: 'Pname', width: $("#bor").width() - 900 },
        { name: 'PcAmount', index: 'PcAmount', width:  100},
        { name: 'PcType', index: 'PcType', width: 70 },
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        { name: 'State', index: 'State', width: 70, hidden: true },
        ],
        pager: jQuery('#pager8'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '采购表',

        gridComplete: function () {
            var ids = jQuery("#list8").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list2").jqGrid('getRowData', id);
                var str = "<a href='#' style='color:blue' onclick='ShowSubWork(\"" + curRowData.EID + "\")' >" + curRowData.EID + "</a>";

                jQuery("#list8").jqGrid('setRowData', ids[i], { EID: str });
            }

        },
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=q' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=q' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager8") {
                if (curPage == $("#list8").getGridParam("lastpage"))
                    return;
                curPage = $("#list8").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager8") {
                curPage = $("#list8").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager8") {
                if (curPage == 1)
                    return;
                curPage = $("#list8").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager8") {
                curPage = 1;
            }
            else {
                curPage = $("#pager8 :input").val();
            }
            reload8();
        },
        loadComplete: function () {
            $("#list8").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list8").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}
function updateCB(id) {
    window.parent.OpenDialog("修改回款记录", "../Contract/UpdateCashBack?id=" + id, 800, 600, '');
}

function dellCB(id) {
    var one = confirm("确定要撤销选中条目吗");
    if (one == false)
        return;
    else {
        $.ajax({
            url: "../Contract/dellCashBack",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    reload1();
                }
                else {
                    return;
                }
            }
        });
    }
    //window.parent.OpenDialog("修改回款记录", "../Contract/UpdateCashBack?id=" + id, 800, 600, '');
}

