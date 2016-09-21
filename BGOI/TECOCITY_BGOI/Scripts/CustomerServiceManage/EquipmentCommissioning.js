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
    jq2("");
    $("#tcxx").click(function () {
        $("#tcxx").show();
        $("#bor2").show();
        $("#bor1").hide();
    });
    $("#cljl").click(function () {
        $("#cljl").show();
        $("#bor1").show();
        $("#bor2").hide();
    });
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
                var TRID = Model.TRID;
                $.ajax({
                    type: "POST",
                    url: "DeEquipmentCommissioning",
                    data: { TRID: TRID },
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
        var rID = jQuery("#list").jqGrid('getRowData', rowid).TRID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).TRID;
            var url = "PrintEquipmentCommissioning?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
    // 打印派工单
    $("#btnPrint1").click(function () {
        var rowid = $("#list2").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list2").jqGrid('getRowData', rowid).BZID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list2").jqGrid('getRowData', rowid).BZID;
            var TRID = jQuery("#list").jqGrid('getRowData', rowid).TRID;
            var url = "PrintProductReport?Info='" + escape(texts) + "'&type=5&TRID="+TRID+"" ;
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})
function ScrapManagementOut() {
    window.parent.parent.OpenDialog("设备调试任务单", "../CustomerService/AddEquipmentCommissioning", 800, 550);
}
function ScrapManagementUP() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择操作的行");
        return;
    }
    window.parent.parent.OpenDialog("修改设备调试任务单", "../CustomerService/UpEquipmentCommissioning?TRID=" + Model.TRID, 800, 550);
}
function jq() {
    var UserName = $('#UserName').val();
    var Tel = $('#Tel').val();
    var Spec = $('#Spec').val();
    var PID = $('#PID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    jQuery("#list").jqGrid({
        url: 'EquipmentCommissioningList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, UserName: UserName, Tel: Tel,Spec:Spec,PID:PID, Begin: Begin, End: End },
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
        colNames: ['序号', '调试任务编号', '用户名称', '用户地址', '联系人', '联系电话', '施工单位', '联系人', '联系电话',
             '单位', '联系人', '联系电话','设备管理方式', '气种', '用户类别 ', '调试人员 ', '调试日期', '备注', '记录人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'TRID', index: 'TRID', width: 120, align: "center" },
        { name: 'UserName', index: 'UserName', width: 120, align: "center" },
        { name: 'Address', index: 'Address', width: 120, align: "center" },
        { name: 'ContactPerson', index: 'ContactPerson', width: 150, align: "center" },
        { name: 'Tel', index: 'Tel', width: 100, align: "center" },
        { name: 'ConstructionUnit', index: 'ConstructionUnit', width: 80, align: "center" },
        { name: 'CUnitPer', index: 'CUnitPer', width: 80, align: "center" },
        { name: 'CUnitTel', index: 'CUnitTel', width: 80, align: "center" },
        { name: 'UnitName', index: 'UnitName', width: 100, align: "center" },
        { name: 'UnitTel', index: 'UnitTel', width: 80, align: "center" },
        { name: 'UnitPer', index: 'UnitPer', width: 80, align: "center" },
        { name: 'EquManType', index: 'EquManType', width: 100, align: "center" },
        //{ name: 'ProductForm', index: 'ProductForm', width: 80, align: "center" }, '设备形式',
        { name: 'Gas', index: 'Gas', width: 80, align: "center" },
        { name: 'UserType', index: 'UserType', width: 80, align: "center" },
        { name: 'DebPerson', index: 'DebPerson', width: 100, align: "center" },
        { name: 'DebTime', index: 'DebTime', width: 80, align: "center" },
        { name: 'Remark', index: 'Remark', width: 80, align: "center" },
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
                var TRID = Model.TRID;
                reload1(TRID);
                reload2(TRID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()/2 - 185, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function reload() {
    var TYID = $('#TYID').val();
    var InspectionPersonnel = $('#InspectionPersonnel').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    //var State = $("input[name='State']:checked").val();
    $("#list").jqGrid('setGridParam', {
        url: 'EquipmentCommissioningList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, TYID: TYID, InspectionPersonnel: InspectionPersonnel, Begin: Begin, End: End },
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
    var Tel = $('#Tel').val();
    var Spec = $('#Spec').val();
    var PID = $('#PID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    //var State = $("input[name='State']:checked").val();
    //if (State == "1") {
    //    $('#Fin').hide();
    //} else {
    //    $('#Fin').show();
    //}
    $("#list").jqGrid('setGridParam', {
        url: 'EquipmentCommissioningList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, UserName: UserName, Tel: Tel, Spec: Spec, PID: PID, Begin: Begin, End: End },
        loadonce: false
    }).trigger("reloadGrid");//重新载入

}
function jq1(TRID) {

    jQuery("#list1").jqGrid({
        url: 'EquipmentCommissioningDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TRID: TRID },
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
        colNames: ['序号', '产品编号', '产品名称', '规格型号', '上台编号', '上台生成日期', '上台初始P ', '下台编号','下台生成日期', '下台初始P ', '进口压力P1', '放散压力P1 ','上台（高台）出口压力P2', '上台（高台）关闭压力Pb ', '上台（高台）切断压力Pq ','下台（低台）出口压力P2', '下台（低台）关闭压力Pb ', '下台（低台）切断压力Pq '],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'PID', index: 'PID', width: 100, align: "center" },
        { name: 'ProName', index: 'ProName', width: 120, align: "center" },
        { name: 'Spec', index: 'Spec', width: 120, align: "center" },
        { name: 'PowerNumber', index: 'PowerNumber', width: 80, align: "center" },
        { name: 'PowerTime', index: 'PowerTime', width: 80, align: "center" },
        { name: 'PowerInitialP', index: 'PowerInitialP', width: 80, align: "center" },
        { name: 'StepDownNumber', index: 'StepDownNumber', width: 120, align: "center" },
        { name: 'StepDownTime', index: 'StepDownTime', width: 120, align: "center" },
        { name: 'StepDownInitialP', index: 'StepDownInitialP', width: 50, align: "center" },
        { name: 'InletPreP1', index: 'InletPreP1', width: 100, align: "center" },
        { name: 'BleedingpreP1', index: 'BleedingpreP1', width: 120, align: "center" },
        { name: 'PowerExportPreP2', index: 'PowerExportPreP2', width: 120, align: "center" },
        { name: 'PowerOffPrePb', index: 'PowerOffPrePb', width: 80, align: "center" },
        { name: 'PowerCutOffPrePq', index: 'PowerCutOffPrePq', width: 80, align: "center" },
        { name: 'SDExportPreP2', index: 'SDExportPreP2', width: 80, align: "center" },
        { name: 'SDPowerOffPrePb', index: 'SDPowerOffPrePb', width: 120, align: "center" },
        { name: 'SDCutOffPrePq', index: 'SDCutOffPrePq', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '基本详细',
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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height()/2 - 160, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}
function reload1(TRID) {
    var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'EquipmentCommissioningDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TRID: TRID },

    }).trigger("reloadGrid");
}
function AddBuyGiveOut() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    var TRID = Model.TRID;
    var UserName = Model.UserName;
    var Tel = Model.Tel;
    var Address = Model.Address;
    if (ids == null) {
        alert("请选择操作的行");
        return;
    } else {

        $.ajax({
            url: "PanDuanIfPro",
            type: "Post",
            data: {
                TRID: TRID
            },
            async: false,
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    alert("已派工！");
                    return;
                } else {
                    window.parent.parent.OpenDialog("派工单", "../CustomerService/AddProductReport?type=" + Model.TRID + "&UserName=" + Model.UserName + "&Tel=" + Model.Tel + "&Address=" + Model.Address + "", 800, 550);
                }
            }
        });

       
    }
}

function reload2(TRID) {
    var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#list2").jqGrid('setGridParam', {
        url: 'EqProductReportList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TRID: TRID },

    }).trigger("reloadGrid");
}
function jq2(TRID) {
    jQuery("#list2").jqGrid({
        url: 'EqProductReportList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TRID: TRID },
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
        colNames: ['序号', '报装编号', '报装时间', '用户姓名', '联系方式', '地址', '出库库房', '派工人员','备注', '状态', '记录人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'BZID', index: 'BZID', width: 150, align: "center" },
        { name: 'InstallTime', index: 'InstallTime', width: 150, align: "center" },
        { name: 'CustomerName', index: 'CustomerName', width: 150, align: "center" },
        { name: 'Tel', index: 'Tel', width: 100, align: "center" },
        { name: 'Address', index: 'Address', width: 100, align: "center" },
        { name: 'HouseName', index: 'HouseName', width: 80, align: "center" },
        { name: 'DiPer', index: 'DiPer', width: 80, align: "center" },
        { name: 'Remark', index: 'Remark', width: 80, align: "center" },
        { name: 'State', index: 'State', width: 80, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 80, align: "center" }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '派工单',
        gridComplete: function () {
            var ids = jQuery("#list2").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list2").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list2").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list2").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
                if (curPage == $("#list2").getGridParam("lastpage"))
                    return;
                curPage = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height()/2 - 160, false);
            $("#list2").jqGrid("setGridWidth", $("#bor2").width() - 30, false);
        }
    });
}
