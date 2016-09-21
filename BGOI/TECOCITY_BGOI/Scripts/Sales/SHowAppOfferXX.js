$(document).ready(function () {
    if (location.search != "") {
        BJID = location.search.split('&')[0].split('=')[1];
    }
    LoadProjectDetail();

});
function LoadProjectDetail() {
    var BJID = $("#BJID").val();
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //  var PID = ID;
    $.ajax({
        url: "GetOfferInfoGrid",
        type: "post",
        data: { BJID: BJID },
        dataType: "json",
        ansyc: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" >';
                    html += '<td ><lable class="labRowNumber' + rowCount + '"  id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + '" id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + '" id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + '" id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + '" id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable  id="Amount' + rowCount + '" style="width:30px;" >' + json[i].Amount + '</lable></td>';
                    html += '<td ><lable onclick=CheckSupplier()  id="Supplier' + rowCount + '" >' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lalbe  id="UnitPrice' + rowCount + '" >' + json[i].UintPrice + '</lable></td>';
                    html += '<td ><lable style="width:100px;" id="txtTotal' + rowCount + '" >' + json[i].Total + '</lable> </td>';
                    html += '<td ><lable style="width:100px;" class="labRemark' + rowCount + ' " id="Remark' + rowCount + '" >' + json[i].Remark + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labXID' + rowCount + '" id="XID' + rowCount + '">' + json[i].XID + '</lable> </td>';
                    html += '</tr>';
                    $("#DetailInfo").append(html);

                }


            }
        }
    })
}