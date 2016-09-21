
var curPage = 1;
var OnePageCount = 30;
var PID;
var RelenvceID;
var Type = "工程立项审批";
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
            var texts = jQuery("#list").jqGrid('getRowData', rowid).cid + "@" + jQuery("#list").jqGrid('getRowData', rowid).Pname;
            window.parent.OpenDialog("竣工验收修改", "../ProjectManage/UpdateProjectCompletions?id=" + texts, 600, 400, '');
        }
    })

    $('#CX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行撤销的条目");
            return;
        }
        else {
            var pid = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var one = confirm("确定要撤销选中条目吗");
            if (one == false)
                return;
            else {
                $.ajax({
                    url: "dellProjectBidding",
                    type: "post",
                    data: { data1: pid },
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

    $('#XZ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行下载的条目");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).cid;
            //var FileName = jQuery("#list").jqGrid('getRowData', rowid).CompleteFileName;
            if (texts == "")
            {
                alert("您选择的条目没有要下载的文件");
                return;
            }
            //window.open("DownLoadProjectCompletions?id=" + texts);
            window.parent.OpenDialog("设计文档下载", "../ProjectManage/DownLoadCompletions?id=" + texts, 400, 200, '');
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
            url: 'ProjectCompletionsGrid',
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
        url: 'ProjectCompletionsGrid',
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
        colNames: ['', '竣工编号','项目编号', 'PID', '内部编号', '项目名称', '项目负责人', '立项时间', '竣工日期', '验收人', '验收说明', '文档名称', '所属单位'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'cid', index: 'cid', width: 200 },
        { name: 'PIDShow', index: 'PIDShow', width: 120 },
        { name: 'PID', index: 'PID', width: 120, hidden: true },
        { name: 'ProID', index: 'ProID', width: 100 },
        { name: 'Pname', index: 'Pname', width: 200 },
        { name: 'Principal', index: 'Principal', width: 70 },
        { name: 'AppDate', index: 'AppDate', width: 130 },
        { name: 'CompleteDate', index: 'CompleteDate', width: 100 },
        { name: 'CompletePerson', index: 'CompletePerson', width: 70 },
        { name: 'CompleteRmark', index: 'CompleteRmark', width: $("#bor").width() - 680 },
        { name: 'CompleteFileName', index: 'CompleteFileName', width: 120, hidden: true },
        { name: 'DeptName', index: 'DeptName', width: 100 },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '项目竣工验收表',

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
