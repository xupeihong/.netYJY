﻿var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ptype = "";

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq("");
    LoadPtypeList();
    $("#btnSave").click(function () {
        
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择行");
            return;
        }
        else {
            var PID = Model.PID;
            //addBasicDetail(PID);
            window.parent.addBasicDetail(PID);
            //setTimeout('parent.ClosePop()', 100);
            window.parent.ClosePop();

        }
        
        //window.parent.ClosePop();
    })

})

function stopBubble(e) {
    //一般用在鼠标或键盘事件上
    if (e && e.stopPropagation) {
        //W3C取消冒泡事件
        e.stopPropagation();
    } else {
        //IE取消冒泡事件
        window.event.cancelBubble = true;
    }
}

function addBasicDetail(PID) { //增加货品信息行

    var rowCount = parent.document.getElementById("GXInfo").rows.length;
    //var CountRows = parseInt(rowCount) ++;
    //rowCount++;
    //var CountRows = rowCount;
    //var strPID = $("#PID").val();
    //$("#PID").val(strPID + "," + PID);
    $.ajax({
        url: "GetBasicDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {

                var html = "";
                for (var i = 0; i < json.length; i++) {
                    var CountRows = parseInt(rowCount) + 1;
                    html = "<tr  id ='GXInfo" + rowCount + "'  onclick='selRow(this)'>"
                    html += "<td ><lable class='labRowNumber" + rowCount + "' id='RowNumber" + rowCount + "'>" + CountRows + "</lable> </td>";
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="XXID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' "  id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text" style="width:70px" value="0" id="Amount' + rowCount + '" onblur="OnBlurAmount(this);" /> </td>';
                    html += '<td ><input type="text"style="width:70px" onclick="CheckSupplier(this)"  id="Supplier' + rowCount + '"/> </td>';
                    html += '<td ><input type="text" style="width:70px"  value="0"  id="NegotiatedPricingNoTax' + rowCount + '" onblur="OnBlurAmount(Amount' + rowCount + ');" > </td>';
                    html += '<td ><input type="text" style="width:70px"   id="TotalNegotiationNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text"  style="width:70px"  id="DrawingNum' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:70px"  id="GoodsUse' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:70px" id="Remark' + rowCount + '"/> </td>';
                    html += '</tr>'
                    alert(html);
                    parent.document.getElementById("GXInfo").innerHTML = html;

                }
            }
        }
    })
}

//菜单绑定货品类型
var lastsel = '';
function LoadPtypeList() {
    jQuery("#RUnitList").jqGrid({
        treeGrid: true,
        treeGridModel: 'adjacency', //treeGrid模式，跟json元数据有关 ,adjacency/nested   
        ExpandColumn: 'id',
        scroll: "true",
        url: 'GetPtype',
        datatype: 'json',

        colNames: ['货品类型', 'ID'],
        colModel: [
            { name: 'Text', index: 'Text', width: 100 },
            { name: 'ID', index: 'ID', width: 100, hidden: true }
        ],
        pager: "false",
        sortname: 'Ptype',
        sortorder: "Ptype",
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            repeatitems: false
        },

        treeReader: {
            level_field: "level",
            parent_id_field: "parent",
            leaf_field: "isLeaf",
            expanded_field: "expanded"
        },

        mtype: "GET",
        height: "auto",    // 设为具体数值则会根据实际记录数出现垂直滚动条   
        rowNum: "-1",     // 显示全部记录   
        shrinkToFit: false,  // 控制水平滚动条
        loadComplete: function () {
            $("#RUnitList").jqGrid("setGridHeight", $("#AListInfo1").height(), false);
        }
        //onSelectRow: function (id) {
        //    if (id && id !== lastsel) {
        //        var selPtype = jQuery("#RUnitList").jqGrid('getRowData', id);
        //        alert(selPtype);

        //        lastsel = id;
        //        ptype = selPtype.ID;
        //        strLevel = selPtype.level;

        //        PtypeReload();
        //        reload(ptype);
        //    }
        //}

    });
    
}

function PtypeReload() {

    $("#RUnitList").jqGrid('setGridParam', {
        url: 'GetPtype',
        datatype: 'json',
        postData: {

        },
        loadonce: false

    }).trigger("reloadGrid");
}

function reload(ptype) {


    $("#list").jqGrid('setGridParam', {
        url: 'ChangePtypeList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ptype: ptype },

    }).trigger("reloadGrid");
}

function jq(ptype) {

    jQuery("#list").jqGrid({
        url: 'ChangePtypeList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ptype: ptype },
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
        colNames: ['序号', '货品编号', '货品名称', '物料编码', '规格型号', '单价', '生产厂家', '货品类型'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'PID', index: 'PID', width: 150, align: "center" },
        { name: 'ProName', index: 'ProName', width: 120, align: "center" },
        { name: 'MaterialNum', index: 'MaterialNum', width: 120, align: "center" },
        { name: 'Spec', index: 'Spec', width: 150, align: "center" },
        { name: 'UnitPrice', index: 'UnitPrice', width: 150, align: "center" },
        { name: 'Manufacturer', index: 'Manufacturer', width: 150, align: "center" },
        { name: 'Text', index: 'Text', width: 80, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}



