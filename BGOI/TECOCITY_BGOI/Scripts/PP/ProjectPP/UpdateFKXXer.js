var RowId = 0;
var chengpinid = new Array();
var array = new Array();
var numonesupplier = "";
var strFFFS;
var strFKZT;
var tbody = "";

$(document).ready(function () {
    var payid = location.search.split('&')[0].split('=')[1];
    var ddid = location.search.split('&')[1].split('=')[1];
    if (location.search != "") {
        addCP(payid)
    }

    $("#CPSubmit").click(function () {
        chengpinid.splice(0, chengpinid.length);
        array.splice(0, array.length);
        $("#GXInfo1").empty();
        var tbody = document.getElementById('GXInfo');

        for (var i = 0; i < tbody.rows.length; i++) {
            var chk = document.getElementById('CPselect' + i);
            var CPID = document.getElementById("CPPid" + i).innerHTML;
            var NUM = $("#FKnums" + i).val();
            var Numsss = document.getElementById("Numsss" + i).value;
            if (chk.checked) {
                chengpinid.push(CPID + "$" + NUM);
                GetLJ(CPID, NUM);
            }
        }
    });

    $.ajax({
        url: "GetPipeSize",
        type: "post",
        data: { where: "and [type]='支付方式'" },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strFFFS = "<select id='PaymentMenthod' style='width:90px;'>";
                for (var i = 0; i < sel.length; i++) {
                    strFFFS += "<option value=" + sel[i] + ">" + val[i] + "</option>";
                }
                strFFFS += "</select>";
            }
            else {
                document.getElementById("PaymentMenthod").options.length = 0;
                return;
            }
        }
    });

    $.ajax({
        url: "GetPipeSize",
        type: "post",
        data: { where: "and [type]='付款状态'" },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strFKZT = "<select id='State' style='width:90px;'>";
                for (var i = 0; i < sel.length; i++) {
                    strFKZT += "<option value=" + sel[i] + ">" + val[i] + "</option>";
                }
                strFKZT += "</select>";
            }
            else {
                document.getElementById("State").options.length = 0;
                return;
            }
        }
    });

});





function addCP(payid) {
    $.ajax({
        url: "SelectFKCP",
        type: "post",
        data: { PayID: payid },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    //
                    html += '<td ><input name="CP" type="checkbox" id="CPselect' + rowCount + '" /></lable> </td>';
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumberss' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td  ><lable  class="labOrderContent' + rowCount + ' " id="CPPid' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td style="display:none"><lable  class="labOrderContent' + rowCount + ' " id="CPid' + rowCount + '">' + json[i].ID + '</lable> </td>';
                    html += '<td style="display:none"><lable  class="labOrderContent' + rowCount + ' " id="CPPIDS' + rowCount + '">' + json[i].CPPID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Namesss' + rowCount + '">' + json[i].Name + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spesss' + rowCount + '">' + json[i].Spc + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Numsss' + rowCount + '">' + json[i].Num + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Unitsss' + rowCount + '">' + json[i].Units + '</lable>  </td>';

                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="UnitPricess' + rowCount + '">' + json[i].UnitPrice + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Price2ss' + rowCount + '">' + json[i].Price2 + '</lable>  </td>';
                    html += '<td ><input type="text" id="FKnums' + rowCount + '" value="' + json[i].FKnum + '"  style="width:150px;"/></td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
            }
        }
    });
}











