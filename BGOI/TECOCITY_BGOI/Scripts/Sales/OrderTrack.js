$(document).ready(function () {
    $("#StartDate").val("");
    $("#EndDate").val("");
    LoadOrderInfo();
    LoadOrderTrack();
 //   document.getElementById('div1').style.display = 'block';
});
var curPage = 1;
var DcurPage = 1;
var OnePageCount = 5;
var DID = 0;
var OOstate;
var oldSelID = 0;
var SPID = "";
var PID = "";
var State;
var OrderJE = 0;
var rowid = 0;
function reload() {
    $("#list").jqGrid('setGridParam', {
        url: 'GetOrderInfo',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },

    }).trigger("reloadGrid");
}
function select(rowid) {
    PID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    DID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
    OrderJE = jQuery("#list").jqGrid('getRowData', rowid).Total;
    LoadOrderTrack();
}
function LoadOrderInfo() {
    jQuery('#list').jqGrid({
        url: 'GetOrderInfo',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
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
        colNames: ['', '合同编号', '订单编号', '订货单位', '使用单位', '合同总额', '回款总额', '欠款金额', '状态', '创建人', '订货单位联系人', '订货单位联系方式', '使用单位联系人', '使用单位联系人方式', '交货日期', '', '', '', '', ''],
        colModel: [
        { name: 'PID', index: 'PID', width: 100, hidden: true },
        { name: 'ContractID', index: 'ContractID', width: 150 },
        { name: 'OrderID', index: 'OrderID', width: 150 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 100 },
         { name: 'UseUnit', index: 'UseUnit', width: 100 },
        { name: 'Total', index: 'Total', width: 100 },
        { name: 'HKAmount', index: 'HKAmount', width: 100 },
         { name: 'DebtAmount', index: 'DebtAmount', width: 100 },
        { name: 'Ostate', index: 'Ostate', width: 100 },
        { name: 'CreateUser', index: 'CreateUser', width: 100 },
          { name: 'OrderContactor', index: 'OrderContactor', width: 100 },
        { name: 'OrderTel', index: 'OrderTel', width: 100 },
        { name: 'UseContactor', index: 'UseContactor', width: 100 },
        { name: 'UseTel', index: 'UseTel', width: 100 },
        { name: 'DeliveryTime', index: 'DeliveryTime', width: 100 },
         { name: 'OOstate', index: 'OOstate', width: 100, hidden: true },
        { name: 'State', index: 'State', width: 100, hidden: true },
        { name: 'ISF', index: 'ISF', width: 100, hidden: true },
             { name: 'ISHT', index: 'ISHT', width: 100, hidden: true },
        { name: 'EXState', index: 'EXState', width: 100, hidden: true }
        ],
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '订单主表',
        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
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
            DID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
            OOstate = jQuery("#list").jqGrid('getRowData', rowid).OOstate;
            State = jQuery("#list").jqGrid('getRowData', rowid).State;
            OrderJE = jQuery("#list").jqGrid('getRowData', rowid).Total;//订单金额
            select(rowid);
            //$("#Billlist tbody").html("");
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager6") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager6") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager6") {
                if (curPage == 1)
                    return;
                curPage = $("#list").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager6") {
                curPage = 1;
            }
            else {
                curPage = $("#pager6 :input").val();
            }
            reloadS();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", 150, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}


function reload1() {
    $("#Detaillist").jqGrid('setGridParam', {
        url: 'GetOrderPstate',
        datatype: 'json',
        postData: { OrderID: DID, curpage: DcurPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}
//获取订单的流程信息
function LoadOrderTrack()
{
    $("#div1").html("");
   // DID = jQuery("#list").jqGrid('getRowData', rowid).OrderID;
    $.ajax({
        url: "GetOrderPstate",
        type: "post",
        data: { Orderid: DID },
        dataType: "json",
        async: false,
        success: function (data) {

            var json = eval(data.datas);

            var html = "";
            if (json.length > 0) {
                //for (var i =0;i<= parseInt(json[0].Pstate); i++) {
                //    // var html = "";
                  //  var ps = i;
                    $.ajax({
                        url: "GetOrderPstateConfig",
                        type: "post",
                        data: { pstate: json[0].Pstate },
                        dataType: "json",
                        ansyc: false,
                        success: function (data) {
                            var json1 = eval(data.datas);
                            if (json1 != undefined) {
                                if (json1.length > 0) {
                                    //var html = "";
                                    for (var j = 0; j < json1.length; j++) {
                                        if (j < json1.length - 1) {
                                            html += '<lable>' + json1[j].StateDesc + '——————></lable>';
                                        }
                                        else {
                                            html += '<lable>' + json1[j].StateDesc + '</lable>';
                                        }
                                        //  $("#div1").append(html);
                                    }
                                    $("#div1").append(html);
                                }
                            } else {
                                html = '<lable>订单新建=></lable>';
                                $("#div1").append(html);
                            }
                          
                        }

                    });
                 
                  //  alert(html);
                //}
            }
            $("#div1").html(html);
        }
    });
}

function LoadSerachOrderInfo() {


    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    if (StartDate == "" && EndDate == "") {
        GetSearchData();
    }
    else {
        var strSeparator = "-"; //日期分隔符
        var strDateArrayStart;
        var strDateArrayEnd;
        var intDay;
        strDateArrayStart = StartDate.split(strSeparator);
        strDateArrayEnd = EndDate.split(strSeparator);
        var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
        var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);
        if (strDateS <= strDateE) {
            GetSearchData();
        }
        else {
            alert("截止日期不能小于开始日期！");
            $("#End").val("");
            return false;
        }
    }


}
function GetSearchData() {
    curPage = 1;
    OnePageCount = 5;
    if ($('.field-validation-error').length == 0) {
        var ContractID = $('#ContractID').val();
        var OrderUnit = $('#OrderUnit').val();
        var UseUnit = $('#UseUnit').val();
        var OrderContent = $('#MainContent').val(); //$('#SpecsModels').click.Text();
        var StartDate = $('#StartDate').val();
        var EndDate = $('#EndDate').val();
        var State = $("input[name='State']:checked").val();
        var HState = $("input[name='HState']:checked").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetSearchOrderInfo',
            datatype: 'json',
            postData: {
                curpage: curPage, rownum: OnePageCount, ContractID: ContractID, OrderUnit: OrderUnit,
                UseUnit: UseUnit, MainContent: OrderContent, StartDate: StartDate,
                EndDate: EndDate, State: State, HState: HState
            },
            loadonce: false

        }).trigger("reloadGrid");//重新载入

    }
}

function reloadS() {
    var ContractID = $('#ContractID').val();
    var OrderUnit = $('#OrderUnit').val();
    var UseUnit = $('#UseUnit').val();
    var OrderContent = $('#MainContent').val(); //$('#SpecsModels').click.Text();
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    var State = $("input[name='State']:checked").val();
    var HState = $("input[name='HState']:checked").val();
    $("#list").jqGrid('setGridParam', {
        url: 'GetSearchOrderInfo',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, ContractID: ContractID, OrderUnit: OrderUnit,
            UseUnit: UseUnit, MainContent: OrderContent, StartDate: StartDate,
            EndDate: EndDate, State: State, HState: HState
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}


