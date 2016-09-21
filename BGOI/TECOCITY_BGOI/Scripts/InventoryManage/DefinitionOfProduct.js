var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var PID;
var curPage1 = 1;
var OnePageCount1 = 6;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    var shenchan;
    if (location.search != "") {
        shenchan = location.search.split('&')[0].split('=')[1];
    }
    if (shenchan == "1") {
        $("#kuc").hide();
        $("#shenchanyong").show();
    }
    jq();
    jq1("");
    LoadTitleF1();
    $("#De").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var msg = "您真的确定要撤销吗?";
            if (confirm(msg) == true) {
                var PID = Model.ProductID;
                $.ajax({
                    type: "POST",
                    url: "DeDefinitionOfProduct",
                    data: { PID: PID },
                    success: function (data) {
                        alert(data.Msg);
                        reload();
                    },
                    dataType: 'json'
                });
                return true;
            } else {
                return false;
            }
        }
    });
    //添加可生产
    $("#TJSC").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var msg = "您真的确定要添加可生产";
            if (confirm(msg) == true) {
                var PID = Model.ProductID;
                $.ajax({
                    type: "POST",
                    url: "AddProFin",
                    data: { PID: PID },
                    success: function (data) {
                        alert(data.Msg);
                        window.parent.frames["iframeRight"].reload();
                        //reload();
                    },
                    dataType: 'json'
                });
                return true;
            } else {
                return false;
            }
        }
    });
    // 打印
    $("#btnPrint").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).ListInID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var url = "PrintDefinitionOfProduct?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})
