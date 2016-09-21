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
    jq1("");
    jq2("");

    $('#CP').click(function () {
        this.className = "btnTw";
        $('#JU').attr("class", "btnTh");
        $("#bor1").css("display", "");
        $("#bor2").css("display", "none");
        
    })


    $('#JU').click(function () {
        this.className = "btnTw";
        $('#CP').attr("class", "btnTh");
        $("#bor2").css("display", "");
        $("#bor1").css("display", "none");
        
    })

    $("#DaYin").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).RKID;
        if (rowid == null) {
            alert("请选择要打印的入库单");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).RKID;
            var url = "PrintRK?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:500px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})


function CXRK() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要撤销的入库单");
        return;
    }
    else {
        var msg = "您真的确定要删除吗?";
        if (confirm(msg) == true) {
            var RKID = Model.RKID;
            var RWID = Model.RWID;
            $.ajax({
                type: "POST",
                url: "CXRK",
                data: { RKID: RKID,RWID:RWID },
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


//跳转到入库单修改页面
function UpdateRK() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要修改的入库单");
        return;
    } else {
        window.parent.parent.OpenDialog("修改入库单", "../ProduceManage/UpdateRK?RKID=" +Model.RKID, 800, 500);
    }
}

//跳转到随入库详情页面
function RKDetail() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要查看详情的入库单");
        return;
    } else {
        window.parent.parent.OpenDialog("入库单详情", "../ProduceManage/RKDetail?RKID=" + Model.RKID, 800, 500);
    }
}

function jq() {
    var RKID = $('#RKID').val();
    var OrderContent = $('#OrderContent').val();
    var Supplier = $('#Supplier').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();

    jQuery("#list").jqGrid({
        url: 'PStocking',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, RKID: RKID, OrderContent: OrderContent, Supplier: Supplier, Starts: Starts, Starte: Starte },
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
        colNames: ['序号', '入库单号', '入库人', '入库日期', '入库说明',''],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'RKID', index: 'RKID', width: 120, align: "center" },
        { name: 'StockInUser', index: 'StockInUser', width: 200, align: "center" },
        { name: 'StockInTime', index: 'StockInTime', editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' }, width: 80, align: "center" },
        { name: 'StockRemark', index: 'StockRemark', width: 200, align: "center" },
        { name: 'RWID', index: 'RWID', width: 200, align: "center",hidden:true }
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
                var RKID = Model.RKID;
                reload1(RKID );
                LoadReceiveBill(RKID);
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
    var RKID = $('#RKID').val();
    var OrderContent = $('#OrderContent').val();
    var Supplier = $('#Supplier').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();

    $("#list").jqGrid('setGridParam', {
        url: 'PStocking',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, RKID: RKID, OrderContent: OrderContent, Supplier: Supplier, Starts: Starts, Starte: Starte },

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

    var RKID = $('#RKID').val();
    var OrderContent = $('#OrderContent').val();
    var Supplier = $('#Supplier').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();


    $("#list").jqGrid('setGridParam', {
        url: 'PStocking',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, RKID: RKID, OrderContent: OrderContent, Supplier: Supplier, Starts: Starts, Starte: Starte },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}

//加载
function jq1(RKID) {
    jQuery("#list1").jqGrid({
        url: 'PStockingDetail',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, RKID: RKID },
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
        colNames: ['序号', '产品编码', '产品名称', '产品规格', '单位', '数量',  '备注'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'PID', index: 'PID', width: 100, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 120, align: "center" },
        { name: 'Specifications', index: 'Specifications', width: 120, align: "center" },
        { name: 'Unit', index: 'Unit', width: 80, align: "center" },
        { name: 'Amount', index: 'Amount', width: 80, align: "center" },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" }
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
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage1"))
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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() - 485, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}

function reload1(RKID) {
    //var curPage1 = 1;
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'PStockingDetail',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, RKID: RKID },

    }).trigger("reloadGrid");
}
function jq2(RKID) {
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadRKs",
        type: "post",
        data: { RKID : RKID },
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
                    if (s == "RK") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '" style="width:33%">入库信息</lable> </td>';
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

function selRows(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

//移除行代码，数据库没删除
function deleteTr(date) {
   
}


function LoadReceiveBill(RKID) {
    for (var i = document.getElementById("ReceiveBill").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("ReceiveBill").removeChild(document.getElementById("ReceiveBill").childNodes[i]);
    }
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadRKs",
        type: "post",
        data: { RKID: RKID },
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
                    if (s == "RK") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '" style="width:33%">入库单信息</lable> </td>';
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
    window.parent.parent.OpenDialog("详细", "../ProduceManage/LoadRK?ID=" + id, 800, 450);
}
