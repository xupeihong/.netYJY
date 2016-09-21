var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    var shenchan;
    if (location.search != "") {
        shenchan = location.search.split('&')[0].split('=')[1];
    }
    if (shenchan == "1") {
        $("#kuc").hide();
        $("#shenchanyong").show();
    }
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
            var msg = "您真的确定要撤销吗?";
            if (confirm(msg) == true) {
                var SID = Model.SID;
                $.ajax({
                    type: "POST",
                    url: "DeInOutUpload",
                    data: { SID: SID },
                    success: function (data) {
                        alert(data.Msg);
                        reloadWard();
                    },
                    dataType: 'json'
                });
                return true;
            } else {
                return false;
            }
        }
    });

    $("#CK").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            window.parent.parent.OpenDialog("查看", "../InventoryManage/ViewPicture?Oid=" + Model.SID, 800, 550);
        }
    });
    // 打印
    $("#btnPrint").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).PID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
            var url = "PrintInventoryAddPro?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
})

function reloadWard() {
    var Award = $("#Award").val();
    $("#list").jqGrid('setGridParam', {
        url: 'InOutUploadList',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, Award: Award },
    }).trigger("reloadGrid");
}
function jq() {
    var Award = $("#Award").val();
    jQuery("#list").jqGrid({
        url: 'InOutUploadList',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, Award: Award },
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
        colNames: ['', 'ID', '序号', '文件名称', '上传时间', '操作'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 100, hidden: true },
        { name: 'ID', index: 'ID', width: 150, hidden: true },
        { name: 'SID', index: 'SID', width: 150, hidden: true },
        { name: 'Award', index: 'Award', width: 350 },
        { name: 'AwardTime', index: 'AwardTime', width: 250 },
        { name: 'Opration', index: 'Opration', width: 80 },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='j" + id + "' onclick='selAward(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).CID + "' name='cb'/>";
                var cancel = "<a onclick='DownloadAward(" + id + ")' style='color:blue;cursor:pointer;'>下载</a>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid, status) {
            //if (oldAward != 0) {
            //    $('input[id=j' + oldAward + ']').prop("checked", false);
            //}
            //$('input[id=j' + rowid + ']').prop("checked", true);
            //oldAward = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("list") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list").getGridParam("list") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#list :input").val();
            }
            reloadWard();
        },
        loadComplete: function () {
            var re_records = $("#list").getGridParam('records');
            if (re_records == 0 || re_records == null) {
                if ($(".norecords").html() == null) {
                    $("#list").parent().append("<div class=\"norecords\" style='text-align:center;margin-top:50px;font-size:20px;'>没有符合数据</div>");
                }
                $(".norecords").show();
            }
            else
                $(".norecords").hide();
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 180, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function Search() {
    curRow = 0;
    curPage = 1;
    var Award = $("#Award").val();
    $("#list").jqGrid('setGridParam', {
        url: 'InOutUploadList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, Award: Award
        },
        loadonce: false
    }).trigger("reloadGrid");//重新载入
}
function AddInventoryAddPro() {
    window.parent.parent.OpenDialog("上传", "../InventoryManage/AddInOutUpload", 800, 280);
}

function DownloadAward(id) {
    var model = jQuery("#list").jqGrid('getRowData', id);
    var Proid = model.ID;//唯一编号
    var job = $("#Department").val();
    window.open("DownLoadAward?sid=" + Proid);

}

