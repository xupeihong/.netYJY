var curPage = 1;
var OnePageCount = 15;
var isConfirm = false;
var OrderDate;

$(document).ready(function () {
    $("#pageContent").height($(window).height());

    jq();

    // 确定发货
    $('#QD').click(function () {
        isConfirm = confirm("是否要确认发货？")
        if (isConfirm == false) {
            return false;
        }
        else {
            var cbval = "";
            $('input[name=cb]:checked').each(function () {
                cbval += $(this).val() + ",";
            });
            cbval = cbval.substr(0, cbval.length - 1);
            var texts = cbval;
            if (texts == "" || texts == null) {
                alert("您还没有选择要确认发货的数据")
                return;
            }
            $("#CheckIDs").val(texts);
            submitInfo();
        }
    });

    // 是否进行出库操作 完成
    $("#OutStock").click(function () {
        var cbval = "";
        $('input[name=cb]:checked').each(function () {
            cbval += $(this).val() + ",";
        });
        cbval = cbval.substr(0, cbval.length - 1);
        var text = cbval;
        if (text == "" || text == null) {
            alert("请先选择数据")
            return;
        }
        $("#CheckIDs").val(text);
        // 先判断是否已经进行过出库操作 
        $.ajax({
            url: "CheckStockOutInfo",
            type: "post",
            data: { CheckIDs: text },
            dataType: "Json",
            success: function (data) {
                if (data.success == "false") {
                    alert(data.Msg);
                    return;
                }
                else {
                    var texts = text + "@" + "iframe";
                    //window.open("../FlowMeterManage/OutStockDetail?Info=" + escape(texts));
                    ShowIframe1("出库详细", "../FlowMeterManage/OutStockDetail?Info=" + escape(texts), 500, 400, '');
                }
            }
        })
    })

})

function submitInfo() {
    var options = {
        url: "CheckSendInfo",
        type: "post",
        data: {},
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 10);
            }
            else {
                //alert(data.Msg);
            }
        }
    }
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

function returnConfirm() {
    return false;
}

function jq() {
    jQuery("#list").jqGrid({
        url: 'LoadSendTaskList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, OrderDate: OrderDate
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
        colNames: ['', '登记卡号', '', '维修编号', '', '客户名称', '客户地址', '联系电话', '送表日期', '仪表编号', '仪表型号', '仪表名称','仪表类型', '状态'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'RID', index: 'RID', width: 120 },
        { name: 'RIDShow', index: 'RIDShow', width: 90, hidden: true },
        { name: 'RepairID', index: 'RepairID', width: 110 },
        { name: 'RepairShow', index: 'RepairShow', width: 90, hidden: true },
        { name: 'CustomerName', index: 'CustomerName', width: 200 },
        { name: 'CustomerAddr', index: 'CustomerAddr', width: 200 },
        { name: 'S_Tel', index: 'S_Tel', width: 120 },
        { name: 'S_Date', index: 'S_Date', width: 120 },
        { name: 'MeterID', index: 'MeterID', width: 100 },
        { name: 'Model', index: 'Model', width: 100 },
        { name: 'MeterName', index: 'MeterName', width: 150 },
        { name: 'ModelType', index: 'ModelType', width: 100 },
        { name: 'State', index: 'State', width: 100 }

        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '登记卡列表',
        optionloadonce: true,
        sortname: 'State',

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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        },
        onSortCol: function (index, iCol, sortorder) {
            OrderDate = index + "@" + sortorder;
            $("#Order").val(OrderDate);
            reload();
        }
    });
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadSendTaskList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, OrderDate: OrderDate
        },
    }).trigger("reloadGrid");
}
