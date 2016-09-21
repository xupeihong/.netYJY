var curPage = 1;
var DcurPage = 1;
var RcurPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var id = 0;
var ID = 0;
var DID = 0;
var OID = 0;
var LogID = 0;
var objData = '';
var OrderID = "";
var SpecsModels = "";
var SalesMan = "";
var StartDate = "";
var EndDate = "";
var IsHK = "";
var State = "";
var OrderContactor = "";
var ISCollection = "";
var OrderTel = "";
$(document).ready(function () {
    var userRole = $("#UserRole").val();
    var ExJob = $("#ExJob").val();
    if (userRole.indexOf(",4,") != "-1" && ExJob == "") {
        $("#divOperate").css("display", "block");
    }
    else {
        $("#divOperate").css("display", "none");
    }
    //
    // var shom = window.setInterval("ShowRetailManage();", 1000);
    //
    document.getElementById('RZDIV').style.display = 'none';
    document.getElementById('div1').style.display = 'none';
    LoadBasInfo();
    LoadDetail('');
    LoadSPInfo();
    LoadLog('');
    //ShowDiv();
    //$("#divMessage").attr("style:height","$('#pageContent').height() / 2 - 160")
    $("#pageContent").height($(window).height());
    $("#btnRecord").click(function () {
        window.parent.OpenDialog("销售记录", "../SalesRetail/SalesRecord", 900, 550, '');
    });

    $("#btnUpdate").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要修改的销售记录...");
            return;
        }
        else {
            var orderID = jQuery("#list").jqGrid("getRowData", rowId).OrderID;
            window.parent.OpenDialog("修改销售记录", "../SalesRetail/UpdateSalesRecord?OrderID=" + orderID, 900, 550, '');
        }
    });

    $("#btnSP").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要进行审批的零售销售记录");
            return false;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowId).OrderID + "@" + "零售销售审批";
            var OrderID = jQuery("#list").jqGrid('getRowData', rowId).OrderID;
            //window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
            window.parent.OpenDialog("提交审批", "../SalesRetail/ApprovalCommon?PID=" + OrderID + "&id=" + texts, 700, 500, '');
        }
    });

    $("#btnSearch").click(function () {
        curPage = 1;
        reload();
        LoadDetail("");
        //LoadBasInfo();
    });

    $("#btnPrintInfo").click(function () {
        SalesProduct = $("#SalesProduct").val();
        SpecsModels = $("#SpecsModels").val();
        SalesMan = $("#SalesMan").val();
        StartDate = $("#StartDate").val();
        EndDate = $("#EndDate").val();
        $('input[name=IsHK]:radio:checked').each(function () {
            IsHK = this.value;
        });
        window.showModalDialog("../SalesRetail/PrintRetail?SalesProduct=" + SalesProduct + "&SpecsModels=" + SpecsModels + "&SalesMan=" + SalesMan + "&StartDate=" + StartDate + "&EndDate=" + EndDate + "&IsHK=" + IsHK, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });

    $("#btnDetail").click(function () {
        this.className = "btnTw";
        $('#btnSPInfo').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'block';
        // document.getElementById('div2').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'none';
    });

    $("#btnSPInfo").click(function () {
        this.className = "btnTw";
        $('#btnDetail').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'block';
    });

    $("#RZXX").click(function () {
        this.className = "btnTw";
        // $('#btnSPInfo').attr("class", "btnTh");
        $('#btnDetail').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        // document.getElementById('div2').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'block';
    })

    $("#btnCancel").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");

        if (rowId == null) {
            alert("请选择要撤销的销售记录...");
            return;
        }
        else {
            var orderID = jQuery("#list").jqGrid("getRowData", rowId).OrderID;
            if (confirm("是否确定要撤销编号为" + orderID + "的销售记录?")) {
                $.ajax({
                    url: "DeleteRecord",
                    type: "post",
                    data: { OrderID: orderID },
                    dataType: "Json",
                    success: function (data) {
                        if (data.success == "true") {
                            alert(data.Msg);
                            reload();
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


function reload() {
    if ($('.field-validation-error').length == 0) {
        OrderID = $("#OrderID").val();
        //SpecsModels = $("#SpecsModels").val();
        SalesMan = $("#SalesMan").val();
        StartDate = $("#StartDate").val();
        EndDate = $("#EndDate").val();
        $('input[name=IsHK]:radio:checked').each(function () {
            IsHK = this.value;
        });
        ISCollection = $("input[name='ISCollection']:checked").val();
        // $('input[name=ISCollection]:radio:checked').each(function () {
        //ISCollection = this.value;
        // });
        State = $("#State").val();
        OrderContactor = $("#OrderContactor").val();
        OrderTel = $("#OrderTel").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetSalesRetailAfterSaleGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, OrderID: OrderID, SalesMan: SalesMan, StartDate: StartDate, EndDate: EndDate, OrderContactor: OrderContactor, OrderTel: OrderTel },

        }).trigger("reloadGrid");
    }
}


function LoadBasInfo() {
    SalesProduct = $("#SalesProduct").val();
    //SpecsModels = $("#SpecsModels").val();
    SalesMan = $("#SalesMan").val();
    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();
    //$('input[name=IsHK]:radio:checked').each(function () {
    //    IsHK = this.value;
    //});
    //State = $("#State").val();
    //$("#list").jqGrid('GridUnload');
    jQuery("#list").jqGrid({
        url: 'GetSalesRetailAfterSaleGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, SalesMan: SalesMan, StartDate: StartDate, EndDate: EndDate, OrderContactor: OrderContactor, OrderTel: OrderTel },
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
        colNames: ['订单编号', '操作人','操作时间','销售时间', '销售日期', "售后状态", '最终安装地址', '客户名称', '客户电话', '安装单位', '备注', '业务负责人'],
        colModel: [
            
        { name: 'OrderID', index: 'OrderID', width: 140, align: 'center', },
      { name: 'Operator', index: 'Operator', width: 140, align: 'center', },
           { name: 'OperationTime', index: 'OperationTime', width: 140, align: 'center', },
        { name: 'ContractDate', index: 'ContractDate', width: 150, align: 'center' },
        { name: 'SupplyTime', index: 'SupplyTime', width: 180, align: 'center' },
        {
            name: 'Text', index: 'Text', width: 100
        },
        { name: 'UseAddress', index: 'UseAddress', width: 150, align: 'center' },
        { name: 'OrderContactor', index: 'OrderContactor', width: 150, align: 'center' },
        { name: 'OrderTel', index: 'OrderTel', width: 150, align: 'center' },
        { name: 'OrderUnit', index: 'OrderUnit', width: 150, align: 'center' },
        { name: 'Remark', index: 'Remark', width: 200 },
        {
            name: 'ProvidManager', index: 'ProvidManager', width: 100
        }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            //var ids = jQuery("#list").jqGrid('getDataIDs');
            //for (var i = 0; i < ids.length; i++) {
            //    var id = ids[i];
            //    var order1 = "";
            //    var OrderContent = jQuery("#list").getCell(ids[i], "OrderContent");
            //    var arrContent = OrderContent.split(',');
            //    if (arrContent.length > 1) {
            //        for (var j = 0; j < arrContent.length; j++) {
            //            order1 += arrContent[j] + "\n";
            //        }
            //        $("#list").jqGrid('setRowData', ids[i], { OrderContent: order1 });
            //    }
            //    else {
            //        $("#list").jqGrid('setRowData', ids[i], { OrderContent: OrderContent });
            //    }

            //    var orderContent = "";
            //    var OrderContent = jQuery("#list").getCell(ids[i], "OrderContent");
            //    var arrContent = OrderContent.split(',');
            //    if (arrContent.length > 1) {
            //        for (var k = 0; k < arrContent.length; k++) {
            //            orderContent += arrContent[k] + "\n";
            //        }
            //        $("#list").jqGrid('setRowData', ids[i], { OrderContent: orderContent });
            //    }
            //    else {
            //        $("#list").jqGrid('setRowData', ids[i], { OrderContent: OrderContent });
            //    }

            //    var orderNum = "";
            //    var OrderNum = jQuery("#list").getCell(ids[i], "OrderNum");
            //    var arrNum = OrderNum.split(',');
            //    if (arrNum.length > 1) {
            //        for (var k = 0; k < arrNum.length; k++) {
            //            orderNum += arrNum[k] + "\n";
            //        }
            //        $("#list").jqGrid('setRowData', ids[i], { OrderNum: orderNum });
            //    }
            //    else {
            //        $("#list").jqGrid('setRowData', ids[i], { OrderNum: OrderNum });
            //    }

            //    var unitPrice = "";
            //    var UnitPrice = jQuery("#list").getCell(ids[i], "UnitPrice");
            //    var arrUPrice = UnitPrice.split(',');
            //    if (arrUPrice.length > 1) {
            //        for (var k = 0; k < arrUPrice.length; k++) {
            //            unitPrice += arrUPrice[k] + "\n";
            //        }
            //        $("#list").jqGrid('setRowData', ids[i], { UnitPrice: unitPrice });
            //    }
            //    else {
            //        $("#list").jqGrid('setRowData', ids[i], { UnitPrice: UnitPrice });
            //    }

            //    var specsModels = "";
            //    var SpecsModels = jQuery("#list").getCell(ids[i], "SpecsModels");
            //    var arrSpecs = SpecsModels.split(',');
            //    if (arrSpecs.length > 1) {
            //        for (var k = 0; k < arrSpecs.length; k++) {
            //            specsModels += arrSpecs[k] + "\n";
            //        }
            //        $("#list").jqGrid('setRowData', ids[i], { SpecsModels: specsModels });
            //    }
            //    else {
            //        $("#list").jqGrid('setRowData', ids[i], { SpecsModels: SpecsModels });
            //    }

            //    var productRemark = "";
            //    var ProductRemark = jQuery("#list").getCell(ids[i], "ProductRemark");
            //    var arrPremark = ProductRemark.split(',');
            //    if (arrPremark.length > 1) {
            //        for (var k = 0; k < arrPremark.length; k++) {
            //            productRemark += arrPremark[k] + "\n";
            //        }
            //        $("#list").jqGrid('setRowData', ids[i], { ProductRemark: productRemark });
            //    }
            //    else {
            //        $("#list").jqGrid('setRowData', ids[i], { ProductRemark: ProductRemark });
            //    }

            //    var DTlotal = "";
            //    var DTotalPrice = jQuery("#list").getCell(ids[i], "DTotalPrice");
            //    var arrPrice = DTotalPrice.split(',');
            //    if (arrPrice.length > 1) {
            //        for (var k = 0; k < arrPrice.length; k++) {
            //            DTlotal += arrPrice[k] + "\n";
            //        }
            //        $("#list").jqGrid('setRowData', ids[i], { DTotalPrice: DTlotal });
            //    }
            //    else {
            //        $("#list").jqGrid('setRowData', ids[i], { DTotalPrice: DTotalPrice });
            //    }
            //}
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var orderID = jQuery("#list").jqGrid("getRowData", rowid).OrderID;
            LogID = orderID;
            reload1(orderID);
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
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 146, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadDetail(orderID) {
    $("#loadlist").jqGrid('GridUnload');
    document.getElementById('div1').style.display = 'block';
    //$("#Orderlist").attr("style", "display:none;");
    jQuery("#loadlist").jqGrid({
        url: 'GetDetailGrid',
        datatype: 'json',
        postData: { OrderID: orderID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['序号', '', '', '产品名称', '规格型号', '数量', '成本单价', '金额小计', '款项情况', '所属分公司', '渠道'],
        colModel: [
        { name: 'ID', index: 'ID', align: 'center', width: 30 },
        { name: 'OrderID', index: 'OrderID', width: 90, hidden: true },
        { name: 'DID', index: 'DID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 150 },
        { name: 'SpecsModels', index: 'SpecsModels', width: 120 },
        { name: 'OrderNum', index: 'OrderNum', width: 120 },
        { name: 'UnitPrice', index: 'UnitPrice', width: 120 },
        { name: 'DTotalPrice', index: 'DTotalPrice', width: 120 },
        { name: 'Remark', index: 'Remark', width: 100 },
        { name: 'BelongCom', index: 'BelongCom', width: 100 },
        { name: 'Channel', index: 'Channel', width: 100 }
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
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            DID = jQuery("#loadlist").jqGrid('getRowData', rowid).PID;
            // LoadOrdersInfo();

        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#loadlist").getGridParam("lastpage"))
                    return;
                curPage = $("#loadlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#loadlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#loadlist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager :input").val();
            }
            reload1(orderID);
        },
        loadComplete: function () {
            $("#loadlist").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#loadlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload1(orderID) {

    var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#loadlist").jqGrid('setGridParam', {
        url: 'GetDetailGrid',
        datatype: 'json',
        postData: { OrderID: orderID, curpage: curPage, rownum: OnePageCount },

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


function LoadSPInfo(PID) {
    var webkey = $('#webkey').val();
    var folderBack = $('#folderBack').val();
    jQuery("#list2").jqGrid({
        url: 'ConditionGrid',
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
            if (pgButton == "next_pager1") {
                if (curPage == $("#list2").getGridParam("lastpage"))
                    return;
                curPage = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
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
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}


//添加消息提示和库存售后关联
function ShowRetailManage() {
    var unitId = $("#UnitId").val();
    var SalesType = "";
    if (unitId == "47")
        SalesType = "Project"
    else
        SalesType = "Retail";
    var nowInfo = $("#curContent").html();
    $("#noticecon2").html("");
    $.ajax({
        url: "../SalesRetail/GetTopRetailLibraryTubeManage",
        type: "post",
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data != null) {
                var strHtml = "";
                var myHtml = "";
                var objTask = JSON.parse(data);
                for (var i = 0; i < objTask.length; i++) {
                    if (SalesType == "Project") {
                    }
                    else {
                        strHtml = "销售记录状态有更新";
                        var OrderID = objTask[i]["OrderID"];
                        var OPerator = objTask[i]["Operator"];
                        var OperationContent = objTask[i]["OperationContent"];
                        var OperTime = objTask[i]["OperationTime"];
                        myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesRetail/SalesRetailManage')\">" + strHtml + "记录编号为：" + OrderID + "操作人：" + OPerator + "操作内容" + OperationContent + "操作时间" + OperTime + "</a></br></br>";

                    }
                }


                $("#noticecon2").append(myHtml);
                ShowRetailManageDIV();
            }
            else {
                $("div[id=noticecon2]", "div[id=]").hide();
            }
        }
    });
    $.ajax({
        url: "../SalesRetail/GetTopRetailLibraryTubeManage",
        type: "post",
        dataType: "json",
        async: false, //是否异步
        success: function (data) {

            if (data != null) {
                var strHtml = "";
                var myHtml = "";
                var objTask = JSON.parse(data);
                for (var i = 0; i < objTask.length; i++) {
                    if (SalesType == "Project") {
                    }
                    else {
                        strHtml = "销售记录状态有更新";
                        var OrderID = objTask[i]["OrderID"];
                        var OPerator = objTask[i]["Operator"];
                        var OperationContent = objTask[i]["OperationContent"];
                        var OperTime = objTask[i]["OperationTime"];
                        myHtml += "<span><a id='curContent' style='color:red;' onclick=\"TurnTo('../SalesRetail/SalesRetailManage')\">" + strHtml + "记录编号为：" + OrderID + "操作人：" + OPerator + "操作内容" + OperationContent + "操作时间" + OperTime + "</a></br></br>";

                    }
                }


                $("#noticecon2").append(myHtml);
                ShowRetailManageDIV();
            }
            else {
                $("div[id=noticecon2]", "div[id=]").hide();
            }
        }
    });
}
function ShowRetailManageDIV() {
    $("#divRetailMessage").css({ "right": "0px", "bottom": "1px" });
    $("#divRetailMessage").slideDown("slow");
    $(window).scroll(function () {
        $("#divRetailMessage").css({ "right": "1px" });
        $("#divRetailMessage").css("bottom", "-" + document.documentElement.scrollTop + "px");
    }).resize(function () {
        $("#divRetailMessage").css({ "right": "1px" });
        $("#divRetailMessage").css("bottom", "-" + document.documentElement.scrollTop + "px");
    });

}
function CloseRetailMessage() {
    $("#divRetailMessage").hide();
}

function ShowDiv() {
    var nowInfo = $("#curContent").html();
    $.post("GetNowRemind?SalesType=Retail", function (data) {
        if (data != "") {
            var strHtml = "";
            var arrTask = new Array();
            arrTask = data.split(',');
            for (var i = 0; i < arrTask.length; i++) {
                strHtml += arrTask[i] + " ";
            }

            var myHtml = "<span><a id='curContent' style='color:red;' href='../SalesRetail/SalesRemind?SalesType=Retail'>" + strHtml + "</a></br>";
            $("#noticecon").html(myHtml);
            ShowMessage();
        }
        else {
            $("div[id=noticecon]", "div[id=divMessage]").hide();
        }
    });
}




//function ShowMessage() {
//    $("#divMessage").css({ "right": "0px", "bottom": "1px" });
//    $("#divMessage").slideDown("slow");
//    $(window).scroll(function () {
//        $("#divMessage").css({ "right": "1px" });
//        $("#divMessage").css("bottom", "-" + document.documentElement.scrollTop + "px");
//    }).resize(function () {
//        $("#divMessage").css({ "right": "1px" });
//        $("#divMessage").css("bottom", "-" + document.documentElement.scrollTop + "px");
//    });


//    $("label[id=toclose]").click(function () {
//        $("#divMessage").hide();
//    });

//}