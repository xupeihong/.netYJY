
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    // 新增 完成
    $("#XZJC").click(function () {
        window.parent.OpenDialog("新增检测", "../FlowMeterManage/AddIncomingInspection", 700, 650, '');
    });
    //修改 完成
    $("#XGJC").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RepairID + "@" + "iframe";
            window.parent.OpenDialog("修改检测表", "../FlowMeterManage/UpdateIncomingInspection?Info=" + escape(texts), 700, 650, '');
        }
    });

    //删除
    $('#SCJC').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要删除的项");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
            var one = confirm("是否删除编号为 " + texts + " 的项吗？");
            if (one == false)
                return;
            else {
                $.ajax({
                    url: "DeleteIncomingInspection",
                    type: "post",
                    data: { id: texts },
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
    LoadCheckDataList();
});

function LoadCheckDataList() {

    jQuery("#list").jqGrid({
        url: 'LoadCheckDataList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
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
        colNames: ['检测人', '检测时间', '检测类型', '检测方式', '流量点m3/h检测', '编号'],
        colModel: [

        { name: 'CheckUser', index: 'CheckUser', width: 100 },
        { name: 'CheckDate', index: 'CheckDate', width: 100 },
        { name: 'RepairType', index: 'RepairType', width: 100 },
        { name: 'RepairMethod', index: 'RepairMethod', width: 100 },
        { name: 'StrQmin', index: 'StrQmin', width: 100 },
        { name: 'RepairID', index: 'RepairID', width: 100 },

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
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).RepairID + "' name='cb'/>";
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
        url: 'LoadCheckDataList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,

        },

    }).trigger("reloadGrid");
}

