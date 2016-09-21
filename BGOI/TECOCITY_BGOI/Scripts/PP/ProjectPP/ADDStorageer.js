var RowId = 0;
var chengpinid = new Array();
var array = new Array();
var numonesupplier = "";
var strKF;
var strFKZT;
var tbody = "";
$(document).ready(function () {
    var SHID = location.search.split('&')[0].split('=')[1];
    addCP(SHID);

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


            if (parseInt(CPRKnums) > parseInt(CPNum)) {
                alert("入库数量超出范围！！！");
                return;
            }
        }

        for (var i = 0; i < tbody.rows.length; i++) {

            var chk = document.getElementById('CPselect' + i);
            var CPID = document.getElementById("CPID" + i).innerHTML;
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






function addCP(SHID) {
    $.ajax({
        url: "SelectSHCP",
        type: "post",
        data: { shid: SHID },
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
                    html += '<td style="display:none"><lable  class="labOrderContent' + rowCount + ' " id="ID' + rowCount + '">' + json[i].ID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPName' + rowCount + '">' + json[i].Name + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPSpe' + rowCount + '">' + json[i].Spc + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPNum' + rowCount + '">' + json[i].SHnum + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPUnits' + rowCount + '">' + json[i].Units + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPUnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="CPPrice2' + rowCount + '">' + json[i].Price2 + '</lable>  </td>';
                    html += '<td ><input type="text" id="CPRKnums' + rowCount + '" value="' + json[i].SHnum + '"  style="width:150px;"/></td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
            }
        }
    });
}

function GetLJ(CPID, num) {
    var shid = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "SelectSHSupplier",
        type: "post",
        data: { cpid: CPID, shid: shid },
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
            $("#bor1").append("  <div id='lingjian" + i + "'style='overflow-y: scroll; border: 1px solid #707070;    margin-left: 10px; height: 200px'> <table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th><th class='th'> 成品ID </th> <th class='th'> 零件ID </th><th class='th'> 零件名称 </th> <th class='th'> 规格型号 </th><th class='th'> 总数量 </th> <th class='th'> 已入库数量 </th> <th class='th'> 本次入库数量 </th>  <th class='th'> 单位 </th> <th class='th'> 税后单价 </th> <th class='th'> 税后总价 </th> <th class='th'> 操作 </th>   <tbody id='GXInfosupplier" + i + "'></tbody></table></div>");
        }
        else {
            $("#supplier").append(" <input  id='supplierss" + i + "' class='btnTh' type='button' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "' style='overflow-y: scroll; border: 1px solid #707070;   display:none; margin-left: 10px; height: 200px'><table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th><th class='th'> 成品ID </th><th class='th'> 零件ID </th> <th class='th'> 零件名称 </th> <th class='th'> 规格型号 </th><th class='th'> 总数量 </th> <th class='th'> 已入库数量 </th> <th class='th'> 本次入库数量 </th> <th class='th'> 单位 </th>  <th class='th'> 税后单价 </th> <th class='th'> 税后总价 </th><th class='th'> 操作 </th>   <tbody id='GXInfosupplier" + i + "'></tbody></table> </div>");

        }
    }
    Supplierss($("#supplierss0").val());
}


