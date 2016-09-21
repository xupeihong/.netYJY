
var curPage = 1;
var OnePageCount = 15;
var Type = "项目立项";
var Type2 = "工程立项";
var PID;
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
    jq();
    //jq1();

    $('#UseXGLX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行修改内容的项目单");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State == "0") {
                alert("该项目还没有立项，不能进行修改立项内容操作");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
            window.parent.OpenDialog("修改立项内容", "../ProjectManage/UseUpdateSetUp?id=" + texts, 700, 570, '');
        }
    })

    $('#XGLX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行修改内容的项目单");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State == "0") {
                alert("该项目还没有立项，不能进行修改立项内容操作");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID + "@" + jQuery("#list").jqGrid('getRowData', rowid).Pname + "@" + jQuery("#list").jqGrid('getRowData', rowid).PsourceDesc + "@" + jQuery("#list").jqGrid('getRowData', rowid).MainContent;
            window.parent.OpenDialog("修改立项内容", "../ProjectManage/UpdateSetUp?id=" + texts, 700, 570, '');
        }
    })

    $('#UseCXLX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要撤销立项的条目");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State == "0") {
                alert("该项目还没有立项，不能进行立项撤销操作");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var one = confirm("是否对编号为 " + texts + " 的项目进行立项撤销吗");
            if (one == false)
                return;
            else {
                $.ajax({
                    url: "deleteUseApp",
                    type: "post",
                    data: { data1: texts },
                    dataType: "Json",
                    success: function (data) {
                        if (data.success == "true") {
                            alert(data.Msg);
                            reload();
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

    $('#CXLX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要撤销立项的条目");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State == "0") {
                alert("该项目还没有立项，不能进行立项撤销操作");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var one = confirm("是否对编号为 " + texts + " 的项目进行立项撤销吗");
            if (one == false)
                return;
            else {
                $.ajax({
                    url: "deleteApp",
                    type: "post",
                    data: { data1: texts },
                    dataType: "Json",
                    success: function (data) {
                        if (data.success == "true") {
                            alert(data.Msg);
                            reload();
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

    $('#CKXX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要查看详细的项目单");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State == "0") {
                alert("该项目还没有立项，没有立项内容");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
            window.parent.OpenDialog("详细内容", "../ProjectManage/DetailApp?id=" + texts, 700, 500, '');
        }
    })

    $('#TJSP').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行提交审批的项目单");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (State == "0") {
                alert("该项目还没有立项，不能进行提交审批");
                return;
            }
            if (State == "2") {
                alert("该项目已经提交审批，不能重复操作");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID + "@" + "工程立项";
            window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
        }
    })

    $('#DYLXSQ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择您要打印的项目单");
            return;
        }
        var State = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (State == "0") {
            alert("该项目还没有立项，不能打印立项申请");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
        var url = "PrintApp?id=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
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
            url: 'AppProjectGrid',
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
        url: 'AppProjectGrid',
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
        colNames: ['', '项目编号', 'PID', '内部编号', '项目名称', '创建时间', '项目来源', '项目概述', '立项编号', '项目地点', '立项时间', '项目负责人', '配合负责人', '所属单位', '项目状态', 'State'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PIDShow', index: 'PIDShow', width: 120 },
        { name: 'PID', index: 'PID', width: 120, hidden: true },
        { name: 'ProID', index: 'ProID', width: 100 },
        { name: 'Pname', index: 'Pname', width: 150 },
        { name: 'CreateTime', index: 'CreateTime', width: 130 },
        { name: 'PsourceDesc', index: 'PsourceDesc', width: 50, hidden: true },
        { name: 'MainContent', index: 'MainContent', width: 50, hidden: true },
        { name: 'AppID', index: 'AppID', width: 100, hidden: true },
        { name: 'Paddress', index: 'Paddress', width: $("#bor").width() - 900 },
        { name: 'AppDate', index: 'AppDate', width: 120 },
        { name: 'Principal', index: 'Principal', width: 80 },
        { name: 'ConcertPerson', index: 'ConcertPerson', width: 80 },
        { name: 'DeptName', index: 'DeptName', width: 100 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 },
        { name: 'State', index: 'State', width: 50, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '项目立项表',

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
            //select(rowid);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()-220, false);
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

function ShowDetail(id) {
    window.parent.OpenDialog("详细内容", "../ProjectManage/DetailApp?id=" + id, 700, 500, '');
}

function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    reload1();
}


function reload1() {
    $("#list1").jqGrid('setGridParam', {
        url: 'CreateProjectUserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, Type: Type, Type2: Type2 },

    }).trigger("reloadGrid");
}

function jq1() {
    jQuery("#list1").jqGrid({
        url: 'CreateProjectUserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, Type: Type, Type2: Type2 },
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

