var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var id = 0;
var ID = 0;
var DID = 0;
var OID = 0;
var objData = '';
var SalesMan = '';
var SalesProduct = '';
var OperateType = "";
var SpecsModels = "";
var ApplyMan = "";
var StartDate = "";
var EndDate = "";
//
var MallID = "";
var Grade = "";

//
var CurHigherUnitID = "";//当前树选中ID
var CurHUnitName = "";//
var oldTreeSelID = "";//记录树节点选中项，
var CurLevel = "";//记录树当前选中单位Level
var CurUserUnitID = "";//当前单位列表内选中单位
var CurUserUnitName = "";//当前单位列表内选中单位名称
//
$(document).ready(function () {
    LoadPtypeList();
    //  LoadBasInfo("Com00", "");
    LoadBelongComSalesRetailList("Com00", "");
    $("#btnSearch").click(function () {
        reload(MallID, Grade);
    });
})

//菜单绑定货品类型
var lastsel = '';


function LoadPtypeList() {
    var unitId = "";
      // $(this).find('>span.ui-icon-' +
      //  (sortDirection ? 'asc' : 'desc')).hide();
      // $('ui-jqgrid-sortable').style.display = "";
      // $('.ui-grid-ico-sort.ui-icon-desc.ui-sort-ltr').hide();
      //sjQuery("#RUnitList", jQuery("#sortableRows")[0]).addClass('unsortable');
      //jQuery("#RUnitList").jqGrid('sortableRows', { items: '.jqgrow:not(.unsortable)' });
    jQuery("#RUnitList").jqGrid({
        treeGrid: true,
       treeGridModel: 'adjacency', //treeGrid模式，跟json元数据有关 ,adjacency/nested   
      //  ExpandColumn: 'UnitCode',
        async: false,
        scroll: "true",
        //        url: '/pages/demo/tree1.json',   
        // url: 'GetBelongCom',
        url: 'GetFiveMall',
        datatype: 'json',
        colNames: ['所属分公司', 'UnitCode', 'Grade'],
        colModel: [
            { name: 'id', index: 'id', width: 150},
            //{ name: 'UnitName', index: 'UnitName', width: 150, sorttype: "int" },
            { name: 'UnitCode', index: 'UnitCode', width: 1, hidden: true },
            { name: 'Grade', index: 'Grade', width: 1, hidden: true }
        ],
        pager: "false",
       //  viewrecords: false,
       // sortorder: false,
       //  jQuery("#list").jqGrid('sortableRows', { items: '.jqgrow:not(.unsortable)'});
        jsonReader: {
            //          root: "Datas" ,   
            root: function (obj) {
                //                alert(obj)
                var data = eval("(" + obj + ")");
                return data.Datas;
            },
            repeatitems: false
        },
    
        mtype: "GET",
        height: "auto",    // 设为具体数值则会根据实际记录数出现垂直滚动条   
        rowNum: "-1",     // 显示全部记录   
        shrinkToFit: false,  // 控制水平滚动条
        loadComplete: function () {
            $("#RUnitList").jqGrid("setGridHeight", $("#AListInfo1").height() -80, false);
            // $("#RUnitList").jqGrid("setGridHeight", 150, false);
           
          //  $("#RUnitList").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
          //  PtypeReload();
        },
        onSelectRow: function (id) {

            if (id && id !== lastsel) {
                var selUnit = jQuery("#RUnitList").jqGrid('getRowData', id);
                lastsel = id;
                unitId = selUnit.UnitCode;
                var grade = selUnit.Grade;
                MallID = unitId;
                Grade = grade;
                //LoadBasInfo(unitId, grade);
                reload(unitId, grade);
            }
        }

    });
}

function PtypeReload() {

    $("#RUnitList").jqGrid('setGridParam', {
        //url: 'GetBelongCom',
        treeGrid: true,
        treeGridModel: 'adjacency', //treeGrid模式，跟json元数据有关 ,adjacency/nested   
        //  ExpandColumn: 'UnitCode',
        async: false,
        scroll: "true",
        //        url: '/pages/demo/tree1.json',   
        // url: 'GetBelongCom',
        url: 'GetFiveMall',
        datatype: 'json',
        colNames: ['所属分公司', 'UnitCode', 'Grade'],
        colModel: [
            { name: 'id', index: 'id', width: 150 },
            //{ name: 'UnitName', index: 'UnitName', width: 150, sorttype: "int" },
            { name: 'UnitCode', index: 'UnitCode', width: 1, hidden: true },
            { name: 'Grade', index: 'Grade', width: 1, hidden: true }
        ],
       // loadonce: false

    }).trigger("reloadGrid");
}

function reload() {
    if ($('.field-validation-error').length == 0) {
        SalesProduct = $("#SalesProduct").val();
        OperateType = $("#sltOperate").val();
        SpecsModels = $("#SpecsModels").val();
        ApplyMan = $("#ApplyMan").val();
        StartDate = $("#StartDate").val();
        EndDate = $("#EndDate").val();
        SalesMan = $("#SalesMan").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetBelongComSalesRetailList',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, OperateType: OperateType, SalesMan:SalesMan,SalesProduct: SalesProduct, SpecsModels: SpecsModels, ApplyMan: ApplyMan, StartDate: StartDate, EndDate: EndDate, MallID: MallID, strGrade: Grade },

        }).trigger("reloadGrid");
    }
}

function LoadBelongComSalesRetailList(MallID, Grade) {
    OperateType = $("#sltOperate").val();
    SpecsModels = $("#SpecsModels").val();
    ApplyMan = $("#ApplyMan").val();
    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();
    jQuery("#list").jqGrid({
        url: 'GetBelongComSalesRetailList',
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
        colNames: ['产品编号', '产品名称', '规格型号', '数量', '渠道', '业务负责人', '生产厂家', '备注'],
        colModel: [
        { name: 'ProductID', index: 'ProductID', width: 140 },
        { name: 'OrderContent', index: 'OrderContent', width: 200 },
        //{ name: 'Ptype', index: 'Ptype', width: 200 },
        { name: 'SpecsModels', index: 'SpecsModels', width: 150 },
        { name: 'OrderNum', index: 'OrderNum', width: 150 },
              //{ name: 'Total', index: 'Total', width: 200 },
        { name: 'Malls', index: 'Malls', width: 150 },
        { name: 'ProvidManager', index: 'ProvidManager', width: 150 },
          { name: 'Manufacturer', index: 'Manufacturer', width: 100 },
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
            //  LoadDetail(PAID);
            //  LoadRevoke(PAID);
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("pager") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list").getGridParam("pager") - 1;
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width(), false);
        }
    });
}

