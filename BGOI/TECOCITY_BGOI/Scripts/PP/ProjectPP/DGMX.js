$(document).ready(function () {
    var Supplier = "";
    //var table = "PurchaseOrder";
    //var lie = "OrderDate";
    //var type = "MAX";
    //$.ajax({
    //    url: "GetDataTime",
    //    type: "post",
    //    data: { table: table, lie: lie, type: type },
    //    dataType: "json",
    //    success: function (data) {
    //        var json = eval(data.datas);
    //        if (json.length > 0) {
    //            document.getElementById("Begin").innerHTML = json[0].OrderDate;
    //            var type = "min";
    //            $.ajax({
    //                url: "GetDataTime",
    //                type: "post",
    //                data: { table: table, lie: lie, type: type },
    //                dataType: "json",
    //                success: function (data) {
    //                    var json = eval(data.datas);
    //                    if (json.length > 0) {
    //                        document.getElementById("End").innerHTML = json[0].OrderDate;
    //                    }
    //                }
    //            });
    //        }
    //    }
    //});

    $("#DY").click(function () {

        var texts = $('#Supplier').val();
            var url = "PrintSupplierTJ?id=" + escape(texts);
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });

    $("#btn").click(function () {
        $("#GXInfo").html("");
        var Supplier = $('#Supplier').val();
        $.ajax({
            url: "SelectGoodsDDID1",
            type: "post",
            data: { Supplier: Supplier },
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
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' "  id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="RKjine' + rowCount + '">' + json[i].RKjine + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="SJFK' + rowCount + '">' + json[i].SJFK + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="NoFK' + rowCount + '" >' + json[i].NoFK + '</lable> </td>';
                        html += '</tr>'
                        $("#GXInfo").append(html);
                    }

                }

            }
        });

    });
    $.ajax({
        url: "SelectGoodsDDID1",
        type: "post",
        data: { Supplier: Supplier },
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
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' "  id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="RKjine' + rowCount + '">' + json[i].RKjine + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="SJFK' + rowCount + '">' + json[i].SJFK + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="NoFK' + rowCount + '" >' + json[i].NoFK + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }

            }
            //var html = "";
            //html = '<tr>'
            //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">总计</lable> </td>';
            //html += '<td ><lable class="labProductID' + rowCount + ' " id="OrderDate' + rowCount + '"></lable> </td>';
            //html += '<td ><lable class="labProductID' + rowCount + ' " id="DDID' + rowCount + '"></lable> </td>';
            //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '"></lable> </td>';
            //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="TheProject' + rowCount + '"></lable> </td>';
            //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="' + rowCount + '"></lable> </td>';
            //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="MaterialNO' + rowCount + '"></lable> </td>';
            //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="OrderContent' + rowCount + '"></lable> </td>';
            //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Specifications' + rowCount + '"></lable> </td>';
            //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Unit' + rowCount + '"></lable> </td>';
            //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="UnitPriceNoTax' + rowCount + '"></lable> </td>';
            //html += '<td ><lable class="labRowNumber' + rowCount + ' " style="color:red" id="Amount' + rowCount + '">' + Amounts + '</lable> </td>';
            //html += '<td ><lable class="labSpecifications' + rowCount + ' " style="color:red"  id="TotalNoTax' + rowCount + '">' + jine + '</lable> </td>';
            //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Remark' + rowCount + '"></lable> </td>';
            //html += '</tr>'
            //$("#GXInfo1").append(html);

            //document.getElementById("jine").innerHTML = jine;
        }

    });


    //$.ajax({
    //    url: "SelectCountDD",
    //    type: "post",
    //    data: {},
    //    dataType: "json",
    //    success: function (data) {
    //        var json = eval(data.datas);
    //        if (json.length > 0) {
    //            document.getElementById("Amount").innerHTML = json[0].Count;
    //        }
    //    }
    //});

});







