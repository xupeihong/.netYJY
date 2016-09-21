var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var RWID;
var curPage1 = 1;
var OnePageCount1 = 6;
var newRowID;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    jq2("");

   
})

$("#DaYin").click(function () {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = jQuery("#list").jqGrid('getRowData', rowid).JHID;
    if (rowid == null) {
        alert("请在列表中选择一条数据");
        return;
    }
    else {
        var texts = jQuery("#list").jqGrid('getRowData', rowid).JHID;
        var url = "PrintJH?Info=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:500px;status:no;resizable:yes;edge:sunken;location:no;");
    }
});

//跳转到制定计划页面
function Addplan() {
    window.parent.parent.OpenDialog("制定计划单", "../ProduceManage/Addplan", 800, 500);
    }


//跳转到随工单修改页面
function UpdateMaterialForm() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        window.parent.parent.OpenDialog("修改领料单", "../ProduceManage/UpdateMaterialForm?LLID=" + Model.LLID, 800, 500);
    }
}

//跳转到随工单详情页面
function MaterialFormDetail() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        window.parent.parent.OpenDialog("领料单详情", "../ProduceManage/MaterialFormDetail?LLID=" + Model.LLID, 800, 500);
    }
}

function jq() {
    var RWID = $('#RWID').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();

    jQuery("#list").jqGrid({
        url: 'withthejobList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, RWID: RWID, OrderContent: OrderContent, SpecsModels: SpecsModels, Starts: Starts, Starte: Starte },
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
        colNames: ['序号', '名称', '规格', '库存成品', '库存半成品', '在线生产数量', '库存成品零件数(套)', '已下单尚未供货零件数(套)', '下月计划生产数', '下月需求零件数', '备注'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'SGID', index: 'SGID', width: 120, align: "center" },
        { name: 'billing', index: 'billing', width: 150, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 200, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 100, align: "center" },
        { name: 'State', index: 'State', width: 80, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 200, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 100, align: "center" },
        { name: 'State', index: 'State', width: 80, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 100, align: "center" },
        { name: 'State', index: 'State', width: 80, align: "center" }
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
                var SGID = Model.SGID;
                reload1(SGID);
                reload2(SGID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 350, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    var RWID = $('#RWID').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();

    $("#list").jqGrid('setGridParam', {
        url: 'withthejobList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, RWID: RWID, OrderContent: OrderContent, SpecsModels: SpecsModels, Starts: Starts, Starte: Starte },

    }).trigger("reloadGrid");//重新载入
}

//查询
function Search() {
    //判断开始日期
    var strDateStart = $('#Starts').val();
    var strDateEnd = $('#Starte').val();
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
            alert("随工单截止日期不能小于开始日期！");
            $("#Starte").val("");
            return false;
        }


    }
}

function getSearch() {
    curRow = 0;
    curPage = 1;

    var RWID = $('#RWID').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();

    $("#list").jqGrid('setGridParam', {
        url: 'withthejobList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, RWID: RWID, OrderContent: OrderContent, SpecsModels: SpecsModels, Starts: Starts, Starte: Starte },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}

//加载

function jq1(SGID) {
    jQuery("#list1").jqGrid({
        url: 'ProduceInDetials',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, SGID: SGID },
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
        colNames: ['序号', '制定人', '指定日期', '指定说明'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'DID', index: 'DID', width: 100, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 120, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 120, align: "center" }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '产品详细',

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
            if (pgButton == "next_pager") {
                if (curPage == $("#list1").getGridParam("lastpage"))
                    return;
                curPage = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager1 :input").val();
            }

        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() - 400, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}

function reload1(SGID) {
    var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'ProduceInDetials',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, SGID: SGID },

    }).trigger("reloadGrid");
}
function jq2(SGID) {
    rowCount = document.getElementById("DetailInfo").rows.length;//1
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "ProProductRDatail",
        type: "post",
        single: true,
        data: { SGID: SGID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProcess' + rowCount + ' " id="Process' + rowCount + '">' + json[i].Process + '</lable> </td>';
                    html += '<td ><lable class="labteam' + rowCount + ' " id="team' + rowCount + '">' + json[i].team + '</lable> </td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">查看</a> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
}

function selRows(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

//移除行代码，数据库没删除
function deleteTr(date) {

}
//点击显示详情
function reload2(SGID) {
    //给选中的行赋值为0
    for (var i = document.getElementById("DetailInfo").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("DetailInfo").removeChild(document.getElementById("DetailInfo").childNodes[i]);
    }
    rowCount = document.getElementById("DetailInfo").rows.length;//1
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "ProProductRDatail",
        type: "post",
        single: true,
        data: { SGID: SGID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    
                    html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProcess' + rowCount + ' " id="Process' + rowCount + '">' + json[i].Process + '</lable> </td>';
                    html += '<td ><lable class="labteam' + rowCount + ' " id="team' + rowCount + '">' + json[i].team + '</lable> </td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">查看</a> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
}

