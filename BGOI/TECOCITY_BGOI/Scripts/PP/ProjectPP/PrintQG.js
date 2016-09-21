
$(document).ready(function () {
    if (location.search != "") {

        var CID = location.search.split('&')[0].split('=')[1];
    }
    addBasicDetail(CID);
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";
        $("#PrintArea").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#PrintArea").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });

});

var curPage = 1;
var OnePageCount = 15;
function addBasicDetail(CID)
{
    if (location.search != "") {

        $.ajax({
            url: "SelectGoodsQGID",
            type: "post",
            data: { CID: CID },
            dataType: "json",
            success: function (data) {

                var json = eval(data.datas);
                if (json.length > 0) {

                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("GXInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr>';
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowCount' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].INID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';


                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Amount' + rowCount + '"  style="width:45px;">' + json[i].Amount + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';

                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Total' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';

                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Use' + rowCount + '">' + json[i].GoodsUse + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="DrawingNum' + rowCount + '">' + json[i].DrawingNum + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                        html += '</tr>'
                        $("#GXInfo").append(html);
                    }

                    document.getElementById("PleaseDate").innerHTML = json[0].PleaseDate;
                    document.getElementById("DeliveryDate").innerHTML = json[0].DeliveryDate;
                    document.getElementById("PleaseExplain").innerHTML = json[0].PleaseExplain;
                    document.getElementById("OrderContacts").innerHTML = json[0].OrderContacts;

                }
            }
        })
    }

}
