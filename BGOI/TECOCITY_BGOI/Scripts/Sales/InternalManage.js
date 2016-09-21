var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var id = 0;
var ID = 0;
var DID = 0;
var OID = 0;
var objData = '';
var SalesProduct = "";
var SpecsModels = "";
var ApplyMan = "";
var StartDate = "";
var EndDate = "";
$(document).ready(function () {
    var op = $("#hdOp").val();
    if (op == "BA") {
        $("#SpTitle").html("项目管理");
        $("#btnRecord").val("备案申请");
        LoadBasInfo();
    }
    else if (op == "BJ") {
        $("#SpTitle").html("报价管理");
        $("#btnRecord").val("报价申请");
        LoadBJInfo();
    }
    else if (op == "DD") {
        $("#SpTitle").html("订单管理");
        $("#btnRecord").val("订单申请");
        LoadDDInfo();
    }
    $("#btnRecord").click(function () {
        if (op == "BA") {
            window.parent.OpenDialog("备案申请", "../SalesManage/ApplyInternal?op=NG", 900, 500, '');
        }
        else if (op == "BJ") {
            window.parent.OpenDialog("报价申请", "../SalesManage/ApplyInternal?op=ZS", 900, 500, '');
        }
        else if (op == "DD") {
            window.parent.OpenDialog("订单申请", "../SalesManage/ApplyInternal?op=ZS", 900, 500, '');
        }
    });

    $('#btnDetail').click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择备案申请...");
            return;
        }
        else {
            var orderID = jQuery("#list").jqGrid("getRowData", rowId).IOID;
            LoadDetail(orderID);
            //reload1(orderID);
            this.className = "btnTw";
            $("#div1").css("display", "");
        }
    })

    $("#btnUpdate").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要修改的备案申请...");
            return;
        }
        else {
            var IOID = jQuery("#list").jqGrid("getRowData", rowId).IOID;
            window.parent.OpenDialog("修改备案申请", "../SalesRetail/UpdateInternal?IOID=" + IOID + "&op=" + op, 900, 500, '');
        }
    });

    $("#btnSP").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要进行审批的备案申请单");
            return false;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowId).IOID + "@" + "备案审批";
            var IOID = jQuery("#list").jqGrid('getRowData', rowId).IOID;

            window.parent.OpenDialog("提交审批", "../SalesRetail/ApprovalCommon?PID=" + IOID + "&id=" + texts, 700, 500, '');
            //window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
        }
    });

    $("#btnSearch").click(function () {
        reload();
    });

    $("#btnCancel").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要撤销的备案申请...");
            return;
        }
        else {
            var IOID = jQuery("#list").jqGrid("getRowData", rowId).IOID;
            if (confirm("是否确定要撤销编号为" + orderID + "的备案申请信息?")) {
                $.ajax({
                    url: "DeleteInternal",
                    type: "post",
                    data: { IOID: IOID },
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
            }
        }
    });
});


function reload(op) {
    if ($('.field-validation-error').length == 0) {
        SalesProduct = $("#SalesProduct").val();
        SpecsModels = $("#SpecsModels").val();
        ApplyMan = $("#ApplyMan").val();
        StartDate = $("#StartDate").val();
        EndDate = $("#EndDate").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetInternalGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, SalesProduct: SalesProduct, SpecsModels: SpecsModels, ApplyMan: ApplyMan, StartDate: StartDate, EndDate: EndDate, OP: op },

        }).trigger("reloadGrid");
    }
}

