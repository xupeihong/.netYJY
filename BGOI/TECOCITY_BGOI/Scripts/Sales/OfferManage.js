$(document).ready(function () {
    LoadBasInfo();

    //1011k
    LoadDetail();
    //LoadOrdersInfo();
    LoadOffersBill();
    jq1();
    LoadFJ();
    document.getElementById('div1').style.display = 'block';
    document.getElementById('div2').style.display = 'none';
    document.getElementById('RZJ').style.display = 'none';
    document.getElementById('FJ').style.display = 'none'
    //1011k

    $("#btnAddOffer").click(function () {

        window.parent.OpenDialog("新增报价单", "../SalesManage/AddOffer", 1000, 550, '');
    });
    $("#DetailXX").click(function () {
        // LoadDetail(ID);
        this.className = "btnTw";
        $('#BillXX').attr("class", "btnTh");
        $('#btnRZ').attr("class", "btnTh");
        $('#btnFJ').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('RZJ').style.display = 'none';
        document.getElementById('FJ').style.display = 'none';

    });
    $("#BillXX").click(function () {
        // LoadOrdersInfo();
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#btnRZ').attr("class", "btnTh");
        $('#btnFJ').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'block';
        document.getElementById('RZJ').style.display = 'none';
        document.getElementById('FJ').style.display = 'none';
    });
    $("#btnFJ").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#btnRZ').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('RZJ').style.display = 'none';
        document.getElementById('FJ').style.display = 'block';
    })
    $("#btnUpdate").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        //报价审批通过不让修改
        if (rowid == null) {
            alert("请选择要修改的报价单");
            return;
        }
        // var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var State = jQuery("#list").jqGrid('getRowData', rowid).OState;
        var ID = jQuery("#list").jqGrid('getRowData', rowid).BJID;
        if (State == "审批通过" || State == "审批中") {
            alert("不能修改");
            return;
        }

        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        if (ISF == 0 || ISF == "") { window.parent.OpenDialog("修改报价", "../SalesManage/UpdateOffer?BJID=" + ID, 1000, 550); }
        if (ISF == 1) { window.parent.OpenDialog("修改报价", "../SalesManage/UpdateOfferF?BJID=" + ID, 1000, 550); }

    });
    $("#btnContract").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        //报价审批通过不让修改
        if (rowid == null) {
            alert("请选择要撤销的报价单");
            return;
        }
        var ID = jQuery("#list").jqGrid('getRowData', rowid).BJID;
        //var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        var OOState = jQuery("#list").jqGrid('getRowData', rowid).OState;
        if (OOState == "审批通过") {
            alert("审批通过不能撤销");
            return;
        }
        Cancel(ID);
    });
    $("#btnSP").click(function () {
        //if (ID == 0) {
        //    alert("请选择要进行审批的报价单");
        //    return false;
        //}
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        //报价审批通过不让修改
        if (rowid == null) {
            alert("请选择要撤销的报价单");
            return;
        }
        else {
            var rowid = $("#list").jqGrid('getGridParam', 'selrow');
            var OOState = jQuery("#list").jqGrid('getRowData', rowid).OState;
            if (OOState == "审批通过") {
                alert("不能重复提交");
                return;
            }
            if (OOState == "审批中") {
                alert("不能重复提交");
                return;
            }
            var texts = ID + "@" + "报价审批";
            window.parent.OpenDialog("提交审批", "../SalesManage/SubmitApproval?id=" + texts, 700, 500, '');
        }
    });
    $("#btnRZ").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        $('#btnFJ').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('RZJ').style.display = 'block';
        document.getElementById('FJ').style.display = 'none';
    });
    $("#btnPrintOffer").click(function () {
        if (ID == 0) {
            alert("请选择要打印的报价单");
            return;
        }
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var State = jQuery("#list").jqGrid('getRowData', rowid).OState;
        if (State != "审批通过") {
            alert("请先审批报价单然后才能再打印");
            return;
        }
        window.showModalDialog("../SalesManage/PrintOffer?BJID=" + ID, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");

    })

    $("#btnSearch").click(function () {
        LoadSerachInfo();
    })
});

