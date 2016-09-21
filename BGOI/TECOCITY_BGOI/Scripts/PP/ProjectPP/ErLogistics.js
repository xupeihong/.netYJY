
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
    $("#GXInfo").html("");
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    jq1();
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
        $("#bor1").css("display", "none");
        $("#bor2").css("display", "");

    });
    $("#DY").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要生成的物流");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ID;
        var url = "PrintWL?id=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });
    $("#XG").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要修改的物流");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ID;
        var ID = escape(texts);
        window.parent.parent.OpenDialog("修改", "../PPManage/ErUpdateWL?ID=" + ID + "", 1040, 600);
    });
    $("#SP").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行提交审批的项目单");
            return;
        }
        else {
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);

            if (Model.State != 1) {
                var texts = jQuery("#list").jqGrid('getRowData', rowid).DDID + "@" + "订购审批";
                window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
            }
            else {
                alert("该订购单已提交审批");
            }

        }
    });
    $("#XGWJ").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var DDID = Model.DDID;
            window.parent.parent.OpenDialog("上传", "../PPManage/AddFile?PID=" + DDID + "", 500, 300);

        }
    });
    $("#CX").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要撤销的物流");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ID;
        var ID = escape(texts);
        isConfirm = confirm("确定要撤销吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            $.ajax({
                url: "DeleteWL",
                type: "post",
                data: { ID: ID },
                dataType: "json",
                success: function (data) {

                    var json = data.datas;

                    if (json == "ture") {
                        alert("撤销成功");
                    }
                }
            });
        }
    });

});












function jq() {


    jQuery("#list").jqGrid({
        url: 'SelectWL',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
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
        colNames: ['', '授权公司', '提货公司', '收货地址', '收获联系人', '收货人电话', '发货人', '发货人电话', '发货人传真', '物流联系人', '物流电话', '物流传真'],
        colModel: [
            { name: 'ID', index: 'ID', width: 150, hidden: true },
        { name: 'SQCompany', index: 'SQCompany', width: 100 },
        { name: 'THCompany', index: 'THCompany', width: 100 },
        { name: 'SHaddress', index: 'SHaddress', width: 100 },
        { name: 'SHContacts', index: 'SHContacts', width: 70 },
        { name: 'SHTel', index: 'SHTel', width: 70 },
        { name: 'FHConsignor', index: 'FHConsignor', width: 150 },
        { name: 'FHTel', index: 'FHTel', width: 70 },
         { name: 'FHFax', index: 'FHFax', width: 70 },
         { name: 'LogisticsS', index: 'LogisticsS', width: 100 },
         { name: 'LogisticsSTel', index: 'LogisticsSTel', width: 70 },
         { name: 'LogisticsSFax', index: 'LogisticsSFax', width: 70 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        //, hidden: true
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
                var ID = Model.ID;
                reload1(ID);

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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 450, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {

    $("#list").jqGrid('setGridParam', {
        url: 'SelectWL',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },

    }).trigger("reloadGrid");
}

function jq1(ID) {


    jQuery("#list1").jqGrid({
        url: 'SelectWLGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, ID: ID },
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
        colNames: ['', '物品名称', '规格型号', '数量'],
        colModel: [
 { name: 'ID', index: 'ID', width: 110, hidden: true },
        { name: 'ProName', index: 'ProName', width: 220 },
        { name: 'Spec', index: 'Spec', width: 160 },
        { name: 'Amount', index: 'Amount', width: 120 }

        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '物品详细',
        multiselect: true,
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
            var ID = Model.ID;
            reload1(ID);
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() - 450, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}

function reload1(ID) {
    $("#list1").jqGrid('setGridParam', {
        url: 'SelectWLGoods',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, ID: ID },

    }).trigger("reloadGrid");
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

    var DDID = $('#DDID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var OrderContent = $('#OrderContent').val();
    var Supplier = $("#Supplier").val();
    var ArrivalStatus = $('#ArrivalStatus').val();
    var DeliveryLimit = $("#DeliveryLimit").val();
    var PayStatus = $('#PayStatus').val();
    var State = $("#State").val();

    var DeliveryLimit1 = $("#DeliveryLimit1").val();

    $("#list").jqGrid('setGridParam', {
        url: 'SelectDD',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, DDID: DDID, OrderContent: OrderContent, Begin: Begin, End: End, Supplier: Supplier, ArrivalStatus: ArrivalStatus, DeliveryLimit: DeliveryLimit, PayStatus: PayStatus, State: State, DeliveryLimit1: DeliveryLimit1 },
        loadonce: false,

    }).trigger("reloadGrid");//重新载入

}
function RZ() {
    var Type = "物流";
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
        var Type = "物流";
        $("#list2").jqGrid('setGridParam', {
            url: 'SelectRZ',
            datatype: 'json',
            postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },

        }).trigger("reloadGrid");
    }

}
