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
    jq1();


    $("#XG").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要操作的行");
            return;
        }
        else {
            var XJID = Model.XJID;
            window.parent.parent.OpenDialog("修改", "../PPManage/UpdateXJXX?XJID=" + XJID + "", 1155, 550);
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

    $("#DY").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要打印的询价单");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).XJID;
        var url = "PrintXJ?id=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });

    $("#DJ").click(function () {
        this.className = "btnTw";
        $('#WP').attr("class", "btnTh");
        $('#rzxq').attr("class", "btnTh");

        $("#bor1").css("display", "none");
        $("#danju").css("display", "");
        $("#bor2").css("display", "none");

    });

    $("#DG").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            return;
        }
        else {
            var XJID = Model.XJID;
            $.ajax({
                url: "SelectDDCID",
                type: "post",
                data: { CID: XJID },
                dataType: "json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json != null) {
                        alert("该商品已订购");
                    }
                    else {
                        window.parent.parent.OpenDialog("订购", "../PPManage/AddOrder?XJID=" + XJID + "", 1155, 650);
                    }
                }
            });
        }
    });

    $("#WP").click(function () {

        this.className = "btnTw";
        $('#DJ').attr("class", "btnTh");
        $('#rzxq').attr("class", "btnTh");
        $("#bor1").css("display", "");
        $("#danju").css("display", "none");
        $("#bor2").css("display", "none");
    });

    $("#CX").click(function () {
        isConfirm = confirm("确定要撤销吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {
                alert("请选择要撤销的询价单");
                return;
            }
            var text = Model.text;
            var XJID = Model.XJID;

            $.ajax({
                url: "SelectDDCID",
                type: "post",
                data: { CID: XJID },
                dataType: "json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json != null) {
                        alert("该商品已订购,须先删除订购单");
                    }
                    else {
                        $.ajax({
                            url: "UpdateXJValidate",
                            type: "Post",
                            data: {
                                XJID: XJID
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
            var XJID = Model.XJID;
            window.parent.parent.OpenDialog("详情", "../PPManage/DetailsXJ?XJIDXQ=" + XJID + "", 900, 550);

        }
    });

    $("#SP").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行提交审批的项目单");
            return;
        }
        else {
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            var id = Model.State;
            if (id != 1)
            {
  var texts = jQuery("#list").jqGrid('getRowData', rowid).XJID + "@" + "询价审批";
            window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
            }
            else
            {
                alert("该询价单已提交审批");
            }
          
        }
    });
})

function DJXQ(XJID) {
    var CID = XJID;
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
function ddxiangqing() {
    var ddid = document.getElementById("DDID").innerHTML;
    window.parent.OpenDialog("详情", "../PPManage/DetailsDD?DDIDXQ=" + ddid + "", 1050, 650);
}

function jq() {

    var XJID = $('#XJID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $("#State").val();


    jQuery("#list").jqGrid({
        url: 'SelectInquiry',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, XJID: XJID, Begin: Begin, End: End, State: State },
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
        colNames: ['询价单号', '询价状态', '询价标题', '询价人', '单位','询价日期','提交审批状态',''],
        colModel: [
        { name: 'XJID', index: 'XJID', width: 150 },
         { name: 'text', index: 'text', width: 60 },
        { name: 'InquiryTitle', index: 'InquiryTitle', width: 60 },
        { name: 'UserName', index: 'UserName', width: 100 },
        { name: 'DeptName', index: 'DeptName', width: 100 },
        { name: 'InquiryDate', index: 'InquiryDate', width: 150 },
         { name: 'sp', index: 'sp', width: 150 },
          { name: 'State', index: 'State', width: 150 ,hidden:true}
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
                var XJID = Model.XJID;
                reload1(XJID);
                DJXQ(XJID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 450, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}


function reload() {

    var XJID = $('#XJID').val();
    var Begin = $('#Begin').val();
    var End = $('#End').val();
    var State = $("#State").val();
    $("#list").jqGrid('setGridParam', {
        url: 'SelectInquiry',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, XJID: XJID, Begin: Begin, End: End, State: State },

    }).trigger("reloadGrid");
}

function jq1(XJID) {
    jQuery("#list1").jqGrid({
        url: 'SelectXJ',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, XJID: XJID },
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
        colNames: ['编号', '物品名称', '规格型号', '单位', '数量', '供货商', '询价单价', '询价金额', '备注'],
        colModel: [
        { name: 'XJID', index: 'XJID', width: 180 },
        { name: 'OrderContent', index: 'OrderContent', width: 150 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Unit', index: 'Unit', width: 50 },
        { name: 'Amount', index: 'Amount', width: 80 },

        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'NegotiatedPricingNoTax', index: 'NegotiatedPricingNoTax', width: 180 },
        { name: 'TotalNegotiationNoTax', index: 'TotalNegotiationNoTax', width: 100 },
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
            var XJID = Model.XJID;
            reload1(CID);
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() - 450, false);
            $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        }
    });
}

function reload1(XJID) {
    $("#list1").jqGrid('setGridParam', {
        url: 'SelectXJ',
        datatype: 'json',
        postData: { curpage: curPage1, rownum: OnePageCount1, XJID: XJID },

    }).trigger("reloadGrid");
}


//查询
function SearchOut() {

    var strDateStart = $('#Begin').val();
    var strDateEnd = $('#End').val();
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

    if (strDateS <= strDateE) {
        getSearch();
    }
    else if (strDateS >= strDateE) {
        alert("截止日期不可以小于或等于开始日期");
        $("#End").val("");
    }
    else (strDateS == "" || strDateE == "")
    {
        getSearch();
    }

}


function getSearch() {
    curRow = 0;
    curPage = 1;

    var XJID = $('#XJID').val();
    var OrderContent = $('#OrderContent').val();
    var State = $('#State').val();
    if (State == "1") {
        State = "2";
    }
    var Begin = $('#Begin').val();
    var End = $('#End').val();

    $("#list").jqGrid('setGridParam', {
        url: 'SelectInquiry',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, XJID: XJID, OrderContent: OrderContent, State: State, Begin: Begin, End: End },
        loadonce: false

    }).trigger("reloadGrid");//重新载入

}

function RZ() {
    var Type = "询价";
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
        var Type = "询价";
        $("#list2").jqGrid('setGridParam', {
            url: 'SelectRZ',
            datatype: 'json',
            postData: { curpage: curPage1, rownum: OnePageCount1, Type: Type },

        }).trigger("reloadGrid");
    }

}





