

var RowId = 0;
var chengpinid = new Array();
var array = new Array();
var numonesupplier = "";
var strKF;
var strFKZT;
var tbody = "";

$(document).ready(function () {
    var RKID = location.search.split('&')[0].split('=')[1];
    addCP(RKID);
    $("#CPSubmit").click(function () {

        chengpinid.splice(0, chengpinid.length);
        array.splice(0, array.length);
        $("#GXInfo1").empty();
        var tbody = document.getElementById('GXInfo');
        for (var i = 0; i < tbody.rows.length; i++) {
            var chk = document.getElementById('CPselect' + i);
            if (chk.checked) {
                var CPRKnums = document.getElementById("CPRKnums" + i).value;
            }
            else {
                var CPRKnums = 0;
            }
            var CPNum = document.getElementById("CPNum" + i).innerHTML;   
            if (CPRKnums > CPNum) {
                alert("入库数量超出范围！！！");
                return;
            }
        }

        for (var i = 0; i < tbody.rows.length; i++) {

            var chk = document.getElementById('CPselect' + i);
            var CPID = document.getElementById("CPPid" + i).innerHTML;
            var NUM = $("#CPRKnums" + i).val();

            if (chk.checked) {
                chengpinid.push(CPID + "$" + NUM);
                GetLJ(CPID, NUM);
            }
        }
    });


    $.ajax({
        url: "GetNewConfigContKF",
        type: "post",
        data: {},
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strKF = "<select id='CKID' style='width:90px;'>";
                for (var i = 0; i < sel.length; i++) {
                    strKF += "<option value=" + sel[i] + ">" + val[i] + "</option>";
                }
                strKF += "</select>";
            }
            else {
                document.getElementById("CKID").options.length = 0;
                return;
            }
        }
    });
});







function addCP(RKID) {
    $.ajax({
        url: "SelectRKCP",
        type: "post",
        data: { rkid: RKID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><input name="CP" type="checkbox" id="CPselect' + rowCount + '" /></lable> </td>';
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="CPRowNumberss' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td  ><lable  class="labOrderContent' + rowCount + ' " id="CPPid' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td style="display:none"><lable  class="labOrderContent' + rowCount + ' " id="CPid' + rowCount + '">' + json[i].ID + '</lable> </td>';
                    html += '<td style="display:none"><lable  class="labOrderContent' + rowCount + ' " id="CPPIDS' + rowCount + '">' + json[i].CPPID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPName' + rowCount + '">' + json[i].Name + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPSpe' + rowCount + '">' + json[i].Spc + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPNum' + rowCount + '">' + json[i].RKnum + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPUnits' + rowCount + '">' + json[i].Units + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPUnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPPrice2' + rowCount + '">' + json[i].Price2 + '</lable>  </td>';
                    html += '<td ><input type="text" id="CPRKnums' + rowCount + '" value="' + json[i].RKnum + '"  style="width:150px;"/></td>';
                    html += '</tr>'

                    $("#GXInfo").append(html);
                }
            }
        }
    });
}


function GetLJ(CPID, num) {
    var rkid = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "SelectRKSupplier",
        type: "post",
        data: { cpid: CPID, rkid: rkid },
        dataType: "Json",
        async: false,
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
            $("#supplier").append(" <input  id='supplierss" + i + "'   class='btnTw' type='button'  value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "' style='overflow-y: scroll; border: 1px solid #707070;   display:none; margin-left: 10px; height: 200px'><table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th><th class='th'> 成品ID </th><th class='th'> 零件ID </th> <th class='th'> 零件名称 </th> <th class='th'> 规格型号 </th><th class='th'> 数量 </th> <th class='th'> 已入库数量 </th><th class='th'> 本次入库数量 </th>  <th class='th'> 单位 </th>  <th class='th'> 税后单价 </th>  <th class='th'> 税后总价 </th><th class='th'> 操作 </th>   <tbody id='GXInfosupplier" + i + "'></tbody></table> </div>");
        }
        else {
            $("#supplier").append(" <input  id='supplierss" + i + "' class='btnTh' type='button' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "' style='overflow-y: scroll; border: 1px solid #707070;   display:none; margin-left: 10px; height: 200px'><table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th><th class='th'> 成品ID </th><th class='th'> 零件ID </th> <th class='th'> 零件名称 </th> <th class='th'> 规格型号 </th><th class='th'> 数量 </th> <th class='th'> 已入库数量 </th><th class='th'> 本次入库数量 </th>  <th class='th'> 单位 </th>     <th class='th'> 税后总价 </th><th class='th'> 操作 </th>   <tbody id='GXInfosupplier" + i + "'></tbody></table> </div>");

        }
    }
    Supplierss($("#supplierss0").val());
}