function Supplierss(text) {
    var shid = location.search.split('&')[0].split('=')[1];
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
            url: "SelectSHSupplier",
            type: "post",
            async: false,
            data: { cpid: numberss[0], text: text, shid: shid },
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
                        html += '<td  style="display:none"><lable class="labProductID' + rowCount + ' " id="SHDID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJCPID' + rowCount + '">' + numberss[0] + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJID' + rowCount + '">' + json[i].INID + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="LJnames' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJspes' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJnums' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labOrderContent' + rowCount + ' " id="LJManufacturer' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                        html += '<td style="display:none" ><lable class="labOrderContent' + rowCount + ' " id="LJManufacturers' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJYFnums' + rowCount + '">' + json[i].ActualAmount + '</lable> </td>';
                        //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJBCnums' + rowCount + '">' + json[i].Number * numberss[1] + '</lable> </td>';
                        html += '<td ><input type="text" id="LJBCnums' + rowCount + '" onblur="OnBlurAmount(this);" value="' + json[i].Number * numberss[1] + '"  style="width:150px;"/></td>';
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
    htmls += "<td>入库库房</td>"
    htmls += '<td >' + strKF + ' </td>';
    htmls += "<td>入库说明</td>"
    htmls += '<td colspan=7><input type="text" id="RKInstructions"  style="width:590px;" /></td>';
    htmls += '<td >   <input   type="button" onclick=ADDFK(' + tbody + ') class="btn"  value="保存" /></td>';
    htmls += '</tr>';
    $("#" + tbody + "").append(htmls);
}

