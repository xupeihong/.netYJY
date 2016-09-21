
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;

$(document).ready(function () {
    $("#pageContent").height($(window).height());

    jq();// 加载详细列表 

    // 确定撤销 完成  
    $("#QDCX").click(function () {
        var one = confirm("确定撤销该缴费记录吗")
        if (one == false)
            return;
        else {
            $.ajax({
                url: "RePayment",
                type: "post",
                data: { PayID: $("#StrPayID").val(), QIDs: $("#Hidden").val() },
                dataType: "json",
                async: false, //是否异步
                success: function (data) {
                    if (data.success == "false") {
                        alert(data.Msg);
                        return;
                    }
                    else {
                        window.parent.frames["iframeRight"].reload();
                        alert(data.Msg);
                        setTimeout('parent.ClosePop()', 100);
                    }
                }
            });
        }
    });

})

function jq() {
    jQuery("#list").jqGrid({
        url: 'LoadBJDList2',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, QIDs: $("#Hidden").val()
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
        colNames: ['报价单号', '报价金额', '登记号', '缴费状态', '欠款金额', '备注'],
        colModel: [
            { name: 'QID', index: 'QID', width: 150 },
            { name: 'ConcesioPrice', index: 'ConcesioPrice', width: 80 },
            { name: 'RID', index: 'RID', width: 120, hide: true },
            { name: 'State', index: 'State', width: 70 },
            { name: 'LowPrice', index: 'LowPrice', width: 80 },
            { name: 'Comments', index: 'Comments', width: 200 }

        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

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
            $("#list").jqGrid("setGridWidth", $("#content1").width() - 65, false);
        }
    });
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadBJDList2',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, QIDs: $("#Hidden").val()
        },
    }).trigger("reloadGrid");
}