//<th class='th'> 税前单价 </th> <th class='th'> 税前总价 </th> <th class='th'>

function Supplierss(text) {
    var rkid = location.search.split('&')[0].split('=')[1];
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
            url: "SelectRKSupplier",
            type: "post",
            async: false,
            data: { cpid: numberss[0], text: text, rkid: rkid },
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
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJCPID' + rowCount + '">' + json[i].LJCPID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJID' + rowCount + '">' + json[i].INID + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="SHDID' + rowCount + '">' + json[i].SHDID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJnames' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJspes' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJnums' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labOrderContent' + rowCount + ' " id="LJManufacturer' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                        html += '<td style="display:none" ><lable class="labOrderContent' + rowCount + ' " id="LJManufacturers' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="LJZQnums' + rowCount + '">' + json[i].SJAmount + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJYRnums' + rowCount + '">' + json[i].SHActualAmount + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJBCnums' + rowCount + '">' + json[i].Number * numberss[1] + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJUnits' + rowCount + '">' + json[i].Unit + '</lable> </td>';
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
    htmls += "<td></td>"
    htmls += "<td></td>"
    htmls += "<td>入库库房</td>"
    htmls += '<td >' + strKF + ' </td>';
    htmls += "<td>入库说明</td>"
    htmls += '<td colspan=6><input type="text" id="RKInstructions"  style="width:590px;" /></td>';
    htmls += '<td >   <input   type="button" onclick=ADDFK(' + tbody + ') class="btn"  value="保存" /></td>';
    htmls += '</tr>';
    $("#" + tbody + "").append(htmls);
}