function GetLJ(CPID, num) {
    var fkid = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "SelectFKSupplier",
        type: "post",
        data: { cpid: CPID, fkid: fkid },
        dataType: "Json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo1").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html += "<tr>";
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo1").append(html);
                    array.push(json[i].COMNameC);

                }
            }
            else {
                return;
            }
            unique(array);
        }
    });
}

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
            $("#supplier").append(" <input  id='supplierss" + i + "'   class='btnTw' type='button' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "'style='overflow-y: scroll; border: 1px solid #707070;    margin-left: 10px; height: 200px'> <table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th><th class='th'> 成品ID </th> <th class='th'> 零件ID </th><th class='th'> 零件名称 </th> <th class='th'> 规格型号 </th><th class='th'> 数量 </th> <th class='th'> 已付款数量 </th> <th class='th'> 本次付款数量 </th> <th class='th'> 单位 </th><th class='th'> 税前单价 </th> <th class='th'> 税前总价 </th> <th class='th'> 税后单价 </th> <th class='th'> 税后总价 </th> <th class='th'> 操作 </th>   <tbody id='GXInfosupplier" + i + "'></tbody></table></div>");
        }
        else {
            $("#supplier").append(" <input  id='supplierss" + i + "' class='btnTh' type='button' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "' style='overflow-y: scroll; border: 1px solid #707070;   display:none; margin-left: 10px; height: 200px'><table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th><th class='th'> 成品ID </th><th class='th'> 零件ID </th> <th class='th'> 零件名称 </th> <th class='th'> 规格型号 </th><th class='th'> 数量 </th> <th class='th'> 供货商 </th> <th class='th'> 单位 </th> <th class='th'> 税前单价 </th> <th class='th'> 税前总价 </th> <th class='th'> 税后单价 </th> <th class='th'> 税后总价 </th><th class='th'> 操作 </th>   <tbody id='GXInfosupplier" + i + "'></tbody></table> </div>");

        }
    }
    Supplierss($("#supplierss0").val());
}
function Suppliers(rowid, lenght) {
    var fkid = location.search.split('&')[0].split('=')[1];
    var zongjia = 0;
    var zongjiano = 0;
    var id = rowid.id;
    $("#" + tbody + "").empty();
    var text = $("#" + id + "").val();
    //var tbody = "";
    for (var i = 0; i < lenght; i++) {
        if (id == "supplierss" + i) {
            $("#supplierss" + i).attr("class", "btnTw");
            $("#lingjian" + i).css("display", "");
            tbody = "GXInfosupplier" + i;
        }
        else {
            $("#supplierss" + i).attr("class", "btnTh");
            $("#lingjian" + i).css("display", "none");
            $("#GXInfosupplier" + i).empty();
        }
    }


    $("#" + tbody + "").empty();
    for (var i = 0; i < chengpinid.length; i++) {

        var numberss = new Array();
        numberss = chengpinid[i].split('$');
        $.ajax({
            url: "SelectFKSupplier",
            type: "post",
            async: false,
            data: { cpid: numberss[0], text: text, fkid: fkid },
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
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="ID' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="names' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="spes' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="nums' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labOrderContent' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                        html += '<td style="display:none " ><lable class="labOrderContent' + rowCount + ' " id="LJManufacturer' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td  style="display:none " ><lable class="labProductID' + rowCount + ' " id="LJZQnums' + rowCount + '">' + json[i].Rate + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJYFnums' + rowCount + '">' + json[i].DDActualAmount + '</lable> </td>';
                        //html += '<td ><lable class="labProductID' + rowCount + ' " id="LJBCnums' + rowCount + '">' + json[i].Number * numberss[1] + '</lable> </td>';
                        html += '<td ><input type="text" id="LJBCnums' + rowCount + '"  onblur="OnBlurAmount(this);" value="' + json[i].Number * numberss[1] + '"  style="width:150px;"/></td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                        var a = json[i].UnitPrice * (json[i].Number * numberss[1]);
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ZJ' + rowCount + '">' + a.toFixed(2) + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Price2' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                        var b = json[i].UnitPriceNoTax * (json[i].Number * numberss[1]);
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ZJ2' + rowCount + '">' + b.toFixed(2) + '</lable> </td>';
                        html += '<td ></td>';
                        html += '</tr>'
                        zongjia += json[i].UnitPrice * json[i].Number * numberss[1];
                        zongjiano += json[i].UnitPriceNoTax * json[i].Number * numberss[1];
                        $("#" + tbody + "").append(html);
                    }

                }
                else {
                    return;
                }
            }
        });

    }

    var htmls = "";
    htmls += "<tr>"
    htmls += '<td > </td>';
    htmls += '<td > </td>';
    htmls += '<td > 付费时间 </td>';
    htmls += '<td > <input type="text" id="PayTime" onclick="WdatePicker()" class="Wdate" style="width:170px;" /><span style="color: red;">*</span> </td>';
    htmls += '<td > 付费方式 </td>';
    htmls += '<td > ' + strFFFS + ' </td>';
    htmls += '<td > 付费状态 </td>';
    htmls += '<td >' + strFKZT + ' </td>';
    htmls += '<td >  </td>';
    htmls += '<td id="zongjia">' + zongjia + ' </td>';
    htmls += '<td > </td>';
    htmls += '<td id="zongjiano"> ' + zongjiano + ' </td>';
    htmls += '<td >  <input  type="button" class="btn" onclick="ADDFK(' + tbody + ')"  value="保存" /> </td>';
    htmls += "</tr>";
    $("#" + tbody + "").append(htmls);


}

