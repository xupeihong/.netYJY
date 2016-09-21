
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var CID;
var curPage1 = 1;
var OnePageCount1 = 20;
$(document).ready(function () {
    $("#GXInfo").html("");
    $("#pageContent").height($(window).height());
    $("#pageContent").width($(window).width() - 5);
    $("#search").width($("#bor").width() - 15);

    jq();
    jq1();
    jq2();
    $("#DY").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        var SP = Model.State;
        if (SP == '2') {
            var rowid = $("#list").jqGrid('getGridParam', 'selrow');
            if (rowid == null) {
                alert("请选择要打印的请购单");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).DDID;
            window.parent.parent.OpenDialog("选择供货商", "../PPManage/PrintSupplier?DDID=" + texts + "", 500, 300);
        }
        else {

            alert("订单未通过审批 不可打印");
            return;

        }
    });
    $("#rzxq").click(function () {
        RZ();
        this.className = "btnTw";
        $('#DJ').attr("class", "btnTh");
        $('#WP').attr("class", "btnTh");
        $("#bor1").css("display", "none");
        $("#danju").css("display", "none");
        $("#bor2").css("display", "");
        $("#bor3").css("display", "none");

    });
    $("#WP").click(function () {


        this.className = "btnTw";
        $('#DJ').attr("class", "btnTh");
        $('#rzxq').attr("class", "btnTh");
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var DDID = Model.DDID;
            var state = Model.DDState;
            if (state == 'L') {
                $("#bor1").css("display", "");
            }
            else {
                $("#bor3").css("display", "");
            }
        }
        $("#danju").css("display", "none");
        $("#bor2").css("display", "none");

    });
    $("#DJ").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {

            return;
        }
        else {
            var DDID = Model.DDID;

            this.className = "btnTw";
            $('#WP').attr("class", "btnTh");
            $('#rzxq').attr("class", "btnTh");

            $("#bor1").css("display", "none");
            $("#danju").css("display", "");
            $("#bor2").css("display", "none");
            $("#bor3").css("display", "none");
            DGXQ(DDID);
        }

    });
    $("#XG").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var DDID = Model.DDID;
            var SP = Model.State;
            if (SP == '2') {
                alert("订单通过审批 不可修改");
                return;
            }
            else {
                $.ajax({
                    url: "SelectGoodsDDID",
                    type: "post",
                    async: false,
                    data: { DDID: DDID },
                    dataType: "Json",
                    success: function (data) {
                        var json = eval(data.datas);
                        var state = Model.DDState;
                        if (json.length > 0) {
                            for (var i = 0; i < json.length; i++) {
                                if (json[i].SJFK > 0 || json[i].ActualAmount > 0) {
                                    alert("此订购单以有付款或收货商品 不可修改");
                                    return;
                                }
                            }
                            if (state == 'L') {
                                window.parent.parent.OpenDialog("修改", "../PPManage/SanUpdateDD?DDID=" + DDID + "", 1155, 650);
                            }
                            else {
                                window.parent.parent.OpenDialog("修改", "../PPManage/UpdateDDXX?DDID=" + DDID + "", 1155, 650);
                            }

                        }

                    }
                });
            }


        }
    });
    $("#XGEr").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var DDID = Model.DDID;
            $.ajax({
                url: "SelectGoodsDDID",
                type: "post",
                async: false,
                data: { DDID: DDID },
                dataType: "Json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            if (json[i].SJFK > 0 || json[i].ActualAmount > 0) {
                                alert("此订购单以有付款或收货商品 不可修改");
                                return;
                            }
                        }
                        window.parent.parent.OpenDialog("修改", "../PPManage/ErUpdateDD?DDID=" + DDID + "", 1155, 650);
                    }
                }

            });
        }
    });
    $("#XGSan").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var DDID = Model.DDID;
            $.ajax({
                url: "SelectGoodsDDID",
                type: "post",
                async: false,
                data: { DDID: DDID },
                dataType: "Json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            if (json[i].SJFK > 0 || json[i].ActualAmount > 0) {
                                alert("此订购单以有付款或收货商品 不可修改");
                                return;
                            }
                        }
                        window.parent.parent.OpenDialog("修改", "../PPManage/SanUpdateDD?DDID=" + DDID + "", 1155, 650);
                    }
                }

            });
        }
    });
    $("#CX").click(function () {

        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var DDID = Model.DDID;

            $.ajax({
                url: "SelectGoodsDDID",
                type: "post",
                async: false,
                data: { DDID: DDID },
                dataType: "Json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            if (json[i].SJFK > 0 || json[i].ActualAmount > 0) {
                                alert("此订购单以有付款或收货商品 不可撤销");
                                return;
                            }
                        }

                        isConfirm = confirm("确定要撤销吗")
                        if (isConfirm == false) {
                            return false;
                        }
                        else {
                            $.ajax({
                                url: "UpdateDDValidate",
                                type: "Post",
                                data: {
                                    DDID: DDID
                                },
                                async: false,
                                success: function (data) {
                                    if (data.success == true) {
                                        window.parent.frames["iframeRight"].reload();
                                        alert("成功");
                                    }
                                    else {
                                        alert("失败");
                                    }
                                }
                            });
                        }
                    }

                }
            });

        }

    });
    $("#XX").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var DDID = Model.DDID;
            var state = Model.DDState;
            if (state == "C") {
                window.parent.parent.OpenDialog("详情", "../PPManage/DetailsDD?DDIDXQ=" + DDID + "", 1105, 680);
            }
            else {
                window.parent.parent.OpenDialog("详情", "../PPManage/DetailsDDSan?DDIDXQ=" + DDID + "", 1105, 680);
            }

        }
    });
    $("#XXEr").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var DDID = Model.DDID;
            window.parent.parent.OpenDialog("详情", "../PPManage/ErDetailsDD?DDIDXQ=" + DDID + "", 1105, 680);

        }
    });
    $("#XXSan").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var DDID = Model.DDID;
            window.parent.parent.OpenDialog("详情", "../PPManage/DetailsDDSan?DDIDXQ=" + DDID + "", 1105, 680);

        }
    });
    $("#SP").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行提交审批的项目单");
            return;
        }
        else {
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);

            if (Model.State == "") {
                var texts = jQuery("#list").jqGrid('getRowData', rowid).DDID + "@" + "订购审批";
                window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
            }
            else {
                alert("该订购单已提交审批");
            }

        }
    });
    $("#XGWJ").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var DDID = Model.DDID;
            //window.parent.parent.OpenDialog("上传", "../PPManage/AddFile?PID=" + DDID + "", 500, 300);
            window.parent.parent.OpenDialog("上传", "../PPManage/InsertBiddingNew?PID=" + DDID + "", 500, 300);

        }
    });
    $("#FKS").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var DDID = Model.DDID;
            var state = Model.DDState;
            var SP = Model.State;
            if (SP == '2') {
                if (state == 'L') {
                    window.parent.OpenDialog("付款", "../PPManage/Payments?DDID=" + DDID + "", 1105, 680);
                }
                else {
                    window.parent.OpenDialog("付款", "../PPManage/PaymentCP?DDID=" + DDID + "", 1105, 680);
                }
            }
            else {
                alert("审批未通过,不能付款");
                return;
            }
        }
    });
});

