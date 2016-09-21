var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ListOutID;
var curPage1 = 1;
var OnePageCount1 =4;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    $("#bxxx").click(function () {
        $("#bxxx").show();
        $("#bor1").show();
        $("#bor2").hide();
        $("#bor3").hide();
    });
    $("#wxxx").click(function () {
        $("#wxxx").show();
        $("#bor2").show();
        $("#bor1").hide();
        $("#bor3").hide();
    });
    $("#ghljxx").click(function () {
        $("#ghljxx").show();
        $("#bor3").show();
        $("#bor2").hide();
        $("#bor1").hide();
    });
    jq();
    jq1("");
    jq2("");
    jq3("");
    $("#WC").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var msg = "确定要完成维修吗?";
            if (confirm(msg) == true) {
                var BXID = Model.BXID;
                $.ajax({
                    type: "POST",
                    url: "CompleteMaintenanceTask",
                    data: { BXID: BXID },
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
                var BXID = Model.BXID;
                $.ajax({
                    type: "POST",
                    url: "DeMaintenanceTask",
                    data: { BXID: BXID },
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
        var type = 2;
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).BXID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            //Maintenance task print
            var texts = jQuery("#list").jqGrid('getRowData', rowid).BXID;
            var url = "PrintMaintenanceTaskList?Info='"+escape(texts)+"'&type=" +type ;
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})
function RepairRegistrationClick() {
    window.parent.parent.OpenDialog("报修登记", "../CustomerService/AddRepairRegistration", 800, 550);
}
function ProcessingRecord() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    var BXID = Model.BXID
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {

        $.ajax({
            type: "POST",
            url: "WXMaintenanceTask",
            data: { BXID: BXID },
            success: function (data) {
                if (data.Msg == "空") {
                    window.parent.parent.OpenDialog("维修记录", "../CustomerService/UpMaintenanceTask?BXID=" + Model.BXID, 800, 450);
                } else {
                    alert("已添加维修记录");
                }
              
            },
            dataType: 'json'
        });
    }
}
function jq() {
    var DeviceID = "";// $('#DeviceID').val();
    var OrderContent = $('#OrderContent').val();
    var UserName = "";// $('#UserName').val();

    var RepairDateBegin = $('#RepairDateBegin').val();
    var RepairDateEnd = $('#RepairDateEnd').val();

    var MaintenanceTimeBegin = $('#MaintenanceTimeBegin').val();
    var MaintenanceTimeEnd = $("#MaintenanceTimeEnd").val();

    var OrderContent1 = $('#OrderContent option:selected').text();
    UserName1 = $('#UserName option:selected').text();

    var Sate = $("input[name='Sate']:checked").val();


    jQuery("#list").jqGrid({
        url: 'MaintenanceTaskList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, DeviceID: DeviceID, OrderContent: OrderContent, UserName: UserName, RepairDateBegin: RepairDateBegin, RepairDateEnd: RepairDateEnd, MaintenanceTimeBegin: MaintenanceTimeBegin, MaintenanceTimeEnd: MaintenanceTimeEnd, Sate: Sate },
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
        colNames: ['序号', '报装编码', '报装编号', '报修日期', '客户名称', '联系人', '电话', '产品名称', '报修简述',  '状态', '备注', '记录人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'BXID', index: 'BXID', width: 200, align: "center" },
        { name: 'RepairID', index: 'RepairID', width: 200, align: "center" },
        { name: 'RepairDate', index: 'RepairDate', width: 200, align: "center" },
        { name: 'Customer', index: 'Customer', width: 80, align: "center" },
        { name: 'ContactName', index: 'ContactName', width: 80, align: "center" },
        { name: 'Tel', index: 'Tel', width: 120, align: "center" },
        { name: 'DeviceType', index: 'DeviceType', width: 120, align: "center" },
        { name: 'RepairTheUser', index: 'RepairTheUser', width: 80, align: "center" },
        //{ name: 'CollectionTime', index: 'CollectionTime', width: 120, align: "center" }, '收款时间',
        { name: 'Sate', index: 'Sate', width: 150, align: "center" },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 120, align: "center" }
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
                var BXID = Model.BXID;
                reload1(BXID);
                reload2(BXID);
                reload3(BXID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()/2 - 135, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function reload() {
    var DeviceID = "";// $('#DeviceID').val();
    var OrderContent = $('#OrderContent').val();
    var UserName = "";// $('#UserName').val();

    var RecordDateBegin = $('#RecordDateBegin').val();
    var RecordDateEnd = $('#RecordDateEnd').val();

    var MaintenanceTimeBegin = $('#MaintenanceTimeBegin').val();
    var MaintenanceTimeEnd = $("#MaintenanceTimeEnd").val();

    var OrderContent1 = $('#OrderContent option:selected').text();
    UserName1 = $('#UserName option:selected').text();

    var Sate = $("input[name='Sate']:checked").val();
    $("#list").jqGrid('setGridParam', {
        url: 'MaintenanceTaskList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, DeviceID: DeviceID, OrderContent: OrderContent, UserName: UserName, RecordDateBegin: RecordDateBegin, RecordDateEnd: RecordDateEnd, MaintenanceTimeBegin: MaintenanceTimeBegin, MaintenanceTimeEnd: MaintenanceTimeEnd, Sate: Sate },
    }).trigger("reloadGrid");
}
//查询
function SearchOut() {
    var strDateStart = $('#RepairDateBegin').val();
    var strDateEnd = $('#RepairDateEnd').val();
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
            $("#RepairDateEnd").val("");
            return false;
        }
    }
}
function getSearch() {
    curRow = 0;
    curPage = 1;
    var DeviceID = "";// $('#DeviceID').val();
    var OrderContent = $('#OrderContent').val();
    var UserName = "";// $('#UserName').val();

    var RepairDateBegin = $('#RepairDateBegin').val();
    var RepairDateEnd = $('#RepairDateEnd').val();


    var MaintenanceTimeBegin = $('#MaintenanceTimeBegin').val();
    var MaintenanceTimeEnd = $('#MaintenanceTimeEnd').val();

    var Sate = $("input[name='Sate']:checked").val();
    if (Sate == "1") {
        $("#WX").hide();
        $("#WC").hide();
        $("#Up").hide();
        $("#De").hide();
    } else {
        $("#WX").show();
        $("#WC").show();
        $("#Up").show();
        $("#De").show();
    }
    $("#list").jqGrid('setGridParam', {
        url: 'MaintenanceTaskList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, DeviceID: DeviceID, OrderContent: OrderContent, UserName: UserName,
            RepairDateBegin: RepairDateBegin, RepairDateEnd: RepairDateEnd, Sate: Sate,
            MaintenanceTimeBegin: MaintenanceTimeBegin, MaintenanceTimeEnd: MaintenanceTimeEnd
        },
        loadonce: false
    }).trigger("reloadGrid");//重新载入
}
function jq1(BXID) {
    jQuery("#list1").jqGrid({
        url: 'UserMaintenanceTaskList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, BXID: BXID },
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
        colNames: ['用户名称', '地址', '产品型号', '保修期', '用户报修简述', '联系人', '产品编号', '保修卡编号', '记录人', '电话', '启用日期', '报修日期', '备注'],
        colModel: [
        { name: 'Customer', index: 'Customer', width: 200, align: "center" },
        { name: 'Address', index: 'Address', width: 200, align: "center" },
        { name: 'DeviceType', index: 'DeviceType', width: 200, align: "center" },
        { name: 'GuaranteePeriod', index: 'GuaranteePeriod', width: 200, align: "center" },
        { name: 'RepairTheUser', index: 'RepairTheUser', width: 200, align: "center" },
        { name: 'ContactName', index: 'ContactName', width: 200, align: "center" },
        { name: 'DeviceID', index: 'DeviceID', width: 200, align: "center" },//PID
        { name: 'BXKNum', index: 'BXKNum', width: 200, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 200, align: "center" },
        { name: 'Tel', index: 'Tel', width: 200, align: "center" },
        { name: 'EnableDate', index: 'EnableDate', width: 200, align: "center" },
        { name: 'RepairDate', index: 'RepairDate', width: 200, align: "center" },
        { name: 'Remark', index: 'Remark', width: 200, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '报修信息',
        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).BXID + "' name='cb'/>";
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
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height()/2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}
function reload1(BXID) {
    $("#list1").jqGrid('setGridParam', {
        url: 'UserMaintenanceTaskList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, BXID: BXID },

    }).trigger("reloadGrid");
}
function jq2(BXID) {
    jQuery("#list2").jqGrid({
        url: 'UserMaintenanceTaskTwoList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, BXID: BXID },
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
        colNames: ['维修日期', '维修车辆', '维修人员', '总计金额', '最终处理结果', '人工费', '材料费', '其他费用', '用户签名姓名', '用户意见','收款日期'],
        colModel: [
        { name: 'MaintenanceTime', index: 'MaintenanceTime', width: 200, align: "center" },
        { name: 'MaintenanceVehicle', index: 'MaintenanceVehicle', width: 200, align: "center" },
        { name: 'MaintenanceName', index: 'MaintenanceName', width: 200, align: "center" },
        { name: 'Total', index: 'Total', width: 200, align: "center" },
        { name: 'FinalResults', index: 'FinalResults', width: 200, align: "center" },
        { name: 'ArtificialCost', index: 'ArtificialCost', width: 200, align: "center" },
        { name: 'MaterialCost', index: 'MaterialCost', width: 200, align: "center" },
        { name: 'OtherCost', index: 'OtherCost', width: 200, align: "center" },
        { name: 'SignatureName', index: 'SignatureName', width: 200, align: "center" },
        { name: 'Sate', index: 'Sate', width: 200, align: "center" },
        { name: 'CollectionTime', index: 'CollectionTime', width: 200, align: "center" }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '维修信息',
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
            if (pgButton == "next_pager2") {
                if (curPage1 == $("#list2").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage1 = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager2 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height()/2 - 180, false);
            $("#list2").jqGrid("setGridWidth", $("#bor2").width() - 30, false);
        }
    })
}
function reload2(BXID) {
   
    $("#list2").jqGrid('setGridParam', {
        url: 'UserMaintenanceTaskTwoList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, BXID: BXID },

    }).trigger("reloadGrid");
}
function jq3(BXID) {
    jQuery("#list3").jqGrid({
        url: 'UserMaintenanceTaskThreeList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, BXID: BXID },
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
        colNames: ['维修单编号', '编号', '零件名称', '单价', '数量', '合计'],
        colModel: [
        { name: 'WXID', index: 'WXID', width: 200, align: "center" },
        { name: 'DID', index: 'DID', width: 200, align: "center" },
        { name: 'Lname', index: 'Lname', width: 200, align: "center" },
        { name: 'UnitPrice', index: 'UnitPrice', width: 200, align: "center" },
        { name: 'Amount', index: 'Amount', width: 200, align: "center" },
        { name: 'Total', index: 'Total', width: 200, align: "center" }
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '更换零件信息',
        gridComplete: function () {
            var ids = jQuery("#list3").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list3").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list3").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list3").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            if (pgButton == "next_pager3") {
                if (curPage1 == $("#list3").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPage1 = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager3 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height()/2 - 180, false);
            $("#list3").jqGrid("setGridWidth", $("#bor3").width() - 30, false);
        }
    })
}
function reload3(BXID) {
    $("#list3").jqGrid('setGridParam', {
        url: 'UserMaintenanceTaskThreeList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, BXID: BXID },

    }).trigger("reloadGrid");
}
//修改
function ScrapManagementUP() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    //alert(Model.BXID);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        window.parent.parent.OpenDialog("修改报修登记", "../CustomerService/UpDateModifyTask?BXID=" + Model.BXID, 800, 550);
    }
}



