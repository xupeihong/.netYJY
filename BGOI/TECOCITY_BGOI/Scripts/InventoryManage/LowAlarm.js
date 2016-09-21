var level;
var curPage = 1;
var OnePageCount = 10;
var strFirsTypeText = "";
var strCount = "";
$(document).ready(function () {
    if (window.location.search.indexOf("?") != -1) {
        level = window.location.search.split('&')[0].split('=')[1];
    }
    else {
        level = "";
    }
    var unitid = $("#UnitIDnew").val();
    if (unitid == "47") {
        jqnew();
    } else {
        jq();
    }
    
    //处理
    $("#Chuli").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要检查的行");
            return;
        }
        else {
            strFirsTypeText = Model.FirsTypeText;
            strCount = Model.Count;
            var xunhuan = setInterval('getAlarm()', 1000);
        }
    });
    
    $("#ZT").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要检查的行");
            return;
        }
        else {
            PID = Model.PID;
            $.ajax({
                url: "LowAlarmZT",
                type: "post",
                data: {
                    PID: PID
                },
                dataType: "json",
                async: false, //是否异步
                success: function (data) {
                    if (data.success == true) {
                        alert("记录成功");
                        setTimeout('parent.ClosePop()', 10);
                        window.parent.frames["iframeRight"].reload();
                    }
                    else {
                        alert(data.msg);
                    }
                }
            });

        }
    })
    $("#ZSC").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要检查的行");
            return;
        }
        else {
            PID = Model.PID;
            $.ajax({
                url: "LowAlarmZSC",
                type: "post",
                data: {
                    PID: PID
                },
                dataType: "json",
                async: false, //是否异步
                success: function (data) {
                    if (data.success == true) {
                        alert("记录成功");
                        setTimeout('parent.ClosePop()', 10);
                        window.parent.frames["iframeRight"].reload();
                    }
                    else {
                        alert(data.msg);
                    }
                }
            });

        }
    })
    $("#btnSave").click(function () {
        var PID = $("#PID").val();
        var Num = $("#Num").val();
        if (PID == "" || PID == null) {
            alert("物料编号为空");
            return;
        }
        if (Num == "" || Num == null) {
            alert("上限数量不能为空");
            return;
        }
        $.ajax({
            url: "SaveLowAlarm",
            type: "Post",
            data: {
                PID: PID, Num: Num
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("保存成功");
                }
                else {
                    alert(data.msg);
                }
            }
        });
    });
})
function getAlarm() {
    $.ajax({
        url: "getAlarm",
        type: "post",
        data: {
            strFirsTypeText: strFirsTypeText, strCount: strCount
        },
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "true") {
                var arrData = data.Alarm.split(',');
                if (parseInt(strCount) < parseInt(arrData[0])) {
                    $("#noticecon").html(arrData[1] + arrData[0]);
                    $("div[id=noticecon]", "div[id=divMessage]").show();
                }
                else {
                    $("div[id=noticecon]", "div[id=divMessage]").hide();
                }
            }
            else {
                return;
            }
        }
    });
}
function showDiv() {
    $("#divMusic").attr("innerHTML", "<embed src='images/mail.wav' hidden='true' autostart='true'>");
    $("div[id=divMessage]").css({ "right": "0px", "bottom": "1px" });
    $("div[id=divMessage]").slideDown("slow");
    $(window).scroll(function () {
        if (isIE6) {
            $("div[id=divMessage]").css({ "bottom": "0px" });
            $("div[id=divMessage]").css({ "right": "0px", "bottom": "1px" });
        }
        else {
            $("div[id=divMessage]").css({ "right": "1px" });
            $("div[id=divMessage]").css("bottom", "-" + document.documentElement.scrollTop + "px");
        }
    }).resize(function () {
        if (isIE6) {
            $("div[id=divMessage]").css({ "bottom": "0px" });
            $("div[id=divMessage]").css({ "right": "0px", "bottom": "1px" });
        }
        else {
            $("div[id=divMessage]").css({ "right": "1px" });
            $("div[id=divMessage]").css("bottom", "-" + document.documentElement.scrollTop + "px");
        }
    });
}
function addCellAttr(rowId, val, rawObject, cm, rdata) {
    if (1 == 1) {
        return "style='color:red; font-weight:bold;'";
    }
}
function jq() {
    ProName = $("#ProName").val();
    jQuery("#list").jqGrid({
        url: 'MaterialBasicData',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, level: level, ProName: ProName
        },
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
        colNames: ['物料编码', '物料描述', '库房名称', '库存数量', '规格型号', '仓库类型', '下限数量', '状态'],

        colModel: [
            { name: 'PID', index: 'PID', width: 150 },
            { name: 'ProName', index: 'ProName', width: 300 },
            { name: 'HouseName', index: 'HouseName', width: 100 },
            { name: 'Count', index: 'Count', width: 100, cellattr: addCellAttr },
            { name: 'Spec', index: 'Spec', width: 100, cellattr: addCellAttr },
            { name: 'Text', index: 'Text', width: 100, cellattr: addCellAttr },
            { name: 'Num', index: 'Num', width: 100, cellattr: addCellAttr },
            { name: 'Remarks', index: 'Remarks', width: 50 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '库存数量报警列表',

        gridComplete: function () {

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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 50, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }

    });

}
function jqnew() {
    ProName = $("#ProName").val();
    jQuery("#list").jqGrid({
        url: 'MaterialBasicData',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, level: level, ProName: ProName
        },
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
        colNames: ['物料编码', '物料描述', '库房名称', '库存数量', '规格型号', '仓库类型', '下限数量'],

        colModel: [
            { name: 'PID', index: 'PID', width: 150 },
            { name: 'ProName', index: 'ProName', width: 300 },
            { name: 'HouseName', index: 'HouseName', width: 100 },
            { name: 'Count', index: 'Count', width: 100, cellattr: addCellAttr },
            { name: 'Spec', index: 'Spec', width: 100, cellattr: addCellAttr },
            { name: 'Text', index: 'Text', width: 100, cellattr: addCellAttr },
            { name: 'Num', index: 'Num', width: 100, cellattr: addCellAttr }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '库存数量报警列表',

        gridComplete: function () {

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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 50, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }

    });

}
function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'MaterialBasicData',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, level: level
        },
        loadonce: false

    }).trigger("reloadGrid");

}
function GetNum(pid) {
    $.ajax({
        url: "UpLowAlarm",
        type: "Post",
        data: {
            pid: pid
        },
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#Num").val(json[i].Num);
                }
            }
        }
    });
}
function SheZi() {
    window.parent.parent.OpenDialog("设置下限", "../InventoryManage/Setlowerlimit", 300, 300);
}

function SearchOut() {
    curRow = 0;
    curPage = 1;
    var ProName = $("#ProName").val();
    $("#list").jqGrid('setGridParam', {
        url: 'MaterialBasicData',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, level: level, ProName: ProName },
        loadonce: false
    }).trigger("reloadGrid");//重新载入

}