
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
   // Search();
    // 打印 
    $("#btnPrint").click(function () {

        document.getElementById("btnPrint").className = "Noprint";
        $("#ReportContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#ReportContent").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });
})
//function Search() {
//    if (location.search != "") {
//        start = location.search.split('&')[0].split('=')[1];
//        end = location.search.split('&')[1].split('=')[1];
//        HouseID = location.search.split('&')[2].split('=')[1];
//        curRow = 0;
//        curPage = 1;
//        $.ajax({
//            url: "PrintMaterial",
//            type: "post",
//            data: { curpage: curPage, rownum: OnePageCount, start: start, end: end, HouseID: HouseID },
//            dataType: "json",
//            success: function (data) {
//                var json = eval(data.datas);
//                if (json.length > 0) {
//                    for (var i = 0; i < json.length; i++) {
//                        rowCount = document.getElementById("DetailInfo").rows.length;
//                        var CountRows = parseInt(rowCount) + 1;
//                        var html = "";
//                        html = '<tr  id ="DetailInfo' + rowCount + '">'

//                        html += '<td ><lable class="labText' + rowCount + ' " id="Text' + rowCount + '">' + json[i].Text + '</lable> </td>';//物料类型
//                        html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';//物料编号
//                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';//物料名称
//                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';//物品规则
//                        html += '<td ><lable class="labHouseNAME' + rowCount + ' " id="HouseNAME' + rowCount + '">' + json[i].HouseNAME + '</lable> </td>';//所属仓库
//                        html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';//单位
//                        html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';//盘点日单价

//                        html += '<td ><lable class="labFinishCount' + rowCount + ' " id="FinishCount' + rowCount + '">' + json[i].FinishCount + '</lable> </td>';//盘点日账面数量
//                        html += '<td ><lable class="labtotal' + rowCount + ' " id="total' + rowCount + '">' + json[i].total + '</lable> </td>';//盘点日账面金额

//                        html += '<td ><lable class="labFinishCount' + rowCount + ' " id="FinishCount' + rowCount + '">' + json[i].FinishCount + '</lable> </td>';//盘点日记录数量

//                        html += '<td ><lable class="labInCount' + rowCount + ' " id="InCount' + rowCount + '">' + json[i].InCount + '</lable> </td>';//盘点日至基准日入帐数入库数量
//                        html += '<td ><lable class="labOutCount' + rowCount + ' " id="FinishCount' + rowCount + '">' + json[i].OutCount + '</lable> </td>';//盘点日至基准日入帐数出库数量

//                        html += '<td ><lable class="labtotalCount' + rowCount + ' " id="totalCount' + rowCount + '">' + json[i].totalCount + '</lable> </td>';//基准日应结存数量
//                        html += '<td ><lable class="labtotalPrice' + rowCount + ' " id="totalPrice' + rowCount + '">' + json[i].totalPrice + '</lable> </td>';//基准日应结存金额

//                        html += '<td ><lable class="labFinishCount1' + rowCount + ' " id="FinishCount1' + rowCount + '">' + json[i].Cynum + '</lable> </td>';//差异数量
//                        html += '<td ><lable class="labtotal1' + rowCount + ' " id="total1' + rowCount + '">' + json[i].CynumPrice + '</lable> </td>';//差异金额
//                        html += '</tr>'
//                        $("#DetailInfo").append(html);
//                    }
//                    //
//                }
//            }
//        })
//    }
   
//}

