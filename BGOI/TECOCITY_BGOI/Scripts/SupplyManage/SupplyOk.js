var SName, Type, Area, ProName, Standard, State, SID, FID, PID;
var curPage = 1, curPageQK = 1, curPageLog = 1, curPageRecord = 1, curPagePro = 1, curPageSer = 1, curPageZiZhi = 1, curPageZS = 1, curPageBR = 1;
var OnePageCount = 20;
var OnePageCountLog = 15, OnePageCountQK = 15, OnePageCountRecord = 15, OnePageCountBR = 15;
var OnePageCountPro = 10, OnePageCountSer = 10, OnePageCountZiZhi = 10, OnePageCountZS = 10;
var oldSelID = 0, oldLog = 0, oldproID = 0, oldSerID = 0, oldplanPro = 0, oldplanSer = 0, oldBR = 0, oldDealRecord = 0;
var curPageAward = 1;
var curPagePrice = 1;
var OnePageCountAward = 15;
var OnrPageCountPrice = 15;
$(document).ready(function () {
    var date = new Date;
    var moth = parseInt(date.getMonth());//只有在10-12月才显示,0表示1月
    var job = $("#ExJob").val();

    if (job == "经理" || job == "副经理") {
        $("#FZR").show();//准出处理建议
        //$("#WGSP").hide();//提交准出审批
        $("#Weigui").show();//违规申请
        $("#Niandu").show();//年度评价
        $("#Huifu").show();
        $("#recover").show();
        //$("#SubYApproval").hide();//提交年度评价
    }
    else if (job == "") {
        $("#FZR").hide();//准出处理建议
        $("#Huifu").hide();
    }
    else {
        $("#FZR").hide();//准出处理建议
        //$("#WGSP").hide();//提交准出审批
        $("#Weigui").hide();//违规申请
        $("#Niandu").hide();//年度评价
        $("#Huifu").hide();
        $("#recover").hide();

    }
    //if (job != "董事长" && job != "总经理" && job != "副总经理") {
    //    $("#Weigui").show();//违规申请
    //    $("#Niandu").show();//年度评价
    //    $("#recover").show();
    //}
    //else if (job != "董事长" && job != "总经理" && job != "副总经理") {
    //    $("#WGSP").hide();//提交准出审批
    //    $("#Niandu").hide();
    //    $("#Weigui").hide();
    //    $("#recover").hide();
    //}
    //else {
    //    $("#Weigui").show();
    //    $("#FZR").show();//准出处理建议
    //    $("#WGSP").show();//提交准出审批
    //    // $("#Niandu").show();//年度评价
    //    $("#Huifu").show();
    //    $("#recover").show();
    //    $("#Weigui").show();
    //    $("#SubYApproval").show();//提交年度评价
    //}
    //暂时关闭
    if (moth == 9 || moth == 10 || moth == 11) {
        $("#Niandu").show();//年度评价
    }
    else {
        $("#Niandu").hide();//年度评价
    }

    jq();
    $("#two").hide();
    $("#three").hide();
    $("#four").hide();
    $("#nine").hide();
    $("#ten").hide();
    $("#six").hide();
    $("#sven").hide();
    $("#eight").hide();
    jqLog();
    jqPro();
    jqSer();
    jqPlanPro();
    jqPlanSer();
    jqW();
    jqAward();
    jqPrice();
    // jqRecored();
    //jqBR();
    //$("#one").hide();

    $("#Niandu").click(function () {

        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要年度评价的数据");
            return;
        }
        else {
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
            var nstate = jQuery("#list").jqGrid('getRowData', rowid).nstate
            var time = $("#time").val();
            if (nstate == "公司级审批未通过" || nstate == "待部门级审批" || nstate == "待公司级审批") {
                alert("该供应商不能进行年度评价操作");
                return;
            }
            else {
                var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
                window.parent.OpenDialog("年度评价", "../SuppliesManage/YearApproval?sid=" + sid, 1080, 350, '');
            }

        }
    });
    //$("#SubYApproval").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("您还没有选择要处理的年度评审");
    //        return;
    //    }
    //    else {
    //        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
    //        if (state != "年度评价通过") {
    //            alert("该项目还不能提交年度评审");
    //            return;
    //        }
    //    }
    //    var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "年度评审";
    //    window.parent.OpenDialog("提交年度评审", "../COM_Approval/SubmitApproval?id=" + sid, 700, 580, '');
    //})
    $("#Weigui").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("你还没有选择要申请处理申请的供应商");
            return;
        } else {
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
            // var user = jQuery("#list").jqGrid('getRowData', rowid).DeclareUser;
            if (state != "合格供应商" && state != "部门级审批未通过") {
                alert("该供应商已经提交申请处理");
                return;
            }
        }
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        window.parent.OpenDialog("准出处理申请", "../SuppliesManage/ApproverSP?sid=" + sid, 900, 330, '');
    })
    //公司级审批变成弹窗
    //$("#WGSP").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    var state = jQuery("#list").jqGrid('getRowData', rowid).State;
    //    var job = $("#ExJob").val();

    //    if (rowid == null) {
    //        alert("您还没有选择要提交准出审批的供应商");
    //        return;
    //    }
    //    else {
    //        //if (state == "建议停止供货待审批" || state == "建议暂停供货待审批" || state == "建议淘汰供应商待审批")
    //        //{ alert("该供应商已经过提交准出审批"); return; }
    //        if (state != "建议停止供货" && state != "建议暂停供货" && state != "建议淘汰供应商") {
    //            alert("该供应商不能提交准出审批");
    //            return;
    //        }
    //    }
    //    //根据处理意见状态判断
    //    if (state == "建议停止供货") {
    //        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "准出停止供货评审";

    //        window.parent.OpenDialog("公司级审批", "../SuppliesManage/SubmitApproval?id=" + sid, 1000, 400, '');
    //    } else if (state == "建议暂停供货") {
    //        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "准出暂停供货评审";

    //        window.parent.OpenDialog("公司级审批", "../SuppliesManage/SubmitApproval?id=" + sid, 1000, 400, '');
    //    }
    //    else if (state == "建议淘汰供应商") {
    //        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "准出淘汰供应商评审";
    //        window.parent.OpenDialog("公司级审批", "../SuppliesManage/SubmitApproval?id=" + sid, 1000, 400, '');
    //    }
    //    else {
    //        return;
    //    }

    //})

    $("#Product").click(function () {
        this.className = "btnTw";
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#sp').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        //$('#BRcord').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#one").css("display", "");
        $("#two").css("display", "none");
        $("#six").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#nine").css("display", "none");
        $("#ten").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
    });
    $("#Server").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#sp').attr("class", "btnTh");
        //$('#BRcord').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#two").css("display", "");
        $("#one").css("display", "none");
        $("#six").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#ten").css("display", "none");
        $("#nine").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
    });
    $("#planPro").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#btnRecord').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#sp').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#three").css("display", "");
        $("#six").css("display", "none");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#four").css("display", "none");
        $("#ten").css("display", "none");
        $("#nine").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
    });
    $("#planServer").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#btnRecord').attr("class", "btnTh");
        $('#sp').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#four").css("display", "");
        $("#one").css("display", "none");
        $("#six").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#ten").css("display", "none");
        $("#nine").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
    });
    $("#btnLog").click(function () {
        this.className = "btnTw";
        $('#sp').attr("class", "btnTh");
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#btnRecord').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#nine").css("display", "");
        $("#four").css("display", "none");
        $("#one").css("display", "none");
        $("#six").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#ten").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
    })
    $("#btnRecord").click(function () {
        this.className = "btnTw";
        $('#btnLog').attr("class", "btnTh");
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#sp').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#ten").css("display", "");
        $("#four").css("display", "none");
        $("#one").css("display", "none");
        $("#six").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#nine").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
    })
    $("#btnAward").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#sp').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");


        $("#sven").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#nine").css("display", "none");
        $("#eight").css("display", "none");
        $("#ten").css("display", "none");
        $("#six").css("display", "none");

    })
    $("#btnPrice").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#DealRecord').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#sp').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");


        $("#eight").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#nine").css("display", "none");
        $("#sven").css("display", "none");
        $("#six").css("display", "none");

    })
    //打印
    $("#PrintOK").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        if (rowid == null) {
            alert("请选择列表一条数据");
            return;
        }
        else {
            window.showModalDialog("../SuppliesManage/PrintOK?sid=" + sid, window, "dialogWidth:800px;dialogHeight:600px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    })
    $("#FZR").click(function () {  //部门级审批
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行部门级审批的供应商");
            return;
        } else {
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
            var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
            var job = $("#ExJob").val();

            //负责人评审分暂停供货和建议淘汰意见两种

        }
        if (state != "建议停止供货" && state != "建议暂停供货" && state != "建议淘汰供应商") {
            alert("该供应商不能进行部门级审批");
            return;
        }
        else {
            //if (state == "建议停止供货") {
            //    var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "准出停止供货评审";
            //    window.parent.OpenDialog("部门级审批", "../SuppliesManage/Submitzhunchu?id=" + sid, 1000, 400, '');
            //}
            //else if (state == "建议暂停供货") {
            //    var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "准出暂停供货评审";

            //    window.parent.OpenDialog("部门级审批", "../SuppliesManage/Submitzhunchu?id=" + sid, 1000, 400, '');
            //}
            //else if (state == "建议淘汰供应商") {
            //    var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "准出淘汰供应商评审";
            //    window.parent.OpenDialog("部门级审批", "../SuppliesManage/Submitzhunchu?id=" + sid, 1000, 400, '');
            //}
            var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
            window.parent.OpenDialog("部门级审批", "../SuppliesManage/FZRSP?sid=" + sid, 900, 400, '');
        }
    })
    //恢复供货部门审批
    $("#Huifu").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行恢复供货的供应商");
            return;
        } else {
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
            var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
            var job = $("#ExJob").val();
        }
        if (state != "待部门级恢复供货审批") {
            alert("该供应商不能进行部门级恢复审批");
            return;
        }
        else {
            //var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "恢复供货";
            var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
            window.parent.OpenDialog("部门级恢复供货审批", "../SuppliesManage/Submithuifu?id=" + sid, 1000, 400, '');

        }
    })

    $("#recover").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要处理的年度评审");
            return;
        }
        else {
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (state != "暂停供货" && state != "停止供货" && state != "部门级恢复供货未通过" && state != "公司级恢复供货未通过") {
                alert("该供应商还不能进行恢复供货操作");
                return;
            }
        }
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        window.parent.OpenDialog("恢复供货", "../SuppliesManage/RecoverSupply?id=" + sid, 600, 250, '');
    })
    $("#sp").click(function () {
        this.className = "btnTw";
        $('#btnLog').attr("class", "btnTh");
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");

        $("#six").css("display", "");
        $("#four").css("display", "none");
        $("#one").css("display", "none");
        $("#ten").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#nine").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
    })
    //新增产品
    $("#AddPro").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
        var job = $("#Department").val();
        if (job != deptname) {
            alert("只有本部门的人员才能进行新增产品"); return;
        }
        if (rowid == null) {
            alert("您还没有选择要添加的产品");
            return;
        }
        window.parent.OpenDialog("新增产品", "../SuppliesManage/AddPro?sid=" + sid, 400, 450, '');
    })
    //新增服务
    $("#AddServer").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
        var job = $("#Department").val();
        if (job != deptname) {
            alert("只有本部门的人员才能新增服务"); return;
        }
        if (rowid == null) {
            alert("您还没有选择要添加的服务行");
            return;
        }
        window.parent.OpenDialog("新增服务", "../SuppliesManage/AddServer?sid=" + sid, 500, 300, '');
    })
    //新增资质
    $("#AddZiZhi").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
        var job = $("#Department").val();
        if (job != deptname) {
            alert("只有本部门的人员才能新增资质"); return;
        }
        if (rowid == null) {
            alert("您还没有选择要添加的资质行");
            return;
        }
        window.parent.OpenDialog("新增资质", "../SuppliesManage/AddFileInfo?sid=" + sid, 500, 300, '');

    })
    //新增证书
    $("#AddZhenShu").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
        var job = $("#Department").val();
        if (job != deptname) {
            alert("只有本部门的人员才能新增证书"); return;
        }
        if (rowid == null) {
            alert("您还没有选择要添加的证书行");
            return;
        }
        window.parent.OpenDialog("新增证书", "../SuppliesManage/AddCertificate?sid=" + sid, 500, 350, '');
    })
    //新增奖项
    $("#AddAward").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
        var job = $("#Department").val();
        if (job != deptname) {
            alert("只有本部门的人员才能上传奖项信息"); return;
        }
        if (rowid == null) {
            alert("您还没有选择要上传的奖项信息");
            return;
        }
        window.parent.OpenDialog("上传曾获奖项", "../SuppliesManage/AddAwardInfo?sid=" + sid, 550, 150, '');
    })
    //新增比价单
    $("#AddPrice").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
        var job = $("#Department").val();
        if (job != deptname) {
            alert("只有本部门的人员才能上传报价/比价单"); return;
        }
        if (rowid == null) {
            alert("您还没有选择要添加的报价/比价单信息");
            return;
        }
        window.parent.OpenDialog("报价/比价单", "../SuppliesManage/AddPriceInfo?sid=" + sid, 550, 150, '');
    })
    $("#UPBas").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var username = jQuery("#list").jqGrid('getRowData', rowid).CreateUser;
        var name = $("#UserName").val();//添加供应商联系人

        if (rowid == null) {
            alert("您还没有选择要修改的基本信息");
            return;
        }
        else if (name != username) {
            alert("只有本人才能修改"); return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        window.parent.OpenDialog("基本信息修改", "../SuppliesManage/UpdateBas?sid=" + texts, 1120, 1100, '');
    })
})
function reload() {
    SName = $("#COMNameC").val();
    Type = $("#Type").val();
    Area = $("#Area").val();
    State = $("#State").val();
    ProName = $("#ProductName").val();
    $("#list").jqGrid('setGridParam', {
        url: 'ManageokGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sName: SName, type: Type, area: Area, state: State, pName: ProName, order: $("#Order").val() },

    }).trigger("reloadGrid");
}
function reload1() {
    SName = $("#COMNameC").val();
    Type = $("#Type").val();
    Area = $("#Area").val();
    State = $("#State").val();
    ProName = $("#ProductName").val();
    $("#list").jqGrid('setGridParam', {
        url: 'ManageokGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sName: SName, type: Type, area: Area, state: State, pName: ProName, order: $("#Order").val() },

    }).trigger("reloadGrid");
}
function reloadRefrsh() {
    curPage = 1;
    SName = $("#COMNameC").val();
    Type = $("#Type").val();
    Area = $("#Area").val();
    State = $("#State").val();
    ProName = $("#ProductName").val();
    $("#list").jqGrid('setGridParam', {
        url: 'ManageokGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sName: SName, type: Type, area: Area, state: State, pName: ProName, order: $("#Order").val() },

    }).trigger("reloadGrid");
}
function reloadRecord() {
    $("#list10").jqGrid('setGridParam', {
        url: 'DealRecordShow',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageRecord, rownum: OnePageCountRecord, Sid: SID },

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
    $("#list2").jqGrid('setGridParam', {
        url: 'ManageServerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageSer, rownum: OnePageCountSer, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadPlanPro() {
    $("#list3").jqGrid('setGridParam', {
        url: 'ManagePlanProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZiZhi, rownum: OnePageCountZiZhi, Sid: SID, Fid: FID },
    }).trigger("reloadGrid");
}
function reloadWard() {
    $("#list7").jqGrid('setGridParam', {
        url: 'AwardNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageAward, rownum: OnePageCountAward, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadPrice() {
    $("#list8").jqGrid('setGridParam', {
        url: 'PriceNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPagePrice, rownum: OnrPageCountPrice, Sid: SID },
    }).trigger("reloadGrid");
}
//证书信息
function reloadPSer() {
    $("#list4").jqGrid('setGridParam', {
        url: 'ManagePlanSerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZS, rownum: OnePageCountZS, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadLog() {
    $("#list9").jqGrid('setGridParam', {
        url: 'LogShowGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageLog, rownum: OnePageCountLog, Sid: SID },

    }).trigger("reloadGrid");
}
function reloadBackRcord() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    // var Content = jQuery("#list").jqGrid('getRowData', rowid).SContent;
    $("#list5").jqGrid('setGridParam', {
        url: 'LoadBR',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageBR, rownum: OnePageCountBR, Sid: SID },

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
function jq() {
    SName = $("#COMNameC").val();
    Type = $("#Type").val();
    Area = $("#Area").val();
    State = $("#State").val();
    ProName = $("#ProductName").val();
    jQuery("#list").jqGrid({
        url: 'ManageokGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sName: SName, type: Type, area: Area, state: State, pName: ProName, order: $("#Order").val() },
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
        colNames: ['', '', '流水号', 'PID', '原供应商名称', '现合格供应商名称', '合格供应商类别', '产品名称', '服务名称', '公司办公地址', '工厂/出货地址', '企业类型', '供需关系', '所属地区', '评审状态', '当前状态', '年审状态', '产品名称', '处理意见', '申请人', '恢复内容', '职位', '所属部门', '成为合格供应商时间', '姓名'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 30, hidden: true },
        { name: 'SIDs', index: 'SIDs', width: 60, hidden: true },
        { name: 'SID', index: 'SID', width: 60 },
        { name: 'PID', index: 'PID', width: 60, hidden: true },
        { name: 'name', index: 'name', width: 120 },
        { name: 'COMNameC', index: 'COMNameC', width: 120 },
        { name: 'SupplierType', index: 'SupplierType', width: 100 },
        { name: 'ProductName', index: 'ProductName', width: 200 },
        { name: 'ServiceName', index: 'ServiceName', width: 200 },
        { name: 'ComAddress', index: 'ComAddress', width: 120, hidden: true },
        { name: 'COMFactoryAddress', index: 'COMFactoryAddress', width: 120, hidden: true },
        { name: 'EnterpriseType', index: 'EnterpriseType', width: 120, hidden: true },
        { name: 'Relation', index: 'Relation', width: 100, hidden: true },
        { name: 'COMArea', index: 'COMArea', width: 120, hidden: true },
        { name: 'State', index: 'State', width: 120 },
        { name: 'WState', index: 'WState', width: 80 },
        { name: 'nstate', index: 'nstate', width: 80 },
        { name: 'ProductName', index: 'ProductName', width: 80, hidden: true },
        { name: 'Opinions', index: 'Opinions', width: 150, hidden: true },
        { name: 'DeclareUser', index: 'DeclareUser', width: 50, hidden: true },
        { name: 'SContent', index: 'SContent', width: 50, hidden: true },
        { name: 'ExJob', index: 'ExJob', width: 50, hidden: true },
        { name: 'DeptName', index: 'DeptName', width: 80 },
        { name: 'AppTime', index: 'AppTime', width: 100 },
        { name: 'CreateUser', index: 'CreateUser', width: 80, hidden: true },

        ],
        pager: jQuery('#pager'),
        sortname: 'State',
        sortable: true,
        optionloadonce: true,
        sortorder: 'desc',
        pgbuttons: true,
        rowNum: OnePageCount,
        rownumbers: true,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '基础信息',

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
            var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs
            var fid = jQuery("#list").jqGrid('getRowData', rowid).FID
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
            laodLog(sid);
            laodRecord(sid);
            loadPrduct(sid);
            loadServer(sid);
            loadPlanPro(sid, fid);
            loadPlanSer(sid);
            loadBR(sid);
            loadAward(sid);
            loadPrice(sid);
            select(rowid);
            zhunchu();//准出申请审批窗口
            huifu();//恢复供货审批窗口
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
function laodLog(sid) {
    SID = sid;
    curPageLog = 1;
    $("#list9").jqGrid('setGridParam', {
        url: 'LogShowGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageLog, rownum: OnePageCountLog, Sid: SID },

    }).trigger("reloadGrid");
}
function loadBR(sid) {
    SID = sid;
    curPageBR = 1;
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //var Content = jQuery("#list").jqGrid('getRowData', rowid).SContent;
    $("#list5").jqGrid('setGridParam', {
        url: 'LoadBR',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageBR, rownum: OnePageCountBR, Sid: SID },
    }).trigger("reloadGrid");
}
function laodRecord(sid) {
    SID = sid;
    curPageRecord = 1;
    $("#list10").jqGrid('setGridParam', {
        url: 'DealRecordShow',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageRecord, rownum: OnePageCountRecord, Sid: SID },

    }).trigger("reloadGrid");
}
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
function loadServer(sid) {
    SID = sid;
    curPageSer = 1;
    $("#list2").jqGrid('setGridParam', {
        url: 'ManageServerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageSer, rownum: OnePageCountSer, Sid: SID },

    }).trigger("reloadGrid");
}
function loadPlanPro(sid, fid) {
    SID = sid;
    FID = fid;
    curPageZiZhi = 1;
    $("#list3").jqGrid('setGridParam', {
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
    $("#list4").jqGrid('setGridParam', {
        url: 'ManagePlanSerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZS, rownum: OnePageCountZS, Sid: SID },

    }).trigger("reloadGrid");
}
function loadAward(sid) {
    SID = sid;
    curPageAward = 1;
    $("#list7").jqGrid('setGridParam', {
        url: 'AwardNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageAward, rownum: OnePageCountAward, Sid: SID },
    }).trigger("reloadGrid");
}
function loadPrice(sid) {
    SID = sid;
    curPagePrice = 1;
    $("#list8").jqGrid('setGridParam', {
        url: 'PriceNewGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPagePrice, rownum: OnrPageCountPrice, Sid: SID },
    }).trigger("reloadGrid");
}
function selChangeLog(rowid9) {
    if ($('input[id=h' + rowid9 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldLog != 0) {
            $('input[id=m' + oldLog + ']').prop("checked", false);
        }
        $('input[id=m' + rowid9 + ']').prop("checked", true);
        $("#list9").setSelection(rowid9)
    }
}
function selChangeRecord(rowid10) {
    if ($('input[id=j' + rowid10 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldDealRecord != 0) {
            $('input[id=j' + oldDealRecord + ']').prop("checked", false);
        }
        $('input[id=j' + rowid10 + ']').prop("checked", true);
        $("#list10").setSelection(rowid10)
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
        $("#list2").setSelection(rowid2)
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
        $("#list3").setSelection(rowid3)
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
        $("#list4").setSelection(rowid4)
    }
}
function selChangeBR(rowid5) {
    if ($('input[id=x' + rowid5 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldBR != 0) {
            $('input[id=x' + oldBR + ']').prop("checked", false);
        }
        $('input[id=x' + rowid5 + ']').prop("checked", true);
        $("#list5").setSelection(rowid5)
    }
}
function jqLog() {
    jQuery("#list9").jqGrid({
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
        pager: jQuery('#pager9'),
        pgbuttons: true,
        rowNum: OnePageCountLog,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '日志记录',
        gridComplete: function () {
            var ids = jQuery("#list9").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list9").jqGrid('getRowData', id);
                var curChk = "<input id='m" + id + "' onclick='selChangeLog(" + id + ")' type='checkbox' value='" + jQuery("#list9").jqGrid('getRowData', id).SID + "' name='cb'/>";
                jQuery("#list9").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid9, status) {
            if (oldLog != 0) {
                $('input[id=m' + oldLog + ']').prop("checked", false);
            }
            $('input[id=m' + rowid9 + ']').prop("checked", true);
            oldLog = rowid9;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager9") {
                if (curPageLog == $("#list9").getGridParam("lastpage"))
                    return;
                curPageLog = $("#list9").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager9") {
                curPageLog = $("#list9").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager9") {
                if (curPageLog == 1)
                    return;
                curPageLog = $("#list9").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager9") {
                curPageLog = 1;
            }
            else {
                curPageLog = $("#pager9 :input").val();
            }
            reloadLog();
        },
        loadComplete: function () {
            // $("#list9").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list9").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function jqRecored() {
    jQuery("#list10").jqGrid({
        url: 'DealRecordShow',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageRecord, rownum: OnePageCountRecord, Sid: SID },
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
        colNames: ['', '被处理供应商编号', '申请处理原因', '部门采购/供应商管理员签字', '申请部门'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 180 },
        { name: 'reason', index: 'reason', width: 130 },
        { name: 'declareuser', index: 'declareuser', width: 150 },
        { name: 'DeptName', index: 'DeptName', width: 150 },
        ],
        pager: jQuery('#pager10'),
        pgbuttons: true,
        rowNum: OnePageCountRecord,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '处理记录',
        gridComplete: function () {
            var ids = jQuery("#list10").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list10").jqGrid('getRowData', id);
                var curChk = "<input id='j" + id + "' onclick='selChangeRecord(" + id + ")' type='checkbox' value='" + jQuery("#list10").jqGrid('getRowData', id).SID + "' name='cb'/>";
                jQuery("#list10").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid10, status) {
            if (oldDealRecord != 0) {
                $('input[id=j' + oldDealRecord + ']').prop("checked", false);
            }
            $('input[id=j' + rowid10 + ']').prop("checked", true);
            oldDealRecord = rowid10;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager10") {
                if (curPageRecord == $("#list10").getGridParam("lastpage"))
                    return;
                curPageRecord = $("#list10").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager10") {
                curPageRecord = $("#list10").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager10") {
                if (curPageRecord == 1)
                    return;
                curPageRecord = $("#list10").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager10") {
                curPageRecord = 1;
            }
            else {
                curPageRecord = $("#pager10 :input").val();
            }
            reloadRecord();
        },
        loadComplete: function () {
            //  $("#list10").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list10").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
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
                var cancel = "    <a onclick='CheckProduct(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                //"<a onclick='deleteTr(" + id + ")' style='color:blue;cursor:pointer;'>删除</a>" + "  <a onclick='UpdatePro(" + id + ")' style='color:blue;cursor:pointer;'>修改</a>";
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
            //  $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqSer() {
    jQuery("#list2").jqGrid({
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
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCountSer,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '拟购服务',
        gridComplete: function () {
            var ids = jQuery("#list2").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list2").jqGrid('getRowData', id);
                var curChk = "<input id='e" + id + "' onclick='selChangeSer(" + id + ")' type='checkbox' value='" + jQuery("#list2").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "    <a onclick='ServerOut(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                //"<a onclick='deleteSer(" + id + ")' style='color:blue;cursor:pointer;'>删除</a>" + "  <a onclick='UpdateSer(" + id + ")' style='color:blue;cursor:pointer;'>修改</a>";
                jQuery("#list2").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list2").jqGrid('setRowData', ids[i], { Opration: cancel });
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
            if (pgButton == "next_pager2") {
                if (curPageSer == $("#list2").getGridParam("lastpage"))
                    return;
                curPageSer = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPageSer = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPageSer == 1)
                    return;
                curPageSer = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPageSer = 1;
            }
            else {
                curPageSer = $("#pager2 :input").val();
            }
            reloadServer();
        },
        loadComplete: function () {
            // $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 30, false);
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
function jqPlanPro() {
    jQuery("#list3").jqGrid({
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
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCountZiZhi,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '资质管理',
        gridComplete: function () {
            var ids = jQuery("#list3").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list3").jqGrid('getRowData', id);
                var curChk = "<input id='f" + id + "' onclick='selChangePlanPro(" + id + ")' type='checkbox' value='" + jQuery("#list3").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "  <a onclick='DownFile(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list3").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list3").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid3, status) {
            if (oldplanPro != 0) {
                $('input[id=e' + oldplanPro + ']').prop("checked", false);
            }
            $('input[id=e' + rowid3 + ']').prop("checked", true);
            oldplanPro = rowid3;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (curPageZiZhi == $("#list3").getGridParam("lastpage"))
                    return;
                curPageZiZhi = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPageZiZhi = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPageZiZhi == 1)
                    return;
                curPageZiZhi = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPageZiZhi = 1;
            }
            else {
                curPageZiZhi = $("#pager3 :input").val();
            }
            reloadPlanPro();
        },
        loadComplete: function () {
            //  $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPlanSer() {
    jQuery("#list4").jqGrid({
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
        { name: 'IsPlan', index: 'IsPlan', width: 70 },
        { name: 'CType', index: 'CType', width: 100 },
        { name: 'CName', index: 'CName', width: 150 },
        { name: 'CCode', index: 'CCode', width: 150 },
        { name: 'COrganization', index: 'COrganization', width: 150 },
        { name: 'CDate', index: 'CDate', width: 120 },
        { name: 'CFileName', index: 'CFileName', width: 150 },
        { name: 'FileType', index: 'FileType', width: 80, hidden: true },
        { name: 'CreateTime', index: 'CreateTime', width: 80 },
        { name: 'Opration', index: 'Opration', width: 100 },
         { name: 'FID', index: 'FID', width: 100, hidden: true },
        ],
        pager: jQuery('#pager4'),
        pgbuttons: true,
        rowNum: OnePageCountZS,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '证书管理',
        gridComplete: function () {
            var ids = jQuery("#list4").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list4").jqGrid('getRowData', id);
                var curChk = "<input id='g" + id + "' onclick='selChangePlanSer(" + id + ")' type='checkbox' value='" + jQuery("#list4").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "  <a onclick='DownZhenshu(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list4").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list4").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid4, status) {
            if (oldplanSer != 0) {
                $('input[id=e' + oldplanSer + ']').prop("checked", false);
            }
            $('input[id=e' + rowid4 + ']').prop("checked", true);
            oldplanSer = rowid4;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager4") {
                if (curPageZS == $("#list4").getGridParam("lastpage"))
                    return;
                curPageZS = $("#list4").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager4") {
                curPageZS = $("#list4").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager4") {
                if (curPageZS == 1)
                    return;
                curPageZS = $("#list4").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager4") {
                curPageZS = 1;
            }
            else {
                curPageZS = $("#pager4 :input").val();
            }
            reloadPSer();
        },
        loadComplete: function () {
            // $("#list4").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list4").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqBR() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //var Content = jQuery("#list").jqGrid('getRowData', rowid).SContent;
    jQuery("#list5").jqGrid({
        url: 'LoadBR',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageBR, rownum: OnePageCountBR, Sid: SID },
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
        colNames: ['', '恢复内容', '恢复原因', '恢复申请人', '申请内容', '恢复人员', '申请恢复时间'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'FContent', index: 'FContent', width: 100 },
        { name: 'FReason', index: 'FReason', width: 180 },
        { name: 'FName', index: 'FName', width: 130 },
        { name: 'SContent', index: 'SContent', width: 150 },
        { name: 'Sperson', index: 'Sperson', width: 150, hidden: true },
        { name: 'SCreate', index: 'SCreate', width: 150 },
        ],
        pager: jQuery('#pager5'),
        pgbuttons: true,
        rowNum: OnePageCountBR,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '恢复记录',
        gridComplete: function () {
            var ids = jQuery("#list5").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list5").jqGrid('getRowData', id);
                var curChk = "<input id='x" + id + "' onclick='selChangeBR(" + id + ")' type='checkbox' value='" + jQuery("#list5").jqGrid('getRowData', id).SID + "' name='cb'/>";
                jQuery("#list5").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid5, status) {
            if (oldBR != 0) {
                $('input[id=x' + oldBR + ']').prop("checked", false);
            }
            $('input[id=x' + rowid5 + ']').prop("checked", true);
            oldBR = rowid5;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager5") {
                if (curPageBR == $("#list5").getGridParam("lastpage"))
                    return;
                curPageBR = $("#list5").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager5") {
                curPageBR = $("#list5").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager5") {
                if (curPageBR == 1)
                    return;
                curPageBR = $("#list5").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager5") {
                curPageBR = 1;
            }
            else {
                curPageBR = $("#pager5 :input").val();
            }
            reloadBackRcord();
        },
        loadComplete: function () {
            //$("#list5").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list5").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqAward() {
    jQuery("#list7").jqGrid({
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
        pager: jQuery('#pager7'),
        pgbuttons: true,
        rowNum: OnePageCountAward,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '曾获奖项',
        gridComplete: function () {
            var ids = jQuery("#list7").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list7").jqGrid('getRowData', id);
                var curChk = "<input id='j" + id + "' onclick='selAward(" + id + ")' type='checkbox' value='" + jQuery("#list7").jqGrid('getRowData', id).CID + "' name='cb'/>";
                var cancel = "<a onclick='DownloadAward(" + id + ")' style='color:blue;cursor:pointer;'>下载</a>";
                jQuery("#list7").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list7").jqGrid('setRowData', ids[i], { Opration: cancel });
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
                if (curPage == $("#list7").getGridParam("lastpage"))
                    return;
                curPage = $("#list7").getGridParam("page7") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list7").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list7").getGridParam("page7") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager7 :input").val();
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
            $("#list7").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPrice() {
    jQuery("#list8").jqGrid({
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
        pager: jQuery('#pager8'),
        pgbuttons: true,
        rowNum: OnrPageCountPrice,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '报价/比价单',
        gridComplete: function () {
            var ids = jQuery("#list8").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list8").jqGrid('getRowData', id);
                var curChk = "<input id='f" + id + "' onclick='selPrice(" + id + ")' type='checkbox' value='" + jQuery("#list8").jqGrid('getRowData', id).CID + "' name='cb'/>";
                var cancel = "<a onclick='DownloadPrice(" + id + ")' style='color:blue;cursor:pointer;'>下载</a>";
                jQuery("#list8").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list8").jqGrid('setRowData', ids[i], { Opration: cancel });
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
            $("#list8").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function ShowDetail(sid) {

    window.parent.OpenDialog("详细内容", "../SuppliesManage/DetailApp?sid=" + sid, 700, 700, '');
}
function zhunchu() {
    var Job = $("#ExJob").val();
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    if (rowid == null) {
        alert("您还没有选择要处理的审批");
        return;
    }
    else if (Job != "董事长" && Job != "总经理" && Job != "副总经理") {
        return;
    }
    else {
        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        var state2 = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (state2 != "建议停止供货待公司审批" && state2 != "建议暂停供货待公司审批" && state2 != "建议淘汰供应商待公司审批") {
            return;
        }
        if (state2 == "建议停止供货待公司审批") {
            $("#webkey").val("准出停止供货评审");
        }
        else if (state2 == "建议暂停供货待公司审批")
            $("#webkey").val("准出暂停供货评审");
        else if (state2 == "建议淘汰供应商待公司审批")
            $("#webkey").val("准出淘汰供应商评审");
        else if (state2 == "建议恢复供应商待公司审批")
            $("#webkey").val("恢复供应商");

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
                        window.parent.OpenDialog("准出审批", "../SuppliesManage/ApprovalzhunchuSup?id=" + texts, 900, 500, '');
                        return;
                    }
                    if (bol == "1" && Job == "总经理") {//副总经理审批
                        var texts = $("#webkey").val() + "@" + SID;
                        window.parent.OpenDialog("准出审批", "../SuppliesManage/ApprovalzhunchuSup?id=" + texts, 900, 500, '');
                        return;
                    }
                    if (bol == "2" && Job == "董事长") {//总经理才能登陆
                        var texts = $("#webkey").val() + "@" + SID;
                        window.parent.OpenDialog("准出审批", "../SuppliesManage/ApprovalzhunchuSup?id=" + texts, 900, 500, '');
                        return;
                    }
                    //window.parent.OpenDialog("准出审批", "../SuppliesManage/ApprovalzhunchuSup?id=" + texts, 600, 400, '');
                }

            }
        });



        // var texts = $("#webkey").val() + "@" + SID;
        // window.parent.OpenDialog("准出审批", "../SuppliesManage/ApprovalzhunchuSup?id=" + texts, 600, 400, '');
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
        //            window.parent.OpenDialog("审批", "../COM_Approval/ApprovalzhunchuSup?id=" + texts, 600, 400, '');
        //        }
        //        else {
        //            return;
        //        }
        //    }
        //});

    }
}
function huifu() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var Job = $("#ExJob").val();
    if (rowid == null) {
        alert("您还没有选择要处理的审批");
        return;
    } else if (Job != "董事长" && Job != "总经理" && Job != "副总经理") {
        //alert("没有权限进行公司级审批");
        return;
    }

    else {
        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        var state2 = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (state2 != "待公司级恢复供货审批") {
            return;
        }
        if (state2 == "待公司级恢复供货审批") {
            $("#webkey").val("恢复供货");
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
                        window.parent.OpenDialog("恢复供货审批", "../SuppliesManage/ApprovalzhunchuSup?id=" + texts, 900, 500, '');
                        return;
                    }

                    if (bol == "1" && Job == "总经理") {//副总经理审批
                        var texts = $("#webkey").val() + "@" + SID;
                        window.parent.OpenDialog("恢复供货审批", "../SuppliesManage/ApprovalzhunchuSup?id=" + texts, 900, 500, '');
                        return;
                    }

                    if (bol == "2" && Job == "董事长") {//总经理才能登陆
                        var texts = $("#webkey").val() + "@" + SID;
                        window.parent.OpenDialog("恢复供货审批", "../SuppliesManage/ApprovalzhunchuSup?id=" + texts, 900, 500, '');
                        return;
                    }
                    // var texts = $("#webkey").val() + "@" + SID;
                    //window.parent.OpenDialog("恢复供货审批", "../SuppliesManage/ApprovalzhunchuSup?id=" + texts, 600, 400, '');
                }

            }
        });




        // var texts = $("#webkey").val() + "@" + SID;
        //window.parent.OpenDialog("恢复供货审批", "../SuppliesManage/ApprovalzhunchuSup?id=" + texts, 600, 400, '');
        //$.ajax({
        //    url: "../COM_Approval/JudgeAppDisable",
        //    type: "post",
        //    data: { data1: $("#webkey").val(), data2: SPID },
        //    dataType: "Json",
        //    success: function (data) {
        //        if (data.success == "true") {
        //            var bol = data.intblo;
        //            if (bol == "-1") {
        //                //alert("您没有审批权限，不能进行审批操作");
        //                return;
        //            }
        //            if (bol == "1") {
        //                //alert("您已经审批完成，不能进行审批操作");
        //                return;
        //            }
        //            if (bol == "2") {
        //                //alert("审批过程还没有进行到您这一步，不能进行审批操作");
        //                return;
        //            }
        //            var texts = $("#webkey").val() + "@" + SPID + "@" + SID;
        //            window.parent.OpenDialog("审批", "../COM_Approval/ApprovalzhunchuSup?id=" + texts, 600, 400, '');
        //        }
        //        else {
        //            return;
        //        }
        //    }
        //});

    }
}
function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    RelenvceID = jQuery("#list").jqGrid('getRowData', rowid).SID;
    reload1();
}
function reload1() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var state = jQuery("#list").jqGrid('getRowData', rowid).State;
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
        webkey = "恢复供货";
        folderBack = "BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/73/tk_PID/75/tk_UserLog";
    }
    //webkey = $('#webkey').val();//恢复审批
    //folderBack = $('#folderBack').val();
    $("#list6").jqGrid('setGridParam', {
        url: '../SuppliesManage/Condition',
        datatype: 'json',
        postData: { curpage: curPageQK, rownum: OnePageCountQK, SID: sid, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}

//根据pid检索一个数据，pid是唯一的。
function jqW() {
    $("#list6").jqGrid('GridUnload');
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var state = jQuery("#list").jqGrid('getRowData', rowid).State;
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
        webkey = "恢复供货";
        folderBack = "BGOI_BasMan/tk_SApproval/tk_SupplierBas/SID/10/73/tk_PID/75/tk_UserLog";
    }
    //webkey = $('#webkey').val();
    // folderBack = $('#folderBack').val();
    jQuery("#list6").jqGrid({
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
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCountQK,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
            }
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager6") {
                if (curPageQK == $("#list6").getGridParam("lastpage"))
                    return;
                curPageQK = $("#list6").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager6") {
                curPageQK = $("#list6").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager6") {
                if (curPageQK == 1)
                    return;
                curPageQK = $("#list6").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager6") {
                curPageQK = 1;
            }
            else {
                curPageQK = $("#pager6 :input").val();
            }
            reload1();
            reload();
        },
        loadComplete: function () {
            $("#list6").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function DownloadAward(id) {
    var model = jQuery("#list7").jqGrid('getRowData', id);
    var Proid = model.ID;//唯一编号

    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    window.open("DownLoadAward?sid=" + Proid);

}
function DownloadPrice(id) {
    var model = jQuery("#list8").jqGrid('getRowData', id);
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
    var model = jQuery("#list2").jqGrid('getRowData', id);
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
    var model = jQuery("#list3").jqGrid('getRowData', id);
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
    var model = jQuery("#list4").jqGrid('getRowData', id);
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