function reload() {
    ProType = $('#ProType option:selected').text();
    PID = $('#PID').val();
    ProName = $('#ProName').val();
    Spec = $('#Spec option:selected').text();
    HouseID = $('#HouseID option:selected').text();
    if (ProType == "请选择") {
        ProType = "";
    }
    if (Spec == "请选择") {
        Spec = "";
    }
    if (HouseID == "请选择") {
        HouseID = "";
    }
    $("#list").jqGrid('setGridParam', {
        url: 'DefinitionOfProductList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProType: ProType, PID: PID, ProName: ProName, Spec: Spec, HouseID: HouseID },

    }).trigger("reloadGrid");
}
function jq() {
    PID = $('#PID').val();
    ProName = $('#ProName').val();
    jQuery("#list").jqGrid({
        url: 'DefinitionOfProductList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, ProName: ProName},
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
        colNames: ['','序号', '货品编号', '货品名称', '规格型号', '物料号',  '备注','状态'],
        colModel: [
            { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'ProductID', index: 'ProductID', width: 200, align: "center" },
        { name: 'ProName', index: 'ProName', width: 200, align: "center" },
        { name: 'Spec', index: 'Spec', width: 200, align: "center" },
        { name: 'MaterialNum', index: 'MaterialNum', width: 150, align: "center" },
        { name: 'Remark', index: 'Remark', width: 150, align: "center" },
        { name: 'State', index: 'State', width: 150, align: "center" }
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
                var PID = Model.ProductID;
                reload1(PID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height()/2 -100 , false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function Search() {
    curRow = 0;
    curPage = 1;
    PID = $("#PID").val();
    ProName = $("#ProName").val();
    $("#list").jqGrid('setGridParam', {
        url: 'DefinitionOfProductList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, PID: PID, ProName: ProName
        },
        loadonce: false
    }).trigger("reloadGrid");//重新载入
    reload1("");
}
function AddInventoryAddPro() {
    window.parent.parent.OpenDialog("新增成品", "../InventoryManage/AddDefinitionOfProduct", 800, 450);
}
//上传
function getDetailData() {
    var path = $("#txtPath").val();
    if (path == "") {
        alert("请选择文件路径");
        return;
    }
    else
        $("#shangcuan").show();
        readEx(path);
}
// 获取要读取的excel文件
function readEx(path) {
    var ExcelSheet;
    var wb;
    try {
        ExcelSheet = new ActiveXObject("Excel.Application");
        wb = ExcelSheet.Workbooks.open(path);
        var objsheet = '';
        var num = 0;
        wb.worksheets(1).select();
        objsheet = wb.ActiveSheet;
        var colCount = objsheet.UsedRange.Columns.Count;
        var rowCount = objsheet.UsedRange.Rows.Count;
        var tab = $("#tabStatistic");
        tab.html('');
        for (var row = 2; row <= rowCount; row++) {
            var tr = $('<tr></tr>');
            for (var col = 1; col <= colCount - 0; col++) {
                var td = '';
                var empty = objsheet.cells(row, 1).Value;
                if (empty == "" || empty == undefined || empty == null)
                    return;
                else {
                    // 物料编码为空 则不显示 不保存
                    var value = objsheet.cells(row, col).Value;
                    if (value == "" || value == undefined || value == null)
                        td = $('<td></td>');
                    else
                        td = $('<td>' + value + '</td>');
                    td.appendTo(tr);
                }
            }
            tr.appendTo(tab);
        }
        // 使EXCEL窗口可见
        ExcelSheet.Visible = true;

    }
    catch (e) {
        if (ExcelSheet != undefined) {
            alert('Error happened : ' + e);
            ExcelSheet.Quit();
        }
        return '';
    }
}
// 保存计划表单数据 
function Save() {
    var record;
    var tab = $("#tabStatistic");
    record = tab[0].rows.length;
    if (record == 0) {
        alert("列表内容为空，没有要保存的数据，请依次上传文件+显示数据");
        return false;
    }
    else {
        $("#shangcuan").hide();
        UpLoadPlandata();
        return true;
    }
}
// 将数据保存到数据库中
function UpLoadPlandata() {
    //货品唯一编号	组装该成品的零件PID	需零件数量	规格型号
    var tab = $("#tabStatistic");
    var countRow = tab[0].rows.length;
    var countCol = tab[0].rows[0].cells.length;
    var str = ''; // 将所有值拼接成需要的字符串
    for (var i = 0; i < countRow; i++) {
        var arr = tab[0].rows[i].cells[1].innerText;
        for (var col = 0; col < countCol; col++) {
            if (arr != "" && arr != null) {
                if (col == countCol - 1) {
                    str += "'" + tab[0].rows[i].cells[col].innerText + "'";
                }
                else {
                    str += "'" + tab[0].rows[i].cells[col].innerText + "',";
                }

            }
        }
        if (arr != "" && arr != null) {
            str += "!";//每条完整数据用！分隔
        }
    }
    str = str.substring(0, str.length - 1);

    $.ajax({
        url: "SaveDefinitionOfProduct",
        type: "post",
        data: { strData: str },
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "false") {
                if (data.Msg != "") {
                    alert(data.Msg);
                }
            }
            else {
                alert('货品数据保存成功');
            }
        }
    })
}
// 加载标题行
function LoadTitleF1() {
    var tr1 = $("#line1");
    tr1.html('');
    //货品唯一编号	组装该成品的零件PID	需零件数量	规格型号
    //加载第一行
    //var th1 = $('<td>序号</td>');
    //th1.appendTo(tr1);
    var th1 = $('<td>货品唯一编号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>组装该成品的零件</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>需零件数量</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>规格型号</td>');
    th1.appendTo(tr1);
}
function jq1(PID) {
    jQuery("#list1").jqGrid({
        url: 'DefinitionOfList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, PID: PID },
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
        colNames: ['序号', '零件编号', '零件名称', '零件规格型号', '零件单位', '所需零件数量', '零件单价', '零件厂家', '零件备注','通用状态'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'PID', index: 'PID', width: 100, align: "center" },
        { name: 'ProName', index: 'ProName', width: 120, align: "center" },
        { name: 'Spec', index: 'Spec', width: 120, align: "center" },
        { name: 'Units', index: 'Units', width: 80, align: "center" },
        { name: 'Number', index: 'Number', width: 80, align: "center" },
        { name: 'Price2', index: 'Price2', width: 80, align: "center" },
        { name: 'COMNameC', index: 'COMNameC', width: 120, align: "center" },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" },
        { name: 'IdentitySharing', index: 'IdentitySharing', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '零件详细',
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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}
function reload1(PID) {
    //var curPage1 = 1;
    //var OnePageCount1 = 6;
    $("#list1").jqGrid('setGridParam', {
        url: 'DefinitionOfList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, PID: PID },
    }).trigger("reloadGrid");
}
//修改
function ScrapManagementUP() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        window.parent.parent.OpenDialog("修改成品", "../InventoryManage/UpDefinitionOfProduct?PID=" + Model.ProductID, 800, 550);
    }
}

function FX() {
    var str = "";
    var rowid = $("#list").jqGrid('getGridParam', 'selarrrow');
    for (var i = 0; i < rowid.length; i++) {
        var m = jQuery("#list").jqGrid('getRowData', rowid[i]).ProductID;
        str += "'" + m + "',";
    }
    $("#PIDN").val(str);
}
