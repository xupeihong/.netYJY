
var RowId = 0;
var chengpinid = new Array();
var array = new Array();
var numonesupplier = "";
var strFFFS;
var strFKZT;
var tbody = "";

$(document).ready(function () {
    var ddid = location.search.split('&')[0].split('=')[1];
    if (location.search != "") {
        addCP(ddid)
    }
    $("#CPSubmit").click(function () {

        chengpinid.splice(0, chengpinid.length);
        array.splice(0, array.length);
        $("#GXInfo1").empty();
        var tbody = document.getElementById('GXInfo');
        for (var i = 0; i < tbody.rows.length; i++) {
            var chk = document.getElementById('CPselect' + i);
            if (chk.checked) {
                var CPSHnums = document.getElementById("CPSHnums" + i).value;
            }
            else {
                var CPSHnums = 0;
            }
            if (parseInt(CPSHnums) != CPSHnums) {
                alert("数量填写整数"); return;
            }
            var CPNum = document.getElementById("CPNum" + i).innerHTML;
            var CPYSHnum = document.getElementById("CPYSHnum" + i).innerHTML;
            var SJSHnum = parseInt(CPSHnums) + parseInt(CPYSHnum);
            if (SJSHnum > CPNum) {
                alert("收货数量超出范围！！！");
                return;
            }
        }
        for (var i = 0; i < tbody.rows.length; i++) {

            var chk = document.getElementById('CPselect' + i);
            var CPID = document.getElementById("CPPid" + i).innerHTML;
            var NUM = $("#CPSHnums" + i).val();

            if (chk.checked) {
                chengpinid.push(CPID + "$" + NUM);
                GetLJ(CPID, NUM);
            }
        }
    });

    $("#upLoad").click(function () {
        var PID = $("#SHID").val();
        //ShowIframe1("管理文件", "../PPManage/AddFile?PID=" + PID, 500, 300, '');
        ShowIframe1("管理文件", "../PPManage/InsertBiddingNew?PID=" + PID, 500, 300, '');
        
    });
});

