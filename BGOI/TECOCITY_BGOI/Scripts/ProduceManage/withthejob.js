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
    jq1("", "");
    //jq2("");
    jq3("");

    $('#CP').click(function () {
        this.className = "btnTw";
        //$('#JU').attr("class", "btnTh");
        $('#DJXX').attr("class", "btnTh");
        $("#CCashBack").css("display", "");
        //$("#UserLog").css("display", "none");
        $("#bor2").css("display", "none");
        reload1();
    })


    //$('#JU').click(function () {
    //    this.className = "btnTw";
    //    $('#CP').attr("class", "btnTh");
    //    $('#DJXX').attr("class", "btnTh");
    //    $("#UserLog").css("display", "");
    //    $("#CCashBack").css("display", "none");
    //    $("#bor2").css("display", "none");

    //})

    $('#DJXX').click(function () {
        this.className = "btnTw";
        $('#CP').attr("class", "btnTh");
        //$('#JU').attr("class", "btnTh");
        $("#bor2").css("display", "");
        $("#CCashBack").css("display", "none");
        //$("#UserLog").css("display", "none");

    })

    //$("#DaYin").click(function () {
    //    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    //    var rID = jQuery("#list").jqGrid('getRowData', rowid).SGID;
    //    if (rowid == null) {
    //        alert("请选择要打印的随工单");
    //        return;
    //    }
    //    else {
    //        var texts = jQuery("#list").jqGrid('getRowData', rowid).SGID;
    //        var url = "PrintSG?Info=" + escape(texts);
    //        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:500px;status:no;resizable:yes;edge:sunken;location:no;");
    //    }
    //});

    $("#SC").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要上传图片的随工单");
            return;
        } else {
            window.parent.parent.OpenDialog("上传文件", "../ProduceManage/SCMaterialForm?OId=" + Model.SGID + "&ID=随工单", 400, 200);
        }
    })

    $("#CK").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要上传图片的随工单");
            return;
        } else {
            window.parent.parent.OpenDialog("查看文件", "../ProduceManage/CKMaterialForm?OId=" + Model.SGID, 500, 500);
        }
    })
})



//跳转到随工单记录页面
function SGJL() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {

        window.parent.parent.OpenDialog("随工单记录", "../ProduceManage/SGJL?SGID=" + Model.SGID, 800, 550);
    }
}

function CXSG() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要撤销的随工单");
        return;
    }
    else {
        var msg = "您真的确定要删除吗?";
        if (confirm(msg) == true) {
            var SGID = Model.SGID;
            var RWID = Model.RWID;
            $.ajax({
                type: "POST",
                url: "CXSG",
                data: { SGID: SGID, RWID: RWID },
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

//跳转到随工单修改页面
function updateSG() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要修改的随工单");
        return;
    }
        //} else {
        //    var SGID = Model.SGID;
        //    $.ajax({
        //        async: false,
        //        url: "getISSGdetail",
        //        type: "post",
        //        single: true,
        //        data: { SGID: SGID },
        //        dataType: "json",
        //        success: function (data) {
        //            var json = eval(data.datas);
        //            if (json.length > 0) {
        //                for (var i = 0; i < json.length; i++) {
        //                    if (json[i].a == "0") {
        //                        alert("此随工单尚未添加随工记录,请先添加!");
        //                        break;
        //                    }
    else {
        window.parent.parent.OpenDialog("修改随工单", "../ProduceManage/updateSG?SGID=" + Model.SGID, 1000, 500);
        //                }
        //            }
        //        }
        //    }

        //})

    }
}

//跳转到随工单详情页面
function SGDtail() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要查看详情的随工单");
        return;
    } else {
        window.parent.parent.OpenDialog("随工单详情", "../ProduceManage/SGDtail?SGID=" + Model.SGID, 1000, 500);
    }
}

