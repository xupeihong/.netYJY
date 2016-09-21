
$(document).ready(function () {
    if (location.search != "") {
        var XJID = location.search.split('&')[0].split('=')[1];
    }

    addBasicDetail(XJID);
  
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";
        $("#PrintArea").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#PrintArea").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });

});


function addBasicDetail(XJID)
{
    $.ajax({
        url: "SelectGoodsXJID",
        type: "post",
        data: { XJID: XJID },
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
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="XXID' + rowCount + '">' + json[i].XXID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    ;
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' "  id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="NegotiatedPricingNoTax' + rowCount + '">' + json[i].NegotiatedPricingNoTax + '</lable> </td>';

                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="TotalNegotiationNoTax' + rowCount + '">' + json[i].TotalNegotiationNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' "  id="DrawingNum' + rowCount + '">' + json[i].DrawingNum + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' "  id="Use' + rowCount + '">' + json[i].Use + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' "  id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
                document.getElementById("InquiryTitle").innerHTML = json[0].InquiryTitle;
                document.getElementById("InquiryDate").innerHTML = json[0].InquiryDate;
                document.getElementById("ContractCondition").innerHTML = json[0].ContractCondition;
                document.getElementById("InquiryExplain").innerHTML = json[0].InquiryExplain;
                document.getElementById("OrderContacts").innerHTML = json[0].OrderContacts;
            }
        }
    });
    }



 