var curPage = 1;
var OnePageCount = 15;
var UserName, BusinessType, TecoName;
var oldSelID = 0;
var newSelID = 0;

$(document).ready(function () {
    $("#search").width($("#bor").width() - 30);
    jq();
    $('#TimeOut').click(function () {
        window.parent.OpenDialog("设置提醒时间", "../SupplyManage/SetWarnTime", 450, 200, '');
    })
})
function reload() {
    UserName = $('#UserName').val();
    BusinessType = $('#BusinessType').val();
    TecoName = $('#TecoName').val();
    $("#list").jqGrid('setGridParam', {
        url: 'ApprovalWarnGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, userName: UserName, businessType: BusinessType, tecoName: TecoName },

    }).trigger("reloadGrid");
}
function jq() {
    UserName = $('#UserName').val();
    BusinessType = $('#BusinessType').val();
    TecoName = $('#TecoName').val();
    jQuery("#list").jqGrid({
        url: 'ApprovalWarnGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, userName: UserName, businessType: BusinessType, tecoName: TecoName },
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
        colNames: ['', '序号', '人员ID', '人员姓名', '业务类型', '专业技术资格名称', '专业技术级别', '取得资格时间', '证书编号', '证书名称', '最新批准日期', '证书到期日期', '状态', 'State'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'ID', index: 'ID', width: 50, hidden: true },
        { name: 'UserID', index: 'UserID', width: 70 },
        { name: 'UserName', index: 'UserName', width: 70 },
        { name: 'BusinessTypeDesc', index: 'BusinessTypeDesc', width: 100 },
        { name: 'TecoName', index: 'TecoName', width: 100 },
        { name: 'TecoClass', index: 'TecoClass', width: 80 },
        { name: 'GetTime', index: 'GetTime', width: 70 },
        { name: 'CertificatCode', index: 'CertificatCode', width: 80 },
        { name: 'CertificateName', index: 'CertificateName', width: $("#bor").width() - 930 },
        { name: 'LastCertificatDate', index: 'LastCertificatDate', width: 80 },
        { name: 'CertificatDate', index: 'CertificatDate', width: 100 },
        { name: 'StateDesc', index: 'StateDesc', width: 70 },
        { name: 'State', index: 'State', width: 50, hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '人员证书表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).CID + "' name='cb'/>";
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
            reload();
        },
        loadComplete: function () {
          //  $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
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