function DGXQ(DDID) {
    $("#GXInfo").html("");
    //var dataSel = jQuery("#list").jqGrid('getGridParam');
    //var ids = dataSel.selrow;
    //var Model = jQuery("#list").jqGrid('getRowData', ids);
    //if (ids == null) {
    //    alert("请选择要操作的行");
    //    return;
    //}
    //else {


    $.ajax({
        url: "SelectSHDDID",
        type: "post",
        data: { DDID: DDID },
        dataType: "json",
        success: function (data) {
            var jsons = eval(data.datass);
            if (jsons != null) {
                
                for (var i = 0; i < jsons.length; i++) {
                    var html = "";
                    html += '<tr>'
                    html += '<td ><lable class="labRowNumber" id="SHID' + i + '">' + jsons[i].SHID + '</lable> </td>';
                    html += '<td > <input class="btn" type="button" name="' + i + '" value="详情" onclick="shxiangqing(this)" /></td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
            }
        }
    });

    $.ajax({
        url: "SelectFKDDID",
        type: "post",
        data: { DDID: DDID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json != null) {
            
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html += '<tr>'
                    html += '<td ><lable class="labRowNumber" id="PayId' + i + '">' + json[i].PayId + '</lable> </td>';
                    html += '<td > <input class="btn" type="button" name="' + i + '" value="详情" onclick="payxiangqing(this)" /></td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
            }
        }
    });



    //}

}
function rkxiangqing() {
    var RKID = document.getElementById("RKID").innerHTML;
    window.parent.OpenDialog("详情", "../PPManage/DetailsRK?RKIDXQ=" + RKID + "", 1050, 650);
}
function payxiangqing(id) {
    var name = id.name;

    var PayId = document.getElementById("PayId" + name).innerHTML;
    window.parent.OpenDialog("详情", "../PPManage/DetailsFK?PayIdXQ=" + PayId + "", 1050, 650);
}
function shxiangqing(id) {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    }
    else {
        var name = id.name;
        var SHID = document.getElementById("SHID" + name).innerHTML;
        var DDID = Model.DDID;
        window.parent.OpenDialog("详情", "../PPManage/DetailsSH?SHIDXQ=" + SHID + "&DDID=" + DDID + "", 1050, 650);
    }
}


