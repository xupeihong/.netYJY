
var curPage = 1;
var OnePageCount = 30;
var PID;
var RelenvceID;

var ProID;
var Pname;
var start;
var end;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $('#SZTXSJ').click(function () {
        window.parent.OpenDialog("设置提醒时间", "../PPManage/SetWarnTime", 450, 200, '');
    })
})

function reload() {
    if ($('.field-validation-error').length == 0) {
        var DDID = $('#DDID').val();

      var  Begin = $('#Begin').val();
      var  End = $('#End').val();
 
        $("#list").jqGrid('setGridParam', {
            url: 'CPlanTimeWarnGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, DDID: DDID, Begin: Begin, End: End },

        }).trigger("reloadGrid");
    }
}

function jq() {
    var DDID = $('#DDID').val();
   var Begin = $('#Begin').val();
   var End = $('#End').val();
    jQuery("#list").jqGrid({
        url: 'CPlanTimeWarnGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, DDID: DDID, Begin: Begin, End: End },
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
        colNames: ['', '订购编号', '订购时间',  '订购说明', '采购人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 20 },
        { name: 'DDID', index: 'DDID', width: 160 },
        { name: 'OrderDate', index: 'OrderDate', width: 100 },
        { name: 'PleaseExplain', index: 'PleaseExplain', width: 130 },
        { name: 'OrderContacts', index: 'OrderContacts', width: 150 },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '采购提醒',

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
