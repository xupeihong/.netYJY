
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
    $("#Prepare").width($("#search").width());
    $("#Prepare").height($("#pageContent").height() / 2 - 75);
    $("#SetUp").width($("#search").width());
    $("#SetUp").height($("#pageContent").height() / 2 - 75);
    $("#Design").width($("#search").width());
    $("#Design").height($("#pageContent").height() / 2 - 75);
    $("#UserLog").width($("#search").width());
    $("#UserLog").height($("#pageContent").height() / 2 - 75);
    $("#Budget").width($("#search").width());
    $("#Budget").height($("#pageContent").height() / 2 - 75);
    $("#Bidding").width($("#search").width());
    $("#Bidding").height($("#pageContent").height() / 2 - 75);
    $("#Contract").width($("#search").width());
    $("#Contract").height($("#pageContent").height() / 2 - 75);
    $("#Price").width($("#search").width());
    $("#Price").height($("#pageContent").height() / 2 - 75);
    //$("#CashBack").width($("#search").width());
    //$("#CashBack").height($("#pageContent").height() / 2 - 75);
    jq();
    jq1();
    jq2();
    jq3();
    jq4();
    jq5();
    jq6();
    jq7();
   
    $('#QQJQdiv').click(function () {
        this.className = "btnTw";
        $('#LXdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#CBYSdiv').attr("class", "btnTh");
        $('#TBdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#BJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#Prepare").css("display", "");
        $("#SetUp").css("display", "none");
        $("#Design").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#Budget").css("display", "none");
        $("#Bidding").css("display", "none");
        $("#Contract").css("display", "none");
        $("#Price").css("display", "none");
        $("#CashBack").css("display", "none");
        reload1();
    })

    $('#LXdiv').click(function () {
        this.className = "btnTw";
        $('#QQJQdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#CBYSdiv').attr("class", "btnTh");
        $('#TBdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#BJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#SetUp").css("display", "");
        $("#Prepare").css("display", "none");
        $("#Design").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#Budget").css("display", "none");
        $("#Bidding").css("display", "none");
        $("#Contract").css("display", "none");
        $("#Price").css("display", "none");
        $("#CashBack").css("display", "none");

        $("#SetUp").html("");
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (state >= 1 && PID != null) {
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
    })

    $('#SJdiv').click(function () {
        this.className = "btnTw";
        $('#QQJQdiv').attr("class", "btnTh");
        $('#LXdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#CBYSdiv').attr("class", "btnTh");
        $('#TBdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#BJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#Design").css("display", "");
        $("#Prepare").css("display", "none");
        $("#SetUp").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#Budget").css("display", "none");
        $("#Bidding").css("display", "none");
        $("#Contract").css("display", "none");
        $("#Price").css("display", "none");
        $("#CashBack").css("display", "none");
        reload2();
    })

    $('#BJdiv').click(function () {
        this.className = "btnTw";
        $('#SJdiv').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");
        $('#LXdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#CBYSdiv').attr("class", "btnTh");
        $('#TBdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#Price").css("display", "");
        $("#Design").css("display", "none");
        $("#Prepare").css("display", "none");
        $("#SetUp").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#Budget").css("display", "none");
        $("#Bidding").css("display", "none");
        $("#Contract").css("display", "none");
        $("#CashBack").css("display", "none");
        reload7();
    })

    $('#CBYSdiv').click(function () {
        this.className = "btnTw";
        $('#LXdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#TBdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#BJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#Budget").css("display", "");
        $("#Prepare").css("display", "none");
        $("#SetUp").css("display", "none");
        $("#Design").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#Bidding").css("display", "none");
        $("#Contract").css("display", "none");
        $("#Price").css("display", "none");
        $("#CashBack").css("display", "none");
        reload4();
    })

    $('#TBdiv').click(function () {
        this.className = "btnTw";
        $('#LXdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#CBYSdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#BJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#Bidding").css("display", "");
        $("#Budget").css("display", "none");
        $("#Prepare").css("display", "none");
        $("#SetUp").css("display", "none");
        $("#Design").css("display", "none");
        $("#UserLog").css("display", "none");
        $("#Contract").css("display", "none");
        $("#Price").css("display", "none");
        $("#CashBack").css("display", "none");
        reload5();
    })

    $('#CZRZdiv').click(function () {
        this.className = "btnTw";
        $('#LXdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");
        $('#CBYSdiv').attr("class", "btnTh");
        $('#TBdiv').attr("class", "btnTh");
        $('#QDHTdiv').attr("class", "btnTh");
        $('#BJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#UserLog").css("display", "");
        $("#Prepare").css("display", "none");
        $("#SetUp").css("display", "none");
        $("#Design").css("display", "none");
        $("#Budget").css("display", "none");
        $("#Bidding").css("display", "none");
        $("#Contract").css("display", "none");
        $("#Price").css("display", "none");
        $("#CashBack").css("display", "none");
        reload3();
    })

    //$('#HKdiv').click(function () {
    //    this.className = "btnTw";
    //    $('#LXdiv').attr("class", "btnTh");
    //    $('#SJdiv').attr("class", "btnTh");
    //    $('#QQJQdiv').attr("class", "btnTh");
    //    $('#CBYSdiv').attr("class", "btnTh");
    //    $('#TBdiv').attr("class", "btnTh");
    //    $('#CZRZdiv').attr("class", "btnTh");
    //    $('#BJdiv').attr("class", "btnTh");
    //    $('#QDHTdiv').attr("class", "btnTh");

    //    $("#CashBack").css("display", "");
    //    $("#UserLog").css("display", "none");
    //    $("#Prepare").css("display", "none");
    //    $("#SetUp").css("display", "none");
    //    $("#Design").css("display", "none");
    //    $("#Budget").css("display", "none");
    //    $("#Bidding").css("display", "none");
    //    $("#Price").css("display", "none");
    //    $("#Contract").css("display", "none");
    //    reload6();
    //})

    $('#QDHTdiv').click(function () {
        this.className = "btnTw";
        $('#LXdiv').attr("class", "btnTh");
        $('#SJdiv').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");
        $('#CBYSdiv').attr("class", "btnTh");
        $('#TBdiv').attr("class", "btnTh");
        $('#CZRZdiv').attr("class", "btnTh");
        $('#BJdiv').attr("class", "btnTh");
        $('#HKdiv').attr("class", "btnTh");

        $("#Contract").css("display", "");
        $("#UserLog").css("display", "none");
        $("#Prepare").css("display", "none");
        $("#SetUp").css("display", "none");
        $("#Design").css("display", "none");
        $("#Budget").css("display", "none");
        $("#Bidding").css("display", "none");
        $("#Price").css("display", "none");
        $("#CashBack").css("display", "none");
        reload6();
    })

    $('#QQJQ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行跟踪洽谈的项目单");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
            window.parent.OpenDialog("跟踪洽谈记录", "../ProjectManage/EarlyContact?id=" + texts, 500, 380, '');
        }
    })

    $('#GCLX').click(function () {
            window.parent.OpenDialog("立项", "../ProjectManage/UseSetUpProject", 720, 600, '');
    })

    $('#LX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行立项的项目单");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State >= "1") {
                alert("该项目已经立项，不能进行立项操作");
                return;
            } 
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID + "@" + jQuery("#list").jqGrid('getRowData', rowid).Pname + "@" + jQuery("#list").jqGrid('getRowData', rowid).PsourceDesc + "@" + jQuery("#list").jqGrid('getRowData', rowid).MainContent + "@" + jQuery("#list").jqGrid('getRowData', rowid).ProID;
            window.parent.OpenDialog("立项", "../ProjectManage/SetUpProject?id=" + texts, 700, 570, '');
        }
    })

    $('#SJ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行设计的项目单");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State < "3") {
                alert("该项目还没有立项审批，不能进行设计操作");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID + "@" + jQuery("#list").jqGrid('getRowData', rowid).Pname;
            window.parent.OpenDialog("设计", "../ProjectManage/PreDesign?id=" + texts, 600, 400, '');
        }
    })

    $('#BJ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行报价的项目单");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State < "3") {
                alert("该项目还没有立项审批，不能进行报价操作");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID + "@" + jQuery("#list").jqGrid('getRowData', rowid).Pname;
            window.parent.OpenDialog("报价", "../ProjectManage/Offer?id=" + texts, 600, 400, '');
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
                alert("该项目还没有立项审批，不能进行预算操作");
                return;
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
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID + "@" + jQuery("#list").jqGrid('getRowData', rowid).Pname;
            window.parent.OpenDialog("投标", "../ProjectManage/AddBidding?id=" + texts, 600, 400, '');
        }
    })

    $('#QDHT').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行签订合同的项目单");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State < "3") {
                alert("该项目还没有立项审批，不能进行签订合同操作");
                return;
            }
            if (State == "5")
            {
                alert("该项目已经签订了合同");
                return;
            }
            var IsContract = jQuery("#list").jqGrid('getRowData', rowid).IsContract;
            if (IsContract == "无")
            {
                alert("该项目在立项时选择的是无合同，请先核对立项信息，再进行操作");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).ProID;
            window.parent.OpenDialog("签订合同", "../Contract/AddProjectContract?id=" + texts, 800, 500, '');
        }
    })

    //$('#HKJL').click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("您还没有选择要进行回款记录的条目");
    //        return;
    //    }
    //    var State = jQuery("#list").jqGrid('getRowData', rowid).State;
    //    if (State < "1") {
    //        alert("该项目还没有立项，不能进行签订合同操作");
    //        return;
    //    }
    //    var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
    //    var IsContract = jQuery("#list").jqGrid('getRowData', rowid).IsContract;
    //    if (IsContract == "有") {
    //        alert("该项目有合同，请到项目实施综合管理进行回款记录");
    //        return;
    //    }
    //    window.parent.OpenDialog("项目回款记录", "../ProjectManage/PCashBack?id=" + texts, 800, 600, '');
    //})
})

