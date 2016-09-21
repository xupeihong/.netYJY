var RowId = 0;
$(document).ready(function () {


    if (location.search != "") {

        var ddid = location.search.split('&')[0].split('=')[1];

        addDDID(ddid);
    }
    $("#btnSubmit").click(function () {

        var str = document.getElementById("GXInfo").innerHTML;
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

            var PayId = $("#PayId").val();
            var DDID = location.search.split('&')[0].split('=')[1];
            //请购人
            var OrderContacts = $("#OrderContacts").val();



            var did = "";
            var rownumber = "";
            var proname = "";
            var spec = "";
            var units = "";
            var amount = "";
            var supplier = "";
            var unitPrice = "";
            var total = "";
            var unitPriceNoTax = "";
            var totalnotax = "";
            var goodsuse = "";
            var inid = "";
            var remark = "";
            var rate = "";
            var rates = "";
            var tbody = document.getElementById("GXInfo");
            for (var i = 0; i < tbody.rows.length; i++) {


                //序号
                var RowNumber = document.getElementById("RowNumber" + i).innerHTML;
                //商品名称
                var ProName = document.getElementById("ProName" + i).innerHTML;
                //商品型号
                var Spec = document.getElementById("Spec" + i).innerHTML;
                //单位
                var Units = document.getElementById("Units" + i).innerHTML;

                var INID = document.getElementById("MaterialNO" + i).innerHTML;

                var Amount = document.getElementById("Amount" + i).innerHTML;

                //供货商
                var Supplier = document.getElementById("Supplier" + i).innerHTML;


                var UnitPrice = document.getElementById("UnitPrice" + i).innerHTML;
                //总价

                var Total = document.getElementById("Total" + i).innerHTML;


                //单价
                var UnitPriceNoTax = document.getElementById("UnitPriceNoTax" + i).innerHTML;
                //总价

                var TotalNoTax = document.getElementById("TotalNoTax" + i).innerHTML;


      
                 
                var Remark = document.getElementById("Remark" + i).value;
                if (Remark == "") {
                    Remark = "无";
                }
                var Rate = document.getElementById("Rate" + i).innerHTML;
                //var Rates = document.getElementById("Rates" + i).value;

                var chk = document.getElementById('LJselect' + i);
                if (chk.checked) {
                    var Rates = document.getElementById("Rates" + i).value;
                }
                else {
                    var Rates = 0;
                }
                if (Rates == "") {
                     Rates = 0;
                }
                var DID = document.getElementById("DID" + i).innerHTML;


                rownumber += RowNumber;
                proname += ProName;
                spec += Spec;
                units += Units;
                amount += Amount;
                supplier += Supplier;
                unitPrice += UnitPrice;
                total += Total;


                unitPriceNoTax += UnitPriceNoTax;
                totalnotax += TotalNoTax;
                
                inid += INID;
                remark += Remark;
                rate += Rate;
                rates += Rates;
                did += DID;

                if (i < tbody.rows.length - 1) {
                    rownumber += ",";
                    proname += ",";
                    spec += ",";
                    units += ",";
                    amount += ",";
                    supplier += ",";
                    unitPrice += ",";
                    total += ",";
                    unitPriceNoTax += ",";
                    totalnotax += ",";
               
                    inid += ",";
                    remark += ",";
                    rate += ",";
                    rates += ",";
                    did += ",";
                }
                else {
                    rownumber += " ";
                    proname += " ";
                    spec += " ";
                    units += "";
                    amount += " ";
                    supplier += " ";
                    unitPrice += "";
                    total += "";
                    unitPriceNoTax += " ";
                    totalnotax += " ";
               
                    inid += "";
                    remark += "";
                    rate += "";
                    rates += "";
                    did += "";
                }
                if (parseInt(Rate) + parseInt(Rates) > parseInt(Amount)) {
                    alert("付款数量大于总数量");
                    return;
                }
            }

            //付费时间
            var PayTime = $("#PayTime").val();
            if (PayTime == "") {
                alert("付费时间不可为空");
                return;
            }
            //支付方式
            var PaymentMenthod = $("#PaymentMenthod").val();
            if (PaymentMenthod == "") {
                alert("支付方式不可为空");
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
                    url: "InsertLJFK",
                    type: "Post",
                    data: {
                    ddid:DDID, CID: CID, RowNumber: rownumber, PayId: PayId, PayTime: PayTime, State: State, PaymentMenthod: PaymentMenthod, OrderContacts: OrderContacts, ProName: proname,
                        inid: inid, did: did,
                        Spec: spec, Units: units, Amount: amount, Supplier: supplier, UnitPriceNoTax: unitPriceNoTax, totalnotax: totalnotax, unitprice: unitPrice, total: total, remark: remark, rate: rate, rates: rates
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
    });
});

function AddNewBasic() {
    //window.parent.OpenDialog("选择货品信息", "../PPManage/ChangeBasic", 800, 500);
    ShowIframe1("选择货品信息", "../InventoryManage/ChangeBasic?id=1", 550, 500);
}

function addBasicDetail(PID) { //增加货品信息行
    //判断重复数据
    rowCount = document.getElementById("GXInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var tbody = document.getElementById("GXInfo");
    if (tbody.rows.length == 0) {
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
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="PIDS' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" style="width:100px" value="0"   onblur="OnBlurAmount(this);" id="Amount' + rowCount + '"> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Suppliersss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td  style="display:none"><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                        html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPrice + '"  onblur="OnBlurAmount(Amount' + rowCount + ');"   id="UnitPriceNoTax' + rowCount + '"> </td>';
                        html += '<td ><input type="text" style="width:100px"    id="TotalNoTax' + rowCount + '"> </td>';
                        html += '<td ><input type="text" style="width:100px" value="' + json[i].Price2 + '"    id="Price' + rowCount + '"> </td>';
                        html += '<td ><input type="text" style="width:100px"   id="TotalTax' + rowCount + '"> </td>';
                        html += '</tr>'
                        $("#GXInfo").append(html);
                        CountRows = CountRows + 1;
                        rowCount += 1;
                    }
                }
            }
        })
    } else {
        var array = new Array();
        var array = PID.split(',');
        for (var i = 0; i < tbody.rows.length; i++) {
            var pID = tbody.getElementsByTagName("tr")[i].cells[3].innerText;
            for (var n = 0; n < array.length; n++) {
                if (array[n].replace("'", "").replace("'", "").trim() == pID.trim()) {
                    alert("所选零件有重复！！！");
                    return;
                }
            }
        }
        rowCount = document.getElementById("GXInfo").rows.length;
        var CountRows = parseInt(rowCount) + 1;
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
                        html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="PIDS' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" style="width:100px" value="0"   onblur="OnBlurAmount(this);" id="Amount' + rowCount + '"> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Suppliersss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td  style="display:none"><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                        html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPrice + '"  onblur="OnBlurAmount(Amount' + rowCount + ');"   id="UnitPriceNoTax' + rowCount + '"> </td>';
                        html += '<td ><input type="text" style="width:100px"    id="TotalNoTax' + rowCount + '"> </td>';
                        html += '<td ><input type="text" style="width:100px" value="' + json[i].Price2 + '"    id="Price' + rowCount + '"> </td>';
                        html += '<td ><input type="text" style="width:100px"   id="TotalTax' + rowCount + '"> </td>';
                        html += '</tr>'
                        $("#GXInfo").append(html);
                    }
                }
            }
        })



    }
}

function addDDID(DDID) { //增加货品信息行
    rowCount = document.getElementById("GXInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //var strPID = $("#DDID").val();
    //$("#DID").val(strPID + "," + DDID);
    $.ajax({
        url: "SelectGoodsDDID",
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
                    html += '<td ><input name="CP" type="checkbox" id="LJselect' + rowCount + '" /></lable> </td>';
                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Supplierss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';

                    html += '<td ><lable class="labSpecifications " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Total' + rowCount + '">' + json[i].Total + '</lable> </td>';

                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';

                    html += '<td > <lable class="labSpecifications " id="Rate' + rowCount + '">' + json[i].SJFK + '</lable></td>';
                    html += '<td ><input type="text"  style="width:160px;" value="' + json[i].Amount + '"  id="Rates' + rowCount + '"></td>';
                    html += '<td ><input type="text"  style="width:160px;" id="Remark' + rowCount + '"></td>';


                    html += '</tr>'
                    if (json[i].Amount != json[i].SJFK) {
                        $("#GXInfo").append(html);
                    }


                }
            }
        }


    })
}



 
 





