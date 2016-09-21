var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ListOutID;
var curPage1 = 1;
var OnePageCount1 = 2;
var curPage3= 1;
var OnePageCount3 = 4;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    $("#tcxx").click(function () {
        $("#tcxx").show();
        $("#bor2").show();
        $("#bor1").hide();
        $("#bor3").hide();
    });
    $("#cljl").click(function () {
        $("#cljl").show();
        $("#bor1").show();
        $("#bor2").hide();
        $("#bor3").hide();
    });


    $("#spqkb").click(function () {
        $("#cljl").show();
        $("#bor1").hide();
        $("#bor2").hide();
        $("#bor3").show();
    });
    jq();
    jq1("");
    jq2("");
    jq3("");
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
                var TSID = Model.TSID;
                $.ajax({
                    type: "POST",
                    url: "DeUserComplaints",
                    data: { TSID: TSID },
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
        var rID = jQuery("#list").jqGrid('getRowData', rowid).TSID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).TSID;
            var url = "PrintUserComplaints?Info=" + escape(texts) + "&a=1";
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})
function ScrapManagementOut() {
    window.parent.parent.OpenDialog("投诉登记", "../CustomerService/AddUserComplaints", 800, 550);
}
function ProcessingRecord() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        window.parent.parent.OpenDialog("处理记录", "../CustomerService/AddProcessingRecord?TSID=" + Model.TSID + "&type=2", 800, 550);
    }
}
function jq() {
    var PID = $('#PID').val();
    var OrderContent = $('#OrderContent').val();
    var Tel = $('#Tel').val();
    var Adderss = $('#Adderss').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var UserName = $("#UserName").val();
    var FirstDealUser = $("#FirstDealUser").val();
    jQuery("#list").jqGrid({
        url: 'UserComplaintsList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, Tel: Tel, Adderss: Adderss, OrderContent: OrderContent, UserName: UserName, FirstDealUser: FirstDealUser, Begin: Begin, End: End },
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
        colNames: ['序号', '投诉编号', '投诉时间', '投诉日期', '客户名称', '电话', '地址', '投诉主题', '投诉类别', '投诉内容', '紧急程度', '首次处理人', '投诉人员', '备注', '登记人',''],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'TSID', index: 'TSID', width: 100, align: "center" },
        { name: 'ComplaintDate', index: 'ComplaintDate', width: 80, align: "center" },
        { name: 'RecordDate', index: 'RecordDate', width: 80, align: "center" },
        { name: 'Customer', index: 'Customer', width: 80, align: "center" },
        { name: 'Tel', index: 'Tel', width: 80, align: "center" },
        { name: 'Adderss', index: 'Adderss', width: 80, align: "center" },
        { name: 'ComplaintTheme', index: 'ComplaintTheme', width: 120, align: "center" },
        { name: 'ComplaintCategory', index: 'ComplaintCategory', width: 120, align: "center" },
        { name: 'ComplainContent', index: 'ComplainContent', width: 80, align: "center" },
        { name: 'EmergencyDegree', index: 'EmergencyDegree', width: 150, align: "center" },
        { name: 'FirstDealUser', index: 'FirstDealUser', width: 120, align: "center" },
        { name: 'UserName', index: 'UserName', width: 120, align: "center" },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" },
       // { name: 'State', index: 'State', width: 120, align: "center" }, '状态',
        { name: 'CreateUser', index: 'CreateUser', width: 50, align: "center" },
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
                var TSID = Model.TSID;
                var PID = Model.PID;
                reload1(TSID);
                reload2(TSID);
                reload3(PID);
                PanDuan();
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function reload() {
    var PID = $('#PID').val();
    var OrderContent = $('#OrderContent').val();
    var Tel = $('#Tel').val();
    var Adderss = $('#Adderss').val();

    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var UserName = $("#UserName").val();
    var FirstDealUser = $("#FirstDealUser").val();
    $("#list").jqGrid('setGridParam', {
        url: 'UserComplaintsList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, Tel: Tel, Adderss: Adderss, OrderContent: OrderContent, UserName: UserName, FirstDealUser: FirstDealUser, Begin: Begin, End: End },
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
    var PID = $('#PID').val();
    var OrderContent = $("#OrderContent").val();
    var UserName = $("#UserName").val();
    var FirstDealUser = $("#FirstDealUser").val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var Tel = $('#Tel').val();
    var Adderss = $('#Adderss').val();
    $("#list").jqGrid('setGridParam', {
        url: 'UserComplaintsList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, Tel: Tel, Adderss: Adderss, OrderContent: OrderContent, UserName: UserName, FirstDealUser: FirstDealUser, Begin: Begin, End: End },
        loadonce: false
    }).trigger("reloadGrid");//重新载入
}
function jq2(TSID) {
    jQuery("#list2").jqGrid({
        url: 'UserProComDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TSID: TSID },
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
        colNames: ['投诉日期', '产品名称', '规格型号', '单位', '数量', '投诉内容', '订单编号', '状态'],
        colModel: [
        { name: 'RecordDate', index: 'RecordDate', width: 200, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 200, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 200, align: "center" },
        { name: 'Unit', index: 'Unit', width: 200, align: "center" },
        { name: 'Num', index: 'Num', width: 200, align: "center" },
        { name: 'ComplainContent', index: 'ComplainContent', width: 200, align: "center" },
        { name: 'OrderID', index: 'OrderID', width: 200, align: "center" },
        { name: 'State', index: 'State', width: 200, align: "center" }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '投诉产品信息',
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
                if (curPage == 1)
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
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list2").jqGrid("setGridWidth", $("#bor2").width() - 30, false);
        }
    })
}
function jq1(TSID) {
    jQuery("#list1").jqGrid({
        url: 'UserComplaintsDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TSID: TSID },
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
        colNames: ['处理过程', '客户反馈', '处理时间', '处理人', '记录人', '审批状态'],
        colModel: [
        { name: 'HandleProcess', index: 'HandleProcess', width: 200, align: "center" },
        { name: 'CustomerFeedback', index: 'CustomerFeedback', width: 200, align: "center" },
        { name: 'HandleDate', index: 'HandleDate', width: 80, align: "center" },
        //{ name: 'CostDate', index: 'CostDate', width: 200, align: "center" },
        { name: 'HandleUser', index: 'HandleUser', width: 50, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 50, align: "center" },
        { name: 'State', index: 'State', width: 80, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '处理记录',
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
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}
function reload2(TSID) {
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list2").jqGrid('setGridParam', {
        url: 'UserProComDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TSID: TSID },

    }).trigger("reloadGrid");
}
function reload1(TSID) {
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'UserComplaintsDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, TSID: TSID },

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
        window.parent.parent.OpenDialog("修改用户投诉", "../CustomerService/ModifyUserComplaints?TSID=" + Model.TSID, 800, 550);
    }
}

function PanDuan() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    var TSID = Model.TSID;
    $.ajax({
        url: "GetBasCus",
        type: "post",
        data: { TSID: TSID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (json[i].State == "0") {
                        $("#tsdj").hide();
                        $("#cljl111").hide();
                        //$("#Up").hide();
                       // $("#De").hide();
                        return;
                    } else {
                       $("#tsdj").show();
                        $("#cljl111").show();
                        $("#Up").show();
                        $("#De").show();
                    }
                }
            }
        }
    })
}
function ProcessingRecordApproval() {

    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    var TSID = Model.TSID;
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        $.ajax({
            url: "GetBasCus",
            type: "post",
            data: { TSID: TSID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        if (json[i].State == "0") {
                            var texts = Model.TSID + "@" + "售后处理记录审批";
                            window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
                        } else {
                            alert(json[i].State == "1" ? "待审批" : "已审批");
                            //$("#tsdj").hide();
                            //$("#cljl111").hide();
                            //$("#Up").hide();
                            //$("#De").hide();


                          
                            return;
                        }
                    }
                }
            }
        })
    }
    //if (ids == null) {
    //    alert("请选择要操作的行");
    //    return;
    //} else {
    //    window.parent.parent.OpenDialog("审批处理记录", "../CustomerService/AddProcessingRecord?TSID=" + Model.TSID + "&type=1", 800, 500);
    //}


    //$("#ShenPi").click(function () {//审批
    //if (location.search != "") {
    //    TSID = location.search.split('&')[0].split('=')[1];
    //}
    ////var texts = Model.TSID + "@" + "售后处理记录审批";
    ////window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
    //});


}

function jq3(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list3").jqGrid({
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
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list3").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list3").jqGrid('getRowData', id);

            }

        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (curPage3 == $("#list3").getGridParam("lastpage3"))
                    return;
                curPage3 = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPage3 = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPage3 == 1)
                    return;
                curPage3 = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPage3 = 1;
            }
            else {
                curPage3 = $("#pager3 :input").val();
            }
            reload3();
        },
        loadComplete: function () {
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list3").jqGrid("setGridWidth", $("#bor3").width() - 30, false);
        }
    });
}

function reload3(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list3").jqGrid('setGridParam', {
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage3, rownum: OnePageCount3, PID: jhid, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}
