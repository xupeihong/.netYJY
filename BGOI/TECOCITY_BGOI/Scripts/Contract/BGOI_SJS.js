var MCode;
var ProName;

var oldSelID = 0;
var newSelID = 0;

var curPage = 1;
var OnePageCount = 15;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $("#QD").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择项目");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).MCode;
        var one = confirm("确定选择编号为 " + texts + "的项目吗")
        if (one == false)
            return;
        else {
            window.parent.document.getElementById("StrPID").value = texts;
            alert("选择项目成功");
            parent.ClosePop();
        }
    })

})

function reload() {
    MCode = $("#MCode").val();
    ProName = $("#ProName").val();

    $("#list").jqGrid('setGridParam', {
        url: 'MandateGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, mcode: MCode, ProName: ProName }

    }).trigger("reloadGrid");
}


function CloseDialog2() {
    window.parent.CloseDialog();
    reload();
}
function jq() {

    MCode = $("#MCode").val();
    ProName = $("#ProName").val();


    jQuery("#list").jqGrid({
        url: 'MandateGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, mcode: MCode, ProName: ProName },
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
        colNames: ['', '预约单号', '委托单编号', '委托方姓名', '工程名称', '样品信息', '检测项目', '提交时间', '要求完成时间', '状态'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'YYCode', index: 'YYCode', width: 100, hidden: true },
        { name: 'MCode', index: 'MCode', width: 80 },
        { name: 'ClienName', index: 'ClienName', width: 100 },
        { name: 'proname', index: 'proname', width: 200 },
        { name: 'sampleInfo', index: 'sampleInfo', width: 120, hidden: true },
        { name: 'testingItems', index: 'testingItems', width: 200 }, // $("#bor").width() - 800
        { name: 'CreateTime', index: 'CreateTime', width: 130 },
        { name: 'demandfinishDate', index: 'demandfinishDate', width: 130 },
        { name: 'text', index: 'text', width: 70 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '委托单表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 220, false);
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