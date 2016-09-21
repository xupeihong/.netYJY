
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
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
            var url = "PrintInventoryFirstPage?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });
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
                var ID = Model.ID;
                $.ajax({
                    type: "POST",
                    url: "DeProductTypeSetting",
                    data: { ID: ID },
                    success: function (data) {
                        alert(data.Msg);
                        reload();
                    },
                    dataType: 'json'
                });
                return true;
            } else {
                return false;
            }
        }
    });
})
function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'ProductTypeSettingList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount},
    }).trigger("reloadGrid");
}
function jq() {
    jQuery("#list").jqGrid({
        url: 'ProductTypeSettingList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount},
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
        //ID, OID, Text
        colNames: ['序号', '类型编号', '类型名称', ],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 100, align: "center" },
        { name: 'ID', index: 'ID', width: 150, align: "center" },
        { name: 'Text', index: 'Text', width: 200, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 220, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function AddInventoryFirstPage() {
    window.parent.parent.OpenDialog("新增产品类型", "../InventoryManage/AddProductTypeSetting", 800, 200);
}
function LianDun() {
    ProType = $('#ProType').val();
    $.ajax({
        url: "InventoryFirstPagetwo",
        type: "post",
        data: { ProType: ProType },
        dataType: "json",
        async: false,
        success: function (data) {
            $.each(data, function (i, item) {
                $("<option></option>").val(item["Value"]).text(item["Text"]).appendTo($("#IsHouseIDone"));
            });
        }
    })
}
function changcollege(va) {
    $.ajax({
        url: "GetHouseIDoneNew",
        type: "post",
        data: { va: va },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                document.getElementById("IsHouseIDone").options.length = 1;
                for (var i = 0; i < json.length; i++) {
                    var IsHouseIDone = document.getElementById("IsHouseIDone");
                    IsHouseIDone.add(new Option(json[i].HouseName, json[i].HouseID));
                }
            }
        }
    })
    $.ajax({
        url: "GetHouseIDtwoNew",
        type: "post",
        data: { va: va },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                document.getElementById("IsHouseIDtwo").options.length = 1;
                for (var i = 0; i < json.length; i++) {
                    var IsHouseIDtwo = document.getElementById("IsHouseIDtwo");
                    IsHouseIDtwo.add(new Option(json[i].HouseName, json[i].HouseID));
                }
            }
        }
    })
}
//修改
function ScrapManagementUP() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        window.parent.parent.OpenDialog("修改产品类型", "../InventoryManage/UpProductTypeSetting?ID=" + Model.ID, 800, 200);
    }
}

//根据部门加载库房
function changhouse(va) {
    $("#IsHouseIDone").html("");
    $("#IsHouseIDtwo").html("");
    document.getElementById("IsHouseIDone").options.add(new Option("请选择", "0"));
    document.getElementById("IsHouseIDtwo").options.add(new Option("请选择", "0"));
    //var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    $.ajax({
        url: "GetHouseIDoneNew",
        type: "post",
        data: { va: va },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var IsHouseIDone = document.getElementById("IsHouseIDone");
                    IsHouseIDone.add(new Option(json[i].HouseName, json[i].HouseID));
                }
            }
        }
    })
    $.ajax({
        url: "GetHouseIDtwoNew",
        type: "post",
        data: { va: va },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var IsHouseIDtwo = document.getElementById("IsHouseIDtwo");
                    IsHouseIDtwo.add(new Option(json[i].HouseName, json[i].HouseID));
                }
            }
        }
    })
}





