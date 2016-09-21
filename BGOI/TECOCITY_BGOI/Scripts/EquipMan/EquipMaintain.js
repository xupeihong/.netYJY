
var curPage = 1;
var OnePageCount = 2000;
var TracingType;
var CheckCompany;
var state;
var StarTime;
var EndTime;
var JStarTime;
var JEndTime;
var OrderDate;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $('#XZSB').click(function () {
        window.parent.OpenDialog("新增设备", "../EquipMan/AddEquip", 600, 550, '');
    })

    $('#XG').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要修改的设备");
            return;
        }
        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ECode;
        if (state == -1) {
            alert("该设备已经报废，不能进行任何操作");
            return;
        }
        window.parent.OpenDialog("修改设备信息", "../EquipMan/UpdateEquip?id=" + texts, 600, 500, '');
    })

    $('#SC').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要删除的设备");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ECode;
        var one = confirm("确定要删除选择的条目吗");
        if (one == false)
            return;
        else {
            $.ajax({
                url: "deleteEquip",
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

    $('#SJXZ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要送检校准的设备");
            return;
        }
        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ECode + "@" + jQuery("#list").jqGrid('getRowData', rowid).CheckCompany + "@" + jQuery("#list").jqGrid('getRowData', rowid).TracingTypeDesc;
        if (state == -1) {
            alert("该设备已经报废，不能进行任何操作");
            return;
        }
        window.parent.OpenDialog("送检校准", "../EquipMan/EquipCheck?id=" + texts, 500, 450, '');
    })

    $('#SJXZLS').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要查看历史的设备");
            return;
        }
        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ECode;
        if (state == -1) {
            alert("该设备已经报废，不能进行任何操作");
            return;
        }
        window.parent.OpenDialog("送检校准历史", "../EquipMan/EquipCheckHistory?id=" + texts, 900, 500, '');
    })

    //$('#XZJL').click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("您还没有选择要进行校准记录的设备");
    //        return;
    //    }
    //    var state = jQuery("#list").jqGrid('getRowData', rowid).State;
    //    var texts = jQuery("#list").jqGrid('getRowData', rowid).ECode;
    //    if (state != 1) {
    //        alert("只有状态为送检校准的设备才可进行校准记录");
    //        return;
    //    }
    //    if (state == -1) {
    //        alert("该设备已经报废，不能进行任何操作");
    //        return;
    //    }
    //    window.parent.OpenDialog("校准记录", "../EquipMan/CheckRecord?id=" + texts, 500, 250, '');
    //})

    //$('#WX').click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("您还没有选择要进行维修的设备");
    //        return;
    //    }
    //    var texts = jQuery("#list").jqGrid('getRowData', rowid).ECode;
    //    var state = jQuery("#list").jqGrid('getRowData', rowid).State;
    //    if (state == -1) {
    //        alert("该设备已经报废，不能进行任何操作");
    //        return;
    //    }
    //    window.parent.OpenDialog("设备维修", "../EquipMan/EquipService?id=" + texts, 500, 350, '');
    //})

    //$('#WXJL').click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("您还没有选择要进行维修记录的设备");
    //        return;
    //    }
    //    var texts = jQuery("#list").jqGrid('getRowData', rowid).ECode;
    //    var state = jQuery("#list").jqGrid('getRowData', rowid).State;
    //    if (state != 2) {
    //        alert("只有状态为维修中的设备才可进行维修记录记录");
    //        return;
    //    }
    //    if (state == -1) {
    //        alert("该设备已经报废，不能进行任何操作");
    //        return;
    //    }
    //    window.parent.OpenDialog("维修记录", "../EquipMan/RepaireRecord?id=" + texts, 500, 400, '');
    //})

    $('#BF').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要进行维修记录的设备");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ECode;
        window.parent.OpenDialog("设备报废", "../EquipMan/EquipScrap?id=" + texts, 500, 350, '');
    })
})

