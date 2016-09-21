
$(document).ready(function () {
    if (location.search != "") {
        DDID = location.search.split('&')[0].split('=')[1];
    }
    addBasicDetail(DDID);
 
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

function addBasicDetail(DDID) {
    DDID = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "SelectGoodsDDID",
        type: "post",
        data: { DDID: DDID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {

                for (var i = 0; i < json.length; i++) {

                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';

                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';

                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    //html += '<td ><input type="text" value="' + json[i].Amount + '" id="Amount' + rowCount + '" onblur="OnBlurAmount(this);" style="width:60px;"  /></td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';

                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    //html += '<td ><input type="text" value="' + json[i].TotalNoTax + '" style="width:80px;" id="TotalNoTax' + rowCount + '"> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="DrawingNum' + rowCount + '">' + json[i].DrawingNum + '</lable> </td>';
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="Use' + rowCount + '">' + json[i].Use + '</lable> </td>';
                    //html += '<td ><input type="text" value="' + json[i].Use + '" id="Use' + rowCount + '"> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
                document.getElementById("OrderDate").innerHTML = json[0].OrderDate;
                document.getElementById("xzrwlx").innerHTML = json[0].BusinessTypes;
                document.getElementById("TaskNum").innerHTML = json[0].PID;
                document.getElementById("Begin").innerHTML = json[0].DeliveryLimit;
                document.getElementById("DeliveryMethod").innerHTML = json[0].DeliveryMethod;
                document.getElementById("IsInvoice").innerHTML = json[0].IsInvoice;
                document.getElementById("PaymentMethod").innerHTML = json[0].PaymentMethod;
                document.getElementById("PaymentAgreement").innerHTML = json[0].PaymentAgreement;
                document.getElementById("ContractNO").innerHTML = json[0].ContractNO;
                document.getElementById("TheProject").innerHTML = json[0].TheProject;
                document.getElementById("OrderContacts").innerHTML = json[0].OrderContacts;

            }
        }
    });
}
