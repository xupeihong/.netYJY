var COMNameC, SupplierType, COMArea, ReviewDate, Opinions, ApprovalState, SID, PID, FID;
var curPage = 1, curPageQK = 1, curPageLog = 1, curPagePro = 1, curPageSer = 1, curPageZiZhi = 1, curPageZS = 1, curPageSPR = 1, curPageRS = 1;
var OnePageCount = 15, OnePageCountQK = 15, OnePageCountSPR = 15, OnePageCountRS = 15;
var OnePageCountLog = 10, OnePageCountPro = 10, OnePageCountSer = 10, OnePageCountZiZhi = 10, OnePageCountZS = 10;
var oldSelID = 0, CountRows = 0, oldSelWeiguiID = 0, oldproID = 0, oldSerID = 0, oldplanPro = 0, oldplanSer = 0, oldLog = 0, oldSPR = 0, oldRS = 0;
var curPageScore = 1;
var OnePageCountScore = 15;
var oldScoreID = 0;
var curPageAward = 1;
var curPagePrice = 1;
var OnePageCountAward = 15;
var OnrPageCountPrice = 15;
var oldAward = 0;
var oldPrice = 0;
$(document).ready(function () {
    var Job = $("#Exjob").val();
    if (Job == "董事长" || Job == "总经理" || Job == "副总经理" || Job == "公司领导") {
        //$("#shenpi").show();//审批
        //$("#ApprovalBackSup").show();//恢复供应商审批
        //$("#SugestionBackSup").hide();//恢复供应商建议
        $("#SubApprovalBackSup").hide();//提交恢复供应商审批
        $("#BackSup").hide();//申请恢复供应商
        //$("#FZR").hide();//准出审批
        $("#bumen").hide();//部门级恢复
        $("#SubApprovalBackSup").show();//公司级恢复
    }
    else if (Job == "经理" || Job == "副经理") {
        //$("#shenpi").hide();
        $("#bumen").show();
        //$("#ApprovalBackSup").hide();
        //$("#SugestionBackSup").show();
        $("#SubApprovalBackSup").show();
        $("#BackSup").show();
        $("#SubApprovalBackSup").hide();//公司级恢复
        //$("#FZR").show();
    }
    else {
        //$("#shenpi").hide();
        $("#bumen").hide();
        //$("#ApprovalBackSup").hide();
        //$("#SugestionBackSup").hide();
        $("#SubApprovalBackSup").hide();
        $("#BackSup").show();
        $("#SubApprovalBackSup").hide();//公司级恢复
        //$("#FZR").hide();
    }

    jqPro();
    jq();
    jqAward();
    jqPrice();
    jqSer();
    jqPlanPro();
    jqPlanSer();
    jqLog();
    jqW();
    jqScore();
    $("#two").hide();
    $("#three").hide();
    $("#four").hide();
    $("#five").hide();
    $("#six").hide();
    $("#seven").hide();
    $("#eight").hide();
    $("#nine").hide();
    $("#Product").click(function () {
        this.className = "btnTw";
        $('#btnAward').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#spQ').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#one").css("display", "");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");
    });
    $("#Server").click(function () {

        this.className = "btnTw";
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");
        $('#Product').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#spQ').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");

        $("#two").css("display", "");
        $("#one").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");
    });
    $("#planPro").click(function () {

        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#spQ').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#three").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");

    });
    $("#planServer").click(function () {

        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#spQ').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#four").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");

    });
    $("#btnLog").click(function () {

        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#spQ').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#five").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");

    });
    $("#spQ").click(function () {

        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#six").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#seven").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");

    });
    $("#ScoreMsg").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#spQ').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#seven").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");
    })
    $("#btnAward").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#spQ').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");


        $("#eight").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#nine").css("display", "none");


    })
    $("#btnPrice").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#spQ').attr("class", "btnTh");
        $('#ScoreMsg').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");


        $("#nine").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#six").css("display", "none");
        $("#seven").css("display", "none");
        $("#eight").css("display", "none");


    })
    //$("#shenpi").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("您还没有选择要处理的审批");
    //        return;
    //    } else {
    //        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    //        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    //        var state2 = jQuery("#list").jqGrid('getRowData', rowid).state;

    //        var Job = $("#Exjob").val();
    //        if (state2 == "合格供应商" || state2 == "恢复供应商未通过") {
    //            alert("审批不通过，不能进行审批了");
    //            return;
    //        }
    //        if (state2 != "建议停止供货待审批" && state2 != "建议暂停供货待审批" && state2 != "建议淘汰供应商待审批") {
    //            alert("只有董事长、总经理、副总经理才有审批权限");
    //            return;
    //        }
    //        if (state2 == "建议停止供货待审批")
    //            $("#webkey").val("准出停止供货评审");
    //        else if (state2 == "建议暂停供货待审批")
    //            $("#webkey").val("准出暂停供货评审");
    //        else if (state2 == "建议淘汰供应商待审批")
    //            $("#webkey").val("准出淘汰供应商评审");
    //        else if (state2 == "建议恢复供应商待审批")
    //            $("#webkey").val("恢复供应商");
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
    //                    window.parent.OpenDialog("审批", "../COM_Approval/ApprovalSup?id=" + texts, 600, 400, '');
    //                }
    //                else {
    //                    return;
    //                }
    //            }
    //        });

    //    }
    //})
    //根据查询条件准出打印
    $("#daying").click(function () {
        COMNameC = $("#COMNameC").val();
        SupplierType = $("#SupplierType").val();
        COMArea = $("#COMArea").val();
        //ReviewDate = $("#ReviewDate").val();
        //Opinions = $("#Opinions").val();
        ApprovalState = $("#ApprovalState").val();
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        if (rowid == null) {
            alert("请选择列表一条数据");
            return;
        }
        else {
            window.showModalDialog("../SuppliesManage/OutPrint?name=" + COMNameC + "&type=" + SupplierType + "&area=" + COMArea + "&state=" + ApprovalState + "&sid=" + sid, window, "dialogWidth:700px;dialogHeight:300px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    })
    //现在此按钮作废
    //$("#FZR").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("您还没有选择违规处理审批");
    //        return;
    //    } else {
    //        var state = jQuery("#list").jqGrid('getRowData', rowid).WState;
    //        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    //        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    //        var opinions = jQuery("#list").jqGrid('getRowData', rowid).Opinions;
    //        //负责人权限问题
    //        if (state == "待审批") {
    //            alert("该项目已提交准入审批，不能进行违规申请");
    //            return;
    //        } if (state == "新增") {
    //            alert("还没有进行违规处理申请");
    //            return;
    //        }
    //        if (state == "负责人审批完成") {
    //            alert("负责人已审批完成，不能重复操作");
    //            return;
    //        }
    //        if (opinions != "") {
    //            alert("准出处理已经处理，不能重复操作");
    //            return;
    //        }
    //    }
    //    var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    //    window.parent.OpenDialog("准出审批", "../SuppliesManage/FZRSP?sid=" + sid, 900, 400, '');
    //})
    $("#BackSup").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        var state = jQuery("#list").jqGrid('getRowData', rowid).state;
        var nstate = jQuery("#list").jqGrid('getRowData', rowid).nstate;
        if (rowid == null) {
            alert("请选择要申请恢复的供应商");
            return;
        } else {
            if (state == "淘汰供应商" || state == "未通过恢复供应商" || state == "恢复供应商未通过" || state == "未通过公司级恢复审批" || state == "未通过部门级恢复审批" || nstate == "公司级审批未通过") {
                window.parent.OpenDialog("恢复供应商", "../SuppliesManage/BackSup?sid=" + sid, 800, 400, '');
            }
            else {
                alert("不能申请恢复供应商");
                return;
            }
        }
    })
    //部门级恢复
    //$("#bumen").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    //    var state = jQuery("#list").jqGrid('getRowData', rowid).state;
    //    if (rowid == null) {
    //        alert("请选择要部门级审批的供应商");
    //        return;
    //    } else {
    //        if (state == "待部门级恢复审批") {
    //            window.parent.OpenDialog("部门级恢复审批", "../SuppliesManage/BackSupbumen?sid=" + sid, 750, 400, '');
    //        }
    //        else {
    //            alert("不能进行部门级恢复审批");
    //            return;
    //        }
    //    }
    //})


    //恢复供应商建议
    //$("#SugestionBackSup").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    var Job = $("#Exjob").val();
    //    if (rowid == null) {
    //        alert("您还没有选择要恢复的供应商");
    //        return;
    //    }
    //    else {
    //        var state = jQuery("#list").jqGrid('getRowData', rowid).state;
    //        if (state != '已提交恢复申请' && Job != "经理" || Job != "副经理") {
    //            //1:内部评审完成
    //            alert("该供应商还不能填写恢复供应商建议/您选择的供应商已填写恢复供应商建议");
    //            return;
    //        }
    //        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    //        window.parent.OpenDialog("恢复供应商建议", "../SuppliesManage/BackSugestionApproval?sid=" + sid, 750, 400, '');
    //    }

    //})
    //公司级恢复 改成部门级恢复
    $("#bumen").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要公司级审批的供应商");
            return;
        }
        else {
            var state = jQuery("#list").jqGrid('getRowData', rowid).state;
            var nstate = jQuery("#list").jqGrid('getRowData', rowid).nstate;
            var Job = $("#Exjob").val();
            if (state != "待部门级恢复审批" && nstate != "待部门级审批") {
                alert("不能进行部门级恢复审批");
                return;
            }
        }

        //var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "恢复供应商";
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;

        window.parent.OpenDialog("部门级恢复审批", "../SuppliesManage/SubmitRcover?id=" + sid, 700, 450, '');
    })
    //恢复供应商审批
    //$("#ApprovalBackSup").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("您还没有选择要恢复成合格供应商的审批");
    //        return;
    //    }
    //    else {
    //        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    //        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    //        var state = jQuery("#list").jqGrid('getRowData', rowid).state;
    //        var Job = $("#Exjob").val();
    //        if (state == $("#Nostate").val()) {
    //            alert("审批不通过，不能进行审批了");
    //            return;
    //        }
    //        if (state != "建议恢复供应商待审批") {
    //            alert("该供应商还不能进行恢复操作"); return;
    //        }
    //        else if (state == "建议恢复供应商待审批")
    //            $("#webkey").val("恢复供应商");
    //        else {
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
    //                    window.parent.OpenDialog("恢复供应商审批", "../COM_Approval/ApprovalSup?id=" + texts, 600, 400, '');
    //                }
    //                else {
    //                    return;
    //                }
    //            }
    //        });
    //    }
    //})
    //LoadDetail(SID);


})
function reload() {
    COMNameC = $("#COMNameC").val();
    SupplierType = $("#SupplierType").val();
    COMArea = $("#COMArea").val();
    //ReviewDate = $("#ReviewDate").val();
    //Opinions = $("#Opinions").val();
    ApprovalState = $("#ApprovalState").val();
    $("#list").jqGrid('setGridParam', {
        url: 'WeiguiGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, comName: COMNameC, suptype: SupplierType, comarea: COMArea, state: ApprovalState, order: $("#Order").val() },
    }).trigger("reloadGrid");
}
function reloadRefresh() {
    curPage = 1;
    COMNameC = $("#COMNameC").val();
    SupplierType = $("#SupplierType").val();
    COMArea = $("#COMArea").val();
    //ReviewDate = $("#ReviewDate").val();
    //Opinions = $("#Opinions").val();
    ApprovalState = $("#ApprovalState").val();
    $("#list").jqGrid('setGridParam', {
        url: 'WeiguiGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, comName: COMNameC, suptype: SupplierType, comarea: COMArea, state: ApprovalState, order: $("#Order").val() },
    }).trigger("reloadGrid");
}
function reloadpro() {
    PName = $("#PName").val();
    Standard = $("#Standard").val();
    $("#list1").jqGrid('setGridParam', {
        url: 'ManageProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPagePro, rownum: OnePageCountPro, pName: PName, standard: Standard, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadServer() {
    $("#list3").jqGrid('setGridParam', {
        url: 'ManageServerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageSer, rownum: OnePageCountSer, Sid: SID },
    }).trigger("reloadGrid");
}
//资质信息
function reloadPlanPro() {
    $("#list4").jqGrid('setGridParam', {
        url: 'ManagePlanProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZiZhi, rownum: OnePageCountZiZhi, Sid: SID, Fid: FID },
    }).trigger("reloadGrid");
}
//证书信息
function reloadPSer() {
    $("#list5").jqGrid('setGridParam', {
        url: 'ManagePlanSerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZS, rownum: OnePageCountZS, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadLog() {
    $("#list6").jqGrid('setGridParam', {
        url: 'LogShowGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageLog, rownum: OnePageCountLog, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadWard() {
    $("#list8").jqGrid('setGridParam', {
        url: 'AwardNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageAward, rownum: OnePageCountAward, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadPrice() {
    $("#list9").jqGrid('setGridParam', {
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
function selChangepro(rowid1) {
    if ($('input[id=d' + rowid1 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldproID != 0) {
            $('input[id=d' + oldproID + ']').prop("checked", false);
        }
        $('input[id=d' + rowid1 + ']').prop("checked", true);
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
        $("#list3").setSelection(rowid2)
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
        $("#list4").setSelection(rowid3)
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
        $("#list5").setSelection(rowid4)
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
        $("#list6").setSelection(rowid9)
    }
}
function selAward(rowid) {
    if ($('input[id=j' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldAward != 0) {
            $('input[id=j' + oldAward + ']').prop("checked", false);
        }
        $('input[id=j' + rowid + ']').prop("checked", true);
        $("#list8").setSelection(rowid)
    }
}
function selPrice(rowid) {
    if ($('input[id=k' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldPrice != 0) {
            $('input[id=k' + oldPrice + ']').prop("checked", false);
        }
        $('input[id=k' + rowid + ']').prop("checked", true);
        $("#list9").setSelection(rowid)
    }
}
function jq() {
    COMNameC = $("#COMNameC").val();
    SupplierType = $("#SupplierType").val();
    COMArea = $("#COMArea").val();
    //ReviewDate = $("#ReviewDate").val();
    //Opinions = $("#Opinions").val();
    ApprovalState = $("#ApprovalState").val();
    $("#list").jqGrid({
        url: 'WeiguiGrid ',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, comName: COMNameC, suptype: SupplierType, comarea: COMArea, state: ApprovalState, order: $("#Order").val() },
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
        colNames: ['', '', '流水号', '审批编号', '供应商类别', '供应商名称', '供应商地址', '产品', '服务', '申报部门', '申请日期', '申请处理原因', '采购/供应商管理人员', '处理意见', '详细内容', '违规状态', '反馈内容', '职位', '所属部门', '淘汰时间', '评审状态', '年审状态', '当前状态'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 30, hidden: true },
        { name: 'SIDs', index: 'SIDs', width: 70, hidden: true },
        { name: 'SID', index: 'SID', width: 70 },
        { name: 'PID', index: 'PID', width: 120, hidden: true },
        { name: 'SupplierType', index: 'SupplierType', width: 100 },
        { name: 'COMNameC', index: 'COMNameC', width: 200 },
        { name: 'ComAddress', index: 'ComAddress', width: 100 },
        { name: 'ProductName', index: 'ProductName', width: 200 },
        { name: 'ServiceName', index: 'ServiceName', width: 200 },
        { name: 'DeclareUnit', index: 'DeclareUnit', hidden: true, width: 100 },
        { name: 'ReviewDate', index: 'ReviewDate', width: 150, hidden: true },
        { name: 'Reason', index: 'Reason', width: 90, hidden: true },
        { name: 'DeclareUser', index: 'DeclareUser', width: 100, hidden: true },
        { name: 'Opinions', index: 'Opinions', width: 150, hidden: true },
        { name: 'OpinionsD', index: 'OpinionsD', hidden: true, width: 150 },
        { name: 'SPState', index: 'SPState', width: 150, hidden: true },
        { name: 'Fcontent', index: 'Fcontent', width: 150, hidden: true },
        { name: 'ExJob', index: 'ExJob', width: 50, hidden: true },
        { name: 'deptname', index: 'deptname', width: 80 },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 80 },
        { name: 'state', index: 'state', width: 150 },
        { name: 'nstate', index: 'nstate', width: 80 },
        { name: 'WState', index: 'WState', width: 80 },
        ],
        pager: jQuery('#pager'),
        sortname: 'state',
        sortable: true,
        optionloadonce: true,
        sortorder: 'desc',
        pgbuttons: true,
        rowNum: OnePageCount,
        rownumbers: true,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '违规处理信息',
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var str = "<a href='#' style='color:blue' onclick='ShowDetail(\"" + curRowData.SID + "\")' >" + curRowData.SID + "</a>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                //获得pid超链接但获取不到pid值    
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
            var pid = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var fid = jQuery("#list").jqGrid('getRowData', rowid).FID
            loadPrduct(sid);
            select(rowid);
            loadServer(sid);
            loadPlanPro(sid, fid);
            loadPlanSer(sid);
            laodLog(sid);
            LoadScore(sid);
            loadAward(sid);
            loadPrice(sid);
            // Zhunchu();
            RecoverSup();//恢复供应商
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
            reloadRefresh();
        }
    });
}
//不要审批按钮，点击行直接弹出窗口
//function Zhunchu() {
//    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
//    if (rowid == null) {
//        alert("您还没有选择要处理的审批");
//        return;
//    } else {
//        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
//        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
//        var state2 = jQuery("#list").jqGrid('getRowData', rowid).state;

//        var Job = $("#Exjob").val();
//        if (state2 == "合格供应商" || state2 == "恢复供应商未通过") {
//            //alert("审批不通过，不能进行审批了");
//            return;
//        }
//        if (state2 != "建议停止供货待审批" && state2 != "建议暂停供货待审批" && state2 != "建议淘汰供应商待审批") {
//            //alert("只有董事长、总经理、副总经理才有审批权限");
//            return;
//        }
//        if (state2 == "建议停止供货待审批")
//            $("#webkey").val("准出停止供货评审");
//        else if (state2 == "建议暂停供货待审批")
//            $("#webkey").val("准出暂停供货评审");
//        else if (state2 == "建议淘汰供应商待审批")
//            $("#webkey").val("准出淘汰供应商评审");
//        else if (state2 == "建议恢复供应商待审批")
//            $("#webkey").val("恢复供应商");
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
//                    window.parent.OpenDialog("审批", "../COM_Approval/ApprovalSup?id=" + texts, 600, 400, '');
//                }
//                else {
//                    return;
//                }
//            }
//        });

//    }
//}
function RecoverSup() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    if (rowid == null) {
        alert("您还没有选择要恢复成合格供应商的审批");
        return;
    }
    else {
        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        var state = jQuery("#list").jqGrid('getRowData', rowid).state;
        var nstate = jQuery("#list").jqGrid('getRowData', rowid).nstate;
        var Job = $("#Exjob").val();
        if (state == $("#Nostate").val()) {
            alert("审批不通过，不能进行审批了");
            return;
        }
        if (state != "待公司级恢复审批" && nstate != "待公司级审批") {
            return;
        }
        else if (Job != "董事长" && Job != "总经理" && Job != "副总经理") {
            return;
        }
        else if (state == "待公司级恢复审批" || nstate == "待公司级审批")
            $("#webkey").val("恢复供应商");
        else {
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
                        var texts = $("#webkey").val() + "@" + SID;
                        window.parent.OpenDialog("恢复供应商审批", "../SuppliesManage/Approvalhfg?id=" + texts, 900, 500, '');
                        return;
                    }

                    if (bol == "1" && Job == "总经理") {//副总经理审批
                        var texts = $("#webkey").val() + "@" + SID;
                        window.parent.OpenDialog("恢复供应商审批", "../SuppliesManage/Approvalhfg?id=" + texts, 900, 500, '');
                        return;
                    }

                    if (bol == "2" && Job == "董事长") {//总经理才能登陆
                        var texts = $("#webkey").val() + "@" + SID;
                        window.parent.OpenDialog("恢复供应商审批", "../SuppliesManage/Approvalhfg?id=" + texts, 900, 500, '');
                        return;
                    }

                    //var texts = $("#webkey").val() + "@" + SID;
                    //window.parent.OpenDialog("恢复供应商审批", "../SuppliesManage/Approvalhfg?id=" + texts, 600, 400, '');
                    //window.parent.OpenDialog("恢复供应商审批", "../COM_Approval/ApprovalzhunchuSup?id=" + texts, 600, 400, '');
                }
            }
        });



        // var texts = $("#webkey").val() + "@" + SID;
        // window.parent.OpenDialog("恢复供应商审批", "../SuppliesManage/Approvalhfg?id=" + texts, 600, 400, '');

        //$.ajax({
        //    url: "../COM_Approval/JudgeAppDisable",
        //    type: "post",
        //    data: { data1: $("#webkey").val(), data2: SPID },
        //    dataType: "Json",
        //    success: function (data) {
        //        if (data.success == "true") {
        //            var bol = data.intblo;
        //            if (bol == "-1") {
        //                alert("您没有审批权限，不能进行审批操作");
        //                return;
        //            }
        //            if (bol == "1") {
        //                alert("您已经审批完成，不能进行审批操作");
        //                return;
        //            }
        //            if (bol == "2") {
        //                alert("审批过程还没有进行到您这一步，不能进行审批操作");
        //                return;
        //            }
        //            var texts = $("#webkey").val() + "@" + SPID + "@" + SID;
        //            window.parent.OpenDialog("恢复供应商审批", "../COM_Approval/ApprovalzhunchuSup?id=" + texts, 600, 400, '');
        //        }
        //        else {
        //            return;
        //        }
        //    }
        //});
    }
}
function LoadDetail(sid) {
    SID = sid
    rowCount = document.getElementById("DetailInfo").rows.length;
    CountRows = parseInt(rowCount);
    $.ajax({
        url: "GetDetailInfo",
        type: "post",
        data: { id: SID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var g = 0; g < json.length; g++) {
                    CountRows++;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                    html += '<td style="width:80px;"><lable  >部门负责人</lable> </td>';
                    html += '<td style="width:80px;"><lable  >审批日期</lable> </td>';
                    html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '">' + json[g].ApprovalTime1 + '</lable> </td>';
                    html += '<td style="width:80px;"><lable  >处理意见</lable> </td>';
                    html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="COMNameC' + rowCount + '">' + json[g].Opinions + '</lable> </td>';
                    html += '<td style="width:80px;"><lable >详细内容</lable> </td>';
                    html += '<td style="width:80px;" colspan="2"><lable class="labProductID' + rowCount + ' " id="COMNameC' + rowCount + '">' + json[g].OpinionsD + '</lable> </td>';
                    // html += '<td style="display:none;"><lable class="labSID' + rowCount + ' " id="SID' + rowCount + '">' + json[g].SID + '</lable> </td>';
                    html += '</tr>'
                    html += '<tr  id ="DetailInfo2' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                    html += '<td style="width:80px;"><label  >部门经理</lable> </td>';
                    html += '<td style="width:80px;"><lable >审批日期</lable> </td>';
                    html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '">' + json[g].ApprovalTime2 + '</lable> </td>';
                    html += '<td style="width:80px;"><lable  >处理意见</lable> </td>';
                    html += '<td style="width:80px;" colspan="3"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '">' + json[g].Opinions + '</lable> </td>';
                    // html += '<td style="display:none;"><lable class="labSID' + rowCount + ' " id="SID' + rowCount + '">' + json[g].SID + '</lable> </td>';
                    html += '</tr>'
                    html += '<tr  id ="DetailInfo3' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                    html += '<td style="width:80px;"><lable >副总经理</lable> </td>';
                    html += '<td style="width:80px;"><lable  >审批日期</lable> </td>';
                    html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '">' + json[g].ApprovalTime3 + '</lable> </td>';
                    html += '<td style="width:80px;"><lable >处理意见</lable> </td>';
                    html += '<td style="width:80px;" colspan="3"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '">' + json[g].Opinions + '</lable> </td>';
                    // html += '<td style="display:none;"><lable class="labSID' + rowCount + ' " id="SID' + rowCount + '">' + json[g].SID + '</lable> </td>';
                    html += '</tr>'
                    html += '<tr  id ="DetailInfo4' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                    html += '<td style="width:80px;"><lable  >供应商管理工作组</lable> </td>';
                    html += '<td style="width:80px;"><lable  >审批日期</lable> </td>';
                    html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '">' + json[g].ApprovalTime4 + '</lable> </td>';
                    html += '<td style="width:80px;"><lable  >处理意见</lable> </td>';
                    html += '<td style="width:80px;" colspan="3"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '">' + json[g].Opinions + '</lable> </td>';
                    // html += '<td style="display:none;"><lable class="labSID' + rowCount + ' " id="SID' + rowCount + '">' + json[g].SID + '</lable> </td>';
                    html += '</tr>'
                    html += '<tr  id ="DetailInfo5' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                    html += '<td style="width:80px;"><lable >总经理</lable> </td>';
                    html += '<td style="width:80px;"><lable >审批日期</lable> </td>';
                    html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '">' + json[g].ApprovalTime5 + '</lable> </td>';
                    html += '<td style="width:80px;"><lable  >处理意见</lable> </td>';
                    html += '<td style="width:80px;" colspan="3"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '">' + json[g].Opinions + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labSID' + rowCount + ' " id="SID' + rowCount + '">' + json[g].SID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
            else {
                var html = "";
                html = '<tr  id ="DetailInfo1' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                html += '<td  style="width:80px;"><lable >部门负责人</lable> </td>';
                html += '<td  style="width:80px;"><lable  >审批日期</lable> </td>';
                html += '<td  style="width:80px;"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '" ></lable> </td>';
                html += '<td style="width:80px;"><lable  >处理意见</lable> </td>';
                html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="COMNameC' + rowCount + '" ></lable> </td>';
                html += '<td style="width:80px;"><lable >详细内容</lable> </td>';
                html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="COMNameC' + rowCount + '" ></lable> </td>';
                html += '</tr>'
                html += '<tr  id ="DetailInfo2' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " style="width:80px;">部门经理</lable> </td>';
                html += '<td style="width:80px;"><lable  >审批日期</lable> </td>';
                html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '"></lable> </td>';
                html += '<td style="width:80px;"><lable  >处理意见</lable> </td>';
                html += '<td style="width:80px;" colspan="3"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '"></lable> </td>';
                html += '</tr>'
                html += '<tr  id ="DetailInfo3' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " style="width:80px;">副总经理</lable> </td>';
                html += '<td style="width:80px;"><lable  >审批日期</lable> </td>';
                html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '"></lable> </td>';
                html += '<td style="width:80px;" ><lable >处理意见</lable> </td>';
                html += '<td style="width:80px;" colspan="3"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '"></lable> </td>';
                html += '</tr>'
                html += '<tr  id ="DetailInfo4' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' "  style="width:80px;">供应商管理工作组</lable> </td>';
                html += '<td style="width:80px;"><lable  >审批日期</lable> </td>';
                html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '"></lable> </td>';
                html += '<td style="width:80px;" ><lable  >处理意见</lable> </td>';
                html += '<td style="width:80px;" colspan="3"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '"></lable> </td>';
                html += '</tr>'
                html += '<tr  id ="DetailInfo5' + rowCount + '" onclick="selRow(this)" style="text-align:center;">'
                html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " style="width:80px;" >总经理</lable> </td>';
                html += '<td style="width:80px;"><lable  >审批日期</lable> </td>';
                html += '<td style="width:80px;"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '"></lable> </td>';
                html += '<td style="width:80px;" ><lable >处理意见</lable> </td>';
                html += '<td style="width:80px;" colspan="3"><lable class="labProductID' + rowCount + ' " id="SupplierCode' + rowCount + '"></lable> </td>';
                html += '<td style="display:none;"><lable class="labSID' + rowCount + ' " id="SID' + rowCount + '"></lable> </td>';
                html += '</tr>'
                $("#DetailInfo").append(html);
            }
        }

    })
}
//拟购产品
function loadPrduct(sid) {
    SID = sid;
    curPagePro = 1;
    PName = $("#PName").val();
    Standard = $("#Standard").val();
    $("#list1").jqGrid('setGridParam', {
        url: 'ManageProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPagePro, rownum: OnePageCountPro, pName: PName, standard: Standard, Sid: SID },

    }).trigger("reloadGrid");
}
//拟购服务
function loadServer(sid) {
    SID = sid;
    curPageSer = 1;
    $("#list3").jqGrid('setGridParam', {
        url: 'ManageServerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageSer, rownum: OnePageCountSer, Sid: SID },

    }).trigger("reloadGrid");
}
// 资质
function loadPlanPro(sid, fid) {
    SID = sid;
    FID = fid;
    curPageZiZhi = 1;
    $("#list4").jqGrid('setGridParam', {
        url: 'ManagePlanProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZiZhi, rownum: OnePageCountZiZhi, Sid: SID, Fid: FID },

    }).trigger("reloadGrid");
}
//证书
function loadPlanSer(sid) {
    SID = sid;
    curPageZS = 1;
    $("#list5").jqGrid('setGridParam', {
        url: 'ManagePlanSerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZS, rownum: OnePageCountZS, Sid: SID },

    }).trigger("reloadGrid");
}
//日志
function laodLog(sid) {
    SID = sid;
    curPageLog = 1;
    $("#list6").jqGrid('setGridParam', {
        url: 'LogShowGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageLog, rownum: OnePageCountLog, Sid: SID },

    }).trigger("reloadGrid");
}
function loadAward(sid) {
    SID = sid;
    curPageAward = 1;
    $("#list8").jqGrid('setGridParam', {
        url: 'AwardNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageAward, rownum: OnePageCountAward, Sid: SID },
    }).trigger("reloadGrid");
}
function loadPrice(sid) {
    SID = sid;
    curPagePrice = 1;
    $("#list9").jqGrid('setGridParam', {
        url: 'PriceNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPagePrice, rownum: OnrPageCountPrice, Sid: SID },
    }).trigger("reloadGrid");
}
function selRow(row) {
    newRowID = row.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    RelenvceID = jQuery("#list").jqGrid('getRowData', rowid).SID;
    reload1();
}
function reload1() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var state = jQuery("#list").jqGrid('getRowData', rowid).state;
    var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    var webkey = "";
    var folderBack = "";
    if (state == "建议停止供货待审批" || state == "建议停止供货" || state == "停止供货" || state == "未通过恢复供应商建议") {
        webkey = "准出停止供货评审";
        folderBack = "BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/23/22/tk_PID/10/tk_UserLog";
    }
    else if (state == "建议暂停供货待审批" || state == "建议暂停供货" || state == "暂停供货" || state == "未通过恢复供应商建议") {
        webkey = "准出暂停供货评审";
        folderBack = "BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/26/25/tk_PID/10/tk_UserLog";
    }
    else if (state == "建议淘汰供应商待审批" || state == "淘汰供应商" || state == "建议淘汰供应商" || state == "未通过恢复供应商建议") {
        webkey = "准出淘汰供应商评审";
        folderBack = "BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/29/28/tk_PID/10/tk_UserLog";
    }
    else if (state == "建议恢复供应商待审批" || state == "建议恢复供应商" || state == "恢复供应商未通过" || state == "未通过恢复供应商建议") {
        webkey = "恢复供应商";
        folderBack = "BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/52/tk_PID/54/tk_UserLog";
    }
    else {
        webkey = "";
        folderBack = "";
    }
    //webkey = $('#webkey').val();//恢复审批
    //folderBack = $('#folderBack').val();
    $("#list2").jqGrid('setGridParam', {
        url: '../SuppliesManage/Condition',
        datatype: 'json',
        postData: { curpage: curPageQK, rownum: OnePageCountQK, SID: sid, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}
function jqW() {
    $("#list2").jqGrid('GridUnload');
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var state = jQuery("#list").jqGrid('getRowData', rowid).state;
    var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    var webkey = "";
    var folderBack = "";
    if (state == "建议停止供货待审批" || state == "建议停止供货" || state == "停止供货" || state == "未通过恢复供应商建议") {
        webkey = "准出停止供货评审";
        folderBack = "BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/23/22/tk_PID/10/tk_UserLog";
    }
    else if (state == "建议暂停供货待审批" || state == "建议暂停供货" || state == "暂停供货" || state == "未通过恢复供应商建议") {
        webkey = "准出暂停供货评审";
        folderBack = "BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/26/25/tk_PID/10/tk_UserLog";
    }
    else if (state == "建议淘汰供应商待审批" || state == "淘汰供应商" || state == "建议淘汰供应商" || state == "未通过恢复供应商建议") {
        webkey = "准出淘汰供应商评审";
        folderBack = "BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/29/28/tk_PID/10/tk_UserLog";
    }
    else if (state == "建议恢复供应商待审批" || state == "建议恢复供应商" || state == "恢复供应商未通过" || state == "未通过恢复供应商建议") {
        webkey = "恢复供应商";
        folderBack = "BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/51/tk_PID/56/tk_UserLog";
    }
    else {
        webkey = "";
        folderBack = "";
    }
    //webkey = $('#webkey').val();
    // folderBack = $('#folderBack').val();
    jQuery("#list2").jqGrid({
        url: '../SuppliesManage/Condition',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageQK, rownum: OnePageCountQK, SID: sid, webkey: webkey, folderBack: folderBack },
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
        { name: 'Num', index: 'Num', width: 60, hidden: true },
        { name: 'stateDesc', index: 'stateDesc', width: 100 },
        { name: 'Opinion', index: 'Opinion', width: $("#bor").width() - 770 },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 100 },
        { name: 'Remark', index: 'Remark', width: 200, hidden: true },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCountQK,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '准出审批情况表',
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
            }
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPageQK == $("#list2").getGridParam("lastpage"))
                    return;
                curPageQK = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPageQK = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPageQK == 1)
                    return;
                curPageQK = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPageQK = 1;
            }
            else {
                curPageQK = $("#pager2 :input").val();
            }
            reload1();
            reload();
        },
        loadComplete: function () {
            // $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function ShowDetail(sid) {
    window.parent.OpenDialog("详细内容", "../SuppliesManage/DetailApp?sid=" + sid, 700, 700, '');
}
function jqPro() {
    PName = $("#PName").val();
    Standard = $("#Standard").val();
    jQuery("#list1").jqGrid({
        url: 'ManageProGrid',
        datatype: 'json',
        height: 150,
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
                var curChk = "<input id='d" + id + "' onclick='selChangepro(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "  <a onclick='CheckProduct(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list1").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid1, status) {
            if (oldproID != 0) {
                $('input[id=d' + oldproID + ']').prop("checked", false);
            }
            $('input[id=d' + rowid1 + ']').prop("checked", true);
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
            // $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqSer() {
    jQuery("#list3").jqGrid({
        url: 'ManageServerGrid',
        datatype: 'json',
        height: 150,
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
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCountSer,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '拟购服务',
        gridComplete: function () {
            var ids = jQuery("#list3").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list3").jqGrid('getRowData', id);
                var curChk = "<input id='e" + id + "' onclick='selChangeSer(" + id + ")' type='checkbox' value='" + jQuery("#list3").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "  <a onclick='ServerOut(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list3").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list3").jqGrid('setRowData', ids[i], { Opration: cancel });
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
            if (pgButton == "next_pager3") {
                if (curPageSer == $("#list3").getGridParam("lastpage"))
                    return;
                curPageSer = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPageSer = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPageSer == 1)
                    return;
                curPageSer = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPageSer = 1;
            }
            else {
                curPageSer = $("#pager3 :input").val();
            }
            reloadServer();
        },
        loadComplete: function () {
            //  $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPlanPro() {
    jQuery("#list4").jqGrid({
        url: 'ManagePlanProGrid',
        datatype: 'json',
        height: 150,
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
        pager: jQuery('#pager4'),
        pgbuttons: true,
        rowNum: OnePageCountZiZhi,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '资质管理',
        gridComplete: function () {
            var ids = jQuery("#list4").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list4").jqGrid('getRowData', id);
                var curChk = "<input id='f" + id + "' onclick='selChangePlanPro(" + id + ")' type='checkbox' value='" + jQuery("#list4").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "  <a onclick='DownFile(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list4").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list4").jqGrid('setRowData', ids[i], { Opration: cancel });
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
            if (pgButton == "next_pager4") {
                if (curPageZiZhi == $("#list4").getGridParam("lastpage"))
                    return;
                curPageZiZhi = $("#list4").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager4") {
                curPageZiZhi = $("#list4").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager4") {
                if (curPageZiZhi == 1)
                    return;
                curPageZiZhi = $("#list4").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager4") {
                curPageZiZhi = 1;
            }
            else {
                curPageZiZhi = $("#pager4 :input").val();
            }
            reloadPlanPro();
        },
        loadComplete: function () {
            //  $("#list4").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list4").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPlanSer() {
    jQuery("#list5").jqGrid({
        url: 'ManagePlanSerGrid',
        datatype: 'json',
        height: 150,
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
        pager: jQuery('#pager5'),
        pgbuttons: true,
        rowNum: OnePageCountZS,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '证书管理',
        gridComplete: function () {
            var ids = jQuery("#list5").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list5").jqGrid('getRowData', id);
                var curChk = "<input id='g" + id + "' onclick='selChangePlanSer(" + id + ")' type='checkbox' value='" + jQuery("#list5").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "  <a onclick='DownZhenshu(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list5").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list5").jqGrid('setRowData', ids[i], { Opration: cancel });
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
            if (pgButton == "next_pager5") {
                if (curPageZS == $("#list5").getGridParam("lastpage"))
                    return;
                curPageZS = $("#list5").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager5") {
                curPageZS = $("#list5").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager5") {
                if (curPageZS == 1)
                    return;
                curPageZS = $("#list5").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager5") {
                curPageZS = 1;
            }
            else {
                curPageZS = $("#pager5:input").val();
            }
            reloadPSer();
        },
        loadComplete: function () {
            //$("#list5").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list5").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqLog() {
    jQuery("#list6").jqGrid({
        url: 'LogShowGrid',
        datatype: 'json',
        height: 150,
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
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCountLog,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '日志记录',
        gridComplete: function () {
            var ids = jQuery("#list6").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list6").jqGrid('getRowData', id);
                var curChk = "<input id='h" + id + "' onclick='selChangeLog(" + id + ")' type='checkbox' value='" + jQuery("#list6").jqGrid('getRowData', id).SID + "' name='cb'/>";
                jQuery("#list6").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            if (pgButton == "next_pager6") {
                if (curPageLog == $("#list6").getGridParam("lastpage"))
                    return;
                curPageLog = $("#list6").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager6") {
                curPageLog = $("#list6").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager6") {
                if (curPageLog == 1)
                    return;
                curPageLog = $("#list6").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager6") {
                curPageLog = 1;
            }
            else {
                curPageLog = $("#pager6 :input").val();
            }
            reloadLog();
        },
        loadComplete: function () {
            // $("#list6").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list6").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqScore() {
    //COMShortName = $("#COMShortName").val();
    //SupplierType = $("#SupplierType").val();
    //ReviewDate = $("#ReviewDate").val();
    //SupplierCode = $("#SID").val();
    $("#list7").jqGrid({
        url: 'ScoreGrids',
        datatype: 'json',
        postData: { curpage: curPageScore, rownum: OnePageCountScore, Sid: SID },
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
        pager: jQuery('#pager7'),
        pgbuttons: true,
        rowNum: OnePageCountScore,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '评分情况',
        gridComplete: function () {
            var ids = jQuery("#list7").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list7").jqGrid('getRowData', id);
                var curChk = "<input id='d" + id + "' onclick='selChangeScore(" + id + ")' type='checkbox' value='" + jQuery("#list7").jqGrid('getRowData', id).SID + "' name='cb'/>";
                jQuery("#list7").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid1, status) {
            if (oldScoreID != 0) {
                $('input[id=j' + oldScoreID + ']').prop("checked", false);
            }
            $('input[id=j' + rowid1 + ']').prop("checked", true);
            oldScoreID = rowid1;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager7") {
                if (curPageScore == $("#list7").getGridParam("lastpage"))
                    return;
                curPageScore = $("#list7").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager7") {
                curPageScore = $("#list7").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager7") {
                if (curPageScore == 1)
                    return;
                curPageScore = $("#list7").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager7") {
                curPageScore = 1;
            }
            else {
                curPageScore = $("#pager7 :input").val();
            }
            reloadScore();
        },
        loadComplete: function () {
            $("#list7").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list7").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqAward() {
    jQuery("#list8").jqGrid({
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
        pager: jQuery('#pager8'),
        pgbuttons: true,
        rowNum: OnePageCountAward,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '曾获奖项',
        gridComplete: function () {
            var ids = jQuery("#list8").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list8").jqGrid('getRowData', id);
                var curChk = "<input id='j" + id + "' onclick='selAward(" + id + ")' type='checkbox' value='" + jQuery("#list8").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "<a onclick='DownloadAward(" + id + ")' style='color:blue;cursor:pointer;'>下载</a>";
                jQuery("#list8").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list8").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldAward != 0) {
                $('input[id=j' + oldAward + ']').prop("checked", false);
            }
            $('input[id=j' + rowid + ']').prop("checked", true);
            oldAward = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list8").getGridParam("lastpage"))
                    return;
                curPage = $("#list8").getGridParam("page8") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list8").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list8").getGridParam("page8") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager8 :input").val();
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
            $("#list8").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPrice() {
    jQuery("#list9").jqGrid({
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
        pager: jQuery('#pager9'),
        pgbuttons: true,
        rowNum: OnrPageCountPrice,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '报价/比价单',
        gridComplete: function () {
            var ids = jQuery("#list9").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list9").jqGrid('getRowData', id);
                var curChk = "<input id='f" + id + "' onclick='selPrice(" + id + ")' type='checkbox' value='" + jQuery("#list9").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "<a onclick='DownloadPrice(" + id + ")' style='color:blue;cursor:pointer;'>下载</a>";
                jQuery("#list9").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list9").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldPrice != 0) {
                $('input[id=k' + oldPrice + ']').prop("checked", false);
            }
            $('input[id=k' + rowid + ']').prop("checked", true);
            oldPrice = rowid;
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
            reloadPrice();
        },
        loadComplete: function () {

            $("#list9").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function LoadScore(sid) {
    SID = sid;
    curPageScore = 1;
    //COMShortName = $("#COMShortName").val();
    //SupplierType = $("#SupplierType").val();
    //ReviewDate = $("#ReviewDate").val();
    //SupplierCode = $("#SID").val();
    $("#list7").jqGrid('setGridParam', {
        url: 'ScoreGrids',
        datatype: 'json',
        postData: { curpage: curPageScore, rownum: OnePageCountScore, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadScore() {
    //COMShortName = $("#COMShortName").val();
    //SupplierType = $("#SupplierType").val();
    //ReviewDate = $("#ReviewDate").val();
    //SupplierCode = $("#SID").val();
    $("#list7").jqGrid('setGridParam', {
        url: 'ScoreGrids',
        datatype: 'json',
        postData: { curpage: curPageScore, rownum: OnePageCountScore, Sid: SID },

    }).trigger("reloadGrid");
}
function selChangeScore(rowid) {
    if ($('input[id=j' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldScoreID != 0) {
            $('input[id=j' + oldScoreID + ']').prop("checked", false);
        }
        $('input[id=j' + rowid + ']').prop("checked", true);
        $("#list7").setSelection(rowid)
    }
}
function DownloadAward(id) {
    var model = jQuery("#list8").jqGrid('getRowData', id);
    var Proid = model.ID;//唯一编号

    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    window.open("DownLoadAward?sid=" + Proid);

}
function DownloadPrice(id) {
    var model = jQuery("#list9").jqGrid('getRowData', id);
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
    var model = jQuery("#list3").jqGrid('getRowData', id);
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
    var model = jQuery("#list4").jqGrid('getRowData', id);
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
    var model = jQuery("#list5").jqGrid('getRowData', id);
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