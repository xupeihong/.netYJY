var array = new Array();
var chengpinid = new Array();
$(document).ready(function () {
    if (location.search != "") {
        DDID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "SelectGoodsDDID",
        type: "post",
        data: { DDID: DDID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    //rowCount = document.getElementById("GXInfo").rows.length;
                    //var CountRows = parseInt(rowCount) + 1;
                    //var html = "";
                    //html = '<tr>'
                    //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    //html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';
                    //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    //html += '</tr>'
                    //$("#GXInfo").append(html);
                    array.push(json[i].COMNameC);
                }
                document.getElementById("DDID").innerHTML = location.search.split('&')[0].split('=')[1];
                document.getElementById("OrderDate").innerHTML = json[0].OrderDate;
                document.getElementById("xzrwlx").innerHTML = json[0].BusinessTypess;
                document.getElementById("TaskNum").innerHTML = json[0].PID;
                document.getElementById("Begin").innerHTML = json[0].DeliveryLimit;
                document.getElementById("OrderContacts").innerHTML = json[0].OrderContacts;
                document.getElementById("DeliveryMethod").innerHTML = json[0].JHFS;
                document.getElementById("IsInvoice").innerHTML = json[0].FP;
                document.getElementById("PaymentMethod").innerHTML = json[0].ZFFS;
                document.getElementById("PaymentAgreement").innerHTML = json[0].FKYD;
                document.getElementById("ContractNO").innerHTML = json[0].ContractNO;
                document.getElementById("TheProject").innerHTML = json[0].TheProject;
            }

        }

    });

    $.ajax({
        url: "SelectCP",
        type: "post",
        data: { DDID: DDID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo1").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="pid' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].Name + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spc + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].Num + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo1").append(html);
                    chengpinid.push(json[i].PID + "$" + json[i].Num);
                }

            }

            unique(array);
        }
    });
});

function unique(arr) {

    var result = new Array();
    for (var i = 0; i < arr.length; i++) {
        isRepeated = false;
        for (var j = i + 1; j < arr.length; j++) {
            if (arr[i] == arr[j]) {
                isRepeated = true;
                break;
            }
        }
        if (!isRepeated) {
            result.push(arr[i]);
        }
    }
    $('#supplier').empty();
    $("#bor1").empty();
    for (var i = 0; i < result.length; i++) {
        $("#lingjian").css("display", "")

        if (i == 0) {
            $("#supplier").append(" <input  id='supplier" + i + "'   class='btnTw' type='button' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "'style='overflow-y: scroll; border: 1px solid #707070;    margin-left: 10px; height: 200px'> <table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th><th class='th'> 成品ID </th> <th class='th'> 零件ID </th>  <th class='th'> 零件名称 </th><th class='th'> 规格型号 </th><th class='th'> 数量 </th> <th class='th'> 单位 </th> <th class='th'> 单价 </th> <th class='th'> 总价 </th> <th class='th'> 税前单价 </th> <th class='th'> 税前总价 </th>  <tbody id='GXInfosupplier0'></tbody></table></div>");
        }
        else {
            $("#supplier").append(" <input  id='supplier" + i + "' class='btnTh' type='button' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "' style='overflow-y: scroll; border: 1px solid #707070;   display:none; margin-left: 10px; height: 200px'><table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th><th class='th'> 成品ID </th> <th class='th'> 零件ID </th> <th class='th'> 成品名称 </th><th class='th'> 规格型号 </th><th class='th'> 数量 </th>  <th class='th'> 单位 </th> <th class='th'> 单价 </th> <th class='th'> 总价 </th> <th class='th'> 税前单价 </th> <th class='th'> 税前总价 </th>  <tbody id='GXInfosupplier" + i + "'></tbody></table> </div>");

        }
    }
    Supplierss($("#supplier0").val());
}
function Suppliers(rowid, lenght) {

    var id = rowid.id;
    var text = $("#" + id + "").val();
    var tbody = "";
    for (var i = 0; i < lenght; i++) {
        if (id == "supplier" + i) {
            $("#supplier" + i).attr("class", "btnTw");
            $("#lingjian" + i).css("display", "");
            tbody = "GXInfosupplier" + i;
        }
        else {
            $("#supplier" + i).attr("class", "btnTh");
            $("#lingjian" + i).css("display", "none");

        }
    }


    $("#" + tbody + "").empty();
    for (var i = 0; i < chengpinid.length; i++) {

        var numberss = new Array();
        numberss = chengpinid[i].split('$');
        $.ajax({
            url: "SelectLingJXQ",
            type: "post",
            async: false,
            data: { cppid: numberss[0], text: text },
            dataType: "Json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("" + tbody + "").rows.length;
                        var CountRows = parseInt(rowCount) + 1;

                        var html = "";
                        html += "<tr>";
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="CPID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="pid' + rowCount + '">' + json[i].PID + '</lable> </td>';
                  
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="names' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="spes' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="nums' + rowCount + '">' + json[i].Number * numberss[1] + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';

                        html += '<td ><lable class="labProductID' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                        var a = json[i].UnitPrice * (json[i].Number * numberss[1]);
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ZJ' + rowCount + '">' + a.toFixed(2) + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Price2' + rowCount + '">' + json[i].Price2 + '</lable> </td>';
                        var b = json[i].Price2 * (json[i].Number * numberss[1]);
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ZJ2' + rowCount + '">' + b.toFixed(2) + '</lable> </td>';
                        html += '</tr>'

                        $("#" + tbody + "").append(html);
                    }
                }
                else {
                    return;
                }
            }
        });

    }




}
function Supplierss(text) {

    var tbody = "";
    tbody = "GXInfosupplier" + 0;
    $("#" + tbody + "").empty();
    $("#supplier" + 0).attr("class", "btnTw");
    $("#lingjian" + 0).css("display", "");

    for (var i = 0; i < chengpinid.length; i++) {
        var numberss = new Array();
        numberss = chengpinid[i].split('$');
        $.ajax({
            url: "SelectLingJXQ",
            type: "post",
            async: false,
            data: { cppid: numberss[0], text: text },
            dataType: "Json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("" + tbody + "").rows.length;
                        var CountRows = parseInt(rowCount) + 1;

                        var html = "";
                        html += "<tr>";
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';

                        html += '<td ><lable class="labProductID' + rowCount + ' " id="CPID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="pid' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="names' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="spes' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="nums' + rowCount + '">' + json[i].Number * numberss[1] + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';

                        html += '<td ><lable class="labProductID' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                        var a = json[i].UnitPrice * (json[i].Number * numberss[1]);
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ZJ' + rowCount + '">' + a.toFixed(2) + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Price2' + rowCount + '">' + json[i].Price2 + '</lable> </td>';
                        var b = json[i].Price2 * (json[i].Number * numberss[1]);
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ZJ2' + rowCount + '">' + b.toFixed(2) + '</lable> </td>';
                        html += '</tr>'

                        $("#" + tbody + "").append(html);
                    }
                }
                else {
                    return;
                }
            }
        });

    }




}