
var curPage = 1;
var DcurPage = 1;
var OnePageCount = 10;
var PID;
var RelenvceID;
var Type = "订单审批";
var IOID;
var SalesProduct;
var StartDate;
var EndDate;
var Op;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    //$("#QQ").width($("#search").width());
    $("#QQ").width($("#search").width());
    $("#QQ").height($("#pageContent").height() / 2 - 75);
    $("#RZJ").width($("#search").width());
    $("#RZJ").height($("#pageContent").height() / 2 - 75);

    var webkey = $("#webkey").val();
    if (webkey == "备案审批") {
        $("#sp2").html("备案申请编号：");
    }
    else if (webkey == "报价审批") {
        $("#sp2").html("报价申请编号：");
    }
    else if (webkey == "订单审批") {
        $("#sp3").html("订单申请编号：");
    }
    jq();
    jq1();
    jq2();
    $('#QQJQdiv').click(function () {
        this.className = "btnTw";
        $('#RZJLdiv').attr("class", "btnTh");
        $('#SHowXXdiv').attr("class", "btnTh");
        $("#QQ").css("display", "");
        $("#RZJ").css("display", "none");
        $("#SHowXX").css("display","none");

    })

    $('#RZJLdiv').click(function () {
        this.className = "btnTw";
        $('#QQJQdiv').attr("class", "btnTh");
        $("#RZJ").css("display", "");
        $("#QQ").css("display", "none");

    })

    $('#SHowAppOrderXX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择订单");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
        window.parent.OpenDialog("审批订单详细", "../SalesManage/SHowAppOrderXX?id=" + texts, 1000, 550, '');



    })

    $('#SP').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行审批的条目");
            return;
        }
        else {
            var SPID = jQuery("#list").jqGrid('getRowData', rowid).SPID;
            var PID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
          //  var AID = jQuery("#list").jqGrid('getRowData', rowid).ID;
            if (state == $("#Nostate").val()) {
                alert("审批不通过，不能进行审批了");
                return;
            }
            if (state == "20")
            {
                $.ajax({
                    url: "../COM_Approval/JudgeAppDisable",
                    type: "post",
                    data: { data1: "订单修改审批", data2: SPID },
                    dataType: "Json",
                    success: function (data) {
                        if (data.success == "true") {
                            var bol = data.intblo;
                            if (bol == "-1") {
                                alert("您没有审批权限，不能进行审批操作");
                                return;
                            }
                            if (bol == "1") {
                                alert("您已经审批完成，不能进行审批操作");
                                return;
                            }
                            if (bol == "2") {
                                alert("审批过程还没有进行到您这一步，不能进行审批操作");
                                return;
                            }
                            var texts = "订单修改审批" + "@" + SPID + "@" + PID;
                            

                            //var texts = $("#webkey").val() + "@" + SPID + "@" + PID;
                            // window.parent.OpenDialog("审批", "../COM_Approval/Approval?id=" + texts, 500, 400, '');
                            window.parent.OpenDialog("审批", "../SalesManage/Approval?id=" + texts, 500, 400, '');
                        }
                        else {
                            return;
                        }
                    }
                });
            }
            else{
            $.ajax({
                url: "../COM_Approval/JudgeAppDisable",
                type: "post",
                data: { data1: $("#webkey").val(), data2: SPID },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        var bol = data.intblo;
                        if (bol == "-1") {
                            alert("您没有审批权限，不能进行审批操作");
                            return;
                        }
                        if (bol == "1") {
                            alert("您已经审批完成，不能进行审批操作");
                            return;
                        }
                        if (bol == "2") {
                            alert("审批过程还没有进行到您这一步，不能进行审批操作");
                            return;
                        }
                       // var texts = $("#webkey").val() + "@" + PID;
                        var texts = $("#webkey").val() + "@" + SPID + "@" + PID;
                        // window.parent.OpenDialog("审批", "../COM_Approval/Approval?id=" + texts, 500, 400, '');
                        window.parent.OpenDialog("审批", "../SalesManage/Approval?id=" + texts, 500, 400, '');
                    }
                    else {
                        return;
                    }
                }
            });
            }

        }
    })


    $('#EXSP').click(function ()
    {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行审批的条目");
            return;
        }
        else {
            var SPID = jQuery("#list").jqGrid('getRowData', rowid).SPID;
            var PID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
            var EXstate = jQuery("#list").jqGrid('getRowData', rowid).EXState;
            if (EXstate == $("#Nostate").val()) {
                alert("审批不通过，不能进行审批了");
                return;
            }
            $.ajax({
                url: "../SalesManage/JudgeAppDisable",
                type: "post",
                data: { data1: "退换货审批", data2: SPID },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        var bol = data.intblo;
                        if (bol == "-1") {
                            alert("您没有审批权限，不能进行审批操作");
                            return;
                        }
                        if (bol == "1") {
                            alert("您已经审批完成，不能进行审批操作");
                            return;
                        }
                        if (bol == "2") {
                            alert("审批过程还没有进行到您这一步，不能进行审批操作");
                            return;
                        }
                        var texts ="退换货审批" + "@" + SPID + "@" + PID;
                        // window.parent.OpenDialog("审批", "../COM_Approval/Approval?id=" + texts, 500, 400, '');
                        window.parent.OpenDialog("审批", "../SalesManage/EXApproval?id=" + texts, 500, 400, '');
                    }
                    else {
                        return;
                    }
                }
            });
        }
    })

    $('#SPQK').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要查看审批情况的条目");
            return;
        }
        else {
            var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var texts = $("#webkey").val() + "@" + SPID;
            // window.parent.OpenDialog("审批情况", "../COM_Approval/ApprovalCondition?id=" + texts, 700, 500, '');
            window.parent.OpenDialog("审批情况", "../SalesManage/ApprovalCondition?id=" + texts, 700, 500, '');
        }
    })
})

