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

            var msg = "您真的确定要删除吗?";
            if (confirm(msg) == true) {
                var DCID = Model.DCID;
                $.ajax({
                    type: "POST",
                    url: "DeCustomerSatisfactionSurvey",
                    data: { DCID: DCID },
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
        var rID = jQuery("#list").jqGrid('getRowData', rowid).DCID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).DCID;
            var url = "PrintCustomerSatisfactionSurvey?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:800px;dialogHeight:500px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})
function ScrapManagementOut() {

    window.parent.parent.OpenDialog("顾客满意度调查", "../CustomerService/AddCustomerSatisfactionSurvey", 800, 550);

}
function jq() {
    var DCID = $('#DCID').val();
    var OrderContent = $('#OrderContent').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();

    jQuery("#list").jqGrid({
        url: 'CustomerSatisfactionSurveyList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, DCID: DCID,OrderContent:OrderContent, Begin: Begin, End: End },
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
        colNames: ['序号', '回访编号', '客户名称', '交货期调查结果', '售后维修,保养服务', '备品,备件供应', '有无漏气现象', '代理售后维修,保养服务', '代理咨询,维护培训', '代理备品,备件供应', '登记人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'DCID', index: 'DCID', width: 60, align: "center" },
        { name: 'Customer', index: 'Customer', width: 80, align: "center" },
        { name: 'ProductDelivery', index: 'ProductDelivery', width: 80, align: "center" },
        { name: 'CustomerServiceSurvey', index: 'CustomerServiceSurvey', width: 120, align: "center" },
        { name: 'SupplySurvey', index: 'SupplySurvey', width: 120, align: "center" },
        { name: 'LeakSurvey', index: 'LeakSurvey', width: 80, align: "center" },
        { name: 'AgencySales', index: 'AgencySales', width: 150, align: "center" },
        { name: 'AgencyConsultation', index: 'AgencyConsultation', width: 120, align: "center" },
        { name: 'AgencySpareParts', index: 'AgencySpareParts', width: 120, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 50, align: "center" }
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

                var DCID = Model.DCID;
                reload1(DCID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 350, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    var DCID = $('#DCID').val();
    // var OrderContent = $('#OrderContent').val();
    var SpecsModels = $("#SpecsModels").val();
    var ReturnVisit = $("#ReturnVisit").val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();

    $("#list").jqGrid('setGridParam', {
        url: 'CustomerSatisfactionSurveyList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, DCID: DCID, ReturnVisit: ReturnVisit, SpecsModels: SpecsModels, Begin: Begin, End: End },

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
    var DCID = $('#DCID').val();
    var OrderContent = $("#OrderContent").val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();

    $("#list").jqGrid('setGridParam', {
        url: 'CustomerSatisfactionSurveyList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, DCID: DCID, OrderContent: OrderContent, Begin: Begin, End: End },
        loadonce: false
    }).trigger("reloadGrid");//重新载入
}

function jq1(DCID) {

    jQuery("#list1").jqGrid({
        url: 'UserCustomerSatisfactionSurveyList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, DCID: DCID },
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
        colNames: ['客户名称', '客户地址', '调查人', '产品调查说明原因', '服务调查说明原因', '代理调查说明原因', '备注'],
        colModel: [
        { name: 'Customer', index: 'Customer', width: 50, align: "center" },
        { name: 'ComAddress', index: 'ComAddress', width: 200, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 200, align: "center" },
        { name: 'ProductSurvey', index: 'ProductSurvey', width: 200, align: "center" },
        { name: 'ServiceSurvey', index: 'ServiceSurvey', width: 200, align: "center" },
        { name: 'AgencySurvey', index: 'AgencySurvey', width: 200, align: "center" },
        { name: 'Remark', index: 'Remark', width: 200, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '其他信息',

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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() - 400, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}

function reload1(DCID) {

    var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'UserCustomerSatisfactionSurveyList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, DCID: DCID },

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
        window.parent.parent.OpenDialog("修改顾客满意度调查", "../CustomerService/UpCustomerSatisfactionSurveyList?DCID=" + Model.DCID, 800, 550);
    }
}


