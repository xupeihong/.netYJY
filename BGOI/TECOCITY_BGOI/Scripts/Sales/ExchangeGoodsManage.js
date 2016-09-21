$(document).ready(function () {
    LoadExchangeGoods();
    LoadExchangeDetail();
    LoadReturnDetail();
    LoadBillRelateData();
    //LoadReceiveBill();
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

    $("#btnSearch").click(function () {
        GetSearchExchangGoods();
    })
    $("#btnUpdate").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择列表数据");
            return;
        }
        var State = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (State == "退货完成") {
            alert("退货完成不能修改");
            return;
        }
        if (State == "已检验") {
            alert("已检验不能修改");
            return;
        }
        //var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        //if (rowid == null) {
        //    alert("请选择列表数据");
        //    return;
        //}
        var ISEXR = jQuery("#list").jqGrid('getRowData', rowid).ISEXR;
        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
        if (ISEXR == "2") {
            if (ISF == "1")
            { window.parent.parent.OpenDialog("修改换货单", "../SalesManage/UpdateExchangeGoodsF?EID=" + ID + "&OrderID=" + OrderID, 1000, 550); }
            else{
                window.parent.parent.OpenDialog("修改换货单", "../SalesManage/UpdateExchangeGoods?EID=" + ID + "&OrderID=" + OrderID, 1000, 500);
            }
        } else
        {
            window.parent.parent.OpenDialog("修改退货单", "../SalesManage/UpdateReturnGoods?EID=" + ID + "&OrderID=" + OrderID, 1000, 500);

          //  UpdateReturnDetail
          //  ShowIframe1("123", "../SalesManage/UpdateReturnDetail?EID=" + ID + "&DID=" + OrderID, 700, 350, '');
        }
        //UpdateExchangeGoods();
    });
    //退换货检验
    $("#btnCheck").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择列表数据");
            return;
        }
        var State = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (State == "已检验") {
            alert("退货检验完成不能重复操作");
            return;
        }
        if (State == "退货完成") {
            alert("退货完成不能重复退货检验");
            return;
        }
        window.parent.parent.OpenDialog("退货检验", "../SalesManage/ExchangeCheckGoods?EID=" + ID + "&OrderID=" + OrderID, 800, 450);
    });
    $("#btnCancel").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要撤销的退换货记录");
            return;
        }
        var State = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (State == "退货完成") {
            alert("退货完成不能撤销");
            return;
        }
        if (State == "已检验") {
            alert("已检验不能撤销");
            return;
        }
        //撤销
        ExchangGoodsCancel();
    });
    //退货完成
    $("#btnFinish").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择列表数据");
            return;
        }
        var ID = jQuery("#list").jqGrid('getRowData', rowid).EID;
        var State = jQuery("#list").jqGrid('getRowData', rowid).State;
        var ProductionState = jQuery("#list").jqGrid('getRowData', rowid).ProductionState;
        //退货检验审批为完成不能提交
        if (ProductionState != "审批通过") {
            alert("检验审批未完成不能提交");
            return;
        }
        if (State == "审批完成") { alert("退换货已完成不能重复"); return; }
        var State = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (State == "退货完成") {
            alert("退货完成不能提交");
            return;
        }
        window.parent.parent.OpenDialog("退货完成", "../SalesManage/ExchangeGoodsFinish?EID=" + ID + "&OrderID=" + OrderID, 750, 170);

        // ExchangeGoodsFinish();
    });

    $("#btnPrint").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择列表数据");
            return;
        }
        var ID = jQuery("#list").jqGrid('getRowData', rowid).EID;
        window.showModalDialog("../SalesManage/PrintExChangeGoods?EID=" + ID, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");

    });
});
var ID = 0;
var OrderID = '';
var curPage = 1;
var OnePageCount = 5;
var DcurPage = 1;
var DOnePageCount = 5;
var RcurPage = 1;
var ROnePageCount = 5;
var oldSelID = 0;
function LoadExchangeGoods() {
    jQuery("#list").jqGrid({
        url: 'LoadExchangeGoodsGrid',
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
        colNames: ['订单编号', '合同编号', '退货单号', '退换货日期', '退货方式', '状态', '', '','审批状态'],
        colModel: [
        { name: 'OrderID', index: 'OrderID', width: 200 },
        { name: 'ContractID', index: 'ContractID', width: 200 },
        { name: 'EID', index: 'EID', width: 200 },
        { name: 'ChangeDate', index: 'ChangeDate', width: 200 },
        { name: 'ReturnWay', index: 'ReturnWay', width: 100 },
        { name: 'State', index: 'State', width: 200 },
        { name: 'ISF', index: 'ISF', width: 200,hidden:true },
        { name: 'ISEXR', index: 'ISEXR', width: 200, hidden: true },
        { name: 'ProductionState', index: 'ProductionState', width: 200}
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
            ID = jQuery("#list").jqGrid('getRowData', rowid).EID//0812k
            OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
            select(rowid);
            // $("#Billlist tbody").html("");
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

function select(rowid) {
    ID = jQuery("#list").jqGrid('getRowData', rowid).EID;
    OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
    reload1();
    reload2();
    LoadBillRelateData();
}
function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadExchangeGoodsGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
function reload1() {
    $("#Detaillist").jqGrid('setGridParam', {
        url: 'LoadExchangeDetail',
        datatype: 'json',
        postData: { Eid: ID, curpage: DcurPage, rownum: DOnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
function reload2() {
    $("#DetailReturnlist").jqGrid('setGridParam', {
        url: 'LoadReturnDetail',
        datatype: 'json',
        postData: { Eid: ID, curpage: RcurPage, rownum: ROnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");

    //LoadReceiveBill();
}
function GetSearchExchangGoods() {
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
            $("#EndDate").val("");
            return false;
        }
    }


}
function GetSearchData() {
    if ($('.field-validation-error').length == 0) {
        var OrderID = $("#OrderID").val();
        var ContractID = $('#ContractID').val();
        //   var Brokerage = $('#Manager').val(); //$('#SpecsModels').click.Text();
        var StartDate = $('#StartDate').val();
        var EndDate = $('#EndDate').val();
        var State = $("input[name='State']:checked").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetSearchExchangeGoodsGrid',
            datatype: 'json',
            postData: {
                curpage: 1, rownum: OnePageCount, OrderID: OrderID, ContractID: ContractID,
                StartDate: StartDate,
                EndDate: EndDate, State: State
            },
            loadonce: false

        }).trigger("reloadGrid");//重新载入
    }

}
//退货详细
function LoadExchangeDetail() {
    jQuery("#Detaillist").jqGrid({
        url: 'LoadExchangeDetail',
        datatype: 'json',
        postData: { Eid: ID, curpage: DcurPage, rownum: DOnePageCount },
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
        colNames: ['退货单号', '物料编码', '产品名称', '规格型号', '生产厂家', '退货数量', '单位', '退货单价含税', '退货总结含税', '退货单价不含税', '退货总价不含税', '备注', ],
        colModel: [
        { name: 'EID', index: 'EID', width: 200 },
        { name: 'ProductID', index: 'ProductID', width: 200 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'Amount', index: 'Amount', width: 100 },
        { name: 'Unit', index: 'Unit', width: 100 },
        { name: 'ExUnit', index: 'ExUnit', width: 100 },
        { name: 'ExTotal', index: 'ExTotal', width: 100 },
         { name: 'ExUnitNo', index: 'ExUnit', width: 100 },
        { name: 'ExTotalNo', index: 'ExTotalNo', width: 100 },
        { name: 'Remark', index: 'Remark', width: 200 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '退货主表',

        gridComplete: function () {
            var ids = jQuery("#Detaillist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#Detaillist").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#Detaillist").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#Detaillist").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            ID = jQuery("#list").jqGrid('getRowData', rowid).RID//0812k
            OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
            //  select(rowid);
            //   $("#Billlist tbody").html("");
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
            reload1();
        },
        loadComplete: function () {
            $("#Detaillist").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#Detaillist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
//换货详细
function LoadReturnDetail() {
    jQuery("#DetailReturnlist").jqGrid({
        url: 'LoadReturnDetail',
        datatype: 'json',
        postData: { Eid: ID, curpage: RcurPage, rownum: ROnePageCount },
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
        colNames: ['退货单号', '物料编码', '产品名称', '规格型号', '生产厂家', '退货数量', '单位', '退货单价含税', '退货总结含税', '退货单价不含税', '退货总价不含税', '备注', ],
        colModel: [
        { name: 'EID', index: 'EID', width: 200 },
        { name: 'ProductID', index: 'ProductID', width: 200 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'Amount', index: 'Amount', width: 100 },
        { name: 'Unit', index: 'Unit', width: 100 },
        { name: 'ExUnit', index: 'ExUnit', width: 100 },
        { name: 'ExTotal', index: 'ExTotal', width: 100 },
         { name: 'ExUnitNo', index: 'ExUnit', width: 100 },
        { name: 'ExTotalNo', index: 'ExTotalNo', width: 100 },
        { name: 'Remark', index: 'Remark', width: 200 }
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '换货详细',

        gridComplete: function () {
            var ids = jQuery("#DetailReturnlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#DetailReturnlist").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#DetailReturnlist").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#DetailReturnlist").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            ID = jQuery("#list").jqGrid('getRowData', rowid).RID//0812k
            OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
            //select(rowid);
            //$("#Billlist tbody").html("");
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (RcurPage == $("#DetailReturnlist").getGridParam("lastpage"))
                    return;
                RcurPage = $("#DetailReturnlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                RcurPage = $("#DetailReturnlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (RcurPage == 1)
                    return;
                RcurPage = $("#DetailReturnlist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                RcurPage = 1;
            }
            else {
                RcurPage = $("#pager3 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#DetailReturnlist").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#DetailReturnlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadBillRelateData() {
    rowCount = document.getElementById("ExchangeBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadExchangeBill",
        type: "post",
        data: { OID: OrderID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {

                $("#ExchangeBill").html("");
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
                    $("#ExchangeBill").append(html);
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
    if (s == "BA") { window.parent.parent.OpenDialog("详细", "../SalesManage/ProjectBill?ID=" + id, 1000, 450); }
    else if (s == "BJ") { window.parent.parent.OpenDialog("详细", "../SalesManage/OfferBill?ID=" + id, 800, 450); }
    else if (s == "DH") { window.parent.parent.OpenDialog("详细", "../SalesManage/OrdersInfoBill?ID=" + id, 800, 450); }
    else if (s == "FH") { window.parent.parent.OpenDialog("详细", "../SalesManage/ShipmentBill?ID=" + id, 800, 450); }
    else if (s == "HK") { window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 800, 450); }
    else if (s == "TH") { window.parent.parent.OpenDialog("详细", "../SalesManage/ExchangeBill?ID=" + id, 800, 450); }

}

function UpdateExchangeGoods() {
    window.parent.parent.OpenDialog("修改退换货单", "../SalesManage/UpdateExchangeGoods?EID=" + ID + "&OrderID=" + OrderID, 800, 450);
}

//退换货撤销
function ExchangGoodsCancel() {
    isConfirm = confirm("确定要撤销退换货记录吗？")
    if (isConfirm == false) {
        return false;
    }
    $.ajax({
        url: "CanCelExchangGoods",
        type: "Post",
        data: {
            ID: ID
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
//退货完成
function ExchangeGoodsFinish() {

}