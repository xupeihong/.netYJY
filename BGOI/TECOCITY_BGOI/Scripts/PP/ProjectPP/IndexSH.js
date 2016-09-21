var curPage = 1;
var OnePageCount = 10;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var SHID;
var curPage1 = 1;
var OnePageCount1 = 20;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#pageContent").width($(window).width() - 5);
    $("#search").width($("#bor").width() - 15);
    jq();
    jq1();
    jq2();
    $("#WP").click(function () {

        this.className = "btnTw";

        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var Remark = Model.Remark;
            if (Remark == 'L') {
                $("#bor1").css("display", "");
                $("#bor3").css("display", "none");
            }
            else {
                $("#bor3").css("display", "");
                $("#bor1").css("display", "none");
            }
        }
        $('#DJ').attr("class", "btnTh");
        $('#rzxq').attr("class", "btnTh");
        $("#bor1").css("display", "");
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
            var SHID = Model.SHID;
            this.className = "btnTw";
            $('#WP').attr("class", "btnTh");
            $('#rzxq').attr("class", "btnTh");
            $("#bor1").css("display", "none");
            $("#danju").css("display", "");
            $("#bor2").css("display", "none");
            $("#bor3").css("display", "none");
            DJXQ(SHID);
        }
    });

    $("#TH").click(function () {
        var dataSel = jQuery("#list1").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list1").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请在下方物品详细列表中选择要退货的物品");
            return;
        }
        else {
            var selectedIds = $("#list1").jqGrid("getGridParam", "selarrrow");
            var texts = "";
            for (var i = 0 ; i < selectedIds.length; i++) {
                var a = jQuery("#list1").jqGrid('getCell', selectedIds[i], 'Amount');
                var b = jQuery("#list1").jqGrid('getCell', selectedIds[i], 'THAmount');
                if (a == b) {
                    alert("选择商品中已有全部退货商品");
                    return;
                }
                texts += jQuery("#list1").jqGrid('getCell', selectedIds[i], 'DID') + ",";

            }
            texts = texts.substr(0, texts.length - 1);
            window.parent.parent.OpenDialog("退货", "../PPManage/ReturnGoods?texts=" + texts + "", 950, 550);
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

    $("#DY").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要打印的请购单");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).SHID;
        var url = "PrintSH?id=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });

    $("#RK").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        var SHID = Model.SHID;
        var XXID = Model.XXID;

        $.ajax({
            url: "SelectshJJD",
            type: "post",
            data: { shid: SHID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json != null) {
                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            if (json[i].Warehouse == "" || json[i].Warehouse == null) {
                                alert("库管员未审核！！！");
                                return;
                            }
                            else {
                                if (XXID == 'L') {

                                    window.parent.parent.OpenDialog("入库", "../PPManage/AddStorage?SHID=" + SHID + "", 950, 550);
                                }
                                else {
                                    //window.parent.parent.OpenDialog("入库", "../PPManage/AddStorageer?SHID=" + SHID + "", 950, 550);
                                    window.parent.parent.OpenDialog("入库", "../PPManage/AddStorage?SHID=" + SHID + "", 950, 550);
                                }
                            }
                        }
                    }
                    else {
                        alert("收货单没有生成交接单！！！");
                        return;
                    }
                }
            }
        });

    });


    $("#RKS").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        var SHID = Model.SHID;
        var XXID = Model.XXID;

        if (XXID == 'L') {

            window.parent.parent.OpenDialog("入库", "../PPManage/AddStorage?SHID=" + SHID + "", 950, 550);
        }
        else {
            window.parent.parent.OpenDialog("入库", "../PPManage/AddStorageer?SHID=" + SHID + "", 950, 550);
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
            var SHID = Model.SHID;
            var DDID = Model.DDID;
            var XXID = Model.XXID;

            $.ajax({
                url: "SelectshJJD",
                type: "post",
                async: false,
                data: { SHID: SHID },
                dataType: "Json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        alert("此收货单已生成交接单！！！");
                        return;
                    }
                    else {


                        if (XXID == 'L') {
                            $.ajax({
                                url: "SelectSHXX",
                                type: "post",
                                async: false,
                                data: { SHID: SHID },
                                dataType: "Json",
                                success: function (data) {
                                    var json = eval(data.datas);
                                    if (json.length > 0) {
                                        for (var i = 0; i < json.length; i++) {
                                            if (json[i].SHStates > 0) {
                                                alert("此收货单以入库 不可修改");
                                                return;
                                            }
                                        }
                                        window.parent.OpenDialog("", "../PPManage/UpdateSHXX?SHID=" + SHID + "", 1050, 650);

                                    }
                                }
                            });
                        }
                        else {
                            $.ajax({
                                url: "SelectSHXX",
                                type: "post",
                                async: false,
                                data: { SHID: SHID },
                                dataType: "Json",
                                success: function (data) {
                                    var json = eval(data.datas);
                                    if (json.length > 0) {
                                        for (var i = 0; i < json.length; i++) {
                                            if (json[i].SHStates > 0) {
                                                alert("此收货单以入库 不可修改");
                                                return;
                                            }
                                        }
                                        window.parent.OpenDialog("", "../PPManage/UpdateSHXXer?SHID=" + SHID + "&DDID=" + DDID + "", 1050, 650);

                                    }
                                }
                            });

                        }
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
            var SHID = Model.SHID;
            var XXID = Model.XXID;
            $.ajax({
                url: "SelectSHXX",
                type: "post",
                async: false,
                data: { SHID: SHID },
                dataType: "Json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            if (json[i].SHStates > 0) {
                                alert("此收货单以入库 不可撤销");
                                return;
                            }
                        }
                        $.ajax({
                            url: "SelectshJJD",
                            type: "post",
                            async: false,
                            data: { SHID: SHID },
                            dataType: "Json",
                            success: function (data) {
                                var json = eval(data.datas);
                                if (json.length > 0) {
                                    alert("此收货单已生成交接单！！！");
                                    return;
                                }
                                else {
                                    isConfirm = confirm("确定要撤销吗")
                                    if (isConfirm == false) {
                                        return false;
                                    }
                                    else {

                                        $.ajax({
                                            url: "UpdateSHValidate",
                                            type: "Post",
                                            data: {
                                                SHID: SHID, xxid: XXID
                                            },
                                            async: false,
                                            success: function (data) {
                                                if (data.success == true) {
                                                    alert("成功");
                                                    return;
                                                }
                                            }

                                        });
                                    }
                                }
                            }
                        });



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
            var SHID = Model.SHID;
            var DDID = Model.DDID;
            window.parent.OpenDialog("", "../PPManage/DetailsSH?SHIDXQ=" + SHID + "&DDID=" + DDID + "", 1050, 650);
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
            var SHID = Model.SHID;
            //window.parent.parent.OpenDialog("上传", "../PPManage/AddFile?PID=" + SHID + "", 500, 300);
            window.parent.parent.OpenDialog("上传", "../PPManage/InsertBiddingNew?PID=" + SHID + "", 500, 300);
        }
    });

    $("#JJD").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        var SHID = Model.SHID;
        var XXID = Model.XXID;
        $.ajax({
            url: "SelectshJJD",
            type: "post",
            async: false,
            data: { SHID: SHID },
            dataType: "Json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    alert("此收货单已生成交接单！！！");
                }
                else {
                    window.parent.parent.OpenDialog("交接单", "../PPManage/Transfer?SHID=" + SHID + "&XXID=" + XXID + "", 950, 550);
                }
            }
        });

    });

});


