var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ListOutID;
var curPage1 = 1;
var OnePageCount1 = 6;
var curPage2 = 1;
var OnePageCount2 = 4;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
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
                var BRDID = Model.BRDID;
                $.ajax({
                    type: "POST",
                    url: "DeBillingRecords",
                    data: { BRDID: BRDID },
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
        var rID = jQuery("#list").jqGrid('getRowData', rowid).BRDID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).BRDID;
            var url = "PrintBillingRecords?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})
function ScrapManagementOut() {
    window.parent.parent.OpenDialog("开票记录", "../CustomerService/AddBillingRecords", 800, 450);
}

function SKJL() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择操作的行");
        return;
    }
    var BRDID = Model.BRDID;
    $.ajax({
        type: "POST",
        url: "GetKPJL",
        data: { BRDID: BRDID },
        success: function (data) {
            if (data.Msg == "已开票") {
                alert(data.Msg);
            } else {
                $.ajax({
                    type: "POST",
                    url: "GetPDSP",
                    data: { BRDID: BRDID },
                    success: function (data) {
                       if (data.Msg == "待审批" || data.Msg == "未提交审批") {
                            alert(data.Msg);
                        } else {
                            window.parent.parent.OpenDialog("收款记录", "../CustomerService/AddCollectionRecord?BRDID=" + Model.BRDID, 800, 450);
                        }
                    },
                    dataType: 'json'
                });
            }
        },
        dataType: 'json'
    });

}
function ScrapManagementUP() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择操作的行");
        return;
    }
    window.parent.parent.OpenDialog("修改开票记录", "../CustomerService/UpBillingRecords?BRDID=" + Model.BRDID, 800, 450);
}

function jq() {
    var PersonCharge = $('#PersonCharge').val();
    var DwName = $('#DwName').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    jQuery("#list").jqGrid({
        url: 'BillingRecordsList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PersonCharge: PersonCharge, DwName: DwName, Begin: Begin, End: End },
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
        colNames: ['编号', '时间', '单位名称', '项目', '金额', '负责人', '收款日期', '支付方式', '备注', ''],
        colModel: [
        { name: 'BRDID', index: 'BRDID', width: 120, align: "center" },
        { name: 'BRDTime', index: 'BRDTime', width: 120, align: "center" },
        { name: 'DwName', index: 'DwName', width: 120, align: "center" },
        { name: 'Project', index: 'Project', width: 150, align: "center" },
        { name: 'Amount', index: 'Amount', width: 120, align: "center" },
        { name: 'PersonCharge', index: 'PersonCharge', width: 120, align: "center" },
        { name: 'ReceivablesTime', index: 'ReceivablesTime', width: 120, align: "center" },
        { name: 'PaymentMethod', index: 'PaymentMethod', width: 150, align: "center" },
        { name: 'Remark', index: 'Remark', width: 150, align: "center" },
        { name: 'PID', index: 'PID', width: 80, align: "center", hidden: true }

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
                var PID = Model.PID;
                reload2(PID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()/2 - 130, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    var PersonCharge = $('#PersonCharge').val();
    var DwName = $('#DwName').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    $("#list").jqGrid('setGridParam', {
        url: 'BillingRecordsList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PersonCharge: PersonCharge, DwName: DwName, Begin: Begin, End: End },
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
    var PersonCharge = $('#PersonCharge').val();
    var DwName = $('#DwName').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    $("#list").jqGrid('setGridParam', {
        url: 'BillingRecordsList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PersonCharge: PersonCharge, DwName: DwName, Begin: Begin, End: End },
        loadonce: false
    }).trigger("reloadGrid");//重新载入

}


function jq2(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list2").jqGrid({
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: jhid, webkey: webkey, folderBack: folderBack },
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
        colNames: ['', '职务', '姓名', '审批方式', '人数', '审批情况', '审批意见', '审批时间', '备注'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'Job', index: 'Job', width: 100, align: "center" },
        { name: 'UserName', index: 'UserName', width: 100, align: "center" },
        { name: 'AppTypeDesc', index: 'AppTypeDesc', width: 100, align: "center" },
        { name: 'Num', index: 'Num', width: 100, align: "center" },
        { name: 'stateDesc', index: 'stateDesc', width: 100, align: "center" },
        { name: 'Opinion', index: 'Opinion', width: $("#bor").width() - 920, align: "center" },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 150, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'Remark', index: 'Remark', width: 200, align: "center" },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list2").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list2").jqGrid('getRowData', id);

            }

        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage2 == $("#list2").getGridParam("lastpage2"))
                    return;
                curPage2 = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage2 = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage2 == 1)
                    return;
                curPage2 = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage2 = 1;
            }
            else {
                curPage2 = $("#pager2 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list2").jqGrid("setGridWidth", $("#bor2").width() - 30, false);
        }
    });
}

function reload2(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list2").jqGrid('setGridParam', {
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, PID: jhid, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}

function ProcessingRecordApproval() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    var BRDID = Model.BRDID;
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        $.ajax({
            url: "GetBasBillingRecords",
            type: "post",
            data: { BRDID: BRDID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        if (json[i].State == "0") {
                            var texts = Model.BRDID + "@" + "售后开票记录审批";
                            window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
                        } else {
                            alert(json[i].State == "1" ? "待审批" : "已审批");
                            return;
                        }
                    }
                }
            }
        })
    }

}
