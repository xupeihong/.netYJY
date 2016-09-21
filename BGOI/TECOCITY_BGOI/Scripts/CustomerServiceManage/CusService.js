var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var ListOutID;
var curPage1 = 1;
var OnePageCount1 = 6;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $("#De").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var msg = "您真的确定要删除吗?";
            if (confirm(msg) == true) {
                var BXKID = Model.BXKID;
                $.ajax({
                    type: "POST",
                    url: "DeWarrantyCard",
                    data: { BXKID: BXKID },
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
    });
    // 打印
    $("#btnPrint").click(function () {
        var type = 2;
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).BXKID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).BXKID;
            var url = "PrintWarrCard?Info='" + escape(texts) + "'&type=" + type;
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})

function ScrapManagementOut() {
    window.parent.parent.OpenDialog("客户登记", "../CustomerService/AddCusService", 800, 400);
}
function ScrapManagementUP() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择操作的行");
        return;
    }
    window.parent.parent.OpenDialog("修改客户服务", "../CustomerService/UpSaveCusService?KHID=" + Model.KHID, 800, 550);
}
function jq() {
    var CusName = $('#CusName').val();
    var CusAdd = $('#CusAdd').val();
    var CusTel = $('#CusTel').val();
    jQuery("#list").jqGrid({
        url: 'CusServiceList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, CusName: CusName, CusAdd: CusAdd, CusTel: CusTel },
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
        colNames: ['序号', '客户编号', '客户名称', '客户电话', '客户地址', '客户单位', '客户邮箱', '记录人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'KHID', index: 'KHID', width: 120, align: "center" },
        { name: 'CusName', index: 'CusName', width: 120, align: "center" },
        { name: 'CusTel', index: 'CusTel', width: 150, align: "center" },
        { name: 'CusAdd', index: 'CusAdd', width: 100, align: "center" },
        { name: 'CusUnit', index: 'CusUnit', width: 80, align: "center" },
        { name: 'CusEmail', index: 'CusEmail', width: 100, align: "center" },
        { name: 'CreateUser', index: 'CreateUser', width: 80, align: "center" }
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

                var KHID = Model.KHID;
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function reload() {
    var CusName = $('#CusName').val();
    var CusAdd = $('#CusAdd').val();
    var CusTel = $('#CusTel').val();
    $("#list").jqGrid('setGridParam', {
        url: 'CusServiceList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, CusName: CusName, CusAdd: CusAdd, CusTel: CusTel },
    }).trigger("reloadGrid");
}
//查询
function SearchOut() {
    var strDateStart = $('#Begin').val();
    var strDateEnd = $('#End').val();
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
            alert("截止日期不能小于开始日期！");
            $("#End").val("");
            return false;
        }
    }
}
function getSearch() {
    curRow = 0;
    curPage = 1;
    var CusName = $('#CusName').val();
    var CusAdd = $('#CusAdd').val();
    var CusTel = $('#CusTel').val();
    $("#list").jqGrid('setGridParam', {
        url: 'CusServiceList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, CusName: CusName, CusAdd: CusAdd, CusTel: CusTel },
        loadonce: false
    }).trigger("reloadGrid");//重新载入

}