function shouhuo() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    }
    else {
        var DDID = Model.DDID;
        var state = Model.DDState;
        var SP = Model.State;
        if (SP == '2') {
            if (state == 'L') {
                window.parent.OpenDialog("收货", "../PPManage/ReceiptGoods?DDID=" + DDID + "", 1105, 580);
            }
            else {
                window.parent.OpenDialog("收货", "../PPManage/InsertCPSH?DDID=" + DDID + "", 1105, 580);
            }
        }
        else {
            alert("审批未通过,不能收货");
            return;
        }

    }
}
function ruku() {
    var dataSel = jQuery("#list1").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list1").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    }
    else {

        var selectedIds = $("#list1").jqGrid("getGridParam", "selarrrow");
        var texts = "";
        for (var i = 0 ; i < selectedIds.length; i++) {
            //if (jQuery("#list1").jqGrid('getCell', selectedIds[i], 'Text') != "已入库") {
            texts += jQuery("#list1").jqGrid('getCell', selectedIds[i], 'DID') + ",";
            //}
        }
        window.parent.OpenDialog("", "../PPManage/AddStorage?texts=" + texts + "", 1050, 650);
    }
}

function fukuan() {


    var dataSel = jQuery("#list1").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list1").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请在下方物品详细列表中选择要付款的物品");
        return;
    }
    else {

        var selectedIds = $("#list1").jqGrid("getGridParam", "selarrrow");
        var texts = "";
        for (var i = 0 ; i < selectedIds.length; i++) {
            var a = jQuery("#list1").jqGrid('getCell', selectedIds[i], 'TotalNoTax');
            var b = jQuery("#list1").jqGrid('getCell', selectedIds[i], 'SJFK');
            if (a == b) {
                alert("选取商品中已全额付款商品！");
                return;
            }

            texts += jQuery("#list1").jqGrid('getCell', selectedIds[i], 'DID') + ",";
        }
        window.parent.OpenDialog("", "../PPManage/Payment?texts=" + texts + "", 1050, 650);
    }
}

