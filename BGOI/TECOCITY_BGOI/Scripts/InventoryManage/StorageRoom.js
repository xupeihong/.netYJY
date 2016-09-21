var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    $("#IsHouseIDoneto").val($("#IsHouseIDone").val());
    $("#IsHouseIDtwoto").val($("#IsHouseIDtwo").val());
    jq();
    changhouse("", "");
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
                var HouseID = Model.HouseID;
                $.ajax({
                    type: "POST",
                    url: "DeStorageRoom",
                    data: { HouseID: HouseID },
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
    //ProType = $('#ProType option:selected').text();
    //HouseID = $('#HouseID option:selected').text();

    ProType = $('#ProType').val();
    HouseID = $('#HouseID').val();

    IsHouseIDone = $('#IsHouseIDone').val();
    IsHouseIDtwo = $('#IsHouseIDtwo').val();
    if (HouseID == "请选择") {
        HouseID = "";
    }
    $("#list").jqGrid('setGridParam', {
        url: 'StorageRoomList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProType: ProType, HouseID: HouseID, IsHouseIDone: IsHouseIDone, IsHouseIDtwo: IsHouseIDtwo },

    }).trigger("reloadGrid");
}
function jq() {
    //ProType = $('#ProType option:selected').text();
    //HouseID = $('#HouseID option:selected').text();

    ProType = $('#ProType').val();
    HouseID = $('#HouseID').val();

    IsHouseIDone = $('#IsHouseIDone').val();
    IsHouseIDtwo = $('#IsHouseIDtwo').val();
    if (HouseID == "请选择") {
        HouseID = "";
    }
    jQuery("#list").jqGrid({
        url: 'StorageRoomList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProType: ProType, HouseID: HouseID, IsHouseIDone: IsHouseIDone, IsHouseIDtwo: IsHouseIDtwo },
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
        colNames: ['','序号', '仓库名称', '仓库地址', '仓库类型', '仓库级别', '所属单位', ''],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'HouseName', index: 'HouseName', width: 100, align: "center" },
        { name: 'Adress', index: 'Adress', width: 120, align: "center" },
        { name: 'Text', index: 'Text', width: 150, align: "center" },
        { name: 'IsHouseID', index: 'IsHouseID', width: 150, align: "center" },
        { name: 'DeptName', index: 'DeptName', width: 100, align: "center" },//, '仓库编号'
        { name: 'HouseID', index: 'HouseID', hidden: true, width: 100, align: "center" }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        multiselect: true,
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
function Search() {
    curRow = 0;
    curPage = 1;
   // ProType = $('#ProType option:selected').text();
    // HouseID = $('#HouseID option:selected').text();
    ProType = $("#ProType").val();
    HouseID = $("#HouseID").val();
    IsHouseIDone = $('#IsHouseIDone').val();
    IsHouseIDtwo = $('#IsHouseIDtwo').val();
    if (HouseID == "请选择") {
        HouseID = "";
    }
    $("#list").jqGrid('setGridParam', {
        url: 'StorageRoomList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, ProType: ProType, HouseID: HouseID, IsHouseIDone: IsHouseIDone, IsHouseIDtwo: IsHouseIDtwo
        },
        loadonce: false
    }).trigger("reloadGrid");//重新载入
}
function AddInventoryFirstPage() {
    window.parent.parent.OpenDialog("创建库房", "../InventoryManage/AddInventoryFirstPage", 800, 200);
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
        window.parent.parent.OpenDialog("修改仓库", "../InventoryManage/UpStorageRoom?HouseID=" + Model.HouseID, 800, 200);
    }
}

//根据部门加载库房
function changhouse(HouseID, ProType) {
    HouseID = $("#HouseID").val();
    ProType = $("#ProType").val();
    $('#IsHouseIDtwo').attr("disabled", false);
    //$('#two').attr("disabled", false);
    //$('#one').attr("disabled", false);
    $('#IsHouseIDone').attr("disabled", false);

    $("#IsHouseIDone").html("");
    $("#IsHouseIDtwo").html("");
    document.getElementById("IsHouseIDone").options.add(new Option("请选择", "0"));
    document.getElementById("IsHouseIDtwo").options.add(new Option("请选择", "0"));
    //var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    $.ajax({
        //url: "GetHouseIDoneNew",
        //type: "post",
        //data: { va: va },

        url: "GetHouseIDoneNewnew",
        type: "post",
        data: { HouseID: HouseID, ProType: ProType },
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
        //url: "GetHouseIDtwoNew",
        //type: "post",
        //data: { va: va },

        url: "GetHouseIDtwoNewnew",
        type: "post",
        data: { HouseID: HouseID, ProType: ProType },
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

function FX() {
    var str = "";
    var rowid = $("#list").jqGrid('getGridParam', 'selarrrow');
    for (var i = 0; i < rowid.length; i++) {
        var m = jQuery("#list").jqGrid('getRowData', rowid[i]).HouseName;
        str += "'" + m + "',";
    }
    $("#HouseNameN").val(str);
}


function chang1() {
    document.getElementById('IsHouseIDtwo').setAttribute('disabled', 'true');
    //document.getElementById('two').setAttribute('disabled', 'true');
    $("#IsHouseIDoneto").val($("#IsHouseIDone").val());
}
function chang2() {
    //document.getElementById('one').setAttribute('disabled', 'true');
    document.getElementById('IsHouseIDone').setAttribute('disabled', 'true');
    $("#IsHouseIDtwoto").val($("#IsHouseIDtwo").val());
}
