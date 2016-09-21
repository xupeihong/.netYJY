var curPage = 1;
var RcurPage = 1;
var OnePageCount = 5;
var DcurPage = 1;
var ZcurPage = 1;
var ScurPage = 1;
var oldSelID = 0;
var id = 0;
var ID = 0;
var DID = 0;
var LogID = 0;
var OID = 0;
var objData = '';
var SalesProduct = "";
var SpecsModels = "";
var ApplyMan = "";
var StartDate = "";
var EndDate = "";
var OrderUnit = "";
var SendRemark = "";
$(document).ready(function () {
    var userRole = $("#UserRole").val();
    var ExJob = $("#ExJob").val();

    if (userRole.indexOf(",4,") != "-1" && ExJob == "") {
        $("#divOperate").css("display", "block");
    }
    else {
        $("#divOperate").css("display", "none");
    }
    $("#pageContent").height($(window).height());
    document.getElementById('div1').style.display = 'block';//loadlist
    // document.getElementById('loadlist').style.display = 'block';
    document.getElementById('RZJ').style.display = 'none';
    document.getElementById('RZDIV').style.display = 'none';
    var op = $("#hdOp").val();
    if (op == "NG") {
        $("#SpTitle").html("内购管理");
        $("#btnRecord").val("内购申请");
        LoadBasInfo();
    }
    else if (op == "ZS") {
        $("#SpTitle").html("赠送管理");
        $("#btnRecord").val("赠送申请");
        LoadZSInfo();
    }
    LoadSPInfo('');
    //LoadDetail('',op);
    LoadLog();
    $("#btnRecord").click(function () {
        if (op == "NG") {
            window.parent.OpenDialog("内购申请", "../SalesRetail/ApplyInternal?op=NG", 1000, 500, '');
        }
        else if (op == "ZS") {
            window.parent.OpenDialog("赠送申请", "../SalesRetail/ApplyInternal?op=ZS", 1000, 500, '');
        }
    });

    $("#btnPrintSP").click(function () {
        var TaskType = "";
        if (op == "NG")
            TaskType = "Internal";
        else if (op == "ZS")
            TaskType = "Send";

        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要打印的申请记录...");
            return;
        }
        else {
            var PID = jQuery("#list").jqGrid("getRowData", rowId).IOID;
            window.showModalDialog("../SalesRetail/PrintSPInfo?PID=" + PID + "&TaskType=" + TaskType, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    $("#btnPrint").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要打印的申请记录...");
            return;
        }
        else {
            var IOID = jQuery("#list").jqGrid("getRowData", rowId).IOID;
            window.showModalDialog("../SalesRetail/PrintInternal?IOID=" + IOID + "&OP=" + op, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    $('#btnDetail').click(function () {
        this.className = "btnTw";
        $('#QQJQdiv').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        // document.getElementById('div1').style.display = 'block';
        document.getElementById('div1').style.display = 'block';
        document.getElementById('RZJ').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'none';
    });

    $('#QQJQdiv').click(function () {
        this.className = "btnTw";
        $('#RZXX').attr("class", "btnTh");
        $('#btnDetail').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('RZJ').style.display = 'block';
        document.getElementById('RZDIV').style.display = 'none';
    });

    $("#RZXX").click(function () {
        this.className = "btnTw";
        $('#QQJQdiv').attr("class", "btnTh");
        $('#btnDetail').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('RZJ').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'block';
    })
    $("#btnUpdate").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要修改的申请记录...");
            return;
        }
        else {
            var IOID = jQuery("#list").jqGrid("getRowData", rowId).IOID;
            if (op == "NG") {
                var State = jQuery("#list").jqGrid("getRowData", rowId).State;
                if (State == "审批完成") {
                    alert("审批完成不能修改");
                    return;
                }
                window.parent.OpenDialog("修改内购申请", "../SalesRetail/UpdateInternal?IOID=" + IOID + "&op=" + op, 1000, 500, '');
            }
            else if (op == "ZS") {
                window.parent.OpenDialog("修改赠送申请", "../SalesRetail/UpdateInternal?IOID=" + IOID + "&op=" + op, 1000, 500, '');
            }
        }
    });

    $("#btnSP").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要进行审批的内购申请单");
            return false;
        }
        else {
            var texts = "";
            if (op == "NG") {
                var State = jQuery("#list").jqGrid("getRowData", rowId).State;
                if (State == "审批中") {
                    alert("不能重复提交审批");
                    return;
                }
                if (State == "审批完成") {
                    alert("不能重复提交审批");
                    return;
                }
                texts = jQuery("#list").jqGrid('getRowData', rowId).IOID + "@" + "内购审批" + "@" + "内购管理";
            }
            else if (op == "ZS") {

                var State = jQuery("#list").jqGrid("getRowData", rowId).State;
                if (State == "审批中") {
                    alert("不能重复提交审批");
                    return;
                }
                if (State == "审批完成") {
                    alert("不能重复提交审批");
                    return;
                }
                texts = jQuery("#list").jqGrid('getRowData', rowId).IOID + "@" + "赠送审批" + "@" + "赠送管理";
            }

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
        var State = $("#list").jqGrid("getRowData", rowId).State;
        if (State == "审批完成") {
            alert("审批完成不能撤销");
            return;
        }
        if (rowId == null) {
            alert("请选择要撤销的内购申请...");
            return;
        }
        else {
            var IOID = jQuery("#list").jqGrid("getRowData", rowId).IOID;
            if (confirm("是否确定要撤销编号为" + IOID + "的申请信息?")) {
                $.ajax({
                    url: "DeleteInternal",
                    type: "post",
                    data: { IOID: IOID },
                    dataType: "Json",
                    success: function (data) {
                        if (data.success == "true") {
                            window.parent.frames["iframeRight"].reload();
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
        OrderUnit = $("#OrderUnit").val();
        SendRemark = $("#SendRemark").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetInternalGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, SalesProduct: SalesProduct, SpecsModels: SpecsModels, ApplyMan: ApplyMan, StartDate: StartDate, EndDate: EndDate, OP: op, OrderUnit: OrderUnit, SendRemark: SendRemark },

        }).trigger("reloadGrid");
    }
}

function LoadBasInfo() {
    var op = "0";
    SalesProduct = $("#SalesProduct").val();
    SpecsModels = $("#SpecsModels").val();
    ApplyMan = $("#ApplyMan").val();
    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();
    jQuery("#list").jqGrid({
        url: 'GetInternalGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, SalesProduct: SalesProduct, SpecsModels: SpecsModels, ApplyMan: ApplyMan, StartDate: StartDate, EndDate: EndDate, OP: op },
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
        colNames: ['申请编号', '内购产品', '申请人', '申请日期', '出库仓库', '主管负责人', '内购公司商品使用人', '状态', '所属单位'],
        colModel: [
        { name: 'IOID', index: 'IOID', width: 140 },
        { name: 'OrderContent', index: 'OrderContent', width: 200 },
        { name: 'Applyer', index: 'Applyer', width: 130 },
        { name: 'OrderDate', index: 'OrderDate', formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' }, width: 150 },
        { name: 'Warehouse', index: 'Warehouse', width: 150 },
        { name: 'Steering', index: 'Steering', width: 150 },
        { name: 'GoodsUser', index: 'GoodsUser', width: 200 },
        { name: 'State', index: 'State', width: 150 },
        { name: 'UnitName', index: 'UnitName', width: 150 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var order1 = "";
                var OrderContent = jQuery("#list").getCell(ids[i], "OrderContent");
                var arrContent = OrderContent.split(',');
                if (arrContent.length > 1) {
                    for (var j = 0; j < arrContent.length; j++) {
                        order1 += arrContent[j] + "\n";
                    }
                    $("#list").jqGrid('setRowData', ids[i], { OrderContent: order1 });
                }
                else {
                    $("#list").jqGrid('setRowData', ids[i], { OrderContent: OrderContent });
                }
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var orderID = jQuery("#list").jqGrid("getRowData", rowid).IOID;
            LogID = orderID;
            LoadDetail(orderID, '0');
            reload3(orderID);
            reload7();
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

function LoadZSInfo() {
    var op = "1";
    SalesProduct = $("#SalesProduct").val();
    SpecsModels = $("#SpecsModels").val();
    ApplyMan = $("#ApplyMan").val();
    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();
    jQuery("#list").jqGrid({
        url: 'GetInternalGrid',
        datatype: 'json',
        postData: { curpage: ZcurPage, rownum: OnePageCount, SalesProduct: SalesProduct, SpecsModels: SpecsModels, ApplyMan: ApplyMan, StartDate: StartDate, EndDate: EndDate, OP: op },
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
        colNames: ['申请编号', '赠送产品', '申请人', '申请日期', '出库仓库', '客户地址', '提货收款人', '赠送原因', '赠送部门', '赠送备注', '状态', '所属单位'],
        colModel: [
        { name: 'IOID', index: 'IOID', width: 140 },
        { name: 'OrderContent', index: 'OrderContent', width: 200 },
        { name: 'Applyer', index: 'Applyer', width: 130 },
        { name: 'OrderDate', index: 'OrderDate', width: 150 },
        { name: 'Warehouse', index: 'Warehouse', width: 100 },
        { name: 'Address', index: 'Address', width: 150 },
        { name: 'Recipiments', index: 'Recipiments', width: 120 },
        { name: 'SendReason', index: 'SendReason', width: 200 },
        { name: 'SendDepartment', index: 'SendDepartment', width: 200 },
        { name: 'SendRemark', index: 'SendRemark', width: 200 },
        { name: 'State', index: 'State', width: 90 },
        { name: 'UnitName', index: 'UnitName', width: 150 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var order1 = "";
                var OrderContent = jQuery("#list").getCell(ids[i], "OrderContent");
                var arrContent = OrderContent.split(',');
                if (arrContent.length > 1) {
                    for (var j = 0; j < arrContent.length; j++) {
                        order1 += arrContent[j] + "\n";
                    }
                    $("#list").jqGrid('setRowData', ids[i], { OrderContent: order1 });
                }
                else {
                    $("#list").jqGrid('setRowData', ids[i], { OrderContent: OrderContent });
                }
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var orderID = jQuery("#list").jqGrid("getRowData", rowid).IOID;
            LoadDetail(orderID, "1");
            reload3(orderID);
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (ZcurPage == $("#list").getGridParam("lastpage"))
                    return;
                ZcurPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                ZcurPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (ZcurPage == 1)
                    return;
                ZcurPage = $("#list").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                ZcurPage = 1;
            }
            else {
                ZcurPage = $("#pager :input").val();
            }
            reload(op);
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 146, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadDetail(IOID, op) {
    if (op == "0") {
        $("#loadlist").jqGrid('GridUnload');
        document.getElementById('div1').style.display = 'block';
        jQuery("#loadlist").jqGrid({
            url: 'GetInternalDetail',
            datatype: 'json',
            postData: { IOID: IOID, curpage: DcurPage, rownum: OnePageCount },
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
            colNames: ['序号', '', '', '产品名称', '产品类型', '规格型号', '数量', '零售价', '折扣', '总价', '付款方式', '备注'],
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
            { name: 'Total', index: 'Total', width: 80 },
            { name: 'PayWay', index: 'PayWay', width: 120 },
            { name: 'Remark', index: 'Remark', width: 200 }
            ],
            pager: jQuery('#pager1'),
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
                    if ($("#loadlist").jqGrid('getRowData', ids[i]).PayWay == "安装后付款") {
                        PayWay = "<label style='color:Red;'>安装后付款</label>";
                        $("#loadlist").jqGrid('setRowData', ids[i], { PayWay: PayWay });
                    }
                }
            },
            onSelectRow: function (rowid, status) {

            },

            onPaging: function (pgButton) {
                if (pgButton == "next_pager1") {
                    if (DcurPage == $("#loadlist").getGridParam("lastpage"))
                        return;
                    DcurPage = $("#loadlist").getGridParam("page") + 1;
                }
                else if (pgButton == "last_pager1") {
                    DcurPage = $("#loadlist").getGridParam("lastpage");
                }
                else if (pgButton == "prev_pager1") {
                    if (DcurPage == 1)
                        return;
                    DcurPage = $("#loadlist").getGridParam("page") - 1;
                }
                else if (pgButton == "first_pager1") {
                    DcurPage = 1;
                }
                else {
                    DcurPage = $("#pager1 :input").val();
                }
                reload1(IOID);
            },
            loadComplete: function () {
                $("#loadlist").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
                $("#loadlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
            }
        });
    }
    else if (op == "1") {
        $("#loadlist").jqGrid('GridUnload');
        document.getElementById('div1').style.display = 'block';
        jQuery("#loadlist").jqGrid({
            url: 'GetInternalDetail',
            datatype: 'json',
            postData: { IOID: IOID, curpage: DcurPage, rownum: OnePageCount },
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
            colNames: ['序号', '', '', '产品名称', '产品类型', '规格型号', '数量', '零售价', '备注'],
            colModel: [
            { name: 'ID', index: 'ID', align: 'center', width: 30 },
            { name: 'IOID', index: 'IOID', width: 90, hidden: true },
            { name: 'DID', index: 'DID', width: 90, hidden: true },
            { name: 'OrderContent', index: 'OrderContent', width: 200 },
            { name: 'GoodsType', index: 'GoodsType', width: 120 },
            { name: 'Specifications', index: 'Specifications', width: 120 },
            { name: 'Amount', index: 'Amount', width: 80 },
            { name: 'UnitPrice', index: 'UnitPrice', width: 80 },
            //{ name: 'Discounts', index: 'Discounts', width: 80 },
            //{ name: 'Total', index: 'Total', width: 80 },
            //{ name: 'PayWay', index: 'PayWay', width: 120 },
            { name: 'Remark', index: 'Remark', width: 200 }
            ],
            pager: jQuery('#pager1'),
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
                    if ($("#loadlist").jqGrid('getRowData', ids[i]).PayWay == "安装后付款") {
                        PayWay = "<label style='color:Red;'>安装后付款</label>";
                        $("#loadlist").jqGrid('setRowData', ids[i], { PayWay: PayWay });
                    }
                }
            },
            onSelectRow: function (rowid, status) {

            },

            onPaging: function (pgButton) {
                if (pgButton == "next_pager1") {
                    if (DcurPage == $("#loadlist").getGridParam("lastpage"))
                        return;
                    DcurPage = $("#loadlist").getGridParam("page") + 1;
                }
                else if (pgButton == "last_pager1") {
                    DcurPage = $("#loadlist").getGridParam("lastpage");
                }
                else if (pgButton == "prev_pager1") {
                    if (DcurPage == 1)
                        return;
                    DcurPage = $("#loadlist1").getGridParam("page") - 1;
                }
                else if (pgButton == "first_pager") {
                    DcurPage = 1;
                }
                else {
                    DcurPage = $("#pager1 :input").val();
                }
                reload1(IOID);
            },
            loadComplete: function () {
                $("#loadlist").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
                $("#loadlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
            }
        });
    }
}

function reload1(IOID, op) {

    var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#loadlist").jqGrid('setGridParam', {
        url: 'GetInternalDetail',
        datatype: 'json',
        postData: { IOID: IOID, curpage: DcurPage, rownum: OnePageCount },

    }).trigger("reloadGrid");
}

function LoadSPInfo(PID) {
    var webkey = $('#webkey').val();
    var folderBack = $('#folderBack').val();
    jQuery("#list2").jqGrid({
        url: 'ConditionGrid',
        datatype: 'json',
        postData: { curpage: DcurPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },
        // loadonce: false,
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
        pager: jQuery('#pager2'),
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
        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (ScurPage == $("#list2").getGridParam("lastpage"))
                    return;
                ScurPage = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                ScurPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (ScurPage == 1)
                    return;
                ScurPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                ScurPage = 1;
            }
            else {
                ScurPage = $("#pager2 :input").val();
            }
            reload3(PID);
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

function reload3(PID) {
    var webkey = $('#webkey').val();
    var folderBack = $('#folderBack').val();
    $("#list2").jqGrid('setGridParam', {
        url: 'ConditionGrid',
        datatype: 'json',
        postData: { curpage: ScurPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}

function LoadLog() {
    jQuery("#RZlist").jqGrid({
        url: 'GetLogGrid',
        datatype: 'json',
        postData: { curpage: RcurPage, rownum: OnePageCount, ID: LogID },
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
        colNames: ['', '日志名称', '日志类型', '操作人', '操作单位', '操作时间'],
        colModel: [
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'LogContent', index: 'LogContent', width: 90 },
        { name: 'ProductType', index: 'ProductType', width: 90 },
        { name: 'Actor', index: 'Actor', width: 90 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'LogTime', index: 'LogTime', width: 100 }
        ],
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#FJXXlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                //var curRowData = jQuery("#FJXXlist").jqGrid('getRowData', id);
                //var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#FJXXlist").jqGrid('getRowData', id).PID + "' name='cb'/>";
                //jQuery("#FJXXlist").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            //ID = jQuery("#list").jqGrid('getRowData', rowid).EID//0812k
            //OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
            //select(rowid);
            //$("#Billlist tbody").html("");
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager6") {
                if (RcurPage == $("#RZlist").getGridParam("lastpage"))
                    return;
                RcurPage = $("#RZlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager6") {
                RcurPage = $("#RZlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager6") {
                if (RcurPage == 1)
                    return;
                RcurPage = $("#RZlist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager6") {
                RcurPage = 1;
            }
            else {
                RcurPage = $("#pager6 :input").val();
            }
            reload7();
        },
        loadComplete: function () {
            $("#RZlist").jqGrid("setGridHeight", $("#pageContent").height() + 80, false);
            $("#RZlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload7() {
    $("#RZlist").jqGrid('setGridParam', {
        url: 'GetLogGrid',
        datatype: 'json',
        postData: {
            curpage: RcurPage, rownum: OnePageCount, ID: LogID
        },
        //loadonce: false

    }).trigger("reloadGrid");//重新载入
}
