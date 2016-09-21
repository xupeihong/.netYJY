var curPage = 1;
var DcurPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var id = 0;
var ID = 0;
var DID = 0;
var OID = 0;
var objData = '';
var OperateType = "";
var SpecsModels = "";
var ApplyMan = "";
var StartDate = "";
var EndDate = "";

$(document).ready(function () {
    //$("#pageContent").height($(window).height());
    ////$("#search").width($("#bor").width() - 30);
    //jq("");
    LoadPtypeList();

    LoadBasInfo("Com00", "");

    $("#btnSearch").click(function () {
        reload("Com00", "");
    });
})

//菜单绑定货品类型
var lastsel = '';
function LoadPtypeList() {
    var unitId = "";
    jQuery("#RUnitList").jqGrid({
        treeGrid: true,
        treeGridModel: 'adjacency', //treeGrid模式，跟json元数据有关 ,adjacency/nested   
        ExpandColumn: 'id',
        scroll: "true",
        //        url: '/pages/demo/tree1.json',   
        url: 'GetFiveMall',
        datatype: 'json',
        colNames: ['5S店名称', 'UnitCode', 'Grade'],
        colModel: [
            { name: 'id', index: 'id', width: 150, sorttype: "int" },
            { name: 'UnitCode', index: 'UnitCode', width: 1, hidden: true },
            { name: 'Grade', index: 'Grade', width: 1, hidden: true }
        ],
        pager: "false",
        sortname: 'UnitCode',
        sortorder: "UnitCode",
        jsonReader: {
            //          root: "Datas" ,   
            root: function (obj) {
                //                alert(obj)
                var data = eval("(" + obj + ")");
                return data.Datas;
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
            $("#RUnitList").jqGrid("setGridHeight", $("#AListInfo1").height()-80, false);
        },
        onSelectRow: function (id) {

            if (id && id !== lastsel) {
                var selUnit = jQuery("#RUnitList").jqGrid('getRowData', id);
                lastsel = id;
                unitId = selUnit.UnitCode;
                var grade = selUnit.Grade;
                //LoadBasInfo(unitId, grade);
                reload(unitId, grade);
            }
        }

    });
}

function PtypeReload() {

    $("#RUnitList").jqGrid('setGridParam', {
        url: 'GetFiveMall',
        datatype: 'json',
        postData: {

        },
        loadonce: false

    }).trigger("reloadGrid");
}

function reload(MallID, Grade) {
    if ($('.field-validation-error').length == 0) {
        OperateType = $("#sltOperate").val();
        SpecsModels = $("#SpecsModels").val();
        ApplyMan = $("#ApplyMan").val();
        StartDate = $("#StartDate").val();
        EndDate = $("#EndDate").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetFiveMallsGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, OperateType: OperateType, SpecsModels: SpecsModels, ApplyMan: ApplyMan, StartDate: StartDate, EndDate: EndDate, MallID: MallID, strGrade: Grade },

        }).trigger("reloadGrid");
    }
}

function LoadBasInfo(MallID, Grade) {
    OperateType = $("#sltOperate").val();
    SpecsModels = $("#SpecsModels").val();
    ApplyMan = $("#ApplyMan").val();
    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();
    jQuery("#list").jqGrid({
        url: 'GetFiveMallsGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OperateType: OperateType, SpecsModels: SpecsModels, ApplyMan: ApplyMan, StartDate: StartDate, EndDate: EndDate, MallID: MallID, strGrade: Grade },
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
        colNames: ['产品编号', '产品名称', '产品大类', '规格型号', '数量', '业务类型', '单价', '合计', '门店名称', '备注'],
        colModel: [
        { name: 'ProductID', index: 'ProductID', width: 140 },
        { name: 'OrderContent', index: 'OrderContent', width: 200 },
        { name: 'Ptype', index: 'Ptype', width: 200 },
        { name: 'Specifications', index: 'Specifications', width: 150 },
        { name: 'Amount', index: 'Amount', width: 150 },
        { name: 'BusinessType', index: 'BusinessType', width: 100 },
        { name: 'UnitPrice', index: 'UnitPrice', width: 150 },
        { name: 'Total', index: 'Total', width: 200 },
        { name: 'Malls', index: 'Malls', width: 150 },
        { name: 'Remark', index: 'Remark', width: 200 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var PAID = jQuery("#list").jqGrid("getRowData", rowid).PAID;
         //   LoadDetail(PAID);
          //  LoadRevoke(PAID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() -200, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width(), false);
        }
    });
}

