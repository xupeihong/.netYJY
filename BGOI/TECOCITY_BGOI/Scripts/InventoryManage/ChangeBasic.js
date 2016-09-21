var curPage = 1;
var OnePageCount = 10;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ptype = "";
var Info = 0;
$(document).ready(function () {

    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    if (location.search != "") {
        Info = location.search.split('&')[0].split('=')[1];
    }
    if (Info == 1) {
        jqnew("");
    } else {
        jq("");
    }
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
            //请购页面调用此方法
            if (location.search.split('&')[1] != undefined) {
                var ID = mids.substr(0, mids.length - 1);
                parent.frames["iframeRight"].addBasicDetail(ID);
                window.parent.ClosePop();
            }
            else {
                var ID = mids.substr(0, mids.length - 1);
                window.parent.addBasicDetail(ID);
                window.parent.ClosePop();
            }

        }


        //var dataSel = jQuery("#list").jqGrid('getGridParam');
        //var ids = dataSel.selrow;
        //var Model = jQuery("#list").jqGrid('getRowData', ids);
        //if (ids == null) {
        //    alert("请选择要修改的行");
        //    return;
        //}
        //else {
        //    var PID = Model.PID;
        //    window.parent.addBasicDetail(PID);
        //    window.parent.ClosePop();
        //}
    })
    $('#Spec').click(function () {
        selid1('getSpecOptionalAdd',  'divGJ', 'ulGJ', 'LoadGJ');//, 'BuildUnit'
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
            { name: 'Text', index: 'Text', width: 100 },
            { name: 'ID', index: 'ID', width: 0, hidden: true }
        ],
        pager: "false",
        sortname: 'Ptype',
        sortorder: "Ptype",
        cmTemplate: { sortable: false },
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
        height: "246",
        //height: "auto",  // 设为具体数值则会根据实际记录数出现垂直滚动条   
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
                // PtypeReload();
                if (Info == 1) {
                    reloadnew(ptype);
                } else {
                    reload(ptype);
                }
            }
        }
    });
}
function PtypeReload() {
    $("#RUnitList").jqGrid('setGridParam', {
        url: 'GetPtype',
        datatype: 'json',
        async: false,
        postData: {
            curpage: curPage, rownum: OnePageCount
        },
        loadonce: false
    }).trigger("reloadGrid");
}
function reload(ptype) {
    var PID = $('#PID').val();
    var Spec = $('#Spec').val();
    $("#list").jqGrid('setGridParam', {
        url: 'ChangePtypeList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ptype: ptype, PID: PID, Spec: Spec },
    }).trigger("reloadGrid");
    LoadPtypeList();
}
function reloadnew() {
    var PID = $('#PID').val();
    var Spec = $('#Spec').val();
    $("#list").jqGrid('setGridParam', {
        url: 'ChangePtypeListLinJian',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ptype: ptype, PID: PID, Spec: Spec },
    }).trigger("reloadGrid");
}
function jq(ptype) {
    var PID = $('#PID').val();
    var Spec = $('#Spec').val();
    jQuery("#list").jqGrid({
        url: 'ChangePtypeList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ptype: ptype, PID: PID, Spec: Spec },
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
        colNames: ['', '序号', '产品编号', '产品名称',  '规格型号', '单价（含税）', '不含税价格', '生产厂家', '产品类型'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'PID', index: 'PID', width: 150, align: "center" },
        { name: 'ProName', index: 'ProName', width: 120, align: "center" },
        //{ name: 'MaterialNum', index: 'MaterialNum', width: 120, align: "center" }, '产品编码',
        { name: 'Spec', index: 'Spec', width: 150, align: "center" },
        { name: 'UnitPrice', index: 'UnitPrice', width: 150, align: "center" },
        { name: 'Price2', index: 'Price2', width: 150, align: "center" },
        { name: 'COMNameC', index: 'COMNameC', width: 150, align: "center" },
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
        height: "290",
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
//零件
function jqnew(ptype) {
    var PID = $('#PID').val();
    var Spec = $('#Spec').val()
    jQuery("#list").jqGrid({
        url: 'ChangePtypeListLinJian',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ptype: ptype, PID: PID, Spec: Spec },
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
        colNames: ['', '序号', '产品编号', '产品名称', '产品编码', '规格型号', '单价（含税）', '不含税价格', '生产厂家', '产品类型'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'PID', index: 'PID', width: 150, align: "center" },
        { name: 'ProName', index: 'ProName', width: 120, align: "center" },
        { name: 'MaterialNum', index: 'MaterialNum', width: 120, align: "center" },
        { name: 'Spec', index: 'Spec', width: 150, align: "center" },
        { name: 'UnitPrice', index: 'UnitPrice', width: 150, align: "center" },
        { name: 'Price2', index: 'Price2', width: 150, align: "center" },
        { name: 'Manufacturer', index: 'Manufacturer', width: 150, align: "center" },
        { name: 'Text', index: 'Text', width: 80, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        height: "290",
        multiselect: true,
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            reloadnew();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
//查询
function SearchOut() {
    getSearch();
}
function getSearch() {
    curRow = 0;
    curPage = 1;
    var PID = $('#PID').val();
    var Spec = $('#Spec').val();
    $("#list").jqGrid('setGridParam', {
        url: 'ChangePtypeListnew',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, Info: Info, Spec: Spec },
        loadonce: false
    }).trigger("reloadGrid");//重新载入
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
}



function LoadGJ(liInfo) {
    $('#Spec').val(liInfo);
    $('#divGJ').hide();
}
//function BuildUnitkey() {
//    $('#divGJ').hide();
//}
function selid1(actionid,  divid, ulid, jsfun) {
    // var TypeID = Type;// 行政区编码
    var spec = $("#Spec").val();
    $.ajax({
        url: actionid,
        type: "post",
        data: { spec: spec },//data1: TypeID,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.success == "true") {
                //var unitid = data.Strid.split(',');
                var unit = data.Strname.split(',');
                $("#" + divid).show();
                $("#" + ulid + " li").remove();
                for (var i = 0; i < unit.length; i++) {
                    $("#" + ulid).append("<li style='cursor:pointer;margin-left:1px; width:190px;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(\"" + unit[i] + "\");' style='margin-left:1px; width:190px; height:20px;display:block;'>" + unit[i] + "</span>");
                }
            }
            else {
                $("#" + divid).hide();
            }
        }
    });
}