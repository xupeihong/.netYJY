var curPage = 1;
var OnePageCount = 15;
var curPage1 = 1;
var OnePageCount1 = 15;
var curPage2 = 1;
var OnePageCount2 = 15;
var CID;
var Cname;
var ContractID;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    $("#CCashBack").width($("#search").width());
    $("#CCashBack").height($("#pageContent").height() / 2 - 75);
    $("#UserLog").width($("#search").width());
    $("#UserLog").height($("#pageContent").height() / 2 - 75);
    jq();
    jq1();
    jq2();

    $('#HKdiv').click(function () {
        this.className = "btnTw";
        $('#CZRZdiv').attr("class", "btnTh");

        $("#CCashBack").css("display", "");
        $("#UserLog").css("display", "none");
        reload1();
    })


    $('#CZRZdiv').click(function () {
        this.className = "btnTw";
        //$('#HKdiv').attr("class", "btnTh");

        $("#UserLog").css("display", "");
        //$("#CCashBack").css("display", "none");
        reload2();
    })

    $('#XZHT').click(function () {
        window.parent.OpenDialog("新增合同", "../Contract/AddContract", 800, 600, '');
    })

    $('#XZ').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要下载文件的合同");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
        window.parent.OpenDialog("合同文件下载", "../Contract/DownloadFileProject?id=" + texts, 400, 200, '');
    })

    $('#BGHT').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要变更的合同");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
        window.parent.OpenDialog("变更合同", "../Contract/ChangeProContract?id=" + texts, 800, 600, '');
    })

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
            if (State == -1) {
                alert("你选择的条目审批未通过，请重新起草内容");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).CID + "@" + "工程项目合同审批";
            window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
        }
    })

    $('#HKJL').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要进行回款记录的合同");
            return;
        }
        var State = jQuery("#list").jqGrid('getRowData', rowid).State
        if (State < "2") {
            alert("该合同还没有审批通过，不能进行回款操作");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID + "@" + jQuery("#list").jqGrid('getRowData', rowid).PID;
        window.parent.OpenDialog("回款记录", "../Contract/ProCashBack?id=" + texts, 800, 600, '');
    })

    $('#HTJS').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要进行结算的合同");
            return;
        }
        var State = jQuery("#list").jqGrid('getRowData', rowid).State
        if (State < "2") {
            alert("该合同还没有审批通过，不能进行结算操作");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID + "@" + jQuery("#list").jqGrid('getRowData', rowid).PID;
        window.parent.OpenDialog("合同结算", "../Contract/SettlementPro?id=" + texts, 800, 400, '');
    })

    $('#CKXX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要查看详细的合同");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
        window.parent.OpenDialog("合同详细", "../Contract/DetailProjectContract?id=" + texts, 800, 450, '');
    })
})
function reload() {
    if ($('.field-validation-error').length == 0) {
        Cname = $('#Cname').val();
        ContractID = $('#ContractID').val();
        $("#list").jqGrid('setGridParam', {
            url: 'ContractGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, cname: Cname, contractID: ContractID },

        }).trigger("reloadGrid");
    }
}

function jq() {
    Cname = $('#Cname').val();
    ContractID = $('#ContractID').val();
    jQuery("#list").jqGrid({
        url: 'ContractGrid',
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
        colNames: ['', '合同ID', '合同编号', '业务类型', '对应项目编号', '合同名称', '预计完工时间', '项目合同额(万元)', '项目前期费用(万元)', '项目成本(万元)', '项目利润(万元)',  '所属单位', '状态', 'State'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'CID', index: 'CID', width: 100 },
        { name: 'ContractID', index: 'ContractID', width: 70 },
        { name: 'BusinessTypeDesc', index: 'BusinessTypeDesc', width: 70 },
        { name: 'PID', index: 'PID', width: 100 },
        { name: 'Cname', index: 'Cname', width: $("#bor").width() - 1000 },
        { name: 'CPlanEndTime', index: 'CPlanEndTime', width: 80 },
        { name: 'PContractAmount', index: 'PContractAmount', width: 100 },
        { name: 'PBudget', index: 'PBudget', width: 100 },
        { name: 'PCost', index: 'PCost', width: 100 },
        { name: 'PProfit', index: 'PProfit', width: 100 },
        //{ name: 'AmountNum', index: 'AmountNum', width: 100 },
        //{ name: 'CurAmountNum', index: 'CurAmountNum', width: 70 },
        { name: 'DeptName', index: 'DeptName', width: 100 },
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        { name: 'State', index: 'State', width: 50, hidden: true }
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
    reload1();
    reload2();
}



function reload1() {
    //JQtype = $("#JQtype").val();
    $("#list1").jqGrid('setGridParam', {
        url: 'CashBackGrid',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, Cid: CID }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

function jq1() {
    //JQtype = $("#JQtype").val();
    jQuery("#list1").jqGrid({
        url: 'CashBackGrid',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, Cid: CID }, //, jqType: JQtype
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
        postData: { curpage: curPage2, rownum: OnePageCount2, Cid: CID }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

function jq2() {
    //JQtype = $("#JQtype").val();
    jQuery("#list2").jqGrid({
        url: 'UserLogGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, Cid: CID }, //, jqType: JQtype
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

