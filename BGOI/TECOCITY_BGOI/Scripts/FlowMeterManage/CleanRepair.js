
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    //查看详情
    $("#CKXQ").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var rid = jQuery("#list").jqGrid('getRowData', rowid).CleanID;
            window.parent.OpenDialog("清洗详情", "../FlowMeterManage/CleanRepairDetail?CleanID=" + rid, 700, 300, '');
        }
    });
    //修改 完成
    $("#XGQX").click(function () {
        var CardType = $("#CardType").val();
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            $.ajax({
                url: "Operate",
                type: "post",
                data: { P: "6", RID: rID, str: $(this).val(), type: CardType },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        var texts = jQuery("#list").jqGrid('getRowData', rowid).CleanID + "@" + "iframe";
                        window.parent.OpenDialog("修改清洗记录", "../FlowMeterManage/UpdateCleanRepair?Info=" + escape(texts), 700, 500, '');
                    }
                    else {
                        alert(data.Msg);
                        return;
                    }

                }
            });

        }
    });

    //撤销
    $('#SCQX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要删除的项");
            return;
        }
        else {
            var CardType = $("#CardType").val();
            var texts = jQuery("#list").jqGrid('getRowData', rowid).CleanID;
            var rid = jQuery("#list").jqGrid('getRowData', rowid).RID;
            $.ajax({
                url: "Operate",
                type: "post",
                data: { P: "6", RID: rid, str: $(this).val(), type: CardType },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {

                        var one = confirm("是否删除编号为 " + rid + " 的项吗？");
                        if (one == false)
                            return;
                        else {

                            $.ajax({
                                url: "DeleteCleanRepair",
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
                    } else {
                        alert(data.Msg);
                        return;
                    }
                }

            })
        }

    })
    LoadCleanRepairList();
});

function LoadCleanRepairList() {

    jQuery("#list").jqGrid({
        url: 'LoadCleanRepairList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, type: $("#CardType").val(),
            RID: $("#StrRID").val(), CleanUser: $("#StrCleanUser").val(), CleanSDate: $("#StrCleanSDate").val(), CleanEDate: $("#StrCleanEDate").val()
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
        colNames: ['登记卡号', '清洗人', '清洗开始时间', '清洗结束时间', ''],
        colModel: [
        { name: 'RID', index: 'RID', width: 250 },
        { name: 'CleanUser', index: 'CleanUser', width: 100 },
        { name: 'CleanSDate', index: 'CleanSDate', width: 100 },
        { name: 'CleanEDate', index: 'CleanEDate', width: 100 },
         { name: 'CleanID', index: 'CleanID', width: 250, hidden: true }

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
    if ($('.field-validation-error').length == 0) {
        $("#list").jqGrid('setGridParam', {
            url: 'LoadCleanRepairList',
            datatype: 'json',
            postData: {
                curpage: curPage, rownum: OnePageCount, type: $("#CardType").val(),
                RID: $("#StrRID").val(), CleanUser: $("#StrCleanUser").val(), CleanSDate: $("#StrCleanSDate").val(), CleanEDate: $("#StrCleanEDate").val()
            },

        }).trigger("reloadGrid");
    }
}

