var curPage = 1;
var OnePageCount = 15;
var sel;
var GridFunction;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $('#XZ').click(function () {
        if (sel == "") {
            alert("请选择添加内容的编辑项");
            return;
        }
        window.parent.OpenDialog("新增内容", "../SystemManage/AddContent?id=" + sel, 400, 200, '');
    })
    jq();
})
function change() {
    sel = $("#sel").val();
    reload();
}
function reload() {
    sel = $("#sel").val();
    $("#list").jqGrid('setGridParam', {
        url: "BasMangeGrid",
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, sel: sel },
        caption: '基本信息配置表',
        loadonce: false,
        colNames: ['序号', '参数名称', 'XID', 'Type', '操作', '操作'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 90 },
        { name: 'Text', index: 'Text', width: $("#bor").width() - 250 },
        { name: 'XID', index: 'XID', width: 200, hidden: true },
        { name: 'Type', index: 'Type', width: 200, hidden: true },
        { name: 'Pro', index: 'XID', width: 50 },
        { name: 'Prodel', index: 'XID', width: 50 }
        ],
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
        },
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var Type;
                var XID;
                var modify;
                var del;
                var Text;

                Type = jQuery("#list").jqGrid('getRowData', id).Type;
                XID = jQuery("#list").jqGrid('getRowData', id).XID;
                Text = jQuery("#list").jqGrid('getRowData', id).Text;
                modify = "<a  href='#' onclick='UpdateType(\"" + XID + "\",\"" + Type + "\",\"" + Text + "\")' style='color:blue'>修改</a>";
                del = "<a href='#' style='color:#f60' onclick='DeleteType(" + XID + ",\"" + Type + "\")' >删除</a>";

                jQuery("#list").jqGrid('setRowData', ids[i], { Pro: modify, Prodel: del });
            }

        }
    }).trigger("reloadGrid");
}

function jq() {
    sel = $("#sel").val();
    jQuery("#list").jqGrid({
        url: "BasMangeGrid",
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, sel: sel },
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
        colNames: ['序号', '参数名称', 'XID', 'Type', '操作', '操作'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 90 },
        { name: 'Text', index: 'Text', width: $("#bor").width() - 250 },
        { name: 'XID', index: 'XID', width: 200, hidden: true },
        { name: 'Type', index: 'Type', width: 200, hidden: true },
        { name: 'Pro', index: 'XID', width: 50 },
        { name: 'Prodel', index: 'XID', width: 50 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '基本信息配置表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var Type;
                var XID;
                var modify;
                var del;
                var Text;

                Type = jQuery("#list").jqGrid('getRowData', id).Type;
                XID = jQuery("#list").jqGrid('getRowData', id).XID;
                Text = jQuery("#list").jqGrid('getRowData', id).Text;
                modify = "<a  href='#' onclick='UpdateType(\"" + XID + "\",\"" + Type + "\",\"" + Text + "\")' style='color:blue'>修改</a>";
                del = "<a href='#' style='color:#f60' onclick='DeleteType(" + XID + ",\"" + Type + "\")' >删除</a>";

                jQuery("#list").jqGrid('setRowData', ids[i], { Pro: modify, Prodel: del });
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 200, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function UpdateType(xid, type, text) {
    window.parent.OpenDialog("修改内容", "../SystemManage/UpdateContent?id=" + xid + "@" + type + "@" + text, 400, 200);
}
function DeleteType(xid, type) {
    var one = confirm("确定删除这条数据吗？");
    if (one == false)
        return
    else {
        $.ajax({
            type: "POST",
            url: "DeleteContent",
            data: { data1: xid, data2: type },
            success: function (data) {
                alert(data.Msg);
                reload();
            },
            dataType: 'json'
        });
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