
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    if (location.search != "") {
        Info = location.search.split('&')[0].split('=')[1];
    }
    if (Info == "1") {
        $("#ckhzb").show();
        $("#kczd").hide();
    } else {
        $("#ckhzb").hide();
        $("#kczd").show();
    }
    // 打印
    $("#btnPrint").click(function () {
        start = $('#start').val();
        end = $('#end').val();

        //Spec = $("#Spec  option:selected").text();
        Spec = $('#Spec').val();
        PID = $('#PID').val();
        ProName = $('#ProName').val();
        ListID = $('#ListID').val();
        var SingleLibrary = $("input[name='SingleLibrary']:checked").val();
        var url = "PrintInventoryBill?start=" + start + "&end=" + end + "&Spec=" + Spec + "&PID=" + PID + "&ProName=" + ProName + "&SL=" + SingleLibrary + "&in=" + ListID + "";
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });


    $('#Spec').click(function () {
        selid1('getSpecOptionalAdd', 'GJ', 'divGJ', 'ulGJ', 'LoadGJ');//, 'BuildUnit'
    })
})

function reload() {
    ProType = $('#ProType option:selected').text();
    PID = $('#PID').val();
    ProName = $('#ProName').val();
    //Spec = $("#Spec  option:selected").text();
    Spec = $('#Spec').val();
    HouseID = $('#HouseID option:selected').text();

    if (ProType == "请选择") {
        ProType = "";
    }
    if (Spec == "请选择") {
        Spec = "";
    }
    if (HouseID == "请选择") {
        HouseID = "";
    }
    $("#list").jqGrid('setGridParam', {
        url: 'MaterialSummaryTableList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProType: ProType, PID: PID, ProName: ProName, Spec: Spec, HouseID: HouseID },

    }).trigger("reloadGrid");
}
function Search1() {
    curRow = 0;
    curPage = 1;
    start = $('#start').val();
    end = $('#end').val();
    Spec = $("#Spec  option:selected").text();
    PID = $('#PID').val();
    ProName = $('#ProName').val();


    ListInID = $('#ListInID').val();
    ListOutID = $('#ListOutID').val();
    var SingleLibrary = $("input[name='SingleLibrary']:checked").val();

    $("#list").jqGrid('setGridParam', {
        url: 'InventoryBillList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, end: end, start: start, ProName: ProName, Spec: Spec, PID: PID
            , ListOutID: ListOutID, ListInID: ListInID, SingleLibrary: SingleLibrary
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入

}
function Search() {
    curRow = 0;
    curPage = 1;
    start = $('#start').val();
    end = $('#end').val();

    //Spec = $("#Spec  option:selected").text();
    Spec = $('#Spec').val();
    PID = $('#PID').val();
    ProName = $('#ProName').val();

    ListID = $('#ListID').val();
   // ListOutID = $('#ListOutID').val();
    var SingleLibrary = $("input[name='SingleLibrary']:checked").val();

    if (start == "") {
        alert("请选择开始日期");
        return;
    }
    if (end == "") {
        alert("请选择结束日期");
        return;
    }
    for (var i = document.getElementById("DetailInfo").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("DetailInfo").removeChild(document.getElementById("DetailInfo").childNodes[i]);
    }
    $.ajax({
        url: "InventoryBillList",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, end: end, start: start, ProName: ProName, Spec: Spec, PID: PID, ListID: ListID, SingleLibrary: SingleLibrary },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    //$("#n").val(json[0].n);
                    document.getElementById('n').innerHTML = json[0].n;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '">'

                        html += '<td ><lable class="laby' + rowCount + ' " id="y' + rowCount + '">' + json[i].y + '</lable> </td>';//月
                        html += '<td ><lable class="labr' + rowCount + ' " id="r' + rowCount + '">' + json[i].r + '</lable> </td>';//日
                        html += '<td ><lable class="labListInID' + rowCount + ' " id="ListInID' + rowCount + '">' + json[i].rkd + '</lable> </td>';//编号

                        html += '<td ><lable class="labText' + rowCount + ' " id="PID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';//编号
                        html += '<td ><lable class="labPID' + rowCount + ' " id="HouseName' + rowCount + '">' + json[i].HouseName + '</lable> </td>';//摘要

                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';//产品名字
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';//规格型号

                        var pandu = json[i].rkd.substr(0, 1);
                        //判断入库单
                        if (pandu == "A") {

                            html += '<td ><lable class="labStockInCount' + rowCount + ' " id="StockInCount' + rowCount + '">' + json[i].StockInCount + '</lable> </td>';//入库数量
                            html += '<td ><lable class="labUnitPricet' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].Price2 + '</lable> </td>';//入库均价
                            html += '<td ><lable class="labintotalprice' + rowCount + ' " id="intotalprice' + rowCount + '">' + json[i].intotalpricec + '</lable> </td>';//入库金额
                        } else {
                            html += '<td ><lable class="labStockInCount' + rowCount + ' " id="StockInCount' + rowCount + '">0</lable> </td>';//入库数量
                            html += '<td ><lable class="labUnitPricet' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].Price2 + '</lable> </td>';//入库均价
                            html += '<td ><lable class="labintotalprice' + rowCount + ' " id="intotalprice' + rowCount + '">0</lable> </td>';//入库金额
                        }
                        if (pandu == "B") {
                            html += '<td ><lable class="labStockOutCount' + rowCount + ' " id="StockOutCount' + rowCount + '">' + json[i].StockInCount + '</lable> </td>';//出库数量
                            html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].Price2 + '</lable> </td>';//出库均价
                            html += '<td ><lable class="labouttotalprice' + rowCount + ' " id="outtotalprice' + rowCount + '">' + json[i].intotalpricec + '</lable> </td>';//出库金额
                        } else {
                            html += '<td ><lable class="labStockOutCount' + rowCount + ' " id="StockOutCount' + rowCount + '">0</lable> </td>';//出库数量
                            html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].Price2 + '</lable> </td>';//出库均价
                            html += '<td ><lable class="labouttotalprice' + rowCount + ' " id="outtotalprice' + rowCount + '">0</lable> </td>';//出库金额
                        }
                        html += '<td ><lable class="labFinishCount' + rowCount + ' " id="FinishCount' + rowCount + '">' + json[i].FinishCount + '</lable> </td>';//结余数量
                        html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].Price2 + '</lable> </td>';//结余均价
                        html += '<td ><lable class="labfintotalprice' + rowCount + ' " id="fintotalprice' + rowCount + '">' + json[i].fintotalprice + '</lable> </td>';//结余金额

                        html += '</tr>'
                        $("#DetailInfo").append(html);
                }
                // TotalAdditional(json.length, CountRows);
            }
        }
    })
}
function TotalAdditional(num, nums) {
    curRow = 0;
    curPage = 1;
    start = $('#start').val();
    end = $('#end').val();
    if (start == "") {
        alert("请选择开始日期");
        return;
    }
    if (end == "") {
        alert("请选择结束日期");
        return;
    }
    //Spec = $("#Spec  option:selected").text();
    Spec = $('#Spec').val();
    $.ajax({
        url: "AddWarehouseList",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, end: end, start: start, Spec: Spec },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (num - nums == 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labText' + rowCount + ' " id="Text' + rowCount + '">合计</lable> </td>';//合计
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '"></lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '"></lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '"></lable> </td>';
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '"></lable> </td>';

                    html += '<td ><lable class="labtotalInCount' + rowCount + ' " id="totalInCount' + rowCount + '">' + json[i].totalInCount + '</lable> </td>';//合计本期入库数量
                    html += '<td ><lable class="labtotalUnitPrice' + rowCount + ' " id="totalUnitPrice' + rowCount + '">' + json[i].totalUnitPrice + '</lable> </td>';//合计本期入库均价
                    html += '<td ><lable class="labtotalRKzts' + rowCount + ' " id="totalRKzts' + rowCount + '">' + json[i].totalRKzts + '</lable> </td>';//合计本期入库次数
                    html += '<td ><lable class="labtotalPrice' + rowCount + ' " id="totalPrice' + rowCount + '">' + json[i].totalPrice + '</lable> </td>';//合计本期入库金额

                    html += '<td ><lable class="labtotalOutCount' + rowCount + ' " id="totalOutCount' + rowCount + '">' + json[i].totalOutCount + '</lable> </td>';//合计本期出库数量
                    html += '<td ><lable class="labtotalUnitPrice1' + rowCount + ' " id="totalUnitPrice1' + rowCount + '">' + json[i].totalUnitPrice + '</lable> </td>';//合计本期出库均价
                    html += '<td ><lable class="labtotalRKzts' + rowCount + ' " id="totalRKzts' + rowCount + '">' + json[i].totalRKzts + '</lable> </td>';//合计物料结余次数
                    html += '<td ><lable class="labtotalOutPrice' + rowCount + ' " id="totalOutPrice' + rowCount + '">' + json[i].totalOutPrice + '</lable> </td>';//合计本期出库金额


                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }

            }
        }
    })
}

//function OffTheLibrary(str) {
//    if (str != "") {
//        $("#chuku").hide();
//        $("#ListOutID").hide();
//    }
//}
//function OnTheLibrary(str) {
//    if (str != "") {
//        $("#ruku").hide();
//        $("#ListInID").hide();
//    }
//}

function LoadGJ(liInfo) {
    $('#Spec').val(liInfo);
    $('#divGJ').hide();
}
function BuildUnitkey() {
    $('#divGJ').hide();
}
function selid1(actionid, selid, divid, ulid, jsfun) {
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