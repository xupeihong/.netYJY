var curPage = 1, curPageYear = 1, curPageScore = 1, curPageLog = 1, curPagePro = 1, curPageSer = 1, curPageZiZhi = 1, curPageZS = 1, curPageYScore = 1;
var OnePageCount = 15, OnePageCountYear = 15, OnePageCountScore = 15;
var OnePageCountLog = 10, OnePageCountPro = 10, OnePageCountSer = 10, OnePageCountZiZhi = 10, OnePageCountZS = 10, OnePageCountYScore = 10;
var SID, PID, FID, Yid, ShortName, SupplyType, AppYear, SupplyCode, COMShortName, SupplierType, ReviewDate, SupplierCode;
var oldSelID = 0, oldScoreID = 0, oldproID = 0, oldSerID = 0, oldplanPro = 0, oldplanSer = 0, oldLog = 0, oldYScore = 0;
var curPageAward = 1;
var curPagePrice = 1;
var OnePageCountAward = 15;
var OnrPageCountPrice = 15;
var oldAward = 0;
var oldPrice = 0;
$(document).ready(function () {
    var Job = $("#Exjob").val();
    if (Job == "董事长" || Job == "总经理" || Job == "副总经理" || Job == "公司领导") {
        $("#RsApproal").show();
        $("#Yeabumen").hide();
        $("#Yeagongsi").show();
    } else if (Job == "经理" || Job == "副经理") {
        $("#Yeabumen").show();
    }
    else {
        $("#RsApproal").hide();
        $("#Yeabumen").hide();
        $("#Yeagongsi").hide();
    }
    $("#search").width($("#bor").width() - 30);
    jq();
    jqN();
    jqScore();
    jqPro();
    jqYScore();
    jqSer();
    jqPlanPro();
    jqPlanSer();
    jqLog();
    jqAward();
    jqPrice();
    $("#one").hide();
    $("#four").hide();
    $("#five").hide();
    $("#six").hide();
    $("#seven").hide();
    $("#TWO").hide();
    $("#nine").hide();
    $("#ten").hide();
    $("#eight").hide();
    $("#Product").click(function () {
        this.className = "btnTw";
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#NDQK').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#ScoreYear').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#three").css("display", "");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#one").css("display", "none");
        $("#TWO").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");
        $("#ten").css("display", "none");
    });
    $("#Server").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#NDQK').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#ScoreYear').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#four").css("display", "");
        $("#three").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#one").css("display", "none");
        $("#TWO").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");
        $("#ten").css("display", "none");
    });
    $("#planPro").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#NDQK').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#ScoreYear').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#five").css("display", "");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#one").css("display", "none");
        $("#TWO").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");
        $("#ten").css("display", "none");
    });
    $("#planServer").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#NDQK').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#ScoreYear').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#six").css("display", "");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#seven").css("display", "none");
        $("#one").css("display", "none");
        $("#TWO").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");
        $("#ten").css("display", "none");
    });
    $("#btnLog").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#NDQK').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#ScoreYear').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#seven").css("display", "");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#one").css("display", "none");
        $("#TWO").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");
        $("#ten").css("display", "none");
    });
    $("#NDQK").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#ScoreYear').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#one").css("display", "");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#TWO").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");
        $("#ten").css("display", "none");
    })
    $("#ScoreMsg").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#NDQK').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#ScoreYear').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#TWO").css("display", "");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#one").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");
        $("#ten").css("display", "none");
    })
    $("#ScoreYear").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#NDQK').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#eight").css("display", "");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#one").css("display", "none");
        $("#TWO").css("display", "none");
        $("#nine").css("display", "none");
        $("#ten").css("display", "none");
    })
    $("#btnAward").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");
        $('#NDQK').attr("class", "btnTh");
        $('#ScoreYear').attr("class", "btnTh");

        $("#nine").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#eight").css("display", "none");
        $("#ten").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");

    })
    $("#btnPrice").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#NDQK').attr("class", "btnTh");
        $('#ScoreYear').attr("class", "btnTh");


        $("#ten").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#nine").css("display", "none");
        $("#eight").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");


    })
    // LoadResult(SID, Yid);
    //$("#YeaApproval").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("您还没有选择要处理的年度评审");
    //        return;
    //    }
    //    else {
    //        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID; //审批编号无
    //        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    //        var state = jQuery("#list").jqGrid('getRowData', rowid).State; //状态无
    //        if (state == $("#Nostate").val()) {
    //            alert("审批不通过，不能进行审批了");
    //            return;
    //        }
    //        if (state == "新增") {
    //            alert("未进行年度评价处理，不能进行审批操作");
    //            return;
    //        }
    //        $.ajax({
    //            url: "../COM_Approval/JudgeAppDisable",
    //            type: "post",
    //            data: { data1: $("#webkey").val(), data2: SPID },
    //            dataType: "Json",
    //            success: function (data) {
    //                if (data.success == "true") {
    //                    var bol = data.intblo;
    //                    if (bol == "-1") {
    //                        alert("您没有审批权限，不能进行审批操作");
    //                        return;
    //                    }
    //                    if (bol == "1") {
    //                        alert("您已经审批完成，不能进行审批操作");
    //                        return;
    //                    }
    //                    if (bol == "2") {
    //                        alert("审批过程还没有进行到您这一步，不能进行审批操作");
    //                        return;
    //                    }
    //                    var texts = $("#webkey").val() + "@" + SPID + "@" + SID;
    //                    window.parent.OpenDialog("年度审批", "../COM_Approval/ApprovalSup?id=" + texts, 900, 500, '');
    //                }
    //                else {
    //                    return;
    //                }
    //            }
    //        });
    //    }
    //})
    //年度最终评审
    //$("#RsApproal").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("请选择要最终评审的供应商");
    //        return;
    //    } else {
    //        var state = jQuery("#list").jqGrid('getRowData', rowid).state;
    //        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    //        if (state != "年度评价通过") {
    //            alert("该供应商不能进行最终评审");
    //            return;
    //        }
    //    }
    //    var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    //    window.parent.OpenDialog("年度最终评审", "../SuppliesManage/YearResult?sid=" + sid, 900, 300, '');
    //})
    //打印
    $("#YearPrint").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        if (rowid == null) {
            alert("请选择列表一条数据");
            return;
        }
        else {
            window.showModalDialog("../SuppliesManage/PrintYear?sid=" + sid, window, "dialogWidth:700px;dialogHeight:400px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    })
    //年度部门级审批 不要了
    //$("#Yeabumen").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("请选择要提交部门级审批的供应商");
    //        return;
    //    } else {
    //        var state = jQuery("#list").jqGrid('getRowData', rowid).state;
    //        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    //        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    //        var job = $("#ExJob").val();
    //    }
    //    if (state != "待部门级审批") {
    //        alert("该供应商不能进行部门级审批");
    //        return;
    //    }
    //    else {
    //        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs ;
    //        window.parent.OpenDialog("部门级年度审批", "../SuppliesManage/BumenSP?sid=" + sid, 1000, 400, '');
    //    }
    //})
    //年度公司级审批 /原来是公司审批
    $("#Yeabumen").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var state = jQuery("#list").jqGrid('getRowData', rowid).NState;
        var job = $("#ExJob").val();
        if (rowid == null) {
            alert("您还没有选择要提交部门级审批的供应商");
            return;
        }
        else {
            if (state != "待部门级审批") {
                alert("该供应商不能提交部门级审批");
                return;
            }
        }
        // var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "年度评审";
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        window.parent.OpenDialog("部门级评审", "../SuppliesManage/SubmittaotaiApproval?id=" + sid, 1000, 400, '');
    })

    $("#Detail").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要查看详细的基本信息");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        window.parent.OpenDialog("详细", "../SuppliesManage/DetailBas?sid=" + texts, 1000, 650, '');
    })
})

