
var curPage = 1;
var OnePageCount = 30;
var curPage1 = 1;
var OnePageCount1 = 30;
var curPage2 = 1;
var OnePageCount2 = 30;
var PID;
var RelenvceID;
var Type = "工程项目合同审批";
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
    //$("#QQ").width($("#search").width());
    $("#QQ").width($("#search").width());
    $("#QQ").height($("#pageContent").height() / 2 - 75);
    $("#RZJ").width($("#search").width());
    $("#RZJ").height($("#pageContent").height() / 2 - 75);
    jq();
    jq1();
    jq2();

    $('#QQJQdiv').click(function () {
        this.className = "btnTw";
        $('#RZJLdiv').attr("class", "btnTh");
        $("#QQ").css("display", "");
        $("#RZJ").css("display", "none");
    })

    $('#RZJLdiv').click(function () {
        this.className = "btnTw";
        $('#QQJQdiv').attr("class", "btnTh");
        $("#RZJ").css("display", "");
        $("#QQ").css("display", "none");
    })

    $('#SP').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行审批的条目");
            return;
        }
        else {
            var SPID = jQuery("#list").jqGrid('getRowData', rowid).SPID;
            var PID = jQuery("#list").jqGrid('getRowData', rowid).CID;
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (state == $("#Nostate").val()) {
                alert("审批不通过，不能进行审批了");
                return;
            }
            $.ajax({
                url: "../COM_Approval/JudgeAppDisable",
                type: "post",
                data: { data1: $("#webkey").val(), data2: SPID },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        var bol = data.intblo;
                        if (bol == "-1") {
                            alert("您没有审批权限，不能进行审批操作");
                            return;
                        }
                        if (bol == "1") {
                            alert("您已经审批完成，不能进行审批操作");
                            return;
                        }
                        if (bol == "2") {
                            alert("审批过程还没有进行到您这一步，不能进行审批操作");
                            return;
                        }
                        var texts = $("#webkey").val() + "@" + SPID + "@" + PID;
                        window.parent.OpenDialog("审批", "../COM_Approval/Approval?id=" + texts, 500, 400, '');
                    }
                    else {
                        return;
                    }
                }
            });
        }
    })

    $('#SPQK').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要查看审批情况的条目");
            return;
        }
        else {
            var SPID = jQuery("#list").jqGrid('getRowData', rowid).SPID;
            var texts = $("#webkey").val() + "@" + SPID;
            window.parent.OpenDialog("审批情况", "../COM_Approval/ApprovalCondition?id=" + texts, 700, 500, '');
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
            url: 'ProjectContractExamineGrid',
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
        url: 'ProjectContractExamineGrid',
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
        colNames: ['', '项目编号', 'PID', '内部编号', '项目名称', '项目负责人', '创建时间', '项目状态', 'State', '合同编号', 'CID', '审批编号', '所属单位'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PIDShow', index: 'PIDShow', width: 110 },
        { name: 'PID', index: 'PID', width: 150, hidden: true },
        { name: 'ProID', index: 'ProID', width: 100 },
        { name: 'Pname', index: 'Pname', width: $("#bor").width() - 990 },
        { name: 'Principal', index: 'Principal', width: 70 },
        { name: 'CreateTime', index: 'CreateTime', width: 150 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 },
        { name: 'State', index: 'State', width: 50, hidden: true },
        { name: 'CIDshow', index: 'CIDshow', width: 110 },
        { name: 'CID', index: 'CID', width: 200, hidden: true },
        { name: 'SPID', index: 'SPID', width: 120 },
        { name: 'DeptName', index: 'DeptName', width: 100 },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '合同审批表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                var str = "<a href='#' style='color:blue' onclick='ShowDetail(\"" + curRowData.PIDShow + "\")' >" + curRowData.PIDShow + "</a>";
                var str1 = "<a href='#' style='color:blue' onclick='ShowContract(\"" + curRowData.CIDshow + "\")' >" + curRowData.CIDshow + "</a>";


                jQuery("#list").jqGrid('setRowData', ids[i], { CIDshow: str1 });
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

function ShowContract(id) {
    window.parent.OpenDialog("合同文件下载", "../Contract/DownloadFileProject?id=" + id, 400, 200, '');
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
    PID = jQuery("#list").jqGrid('getRowData', rowid).SPID;
    RelenvceID = jQuery("#list").jqGrid('getRowData', rowid).CID;
    reload1();
    reload2();
}

function reload1() {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list1").jqGrid('setGridParam', {
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, PID: PID, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}

function jq1() {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list1").jqGrid({
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, PID: PID, webkey: webkey, folderBack: folderBack },
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
        colNames: ['', '职务', '姓名', '审批方式', '人数', '审批情况', '审批意见', '审批时间', '备注'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'Job', index: 'Job', width: 100 },
        { name: 'UserName', index: 'UserName', width: 100 },
        { name: 'AppTypeDesc', index: 'AppTypeDesc', width: 100 },
        { name: 'Num', index: 'Num', width: 100 },
        { name: 'stateDesc', index: 'stateDesc', width: 100 },
        { name: 'Opinion', index: 'Opinion', width: $("#bor").width() - 920 },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 150 },
        { name: 'Remark', index: 'Remark', width: 200 },
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                //var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                //jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            if (pgButton == "next_pager1") {
                if (curPage == $("#list1").getGridParam("lastpage"))
                    return;
                curPage = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
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
    $("#list2").jqGrid('setGridParam', {
        url: 'CreateProjectUserLogGrid',
        datatype: 'json',
        postData: { curpage2: curPage, rownum: OnePageCount2, PID: RelenvceID, Type: Type },

    }).trigger("reloadGrid");
}

function jq2() {
    jQuery("#list2").jqGrid({
        url: 'CreateProjectUserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, PID: RelenvceID, Type: Type },
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
        colNames: ['', '项目编号', '操作内容', '操作结果', '操作时间', '操作人'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RelevanceID', index: 'RelevanceID', width: 120 },
        { name: 'LogTitle', index: 'LogTitle', width: 200 },
        { name: 'LogContent', index: 'LogContent', width: $("#bor").width() - 700 },
        { name: 'LogTime', index: 'LogTime', width: 150 },
        { name: 'LogPerson', index: 'LogPerson', width: 70 }
        ],
        pager: jQuery('#pager2'),
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