function DJXQ(SHID) {
    $("#GXInfo").html("");

    $.ajax({
        url: "SelectRKDDID",
        type: "post",
        data: { SHID: SHID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json != null) {
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber" id="RKID' + i + '">' + json[i].RKID + '</lable> </td>';
                    html += '<td > <input class="btn" type="button" name="' + i + '" value="详情" onclick="rkxiangqing(this)" /></td>';
                    html += '</tr>'

                    $("#GXInfo").append(html);
                }
            }
        }
    });

}

function rkxiangqing(id) {
    var name = id.name;
    var RKID = document.getElementById("RKID" + name).innerHTML;
    window.parent.OpenDialog("详情", "../PPManage/DetailsRK?RKIDXQ=" + RKID + "", 1050, 650);
}

function thxiangqing() {
    var THID = document.getElementById("THID").innerHTML;
    window.parent.OpenDialog("详情", "../PPManage/DetailsTH?THIDXQ=" + THID + "", 1050, 650);
}
function jq() {

    var SHID = $('#SHID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    jQuery("#list").jqGrid({
        url: 'SelectSH',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, SHID: SHID, Begin: Begin, End: End },
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
        colNames: ['', '订单单号', '收货单号', '收货日期', '收货说明', '收货人', '单位', '收获状态'],
        colModel: [
             { name: 'XXID', index: 'XXID', width: 150, hidden: true },
             { name: 'DDID', index: 'DDID', width: 150 },
        { name: 'SHID', index: 'SHID', width: 150 },
        { name: 'ArrivalDate', index: 'ArrivalDate', width: 100 },
        { name: 'ArrivalDescription', index: 'ArrivalDescription', width: 150 },
        { name: 'CreateUser', index: 'CreateUser', width: 100 },
         { name: 'DeptName', index: 'DeptName', width: 100 },
            { name: 'RK', index: 'RK', width: 100 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',



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
                var SHID = Model.SHID;
                var XXID = Model.XXID;
                $('#WP').attr("class", "btnTw");
                $('#DJ').attr("class", "btnTh");
                $('#rzxq').attr("class", "btnTh");
                $("#danju").css("display", "none");
                $("#bor2").css("display", "none");
                if (XXID == 'L') {
                    $("#bor1").css("display", "");
                    reload1(SHID);
                }
                else {
                    $("#bor3").css("display", "");
                    reload2(SHID);
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
    var SHID = $('#SHID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val()
    $("#list").jqGrid('setGridParam', {
        url: 'SelectSH',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, SHID: SHID, Begin: Begin, End: End },

    }).trigger("reloadGrid");
}
function jq1(SHID) {


    jQuery("#list1").jqGrid({
        url: 'SelectSHGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, SHID: SHID },
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
        colNames: ['', '', '物品名称', '规格型号', '单位', '总数量', '收货数量', '供货商', '单价', '金额', '备注', '入库数量'],
        colModel: [
             { name: 'SHID', index: 'SHID', width: 0, hidden: true },
        { name: 'DID', index: 'DID', width: 0, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 180 },
        { name: 'Specifications', index: 'Specifications', width: 130 },
        { name: 'Unit', index: 'Unit', width: 60 },
        { name: 'Amount', index: 'Amount', width: 60 },//width: $("#bor").width() - 800 
         { name: 'ActualAmount', index: 'ActualAmount', width: 60 },
        { name: 'COMNameC', index: 'COMNameC', width: 100 },
        { name: 'UnitPriceNoTax', index: 'UnitPriceNoTax', width: 100 },
        { name: 'TotalNoTax', index: 'TotalNoTax', width: 100 },

        { name: 'Remark', index: 'Remark', width: 50 },
         { name: 'SHStates', index: 'SHStates', width: 80 }
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
            var SHID = Model.SHID;
            reload1(SHID);
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function reload1(SHID) {
    $("#bor1").css("display", "")
    $("#bor3").css("display", "none")
    $("#list1").jqGrid('setGridParam', {
        url: 'SelectSHGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, SHID: SHID },

    }).trigger("reloadGrid");
}


function jq2(SHID) {

    $("#bor3").css("display", "")
    $("#bor1").css("display", "none")
    jQuery("#list3").jqGrid({
        url: 'SelectSHCPXX',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, shid: SHID },
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
        colNames: ['', '', '物品名称', '规格型号', '单位', '数量', '税前单价', '税前总价', '税后单价', '税后总价'],
        colModel: [
        { name: 'ID', index: 'ID', width: 0, hidden: true },
        { name: 'SHID', index: 'SHID', width: 180, hidden: true },
        { name: 'Name', index: 'Name', width: 110 },
        { name: 'Spc', index: 'Spc', width: 80 },
        { name: 'Units', index: 'Units', width: 60 },
        { name: 'SHnum', index: 'SHnum', width: 80 },
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
            var SHID = Model.SHID;
            reload2(SHID);
        },
        loadComplete: function () {
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 50, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload2(SHID) {
    $("#bor3").css("display", "")
    $("#bor1").css("display", "none")
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list3").jqGrid('setGridParam', {
        url: 'SelectSHCPXX',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, shid: SHID },

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

    var SHID = $('#SHID').val();
    var OrderContent = $('#OrderContent').val();
    var Supplier = $('#Supplier').val();

    var Begin = $('#Begin').val();
    var End = $('#End').val();

    $("#list").jqGrid('setGridParam', {
        url: 'SelectSH',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, SHID: SHID, OrderContent: OrderContent, Supplier: Supplier, Begin: Begin, End: End },
        loadonce: false

    }).trigger("reloadGrid");//重新载入

}
function RZ() {
    var Type = "收货";
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
        var Type = "收货";
        $("#list2").jqGrid('setGridParam', {
            url: 'SelectRZ',
            datatype: 'json',
            postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },

        }).trigger("reloadGrid");
    }

}
