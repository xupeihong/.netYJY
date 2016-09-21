
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ptype = "";
var PName = "";
var PId = "";
var Spec = "";
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq("");
    LoadPtypeList();
    $("#btnSave").click(function () {
        //

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
            window.parent.addBasicDetail1(ID);
            window.parent.ClosePop();
        }




        //
        //var dataSel = jQuery("#list").jqGrid('getGridParam');
        //var ids = dataSel.selrow;
        //var Model = jQuery("#list").jqGrid('getRowData', ids);
        //if (ids == null) {
        //    alert("请选择要修改的行");
        //    return;
        //}
        //else {
        //    var returnInfo = Model.ProName + "," + Model.PID + "," + Model.Spec + "," + Model.UnitPrice;
        //    window.returnValue = returnInfo;
        //    window.close();
        //}


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
        postData: { curpage: curPage, rownum: OnePageCount, ptype: ptype, PName: PName, PId: PId, Spec: Spec },

    }).trigger("reloadGrid");
}

function jq(ptype) {

    jQuery("#list").jqGrid({
        url: 'ChangePtypeList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ptype: ptype, PName: PName, PId: PId, Spec: Spec },
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
        colNames: ['', '序号', '货品编号', '货品名称', '物料编码', '规格型号', '单价', '生产厂家', '货品类型'],
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
        multiselect: true,
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}



