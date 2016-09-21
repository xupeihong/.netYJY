var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
var CID;
var curPage1 = 1;
var OnePageCount1 = 20;


$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    jq1("");



    $("#DY").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要打印的请购单");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).CID;
        var url = "PrintQG?id=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });
    $("#XX").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var CID = Model.CID;
            window.parent.OpenDialog("请购详情", "../PPManage/DetailsQG?CIDXQ=" + CID + "", 850, 550);
        }
    });
    $("#XG").click(function () {

            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {
                alert("请选择要操作的行");
                return;
            }
            else {
                var CID = Model.CID;
                window.parent.parent.OpenDialog("修改", "../PPManage/UpdateQGXX?CID=" + CID + "", 1155, 550);

            }
        

    })

    $("#CX").click(function () {

        isConfirm = confirm("确定要删除吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {
                alert("请选择要撤销的请购单");
            }
            var Text = Model.Text;
            var CID = Model.CID;

            if (Text == "新建") {
                $.ajax({
                    url: "UpdateQGValidate",
                    type: "Post",
                    data: {
                        CID: CID
                    },
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            window.parent.frames["iframeRight"].reload();
                            alert("成功");
                        }
                        else {
                            alert("失败");
                        }
                    }
                });
            }
            if (Text == "已订购") {

                $.ajax({
                    url: "SelectDDCID",
                    type: "Post",
                    data: {
                        CID: CID
                    },
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            alert("该商品已订购，须先删除订购单");
                        }
                        else {
                            $.ajax({
                                url: "UpdateQGValidate",
                                type: "Post",
                                data: {
                                    CID: CID
                                },
                                async: false,
                                success: function (data) {
                                    if (data.success == true) {
                                        window.parent.frames["iframeRight"].reload();
                                        alert("成功");
                                    }
                                    else {
                                        alert("失败");
                                    }
                                }
                            });
                        }
                    }
                });
            }
            if (Text == "已询价") {
                $.ajax({
                    url: "SelectXJCID",
                    type: "Post",
                    data: {
                        CID: CID
                    },
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            alert("该商品已询价，须先删除询价单")
                        }
                        else {
                            $.ajax({
                                url: "UpdateQGValidate",
                                type: "Post",
                                data: {
                                    CID: CID
                                },
                                async: false,
                                success: function (data) {
                                    if (data.success == true) {
                                        window.parent.frames["iframeRight"].reload();
                                        alert("成功");
                                    }
                                    else {
                                        alert("失败");
                                    }
                                }
                            });
                        }
                    }
                });
            }
        }


    });
    $("#SP").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {

            var CID = Model.CID;
            $.ajax({
                type: "POST",
                url: "UpdatePurchaseGoods",
                data: { CID: CID },
                success: function (data) {
                    reload();
                },
                dataType: 'json'
            });
            alert(CID);
            window.parent.parent.OpenDialog("审批", "../PPManage/Approval?CID=" + CID + "", 800, 450);

        }
    });
    $("#rzxq").click(function () {
        RZ();
        this.className = "btnTw";
        $('#DJ').attr("class", "btnTh");
        $('#WP').attr("class", "btnTh");
        $("#bor1").css("display", "none");
        $("#danju").css("display", "none");
        $("#bor2").css("display", "");
    });
    $("#WP").click(function () {

        this.className = "btnTw";
        $('#DJ').attr("class", "btnTh");
        $('#rzxq').attr("class", "btnTh");
        $("#bor1").css("display", "");
        $("#danju").css("display", "none");
        $("#bor2").css("display", "none");

    });
    $("#DJ").click(function () {
        this.className = "btnTw";
        $('#WP').attr("class", "btnTh");
        $('#rzxq').attr("class", "btnTh");

        $("#bor1").css("display", "none");
        $("#danju").css("display", "");
        $("#bor2").css("display", "none");





    });
})

function xiangqing() {
    var xjid = document.getElementById("RowNumber").innerHTML;
    window.parent.OpenDialog("详情", "../PPManage/DetailsXJ?XJIDXQ=" + xjid + "", 1050, 650);
}
function ddxiangqing() {
    var ddid = document.getElementById("DDID").innerHTML;
    window.parent.OpenDialog("详情", "../PPManage/DetailsDD?DDIDXQ=" + ddid + "", 1050, 650);
}

