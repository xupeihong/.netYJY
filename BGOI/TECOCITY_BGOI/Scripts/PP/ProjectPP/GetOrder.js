var curPage = 1;
var OnePageCount = 6;
var oldSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();


    $("#btnSave").click(function () {
        
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要修改的行");
            return;
        }
        else {
            var selectedIds = $("#list").jqGrid("getGridParam", "selarrrow");
            var texts ="";
            for (var i = 0 ; i < selectedIds.length; i++) {
                texts += jQuery("#list").jqGrid('getCell', selectedIds[i], 'DID') + ",";
            }
            texts = texts.substr(0, texts.length - 1);
            var mycars = new Array()
            mycars = texts.split(',');
            for (var i = 0; i < mycars.length; i++) {
                var DID = mycars[i];
                window.parent.addBasicDetail(DID);
            }
            window.parent.ClosePop();

        }


    })
});

function jq() {
    jQuery("#list").jqGrid({
        url: 'GetByOrderID',
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
            colNames: [ '','编号', '物品摘要', '物品型号','物品编码', '生产厂家', '单位','数量', '单价', '总金额', '用途'],
            colModel: [
                 { name: 'DID', index: 'DID', width: 100 ,hidden:true},
            { name: 'DDID', index: 'DDID', width: 100 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'MaterialNO', index: 'MaterialNO', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 90 },
        { name: 'Unit', index: 'Unit', width: 50 },
        { name: 'Amount', index: 'Amount', width: 50 },
        { name: 'UnitPriceNoTax', index: 'UnitPriceNoTax', width: 120 },
        { name: 'TotalNoTax', index: 'TotalNoTax', width: 120 },
        { name: 'Use', index: 'Use', width: 100 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        multiselect: true,
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
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

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

function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    reload1();
}

function reload1() {
    $("#list1").jqGrid('setGridParam', {
        url: 'CreateProjectUserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID },

    }).trigger("reloadGrid");
}
function reload() {
   
        $("#list").jqGrid('setGridParam', {
            url: 'GetByOrderID',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount },

        }).trigger("reloadGrid");
   
}

