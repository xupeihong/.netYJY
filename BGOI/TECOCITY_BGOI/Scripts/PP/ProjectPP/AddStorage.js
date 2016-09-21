function AddNewDD() {
    ShowIframe1("选择物品信息", "../PPManage/GetSHID", 1000, 450);
}

$(document).ready(function () {

    var SHID = location.search.split('&')[0].split('=')[1];

    addBasicDetail(SHID);
});

//function addBasicDetails(DID) { //增加货品信息行
//    rowCount = document.getElementById("GXInfo").rows.length;
//    var CountRows = parseInt(rowCount) + 1;
//    var strPID = $("#DID").val();
//    $("#DID").val(strPID + "," + DID);
//    $.ajax({
//        url: "GetByOrderID",
//        type: "post",
//        data: { DID: DID },
//        dataType: "json",
//        success: function (data) {
//            var json = eval(data.datas);
//            if (json.length > 0) {
//                for (var i = 0; i < json.length; i++) {
//                    rowCount = document.getElementById("GXInfo").rows.length;
//                    var CountRows = parseInt(rowCount) + 1;
//                    var html = "";
//                    html = '<tr>'
//                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
//                    html += '<td style="display:none" ><lable class="labRowNumber " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
//                    html += '<td ><lable class="labRowNumber " id="DDID' + rowCount + '">' + json[i].DDID + '</lable> </td>';
//                    html += '<td ><lable class="labProductID "   id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
//                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
//                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';
//                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
//                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
//                    html += '<td ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
//                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
//                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
//                    html += '<td ><lable class="labSpecifications " id="Use' + rowCount + '">' + json[i].GoodsUse + '</lable> </td>';
//                    html += '<td ><lable class="labSpecifications " id="RKState' + rowCount + '">' + json[i].RKState + '</lable> </td>';
//                    html += '<td ><input type="text" id="SJAmount' + rowCount + '" style="width:80px;"/></td>';
//                    html += '</tr>'
//                    $("#GXInfo").append(html);

//                }
//            }
//        }


//    })
//}

//收货
function addBasicDetail(SHID) { //增加货品信息行


    $.ajax({
        url: "SelectSHXX",
        type: "post",
        data: { SHID: SHID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>'
                    html += '<td ><input name="CP" type="checkbox" id="CPselect' + rowCount + '" /> </td>';
                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td style="display:none" ><lable class="labProductID " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';

                    html += '<td ><lable class="labProductID " id="DDID' + rowCount + '">' + json[i].SHID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';

                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    var a = parseInt(json[i].ActualAmount) - parseInt(json[i].THAmount);
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + a + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Supplierss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td style="display:none" ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    var TotalNoTax = a * parseInt(json[i].UnitPriceNoTax);
                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + TotalNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="RKState' + rowCount + '">' + json[i].SHStates + '</lable> </td>';
                    html += '<td ><input type="text" id="SJAmount' + rowCount + '"  style="width:80px;"/></td>';
                    html += '</tr>'
                     if (a > 0) {
                        $("#GXInfo").append(html);
                    }


                }
            }
        }
    })
}
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
                    html += '<td ><lable class="labRowNumber " id="DDID' + rowCount + '">' + json[i].DDID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';

                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';

                    html += '<td ><lable class="labSpecifications " id="Use' + rowCount + '">' + json[i].Bak + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);

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
        if (rkdate == "") {
            alert("入库时间不可为空");
            return;
        }
        var rkinstructions = document.getElementById("RKInstructions").value;
        if (rkinstructions == "") {
            rkinstructions = "无";
        }
        var RKID = $("#RKID").val();


        if (location.search != "") {
            var SHID = location.search.split('&')[0].split('=')[1];
        }
        else {
            var SHID = "无";
        }


        var rownumber = "";
        var proname = "";
        var spec = "";
        var units = "";
        var amount = "";
        var supplier = "";
        var unitPricenotax = "";
        var totalnotax = "";
        var did = "";
        var materialno = "";
        var shid = "";
        var sjamount = "";
        var rkstate = "";
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
            var DID = document.getElementById("DID" + i).innerHTML;
            var MaterialNO = document.getElementById("MaterialNO" + i).innerHTML;
            var SHID = document.getElementById("DDID" + i).innerHTML;

            //var SJAmount = document.getElementById("SJAmount" + i).value;
            var chk = document.getElementById('CPselect' + i);
            if (chk.checked) {
                var SJAmount = document.getElementById("SJAmount" + i).value;
            }
            else {
                var SJAmount = '0';
            }
            if (SJAmount == "") {
                alert("入库数量不可为空");
                return;
            }

            var RKState = document.getElementById("RKState" + i).innerHTML;
            rownumber += RowNumber;
            proname += ProName;
            spec += Spec;
            units += Units;
            amount += Amount;
            supplier += Supplier;
            unitPricenotax += UnitPriceNoTax;
            totalnotax += TotalNoTax;
            did += DID;
            sjamount += SJAmount;
            rkstate += RKState;
            materialno += MaterialNO;
            var a = parseInt(SJAmount) + parseInt(RKState);
            if (Amount < a) {
                alert("入库数量超出总数量");
                return;
            }
            if (i < tbody.rows.length - 1) {
                rownumber += ",";
                proname += ",";
                spec += ",";
                units += ",";
                amount += ",";
                supplier += ",";
                unitPricenotax += ",";
                totalnotax += ",";
                did += ",";
                materialno += ",";
                sjamount += ",";
                rkstate += ",";
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
                did += "";
                materialno += "";
                sjamount += "";
                rkstate += "";
            }
        }
        var CKID = document.getElementById("CKID").value;
        if (CKID == "") {
            alert("入库库房不可为空");
            return;
        }
        isConfirm = confirm("确定要添加吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            $.ajax({
                url: "InsertRK",
                type: "Post",
                data: {
                    rownumber: rownumber, SHID: SHID, RKID: RKID, proname: proname, spec: spec, units: units, amount: amount, supplier: supplier, unitPricenotax: unitPricenotax, totalnotax: totalnotax,
                    handler: handler, rkdate: rkdate, rkinstructions: rkinstructions, CKID: CKID, did: did, materialno: materialno, sjamount: sjamount, rkstate: rkstate
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


}