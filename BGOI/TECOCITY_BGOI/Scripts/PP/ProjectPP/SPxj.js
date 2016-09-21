var curPage = 1;
var OnePageCount = 30;
var PID;
var RelenvceID;
var Type = "工程项目合同审批";
var ProID;
var Pname;
var StartDate;
var EndDate;
var Principal;
var oldSelID = 0;
var newSelID = 0;
var curPage1 = 1;
var OnePageCount1 = 6;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    //$("#QQ").width($("#search").width());
    $("#QQ").width($("#search").width());
    $("#QQ").height($("#pageContent").height() / 2 - 75);
    $("#RZJ").width($("#search").width());
    $("#RZJ").height($("#pageContent").height() / 2 - 75);
    jq();
    jq1();
    RZ();
    $('#QQJQdiv').click(function () {
        this.className = "btnTw";
        $('#RZJLdiv').attr("class", "btnTh");
        $("#QQ").css("display", "");
        $("#RZJ").css("display", "none");
    })

    $('#RZJLdiv').click(function () {
        this.className = "btnTw";
        $('#QQJQdiv').attr("class", "btnTh");
        $("#RZJ").css("display", "");
        $("#QQ").css("display", "none");
    })

    $('#SP').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行审批的条目");
            return;
        }
        else {
            var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var PID = jQuery("#list").jqGrid('getRowData', rowid).XJID;
            var state = jQuery("#list").jqGrid('getRowData', rowid).State;
            if (state == $("#Nostate").val()) {
                alert("审批不通过，不能进行审批了");
                return;
            }
            $.ajax({
                url: "../COM_Approval/JudgeAppDisable",
                type: "post",
                data: { data1: $("#webkey").val(), data2: SPID },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        var bol = data.intblo;
                        if (bol == "-1") {
                            alert("您没有审批权限，不能进行审批操作");
                            return;
                        }
                        if (bol == "1") {
                            alert("您已经审批完成，不能进行审批操作");
                            return;
                        }
                        if (bol == "2") {
                            alert("审批过程还没有进行到您这一步，不能进行审批操作");
                            return;
                        }
                        var texts = $("#webkey").val() + "@" + SPID + "@" + PID;
                        window.parent.OpenDialog("审批", "../COM_Approval/Approval?id=" + texts, 500, 400, '');
                    }
                    else {
                        return;
                    }
                }
            });
        }
    })

    $('#SPQK').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要查看审批情况的条目");
            return;
        }
        else {
            var SPID = jQuery("#list").jqGrid('getRowData', rowid).SPID;
            var texts = $("#webkey").val() + "@" + SPID;
            window.parent.OpenDialog("审批情况", "../COM_Approval/ApprovalCondition?id=" + texts, 700, 500, '');
        }
    })
})

function reload() {
    if ($('.field-validation-error').length == 0) {
        XJID = $('#XJID').val();
        PID = $('#PID').val();
        Begin = $('#Begin').val();
        End = $('#End').val();
        $("#list").jqGrid('setGridParam', {
            url: 'SelectSP',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, XJID: XJID, PID: PID, Begin: Begin, End: End },

        }).trigger("reloadGrid");
    }
}

function jq() {
    XJID = $('#XJID').val();
    PID = $('#PID').val();
    Begin = $('#Begin').val();
    End = $('#End').val();
 
    jQuery("#list").jqGrid({
        url: 'SelectSP',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, XJID: XJID, PID: PID, Begin: Begin, End: End },
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
        colNames: ['询价编号','询价标题','询价时间','审批编号','询价人','审批状态','State'],
        colModel: [
        { name: 'XJID', index: 'XJID', width: 110 },
        { name: 'InquiryTitle', index: 'InquiryTitle', width: 170 },
        { name: 'InquiryDate', index: 'InquiryDate', width: 110 },
        { name: 'PID', index: 'PID', width: 170 },
        { name: 'UserName', index: 'UserName', width: 150 },
        { name: 'Text', index: 'Text', width: 100 },
        { name: 'State', index: 'State', width: 100, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '询价审批表',
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            select(rowid);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 166, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function ShowDetail(id) {
    window.parent.OpenDialog("详细内容", "../ProjectManage/DetailApp?id=" + id, 700, 500, '');
}

function ShowContract(id) {
    window.parent.OpenDialog("合同文件下载", "../Contract/DownloadFile?id=" + id, 400, 200, '');
}

function selChange(rowid) {
    if ($('input[id=c' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSelID != 0) {
            $('input[id=c' + oldSelID + ']').prop("checked", false);
        }
        $('input[id=c' + rowid + ']').prop("checked", true);
        $("#list").setSelection(rowid)
    }
}

function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    RelenvceID = jQuery("#list").jqGrid('getRowData', rowid).CID;
    reload1();
}

function reload1() {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list1").jqGrid('setGridParam', {
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}

function jq1() {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list1").jqGrid({
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },
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
        colNames: ['', '职务', '姓名', '审批方式', '人数', '审批情况', '审批意见', '审批时间', '备注'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'Job', index: 'Job', width: 100 },
        { name: 'UserName', index: 'UserName', width: 100 },
        { name: 'AppTypeDesc', index: 'AppTypeDesc', width: 100 },
        { name: 'Num', index: 'Num', width: 100 },
        { name: 'stateDesc', index: 'stateDesc', width: 100 },
        { name: 'Opinion', index: 'Opinion', width: $("#bor").width() - 920 },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 150 },
        { name: 'Remark', index: 'Remark', width: 200 },
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                //var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                //jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
            if (pgButton == "next_pager1") {
                if (curPage == $("#list1").getGridParam("lastpage"))
                    return;
                curPage = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage = 1;
            }
            else {
                curPage = $("#pager1 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}


function RZ() {
    var Type = "询价审批";
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
        var Type = "询价审批";
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

    XJID = $('#XJID').val();
    PID = $('#PID').val();
    Begin = $('#Begin').val();
    End = $('#End').val();
    $("#list").jqGrid('setGridParam', {
        url: 'SelectSP',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, XJID: XJID, PID: PID, Begin: Begin, End: End },
        loadonce: false,

    }).trigger("reloadGrid");//重新载入

}