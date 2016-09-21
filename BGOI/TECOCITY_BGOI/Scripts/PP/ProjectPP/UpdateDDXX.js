var chengpinid = new Array();
$(document).ready(function () {
    DDID = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "SelectGoodsDDID",
        type: "post",
        data: { DDID: DDID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                document.getElementById("OrderDate").value = json[0].OrderDate;
                document.getElementById("xzrwlx").value = json[0].BusinessTypes;
                document.getElementById("TaskNum").value = json[0].PID;
                if (json[0].DeliveryLimit != "0001/1/1 0:00:00") {
                    document.getElementById("Begin").value = json[0].DeliveryLimit;
                }
                document.getElementById("DeliveryMethod").value = json[0].DeliveryMethod;
                //document.getElementById("IsInvoice").value = json[0].IsInvoice;
                document.getElementById("PaymentMethod").value = json[0].PaymentMethod;
                document.getElementById("PaymentAgreement").value = json[0].PaymentAgreement;
                document.getElementById("ContractNO").value = json[0].ContractNO;
                document.getElementById("TheProject").value = json[0].TheProject;
                document.getElementById("OrderContacts").value = json[0].OrderContacts;
            }
        }
    });
    var Name = "";
    var Spc = "";
    var Num = "";
    var Units = "";
    var Pid = "";
    var UnitPrice = "";
    var Price2 = "";
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
                    html = '<tr  id ="GXInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable  class="labOrderContent' + rowCount + ' " id="CPPid' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Names' + rowCount + '">' + json[i].Name + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spes' + rowCount + '">' + json[i].Spc + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Nums' + rowCount + '">' + json[i].Num + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable>  </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="UnitPrices' + rowCount + '">' + json[i].UnitPrice * json[i].Num + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Price2' + rowCount + '">' + json[i].Price2 + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Price2s' + rowCount + '">' + json[i].Price2 * json[i].Num + '</lable> </td>';
                    html += '</tr>'
                  
                    Pid += json[i].PID + '$';
                    Name += json[i].Name + '$';
                    Spc += json[i].Spc + '$';
                    Num += json[i].Num + '$';
                    Units += json[i].Units + '$';

                    UnitPrice += json[i].UnitPrice + '$';
                    Price2 += json[i].Price2 + '$';
                  
                    $("#GXInfo1").append(html);

                    chengpinid.push(json[i].PID + "$" + json[i].Num);

                    GetLJ(json[i].PID, json[i].Num);
                }
                var Names = Name.substring(0, Name.length - 1);
                var Spcs = Spc.substring(0, Spc.length - 1);
                var Nums = Num.substring(0, Num.length - 1);
                var Unitss = Units.substring(0, Units.length - 1);
                var Pids = Pid.substring(0, Pid.length - 1);

                var UnitPrices = UnitPrice.substring(0, UnitPrice.length - 1);
                var Price2s = Price2.substring(0, Price2.length - 1);

                $("#Name").val(Names);
                $("#Spc").val(Spcs);
                $("#Num").val(Nums);
                $("#Units").val(Unitss);
                $("#Pid").val(Pids);
                $("#UnitPrice").val(UnitPrices);
                $("#Price2").val(Price2s);
            }
        }
    });

    $("#Splits").click(function () {
        var PID = location.search.split('&')[0].split('=')[1];
        //var PID = "DD-16072600470001";
        window.parent.OpenDialog("拆分零件", "../PPManage/LJSplits?PID=" + PID, 1095, 600, '');
    });

    $("#DH").click(function () {
        var text = $("#xzrwlx").val();
        if (text == "47") {
            ShowIframe1("选择任务单号", "../PPManage/OrderInfoManage?id", 1095, 400);
        }
        if (text == "37") {
            ShowIframe1("选择任务单号", "../PPManage/SalesRetailManage?id", 1095, 400);
        }
        if (text == "36") {
            ShowIframe1("选择任务单号", "../PPManage/General?id", 1095, 400);
        }
    });

    $("#btnSubmit").click(function () {


        //订购时间
        var OrderDate = $("#OrderDate").val();
        if (OrderDate == "") {
            alert("订购时间不可为空");
            return;
        }

        //任务单号
        var TaskNum = $("#TaskNum").val();
        if (TaskNum == "") {
            TaskNum = "无";
        }
        //选择业务类型
        var BusinessTypes = $("#xzrwlx").val();
        if (BusinessTypes == "") {
            BusinessTypes = "无";
        }


        var DDID = location.search.split('&')[0].split('=')[1];
        //交货期限
        var Begin = $("#Begin").val();

        //合同编号
        var ContractNO = $("#ContractNO").val();
        if (ContractNO == "") {
            var ContractNO = "无";

        }
        //所属项目
        var TheProject = $("#TheProject").val();
        if (TheProject == "") {
            var TheProject = "无";
        }
        //订购人
        var OrderContacts = $("#OrderContacts").val();
        var CPname = "";
        var CPspec = "";
        var CPnums = "";
        var CPunits = "";
        var CPpid = "";

        var CPunitPrice = "";
        var CPunitPrices = "";
        var CPprice2 = "";
        var CPprice2s = "";
        var tbody = document.getElementById("GXInfo1");
        for (var i = 0; i < tbody.rows.length; i++) {
            //商品名称
            var CPName = document.getElementById("Names" + i).innerHTML;
            //商品型号
            var CPSpec = document.getElementById("Spes" + i).innerHTML;
            //商品数量
            var CPNums = document.getElementById("Nums" + i).innerHTML;
            //单位
            var CPUnits = document.getElementById("Units" + i).innerHTML;
            //pid
            var CPPid = document.getElementById("CPPid" + i).innerHTML;


            var CPUnitPrice = document.getElementById("UnitPrice" + i).innerHTML;
            var CPUnitPrices = document.getElementById("UnitPrices" + i).innerHTML;
            var CPPrice2 = document.getElementById("Price2" + i).innerHTML;
            var CPPrice2s = document.getElementById("Price2s" + i).innerHTML;
            CPname += CPName;
            CPspec += CPSpec;
            CPnums += CPNums;
            CPunits += CPUnits;
            CPpid += CPPid;

            CPunitPrice += CPUnitPrice;
            CPunitPrices += CPUnitPrices;
            CPprice2 += CPPrice2;
            CPprice2s += CPPrice2s;
            if (i < tbody.rows.length - 1) {
                CPname += ",";
                CPspec += ",";
                CPnums += ",";
                CPunits += ",";
                CPpid += ",";


                CPunitPrice += ",";
                CPunitPrices += ",";
                CPprice2 += ",";
                CPprice2s += ",";
            }
            else {
                CPname += " ";
                CPspec += " ";
                CPnums += " ";
                CPunits += "";
                CPpid += " ";


                CPunitPrice += "";
                CPunitPrices += "";
                CPprice2 += "";
                CPprice2s += "";
            }

        }

        var LJCPid = "";
        var LJpid = "";
        var LJnames = "";
        var LJspes = "";
        var LJnums = "";
        var LJmanufacturer = "";
        var LJrownumber = "";

        var LJunits = "";
        var LJunitprice = "";
        var LJzj = "";
        var LJprice2 = "";
        var LJzj2 = "";
        var tbody1 = document.getElementById("GXInfo2");
        for (var i = 0; i < tbody1.rows.length; i++) {
            //序号
            var LJRowNumber = document.getElementById("RowNumber" + i).innerHTML;
            var LJCPID = document.getElementById("LJCPID" + i).innerHTML;
            //零件id
            var LJPid = document.getElementById("LJPid" + i).innerHTML;
            //零件名称
            var LJNames = document.getElementById("LJNames" + i).innerHTML;
            //零件规格
            var LJSpes = document.getElementById("LJSpes" + i).innerHTML;
            //零件数量
            var LJNums = document.getElementById("LJNums" + i).innerHTML;
            //零件供货商
            var LJManufacturer = document.getElementById("LJManufacturer" + i).innerHTML;


            //单位
            var LJUnits = document.getElementById("LJUnits" + i).innerHTML;
            //单价
            var LJUnitPrice = document.getElementById("LJUnitPrice" + i).innerHTML;
            //总价
            var LJZJ = document.getElementById("LJZJ" + i).innerHTML;
            //税前单价
            var LJPrice2 = document.getElementById("LJPrice2" + i).innerHTML;
            //税前总价
            var LJZJ2 = document.getElementById("LJZJ2" + i).innerHTML;
            LJCPid += LJCPID;
            LJpid += LJPid;
            LJnames += LJNames;
            LJspes += LJSpes;
            LJnums += LJNums;
            LJmanufacturer += LJManufacturer;
            LJrownumber += LJRowNumber;

            LJunits += LJUnits;
            LJunitprice += LJUnitPrice;
            LJzj += LJZJ;
            LJprice2 += LJPrice2;
            LJzj2 += LJZJ2;
            if (i < tbody1.rows.length - 1) {
                LJCPid += ",";
                LJpid += ",";
                LJnames += ",";
                LJspes += ",";
                LJnums += ",";
                LJmanufacturer += ",";
                LJrownumber += ",";
                LJunits += ",";
                LJunitprice += ",";
                LJzj += ",";
                LJprice2 += ",";
                LJzj2 += ",";
            }
            else {
                LJCPid += "";
                LJpid += " ";
                LJnames += " ";
                LJspes += " ";
                LJnums += "";
                LJmanufacturer += "";
                LJrownumber += "";

                LJunits += "";
                LJunitprice += "";
                LJzj += "";
                LJprice2 += "";
                LJzj2 += "";
            }

        }
        //交货方式
        var DeliveryMethod = $("#DeliveryMethod").val();
        if (DeliveryMethod == "") {
            alert("交货方式不可为空");
            return;
        }
        //发票
        var IsInvoice = "";
        //if (IsInvoice == "") {
        //    alert("发票信息不可为空");
        //    return;
        //}
        //支付方式
        var PaymentMethod = $("#PaymentMethod").val();
        if (PaymentMethod == "") {
            alert("支付方式不可为空");
            return;
        }
        //付款约定
        var PaymentAgreement = $("#PaymentAgreement").val();
        if (PaymentAgreement == "") {
            alert("付款约定不可为空");
            return;
        }
        isConfirm = confirm("确定要修改吗")
        if (isConfirm == false) {
            return false;
        }
        else {

            $.ajax({
                url: "UpdateDD",
                type: "Post",
                data: {
                    orderdate: OrderDate, tasknum: TaskNum, businesstypes: BusinessTypes, ddid: DDID, begin: Begin, contractno: ContractNO, theproject: TheProject, ordercontacts: OrderContacts, cppid: CPpid, cpname: CPname, cpspec: CPspec, cpnums: CPnums, cpunits: CPunits, cpunitprice: CPunitPrice,
                    cpunitprices: CPunitPrices, cpprice2: CPprice2, cpprice2s: CPprice2s,
                    ljunits: LJunits, ljunitprice: LJunitprice, ljzj: LJzj,
                    ljprice2: LJprice2, ljzj2: LJzj2,ljcpid:LJCPid,

                    ljrownumber: LJrownumber, ljpid: LJpid, ljnames: LJnames, ljspes: LJspes, ljNums: LJnums, ljmanufacturer: LJmanufacturer, deliverymethod: DeliveryMethod, isinvoice: IsInvoice, paymentmethod: PaymentMethod, paymentagreement: PaymentAgreement
                },
                async: false,
                success: function (data) {
                    if (data.success == true) {
                     
                        window.parent.frames["iframeRight"].reload();
                        alert("成功"); 
                        $("#Splits").css("display", "");
                    }
                    else {
                        alert("失败");
                    }
                }
            });

        }


    });

    $("#SelectCP").click(function () {
        //var RelevanceID = $("#DDID").val();
        //var dataT = "[BGOI_PP]..ProductTempprary";
        ShowIframe1("选择成品", "../COM_Approval/ChoseGoods", 900, 550);
    });
});

