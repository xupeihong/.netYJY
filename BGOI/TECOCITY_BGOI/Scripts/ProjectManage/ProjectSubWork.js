﻿
var curPage = 1;
var OnePageCount = 30;
var PID;
var RelenvceID;
var Type = "分包施工审批";
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

    $('#XG').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行修改的条目");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).EID + "@" + jQuery("#list").jqGrid('getRowData', rowid).Pname;
            window.parent.OpenDialog("分包施工修改", "../ProjectManage/UpdateSubWork?id=" + texts, 700, 570, '');
        }
    })

    $('#XZ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行下载的条目");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).EID;
            window.parent.OpenDialog("分包施工文档下载", "../ProjectManage/DownLoadSubWork?id=" + texts, 400, 200, '');
        }
    })

    $('#CX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行撤销的条目");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).EID;
            var PID = jQuery("#list").jqGrid('getRowData', rowid).PID
            var one = confirm("确定要撤销选中条目吗");
            if (one == false)
                return;
            else {
                $.ajax({
                    url: "dellProjectSubWork",
                    type: "post",
                    data: { data1: texts, data2: PID },
                    dataType: "Json",
                    success: function (data) {
                        if (data.success == "true") {
                            alert(data.Msg);
                            reload();
                        }
                        else {
                            return;
                        }
                    }
                });
            }
        }
    })

    $('#DY').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择您要打印的条目");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).EID;
        var url = "PrintSubWork?id=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    })


    $('#TJSP').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行提交审批的条目");
            return;
        }
        else {
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (state >= 1) {
                alert("你选择的条目已经提交审批，不能进行重复操作");
                return;
            }
            if (state == -1) {
                alert("你选择的条目审批未通过，请重新起草内容");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).EID + "@" + "分包施工审批";
            window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
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
            url: 'ProjectSubWorkGrid',
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
        url: 'ProjectSubWorkGrid',
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
        colNames: ['', '项目编号', 'PID', '内部编号', '项目名称', '立项时间', '项目负责人', '分包施工编号', '分包施工单位', '分包施工负责人', '分包费用', '分包施工原因', '分包施工主要内容', '所属单位', '状态', 'state'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PIDShow', index: 'PIDShow', width: 120 },
        { name: 'PID', index: 'PID', width: 120, hidden: true },
        { name: 'ProID', index: 'ProID', width: 100 },
        { name: 'Pname', index: 'Pname', width: 100 },
        { name: 'AppDate', index: 'AppDate', width: 100 },
        { name: 'ProPrincipal', index: 'ProPrincipal', width: 70 },
        { name: 'EID', index: 'EID', width: 150 },
        { name: 'SubUnit', index: 'SubUnit', width: 120 },
        { name: 'Principal', index: 'Principal', width: 120 },
        { name: 'SubPrice', index: 'SubPrice', width: 120 },
        { name: 'SubWorkReason', index: 'SubWorkReason', width: 120 },
        { name: 'MainContent', index: 'MainContent', width: $("#bor").width() - 900 },
        { name: 'DeptName', index: 'DeptName', width: 100 },
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        { name: 'State', index: 'State', width: 70, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '分包施工表',

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

function ShowDetail(id) {
    window.parent.OpenDialog("详细内容", "../ProjectManage/DetailApp?id=" + id, 700, 500, '');
}
