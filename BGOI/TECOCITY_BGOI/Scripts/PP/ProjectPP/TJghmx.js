$(document).ready(function () {
    var SHID = "";
    var THID = "";
    var table = "ReceivingInformation";
    var lie = "ArrivalDate";
    var type = "MAX";
    $.ajax({
        url: "GetDataTime",
        type: "post",
        data: { table: table, lie: lie, type: type },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                document.getElementById("Begin").innerHTML = json[0].ArrivalDate;
                var table = "ReturnGoods";
                var type = "min";
                var lie = "ReturnDate";
                $.ajax({
                    url: "GetDataTime",
                    type: "post",
                    data: { table: table, lie: lie, type: type },
                    dataType: "json",
                    success: function (data) {
                        var json = eval(data.datas);
                        if (json.length > 0) {
                            document.getElementById("End").innerHTML = json[0].ReturnDate;
                        }
                    }
                });
            }
        }
    });

    var Amounts = 0;
    var jine = 0;
    $.ajax({
        url: "SelectSHXX",
        type: "post",
        data: { SHID: SHID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {

                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ArrivalDate' + rowCount + '">' + json[i].ArrivalDate + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " >收货</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="SHID' + rowCount + '">' + json[i].SHID + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="OrderContent' + rowCount + '" ></lable>' + json[i].OrderContent + ' </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' id="Specifications' + rowCount + '" "></lable>' + json[i].Specifications + ' </td>';
       
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Unit' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    html += '</tr>'
                    Amounts += parseInt(json[i].Amount);
                    jine += parseInt(json[i].TotalNoTax);
                    $("#GXInfo").append(html);
                }
                $.ajax({
                    url: "SelectTHXQ",
                    type: "post",
                    data: { THID: THID },
                    dataType: "json",
                    success: function (data) {
                        var json = eval(data.datas);
                        if (json.length > 0) {

                            for (var i = 0; i < json.length; i++) {
                                rowCount = document.getElementById("GXInfo").rows.length;
                                var CountRows = parseInt(rowCount) + 1;
                                var html = "";
                                html = '<tr>'
                                html += '<td ><lable class="labProductID' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                                html += '<td ><lable class="labProductID' + rowCount + ' " id="ReturnDate' + rowCount + '">' + json[i].ReturnDate + '</lable> </td>';
                                html += '<td ><lable class="labOrderContent' + rowCount + ' " >退货</lable> </td>';
                                html += '<td ><lable class="labOrderContent' + rowCount + ' " id="THID' + rowCount + '">' + json[i].THID + '</lable> </td>';
                                html += '<td ><lable class="labSpecifications' + rowCount + ' " id="OrderContent' + rowCount + '" ></lable>' + json[i].OrderContent + ' </td>';
                                html += '<td ><lable class="labSpecifications' + rowCount + ' id="Specifications' + rowCount + '" "></lable>' + json[i].Specifications + ' </td>';
                                html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Unit' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                                html += '<td ><lable class="labSpecifications' + rowCount + ' " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                                html += '<td ><lable class="labRowNumber' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                                html += '<td ><lable class="labSpecifications' + rowCount + ' " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                                html += '</tr>'
                                Amounts += parseInt(json[i].Amount);
                                jine += parseInt(json[i].TotalNoTax);
                                $("#GXInfo1").append(html);


                            }

                            var html = "";
                        
                            html = '<tr>'
                            html += '<td ><lable class="labProductID' + rowCount + ' " id="Supplier' + rowCount + '">统计</lable> </td>';
                            html += '<td ><lable class="labProductID' + rowCount + ' " id="ReturnDate' + rowCount + '"></lable> </td>';
                            html += '<td ><lable class="labOrderContent' + rowCount + ' " ></lable> </td>';
                            html += '<td ><lable class="labOrderContent' + rowCount + ' " id="THID' + rowCount + '"></lable> </td>';
                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="OrderContent' + rowCount + '" ></lable></td>';
                            html += '<td ><lable class="labSpecifications' + rowCount + ' id="Specifications' + rowCount + '" "></lable></td>';
                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Unit' + rowCount + '"></lable> </td>';
                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="UnitPriceNoTax' + rowCount + '"></lable> </td>';
                            html += '<td ><lable class="labRowNumber' + rowCount + ' " id="Amount' + rowCount + '">' + Amounts + '</lable> </td>';
                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="TotalNoTax' + rowCount + '">' + jine + '</lable> </td>';
                            html += '</tr>'
                   
                      
                            $("#GXInfo2").append(html);
                        }
                    }
                });

            }
        }
    });
});