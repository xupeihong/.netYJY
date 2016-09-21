var RowId = 0;
var str = "";
$(document).ready(function () {

    if (location.search != "") {
        if (location.search.split('&')[0].split('=')[0] == "?XJID") {
            XJID = location.search.split('&')[0].split('=')[1];
            $.ajax({
                url: "SelectGoodsXJID",
                type: "post",
                data: { XJID: XJID },
                dataType: "json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {

                        for (var i = 0; i < json.length; i++) {

                            rowCount = document.getElementById("GXInfo").rows.length;
                            var CountRows = parseInt(rowCount) + 1;
                            var html = "";
                            html = '<tr  id ="GXInfo' + rowCount + '" onclick="selRow(this)">'
                            html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                            html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                            html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].XXID + '</lable> </td>';
                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].Amount + '" onblur="OnBlurAmount(this);"  id="Amount' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].Supplier + '"  id="Supplier' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].NegotiatedPricingNoTax + '" onblur="OnBlurAmount(Amount' + rowCount + ');"  id="UnitPriceNoTax' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].TotalNegotiationNoTax + '"  id="TotalNoTax' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].GoodsUse + '"  id="GoodsUse' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].DrawingNum + '"  id="DrawingNum' + rowCount + '"> </td>';
                            html += '</tr>'
                            $("#GXInfo").append(html);
                        }
                    }
                }
            });
        }
        else if (location.search.split('&')[0].split('=')[0] == "?CID") {
            CID = location.search.split('&')[0].split('=')[1];
            $.ajax({
                url: "SelectGoodsQGID",
                type: "post",
                data: { CID: CID },
                dataType: "json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {

                        for (var i = 0; i < json.length; i++) {
                            rowCount = document.getElementById("GXInfo").rows.length;
                            var CountRows = parseInt(rowCount) + 1;
                            var html = "";
                            html = '<tr  id ="GXInfo' + rowCount + '" onclick="selRow(this)">'
                            html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                            html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                            html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';

                            html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';

                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';

                            html += '<td ><input type="text" style="width:100px" value="' + json[i].Amount + '"  onblur="OnBlurAmount(this);" id="Amount' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].Supplier + '" onclick=CheckSupplier(this)  id="Supplier' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPriceNoTax + '" onblur="OnBlurAmount(Amount' + rowCount + ');"  id="UnitPriceNoTax' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].TotalNoTax + '"  id="TotalNoTax' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].GoodsUse + '"  id="GoodsUse' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].DrawingNum + '"  id="DrawingNum' + rowCount + '"> </td>';
                            html += '</tr>'
                            $("#GXInfo").append(html);
                        }


                    }
                }
            });

        }

    } 

    $("#upLoad").click(function () {
        var PID = $("#DDID").val();
        //window.parent.OpenDialog("管理文件", "../PPManage/AddFile?PID=" + PID, 500, 300, '');
        window.parent.OpenDialog("管理文件", "../PPManage/InsertBiddingNew?PID=" + PID, 500, 300, '');
        
    });

    $("#Splits").click(function () {
         var PID = $("#DDID").val(); 
        //var PID = "DD-16072600470001";
        window.parent.OpenDialog("拆分零件", "../PPManage/LJSplits?PID=" + PID, 1095, 600, '');
    });

    $("#DH").click(function () {
        var text = $("#xzrwlx").val();
        if (text == "47") {
            window.parent.OpenDialog("选择任务单号", "../PPManage/OrderInfoManage", 1095, 600);
        }
        if (text == "37") {
            window.parent.OpenDialog("选择任务单号", "../PPManage/SalesRetailManage", 1095, 400);
        }
        if (text == "36") {
            window.parent.OpenDialog("选择任务单号", "../PPManage/General", 1095, 400);
        }
        if (text == "") {
            alert("请选择业务类型");
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


        var DDID = $("#DDID").val();
    

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
        var tbody = document.getElementById("GXInfo");
        if (tbody.rows.length == 0)
        {
            alert("请选择成品！！！")
            return;
        }
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
        var tbody1 = document.getElementById("GXInfo1");
        for (var i = 0; i < tbody1.rows.length; i++) {
            //序号
            var LJRowNumber = document.getElementById("RowNumber" + i).innerHTML;
            //成品ID
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

            LJpid += LJPid;
            LJCPid += LJCPID;
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
        //交货期限
        var Begin = $("#Begin").val();
        if (Begin == "") {
            alert("预计交货日期不可以为空");
            return;
        }


        //交货方式
        var DeliveryMethod = $("#DeliveryMethod").val();
        if (DeliveryMethod == "") {
            alert("交货方式不可为空");
            return;
        }
        //发票
        var IsInvoice = $("#IsInvoice").val();
        
 
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
        isConfirm = confirm("确定要添加吗")
        if (isConfirm == false) {
            return false;
        }
        else {


            LJunits += "";
            LJunitprice += "";
            LJzj += "";
            LJprice2 += "";
            LJzj2 += "";

            $.ajax({
                url: "InsertOrder",
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
                        if (location.search.split('&')[0].split('=')[0] != "") {
                            window.parent.frames["iframeRight"].reload();
                        }
                        alert("成功");
                        $("#upLoad").css("display", "");
                        $("#Splits").css("display", "");

                    }
                    else {
                        alert("失败");
                    }
                }
            });

        }

    });
    $("#btnadd").click(function () {
        {
            rowCount = document.getElementById("GXInfo").rows.length;
            var CountRows = parseInt(rowCount) + 1;
            $.ajax({
                url: "SelectKC",
                type: "post",
                data: {},
                dataType: "Json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        strSelect = " 选择成品&nbsp;&nbsp;&nbsp; <select id='Pip' style='width:160px;' >";
                        for (var i = 0; i < json.length; i++) {
                            strSelect += "<option>" + json[i].ProName + "</option>";
                        }
                        strSelect += "</select>";
                        var html = "";
                        html += '<tr>'
                        html += '<td >' + strSelect + ' </td>';
                        html += '<td >选择口径&nbsp;&nbsp;&nbsp;<select id="buidperson" class="txtCss" ></select></td>';
                        html += '</tr>'
                        $("#GXInfo").append(html);
                    }
                    else {
                        return;
                    }
                }
            });
        }
    });

    $("#SelectCP").click(function () {
        //var RelevanceID = $("#DDID").val();
        //var dataT = "[BGOI_PP]..ProductTempprary";
        window.parent.OpenDialog("选择成品", "../COM_Approval/ChoseGoods?id=CG", 900, 550);
    });


});
var chengpinid = new Array();
var array = new Array();
var numonesupplier = "";
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

        rowCount = document.getElementById("GXInfo").rows.length;
        var CountRows = parseInt(rowCount) + 1;
        var html = "";
        html += "<tr>";
        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
        html += '<td ><lable class="labProductID' + rowCount + ' " id="CPPid' + rowCount + '">' + pids[i] + '</lable> </td>';
        html += '<td ><lable class="labProductID' + rowCount + ' " id="Names' + rowCount + '">' + names[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spes' + rowCount + '">' + spes[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Nums' + rowCount + '">' + nums[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Units' + rowCount + '">' + units[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="UnitPrice' + rowCount + '">' + unitprice[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="UnitPrices' + rowCount + '">' + unitprice[i] * nums[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Price2' + rowCount + '">' + price2[i] + '</lable> </td>';
        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Price2s' + rowCount + '">' + price2[i] * nums[i] + '</lable> </td>';
        html += '</tr>'
        $("#GXInfo").append(html);
        chengpinid.push(pids[i] + "$" + nums[i]);
        GetLJ(pids[i], nums[i]);

    }


}


function GetLJ(cppid, num) {

    $.ajax({
        url: "SelectLingJXQ",
        type: "post",
        data: { cppid: cppid },
        dataType: "Json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo1").rows.length;
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




function GetLingJianID(PID, CPnum) {
    $.ajax({
        url: "SelectLingJ",
        type: "post",
        data: { PID: PID },
        dataType: "Json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    Getlingjian(json[i].PartPID, json[i].Number * CPnum);
                }
            }
            else {
                return;
            }
        }
    });
}

