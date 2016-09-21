var curPage = 1;
var OnePageCount = 9;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ListOutID;
var curPage1 = 1;
var OnePageCount1 = 6;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    LoadTitleF1(); // 加载 标题行
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
                var BXKID = Model.BXKID;
                $.ajax({
                    type: "POST",
                    url: "DeWarrantyCard",
                    data: { BXKID: BXKID },
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
    // 打印
    $("#btnPrint").click(function () {
        var type = 2;
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).BXKID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).BXKID;
            var url = "PrintWarrCard?Info='" + escape(texts) + "'&type="+type ;
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})

function ScrapManagementOut() {
    window.parent.parent.OpenDialog("保修卡登记", "../CustomerService/AddWarrantyCard", 800, 550);
}
function ScrapManagementUP() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择操作的行");
        return;
    }
    window.parent.parent.OpenDialog("修改保修卡", "../CustomerService/UpWarrantyCard?BXKID=" + Model.BXKID, 800, 550);
}

function jq() {
    var Contact = $('#Contact').val();
    var SpecsModels = $('#SpecsModels').val();
    var OrderContent = $('#OrderContent').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var UserAdd = $("#UserAdd").val();
    //var State = $("input[name='State']:checked").val();
    jQuery("#list").jqGrid({
        url: 'WarrantyCardList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, Contact: Contact, SpecsModels: SpecsModels, Begin: Begin, End: End, OrderContent: OrderContent, UserAdd: UserAdd },
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
        colNames: ['序号', '保修编码', '购买日期', '保修时间', '联系人', '联系方式','产品名称', '产品型号', '备注', '登记人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'BXKID', index: 'BXKID', width: 120, align: "center" },
        { name: 'BuyDate', index: 'BuyDate', width: 120, align: "center" },
        { name: 'BXDate', index: 'BXDate', width: 150, align: "center" },
        { name: 'Contact', index: 'Contact', width: 80, align: "center" },
        { name: 'Tel', index: 'Tel', width: 100, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 100, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 80, align: "center" },
        { name: 'Remark', index: 'Remark', width: 80, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 80, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        gridComplete: function () {

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
                var BXKID = Model.BXKID;
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 220, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    var Contact = $('#Contact').val();
    var SpecsModels = $('#SpecsModels').val();
    var OrderContent = $('#OrderContent').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var UserAdd = $("#UserAdd").val();
    $("#list").jqGrid('setGridParam', {
        url: 'WarrantyCardList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, Contact: Contact, SpecsModels: SpecsModels, Begin: Begin, End: End, OrderContent: OrderContent, UserAdd: UserAdd },
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
    var Contact = $('#Contact').val();
    var SpecsModels = $('#SpecsModels').val();
    var OrderContent = $('#OrderContent').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var UserAdd = $("#UserAdd").val();
    $("#list").jqGrid('setGridParam', {
        url: 'WarrantyCardList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, Contact: Contact, SpecsModels: SpecsModels, Begin: Begin, End: End, OrderContent: OrderContent, UserAdd: UserAdd },
        loadonce: false
    }).trigger("reloadGrid");//重新载入

}


//------------上传

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
    //维修记录编号	保修卡所属单位	合同编号	保修卡编号	产品名称	产品编号	产品规格型号	购买日期  （2015-10-19 00:00:00.000）	
    //保修时间	最终用户单位	联系人	联系方式	备注	创建时间   （2015-10-19 00:00:00.000）	
    //登记人	初始状态0	客户地址  
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
        url: "SaveWarrantyCardData",
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
    //维修记录编号	保修卡所属单位	合同编号	保修卡编号	产品名称	产品编号	产品规格型号	购买日期  （2015-10-19 00:00:00.000）	
    //保修时间	最终用户单位	联系人	联系方式	备注	创建时间   （2015-10-19 00:00:00.000）	
    //登记人	初始状态0	客户地址

    //加载第一行
    var th1 = $('<td>维修记录编号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>保修卡所属单位</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>合同编号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>保修卡编号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>产品名称</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>产品编号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>产品规格型号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>购买日期</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>保修时间</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>最终用户单位</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>联系人</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>联系方式</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>备注</td>');
    th1.appendTo(tr1);
   
    var th1 = $('<td>登记人</td>');
    th1.appendTo(tr1);
    
    var th1 = $('<td>客户地址</td>');
    th1.appendTo(tr1);
}