function Towod() {
    var record = $("#list").getGridParam("reccount");
    if (record == 0) {
        alert("列表内容为空，没有要导出的数据，不能进行导出操作");
        return false;
    }
    else {
        var one = confirm("确定将列表内容导出吗？")
        if (one == false) {
            return false;
        }
        else {
            return true;
        }
    }
}

function reload() {
    TracingType = $('#TracingType').val();
    CheckCompany = $('#CheckCompany').val();
    JStarTime = $('#JStarTime').val();
    JEndTime = $('#JEndTime').val();
    StarTime = $('#StarTime').val();
    EndTime = $('#EndTime').val();
    state = $('#state').val();
    $("#list").jqGrid('setGridParam', {
        url: 'EquipGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, TracingType: TracingType, CheckCompany: CheckCompany, JStarTime: JStarTime, JEndTime: JEndTime, starTime: StarTime, endTime: EndTime, state: state, OrderDate: OrderDate },

    }).trigger("reloadGrid");
}

function jq() {
    TracingType = $('#TracingType').val();
    CheckCompany = $('#CheckCompany').val();
    JStarTime = $('#JStarTime').val();
    JEndTime = $('#JEndTime').val();
    StarTime = $('#StarTime').val();
    EndTime = $('#EndTime').val();
    state = $('#state').val();
    jQuery("#list").jqGrid({
        url: 'EquipGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, TracingType: TracingType, CheckCompany: CheckCompany, JStarTime: JStarTime, JEndTime: JEndTime, starTime: StarTime, endTime: EndTime, state: state, OrderDate: OrderDate },
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
        colNames: ['', "序号", '设备编号', '控制编号', '设备名称', '规格型号', '测量范围', '准确度等级/ 不确定度 ',
            '溯源方式', '周期类型', '周期', '上次检定/校准日期', '有效截止日期至', '检定/校准单位名称', '备注', '状态', 'State'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'xu', index: 'xu', width: 30 },
        { name: 'ECode', index: 'ECode', width: 100, hidden: true },
        { name: 'ControlCode', index: 'ControlCode', width: 70 },
        { name: 'Ename', index: 'Ename', width: 70 },
        { name: 'Specification', index: 'Specification', width: $("#bor").width() - 950 },
        { name: 'Clrange', index: 'Clrange', width: 100 },
        { name: 'Precision', index: 'Precision', width: 130 },
        { name: 'TracingTypeDesc', index: 'TracingTypeDesc', width: 70 },
        { name: 'CycleType', index: 'CycleType', width: 70 },
        { name: 'Cycle', index: 'Cycle', width: 70 },
        { name: 'LastDate', index: 'LastDate', width: 120 },
        { name: 'PlanDate', index: 'PlanDate', width: 120 },
        { name: 'CheckCompany', index: 'CheckCompany', width: 110 },
        { name: 'Remark', index: 'Remark', width: 100 },
        { name: 'stateDesc', index: 'stateDesc', width: 50 },
        { name: 'State', index: 'State', width: 50, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '台账表',
        sortable: true,
        optionloadonce: true,
        sortname: 'LastDate',

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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        },
        onSortCol: function (index, iCol, sortorder) {
            OrderDate = index + "@" + sortorder;
            $("#Order").val(OrderDate);
            reload();
        }
    });
}

//function jq() {
//    TracingType = $('#TracingType').val();
//    CheckCompany = $('#CheckCompany').val();
//    JStarTime = $('#JStarTime').val();
//    JEndTime = $('#JEndTime').val();
//    StarTime = $('#StarTime').val();
//    EndTime = $('#EndTime').val();
//    state = $('#state').val();
    
