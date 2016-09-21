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
    jq3("");

    $('#CP').click(function () {
        this.className = "btnTw";
        $('#JU').attr("class", "btnTh");
        $("#bor1").css("display", "");
        $("#bor2").css("display", "none");
       
        //reload1();
    })


    $('#JU').click(function () {
        this.className = "btnTw";
        $('#CP').attr("class", "btnTh");
        $("#bor2").css("display", "");
        $("#bor1").css("display", "none");
        //reload2();
    })
})


function CXBG() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要撤销的检验报告");
        return;
    }
    else {
        var msg = "您真的确定要删除吗?";
        if (confirm(msg) == true) {
            var BGID = Model.BGID;
            $.ajax({
                type: "POST",
                url: "CXBG",
                data: { BGID: BGID },
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
}
//跳转到修改报告页面
function XGBG() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要修改的检验报告");
        return;
    } else {
        window.parent.parent.OpenDialog("修改随工单", "../ProduceManage/UpdateBG?BGID=" +Model.BGID + "&DID=" + Model.DID + "", 800, 500);
    }
}

//跳转到随工单修改页面
function CX() {
    var one = confirm("确定要撤销选中条目吗");
    if (one == false)
        return;
    else {
        $.ajax({
            url: "dellCashBack",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                }
                else {
                    return;
                }
            }
        });
    }
}


function jq() {
    var RWID = $('#RWID').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();

    jQuery("#list").jqGrid({
        url: 'ReportInfo',
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
        colNames: ['序号','任务单编号', '报告单编号', '创建日期', '产品编号', '产品名称', '规格型号', '数量', '备注', '记录人',''],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'RWID', index: 'RWID', width: 120, align: "center" },
        { name: 'BGID', index: 'BGID', width: 120, align: "center" },
        { name: 'CreateTime', index: 'CreateTime', editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' }, width: 150, align: "center" },
        { name: 'PID', index: 'PID', width: 200, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 100, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 80, align: "center" },
        { name: 'OrderNum', index: 'OrderNum', width: 200, align: "center" },
        { name: 'Remark', index: 'Remark', width: 100, align: "center" },
        { name: 'CreatePerson', index: 'CreatePerson', width: 80, align: "center" },
        { name: 'DID', index: 'DID', width: 80, align: "center",hidden:true }
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
                var BGID = Model.BGID;
                reload2(BGID);
                LoadReceiveBill(BGID);
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
        url: 'ReportInfo',
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
        url: 'ReportInfo',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, RWID: RWID, OrderContent: OrderContent, SpecsModels: SpecsModels, Starts: Starts, Starte: Starte },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}

//加载


function jq2(BGID) {
    $.ajax({
        url: "FileInfo",
        type: "post",
        single: true,
        data: { BGID: BGID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRows(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProcess' + rowCount + ' " id="Type' + rowCount + '">' + json[i].Type + '</lable> </td>';
                    html += '<td ><lable class="labteam' + rowCount + ' " id="FileName' + rowCount + '">' + json[i].FileName + '</lable> </td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">查看</a></td>';
                    html += '<td ><lable class="BGID' + rowCount + ' " id="BGID' + rowCount + '" style="display:none">' + json[i].BGID + '</lable><lable class="BGID' + rowCount + ' " id="DIDsss' + rowCount + '" style="display:none">' + json[i].DID + '</lable>  </td>';
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


function deleteTr(curRow) {
        newRowID = curRow.id;
        $("#DetailInfo tr").removeAttr("class");
        $("#" + newRowID).attr("class", "RowClass");
        var a = "#" + newRowID
        ss = Number(a.substring(11, 12));
        m = ss + 1;
        //alert(ss);
        //字符串截取DetailInfo，要剩下的   int
        //和did的id组装成需要的did的id
        var DID =document.getElementById("DIDsss" + ss).innerHTML;
        var id = DID;
        window.parent.OpenDialog("生产任务文档下载", "../ProduceManage/DownLoadFile?id=" + id, 400, 200, '');
}
//点击显示详情
function reload2(BGID) {
    //给选中的行赋值为0
    for (var i = document.getElementById("DetailInfo").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("DetailInfo").removeChild(document.getElementById("DetailInfo").childNodes[i]);
    }
   
    $.ajax({
        url: "FileInfo",
        type: "post",
        single: true,
        data: { BGID: BGID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;//1
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRows(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProcess' + rowCount + ' " id="Type' + rowCount + '">' + json[i].Type + '</lable> </td>';
                    html += '<td ><lable class="labteam' + rowCount + ' " id="FileName' + rowCount + '">' + json[i].FileName + '</lable> </td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">查看</a> </td>';
                    html += '<td ><lable class="BGID' + rowCount + ' " id="BGID' + rowCount + '"style="display:none">' + json[i].BGID + '</lable><lable class="BGID' + rowCount + ' " id="DIDsss' + rowCount + '" style="display:none">' + json[i].DID + '</lable>  </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
}

function jq3(BGID)
{
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadBGs",
        type: "post",
        data: { BGID: BGID },
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
                    if (s == "BG") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '" style="width:33%">检测报告信息</lable> </td>';
                    }
                    html += '<td ><lable class="labID' + rowCount + ' " id="ID' + rowCount + '" style="width:33%">' + json[i].ID + '</lable> </td>';
                    html += '<td ><a href="#" style="color:blue;width:33%" onclick=GetXX("' + json[i].ID + '")>详情</a></td>';
                    html += '</tr>'
                    $("#ReceiveBill").append(html);
                }
            }
        }
    })
}
function LoadReceiveBill(BGID) {
    for (var i = document.getElementById("ReceiveBill").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("ReceiveBill").removeChild(document.getElementById("ReceiveBill").childNodes[i]);
    }
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadBGs",
        type: "post",
        data: { BGID: BGID },
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
                    if (s == "BG") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '" style="width:33%">检测报告信息</lable> </td>';
                    }
                    html += '<td ><lable class="labID' + rowCount + ' " id="ID' + rowCount + '" style="width:33%">' + json[i].ID + '</lable> </td>';
                    html += '<td ><a href="#" style="color:blue;width:33%" onclick=GetXX("' + json[i].ID + '")>详情</a></td>';
                    html += '</tr>'
                    $("#ReceiveBill").append(html);
                }
            }
        }
    })
}

function GetXX(SDI) {
    var id = SDI;
    window.parent.parent.OpenDialog("详细", "../ProduceManage/LoadBG?ID=" + id, 800, 450);
}