function Supplierss(text) {
    var fkid = location.search.split('&')[0].split('=')[1];
    var tbody = "";
    var zongjia = 0;
    var zongjiano = 0;
    //var tbody = "";
    $("#supplierss" + 0).attr("class", "btnTw");
    $("#lingjian" + 0).css("display", "");
    tbody = "GXInfosupplier" + 0;
    $("#" + tbody + "").empty();
    for (var i = 0; i < chengpinid.length; i++) {

        var numberss = new Array();
        numberss = chengpinid[i].split('$');
        $.ajax({
            url: "SelectFKSupplier",
            type: "post",
            async: false,
            data: { cpid: numberss[0], text: text, fkid: fkid },
            dataType: "Json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("" + tbody + "").rows.length;
                        var CountRows = parseInt(rowCount) + 1;

                        var html = "";
                        html += "<tr>";
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="LJRowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJID' + rowCount + '">' + json[i].INID + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="DDDID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJCPID' + rowCount + '">' + json[i].LJCPID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJNames' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJSpes' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJNums' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labOrderContent' + rowCount + ' " id="LJManufacturer' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                        html += '<td style="display:none " ><lable class="labOrderContent' + rowCount + ' " id="LJManufacturer' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td  style="display:none " ><lable class="labProductID' + rowCount + ' " id="LJZQnums' + rowCount + '">' + json[i].Rate + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJYFnums' + rowCount + '">' + json[i].SJFK + '</lable> </td>';
                        //html += '<td ><lable class="labProductID' + rowCount + ' " id="LJBCnums' + rowCount + '">' + json[i].Number * numberss[1] + '</lable> </td>';
                        html += '<td ><input type="text" id="LJBCnums' + rowCount + '"  onblur="OnBlurAmount(this);" value="' + json[i].Number * numberss[1] + '"  style="width:150px;"/></td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJUnits' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJUnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                        var a = json[i].UnitPrice * (json[i].Number * numberss[1]);
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJZJ' + rowCount + '">' + a.toFixed(2) + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJPrice2' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                        var b = json[i].UnitPriceNoTax * (json[i].Number * numberss[1]);
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJZJ2' + rowCount + '">' + b.toFixed(2) + '</lable> </td>';
                        html += '<td ></td>';
                        html += '</tr>'
                        zongjia += json[i].UnitPrice * json[i].Number * numberss[1];
                        zongjiano += json[i].UnitPriceNoTax * json[i].Number * numberss[1];
                        $("#" + tbody + "").append(html);
                    }

                }
                else {
                    return;
                }
            }
        });

    }

    var htmls = "";
    htmls += "<tr>"
    htmls += '<td > </td>';
    htmls += '<td > </td>';
    htmls += '<td > 付费时间 </td>';
    htmls += '<td > <input type="text" id="PayTime" onclick="WdatePicker()" class="Wdate" style="width:170px;" /><span style="color: red;">*</span> </td>';
    htmls += '<td > 付费方式 </td>';
    htmls += '<td > ' + strFFFS + ' </td>';
    htmls += '<td > 付费状态 </td>';
    htmls += '<td >' + strFKZT + ' </td>';
    htmls += '<td >  </td>';
    htmls += '<td >  </td>';
    htmls += '<td id="zongjia">' + zongjia + ' </td>';
    htmls += '<td > </td>';
    htmls += '<td id="zongjiano"> ' + zongjiano + ' </td>';
    htmls += '<td >  <input  type="button" class="btn" onclick="ADDFK(' + tbody + ')"  value="保存" /> </td>';
    htmls += "</tr>";
    $("#" + tbody + "").append(htmls);


}
function ADDFK(table) {
    //alert(table.id);



    var str = document.getElementById(table.id).innerHTML;
    if (str == "") {
        alert("请选择商品");
    }
    else {
        if (location.search.split('&')[0].split('=')[0] == "?texts") {
            var texts = location.search.split('&')[0].split('=')[1];
            texts = texts.substr(0, texts.length - 1);
            var mycars = new Array()
            mycars = texts.split(',');

            var list = new Array();
            list = mycars[0].split('-');
            var CID = list[0] + "-" + list[1];
        }




        var Rownumberss = "";
        var CPpid = "";
        var CPid = "";
        var CPpids = "";
        var Namesss = "";
        var Spesss = "";
        var Numsss = "";
        var Unitsss = "";
        var Unitpricess = "";
        var Price2ss = "";
        var Fknums = "";

        var tbodys = document.getElementById("GXInfo");
        for (var i = 0; i < tbodys.rows.length; i++) {
            var RowNumberss = document.getElementById("RowNumberss" + i).innerHTML;
            var CPPid = document.getElementById("CPPid" + i).innerHTML;
            var CPID = document.getElementById("CPid" + i).innerHTML;
            var CPPIDS = document.getElementById("CPPIDS" + i).innerHTML;
            var NAmesss = document.getElementById("Namesss" + i).innerHTML;
            var SPesss = document.getElementById("Spesss" + i).innerHTML;
            var NUmsss = document.getElementById("Numsss" + i).innerHTML;
            var UNitsss = document.getElementById("Unitsss" + i).innerHTML;
            var UnitPricess = document.getElementById("UnitPricess" + i).innerHTML;
            var PRice2ss = document.getElementById("Price2ss" + i).innerHTML;
            var chk = document.getElementById('CPselect' + i);
            if (chk.checked) {
                var FKnums = document.getElementById("FKnums" + i).value;
            }
            else {
                var FKnums = 0;
            }

            Rownumberss += RowNumberss;
            CPpid += CPPid;
            CPid += CPID;
            CPpids += CPPIDS;
            Namesss += NAmesss;
            Spesss += SPesss;
            Numsss += NUmsss;
            Unitsss += UNitsss;
            Unitpricess += UnitPricess;
            Price2ss += PRice2ss;
            Fknums += FKnums;
            if (i < tbodys.rows.length - 1) {
                Rownumberss += ",";
                CPpid += ",";
                CPid += ",";
                CPpids += ",";
                Namesss += ",";
                Spesss += ",";
                Numsss += ",";
                Unitsss += ",";
                Unitpricess += ",";
                Price2ss += ",";
                Fknums += ",";
            }
            else {
                Rownumberss += "";
                CPpid += "";
                CPid += "";
                CPpids += "";
                Namesss += "";
                Spesss += "";
                Numsss += "";
                Unitsss += "";
                Unitpricess += "";
                Price2ss += "";
                Fknums += "";
            }

        }
        var LJrownumber = "";
        var LJid = "";
        var DDdid = "";
        var LJCPid = "";
        var LJname = "";
        var LJspes = "";
        var LJnums = "";
        var LJmanufacturer = "";
        var LJsjfknum = "";
        var LJyfnums = "";
        var LJbcnums = "";
        var LJunits = "";
        var LJunitprice = "";
        var LJzj = "";

        var LJprice2 = "";
        var LJzj2 = "";


        var tbodyss = document.getElementById(table.id);
        for (var i = 0; i < tbodyss.rows.length - 1 ; i++) {

            var LJRowNumber = document.getElementById("LJRowNumber" + i).innerHTML;
            var LJID = document.getElementById("LJID" + i).innerHTML;
            var DDDID = document.getElementById("DDDID" + i).innerHTML;
            var LJCPID = document.getElementById("LJCPID" + i).innerHTML;
            var LJNames = document.getElementById("LJNames" + i).innerHTML;
            var LJSpes = document.getElementById("LJSpes" + i).innerHTML;
            var LJNums = document.getElementById("LJNums" + i).innerHTML;
            var LJManufacturer = document.getElementById("LJManufacturer" + i).innerHTML;
            var LJZQnums = document.getElementById("LJZQnums" + i).innerHTML;
            var LJYFnums = document.getElementById("LJYFnums" + i).innerHTML;
            var LJBCnums = document.getElementById("LJBCnums" + i).value;
            var LJUnits = document.getElementById("LJUnits" + i).innerHTML;
            var LJUnitPrice = document.getElementById("LJUnitPrice" + i).innerHTML;
            var LJZJ = document.getElementById("LJZJ" + i).innerHTML;
            var LJPrice2 = document.getElementById("LJPrice2" + i).innerHTML;
            var LJZJ2 = document.getElementById("LJZJ2" + i).innerHTML;
            var SJFKnum = parseInt(LJYFnums) - (parseInt(LJZQnums) - parseInt(LJBCnums));

            if (SJFKnum > LJNums) {
                alert("付款零件数量大于总数量！！！");
                return;
            }

            LJrownumber += LJRowNumber;
            LJid += LJID;
            DDdid += DDDID;
            LJCPid += LJCPID;
            LJname += LJNames;
            LJspes += LJSpes;
            LJnums += LJNums;
            LJmanufacturer += LJManufacturer;
            LJsjfknum += SJFKnum;
            LJyfnums += LJYFnums;
            LJbcnums += LJBCnums;
            LJunits += LJUnits;
            LJunitprice += LJUnitPrice;
            LJzj += LJZJ;
            LJprice2 += LJPrice2;
            LJzj2 += LJZJ2;
            if (i < tbodyss.rows.length - 1 - 1) {

                LJrownumber += ",";
                LJid += ",";
                DDdid += ",";
                LJCPid += ",";
                LJname += ",";
                LJspes += ",";
                LJnums += ",";
                LJmanufacturer += ",";
                LJsjfknum += ",";
                LJyfnums += ",";
                LJbcnums += ",";
                LJunits += ",";
                LJunitprice += ",";
                LJzj += ",";
                LJprice2 += ",";
                LJzj2 += ",";
            }
            else {
                LJrownumber += "";
                LJid += "";
                DDdid += "";
                LJCPid += "";
                LJname += "";
                LJspes += "";
                LJnums += "";
                LJmanufacturer += "";
                LJsjfknum += "";
                LJyfnums += "";
                LJbcnums += "";
                LJunits += "";
                LJunitprice += "";
                LJzj += "";
                LJprice2 += "";
                LJzj2 += "";
            }
        }
        var zongjia = document.getElementById('zongjia').innerHTML;


        var zongjiano = document.getElementById('zongjiano').innerHTML;


        var PayId = location.search.split('&')[0].split('=')[1];

        //请购人
        var OrderContacts = $("#OrderContacts").val();

        var DDID = location.search.split('&')[1].split('=')[1];
        //付费时间
        var PayTime = $("#PayTime").val();
        if (PayTime == "") {
            alert("付费时间不可为空");
            return;
        }
        //支付方式
        var PaymentMenthod = $("#PaymentMenthod").val();
        if (PaymentMenthod == "") {
            alert("付费方式不可为空");
            return;
        }
        //付费状态
        var State = $("#State").val();
        if (State == "") {
            alert("付费状态不可为空");
            return;
        }

        isConfirm = confirm("确定要付款吗")
        if (isConfirm == false) {
            return false;
        }
        else {

            $.ajax({
                url: "UpdateFKCP",
                type: "Post",
                data: {
                    rownumberss: Rownumberss, cppid: CPpid, cpid: CPid, cpppids: CPpids, namesss: Namesss, spesss: Spesss, numsss: Numsss, unitsss: Unitsss, unitpricess: Unitpricess, price2ss: Price2ss, fknums: Fknums,

                    ljrownumber: LJrownumber, ljid: LJid, dddid: DDdid, ljname: LJname, ljspes: LJspes, ljnums: LJnums, ljmanufacturer: LJmanufacturer, ljsjfknum: LJsjfknum, ljyfnums: LJyfnums, ljbcnums: LJbcnums, ljunits: LJunits, ljunitprice: LJunitprice, ljzj: LJzj, ljprice2: LJprice2, ljzj2: LJzj2, ljcpid: LJCPid,

                    ordercontacts: OrderContacts, ddid: DDID, payid: PayId, paytime: PayTime, paymentmenthod: PaymentMenthod, state: State
                },
                async: false,
                success: function (data) {

                    if (data.success == true) {
                        window.parent.frames["iframeRight"].reload1();
                        alert("成功");
                        setTimeout('parent.ClosePop()', 100);
                    }
                    else {
                        alert("失败");
                    }
                }
            });
        }
    }
}
//function ADDFKS(table) {
//    //alert(table.id);
//    var str = document.getElementById(table.id).innerHTML;
//    if (str == "") {
//        alert("请选择商品");
//    }
//    else {
//        if (location.search.split('&')[0].split('=')[0] == "?texts") {
//            var texts = location.search.split('&')[0].split('=')[1];
//            texts = texts.substr(0, texts.length - 1);
//            var mycars = new Array()
//            mycars = texts.split(',');

