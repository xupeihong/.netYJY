var curPage = 1;
var curPageInner = 1;
var curPageQK = 1;
var curPagePro = 1;
var curPageLog = 1;
var curPageSer = 1;
var curPageZiZhi = 1;
var curPageZS = 1;
var curPageAward = 1;
var curPagePrice = 1;

var OnePageCount = 15;
var OnePageCountInner = 15;
var OnePageCountQK = 15;
var OnePageCountPro = 10;
var OnePageCountLog = 10;
var OnePageCountSer = 10;
var OnePageCountZiZhi = 10;
var OnePageCountZS = 10;
var OnePageCountAward = 10;
var OnrPageCountPrice = 10;
var SName, Type, Area, PName, Standard, State;
var oldSelID = 0;
var oldproID = 0
var oldCerID = 0;
var oldLog = 0;
var oldSerID = 0;
var oldplanPro = 0;
var oldplanSer = 0;
var Evaluation1, Evaluation2, Evaluation3, Evaluation4, Evaluation5, Evaluation7;
var PID, SID, FID;

$(document).ready(function () {
    var job = $("#Exjob").val();
    if (job != "董事长" && job != "总经理" && job != "副总经理" && job != "公司领导") {
        $("#ResApproval").hide();

        //$("#resrt").show();
    }
    //else {
    //    $("#resrt").hide();
    //}

    jq();
    //jqCer();
    jqLog();
    //jq1();
    jqSer();
    jqPlanPro();
    jqPro();
    jqPlanSer();
    jqAward();
    jqPrice();
    $("#five").show();
    $("#zrpsxx").show();
    $("#one").hide();
    $("#two").hide();
    $("#four").hide();
    $("#fives").hide();
    $("#six").hide();
    $("#nine").hide();
    $("#sven").hide();
    $("#eight").hide();
    //$("#Neibu").click(function () {
    //    this.className = "btnTw";
    //    $('#QK').attr("class", "btnTh");
    //    $('#Server').attr("class", "btnTh");
    //    $('#planPro').attr("class", "btnTh");
    //    $('#planServer').attr("class", "btnTh");
    //    $('#btnLog').attr("class", "btnTh");
    //    $('#Product').attr("class", "btnTh");


    //    $("#one").css("display", "");
    //    $("#three").css("display", "none");
    //    $("#four").css("display", "none");
    //    $("#fives").css("display", "none");
    //    $("#six").css("display", "none");
    //    $("#nine").css("display", "none");
    //    $("#two").css("display", "none");

    //});
    $("#QK").click(function () {
        this.className = "btnTw";
        //$('#Neibu').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#Product').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $("#btnPrice").attr("class", "btnTh");

        $("#two").css("display", "");
        //$("#one").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#fives").css("display", "none");
        $("#six").css("display", "none");
        $("#nine").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
    });
    $("#Product").click(function () {
        this.className = "btnTw";
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        //$('#Neibu').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#QK').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $("#btnPrice").attr("class", "btnTh");
        $("#three").css("display", "");
        $("#four").css("display", "none");
        $("#fives").css("display", "none");
        $("#six").css("display", "none");
        $("#nine").css("display", "none");
        //$("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
    });
    $("#Server").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#QK').attr("class", "btnTh");
        //$('#Neibu').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $("#btnPrice").attr("class", "btnTh");
        $("#four").css("display", "");
        //$("#one").css("display", "none");
        $("#three").css("display", "none");
        $("#fives").css("display", "none");
        $("#six").css("display", "none");
        $("#sven").css("display", "none");
        $("#nine").css("display", "none");
        $("#sven").css("display", "none");
        $("#eight").css("display", "none");
    });
    $("#planPro").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#QK').attr("class", "btnTh");
        //$('#Neibu').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $("#btnPrice").attr("class", "btnTh");

        $("#fives").css("display", "");
        //$("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#six").css("display", "none");
        $("#sven").css("display", "none");
        $("#nine").css("display", "none");
        $("#eight").css("display", "none");
    });
    $("#planServer").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#QK').attr("class", "btnTh");
        //$('#Neibu').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $("#btnPrice").attr("class", "btnTh");

        $("#six").css("display", "");
        //$("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#fives").css("display", "none");
        $("#sven").css("display", "none");
        $("#nine").css("display", "none");
        $("#eight").css("display", "none");
    });
    $("#btnLog").click(function () {

        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#QK').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");
        $("#btnPrice").attr("class", "btnTh");

        $("#nine").css("display", "");
        $("#sven").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#fives").css("display", "none");
        $("#six").css("display", "none");
        $("#eight").css("display", "none");
    })
    $("#btnAward").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnPrice').attr("class", "btnTh");


        $("#sven").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#fives").css("display", "none");
        $("#nine").css("display", "none");
        $("#eight").css("display", "none");
        $("#six").css("display", "none");

    })
    $("#btnPrice").click(function () {
        this.className = "btnTw";
        $('#Product').attr("class", "btnTh");
        $('#Server').attr("class", "btnTh");
        $('#planPro').attr("class", "btnTh");
        $('#planServer').attr("class", "btnTh");
        //$('#DealRecord').attr("class", "btnTh");
        $('#btnLog').attr("class", "btnTh");
        $('#btnAward').attr("class", "btnTh");


        $("#eight").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#fives").css("display", "none");
        $("#nine").css("display", "none");
        $("#sven").css("display", "none");
        $("#six").css("display", "none");

    })
    $("#AddPro").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SID;
        if (rowid == null) {
            alert("您还没有选择要添加的产品");
            return;
        }
        window.parent.OpenDialog("新增产品", "../SuppliesManage/AddPro?sid=" + sid, 500, 450, '');
    })
    $("#AddServer").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SID;
        if (rowid == null) {
            alert("您还没有选择要添加的服务行");
            return;
        }
        window.parent.OpenDialog("新增服务", "../SuppliesManage/AddServer?sid=" + sid, 500, 300, '');
    })
    $("#AddZiZhi").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SID;
        if (rowid == null) {
            alert("您还没有选择要添加的资质行");
            return;
        }
        window.parent.OpenDialog("新增资质", "../SuppliesManage/AddFileInfo?sid=" + sid, 500, 300, '');
        //window.parent.OpenPage();
    })
    $("#AddZhenShu").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SID;
        if (rowid == null) {
            alert("您还没有选择要添加的证书行");
            return;
        }
        window.parent.OpenDialog("新增证书", "../SuppliesManage/AddCertificate?sid=" + sid, 500, 350, '');
    })
    $("#zrpsxx").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要查评审详细的信息");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).SIDS;
        window.parent.OpenDialog("准入评审详细", "../SuppliesManage/DetailPS?sid=" + texts, 1000, 650, '');
    })
    //$("#shenpi").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    if (rowid == null) {
    //        alert("您还没有选择要处理的会签评审");
    //        return;
    //    }
    //    else {
    //        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    //        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDS;
    //        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
    //        if (state == $("#Nostate").val()) {
    //            alert("审批不通过，不能进行审批了");
    //            return;
    //        }
    //        if (state !== "准入评审待审批") {
    //            alert("不能进行此操作");
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
    //                    window.parent.OpenDialog("会签评审", "../COM_Approval/ApprovalSup?id=" + texts, 600, 400, '');
    //                }
    //                else {
    //                    return;
    //                }
    //            }
    //        });
    //    }
    //})
    $("#five").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要查看详细的基本信息");
            return;
        }
        var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDS;
        window.parent.OpenDialog("详细信息", "../SuppliesManage/DetailBas?sid=" + sid, 1000, 650, '');
    })
    $("#ResApproval").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
        var user = jQuery("#list").jqGrid('getRowData', rowid).Approval4User;
        var apptype = jQuery("#list").jqGrid('getRowData', rowid).Apptype;
        var Opinions = jQuery("#list").jqGrid('getRowData', rowid).opinion;
        //var job = jQuery("#list").jqGrid('getRowData', rowid).ExJob;
        //if (job != "董事长" && job != "部门经理" && job != "部门副经理" && job != "总经理" && job != "副总经理" && job != "公司领导") {
        //    $("#shenpi").attr('disabled', 'disabled');
        //    $("#ResApproval").attr('disabled', 'disabled');
        //}
        if (rowid == null) {
            alert("你还没有选择要最终评审意见的供应商");
            return;
        }
        if (state != "待最终评审") {
            alert("您选择的供应商还不能填写最终评审意见");
            return;
        }
        else {
            var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDS;
            window.parent.OpenDialog("最终评审", "../SuppliesManage/ResultApp?sid=" + sid, 600, 300, '');
        }
    })
});
function reload1() {
    SName = document.getElementById("COMNameC").value;
    Type = $("#Type").val();
    Area = $("#Area").val();
    State = $("#State").val();
    $("#list").jqGrid('setGridParam', {
        url: 'ApprovalProcess',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sName: SName, type: Type, area: Area, state: State },
    }).trigger("reloadGrid");
};
function reloadRefrsh() {
    //$("#list4").jqGrid("setGridParam", { curpage: curPageSer, rownum: OnePageCountSer }).trigger("reloadGrid");
    curPage = 1;
    SName = document.getElementById("COMNameC").value;
    Type = $("#Type").val();
    Area = $("#Area").val();
    State = $("#State").val();
    $("#list").jqGrid('setGridParam', {
        url: 'ApprovalProcess',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sName: SName, type: Type, area: Area, state: State, order: $("#Order").val() },
    }).trigger("reloadGrid");

}
function reloadinner() {
    $("#list1").jqGrid('setGridParam', {
        url: 'MainCertificateGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageInner, rownum: OnePageCountInner, sid: SID },
    }).trigger("reloadGrid");
}
function reloadpro() {
    PName = $("#PName").val();
    Standard = $("#Standard").val();
    $("#list3").jqGrid('setGridParam', {
        url: 'ManageProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPagePro, rownum: OnePageCountPro, pName: PName, standard: Standard, Sid: SID },
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
function reloadServer() {
    $("#list4").jqGrid('setGridParam', {
        url: 'ManageServerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageSer, rownum: OnePageCountSer, Sid: SID },
    }).trigger("reloadGrid");
}
function reloadPlanPro() {
    $("#list5").jqGrid('setGridParam', {
        url: 'ManagePlanProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZiZhi, rownum: OnePageCountZiZhi, Sid: SID, Fid: FID },
    }).trigger("reloadGrid");
}
function reloadPSer() {
    $("#list6").jqGrid('setGridParam', {
        url: 'ManagePlanSerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZS, rownum: OnePageCountZS, Sid: SID },
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
function selChangeInner(rowid) {
    if ($('input[id=d' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldCerID != 0) {
            $('input[id=d' + oldCerID + ']').prop("checked", false);
        }
        $('input[id=d' + rowid + ']').prop("checked", true);
        $("#list1").setSelection(rowid)
    }
}
function selChangepro(rowid1) {
    if ($('input[id=f' + rowid1 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldproID != 0) {
            $('input[id=f' + oldproID + ']').prop("checked", false);
        }
        $('input[id=f' + rowid1 + ']').prop("checked", true);
        $("#list3").setSelection(rowid1)
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
    if ($('input[id=g' + rowid3 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldplanPro != 0) {
            $('input[id=g' + oldplanPro + ']').prop("checked", false);
        }
        $('input[id=g' + rowid3 + ']').prop("checked", true);
        $("#list5").setSelection(rowid3)
    }
}
function selChangePlanSer(rowid4) {
    if ($('input[id=k' + rowid4 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldplanSer != 0) {
            $('input[id=k' + oldplanSer + ']').prop("checked", false);
        }
        $('input[id=k' + rowid4 + ']').prop("checked", true);
        $("#list6").setSelection(rowid4)
    }
}
function jq() {
    SName = document.getElementById("COMNameC").value;
    Type = $("#Type").val();
    Area = $("#Area").val();
    State = $("#State").val();
    jQuery("#list").jqGrid({
        url: 'ApprovalProcess',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sName: SName, type: Type, area: Area, state: State, order: $("#Order").val() },
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
        colNames: ['', '', '流水号', '审批编号', '申报部门', '供应商名称', '供应商类别', '产品', '服务', '公司办公地址', '工厂/出货地址', '企业类型', '供需关系', '所属地区', '评审状态', '当前状态', '最终评审人', '审批类型', '审批意见', '职位'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 20, hidden: true },
        { name: 'SIDS', index: 'SIDS', width: 80, hidden: true },
        { name: 'SID', index: 'SID', width: 80 },
        { name: 'PID', index: 'PID', width: 130, hidden: true },
        { name: 'DeptName', index: 'DeptName', width: 100 },
        { name: 'COMNameC', index: 'COMNameC', width: 200 },
        { name: 'SupplierType', index: 'SupplierType', width: 100 },
        { name: 'ProductName', index: 'ProductName', width: 200 },
        { name: 'ServiceName', index: 'ServiceName', width: 200 },
        { name: 'ComAddress', index: 'ComAddress', width: 100, hidden: true },
        { name: 'COMFactoryAddress', index: 'COMFactoryAddress', width: 100, hidden: true },
        { name: 'EnterpriseType', index: 'EnterpriseType', width: 90, hidden: true },
        { name: 'Relation', index: 'Relation', width: 100, hidden: true },
        { name: 'COMArea', index: 'COMArea', width: 100, hidden: true },
        { name: 'State', index: 'State', width: 100 },
         { name: 'WState', index: 'WState', width: 70 },
        { name: 'Approval4User', index: 'Approval4User', width: 80, hidden: true },
        { name: 'Apptype', index: 'Apptype', width: 100, hidden: true },
        { name: 'opinion', index: 'opinion', width: 100, hidden: true },
        { name: 'ExJob', index: 'ExJob', width: 100, hidden: true },
        ],
        pager: jQuery('#pager'),
        sortname: 'State',
        sortable: true,
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
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
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
            var pid = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var sid = jQuery("#list").jqGrid('getRowData', rowid).SIDS;
            var fid = jQuery("#list").jqGrid('getRowData', rowid).FID;
            LoadCertifity(sid);
            loadPrduct(sid);
            laodLog(sid);
            loadServer(sid);
            loadPlanPro(sid, fid);
            loadPlanSer(sid);
            loadAward(sid);
            loadPrice(sid)
            select(rowid);
            Zhunchu();//会签审批
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
            reload1();
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
            reloadRefrsh();
        }
    });
}
//内部评价
function jqCer() {
    jQuery("#list1").jqGrid({
        url: 'MainCertificateGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageInner, rownum: OnePageCountInner, sid: SID },
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
        colNames: ['', '序号', '对供应商信誉评价', '对供方价格水平的评价', '对供方产品(工程)质量，技术能力的评价', '对供方合作意见的评价', '是否对供方进行实地考察', '考察评价', '供应商初步评审综合评价'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 50, hidden: true },
        { name: 'Evaluation1', index: 'Evaluation1', width: 150 },
        { name: 'Evaluation2', index: 'Evaluation2', width: 150 },
        { name: 'Evaluation3', index: 'Evaluation3', width: 150 },
        { name: 'Evaluation4', index: 'Evaluation4', width: 150 },
        { name: 'Evaluation5', index: 'Evaluation5', width: 150 },
        { name: 'Evaluation6', index: 'Evaluation6', width: 150 },
        { name: 'Evaluation7', index: 'Evaluation7', width: 150 },
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCountInner,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '基础信息',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='d" + id + "' onclick='selChangeInner(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).SID + "' name='cb1'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldCerID != 0) {
                $('input[id=d' + oldCerID + ']').prop("checked", false);
            }
            $('input[id=d' + rowid + ']').prop("checked", true);
            oldCerID = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPageInner == $("#list1").getGridParam("lastpage"))
                    return;
                curPageInner = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPageInner = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPageInner == 1)
                    return;
                curPageInner = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPageInner = 1;
            }
            else {
                curPageInner = $("#pager1 :input").val();
            }
            reloadinner();
        },
        loadComplete: function () {
            //$("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function LoadCertifity(sid) {
    SID = sid;
    curPageInner = 1;
    $("#list1").jqGrid('setGridParam', {
        url: 'MainCertificateGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageInner, rownum: OnePageCountInner, sid: SID },
    }).trigger("reloadGrid");
}
function loadPrduct(sid) {
    SID = sid;
    curPagePro = 1;
    PName = $("#PName").val();
    Standard = $("#Standard").val();
    $("#list3").jqGrid('setGridParam', {
        url: 'ManageProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPagePro, rownum: OnePageCountPro, pName: PName, standard: Standard, Sid: SID },
    }).trigger("reloadGrid");
}
function laodLog(sid) {
    SID = sid;
    curPageLog = 1;
    $("#list9").jqGrid('setGridParam', {
        url: 'LogShowGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageLog, rownum: OnePageCountLog, Sid: SID },
        //loadComplete: function () {
        //    $("#list9").jqGrid("setGridWidth", $("#bor9").width() + 15, false);
        //},
    }).trigger("reloadGrid");
}
function loadServer(sid) {
    SID = sid;
    curPageSer = 1;
    $("#list4").jqGrid('setGridParam', {
        url: 'ManageServerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageSer, rownum: OnePageCountSer, Sid: SID },
        //loadComplete: function () {
        //    $("#list4").jqGrid("setGridWidth", $("#bor4").width() + 15, false);
        //},
    }).trigger("reloadGrid");
}
function loadPlanPro(sid, fid) {
    SID = sid;
    FID = fid;
    curPageZiZhi = 1;
    $("#list5").jqGrid('setGridParam', {
        url: 'ManagePlanProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZiZhi, rownum: OnePageCountZiZhi, Sid: SID, Fid: FID },
        //loadComplete: function () {
        //    $("#list5").jqGrid("setGridWidth", $("#bor5").width() + 15, false);
        //},
    }).trigger("reloadGrid");
}
function loadPlanSer(sid) {
    SID = sid;
    curPageZS = 1;
    $("#list6").jqGrid('setGridParam', {
        url: 'ManagePlanSerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageZS, rownum: OnePageCountZS, Sid: SID },
        //loadComplete: function () {
        //    $("#list4").jqGrid("setGridWidth", $("#bor4").width() + 15, false);
        //},
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
function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    RelenvceID = jQuery("#list").jqGrid('getRowData', rowid).SID;
    reload();
}
function reload() {
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list2").jqGrid('setGridParam', {
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageQK, rownum: OnePageCountQK, PID: PID, webkey: webkey, folderBack: folderBack, order: $("#Order").val() },

    }).trigger("reloadGrid");
}
function jq1() {
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list2").jqGrid({
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageQK, rownum: OnePageCountQK, PID: PID, webkey: webkey, folderBack: folderBack },
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
        { name: 'Opinion', index: 'Opinion', width: 150 },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 130 },
        { name: 'Remark', index: 'Remark', width: 200 },
        ],
        pager: jQuery('#pager2'),
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
function jqPro() {
    PName = $("#PName").val();
    Standard = $("#Standard").val();
    jQuery("#list3").jqGrid({
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
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCountPro,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '拟购产品',
        gridComplete: function () {
            var ids = jQuery("#list3").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list3").jqGrid('getRowData', id);
                var curChk = "<input id='f" + id + "' onclick='selChangepro(" + id + ")' type='checkbox' value='" + jQuery("#list3").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "<a onclick='CheckProduct(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list3").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list3").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid1, status) {
            if (oldproID != 0) {
                $('input[id=f' + oldproID + ']').prop("checked", false);
            }
            $('input[id=f' + rowid1 + ']').prop("checked", true);
            oldproID = rowid1;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (curPagePro == $("#list1").getGridParam("lastpage"))
                    return;
                curPagePro = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPagePro = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPagePro == 1)
                    return;
                curPagePro = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPagePro = 1;
            }
            else {
                curPagePro = $("#pager3 :input").val();
            }
            reloadpro();
        },
        loadComplete: function () {
            // $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqSer() {
    jQuery("#list4").jqGrid({
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
                var cancel = "<a onclick='ServerOut(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
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
            //$("#list4").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
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
            //  $("#list9").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list9").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPlanPro() {
    jQuery("#list5").jqGrid({
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
        { name: 'CreateTime', index: 'CreateTime', width: 100 },
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
                var curChk = "<input id='g" + id + "' onclick='selChangePlanPro(" + id + ")' type='checkbox' value='" + jQuery("#list5").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "<a onclick='DownFile(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list5").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list5").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid3, status) {
            if (oldplanPro != 0) {
                $('input[id=g' + oldplanPro + ']').prop("checked", false);
            }
            $('input[id=g' + rowid3 + ']').prop("checked", true);
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
            //  $("#list5").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list5").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPlanSer() {
    jQuery("#list6").jqGrid({
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
        { name: 'CreateTime', index: 'CreateTime', width: 100 },
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
                var curRowData = jQuery("#list4").jqGrid('getRowData', id);
                var curChk = "<input id='k" + id + "' onclick='selChangePlanSer(" + id + ")' type='checkbox' value='" + jQuery("#list6").jqGrid('getRowData', id).SID + "' name='cb'/>";
                var cancel = "<a onclick='DownZhenshu(" + id + ")' style='color:blue;cursor:pointer;'>查看</a>";
                jQuery("#list6").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list6").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid4, status) {
            if (oldplanSer != 0) {
                $('input[id=k' + oldplanSer + ']').prop("checked", false);
            }
            $('input[id=k' + rowid4 + ']').prop("checked", true);
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
                curPageZS = $("#pager6 :input").val();
            }
            reloadPSer();
        },
        loadComplete: function () {
            //$("#list6").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
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
//删除产品信息
function deleteTr(id) {
    var curRowData = jQuery("#list3").jqGrid('getRowData', id);
    var Time = curRowData.CreateTime;
    var SID = curRowData.SID;
    var one = confirm("确实要删除产品信息吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deletepPro",
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
        $("#list").trigger('reloadGrid');
    }
}
function deleteSer(id) {
    var curRowData = jQuery("#list4").jqGrid('getRowData', id);
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
        $("#list4").trigger('reloadGrid');
        $("#list").trigger('reloadGrid');
    }
}
function deleteFile(id) {
    var curRowData = jQuery("#list5").jqGrid('getRowData', id);
    var Time = curRowData.CreateTime;
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
        $("#list5").trigger('reloadGrid');
    }
}
function deleteCertificate(id) {
    var curRowData = jQuery("#list6").jqGrid('getRowData', id);
    var Time = curRowData.CreateTime;
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
                    $("#list6").trigger('reloadGrid');
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
        $("#list6").trigger('reloadGrid');
    }
}
//更新产品信息
function UpdatePro(id) {
    var model = jQuery("#list3").jqGrid('getRowData', id);
    var Time = model.CreateTime;
    var sid = model.SID;
    window.parent.OpenDialog("更新产品信息", "../SuppliesManage/UPProduct?sid=" + sid + "&time=" + Time, 900, 250, '');
}
//修改服务
function UpdateSer(id) {
    var model = jQuery("#list4").jqGrid('getRowData', id);
    var time = model.CreateTime;
    var sid = model.SID;
    window.parent.OpenDialog("更新服务信息", "../SuppliesManage/UPServer?sid=" + sid + "&time=" + time, 450, 250, '');
}
//修改资质
function UpdateFiles(id) {
    var model = jQuery("#list5").jqGrid('getRowData', id);
    var time = model.CreateTime;
    var sid = model.SID;
    var fid = model.FID;
    window.parent.OpenDialog("更新资质文件信息", "../SuppliesManage/UPFile?sid=" + sid + "&fid=" + fid + "&time=" + time, 800, 220, '');
}
//修改证书
function UpdateCertify(id) {
    var model = jQuery("#list6").jqGrid('getRowData', id);
    var time = model.CreateTime;
    var sid = model.SID;
    window.parent.OpenDialog("更新证书信息", "../SuppliesManage/UPCertifify?sid=" + sid + "&time=" + time, 900, 200, '');
}

function ShowDetail(sid) {
    window.parent.OpenDialog("详细内容", "../SuppliesManage/DetailApp?sid=" + sid, 700, 700, '');
}
//公司级审批产生pid，
function Zhunchu() {
    var job = $("#Exjob").val();
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    if (rowid == null) {
        alert("您还没有选择要处理的会签评审");
        return;
    }
    else if (job != "副总经理" && job != "总经理" && job != "董事长") {
        //alert("只有总工程师才能会签审批");
        return;
    }
    else {
        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;//现在只有创建
        var SID = jQuery("#list").jqGrid('getRowData', rowid).SIDS;
        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (state == $("#Nostate").val()) {
            alert("审批不通过，不能进行审批了");
            return;
        }
        if (state !== "待会签审批") {

            return;
        }
        $.ajax({
            url: "../SuppliesManage/JudgeAppDisable",
            type: "post",
            data: { data1: $("#webkey").val(), SPID: SPID, Job: job },
            dataType: "Json",
            success: function (data) {

                if (data.success == "true") {
                    var bol = data.intblo;//当前是副总经理登陆可以审批，其他不能
                    if (bol == "-1" && job == "副总经理") {
                        window.parent.OpenDialog("会签评审", "../SuppliesManage/ApprovalSup?id=" + SID, 900, 500, '');
                        return;
                    }

                    if (bol == "1" && job == "总经理") {//总经理审批

                        window.parent.OpenDialog("会签评审", "../SuppliesManage/ApprovalSup?id=" + SID, 900, 500, '');
                        return;
                    }

                    if (bol == "2" && job == "董事长") {//董事长审批

                        window.parent.OpenDialog("会签评审", "../SuppliesManage/ApprovalSup?id=" + SID, 900, 500, '');
                        return;
                    }

                }
                else {
                    alert("还没到该人员审批"); return;
                }

            }
        });

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
        // var texts = $("#webkey").val() + "@" + SPID + "@" + SID;
        //   window.parent.OpenDialog("会签评审", "../COM_Approval/ApprovalSup?id=" + texts, 600, 300, '');
        //    }
        //    else {
        //        return;
        //    }
        //}
        // });
    }
}

function Cancel() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var state = jQuery("#list").jqGrid('getRowData', rowid).State;
    if (rowid == null) {
        alert("您还没有选择要重新提交的供应商？");
        return;
    }
    else if (state != "最终评审未通过" && state != "会签审批未通过") {
        alert("不能进行重新提交操作"); return;
    }
    else {
        var one = confirm("确实要重新提交供应商吗");
        if (one == false) {
            return;
        } else {
            $.ajax({
                url: "RestSup",
                type: "post",
                data: { data1: SID },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        alert(data.Msg);
                        reload();
                        $("#list").trigger('reloadGrid');
                        //$("#list1").trigger('reloadGrid');
                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });
            $("#list").trigger('reloadGrid');
            // $("#list1").trigger('reloadGrid');
        }

    }
}
function CheckProduct(id) {
    var model = jQuery("#list3").jqGrid('getRowData', id);
    var Proid = model.ID;//唯一编号
    var filename = model.FFileName;
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var deptname = jQuery("#list").jqGrid('getRowData', rowid).DeptName;
    // var job = $("#Department").val();
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