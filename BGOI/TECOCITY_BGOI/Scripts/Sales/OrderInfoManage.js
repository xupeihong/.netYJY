$(document).ready(function () {
    $("#StartDate").val("");
    $("#EndDate").val("");
    LoadOrderInfo();
    LoadOrderDetail();
    // LoadOrderBill();
    LoadOrderInfosBill();
    jq1();
    LoadExChangeGoods();
    LoadHTXX();
    LoadFJXX();
    LoadLog();
    document.getElementById('CPXX').style.display = 'block';
    document.getElementById('div2').style.display = 'none';
    document.getElementById('QQ').style.display = 'none';
    document.getElementById('THDIV').style.display = 'none';
    document.getElementById('HTXXDIV').style.display = 'none';
   // document.getElementById('FJXXDIV').style.display = 'none';
    document.getElementById('RZDIV').style.display = 'none';
    $("#btnAddOrdersInfo").click(function () {
        window.parent.OpenDialog("新增订单", "../SalesManage/AddOrder", 1000, 550);
        // reload();
    });
    $("#btnAddOrdersInfoF").click(function () {
        window.parent.OpenDialog("新增非常规订单", "../SalesManage/AddOrderF", 1000, 550);
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
            OrderJE = jQuery("#list").jqGrid('getRowData', rowid).OrderActualTotal;//订单金额
            //if (ISF == "1" && (ISHT == "0" || ISHT == null || ISHT == "")) {
            //    alert("请先上传工业买卖合同");
            //    return;
            //}

            if (State >= "1" && State != "21" && State != "19") {
                alert("该订单已经提交审批，不能重复操作");
                return;
            }

            if (OOstate == "1" || OOstate == "2" || OOstate == "3" || OOstate == "4" || OOstate == "5") {
                alert("订单不能提交审批");
                return;
            }

            if (State == "20") {
                alert("订单修改审批中不能提交其他审批");
                return;
            }
            if (ISF === "")
            {
                alert("常规产品不审批");
                return;
            }
            if (ISF == "1" && OrderJE <= 100000) { //&& ISHT == "1"
                var texts = DID + "@" + "10万订单审批";
                window.parent.OpenDialog("提交审批", "../SalesManage/SubmitApproval?id=" + texts, 700, 500, '')

            } else if (ISF == "1" && OrderJE > 100000 && OrderJE <= 300000)//&& ISHT == "1" 
            {
                var texts = DID + "@" + "10万至30万审批";
                window.parent.OpenDialog("提交审批", "../SalesManage/SubmitApproval?id=" + texts, 700, 500, '')
            } else if (ISF == "1" && OrderJE > 300000)//&& ISHT == "1"
            {
                var texts = DID + "@" + "30万以上审批";
                window.parent.OpenDialog("提交审批", "../SalesManage/SubmitApproval?id=" + texts, 700, 500, '')

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
        if (rowid == null) {
            alert("请选择要发货的订单");
            return false;
        }
        else {
            if (State == "-1") {
                alert("审批不通过不能进行下面操作");
                return;
            }
            var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
            var ISHT = jQuery("#list").jqGrid('getRowData', rowid).ISHT;
            var ContractID = jQuery("#list").jqGrid('getRowData', rowid).ContractID;
            if (ISF == "1"&&( OOstate != "2"&& State != "2")) {
                alert("非常规产品请先审批完成后才能发货");
                return;
            } else if (OOstate=="3")
                {
                    alert("已经发货完成");
                    return;
                }
            else if (DID != "0") {
                window.parent.OpenDialog("发货", "../SalesManage/OrderShipments?OrderID=" + DID + "&ContractID=" + ContractID, 850, 350, '');
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
        var State = jQuery("#list").jqGrid('getRowData', rowid).State;
        var OState = jQuery("#list").jqGrid('getRowData', rowid).OState;
        if (rowid == null) {
            alert("您还没有选择进行回款的订单");
            return;
        }
        if (State == "-1") {
            alert("审批不通过不能进行下面操作");
            return;
        }
        
        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        var ISHT = jQuery("#list").jqGrid('getRowData', rowid).ISHT;
        var DeptAmount = jQuery("#list").jqGrid('getRowData', rowid).DebtAmount;
        var ContractID = jQuery("#list").jqGrid('getRowData', rowid).ContractID;
        //if (ISF == "1" && ISHT == "1" && OOstate != "3"&&OOstate!="4") 
         if (OOstate == "5") {
            alert("已经结算不能再回款");
            return;
        }
        if (DeptAmount == 0)
        {
            alert("已经回款完成");
            return;
        }
        if (ISF =="1" && (OOstate != 3 && OOstate != 4)) {
            alert("非常规产品请按流程走");
            return;
        } else if (DID != 0) {
            window.parent.OpenDialog("订单回款", "../SalesManage/AddReceivePayment?OrderID=" + DID+ "&ContractID=" + ContractID, 850,450, '');
        }
        else {
            //  alert("请选择列表中已发货的订单数据");
            // return;
            window.parent.OpenDialog("订单回款", "../SalesManage/AddReceivePayment", 850,450, '');

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
        var DebtAmount = jQuery("#list").jqGrid('getRowData', rowid).DebtAmount;
        if (OOstate == "5") {
            alert("已经结算不能再结算");
            return;
        }
        //  if (ISF == "1" && ISHT == "1" && OOstate != "4") 
        if (ISF == "1" && ISHT != "1" && OOstate != "4") {
            alert("非常规产品请按流程走");
            return;
        }
        else if (DID != "0") {
            window.parent.OpenDialog("订单结算", "../SalesManage/OrdersSettlement?id=" + texts + "&PID=" + PID + "&DebtAmount=" + DebtAmount, 800, 400, '');
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
    //退货
    $("#btnEXC").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (State == "-1") {
            alert("审批不通过不能进行下面操作");
            return;
        }
        if (rowid ==null) {
            alert("请选择要退换货的订单");
            return;
        }
        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        var ISHT = jQuery("#list").jqGrid('getRowData', rowid).ISHT;
        var EXState = jQuery("#list").jqGrid('getRowData', rowid).EXState;
        var ContractID = jQuery("#list").jqGrid('getRowData', rowid).ContractID;
        var OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
        if (ISF == "1" &&ISHT == "0"&&OOstate >= "3"&&(EXState != "2"||EXState=="")) {
           // alert("非标准产品退换货审批后才能退换货");
            isConfirm = confirm("非标准产品退换货审批后才能退换货")
            if (isConfirm == false) {
                return false;
            }
            else {
                //先审批
                // var texts = DID + "@" + "退货审批";
                var texts = DID + "@" + "退换货审批";
                window.parent.OpenDialog("提交审批", "../SalesManage/EXSubmitApproval?id=" + texts, 700, 500, '')
            }
            return;
        }// else if (ISF == "1" && ISHT == "1" && OOstate >= "3" && EXState == "2") {
        //    window.parent.OpenDialog("退换货", "../SalesManage/AddExchangeGoods?OrderID=" + DID, 1000, 500, '');
            //}
        //else if (ISF == "1")
        //{
        //    window.parent.OpenDialog("退换货", "../SalesManage/AddExchangeGoodsF?OrderID=" + OrderID + "&ContractID=" + ContractID, 1000, 500, '');
        //}

        else {
            window.parent.OpenDialog("退货", "../SalesManage/AddReturnGoods?OrderID=" + OrderID + "&ContractID=" + ContractID, 1000, 500, '');
        }

    });
    //换货
    $("#btnExchange").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (State == "-1") {
            alert("审批不通过不能进行下面操作");
            return;
        }
        if (rowid == null) {
            alert("请选择要换货的订单");
            return;
        }
        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        var ISHT = jQuery("#list").jqGrid('getRowData', rowid).ISHT;
        var EXState = jQuery("#list").jqGrid('getRowData', rowid).EXState;
        var ContractID = jQuery("#list").jqGrid('getRowData', rowid).ContractID;
        var OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
        if (ISF == "1" && ISHT == "0" && OOstate >= "3" && (EXState != "2" || EXState == "")) {
            // alert("非标准产品退换货审批后才能退换货");
            isConfirm = confirm("非标准产品换货审批后才能退换货")
            if (isConfirm == false) {
                return false;
            }
            else {
                //先审批
                var texts = DID + "@" + "退换货审批";
                window.parent.OpenDialog("提交审批", "../SalesManage/EXSubmitApproval?id=" + texts, 700, 500, '')
            }
            return;
        }
        if (ISF == "1") {
            window.parent.OpenDialog("换货", "../SalesManage/AddExchangeGoodsF?OrderID=" + OrderID + "&ContractID=" + ContractID, 1000, 500, '');
        }
        else {
            window.parent.OpenDialog("换货", "../SalesManage/AddExchangeGoods?OrderID=" + OrderID + "&ContractID=" + ContractID, 1000, 500, '');
        }
    })
    $("#btnUPOrder").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要修改的订单");
            return;
        }
        var State = jQuery("#list").jqGrid('getRowData', rowid).Ostate;
        if (State == "审批中") {
            alert("订单审批中不能修改");
            return;
        }
        if (State == "审批通过") {
            alert("订单审批通过不能修改");
            return;
        
        }
        if(State == "订单修改审批中")
        {
            alert("订单修改审批中不能修改");
            return;
        }
        if (State == "审批完成")
        {
            alert("审批完成的订单不能修改");
            return;
        }
        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        if(ISF==0){
            window.parent.OpenDialog("修改订单", "../SalesManage/UpdateOrdersInfo?OrderID=" + DID, 1000, 550);
        }
        if (ISF == 1)
        {
            window.parent.OpenDialog("修改订单", "../SalesManage/UpdateOrdersInfoF?OrderID=" + DID, 1000, 550);
        }
    })
    $("#btnCancel").click(function () {
        //撤销
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var i = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
        //    var i = $("#list").jqGrid('getGridParam', 'selrow').OrderID
        var State = $("#list").jqGrid("getRowData", rowid).OOstate;
        if (State == "审批完成") {
            alert("审批完成不能撤销");
            return;
        }
        if (rowid == null) {
            alert("请选择要撤销的订单");
            return;
        }
        isConfirm = confirm("确定要撤销编号为"+i+"的订单吗？")
        if (isConfirm == false) {
            return false;
        }
        OrdersInfoCanCel();
    });
   
    $("#DetailXX").click(function () {
        this.className = "btnTw";
        $('#BillXX').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");
        $('#THJL').attr("class", "btnTh");
        $('#HTXX').attr("class", "btnTh");
       // $('#FJXX').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('CPXX').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('QQ').style.display = 'none';
        document.getElementById('THDIV').style.display = 'none';
        document.getElementById('HTXXDIV').style.display = 'none';
       // document.getElementById('FJXXDIV').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'none';
    });
    $("#BillXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");
        $('#THJL').attr("class", "btnTh");
        $('#HTXX').attr("class", "btnTh");
       // $('#FJXX').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('CPXX').style.display = 'none';
        document.getElementById('div2').style.display = 'block';
        document.getElementById('QQ').style.display = 'none';
        document.getElementById('THDIV').style.display = 'none';
        document.getElementById('HTXXDIV').style.display = 'none';
       // document.getElementById('FJXXDIV').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'none';
    })
    $("#QQJQdiv").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        $('#THJL').attr("class", "btnTh");
        $('#HTXX').attr("class", "btnTh");
        //$('#FJXX').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('CPXX').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('QQ').style.display = 'block';
        document.getElementById('THDIV').style.display = 'none';
        document.getElementById('HTXXDIV').style.display = 'none';
       // document.getElementById('FJXXDIV').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'none';
    });
    $("#THJL").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");
        $('#HTXX').attr("class", "btnTh");
       // $('#FJXX').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('CPXX').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('QQ').style.display = 'none';
        document.getElementById('THDIV').style.display = 'block';
        document.getElementById('HTXXDIV').style.display = 'none';
       // document.getElementById('FJXXDIV').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'none';
    });
  

    $("#PrintOrderInfo").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid ==null) {
            alert("请选择要打印的订货单");
            return;
        }
        window.showModalDialog("../SalesManage/PrintOrderInfo?OrderID=" + DID, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    })
    $("#UploadContract").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要上传合同的订单");
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
        window.parent.OpenDialog("管理附件", "../SalesManage/AddFile?id=" + texts, 500, 300, '');
    })
    $("#HTXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");//
        $('#THJL').attr("class", "btnTh");
        $('#FJXX').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('CPXX').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('QQ').style.display = 'none';
        document.getElementById('THDIV').style.display = 'none';//
        document.getElementById('HTXXDIV').style.display = 'block';
       // document.getElementById('FJXXDIV').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'none';
    })
    $("#FJXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");//
        $('#THJL').attr("class", "btnTh");
        $('#HTXX').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('CPXX').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('QQ').style.display = 'none';
        document.getElementById('THDIV').style.display = 'none';//
       // document.getElementById('FJXXDIV').style.display = 'block';
        document.getElementById('HTXXDIV').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'none';
    })
    $("#RZXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        $('#QQJQdiv').attr("class", "btnTh");//
        $('#THJL').attr("class", "btnTh");
        $('#HTXX').attr("class", "btnTh");
        $('#FJXX').attr("class", "btnTh");
        document.getElementById('CPXX').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('QQ').style.display = 'none';
        document.getElementById('THDIV').style.display = 'none';//
       // document.getElementById('FJXXDIV').style.display = 'none';
        document.getElementById('HTXXDIV').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'block';
    });
});
var curPage = 1;//订单列表的当前页
var DcurPage = 1;//订单产品列表的当前页
var SPcurPage = 1;//审批情况表的当前页
var EXcurPage = 1;//退换货表的当前页
var HTcurPage = 1;//合同
var FJcurPage = 1;//附件
var RcurPage = 1;//操作日志
var OnePageCount = 5;
var DOnePageCount = 5;
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
        //loadonce: false
    }).trigger("reloadGrid");
}

