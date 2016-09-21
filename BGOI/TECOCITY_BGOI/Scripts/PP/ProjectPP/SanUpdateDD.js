﻿$(document).ready(function () {

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

        var str = document.getElementById("GXInfo").innerHTML;
        if (str == "") {
            alert("请选择商品");
            return;
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
        //请购人
        var OrderContacts = $("#OrderContacts").val();


        var rownumber = "";
        var proname = "";
        var spec = "";
        var units = "";
        var amount = "";
        var supplier = "";
        var unitpricenotax = "";
        var totalnotax = "";
        var price = "";
        var totaltax = "";
        var pids = "";
        var tbody = document.getElementById("GXInfo");

        for (var i = 0; i < tbody.rows.length; i++) {



            if (location.search.split('&')[0].split('=')[1] != undefined) {
                //请购单id
                var CID = location.search.split('&')[0].split('=')[1];
            }
            else {
                var CID = "无";
            }


            //序号
            var RowNumber = document.getElementById("RowNumber" + i).innerHTML;
            //商品名称
            var ProName = document.getElementById("ProName" + i).innerHTML;
            //商品型号
            var Spec = document.getElementById("Spec" + i).innerHTML;
            //单位
            var Units = document.getElementById("Units" + i).innerHTML;
            //数量
            var Amount = document.getElementById("Amount" + i).value;
            if (Amount == "") {
                alert("数量不可为空");
                return;
            }
            //供货商
            var Supplier = document.getElementById("Supplier" + i).innerHTML;

            //不含税单价
            var UnitPriceNoTax = document.getElementById("UnitPriceNoTax" + i).value;
            if (UnitPriceNoTax == "") {
                alert("单价不可为空");
                return;
            }
            //不含税总价
            var TotalNoTax = document.getElementById("TotalNoTax" + i).value;
            if (TotalNoTax == "") {
                alert("总价不可为空");
                return;
            }
            //单价
            var Price = document.getElementById("Price" + i).value;
            if (Price == "") {
                alert("单价不可为空");
                return;
            }
            //总价
            var TotalTax = document.getElementById("TotalTax" + i).value;
            if (TotalTax == "") {
                alert("总价不可为空");
                return;
            }

            rownumber += RowNumber;
            proname += ProName;
            spec += Spec;
            units += Units;
            amount += Amount;
            supplier += Supplier;
            unitpricenotax += UnitPriceNoTax;
            totalnotax += TotalNoTax;
            price += Price;
            totaltax += TotalTax;
     


            if (i < tbody.rows.length - 1) {
                rownumber += ",";
                proname += ",";
                spec += ",";
                units += ",";
                amount += ",";
                supplier += ",";
                unitpricenotax += ",";
                totalnotax += ",";
                price += ",";
                totaltax += ",";
             
            }
            else {
                rownumber += " ";
                proname += " ";
                spec += " ";
                units += "";
                amount += " ";
                supplier += " ";
                unitpricenotax += "";
                totalnotax += "";
                price += "";
                totaltax += "";
             
            }
        }
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
        isConfirm = confirm("确定要修改吗")
        if (isConfirm == false) {
            return false;
        }
        else {

            $.ajax({
                url: "SanUpdateDDS",
                type: "Post",
                data: {
                    rownumber: rownumber, ddid: DDID, cid: CID, orderdate: OrderDate, begin: Begin, deliverymethod: DeliveryMethod, isinvoice: IsInvoice, paymentmethod: PaymentMethod, ordercontacts: OrderContacts, paymentagreement: PaymentAgreement, contractno: ContractNO, theproject: TheProject, ordercontacts: OrderContacts, tasknum: TaskNum, businesstypes: BusinessTypes, proname: proname, spec: spec, units: units, amount: amount, supplier: supplier, unitpricenotax: unitpricenotax, totalnotax: totalnotax, price: price, totaltax: totaltax
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
        if (text == "") {
            alert("请选择业务类型");
        }
    });
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
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="GXInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="PIDS' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><input type="text" style="width:100px" value="' + json[i].Amount + '"  onblur="OnBlurAmount(this);" id="Amount' + rowCount + '"> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Suppliersss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td  style="display:none"><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPriceNoTax + '"  onblur="OnBlurAmount(Amount' + rowCount + ');"   id="UnitPriceNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"  value="' + json[i].TotalNoTax + '"   id="TotalNoTax' + rowCount + '"> </td>';

                    html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPrice + '"    id="Price' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"  value="' + json[i].Total + '"  id="TotalTax' + rowCount + '"> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);

                }
                document.getElementById("OrderDate").value = json[0].OrderDate;
                document.getElementById("xzrwlx").value = json[0].BusinessTypes;
                document.getElementById("TaskNum").value = json[0].PID;
                if (json[0].PurchaseDate != "0001/1/1 0:00:00") {
                    document.getElementById("Begin").value = json[0].PurchaseDate;
                }
                document.getElementById("DeliveryMethod").value = json[0].DeliveryMethod;
                document.getElementById("IsInvoice").value = json[0].IsInvoice;
                document.getElementById("PaymentMethod").value = json[0].PaymentMethod;
                document.getElementById("PaymentAgreement").value = json[0].PaymentAgreement;
                document.getElementById("ContractNO").value = json[0].ContractNO;
                document.getElementById("TheProject").value = json[0].TheProject;
                document.getElementById("OrderContacts").value = json[0].OrderContacts;
            }

        }

    });


});
function AddNewBasic() {
    ShowIframe1("选择货品信息", "../PPManage/ChangeBasic?id", 800, 500);
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
                        CountRows = CountRows + 1;
                        rowCount += 1;
                    }
                }
            }
        })



    }
}
function DelRow() {
    var tbodyID = "GXInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "ProName", "Spec", "PIDS", "Units", "Amount", "Suppliersss", "Supplier", "UnitPriceNoTax", "TotalNoTax", 'Price', 'TotalTax'];

    if (newRowID != "")
        rowIndex = newRowID.replace(tbodyID, '');
    if (rowIndex != -1) {
        document.getElementById(tbodyID).deleteRow(rowIndex);

        if (rowIndex < document.getElementById(tbodyID).rows.length) {
            for (var i = rowIndex; i < document.getElementById(tbodyID).rows.length; i++) {
                var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                tr.id = tbodyID + i;
                //tr.childNodes[0].innerHTML = parseInt(i) + 1;

                for (var j = 0; j < tr.childNodes.length; j++) {
                    var td = tr.childNodes[j];
                    td.childNodes[0].id = typeNames[j] + i;
                    td.childNodes[0].name = typeNames[j] + i;
                }
                $("#RowNumber" + i).html(parseInt(i) + 1);
            }
        }
        if (document.getElementById(tbodyID).rows.length > 0) {

            if (rowIndex == document.getElementById(tbodyID).rows.length)
                selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
            else
                selRow(document.getElementById(tbodyID + rowIndex), '');
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

function Getid(id) {//获取任务单号
    document.getElementById("TaskNum").value = id;
}

function OnBlurAmount(rowcount) //求金额和
{

    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    var value = newCount.replace(/[^0-9]/ig, "");
    var strRow = newCount.charAt(newCount.length - 1);

    var strU = "#UnitPriceNoTax" + value;
    var strUnitPrice = $(strU).val();

    var strP = "#Price" + value;
    var strPrice2 = $(strP).val();

    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    var TotalTax = parseFloat(Count) * parseFloat(strPrice2);

    $("#TotalNoTax" + value).val(strTotal);
    $("#TotalTax" + value).val(TotalTax);

    //GetAmount();
}