function reload() {
    ShortName = document.getElementById("COMNameC").value; //$("COMShortName").val();
    SupplyType = document.getElementById("SupplierType").value;//$("SupplierType").val();
    AppYear = document.getElementById("ReviewDate").value; //$("ReviewDate").val();
    // SupplyCode = document.getElementById("SID").value;
    $("#list").jqGrid('setGridParam', {
        url: 'ReviewGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, sName: ShortName, type: SupplyType, appyear: AppYear, order: $("#Order").val() },
    }).trigger("reloadGrid");
}
function reloadpro() {
    PName = $("#PName").val();
    Standard = $("#Standard").val();
    $("#list1").jqGrid('setGridParam', {
        url: 'ManageProGrid',
        datatype: 'json',
        postData: { curpage: curPagePro, rownum: OnePageCountPro, pName: PName, standard: Standard, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadServer() {
    $("#list4").jqGrid('setGridParam', {
        url: 'ManageServerGrid',
        datatype: 'json',
        postData: { curpage: curPageSer, rownum: OnePageCountSer, Sid: SID },
    }).trigger("reloadGrid");
}
//资质信息
function reloadPlanPro() {
    $("#list5").jqGrid('setGridParam', {
        url: 'ManagePlanProGrid',
        datatype: 'json',
        postData: { curpage: curPageZiZhi, rownum: OnePageCountZiZhi, Sid: SID, Fid: FID },
    }).trigger("reloadGrid");
}
//证书信息
function reloadPSer() {
    $("#list6").jqGrid('setGridParam', {
        url: 'ManagePlanSerGrid',
        datatype: 'json',
        postData: { curpage: curPageZS, rownum: OnePageCountZS, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadLog() {
    $("#list7").jqGrid('setGridParam', {
        url: 'LogShowGrid',
        datatype: 'json',
        postData: { curpage: curPageLog, rownum: OnePageCountLog, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadYearScore() {
    $("#list8").jqGrid('setGridParam', {
        url: 'YearShowGrid',
        datatype: 'json',
        postData: { curpage: curPageYScore, rownum: OnePageCountYScore, Sid: SID },
    }).trigger("reloadGrid");
}

function reloadWard() {
    $("#list9").jqGrid('setGridParam', {
        url: 'AwardNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageAward, rownum: OnePageCountAward, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadPrice() {
    $("#list10").jqGrid('setGridParam', {
        url: 'PriceNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPagePrice, rownum: OnrPageCountPrice, Sid: SID },
    }).trigger("reloadGrid");
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
function selChangeYScore(rowid) {
    if ($('input[id=j' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldYScore != 0) {
            $('input[id=j' + oldYScore + ']').prop("checked", false);
        }
        $('input[id=j' + rowid + ']').prop("checked", true);
        $("#list8").setSelection(rowid)
    }
}
function jq() {
    ShortName = document.getElementById("COMNameC").value;
    SupplyType = document.getElementById("SupplierType").value;
    AppYear = document.getElementById("ReviewDate").value;
    // SupplyCode = document.getElementById("SID").value;
    $("#list").jqGrid({
        url: 'ReviewGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, sName: ShortName, type: SupplyType, appyear: AppYear, order: $("#Order").val() },
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
        colNames: ['', '', '流水号', '审批编号', '年度编号', '申报部门', '供应商名称', '供应商地址', '供应商编号',
            '供应商简称', '供应商类型', '产品', '服务', '供应商等级', '主要供应产品或服务', '总分', '综合评价结果', '待改进意见', '评审状态', '年审状态'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SIDs', index: 'SIDs', width: 80, hidden: true },
        { name: 'SID', index: 'SID', width: 80 },
        { name: 'PID', index: 'PID', width: 120, hidden: true },
        { name: 'YRID', index: 'YRID', width: 120, hidden: true },
        { name: 'DeptName', index: 'DeptName', width: 120 },
        { name: 'COMNameC', index: 'COMNameC', width: 120 },
        { name: 'ComAddress', index: 'ComAddress', width: 120 },
        { name: 'SID', index: 'SID', width: 120, hidden: true },
        { name: 'COMShortName', index: 'COMShortName', width: 100, hidden: true },
        { name: 'SupplierType', index: 'SupplierType', width: 100, hidden: true },
        { name: 'ProductName', index: 'ProductName', width: 200, hidden: true },
        { name: 'ServiceName', index: 'ServiceName', width: 200, hidden: true },
        { name: 'SupplierClass', index: 'SupplierClass', width: 100, hidden: true },
        { name: 'ScaleType', index: 'ScaleType', width: 100, hidden: true },
        { name: 'Score5', index: 'Score5', width: 80 },
        { name: 'Result', index: 'Result', width: 80 },
        { name: 'ResultDesc', index: 'ResultDesc', width: 80 },
        { name: 'State', index: 'State', width: 80 },
        { name: 'NState', index: 'NState', width: 80 },
        ],
        pager: jQuery('#pager'),
        sortname: 'NState',
        sortable: true,
        optionloadonce: true,
        sortorder: 'desc',
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '年度评审分项信息',
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var str = "<a href='#' style='color:blue' onclick='ShowDetail(\"" + curRowData.SID + "\")' >" + curRowData.SID + "</a>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list").jqGrid('setRowData', ids[i], { SID: str });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            //获得基础表中的SID
            var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
            var yrid = jQuery("#list").jqGrid('getRowData', rowid).YRID;
            var pid = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var fid = jQuery("#list").jqGrid('getRowData', rowid).FID;
            //$("#tab tbody").html("");
            LoadScore(sid);
            select(rowid);
            selRowScore(rowid);
            loadPrduct(sid);
            loadServer(sid);
            loadPlanPro(sid, fid);
            loadPlanSer(sid);
            laodLog(sid);
            loadAward(sid);
            loadPrice(sid);
            loadYScore(sid);
            shenpi();//年度评审
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
            var re_records = $("#list").getGridParam('records');
            if (re_records == 0 || re_records == null) {
                if ($(".norecords").html() == null) {
                    $("#list").parent().append("<div class=\"norecords\" style='text-align:center;margin-top:50px;font-size:20px;'>没有符合数据</div>");
                }
                $(".norecords").show();
            }
            else
                $(".norecords").hide();
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        },
        onSortCol: function (index, iCol, sortorder) {
            OrderDate = index + "@" + sortorder;
            $("#Order").val(OrderDate);
            reload();
        }
    });
}
function selRow(row) {
    newRowID = row.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function selRowScore(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    RelenvceID = jQuery("#list").jqGrid('getRowData', rowid).SID;
    reloadScore();
}
function selChangeScore(rowid) {
    if ($('input[id=d' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldScoreID != 0) {
            $('input[id=d' + oldScoreID + ']').prop("checked", false);
        }
        $('input[id=d' + rowid + ']').prop("checked", true);
        $("#list3").setSelection(rowid)
    }
}
function selChangepro(rowid1) {
    if ($('input[id=k' + rowid1 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldproID != 0) {
            $('input[id=k' + oldproID + ']').prop("checked", false);
        }
        $('input[id=k' + rowid1 + ']').prop("checked", true);
        $("#list1").setSelection(rowid1)
    }
}
function selChangeSer(rowid2) {
    if ($('input[id=e' + rowid2 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSerID != 0) {
            $('input[id=e' + oldSerID + ']').prop("checked", false);
        }
        $('input[id=e' + rowid2 + ']').prop("checked", true);
        $("#list4").setSelection(rowid2)
    }
}
function selChangePlanPro(rowid3) {
    if ($('input[id=f' + rowid3 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldplanPro != 0) {
            $('input[id=f' + oldplanPro + ']').prop("checked", false);
        }
        $('input[id=f' + rowid3 + ']').prop("checked", true);
        $("#list5").setSelection(rowid3)
    }
}
function selChangePlanSer(rowid4) {
    if ($('input[id=g' + rowid4 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldplanSer != 0) {
            $('input[id=g' + oldplanSer + ']').prop("checked", false);
        }
        $('input[id=g' + rowid4 + ']').prop("checked", true);
        $("#list6").setSelection(rowid4)
    }
}
function selChangeLog(rowid9) {
    if ($('input[id=h' + rowid9 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldLog != 0) {
            $('input[id=h' + oldLog + ']').prop("checked", false);
        }
        $('input[id=h' + rowid9 + ']').prop("checked", true);
        $("#list7").setSelection(rowid9)
    }
}
function selAward(rowid) {
    if ($('input[id=m' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldAward != 0) {
            $('input[id=m' + oldAward + ']').prop("checked", false);
        }
        $('input[id=m' + rowid + ']').prop("checked", true);
        $("#list9").setSelection(rowid)
    }
}
function selPrice(rowid) {
    if ($('input[id=n' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldPrice != 0) {
            $('input[id=n' + oldPrice + ']').prop("checked", false);
        }
        $('input[id=n' + rowid + ']').prop("checked", true);
        $("#list10").setSelection(rowid)
    }
}
function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    RelenvceID = jQuery("#list").jqGrid('getRowData', rowid).SID;
    reload1();
}
function reload1() {
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    $("#list2").jqGrid('setGridParam', {
        url: '../SuppliesManage/Condition',
        datatype: 'json',
        postData: { curpage: curPageYear, rownum: OnePageCountYear, SID: sid, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}
function reloadScore() {
    COMShortName = $("#COMShortName").val();
    SupplierType = $("#SupplierType").val();
    ReviewDate = $("#ReviewDate").val();
    //SupplierCode = $("#SID").val();
    $("#list3").jqGrid('setGridParam', {
        url: 'ScoreGrid',
        datatype: 'json',
        postData: { curpage: curPageScore, rownum: OnePageCountScore, ShortName: COMShortName, supplyType: SupplierType, ReDate: ReviewDate, Sid: SID },

    }).trigger("reloadGrid");
}
function jqN() {
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    jQuery("#list2").jqGrid({
        url: '../SuppliesManage/Condition',
        datatype: 'json',
        postData: { curpage: curPageYear, rownum: OnePageCountYear, SID: sid, webkey: webkey, folderBack: folderBack },
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
        { name: 'ApprovalContent', index: 'ApprovalContent', width: 100 },
        { name: 'Num', index: 'Num', width: 100, hidden: true },
        { name: 'stateDesc', index: 'stateDesc', width: 100 },
        { name: 'Opinion', index: 'Opinion', width: $("#bor").width() - 770 },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 100 },
        { name: 'Remark', index: 'Remark', width: 200, hidden: true },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCountYear,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '年度审批情况表',
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
            }
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPageYear == $("#list2").getGridParam("lastpage"))
                    return;
                curPageYear = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPageYear = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPageYear == 1)
                    return;
                curPageYear = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPageYear = 1;
            }
            else {
                curPageYear = $("#pager2 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqScore() {
    COMShortName = $("#COMNameC").val();
    SupplierType = $("#SupplierType").val();
    ReviewDate = $("#ReviewDate").val();
    //SupplierCode = $("#SID").val();
    $("#list3").jqGrid({
        url: 'ScoreGrid',
        datatype: 'json',
        postData: { curpage: curPageScore, rownum: OnePageCountScore, ShortName: COMShortName, supplyType: SupplierType, ReDate: ReviewDate, Sid: SID },
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
        colNames: ['', '序号', '评分人', '质量体系分数', '价格分数', '供货及时率分数', '服务分数', '总分', '综合评价结果', '待改进意见或淘汰原因', '评分时间', '评分部门'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'ID', index: 'ID', width: 50, hidden: true },
        { name: 'username', index: 'username', width: 80 },
        { name: 'Score1', index: 'Score1', width: 80 },
        { name: 'Score2', index: 'Score2', width: 80 },
        { name: 'Score3', index: 'Score3', width: 80 },
        { name: 'Score4', index: 'Score4', width: 80 },
        { name: 'Score5', index: 'Score5', width: 80 },
        { name: 'Result', index: 'Result', width: 80 },
        { name: 'ResultDesc', index: 'ResultDesc', width: 180 },
        { name: 'ReviewDate', index: 'ReviewDate', width: 80 },
        { name: 'DeclareUnit', index: 'DeclareUnit', width: 80 },
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCountScore,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '评分情况',
        gridComplete: function () {
            var ids = jQuery("#list3").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list3").jqGrid('getRowData', id);
                var curChk = "<input id='d" + id + "' onclick='selChangeScore(" + id + ")' type='checkbox' value='" + jQuery("#list3").jqGrid('getRowData', id).SID + "' name='cb'/>";
                jQuery("#list3").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid1, status) {
            if (oldScoreID != 0) {
                $('input[id=d' + oldScoreID + ']').prop("checked", false);
            }
            $('input[id=d' + rowid1 + ']').prop("checked", true);
            oldScoreID = rowid1;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (curPageScore == $("#list3").getGridParam("lastpage"))
                    return;
                curPageScore = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPageScore = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPageScore == 1)
                    return;
                curPageScore = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPageScore = 1;
            }
            else {
                curPageScore = $("#pager3 :input").val();
            }
            reloadScore();
        },
        loadComplete: function () {
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function LoadScore(sid) {
    SID = sid;
    COMShortName = $("#COMShortName").val();
    SupplierType = $("#SupplierType").val();
    ReviewDate = $("#ReviewDate").val();
    //SupplierCode = $("#SID").val();
    $("#list3").jqGrid('setGridParam', {
        url: 'ScoreGrid',
        datatype: 'json',
        postData: { curpage: curPageScore, rownum: OnePageCountScore, ShortName: COMShortName, supplyType: SupplierType, ReDate: ReviewDate, Sid: SID },
    }).trigger("reloadGrid");
}
function loadPrduct(sid) {
    SID = sid;
    PName = $("#PName").val();
    Standard = $("#Standard").val();
    $("#list1").jqGrid('setGridParam', {
        url: 'ManageProGrid',
        datatype: 'json',
        postData: { curpage: curPagePro, rownum: OnePageCountPro, pName: PName, standard: Standard, Sid: SID },
        //loadComplete: function () {
        //    $("#list1").jqGrid("setGridWidth", $("#bor1").width() + 15, false);
        //},
    }).trigger("reloadGrid");
}
function loadServer(sid) {
    SID = sid;
    $("#list4").jqGrid('setGridParam', {
        url: 'ManageServerGrid',
        datatype: 'json',
        postData: { curpage: curPageSer, rownum: OnePageCountSer, Sid: SID },
        //loadComplete: function () {
        //    $("#list2").jqGrid("setGridWidth", $("#bor2").width() + 15, false);
        //},
    }).trigger("reloadGrid");
}
// 资质
function loadPlanPro(sid, fid) {
    SID = sid;
    FID = fid;
    $("#list5").jqGrid('setGridParam', {
        url: 'ManagePlanProGrid',
        datatype: 'json',
        postData: { curpage: curPageZiZhi, rownum: OnePageCountZiZhi, Sid: SID, Fid: FID },
        //loadComplete: function () {
        //    $("#list3").jqGrid("setGridWidth", $("#bor3").width() + 15, false);
        //},
    }).trigger("reloadGrid");
}
//证书
function loadPlanSer(sid) {
    SID = sid;
    $("#list6").jqGrid('setGridParam', {
        url: 'ManagePlanSerGrid',
        datatype: 'json',
        postData: { curpage: curPageZS, rownum: OnePageCountZS, Sid: SID },
        //loadComplete: function () {
        //    $("#list4").jqGrid("setGridWidth", $("#bor4").width() + 15, false);
        //},
    }).trigger("reloadGrid");
}
//日志
function laodLog(sid) {
    SID = sid;
    $("#list7").jqGrid('setGridParam', {
        url: 'LogShowGrid',
        datatype: 'json',
        postData: { curpage: curPageLog, rownum: OnePageCountLog, Sid: SID },
    }).trigger("reloadGrid");
}
function loadYScore(sid) {
    SID = sid;
    $("#list8").jqGrid('setGridParam', {
        url: 'YearShowGrid',
        datatype: 'json',
        postData: { curpage: curPageYScore, rownum: OnePageCountYScore, Sid: SID },
    }).trigger("reloadGrid");
}
function loadAward(sid) {
    SID = sid;
    curPageAward = 1;
    $("#list9").jqGrid('setGridParam', {
        url: 'AwardNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageAward, rownum: OnePageCountAward, Sid: SID },
    }).trigger("reloadGrid");
}
function loadPrice(sid) {
    SID = sid;
    curPagePrice = 1;
    $("#list10").jqGrid('setGridParam', {
        url: 'PriceNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPagePrice, rownum: OnrPageCountPrice, Sid: SID },
    }).trigger("reloadGrid");
}

function jqPro() {
    PName = $("#PName").val();
    Standard = $("#Standard").val();
    jQuery("#list1").jqGrid({
        url: 'ManageProGrid',
        datatype: 'json',
        postData: { curpage: curPagePro, rownum: OnePageCountPro, pName: PName, standard: Standard, Sid: SID },
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
        colNames: ['', '序号', '供应商编号', '产品分类', '产品编号', '产品名称', '规格型号', '单位', '详细说明', '参考价格', '产地', '文档名称', '时间', '操作'],
        colModel: [
         { name: 'IDCheck', index: 'id', width: 50, hidden: true },
         { name: 'ID', index: 'ID', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 50, hidden: true },
        { name: 'Ptype', index: 'Ptype', width: 80 },
        { name: 'ProductID', index: 'ProductID', width: 100 },
        { name: 'ProductName', index: 'ProductName', width: 100 },
        { name: 'Standard', index: 'Standard', width: 100 },
        { name: 'MeasureUnit', index: 'MeasureUnit', width: 50 },
        { name: 'DetailDesc', index: 'DetailDesc', width: 150 },
        { name: 'Price', index: 'Price', width: 80 },
        { name: 'OriginPlace', index: 'OriginPlace', width: 100 },
        { name: 'FFileName', index: 'FFileName', width: 100 },
        { name: 'CreateTime', index: 'CreateTime', width: 100, hidden: true },
        { name: 'Opration', index: 'Opration', width: 80 },
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCountPro,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '拟购产品',
        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='k" + id + "' onclick='selChangepro(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "<a onclick='CheckProduct(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list1").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid1, status) {
            if (oldproID != 0) {
                $('input[id=k' + oldproID + ']').prop("checked", false);
            }
            $('input[id=k' + rowid1 + ']').prop("checked", true);
            oldproID = rowid1;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPagePro == $("#list1").getGridParam("lastpage"))
                    return;
                curPagePro = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPagePro = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPagePro == 1)
                    return;
                curPagePro = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPagePro = 1;
            }
            else {
                curPagePro = $("#pager1 :input").val();
            }
            reloadpro();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqSer() {
    jQuery("#list4").jqGrid({
        url: 'ManageServerGrid',
        datatype: 'json',
        postData: { curpage: curPageSer, rownum: OnePageCountSer, Sid: SID },
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
        colNames: ['', '序号', '服务名称', '服务编号', '服务描述', '用途', '文档名称', '时间', '操作'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 50, hidden: true },
        { name: 'ServiceName', index: 'ServiceName', width: 100 },
         { name: 'ServiceID', index: 'ServiceID', width: 100, hidden: true },
        { name: 'ServiceDesc', index: 'ServiceDesc', width: 200 },
        { name: 'Purpose', index: 'Purpose', width: 150 },
        { name: 'FFileName', index: 'FFileName', width: 150 },
        { name: 'CreateTime', index: 'CreateTime', width: 90, hidden: true },
        { name: 'Opration', index: 'Opration', width: 150 },
        ],
        pager: jQuery('#pager4'),
        pgbuttons: true,
        rowNum: OnePageCountSer,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '拟购服务',
        gridComplete: function () {
            var ids = jQuery("#list4").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list4").jqGrid('getRowData', id);
                var curChk = "<input id='e" + id + "' onclick='selChangeSer(" + id + ")' type='checkbox' value='" + jQuery("#list4").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "  <a onclick='ServerOut(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list4").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list4").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid2, status) {
            if (oldSerID != 0) {
                $('input[id=e' + oldSerID + ']').prop("checked", false);
            }
            $('input[id=e' + rowid2 + ']').prop("checked", true);
            oldSerID = rowid2;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager4") {
                if (curPageSer == $("#list4").getGridParam("lastpage"))
                    return;
                curPageSer = $("#list4").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager4") {
                curPageSer = $("#list4").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager4") {
                if (curPageSer == 1)
                    return;
                curPageSer = $("#list4").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager4") {
                curPageSer = 1;
            }
            else {
                curPageSer = $("#pager4 :input").val();
            }
            reloadServer();
        },
        loadComplete: function () {
            $("#list4").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list4").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPlanPro() {
    jQuery("#list5").jqGrid({
        url: 'ManagePlanProGrid',
        datatype: 'json',
        postData: { curpage: curPageZiZhi, rownum: OnePageCountZiZhi, Sid: SID, Fid: FID },
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
        colNames: ['', '序号', '内容项', 'FID', '其他类说明', '文件具体项目', '其他项说明', '文档名称', '文档类型', '时间', '操作'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 50, hidden: true },
        { name: 'TypeO', index: 'TypeO', width: 200 },
        { name: 'FID', index: 'FID', width: 50, hidden: true },
        { name: 'FType', index: 'FType', width: 100, hidden: true },
        { name: 'Item', index: 'Item', width: 150 },
        { name: 'ItemO', index: 'ItemO', width: 150 },
        { name: 'FFileName', index: 'FFileName', width: 150 },
        { name: 'FileType', index: 'FileType', width: 80, hidden: true },
        { name: 'CreateTime', index: 'CreateTime', width: 130 },
        { name: 'Opration', index: 'Opration', width: 150 },
        ],
        pager: jQuery('#pager5'),
        pgbuttons: true,
        rowNum: OnePageCountZiZhi,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '资质管理',
        gridComplete: function () {
            var ids = jQuery("#list5").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list5").jqGrid('getRowData', id);
                var curChk = "<input id='f" + id + "' onclick='selChangePlanPro(" + id + ")' type='checkbox' value='" + jQuery("#list5").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "  <a onclick='DownFile(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list5").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list5").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid3, status) {
            if (oldplanPro != 0) {
                $('input[id=f' + oldplanPro + ']').prop("checked", false);
            }
            $('input[id=f' + rowid3 + ']').prop("checked", true);
            oldplanPro = rowid3;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager5") {
                if (curPageZiZhi == $("#list5").getGridParam("lastpage"))
                    return;
                curPageZiZhi = $("#list5").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager5") {
                curPageZiZhi = $("#list5").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager5") {
                if (curPageZiZhi == 1)
                    return;
                curPageZiZhi = $("#list5").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager5") {
                curPageZiZhi = 1;
            }
            else {
                curPageZiZhi = $("#pager5 :input").val();
            }
            reloadServer();
        },
        loadComplete: function () {
            $("#list5").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list5").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPlanSer() {
    jQuery("#list6").jqGrid({
        url: 'ManagePlanSerGrid',
        datatype: 'json',
        postData: { curpage: curPageZS, rownum: OnePageCountZS, Sid: SID },
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
        colNames: ['', '序号', '是否为计划性证书', '证书类型', '证书名称', '证书编号', '证书认证机构', '通过认证时间', '文档名称', '文档类型', '时间', '操作', 'FID'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 50, hidden: true },
        { name: 'IsPlan', index: 'IsPlan', width: 80 },
        { name: 'CType', index: 'CType', width: 100 },
        { name: 'CName', index: 'CName', width: 150 },
        { name: 'CCode', index: 'CCode', width: 150 },
        { name: 'COrganization', index: 'COrganization', width: 150 },
        { name: 'CDate', index: 'CDate', width: 120 },
        { name: 'CFileName', index: 'CFileName', width: 150 },
        { name: 'FileType', index: 'FileType', width: 80, hidden: true },
        { name: 'CreateTime', index: 'CreateTime', width: 80 },
        { name: 'Opration', index: 'Opration', width: 150 },
         { name: 'FID', index: 'FID', width: 100, hidden: true },
        ],
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCountZS,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '证书管理',
        gridComplete: function () {
            var ids = jQuery("#list6").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list6").jqGrid('getRowData', id);
                var curChk = "<input id='g" + id + "' onclick='selChangePlanSer(" + id + ")' type='checkbox' value='" + jQuery("#list6").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "  <a onclick='DownZhenshu(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list6").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list6").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid4, status) {
            if (oldplanSer != 0) {
                $('input[id=g' + oldplanSer + ']').prop("checked", false);
            }
            $('input[id=g' + rowid4 + ']').prop("checked", true);
            oldplanSer = rowid4;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager6") {
                if (curPageZS == $("#list6").getGridParam("lastpage"))
                    return;
                curPageZS = $("#list6").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager6") {
                curPageZS = $("#list6").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager6") {
                if (curPageZS == 1)
                    return;
                curPageZS = $("#list6").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager6") {
                curPageZS = 1;
            }
            else {
                curPageZS = $("#pager6:input").val();
            }
            reloadPSer();
        },
        loadComplete: function () {
            $("#list6").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list6").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqLog() {
    jQuery("#list7").jqGrid({
        url: 'LogShowGrid',
        datatype: 'json',
        postData: { curpage: curPageLog, rownum: OnePageCountLog, Sid: SID },
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
        colNames: ['', '供应商编号', '日志标题', '日志内容', '记录时间', '记录人', '日志类型'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'UserId', index: 'UserId', width: 100 },
        { name: 'LogTitle', index: 'LogTitle', width: 180 },
        { name: 'LogContent', index: 'LogContent', width: 130 },
        { name: 'LogTime', index: 'LogTime', width: 150 },
        { name: 'LogPerson', index: 'LogPerson', width: 150 },
        { name: 'Type', index: 'Type', width: 150 },
        ],
        pager: jQuery('#pager7'),
        pgbuttons: true,
        rowNum: OnePageCountLog,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '日志记录',
        gridComplete: function () {
            var ids = jQuery("#list7").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list7").jqGrid('getRowData', id);
                var curChk = "<input id='h" + id + "' onclick='selChangeLog(" + id + ")' type='checkbox' value='" + jQuery("#list7").jqGrid('getRowData', id).SID + "' name='cb'/>";
                jQuery("#list7").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid9, status) {
            if (oldLog != 0) {
                $('input[id=h' + oldLog + ']').prop("checked", false);
            }
            $('input[id=h' + rowid9 + ']').prop("checked", true);
            oldLog = rowid9;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager7") {
                if (curPageLog == $("#list7").getGridParam("lastpage"))
                    return;
                curPageLog = $("#list7").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager7") {
                curPageLog = $("#list7").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager7") {
                if (curPageLog == 1)
                    return;
                curPageLog = $("#list7").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager7") {
                curPageLog = 1;
            }
            else {
                curPageLog = $("#pager7 :input").val();
            }
            reloadLog();
        },
        loadComplete: function () {
            $("#list7").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list7").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqYScore() {
    jQuery("#list8").jqGrid({
        url: 'YearShowGrid',
        datatype: 'json',
        postData: { curpage: curPageYScore, rownum: OnePageCountYScore, Sid: SID },
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
        colNames: ['', '供应商编号', '年度评审编号', '年度评审年份', '年度评审意见', '评审人', '部门'],
        colModel: [
         { name: 'IDCheck', index: 'id', width: 50, hidden: true },
         { name: 'SID', index: 'SID', width: 50, hidden: true },
        { name: 'YRID', index: 'YRID', width: 80 },
        { name: 'Year', index: 'Year', width: 80, hidden: true },
        { name: 'ResultDesc', index: 'ResultDesc', width: 100 },
        { name: 'DeclareUser', index: 'DeclareUser', width: 100 },
        { name: 'DeclareUnit', index: 'DeclareUnit', width: 100 },
        ],
        pager: jQuery('#pager8'),
        pgbuttons: true,
        rowNum: OnePageCountYScore,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '年度评审情况',
        gridComplete: function () {
            var ids = jQuery("#list8").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list8").jqGrid('getRowData', id);
                var curChk = "<input id='j" + id + "' onclick='selChangeYScore(" + id + ")' type='checkbox' value='" + jQuery("#list8").jqGrid('getRowData', id).SID + "' name='cb'/>";
                jQuery("#list8").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid1, status) {
            if (oldYScore != 0) {
                $('input[id=j' + oldYScore + ']').prop("checked", false);
            }
            $('input[id=j' + rowid1 + ']').prop("checked", true);
            oldYScore = rowid1;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager8") {
                if (curPageYScore == $("#list8").getGridParam("lastpage"))
                    return;
                curPageYScore = $("#list8").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager8") {
                curPageYScore = $("#list8").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager8") {
                if (curPageYScore == 1)
                    return;
                curPageYScore = $("#list8").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager8") {
                curPageYScore = 1;
            }
            else {
                curPageYScore = $("#pager8 :input").val();
            }
            reloadYearScore();
        },
        loadComplete: function () {
            // $("#list8").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list8").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function OutExcel() {
    var record = $("#list").getGridParam("reccount");
    if (record == 0) {
        alert("列表内容为空，没有要导出的数据，不能进行导出操作");
        return false;
    }
    else {
        var one = confirm("确定将列表内容导出吗？")
        if (one == false) {
            return false;
        }
        else {
            return true;
        }
    }
}
function jqAward() {
    jQuery("#list9").jqGrid({
        url: 'AwardNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageAward, rownum: OnePageCountAward, Sid: SID },
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
        colNames: ['', 'ID', '序号', '奖项名称', '上传时间', '操作'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 100, hidden: true },
        { name: 'ID', index: 'ID', width: 150, hidden: true },
        { name: 'SID', index: 'SID', width: 150, hidden: true },
        { name: 'Award', index: 'Award', width: 250 },
        { name: 'AwardTime', index: 'AwardTime', width: 250 },
        { name: 'Opration', index: 'Opration', width: 80 },
        ],
        pager: jQuery('#pager9'),
        pgbuttons: true,
        rowNum: OnePageCountAward,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '曾获奖项',
        gridComplete: function () {
            var ids = jQuery("#list9").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list9").jqGrid('getRowData', id);
                var curChk = "<input id='m" + id + "' onclick='selAward(" + id + ")' type='checkbox' value='" + jQuery("#list9").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "<a onclick='DownloadAward(" + id + ")' style='color:blue;cursor:pointer;'>下载</a>";
                jQuery("#list9").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list9").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldAward != 0) {
                $('input[id=m' + oldAward + ']').prop("checked", false);
            }
            $('input[id=m' + rowid + ']').prop("checked", true);
            oldAward = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list9").getGridParam("lastpage"))
                    return;
                curPage = $("#list9").getGridParam("page9") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list9").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list9").getGridParam("page9") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager9 :input").val();
            }
            reloadWard();
        },
        loadComplete: function () {
            //var re_records = $("#list7").getGridParam('records');
            //if (re_records == 0 || re_records == null) {
            //    if ($(".norecords").html() == null) {
            //        $("#list7").parent().append("<div class=\"norecords\" style='text-align:center;margin-top:50px;font-size:20px;'>没有符合数据</div>");
            //    }
            //    $(".norecords").show();
            //}
            //else
            //    $(".norecords").hide();
            $("#list9").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPrice() {
    jQuery("#list10").jqGrid({
        url: 'PriceNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPagePrice, rownum: OnrPageCountPrice, Sid: SID },
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
        colNames: ['', 'ID', '序号', '报价/比价单名称', '上传时间', '操作'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 100, hidden: true },
        { name: 'ID', index: 'ID', width: 150, hidden: true },
        { name: 'SID', index: 'SID', width: 150, hidden: true },
        { name: 'PriceName', index: 'PriceName', width: 250 },
        { name: 'PriceTime', index: 'PriceTime', width: 250 },
         { name: 'Opration', index: 'Opration', width: 80 },
        ],
        pager: jQuery('#pager10'),
        pgbuttons: true,
        rowNum: OnrPageCountPrice,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '报价/比价单',
        gridComplete: function () {
            var ids = jQuery("#list10").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list10").jqGrid('getRowData', id);
                var curChk = "<input id='n" + id + "' onclick='selPrice(" + id + ")' type='checkbox' value='" + jQuery("#list10").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "<a onclick='DownloadPrice(" + id + ")' style='color:blue;cursor:pointer;'>下载</a>";
                jQuery("#list10").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list10").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldPrice != 0) {
                $('input[id=n' + oldPrice + ']').prop("checked", false);
            }
            $('input[id=n' + rowid + ']').prop("checked", true);
            oldPrice = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list10").getGridParam("lastpage"))
                    return;
                curPage = $("#list10").getGridParam("page10") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list10").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list10").getGridParam("page10") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager10 :input").val();
            }
            reloadPrice();
        },
        loadComplete: function () {
            //var re_records = $("#list").getGridParam('records');
            //if (re_records == 0 || re_records == null) {
            //    if ($(".norecords").html() == null) {
            //        $("#list").parent().append("<div class=\"norecords\" style='text-align:center;margin-top:50px;font-size:20px;'>没有符合数据</div>");
            //    }
            //    $(".norecords").show();
            //}
            //else
            //    $(".norecords").hide();
            $("#list10").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function ShowDetail(sid) {

    window.parent.OpenDialog("详细内容", "../SuppliesManage/DetailApp?sid=" + sid, 700, 700, '');
}

function shenpi() {
    var Job = $("#Exjob").val();
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    if (rowid == null) {
        alert("您还没有选择要处理的年度评审");
        return;
    }
    else {
        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID; //审批编号无
        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        var state = jQuery("#list").jqGrid('getRowData', rowid).NState; //状态无
        if (state == $("#Nostate").val()) {
            alert("审批不通过，不能进行审批了");
            return;
        }
        else if (state != "待公司级审批") {
            return;
        }
        else if (Job != "董事长" && Job != "总经理" && Job != "副总经理" && Job != "公司领导") {
            return;
        }
        $.ajax({
            url: "../SuppliesManage/JudgeAppDisable",
            type: "post",
            data: { data1: $("#webkey").val(), SPID: SPID, Job: Job },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    var bol = data.intblo;
                    if (bol == "-1" && Job == "副总经理") {
                        window.parent.OpenDialog("年度审批", "../SuppliesManage/Approvalnd?id=" + SID, 900, 500, '');
                        return;
                    }

                    if (bol == "1" && Job == "总经理") {//副总经理审批

                        window.parent.OpenDialog("年度审批", "../SuppliesManage/Approvalnd?id=" + SID, 900, 500, '');
                        return;
                    }

                    if (bol == "2" && Job == "董事长") {//总经理才能登陆

                        window.parent.OpenDialog("年度审批", "../SuppliesManage/Approvalnd?id=" + SID, 900, 500, '');
                        return;
                    }

                }
            }
        });
        //window.parent.OpenDialog("年度审批", "../SuppliesManage/Approvalnd?id=" + SID, 1200, 500, '');
        //else {
        //    $.ajax({
        //        url: "../COM_Approval/JudgeAppDisable",
        //        type: "post",
        //        data: { data1: $("#webkey").val(), data2: SPID },
        //        dataType: "Json",
        //        success: function (data) {
        //            if (data.success == "true") {
        //                var bol = data.intblo;
        //                if (bol == "-1") {
        //                    alert("您没有审批权限，不能进行审批操作");
        //                    return;
        //                }
        //                if (bol == "1") {
        //                    alert("您已经审批完成，不能进行审批操作");
        //                    return;
        //                }
        //                if (bol == "2") {
        //                    alert("审批过程还没有进行到您这一步，不能进行审批操作");
        //                    return;
        //                }
        //                var texts = $("#webkey").val() + "@" + SPID + "@" + SID;
        //                window.parent.OpenDialog("年度审批", "../COM_Approval/ApprovalzhunchuSup?id=" + texts, 1200, 500, '');
        //            }
        //            else {
        //                return;
        //            }
        //        }
        //    });
    }
}
function DownloadAward(id) {
    var model = jQuery("#list9").jqGrid('getRowData', id);
    var Proid = model.ID;//唯一编号
    //var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    //var job = $("#Department").val();
    window.open("DownLoadAward?sid=" + Proid);

}
function DownloadPrice(id) {
    var model = jQuery("#list10").jqGrid('getRowData', id);
    var Proid = model.ID;//唯一编号

    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    window.open("DownLoadPrice?sid=" + Proid);

}
function CheckProduct(id) {
    var model = jQuery("#list1").jqGrid('getRowData', id);
    var Proid = model.ID;//唯一编号
    var filename = model.FFileName;
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (filename == "") {
        alert("没有文件可以查看"); return;
    }
    else {
        window.open("../SuppliesManage/DownLoadSUProduct?sid=" + Proid);
    }
}
function ServerOut(id) {
    var model = jQuery("#list4").jqGrid('getRowData', id);
    var Proid = model.ServiceID;//唯一编号
    var filename = model.FFileName;

    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (filename == "") {
        alert("没有文件可以查看"); return;
    }
    else {
        window.open("../SuppliesManage/DownLoadNewServer?sid=" + Proid);
    }
}
function DownFile(id) {
    var model = jQuery("#list5").jqGrid('getRowData', id);
    var Proid = model.FID;//唯一编号
    var filename = model.FFileName;

    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (filename == "") {
        alert("没有文件可以查看"); return;
    }
    else {
        window.open("../SuppliesManage/DownLoadNewFile?sid=" + Proid);
    }
}
function DownZhenshu(id) {
    var model = jQuery("#list6").jqGrid('getRowData', id);
    var Proid = model.FID;//唯一编号
    var filename = model.CFileName;

    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (filename == "") {
        alert("没有文件可以查看"); return;
    }
    else {
        window.open("../SuppliesManage/DownLoadZhenshu?sid=" + Proid);
    }
}
window.onload = function () {
    document.getElementsByTagName("body")[0].onkeydown = function () {

        //获取事件对象  
        var elem = event.relatedTarget || event.srcElement || event.target || event.currentTarget;

        if (event.keyCode == 8) {//判断按键为backSpace键  

            //获取按键按下时光标做指向的element  
            var elem = event.srcElement || event.currentTarget;

            //判断是否需要阻止按下键盘的事件默认传递  
            var name = elem.nodeName;

            if (name != 'INPUT' && name != 'TEXTAREA') {
                return _stopIt(event);
            }
            var type_e = elem.type.toUpperCase();
            if (name == 'INPUT' && (type_e != 'TEXT' && type_e != 'TEXTAREA' && type_e != 'PASSWORD' && type_e != 'FILE')) {
                return _stopIt(event);
            }
            if (name == 'INPUT' && (elem.readOnly == true || elem.disabled == true)) {
                return _stopIt(event);
            }
        }
    }

    function _stopIt(e) {
        if (e.returnValue) {
            e.returnValue = false;
        }
        if (e.preventDefault) {
            e.preventDefault();
        }

        return false;
    }
}