
var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    //jq();
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
    curRow = 0;
    curPage = 1;
    $.ajax({
        url: "InventoryStatisticsList",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr id ="DetailInfo' + rowCount + '">'
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';//物料编号
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';//物料名称
                    html += '<td ><lable class="labHouseName' + rowCount + ' " id="HouseName' + rowCount + '">' + json[i].Spec + '</lable> </td>';//
                    html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';//计量单位
                    html += '<td ><lable class="labFinCount' + rowCount + ' " id="FinCount' + rowCount + '">' + json[i].StockInCount + '</lable> </td>';//
                    html += '<td ><lable class="labOnlineCount' + rowCount + ' " id="OnlineCount' + rowCount + '">' + json[i].OnlineCount + '</lable> </td>';//在线生产数量
                  //  html += '<td ><lable class="labCPnum' + rowCount + ' " id="CPnum' + rowCount + '">' + json[i].CPnum + '</lable> </td>';//库存成品零件数（套）
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
}



