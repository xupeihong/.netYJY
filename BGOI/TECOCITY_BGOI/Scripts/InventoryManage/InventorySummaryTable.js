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
        var Spec = $("#Spec").val();
        var ProName = $("#ProName").val();
        var url = "PrintInventorySummaryTable?start=" + start + "&end=" + end + "&Spec=" + Spec + "&ProName=" + ProName + "";
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });

    $('#Spec').click(function () {
        selid1('getSpecOptionalAdd', 'GJ', 'divGJ', 'ulGJ', 'LoadGJ');//, 'BuildUnit'
    })
})
function Search() {
    curRow = 0;
    curPage = 1;
    start = $('#start').val();
    end = $('#end').val();
    var Spec = $("#Spec").val();
    var ProName = $("#ProName").val();
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
    //rowCount = document.getElementById("DetailInfo").rows.length;
    $.ajax({
        url: "InventorySummaryTableList",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, end: end, start: start, ProName: ProName, Spec: Spec },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '">';
                    html += '<td ><lable class="labText' + rowCount + ' " id="Text' + rowCount + '">' + json[i].Text + '</lable> </td>';//物料类型
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';//物料编号
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';//物料名称
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';//物品规则
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';//单位

                    html += '<td ><lable class="labInCount' + rowCount + ' " id="InCount' + rowCount + '">' + json[i].InCount + '</lable> </td>';//本期入库数量
                    html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';//本期入库单价
                    html += '<td ><lable class="labinPrice' + rowCount + ' " id="inPrice' + rowCount + '">' + json[i].inPrice + '</lable> </td>';//本期入库金额

                    html += '<td ><lable class="labOutCount' + rowCount + ' " id="OutCount' + rowCount + '">' + json[i].OutCount + '</lable> </td>';//本期出库数量
                    html += '<td ><lable class="lablabUnitPrice1' + rowCount + ' " id="labUnitPrice1' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';//本期出库单价
                    html += '<td ><lable class="labOutPrice' + rowCount + ' " id="OutPrice' + rowCount + '">' + json[i].OutPrice + '</lable> </td>';//本期出库金额

                    html += '<td ><lable class="labpronum' + rowCount + ' " id="pronum' + rowCount + '">' + json[i].pronum + '</lable> </td>';//物料结余数量
                    html += '<td ><lable class="labUnitPrice2' + rowCount + ' " id="UnitPrice2' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';//物料结余单价
                    html += '<td ><lable class="labpronumtatalprice' + rowCount + ' " id="pronumtatalprice' + rowCount + '">' + json[i].pronumtatalprice + '</lable> </td>';//物料结余金额

                    html += '</tr>';
                    $("#DetailInfo").append(html);
                }
                TotalAdditional(json.length,CountRows);
            }
        }
    })
}

function TotalAdditional(num,nums) {
    curRow = 0;
    curPage = 1;
    start = $('#start').val();
    end = $('#end').val();
    var Spec = $("#Spec").val();
    var ProName = $("#ProName").val();
    if (start == "") {
        alert("请选择开始日期");
        return;
    }
    if (end == "") {
        alert("请选择结束日期");
        return;
    }
    $.ajax({
        url: "AdditionalList",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, end: end, start: start, Spec: Spec, ProName: ProName },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (num-nums== 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">';
                    html += '<td ><lable class="labText' + rowCount + ' " id="Text' + rowCount + '">合计</lable> </td>';//合计
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '"></lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '"></lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '"></lable> </td>';
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '"></lable> </td>';

                    html += '<td ><lable class="labInCount' + rowCount + ' " id="InCount' + rowCount + '">' + json[i].totalInCount + '</lable> </td>';//合计本期入库数量
                    html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].totalUnitPrice + '</lable> </td>';//合计本期入库单价
                    html += '<td ><lable class="labinPrice' + rowCount + ' " id="inPrice' + rowCount + '">' + json[i].totalPrice + '</lable> </td>';//合计本期入库金额

                    html += '<td ><lable class="labOutCount' + rowCount + ' " id="OutCount' + rowCount + '">' + json[i].totalOutCount + '</lable> </td>';//合计本期出库数量
                    html += '<td ><lable class="lablabUnitPrice1' + rowCount + ' " id="labUnitPrice1' + rowCount + '">' + json[i].totalUnitPrice + '</lable> </td>';//合计本期出库单价
                    html += '<td ><lable class="labOutPrice' + rowCount + ' " id="OutPrice' + rowCount + '">' + json[i].totalOutPrice + '</lable> </td>';//合计本期出库金额

                    html += '<td ><lable class="labpronum' + rowCount + ' " id="pronum' + rowCount + '">' + json[i].totalProNum + '</lable> </td>';//合计物料结余数量
                    html += '<td ><lable class="labUnitPrice2' + rowCount + ' " id="UnitPrice2' + rowCount + '">' + json[i].totalUnitPrice + '</lable> </td>';//合计物料结余单价
                    html += '<td ><lable class="labpronumtatalprice' + rowCount + ' " id="pronumtatalprice' + rowCount + '">' + json[i].totalProNumTotalPrice + '</lable> </td>';//合计物料结余金额

                    html += '</tr>';
                    $("#DetailInfo").append(html);
                }
               
            }
        }
    })
}



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