function addCP(DDID) {
    $.ajax({
        url: "SelectCP",
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
                    //
                    html += '<td ><input name="CP" type="checkbox" id="CPselect' + rowCount + '" /></lable> </td>';
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="CPRowNumberss' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td  ><lable  class="labOrderContent' + rowCount + ' " id="CPPid' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td style="display:none"><lable  class="labOrderContent' + rowCount + ' " id="CPid' + rowCount + '">' + json[i].ID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPName' + rowCount + '">' + json[i].Name + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPSpe' + rowCount + '">' + json[i].Spc + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPNum' + rowCount + '">' + json[i].Num + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPUnits' + rowCount + '">' + json[i].Units + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPUnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPPrice2' + rowCount + '">' + json[i].Price2 + '</lable>  </td>';
                    html += '<td style="display:none"><lable class="labOrderContent' + rowCount + ' "   id="CPYSHnum' + rowCount + '">0</lable>  </td>';
                    html += '<td ><input type="text" id="CPSHnums' + rowCount + '" value="' + json[i].Num + '"  style="width:150px;"/></td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
            }
        }
    });
}
function GetLJ(CPID, num) {
    var ddid = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "SelectDDSupplier",
        type: "post",
        data: { cpid: CPID, ddid: ddid },
        dataType: "Json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo1").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html += "<tr>";
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJManufacturers' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
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
            $("#bor1").append("  <div id='lingjian" + i + "'style='overflow-y: scroll; border: 1px solid #707070;    margin-left: 10px; height: 200px'> <table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th><th class='th'> 成品ID </th> <th class='th'> 零件ID </th><th class='th'> 零件名称 </th> <th class='th'> 规格型号 </th><th class='th'> 总数量 </th> <th class='th'> 已收货数量 </th> <th class='th'> 本次收货数量 </th>  <th class='th'> 单位 </th><th class='th'> 税前单价 </th> <th class='th'> 税前总价 </th> <th class='th'> 税后单价 </th> <th class='th'> 税后总价 </th> <th class='th'> 操作 </th>   <tbody id='GXInfosupplier" + i + "'></tbody></table></div>");
        }
        else {
            $("#supplier").append(" <input  id='supplierss" + i + "' class='btnTh' type='button' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "' style='overflow-y: scroll; border: 1px solid #707070;   display:none; margin-left: 10px; height: 200px'><table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th><th class='th'> 成品ID </th><th class='th'> 零件ID </th> <th class='th'> 零件名称 </th> <th class='th'> 规格型号 </th><th class='th'> 总数量 </th> <th class='th'> 已收货数量 </th> <th class='th'> 本次收货数量 </th> <th class='th'> 单位 </th> <th class='th'> 税前单价 </th> <th class='th'> 税前总价 </th> <th class='th'> 税后单价 </th> <th class='th'> 税后总价 </th><th class='th'> 操作 </th>   <tbody id='GXInfosupplier" + i + "'></tbody></table> </div>");

        }
    }
    Supplierss($("#supplierss0").val());
}
function Suppliers(rowid, lenght) {
    var ddid = location.search.split('&')[0].split('=')[1];
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
            url: "SelectDDSupplier",
            type: "post",
            async: false,
            data: { cpid: numberss[0], text: text, ddid: ddid },
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
                        html += '<td  style="display:none"><lable class="labProductID' + rowCount + ' " id="DDDID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJCPID' + rowCount + '">' + numberss[0] + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJID' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJnames' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJspes' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJnums' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labOrderContent' + rowCount + ' " id="LJManufacturer' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                        html += '<td style="display:none" ><lable class="labOrderContent' + rowCount + ' " id="LJManufacturers' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';

                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJYSnums' + rowCount + '">' + json[i].ActualAmount + '</lable> </td>';
                        //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJBCnums' + rowCount + '">' + json[i].Number * numberss[1] + '</lable> </td>';
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
    htmls += "<tr>";
    htmls += "  <td>收货时间</td>";
    htmls += '<td> <input type="text" id="ArrivalDate" onclick="WdatePicker()" class="Wdate" style="width:160px;" /><span style="color: red;">*</span> </td>';
    htmls += '  <td>收货说明</td>';
    htmls += '<td colspan=10><input type="text" id="ArrivalDescription" style="width:590px;" /></td>';
    htmls += '<td >   <input   type="button" onclick=ADDFK(' + tbody + ') class="btn"  value="保存" /></td>';
    htmls += '</tr>';
    $("#" + tbody + "").append(htmls);
}

function Supplierss(text) {
    var ddid = location.search.split('&')[0].split('=')[1];
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
            url: "SelectDDSupplier",
            type: "post",
            async: false,
            data: { cpid: numberss[0], text: text, ddid: ddid },
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
                        html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="DDDID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJCPID' + rowCount + '">' + numberss[0] + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJID' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJnames' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJspes' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJnums' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labOrderContent' + rowCount + ' " id="LJManufacturer' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                        html += '<td style="display:none" ><lable class="labOrderContent' + rowCount + ' " id="LJManufacturers' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';

                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJYSnums' + rowCount + '">' + json[i].ActualAmount + '</lable> </td>';
                        //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJBCnums' + rowCount + '">' + json[i].Number * numberss[1] + '</lable> </td>';
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
    htmls += "<tr>";
    htmls += "  <td>收货时间</td>";
    htmls += '<td> <input type="text" id="ArrivalDate" onclick="WdatePicker()" class="Wdate" style="width:160px;" /><span style="color: red;">*</span> </td>';
    htmls += '  <td>收货说明</td>';
    htmls += '<td colspan=10><input type="text" id="ArrivalDescription" style="width:590px;" /></td>';
    htmls += '<td >   <input   type="button" onclick=ADDFK(' + tbody + ') class="btn"  value="保存" /></td>';
    htmls += '</tr>';
    $("#" + tbody + "").append(htmls);
}