function jq() {

    var DDID = $('#DDID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var ArrivalStatus = $('#ArrivalStatus').val();
    var PayStatus = $('#PayStatus').val();
    var State = $("#State").val();
    var DeliveryLimit = $("#DeliveryLimit").val();

    var DeliveryLimit1 = $("#DeliveryLimit1").val();

    jQuery("#list").jqGrid({
        url: 'SelectDD',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, DDID: DDID, Begin: Begin, DeliveryLimit: DeliveryLimit, End: End, ArrivalStatus: ArrivalStatus, PayStatus: PayStatus, State: State, DeliveryLimit1: DeliveryLimit1 },
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
        colNames: ['', '', '订购单号', '订购人', '单位', '订购日期', '交货期限', '审批状态', '收获状态', '付款状态'],
        colModel: [
           { name: 'State', index: 'State', width: 150, hidden: true },
                { name: 'DDState', index: 'DDState', width: 150, hidden: true },
        { name: 'DDID', index: 'DDID', width: 150 },

        //{ name: 'PayStatusdesc', index: 'PayStatusdesc', width: 100 },
        { name: 'OrderContacts', index: 'OrderContacts', width: 100 },
        { name: 'DeptName', index: 'DeptName', width: 100 },
        { name: 'OrderDate', index: 'OrderDate', width: 150 },
        { name: 'DeliveryLimit', index: 'DeliveryLimit', width: 100 },
        { name: 'sp', index: 'sp', width: 150 },
          { name: 'SH', index: 'SH', width: 150 },
            { name: 'FK', index: 'FK', width: 150 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        //, hidden: true
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;

            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {

                return;
            }
            else {
                $('#WP').attr("class", "btnTw");
                $('#DJ').attr("class", "btnTh");
                $('#rzxq').attr("class", "btnTh");
                $("#danju").css("display", "none");
                $("#bor2").css("display", "none");
                var DDID = Model.DDID;
                var state = Model.DDState;
                if (state == 'L') {
                    $("#bor1").css("display", "");
                    reload1(DDID);
                }
                else {
                    $("#bor3").css("display", "");
                    reload2(DDID);
                }

            }

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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload() {
    var DDID = $('#DDID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var ArrivalStatus = $('#ArrivalStatus').val();
    var PayStatus = $('#PayStatus').val();
    var State = $("#State").val();
    var DeliveryLimit = $("#DeliveryLimit").val();
    $("#list").jqGrid('setGridParam', {
        url: 'SelectDD',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, DDID: DDID, Begin: Begin, End: End, DeliveryLimit: DeliveryLimit, ArrivalStatus: ArrivalStatus, PayStatus: PayStatus, State: State },

    }).trigger("reloadGrid");
}

function jq1(DDID) {

    $("#bor3").css("display", "none")
    $("#bor1").css("display", "")
    jQuery("#list1").jqGrid({
        url: 'SelectDDGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, DDID: DDID },
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
        colNames: ['', '编号', '物品名称', '规格型号', '单位', '数量', '已收货数量', '订购单价', '订购金额', '已付款'],
        colModel: [
 { name: 'DID', index: 'DID', width: 0, hidden: true },
        { name: 'DDID', index: 'DDID', width: 180 },
        { name: 'OrderContent', index: 'OrderContent', width: 110 },
        { name: 'Specifications', index: 'Specifications', width: 80 },
        { name: 'Unit', index: 'Unit', width: 60 },
        { name: 'Amount', index: 'Amount', width: 80 },
        { name: 'ActualAmount', index: 'ActualAmount', width: 80 },
        { name: 'UnitPriceNoTax', index: 'UnitPriceNoTax', width: 90 },
        { name: 'TotalNoTax', index: 'TotalNoTax', width: 90 },

         { name: 'SJFK', index: 'SJFK', width: 90 }
          //{ name: 'RKState', index: 'RKState', width: 90 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '物品详细',
        //multiselect: true,
        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            var dataSel = jQuery("#list1").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list1").jqGrid('getRowData', ids);
            var DDID = Model.DDID;
            reload1(DDID);
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() - 450, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}

function reload1(DDID) {
    $("#bor3").css("display", "none")
    $("#bor1").css("display", "")
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'SelectDDGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, DDID: DDID },

    }).trigger("reloadGrid");
}


function jq2(DDID) {

    $("#bor3").css("display", "")
    $("#bor1").css("display", "none")
    jQuery("#list3").jqGrid({
        url: 'SelectCPXX',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, DDID: DDID },
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
        colNames: ['', '编号', '物品名称', '规格型号', '单位', '数量', '税前单价', '税前总价', '税后单价', '税后总价'],
        colModel: [
        { name: 'ID', index: 'ID', width: 0, hidden: true },
        { name: 'DDID', index: 'DDID', width: 180 },
        { name: 'Name', index: 'Name', width: 110 },
        { name: 'Spc', index: 'Spc', width: 80 },
        { name: 'Units', index: 'Units', width: 60 },
        { name: 'Num', index: 'Num', width: 80 },
        { name: 'Price2', index: 'Price2', width: 80 },
        { name: 'Price2s', index: 'Price2s', width: 80 },
        { name: 'UnitPrice', index: 'UnitPrice', width: 80 },
        { name: 'UnitPrices', index: 'UnitPrices', width: 80 }
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '物品详细',
        //multiselect: true,
        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list3").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager3 :input").val();
            }
            var dataSel = jQuery("#list3").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list3").jqGrid('getRowData', ids);
            var DDID = Model.DDID;
            reload2(DDID);
        },
        loadComplete: function () {
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload2(DDID) {
    $("#bor3").css("display", "")
    $("#bor1").css("display", "none")
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list3").jqGrid('setGridParam', {
        url: 'SelectCPXX',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, DDID: DDID },

    }).trigger("reloadGrid");
}

function SearchOut() {

    var strDateStart = $('#Begin').val();
    var strDateEnd = $('#End').val();
    //if (strDateStart == "" && strDateEnd == "" && strDateStart1 == "" && strDateEnd1 == "") {

    //    getSearch();
    //}
    //else {
    var strSeparator = "-"; //日期分隔符
    var strDateArrayStart;
    var strDateArrayEnd;
    var intDay;
    strDateArrayStart = strDateStart.split(strSeparator);
    strDateArrayEnd = strDateEnd.split(strSeparator);
    var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
    var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);

    if (strDateS <= strDateE) {
        getSearch();
    }
    else if (strDateS >= strDateE) {
        alert("截止日期不可以小于或等于开始日期");
        $("#End").val("");
    }
    else (strDateS == "" || strDateE == "")
    {
        getSearch();
    }

}


function getSearch() {
    curRow = 0;
    curPage = 1;

    var DDID = $('#DDID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var OrderContent = $('#OrderContent').val();
    var Supplier = $("#Supplier").val();
    var ArrivalStatus = $('#ArrivalStatus').val();
    var DeliveryLimit = $("#DeliveryLimit").val();
    var PayStatus = $('#PayStatus').val();
    var State = $("#State").val();

    var DeliveryLimit1 = $("#DeliveryLimit1").val();

    $("#list").jqGrid('setGridParam', {
        url: 'SelectDD',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, DDID: DDID, OrderContent: OrderContent, Begin: Begin, End: End, Supplier: Supplier, ArrivalStatus: ArrivalStatus, DeliveryLimit: DeliveryLimit, PayStatus: PayStatus, State: State, DeliveryLimit1: DeliveryLimit1 },
        loadonce: false,

    }).trigger("reloadGrid");//重新载入

}
function RZ() {
    var Type = "订购";
    jQuery("#list2").jqGrid({
        url: 'SelectRZ',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },
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
        colNames: ['操作ID', '操作', '状态', '时间', '操作人'],
        colModel: [
              { name: 'RelevanceID', index: 'RelevanceID', width: 150 },
        { name: 'LogTitle', index: 'LogTitle', width: 150 },
          { name: 'LogContent', index: 'LogContent', width: 150 },
        { name: 'LogTime', index: 'LogTime', width: 150 },
        { name: 'LogPerson', index: 'LogPerson', width: 100 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        onSelectRow: function (rowid, status) {
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage1 == $("#list2").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage1 = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager2 :input").val();
            }
            list2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });

}

function list2() {

    if ($('.field-validation-error').length == 0) {
        var Type = "订购";
        $("#list2").jqGrid('setGridParam', {
            url: 'SelectRZ',
            datatype: 'json',
            postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },

        }).trigger("reloadGrid");
    }

}