//            var list = new Array();
//            list = mycars[0].split('-');
//            var CID = list[0] + "-" + list[1];
//        }

//        var PayId = location.search.split('&')[0].split('=')[1];

//        //请购人
//        var OrderContacts = $("#OrderContacts").val();

//        var DDID = location.search.split('&')[1].split('=')[1];


//        var rownumber = "";
//        var id = "";
//        var name = "";
//        var spec = "";
//        var nums = "";
//        var units = "";
//        var unitprice = "";
//        var zj = "";
//        var price2 = "";
//        var zj2 = "";
//        var manufacturer = "";

//        var tbodys = document.getElementById(table.id);
//        for (var i = 0; i < tbodys.rows.length - 1; i++) {
//            var RowNumber = document.getElementById("RowNumberss" + i).innerHTML;
//            var ID = document.getElementById("IDs" + i).innerHTML;
//            var Name = document.getElementById("namess" + i).innerHTML;
//            var Spes = document.getElementById("spess" + i).innerHTML;
//            var Nums = document.getElementById("numss" + i).innerHTML;
//            var Units = document.getElementById("Unitss" + i).innerHTML;
//            var UnitPrice = document.getElementById("UnitPrices" + i).innerHTML;
//            var ZJ = document.getElementById("ZJs" + i).innerHTML;
//            var Price2 = document.getElementById("Price2s" + i).innerHTML;
//            var ZJ2 = document.getElementById("ZJ2s" + i).innerHTML;
//            var Manufacturer = document.getElementById("Manufacturers" + i).innerHTML;

