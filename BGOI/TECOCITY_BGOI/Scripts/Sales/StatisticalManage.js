var curPage = 1;
var OnePageCount =15;
var PID;
var RelenvceID;
var Type = "";
var ProID;
var Pname;
var start;
var end;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    //jq();
    $("#pageContent").height($(window).height());
    $("#search").width($("#pageContent").width() - 30);
    $("#StaTab").height($("#pageContent").height() - 150);
    $("#CX").click(function () {
        var StartDate = $("#StartDate").val();
        var EndDate = $("#EndDate").val();
        var str1 = "<table id=\"T\" class=\"statitab scroll\" style=\"margin-left:10px;margin-top:5px;line-height:15px;\">" +
           "<tr  class=\"left\"><td style=\"width:4%\">规格型号</td><td style=\"width:4%\">物料编号</td><td style=\"width:4%\">合同编号</td><td style=\"width:4%\">订货单位</td><td style=\"width:4%\">订货内容</td><td style=\"width:4%\">单位</td>" +
            "<td style=\"width:3%\">数量</td><td style=\"width:4%\">订单下发日期</td><td style=\"width:4%\">交（提)货日期</td><td style=\"width:4%\">发货通知下发日期</td>" +
            "<td style=\"width:4%\">实际送货数量</td><td style=\"width:4%\">实际送货时间</td><td style=\"width:4%\">未发数量</td>" +
            "<td style=\"width:4%\">产品编号</td><td style=\"width:4%\">送货地址</td><td style=\"width:4%\">接货人电话</td><td style=\"width:4%\">备注</td></tr>";
        $("#StaTab").html(str1);
        $("#Sign").html("汇总说明:");
        $.ajax({
            url: "StatisticsManageTable",
            type: "post",
            data: { StartDate: StartDate, EndDate: EndDate  },
            dataType: "json",
            ansyc: false,
            success: function (data) {
               // var json = eval(data.datas);
                if (data.success == "true") {
                    $("#StaTab").html(data.strSb);
                    $("#Sign").html(data.strSign);
                }
            }
        })
    })
})
function jq() {
    var StartDate = $("#StartDate").val();
    var EndDate = $("#EndDate").val();

    $("#pageContent").height($(window).height());
    $("#search").width($("#pageContent").width() - 30);

    $("#StaTab").height($("#pageContent").height() - 50);



    var str1 = "<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">" +
       "<tr  class=\"left\"><td style=\"width:4%\">规格型号</td><td style=\"width:4%\">物料编号</td><td style=\"width:4%\">合同编号</td><td style=\"width:4%\">订货单位</td><td style=\"width:4%\">订货内容</td><td style=\"width:4%\">单位</td>" +
        "<td style=\"width:5%\">数量</td><td style=\"width:4%\">订单下发日期</td><td style=\"width:4%\">交（提)货日期</td><td style=\"width:4%\">发货通知下发日期</td>" +
        "<td style=\"width:4%\">实际送货数量</td><td style=\"width:4%\">实际送货时间</td><td style=\"width:4%\">未发数量</td>" +
        "<td style=\"width:4%\">产品编号</td><td style=\"width:4%\">送货地址</td><td style=\"width:4%\">接货人电话</td><td style=\"width:4%\">备注</td></tr>";


    $.ajax({
        url: "GetBasicDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        ansyc: false,
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick=selRow(this)>'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text"  onblur=XJ() id="Amount' + rowCount + '" style="width:30px;"/></td>';
                    //html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    //html += '<td ><lable class="labManufacturer' + rowCount + ' " readonly="readonly" id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><input type="text" onclick=CheckSupplier() id="Supplier' + rowCount + '"> </td>';
                    html += '<td ><input type="text" onblur=XJ() id="UnitPrice' + rowCount + '"> </td>';
                    html += '<td ><input type="text" id="txtTotal' + rowCount + '" style="width:60px;"/></td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                }


            }
        }
    })
    // 订单下发日期	交（提)货日期	发货通知下发日期	实际送货数量	实际送货时间	未发数量	产品编号	送货地址	接货人电话	备注
    str1 += "<tr class=\"staleft\"  style=\"color:red\">" + "<td style=\"width:4%\">合计</td>" +
        "<td style=\"width:4%\"></td>" +
         "<td style=\"width:4%\"></td>" +
          "<td style=\"width:4%\"></td>" +
           "<td style=\"width:4%\"></td>" +
            "<td style=\"width:4%\"></td>" +
             "<td style=\"width:4%\"></td>" +
              "<td style=\"width:4%\"></td>" +
               "<td style=\"width:4%\"></td>" +
                "<td style=\"width:4%\"></td>" +
                 "<td style=\"width:4%\"></td>" +
                  "<td style=\"width:4%\"></td>" +
                   "<td style=\"width:4%\"></td>" +
                    "<td style=\"width:4%\"></td>" +
                     "<td style=\"width:4%\"></td>" +
                      "<td style=\"width:4%\"></td>" +
                       "<td style=\"width:4%\"></td>" +
    "</tr></table>"
    $("#StaTab").html(str1);
    $("#Sign").html("汇总说明:开始日期-结束日期，共发生订单xxx起，累计订购金额xxxx元");




}

