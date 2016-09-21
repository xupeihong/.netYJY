
var curPage = 1;
var OnePageCount = 10;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    $("#IsHouseIDoneto").val($("#IsHouseIDone").val());
    $("#IsHouseIDtwoto").val($("#IsHouseIDtwo").val());
    changhouse("", "");
    jq();
    //getruchuNum();//提醒功能
    //if ($("#UnitID").val() == "47")// 只有可以弹框报警 
    getLowCount();
    //var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".49.") > 0) {
        var oid = 1;
        changcollege(oid);
    }
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
            var Text = jQuery("#list").jqGrid('getRowData', rowid).Text;
            var HouseName = jQuery("#list").jqGrid('getRowData', rowid).HouseName;
            var url = "PrintInventoryFirstPage?Info=" + escape(texts) + "&Text=" + escape(Text) + "&HouseName=" + escape(HouseName);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    $('#Spec').click(function () {
        selid1('getSpecOptionalAdd','divGJ', 'ulGJ', 'LoadGJnew');//, 'BuildUnit'
    })
})
function getruchuNum() {
    $.ajax({
        type: "post",
        url: "GetNumTiXinRu",
        data: {},
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#alarm1").show();
                    var warnstring = "";
                    //入库
                    if (json[0].jb != "" && json[0].jb != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"Clickhu('../InventoryManage/BasicStockIn')\">基本未入库:'" + json[0].jb + "'</a></p>";
                    }
                    if (json[0].cg != "" && json[0].cg != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"Clickhu('../InventoryManage/ProcureStockIn')\">采购未入库:'" + json[0].cg + "'</a></p>";
                    }
                    if (json[0].th != "" && json[0].th != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"Clickhu('../InventoryManage/TestStockIn')\">退货检验单未入库:'" + json[0].th + "'</a></p>";
                    }
                    if (json[0].cj != "" && json[0].cj != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"Clickhu('../InventoryManage/ProtoStockIn')\">撤样机未调拨:'" + json[0].cj + "'</a></p>";
                    }
                    if (json[0].sc != "" && json[0].sc != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"Clickhu('../InventoryManage/ProductionStockIn')\">生产组装未入库:'" + json[0].sc + "'</a></p>";
                    }
                    //出库
                    if (json[0].jbc != "" && json[0].jbc != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"Clickhu('../InventoryManage/BasicStockOut')\">基本未出库:'" + json[0].jbc + "'</a></p>";
                    }
                    if (json[0].ls != "" && json[0].ls != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"Clickhu('../InventoryManage/RetailSalesOut')\">销售订单未出库:'" + json[0].ls + "'</a></p>";
                    }

                    if (json[0].xm != "" && json[0].xm != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"Clickhu('../InventoryManage/ProjectSalesOut')\">项目销售未出库:'" + json[0].xm + "'</a></p>";
                    }

                    if (json[0].rj != "" && json[0].rj != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"Clickhu('../InventoryManage/SecondOut')\">二级库未出库:'" + json[0].rj + "'</a></p>";
                    }

                    if (json[0].sx != "" && json[0].sx != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"Clickhu('../InventoryManage/ProtoUpOut')\">上样机未出库:'" + json[0].sx + "'</a></p>";
                    }

                    if (json[0].ng != "" && json[0].ng != 0) {
                        warnstring += "<p><a style='cursor:pointer;color:red;' onclick=\"Clickhu('../InventoryManage/BuyGiveOut')\">内购/赠送出库:'" + json[0].ng + "'</a></p>";
                    }
                    $("#content1").html(warnstring);
                }
            }
        }
    })

}
function Clickhu(Url) {
    window.parent.TurnTo(Url);
}
function closeAlarm() {
    document.getElementById("alarm1").style.display = "none";
}
function reload() {
    ProType = $('#ProType option:selected').text();
    PID = $('#PIDCX').val();
    ProName = $('#ProName').val();
    Spec = $("#Spec").val();
    UnitID = $('#HouseID').val();
    if (ProType == "请选择") {
        ProType = "";
    }
    if (Spec == "请选择") {
        Spec = "";
    }
    if (UnitID == "请选择") {
        UnitID = "";
    }
    $("#list").jqGrid('setGridParam', {
        url: 'StockRemainList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProType: ProType, PID: PID, ProName: ProName, Spec: Spec, UnitID: UnitID },
    }).trigger("reloadGrid");
}
function jq() {
    // var UnitID = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".49.") > 0) {
        ProType = "零件库";
    } else {
        ProType = $('#ProType option:selected').text();
    }
    PID = $('#PIDCX').val();
    ProName = $('#ProName').val();
    Spec = $("#Spec").val();
    UnitID = $('#HouseID').val();
    IsHouseIDone = $('#IsHouseIDone').val();
    IsHouseIDtwo = $('#IsHouseIDtwo').val();
    if (ProType == "请选择") {
        ProType = "";
    }
    if (Spec == "请选择") {
        Spec = "";
    }
    if (UnitID == "请选择") {
        UnitID = "";
    }
    var str = "";
    jQuery("#list").jqGrid({
        url: 'StockRemainList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProType: ProType, PID: PID, ProName: ProName, Spec: Spec, UnitID: UnitID, IsHouseIDone: IsHouseIDone, IsHouseIDtwo: IsHouseIDtwo },
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
        colNames: [' ','序号', '货品库类型', '货品编号', '货品名称', '规格型号', '库存数量', '在线生产数量',  '存放位置', '所属仓库'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'Text', index: 'Text', width: 100, align: "center" },
        { name: 'PID', index: 'PID', width: 120, align: "center" },
        { name: 'ProName', index: 'ProName', width: 150, align: "center" },
        { name: 'Spec', index: 'Spec', width: 150, align: "center" },
        { name: 'FinishCount', index: 'FinishCount', width: 80, align: "center" },
        { name: 'OnlineCount', index: 'OnlineCount', width: 80, align: "center" },
        //{ name: 'UsableStock', index: 'UsableStock', width: 100, align: "center" }, '可用库存',
        //{ name: 'Costing', index: 'Costing', width: 100, align: "center" }, '成本',
        { name: 'Location', index: 'Location', width: 80, align: "center" },
        { name: 'HouseName', index: 'HouseName', width: 80, align: "center" }
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
        multiselect: true,

        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {
                return;
            }
            else {
                var PID = Model.PID;
                $("#PIDnew").val(PID);
                //reload1(PID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 220, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function Search() {
    curRow = 0;
    curPage = 1;
    // var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".49.") > 0) {
        ProType = "零件库";
    } else {
        ProType = $('#ProType option:selected').text();
    }
    var PID = $("#PIDCX").val();
    var ProName = $("#ProName").val();
    var Spec = $("#Spec").val();
    var UnitID = $("#HouseID").val();
   var IsHouseIDone = $('#IsHouseIDone').val();
   var  IsHouseIDtwo = $('#IsHouseIDtwo').val();
    if (ProType == "请选择") {
        ProType = "";
    }
    if (Spec == "请选择") {
        Spec = "";
    }
    if (UnitID == "请选择") {
        UnitID = "";
    }
    $("#list").jqGrid('setGridParam', {
        url: 'StockRemainList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, ProType: ProType, PID: PID, ProName: ProName, Spec: Spec, UnitID: UnitID, IsHouseIDone: IsHouseIDone, IsHouseIDtwo: IsHouseIDtwo
        },
        loadonce: false
    }).trigger("reloadGrid");//重新载入
}
function AddInventoryFirstPage() {
    window.parent.parent.OpenDialog("新增仓库", "../InventoryManage/AddInventoryFirstPage", 800, 200);
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
      if (va == "") {
        va = $("#ProType").val();
    }
    $('#IsHouseIDtwo').attr("disabled", false);
    $('#two').attr("disabled", false);
    $('#one').attr("disabled", false);
    $('#IsHouseIDone').attr("disabled", false);
    $("#IsHouseIDone").html("");
    $("#IsHouseIDtwo").html("");
    document.getElementById("IsHouseIDone").options.add(new Option("请选择", "0"));
    document.getElementById("IsHouseIDtwo").options.add(new Option("请选择", "0"));
    //var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    if (UnitIDnew.indexOf(".49.") > 0) {
        //$.ajax({
        //    url: "GetHouseIDoneNew",
        //    type: "post",
        //    data: { va: va },
        //    dataType: "json",
        //    success: function (data) {
        //        var json = eval(data.datas);
        //        if (json.length > 0) {
        //            for (var i = 0; i < json.length; i++) {
        //                var IsHouseIDone = document.getElementById("IsHouseIDone");
        //                IsHouseIDone.add(new Option(json[i].HouseName, json[i].HouseID));
        //            }
        //        }
        //    }
        //})
        //$.ajax({
        //    url: "GetHouseIDtwoNew",
        //    type: "post",
        //    data: { va: va },
        //    dataType: "json",
        //    success: function (data) {
        //        var json = eval(data.datas);
        //        if (json.length > 0) {
        //            for (var i = 0; i < json.length; i++) {
        //                var IsHouseIDtwo = document.getElementById("IsHouseIDtwo");
        //                IsHouseIDtwo.add(new Option(json[i].HouseName, json[i].HouseID));
        //            }
        //        }
        //    }
        //})
    } else {
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
}
function chang1() {
    document.getElementById('IsHouseIDtwo').setAttribute('disabled', 'true');
    document.getElementById('two').setAttribute('disabled', 'true');
    $("#IsHouseIDoneto").val($("#IsHouseIDone").val());
}
function chang2() {
    document.getElementById('one').setAttribute('disabled', 'true');
    document.getElementById('IsHouseIDone').setAttribute('disabled', 'true');
    $("#IsHouseIDtwoto").val($("#IsHouseIDtwo").val());
}
// 获取
function getLowCount() {
    var curPage = 1;
    var OnePageCount = 10;
    $.ajax({
        url: "../InventoryManage/MaterialBasicNum",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, level: "" },
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data != "") {
                var json = eval(data.datas);
                //alert(json);
                //alert(data);
                if (json != "") {
                    window.parent.parent.OpenDialog('库存报警', '../InventoryManage/LowWarn', 400, 170);
                }
            }
        }
    })
}
//根据部门加载库房
function changhouse(HouseID, ProType) {
   // if (HouseID == "") {
        HouseID = $("#HouseID").val();
   // } 
   // if (ProType == "") {
        ProType = $("#ProType").val();
    //}
        $('#IsHouseIDtwo').attr("disabled", false);
        $('#two').attr("disabled", false);
        $('#one').attr("disabled", false);
        $('#IsHouseIDone').attr("disabled", false);
    $("#IsHouseIDone").html("");
    $("#IsHouseIDtwo").html("");
    document.getElementById("IsHouseIDone").options.add(new Option("请选择", "0"));
    document.getElementById("IsHouseIDtwo").options.add(new Option("请选择", "0"));
    //var UnitIDnew = $("#UnitIDnew").val();
    var UnitIDnew = $("#UnitIDo").val();
    $.ajax({
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
function returnConfirm() {

    $("#PID").val($("#PIDnew").val());
    // var str = "";
    // var r = document.getElementsByName("RIDCheck");
    //for (var i = 0; i < r.length; i++) {
    //    if (r[i].checked) {
    //        str += r[i].value + ",";
    //    }
    //}
    //$("#ListInID").val(str);
    //if (str == "") {

    //    return false;
    //} else {
    //    return true;
    //}
}
function LoadGJnew(liInfo) {
    // document.getElementById("Spec").value = liInfo;
 $("#Spec").val(liInfo);
    $("#divGJ").hide();
}
//function BuildUnitkey() {
//    $("#divGJ").hide();
//}
function selid1(actionid,divid, ulid, jsfun) {
    // var TypeID = Type;// 行政区编码
    var spec = $("#Spec").val();
    $.ajax({
        url: actionid,
        type: "post",
        data: { spec: spec },//data1: TypeID,
        dataType: "json",
        async: false,
        success: function (data) {
            if (data.success == "true") {
                //var unitid = data.Strid.split(',');
                var unit = data.Strname.split(',');
                $("#" + divid).show();
                $("#" + ulid + " li").remove();
                for (var i = 0; i < unit.length; i++) {
                    $("#" + ulid).append("<li style='cursor:pointer;margin-left:1px; width:190px;height:20px;list-style-type:none; color:black;'><span onclick='" + jsfun + "(\"" + unit[i] + "\");' style='margin-left:1px; width:190px; height:20px;display:block;'>" + unit[i] + "</span>");
                }
            }
            else {
                $("#" + divid).hide();
            }
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
function FX() {
    var str="";
    var rowid = $("#list").jqGrid('getGridParam', 'selarrrow');
    for (var i = 0; i < rowid.length; i++) {
        var m = jQuery("#list").jqGrid('getRowData', rowid[i]).PID;
        str += "'" + m + "',";
    }
    $("#PIDN").val(str);
}