//            rownumber += RowNumber;
//            id += ID;
//            name += Name;
//            spec += Spes;
//            nums += Nums;
//            units += Units;
//            unitprice += UnitPrice;
//            zj += ZJ;
//            price2 += Price2;
//            zj2 += ZJ2;
//            manufacturer += Manufacturer
//            if (i < tbodys.rows.length - 1 - 1) {
//                rownumber += ",";
//                id += ",";
//                name += ",";
//                spec += ",";
//                nums += ",";
//                units += ",";
//                unitprice += ",";
//                zj += ",";
//                price2 += ",";
//                zj2 += ",";
//                manufacturer += ",";
//            }
//            else {
//                rownumber += " ";
//                id += " ";
//                name += "";
//                spec += " ";
//                nums += "";
//                units += " ";
//                unitprice += " ";
//                zj += " ";
//                price2 += " ";
//                zj2 += "";
//                manufacturer += "";
//            }

//        }
//        var CPID = "";
//        var CPPid = "";
//        var CPPids="";
//        var FKnum = "";
//        var FKNums = "";
//        var CPRowNumberss = "";
//        var CPNamesss = "";
//        var CPSpesss = "";
//        var CPNumsss = "";
//        var CPUnitsss = "";
//        var CPUnitPricess = "";
//        var CPPrice2ss = "";

