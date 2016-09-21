var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    jq();

    // 新建送检单 完成
    $("#XJSJD").click(function () {
        window.parent.OpenDialog("新建送检单", "../FlowMeterManage/AddInspection", 800, 600, '');
    });

    // 修改送检单 完成 
    $("#XGSJD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).SID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).SID + "@" + "iframe";
            window.parent.OpenDialog("修改送检单", "../FlowMeterManage/EditInspection?Info=" + escape(texts), 900, 600, '');
        }
    });

    // 查看详细并导出 完成 
    $("#CKXX").click(function () {
        window.parent.OpenDialog("选择送检详细信息", "../FlowMeterManage/InspectionDetail", 800, 600, '');
    });

});

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

// 列表显示送检单列表 
function jq() {
    jQuery("#list").jqGrid({
        url: 'LoadInspecList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            SID: $("#SID").val(), UnitName: $("#UnitName").val(), BathID: $("#BathID").val(),
            SInspecDate: $("#SInspecDate").val(), EInspecDate: $("#EInspecDate").val()
        },
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
        colNames: ['', '送检单编号', '联系人', '联系电话', '送检日期', '送检批次'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'SID', index: 'SID', width: 120 },
        { name: 'LinkPerson', index: 'LinkPerson', width: 110 },
        { name: 'LinkTel', index: 'LinkTel', width: 90 },
        { name: 'InspecDate', index: 'InspecDate', width: 200 },
        { name: 'BathID', index: 'BathID', width: 200 }

        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).SID + "' name='cb'/>";
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadInspecList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            SID: $("#SID").val(), UnitName: $("#UnitName").val(), BathID: $("#BathID").val(),
            SInspecDate: $("#SInspecDate").val(), EInspecDate: $("#EInspecDate").val()
        },

    }).trigger("reloadGrid");
}

