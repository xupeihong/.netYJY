
$(document).ready(function () {

    if (location.search.split('&')[0].split('=')[0] == "?RKIDXQ") {
        var RKID = location.search.split('&')[0].split('=')[1];
        XGRK(RKID);
    }
});

function XGRK(RKID) {
    $.ajax({
        url: "RKXQ",
        type: "post",
        data: { RKID: RKID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].SJAmount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
     
                    html += '</tr>'
                    $("#GXInfo").append(html);

                }

                document.getElementById("Begin").innerHTML = json[0].Rkdate;
                document.getElementById("CKID").innerHTML = json[0].HouseName;
                document.getElementById('RKInstructions').innerHTML = json[0].RKInstructions;
                document.getElementById("OrderContacts").innerHTML = json[0].Handler;
            }
        }


    })
}

