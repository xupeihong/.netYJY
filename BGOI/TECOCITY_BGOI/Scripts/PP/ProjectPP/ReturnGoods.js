function AddNewDD() {
    ShowIframe1("选择物品信息", "../PPManage/GetSHID", 1000, 450);
}
$(document).ready(function () {
    if (location.search != "") {
        //if (location.search.split('&')[0].split('=')[0] == "?XGTHID") {
        //    $("#biaoti").css("display", "none")
        //    $("#biaoti1").css("display", "block")
        //    $("#danhao").css("display", "none")
        //    var THID = location.search.split('&')[0].split('=')[1];
        //    $.ajax({
        //        url: "SelectTHXQ",
        //        type: "post",
        //        data: { THID: THID },
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
        //                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
        //                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
        //                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
        //                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
        //                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
        //                    html += '<td ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
        //                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
        //                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
        //                    html += '<td ><lable class="labSpecifications " id="Use' + rowCount + '">' + json[i].Use + '</lable> </td>';
        //                    html += '</tr>'
        //                    $("#GXInfo").append(html);
        //                }

        //                document.getElementById("Begin").value = json[0].ReturnDate;

        //                document.getElementById("ReturnType").value = json[0].ReturnType;
        //                document.getElementById("ReturnMode").value = json[0].ReturnMode;
        //                document.getElementById("ReturnAgreement").value = json[0].ReturnAgreement;
        //                document.getElementById("TheProject").value = json[0].TheProject;
        //                document.getElementById("ReturnDescription").value = json[0].ReturnDescription;
        //                document.getElementById("OrderContacts").value = json[0].ReturnHandler;
        //            }
        //        }
        //    })
        //}

        if (location.search.split('&')[0].split('=')[0] == "?texts") {
            $("#select").css("display", "none");
            var texts = location.search.split('&')[0].split('=')[1];
            var mycars = new Array()
            mycars = texts.split(',');
            for (var i = 0; i < mycars.length; i++) {
                var DID = mycars[i];
                addBasicDetail(DID);
            }
        }
    }
});


function addBasicDetail(DID) { //增加货品信息行
    rowCount = document.getElementById("GXInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var strPID = $("#DID").val();
    $("#DID").val(strPID + "," + DID);
    $.ajax({
        url: "SelectSHGoodsDID",
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
                    html += '<td style="display:none" ><lable class="labProductID " id="DID' + rowCount + '">' + DID + '</lable> </td>';
                    html += '<td style="display:none" ><lable class="labProductID " id="SHID' + rowCount + '">' + json[i].SHID + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    //html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';

                    html += '<td ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    var a = parseInt(json[i].ActualAmount) - parseInt(json[i].SHStates);
                    html += '<td ><lable class="labSpecifications " id="ZAmount' + rowCount + '">' + a + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="THAmount' + rowCount + '">' + json[i].THAmount + '</lable> </td>';
                    html += '<td ><input type="text" id="Amount' + rowCount + '" value="' + json[i].ActualAmount + '" style="width:150px;"/></td>';

                    html += '</tr>'
                    $("#GXInfo").append(html);

                }
            }
        }
    })
}
function AddTH() {


    var str = document.getElementById("GXInfo").innerHTML;
    if (str == "") {
        alert("请选择商品");
    }
    else {
        var THID = $("#THID").val();
        var returnhandler = document.getElementById("OrderContacts").value;
        var returndate = document.getElementById("Begin").value;
        if (returndate == "")
        {
            alert("退货时间不可为空");
            return;
        }
       


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
        var thamount = "";
        var zamount = "";
        var tbody = document.getElementById("GXInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            if (location.search.split('&')[0].split('=')[0] == "?texts") {
                var SHID = document.getElementById("SHID" + i).innerHTML;
            }
            else {
                var SHID = "无";
            }
            var RowNumber = document.getElementById("RowNumber" + i).innerHTML;
            var ProName = document.getElementById("ProName" + i).innerHTML;
            var Spec = document.getElementById("Spec" + i).innerHTML;
            var XXID = document.getElementById("MaterialNO" + i).innerHTML;
            var Units = document.getElementById("Units" + i).innerHTML;
            var Amount = document.getElementById("Amount" + i).value;
            if (Amount == "")
            {
                alert("退货数量不可以为空");
                return;
            }
            var Supplier = document.getElementById("Supplier" + i).innerHTML;
            var UnitPriceNoTax = document.getElementById("UnitPriceNoTax" + i).innerHTML;
            var TotalNoTax = document.getElementById("TotalNoTax" + i).innerHTML;
            var DID = document.getElementById("DID" + i).innerHTML;
            var THAmount = document.getElementById("THAmount" + i).innerHTML;
            var ZAmount = document.getElementById("ZAmount" + i).innerHTML;

       

            rownumber += RowNumber;
            proname += ProName;
            spec += Spec;
            xxid += XXID;
            units += Units;
            amount += Amount;
            supplier += Supplier;
            unitPricenotax += UnitPriceNoTax;
            totalnotax += TotalNoTax;
            did += DID;
            thamount += THAmount;
            zamount += ZAmount;
            var a = parseInt(Amount) + parseInt(THAmount);
            var b = parseInt(ZAmount);
            if (a > b) {

                alert("退货量大于总量");
                return;
            }
            if (i < tbody.rows.length - 1) {
                rownumber += ",";
                proname += ",";
                spec += ",";
                xxid += ",";
                units += ",";
                amount += ",";
                supplier += ",";
                unitPricenotax += ",";
                totalnotax += ",";
                did += ",";
                thamount += ",";
                zamount += ",";
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
                totalnotax += "";
                did += "";
                thamount += "";
                zamount += "";
            }

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
        isConfirm = confirm("确定要添加吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            $.ajax({
                url: "InsertTH",
                type: "Post",
                data: {
                    rownumber: rownumber, THID: THID, proname: proname, spec: spec, units: units, amount: amount, supplier: supplier, unitPricenotax: unitPricenotax, totalnotax: totalnotax,
                    returnhandler: returnhandler, returndate: returndate, returntype: returntype, xxid: xxid,
                    returnmode: returnmode, returnagreement: returnagreement, theproject: theproject,
                    returndescription: returndescription, did: did, SHID: SHID, thamount: thamount
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