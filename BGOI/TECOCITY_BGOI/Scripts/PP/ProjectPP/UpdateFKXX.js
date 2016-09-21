var shuliang = "";
$(document).ready(function () {
    if (location.search != "") {
        payid = location.search.split('&')[0].split('=')[1];
        $.ajax({
            url: "SelectFKXQ",
            type: "post",
            data: { payid: payid },
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
                        html += '<td style="display:none" ><lable class="labProductID " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                        html += '<td style="display:none" ><lable class="labProductID " id="Payxid' + rowCount + '">' + json[i].PayXid + '</lable> </td>';
                        html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>'; 
                        html += '<td style="display:none"><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications " id="Suppliersss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                        html += '<td ><input type="text" id="Rate' + rowCount + '" value="' + json[i].Rate + '" style="width:150px;"/></td>';
                        html += '</tr>'
                        $("#GXInfo").append(html);
                        var Rate = document.getElementById("Rate" + i).value;
                        shuliang += Rate;
                        if (i == json.length - 1) {
                            shuliang += "";
                        }
                        else {
                            shuliang += ",";
                        }
                    }
                    document.getElementById("PayTime").value = json[0].PayTime;
                    document.getElementById("PaymentMenthod").value = json[0].PaymentMenthod;
                    document.getElementById("State").value = json[0].State;
                    document.getElementById("OrderContacts").value = json[0].OrderContacts;
                }
            }
        })
    }

    $("#btnSubmit").click(function () {

        var str = document.getElementById("GXInfo").innerHTML;
        if (str == "") {
            alert("请选择商品");
        }
        else {
            var PayId = location.search.split('&')[0].split('=')[1];
         
            //请购人
            var OrderContacts = $("#OrderContacts").val();

            var rownumber = "";
            var proname = "";
            var spec = "";
            var units = "";
            var amount = "";
            var supplier = "";
            var unitPriceNoTax = "";
            var totalnotax = "";
            var use = "";
            var inid = "";
            var did = "";
            var payxid = "";
            var rate = "";
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
                //数量
                var Amount = document.getElementById("Amount" + i).innerHTML;
                //供货商
                var Supplier = document.getElementById("Supplier" + i).innerHTML;
                //单价
                var UnitPriceNoTax = document.getElementById("UnitPriceNoTax" + i).innerHTML;
                //总价
                var TotalNoTax = document.getElementById("TotalNoTax" + i).innerHTML;
                //用途
     
                var DID = document.getElementById("DID" + i).innerHTML;
                var Payxid = document.getElementById("Payxid" + i).innerHTML;
               
                var chk = document.getElementById('LJselect' + i);
                if (chk.checked) {
                    var Rate = document.getElementById("Rate" + i).value;
                }
                else {
                    var Rate = 0;
                }
                if (Rate == "")
                {
                    Rate = 0;
                }

                rownumber += RowNumber;
                proname += ProName;
                spec += Spec;
                units += Units;
                amount += Amount;
                supplier += Supplier;
                unitPriceNoTax += UnitPriceNoTax;
                totalnotax += TotalNoTax;
               
                inid += INID;
                did += DID;
                payxid += Payxid;
                rate += Rate;
                if (i < tbody.rows.length - 1) {
                    rownumber += ",";
                    proname += ",";
                    spec += ",";
                    units += ",";
                    amount += ",";
                    supplier += ",";
                    unitPriceNoTax += ",";
                    totalnotax += ",";
                 
                    inid += ",";
                    did += ",";
                    payxid += ",";
                    rate += ",";
                }
                else {
                    rownumber += " ";
                    proname += " ";
                    spec += " ";
                    units += "";
                    amount += " ";
                    supplier += " ";
                    unitPriceNoTax += " ";
                    totalnotax += " ";
               
                    inid += "";
                    did += "";
                    payxid += "";
                    rate += "";
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
                alert("付费方式不可为空");
                return;
            }
            //付费状态
            var State = $("#State").val();
            if (State == "") {
                alert("付费状态不可为空");
                return;
            }
            isConfirm = confirm("确定要修改吗")
            if (isConfirm == false) {
                return false;
            }
            else {
                $.ajax({
                    url: "UpdateFK",
                    type: "Post",
                    data: {
                        RowNumber: rownumber, PayId: PayId, PayTime: PayTime, State: State, PaymentMenthod: PaymentMenthod, OrderContacts: OrderContacts, ProName: proname,
                        inid: inid,
                        Spec: spec, Units: units, Amount: amount, Supplier: supplier, UnitPriceNoTax: unitPriceNoTax, TotalNoTax: totalnotax,
                        did: did, shuliang: shuliang, payxid: payxid, rate: rate
                    },
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            window.parent.frames["iframeRight"].reload();
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


