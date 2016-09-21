var curPage = 1;
var OnePageCount = 15;
var UserName, UserID;
var oldSelID = 0;
var newSelID = 0;

$(document).ready(function () {
    $('#XZZS').click(function () {
        var sid = $("#Sid").val();
        window.parent.OpenDialog("新增证书", "../SuppliesManage/AddCertificate?sid=" + sid, 500, 350, '');
    })
    $('#GX').click(function () {

    })
    jq();
    jqDetail();
})
function reload() {
    var UserName = $('#UserName').val();
    $("#list").jqGrid('setGridParam', {
        url: 'ApprovalUserGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, userName: UserName },

    }).trigger("reloadGrid");
}
function jq() {
    var UserName = $('#UserName').val();
    $("#list").jqGrid({
        url: 'ApprovalUserGrid',
        datatype: 'json',
        height: 150,
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
        { name: 'Email', index: 'Email', width: 200 },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '人员表',
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='u" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).UserId + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
                if (curPage == $("#list1").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pagerU") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pagerU") {
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
           // $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function selChange(rowid) {
    if ($('input[id=u' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSelID != 0) {
            $('input[id=u' + oldSelID + ']').prop("checked", false);
        }
        $('input[id=u' + rowid + ']').prop("checked", true);
        $("#list").setSelection(rowid)
    }
}
function select(rowid) {
    UserID = jQuery("#list").jqGrid('getRowData', rowid).UserId;
    $("#list1").jqGrid('setGridParam', {
        url: 'ZSGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, userid: UserID },

    }).trigger("reloadGrid");
}
function reloadDetail() {
    $("#list1").jqGrid('setGridParam', {
        url: 'ZSGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, userid: UserID },

    }).trigger("reloadGrid");
}
function jqDetail() {
    jQuery("#list1").jqGrid({
        url: 'ZSGrid',
        datatype: 'json',
        height: 150,
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
        { name: 'CertificateName', index: 'CertificateName', width: 80 },
        { name: 'LastCertificatDate', index: 'LastCertificatDate', width: 80 },
        { name: 'CertificatDate', index: 'CertificatDate', width: 100 },
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        { name: 'State', index: 'State', width: 50, hidden: true },
        { name: 'Pro', index: 'AID', width: 50 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '证书信息表',
        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChangeDetail(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).CID + "' name='cb'/>";
                var modify = "<a style='color:blue' onclick='look(" + jQuery("#list1").jqGrid('getRowData', id).ID + ")'>查看</a>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list1").jqGrid('setRowData', ids[i], { Pro: modify });
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
                if (curPage == $("#list1").getGridParam("lastpage"))
                    return;
                curPage = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager1 :input").val();
            }
            reloadDetail();
        },
        loadComplete: function () {
          //  $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });

}
function selChangeDetail(rowid) {
    if ($('input[id=c' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSelID != 0) {
            $('input[id=c' + oldSelID + ']').prop("checked", false);
        }
        $('input[id=c' + rowid + ']').prop("checked", true);
        $("#list1").setSelection(rowid)
    }
}
function look(id) {
    window.parent.OpenDialog("证书图片", "../SuppliesManage/LookPicture?id=" + Id, 800, 500, '');
}