function LoadBasInfo() {
    var op = "0";
    //PID = $("#PID").val();
    //SpecsModels = $("#SpecsModels").val();
    //PlanName = $("#PlanName").val();
    //StartDate = $("#StartDate").val();
    //EndDate = $("#EndDate").val();

    var ProjectName = $('#PlanName').val();
    var PlanID = $('#PlanID').val();
    var RecordContent = $('#RecordContent').val();
    var SpecsModels = $('#SpecsModels').val(); //$('#SpecsModels').click.Text();
    var BelongArea = $('#BelongArea').val();
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    var WorkChief = $('#WorkChief').val();
    var State = $("input[name='State']:checked").val();
    var HState = $("input[name='HState']:checked").val();
    jQuery("#list").jqGrid({
        url: 'GetSearchData',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, ProjectName: ProjectName, PlanID: PlanID,
            RecordContent: RecordContent, SpecsModels: SpecsModels, BelongArea: BelongArea,
            StartDate: StartDate, EndDate: EndDate, OP: op
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
        colNames: ['', '信息日期', '项目名称', '工程编号', '内容', '规格型号', '业务负责人', '所属区域', '渠道来源', '进度'],
        colModel: [
         //{ name: 'IDCheck', index: 'Id', width: 20 },
         { name: 'PID', index: 'PID', width: 90, hidden: true },
         { name: 'recorddate', index: 'recorddate', width: 100 },
         { name: 'planname', index: 'planname', width: 80 },
         { name: 'planid', index: 'planid', width: 80 },
         { name: 'MainContent', index: 'MainContent', width: 150 },
         { name: 'SpecsModels', index: 'SpecsModels', width: 150 },
         { name: 'WorkChief', index: 'WorkChief', width: 70 },
         { name: 'BelongArea', index: 'BelongArea', width: 50 },
         { name: 'ChannelsFrom', index: 'ChannelsFrom', width: 100 },
         { name: 'State', index: 'State', width: 100 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var orderID = jQuery("#list").jqGrid("getRowData", rowid).IOID;
            LoadDetail(orderID);
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
            reload(op);
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 146, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadBJInfo() {
    var op = "1";
    var ProjectName = $('#PlanName').val();
    var PlanID = $('#PlanID').val();
    var OfferTitle = $('#OfferTitle').val();
    var BelongArea = $('#BelongArea').val();
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    var OfferContacts = $('#Manager').val();
    var State = $("input[name='State']:checked").val();
    var HState = $("input[name='HState']:checked").val();
    jQuery("#list").jqGrid({
        url: 'GetSearchOfferGrid',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            PlanName: ProjectName, PlanID: PlanID,
            OfferTitle: OfferTitle, BelongArea: BelongArea, StartDate: StartDate,
            EndDate: EndDate, Manager: OfferContacts, State: State, HState: HState, OP: op
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
        colNames: ['报价单号', '项目单号', '报价标题', '报价说明', '报价人', '报价单位', '报价金额', '报价时间', '进度'],
        colModel: [
        { name: 'BJID', index: 'BJID', width: 90 },
        { name: 'offerPID', index: 'offerPID', width: 90 },
        { name: 'OfferTitle', index: 'OfferTitle', width: 90 },
        { name: 'Description', index: 'Description', width: 100 },
        { name: 'OfferContacts', index: 'OfferContacts', width: 80 },
        { name: 'OfferUnit', index: 'OfferUnit', width: 80 },
        { name: 'Total', index: 'Total', width: 150 },
        { name: 'OfferTime', index: 'OfferTime', width: 150 },
        { name: 'State', index: 'State', width: 70 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var orderID = jQuery("#list").jqGrid("getRowData", rowid).IOID;
            LoadDetail(orderID);
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
            reload(op);
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 146, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadDDInfo() {
    var op = "1";
    var ContractID = $('#ContractID').val();
    var OrderUnit = $('#OrderUnit').val();
    var UseUnit = $('#UseUnit').val();
    var OrderContent = $('#MainContent').val(); //$('#SpecsModels').click.Text();
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    var State = $("input[name='State']:checked").val();
    var HState = $("input[name='HState']:checked").val();
    jQuery("#list").jqGrid({
        url: 'GetSearchOrderInfo',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, ContractID: ContractID, OrderUnit: OrderUnit,
            UseUnit: UseUnit, MainContent: OrderContent, StartDate: StartDate,
            EndDate: EndDate, State: State, HState: HState, OP: op
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
        colNames: ['', '', '订单编号', '订货单位', '联系人', '联系方式', '使用单位', '联系人', '联系人方式', '交货日期', '状态'],
        colModel: [
        { name: 'PID', index: 'PID', width: 100, hidden: true },
        { name: 'ContractID', index: 'ContractID', width: 100, hidden: true },
        { name: 'OrderID', index: 'OrderID', width: 100 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 100 },
        { name: 'OrderContactor', index: 'OrderContactor', width: 100 },
        { name: 'OrderTel', index: 'OrderTel', width: 100 },
        { name: 'UseUnit', index: 'UseUnit', width: 100 },
        { name: 'UseContactor', index: 'UseContactor', width: 100 },
        { name: 'UseTel', index: 'UseTel', width: 100 },
        { name: 'DeliveryTime', index: 'DeliveryTime', width: 100 },
        { name: 'State', index: 'State', width: 100 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var orderID = jQuery("#list").jqGrid("getRowData", rowid).IOID;
            LoadDetail(orderID);
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
            reload(op);
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 146, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });

}

function LoadDetail(IOID) {
    $("#loadlist").jqGrid('GridUnload');
    document.getElementById('div1').style.display = 'block';
    //$("#Orderlist").attr("style", "display:none;");
    jQuery("#loadlist").jqGrid({
        url: 'GetInternalDetail',
        datatype: 'json',
        postData: { IOID: IOID, curpage: curPage, rownum: OnePageCount },
        loadonce: true,
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
        colNames: ['序号', '', '', '产品名称', '产品类型', '规格型号', '数量', '零售价', '折扣', '总价'],
        colModel: [
        { name: 'ID', index: 'ID', align: 'center', width: 30 },
        { name: 'IOID', index: 'IOID', width: 90, hidden: true },
        { name: 'DID', index: 'DID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 200 },
        { name: 'GoodsType', index: 'GoodsType', width: 120 },
        { name: 'Specifications', index: 'Specifications', width: 120 },
        { name: 'Amount', index: 'Amount', width: 80 },
        { name: 'UnitPrice', index: 'UnitPrice', width: 80 },
        { name: 'Discounts', index: 'Discounts', width: 80 },
        { name: 'Total', index: 'Total', width: 80 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        gridComplete: function () {
            var ids = jQuery("#loadlist").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var ID = "" + id + "";
                $("#loadlist").jqGrid('setRowData', ids[i], { ID: ID });
            }
        },
        onSelectRow: function (rowid, status) {

        },

        onPaging: function (pgButton) {
            //if (pgButton == "next_pager") {
            //    if (curPage == $("#loadlist").getGridParam("lastpage"))
            //        return;
            //    curPage = $("#loadlist").getGridParam("page") + 1;
            //}
            //else if (pgButton == "last_pager") {
            //    curPage = $("#loadlist").getGridParam("lastpage");
            //}
            //else if (pgButton == "prev_pager") {
            //    if (curPage == 1)
            //        return;
            //    curPage = $("#loadlist").getGridParam("page") - 1;
            //}
            //else if (pgButton == "first_pager") {
            //    curPage = 1;
            //}
            //else {
            //    curPage = $("#pager :input").val();
            //}
            reload1(IOID);
        },
        loadComplete: function () {
            $("#loadlist").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#loadlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload1(IOID) {

    var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#loadlist").jqGrid('setGridParam', {
        url: 'GetInternalDetail',
        datatype: 'json',
        postData: { IOID: IOID, curpage: curPage, rownum: OnePageCount },

    }).trigger("reloadGrid");
}