//    jQuery("#list").jqGrid({
//        url: 'EquipGrid',
//        datatype: 'json',
//        postData: {
//            curpage: curPage, rownum: OnePageCount,
//            TracingType: TracingType, CheckCompany: CheckCompany, JStarTime: JStarTime,
//            JEndTime: JEndTime, starTime: StarTime, endTime: EndTime,
//            state: state, OrderDate: OrderDate
//        },
//        mtype: 'POST',
//        loadonce: false,
//        jsonReader: {
//            root: function (obj) {
//                var data = eval("(" + obj + ")");
//                return data.rows;
//            },
//            page: function (obj) {
//                var data = eval("(" + obj + ")");
//                return data.page;
//            },
//            total: function (obj) {
//                var data = eval("(" + obj + ")");
//                return data.total;
//            },
//            records: function (obj) {
//                var data = eval("(" + obj + ")");
//                return data.records;
//            },
//            repeatitems: false
//        },
//        colNames: ['', "序号", '设备编号', '控制编号', '设备名称', '规格型号', '测量范围', '准确度等级/ 不确定度 ',
//            '溯源方式', '周期类型', '周期', '上次检定/校准日期', '有效截止日期至', '检定/校准单位名称', '备注', '状态', 'State'],
//        colModel: [
//        { name: 'IDCheck', index: 'Id', width: 20 },
//        { name: 'xu', index: 'xu', width: 30 },
//        { name: 'ECode', index: 'ECode', width: 100, hidden: true },
//        { name: 'ControlCode', index: 'ControlCode', width: 70 },
//        { name: 'Ename', index: 'Ename', width: 70 },
//        { name: 'Specification', index: 'Specification', width: $("#bor").width() - 950 },
//        { name: 'Clrange', index: 'Clrange', width: 100 },
//        { name: 'Precision', index: 'Precision', width: 130 },
//        { name: 'TracingTypeDesc', index: 'TracingTypeDesc', width: 70 },
//        { name: 'CycleType', index: 'CycleType', width: 70 },
//        { name: 'Cycle', index: 'Cycle', width: 70 },
//        { name: 'LastDate', index: 'LastDate', width: 120 },
//        { name: 'PlanDate', index: 'PlanDate', width: 120 },
//        { name: 'CheckCompany', index: 'CheckCompany', width: 110 },
//        { name: 'Remark', index: 'Remark', width: 100 },
//        { name: 'stateDesc', index: 'stateDesc', width: 50 },
//        { name: 'State', index: 'State', width: 50, hidden: true }
//        ],
//        pager: jQuery('#pager'),
//        pgbuttons: true,
//        rowNum: OnePageCount,
//        viewrecords: true,
//        imgpath: 'themes/basic/images',
//        caption: '设备表',
//        sortable: true,
//        optionloadonce: true,
//        sortname: 'LastDate',
//        sortname: 'PlanDate',

//        gridComplete: function () {
//            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
//            for (var i = 0; i < ids.length; i++) {
//                var id = ids[i];
//                var curRowData = jQuery("#list").jqGrid('getRowData', id);
//                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).TaskID + "' name='cb'/>";
//                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
//            }

//        },
//        onSelectRow: function (rowid, status) {
//            if (oldSelID != 0) {
//                $('input[id=c' + oldSelID + ']').prop("checked", false);
//            }
//            $('input[id=c' + rowid + ']').prop("checked", true);
//            oldSelID = rowid;
//        },

//        onPaging: function (pgButton) {
//            if (pgButton == "next_pager") {
//                if (curPage == $("#list").getGridParam("lastpage"))
//                    return;
//                curPage = $("#list").getGridParam("page") + 1;
//            }
//            else if (pgButton == "last_pager") {
//                curPage = $("#list").getGridParam("lastpage");
//            }
//            else if (pgButton == "prev_pager") {
//                if (curPage == 1)
//                    return;
//                curPage = $("#list").getGridParam("page") - 1;
//            }
//            else if (pgButton == "first_pager") {
//                curPage = 1;
//            }
//            else {
//                curPage = $("#pager :input").val();
//            }
//            reload();
//        },
//        loadComplete: function () {
//            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
//            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
//        }, onSortCol: function (index, iCol, sortorder) {
//            OrderDate = index + "@" + sortorder;
//            $("#Order").val(OrderDate);
//            reload();
//        }
//    });
//}

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