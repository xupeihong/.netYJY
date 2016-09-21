var curPage = 1;
var OnePageCount = 15;
var CID;
var Cname;
var ContractID;
var oldSelID = 0;
var newSelID = 0;
var curPage3 = 1;
var OnePageCount3 = 3;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    $("#CCashBack").width($("#search").width());
    $("#CCashBack").height($("#pageContent").height() / 2 - 75);
    $("#UserLog").width($("#search").width());
    $("#UserLog").height($("#pageContent").height() / 2 - 75);
    jq();
    jq1();
    //jq2();
    jq3("");
    // 打印
    $("#btnPrint").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).CID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
            var url = "PrintXSContract?Info=" + escape(texts) ;
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
     // 打印
    $("#btnPrint2").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).CID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
            var url = "PrintXSContractCG?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
    $('#QQJQdiv').click(function () {
        this.className = "btnTw";
        $('#HKdiv').attr("class", "btnTh");
        $("#CCashBack").css("display", "none");
        $("#SPJLQK").css("display", "");
        $("#UserLog").css("display", "none");
        reload3();
    })

    $('#HKdiv').click(function () {
        this.className = "btnTw";
        $('#QQJQdiv').attr("class", "btnTh");

        $("#CCashBack").css("display", "");
        $("#UserLog").css("display", "none");
        $("#SPJLQK").css("display", "none");
        reload1();
    })
    $('#CZRZdiv').click(function () {
        this.className = "btnTw";
        $('#HKdiv').attr("class", "btnTh");
        $("#UserLog").css("display", "");
        $("#CCashBack").css("display", "none");
        $("#SPJLQK").css("display", "none");
        reload2();
    })
    //新增合同
    $('#XZHT').click(function () {
        window.parent.OpenDialog("新增合同", "../CustomerService/AddContractnew", 800, 600, '');
    })
    //管理文件
    $('#GLWJ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要管理文件的合同");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
        window.parent.OpenDialog("管理文件", "../Contract/AddFile?id=" + texts, 500, 300, '');
    })
    //变更合同
    $('#BGHT').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要变更的合同");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
        window.parent.OpenDialog("变更合同", "../CustomerService/ChangeContracNew?id=" + texts, 800, 600, '');
    })
    //提交审批
    $('#TJSP').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行提交审批的合同");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State
            if (State >= "1") {
                alert("该合同已经提交审批，不能重复操作");
                return;
            }
            var texts = "";
            var UnitID = $("#UnitID").val();
            if (UnitID == "36") {
                texts = jQuery("#list").jqGrid('getRowData', rowid).CID + "@" + "工程项目合同审批";
            }
            if (UnitID == "47") {
                texts = jQuery("#list").jqGrid('getRowData', rowid).CID + "@" + "销售合同审批";
            }
            if (UnitID == "55") {
                texts = jQuery("#list").jqGrid('getRowData', rowid).CID + "@" + "售后部门合同审批";
            }
            window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
        }
    })
    //提交审批
    $('#TJSPN').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行提交审批的合同");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).State
            if (State >= "1") {
                alert("该合同已经提交审批，不能重复操作");
                return;
            }
            var texts = "";
            var UnitID = $("#UnitID").val();
            if (UnitID == "55") {
                texts = jQuery("#list").jqGrid('getRowData', rowid).CID + "@" + "售后部门合同审批";
            }
            TJConten(texts);
        }
    })
    //回款记录
    $('#HKJL').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要进行回款记录的合同");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
        window.parent.OpenDialog("回款记录", "../CustomerService/CashBackNew?id=" + texts, 800, 600, '');
    })
    //合同结算
    $('#HTJS').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要进行结算的合同");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
        window.parent.OpenDialog("合同结算", "../Contract/Settlement?id=" + texts, 800, 400, '');
    })
    //查看详细
    $('#CKXX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要查看详细的合同");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
        window.parent.OpenDialog("合同详细", "../Contract/DetailContract?id=" + texts, 800, 550, '');
    })
    //查看巡检
    $('#CKXJ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要查看巡检的合同");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
        $.ajax({
            url: "ViewInspection",
            type: "post",
            data: { data1: texts },
            dataType: "Json",
            success: function (data) {
                if (data.success == true) {
                    alert(data.Msg);
                }
                else {
                    return;
                }
            }
        });

        //var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
        //window.parent.OpenDialog("合同详细", "../Contract/DetailContract?id=" + texts, 800, 550, '');
    })
})
function TJConten(texts) {
    $.ajax({
        type: "POST",
        url: "TJContract",
        data: { texts: texts },
        success: function (data) {
            alert(data.Msg);
            reload();
        },
        dataType: 'json'
    });
}
function reload() {
    if ($('.field-validation-error').length == 0) {
        Cname = $('#Cname').val();
        ContractID = $('#ContractID').val();
        $("#list").jqGrid('setGridParam', {
            url: 'ContractGridnew',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, cname: Cname, contractID: ContractID },

        }).trigger("reloadGrid");
    }
}
function jq() {
    Cname = $('#Cname').val();
    ContractID = $('#ContractID').val();
    jQuery("#list").jqGrid({
        url: 'ContractGridnew',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, cname: Cname, contractID: ContractID },
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
        colNames: ['', '合同ID', '合同编号', '业务类型', '对应项目编号', '合同名称', '合同初始金额', '合同变更后金额', '合同签订日期', '预计完工时间', '合同签订回款次数', '已回款次数', '状态', 'State'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'CID', index: 'CID', width: 100 },
        { name: 'ContractID', index: 'ContractID', width: 70 },
        { name: 'BusinessTypeDesc', index: 'BusinessTypeDesc', width: 70 },
        { name: 'PID', index: 'PID', width: 100 }, 
        { name: 'Cname', index: 'Cname', width: $("#bor").width() - 1000 },
        { name: 'CBeginAmount', index: 'CBeginAmount', width: 70 },
        { name: 'CEndAmount', index: 'CEndAmount', width: 80 }, 
        { name: 'Ctime', index: 'Ctime', width: 80 },
        { name: 'CPlanEndTime', index: 'CPlanEndTime', width: 80 },
        { name: 'AmountNum', index: 'AmountNum', width: 100 }, 
        { name: 'CurAmountNum', index: 'CurAmountNum', width: 70 }, 
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        { name: 'State', index: 'State', width: 50, hidden: true },
        //{ name: 'SPID', index: 'SPID', width: 70 },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '合同表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).CID + "' name='cb'/>";
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
    CID = jQuery("#list").jqGrid('getRowData', rowid).CID;
    PID = jQuery("#list").jqGrid('getRowData', rowid).SPID;
    reload1();
    reload2();
    reload3(PID);
}
function reload1() {
    //JQtype = $("#JQtype").val();
    $("#list1").jqGrid('setGridParam', {
        url: 'CashBackGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, Cid: CID }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
function jq1() {
    //JQtype = $("#JQtype").val();
    jQuery("#list1").jqGrid({
        url: 'CashBackGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, Cid: CID }, //, jqType: JQtype
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
        colNames: ['合同编号', '回款编号', '回款次数', '回款金额', '缴费单位', '回款日期', '操作', '操作'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'CID', index: 'CID', width: 120 },
        { name: 'CBID', index: 'CBID', width: 120 },
        { name: 'CurAmountNum', index: 'CurAmountNum', width: 120 },
        { name: 'CBMoney', index: 'CBMoney', width: 120 },
        { name: 'PayCompany', index: 'PayCompany', width: $("#bor").width() - 850 },
        { name: 'CBDate', index: 'CBDate', width: 180 },
        { name: 'IDCheck', index: 'Id', width: 50 },
        { name: 'deCheck', index: 'Id', width: 50 },
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '回款记录表',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<a style='color:blue;cursor:pointer' onclick=\"updateCB('" + jQuery("#list1").jqGrid('getRowData', id).CBID + "')\">修改</a>";
                var curChk1 = "<a style='color:blue;cursor:pointer' onclick=\"dellCB('" + jQuery("#list1").jqGrid('getRowData', id).CBID + "@" + jQuery("#list1").jqGrid('getRowData', id).CID + "')\">撤销</a>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk, deCheck: curChk1 });
            }

        },
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=q' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=q' + rowid + ']').prop("checked", true);
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
            else if (pgButton == "prev_pager1") {
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
function updateCB(id) {
    window.parent.OpenDialog("修改回款记录", "../Contract/UpdateCashBack?id=" + id, 800, 600, '');
}
function dellCB(id) {
    var one = confirm("确定要撤销选中条目吗");
    if (one == false)
        return;
    else {
        $.ajax({
            url: "dellCashBack",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    reload1();
                }
                else {
                    return;
                }
            }
        });
    }
    //window.parent.OpenDialog("修改回款记录", "../Contract/UpdateCashBack?id=" + id, 800, 600, '');
}
function reload2() {
    //JQtype = $("#JQtype").val();
    $("#list2").jqGrid('setGridParam', {
        url: 'UserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, Cid: CID }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
function jq2() {
    //JQtype = $("#JQtype").val();
    jQuery("#list2").jqGrid({
        url: 'UserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, Cid: CID }, //, jqType: JQtype
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
        colNames: ['合同编号', '操作内容', '操作结果', '操作时间', '操作人'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'UserId', index: 'UserId', width: 120 },
        { name: 'LogTitle', index: 'LogTitle', width: $("#bor").width() - 700 },
        { name: 'LogContent', index: 'LogContent', width: 120 },
        { name: 'LogTime', index: 'LogTime', width: 200 },
        { name: 'LogPerson', index: 'LogPerson', width: 120 },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '日志记录表',

        //gridComplete: function () {
        //    var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list1").jqGrid('getRowData', id);
        //        var curChk = "<input id='q" + id + "' onclick='selChangeq(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
        //        jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    }

        //},
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=q' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=q' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

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
function jq3(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list3").jqGrid({
        url: 'ConditionGridNew',
       // url: '../COM_Approval/ConditionGrid',
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
        colNames: ['', '职务', '姓名',  '审批情况', '审批意见', '审批时间'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'Job', index: 'Job', width: 100, align: "center" },
        { name: 'UserName', index: 'UserName', width: 100, align: "center" },
        //{ name: 'AppTypeDesc', index: 'AppTypeDesc', width: 100, align: "center" }, '审批方式', '人数',
        //{ name: 'Num', index: 'Num', width: 100, align: "center" },
        { name: 'stateDesc', index: 'stateDesc', width: 100, align: "center" },
        { name: 'Opinion', index: 'Opinion', width: $("#bor").width() - 920, align: "center" },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 150, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } }
       // { name: 'Remark', index: 'Remark', width: 200, align: "center" },, '备注'
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
                curPage3= $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPage3 = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPage3== 1)
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
        url: 'ConditionGridNew',
        datatype: 'json',
        postData: { curpage: curPage3, rownum: OnePageCount3, PID: jhid, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}
