$(document).ready(function () {
    LoadShipments();
    GetShipmentsDetail();
    //GetShipmentsBill();
    LoadShipmentsBill();
    document.getElementById('div1').style.display = 'block';
    document.getElementById('div2').style.display = 'none';
    $("#DetailXX").click(function () {
        this.className = "btnTw";
        $('#BillXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
    });
    $("#BillXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'block';
    });
    $("#btnPrintShipments").click(function () {
       // window.parent.OpenDialog("打印发货单", "../SalesManage/PrintShipments?ShipGoodsID=" + ID+"&OrderID="+OrderID, 850, 600, '');
        // PrintShipment();
        if (ID == 0) {
            alert("请选择要打印的发货单");
            return;
        }
        window.showModalDialog("../SalesManage/PrintShipments?ShipGoodsID=" + ID+"&OrderID="+OrderID, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });
    $("#btnUpdate").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要修改的发货单");
            return;
        }
       var ID = jQuery("#list").jqGrid('getRowData', rowid).ShipGoodsID;
      //  var ID = $("#list").jqGrid('getGridParam', rowid).ShipGoodsID
        window.parent.OpenDialog("修改发货单", "../SalesManage/UpdateShipments?ShipGoodsID=" + ID, 850, 400, '');
    });
    $("#btnContract").click(function () {
        //if (ID == 0) {
        //    alert("请选择要撤销的发货单");
        //    return;
        //}
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        //报价审批通过不让修改
        if (rowid == null) {
            alert("请选择要撤销的发货单");
            return;
        }
       var  ID = jQuery("#list").jqGrid('getRowData', rowid).ShipGoodsID;
        //var ID = $("#list").jqGrid('getGridParam', rowid).ShipGoodsID
        isConfirm = confirm("要撤销的发货单号为："+ID)
        if (isConfirm == false) {
            return false;
        }
        ShipmentContract();
    })
});
var ID = 0;
var OrderID = 0;
var curPage = 1;
var DcurPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
function LoadShipments()
{
    jQuery("#list").jqGrid({
        url: 'LoadShipmentsGrid',
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
        colNames: ['订货编号','合同编号','发货单号','发货日期','发货人','订货人','备注'],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'OrderID', index: 'OrderID', width: 200 },
        { name: 'ContractID', index: 'ContractID', width: 200 },
        { name: 'ShipGoodsID', index: 'ShipGoodsID', width: 200 },
        { name: 'ShipmentDate', index: 'ShipmentDate', width: 80 },
        { name: 'Shippers', index: 'Shippers', width: 80 },
        { name: 'Orderer', index: 'Orderer', width: 150 },
        { name: 'Remark', index: 'Remark', width: 100 }
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '发货表',

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
            ID = jQuery("#list").jqGrid('getRowData', rowid).ShipGoodsID//0812k
            OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
            select(rowid);
          //  $("#Billlist tbody").html("");
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPage == 1)
                    return;
                curPage = $("#list").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPage = 1;
            }
            else {
                curPage = $("#pager3 :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}


function reload() 
{
    $("#list").jqGrid('setGridParam', {
        url: 'LoadShipmentsGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },

    }).trigger("reloadGrid");
}

function LoadSerachInfo()
{
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

function GetSearchData()
{
    if ($('.field-validation-error').length == 0) {
    var ContractID = $("#ContractID").val();
   // var OrderUnit = $("#OrderUnit").val();
   // var UseUnit = $("#UseUnit").val();
   // var OrderID = $("#OrderID").val();
    var OrderContent = $("#MainContent").val();
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    $("#list").jqGrid('setGridParam', {
        url: 'GetSearchShipmentsGrid',
        datatype: 'json',
        postData: {
            curpage: 1, rownum: OnePageCount, ContractID: ContractID, OrderContent: OrderContent, StartDate: StartDate,
            EndDate: EndDate
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}
}

function GetShipmentsDetail()
{
    jQuery('#Detaillist').jqGrid({
        url: 'LoadOrderShipmentDetail',
        datatype: 'json',
        postData: { ShipGoodsID: ID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['发货编码', '', '物品编码', '物品名称', '规格型号', '生成厂家', '单位', '单价', '数量', '备注'],
        colModel: [
        { name: 'ShipGoodsID', index: 'ShipGoodsID', width: 100, hidden: true },
        { name: 'DID', index: 'DID', width: 100,hidden:true },
        { name: 'ProductID', index: 'ProductID', width: 100 },//
         { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'Unit', index: 'Unit', width: 100 },
        { name: 'Price', index: 'Price', width: 100 },
        { name: 'Amount', index: 'Amount', width: 100 },
        { name: 'Remark', index: 'Remark', width: 100 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '发货物品详细表',
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
           // select(rowid);
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#listDetail").jqGrid("setGridHeight", 150, false);
            $("#listDetail").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function GetShipmentsBill()
{
    jQuery("#Billlist").jqGrid({
        url: 'LoadShipmentBill',
        datatype: 'json',
        postData: { ShipGoodsID: ID, curpage: curPage, rownum: OnePageCount },
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
function select(rowid) {
    ID = jQuery("#list").jqGrid('getRowData', rowid).ShipGoodsID;
    OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
    reload1();
    LoadShipmentsBill();
    reload2();
}
function reload1() {
    $("#Detaillist").jqGrid('setGridParam', {
        url: 'LoadOrderShipmentDetail',
        datatype: 'json',
        postData: { ShipGoodsID: ID, curpage: DcurPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
function reload2() {
    $("#Billlist").jqGrid('setGridParam', {
        url: 'LoadShipmentBill',
        datatype: 'json',
        postData: { ShipGoodsID: ID, curpage: curPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
//打印
function PrintShipment() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = jQuery("#list").jqGrid('getRowData', rowid).RepairID;
    if (rowid == null) {
        alert("请在列表中选择一条数据");
        return;
    }
    else {
        var texts = jQuery("#list").jqGrid('getRowData', rowid).RepairID + "@" + "iframe";
        var url = "PrintCard?Info=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    }
}
//撤销发货单
function ShipmentContract()
{
    $.ajax({
        url: "CanCelShipments",
        type: "Post",
        data: {
            ShipGoodsID: ID
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

//1017k相关单据New
function LoadShipmentsBill() 
{
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadReceiveBill",
        type: "post",
        data: { OID: OrderID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {

                $("#ReceiveBill").html("");
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html += '<tr>'
                    var s = json[i].ID.substr(0, 2);
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
    })
}

function GetXX(SDI) {
    //var id = SDI;
    //window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 500, 450);
    var id = SDI;
    var s = id.substr(0, 2);
    if (s == "BA") { window.parent.parent.OpenDialog("备案详细", "../SalesManage/ProjectBill?ID=" + id, 1000, 450); }
    else if (s == "BJ") { window.parent.parent.OpenDialog("报价详细", "../SalesManage/OfferBill?ID=" + id, 800, 450); }
    else if (s == "DH") { window.parent.parent.OpenDialog("订单详细", "../SalesManage/OrdersInfoBill?ID=" + id, 800, 450); }
    else if (s == "FH") { window.parent.parent.OpenDialog("发货详细", "../SalesManage/ShipmentBill?ID=" + id, 800, 450); }
    else if (s == "HK") { window.parent.parent.OpenDialog("回款详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 800, 450); }
    else if (s == "TH") { window.parent.parent.OpenDialog("退换货详细", "../SalesManage/ExchangeBill?ID=" + id, 800, 450); }

}

//1017k相关单据New