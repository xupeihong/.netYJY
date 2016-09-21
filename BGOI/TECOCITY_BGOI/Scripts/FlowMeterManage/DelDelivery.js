
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());

    jq();// 加载详细列表 

    // 确定撤销 完成  
    $("#QDCX").click(function () {
        var cbval = "";
        $('input[name=cb]:checked').each(function () {
            cbval += $(this).val() + ",";
        });
        var texts = cbval.substr(0, cbval.length - 1);
        if (texts == "" || texts == null) {
            alert("您还没有选择要撤销的记录")
            return;
        }
        var one = confirm("确定撤销 " + texts + "吗")
        if (one == false)
            return;
        else {
            $.ajax({
                url: "ReDelivery",
                type: "post",
                data: { RID: texts },
                dataType: "json",
                async: false, //是否异步
                success: function (data) {
                    if (data.success == "false") {
                        alert(data.Msg);
                        return;
                    }
                    else {
                        window.parent.frames["iframeRight"].reload();
                        window.parent.frames["iframeRight"].reload1();
                        alert(data.Msg);
                        setTimeout('parent.ClosePop()', 10);
                    }
                }
            });
        }
    });
})

function jq() {
    jQuery("#list").jqGrid({
        url: 'LoadDetailList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            TakeID: $("#Hidden").val()
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
        colNames: ['','登记卡号', '仪表编号', '仪表名称', '仪表型号', '口径', '现使用单位/地址', '随表附件', '外观检查项'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'RID', index: 'RID', width: 120 },
        { name: 'MeterID', index: 'MeterID', width: 120 },
        { name: 'MeterName', index: 'MeterName', width: 120 },
        { name: 'Model', index: 'Model', width: 90 },
        { name: 'Caliber', index: 'Caliber', width: 100 },
        { name: 'NewUnit', index: 'NewUnit', width: 180 },
        { name: 'Files', index: 'Files', width: 100 },
        { name: 'FaceText', index: 'FaceText', width: 250 }

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
                var curChk = "<input id='c" + id + "' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).RID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()/2 - 70, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function reload() {   
    $("#list").jqGrid('setGridParam', {
        url: 'LoadDetailList',
        datatype: 'json',
        loadonce: false,
        postData: {
            curpage: curPage, rownum: OnePageCount,
            TakeID: $("#Hidden").val()
        },
    }).trigger("reloadGrid");
}
