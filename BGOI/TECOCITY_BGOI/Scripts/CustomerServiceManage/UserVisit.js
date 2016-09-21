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
    jq();
    jq1("");
    jq2("");
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
                var HFID = Model.HFID;
                $.ajax({
                    type: "POST",
                    url: "DeUserVisit",
                    data: { HFID: HFID },
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
        var rID = jQuery("#list").jqGrid('getRowData', rowid).HFID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).HFID;
            var url = "PrintUserVisit?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
    // 打印
    $("#btnPrint2").click(function () {
        var rowid = $("#list2").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list2").jqGrid('getRowData', rowid).HFIDID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list2").jqGrid('getRowData', rowid).HFIDID;
            var url = "PrintCustomerSatisfactionSurvey?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:800px;dialogHeight:500px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
   
})
function ScrapManagementOut() {
    window.parent.parent.OpenDialog("回访登记", "../CustomerService/AddUserVisit", 800, 550);
}
function jq() {
    var HFID = $("#HFID").val();
    var ReturnVisit = $("#ReturnVisit").val();
    var Begin = $("#Begin").val();
    var End = $("#End").val();
    var Tel = $("#Tel").val();

    jQuery("#list").jqGrid({
        url: 'UserVisitList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, HFID: HFID, ReturnVisit: ReturnVisit, Begin: Begin, End: End, Tel: Tel },
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
        colNames: ['序号', '回访编号(系统)','回访编号（手动）', '用户情况描述', '联系人', '电话', '对此次服务满意度', '回访人', '回访日期', '记录人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'HFID', index: 'HFID', width: 150, align: "center" },
        { name: 'RecordID', index: 'RecordID', width: 150, align: "center" },
        { name: 'UserInformation', index: 'UserInformation', width: 150, align: "center" },
        { name: 'ContactPerson', index: 'ContactPerson', width: 100, align: "center" },
        { name: 'Tel', index: 'Tel', width: 100, align: "center" },
        { name: 'SatisfiedDegree', index: 'SatisfiedDegree', width: 80, align: "center" },
        { name: 'ReturnVisit', index: 'ReturnVisit', width: 100, align: "center" },
        { name: 'RVDate', index: 'RVDate', width: 100, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 80, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        //gridComplete: function () {

        //},
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
                var HFID = Model.HFID;
                reload1(HFID);
                reload2(HFID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 145, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function reload2(HFID) {
    var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#list2").jqGrid('setGridParam', {
        url: 'CustomerSatisfactionSurveyList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, HFID: HFID },

    }).trigger("reloadGrid");
}
function jq2(HFID) {
    jQuery("#list2").jqGrid({
        url: 'CustomerSatisfactionSurveyList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, HFID: HFID },
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
        colNames: ['序号', '回访编号', '客户名称', '产品质量',  '产品价格',
            '产品交货期', '售后维修,保养服务', '备品,备件供应', '有无漏气现象',
            '代理售后维修,保养服务', '代理咨询,维护培训', '代理备品,备件供应', '登记人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'HFIDID', index: 'HFIDID', width: 60, align: "center" },
        { name: 'Customer', index: 'Customer', width: 80, align: "center" },
        { name: 'ProductQuality', index: 'ProductQuality', width: 80, align: "center" },
        { name: 'ProductQrice', index: 'ProductQrice', width: 80, align: "center" },
        { name: 'ProductDelivery', index: 'ProductDelivery', width: 80, align: "center" },


        { name: 'CustomerServiceSurvey', index: 'CustomerServiceSurvey', width: 120, align: "center" },
        { name: 'SupplySurvey', index: 'SupplySurvey', width: 120, align: "center" },
        { name: 'LeakSurvey', index: 'LeakSurvey', width: 80, align: "center" },


        { name: 'AgencySales', index: 'AgencySales', width: 150, align: "center" },
        { name: 'AgencyConsultation', index: 'AgencyConsultation', width: 120, align: "center" },
        { name: 'AgencySpareParts', index: 'AgencySpareParts', width: 120, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 50, align: "center" }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '顾客满意度',
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
            var dataSel = jQuery("#list2").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list2").jqGrid('getRowData', ids);
            if (ids == null) {
                return;
            }
            else {

                var HFIDID = Model.HFIDID;
            }
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
            reload2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list2").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}
function reload() {
    var HFID = $('#HFID').val();
    //var SpecsModels = $("#SpecsModels").val();
    //SpecsModels: SpecsModels,
    var ReturnVisit = $("#ReturnVisit").val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var Tel = $("#Tel").val();
    $("#list").jqGrid('setGridParam', {
        url: 'UserVisitList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, HFID: HFID, ReturnVisit: ReturnVisit, Begin: Begin, End: End, Tel: Tel },

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
    var HFID = $('#HFID').val();
    // var OrderContent = $('#OrderContent').val();
    // var SpecsModels = $("#SpecsModels").val();
    var ReturnVisit = $("#ReturnVisit").val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var Tel = $("#Tel").val();
    $("#list").jqGrid('setGridParam', {
        url: 'UserVisitList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, HFID: HFID, ReturnVisit: ReturnVisit, Begin: Begin, End: End, Tel: Tel },
        loadonce: false
    }).trigger("reloadGrid");//重新载入
}
function jq1(HFID) {

    jQuery("#list1").jqGrid({
        url: 'UserVisitDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, HFID: HFID },
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
        colNames: ['序号', '产品编号', '产品名称', '规格型号', '单位', '数量', '单价', '总价'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'PID', index: 'PID', width: 100, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 120, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 120, align: "center" },
        { name: 'Unit', index: 'Unit', width: 80, align: "center" },
        { name: 'Num', index: 'Num', width: 80, align: "center" },
        { name: 'UnitPrice', index: 'UnitPrice', width: 80, align: "center" },
        { name: 'Total', index: 'Total', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '产品信息',

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
function reload1(HFID) {
    var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'UserVisitDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, HFID: HFID },

    }).trigger("reloadGrid");
}
//修改
function ScrapManagementUP() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        window.parent.parent.OpenDialog("修改回访登记", "../CustomerService/UpUserVisitList?HFID=" + Model.HFID, 800, 550);
    }
}
function CustomerSatisfactionSurvey() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    var HFID = Model.HFID;
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        $.ajax({
            url: "IfGetDiaoCha",
            type: "post",
            data: { HFID: HFID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    alert("已调查，请选择其它行");
                } else {
                    window.parent.parent.OpenDialog("顾客满意度调查", "../CustomerService/AddCustomerSatisfactionSurvey?HFID=" + Model.HFID, 800, 550);
                }
            }
        })
        //window.parent.parent.OpenDialog("修改产品安装", "../CustomerService/UpUserVisitList?HFID=" + Model.HFID, 800, 550);
    }
}






