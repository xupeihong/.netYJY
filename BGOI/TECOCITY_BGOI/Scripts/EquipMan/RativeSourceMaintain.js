var curPage = 1;
var OnePageCount = 15;
var RID;
var Manufacturer;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $('#XZFSY').click(function () {
        window.parent.OpenDialog("新增放射源", "../EquipMan/AddRativeSource", 600, 300, '');
    })

    $('#XG').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要修改的放射源");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ID;
        window.parent.OpenDialog("修改放射源", "../EquipMan/UpdateRativeSource?id=" + texts, 600, 500, '');
    })

    $('#SC').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要删除的放射源");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ID;
        var one = confirm("确定要删除选择的条目吗");
        if (one == false)
            return;
        else {
            $.ajax({
                url: "deleteRativeSource",
                type: "post",
                data: { data1: texts },
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
    })
})

function reload() {
    RID = $('#RID').val();
    Manufacturer = $('#Manufacturer').val();
    $("#list").jqGrid('setGridParam', {
        url: 'RativeSourceGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, rID: RID, manufacturer: Manufacturer },

    }).trigger("reloadGrid");
}

function jq() {
    RID = $('#RID').val();
    Manufacturer = $('#Manufacturer').val();
    jQuery("#list").jqGrid({
        url: 'RativeSourceGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, rID: RID, manufacturer: Manufacturer },
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
        colNames: ['', 'ID', '内部编号', '设备编号', '产品型号', '源项', '生产厂家', '标称活度', '放射源编码'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'ID', index: 'ID', width: 100, hidden: true },
        { name: 'RID', index: 'RID', width: 100 },
        { name: 'EquipID', index: 'EquipID', width: 100 },
        { name: 'ProModel', index: 'ProModel', width: $("#bor").width() - 900 },
        { name: 'Source', index: 'Source', width: 100 },
        { name: 'Manufacturer', index: 'Manufacturer', width: 200 },
        { name: 'Nominal', index: 'Nominal', width: 100 },
        { name: 'SourceNumber', index: 'SourceNumber', width: 200 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '放射源表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).TaskID + "' name='cb'/>";
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