//        var tbodyss = document.getElementById("GXInfo");
//        var aa = tbodyss.rows.length;
//        for (var i = 0; i < tbodyss.rows.length ; i++) {

//            var CPid = document.getElementById("CPid" + i).innerHTML;
//            var CPPID = document.getElementById("CPPid" + i).innerHTML;
//            var CPPIDS = document.getElementById("CPPIDS" + i).innerHTML;
//            var CPrownumberss = document.getElementById("RowNumberss" + i).innerHTML;
//            var CPnamesss = document.getElementById("Namesss" + i).innerHTML;
//            var CPspesss = document.getElementById("Spesss" + i).innerHTML;
//            var CPnumsss = document.getElementById("Numsss" + i).innerHTML;
//            var CPunitsss = document.getElementById("Unitsss" + i).innerHTML;
//            var CPunitPricess = document.getElementById("UnitPricess" + i).innerHTML;
//            var CPprice2ss = document.getElementById("Price2ss" + i).innerHTML;
//            var chk = document.getElementById('CPselect' + i);
//            if (chk.checked) {
//                var FKnums = document.getElementById("FKnums" + i).value;
//            }
//            else {
//                var FKnums = 0;
//            }
//            var YFKnum = document.getElementById("YFKnum" + i).innerHTML;
//            var ZQFKnum = document.getElementById("ZQFKnum" + i).innerHTML;
//            var SJFKnum = parseInt(YFKnum) - (parseInt(ZQFKnum) - parseInt(FKnums));
//            CPID += CPid;
//            FKnum += SJFKnum;
//            CPPid += CPPID;
//            CPPids += CPPIDS;
//            CPRowNumberss += CPrownumberss;
//            CPNamesss += CPnamesss;
//            CPSpesss += CPspesss;
//            CPNumsss += CPnumsss;
//            CPUnitsss += CPunitsss;
//            CPUnitPricess += CPunitPricess;
//            CPPrice2ss += CPprice2ss;
//            FKNums += FKnums;
//            if (i < tbodyss.rows.length - 1) {

