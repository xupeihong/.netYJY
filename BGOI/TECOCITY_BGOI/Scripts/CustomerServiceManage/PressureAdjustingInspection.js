var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ListOutID;
var curPage1 = 1;
var OnePageCount1 = 6;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    jq1("");
    $("#De").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var msg = "您真的确定要撤销吗?";
            if (confirm(msg) == true) {
                var TYID = Model.TYID;
                $.ajax({
                    type: "POST",
                    url: "DePressureAdjustingInspection",
                    data: { TYID: TYID },
                    success: function (data) {
                        alert(data.Msg);
                        reload();
                    },
                    dataType: 'json'
                });
                return true;
            } else {
                return false;
            }
        }
    });
    // 打印
    $("#btnPrint").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).TYID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).TYID;
            var url = "PrintPressureAdjustingInspection?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})

function ScrapManagementOut() {
    window.parent.parent.OpenDialog("调压箱巡检", "../CustomerService/AddPressureAdjustingInspection", 950, 550);
}
function ScrapManagementUP() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择操作的行");
        return;
    }
    window.parent.parent.OpenDialog("修改调压箱巡检", "../CustomerService/UpPressureAdjustingInspection?TYID=" + Model.TYID, 950, 550);
}

function jq() {
    var UserName = $('#UserName').val();
    var Tel = $("#Tel").val();
    var InspectionPersonnel = $('#InspectionPersonnel').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    //var State = $("input[name='State']:checked").val();
    jQuery("#list").jqGrid({
        url: 'PressureAdjustingInspectionList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, UserName: UserName, InspectionPersonnel: InspectionPersonnel, Begin: Begin, End: End, Tel: Tel },
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
        colNames: ['序号', '调压巡检编号', '运维时间', '巡检人员', '用户名称', '联系人', '电话', '用途', '记录人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'TYID', index: 'TYID', width: 120, align: "center" },
        { name: 'OperationTime', index: 'OperationTime', width: 120, align: "center" },
        { name: 'InspectionPersonnel', index: 'InspectionPersonnel', width: 150, align: "center" },
        { name: 'UserName', index: 'UserName', width: 100, align: "center" },
        { name: 'Users', index: 'Users', width: 80, align: "center" },
        { name: 'Tel', index: 'Tel', width: 100, align: "center" },
        //{ name: 'KeyStorageUnitJia', index: 'KeyStorageUnitJia', width: 80, align: "center" },
        //{ name: 'KeyStorageUnitYi', index: 'KeyStorageUnitYi', width: 80, align: "center" },
         { name: 'Uses', index: 'Uses', width: 100, align: "center" },
        //{ name: 'Boiler', index: 'Boiler', width: 80, align: "center" }, '锅炉',
        //{ name: 'KungFu', index: 'KungFu', width: 80, align: "center" }, '公福',
        //{ name: 'Civil', index: 'Civil', width: 80, align: "center" }, '民用',
        { name: 'CreateUser', index: 'CreateUser', width: 80, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        gridComplete: function () {
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {

                return;
            }
            else {
                var TYID = Model.TYID;
                reload1(TYID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    var UserName = $('#UserName').val();
    var Tel = $("#Tel").val();
    var InspectionPersonnel = $('#InspectionPersonnel').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    //var State = $("input[name='State']:checked").val();
    $("#list").jqGrid('setGridParam', {
        url: 'PressureAdjustingInspectionList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, UserName: UserName, InspectionPersonnel: InspectionPersonnel, Begin: Begin, End: End, Tel: Tel },
    }).trigger("reloadGrid");
}

//查询
function SearchOut() {
    var strDateStart = $('#Begin').val();
    var strDateEnd = $('#End').val();
    if (strDateStart == "" && strDateEnd == "") {
        getSearch();
    }
    else {
        var strSeparator = "-"; //日期分隔符
        var strDateArrayStart;
        var strDateArrayEnd;
        var intDay;
        strDateArrayStart = strDateStart.split(strSeparator);
        strDateArrayEnd = strDateEnd.split(strSeparator);
        var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
        var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);
        if (strDateS <= strDateE) {
            getSearch();
        }
        else {
            alert("截止日期不能小于开始日期！");
            $("#End").val("");
            return false;
        }
    }
}

function getSearch() {
    curRow = 0;
    curPage = 1;
    var UserName = $('#UserName').val();
    var Tel = $("#Tel").val();
    var InspectionPersonnel = $('#InspectionPersonnel').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    //var State = $("input[name='State']:checked").val();
    //if (State == "1") {
    //    $('#Fin').hide();
    //} else {
    //    $('#Fin').show();
    //}
    $("#list").jqGrid('setGridParam', {
        url: 'PressureAdjustingInspectionList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, UserName: UserName, InspectionPersonnel: InspectionPersonnel, Begin: Begin, End: End, Tel: Tel },
        loadonce: false
    }).trigger("reloadGrid");//重新载入

}
function jq1(TYID) {
    jQuery("#list1").jqGrid({
        url: 'PressureAdjustingInspectionDetailList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TYID: TYID },
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
        colNames: ['序号', '上台 P1', '上台 P2', '上台 Pb', '下台 P1', '下台 P2', '下台 Pb', 'Pf'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'UsePressureShangP1', index: 'UsePressureShangP1', width: 100, align: "center" },
        { name: 'UsePressureShangP2', index: 'UsePressureShangP2', width: 120, align: "center" },
        { name: 'UsePressureShangPb', index: 'UsePressureShangPb', width: 120, align: "center" },
        { name: 'UsePressureXiaP1', index: 'UsePressureXiaP1', width: 80, align: "center" },
        { name: 'UsePressureXiaP2', index: 'UsePressureXiaP2', width: 80, align: "center" },
        { name: 'UsePressureXiaPb', index: 'UsePressureXiaPb', width: 80, align: "center" },
        { name: 'UsePressureShangPf', index: 'UsePressureShangPf', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '使用压力',
        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
                if (curPage == $("#list1").getGridParam("lastpage"))
                    return;
                curPage = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager1 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}

function reload1(TYID) {
    var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'PressureAdjustingInspectionDetailList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TYID: TYID },

    }).trigger("reloadGrid");
}

