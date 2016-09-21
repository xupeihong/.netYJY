var curPage = 1;
var OnePageCount = 15;
var UserName;
var UserID;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    jqU();

    $('#XZCZZZ').click(function () {
        window.parent.OpenDialog("新增持证资质", "../UserAptitude/AddAptitude", 800, 500, '');
    })

    $('#GX').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要更新的条目");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ID;
        window.parent.OpenDialog("修改持证资质", "../UserAptitude/UpdateAptitude?id=" + texts, 800, 500, '');
    })

    $('#JC').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要借出的条目");
            return;
        }
        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (state == 1) {
            alert("该资质证书已借出");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ID + "@" + jQuery("#list").jqGrid('getRowData', rowid).UserID + "@" + jQuery("#list").jqGrid('getRowData', rowid).UserName + "@" + jQuery("#list").jqGrid('getRowData', rowid).CertificatCode;
        window.parent.OpenDialog("借出持证资质", "../UserAptitude/LendAptitude?id=" + texts, 600, 500, '');
    })

    $('#GH').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要借出归还的条目");
            return;
        }
        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
        if (state != 1) {
            alert("该资质证书未借出,不能进行归还操作");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).ID + "@" + jQuery("#list").jqGrid('getRowData', rowid).UserID + "@" + jQuery("#list").jqGrid('getRowData', rowid).UserName + "@" + jQuery("#list").jqGrid('getRowData', rowid).CertificatCode;
        window.parent.OpenDialog("归还持证资质", "../UserAptitude/BackAptitude?id=" + texts, 450, 250, '');
    })
})
function reloadU() {
    UserName = $('#UserName').val();
    $("#listU").jqGrid('setGridParam', {
        url: 'AptitudeUserGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, userName: UserName },

    }).trigger("reloadGrid");
}

function jqU() {
    UserName = $('#UserName').val();
    jQuery("#listU").jqGrid({
        url: 'AptitudeUserGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, userName: UserName },
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
        colNames: ['', '人员ID', '人员姓名', '手机号', '邮箱'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'UserId', index: 'UserId', width: 70 },
        { name: 'UserName', index: 'UserName', width: 70 },
        { name: 'MobilePhone', index: 'MobilePhone', width: 200 },
        { name: 'Email', index: 'Email', width: $("#borU").width() - 730 },
        ],
        pager: jQuery('#pagerU'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '人员表',

        gridComplete: function () {
            var ids = jQuery("#listU").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#listU").jqGrid('getRowData', id);
                var curChk = "<input id='u" + id + "' onclick='selChangeU(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).UserId + "' name='cb'/>";
                jQuery("#listU").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=u' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=u' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            select(rowid);
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pagerU") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#listU").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pagerU") {
                curPage = $("#listU").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pagerU") {
                if (curPage == 1)
                    return;
                curPage = $("#listU").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pagerU") {
                curPage = 1;
            }
            else {
                curPage = $("#pagerU :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#listU").jqGrid("setGridHeight", $("#pageContent").height() - 420, false);
            $("#listU").jqGrid("setGridWidth", $("#borU").width() - 30, false);
        }
    });
}

function selChangeU(rowid) {
    if ($('input[id=u' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSelID != 0) {
            $('input[id=u' + oldSelID + ']').prop("checked", false);
        }
        $('input[id=u' + rowid + ']').prop("checked", true);
        $("#listU").setSelection(rowid)
    }
}

function select(rowid) {
    UserID = jQuery("#listU").jqGrid('getRowData', rowid).UserId;
    $("#list").jqGrid('setGridParam', {
        url: 'AptitudeGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, userid: UserID },

    }).trigger("reloadGrid");
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'AptitudeGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, userid: UserID },

    }).trigger("reloadGrid");
}

function jq() {
    jQuery("#list").jqGrid({
        url: 'AptitudeGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, userid: UserID },
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
        colNames: ['', '序号', '人员ID', '人员姓名', '业务类型', '发证单位', '证书级别', '取得资格时间', '证书编号', '证书名称', '最新批准日期', '证书到期日期', '状态', 'State', ''],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'ID', index: 'ID', width: 50, hidden: true },
        { name: 'UserID', index: 'UserID', width: 50 },
        { name: 'UserName', index: 'UserName', width: 70 },
        { name: 'BusinessTypeDesc', index: 'BusinessTypeDesc', width: 100 },
        { name: 'CertificateUnit', index: 'CertificateUnit', width: 80 },
        { name: 'TecoClass', index: 'TecoClass', width: 80 },
        { name: 'GetTime', index: 'GetTime', width: 70 },
        { name: 'CertificatCode', index: 'CertificatCode', width: 80 },
        { name: 'CertificateName', index: 'CertificateName', width: $("#bor").width() - 930 },
        { name: 'LastCertificatDate', index: 'LastCertificatDate', width: 80 },
        { name: 'CertificatDate', index: 'CertificatDate', width: 100 },
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        { name: 'State', index: 'State', width: 50, hidden: true },
        { name: 'Pro', index: 'AID', width: 50 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '资质信息表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).CID + "' name='cb'/>";
                var modify = "<a style='color:blue' onclick='look(" + jQuery("#list").jqGrid('getRowData', id).ID + ")'>查看</a>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list").jqGrid('setRowData', ids[i], { Pro: modify });
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
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 420, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
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

function look(Id) {
    window.parent.OpenDialog("资质证书图片", "../UserAptitude/LookPicture?id=" + Id, 800, 500, '');
}