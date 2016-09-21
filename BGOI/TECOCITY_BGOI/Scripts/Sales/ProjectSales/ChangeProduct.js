
var curPage = 1;
var OnePageCount = 20;
var DcurPage = 1;
var DOnePageCount = 15;


var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ptype = "";
var PName = "";
var PId = "";
var Spec = "";
var RowId = "";
var PID = "";
var curPageIndex = "";
var PageCount = "";
var LPage;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq("");

    // LoadProduct();
    LoadPtypeList();
    $("#btnSave").click(function () {
        //---获取jQGrid里面的复选框
        var selectedIds = $("#list").jqGrid("getGridParam", "selarrrow");
        if (selectedIds.length == 0) {
            alert("请选择物品");
            return;
        }
        var mids = "";
        var SampleCode = "";
        for (var i = 0; i < selectedIds.length; i++) {
            var PID = jQuery("#list").jqGrid('getCell', selectedIds[i], 'PID');
            if (mids.indexOf(PID) < 0)
                mids += "'" + PID + "'" + ",";
        }
        if (mids == "") {
            alert("请选择物品");
        }

        else {
            //var PID = Model.PID;
            var ID = mids.substr(0, mids.length - 1);
            window.parent.addBasicDetail(ID);
            window.parent.ClosePop();
        }
        //-----------------------
        //--
        //   var dataSel = jQuery("#list").jqGrid('getGridParam');
        //   var ids = dataSel.selrow;
        //   var Model = jQuery("#list").jqGrid('getRowData', ids);
        //   if (ids == null) {
        //       alert("请选择要修改的行");
        //       return;
        //   }
        //   else {

        //      var PID = Model.PID;
        //       // window.parent.addBasicDetail(PID);
        //       window.parent.addBasicDetail(PID);
        //window.parent.ClosePop();
        //  }


    })

    $("#SelectProduct").click(function () {
        var PName = $("#PlanName").val();
        var PId = $("#PlanID").val();
        var Spec = $("#SpecsModels").val();
        $("#list").jqGrid('setGridParam', {
            url: 'ChangePtypeList',
            datatype: 'json',
            postData: { curpage: 1, rownum: OnePageCount, ptype: ptype, PName: PName, PId: PId, Spec: Spec },

        }).trigger("reloadGrid");
    })
})

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
        async: false,
        postData: { curpage: curPage, rownum: OnePageCount },
        colNames: ['产品类型', 'ID'],
        colModel: [
            { name: 'Text', index: 'Text', width: 100, sorttype: "string", frozen: true },
            { name: 'ID', index: 'ID', width: 100, hidden: true }
        ],
       pager: jQuery('#Rpager'),
        pgbuttons: true,
        //rowNum: DOnePageCount,
        // viewrecords: true,
        // sortname: 'ID',
        //sortorder: "desc",
     sortable: false,
        //editable : false,
        cmTemplate: { sortable: false },
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
            $("#RUnitList").jqGrid("setGridHeight", 300, false);
          //  $("#RUnitList").jqGrid("setGridHeight", 150, false);
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (DcurPage == $("#RUnitList").getGridParam("lastpage"))
                    return;
                DcurPage = $("#RUnitList").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#RUnitList").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                DcurPage = $("#RUnitList").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                DcurPage = 1;
            }
            else {
                DcurPage = $("#Rpager :input").val();
            }
            reload(ptype);
        },
        onSelectRow: function (id) {
            if (id && id !== lastsel) {
                var selPtype = jQuery("#RUnitList").jqGrid('getRowData', id);

                //lastsel = id;
                ptype = selPtype.ID;
                //strLevel = selPtype.level;
               PtypeReload();
                reload(ptype);
            }
        }

    });
}

function PtypeReload() {
    DcurPage = 1;
    $("#RUnitList").jqGrid('setGridParam', {
        url: 'GetPtype',
        datatype: 'json',
        async: false,
        postData: {
            curpage: curPage, rownum: OnePageCount
        },
        sortable: false,
        //   editable: false
        // loadonce: false

    }).trigger("reloadGrid");
}

function reload(ptype) {
    var PName = $("#PlanName").val();
    var PId = $("#PlanID").val();
    var Spec = $("#SpecsModels").val();
    $("#list").jqGrid('setGridParam', {
        url: 'ChangePtypeList',
        datatype: 'json',
        async: false,
        postData: { curpage: DcurPage, rownum: DOnePageCount, ptype: ptype, PName: PName, PId: PId, Spec: Spec },

    }).trigger("reloadGrid");
    //$("#DetailInfo").html("");
    //LoadProduct();
}

