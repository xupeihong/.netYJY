var SName, Type, Area, PName, Standard, State, SID, PID, FID, OrderDate, Role, UnitId;
var oldSelID = 0;
var oldproID = 0;
var oldSerID = 0;
var oldplanPro = 0;
var oldplanSer = 0;
var oldLog = 0;
var oldDealRecord = 0;
var oldSP = 0;
var oldAward = 0;
var oldPrice = 0;
var curPageSP = 1;
var OnePageCountSP = 15;
var curPageQK = 1;
var OnePageCountQK = 15;
var curPage = 1, curPageLog = 1, curPagePro = 1, curPageSer = 1, curPageZiZhi = 1, curPageZS = 1, curPageAward = 1, curPagePrice = 1;
var OnePageCountLog = 10, OnePageCountPro = 10, OnePageCountSer = 10, OnePageCountZiZhi = 10, OnePageCountZS = 10, OnePageCount = 10, OnePageCountAward = 10, OnrPageCountPrice = 10;

$(document).ready(function () {


    var job = $("#Job").val();
    if (job != "经理" && job != "副经理") {
        $("#DaiSP").hide();
        $("#InnerApproval").hide();
    }

    //if (job != "董事长" && job != "总经理" && job != "副总经理") {
    //    $("#InnerApproval").show();
    //}
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    $("#list1").jqGrid("setGridWidth", $("#bor1").width() + 15, false);
    $("#list2").jqGrid("setGridWidth", $("#bor2").width() + 15, false);
    $("#list3").jqGrid("setGridWidth", $("#bor3").width() + 15, false);
    $("#list8").jqGrid("setGridWidth", $("#bor8").width() + 15, false);
    $("#list9").jqGrid("setGridWidth", $("#bor9").width() + 15, false);
    jq();
    $("#two").hide();
    $("#three").hide();
    $("#four").hide();
    $("#five").hide();
    $("#six").hide();
    $("#sven").hide();
    $("#eight").hide();
    $("#nine").hide();
    jqPro();
    jqLog();
    //jqSP();
    // jq1();
    jqSer();
    isExjob();
    jqPlanPro();
    jqPlanSer();
    jqAward();
    jqPrice();
    $("#Product").click(function () {
        this.className = "btnTw";
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#DealRecord').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");


        $("#one").css("display", "");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");


    });

    $("#Server").click(function () {

        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#DealRecord').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");


        $("#two").css("display", "");
        $("#one").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");

    });
    $("#planPro").click(function () {

        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#DealRecord').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");


        $("#three").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");

    });
    $("#planServer").click(function () {

        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#DealRecord').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");


        $("#four").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#five").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");

    });
    $("#BuyRecord").click(function () {

        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#DealRecord').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");


        $("#four").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#five").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
        $("#nine").css("display", "none");
    });


    $("#AcceptRecord").click(function () {
        $("#six").show();
        $("#one").hide();
        $("#two").hide();
        $("#three").hide();
        $("#four").hide();
        $("#five").hide();
        $("#sven").hide();
        $("#eight").hide();
        $("#nine").hide();
    });
    $("#BackRecord").click(function () {
        $("#sven").show();
        $("#one").hide();
        $("#two").hide();
        $("#three").hide();
        $("#four").hide();
        $("#five").hide();
        $("#six").hide();
        $("#sven").hide();
        $("#eight").hide();
        $("#nine").hide();
    });
    $("#DealRecord").click(function () {

        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        //$('#btnSP').attr("class", "btnTh");


        $("#eight").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#nine").css("display", "none");

    });
    $("#btnLog").click(function () {

        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#DealRecord').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");


        $("#nine").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");

    })
    $("#btnAward").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#DealRecord').attr("class", "btnTh");
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


    })
    $("#btnPrice").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#DealRecord').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");


        $("#eight").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five").css("display", "none");
        $("#nine").css("display", "none");
        $("#sven").css("display", "none");


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
    $("#PSDetail").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要查评审详细的信息");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        window.parent.OpenDialog("准入评审详细", "../SuppliesManage/DetailPS?sid=" + texts, 1000, 650, '');
    })
    $("#SuppMan").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
        var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
        var username = jQuery("#list").jqGrid('getRowData', rowid).CreateUser;
        var job = $("#Department").val();
        var name = $("#UserName").val();//添加供应商联系人

        if (rowid == null) {
            alert("您还没有选择要修改的基本信息");
            return;
        }
        else if (name != username) {
            alert("只有本人才能修改"); return;
        }
        if (state != "内部评审未通过" && state != "部门负责人审批未通过" && state != "最终评审未通过" && state != "待内部评审申请" && state != "会签审批未通过") {
            alert("该供应商信息不能进行修改");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
            window.parent.OpenDialog("基本信息修改", "../SuppliesManage/UpdateBas?sid=" + texts, 1120, 1100, '');
        }
    })
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
    $("#AddZiZhi").click(function () {
        // var sid = $("#Sid").val();
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
    $("#AddZhenShu").click(function () {
        //var sid = $("#Sid").val();
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
    $("#DaiSP").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要处理的准入审批");
            return;
        }
        else {
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;

            if (state != "待部门负责人审批") {
                alert("不能进行部门负责人评审操作");
                return;
            }
        }
        //var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "准入评审";
        //window.parent.OpenDialog("部门负责人审批", "../SuppliesManage/ApprovalZhuRu?id=" + sid, 700, 250, '');


        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
        window.parent.OpenDialog("部门负责人审批", "../SuppliesManage/ApprovalZhuRu1?id=" + sid, 700, 250, '');
    })
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
    $("#InnerApproval").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        //判断资质证明文件是否存在？
        if (rowid == null) {
            alert("你还没有选择要内部评审申请的的供应商");
            return;
        }
        else {
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (state != '待内部评审申请' && state != "最终评审未通过" && state != "内部评审未通过" && state != "部门负责人审批未通过" && state != "会签审批未通过") {
                alert("该项目已完成内部评审申请，不能进行重复操作");
                return;
            }

            var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
            window.parent.OpenDialog("内部评审", "../SuppliesManage/InnerApproval?sid=" + sid, 800, 550, '');
        }
    })
    //$("#FZInnerApproval").click(function () {   //这个没用了
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("您还没有选择负责人要处理的内部评审申请数据");
    //        return;
    //    }
    //    else {
    //        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
    //        if (state == '待内部评审申请') {
    //            alert("还不能进行负责人评审意见");
    //            return;
    //        }
    //        else if (state != "内部评审待处理") {
    //            alert("该项目已完成内部评审,不能进行重复操作");
    //            return;
    //        }
    //        else {
    //            // var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs;
    //            var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDs + "@" + "准入评审";
    //            window.parent.OpenDialog("负责人意见", "../SuppliesManage/FZInnerApproval?sid=" + sid, 750, 200, '');
    //        }
    //    }
    //})

})
//供应商基本信息
function reload() {
    if ($('.field-validation-error').length == 0) {
        SName = document.getElementById("COMNameC").value;
        Type = $("#Type").val();
        Area = $("#Area").val();
        State = $("#State").val();
        ProName = $("#ProductName").val();
        $("#list").jqGrid('setGridParam', {
            url: 'ManageGrid',
            datatype: 'json',
            height: 150,
            postData: { curpage: curPage, rownum: OnePageCount, sName: SName, type: Type, area: Area, state: State, pName: ProName, order: $("#Order").val() },

        }).trigger("reloadGrid");
    }
}
function reloadRefresh() {
    curPage = 1;
    if ($('.field-validation-error').length == 0) {
        SName = document.getElementById("COMNameC").value;
        Type = $("#Type").val();
        Area = $("#Area").val();//所属部门
        State = $("#State").val();
        ProName = $("#ProductName").val();
        $("#list").jqGrid('setGridParam', {
            url: 'ManageGrid',
            datatype: 'json',
            height: 150,
            postData: { curpage: curPage, rownum: OnePageCount, sName: SName, type: Type, area: Area, state: State, pName: ProName, order: $("#Order").val() },
        }).trigger("reloadGrid");
    }
}
//产品信息
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
//服务信息
function reloadServer() {
    $("#list2").jqGrid('setGridParam', {
        url: 'ManageServerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageSer, rownum: OnePageCountSer, Sid: SID },
    }).trigger("reloadGrid");
}
//资质信息
function reloadPlanPro() {
    $("#list3").jqGrid('setGridParam', {
        url: 'ManagePlanProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZiZhi, rownum: OnePageCountZiZhi, Sid: SID, Fid: FID },
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
function reloadSP() {
    $("#list5").jqGrid('setGridParam', {
        url: 'SPShowGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageSP, rownum: OnePageCountSP, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadss() {
    //webkey = $('#webkey').val();
    //folderBack = $('#folderBack').val();
    $("#list6").jqGrid('setGridParam', {
        url: 'ConditionGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageQK, rownum: OnePageCountQK, Sid: SID },

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
function selAward(rowid) {
    if ($('input[id=j' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldAward != 0) {
            $('input[id=j' + oldAward + ']').prop("checked", false);
        }
        $('input[id=j' + rowid + ']').prop("checked", true);
        $("#list7").setSelection(rowid)
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
        $("#list8").setSelection(rowid)
    }
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
function selChangeLog(rowid9) {
    if ($('input[id=h' + rowid9 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldLog != 0) {
            $('input[id=h' + oldLog + ']').prop("checked", false);
        }
        $('input[id=h' + rowid9 + ']').prop("checked", true);
        $("#list9").setSelection(rowid9)
    }
}
function selChangeSP(rowid5) {
    if ($('input[id=i' + rowid5 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSP != 0) {
            $('input[id=i' + oldSP + ']').prop("checked", false);
        }
        $('input[id=i' + rowid5 + ']').prop("checked", true);
        $("#list5").setSelection(rowid5)
    }
}
function jq() {

    SName = document.getElementById("COMNameC").value;
    Type = $("#Type").val();
    Area = $("#Area").val();
    State = $("#State").val();
    ProName = $("#ProductName").val();
    jQuery("#list").jqGrid({
        url: 'ManageGrid',
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
        colNames: ['', '', '流水号', '审批编号', '供应商名称', '供应商类别', '产品名称', '服务名称', '工厂/出货地址', '企业类型', '供需关系', '所属地区', '评审状态', '当前状态', '年度评价状态', '产品名称', '时间', '资质到期时间', '证书到期时间', '所属部门', '姓名'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 20, hidden: true },
         { name: 'SIDs', index: 'SIDs', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 50 },
        { name: 'PID', index: 'PID', width: 130, hidden: true },
        { name: 'COMNameC', index: 'COMNameC', width: 210 },
        { name: 'SupplierType', index: 'SupplierType', width: 120 },
        { name: 'ProductName', index: 'ProductName', width: 200 },
        { name: 'ServiceName', index: 'ServiceName', width: 200 },
        { name: 'COMFactoryAddress', index: 'COMFactoryAddress', width: 200, hidden: true },
        { name: 'EnterpriseType', index: 'EnterpriseType', width: 120, hidden: true },
        { name: 'Relation', index: 'Relation', width: 100, hidden: true },
        { name: 'COMArea', index: 'COMArea', width: 120, hidden: true },
        { name: 'State', index: 'State', width: 80 },
        { name: 'WState', index: 'WState', width: 70 },
        { name: 'NState', index: 'NState', width: 80, hidden: true },
        { name: 'ProductName', index: 'ProductName', width: 80, hidden: true },
        { name: 'CreateTime', index: 'CreateTime', width: 80, hidden: true },
        { name: 'FTimeOut', index: 'FTimeOut', width: 80, hidden: true },
        { name: 'TimeOut', index: 'TimeOut', width: 80, hidden: true },
        { name: 'DeptName', index: 'DeptName', width: 80 },
        { name: 'CreateUser', index: 'CreateUser', width: 80, hidden: true },
        ],
        pager: jQuery('#pager'),
        sortname: 'State',
        sortable: true,
        optionloadonce: true,
        sortorder: 'asc',
        pgbuttons: true,
        rownumbers: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '基础信息',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var str = "<a href='#' style='color:blue' onclick='ShowDetail2(\"" + curRowData.SID + "\")' >" + curRowData.SID + "</a>";
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
            loadPrduct(sid);
            loadServer(sid);
            loadPlanPro(sid, fid);
            loadPlanSer(sid);
            laodLog(sid);
            loadSP(sid);
            loadAward(sid);
            loadPrice(sid);
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
//产品
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
        //loadComplete: function () {
        //    $("#list1").jqGrid("setGridWidth", $("#bor1").width() + 15, false);
        //},
    }).trigger("reloadGrid");
}
//服务
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
// 资质
function loadPlanPro(sid, fid) {
    SID = sid;
    FID = fid;
    curPageZiZhi = 1;
    $("#list3").jqGrid('setGridParam', {
        url: 'ManagePlanProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZiZhi, rownum: OnePageCountZiZhi, Sid: SID, Fid: FID },
        //loadComplete: function () {
        //    $("#list3").jqGrid("setGridWidth", $("#bor3").width() + 15, false);
        //},
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
        //loadComplete: function () {
        //    $("#list4").jqGrid("setGridWidth", $("#bor4").width() + 15, false);
        //},
    }).trigger("reloadGrid");
}
//日志
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
function loadSP(sid) {
    SID = sid;
    curPageSP = 1;
    $("#list5").jqGrid('setGridParam', {
        url: 'SPShowGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageSP, rownum: OnePageCountSP, Sid: SID },
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
        colNames: ['', '序号', '供应商编号', '产品分类', '产品编号', '产品名称', '规格类型', '单位', '详细说明', '参考价格', '产地', '文档名称', '时间', '操作', '部门'],
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
        { name: 'Opration', index: 'Opration', width: 100 },
       { name: 'DeptName', index: 'DeptName', width: 50, hidden: true },
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
                var cancel = "<a onclick='deleteTr(" + id + ")' style='color:blue;cursor:pointer;'>删除</a>" + "  <a onclick='UpdatePro(" + id + ")' style='color:blue;cursor:pointer;'>修改</a>" + "    <a onclick='CheckProduct(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
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
            //var re_records = $("#list1").getGridParam('records');
            //if (re_records == 0 || re_records == null) {
            //    if ($(".norecords").html() == null) {
            //        $("#list1").parent().append("<div class=\"norecords\" style='text-align:center;margin-top:50px;font-size:20px;'>没有符合数据</div>");
            //    }
            //    $(".norecords").show();
            //}
            //else
            //    $(".norecords").hide();
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
        colNames: ['', '序号', '服务名称', '服务编号', '服务描述', '用途', '文档名称', '时间', '操作', '部门'],
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
        { name: 'DeptName', index: 'DeptName', width: 50, hidden: true },
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
                var cancel = "<a onclick='deleteSer(" + id + ")' style='color:blue;cursor:pointer;'>删除</a>" + "  <a onclick='UpdateSer(" + id + ")' style='color:blue;cursor:pointer;'>修改</a>" + "    <a onclick='ServerOut(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
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
            //var re_records = $("#list2").getGridParam('records');
            //if (re_records == 0 || re_records == null) {
            //    if ($(".norecords").html() == null) {
            //        $("#list2").parent().append("<div class=\"norecords\" style='text-align:center;margin-top:50px;font-size:20px;'>没有符合数据</div>");
            //    }
            //    $(".norecords").show();
            //}
            //else
            //    $(".norecords").hide();
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
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
        colNames: ['', '序号', '资质类型', 'FID', '资质具体项', '资质证书具体项', '其他项说明', '文档名称', '内部评审时资质文档名称', '文档类型', '时间', '操作'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 50, hidden: true },
        { name: 'TypeO', index: 'TypeO', width: 100 },
        { name: 'FID', index: 'FID', width: 50, hidden: true },
        { name: 'FType', index: 'FType', width: 100 },
        { name: 'Item', index: 'Item', width: 150 },
        { name: 'ItemO', index: 'ItemO', width: 150 },
        { name: 'FFileName', index: 'FFileName', width: 150 },
        { name: 'MffileName', index: 'MffileName', width: 150, hidden: true },
        { name: 'FileType', index: 'FileType', width: 80, hidden: true },
        { name: 'CreateTime', index: 'CreateTime', width: 80 },
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
                var cancel = "<a onclick='deleteFile(" + id + ")' style='color:blue;cursor:pointer;'>删除</a>" + "  <a onclick='UpdateFiles(" + id + ")' style='color:blue;cursor:pointer;'>修改</a>" + "  <a onclick='DownFile(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
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
            reloadServer();
        },
        loadComplete: function () {
            //var re_records = $("#lis3t").getGridParam('records');
            //if (re_records == 0 || re_records == null) {
            //    if ($(".norecords").html() == null) {
            //        $("#list3").parent().append("<div class=\"norecords\" style='text-align:center;margin-top:50px;font-size:20px;'>没有符合数据</div>");
            //    }
            //    $(".norecords").show();
            //}
            //else
            //    $(".norecords").hide();
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
        colNames: ['', '序号', '是否为计划性证书', '证书类型', '证书名称', '证书编号', '认证机构名称', '文档名称', '文档类型', '添加证书时间', '证书到期时间', '操作', 'FID'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 50, hidden: true },
        { name: 'IsPlan', index: 'IsPlan', width: 80 },
        { name: 'CType', index: 'CType', width: 100 },
        { name: 'CName', index: 'CName', width: 150 },
        { name: 'CCode', index: 'CCode', width: 150 },
        { name: 'COrganization', index: 'COrganization', width: 150 },
       // { name: 'CDate', index: 'CDate', width: 120 },
        { name: 'CFileName', index: 'CFileName', width: 150 },
        { name: 'FileType', index: 'FileType', width: 80, hidden: true },
        { name: 'CreateTime', index: 'CreateTime', width: 100 },
        { name: 'TimeOut', index: 'TimeOut', width: 100, hidden: true },
        { name: 'Opration', index: 'Opration', width: 150 },
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
                var cancel = "<a onclick='deleteCertificate(" + id + ")' style='color:blue;cursor:pointer;'>删除</a>" + "  <a onclick='UpdateCertify(" + id + ")' style='color:blue;cursor:pointer;'>修改</a>" + "  <a onclick='DownZhenshu(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
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
            //var re_records = $("#list4").getGridParam('records');
            //if (re_records == 0 || re_records == null) {
            //    if ($(".norecords").html() == null) {
            //        $("#list4").parent().append("<div class=\"norecords\" style='text-align:center;margin-top:50px;font-size:20px;'>没有符合数据</div>");
            //    }
            //    $(".norecords").show();
            //}
            //else
            //    $(".norecords").hide();
            $("#list4").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
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
                var curChk = "<input id='h" + id + "' onclick='selChangeLog(" + id + ")' type='checkbox' value='" + jQuery("#list9").jqGrid('getRowData', id).SID + "' name='cb'/>";
                jQuery("#list9").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            //var re_records = $("#list9").getGridParam('records');
            //if (re_records == 0 || re_records == null) {
            //    if ($(".norecords").html() == null) {
            //        $("#list9").parent().append("<div class=\"norecords\" style='text-align:center;margin-top:50px;font-size:20px;'>没有符合数据</div>");
            //    }
            //    $(".norecords").show();
            //}
            //else
            //    $(".norecords").hide();
            $("#list9").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqSP() {
    jQuery("#list5").jqGrid({
        url: 'SPShowGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageSP, rownum: OnePageCountSP, Sid: SID },
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
        colNames: ['', '供应商编号', '是否是免评供应商', '是否为集团合格供应商', '项目经理名称', '供应商为', '免评审供应商资质证明', '供应商初步评审综合评价', '是否通过最终评审', '最终意见', '签字人', '签字时间'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 100 },
        { name: 'IsUnReview', index: 'IsUnReview', width: 180 },
        { name: 'URConfirmUser', index: 'URConfirmUser', width: 130 },
        { name: 'IsUnreviewUser', index: 'IsUnreviewUser', width: 150 },
        { name: 'UnReviewUnit', index: 'UnReviewUnit', width: 150, hidden: true },
        { name: 'MfFilename', index: 'MfFilename', width: 150 },
        { name: 'SContent', index: 'SContent', width: 150 },
        { name: 'ResState', index: 'ResState', width: 150 },
        { name: 'ApprovalRes', index: 'ApprovalRes', width: 150 },
        { name: 'Approval4User', index: 'Approval4User', width: 150 },
        { name: 'AppTime', index: 'AppTime', width: 150 },
        ],
        pager: jQuery('#pager5'),
        pgbuttons: true,
        rowNum: OnePageCountSP,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批记录',
        gridComplete: function () {
            var ids = jQuery("#list5").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list5").jqGrid('getRowData', id);
                var curChk = "<input id='i" + id + "' onclick='selChangeSP(" + id + ")' type='checkbox' value='" + jQuery("#list5").jqGrid('getRowData', id).SID + "' name='cb'/>";
                jQuery("#list5").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid5, status) {
            if (oldSP != 0) {
                $('input[id=i' + oldSP + ']').prop("checked", false);
            }
            $('input[id=i' + rowid5 + ']').prop("checked", true);
            oldSP = rowid5;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager5") {
                if (curPageSP == $("#list5").getGridParam("lastpage"))
                    return;
                curPageSP = $("#list5").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager5") {
                curPageSP = $("#list5").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager5") {
                if (curPageSP == 1)
                    return;
                curPageSP = $("#list5").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager5") {
                curPageSP = 1;
            }
            else {
                curPageSP = $("#pager5 :input").val();
            }
            reloadSP();
        },
        loadComplete: function () {
            //var re_records = $("#list5").getGridParam('records');
            //if (re_records == 0 || re_records == null) {
            //    if ($(".norecords").html() == null) {
            //        $("#list5").parent().append("<div class=\"norecords\" style='text-align:center;margin-top:50px;font-size:20px;'>没有符合数据</div>");
            //    }
            //    $(".norecords").show();
            //}
            //else
            //    $(".norecords").hide();
            $("#list5").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jq1() {
    //webkey = $('#webkey').val();
    //folderBack = $('#folderBack').val();
    jQuery("#list6").jqGrid({
        url: 'ConditionGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageQK, rownum: OnePageCountQK, Sid: SID },
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
        colNames: ['', '评审类型', '姓名', '负责人意见', '评审时间'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'Type', index: 'Type', width: 100 },
        { name: 'LogPerson', index: 'LogPerson', width: 100 },
        { name: 'LogContent', index: 'LogContent', width: 150 },
        { name: 'LogTime', index: 'LogTime', width: 130 },
        ],
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCountQK,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '评审过程情况表',
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
            reloadss();
            //reload();
        },
        loadComplete: function () {
            // $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
            $("#list6").jqGrid("setGridWidth", $("#bor").width() - 30, false);
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
                var cancel = "<a onclick='deleteAward(" + id + ")' style='color:blue;cursor:pointer;'>删除</a>&nbsp;&nbsp;" + "<a onclick='DownloadAward(" + id + ")' style='color:blue;cursor:pointer;'>下载</a>";
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
                var cancel = "<a onclick='deletePrice(" + id + ")' style='color:blue;cursor:pointer;'>删除</a>&nbsp;&nbsp;" + "<a onclick='DownloadPrice(" + id + ")' style='color:blue;cursor:pointer;'>下载</a>";
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


//删除可供应产品
function deleteTr(id) {
    var curRowData = jQuery("#list1").jqGrid('getRowData', id);
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    var right = $("#Rights").val();

    if (job != deptname) {
        alert("只有本部门的人员才能删除产品"); return;
    }
    var SID = curRowData.SID;
    var ID = curRowData.ID;
    var one = confirm("确实要删除产品信息吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deletepPro",
            type: "post",
            data: { data1: SID, id: ID },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
        $("#list1").trigger('reloadGrid');
        $("#list").trigger('reloadGrid');
    }
}
function deleteSer(id) {
    var curRowData = jQuery("#list2").jqGrid('getRowData', id);
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (job != deptname) {
        alert("只有本部门的人员才能删除服务"); return;
    }
    var Time = curRowData.CreateTime;
    var SID = curRowData.SID;
    var one = confirm("确实要删除服务吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deletepServer",
            type: "post",
            data: { data1: SID, time: Time },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
        $("#list2").trigger('reloadGrid');
        $("#list").trigger('reloadGrid');
    }
}
function deleteFile(id) {
    var curRowData = jQuery("#list3").jqGrid('getRowData', id);
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (job != deptname) {
        alert("只有本部门的人员才能删除资质信息"); return;
    }
    var Time = curRowData.CreateTime;//jQuery("#list").jqGrid('getRowData', rowid).CreateTime;
    var SID = curRowData.SID;
    var one = confirm("确实要删除资质信息吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deletepApproval",
            type: "post",
            data: { data1: SID, time: Time },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
        $("#list3").trigger('reloadGrid');
    }
}
function deleteCertificate(id) {
    var curRowData = jQuery("#list4").jqGrid('getRowData', id);
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (job != deptname) {
        alert("只有本部门的人员才能删除证书信息"); return;
    }
    var Time = curRowData.CreateTime; //jQuery("#list").jqGrid('getRowData', rowid).CreateTime;
    var SID = curRowData.SID;
    var one = confirm("确实要删除证书信息吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deletepZhenshu",
            type: "post",
            data: { data1: SID, time: Time },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    $("#list4").trigger('reloadGrid');
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
        $("#list4").trigger('reloadGrid');
    }
}
function deletePrice(id) {
    var curRowData = jQuery("#list8").jqGrid('getRowData', id);
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (job != deptname) {
        alert("只有本部门的人员才能删除报价/比价单"); return;
    }
    var SID = curRowData.SID;
    var ID = curRowData.ID;
    var one = confirm("确实要删除报价/比价单吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deletepPrice",
            type: "post",
            data: { data1: SID, id: ID },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
        $("#list8").trigger('reloadGrid');
        $("#list").trigger('reloadGrid');
    }
}
function deleteAward(id) {
    var curRowData = jQuery("#list7").jqGrid('getRowData', id);
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (job != deptname) {
        alert("只有本部门的人员才能删除曾获奖项"); return;
    }
    var SID = curRowData.SID;
    var ID = curRowData.ID;
    var one = confirm("确实要删除曾获奖项吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deletepAward",
            type: "post",
            data: { data1: SID, id: ID },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
        $("#list7").trigger('reloadGrid');
        $("#list").trigger('reloadGrid');
    }
}

//修改可供应产品
function UpdatePro(id) {
    var model = jQuery("#list1").jqGrid('getRowData', id);
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (job != deptname) {
        alert("只有本部门的人员才能修改产品"); return;
    }
    var Time = model.CreateTime;
    var Proid = model.ID;//唯一编号
    var sid = model.SID;
    var filename = model.FFileName;//文档名称
    window.parent.OpenDialog("更新产品", "../SuppliesManage/UPProduct?sid=" + sid + "&id=" + Proid + "&time=" + Time + "&filename=" + filename, 450, 500, '');
}
//修改服务
function UpdateSer(id) {
    var model = jQuery("#list2").jqGrid('getRowData', id);
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (job != deptname) {
        alert("只有本部门的人员才能修改服务"); return;
    }
    var sid = model.SID;
    var serid = model.ServiceID;//服务编号唯一
    var filename = model.FFileName;
    window.parent.OpenDialog("更新服务", "../SuppliesManage/UPServer?sid=" + sid + "&id=" + serid + "&filename=" + filename, 450, 250, '');
}
//修改资质
function UpdateFiles(id) {
    var model = jQuery("#list3").jqGrid('getRowData', id);
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (job != deptname) {
        alert("只有本部门的人员才能修改资质信息"); return;
    }
    var time = model.CreateTime;
    var sid = model.SID;
    var fid = model.FID;
    var filename = model.FFileName;
    window.parent.OpenDialog("更新资质", "../SuppliesManage/UPFile?sid=" + sid + "&fid=" + fid + "&time=" + time + "&filename=" + filename, 500, 400, '');
}
//修改证书
function UpdateCertify(id) {
    var model = jQuery("#list4").jqGrid('getRowData', id);
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var job = $("#Department").val();
    if (job != deptname) {
        alert("只有本部门的人员才能修改证书信息"); return;
    }
    var time = model.CreateTime;
    var sid = model.SID;
    var fid = model.FID;
    // var filetype = model.TimeOut;
    var filename = model.CFileName;
    window.parent.OpenDialog("更新证书", "../SuppliesManage/UPCertifify?sid=" + sid + "&fid=" + fid + "&time=" + time + "&filename=" + filename, 500, 400, '');
}



function ck_Info() {
    $('ckb').click(function () {
        var ckbname = "";
        $('input[name=guonei]:checkbox:checked').each(function () {
            ckbname += "," + $(this).val();
        });
        $('input[name=guonei]:text:input').each(function () {
            ckbname += "," + $(this).val();
        });
        document.getElementById("BusinessDistribute").value = ckbname;
    })
}
function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    RelenvceID = jQuery("#list").jqGrid('getRowData', rowid).SID;
    reloadss();
}
function Cancel() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    var username = jQuery("#list").jqGrid('getRowData', rowid).CreateUser;
    var job = $("#Department").val();
    var name = $("#UserName").val();//添加供应商联系人
    if (rowid == null) {
        alert("您还没有选择要删除供应商？");
        return;
    }
    else if (name != username) {
        alert("只有本人才能进行撤销"); return;
    }
    else {
        var one = confirm("确实要删除供应商信息吗");
        if (one == false) {
            return;
        } else {
            $.ajax({
                url: "CancelSup",
                type: "post",
                data: { data1: SID },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        alert(data.Msg);
                        reload();
                        $("#list").trigger('reloadGrid');
                        $("#list1").trigger('reloadGrid');
                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });
            $("#list").trigger('reloadGrid');
            $("#list1").trigger('reloadGrid');
        }

    }
}
function isExjob() {
    RoleName = $("#Job").val();
    if (RoleName == "董事长" || RoleName == "总经理" || RoleName == "副总经理" || RoleName == "副经理" || RoleName == "经理" || RoleName == "总工程师") {
        $("#FZInnerApproval").show();
    }
    else {
        $("#FZInnerApproval").hide();
    }


}

function ShowDetail2(sid) {

    window.parent.OpenDialog("详细内容", "../SuppliesManage/DetailApp?sid=" + sid, 700, 700, '');
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


function banBackSpace(e) {
    var ev = e || window.event;//获取event对象 
    var obj = ev.target || ev.srcElement;//获取事件源 
    var t = obj.type || obj.getAttribute('type');//获取事件源类型 
    //获取作为判断条件的事件类型
    var vReadOnly = obj.getAttribute('readonly');
    //处理null值情况
    vReadOnly = (vReadOnly == "") ? false : vReadOnly;
    //当敲Backspace键时，事件源类型为密码或单行、多行文本的，
    //并且readonly属性为true或enabled属性为false的，则退格键失效
    var flag1 = (ev.keyCode == 8 && (t == "password" || t == "text" || t == "textarea")
       && vReadOnly == "readonly") ? true : false;
    //当敲Backspace键时，事件源类型非密码或单行、多行文本的，则退格键失效
    var flag2 = (ev.keyCode == 8 && t != "password" && t != "text" && t != "textarea")
       ? true : false;

    //判断
    if (flag2) {
        return false;
    }
    if (flag1) {
        return false;
    }
}
window.onload = function () {
    //禁止后退键 作用于Firefox、Opera
    document.onkeypress = banBackSpace;
    //禁止后退键 作用于IE、Chrome
    document.onkeydown = banBackSpace;

}