//                CPID += ",";
//                FKnum += ",";
//                CPPid += ",";
//                CPPids += ",";
//                CPRowNumberss += ",";
//                CPNamesss += ",";
//                CPSpesss += ",";
//                CPNumsss += ",";
//                CPUnitsss += ",";
//                CPUnitPricess += ",";
//                CPPrice2ss += ",";
//                FKNums += ",";





//            }
//            else {
//                CPID += "";
//                FKnum += "";
//                CPPid += "";
//                CPPids += "";
//                CPRowNumberss += "";
//                CPNamesss += "";
//                CPSpesss += "";
//                CPNumsss += "";
//                CPUnitsss += "";
//                CPUnitPricess += "";
//                CPPrice2ss += "";
//                FKNums += "";
//            }

//        }


//        var zongjia = document.getElementById('zongjias').innerHTML;


//        var zongjiano = document.getElementById('zongjianos').innerHTML;


//        //付费时间
//        var PayTime = $("#PayTimes").val();
//        if (PayTime == "") {
//            alert("付费时间不可为空");
//            return;
//        }
//        //支付方式
//        var PaymentMenthod = $("#PaymentMenthod").val();
//        if (PaymentMenthod == "") {
//            alert("付费方式不可为空");
//            return;
//        }
//        //付费状态
//        var State = $("#State").val();
//        if (State == "") {
//            alert("付费状态不可为空");
//            return;
//        }

//        isConfirm = confirm("确定要付款吗")
//        if (isConfirm == false) {
//            return false;
//        }
//        else {
//            $.ajax({
//                url: "UpdateFKCP",
//                type: "Post",
//                data: {
//                    rownumber: rownumber, id: id, spec: spec, nums: nums, name: name, units: units, unitprice: unitprice, zj: zj, price2: price2, zj2: zj2, zongjia: zongjia, zongjiano: zongjiano, paytime: PayTime, paymentmenthod: PaymentMenthod, state: State, ordercontacts: OrderContacts, payid: PayId, manufacturer: manufacturer, ddid: DDID, cpid: CPID, fknum: FKnum, cprownumberss: CPRowNumberss, cpnamesss: CPNamesss, cpspesss: CPSpesss, cpnumsss: CPNumsss, cpunitsss: CPUnitsss, cpunitpricess: CPUnitPricess, cpprice2ss: CPPrice2ss, cppid: CPPid, fknums: FKNums,cppids:CPPids
//                },
//                async: false,
//                success: function (data) {

//                    if (data.success == true) {
//                        window.parent.frames["iframeRight"].reload1();
//                        alert("成功");
//                        setTimeout('parent.ClosePop()', 100);
//                    }
//                    else {
//                        alert("失败");
//                    }
//                }
//            });
//        }
//    }
//}

function OnBlurAmount(rowcount) //求金额和
{

    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    var value = newCount.replace(/[^0-9]/ig, "");
    var strRow = newCount.charAt(newCount.length - 1);

    var strU = document.getElementById("LJUnitPrice" + value).innerText;
    //var strUnitPrice = $(strU).val();

    var strP = document.getElementById("LJPrice2" + value).innerText;
    //var strPrice2 = $(strP).val();

    var strTotal = parseFloat(Count) * parseFloat(strU);
    var TotalTax = parseFloat(Count) * parseFloat(strP);

    document.getElementById("LJZJ" + value).innerHTML = strTotal;
    document.getElementById("LJZJ2" + value).innerHTML = TotalTax;

}



