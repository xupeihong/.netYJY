var curPage = 1;
var curPageCP = 1;
var curPageShare = 1;
var curPageLog = 1;
var OnePageCount = 15;
var OnePageCountCP = 10;
var OnePageCountShare = 10;
var OnePageCountLog = 10;
var oldSelID = 0;
var oldRelationID = 0;
var oldShare = 0;
var oldLog = 0;
var CName, Industry, Products, CType, CRelation, GainDate, WType, DeclareUser, State, KID, Share;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    jq();
    jqPerson();
    $("#lianxiren").click(function () {
        this.className = "btnTw";
        $('#Unite').attr("class", "btnTh");
        $('#sysLog').attr("class", "btnTh");

        $("#one").css("display", "");
        $("#two").css("display", "none");
        $("#three").css("display", "none");

    })
    $("#Unite").click(function () {

        this.className = "btnTw";
        $('#lianxiren').attr("class", "btnTh");
        $('#sysLog').attr("class", "btnTh");

        $("#two").css("display", "");
        $("#one").css("display", "none");
        $("#three").css("display", "none");
        jqShare();
    })
    $("#sysLog").click(function () {

        this.className = "btnTw";
        $('#lianxiren').attr("class", "btnTh");
        $('#Unite').attr("class", "btnTh");

        $("#three").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        jqLog();
    })
    $("#DetailMsg").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要查看详细的客户信息");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).KID;
        window.parent.OpenDialog("客户详细", "../SuppliesManage/DetailCustomer?kid=" + texts, 900, 350, '');
    })
    $("#Add").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var texts = jQuery("#list").jqGrid('getRowData', rowid).KID;
        window.parent.OpenDialog("添加客户", "../SuppliesManage/AddCustomer?kid=" + texts, 900, 550, '');
    })
    $("#Update").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要更新的客户信息");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).KID;
        window.parent.OpenDialog("更新客户信息", "../SuppliesManage/UpdateCustomer?kid=" + texts, 900, 450, '');
    })
    $("#AddPerson").click(function () {
        //var kid = $("#KID").val();
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var kid = jQuery("#list").jqGrid('getRowData', rowid).KID;
        if (rowid == null) {
            alert("您还没有选择要添加的联系人");
            return;
        }
        window.parent.OpenDialog("添加联系人", "../SuppliesManage/AddPerson?kid=" + kid, 800, 300, '');
    })
    $("#AddUnite").click(function () {
        //var kid = $("#KID").val();
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var kid = jQuery("#list").jqGrid('getRowData', rowid).KID;
        if (rowid == null) {
            alert("您还没有选择要添加的共享单位");
            return;
        }
        window.parent.OpenDialog("添加共享单位", "../SuppliesManage/AddUnite?kid=" + kid, 450, 200, '');
    })
})
function reload() {
    if ($('.field-validation-error').length == 0) {
        CName = $("#CName").val();
        CType = $("#CType").val();
        CRelation = $("#CRelation").val();
        GainDate = $("#GainDate").val();;
        State = $("#State").val();
        $("#list").jqGrid('setGridParam', {
            url: 'CustomerGrid',
            datatype: 'json',
            height: 150,
            postData: { curpage: curPage, rownum: OnePageCount, cname: CName, ctype: CType, crelation: CRelation, gandate: GainDate, state: State },
        }).trigger("reloadGrid");
    }
}
function reloadMan() {
    $("#list1").jqGrid('setGridParam', {
        url: 'PersonGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageCP, rownum: OnePageCountCP, kid: KID },
    }).trigger("reloadGrid");
}
function reloadShare() {
    $("#list2").jqGrid('setGridParam', {
        url: 'ShareGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageShare, rownum: OnePageCountShare, kid: KID, share: Share },
    }).trigger("reloadGrid");
}
function reloadLog() {
    $("#list3").jqGrid('setGridParam', {
        url: 'LogShowGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageLog, rownum: OnePageCountLog, kid: KID },
    }).trigger("reloadGrid");
}
function selchange(rowid) {
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
function selchangeMan(rowid2) {
    if ($('input[id=d' + rowid2 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldRelationID != 0) {
            $('input[id=d' + oldRelationID + ']').prop("checked", false);
        }
        $('input[id=d' + rowid2 + ']').prop("checked", true);
        $("#list1").setSelection(rowid2)
    }
}
function selchangeShare(rowid3) {
    if ($('input[id=f' + rowid3 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldShare != 0) {
            $('input[id=f' + oldShare + ']').prop("checked", false);
        }
        $('input[id=f' + rowid3 + ']').prop("checked", true);
        $("#list2").setSelection(rowid3)
    }
}
function selChangeLog(rowid4) {
    if ($('input[id=g' + rowid4 + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldLog != 0) {
            $('input[id=g' + oldLog + ']').prop("checked", false);
        }
        $('input[id=g' + rowid4 + ']').prop("checked", true);
        $("#list3").setSelection(rowid4)
    }
}
function jq() {
    CName = $("#CName").val();
    CType = $("#CType").val();
    CRelation = $("#CRelation").val();
    GainDate = $("#GainDate").val();
    State = $("#State").val();
    $("#list").jqGrid({
        url: 'CustomerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, cname: CName, ctype: CType, crelation: CRelation, gandate: GainDate, state: State },
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
        colNames: ['', '序号', '客户名称', '是否共享', '所属省份', '客户地址', '客户座机', '传真', '人员规模', '所属行业', '客户类别', '客户状态', '填表人', '客户关系'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50 },
        { name: 'KID', index: 'KID', width: 50, hidden: true },
        { name: 'CName', index: 'CName', width: 80 },
        { name: 'IsShare', index: 'IsShare', width: 80 },
        { name: 'Province', index: 'Province', width: 70 },
        { name: 'CSource', index: 'CSource', width: 150 },
        { name: 'Phone', index: 'Phone', width: 100 },
        { name: 'FAX', index: 'FAX', width: 100 },
        { name: 'StaffSize', index: 'StaffSize', width: 60 },
        { name: 'Industry', index: 'Industry', width: 150 },
        { name: 'CType', index: 'CType', width: 100 },
        { name: 'State', index: 'State', width: 50 },
        { name: 'DeclareUser', index: 'DeclareUser', width: 50 },
        { name: 'CRelation', index: 'CRelation', width: 50 },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '客户信息表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selchange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).KID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var kid = jQuery("#list").jqGrid('getRowData', rowid).KID
            var share = jQuery("#list").jqGrid('getRowData', rowid).IsShare
            loadPerson(kid);
            loadUnit(kid, share);
            loadLog(kid);
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
function jqPerson() {
    $("#list1").jqGrid({
        url: 'PersonGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageCP, rownum: OnePageCountCP, kid: KID },
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
        colNames: ['', '序号', '姓名', '性别', '职务', '生日', '手机', '邮箱', 'QQ', '微信', '时间', '操作'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'KID', index: 'KID', width: 50, hidden: true },
        { name: 'CName', index: 'CName', width: 80 },
        { name: 'Sex', index: 'Sex', width: 40 },
        { name: 'Job', index: 'Job', width: 70 },
        { name: 'Birthday', index: 'Birthday', width: 150 },
        { name: 'Mobile', index: 'Mobile', width: 100 },
        { name: 'Email', index: 'Email', width: 100 },
        { name: 'QQ', index: 'QQ', width: 60 },
        { name: 'WeiXin', index: 'WeiXin', width: 120 },
        { name: 'CreateTime', index: 'CreateTime', width: 150 },
        { name: 'Opration', index: 'Opration', width: 150 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCountCP,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '联系人信息表',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='d" + id + "' onclick='selchangeMan(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).KID + "' name='cb'/>";
                var cancel = "<a  onclick='deleteMan(" + id + ")' style='color:blue;cursor:pointer;'>删除</a>" + "  <a onclick='UpdatePerson(" + id + ")' style='color:blue;cursor:pointer;'>修改</a>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                jQuery("#list1").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldRelationID != 0) {
                $('input[id=d' + oldRelationID + ']').prop("checked", false);
            }
            $('input[id=d' + rowid + ']').prop("checked", true);
            oldRelationID = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPageCP == $("#list1").getGridParam("lastpage"))
                    return;
                curPageCP = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPageCP = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPageCP == 1)
                    return;
                curPageCP = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPageCP = 1;
            }
            else {
                curPageCP = $("#pager1 :input").val();
            }
            reloadMan();
        },
        loadComplete: function () {
           // $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqShare() {
    $("#list2").jqGrid({
        url: 'ShareGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageShare, rownum: OnePageCountShare, kid: KID, share: Share },
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
        colNames: ['', '序号', '共享部门', '操作'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'KID', index: 'KID', width: 50, hidden: true },
        { name: 'ShareUnits', index: 'ShareUnits', width: 380 },
        { name: 'Opration', index: 'Opration', width: 150, hidden: true },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCountShare,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '共享部门信息表',

        gridComplete: function () {
            var ids = jQuery("#list2").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list2").jqGrid('getRowData', id);
                var curChk = "<input id='f" + id + "' onclick='selchangeShare(" + id + ")' type='checkbox' value='" + jQuery("#list2").jqGrid('getRowData', id).KID + "' name='cb'/>";
                //var cancel = "<a onclick='deleteShare(" + id + ")' style='color:blue;cursor:pointer;'>删除</a>" + "  <a onclick='UpdateUnit(" + id + ")' style='color:blue;cursor:pointer;'>修改</a>";
                jQuery("#list2").jqGrid('setRowData', ids[i], { IDCheck: curChk });
                // jQuery("#list2").jqGrid('setRowData', ids[i], { Opration: cancel });
            }
        },
        onSelectRow: function (rowid, status) {
            if (selchangeShare != 0) {
                $('input[id=f' + selchangeShare + ']').prop("checked", false);
            }
            $('input[id=f' + rowid + ']').prop("checked", true);
            selchangeShare = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPageShare == $("#list2").getGridParam("lastpage"))
                    return;
                curPageShare = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPageShare = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPageShare == 1)
                    return;
                curPageShare = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPageShare = 1;
            }
            else {
                curPageShare = $("#pager2 :input").val();
            }
            reloadShare();
        },
        loadComplete: function () {
           // $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqLog() {
    $("#list3").jqGrid({
        url: 'LogShowGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageLog, rownum: OnePageCountLog, kid: KID },
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
        colNames: ['', '供应商编号', '日志标题', '日志内容', '记录时间', '记录人', '日志类型'],
        colModel: [
       { name: 'IDCheck', index: 'id', width: 50, hidden: true },
       { name: 'UserId', index: 'UserId', width: 100 },
       { name: 'LogTitle', index: 'LogTitle', width: 180 },
       { name: 'LogContent', index: 'LogContent', width: 130 },
       { name: 'LogTime', index: 'LogTime', width: 150 },
       { name: 'LogPerson', index: 'LogPerson', width: 150 },
       { name: 'Type', index: 'Type', width: 150 },
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCountLog,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '日志记录',

        gridComplete: function () {
            var ids = jQuery("#list3").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list3").jqGrid('getRowData', id);
                var curChk = "<input id='g" + id + "' onclick='selChangeLog(" + id + ")' type='checkbox' value='" + jQuery("#list3").jqGrid('getRowData', id).KID + "' name='cb'/>";
                jQuery("#list3").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldLog != 0) {
                $('input[id=g' + oldLog + ']').prop("checked", false);
            }
            $('input[id=g' + rowid + ']').prop("checked", true);
            oldLog = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (curPageLog == $("#list3").getGridParam("lastpage"))
                    return;
                curPageLog = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPageLog = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPageLog == 1)
                    return;
                curPageLog = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPageLog = 1;
            }
            else {
                curPageLog = $("#pager3 :input").val();
            }
            reloadLog();
        },
        loadComplete: function () {
            //$("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function deleteCus() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var state = jQuery("#list").jqGrid('getRowData', rowid).State;
    if (rowid == null) {
        alert("您还没有选择要删除客户信息");
        return;
    } else if (state == "已删除") { alert("该客户已经删除，不能重复操作"); return; }
    else {
        var one = confirm("确实要删除客户信息吗");
        if (one == false) {
            return;
        } else {
            $.ajax({
                url: "DeleCus",
                type: "post",
                data: { data1: KID },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        alert(data.Msg);
                        $("#list").trigger('reloadGrid');
                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });
        }
    }
}
function loadPerson(kid) {
    KID = kid;
    $("#list1").jqGrid('setGridParam', {
        url: 'PersonGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageCP, rownum: OnePageCountCP, kid: KID },
    }).trigger("reloadGrid");
}
function loadUnit(kid, share) {
    KID = kid;
    Share = share;
    $("#list2").jqGrid('setGridParam', {
        url: 'ShareGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageShare, rownum: OnePageCountShare, kid: KID, share: Share },
    }).trigger("reloadGrid");
}
function loadLog(kid) {
    KID = kid;
    $("#list3").jqGrid('setGridParam', {
        url: 'LogShowGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPageLog, rownum: OnePageCountLog, kid: KID },
    }).trigger("reloadGrid");
}
function deleteMan(id) {
    var model = jQuery("#list1").jqGrid('getRowData', id);
    var KID = model.KID;
    var one = confirm("确实要删除联系人信息吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deletepPerson",
            type: "post",
            data: { data1: KID },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    $("#list1").trigger('reloadGrid');
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
        $("#list1").trigger('reloadGrid');
        $("#list").trigger('reloadGrid');
    }
}
function deleteShare(id) {
    var model = jQuery("#list2").jqGrid('getRowData', id);
    var KID = model.KID;
    var one = confirm("确实要删除共享部门信息吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deletepShareUnit",
            type: "post",
            data: { data1: KID },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    $("#list2").trigger('reloadGrid');
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
        $("#list2").trigger('reloadGrid');
        $("#list").trigger('reloadGrid');
    }
}
function UpdatePerson(id) {
    var model = jQuery("#list1").jqGrid('getRowData', id);
    var KID = model.KID;
    var time = model.CreateTime;
    window.parent.OpenDialog("联系人信息", "../SuppliesManage/UPPerson?kid=" + KID + "&time=" + time, 900, 350, '');
}
function UpdateUnit(id) {
    var model = jQuery("#list2").jqGrid('getRowData', id);
    var kid = model.KID;
    window.parent.OpenDialog("共享部门信息", "../SuppliesManage/UPUnite?kid=" + kid, 500, 200, '');
}
function OutExcel() {
    var record = $("#list").getGridParam("reccount");
    if (record == 0) {
        alert("列表内容为空，没有要导出的数据，不能进行导出操作");
        return false;
    }
    else {
        var one = confirm("确定将列表内容导出吗？")
        if (one == false) {
            return false;
        }
        else {
            return true;
        }
    }
}
window.onload = function () {
    document.getElementsByTagName("body")[0].onkeydown = function () {

        //获取事件对象  
        var elem = event.relatedTarget || event.srcElement || event.target || event.currentTarget;

        if (event.keyCode == 8) {//判断按键为backSpace键  

            //获取按键按下时光标做指向的element  
            var elem = event.srcElement || event.currentTarget;

            //判断是否需要阻止按下键盘的事件默认传递  
            var name = elem.nodeName;

            if (name != 'INPUT' && name != 'TEXTAREA') {
                return _stopIt(event);
            }
            var type_e = elem.type.toUpperCase();
            if (name == 'INPUT' && (type_e != 'TEXT' && type_e != 'TEXTAREA' && type_e != 'PASSWORD' && type_e != 'FILE')) {
                return _stopIt(event);
            }
            if (name == 'INPUT' && (elem.readOnly == true || elem.disabled == true)) {
                return _stopIt(event);
            }
        }
    }

    function _stopIt(e) {
        if (e.returnValue) {
            e.returnValue = false;
        }
        if (e.preventDefault) {
            e.preventDefault();
        }

        return false;
    }
}