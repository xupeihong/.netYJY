
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());

    jq();

    // 保存选项 
    $("#btnSave").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            window.parent.addMeterDetail(rID);
            window.parent.ClosePop();
        }
    });

})

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

//
function jq() {
    jQuery("#list").jqGrid({
        url: 'LoadMeterList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount
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
        colNames: ['', '登记号', '仪表编号', '证书编号', '仪表名称', '生产厂家', '仪表型号',
            '出厂日期', '流量范围', '承压等级', '口径', '客户地址','隶属单位', '仪表类型','第三方检测单位'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'RID', index: 'RID', width: 120 },
        { name: 'MeterID', index: 'MeterID', width: 100 },
        { name: 'CertifID', index: 'CertifID', width: 100 },
        { name: 'MeterName', index: 'MeterName', width: 90 },
        { name: 'Manufacturer', index: 'Manufacturer', width: 110 },
        { name: 'Model', index: 'Model', width: 90 },
        { name: 'FactoryDate', index: 'FactoryDate', width: 90 },
        { name: 'FlowRange', index: 'FlowRange', width: 90 },
        { name: 'Pressure', index: 'Pressure', width: 90 },
        { name: 'Caliber', index: 'Caliber', width: 90 },
        { name: 'CustomerAddr', index: 'CustomerAddr', width: 150 },
        { name: 'SubUnit', index: 'SubUnit', width: 150 },
        { name: 'ModelType', index: 'ModelType', width: 100 },
        { name: 'OutUnit', index: 'OutUnit', width: 120 }

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
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).RID + "' name='cb'/>";
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
        url: 'LoadMeterList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount
        },
    }).trigger("reloadGrid");
}