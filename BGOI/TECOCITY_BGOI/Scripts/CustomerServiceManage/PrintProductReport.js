
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
        trid = location.search.split('&')[2].split('=')[1];
    }
    if (type == "1") {
        $("#dayin").hide();
        $("#jc").hide();
        $("#rq").hide();
    }
    if (type == "5") {
        $("#bzpg").hide();
        $("#idbh").show();

        $("#pgd").show();
        $("#pgd1").show();
        $("#pgd2").show();

        $("#pgry").show();

        $("#badj").hide();
        $("#bzdj1").hide();
        $("#bzdj2").hide();

        $("#TRID").val(trid);
    }
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetProductReport",
        type: "Post",
        data: {
            Info: Info
        },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    //AZID, BZDID, DID, PID, OrderContent, SpecsModels, Unit, Num, UnitPrice, Total
                    var html = "";
                    html = '<tr id ="DetailInfo' + rowCount + '">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td style="display:none" ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + '  id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable></td>';
                    html += '<td ><lable class="labunit' + rowCount + '  id="unit' + rowCount + '">' + json[i].Unit + '</lable></td>';
                    html += '<td ><lable class="labNum' + rowCount + '  id="Num' + rowCount + '">' + json[i].Num + '</lable></td>';
                    html += '<td ><lable class="labUnitPrice' + rowCount + '  id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable></td>';
                    html += '<td ><lable class="labPrice' + rowCount + '  id="Price' + rowCount + '"></lable></td>';
                    html += '<td ><lable class="labSalesChannel' + rowCount + '  id="SalesChannel' + rowCount + '">' + json[i].SalesChannel + '</lable></td>';
                    if (json[i].IsPendingCollection == "1") {
                        html += '<td ><lable class="labIsPendingCollection' + rowCount + '  id="IsPendingCollection' + rowCount + '">是</lable></td>';
                    } else {
                        html += '<td ><lable class="labIsPendingCollection' + rowCount + '  id="IsPendingCollection' + rowCount + '">否</lable></td>';
                    }
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows += 1;
                }
            }
        }
    });
}