function ADDFK(table) {


    var SHID = location.search.split('&')[1].split('=')[1];
    var RKID = location.search.split('&')[0].split('=')[1];
    var Begin = $("#Begin").val();
    if (Begin == "") {
        alert("入库时间不可为空！！");
        return;
    }
    var OrderContacts = $("#OrderContacts").val();


    var LJrownumber = "";
    var LJCPid = "";
    var LJid = "";
    var LJname = "";
    var LJspec = "";
    var LJnums = "";
    var LJmanufacturer = "";
    var LJunits = "";
    var LJprice2 = "";
    var LJzj2 = "";
    var LJzqnums = "";
    var LJyrnums = "";
    var LJbcnums = "";
    var SHdid = "";
    var tbodys = document.getElementById(table.id);
    for (var i = 0; i < tbodys.rows.length - 1; i++) {
        var LJRowNumber = document.getElementById("LJRowNumber" + i).innerHTML;
        
        var LJCPID = document.getElementById("LJCPID" + i).innerHTML;
        var LJID = document.getElementById("LJID" + i).innerHTML;
        var LJnames = document.getElementById("LJnames" + i).innerHTML;
        var LJspes = document.getElementById("LJspes" + i).innerHTML;
        var LJNums = document.getElementById("LJnums" + i).innerHTML;
        var LJManufacturer = document.getElementById("LJManufacturer" + i).innerHTML;
        var LJUnits = document.getElementById("LJUnits" + i).innerHTML;
        var LJPrice2 = document.getElementById("LJPrice2" + i).innerHTML;
        var LJZJ2 = document.getElementById("LJZJ2" + i).innerHTML;

        var LJZQnums = document.getElementById("LJZQnums" + i).innerHTML;
        var LJYRnums = document.getElementById("LJYRnums" + i).innerHTML;
        var LJBCnums = document.getElementById("LJBCnums" + i).innerHTML;
        var SHDID = document.getElementById("SHDID" + i).innerHTML;
        LJrownumber += LJRowNumber;
        LJCPid += LJCPID;
        LJid += LJID;
        LJname += LJnames;
        LJspec += LJspes;
        LJnums += LJNums;
        LJmanufacturer += LJManufacturer
        LJunits += LJUnits;
        LJprice2 += LJPrice2;
        LJzj2 += LJZJ2;
        LJzqnums += LJZQnums;
        LJyrnums += LJYRnums;
        LJbcnums += LJBCnums;
        SHdid += SHDID;
        if (i < tbodys.rows.length - 1 - 1) {
            LJrownumber += ",";
            LJid += ",";
            LJname += ",";
            LJspec += ",";
            LJnums += ",";
            LJmanufacturer += ",";
            LJunits += ",";
            LJprice2 += ",";
            LJzj2 += ",";
            LJCPid += ",";
            LJzqnums += ",";
            LJyrnums += ",";
            LJbcnums += ",";
            SHdid += ",";
        }
        else {
            LJrownumber += " ";
            LJid += " ";
            LJname += "";
            LJspec += " ";
            LJnums += "";
            LJmanufacturer += "";
            LJunits += " ";
            LJprice2 += " ";
            LJzj2 += "";
            LJCPid += "";
            LJzqnums += "";
            LJyrnums += "";
            LJbcnums += "";
            SHdid += "";
        }

    }
    var CPRownumberss = "";
    var CPpid = "";
    var CPid = "";
    var CPname = "";
    var CPspe = "";
    var CPnum = "";
    var CPrknums = "";
    var CPunits = "";
    var CPunitprice = "";
    var CPprice2 = ""; 
    var CPpids = "";
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
        var CPPIDS = document.getElementById("CPPIDS" + i).innerHTML;
        var chk = document.getElementById("CPselect" + i).innerHTML;
        if (chk.checked) {
            var CPRKnums = document.getElementById("CPRKnums" + i).value;
        }
        else {
            var CPRKnums = 0;
        } 
        if (CPRKnums > CPNum) {
            alert("入库数量超出范围！！！");
            return;
        }
        CPRownumberss += CPRowNumberss;
        CPpid += CPPid;
        CPid += CPID;
        CPname += CPName;
        CPspe += CPSpe;
        CPnum += CPNum;
        CPrknums += CPRKnums;
        CPunits += CPUnits;
        CPunitprice += CPUnitPrice;
        CPprice2 += CPPrice2;
        CPpids += CPPIDS;
        if (i < tbodyss.rows.length - 1) {
            CPRownumberss += ',';
            CPpid += ',';
            CPid += ',';
            CPname += ',';
            CPspe += ',';
            CPnum += ',';
            CPrknums += ',';
            CPunits += ',';
            CPunitprice += ',';
            CPprice2 += ',';
            CPpids += ",";
        }
        else {
            CPRownumberss += '';
            CPpid += '';
            CPid += '';
            CPname += '';
            CPspe += '';
            CPnum += '';
            CPrknums += '';
            CPunits += '';
            CPunitprice += '';
            CPprice2 += '';
            CPpids += "";
        }

    }

    var CKID = $("#CKID").val();
    if (CKID == "") {
        alert("库房不可为空");
        return;
    }

    var RKInstructions = $("#RKInstructions").val();
    if (RKInstructions == "") {
        alert("入库说明不可为空");
        return;
    }


    isConfirm = confirm("确定要修改吗")
    if (isConfirm == false) {
        return false;
    }
    else {
        $.ajax({
            url: "UpdateCPRK",
            type: "Post",
            data: {
                ljrownumber: LJrownumber, ljid: LJid, ljname: LJname, ljspec: LJspec, ljnums: LJnums, ljmanufacturer: LJmanufacturer, ljunits: LJunits, ljprice2: LJprice2, ljzj2: LJzj2,ljcpid:LJCPid,ljzqnums:LJzqnums,LJyrnums:LJyrnums,ljbcnums:LJbcnums,shdid:SHdid,

                cprownumberss: CPRownumberss, cppid: CPpid, cpid: CPid, cpname: CPname, cpspe: CPspe, cpnum: CPnum, cprknums: CPrknums, cpunits: CPunits, cpunitprice: CPunitprice, cpprice2: CPprice2,  cppids : CPpids ,

                shid: SHID, rkid: RKID, begin: Begin, ckid: CKID, rkinstructions: RKInstructions, ordercontacts: OrderContacts
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