$(document).ready(function () {
    var PayId = "";
    var table = "Payment";
    var lie = "PayTime";
    var type = "MAX";
    $.ajax({
        url: "GetDataTime",
        type: "post",
        data: { table: table, lie: lie, type: type },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                document.getElementById("Begin").innerHTML = json[0].PayTime;
                var type = "min";
                $.ajax({
                    url: "GetDataTime",
                    type: "post",
                    data: { table: table, lie: lie, type: type },
                    dataType: "json",
                    success: function (data) {
                        var json = eval(data.datas);
                        if (json.length > 0) {
                            document.getElementById("End").innerHTML = json[0].PayTime;
                        }
                    }
                });
            }
        }
    });

    $.ajax({
        url: "GetSuppliers",
        type: "post",
        data: { },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                document.getElementById("supplier").innerHTML = json.length;
            }
        }
    });
    $.ajax({
        url: "SelectFKXQ",
        type: "post",
        data: { PayId: PayId },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                var weifukuan = 0;
                var jine = 0;
                var NOTotalNoTax = 0;
                var NOTotalNoTaxs = 0;
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    NOTotalNoTax = parseInt(json[i].TotalNoTax - json[i].Rate);
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="Amount' + rowCount + '"></lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="TotalNoTaxS' + rowCount + '">' + json[i].Rate + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="NOTotalNoTax' + rowCount + '">' + NOTotalNoTax + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                    jine += parseInt(json[i].TotalNoTax);
                    weifukuan += parseInt(json[i].Rate);
                    NOTotalNoTaxs += parseInt(NOTotalNoTax);
                }





                var html = "";
                html = '<tr>'
                html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">总计</lable> </td>';
                html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Specifications' + rowCount + '"></lable> </td>';
                html += '<td ><lable class="labSpecifications' + rowCount + ' " style="color:red"  id="Unit' + rowCount + '"></lable> </td>';
                html += '<td ><lable class="labSpecifications' + rowCount + ' " style="color:red" id="Amount' + rowCount + '">' + jine + '</lable> </td>';
                html += '<td ><lable class="labSpecifications' + rowCount + ' " style="color:red"  id="TotalNoTax' + rowCount + '">' + weifukuan + '</lable> </td>';
                html += '<td ><lable class="labSpecifications' + rowCount + ' " style="color:red" id="NOTotalNoTax' + rowCount + '">' + NOTotalNoTaxs + '</lable> </td>';
                html += '</tr>'
            
                $("#GXInfo1").append(html);
                document.getElementById("TotalNoTax").innerHTML = jine;
                document.getElementById("TotalNoTaxS").innerHTML = NOTotalNoTaxs;
 
            }
        }
    });
});