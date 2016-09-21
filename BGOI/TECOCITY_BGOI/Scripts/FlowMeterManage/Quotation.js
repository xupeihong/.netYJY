
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 20);

    //详情 完成
    $("#CKXQ").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var id = jQuery("#list").jqGrid('getRowData', rowid).RID;
            window.parent.OpenDialog("报价表详情", "../FlowMeterManage/QuotationDetail?RID=" + id, 700, 500, '');
        }
    });
    //修改 完成
    $("#XGBJ").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var id = jQuery("#list").jqGrid('getRowData', rowid).RID;
            window.parent.OpenDialog("修改报价表", "../FlowMeterManage/UpdateQuotation?RID=" + id, 700, 500, '');
        }
    });

    //删除
    $('#CXBJ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要删除的项");
            return;
        }
        else {

            var rid = jQuery("#list").jqGrid('getRowData', rowid).RID;
            var one = confirm("是否删除编号为 " + rid + " 的项吗？");

            if (one == false)
                return;
            else {
                $.ajax({
                    url: "DeleteQuotation",
                    type: "post",
                    data: { id: rid },
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
    //汇总单
    $('#HZBJ').click(function () {

        var str = "";
        $("input[name='cb']:checkbox").each(function () {

            if ($(this)[0].checked) {
                str += $(this)[0].value + ","
            }
        })
        if (str == "") { alert("请勾选报价单"); return; } else {
            window.parent.OpenDialog("报价单汇总", "../FlowMeterManage/QuotationSummary?RID=" + str + "&CardType=" + $("#CardType").val(), 1100, 500, '');
            //var url = "../FlowMeterManage/QuotationSummary?RID="+str;
            // window.showModalDialog(url, window, "dialogWidth:1100px;dialogHeight:500px;status:no;resizable:yes;edge:sunken;location:no;");
        }

    })

    LoadQuotationList();
});

function LoadQuotationList() {

    jQuery("#list").jqGrid({
        url: 'GetGenQtnList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            RID: $("#StrRID").val(), Type: $("#StrType").val(), CardType: $("#CardType").val()
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
        colNames: ['', '登记卡号', '价格', '状态', ],
        colModel: [
              { name: 'IDCheck', index: 'RID', width: 20 },
        { name: 'RID', index: 'RID', width: 250 },
        { name: 'TotalPriceU', index: 'TotalPriceU', width: 100 },
        { name: 'name', index: 'name', width: 100 },

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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function reload() {
    if ($('.field-validation-error').length == 0) {
        $("#list").jqGrid('setGridParam', {
            url: 'GetGenQtnList',
            datatype: 'json',
            postData: {
                curpage: curPage, rownum: OnePageCount,
                RID: $("#StrRID").val(), Type: $("#StrType").val(), CardType: $("#CardType").val()
            },

        }).trigger("reloadGrid");
    }
}

