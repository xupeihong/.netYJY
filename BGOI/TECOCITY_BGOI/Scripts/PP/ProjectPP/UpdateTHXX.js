
var shuliang = "";
$(document).ready(function () {
    if (location.search != "") {
        var THID = location.search.split('&')[0].split('=')[1];
        $.ajax({
            url: "SelectTHXQ",
            type: "post",
            data: { THID: THID },
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
                        html += '<td style="display:none"><lable class="labOrderContent " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labOrderContent " id="Bak' + rowCount + '">' + json[i].Bak + '</lable> </td>';
                        html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                        html += '<td ><input type="text" id="Amount' + rowCount + '" value="' + json[i].Amount + '" style="width:150px;"/></td>';
                        //html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                        html += '</tr>'
                        $("#GXInfo").append(html);
                        var Amount = document.getElementById("Amount" + i).value;
                        shuliang += Amount;
                        if (i == json.length - 1) {
                            shuliang += "";
                        }
                        else {
                            shuliang += ",";
                        }
                    }

                    document.getElementById("Begin").value = json[0].ReturnDate;
                    document.getElementById("ReturnType").value = json[0].ReturnType;
                    document.getElementById("ReturnMode").value = json[0].ReturnMode;
                    document.getElementById("ReturnAgreement").value = json[0].ReturnAgreement;
                    document.getElementById("TheProject").value = json[0].TheProject;
                    document.getElementById("ReturnDescription").value = json[0].ReturnDescription;
                    document.getElementById("OrderContacts").value = json[0].ReturnHandler;
                }
            }
        })
    }
});

function AddTH() {


    var str = document.getElementById("GXInfo").innerHTML;
    if (str == "") {
        alert("请选择商品");
    }
    else {
        var THID = location.search.split('&')[0].split('=')[1];
        var returnhandler = document.getElementById("OrderContacts").value;
      


        var rownumber = "";
        var proname = "";
        var spec = "";
        var xxid = "";
        var units = "";
        var amount = "";
        var supplier = "";
        var unitPricenotax = "";
        var totalnotax = "";
        var use = "";
        var did = "";
        var shid = "";
        var bak = "";
        var tbody = document.getElementById("GXInfo");
        for (var i = 0; i < tbody.rows.length; i++) {

            var SHID = location.search.split('&')[0].split('=')[1];

            var RowNumber = document.getElementById("RowNumber" + i).innerHTML;
            var ProName = document.getElementById("ProName" + i).innerHTML;
            var Spec = document.getElementById("Spec" + i).innerHTML;
            var XXID = document.getElementById("MaterialNO" + i).innerHTML;
            var Units = document.getElementById("Units" + i).innerHTML;
            var Amount = document.getElementById("Amount" + i).value;
            if (Amount == "")
            {
                alert("退货数量不可为空");
                return;
            }
            var Supplier = document.getElementById("Supplier" + i).innerHTML;
            var UnitPriceNoTax = document.getElementById("UnitPriceNoTax" + i).innerHTML;
            var DID = document.getElementById("DID" + i).innerHTML;
            var Bak = document.getElementById("Bak" + i).innerHTML;


            rownumber += RowNumber;
            proname += ProName;
            spec += Spec;
            xxid += XXID;
            units += Units;
            amount += Amount;
            supplier += Supplier;
            unitPricenotax += UnitPriceNoTax;
            did += DID;
            bak += Bak;
            if (i < tbody.rows.length - 1) {
                rownumber += ",";
                proname += ",";
                spec += ",";
                xxid += ",";
                units += ",";
                amount += ",";
                supplier += ",";
                unitPricenotax += ",";
                did += ",";
                bak += ",";

            }
            else {
                rownumber += "";
                proname += "";
                spec += "";
                xxid += "";
                units += "";
                amount += "";
                supplier += "";
                unitPricenotax += "";
                did += "";
                bak += "";
            }
        }
        var returndate = document.getElementById("Begin").value;
        if (returndate == "") {
            alert("退货时间不可为空");
            return;
        }
        var returntype = document.getElementById("ReturnType").value;
        if (returntype == "") {
            alert("退货类型不可为空");
            return;
        }
        var returnmode = document.getElementById("ReturnMode").value;
        if (returnmode == "") {
            alert("退货方式不可为空");
            return;
        }
        var returnagreement = document.getElementById("ReturnAgreement").value;
        if (returnagreement == "") {
            returnagreement = "无";
        }
        var theproject = document.getElementById("TheProject").value;
        if (theproject == "") {
            theproject = "无";
        }
        var returndescription = document.getElementById("ReturnDescription").value;
        if (returndescription == "") {
            returndescription = "无";
        }
        isConfirm = confirm("确定要修改吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            $.ajax({
                url: "UpdateTH",
                type: "Post",
                data: {
                    THID: THID,
                    returnhandler: returnhandler, returndate: returndate, returntype: returntype,
                    returnmode: returnmode, returnagreement: returnagreement, theproject: theproject,
                    returndescription: returndescription, amount: amount, did: did, shuliang: shuliang, bak: bak
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


}