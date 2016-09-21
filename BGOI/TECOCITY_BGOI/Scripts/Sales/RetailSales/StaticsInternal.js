var SalesProduct = "";
var SpecsModels = "";
var StartDate = "";
var EndDate = "";
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    LoadBasInfo();

    $("#btnSearch").click(function () {
        reload();
    });

    $("#btnPrint").click(function () {
        SalesProduct = $("#SalesProduct").val();
        SpecsModels = $("#SpecsModels").val();
        StartDate = $("#StartDate").val();
        EndDate = $("#EndDate").val();
        window.showModalDialog("../SalesRetail/PrintInterDetail?SalesProduct=" + SalesProduct + "&SpecsModels=" + SpecsModels + "&StartDate=" + StartDate + "&EndDate=" + EndDate, window, "dialogWidth:950px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });

    $("#btnHK").click(function () {
        if (confirm("是否确认已回款？")) {
            var rowId = $("#list").jqGrid("getGridParam", "selrow");
            if (rowId == null) {
                alert("请选择修改的产品记录...");
                return;
            }
            else {
                var IOID = jQuery("#list").jqGrid("getRowData", rowId).IOID;
                var DID = jQuery("#list").jqGrid("getRowData", rowId).DID;
                $.ajax({
                    url: "AlterInternalHK",
                    type: "post",
                    data: { IOID: IOID, DID: DID },
                    dataType: "Json",
                    success: function (data) {
                        if (data.success == true) {
                            reload();
                        }
                        else {
                            alert(data.Msg);
                            return;
                        }
                    }
                });
            }
        }
    });
});

function reload() {
    if ($('.field-validation-error').length == 0) {
        SalesProduct = $("#SalesProduct").val();
        SpecsModels = $("#SpecsModels").val();
        StartDate = $("#StartDate").val();
        EndDate = $("#EndDate").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetInternalDetailGrid',
            datatype: 'json',
            postData: { SalesProduct: SalesProduct, SpecsModels: SpecsModels, StartDate: StartDate, EndDate: EndDate },

        }).trigger("reloadGrid");
    }
}

function LoadBasInfo() {
    SalesProduct = $("#SalesProduct").val();
    SpecsModels = $("#SpecsModels").val();
    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();
    jQuery("#list").jqGrid({
        url: 'GetInternalDetailGrid',
        datatype: 'json',
        postData: { SalesProduct: SalesProduct, SpecsModels: SpecsModels, StartDate: StartDate, EndDate: EndDate },
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
        colNames: ['序号', '', '', '产品名称', '产品类型', '规格型号', '数量', '零售价', '折扣', '总价', '付款方式', '是否回款', '备注'],
        colModel: [
        { name: 'ID', index: 'ID', align: 'center', width: 30 },
        { name: 'IOID', index: 'IOID', width: 90, hidden: true },
        { name: 'DID', index: 'DID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 150 },
        { name: 'GoodsType', index: 'GoodsType', width: 120 },
        { name: 'Specifications', index: 'Specifications', width: 120 },
        { name: 'Amount', index: 'Amount', width: 80 },
        { name: 'UnitPrice', index: 'UnitPrice', width: 80 },
        { name: 'Discounts', index: 'Discounts', width: 80 },
        { name: 'Total', index: 'Total', width: 80 },
        { name: 'PayWay', index: 'PayWay', width: 120 },
        { name: 'IsHK', index: 'IsHK', width: 120 },
        { name: 'Remark', index: 'Remark', width: 250 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        //rowNum: 50,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var ID = "" + id + "";
                $("#list").jqGrid('setRowData', ids[i], { ID: ID });
                if ($("#list").jqGrid('getRowData', ids[i]).PayWay == "安装后付款") {
                    PayWay = "<label style='color:Red;'>安装后付款</label>";
                    $("#list").jqGrid('setRowData', ids[i], { PayWay: PayWay });

                    var IsHK = "";
                    if ($("#list").jqGrid('getRowData', ids[i]).IsHK == "") {
                        IsHK = "<label style=''>未回款</label>";
                        $("#list").jqGrid('setRowData', ids[i], { IsHK: IsHK });
                    }
                    else if ($("#list").jqGrid('getRowData', ids[i]).IsHK == "y") {
                        IsHK = "<label style=''>已回款</label>";
                        $("#list").jqGrid('setRowData', ids[i], { IsHK: IsHK });
                    }
                }
                else {
                    IsHK = "<label style=''>不涉及</label>";
                    $("#list").jqGrid('setRowData', ids[i], { IsHK: IsHK });
                }
            }
        },
        onSelectRow: function (rowid, status) {

        },

        onPaging: function (pgButton) {
            //if (pgButton == "next_pager") {
            //    if (curPage == $("#list").getGridParam("lastpage"))
            //        return;
            //    curPage = $("#list").getGridParam("page") + 1;
            //}
            //else if (pgButton == "last_pager") {
            //    curPage = $("#list").getGridParam("lastpage");
            //}
            //else if (pgButton == "prev_pager") {
            //    if (curPage == 1)
            //        return;
            //    curPage = $("#list").getGridParam("page") - 1;
            //}
            //else if (pgButton == "first_pager") {
            //    curPage = 1;
            //}
            //else {
            //    curPage = $("#pager :input").val();
            //}
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 180, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
