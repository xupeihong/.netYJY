var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var SID;
var COMNameC;
var Contacts;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    jq();
    $("#Add").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var texts = jQuery("#list").jqGrid('getRowData', rowid).SID;
        window.parent.OpenDialog("添加供应商", "../SuppliesManage/Addisnotsuply?sid=" + texts, 900, 250, '');
    })
    $("#Update").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没有选择要更新的供应商信息");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).SID;
        window.parent.OpenDialog("更新供应商信息", "../SuppliesManage/Updateisnotsuply?sid=" + texts, 900, 250, '');
    })
})
function jq() {
    COMNameC = $("#COMNameC").val();
    Contacts = $("#Contacts").val();
    $("#list").jqGrid({
        url: 'NoSupplyGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount,comname:COMNameC,contact:Contacts },
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
        colNames: ['', '序号', '厂家名称', '供货内容', '联系人', '电话/传真', '手机', '邮箱', '部门', '状态'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 50, hidden: true },
        { name: 'COMNameC', index: 'COMNameC', width: 100 },
        { name: 'SupplyContent', index: 'SupplyContent', width: 90 },
        { name: 'Contacts', index: 'Contacts', width: 70 },
        { name: 'TelFax', index: 'TelFax', width: 150 },
        { name: 'Phone', index: 'Phone', width: 100 },
        { name: 'Mailbox', index: 'Mailbox', width: 120 },
        { name: 'deptname', index: 'deptname', width: 60 },
        { name: 'State', index: 'State', width: 60, hidden: true },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        rownumbers: true,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '非合格供应商信息表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selchange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).SID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var sid = jQuery("#list").jqGrid('getRowData', rowid).SID
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
            reload23();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function deleteCus() {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    var SID = jQuery("#list").jqGrid('getRowData', rowid).SID;
    if (rowid == null) {
        alert("您还没有选择要删除的供应商信息");
        return;
    }
    else {
        var one = confirm("确实要删除该供应商信息吗");
        if (one == false) {
            return;
        } else {
            $.ajax({
                url: "Deleisnotsuply",
                type: "post",
                data: { data1: SID },
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
function reload() {
    COMNameC = $("#COMNameC").val();
    Contacts = $("#Contacts").val();
    $("#list").jqGrid('setGridParam', {
        url: 'NoSupplyGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, comname: COMNameC, contact: Contacts },
    }).trigger("reloadGrid");

}
function reload23() {
    curPage = 1;
    COMNameC = $("#COMNameC").val();
    Contacts = $("#Contacts").val();
    $("#list").jqGrid('setGridParam', {
        url: 'NoSupplyGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, comname: COMNameC, contact: Contacts },
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