function LoadOrderInfo() {
    jQuery('#list').jqGrid({
        url: 'GetOrderInfo',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
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
        colNames: ['', '合同编号', '订单编号', '订单单位', '使用单位', '合同总额', '回款总额', '欠款金额', '状态','创建人', '订货单位联系人', '订货单位联系方式', '使用单位联系人', '使用单位联系人方式', '交货日期', '', '', '', '', ''],
        colModel: [
        { name: 'PID', index: 'PID', width: 100, hidden: true },
        { name: 'ContractID', index: 'ContractID', width: 150},
        { name: 'OrderID', index: 'OrderID', width:150 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 100 },
         { name: 'UseUnit', index: 'UseUnit', width: 100 },
        { name: 'OrderActualTotal', index: 'OrderActualTotal', width: 100 },
        { name: 'HKAmount', index: 'HKAmount', width: 100 },
         { name: 'DebtAmount', index: 'DebtAmount', width: 100 },
        { name: 'Ostate', index: 'Ostate', width: 100 },
        { name: 'CreateUser', index: 'CreateUser', width: 100 },
          { name: 'OrderContactor', index: 'OrderContactor', width: 100 },
        { name: 'OrderTel', index: 'OrderTel', width: 100 },
        { name: 'UseContactor', index: 'UseContactor', width: 100 },
        { name: 'UseTel', index: 'UseTel', width: 100 },
        { name: 'DeliveryTime', index: 'DeliveryTime', width: 100, formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' } },
         { name: 'OOstate', index: 'OOstate', width: 100, hidden: true },
        { name: 'State', index: 'State', width: 100, hidden: true },
        { name: 'ISF', index: 'ISF', width: 100, hidden: true },
             { name: 'ISHT', index: 'ISHT', width: 100, hidden: true },
        { name: 'EXState', index: 'EXState', width: 100, hidden: true }
        ],
        pager: jQuery('#pager0'),
       // emptyrecords: "Nothing to display",
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
            OrderJE = jQuery("#list").jqGrid('getRowData', rowid).OrderActualTotal;//订单金额
            select(rowid);
            //$("#Billlist tbody").html("");
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager0") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager0") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager0") {
                if (curPage == 1)
                    return;
                curPage = $("#list").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager0") {
                curPage = 1;
            }
            else {
                curPage = $("#pager0 :input").val();
            }
            reloadS();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    DID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
    OrderJE = jQuery("#list").jqGrid('getRowData', rowid).Total;
    DcurPage = 1;
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
    reload7();
}
function reload1() {
    $("#Detaillist").jqGrid('setGridParam', {
        url: 'LoadOrderDetail',
        datatype: 'json',
        postData: { OrderID: DID, curpage: DcurPage, rownum: DOnePageCount }, //,jqType:JQtype
        loadonce: false
    }).trigger("reloadGrid");
}
function reload2() {
    $("#Billlist").jqGrid('setGridParam', {
        url: 'LoadOrderBill',
        datatype: 'json',
        postData: { OrderID: DID, curpage: curPage, rownum: OnePageCount }, //,jqType:JQtype
        loadonce: false
    }).trigger("reloadGrid");
}
//审批信息
function reload3() {
    //PID = $('#PID').val();
    //webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list1").jqGrid('setGridParam', {
        url: '../SalesManage/ConditionGrid',
        datatype: 'json',
        postData: { curpage: SPcurPage, rownum: OnePageCount, PID: SPID, webkey: webkey, folderBack: folderBack },
        loadonce: false
    }).trigger("reloadGrid");
}
var webkey = "订单审批";
function jq1() {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list1").jqGrid({
        url: '../SalesManage/ConditionGrid',
        datatype: 'json',
        postData: { curpage: SPcurPage, rownum: OnePageCount, PID: SPID, webkey: webkey, folderBack: folderBack },
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
                if (SPcurPage == $("#list1").getGridParam("lastpage"))
                    return;
                SPcurPage = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                SPcurPage = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (SPcurPage == 1)
                    return;
                SPcurPage = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                SPcurPage = 1;
            }
            else {
                SPcurPage = $("#pager1 :input").val();
            }
            reload3();
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
    curPage=1;
    OnePageCount = 5;
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
            url: 'GetSearchOrderInfo',
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


