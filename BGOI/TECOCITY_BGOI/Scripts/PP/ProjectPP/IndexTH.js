
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var CID;
var curPage1 = 1;
var OnePageCount1 = 20;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    jq1();


    $("#XG").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var THID = Model.THID;
            window.parent.parent.OpenDialog("修改", "../PPManage/UpdateTHXX?THID=" + THID + "", 900, 550);

        }
    });
    $("#CX").click(function () {
        isConfirm = confirm("确定要撤销吗")
        if (isConfirm == false) {
            return false;
        }
        else
        {
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {
                alert("请选择要操作的行");
                return;
            }
            else {
                var THID = Model.THID;
                $.ajax({
                    url: "UpdateTHValidate",
                    type: "Post",
                    data: {
                        THID: THID
                    },
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            reload();
                            alert("成功");
                        }
                        else {
                            alert("失败");
                        }
                    }
                });

            }
        }
      
    });
    $("#XX").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var THID = Model.THID;
            window.parent.parent.OpenDialog("详细", "../PPManage/DetailsTH?THIDXQ=" + THID + "", 900, 550);

        }
    });


    $("#DY").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要打印的请购单");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).THID;
        var url = "PrintTH?id=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });
    $("#WP").click(function () {

        this.className = "btnTw";

        $('#rzxq').attr("class", "btnTh");
        $("#bor1").css("display", "");

        $("#bor2").css("display", "none");

    });
    $("#rzxq").click(function () {
        RZ();
        this.className = "btnTw";
        $('#WP').attr("class", "btnTh");
        $("#bor2").css("display", "");
        $("#bor1").css("display", "none");
    });
});

function jq() {
    var THID = $('#THID').val();

    var Begin = $('#Begin').val();
    var End = $('#End').val();

    jQuery("#list").jqGrid({
        url: 'SelectTH',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, THID: THID, Begin: Begin, End: End },
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
        colNames: ['编号', '所属项目', '退货约定', '退货处理人', '单位','退货说明', '退货日期'],
        colModel: [
        { name: 'THID', index: 'THID', width: 150 },
        { name: 'TheProject', index: 'TheProject', width: 70 },
        { name: 'ReturnAgreement', index: 'ReturnAgreement', width: 50 },
        { name: 'UserName', index: 'UserName', width: 180 },
           { name: 'DeptName', index: 'DeptName', width: 150 },
        { name: 'ReturnDescription', index: 'ReturnDescription', width: 180 },
         { name: 'ReturnDate', index: 'ReturnDate', width: 180 }

        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',


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

                var THID = Model.THID;
                reload1(THID);

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
            //$("#list").jqGrid("setGridHeight", $("#pageContent").height() - 350, false);
            //$("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    var THID = $('#THID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    $("#list").jqGrid('setGridParam', {
        url: 'SelectTH',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, THID: THID, Begin: Begin, End: End },

    }).trigger("reloadGrid");
}

function jq1(EID) {

    jQuery("#list1").jqGrid({
        url: 'SelectTHGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, EID: EID },
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
        colNames: ['编号', '物料编码', '物品名称', '规格型号', '供应商', '单位', '退货数量',   '预计单价', '预计金额'],
        colModel: [
        { name: 'EID', index: 'EID', width: 180 },
        { name: 'INID', index: 'INID', width: 120 },
        { name: 'OrderContent', index: 'OrderContent', width: 150 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'COMNameC', index: 'COMNameC', width: 100 },
        { name: 'Unit', index: 'Unit', width: 60 },
        { name: 'Amount', index: 'Amount', width: 60 },
     
        { name: 'UnitPriceNoTax', index: 'UnitPriceNoTax', width: 100 },
        { name: 'TotalNoTax', index: 'TotalNoTax', width: 100 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '物品详细',

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage"))
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
            var dataSel = jQuery("#list1").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list1").jqGrid('getRowData', ids);
            var EID = Model.EID;
            reload1(EID)
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

function reload1(EID) {
    $("#list1").jqGrid('setGridParam', {
        url: 'SelectTHGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, EID: EID },

    }).trigger("reloadGrid");
}
function RZ() {
    var Type = "退货";
    jQuery("#list2").jqGrid({
        url: 'SelectRZ',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },
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
        colNames: ['操作ID', '操作', '状态', '时间', '操作人'],
        colModel: [
              { name: 'RelevanceID', index: 'RelevanceID', width: 150 },
        { name: 'LogTitle', index: 'LogTitle', width: 150 },
          { name: 'LogContent', index: 'LogContent', width: 150 },
        { name: 'LogTime', index: 'LogTime', width: 150 },
        { name: 'LogPerson', index: 'LogPerson', width: 100 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        onSelectRow: function (rowid, status) {
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage1 == $("#list2").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage1 = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager2 :input").val();
            }
            list2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });

}

function list2() {

    if ($('.field-validation-error').length == 0) {
        var Type = "退货";
        $("#list2").jqGrid('setGridParam', {
            url: 'SelectRZ',
            datatype: 'json',
            postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },

        }).trigger("reloadGrid");
    }

}

function SearchOut() {

    var strDateStart = $('#Begin').val();
    var strDateEnd = $('#End').val();
    //if (strDateStart == "" && strDateEnd == "" && strDateStart1 == "" && strDateEnd1 == "") {

    //    getSearch();
    //}
    //else {
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
    else if (strDateS >= strDateE) {
        alert("截止日期不可以小于或等于开始日期");
        $("#End").val("");
    }
    else (strDateS == "" || strDateE == "")
    {
        getSearch();
    }

}


function getSearch() {
    curRow = 0;
    curPage = 1;
    var THID = $('#THID').val();

    var Begin = $('#Begin').val();
    var End = $('#End').val();

    $("#list").jqGrid('setGridParam', {
        url: 'SelectTH',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, THID: THID, Begin: Begin, End: End },
        loadonce: false,

    }).trigger("reloadGrid");//重新载入

}