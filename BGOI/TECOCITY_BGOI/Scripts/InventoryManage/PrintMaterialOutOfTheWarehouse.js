
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
        $.ajax({
            url: "PrintMaterialOutOfTheList",
            type: "post",
            data: { curpage: curPage, rownum: OnePageCount, end: end, start: start },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '">'
                        html += '<td ><lable class="labText' + rowCount + ' " id="Text' + rowCount + '">' + json[i].Text + '</lable> </td>';//物料类型
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';//物料编号
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';//物料名称
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';//物品规则
                        html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';//单位

                        html += '<td ><lable class="labInCount' + rowCount + ' " id="InCount' + rowCount + '">' + json[i].InCount + '</lable> </td>';//入库数量
                        html += '<td ><lable class="labUnitPricet' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';//入库均价
                        html += '<td ><lable class="labRKzts' + rowCount + ' " id="RKzts' + rowCount + '">' + json[i].RKzts + '</lable> </td>';//入库次数
                        html += '<td ><lable class="labinPrice' + rowCount + ' " id="inPrice' + rowCount + '">' + json[i].inPrice + '</lable> </td>';//入库金额

                        html += '<td ><lable class="labOutCount' + rowCount + ' " id="OutCount' + rowCount + '">' + json[i].OutCount + '</lable> </td>';//出库数量
                        html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';//出库均价
                        html += '<td ><lable class="labCKzts' + rowCount + ' " id="CKzts' + rowCount + '">' + json[i].CKzts + '</lable> </td>';//出库次数
                        html += '<td ><lable class="labOutPrice' + rowCount + ' " id="OutPrice' + rowCount + '">' + json[i].OutPrice + '</lable> </td>';//出库金额
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                }
            }
        })
    }
}