function DaYin() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = jQuery("#list").jqGrid('getRowData', rowid).SGID;
    if (rowid == null) {
        alert("请选择要打印的随工单");
        return;
    }
    else {
        var texts = jQuery("#list").jqGrid('getRowData', rowid).SGID;
        var url = "PrintSG?Info=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:500px;status:no;resizable:yes;edge:sunken;location:no;");
    }
}
function DY() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var rID = jQuery("#list").jqGrid('getRowData', rowid).SGID;
    var texts = jQuery("#list").jqGrid('getRowData', rowid).SGID;
    var url = "PrintSG?Info=" + escape(texts);
    window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:500px;status:no;resizable:yes;edge:sunken;location:no;");
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
        colNames: ['序号', '任务单编号', '随工单编号', '发单日期', '技术负责人', '状态', ''],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'RWID', index: 'RWID', width: 140, align: "center" },
         { name: 'SGID', index: 'SGID', width: 140, align: "center" },
        { name: 'billing', index: 'billing', editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' }, width: 250, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 200, align: "center" },
        { name: 'State', index: 'State', width: 180, align: "center" },
        { name: 'SGID', index: 'SGID', width: 120, align: "center", hidden: true }
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
            select(rowid);
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {

                return;
            }
            else {
                var SGID = Model.SGID;
                var RWID = Model.RWID;
                //reload1(SGID);
                //reload2(SGID);
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

function select(rowid) {
    SGID = jQuery("#list").jqGrid('getRowData', rowid).SGID;
    RWID = jQuery("#list").jqGrid('getRowData', rowid).RWID;
    reload1(SGID, RWID);
    //reload2(SGID, RWID);
    LoadReceiveBill(SGID, RWID);
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
function jq1(SGID, RWID) {
    jQuery("#list1").jqGrid({
        url: 'ProduceInDetials',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, SGID: SGID, RWID: RWID },
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
        colNames: ['序号', '产品编码', '产品名称', '产品规格', '单位', '数量', '批次号', '备注', ''],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'PID', index: 'PID', width: 100, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 120, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 120, align: "center" },
        { name: 'OrderUnit', index: 'OrderUnit', width: 80, align: "center" },
        { name: 'OrderNum', index: 'OrderNum', width: 80, align: "center" },
        { name: 'photo', index: 'photo', width: 80, align: "center" },
        { name: 'Remark', index: 'Remark', width: 120, align: "center" },
        { name: 'DID', index: 'DID', width: 100, align: "center", hidden: true },
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
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() - 488, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}

function reload1(SGID, RWID) {
    var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'ProduceInDetials',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, SGID: SGID, RWID: RWID },

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
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRows(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProcess' + rowCount + ' " id="Process' + rowCount + '">' + json[i].Process + '</lable> </td>';
                    html += '<td ><lable class="labteam' + rowCount + ' " id="team' + rowCount + '">' + json[i].team + '</lable> </td>';
                    html += '<td ><lable class="labEstimatetime' + rowCount + ' "onclick="WdatePicker()" id="Estimatetime' + rowCount + '">' + json[i].Estimatetime + '</lable> </td>';
                    html += '<td ><lable class="labperson' + rowCount + ' " id="person' + rowCount + '">' + json[i].person + '</lable> </td>';
                    html += '<td ><lable class="labplannumber' + rowCount + ' " id="plannumber' + rowCount + '">' + json[i].plannumber + '</lable> </td>';
                    html += '<td ><lable class="labQualified' + rowCount + ' " id="Qualified' + rowCount + '">' + json[i].Qualified + '</lable> </td>';
                    html += '<td ><lable class="labnumber' + rowCount + ' " id="number' + rowCount + '">' + json[i].number + '</lable> </td>';
                    html += '<td ><lable class="labnumbers' + rowCount + ' " id="numbers' + rowCount + '">' + json[i].numbers + '</lable> </td>';
                    html += '<td ><lable class="labFnubers' + rowCount + ' " id="Fnubers' + rowCount + '">' + json[i].Fnubers + '</lable> </td>';
                    html += '<td ><lable class="labfinishtime' + rowCount + ' "onclick="WdatePicker()" id="finishtime' + rowCount + '">' + json[i].finishtime + '</lable> </td>';
                    html += '<td ><lable class="labpeople' + rowCount + ' " id="people' + rowCount + '">' + json[i].people + '</lable> </td>';
                    html += '<td ><lable class="labreason' + rowCount + ' " id="reason' + rowCount + '">' + json[i].reason + '</lable> </td>';
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
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        newRowID = curRow.id;
        $("#DetailInfo tr").removeAttr("class");
        $("#" + newRowID).attr("class", "RowClass");
        var a = "#" + newRowID
        ss = Number(a.substring(11, 12));
        m = ss + 1;
        //alert(ss);
        //字符串截取DetailInfo，要剩下的   int
        //和did的id组装成需要的did的id
        var DID = "";
        for (var i = ss; i < m; i++) {
            var dID = document.getElementById("DID" + i).innerHTML;

            DID += dID;
            if (i < m - 1) {
                DID += ",";
            }
            else {
                DID += "";
            }
        }
        var tbodyID = "DetailInfo";
        var rowIndex = -1;
        var typeNames = ["RowNumber", "DID", "Process", "team", "Estimatetime", "person", "plannumber", "Qualified", "number", "numbers", "Fnubers", "finishtime", "people", "reason", "DetailInfo"];
        if (newRowID != "")
            rowIndex = newRowID.replace(tbodyID, '');
        if (rowIndex != -1) {
            document.getElementById(tbodyID).deleteRow(rowIndex);
            //var a = $("#" + tbodyID + " tr").length;
            if (rowIndex < $("#" + tbodyID + " tr").length) {
                for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
                    // var b = parseInt(i);
                    var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                    var tr1 = $("#" + tbodyID + (parseInt(i) + 1));
                    tr.id = tbodyID + i;
                    for (var j = 0; j < tr.childNodes.length; j++) {
                        var html = tr1.html();
                        for (var k = 0; k < typeNames.length; k++) {
                            var olPID = typeNames[k] + (parseInt(i) + 1);
                            var newid = typeNames[k] + i;
                            var reg = new RegExp(olPID, "g");
                            html = html.replace(reg, newid);
                        }
                        tr1.html(html);//.toLocaleLowerCase());//replace('TD','td'));
                    }
                    document.getElementById("RowNumber" + i).innerHTML = parseInt(i) + 1;
                    // $("#RowNumber" + i).html(parseInt(i) + 1);
                }
            }
            if (document.getElementById(tbodyID).rows.length > 0) {
                if (rowIndex == document.getElementById(tbodyID).rows.length)
                    selRows(document.getElementById(tbodyID + (rowIndex - 1)), '');
                else
                    selRows(document.getElementById(tbodyID + rowIndex), '');;
            }
        }
        $("#DetailInfo tr").removeAttr("class");
        $.ajax({
            type: "POST",
            url: "SCSG",
            data: { DID: DID },
            success: function (data) {
                alert(data.Msg);
                reload();
            },
            dataType: 'json'
        });
        return true;
    }
}


//点击显示详情
function reload2(SGID) {
    var aa = $("#Rights").val();
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
                    rowCount = document.getElementById("DetailInfo").rows.length;//1
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRows(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProcess' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td ><lable class="labProcess' + rowCount + ' " id="Process' + rowCount + '">' + json[i].Process + '</lable> </td>';
                    html += '<td ><lable class="labteam' + rowCount + ' " id="team' + rowCount + '">' + json[i].team + '</lable> </td>';
                    html += '<td ><lable class="labEstimatetime' + rowCount + ' " id="Estimatetime' + rowCount + '">' + json[i].Estimatetime + '</lable> </td>';
                    html += '<td ><lable class="labperson' + rowCount + ' " id="person' + rowCount + '">' + json[i].person + '</lable> </td>';
                    html += '<td ><lable class="labplannumber' + rowCount + ' " id="plannumber' + rowCount + '">' + json[i].plannumber + '</lable> </td>';
                    html += '<td ><lable class="labQualified' + rowCount + ' " id="Qualified' + rowCount + '">' + json[i].Qualified + '</lable> </td>';
                    html += '<td ><lable class="labnumber' + rowCount + ' " id="number' + rowCount + '">' + json[i].number + '</lable> </td>';
                    html += '<td ><lable class="labnumbers' + rowCount + ' " id="numbers' + rowCount + '">' + json[i].numbers + '</lable> </td>';
                    html += '<td ><lable class="labFnubers' + rowCount + ' " id="Fnubers' + rowCount + '">' + json[i].Fnubers + '</lable> </td>';
                    html += '<td ><lable class="labfinishtime' + rowCount + ' " id="finishtime' + rowCount + '">' + json[i].finishtime + '</lable> </td>';
                    html += '<td ><lable class="labpeople' + rowCount + ' " id="people' + rowCount + '">' + json[i].people + '</lable> </td>';
                    html += '<td ><lable class="labreason' + rowCount + ' " id="reason' + rowCount + '">' + json[i].reason + '</lable> </td>';
                    //if (aa.indexOf("6") >= 0) {
                    //    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                    //}
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
}

function jq3(SGID) {
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadSGs",
        type: "post",
        data: { SGID: SGID },
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
                    if (s == "SG") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '" style="width:33%">随工单信息</lable> </td>';
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

function LoadReceiveBill(SGID) {
    for (var i = document.getElementById("ReceiveBill").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("ReceiveBill").removeChild(document.getElementById("ReceiveBill").childNodes[i]);
    }
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadSGs",
        type: "post",
        data: { SGID: SGID },
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
                    if (s == "SG") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '" style="width:33%">随工单信息</lable> </td>';
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
    window.parent.parent.OpenDialog("详细", "../ProduceManage/LoadSG?ID=" + id, 800, 450);
}

