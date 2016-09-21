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
                var CRID = Model.CRID;
                $.ajax({
                    type: "POST",
                    url: "DeCollectionRecord",
                    data: { CRID: CRID },
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
        var rID = jQuery("#list").jqGrid('getRowData', rowid).CRID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).CRID;
            var url = "PrintCollectionRecord?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})

function ScrapManagementOut() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择操作的行");
        return;
    }
    var CRID = Model.CRID;
    $.ajax({
        type: "POST",
        url: "GetSKSP",
        data: { CRID: CRID },
        success: function (data) {
            if (data.Msg == "待审批" || data.Msg == "未提交审批") {
                alert(data.Msg);
            } else {
                $.ajax({
                    type: "POST",
                    url: "GetState",
                    data: { CRID: CRID },
                    success: function (data) {
                        alert(data.Msg);
                        reload();
                    },
                    dataType: 'json'
                });
            }
            //reload();
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
    window.parent.parent.OpenDialog("修改收票记录", "../CustomerService/UpCollectionRecord?CRID=" + Model.CRID, 800, 450);
}

function jq() {
    var PaymentPeo = $('#PaymentPeo').val();
    var PaymentUnit = $('#PaymentUnit').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $("input[name='State']:checked").val();
    jQuery("#list").jqGrid({
        url: 'CollectionRecordList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount,State:State, PaymentPeo: PaymentPeo, PaymentUnit: PaymentUnit, Begin: Begin, End: End},
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
        colNames: ['日期', '付款单位', '收款额', '付款内容', '付款方式', '收款人', '公司财务', '备注','编号','开票记录编号','状态',''],
        colModel: [
        { name: 'CRTime', index: 'CRTime', width: 120, align: "center" },
        { name: 'PaymentUnit', index: 'PaymentUnit', width: 120, align: "center" },
        { name: 'CollectionAmount', index: 'CollectionAmount', width: 120, align: "center" },
        { name: 'PaymentContent', index: 'PaymentContent', width: 150, align: "center" },
        { name: 'PaymentMethod', index: 'PaymentMethod', width: 120, align: "center" },
        { name: 'PaymentPeo', index: 'PaymentPeo', width: 120, align: "center" },
        { name: 'CorporateFinance', index: 'CorporateFinance', width: 120, align: "center" },
        { name: 'Remark', index: 'Remark', width: 150, align: "center" },
        { name: 'CRID', index: 'CRID', width: 150, align: "center" },
        { name: 'BRDID', index: 'BRDID', width: 150, align: "center" },
        { name: 'State', index: 'State', width: 150, align: "center" },
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()/2 -145, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    var PaymentPeo = $('#PaymentPeo').val();
    var PaymentUnit = $('#PaymentUnit').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $("input[name='State']:checked").val();
    $("#list").jqGrid('setGridParam', {
        url: 'CollectionRecordList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount,State:State, PaymentPeo: PaymentPeo, PaymentUnit: PaymentUnit, Begin: Begin, End: End },
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
    var State = $("input[name='State']:checked").val();
    if (State == "1") {
        $("#SKWC").hide();
        $("#De").hide();
        $("#Up").hide();
    } else {
        $("#SKWC").show();
        $("#De").show();
        $("#Up").show();
    }
    curRow = 0;
    curPage = 1;
    var PaymentPeo = $('#PaymentPeo').val();
    var PaymentUnit = $('#PaymentUnit').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $("input[name='State']:checked").val();
    $("#list").jqGrid('setGridParam', {
        url: 'CollectionRecordList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount,State:State, PaymentPeo: PaymentPeo, PaymentUnit: PaymentUnit, Begin: Begin, End: End },
        loadonce: false
    }).trigger("reloadGrid");//重新载入

}

function ProcessingRecordApproval() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    var CRID = Model.CRID;
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        $.ajax({
            url: "GetBasCusCollectionRecord",
            type: "post",
            data: { CRID: CRID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        if (json[i].State == "0") {
                            var texts = Model.CRID + "@" + "售后收款记录审批";
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