function loadLX()
{
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
        StartDate = $('#StartDate').val();
        EndDate = $('#EndDate').val();
        Principal = $('#Principal').val();
        $("#list").jqGrid('setGridParam', {
            url: 'GeneralGrid',
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
        url: 'GeneralGrid',
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
        colNames: ['', '项目编号', 'PID', '内部编号', '项目名称', '客户名称', '项目来源', '项目目标', '项目概述', '项目负责人', '立项时间', '是否设计', '是否报价', '是否预算', '有无合同', '回款情况', '所属单位', '项目状态', 'State'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PIDShow', index: 'PIDShow', width: 120 },
        { name: 'PID', index: 'PID', width: 120, hidden: true },
        { name: 'ProID', index: 'ProID', width: 100 },
        { name: 'Pname', index: 'Pname', width: $("#bor").width() - 900  },
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

function ShowDetail(id)
{
    window.parent.OpenDialog("详细内容", "../ProjectManage/DetailApp?id=" + id, 700, 500, '');
}

function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    ProID = jQuery("#list").jqGrid('getRowData', rowid).ProID;
    reload1();
    loadLX();
    reload2();
    reload3();
    reload4();
    reload5();
    reload6();
    reload7();
}



function reload1() {
    //JQtype = $("#JQtype").val();
    $("#list1").jqGrid('setGridParam', {
        url: 'ProjectQQGrid',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, pid: PID }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

function jq1() {
    //JQtype = $("#JQtype").val();
    jQuery("#list1").jqGrid({
        url: 'ProjectQQGrid',
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
        colNames: ['ID', '项目编号', '跟踪洽谈类型', '内容概述', '跟踪人员', '跟踪洽谈时间'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'ID', index: 'ID', width: 120, hidden: true },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'JQTypeDesc', index: 'JQTypeDesc', width: 150 },
        { name: 'Pview', index: 'Pview', width: $("#bor").width() - 680 },
        { name: 'FollowPerson', index: 'FollowPerson', width: 120 },
        { name: 'JQTime', index: 'JQTime', width: 180 },
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '跟踪洽谈表',

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
        url: 'DesignGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, pid: PID }, 

    }).trigger("reloadGrid");
}

function jq2() {
    jQuery("#list2").jqGrid({
        url: 'DesignGrid',
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
        colNames: [ '设计编号', '项目编号', '设计内容', '设计概述','状态'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'sid', index: 'sid', width: 150 },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'DesignType', index: 'DesignType', width: 150 },
        { name: 'Pview', index: 'Pview', width: $("#bor").width() - 600 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '项目设计表',

        gridComplete: function () {
            var ids = jQuery("#list2").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list2").jqGrid('getRowData', id);
                var str = "<a href='#' style='color:blue' onclick='ShowDesign(\"" + curRowData.sid + "\")' >" + curRowData.sid + "</a>";
                jQuery("#list2").jqGrid('setRowData', ids[i], { sid: str });
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

function ShowDesign(id)
{
    window.parent.OpenDialog("设计文档下载", "../ProjectManage/DownLoadPreDesign?id=" + id, 400, 200, '');
}


function reload7() {
    //JQtype = $("#JQtype").val();
    $("#list7").jqGrid('setGridParam', {
        url: 'PriceGrid',
        datatype: 'json',
        postData: { curpage: curPage7, rownum: OnePageCount7, pid: PID },

    }).trigger("reloadGrid");
}

function jq7() {
    jQuery("#list7").jqGrid({
        url: 'PriceGrid',
        datatype: 'json',
        postData: { curpage: curPage7, rownum: OnePageCount7, pid: PID },
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
        colNames: ['报价编号', '项目编号',  '报价概述', '报价总额','状态'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'oid', index: 'oid', width: 150 },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'Pview', index: 'Pview', width: $("#bor").width() - 600 },
        { name: 'PriceAmount', index: 'PriceAmount', width: 120 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 },
        ],
        pager: jQuery('#pager7'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '项目报价表',

        gridComplete: function () {
            var ids = jQuery("#list7").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list7").jqGrid('getRowData', id);
                var str = "<a href='#' style='color:blue' onclick='ShowPrice(\"" + curRowData.oid + "\")' >" + curRowData.oid + "</a>";
                jQuery("#list7").jqGrid('setRowData', ids[i], { oid: str });
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

function ShowPrice(id) {
    window.parent.OpenDialog("报价文档下载", "../ProjectManage/DownLoadPrice?id=" + id, 400, 200, '');
}


function reload3() {
    //JQtype = $("#JQtype").val();
    $("#list3").jqGrid('setGridParam', {
        url: 'UserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage3, rownum: OnePageCount3, pid: PID,proid:ProID },

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
        colNames: [ '项目编号', '操作内容', '操作结果', '操作时间', '操作人'],
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


function reload4() {
    //JQtype = $("#JQtype").val();
    $("#list4").jqGrid('setGridParam', {
        url: 'BudgetGrid',
        datatype: 'json',
        postData: { curpage: curPage4, rownum: OnePageCount4, pid: PID },

    }).trigger("reloadGrid");
}

function jq4() {
    jQuery("#list4").jqGrid({
        url: 'BudgetGrid',
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
        colNames: ['预算编号', '项目编号', '预算概述', '状态'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'bid', index: 'bid', width: 150 },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'Pview', index: 'Pview', width: $("#bor").width() - 600 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 },
        ],
        pager: jQuery('#pager4'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '成本预算表',

        gridComplete: function () {
            var ids = jQuery("#list4").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list4").jqGrid('getRowData', id);
                var str = "<a href='#' style='color:blue' onclick='ShowBudget(\"" + curRowData.bid + "\")' >" + curRowData.bid + "</a>";
                jQuery("#list4").jqGrid('setRowData', ids[i], { bid: str });
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

function ShowBudget(id) {
    window.parent.OpenDialog("预算文档下载", "../ProjectManage/DownLoadBudget?id=" + id, 400, 200, '');
}


function reload5() {
    //JQtype = $("#JQtype").val();
    $("#list5").jqGrid('setGridParam', {
        url: 'BiddingGrid',
        datatype: 'json',
        postData: { curpage: curPage5, rownum: OnePageCount5, pid: PID },

    }).trigger("reloadGrid");
}

function jq5() {
    jQuery("#list5").jqGrid({
        url: 'BiddingGrid',
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
        colNames: ['投标编号', '项目编号', '概述', '投标时间', '状态'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'BidID', index: 'BidID', width: 150 },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'Pview', index: 'Pview', width: $("#bor").width() - 600 },
        { name: 'BiddingTime', index: 'BiddingTime', width: 150 },
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        ],
        pager: jQuery('#pager5'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '投标表',

        gridComplete: function () {
            var ids = jQuery("#list5").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list5").jqGrid('getRowData', id);
                var str = "<a href='#' style='color:blue' onclick='ShowBidding(\"" + curRowData.BidID + "\")' >" + curRowData.BidID + "</a>";
                jQuery("#list5").jqGrid('setRowData', ids[i], { BidID: str });
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
                curPage = $("#list5 :input").val();
            }
            reload5();
        },
        loadComplete: function () {
            $("#list5").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list5").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

function ShowBidding(id) {
    window.parent.OpenDialog("投标文档下载", "../ProjectManage/DownLoadBidding?id=" + id, 400, 200, '');
}

function reload6() {
    $("#list6").jqGrid('setGridParam', {
        url: 'ContractGrid',
        datatype: 'json',
        postData: { curpage: curPage6, rownum: OnePageCount6, pid: ProID },

    }).trigger("reloadGrid");
}

function jq6() {
    jQuery("#list6").jqGrid({
        url: 'ContractGrid',
        datatype: 'json',
        postData: { curpage: curPage6, rownum: OnePageCount6, pid: ProID },
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
        colNames: ['合同ID', '合同编号', '业务类型', '对应项目编号', '合同名称',  '预计完工时间', '项目合同额', '项目前期费用', '项目成本', '项目利润', '状态', 'State'],
        colModel: [
        { name: 'CID', index: 'CID', width: 100 },
        { name: 'ContractID', index: 'ContractID', width: 70 },
        { name: 'BusinessTypeDesc', index: 'BusinessTypeDesc', width: 70 },
        { name: 'PID', index: 'PID', width: 100 },
        { name: 'Cname', index: 'Cname', width: $("#bor").width() - 850 },
        { name: 'CPlanEndTime', index: 'CPlanEndTime', width: 80 },
        { name: 'PContractAmount', index: 'PContractAmount', width: 60 },
        { name: 'PBudget', index: 'PBudget', width: 70 },
        { name: 'PCost', index: 'PCost', width: 60 },
        { name: 'PProfit', index: 'PProfit', width: 60 },
        //{ name: 'AmountNum', index: 'AmountNum', width: 100 },
        //{ name: 'CurAmountNum', index: 'CurAmountNum', width: 70 },
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        { name: 'State', index: 'State', width: 50, hidden: true }
        ],
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '合同表',

        gridComplete: function () {
            var ids = jQuery("#list6").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list6").jqGrid('getRowData', id);
                var str = "<a href='#' style='color:blue' onclick='ShowContract(\"" + curRowData.CID + "\")' >" + curRowData.CID + "</a>";
                jQuery("#list6").jqGrid('setRowData', ids[i], { CID: str });
            }

        },
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

function ShowContract(id) {
    window.parent.OpenDialog("合同文件下载", "../Contract/DownloadFileProject?id=" + id, 400, 200, '');
}