var arrpids = new Array();
function Getlingjian(pid, num) {
    $.ajax({
        url: "SelectLingJXQ",
        type: "post",
        data: { pid: pid },
        dataType: "Json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo1").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html += "<tr>";
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJPid' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJNames' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJSpes' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJNums' + rowCount + '">' + num + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJManufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo1").append(html);
                    array.push(json[i].COMNameC);
                    arrpids.push(pid);
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
            $("#supplier").append(" <input  id='supplier" + i + "'   class='btnTw' type='button' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "'style='overflow-y: scroll; border: 1px solid #707070;    margin-left: 10px; height: 200px'> <table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th> <th class='th'> 成品ID </th> <th class='th'> 零件ID </th><th class='th'> 零件名称 </th> <th class='th'> 规格型号 </th><th class='th'> 数量 </th> <th class='th'> 单位 </th> <th class='th'> 单价 </th> <th class='th'> 总价 </th> <th class='th'> 税前单价 </th> <th class='th'> 税前总价 </th>  <tbody id='GXInfosupplier" + i + "'></tbody></table></div>");
        }
        else {
            $("#supplier").append(" <input  id='supplier" + i + "' class='btnTh' type='button' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
            $("#bor1").append("  <div id='lingjian" + i + "' style='overflow-y: scroll; border: 1px solid #707070;   display:none; margin-left: 10px; height: 200px'><table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th class='th'> 序号 </th><th class='th'> 成品ID </th> <th class='th'> 零件ID </th> <th class='th'> 零件名称 </th> <th class='th'> 规格型号 </th><th class='th'> 数量 </th>  <th class='th'> 单位 </th> <th class='th'> 单价 </th> <th class='th'> 总价 </th> <th class='th'> 税前单价 </th> <th class='th'> 税前总价 </th>  <tbody id='GXInfosupplier" + i + "'></tbody></table> </div>");

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
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="CPID' + rowCount + '">' + numberss[0] + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="ID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                      
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
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="CPID' + rowCount + '">' + numberss[0] + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="ID' + rowCount + '">' + json[i].PID + '</lable> </td>';
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
//function Koujing() {
//    $("#koujing").empty();
//    var chengpin = $("#chengpin").val();
//    $.ajax({
//        url: "Selectkooujing",
//        type: "post",
//        data: { chengpin: chengpin },
//        dataType: "Json",
//        success: function (data) {
//            var json = eval(data.datas);
//            if (json.length > 0) {
//                var BandingTime = document.getElementById('koujing');
//                for (var i = 0; i < json.length; i++) {
//                    var tOption = document.createElement("Option");
//                    tOption.text = json[i].Spec;
//                    BandingTime.add(tOption);
//                }
//            }
//            else {
//                return;
//            }
//        }
//    });
//}


function AddNewBasic() {
    ShowIframe1("选择货品信息", "../PPManage/ChangeBasic", 800, 500);
}

function CheckSupplier(rowid) {
    RowId = rowid.id;
    ShowIframe1("选择供货商信息", "../PPManage/Supplier", 500, 350);
}
function Getid(id) {
    document.getElementById("TaskNum").value = id;
}

function addSupplier(SID) {
    var rownumber = RowId.substr(RowId.length - 1, 1);
    //var ProID = document.getElementById("PIDS" + rownumber).innerHTML;
    $.ajax({
        url: "GetSupplier",
        type: "post",
        data: { SID: SID },
        dataType: "json",
        ansyc: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#Supplier" + rownumber).val(json[0].COMNameC);

                    var ProID = document.getElementById("MaterialNO" + rownumber).innerHTML;
                    var SupID = document.getElementById("Supplier" + rownumber).value;
                    $.ajax({
                        url: "GetProductPrice",
                        type: "post",
                        data: { ProID: ProID, SupID: SupID },
                        dataType: "json",
                        ansyc: false,
                        success: function (data) {

                            var json = eval(data.datas);
                            if (json.length > 0) {

                                $("#UnitPriceNoTax" + rownumber).val(json[0].price);
                            }
                        }
                    });
                }
            }
        }
    });
}

function addBasicDetail(PID) { //增加货品信息行
    rowCount = document.getElementById("GXInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //var strPID = $("#PID").val();
    //$("#PID").val(strPID + "," + PID);
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
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="PIDS' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text" style="width:100px" value="0"   onblur="OnBlurAmount(this);" id="Amount' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"   onclick=CheckSupplier(this)  id="Supplier' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px" value="0"  onblur="OnBlurAmount(Amount' + rowCount + ');"   id="UnitPriceNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"    id="TotalNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"    id="GoodsUse' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"  id="DrawingNum' + rowCount + '"> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
            }
        }
    })
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
    for (var i = 1; i < tbody.rows.length; i++) {
        var totalAmount = document.getElementById("TotalNoTax" + i).value;
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
    var typeNames = ["RowNumber", "ProName", "Spec", "MaterialNO", "PIDS", "Units", "Amount", "Supplier", "UnitPriceNoTax", "TotalNoTax", "GoodsUse", "Remark"];

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


}