function ADDFK(table) {


    var SHID = location.search.split('&')[0].split('=')[1];
    var RKID = $("#RKID").val();
    var Begin = $("#Begin").val();
    if (Begin == "") {
        alert("入库时间不可为空！！");
        return;
    }
    var OrderContacts = $("#OrderContacts").val();


    var LJrownumber = "";
    var SHdid = "";
    var LJcpid = "";
    var LJid = "";
    var LJname = "";
    var LJspec = "";
    var LJnums = "";
    var LJmanufacturer = "";
    var LJyfnums = "";
    var LJbcnums = "";
    var LJunits = "";
    var LJprice2 = "";
    var LJzj2 = "";
    var LJbcrknums = "";

    var tbodys = document.getElementById(table.id);
    for (var i = 0; i < tbodys.rows.length - 1; i++) {

        var LJRowNumber = document.getElementById("LJRowNumber" + i).innerHTML;
        var SHDID = document.getElementById("SHDID" + i).innerHTML;
        var LJCPID = document.getElementById("LJCPID" + i).innerHTML;
        var LJID = document.getElementById("LJID" + i).innerHTML;
        var LJnames = document.getElementById("LJnames" + i).innerHTML;
        var LJspes = document.getElementById("LJspes" + i).innerHTML;
        var LJNums = document.getElementById("LJnums" + i).innerHTML;
        var LJManufacturer = document.getElementById("LJManufacturer" + i).innerHTML;
        var LJYFnums = document.getElementById("LJYFnums" + i).innerHTML;
        var LJBCnums = document.getElementById("LJBCnums" + i).value;
        var LJBCRKnum = parseInt(LJYFnums) + parseInt(LJBCnums)
        var LJUnits = document.getElementById("LJUnits" + i).innerHTML;
        var LJPrice2 = document.getElementById("LJPrice2" + i).innerHTML;
        var LJZJ2 = document.getElementById("LJZJ2" + i).innerHTML;
        if (LJBCRKnum > LJNums)
        {
            alert("入库零件数量超出总数量！！！");
            return;
        }

        LJrownumber += LJRowNumber;
        SHdid += SHDID;
        LJcpid += LJCPID;
        LJid += LJID;
        LJname += LJnames;
        LJspec += LJspes;
        LJnums += LJNums;
        LJmanufacturer += LJManufacturer
        LJyfnums += LJYFnums;
        LJbcnums += LJBCnums;
        LJunits += LJUnits;
        LJprice2 += LJPrice2;
        LJzj2 += LJZJ2;
        LJbcrknums += LJBCRKnum;
        if (i < tbodys.rows.length - 1 - 1) {
            LJrownumber += ",";
            SHdid += ",";
            LJcpid += ",";
            LJid += ",";
            LJname += ",";
            LJspec += ",";
            LJnums += ",";
            LJmanufacturer += ",";
            LJyfnums += ",";
            LJbcnums += ",";
            LJunits += ",";
            LJprice2 += ",";
            LJzj2 += ",";
            LJbcrknums += ",";
        }
        else {
            LJrownumber += " ";
            SHdid += "";
            LJcpid += "";
            LJid += " ";
            LJname += "";
            LJspec += " ";
            LJnums += "";
            LJmanufacturer += "";
            LJyfnums += "";
            LJbcnums += "";
            LJunits += " ";
            LJprice2 += " ";
            LJzj2 += "";
            LJbcrknums += "";
        }

    }
    var CPrownumberss = "";
    var Id = "";
    var CPid = "";
    var CPname = "";
    var CPspe = "";
    var CPnum = "";
    var CPunits = "";
    var CPunitprice = "";
    var CPprice2 = "";
    var SJrknum = "";
    var tbodyss = document.getElementById("GXInfo");
    for (var i = 0; i < tbodyss.rows.length ; i++) {
        var CPRowNumberss = document.getElementById("CPRowNumberss" + i).innerHTML;
        var ID = document.getElementById("ID" + i).innerHTML;
        var CPID = document.getElementById("CPID" + i).innerHTML;
        var CPName = document.getElementById("CPName" + i).innerHTML;
        var CPSpe = document.getElementById("CPSpe" + i).innerHTML;
        var CPNum = document.getElementById("CPNum" + i).innerHTML;
        var CPUnits = document.getElementById("CPUnits" + i).innerHTML;
        var CPUnitPrice = document.getElementById("CPUnitPrice" + i).innerHTML;
        var CPPrice2 = document.getElementById("CPPrice2" + i).innerHTML;
        var chk = document.getElementById('CPselect' + i);
        if (chk.checked) {
            var CPRKnums = document.getElementById("CPRKnums" + i).value;
        }
        else {
            var CPRKnums = 0;
        } if (CPRKnums > CPNum) {
            alert("入库数量超出范围！！！");
            return;
        }
        CPrownumberss += CPRowNumberss;
        Id += ID;
        CPid += CPID;
        CPname += CPName;
        CPspe += CPSpe;
        CPnum += CPNum;
        CPunits += CPUnits;
        CPunitprice += CPUnitPrice;
        CPprice2 += CPPrice2;
        SJrknum += CPRKnums;
        if (i < tbodyss.rows.length - 1) {
            CPrownumberss += ',';
            Id += ',';
            CPid += ',';
            CPname += ',';
            CPspe += ',';
            CPnum += ',';

            CPunits += ',';
            CPunitprice += ',';
            CPprice2 += ',';
            SJrknum += ',';
        }
        else {
            CPrownumberss += '';
            Id += '';
            CPid += '';
            CPname += '';
            CPspe += '';
            CPnum += '';
            CPunits += '';
            CPunitprice += '';
            CPprice2 += '';
            SJrknum += '';
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


    isConfirm = confirm("确定要收货吗")
    if (isConfirm == false) {
        return false;
    }
    else {
        $.ajax({
            url: "InsertCPRK",
            type: "Post",
            data: {
                ljrownumber: LJrownumber, shdid: SHdid, ljcpid: LJcpid, ljid: LJid, ljname: LJname, ljspec: LJspec, ljnums: LJnums, ljmanufacturer: LJmanufacturer, ljyfnums: LJyfnums, ljbcnums: LJbcnums, ljunits: LJunits, ljprice2: LJprice2, ljzj2: LJzj2, ljbcrknums: LJbcrknums,

                cprownumberss: CPrownumberss, id: Id, cpid: CPid, cpname: CPname, cpspe: CPspe, cpnum: CPnum, cpunits: CPunits, cpunitprice: CPunitprice, cpprice2: CPprice2, sjrknum: SJrknum,

                shid: SHID, rkid: RKID, begin: Begin, ckid: CKID, rkinstructions: RKInstructions, ordercontacts: OrderContacts
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
     
    //var strUnitPrice = $(strU).val();

    var strP = document.getElementById("LJPrice2" + value).innerText;
    //var strPrice2 = $(strP).val();
     
    var TotalTax = parseFloat(Count) * parseFloat(strP);
     
    document.getElementById("LJZJ2" + value).innerHTML = TotalTax;

}