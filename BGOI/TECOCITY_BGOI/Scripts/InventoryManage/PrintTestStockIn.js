
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
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "PrictStockIn",
        type: "Post",
        data: {
            Info: Info
        },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            var total = 0;
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var num = json[i].TotalAmount.replace('.', '');
                    var str = num.split("");
                    var len = str.length;
                    total += parseInt(num);
                    var html = "";
                    html = '<tr id ="DetailInfo' + rowCount + '">'
                    html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td colspan="2" style="width: 150px;">' + json[i].ProName + '</td>';
                    html += '<td style="display:none" ><lable class="labSpec' + rowCount + '  id="Spec' + rowCount + '">' + json[i].Spec + '</lable></td>';
                    html += '<td style="width: 80px;">' + json[i].Units + '</td>';
                    html += '<td style="width: 80px;">' + json[i].StockInCount + '</td>';
                    html += '<td style="width: 80px;">' + json[i].UnitPrice + '</td>';
                    for (var j = 9; j >= 1; j--) {
                        if (str[len - j] != undefined) {
                            html += '<td style="width: 2%;">' + str[len - j] + '</td>';
                        } else {
                            if (j == len + 1) {
                                html += '<td style="width: 2%;">￥</td>';
                            } else {
                                html += '<td style="width: 2%;"></td>';
                            }
                        }
                    }
                    html += '<td colspan="3" style="width: 150px;"></td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows += 1;



                    //var html = "";
                    //html = '<tr id ="DetailInfo' + rowCount + '">'
                    //html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    //html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    //html += '<td ><lable class="labSpec' + rowCount + '  id="Spec' + rowCount + '">' + json[i].Spec + '</lable></td>';
                    //html += '<td ><lable class="labStockInCount' + rowCount + '  id="StockInCount' + rowCount + '">' + json[i].StockInCount + '</lable></td>';
                    //html += '<td ><lable class="labUnits' + rowCount + '  id="Units' + rowCount + '">' + json[i].Units + '</lable></td>';
                    //html += '<td ></td>';
                    //html += '<td ></td>';

                    ////html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    ////html += '<td ><lable class="labTotal' + rowCount + ' " id="Total' + rowCount + '"></lable> </td>';
                    //html += '</tr>'
                    //$("#DetailInfo").append(html);
                    //CountRows += 1;
                }
                var totalnum = total.toString().replace(',', '');
                var strtotal = totalnum.split("");
                var totlen = strtotal.length;
                var html = "";
                html = '<tr>'
                html += '<td colspan="5">合计</td>';
                for (var j = 9; j >= 1; j--) {
                    if (str[len - j] != undefined) {
                        html += '<td style="width: 2%;">' + str[len - j] + '</td>';
                    } else {
                        if (j == len + 1) {
                            html += '<td style="width: 2%;">￥</td>';
                        } else {
                            html += '<td style="width: 2%;"></td>';
                        }
                    }
                }
                html += '<td colspan="3" style="width: 150px;"></td>';
                html += '</tr>'
                $("#DetailInfo").append(html);
            }
        }
    });
}