var oldid;
function selRow(curRow) {
    if (oldid != null) {
        if (document.getElementById(oldid) != undefined) {
            document.getElementById(oldid).style.backgroundColor = 'white';
        }
    }
    document.getElementById("myTable").style.backgroundColor = 'white';
    newRowID = curRow.id;
    document.getElementById(newRowID).style.backgroundColor = '#EEEEF2';
    oldid = newRowID;
}


function getDetailData() {
    var path = $("#txtPath").val();
    if (path == "") {
        alert("请选择文件路径");
        return;
    }
    else
        $("#shangcuan").show();
    readEx(path);
}

function readEx(path) {
    var ExcelSheet;
    var wb;

    try {
        ExcelSheet = new ActiveXObject("Excel.Application");
        wb = ExcelSheet.Workbooks.open(path);
        var objsheet = '';
        var num = 0;
        wb.worksheets(1).select();
        objsheet = wb.ActiveSheet;
        var colCount = objsheet.UsedRange.Columns.Count;
        var rowCount = objsheet.UsedRange.Rows.Count;
        var tab = $("#tabStatistic");
        tab.html('');

        for (var row = 2; row <= 4; row++) {
            var tr = $('<tr></tr>');
            for (var col = 1; col <= colCount - 0; col++) {
                var td = '';
                //var empty = objsheet.cells(row, 1).Value;
                //if (empty == "" || empty == undefined || empty == null)
                //    return;
                //else {
                var value = objsheet.cells(row, col).Value;
                if (value == "" || value == undefined || value == null)
                    td = $('<td></td>');
                else
                    td = $('<td>' + value + '</td>');
                td.appendTo(tr);
                //}
            }
            tr.appendTo(tab);
        }

        for (var row = 5; row <= rowCount; row++) {
            var tr = $('<tr></tr>');
            for (var col = 1; col <= colCount - 0; col++) {
                var td = '';
                var empty = objsheet.cells(row, 1).Value;
                //if (empty == "" || empty == undefined || empty == null)
                //    return;
                //else {
                var value = objsheet.cells(row, col).Value;
                if (value == undefined)
                    td = $('<td><span style="display:none">无</span></td>');
                else
                    td = $('<td>' + value + '</td>');
                td.appendTo(tr);
                //}
            }
            tr.appendTo(tab);
        }
    }
    catch (e) {
        if (ExcelSheet != undefined) {
            alert('Error happened : ' + e);
            ExcelSheet.Quit();
        }
        return '';
    }
}