function jq(ptype) {

    jQuery("#list").jqGrid({
        url: 'ChangePtypeList',
        datatype: 'json',
        postData: { curpage: DcurPage, rownum: DOnePageCount, ptype: ptype, PName: PName, PId: PId, Spec: Spec },
        loadonce: false,
        async: false,
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
        colNames: ['', '序号', '产品编号', '产品名称', '物料编码', '规格型号', '单价', '生产厂家', '货品类型'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
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
        //viewrecords: true,
        imgpath: 'themes/basic/images',
       // caption: '',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }



            //var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            //for (var i = 0; i < ids.length; i++) {
            //    var RowNumber = ids[i];
            //    var curRowData = jQuery("#list").jqGrid('getRowData', RowNumber);
            //    var curChk = "<input id='c" + RowNumber + "'type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', RowNumber).PID + "' name='cb'/>";
            //    jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            //}

        },
        multiselect: true,
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},
        onSelectRow: function (rowid, status, e) {
            var rowIds = jQuery("#list").jqGrid('getGridParam', 'selarrrow');
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (DcurPage == $("#list").getGridParam("lastpage"))
                    return;
                DcurPage = $("#list").getGridParam("page") + 1;
                //  LPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                DcurPage = $("#list").getGridParam("lastpage");
                // LPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (DcurPage == 1)
                    return;
                DcurPage = $("#list").getGridParam("page") - 1;
                // LPage = $("#list").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                DcurPage = 1;
                // LPage = 1;
            }
            else {
                DcurPage = $("#pager :input").val();
                // LPage = $("#pager :input").val();
            }
            //  reload(ptype);
            var rowid = $("#RUnitList").jqGrid('getGridParam', 'selrow');
           // var ids = jQuery("#list").jqGrid('getDataIDs');
            var selPtype = jQuery("#RUnitList").jqGrid('getRowData', rowid);

            ////lastsel = id;
            ptype = selPtype.ID;
            ////strLevel = selPtype.level;
            //PtypeReload();
            reload(ptype);
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", 300, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function Selrow(select) {
    newRowID = select.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

//选中多条物品
function selChange(rowid) {
    if ($('input[id=c' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {

        if (oldSelID != 0) {
            $('input[id=c' + oldSelID + ']').prop("checked", false);
        }
        $('input[id=c' + rowid + ']').prop("checked", true);
        $("#list").setSelection(rowid);

    }

    //IDCheck

    //var aa = "";
    //var cbNum = document.getElementsByName("cb");
    //var salesNum = "";
    //for (var i = 0; i < cbNum.length - 1; i++) {
    //   // alert(cbNum[i].ID);
    //   // alert(cbNum[i].PID);
    //    //$('input[id=c' + cbNum[i].id + ']').prop("checked", false);
    //}
}

function SaveProduct() {
    //var dataSel = jQuery("#list").jqGrid('getGridParam');
    //var ids = dataSel.selrow;
    //var Model = jQuery("#list").jqGrid('getRowData', ids);
    //if (ids == null) {
    //    alert("请选择要修改的行");
    //    return;
    //}
    //else {
    //    var PID = Model.PID;
    //window.parent.addBasicDetail(PID);
    // this.close.bindAsEventListener(window.this);
    // parent.ClosePop();
    // setTimeout("parent.ClosePop()", 1000)
    // }


}
function closeaaa() {
    alert("aaa")
    parent.ClosePop();
}

function LoadProduct() {
    $.ajax({
        url: "ChangePtypeList",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, ptype: ptype },
        dataType: "json",

        success: function (data) {
            var json2 = eval(data);
            json = json2[0].rows;
            var page1 = json2[0].page;
            var total1 = json2[0].total;
            var record1 = json2[0].records;
            curPageIndex = page1;
            PageCount = total1;
            var rowCount = 0;
            var html1 = "";
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick=selRow(this)>'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (rowCount + 1) + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="MaterialNum' + rowCount + '">' + json[i].MaterialNum + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    //html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    html += '<td ><lable class="labManufacturer' + rowCount + ' "  id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Text' + rowCount + '">' + json[i].Text + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                }
                if (total1 == 1) { }
                else {
                    html1 = "<tr style='text-align:center'><td colspan='2'><a href='###' onclick=CheckPage('1')>首页</a></td>"
                    html1 += "<td colspan='2'><a  href='####' onclick=CheckPage('prev')>上一页</a></td>"
                    html1 += "<td colspan='2'><a  href='####' onclick=CheckPage('next')>下一页</a></td>"
                    html1 += "<td colspan='2'><a  href='####' onclick=CheckPage('total1')>尾页</a></td>"
                    html1 += "<td colspan='2'>当前<" + curPageIndex + ">&nbsp;:共页<" + PageCount + "></td></tr>"
                    $("#pager").append(html1);
                }
            }
        }
    })
}
function selRow(curRow) {
    RowId = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + RowId).attr("class", "RowClass");

    var rownumber = RowId.substr(RowId.length - 1, 1);
    PID = document.getElementById("PID" + rownumber).innerHTML;
}

function CheckPage(current) {
    if (current == "prev") {
        curPage = curPageIndex - 1;
        $("#DetailInfo").html("");
        $("#pager").html("");
        LoadProduct();
    }
    if (current == "next") {
        curPage = curPageIndex + 1;
        $("#DetailInfo").html("");
        $("#pager").html("");
        LoadProduct();
    }
    if (current == "total1") {
        curPage = PageCount;
        $("#DetailInfo").html("");
        $("#pager").html("");
        LoadProduct();
    }
    if (current == "1") {
        curPage = 1;
        $("#DetailInfo").html("");
        $("#pager").html("");
        LoadProduct();
    }
}