function ADDFK(table) {


    var str = document.getElementById(table.id).innerHTML;
   
    //收货ID
    var SHID = $("#SHID").val();
    //收货人
    var ArrivalProcess = $("#ArrivalProcess").val();

    var DDID = location.search.split('&')[0].split('=')[1];


    var LJrownumber = "";
    var LJid = "";
    var LJname = "";
    var LJspec = "";
    var LJnums = "";
    var LJmanufacturer = "";
    var LJunits = "";
    var LJunitprice = "";
    var LJzj = "";
    var LJprice2 = "";
    var LJzj2 = "";
    var LJysnums = "";
    var LJbcnums = "";
    var LJbcshnums = "";
    var DDdid = "";
    var LJCPid = "";
    var tbodys = document.getElementById(table.id);
    for (var i = 0; i < tbodys.rows.length - 1; i++) {
        var LJRowNumber = document.getElementById("LJRowNumber" + i).innerHTML;
        var LJID = document.getElementById("LJID" + i).innerHTML;
        var LJnames = document.getElementById("LJnames" + i).innerHTML;
        var LJspes = document.getElementById("LJspes" + i).innerHTML;
        var LJNums = document.getElementById("LJnums" + i).innerHTML;
        var LJManufacturer = document.getElementById("LJManufacturer" + i).innerHTML;
        var LJUnits = document.getElementById("LJUnits" + i).innerHTML;
        var LJUnitPrice = document.getElementById("LJUnitPrice" + i).innerHTML;
        var LJZJ = document.getElementById("LJZJ" + i).innerHTML;
        var LJPrice2 = document.getElementById("LJPrice2" + i).innerHTML;
        var LJZJ2 = document.getElementById("LJZJ2" + i).innerHTML;

        var LJYSnums = document.getElementById("LJYSnums" + i).innerHTML;
        var LJBCnums = document.getElementById("LJBCnums" + i).value;
        var DDDID = document.getElementById("DDDID" + i).innerHTML;
        var LJCPID = document.getElementById("LJCPID" + i).innerHTML;
        LJBCSHnums = parseInt(LJYSnums) + parseInt(LJBCnums);
        if (parseInt(LJBCSHnums) > parseInt(LJNums)) {
            alert("收货数量大于总数量！！！");
            return;
        }
        LJrownumber += LJRowNumber;
        LJid += LJID;
        LJname += LJnames;
        LJspec += LJspes;
        LJnums += LJNums;
        LJmanufacturer += LJManufacturer
        LJunits += LJUnits;
        LJunitprice += LJUnitPrice;
        LJzj += LJZJ;
        LJprice2 += LJPrice2;
        LJzj2 += LJZJ2;
        LJbcshnums += LJBCSHnums;
        LJbcnums += LJBCnums;
        DDdid += DDDID;
        LJCPid += LJCPID;
        if (i < tbodys.rows.length - 1 - 1) {
            LJbcshnums += ",";
            LJbcnums += ",";
            LJrownumber += ",";
            LJid += ",";
            LJname += ",";
            LJspec += ",";
            LJnums += ",";
            LJmanufacturer += ",";
            LJunits += ",";
            LJunitprice += ",";
            LJzj += ",";
            LJprice2 += ",";
            LJzj2 += ",";
            DDdid += ",";
            LJCPid += ",";
        }
        else {
            LJbcshnums += "";
            LJbcnums += "";
            LJrownumber += " ";
            LJid += " ";
            LJname += "";
            LJspec += " ";
            LJnums += "";
            LJmanufacturer += "";
            LJunits += " ";
            LJunitprice += " ";
            LJzj += " ";
            LJprice2 += " ";
            LJzj2 += "";
            DDdid += "";
            LJCPid += "";
        }

    }
    var CPRownumberss = "";
    var CPpid = "";
    var CPid = "";
    var CPname = "";
    var CPspe = "";
    var CPnum = "";
    var CPshnums = "";
    var CPunits = "";
    var CPunitprice = "";
    var CPprice2 = "";
    var SJshnum = "";
    var tbodyss = document.getElementById("GXInfo");
    for (var i = 0; i < tbodyss.rows.length ; i++) {

        var CPRowNumberss = document.getElementById("CPRowNumberss" + i).innerHTML;
        var CPPid = document.getElementById("CPPid" + i).innerHTML;
        var CPID = document.getElementById("CPid" + i).innerHTML;
        var CPName = document.getElementById("CPName" + i).innerHTML;
        var CPSpe = document.getElementById("CPSpe" + i).innerHTML;
        var CPNum = document.getElementById("CPNum" + i).innerHTML;
        var CPUnits = document.getElementById("CPUnits" + i).innerHTML;
        var CPUnitPrice = document.getElementById("CPUnitPrice" + i).innerHTML;
        var CPPrice2 = document.getElementById("CPPrice2" + i).innerHTML;
        var CPYSHnum = document.getElementById("CPYSHnum" + i).innerHTML;
        var CPSHnums = document.getElementById("CPSHnums" + i).value;
        var SJSHnum = parseInt(CPSHnums) + parseInt(CPYSHnum);
        if (SJSHnum > CPNum) {
            alert("收货数量超出范围！！！");
            return;
        }
        CPRownumberss += CPRowNumberss;
        CPpid += CPPid;
        CPid += CPID;
        CPname += CPName;
        CPspe += CPSpe;
        CPnum += CPNum;
        CPshnums += CPSHnums;
        CPunits += CPUnits;
        CPunitprice += CPUnitPrice;
        CPprice2 += CPPrice2;
        SJshnum += SJSHnum;

        if (i < tbodyss.rows.length - 1) {

            CPRownumberss += ',';
            CPpid += ',';
            CPid += ',';
            CPname += ',';
            CPspe += ',';
            CPnum += ',';
            CPshnums += ',';
            CPunits += ',';
            CPunitprice += ',';
            CPprice2 += ',';
            SJshnum += ',';
        }

        else {
            CPRownumberss += '';
            CPpid += '';
            CPid += '';
            CPname += '';
            CPspe += '';
            CPnum += '';
            CPshnums += '';
            CPunits += '';
            CPunitprice += '';
            CPprice2 += '';
            SJshnum += '';
        }


    }
    //收货时间
    var ArrivalDate = $("#ArrivalDate").val();
    if (ArrivalDate == "") {
        alert("收货时间不可为空");
        return;
    }
    //收货说明
    var ArrivalDescription = $("#ArrivalDescription").val();
  


    isConfirm = confirm("确定要收货吗")
    if (isConfirm == false) {
        return false;
    }
    else {
        $.ajax({
            url: "InsertCPSHXX",
            type: "Post",
            data: {
                ljrownumber: LJrownumber, ljid: LJid, ljname: LJname, ljspec: LJspec, ljnums: LJnums, ljmanufacturer: LJmanufacturer, ljunits: LJunits, ljunitprice: LJunitprice, ljzj: LJzj, ljprice2: LJprice2, ljzj2: LJzj2, ljbcshnums: LJbcshnums, ljbcnums: LJbcnums, dddid: DDdid, ljcpid: LJCPid,

                cprownumberss: CPRownumberss, cppid: CPpid, cpid: CPid, cpname: CPname, cpspe: CPspe, cpnum: CPnum, cpshnums: CPshnums, cpunits: CPunits, cpunitprice: CPunitprice, cpprice2: CPprice2, sjshnum: SJshnum,
                shid: SHID, ddid: DDID, arrivalprocess: ArrivalProcess, arrivaldate: ArrivalDate, arrivaldescription: ArrivalDescription
            },
            async: false,
            success: function (data) {

                if (data.success == true) {
                    window.parent.frames["iframeRight"].reload1();
                    alert("成功");
                    location.reload(true);
                }
                else {
                    alert("失败");
                }
            }
        });
    }
}

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