var curPage = 1;
var DcurPage = 1;
var OnePageCount = 5;
var DOnePageCount = 5;
var RcurPage = 1;
var FcunPage = 1;
var ROnePageCount = 5;
var oldSelID = 0;
var ID = 0;
var PID = 0;
var SPID = "";
function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'GetOfferGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },

    }).trigger("reloadGrid");
}
function LoadBasInfo() {
    jQuery("#list").jqGrid({
        url: 'GetOfferGrid',
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
        colNames: ['报价单号', '工程编号', '项目名称', '项目单号', '报价标题', '所属区域', '进度', '报价说明', '报价人', '报价单位', '客户', '客户电话', '报价金额', '报价时间', ''],
        colModel: [
        { name: 'BJID', index: 'BJID', width: 150 },
         { name: 'PlanID', index: 'PlanID', width: 100 },
        { name: 'PlanName', index: 'PlanName', width: 150 },
        { name: 'offerPID', index: 'offerPID', width: 90 },
        { name: 'OfferTitle', index: 'OfferTitle', width: 90 },
        { name: 'BelongArea', index: 'BelongArea', width: 90 },
        { name: 'OState', index: 'OState', width: 70 },
        { name: 'Description', index: 'Description', width: 100 },
        { name: 'OfferContacts', index: 'OfferContacts', width: 80 },
        { name: 'OfferUnit', index: 'OfferUnit', width: 80 },
        { name: 'CuStomer', index: 'CuStomer', width: 100 },
         { name: 'CustomerTel', index: 'CustomerTel', width: 100 },
        { name: 'Total', index: 'Total', width: 150 },
        { name: 'OfferTime', index: 'OfferTime', width: 150, formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' } },
        { name: 'ISF', index: 'ISF', width: 100, hidden: true }
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
            ID = jQuery("#list").jqGrid('getRowData', rowid).BJID//0812k
            select(rowid);
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
            reloadS();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
//加载产品详细
function LoadDetailaa() {
    jQuery("#Detaillist").jqGrid({
        url: 'GetOfferDetailGrid',//'GetOfferInfoGrid'
        datatype: 'json',
        postData: { BJID: ID, curpage: DcurPage, rownum: DOnePageCount },
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
        colNames: ['', '', '', '产品名称', '规格型号', '单位', '数量'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'XID', index: 'XID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'Amount', index: 'Amount', width: 80 }

        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: DOnePageCount,
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
            DID = jQuery("#Detaillist").jqGrid('getRowData', rowid).PID;
            // LoadOrdersInfo();

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
            $("#Detaillist").jqGrid("setGridHeight", $("#pageContent").height() + 10, false);
            $("#Detaillist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadDetail() {

    jQuery("#Detaillist").jqGrid({
        url: 'GetOfferDetailGrid',
        datatype: 'json',
        postData: { BJID: ID, curpage: DcurPage, rownum: DOnePageCount },
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
        colNames: ['', '', '', '产品名称', '规格型号', '单位', '数量'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'XID', index: 'XID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'Amount', index: 'Amount', width: 80 }

        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
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
            $("#Detaillist").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
//加载相关单据
function LoadOrdersInfo() {

    // var PID = jQuery("#list").jqGrid('getRowData', oldSelID).offerPID;//1012k
    PID = ID;
    document.getElementById('div2').style.display = 'block';
    document.getElementById('div1').style.display = 'none';
    //var oID = ID;
    jQuery('#Billlist').jqGrid({
        url: 'GetBJOrders',
        datatype: 'json',
        postData: { PID: PID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['单据编号', '操作'],
        colModel: [
        { name: 'OrderID', index: 'OrderID', width: 150 },
        {
            name: 'DID', index: 'DID', width: 100
        }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#Billlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                //    var id = ids[i];
                //    var curRowData = jQuery("#list").jqGrid('getRowData', id);
                //    var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#rlist").jqGrid('getRowData', id).ID + "' name='cb'/>";
                //    jQuery("#rlist").jqGrid('setRowData', ids[i], { IDCheck: curChk });

                var id = ids[i];
                var Model = jQuery("#Billlist").jqGrid('getRowData', id);
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
            //DID = jQuery("#loadlist").jqGrid('getRowData', rowid).PID;
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
            $("#loadlist").jqGrid("setGridWidth", $("#bottom").width() - 15, false);
        }
    });
}


//1017k相关单据New
function LoadOffersBill() {
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "ProjectBasInfoRelBill",
        type: "post",
        data: { ID: PID },
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
    //window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 800, 450);
    var id = SDI;
    var s = id.substr(0, 2);
    if (s == "BA") { window.parent.parent.OpenDialog("详细", "../SalesManage/ProjectBill?ID=" + id, 800, 450); }
    else if (s == "BJ") { window.parent.parent.OpenDialog("详细", "../SalesManage/OfferBill?ID=" + id, 800, 450); }
    else if (s == "DH") { window.parent.parent.OpenDialog("详细", "../SalesManage/OrdersInfoBill?ID=" + id, 800, 450); }
    else if (s == "FH") { window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 800, 450); }
    else if (s == "HK") { window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 800, 450); }
    else if (s == "TH") { window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 800, 450); }

}

//1017k相关单据New

//查询
function LoadSerachInfo() {
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
        var ProjectName = $('#PlanName').val();
        var PlanID = $('#PlanID').val();
        var OfferTitle = $('#OfferTitle').val();
        var BelongArea = $('#BelongArea').val();
        var StartDate = $('#StartDate').val();
        var EndDate = $('#EndDate').val();
        var OfferContacts = $('#Manager').val();
        var State = $("input[name='State']:checked").val();
        var HState = $("input[name='HState']:checked").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetSearchOfferGrid',
            datatype: 'json',
            postData: {
                curpage: 1, rownum: OnePageCount, PlanName: ProjectName, PlanID: PlanID,
                OfferTitle: OfferTitle, BelongArea: BelongArea, StartDate: StartDate,
                EndDate: EndDate, Manager: OfferContacts, State: State, HState: HState
            },
            loadonce: false

        }).trigger("reloadGrid");//重新载入
    }

}

function reloadS() {
    var ProjectName = $('#PlanName').val();
    var PlanID = $('#PlanID').val();
    var OfferTitle = $('#OfferTitle').val();
    var BelongArea = $('#BelongArea').val();
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    var OfferContacts = $('#Manager').val();
    var State = $("input[name='State']:checked").val();
    var HState = $("input[name='HState']:checked").val();
    $("#list").jqGrid('setGridParam', {
        url: 'GetSearchOfferGrid',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, PlanName: ProjectName, PlanID: PlanID,
            OfferTitle: OfferTitle, BelongArea: BelongArea, StartDate: StartDate,
            EndDate: EndDate, Manager: OfferContacts, State: State, HState: HState
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}

//撤销
function Cancel(ID) {
    $.ajax({
        url: "CancelOffer",
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
                alert("撤销失败");
            }
        }
    });
}

function select(rowid) {
    ID = jQuery("#list").jqGrid('getRowData', rowid).BJID;
    PID = jQuery("#list").jqGrid('getRowData', rowid).offerPID;
    DcurPage = 1;
    $.ajax({
        url: "GetContractSPID",
        type: "post",
        data: { cid: ID },
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
    LoadOffersBill();
    reload2();
    reload3();
    reloadFJ();

}
function reload1() {
    $("#Detaillist").jqGrid('setGridParam', {
        url: 'GetOfferDetailGrid',
        datatype: 'json',
        type: "post",
        async: false,
        postData: { BJID: ID, curpage: DcurPage, rownum: DOnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
function reload2() {
    $("#Billlist").jqGrid('setGridParam', {
        url: 'GetBJOrders',
        datatype: 'json',
        postData: { PID: PID, curpage: DcurPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

//var webkey = "报价审批";
function reload3() {
    $("#list2").jqGrid('setGridParam', {
        url: 'GetLogGrid',
        datatype: 'json',
        postData: { curpage: RcurPage, rownum: ROnePageCount, ID: ID },

    }).trigger("reloadGrid");
}

function jq1() {

    jQuery("#list2").jqGrid({
        url: 'GetLogGrid',
        datatype: 'json',
        postData: { curpage: RcurPage, rownum: ROnePageCount, ID: ID },
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
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        // caption: '审批情况表',

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
            if (pgButton == "next_pager3") {
                if (curPage == $("#list2").getGridParam("lastpage"))
                    return;
                curPage = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPage == 1)
                    return;
                curPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPage = 1;
            }
            else {
                curPage = $("#pager3 :input").val();
            }
            reload3();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

//附件
function LoadFJ() {
    jQuery("#list4").jqGrid({
        url: 'GetUploadFileGrid',
        datatype: 'json',
        postData: { curpage: FcunPage, rownum: ROnePageCount, CID: ID },
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
        colNames: ['','报价编号', '文件', '操作人', '操作时间', '操作', '操作'],
        colModel: [
            {name:'ID',index:'ID',width:90,hidden:true},
        { name: 'CID', index: 'CID', width: 90 },
        { name: 'FileName', index: 'FileName', width: 90 },
        { name: 'CreateUser', index: 'CreateUser', width: 90 },
        { name: 'CreateTime', index: 'CreateTime', width: 90 },
        { name: 'IDCheck', index: 'Id', width: 50 },
        { name: 'deCheck', index: 'Id', width: 50 }
        ],
        pager: jQuery('#pager4'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        // caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list4").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list4").jqGrid('getRowData', id);
                var curChk = "<a style='color:blue;cursor:pointer' onclick=\"deleteFile('" + jQuery("#list4").jqGrid('getRowData', id).ID + "')\">删除</a>";
                var curChk1 = "<a style='color:blue;cursor:pointer' onclick=\"DownloadFile('" + jQuery("#list4").jqGrid('getRowData', id).ID + "')\">下载</a>";
                jQuery("#list4").jqGrid('setRowData', ids[i], { IDCheck: curChk, deCheck: curChk1 });
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
            if (pgButton == "next_pager4") {
                if (curPage == $("#list4").getGridParam("lastpage"))
                    return;
                curPage = $("#list4").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager4") {
                curPage = $("#list4").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager4") {
                if (curPage == 1)
                    return;
                curPage = $("#list4").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager4") {
                curPage = 1;
            }
            else {
                curPage = $("#pager4 :input").val();
            }
            reloadFJ();
        },
        loadComplete: function () {
            $("#list4").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list4").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}


function reloadFJ()
{
    $("#list4").jqGrid('setGridParam', {
        url: 'GetUploadFileGrid',
        datatype: 'json',
        postData: { CID: ID, curpage: FcunPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}


function DownloadFile(id) {
    window.open("DownLoad2?id=" + id);
}
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
                    reloadFJ();
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
    }
}
