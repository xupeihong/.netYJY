
var curPage = 1;
var OnePageCount = 20;
var UserName;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $("#QD").click(function () {
        var parent = $("#parentID").val();
        var userIDs = $("#userIDs").val();
        var selectedIds = $("#list").jqGrid("getGridParam", "selarrrow");
        var texts;
        var val;
        var judgeUser = "";
        var job;
        for (var i = 0 ; i < selectedIds.length; i++)
        {
            texts += jQuery("#list").jqGrid('getCell', selectedIds[i], 'UserName') + ",";
            judgeUser = jQuery("#list").jqGrid('getCell', selectedIds[i], 'UserId');
            if (userIDs.indexOf(judgeUser) >= 0)
            {
                alert("您选择的人员中有重复人员，不能进行保存");
                return;
            }
            val += jQuery("#list").jqGrid('getCell', selectedIds[i], 'UserId') + ",";
            job += jQuery("#list").jqGrid('getCell', selectedIds[i], 'ExJob') + ",";
        }
        texts = texts.substr(0, texts.length - 1);
        texts = texts.replace("undefined", "");
        val = val.substr(0, val.length - 1);
        val = val.replace("undefined", "");
        job = job.substr(0, job.length - 1);
        job = job.replace("undefined", "");
        if (texts == "") {
            alert("您还没有选择审批人员");
            return;
        }
        var one = confirm("确定选择 " + texts + "吗")
        if (one == false)
            return;
        else {
            window.parent.document.getElementById("Text" + parent).value = texts;
            window.parent.document.getElementById("Hid" + parent).value = val;
            window.parent.document.getElementById("HidW" + parent).value = job;
            alert("选择成功");
            setTimeout('parent.ClosePop()', 100);
        }
    })
})

function reload() {
    UserName = $('#UserName').val();
    $("#list").jqGrid('setGridParam', {
        url: 'UserGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, uaskname: UserName },

    }).trigger("reloadGrid");
}

function jq() {
    UserName = $('#UserName').val();
    jQuery("#list").jqGrid({
        url: 'UserGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, uaskname: UserName },
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
        colNames: [ '用户编号', '用户姓名','审批职务', '单位id'],
        colModel: [
        { name: 'UserId', index: 'UserId', width: 150 },
        { name: 'UserName', index: 'UserName', width: 150 },
        { name: 'ExJob', index: 'ExJob', width: 150 },
        { name: 'DeptId', index: 'DeptId', width: 50, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批人员表',
        multiselect: true,

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