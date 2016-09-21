
var shuliang = "";
$(document).ready(function () {
    var RKID = location.search.split('&')[0].split('=')[1];
    XGRK(RKID);
});


function XGRK(RKID) {
    $.ajax({
        url: "RKXQ",
        type: "post",
        data: { RKID: RKID },
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
                    html += '<td style="display:none"><lable class="labRowNumber " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td style="display:none" ><lable class="labRowNumber " id="Bak' + rowCount + '">' + json[i].Bak + '</lable> </td>';
                    html += '<td ><lable class="labRowNumber " id="DDID' + rowCount + '">' + json[i].SHID + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    html += '<td ><input type="text" id="SJAmount' + rowCount + '" value="' + json[i].SJAmount + '" style="width:150px;"/></td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                    var Amount = document.getElementById("SJAmount" + i).value;
                    shuliang += Amount;
                    if (i == json.length - 1) {
                        shuliang += "";
                    }
                    else {
                        shuliang += ",";
                    }

                }

                document.getElementById("Begin").value = json[0].Rkdate;
                document.getElementById("CKID").value = json[0].CKID;
                document.getElementById("RKInstructions").value = json[0].RKInstructions;
                document.getElementById("OrderContacts").value = json[0].Handler;
            }
        }


    })
}



function AddRK() {



    var str = document.getElementById("GXInfo").innerHTML;
    if (str == "") {
        alert("请选择商品");
    }
    else {
        var handler = document.getElementById("OrderContacts").value;
        var rkdate = document.getElementById("Begin").value;
        if (rkdate == "")
        {
            alert("入库库时间不可为空");
            return;
        }
     
        var RKID = location.search.split('&')[0].split('=')[1];
        var rownumber = "";
        var proname = "";
        var spec = "";
        var units = "";
        var amount = "";
        var supplier = "";
        var unitPricenotax = "";
        var totalnotax = "";
        // var use = "";
        var materialno = "";
        var shid = "";

        var sjamount = "";
        var bak = "";
        var did = "";
        var tbody = document.getElementById("GXInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var RowNumber = document.getElementById("RowNumber" + i).innerHTML;
            var ProName = document.getElementById("ProName" + i).innerHTML;
            var Spec = document.getElementById("Spec" + i).innerHTML;
            var Units = document.getElementById("Units" + i).innerHTML;
            var Amount = document.getElementById("Amount" + i).innerHTML;
            var Supplier = document.getElementById("Supplier" + i).innerHTML;
            var UnitPriceNoTax = document.getElementById("UnitPriceNoTax" + i).innerHTML;
            var TotalNoTax = document.getElementById("TotalNoTax" + i).innerHTML;
            //var Use = document.getElementById("Use" + i).innerHTML;
            var MaterialNO = document.getElementById("MaterialNO" + i).innerHTML;
            var SHID = document.getElementById("DDID" + i).innerHTML;
            var SJAmount = document.getElementById("SJAmount" + i).value;
            if (SJAmount == "")
            {
                alert("入库数量不可为空");
                return;
            }
            var Bak = document.getElementById("Bak" + i).innerHTML;
            var DID = document.getElementById("DID" + i).innerHTML;
            rownumber += RowNumber;
            proname += ProName;
            spec += Spec;
            units += Units;
            amount += Amount;
            supplier += Supplier;
            unitPricenotax += UnitPriceNoTax;
            totalnotax += TotalNoTax;
            //use += Use;
            sjamount += SJAmount;
            bak += Bak;
            did += DID;
            materialno += MaterialNO;
            if (i < tbody.rows.length - 1) {
                rownumber += ",";
                proname += ",";
                spec += ",";
                units += ",";
                amount += ",";
                supplier += ",";
                unitPricenotax += ",";
                totalnotax += ",";
                // use += ",";
                materialno += ",";
                sjamount += ",";
                bak += ",";
                did += ",";
            }
            else {
                rownumber += "";
                proname += "";
                spec += "";
                units += "";
                amount += "";
                supplier += "";
                unitPricenotax += "";
                totalnotax += "";
                // use += "";
                materialno += "";
                sjamount += "";
                bak += "";
                did += "";
            }
        }
        var rkinstructions = document.getElementById("RKInstructions").value;
        if (rkinstructions == "") {
            rkinstructions = "无";
        }
        var CKID = document.getElementById("CKID").value;
        if (CKID == "") {
            alert("入库库房不可为空");
            return;
        }
        isConfirm = confirm("确定要修改吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            $.ajax({
                url: "UpdateRK",
                type: "Post",
                data: {
                    rownumber: rownumber, shid: shid, RKID: RKID, proname: proname, spec: spec, units: units, amount: amount, supplier: supplier, unitPricenotax: unitPricenotax, totalnotax: totalnotax,
                    handler: handler, rkdate: rkdate, rkinstructions: rkinstructions, CKID: CKID, materialno: materialno, sjamount: sjamount, bak: bak, did: did, shuliang: shuliang
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