var array = new Array();
function loadProduct(Name, Spc, Pid, Num, Units, UnitPrice, Price2) {
    chengpinid.splice(0, chengpinid.length);
    $("#GXInfo").empty();
    $("#GXInfo1").empty();

    $("#Name").val(Name);
    $("#Spc").val(Spc);
    $("#Pid").val(Pid);
    $("#Num").val(Num);
    $("#Units").val(Units);
    $("#UnitPrice").val(UnitPrice);
    $("#Price2").val(Price2);
    var names = new Array();
    names = Name.split('$');
    var spes = new Array();
    spes = Spc.split('$');
    var pids = new Array();
    pids = Pid.split('$');
    var nums = new Array();
    nums = Num.split('$');
    var units = new Array();
    units = Units.split('$');

    var unitprice = new Array();
    unitprice = UnitPrice.split('$');
    var price2 = new Array();
    price2 = Price2.split('$');

    array.splice(0, array.length);
    for (var i = 0; i < names.length; i++) {
        rowCount = document.getElementById("GXInfo1").rows.length;
        var CountRows = parseInt(rowCount) + 1;
        var html = "";
        html += "<tr>";
        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
        html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="CPPid' + rowCount + '">' + pids[i] + '</lable> </td>';
        html += '<td ><lable class="labProductID' + rowCount + ' " id="Names' + rowCount + '">' + names[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spes' + rowCount + '">' + spes[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Nums' + rowCount + '">' + nums[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Units' + rowCount + '">' + units[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="UnitPrice' + rowCount + '">' + unitprice[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="UnitPrices' + rowCount + '">' + unitprice[i] * nums[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Price2' + rowCount + '">' + price2[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Price2s' + rowCount + '">' + price2[i] * nums[i] + '</lable> </td>';
        html += '</tr>'
        $("#GXInfo1").append(html);
        chengpinid.push(pids[i] + "$" + nums[i]);

        GetLJ(pids[i], nums[i]);

    }

}
var array = new Array();

function GetLJ(cppid, num) {
    $('#GXInfo2').empty();
    $.ajax({
        url: "SelectLingJXQ",
        type: "post",
        data: { cppid: cppid },
        dataType: "Json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
     
                    rowCount = document.getElementById("GXInfo2").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html += "<tr>";
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJCPID' + rowCount + '">' + cppid + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJPid' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJNames' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJSpes' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJNums' + rowCount + '">' + json[i].Number * num + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJManufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';

                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJUnits' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJUnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJZJ' + rowCount + '">' + json[i].UnitPrice * json[i].Number * num + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJPrice2' + rowCount + '">' + json[i].Price2 + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJZJ2' + rowCount + '">' + json[i].Price2 * json[i].Number * num + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo2").append(html);
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
            $("#supplier").append(" <input  id='supplier" + i + "' class='btnTw' type='button' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "'style='overflow-y: scroll; border: 1px solid #707070;    margin-left: 10px; height: 200px'> <table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th> <th class='th'> 零件名称 </th><th class='th'> 规格型号 </th><th class='th'> 数量 </th> <th class='th'> 单位 </th> <th class='th'> 单价 </th> <th class='th'> 总价 </th> <th class='th'> 税前单价 </th> <th class='th'> 税前总价 </th>  <tbody id='GXInfosupplier" + i + "'></tbody></table></div>");
        }
        else {
            $("#supplier").append(" <input  id='supplier" + i + "' class='btnTh' type='button' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "' style='overflow-y: scroll; border: 1px solid #707070;   display:none; margin-left: 10px; height: 200px'><table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th> <th class='th'> 成品名称 </th><th class='th'> 规格型号 </th><th class='th'> 数量 </th>  <th class='th'> 单位 </th> <th class='th'> 单价 </th> <th class='th'> 总价 </th> <th class='th'> 税前单价 </th> <th class='th'> 税前总价 </th>  <tbody id='GXInfosupplier" + i + "'></tbody></table> </div>");
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

    //alert(text);
    var tbody = "";


    $("#supplier" + 0).attr("class", "btnTw");
    $("#lingjian" + 0).css("display", "");
    tbody = "GXInfosupplier" + 0;




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
function CheckSupplier(rowid) {
    RowId = rowid.id;
    ShowIframe1("选择供货商信息", "../PPManage/Supplier", 500, 350);
}
function addSupplier(SID) {
    $("#" + RowId).val(SID);
}
function Getid(id) {
    document.getElementById("TaskNum").value = id;
}
function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    var strRow = newCount.charAt(newCount.length - 1);

    var strU = "#UnitPriceNoTax" + strRow;
    var strUnitPrice = $(strU).val();
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);

    $("#TotalNoTax" + strRow).val(strTotal);

    GetAmount();
}
function GetAmount() {  //获取总数金额
    $("#AmountM").val("");
    var Amount = 0;
    var tbody = document.getElementById("GXInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var totalAmount = document.getElementById("UnitPriceNoTax" + i).value;
        if (totalAmount == "" || totalAmount == null) {
            totalAmount = "0";
        }
        Amount = Amount + parseFloat(totalAmount);

        $("#AmountM").val(Amount);
    }
}
function DelRow() {
    var tbodyID = "GXInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "ProName", "Spec", "Units", "Amount", "UnitPrice", "Total", "GoodsUse", "Remark"];

    if (newRowID != "")
        rowIndex = newRowID.replace(tbodyID, '');
    if (rowIndex != -1) {
        document.getElementById(tbodyID).deleteRow(rowIndex);
        if (rowIndex < $("#" + tbodyID + " tr").length) {
            for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
                var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                var tr1 = $("#" + tbodyID + (parseInt(i) + 1));
                tr.id = tbodyID + i;
                for (var j = 0; j < tr.childNodes.length; j++) {
                    var html = tr1.html();
                    for (var k = 0; k < typeNames.length; k++) {
                        var oldid = typeNames[k] + (parseInt(i) + 1);
                        var newid = typeNames[k] + i;
                        var reg = new RegExp(oldid, "g");
                        html = html.replace(reg, newid);
                    }
                    tr1.html(html);
                }
                $("#RowNumber" + i).html(parseInt(i) + 1);
            }
        }
        if (document.getElementById(tbodyID).rows.length > 0) {
            if (rowIndex == document.getElementById(tbodyID).rows.length)
                selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
            else
                selRow(document.getElementById(tbodyID + rowIndex), '');;
        }
    }

    $("#GXInfo tr").removeAttr("class");

}
function addBasicDetail(PID) { //增加货品信息行
    var rowCount = document.getElementById("GXInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //var CountRows = parseInt(rowCount) ++;
    //rowCount++;
    //var CountRows = rowCount;
    var strPID = $("#PID").val();
    $("#PID").val(strPID + "," + PID);
    $.ajax({
        url: "GetBasicDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="GXInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' "  id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text" style="width:70px" id="Amount' + rowCount + '" onblur="OnBlurAmount(this);" /> </td>';
                    html += '<td ><input type="text"style="width:70px" onclick="CheckSupplier(this)"  id="Supplier' + rowCount + '"/> </td>';
                    html += '<td ><input type="text" style="width:70px"  value="' + json[i].UnitPrice + '"  id="UnitPriceNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:70px"   id="TotalNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text"  style="width:70px"  id="DrawingNum' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:70px"  id="GoodsUse' + rowCount + '"> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
            }
        }
    })
}
function AddNewBasic() {
    ShowIframe1("选择货品信息", "../PPManage/ChangeBasic", 800, 500);
}
function selRow(curRow) {
    newRowID = curRow.id;
}
