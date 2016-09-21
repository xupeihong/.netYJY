var curPage = 1;
var OnePageCount = 50;
var sel;
var GridFunction;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    jq();

    $('#XZ').click(function () {
        sel = $("#sel").val();
        if (sel == "") {
            alert("请选择属地公司");
            return;
        }
        window.parent.OpenDialog("新增5S店", "../SalesRetail/AddFiveMalls?id=" + sel, 400, 200, '');
    })
})


function change() {
    sel = $("#sel").val();
    reload();
}

function reload() {
    sel = $("#sel").val();

    $("#list").jqGrid('setGridParam', {
        url: "GetFivMallsGrid",
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, sel: sel },
        caption: '5S店信息表',
        loadonce: false,
        colNames: ['序号', '5S店名称', '所属公司', 'ID', 'ChildGrade', 'HigherUnitID', '操作', '操作'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 90, align: 'center' },
        { name: 'Malls', index: 'Malls', width: 360 },
        { name: 'UnitName', index: 'UnitName', width: 360 },
        { name: 'ID', index: 'ID', width: 200, hidden: true },
        { name: 'ChildGrade', index: 'ChildGrade', width: 200, hidden: true },
        { name: 'HigherUnitID', index: 'HigherUnitID', width: 200, hidden: true },
        { name: 'Pro', index: 'ID', width: 50 },
        { name: 'Prodel', index: 'ID', width: 50 }
        ],
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
        },
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var Malls;
                var UnitName;
                var ID;
                var ChildGrade;
                var HigherUnitID;
                var modify;
                var del;

                Malls = jQuery("#list").jqGrid('getRowData', id).Malls;
                UnitName = jQuery("#list").jqGrid('getRowData', id).UnitName;
                ID = jQuery("#list").jqGrid('getRowData', id).ID;
                ChildGrade = jQuery("#list").jqGrid('getRowData', id).ChildGrade;
                HigherUnitID = jQuery("#list").jqGrid('getRowData', id).HigherUnitID;
                modify = "<a  href='#' onclick='UpdateType(\"" + ID + "\",\"" + HigherUnitID + "\",\"" + Malls + "\")' style='color:blue'>修改</a>";
                del = "<a href='#' style='color:#f60' onclick='DeleteType(" + ID + ",\"" + HigherUnitID + "\")' >删除</a>";

                jQuery("#list").jqGrid('setRowData', ids[i], { Pro: modify, Prodel: del });
            }

        }
    }).trigger("reloadGrid");
}

function jq() {
    sel = $("#sel").val();
    jQuery("#list").jqGrid({
        url: "GetFivMallsGrid",
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, sel: sel },
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
        colNames: ['序号', '5S店名称', '所属公司', 'ID', 'ChildGrade', 'HigherUnitID', '操作', '操作'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 90, align: 'center' },
        { name: 'Malls', index: 'Malls', width: 360 },
        { name: 'UnitName', index: 'UnitName', width: 360 },
        { name: 'ID', index: 'ID', width: 200, hidden: true },
        { name: 'ChildGrade', index: 'ChildGrade', width: 200, hidden: true },
        { name: 'HigherUnitID', index: 'HigherUnitID', width: 200, hidden: true },
        { name: 'Pro', index: 'ID', width: 50 },
        { name: 'Prodel', index: 'ID', width: 50 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '5S店信息表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var Malls;
                var UnitName;
                var ID;
                var ChildGrade;
                var HigherUnitID;
                var modify;
                var del;

                Malls = jQuery("#list").jqGrid('getRowData', id).Malls;
                UnitName = jQuery("#list").jqGrid('getRowData', id).UnitName;
                ID = jQuery("#list").jqGrid('getRowData', id).ID;
                ChildGrade = jQuery("#list").jqGrid('getRowData', id).ChildGrade;
                HigherUnitID = jQuery("#list").jqGrid('getRowData', id).HigherUnitID;
                modify = "<a  href='#' onclick='UpdateType(\"" + ID + "\",\"" + HigherUnitID + "\",\"" + Malls + "\")' style='color:blue'>修改</a>";
                del = "<a href='#' style='color:#f60' onclick='DeleteType(" + ID + ",\"" + HigherUnitID + "\")' >删除</a>";

                jQuery("#list").jqGrid('setRowData', ids[i], { Pro: modify, Prodel: del });
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
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function UpdateType(id, higherUnitID, malls) {
    window.parent.OpenDialog("修改5S店", "../SalesRetail/UpdateFiveMalls?id=" + id + "&HigherUnitID=" + higherUnitID + "&Malls=" + malls, 400, 200);
}

function DeleteType(id, higherUnitID) {

    var one = confirm("确定删除这条数据吗？");
    if (one == false)
        return
    else {
        $.ajax({
            type: "POST",
            url: "DeleteMalls",
            data: { ID: id, HigherUnitID: higherUnitID },
            success: function (data) {
                alert(data.Msg);
                reload();
            },
            dataType: 'json'
        });
    }
}


