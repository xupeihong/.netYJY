﻿var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ListInID;
var curPage1 = 1;
var OnePageCount1 = 4;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    changcollege("");
    $("#IsHouseIDoneto").val($("#IsHouseIDone").val());
    $("#IsHouseIDtwoto").val($("#IsHouseIDtwo").val());
    jq();
    jq1("");
    //var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".49.") > 0) {
        var oid = 1;
        changcollege(oid);
    }
    // 打印
    $("#btnPrint").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).ListInID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).ListInID;
            var url = "PrintBasicStockIn?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });



    $("#Fin").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var ListInID = Model.ListInID;
            $.ajax({
                type: "POST",
                url: "UpDateState",
                data: { ListInID: ListInID },
                success: function (data) {
                    alert(data.Msg);
                    reload();
                },
                dataType: 'json'
            });
            reload1("");
        }
    });
    $('#Spec').click(function () {
        selid1('getSpecOptionalAdd', 'GJ', 'divGJ', 'ulGJ', 'LoadGJ');//, 'BuildUnit'
    })
})

// 成本
function AddCost() {
    window.parent.parent.OpenDialog("成本详细页面", "../InventoryManage/WarehousingCost", 850, 450);
}
function AddBasicStockIn() {
    window.parent.parent.OpenDialog("新增基本入库", "../InventoryManage/AddBasicStockIn", 850, 550);
}
function jq() {
    var UnitID = $("#UnitIDnew").val();
    var BatchID = $("#BatchID").val();
    var ListInID = $("#ListInIDC").val();
    //var HouseID = $('#HouseID').val();
    //var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".49.") > 0) {
        var ProType = 1;
    } else {
        var ProType = $("#ProType").val();
    }
    var Spec = $("#Spec").val();
    var IsHouseIDone = $("#IsHouseIDone").val();
    var IsHouseIDtwo = $("#IsHouseIDtwo").val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $("input[name='State']:checked").val();
    if (UnitID == "47") {
        jQuery("#list").jqGrid({
            url: 'BasicStockInList',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, BatchID: BatchID, ListInID: ListInID, ProType: ProType, Begin: Begin, End: End, State: State, Spec: Spec, IsHouseIDone: IsHouseIDone, IsHouseIDtwo: IsHouseIDtwo },
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
            colNames: ['', '序号', '入库单编号', '入库批号', '科目', '入库时间', '入库操作员', '总金额', '产品库类型', '所属仓库', '备注', '状态'],
            colModel: [
            { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
            { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
            { name: 'ListInID', index: 'ListInID', width: 120, align: "center" },
            { name: 'BatchID', index: 'BatchID', width: 120, align: "center" },
            { name: 'HandwrittenAccount', index: 'HandwrittenAccount', width: 150, align: "center" },
            { name: 'StockInTime', index: 'StockInTime', width: 100, align: "center" },
            { name: 'StockInUser', index: 'StockInUser', width: 100, align: "center" },
            { name: 'Amount', index: 'Amount', width: 80, align: "center" },
            { name: 'T', index: 'T', width: 80, align: "center" },
            { name: 'HouseName', index: 'HouseName', width: 80, align: "center" },
            { name: 'Remark', index: 'Remark', width: 80, align: "center" },
            { name: 'State', index: 'State', width: 80, align: "center" }
            ],
            pager: jQuery('#pager'),
            pgbuttons: true,
            rowNum: OnePageCount,
            viewrecords: true,
            imgpath: 'themes/basic/images',
            caption: '',
            multiselect: true,
            gridComplete: function () {
                var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
                for (var i = 0; i < ids.length; i++) {
                    var id = ids[i];
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

                var dataSel = jQuery("#list").jqGrid('getGridParam');
                var ids = dataSel.selrow;
                var Model = jQuery("#list").jqGrid('getRowData', ids);
                if (ids == null) {
                    return;
                }
                else {
                    var ListInID = Model.ListInID;
                    $("#ListInIDnew").val(ListInID);
                    reload1(ListInID);
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
                $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
                $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
            }
        });
    } else {
        jQuery("#list").jqGrid({
            url: 'BasicStockInList',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, BatchID: BatchID, ListInID: ListInID, ProType: ProType, Begin: Begin, End: End, State: State, Spec: Spec, IsHouseIDone: IsHouseIDone, IsHouseIDtwo: IsHouseIDtwo },
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
            colNames: ['', '序号', '入库单编号', '入库批号', '科目', '入库时间', '入库操作员', '总金额', '产品库类型', '所属仓库', '备注', '状态'],
            colModel: [
                 { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
            { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
            { name: 'ListInID', index: 'ListInID', width: 120, align: "center" },
            { name: 'BatchID', index: 'BatchID', width: 120, align: "center" },
            { name: 'SubjectName', index: 'SubjectName', width: 150, align: "center" },
            { name: 'StockInTime', index: 'StockInTime', width: 100, align: "center" },
            { name: 'StockInUser', index: 'StockInUser', width: 100, align: "center" },
            { name: 'Amount', index: 'Amount', width: 80, align: "center" },
            { name: 'T', index: 'T', width: 80, align: "center" },
            { name: 'HouseName', index: 'HouseName', width: 80, align: "center" },
             { name: 'Remark', index: 'Remark', width: 80, align: "center" },
            { name: 'State', index: 'State', width: 80, align: "center" }
            ],
            pager: jQuery('#pager'),
            pgbuttons: true,
            rowNum: OnePageCount,
            viewrecords: true,
            imgpath: 'themes/basic/images',
            caption: '',
            multiselect: true,
            gridComplete: function () {
                var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
                for (var i = 0; i < ids.length; i++) {
                    var id = ids[i];
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

                var dataSel = jQuery("#list").jqGrid('getGridParam');
                var ids = dataSel.selrow;
                var Model = jQuery("#list").jqGrid('getRowData', ids);
                if (ids == null) {
                    return;
                }
                else {
                    var ListInID = Model.ListInID;
                    $("#ListInIDnew").val(ListInID);
                    reload1(ListInID);
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
                $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
                $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
            }
        });
    }
}
function reload() {
    var BatchID = $('#BatchID').val();
    var ListInID = $('#ListInID').val();
    // var HouseID = $('#HouseID').val();
    //var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".49.") > 0) {
        var ProType = 1;
    } else {
        var ProType = $("#ProType").val();
    }

    var Spec = $("#Spec").val();
    var IsHouseIDone = $("#IsHouseIDone").val();
    var IsHouseIDtwo = $("#IsHouseIDtwo").val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $("input[name='State']:checked").val();

    $("#list").jqGrid('setGridParam', {
        url: 'BasicStockInList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, BatchID: BatchID, ListInID: ListInID, ProType: ProType, Begin: Begin, End: End, State: State, Spec: Spec, IsHouseIDone: IsHouseIDone, IsHouseIDtwo: IsHouseIDtwo },

    }).trigger("reloadGrid");
}
//查询
function SearchOut() {

    var strDateStart = $('#Begin').val();
    var strDateEnd = $('#End').val();
    if (strDateStart == "" && strDateEnd == "") {

        getSearch();
    }
    else {
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
        else {
            alert("截止日期不能小于开始日期！");
            $("#End").val("");
            return false;
        }
    }
}
function getSearch() {
    curRow = 0;
    curPage = 1;
    var BatchID = $("#BatchID").val();
    var ListInID = $("#ListInIDC").val();
    //var HouseID = $('#HouseID').val();
    //var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".49.") > 0) {
        var ProType = 1;
    } else {
        var ProType = $("#ProType").val();
    }
    var Spec = $("#Spec").val();
    var IsHouseIDone = $("#IsHouseIDone").val();
    var IsHouseIDtwo = $("#IsHouseIDtwo").val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $("input[name='State']:checked").val();
    if (State == "1") {
        $('#Fin').hide();
    } else {
        $('#Fin').show();
    }
    $("#list").jqGrid('setGridParam', {
        url: 'BasicStockInList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, BatchID: BatchID, ListInID: ListInID, ProType: ProType, Begin: Begin, End: End, State: State, Spec: Spec, IsHouseIDone: IsHouseIDone, IsHouseIDtwo: IsHouseIDtwo },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}
function jq1(ListInID) {
    jQuery("#list1").jqGrid({
        url: 'StockInDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, ListInID: ListInID },
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
        colNames: ['序号', '产品编号', '产品名称', '规格型号', '单位', '数量', '单价', '厂家', '备注'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'ProductID', index: 'PIDame', width: 100, align: "center" },
        { name: 'ProName', index: 'ProName', width: 120, align: "center" },
        { name: 'Spec', index: 'Spec', width: 120, align: "center" },
        { name: 'Units', index: 'Units', width: 80, align: "center" },
        { name: 'StockInCount', index: 'StockInCount', width: 80, align: "center" },
        { name: 'UnitPrice', index: 'UnitPrice', width: 80, align: "center" },
        { name: 'Manufacturer', index: 'Manufacturer', width: 120, align: "center" },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '产品详细',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs'); //alert(ids);
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
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}
function reload1(ListInID) {
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'StockInDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, ListInID: ListInID },

    }).trigger("reloadGrid");
}
function changcollege(va) {
    if (va == "") {
        va = $("#ProType").val();
    }
    $('#IsHouseIDtwo').attr("disabled", false);
    $('#two').attr("disabled", false);
    $('#one').attr("disabled", false);
    $('#IsHouseIDone').attr("disabled", false);

    document.getElementById("IsHouseIDone").innerHTML = "";
    document.getElementById("IsHouseIDtwo").innerHTML = "";
    document.getElementById("IsHouseIDone").add(new Option("请选择", "0"));
    document.getElementById("IsHouseIDtwo").add(new Option("请选择", "0"));

    $.ajax({
        url: "GetHouseIDoneNew",
        type: "post",
        data: { va: va },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var IsHouseIDone = document.getElementById("IsHouseIDone");
                    IsHouseIDone.add(new Option(json[i].HouseName, json[i].HouseID));

                }
            }
        }
    })
    $.ajax({
        url: "GetHouseIDtwoNew",
        type: "post",
        data: { va: va },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var IsHouseIDtwo = document.getElementById("IsHouseIDtwo");
                    IsHouseIDtwo.add(new Option(json[i].HouseName, json[i].HouseID));

                }
            }
        }
    })
}
function chang1() {
    document.getElementById('IsHouseIDtwo').setAttribute('disabled', 'true');
    document.getElementById('two').setAttribute('disabled', 'true');
    $("#IsHouseIDoneto").val($("#IsHouseIDone").val());
}
function chang2() {
    document.getElementById('one').setAttribute('disabled', 'true');
    document.getElementById('IsHouseIDone').setAttribute('disabled', 'true');
    $("#IsHouseIDtwoto").val($("#IsHouseIDtwo").val());
}
function returnConfirm() {

    $("#ListInID").val($("#ListInIDnew").val());
    // var str = "";
    // var r = document.getElementsByName("RIDCheck");
    //for (var i = 0; i < r.length; i++) {
    //    if (r[i].checked) {
    //        str += r[i].value + ",";
    //    }
    //}
    //$("#ListInID").val(str);
    //if (str == "") {

    //    return false;
    //} else {
    //    return true;
    //}
}
function LoadGJ(liInfo) {
    $('#Spec').val(liInfo);
    $('#divGJ').hide();
}
function BuildUnitkey() {
    $('#divGJ').hide();
}
function selid1(actionid, selid, divid, ulid, jsfun) {
    // var TypeID = Type;// 行政区编码
    var spec = $("#Spec").val();
    $.ajax({
        url: actionid,
        type: "post",
        data: { spec: spec },//data1: TypeID,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.success == "true") {
                //var unitid = data.Strid.split(',');
                var unit = data.Strname.split(',');
                $("#" + divid).show();
                $("#" + ulid + " li").remove();
                for (var i = 0; i < unit.length; i++) {
                    $("#" + ulid).append("<li style='cursor:pointer;margin-left:1px; width:190px;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(\"" + unit[i] + "\");' style='margin-left:1px; width:190px; height:20px;display:block;'>" + unit[i] + "</span>");
                }
            }
            else {
                $("#" + divid).hide();
            }
        }
    });
}

function FX() {
    var str = "";
    var rowid = $("#list").jqGrid('getGridParam', 'selarrrow');
    for (var i = 0; i < rowid.length; i++) {
        var m = jQuery("#list").jqGrid('getRowData', rowid[i]).ListInID;
        str += "'" + m + "',";
    }
    $("#ListInIDN").val(str);
}