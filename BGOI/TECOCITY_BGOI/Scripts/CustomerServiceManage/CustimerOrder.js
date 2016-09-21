$(document).ready(function () {
    $("#StartDate").val("");
    $("#EndDate").val("");
    LoadOrderInfo();
    //LoadOrderDetail();
    // LoadOrderBill();
    LoadOrderInfosBill();
    jq1();
    LoadExChangeGoods();
    LoadHTXX();
    LoadFJXX();
    document.getElementById('div1').style.display = 'block';
    document.getElementById('div2').style.display = 'none';
    document.getElementById('QQ').style.display = 'none';
    document.getElementById('THDIV').style.display = 'none';
    document.getElementById('HTXXDIV').style.display = 'none';
    document.getElementById('FJXXDIV').style.display = 'none';
    $("#btnAddOrdersInfo").click(function () {
        window.parent.OpenDialog("新增订单", "../CustomerService/AddCustimerOrder", 1000, 580);
        // reload();
    });
    $("#btnAddOrdersInfoF").click(function () {
        window.parent.OpenDialog("新增非常规订单", "../SalesManage/AddOrderF", 1000, 450);
    });
    //审批
    $("#btnSP").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要进行审批的订单");
            return;
        }
        else {
            var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
            var OOState = jQuery("#list").jqGrid('getRowData', rowid).OOstate;
            var State = jQuery("#list").jqGrid('getRowData', rowid).State;
            var ISHT = jQuery("#list").jqGrid('getRowData', rowid).ISHT;
            if (ISF == "1" && (ISHT == "0" || ISHT == null || ISHT == "")) {
                alert("请先上传工业买卖合同");
                return;
            }
            else if (ISF == "1" && ISHT == "1" && OrderJE <= 100000) {
                var texts = DID + "@" + "10万订单审批";
                window.parent.OpenDialog("提交审批", "../SalesManage/SubmitApproval?id=" + texts, 700, 500, '')

            } else if (ISF == "1" && ISHT == "1" && OrderJE > 100000 && OrderJE <= 300000) {
                var texts = DID + "@" + "10万至30万审批";
                window.parent.OpenDialog("提交审批", "../SalesManage/SubmitApproval?id=" + texts, 700, 500, '')
            } else if (ISF == "1" && ISHT == "1" && OrderJE > 300000) {
                var texts = DID + "@" + "30万以上审批";
                window.parent.OpenDialog("提交审批", "../SalesManage/SubmitApproval?id=" + texts, 700, 500, '')

            }
            if (State >= "1") {
                alert("该订单已经提交审批，不能重复操作");
                return;
            }
            //if (OOState == "0") {
            //    // alert("该项目还没有报价，不能进行签订合同操作");
            //    var texts = DID + "@" + "订单审批";
            //    window.parent.OpenDialog("提交审批", "../SalesManage/SubmitApproval?id=" + texts, 700, 500, '')
            //}

        }
    })
    //发货
    $("#btnShipments").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (DID == 0) {
            alert("请选择要进行审批的备案申请单");
            return false;
        }
        else {
            if (State == "-1") {
                alert("审批不通过不能进行下面操作");
                return;
            }
            var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
            var ISHT = jQuery("#list").jqGrid('getRowData', rowid).ISHT;
            if (ISF == "1" && ISHT != "1" && (OOstate != "2" || State != "2")) {
                alert("非常规产品请先审批完成后才能发货");
                return;
            } //else if (DID != "0" && OrderJE > 100000&&State!="2")
                //{
                //    alert("金额大于10万的订单必须审批");
                //    return;
                //}
            else if (DID != "0") {
                window.parent.OpenDialog("发货", "../SalesManage/OrderShipments?OrderID=" + DID, 850, 350, '');
            }
        }


        //if (DID != 0 && OOstate=="2"){
        //    window.parent.OpenDialog("发货", "../SalesManage/OrderShipments?OrderID=" + DID, 850, 350, '');
        //} else {
        //    alert("请选择列表中审批通过的订单数据");
        //    return;
        //}

    });
    //回款
    $("#btnReceivePayment").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要进行结算的订单");
            return;
        }
        if (State == "-1") {
            alert("审批不通过不能进行下面操作");
            return;
        }
        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        var ISHT = jQuery("#list").jqGrid('getRowData', rowid).ISHT;
        //if (ISF == "1" && ISHT == "1" && OOstate != "3"&&OOstate!="4") 
        if (ISF == "1" && ISHT != "1" && (OOstate != "3" || OOstate != "4")) {
            alert("非常规产品请按流程走");
            return;
        } else if (DID != 0) {
            window.parent.OpenDialog("订单回款", "../SalesManage/AddReceivePayment?OrderID=" + DID, 300, 200, '');
        }
        else {
            //  alert("请选择列表中已发货的订单数据");
            // return;
            window.parent.OpenDialog("订单回款", "../SalesManage/AddReceivePayment", 300, 280, '');

        }
        //if (DID != 0 && OOstate == "3") {
        //    window.parent.OpenDialog("订单回款", "../SalesManage/AddReceivePayment?OrderID=" + DID, 300, 280, '');
        //} else {
        //    alert("请选择列表中已发货的订单数据");
        //    return;
        //}

    });
    //结算
    $('#btnJS').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (State == "-1") {
            alert("审批不通过不能进行下面操作");
            return;
        }
        if (rowid == null) {
            alert("您还没有选择要进行结算的订单");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        var ISHT = jQuery("#list").jqGrid('getRowData', rowid).ISHT;
        //  if (ISF == "1" && ISHT == "1" && OOstate != "4") 
        if (ISF == "1" && ISHT != "1" && OOstate != "4") {
            alert("非常规产品请按流程走");
            return;
        }
        else if (DID != "0") {
            window.parent.OpenDialog("订单结算", "../SalesManage/OrdersSettlement?id=" + texts + "&PID=" + PID, 800, 400, '');
        }
        else {
            alert("请选择列表中已发货的订单数据");
            return;
        }
        //if (DID != 0 && OOstate == "4") {
        // window.parent.OpenDialog("订单结算", "../SalesManage/OrdersSettlement?id=" + texts + "&PID=" + PID, 800, 400, '');
        //}
        //else {
        //    alert("请选择列表中已发货的订单数据");
        //    return;
        //}

    })
    $("#btnEXC").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (State == "-1") {
            alert("审批不通过不能进行下面操作");
            return;
        }
        if (DID == 0) {
            alert("请选择列表中订单数据");
            return;
        }
        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        var ISHT = jQuery("#list").jqGrid('getRowData', rowid).ISHT;
        var EXState = jQuery("#list").jqGrid('getRowData', rowid).EXState;
        if (ISF == "1" && ISHT == "1" && OOstate >= "3" && (EXState != "2" || EXState == "")) {
            // alert("非标准产品退换货审批后才能退换货");
            isConfirm = confirm("非标准产品退换货审批后才能退换货")
            if (isConfirm == false) {
                return false;
            }
            else {
                //先审批
                var texts = DID + "@" + "退换货审批";
                window.parent.OpenDialog("提交审批", "../SalesManage/EXSubmitApproval?id=" + texts, 700, 500, '')
            }
            return;
        }// else if (ISF == "1" && ISHT == "1" && OOstate >= "3" && EXState == "2") {
            //    window.parent.OpenDialog("退换货", "../SalesManage/AddExchangeGoods?OrderID=" + DID, 1000, 500, '');
            //}
        else {
            window.parent.OpenDialog("退换货", "../SalesManage/AddExchangeGoods?OrderID=" + DID, 1000, 500, '');
        }

    });
    $("#btnUPOrder").click(function () {
        if (DID == 0) {
            alert("请选择列表中订单数据");
            return;
        }
        window.parent.OpenDialog("修改订单", "../CustomerService/UpdateOrdersInfonew?OrderID=" + DID, 1000, 580);
    })
    //撤销
    $("#btnCancel").click(function () {
        if (DID == 0) {
            alert("请选择列表中订单数据");
            return;
        }
        var msg = "您真的确定要撤销吗?";
        if (confirm(msg) == true) {
            OrdersInfoCanCel();
            return true;
        } else {
            return false;
        }
    });
    $("#DetailXX").click(function () {
        this.className = "btnTw";
        $('#BillXX').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");
        $('#THJL').attr("class", "btnTh");
        // $("#QQ").css("display", "");
        // $("#RZJ").css("display", "none");
        document.getElementById('div1').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('QQ').style.display = 'none';
        document.getElementById('THDIV').style.display = 'none';
    });
    $("#BillXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");
        $('#THJL').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'block';
        document.getElementById('QQ').style.display = 'none';
        document.getElementById('THDIV').style.display = 'none';
        document.getElementById('HTXXDIV').style.display = 'none';
        document.getElementById('FJXXDIV').style.display = 'none';
    })
    $("#QQJQdiv").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        $('#THJL').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('QQ').style.display = 'block';
        document.getElementById('THDIV').style.display = 'none';
        document.getElementById('HTXXDIV').style.display = 'none';
        document.getElementById('FJXXDIV').style.display = 'none';
    });
    $("#THJL").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");
        $('#HTXX').attr("class", "btnTh");
        $('#FJXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('QQ').style.display = 'none';
        document.getElementById('THDIV').style.display = 'block';
        document.getElementById('HTXXDIV').style.display = 'none';
        document.getElementById('FJXXDIV').style.display = 'none';
    });
    $("#PrintOrderInfo").click(function () {
        if (DID == 0) {
            alert("请选择要打印的订货单");
            return;
        }
        window.showModalDialog("../CustomerService/PrintOrderInfonew?OrderID=" + DID, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    })
    $("#UploadContract").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要管理文件的订单");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
        window.parent.OpenDialog("合同文件", "../SalesManage/AddFile?id=" + texts, 500, 300, '');
    });
    $("#UploadFile").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要管理文件的订单");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
        window.parent.OpenDialog("管理附件", "../SalesManage/UploadFile?id=" + texts, 500, 300, '');
    })
    $("#HTXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");//
        $('#THJL').attr("class", "btnTh");
        $('#FJXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('QQ').style.display = 'none';
        document.getElementById('THDIV').style.display = 'none';//
        document.getElementById('HTXXDIV').style.display = 'block';
        document.getElementById('FJXXDIV').style.display = 'none';
    })
    $("#FJXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");//
        $('#THJL').attr("class", "btnTh");
        $('#HTXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('QQ').style.display = 'none';
        document.getElementById('THDIV').style.display = 'none';//
        document.getElementById('FJXXDIV').style.display = 'block';
        document.getElementById('HTXXDIV').style.display = 'none';
    })
});
var curPage = 1;
var DcurPage = 1;
var OnePageCount = 5;
var DID = 0;
var OOstate;
var oldSelID = 0;
var SPID = "";
var PID = "";
var State;
var OrderJE = 0;
function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'GetOrderInfo',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },

    }).trigger("reloadGrid");
}
function LoadOrderInfo() {
    jQuery('#list').jqGrid({
        url: 'GetOrderInfo',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
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
        colNames: ['', '合同编号', '订单编号', '订货单位', '订货单位联系人', '订货单位联系方式',
            '使用单位', '合同总额', '使用单位联系人',
            '使用单位联系人方式',  '', '', '', '', ''],
        colModel: [
        { name: 'PID', index: 'PID', width: 100, hidden: true },
        { name: 'ContractID', index: 'ContractID', width: 100 },//合同编号
        { name: 'OrderID', index: 'OrderID', width: 150 },//订单编号
        { name: 'OrderUnit', index: 'OrderUnit', width: 100 },//订货单位
        { name: 'OrderContactor', index: 'OrderContactor', width: 100 },//订货单位联系人
        { name: 'OrderTel', index: 'OrderTel', width: 100 },//订货单位联系方式
        { name: 'UseUnit', index: 'UseUnit', width: 100 },//使用单位
        { name: 'Total', index: 'Total', width: 100 },//合同总额
        //{ name: 'HKAmount', index: 'HKAmount', width: 100 },//回款总额
        //{ name: 'DebtAmount', index: 'DebtAmount', width: 100 },//欠款金额
        //{ name: 'Ostate', index: 'Ostate', width: 100 },//状态
        { name: 'UseContactor', index: 'UseContactor', width: 100 },//使用单位联系人
        { name: 'UseTel', index: 'UseTel', width: 100 },//使用单位联系人方式
        //{ name: 'DeliveryTime', index: 'DeliveryTime', width: 100 },//交货日期
        { name: 'OOstate', index: 'OOstate', width: 100, hidden: true },//''
        { name: 'State', index: 'State', width: 100, hidden: true },//''
        { name: 'ISF', index: 'ISF', width: 100, hidden: true },// ''
        { name: 'ISHT', index: 'ISHT', width: 100, hidden: true },// ''
        { name: 'EXState', index: 'EXState', width: 100, hidden: true }//''
        ],
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '订单主表',
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;

            DID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
            OOstate = jQuery("#list").jqGrid('getRowData', rowid).OOstate;
            State = jQuery("#list").jqGrid('getRowData', rowid).State;
            OrderJE = jQuery("#list").jqGrid('getRowData', rowid).Total;//订单金额
            select(rowid);
            $("#Billlist tbody").html("");



            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {
                return;
            }
            else {
                var OrderID = Model.OrderID;
                LoadOrderDetail(OrderID);
            }


        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager6") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager6") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager6") {
                if (curPage == 1)
                    return;
                curPage = $("#list").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager6") {
                curPage = 1;
            }
            else {
                curPage = $("#pager6 :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", 150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    DID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
    OrderJE = jQuery("#list").jqGrid('getRowData', rowid).Total;
    $.ajax({
        url: "GetContractSPID",
        type: "post",
        data: { cid: DID },
        dataType: "json",
        async: false,
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                //for (var i = 0; i < json.length; i++) {
                // $("#" + RowId).val(json[i].COMNameC);
                SPID = json[0].PID;
                // }
            }
        }
    });
    reload1();
    LoadOrderInfosBill();
    //reload2();
    reload3();
    reload4();
    reload5();
    reload6();
}
function reload1() {
    $("#Detaillist").jqGrid('setGridParam', {
        url: 'LoadOrderDetailnew',
        datatype: 'json',
        postData: { OrderID: DID, curpage: DcurPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
function reload2() {
    $("#Billlist").jqGrid('setGridParam', {
        url: 'LoadOrderBill',
        datatype: 'json',
        postData: { OrderID: DID, curpage: curPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
//审批信息
function reload3() {
    //PID = $('#PID').val();
    //webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list1").jqGrid('setGridParam', {
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: SPID, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}
var webkey = "订单审批";
function jq1() {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list1").jqGrid({
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: SPID, webkey: webkey, folderBack: folderBack },
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
        pager: jQuery('#pager3'),
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
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
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
            else if (pgButton == "prev_pager") {
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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() + 60, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}
function LoadSerachOrderInfo() {
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    if (StartDate == "" && EndDate == "") {
        GetSearchData();
    }
    else {
        var strSeparator = "-"; //日期分隔符
        var strDateArrayStart;
        var strDateArrayEnd;
        var intDay;
        strDateArrayStart = StartDate.split(strSeparator);
        strDateArrayEnd = EndDate.split(strSeparator);
        var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
        var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);
        if (strDateS <= strDateE) {
            GetSearchData();
        }
        else {
            alert("截止日期不能小于开始日期！");
            $("#End").val("");
            return false;
        }
    }


}
function GetSearchData() {
    if ($('.field-validation-error').length == 0) {
        var ContractID = $('#ContractID').val();
        var OrderUnit = $('#OrderUnit').val();
        var UseUnit = $('#UseUnit').val();
        var OrderContent = $('#MainContent').val(); //$('#SpecsModels').click.Text();
        var StartDate = $('#StartDate').val();
        var EndDate = $('#EndDate').val();
        var State = $("input[name='State']:checked").val();
        var HState = $("input[name='HState']:checked").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetSearchOrderInfonew',
            datatype: 'json',
            postData: {
                curpage: curPage, rownum: OnePageCount, ContractID: ContractID, OrderUnit: OrderUnit,
                UseUnit: UseUnit, MainContent: OrderContent, StartDate: StartDate,
                EndDate: EndDate, State: State, HState: HState
            },
            loadonce: false

        }).trigger("reloadGrid");//重新载入

    }
}
//订单撤销
function OrdersInfoCanCel() {
    $.ajax({
        url: "CanCelOrdersInfonew",
        type: "Post",
        data: {
            ID: DID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                reload();
                alert("撤销完成！");
            }
            else {
                alert("撤销失败-" + data.msg);
            }
        }
    });
}
function LoadOrderDetail(OrderID) {
    jQuery("#Detaillist").jqGrid({
        url: 'LoadOrderDetailnew',
        datatype: 'json',
        postData: { OrderID: OrderID, curpage: 1, rownum: OnePageCount },
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
        colNames: ['', '', '', '物品编码', '物品名称', '规格型号', '单位', '数量', '生产厂家', '单价', '合计', '技术参数', '提交时间', '备注'],
        colModel: [
        { name: 'PID', index: 'PID', width: 20, hidden: true },
        { name: 'OrderID', index: 'OrderID', width: 90, hidden: true },
        { name: 'DID', index: 'DID', width: 90, hidden: true },
        { name: 'ProductID', index: 'ProductID', width: 90 },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'SpecsModels', index: 'SpecsModels', width: 100 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 100 },
        { name: 'OrderNum', index: 'OrderNum', width: 100 },
        { name: 'Manufacturer', index: 'Manufacturer', width: 100 },
        { name: 'Price', index: 'Price', width: 100 },
        { name: 'Subtotal', index: 'Subtotal', width: 100 },
        { name: 'Technology', index: 'Technology', width: 80 },
        { name: 'DeliveryTime', index: 'DeliveryTime', width: 80 },
       // { name: 'State', index: 'State', width: 80 },
        { name: 'Remark', index: 'Remark', width: 80 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        //gridComplete: function () {
        //    var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
        //        jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    }

        //},
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
            reload();
        },
        loadComplete: function () {
            $("#loadlist").jqGrid("setGridHeight", $("#pageContent").height() + 10, false);
            $("#loadlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function LoadOrderBill() {
    jQuery("#Billlist").jqGrid({
        url: 'LoadOrderBill',
        datatype: 'json',
        postData: { OrderID: DID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['发货单号', '操作'],
        colModel: [
      { name: 'ShipGoodsID', index: 'ShipGoodsID', width: 100 },
      { name: 'DID', index: 'DID', width: 100 },

        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                //var id = ids[i];
                //var curRowData = jQuery("#list").jqGrid('getRowData', id);
                //var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                //jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                var id = ids[i];
                var Model = jQuery("#list").jqGrid('getRowData', id);
                Up_Down = "<a href='#' style='color:blue' onclick='GetXX(" + id + ")'  >详情</a>";
                jQuery("#Billlist").jqGrid('setRowData', ids[i], { DID: Up_Down });

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
            reload();
        },
        loadComplete: function () {
            $("#loadlist").jqGrid("setGridHeight", $("#pageContent").height() + 10, false);
            $("#loadlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
//1017k相关单据New
function LoadOrderInfosBill() {
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadReceiveBill",
        type: "post",
        data: { OID: DID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {

                $("#Billlist ReceiveBill").val("");
                $("#Billlist th").html("");
                var html0 = '<tr><th style="width: 5%;" class="th">描述</th><th style="width: 5%;" class="th">编号</th><th style="width: 5%;" class="th">操作</th></tr>'
                $("#ReceiveBill").append(html0);
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html += '<tr>'
                    var s = json[i].ID.substr(0, 2);
                    if (s != "") {
                        if (s == "BA") {
                            html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">备案基本信息</lable> </td>';
                        }
                        if (s == "BJ") {
                            html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">报价信息</lable> </td>';
                        }
                        if (s == "DH") {
                            html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">订货信息</lable> </td>';
                        }
                        if (s == "FH") {
                            html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">发货信息</lable> </td>';
                        }
                        if (s == "HK") {
                            html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">报价信息</lable> </td>';
                        }
                        if (s == "TH") {
                            html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">退换信息</lable> </td>';
                        }

                        html += '<td ><lable class="labID' + rowCount + ' " id="ID' + rowCount + '">' + json[i].ID + '</lable> </td>';

                        html += '<td ><a href="#" style="color:blue" onclick=GetXX("' + json[i].ID + '")>详情</a></td>';
                        html += '</tr>'
                        $("#ReceiveBill").append(html);
                    }
                }


            }
        }
    })
}
function GetXX(SDI) {
    var id = SDI;
    window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 800, 450);


}
// 1017k相关单据New
//function GetXX(id) {
//    var Model = jQuery("#Billlist").jqGrid('getRowData', id);
//    var Xid = Model.ShipGoodsID;
//    window.parent.parent.OpenDialog("发货详细", "../SalesManage/LoadOrdersShipments?ID=" + Xid, 500, 450);
//   }
//退换货记录
function LoadExChangeGoods() {
    jQuery("#list2").jqGrid({
        url: 'GetExChangeGoodsByOID',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderID: DID },
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
        colNames: ['订单编号', '退货单号', '退货日期', '退货方式', '状态'],
        colModel: [
        { name: 'OrderID', index: 'OrderID', width: 200 },
        { name: 'EID', index: 'EID', width: 200 },
        { name: 'ChangeDate', index: 'ChangeDate', width: 200 },
        { name: 'ReturnWay', index: 'ReturnWay', width: 100 },
        { name: 'State', index: 'State', width: 200 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function reload4() {
    $("#list2").jqGrid('setGridParam', {
        url: 'GetExChangeGoodsByOID',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, OrderID: DID
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}
//合同文件
function LoadHTXX() {
    jQuery("#HTXXlist").jqGrid({
        url: 'GetOrderHTXXGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderID: DID },
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
        colNames: ['', '文件名称', '创建时间', '创建人', '操作', '操作'],
        colModel: [
        { name: 'ID', index: 'ID', width: 200, hidden: true },
        { name: 'FileName', index: 'FileName', width: 200 },
        { name: 'CreateTime', index: 'CreateTime', width: 200 },
        { name: 'CreateUser', index: 'CreateUser', width: 200 },
        { name: 'IDCheck', index: 'Id', width: 50 },
        { name: 'deCheck', index: 'Id', width: 50 }
        ],
        pager: jQuery('#pager4'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#HTXXlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#HTXXlist").jqGrid('getRowData', id);
                var curChk = "<a style='color:blue;cursor:pointer' onclick=\"updateCB('" + id + "')\">修改</a>";
                var curChk1 = "<a style='color:blue;cursor:pointer' onclick=\"dellCB('" + jQuery("#HTXXlist").jqGrid('getRowData', id).CBID + "@" + jQuery("#list1").jqGrid('getRowData', id).CID + "')\">撤销</a>";
                jQuery("#HTXXlist").jqGrid('setRowData', ids[i], { IDCheck: curChk, deCheck: curChk1 });
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
            if (pgButton == "next_pager") {
                if (curPage == $("#HTXXlist").getGridParam("lastpage"))
                    return;
                curPage = $("#HTXXlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#HTXXlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#HTXXlist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager4 :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#HTXXlist").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#HTXXlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
//撤销
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
function reload5() {
    $("#HTXXlist").jqGrid('setGridParam', {
        url: 'GetOrderHTXXGrid',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, OrderID: DID
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}
function LoadFJXX() {
    jQuery("#FJXXlist").jqGrid({
        url: 'GetOrderFJXXGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderID: DID },
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
        colNames: ['文件名称', '创建时间', '创建人'],
        colModel: [
        { name: 'FileName', index: 'FileName', width: 200 },
        { name: 'CreateTime', index: 'CreateTime', width: 200 },
        { name: 'CreateUser', index: 'CreateUser', width: 200 }

        ],
        pager: jQuery('#pager5'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#FJXXlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#FJXXlist").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#FJXXlist").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#FJXXlist").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            if (pgButton == "next_pager") {
                if (curPage == $("#FJXXlist").getGridParam("lastpage"))
                    return;
                curPage = $("#FJXXlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#FJXXlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#FJXXlist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager5 :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#FJXXlist").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#FJXXlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });

}
function reload6() {
    $("#FJXXlist").jqGrid('setGridParam', {
        url: 'GetOrderFJXXGrid',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, OrderID: DID
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}