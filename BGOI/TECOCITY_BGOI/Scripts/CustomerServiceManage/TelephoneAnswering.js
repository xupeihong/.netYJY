var curPage = 1;
var OnePageCount =9;
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
                var DHID = Model.DHID;
                $.ajax({
                    type: "POST",
                    url: "DeTelephoneAnswering",
                    data: { DHID: DHID },
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
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).BXKID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).BXKID;
            var url = "PrintWarrCard?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})

function ScrapManagementOut() {

    window.parent.parent.OpenDialog("电话记录", "../CustomerService/AddTelephoneAnswering", 800, 550);

}
function ScrapManagementUP() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择操作的行");
        return;
    }
    window.parent.parent.OpenDialog("修改电话记录", "../CustomerService/UpTelephoneAnswering?DHID=" + Model.DHID, 800, 550);
}

function jq() {
    var UserName = $('#UserName').val();
    var Address = $('#Address').val();
    var Tel = $('#Tel').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    jQuery("#list").jqGrid({
        url: 'TelephoneAnsweringList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, UserName: UserName, Address: Address, Begin: Begin, End: End, Tel: Tel },
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
        colNames: ['序号', '电话编号', '接听时间', '地址内容', '联系人', '联系电话', '派工单号', '处理结果', '备注', '记录人'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'DHID', index: 'DHID', width: 120, align: "center" },
        { name: 'AnswerDate', index: 'AnswerDate', width: 120, align: "center" },
        { name: 'Address', index: 'Address', width: 150, align: "center" },
        { name: 'UserName', index: 'UserName', width: 100, align: "center" },
        { name: 'Tel', index: 'Tel', width: 80, align: "center" },
        { name: 'SchoolWork', index: 'SchoolWork', width: 100, align: "center" },
        { name: 'ProcessingResults', index: 'ProcessingResults', width: 80, align: "center" },
        { name: 'Remark', index: 'Remark', width: 80, align: "center" },
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
                var DHID = Model.DHID;
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 220, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function reload() {
    var UserName = $('#UserName').val();
    var Address = $('#Address').val();
    var Tel = $('#Tel').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    $("#list").jqGrid('setGridParam', {
        url: 'TelephoneAnsweringList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, UserName: UserName, Address: Address, Begin: Begin, End: End, Tel: Tel },

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
    var UserName = $('#UserName').val();
    var Address = $('#Address').val();
    var Tel = $('#Tel').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    //var State = $("input[name='State']:checked").val();
    //if (State == "1") {
    //    $('#Fin').hide();
    //} else {
    //    $('#Fin').show();
    //}
    $("#list").jqGrid('setGridParam', {
        url: 'TelephoneAnsweringList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, UserName: UserName, Address: Address, Begin: Begin, End: End, Tel: Tel },
        loadonce: false
    }).trigger("reloadGrid");//重新载入

}