function jq1()
{
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    jQuery('#list').jqGrid({
        url: 'GetOrderSGrid',
        ansyc:true,
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, StartDate: StartDate, EndDate: EndDate },
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
        //订货单位	订货内容	规格型号	单位	数量	订单下发日期	交（提)货日期	发货通知下发日期	实际送货数量	实际送货时间	未发数量	产品编号	送货地址	接货人电话	备注
        colNames: ['规则型号', '物资编号', '合同编号', '订货单位', '订货内容', '单位', '数量', '订单下发日期', '交（提)货日期', '发货通知下发日期', '实际送货数量', '实际送货时间', '未发数量', '产品编号', '送货地址', '接货人电话','备注'],
        colModel: [
        { name: 'SpecsModels', index: 'SpecsModels' },
        { name: 'OrderID', index: 'OrderID', width: 100},
        { name: 'ContractID', index: 'ContractID', width: 100 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 100 },
        { name: 'OrderContactor', index: 'OrderContactor', width: 100 },
        { name: 'OrderDUnit', index: 'OrderDUnit', width: 100 },//货品单位
        { name: 'OrderNum', index: 'OrderNum', width: 100 },//数量
        { name: 'ContractDate', index: 'ContractDate', width: 100 },
        { name: 'DeliveryTime', index: 'DeliveryTime', width: 100 },//交货日期
        { name: 'ShipmentDate', index: 'ShipmentDate', width: 100 },
        { name: 'ShipmentNum', index: 'ShipmentNum', width: 100 },
        { name: 'SJFHDate', index: 'SJFHDate', width: 100 },
        { name: 'WFSL', index: 'WFSL', width: 100 },
        { name: 'ProductID', index: 'ProductID', width: 100 },
         { name: 'OrderAddress', index: 'OrderAddress', width: 100},
        { name: 'OrderTel', index: 'OrderTel', width: 100},
        { name: 'Remark', index: 'Remark', width: 100 }
        ],
        pager: jQuery('#pager'),
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
          //  select(rowid);
            $("#Billlist tbody").html("");
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
                curPage = $("#pager:input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() -30, false);
        }
    });
}


function LoadJq()
{
   // $("#StaTab").html(str1);
    $("#Sign").html("汇总说明:");
    $('#CX').click(function () {
     
        var StartDate = $('#StartDate').val();
        var EndDate = $('#EndDate').val();
      //  var Principal = $('#Principal').val();

        $.ajax({
            url: "GetOrdersInfoStatisticalGrid",
            type: "post",
            data: { StartDate: StartDate, EndDate: EndDate },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    //alert(data.strSb);
                   // $("#StaTab").html(data.strSb);
                    $("#Sign").html(data.strSign);

                }
                else {
                    return;
                }
            }
        });
    })
}

function reload()
{
    $("#list").jqGrid('setGridParam', {
        url: 'GetOrderSGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, StartDate: StartDate, EndDate: EndDate },

    }).trigger("reloadGrid");
}

function CheckPage(current) {
    if (current == "prev") {
        curPage = curPageIndex - 1;
        $("#T").html("");
        $("#pager").html("");
        LoadProduct();
    }
    if (current == "next") {
        curPage = curPageIndex + 1;
        $("#T").html("");
        $("#pager").html("");
        LoadProduct();
    }
    if (current == "total1") {
        curPage = PageCount;
        $("#T").html("");
        $("#pager").html("");
        LoadProduct();
    }
    if (current == "1") {
        curPage = 1;
        $("#T").html("");
        $("#pager").html("");
        LoadProduct();
    }
}