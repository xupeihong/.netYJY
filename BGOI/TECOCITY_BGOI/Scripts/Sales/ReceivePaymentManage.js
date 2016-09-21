$(document).ready(function () {
    LoadReceivePayment();
    GetReceiveDetail();
    LoadReceiveBill();
    LoadFJ();
    document.getElementById('div1').style.display = 'block';
    document.getElementById('div2').style.display = 'none';
    document.getElementById('FJ').style.display = 'none';
    $("#DetailXX").click(function () {
        this.className = "btnTw";
        $('#BillXX').attr("class", "btnTh");
        $('#btnFJ').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('FJ').style.display = 'none';
    });
    $("#BillXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#btnFJ').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'block';
        document.getElementById('FJ').style.display = 'none';
    });
    $("#btnFJ").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        $('#BillXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('FJ').style.display = 'block';
    })
    $("#btnSearch").click(function () {
        LoadSearchRecevie();
    })

    $("#btnUpdate").click(function () {
        if (ID == 0)
        {
            alert("请选择列表数据");
            return;
        }
        window.parent.parent.OpenDialog("修改", "../SalesManage/UpdateReceivePayment?RID=" + ID, 750, 450);

    });

    $("#btnCancel").click(function () {
        if (ID == 0) {
            alert("请选择列表数据");
            return;
        }
        isConfirm = confirm("是否撤销此条回款？")
        if (isConfirm == false) {
            return false;
        }
        CancelReceivePayment();
    });

    $("#btnPrint").click(function () {
        if (ID == 0) {
            alert("请选择要打印的回款单");
            return;
        }
        window.showModalDialog("../SalesManage/PrintReceivePayment?OrderID=" + OrderID, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");


    });
});
var ID = 0;
var OrderID = '';
var curPage = 1;
var FcunPage = 1;
var DcurPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
function LoadReceivePayment()
{
    jQuery("#list").jqGrid({
        url: 'LoadReceivePayment',
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
        colNames: ['回款单号','合同编号', '关联订单', '关联订单内容', '回款金额', '回款方式','回款日期'],
        colModel: [
        { name: 'RID', index: 'RID', width: 200 },
        { name: 'ContractID', index: 'ContractID', width: 200 },
        { name: 'OrderID', index: 'OrderID', width: 200 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Amount', index: 'Amount', width: 80 },
        { name: 'Mothods', index: 'Mothods', width: 80 },
        { name: 'PayTime', index: 'PayTime', width:200 }
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
            ID = jQuery("#list").jqGrid('getRowData', rowid).RID//0812k
            OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
            select(rowid);
            $("#Billlist tbody").html("");
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

function reload() {

    $("#list").jqGrid('setGridParam', {
        url: 'LoadReceivePayment',
        datatype: 'json',
        postData: {curpage: curPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
function select(rowid) {
    ID = jQuery("#list").jqGrid('getRowData', rowid).RID;
    OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
    reload1();
    reload2();
    reloadFJ();
}
function reload1() {
    $("#Detaillist").jqGrid('setGridParam', {
        url: 'LoadOrderDetail',
        datatype: 'json',
        postData: { OrderID: OrderID, curpage: DcurPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
function reload2() {
    //$("#Billlist").jqGrid('setGridParam', {
    //    url: 'LoadReceiveBill',
    //    datatype: 'json',
    //    postData: { OID: OrderID, curpage: curPage, rownum: OnePageCount }, //,jqType:JQtype

    //}).trigger("reloadGrid");

    LoadReceiveBill();
}
function GetReceiveDetail()
{
    jQuery("#Detaillist").jqGrid({
        url: 'LoadOrderDetail',
        datatype: 'json',
        postData: { OrderID: OrderID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['', '', '', '物品编码', '物品名称', '规格型号', '单位', '数量', '生产厂家', '单价', '合计', '技术参数', '提交时间', '状态', '备注'],
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
        { name: 'State', index: 'State', width: 80 },
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

function GetReceiveBill()
{
    jQuery("#Billlist").jqGrid({
        url: 'LoadReceivePaymentBill',
        datatype: 'json',
        postData: { OID: OrderID, curpage: curPage, rownum: OnePageCount },
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
      //{ name: '', index: '', width: 100 },
      { name: 'ID', index: 'ID', width: 100 },
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
            $("#Billlist").jqGrid("setGridHeight", $("#pageContent").height() + 50, false);
            $("#Billlist").jqGrid("setGridWidth", $("#bor").width(), false);
        }
    });
}
function LoadSearchRecevie() {
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    if (StartDate == "" && EndDate == "") {
        GetSearchRecevie();
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
            GetSearchRecevie();
        }
        else {
            alert("截止日期不能小于开始日期！");
            $("#End").val("");
            return false;
        }
    }


}
function GetSearchRecevie()
{
    if ($('.field-validation-error').length == 0) {
        var OrderID = $("#OrderID").val();
        var OrderUnit = $("#OrderUnit").val();
        var UseUnit = $("#UseUnit").val();
        var OrderContent = $("#MainContent").val(); //$('#SpecsModels').click.Text();
        var StartDate = $('#StartDate').val();
        var EndDate = $('#EndDate').val();
        $("#list").jqGrid('setGridParam', {
            url: 'LoadReceivePayment',
            datatype: 'json',
            postData: {
                curpage: curPage, rownum: OnePageCount, OrderID: OrderID, OrderUnit: OrderUnit,
                UseUnit: UseUnit, MainContent: OrderContent, StartDate: StartDate, EndDate: EndDate
            },
            loadonce: false

        }).trigger("reloadGrid");//重新载入

    }
}

function LoadReceiveBill()
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

                $("#ReceiveBill").val("");
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html += '<tr>'
                    var s = json[i].ID.substr(0, 2);
                    if (s == "BA") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">备案基本信息</lable> </td>';
                    }
                    if(s=="BJ")
                    {
                       html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">报价信息</lable> </td>';
                    }
                    if (s == "DH")
                    {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">订货信息</lable> </td>';
                    }
                    if (s == "FH")
                    {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">发货信息</lable> </td>';
                    }
                    if(s=="HK")
                    {
                      html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">报价信息</lable> </td>';
                    }
                    if (s == "TH")
                    {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">退换信息</lable> </td>';
                    }
                
                    html += '<td ><lable class="labID' + rowCount + ' " id="ID' + rowCount + '">' + json[i].ID+ '</lable> </td>';

                    html += '<td ><a href="#" style="color:blue" onclick=GetXX("' + json[i].ID + '")>详情</a></td>';
                    html += '</tr>'
                    $("#ReceiveBill").append(html);
                }


            }
        }
    })
}

function GetXX(SDI) {
    var id = SDI;
 window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 500, 450);

  
}

function CancelReceivePayment()
{
    $.ajax({
        url: "CancelReceivePayment",
        type: "Post",
        data: {
            ID: ID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("撤销完成！");
            }
            else {
                alert("撤销失败-" + data.msg);
            }
        }
    });
}

//附件
function LoadFJ() {
    jQuery("#list4").jqGrid({
        url: 'GetUploadFileGrid',
        datatype: 'json',
        postData: { curpage: FcunPage, rownum: OnePageCount, CID: ID },
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
        colNames: ['', '报价编号', '文件', '操作人', '操作时间', '操作', '操作'],
        colModel: [
            { name: 'ID', index: 'ID', width: 90, hidden: true },
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
                if (FcunPage == $("#list4").getGridParam("lastpage"))
                    return;
                FcunPage = $("#list4").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager4") {
                FcunPage = $("#list4").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager4") {
                if (FcunPage == 1)
                    return;
                FcunPage = $("#list4").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager4") {
                FcunPage = 1;
            }
            else {
                FcunPage = $("#pager4 :input").val();
            }
            reloadFJ();
        },
        loadComplete: function () {
            $("#list4").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list4").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}


function reloadFJ() {
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