function reloadS()
{
    var ContractID = $('#ContractID').val();
    var OrderUnit = $('#OrderUnit').val();
    var UseUnit = $('#UseUnit').val();
    var OrderContent = $('#MainContent').val(); //$('#SpecsModels').click.Text();
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    var State = $("input[name='State']:checked").val();
    var HState = $("input[name='HState']:checked").val();
    $("#list").jqGrid('setGridParam', {
        url: 'GetSearchOrderInfo',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, ContractID: ContractID, OrderUnit: OrderUnit,
            UseUnit: UseUnit, MainContent: OrderContent, StartDate: StartDate,
            EndDate: EndDate, State: State, HState: HState
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}
//订单撤销
function OrdersInfoCanCel() {
    $.ajax({
        url: "CanCelOrdersInfo",
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
                alert("撤销失败");
            }
        }
    });
}

function LoadOrderDetailqq() {
    jQuery("#Detaillist").jqGrid({
        url: 'LoadOrderDetail',
        datatype: 'json',
        postData: { OrderID: DID, curpage: DcurPage, rownum: DOnePageCount },
        loadonce: true,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            pager: function (obj) {
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
        colNames: ['', '', '', '产品名称', '规格型号', '单位', '数量', '生产厂家', '单价', '合计', '技术参数', '备注'],
        colModel: [
        { name: 'PID', index: 'PID', width: 20, hidden: true },
        { name: 'OrderID', index: 'OrderID', width: 90, hidden: true },
        { name: 'DID', index: 'DID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'SpecsModels', index: 'SpecsModels', width: 100 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 100 },
        { name: 'ActualAmount', index: 'ActualAmount', width: 100 },
        { name: 'Manufacturer', index: 'Manufacturer', width: 100 },
        { name: 'Price', index: 'Price', width: 100 },
      { name: 'ActualSubTotal', index: 'ActualSubTotal', width: 100 },
        { name: 'Technology', index: 'Technology', width: 80 },
        { name: 'Remark', index: 'Remark', width: 80 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //    DID = jQuery("#loadlist").jqGrid('getRowData', rowid).PID;
        //    // LoadOrdersInfo();

        //},
    
        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (DcurPage == $("#Detaillist").getGridParam("lastpager1"))
                    return;
                DcurPage = $("#Detaillist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                DcurPage = $("#Detaillist").getGridParam("lastpager1");
            }
            else if (pgButton == "prev_pager1") {
                if (DcurPage == 1)
                    return;
                DcurPage = $("#Detaillist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                DcurPage = 1;
            }
            else {
                DcurPage = $("#pager1:input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#Detaillist").jqGrid("setGridHeight", $("#pageContent").height() + 80, false);
            $("#Detaillist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadOrderDetail() {
    jQuery("#Detaillist").jqGrid({
        url: 'LoadOrderDetail',
        datatype: 'json',
        postData: { OrderID: DID, curpage: DcurPage, rownum: OnePageCount },
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
        colNames: ['', '', '', '产品名称', '规格型号', '单位', '数量', '生产厂家', '单价', '合计', '技术参数', '备注'],
        colModel: [
        { name: 'PID', index: 'PID', width: 20, hidden: true },
        { name: 'OrderID', index: 'OrderID', width: 90, hidden: true },
        { name: 'DID', index: 'DID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'SpecsModels', index: 'SpecsModels', width: 100 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 100 },
        { name: 'ActualAmount', index: 'ActualAmount', width: 100 },
        { name: 'Manufacturer', index: 'Manufacturer', width: 100 },
        { name: 'Price', index: 'Price', width: 100 },
        { name: 'ActualSubTotal', index: 'ActualSubTotal', width: 100 },
        { name: 'Technology', index: 'Technology', width: 80 },
        { name: 'Remark', index: 'Remark', width: 80 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: DOnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (DcurPage == $("#Detaillist").getGridParam("lastpage"))
                    return;
                DcurPage = $("#Detaillist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                DcurPage = $("#Detaillist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (DcurPage == 1)
                    return;
                DcurPage = $("#Detaillist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                DcurPage = 1;
            }
            else {
                DcurPage = $("#pager1 :input").val();
            }
            reload1()
        },
        loadComplete: function () {
            $("#Detaillist").jqGrid("setGridHeight", $("#pageContent").height() + 80, false);
            $("#Detaillist").jqGrid("setGridWidth", $("#bor").width() - 30, false);
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
            $("#loadlist").jqGrid("setGridHeight", $("#pageContent").height() + 80, false);
            $("#loadlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

//1017k相关单据New
function LoadOrderInfosBill() {
  //  $("#Billlist ReceiveBill").empty();
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
                //alert($("#ReceiveBill").html())
                $("#ReceiveBill").html("");
                //$("#trth").html("");
                var html3 = $("#ReceiveBill").InnerHTML;
                var html0 = '<th style="width: 5%;" class="th">描述</th><th style="width: 5%;" class="th">编号</th><th style="width: 5%;" class="th">操作</th>'
                //$("#trth").html(html0);
                
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
                        $("#ReceiveBill").html(html);
                    }
                }


            }
        }
    })
}

function GetXX(SDI) {
    //var id = SDI;
    //window.parent.parent.OpenDialog("详细", "../ConditionGrid/LoadReceivePaymentXX?ID=" + id, 800, 450);
    var id = SDI;
    var s = id.substr(0, 2);
    if (s == "BA") { window.parent.parent.OpenDialog("备案详细", "../SalesManage/ProjectBill?ID=" + id, 1000, 450); }
    else if (s == "BJ") { window.parent.parent.OpenDialog("报价详细", "../SalesManage/OfferBill?ID=" + id, 800, 450); }
    else if (s == "DH") { window.parent.parent.OpenDialog("订单详细", "../SalesManage/OrdersInfoBill?ID=" + id, 800, 450); }
    else if (s == "FH") { window.parent.parent.OpenDialog("发货详细", "../SalesManage/ShipmentBill?ID=" + id, 800, 450); }
    else if (s == "HK") { window.parent.parent.OpenDialog("回款详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 800, 450); }
    else if (s == "TH") { window.parent.parent.OpenDialog("退换货详细", "../SalesManage/ExchangeBill?ID=" + id, 800, 450); }

}

function LoadExChangeGoods() {
    jQuery("#list2").jqGrid({
        url: 'GetExChangeGoodsByOID',
        datatype: 'json',
        postData: { curpage: EXcurPage, rownum: OnePageCount, OrderID: DID },
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
        { name: 'ChangeDate', index: 'ChangeDate', width: 200, formatoptions: { newformat: 'yyyy-MM-dd' } },
        { name: 'ReturnWay', index: 'ReturnWay', width: 100 },
        { name: 'State', index: 'State', width: 200 }
        ],
        pager: jQuery('#pager7'),
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
            if (pgButton == "next_pager7") {
                if (EXcurPage == $("#list2").getGridParam("lastpage"))
                    return;
                EXcurPage = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager7") {
                EXcurPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager7") {
                if (EXcurPage == 1)
                    return;
                EXcurPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager7") {
                EXcurPage = 1;
            }
            else {
                EXcurPage = $("#pager7 :input").val();
            }
            reload4();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() + 80, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload4() {
    $("#list2").jqGrid('setGridParam', {
        url: 'GetExChangeGoodsByOID',
        datatype: 'json',
        postData: {
            curpage: EXcurPage, rownum: OnePageCount, OrderID: DID
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}
//合同文件
function reload5() {
    $("#HTXXlist").jqGrid('setGridParam', {
        url: 'GetOrderHTXXGrid',
        datatype: 'json',
        postData: {
            curpage: HTcurPage, rownum: OnePageCount, OrderID: DID
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}
function LoadHTXX() {
    jQuery("#HTXXlist").jqGrid({
        url: 'GetOrderHTXXGrid',
        datatype: 'json',
        postData: { curpage: HTcurPage, rownum: OnePageCount, OrderID: DID },
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
        colNames: ['','', '文件名称', '创建时间', '创建人', '操作', '操作'],
        colModel: [
        { name: 'ID', index: 'ID', width: 200, hidden: true },
        { name: 'CID', index: 'CID', width: 200, hidden: true },
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
                var curChk = "<a style='color:blue;cursor:pointer' onclick=\"deleteFile('" + jQuery("#HTXXlist").jqGrid('getRowData', id).ID + "')\">删除</a>";
                var curChk1 = "<a style='color:blue;cursor:pointer' onclick=\"DownloadFile('" + jQuery("#HTXXlist").jqGrid('getRowData', id).ID + "')\">下载</a>";
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
            if (pgButton == "next_pager4") {
                if (HTcurPage == $("#HTXXlist").getGridParam("lastpage"))
                    return;
                HTcurPage = $("#HTXXlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager4") {
                HTcurPage = $("#HTXXlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager4") {
                if (HTcurPage == 1)
                    return;
                HTcurPage = $("#HTXXlist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager4") {
                HTcurPage = 1;
            }
            else {
                HTcurPage = $("#pager4 :input").val();
            }
            reload5();
        },
        loadComplete: function () {
            $("#HTXXlist").jqGrid("setGridHeight", $("#pageContent").height() + 80, false);
            $("#HTXXlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
//修改
function updateCB(id) {
    window.parent.OpenDialog("修改合同上传", "../SalesManage/AddFile?id=" + id, 500, 300, '');
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

//下载
function DownloadFile(id) {
    window.open("DownLoad2?id=" + id);
}
//删除
function deleteFile(id) {
    var one = confirm("确实要删除这条条目吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deleteFile",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    loadFile();
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
    }
}





function LoadFJXX() {
    jQuery("#FJXXlist").jqGrid({
        url: 'GetOrderFJXXGrid',
        datatype: 'json',
        postData: { curpage: FJcurPage, rownum: OnePageCount, OrderID: DID },
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
            if (pgButton == "next_pager5") {
                if (FJcurPage == $("#FJXXlist").getGridParam("lastpage"))
                    return;
                FJcurPage = $("#FJXXlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager5") {
                FJcurPage = $("#FJXXlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager5") {
                if (FJcurPage == 1)
                    return;
                FJcurPage = $("#FJXXlist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager5") {
                FJcurPage = 1;
            }
            else {
                FJcurPage = $("#pager5 :input").val();
            }
            reload6();
        },
        loadComplete: function () {
            $("#FJXXlist").jqGrid("setGridHeight", $("#pageContent").height() + 80, false);
            $("#FJXXlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });

}

function reload6() {
    $("#FJXXlist").jqGrid('setGridParam', {
        url: 'GetOrderFJXXGrid',
        datatype: 'json',
        postData: {
            curpage: FJcurPage, rownum: OnePageCount, OrderID: DID
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}

function LoadLog()
{
    jQuery("#RZlist").jqGrid({
        url: 'GetLogGrid',
        datatype: 'json',
        postData: { curpage: RcurPage, rownum: OnePageCount, ID: DID },
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
            $("#FJXXlist").jqGrid("setGridHeight", $("#pageContent").height() + 80, false);
            $("#FJXXlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload7()
{
    $("#RZlist").jqGrid('setGridParam', {
        url: 'GetLogGrid',
        datatype: 'json',
        postData: {
            curpage: RcurPage, rownum: OnePageCount, ID: DID
        },
        //loadonce: false

    }).trigger("reloadGrid");//重新载入
}