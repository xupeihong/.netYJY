
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    
    // 打印
    $("#btnPrint").click(function () {
        start = $('#start').val();
        end = $('#end').val();
        Spec = $("#Spec  option:selected").text();
        var url = "PrintTestingOfEquipmentStatistics?start=" + start + "&end=" + end + "&Spec=" + Spec + "";
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });
})
function reload() {
    ProType = $('#ProType option:selected').text();
    PID = $('#PID').val();
    ProName = $('#ProName').val();
    Spec = $('#Spec option:selected').text();
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
        url: 'TestingOfEquipmentStatisticsList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProType: ProType, PID: PID, ProName: ProName, Spec: Spec, HouseID: HouseID },

    }).trigger("reloadGrid");
}
function Search1() {
    curRow = 0;
    curPage = 1;
    start = $('#start').val();
    end = $('#end').val();
    HouseID = $('#HouseID option:selected').text();
    $("#list").jqGrid('setGridParam', {
        url: 'TestingOfEquipmentStatisticsList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, end: end, start: start, HouseID: HouseID
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入

}
function Searchout() {
    $("#titlenew").show();
    jq();
    curRow = 0;
    curPage = 1;
    start = $('#start').val();
    end = $('#end').val();
    Spec = $("#Spec  option:selected").text();
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
        url: "TestingOfEquipmentStatisticsList",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, end: end, start: start,  Spec: Spec },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '">'
                    html += '<td ><lable class="labUserName' + rowCount + ' " id="UserName' + rowCount + '">' + json[i].UserName + '</lable> </td>';//用户名称
                    html += '<td ><lable class="labTel' + rowCount + ' " id="Tel' + rowCount + '">' + json[i].Tel + '</lable> </td>';//联系电话
                    html += '<td ><lable class="labAddress' + rowCount + ' " id="Address' + rowCount + '">' + json[i].Address + '</lable> </td>';//用户地址

                    html += '<td ><lable class="labEquManType' + rowCount + ' " id="EquManType' + rowCount + '">' + json[i].EquManType + '</lable> </td>';//设备管理方式
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';//设备名称
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';//规格型号

                    html += '<td ><lable class="labUserType' + rowCount + ' " id="UserType' + rowCount + '">' + json[i].UserType + '</lable> </td>';//用户类别
                    html += '<td ><lable class="labDebPerson' + rowCount + ' " id="DebPerson' + rowCount + '">' + json[i].DebPerson + '</lable> </td>';//调试人员
                    html += '<td ><lable class="labInletPreP1' + rowCount + ' " id="InletPreP1' + rowCount + '">' + json[i].InletPreP1 + '</lable> </td>';//进口压力
                    html += '<td ><lable class="labBleedingpreP1' + rowCount + ' " id="BleedingpreP1' + rowCount + '">' + json[i].BleedingpreP1 + '</lable> </td>';//放散压力

                    html += '<td ><lable class="labPowerExportPreP2' + rowCount + ' " id="PowerExportPreP2' + rowCount + '">' + json[i].PowerExportPreP2 + '</lable> </td>';//出口压力P2
                    html += '<td ><lable class="labPowerOffPrePb' + rowCount + ' " id="PowerOffPrePb' + rowCount + '">' + json[i].PowerOffPrePb + '</lable> </td>';//关闭压力Pb
                    html += '<td ><lable class="labPowerCutOffPrePq' + rowCount + ' " id="PowerCutOffPrePq' + rowCount + '">' + json[i].PowerCutOffPrePq + '</lable> </td>';//切断压力Pq
                   
                    html += '<td ><lable class="labSDExportPreP2' + rowCount + ' " id="SDExportPreP2' + rowCount + '">' + json[i].SDExportPreP2 + '</lable> </td>';//出口压力P2
                    html += '<td ><lable class="labSDPowerOffPrePb' + rowCount + ' " id="SDPowerOffPrePb' + rowCount + '">' + json[i].SDPowerOffPrePb + '</lable> </td>';//关闭压力Pb
                    html += '<td ><lable class="labSDCutOffPrePq' + rowCount + ' " id="SDCutOffPrePq' + rowCount + '">' + json[i].SDCutOffPrePq + '</lable> </td>';//切断压力Pq

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
    Spec = $("#Spec  option:selected").text();
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
function jq() {
    start = $('#start').val();
    end = $('#end').val();
    document.getElementById("kaishi").innerHTML = start;
    document.getElementById("jiesu").innerHTML = end;
    //$("#kaishi").val(start);
    //$("#jiesu").val(end);
    $.ajax({
        url: "GetTestingOfEquipmentStatistics",
        type: "post",
        data: { start: start, end: end },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    document.getElementById("shulian").innerHTML = json[i].TotalNum;
                   // $("#shulian").val(json[i].TotalNum);
                }
            }
        }
    })
}


