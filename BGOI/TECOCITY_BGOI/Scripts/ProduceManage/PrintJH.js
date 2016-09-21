
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
        url: "PrintJHs",
        type: "Post",
        data: {
            Info: Info
        },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    // alert(json[i].PID);
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr id ="DetailInfo' + rowCount + '">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].Name + '</lable> </td>';
                    html += '<td  ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td  ><lable class="labSpecsModels' + rowCount + '  id="SpecsModels' + rowCount + '">' + json[i].Finishedproduct + '</lable></td>';
                    html += '<td ><lable class="labOrderNum' + rowCount + '  id="OrderNum' + rowCount + '">' + json[i].finishingproduct + '</lable></td>';
                    html += '<td ><lable class="labOnlineCount' + rowCount + '  id="OnlineCount' + rowCount + '">' + json[i].OnlineCount + '</lable></td>';

                    html += '<td><lable class="labOrderUnit' + rowCount + '  id="OrderUnit' + rowCount + '">' + json[i].Spareparts + '</lable></td>';
                    html += '<td  ><lable class="labTechnology' + rowCount + '  id="Technology' + rowCount + '">' + json[i].notavailable + '</lable></td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + '  id="DeliveryTime' + rowCount + '">' + json[i].Total + '</lable></td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + '  id="DeliveryTime' + rowCount + '">' + json[i].plannumber + '</lable></td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + '  id="DeliveryTime' + rowCount + '">' + json[i].demandnumber + '</lable></td>';
                    html += '<td ><lable class="labDeliveryTime' + rowCount + '  id="DeliveryTime' + rowCount + '">' + json[i].Remarks + '</lable></td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows += 1;
                }
            }
        }
    });
}


