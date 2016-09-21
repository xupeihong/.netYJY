
$(document).ready(function () {
   jq();//加载数据
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
function jq() {
    if (location.search != "") {
        Info = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "PrintAllocationSheetJS",
        type: "Post",
        data: {
            Info: Info
        },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr id ="DetailInfo' + rowCount + '">'
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + '  id="Spec' + rowCount + '">' + json[i].SpecsModels + '</lable></td>';
                    html += '<td ><lable class="labStockInCount' + rowCount + '  id="StockInCount' + rowCount + '">' + json[i].OrderNum + '</lable></td>';
                    html += '<td ><lable class="labUnits' + rowCount + '  id="Units' + rowCount + '">' + json[i].NoTaxuUnit + '</lable></td>';
                    html += '<td ><lable class="labUnits' + rowCount + '  id="Units' + rowCount + '">' + json[i].Remark + '</lable></td>';
                    //html += '<td ></td>';
                    //html += '<td ></td>';
                    //html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    //html += '<td ><lable class="labTotal' + rowCount + ' " id="Total' + rowCount + '"></lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows += 1;
                }
            }
        }
    });
}