function reload() {
    if ($('.field-validation-error').length == 0) {
        ContractID = $('#ContractID').val();
        OrderUnit = $('#OrderUnit').val();
        UseUnit = $("#UseUnit").val();
        MainContent = $("#MainContent").val();
        State = $("input[name='State']:checked").val();
        // State = $("#State").val();
        StartDate = $('#StartDate').val();
        EndDate = $('#EndDate').val();

        var strSeparator = "-"; //日期分隔符
        var strDateArrayStart;
        var strDateArrayEnd;
        var intDay;
        strDateArrayStart = StartDate.split(strSeparator);
        strDateArrayEnd = EndDate.split(strSeparator);
        var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
        var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);
        if (StartDate == "" && EndDate == "") {
            Op = $("#webkey").val();
            $("#list").jqGrid('setGridParam', {
                url: 'SearchOrderApproval',
                datatype: 'json',
                postData: {
                    curpage: curPage, rownum: OnePageCount, ContractID: ContractID, OrderUnit: OrderUnit, UseUnit: UseUnit,
                    MainContent: MainContent, State: State, StartDate: StartDate, EndDate: EndDate, Op: Op
                },

            }).trigger("reloadGrid");

        }
        else {
            if (strDateS <= strDateE) {
                Op = $("#webkey").val();
                $("#list").jqGrid('setGridParam', {
                    url: 'SearchOrderApproval',
                    datatype: 'json',
                    postData: {
                        curpage: curPage, rownum: OnePageCount, ContractID: ContractID, OrderUnit: OrderUnit, UseUnit: UseUnit,
                        MainContent: MainContent, State: State, StartDate: StartDate, EndDate: EndDate, Op: Op
                    },

                }).trigger("reloadGrid");
            }
            else {
                alert("截止日期不能小于开始日期！");
                $("#End").val("");
                return false;
            }
        }
    }
}

function jq() {
    ContractID = $('#ContractID').val();
    OrderUnit = $('#OrderUnit').val();
    UseUnit = $("#UseUnit").val();
    MainContent = $("#MainContent").val();
    State = $("#State").val();
    StartDate = $('#StartDate').val();
    EndDate = $('#EndDate').val();
    //HState = $("#HState").val();
    Op = $("#webkey").val();

    jQuery("#list").jqGrid({
        url: 'SearchOrderApproval',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, ContractID: ContractID, OrderUnit: OrderUnit, UseUnit: UseUnit,
            MainContent: MainContent, State: State, StartDate: StartDate, EndDate: EndDate, Op: Op
        },
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
        colNames: ['', '订单编号号', '合同编号', '签订日期', '订货单位', '订货人', '审批状态', '合计', 'State','',''],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'OrderID', index: 'OrderID', width: 150 },
         { name: 'ContractID', index: 'ContractID', width: 150 },
         { name: 'ContractDate', index: 'ContractDate', width: 150, formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' } },
        { name: 'OrderUnit', index: 'OrderUnit', width: 150 },
        { name: 'OrderContactor', index: 'OrderContactor', width: 150 },
        { name: 'StateDesc', index: 'StateDesc', width: 150 },
         { name: 'TotalTax', index: 'TotalTax', width: 150 },
        { name: 'State', index: 'State', width: 100, hidden: true },
       { name: 'SPID', index: 'SPID', width: 200 },
       { name: 'OPID', index: 'OPID', width: 200, hidden: true },
       // { name: 'ID', index: 'ID', width: 200, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '备案审批表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            select(rowid);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

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

function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).SPID;
    RelenvceID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
    reload1();
    reload2();
   // reload3();
}

function reload1() {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list1").jqGrid('setGridParam', {
        url: '../SalesManage/ConditionGrid',
        datatype: 'json',
        postData: { curpage: DcurPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}

function jq1() {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list1").jqGrid({
        url: '../SalesManage/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },
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
        { name: 'Job', index: 'Job', width: 100 },
        { name: 'UserName', index: 'UserName', width: 100 },
        { name: 'AppTypeDesc', index: 'AppTypeDesc', width: 100 },
        { name: 'Num', index: 'Num', width: 100 },
        { name: 'stateDesc', index: 'stateDesc', width: 100 },
        { name: 'Opinion', index: 'Opinion', width: $("#bor").width() - 920 },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 150 },
        { name: 'Remark', index: 'Remark', width: 200 },
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                //var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                //jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage == $("#list1").getGridParam("lastpage"))
                    return;
                curPage = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage = 1;
            }
            else {
                curPage = $("#pager1 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}
function reload2() {
    $("#list2").jqGrid('setGridParam', {
        url: 'GetUserLogGrid',
        datatype: 'json',
        loadonce: false,
        mtype: 'POST',
        postData: { curpage: curPage, rownum: OnePageCount, PID: RelenvceID, Type: Type },

    }).trigger("reloadGrid");
}

function jq2() {
    jQuery("#list2").jqGrid({
        url: 'GetUserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: RelenvceID, Type: Type },
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
        colNames: ['', '项目编号', '操作内容', '操作结果', '操作时间', '操作人'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'Relevanceid', index: 'Relevanceid', width: 120 },
        { name: 'LogTitle', index: 'LogTitle', width: 200 },
        { name: 'LogContent', index: 'LogContent', width: $("#bor").width() - 700 },
        { name: 'LogTime', index: 'LogTime', width: 150 },
        { name: 'LogPerson', index: 'LogPerson', width: 70 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '操作日志记录表',

        gridComplete: function () {
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
                if (curPage == $("#list2").getGridParam("lastpage"))
                    return;
                curPage = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage == 1)
                    return;
                curPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

