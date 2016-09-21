var RowId = 0;
$(document).ready(function () {


    if (location.search != "") {

        if (location.search.split('&')[0].split('=')[0] == "?texts") {
            var texts = location.search.split('&')[0].split('=')[1];
            texts = texts.substr(0, texts.length - 1);
            var mycars = new Array()
            mycars = texts.split(',');
            for (var i = 0; i < mycars.length; i++) {
                var DID = mycars[i];
                addDDID(DID);

            }
        }
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
      
            //请购人
            var OrderContacts = $("#OrderContacts").val();



            var did = "";
            var rownumber = "";
            var proname = "";
            var spec = "";
            var units = "";
            var amount = "";
            var supplier = "";
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
                //单价
                var UnitPriceNoTax = document.getElementById("UnitPriceNoTax" + i).innerHTML;
                //总价

                var TotalNoTax = document.getElementById("TotalNoTax" + i).innerHTML;


                //用途

                var GoodsUse = document.getElementById("GoodsUse" + i).innerHTML;

                var Remark = document.getElementById("Remark" + i).value;
                if (Remark == "")
                {
                    Remark = "无";
                }
                var Rate = document.getElementById("Rate" + i).innerHTML;
                var Rates = document.getElementById("Rates" + i).value;
                if (Rates == "")
                {
                    alert("付款金额不可为空");
                    return;
                }
                var DID = document.getElementById("DID" + i).innerHTML;


                rownumber += RowNumber;
                proname += ProName;
                spec += Spec;
                units += Units;
                amount += Amount;
                supplier += Supplier;
                unitPriceNoTax += UnitPriceNoTax;
                totalnotax += TotalNoTax;
                goodsuse += GoodsUse;
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
                    unitPriceNoTax += ",";
                    totalnotax += ",";
                    goodsuse += ",";
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
                    unitPriceNoTax += " ";
                    totalnotax += " ";
                    goodsuse += "";
                    inid += "";
                    remark += "";
                    rate += "";
                    rates += "";
                    did += "";
                }
                if (parseInt(Rate) + parseInt(Rates) > parseInt(TotalNoTax)) {
                    alert("付款金额大于总金额");
                    return;
                }
            }

            //付费时间
            var PayTime = $("#PayTime").val();
            if (PayTime == "")
            {
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
                    url: "InsertFK",
                    type: "Post",
                    data: {
                        CID: CID, RowNumber: rownumber, PayId: PayId, PayTime: PayTime, State: State, PaymentMenthod: PaymentMenthod, OrderContacts: OrderContacts, ProName: proname,
                        inid: inid, did: did,
                        Spec: spec, Units: units, Amount: amount, Supplier: supplier, UnitPriceNoTax: unitPriceNoTax, totalnotax: totalnotax, goodsuse: goodsuse, remark: remark, rate: rate, rates: rates
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
    window.parent.OpenDialog("选择货品信息", "../PPManage/ChangeBasic", 1100, 550);
}

function CheckSupplier(rowid) {
    RowId = rowid.id;
    ShowIframe1("选择供货商信息", "../PPManage/Supplier", 500, 350);
}


function addDDID(DID) { //增加货品信息行
    rowCount = document.getElementById("GXInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var strPID = $("#DID").val();
    $("#DID").val(strPID + "," + DID);
    $.ajax({
        url: "GetByOrderID",
        type: "post",
        data: { DID: DID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="GoodsUse' + rowCount + '">' + json[i].GoodsUse + '</lable> </td>';
                    html += '<td > <lable class="labSpecifications " id="Rate' + rowCount + '">' + json[i].SJFK + '</lable></td>';
                    html += '<td ><input type="text"  style="width:160px;" value=' + json[i].TotalNoTax + ' id="Rates' + rowCount + '"></td>';
                    html += '<td ><input type="text"  style="width:160px;" id="Remark' + rowCount + '"></td>';


                    html += '</tr>'
                    $("#GXInfo").append(html);

                }
            }
        }


    })
}

function addSupplier(SID) {
    //var rownumber = RowId.substr(8,RowId.length-8);
    $.ajax({
        url: "GetSupplier",
        type: "post",
        data: { SID: SID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#" + RowId).val(json[i].COMNameC);
                }
            }
        }
    });
}

function addBasicDetail(PID) { //增加货品信息行

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
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].MaterialNum + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text"  onblur="OnBlurAmount(this);" id="Amount' + rowCount + '"> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    html += '<td ><input type="text"  id="TotalNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text"  id="GoodsUse' + rowCount + '"> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }

                //$("#GXInfo").append(html);
            }
        }
    })
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

    $("#DetailInfo tr").removeAttr("class");
}


function selRow(curRow) {
    newRowID = curRow.id;
}
function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    var strRow = newCount.charAt(newCount.length - 1);

    var strU = "#UnitPriceNoTax" + strRow;
    var strUnitPrice = $(strU).text();
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);

    $("#TotalNoTax" + strRow).val(strTotal);

    GetAmount();
}
function GetAmount() {  //获取总数金额
    $("#AmountM").val("");
    var Amount = 0;
    var tbody = document.getElementById("GXInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
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
    var typeNames = ["RowNumber", "ProductID", "OrderContent", "SpecsModels", "OrderUnit", "OrderNum", "Price", "Subtotal", "Manufacturer", "Remark", "DID", 'OrderID', 'Count'];

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
    GetAmount();
    GetCount();

    $("#DetailInfo tr").removeAttr("class");
}





