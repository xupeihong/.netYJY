
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var curPage1 = 1;
var OnePageCount1 = 4;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    jq1("");
    // 打印
    $("#btnPrint").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).ID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).ID;
            var url = "PrintAllocationSheet?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
    $('#SpecsModels').click(function () {
        selid1('getSpecOptionalAdd', 'GJ', 'divGJ', 'ulGJ', 'LoadGJ');//, 'BuildUnit'
    })
})
function reload() {
    HouseID = $('#HouseID option:selected').text();
    ID = $('#ID').val();
    OrderContent = $('#OrderContent').val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".47.") >= 0) {
        SpecsModels = $('#SpecsModels option:selected').text();
    } else {
        SpecsModels = $("#SpecsModels").val();
    }
    Begin = $("#Begin").val();
    End = $("#End").val();
    if (HouseID == "请选择") {
        HouseID = "";
    }
    if (SpecsModels == "请选择") {
        SpecsModels = "";
    }
    $("#list").jqGrid('setGridParam', {
        url: 'AllocationSheetList',//InventoryAddProList
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, HouseID: HouseID, ID: ID, OrderContent: OrderContent, SpecsModels: SpecsModels, Begin: Begin, End: End },

    }).trigger("reloadGrid");
}

function jq() {
    HouseID = $('#HouseID option:selected').text();
    ID = $('#ID').val();
    OrderContent = $('#OrderContent').val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".47.") >= 0) {
        SpecsModels = $('#SpecsModels option:selected').text();
    } else {
        SpecsModels = $("#SpecsModels").val();
    }
    Begin = $("#Begin").val();
    End = $("#End").val();
    if (HouseID == "请选择") {
        HouseID = "";
    }
    if (SpecsModels == "请选择") {
        SpecsModels = "";
    }
    jQuery("#list").jqGrid({
        url: 'AllocationSheetList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, HouseID: HouseID, ID: ID, OrderContent: OrderContent, SpecsModels: SpecsModels, Begin: Begin, End: End },
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
        colNames: ['','序号', '发货编号', '创建单位', '单据日期', '出库仓库', '入库仓库', '备注', '原因描述', '创建人'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'ID', index: 'ID', width: 100, align: "center" },
        { name: 'CreateUnitID', index: 'CreateUnitID', width: 120, align: "center" },
        { name: 'Inspector', index: 'Inspector', width: 150, align: "center" },
        { name: 'CK', index: 'CK', width: 150, align: "center" },
        { name: 'RK', index: 'RK', width: 80, align: "center" },
        { name: 'Remark', index: 'Remark', width: 100, align: "center" },
        { name: 'ReasonRemark', index: 'ReasonRemark', width: 100, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 80, align: "center" }
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
                var ID = Model.ID;
                //$("#ListInIDnew").val(ListInID);
                reload1(ID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()/2 -100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }

    });
}

function Search() {
    curRow = 0;
    curPage = 1;
    HouseID = $('#HouseID option:selected').text();
    ID = $('#ID').val();
    OrderContent = $('#OrderContent').val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".47.") >= 0) {
        SpecsModels = $('#SpecsModels option:selected').text();
    } else {
        SpecsModels = $("#SpecsModels").val();
    }
    Begin = $("#Begin").val();
    End = $("#End").val();
    if (HouseID == "请选择") {
        HouseID = "";
    }
    if (SpecsModels == "请选择") {
        SpecsModels = "";
    }
    $("#list").jqGrid('setGridParam', {
        url: 'AllocationSheetList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, HouseID: HouseID, ID: ID, OrderContent: OrderContent, SpecsModels: SpecsModels, Begin: Begin, End: End
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}

function AddInventoryAddPro() {
    window.parent.parent.OpenDialog("新增发货单", "../InventoryManage/AddAllocationSheet", 850, 550);
}


function jq1(ID) {
    jQuery("#list1").jqGrid({
        url: 'AllocationSheetDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, ID: ID },
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
        colNames: ['序号', '产品编号', '产品名称', '规格型号', '单位', '数量', '单价',  '备注'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'ProductID', index: 'PIDame', width: 100, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 120, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 120, align: "center" },
        { name: 'OrderUnit', index: 'OrderUnit', width: 80, align: "center" },
        { name: 'OrderNum', index: 'OrderNum', width: 80, align: "center" },
        { name: 'NoTaxuUnit', index: 'NoTaxuUnit', width: 80, align: "center" },
        //{ name: 'Manufacturer', index: 'Manufacturer', width: 120, align: "center" },'厂家',
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
                if (curPage == 1)
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
function reload1(ID) {
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'AllocationSheetDetialList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, ID: ID },

    }).trigger("reloadGrid");
}
function LoadGJ(liInfo) {
    $('#SpecsModels').val(liInfo);
    $('#divGJ').hide();
}
function BuildUnitkey() {
    $('#divGJ').hide();
}
function selid1(actionid, selid, divid, ulid, jsfun) {
    // var TypeID = Type;// 行政区编码
    var spec = $("#SpecsModels").val();
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
        var m = jQuery("#list").jqGrid('getRowData', rowid[i]).ID;
        str += "'" + m + "',";
    }
    $("#IDN").val(str);
}