function AddInquiry() {


    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    }
    else {
        var CID = Model.CID;
        $.ajax({
            url: "SelectXJCID",
            type: "Post",
            data: {
                CID: CID
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("该商品已询价")
                }
                else {

                    window.parent.parent.OpenDialog("询价", "../PPManage/Inquiry?CID=" + CID + "", 1160, 655);
                }
            }
        });
    }
}

function AddOrder() {

    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    }
    else {
        var CID = Model.CID;
        $.ajax({
            url: "SelectDDCID",
            type: "Post",
            data: {
                CID: CID
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("该商品已订购");
                }
                else {
                    window.parent.parent.OpenDialog("订购", "../PPManage/ErOrder?CID=" + CID + "", 1160, 655);
                }
            }
        });
    }
}

function XGDJ(CID) {
    $.ajax({
        url: "SelectXJCID",
        type: "post",
        data: { CID: CID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json != null) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber">' + json[i].XJID + '</lable> </td>';
                    html += '<td > <input class="btn" type="button" value="详情" onclick="xiangqing()" /></td>';
                    html += '</tr>'
                    $("#GXInfo").html("");
                    $("#GXInfo").append(html);
                }
            }
        }
    });
    $.ajax({
        url: "SelectDDCID",
        type: "post",
        data: { CID: CID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json != null) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;

                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="DDID">' + json[i].DDID + '</lable> </td>';
                    html += '<td > <input class="btn" type="button" value="详情" onclick="ddxiangqing()" /></td>';
                    html += '</tr>'
                    $("#GXInfo").html("");
                    $("#GXInfo").append(html);
                }
            }
        }
    });
}
function jq() {
    var CID = $('#CID').val();
    var State = $('#State').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var Begin1 = $('#Begin1').val();
    var End1 = $('#End1').val();


    jQuery("#list").jqGrid({
        url: 'SelectQG',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, CID: CID, State: State, Begin: Begin, End: End, Begin1: Begin1, End1: End1 },
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
        colNames: ['编号', '请购人', '请购日期', '期望到货日期','单位', '请购说明', '请购状态'],
        colModel: [
        { name: 'CID', index: 'CID', width: 150 },
        { name: 'UserName', index: 'UserName', width: 50 },
        { name: 'PleaseDate', index: 'PleaseDate', width: 180 },
        { name: 'DeliveryDate', index: 'DeliveryDate', width: 180 },
        { name: 'DeptName', index: 'DeptName', width: 180 },
        { name: 'PleaseExplain', index: 'PleaseExplain', width: 180 },
        { name: 'Text', index: 'Text', width: 60 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
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
                var CID = Model.CID;
                reload1(CID);
                XGDJ(CID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    if ($('.field-validation-error').length == 0) {
        var CID = $('#CID').val();
        var OrderContent = $('#OrderContent').val();
        var State = $('#State').val();

        var Begin = $('#Begin').val();
        var End = $('#End').val();
        var Begin1 = $('#Begin1').val();
        var End1 = $('#End1').val();
        $("#list").jqGrid('setGridParam', {
            url: 'SelectQG',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, CID: CID, OrderContent: OrderContent, State: State, Begin: Begin, End: End, Begin1: Begin1, End1: End1 },

        }).trigger("reloadGrid");
    }
}

//查询
function SearchOut() {

    var strDateStart = $('#Begin').val();
    var strDateEnd = $('#End').val();
    var strDateStart1 = $('#Begin1').val();
    var strDateEnd1 = $('#End1').val();
    //if (strDateStart == "" && strDateEnd == "" && strDateStart1 == "" && strDateEnd1 == "") {

    //    getSearch();
    //}
    //else {
    var strSeparator = "-"; //日期分隔符
    var strDateArrayStart;
    var strDateArrayEnd;
    var intDay;
    strDateArrayStart = strDateStart.split(strSeparator);
    strDateArrayEnd = strDateEnd.split(strSeparator);
    var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
    var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);

    strDateArrayStart1 = strDateStart1.split(strSeparator);
    strDateArrayEnd1 = strDateEnd1.split(strSeparator);
    var strDateS1 = new Date(strDateArrayStart1[0] + "/" + strDateArrayStart1[1] + "/" + strDateArrayStart1[2]);
    var strDateE1 = new Date(strDateArrayEnd1[0] + "/" + strDateArrayEnd1[1] + "/" + strDateArrayEnd1[2]);
    if (strDateS <= strDateE) {
        if (strDateS1 <= strDateE1) {
            getSearch();
        }
        else if (strDateStart1 == "" && strDateEnd1 == "") {
            getSearch();
        }
        else {
            alert("截止日期不能小于开始日期！");
            $("#End1").val("");
        }

    }
    else if (strDateStart == "" && strDateEnd == "") {
        if (strDateS1 <= strDateE1) {
            getSearch();
        }
        else if (strDateStart1 == "" && strDateEnd1 == "") {
            getSearch();
        }
        else {
            alert("截止日期不能小于开始日期！");
            $("#End1").val("");
        }
    }
    else {
        alert("截止日期不能小于开始日期！");
        $("#End").val("");
        return false;
    }
}
//}

function getSearch() {
    curRow = 0;
    curPage = 1;

    var CID = $('#CID').val();
    var OrderContent = $('#OrderContent').val();
    var State = $('#State').val();

    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var Begin1 = $('#Begin1').val();
    var End1 = $('#End1').val();

    $("#list").jqGrid('setGridParam', {
        url: 'SelectQG',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, CID: CID, OrderContent: OrderContent, State: State, Begin: Begin, End: End, Begin1: Begin1, End1: End1 },
        loadonce: false

    }).trigger("reloadGrid");//重新载入

}

function jq1(CID) {
    jQuery("#list1").jqGrid({
        url: 'PurchaseGoodsList',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, CID: CID },
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
        colNames: ['编号', '物品名称', '规格型号', '单位', '数量', '预计单价', '预计金额',  '备注'],
        colModel: [
        { name: 'CID', index: 'CID', width: 180 },
        { name: 'OrderContent', index: 'OrderContent', width: 150 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Unit', index: 'Unit', width: 60 },
        { name: 'Amount', index: 'Amount', width: 60 },
        { name: 'UnitPriceNoTax', index: 'UnitPriceNoTax', width: 100 },
        { name: 'TotalNoTax', index: 'TotalNoTax', width: 100 },

        { name: 'Remark', index: 'Remark', width: 50 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '物品详细',
        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage1 == $("#list1").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }

            var dataSel = jQuery("#list1").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list1").jqGrid('getRowData', ids);
            var CID = Model.CID;
            reload1(CID);
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

function reload1(CID) {
    if ($('.field-validation-error').length == 0) {
        $("#list1").jqGrid('setGridParam', {
            url: 'PurchaseGoodsList',
            datatype: 'json',
            postData: { curpage: curPage1, rownum: OnePageCount1, CID: CID },

        }).trigger("reloadGrid");
    }
}
function RZ() {
    var Type = "请购";
    jQuery("#list2").jqGrid({
        url: 'SelectRZ',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },
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
        colNames: ['操作ID', '操作', '状态', '时间', '操作人'],
        colModel: [
              { name: 'RelevanceID', index: 'RelevanceID', width: 150 },
        { name: 'LogTitle', index: 'LogTitle', width: 150 },
          { name: 'LogContent', index: 'LogContent', width: 150 },
        { name: 'LogTime', index: 'LogTime', width: 150 },
        { name: 'LogPerson', index: 'LogPerson', width: 100 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount1,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',
        onSelectRow: function (rowid, status) {
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (curPage1 == $("#list2").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage1 = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager2 :input").val();
            }
            list2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });

}

function list2() {

    if ($('.field-validation-error').length == 0) {
        var Type = "请购";
        $("#list2").jqGrid('setGridParam', {
            url: 'SelectRZ',
            datatype: 'json',
            postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },

        }).trigger("reloadGrid");
    }

}


