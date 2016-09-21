
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
        type = location.search.split('&')[1].split('=')[1];
    }
    if (type == "1") {
        $("#orderid").show();
        $("#baid").hide();
    } else {
        $("#orderid").hide();
        $("#baid").show();
    }
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetProductInstallation",
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
                    var html = "";
                    html = '<tr id ="DetailInfo' + rowCount + '">'
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + '  id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable></td>';
                    html += '<td ><lable class="labNum' + rowCount + '  id="Num' + rowCount + '">' + json[i].Num + '</lable></td>';
                    html += '<td ><lable class="labUnitPrice' + rowCount + '  id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable></td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows += 1;
                }
            }
        }
    });
}

