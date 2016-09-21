
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    Search();
    // 打印 
    $("#btnPrint").click(function () {
        //alert(1);
        document.getElementById("btnPrint").className = "Noprint";
        $("#ReportContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#ReportContent").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });
})
function Search() {
    curRow = 0;
    curPage = 1;
    if (location.search != "") {
        start = location.search.split('&')[0].split('=')[1];
        end = location.search.split('&')[1].split('=')[1];
        Spec = location.search.split('&')[2].split('=')[1];
        PID = location.search.split('&')[3].split('=')[1];
        ProName = location.search.split('&')[4].split('=')[1];
        SingleLibrary = location.search.split('&')[5].split('=')[1];
        ListID = location.search.split('&')[6].split('=')[1];
     
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
}



