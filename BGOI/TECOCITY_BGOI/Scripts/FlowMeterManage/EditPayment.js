var curPage = 1;
var OnePageCount = 15;
var isConfirm = false;

$(document).ready(function () {
    $("#pageContent").height($(window).height());

    jq();// 加载关联的报价单列表  

    // 确定
    $("#QRXG").click(function () {
        isConfirm = confirm("确定要修改缴费记录吗")
        if (isConfirm == false) {
            return false;
        }
        else
            submitInfo();
    });

})

// 界面提交
function submitInfo() {
    var options = {
        url: "UpdatePayment",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                window.parent.frames["iframeRight"].reload();
                alert(data.Msg);
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}

function returnConfirm() {
    return false;
}

function jq() {
    jQuery("#list").jqGrid({
        url: 'LoadBJDList2',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, QIDs: $("#QIDs").val()
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
        colNames: [ '报价单号', '报价金额', '登记号', '缴费状态', '欠款金额', '备注'],
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 170, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadBJDList2',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, QIDs: $("#QIDs").val()
        },
    }).trigger("reloadGrid");
}

// 文本框触发事件 
function getPrice() {
    var PayMount = parseFloat($("#StrPayMount").val());// 实际缴费
    var TotalPriceC = parseFloat($("#TotalPriceC").val());// 应缴费用
    var LowPrice = TotalPriceC - PayMount;
    $("#LowPrice").val(LowPrice);// 欠款金额
    if ($("#Lowprice").val() != "0.00" && $("#QIDs").val().split(',').length > 1) {
        alert("批量缴费不允许欠款！");
        return;
    }
}
