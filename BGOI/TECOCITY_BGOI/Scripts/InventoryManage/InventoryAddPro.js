
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
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
    LoadTitleF1(); // 加载 标题行

    // 打印
    $("#btnPrint").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).PID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var url = "PrintInventoryAddPro?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
    $('#Spec').click(function () {
        selid1('getSpecOptionalAdd', 'GJ', 'divGJ', 'ulGJ', 'LoadGJ');//, 'BuildUnit'
    })
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
        url: 'InventoryAddProList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProType: ProType, PID: PID, ProName: ProName, Spec: Spec, HouseID: HouseID },

    }).trigger("reloadGrid");
}

function jq() {
    //var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".49.") > 0) {
        ProTypeID = 1;
    } else {
        //ProTypeID = $('#ProTypeID option:selected').text();
        ProTypeID = $('#ProTypeID').val();
    }

    PID = $('#PID').val();
    ProName = $('#ProName').val();
    Spec = $('#Spec option:selected').text();
    if (ProTypeID == "0") {
        ProTypeID = "";
    }
    if (Spec == "请选择") {
        Spec = "";
    }
    jQuery("#list").jqGrid({
        url: 'InventoryAddProList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProTypeID: ProTypeID, PID: PID, ProName: ProName, Spec: Spec },
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
        colNames: ['','序号', '产品库类型', '产品编号', '产品名称', '规格型号', '物料号', '单价（含税）', '单位', '不含税单价', '详细说明', '产品类型', '供应商', '备注'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'Text', index: 'Text', width: 100, align: "center" },
        { name: 'PID', index: 'PID', width: 120, align: "center" },
        { name: 'ProName', index: 'ProName', width: 150, align: "center" },
        { name: 'Spec', index: 'Spec', width: 150, align: "center" },
        { name: 'MaterialNum', index: 'MaterialNum', width: 80, align: "center" },
        { name: 'UnitPrice', index: 'UnitPrice', width: 100, align: "center" },
        { name: 'Units', index: 'Units', width: 100, align: "center" },
        { name: 'Price2', index: 'Price2', width: 80, align: "center" },
        //{ name: 'FinishCount', index: 'FinishCount', width: 80, align: "center" }, '库存数量',
        { name: 'Detail', index: 'Detail', width: 80, align: "center" },
        { name: 'Ptext', index: 'Ptext', width: 80, align: "center" },
        //{ name: 'HouseName', index: 'HouseName', width: 80, align: "center" }, '所属仓库',
        { name: 'COMNameC', index: 'COMNameC', width: 80, align: "center" },
       { name: 'Remark', index: 'Remark', width: 80, align: "center" }
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 220, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function Search() {
    curRow = 0;
    curPage = 1;
    //var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".49.") > 0) {
        ProTypeID = 1;
    } else {
        ProTypeID = $('#ProTypeID').val();
    }
    PID = $("#PID").val();
    ProName = $("#ProName").val();
    Spec = $('#Spec option:selected').text();
    if (ProTypeID == "0") {
        ProTypeID = "";
    }
    if (Spec == "请选择") {
        Spec = "";
    }
    $("#list").jqGrid('setGridParam', {
        url: 'InventoryAddProList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, ProTypeID: ProTypeID, PID: PID, ProName: ProName, Spec: Spec
        },
        loadonce: false
    }).trigger("reloadGrid");//重新载入
}
function AddInventoryAddPro() {
    window.parent.parent.OpenDialog("新增货品", "../InventoryManage/AddInventoryAddPro", 800, 280);
}
function AddSpec() {
    window.parent.parent.OpenDialog("新增货品规格", "../InventoryManage/AddSpec", 300, 150);
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
//var ExcelSheet = new ActiveXObject("Excel.Application");
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
        //ExcelSheet.Visible = true;
        //Save();
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
        alert("请重新上传文件");
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
    $("#shangcuan").hide();
    //编号/货品库类型/货品名称/物料号/规格型号/单价（含税）/不含税价格/	单位/厂家，生产厂商，供应商	/备注/详细说明/物品类型
    var tab = $("#tabStatistic");
    var countRow = tab[0].rows.length;
    var countCol = tab[0].rows[0].cells.length;
    var str = ''; // 将所有值拼接成需要的字符串
    for (var i = 0; i < countRow; i++) {
        var arr = tab[0].rows[i].cells[1].innerText;
        var arr1 = tab[0].rows[0].cells[1].innerText;
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
        url: "SavePlanData",
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
                // setTimeout('parent.ClosePop()', 10);
                window.parent.frames["iframeRight"].reload();
            }
        }
    })
}
// 加载标题行
function LoadTitleF1() {
    var tr1 = $("#line1");
    tr1.html('');
    //编号/货品库类型/货品名称/物料号/规格型号/单价（含税）/不含税价格/	单位/厂家，生产厂商，供应商	/备注/详细说明/物品类型
    //加载第一行
    //var th1 = $('<td>序号</td>');
    //th1.appendTo(tr1);
    var th1 = $('<td>编号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>货品库类型</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>货品名称</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>物料号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>规格型号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>单价（含税）</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>不含税价格</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>单位</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>厂家</td>');
    th1.appendTo(tr1);
    var th1 = $('<td  style="width:150px;">备注</td>');
    th1.appendTo(tr1);
    var th1 = $('<td  style="width:150px;">详细说明</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>物品类型</td>');
    th1.appendTo(tr1);
}
function UPInventoryUPPro() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        window.parent.parent.OpenDialog("修改货品", "../InventoryManage/UPInventoryUPPro?PID=" + Model.PID, 800, 300);
    }
}

function FX() {
    var str = "";
    var rowid = $("#list").jqGrid('getGridParam', 'selarrrow');
    for (var i = 0; i < rowid.length; i++) {
        var m = jQuery("#list").jqGrid('getRowData', rowid[i]).PID;
        str += "'" + m + "',";
    }
    $("#PIDN").val(str);
}
