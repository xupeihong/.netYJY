var curPage = 1;
var OnePageCount = 15;

$(document).ready(function () {
    $("#pageContent").height($(window).height());
    jq();

})

function jq() {
    jQuery("#list").jqGrid({
        url: 'LoadInspecDetail',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount,
            OutUnit: $("#OutUnit").val(), BathID: $("#BathID").val()
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
        colNames: ['序号','仪表名称', '生产厂家', '规格型号', '编号', '管径（DN）', '准确度',
            '数量（台）', '使用地点', '送检批次'],
        colModel: [
        { name: 'ID', index: 'ID', width: 50 },
        { name: 'MeterName', index: 'MeterName', width: 120 },
        { name: 'Manufacturer', index: 'Manufacturer', width: 150 },
        { name: 'Model', index: 'Model', width: 110 },
        { name: 'MeterID', index: 'MeterID', width: 120 },
        { name: 'Caliber', index: 'Caliber', width: 100 },
        { name: 'Accuracy', index: 'Accuracy', width: 120 },
        { name: 'Mcount', index: 'Mcount', width: 100 },
        { name: 'CustomerAddr', index: 'CustomerAddr', width: 200 },
        { name: 'BathID', index: 'BathID', width: 100 }

        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',

        gridComplete: function () {

        },
        onSelectRow: function (rowid, status) {

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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 20, false);
        }
    });
}

function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'LoadInspecDetail',
        datatype: 'json',
        loadonce: false,
        postData: {
            curpage: curPage, rownum: OnePageCount,
            OutUnit: $("#OutUnit").val(), BathID: $("#BathID").val()
        },

    }).trigger("reloadGrid");
}

function ToAlter() {
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
