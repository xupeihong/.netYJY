var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var SID, UserName, ApprovalLever, ApprovalContent, ApprovalPersons, ApprovalTime, ApprovalType;


$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        //window.parent.frames["iframeRight"].reload();
        window.parent.ClosePop();
    }
    jq();
    ck_check();
    $("#hole").height($(window).height());
    $("#sure1").click(function () {
        if ($("#Job").val() == "") {
            alert("职务描述不能为空"); return;
        }
        if ($("#ApprovalPerson").val() == "") {
            alert("审批人不能为空"); return;
        }
        if ($("#ApprovalContent").val() == "") {
            alert("业务类型不能为空"); return;
        }
        if ($("#ApprovalLever").val() == "") {
            alert("级别不能为空"); return;
        }
        var res = confirm("确定要提交审批过程信息吗？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }
    });
})
function reload() {
    SID = $("#SID").val();
    ApprovalLever = $("#ApprovalLever").val();
    ApprovalContent = $("#ApprovalContent").val();
    ApprovalPersons = $("#ApprovalPersons").val();
    ApprovalTime = $("#ApprovalTime").val();
    ApprovalType = $("#ApprovalType").val();
    $("#list").jqGrid('setGridParam', {
        url: 'ApprovalGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: SID, appLever: ApprovalLever, appcontent: ApprovalContent, apppersons: ApprovalPersons, apptime: ApprovalTime, apptype: ApprovalType },
    }).trigger("reloadGrid");
}
function selChange(rowid) {
    if ($('input[id=g' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSelID != 0) {
            $('input[id=g' + oldSelID + ']').prop("checked", false);
        }
        $('input[id=g' + rowid + ']').prop("checked", true);
        $("#list").setSelection(rowid)
    }
}
function jq() {
    SID = $("#SID").val();
    ApprovalLever = $("#ApprovalLever").val();
    ApprovalContent = $("#ApprovalContent").val();
    ApprovalPersons = $("#ApprovalPersons").val();
    ApprovalTime = $("#ApprovalTime").val();
    ApprovalType = $("#ApprovalType").val();
    jQuery("#list").jqGrid({
        url: 'ApprovalGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: SID, appLever: ApprovalLever, appcontent: ApprovalContent, apppersons: ApprovalPersons, apptime: ApprovalTime, apptype: ApprovalType },
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
        colNames: ['', '序号', '审批人', '职务', '方式'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50 },
        { name: 'PID', index: 'PID', width: 200, hidden: true },
        { name: 'userName', index: 'userName', width: 50 },
        { name: 'Duty', index: 'Duty', width: 200 },
        { name: 'BuType', index: 'BuType', width: 90 },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批信息',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='g" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=g' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=g' + rowid + ']').prop("checked", true);
            oldSelID = rowid;

            var userName = jQuery("#list").jqGrid('getRowData', rowid).UserName;


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
           // $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function ck_check() {
    $('.ckb').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("ApprovalType").value = res;
            $('.ckb').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb').attr("disabled", false);
        }
    })
}