function Save() {
    var record;
    var tab = $("#tabStatistic");
    record = tab[0].rows.length;
    if (record == 0) {
        alert("请重新上传文件");
        return false;
    }
    else {
        //$("#shangcuan").hide();
        UpLoadPlandata();
        return true;
    }
}


// 将数据保存到数据库中
function UpLoadPlandata() {
    //$("#shangcuan").hide();
    //编号/货品库类型/货品名称/物料号/规格型号/单价（含税）/不含税价格/	单位/厂家，生产厂商，供应商	/备注/详细说明/物品类型
    var tab = $("#tabStatistic");
    var countRow = tab[0].rows.length;
    var countCol = tab[0].rows[0].cells.length;
    var str = ''; // 将所有值拼接成需要的字符串
    var strs = '';
    for (var n = 0; n < 3; n++) {

        //var arr1 = tab[0].rows[0].cells[1].innerText;
        for (var col = 0; col < countCol; col++) {
            var arr = tab[0].rows[n].cells[col].innerText;
            if (arr != "" && arr != null) {
                if (parseInt(col) == parseInt(countCol) - 1) {
                    strs += "" + tab[0].rows[n].cells[col].innerText + "";
                }
                else {
                    strs += "" + tab[0].rows[n].cells[col].innerText + ",";
                }
            }
        }
    }
    for (var i = 4; i < countRow; i++) {

        //var arr1 = tab[0].rows[0].cells[1].innerText;
        for (var col = 0; col < countCol; col++) {
            var arr = tab[0].rows[i].cells[col].innerText;
            if (arr != "" && arr != null) {
                if (col == countCol - 1) {
                    str += "" + tab[0].rows[i].cells[col].innerText + ",";
                }
                else {
                    str += "" + tab[0].rows[i].cells[col].innerText + ",";
                }
            }

        }
        str += strs + "!";//每条完整数据用！分隔
    }
    str = str.substring(0, str.length - 1);

    //订购时间
    var OrderDate = $("#OrderDate").val();
    if (OrderDate == "") {
        alert("订购时间不可为空");
        return;
    }


    var strss = document.getElementById("tabStatistic").innerHTML;
    if (strss == "") {
        alert("请导入商品");
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


    var DDID = $("#DDID").val();
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
    //请购人
    var OrderContacts = $("#OrderContacts").val();
    //交货方式
    var DeliveryMethod = $("#DeliveryMethod").val();
    if (DeliveryMethod == "") {
        alert("交货方式不可为空");
        return;
    }
    //发票
    var IsInvoice = $("#IsInvoice").val();
    if (IsInvoice == "") {
        alert("发票信息不可为空");
        return;
    }
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
    isConfirm = confirm("确定要添加吗")
    if (isConfirm == false) {
        return false;
    }
    else {
        $.ajax({
            url: "SavePlanData",
            type: "post",
            data: { strData: str, orderdate: OrderDate, tasknum: TaskNum, businesstypes: BusinessTypes, ddid: DDID, begin: Begin, contractno: ContractNO, theproject: TheProject, ordercontacts: OrderContacts, deliverymethod: DeliveryMethod, isinvoice: IsInvoice, paymentmethod: PaymentMethod, paymentagreement: PaymentAgreement },
            dataType: "json",
            async: false, //是否异步
            success: function (data) {
                if (data.success == "false") {
                    if (data.Msg != "") {

                        alert(data.Msg);
                    }
                }
                else {
                    alert('货品数据保存成功');
                }
            }
        })
    }
}
function LoadTitleF1() {
    var tr1 = $("#line1");
    tr1.html('');

    //加载第一行
    var th1 = $('<td>序号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>名称</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>图纸或规格</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>单位</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>计划采购量</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>单价</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>金额</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>税前单价</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>税前总价</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>结账方式</td>');
    th1.appendTo(tr1);
    var th1 = $('<td  style="width:150px;">备注</td>');
    th1.appendTo(tr1);
}






