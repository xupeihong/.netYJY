var curPage = 1;
var OnePageCount = 15;
var isConfirm = false;

$(document).ready(function () {
    $("#pageContent").height($(window).height());

    jq();// 加载未缴费的报价单列表  

    $("#QD").click(function () {
        isConfirm = confirm("确定要新增缴费记录吗")
        if (isConfirm == false) {
            return false;
        }
        else
            submitInfo();
    });

})

function submitInfo() {
    var options = {
        url: "AddNewPay",
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
        url: 'LoadBJDList1',
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
        colNames: ['', '报价单号', '报价金额', '登记号', '缴费状态', '欠款金额', '备注'],
        colModel: [
            { name: 'IDCheck', index: 'Id', width: 20 },
            { name: 'QID', index: 'QID', width: 100 },
            { name: 'ConcesioPrice', index: 'ConcesioPrice', width: 100 },
            { name: 'RID', index: 'RID', width: 120 },
            { name: 'State', index: 'State', width: 100 },
            { name: 'LowPrice', index: 'LowPrice', width: 100 },
            { name: 'Comments', index: 'Comments', width: 200 }

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
                var curChk = "<input id='c" + id + "' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).QID
                    + '#' + jQuery("#list").jqGrid('getRowData', id).ConcesioPrice + '#' + jQuery("#list").jqGrid('getRowData', id).LowPrice + "' name='cb'/>";
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 170, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadBJDList1',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount 
        },
    }).trigger("reloadGrid");
}

// 获取应缴费金额 
function getRPrice() {
    var cbval = "";
    var price = 0.00;// 获取金额
    $('input[name=cb]:checked').each(function () {
        var cV = $(this).val().split('#');
        cbval += cV[0] + ",";
        if (parseFloat(cV[2]) != "0"&& cV[2]!="")// 有欠款
            price += parseFloat(cV[2]);
        else if (parseFloat(cV[2]) == "0" || cV[2] == "")// 无欠款
            price += parseFloat(cV[1]);
    });
    cbval = cbval.substr(0, cbval.length - 1);
    var texts = cbval;
    if (texts == "" || texts == null) {
        alert("您还没有选择要缴费的报价单")
        return;
    }
    $("#Rprice").val(price);// 应缴金额
    $("#HCheck").val(cbval);// 所选报价单号

}

// 点击实际缴费文本框 计算金额 
function getPrice() {
    var Rprice = $("#Rprice").val();// 应缴金额
    var Price = $("#StrPayMount").val();// 实际缴费
    if (Rprice != "" && Price != "")
        $("#Lowprice").val(parseFloat(Rprice) - parseFloat(Price));
    else
        $("#Lowprice").val("0.00");
    if ($("#Lowprice").val() != "0.00" && $("#HCheck").val().split(',').length > 1) {
        alert("批量缴费不允许欠款！");
        return;
    }
    
}


