
var curPage = 1;
var OnePageCount = 15;
var Type = "前期接洽";
var PID;
var ProID;
var Pname;
var StartDate;
var EndDate;
var Principal;
//var JQtype;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $('#XJJQ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行修改内容的跟踪洽谈记录信息");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).ID;
            window.parent.OpenDialog("修改跟踪洽谈记录内容", "../ProjectManage/UpdateEarlyContact?id=" + texts, 500, 380, '');
        }
    })

    $('#CXJQ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要撤销的跟踪洽谈信息");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).ID + "@" + jQuery("#list").jqGrid('getRowData', rowid).PID;
            var one = confirm("是否撤销这条跟踪洽谈信息");
            if (one == false)
                return;
            else {
                $.ajax({
                    url: "deleteJQ",
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
})

function reload() {
    if ($('.field-validation-error').length == 0) {
        ProID = $('#ProID').val();
        Pname = $('#Pname').val();
        StartDate = $('#StartDate').val();
        EndDate = $('#EndDate').val();
        Principal = $('#Principal').val();
        $("#list").jqGrid('setGridParam', {
            url: 'PrepareGrid',
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
        url: 'PrepareGrid',
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
        colNames: ['', 'ID', '项目编号', '内部编号', '项目名称', '跟踪洽谈时间', '创建时间', '跟踪洽谈类型', '内容概述', '跟踪人员', '所属单位','项目负责人'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'ID', index: 'ID', width: 120, hidden: true },
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'ProID', index: 'ProID', width: 120 },
        { name: 'Pname', index: 'Pname', width: 120 },
        { name: 'JQTime', index: 'JQTime', width: 120 },
        { name: 'CreateTime', index: 'CreateTime', width: 120 },
        { name: 'JQTypeDesc', index: 'JQTypeDesc', width: 150 },
        { name: 'Pview', index: 'Pview', width: $("#bor").width() - 920 },
        { name: 'FollowPerson', index: 'FollowPerson', width: 100 },
        { name: 'DeptName', index: 'DeptName', width: 100 },
        { name: 'Principal', index: 'Principal', width: 100 },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '项目跟踪洽谈表',

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