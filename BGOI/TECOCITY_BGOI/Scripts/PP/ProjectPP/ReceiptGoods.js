$(document).ready(function () {


    addBasicDetail(location.search.split('&')[0].split('=')[1]);


    $("#upLoad").click(function () {
        var texts = location.search.split('&')[0].split('=')[1];
        var PID = $("#SHID").val();
        if (texts != undefined) {
            //window.parent.OpenDialog("管理文件", "../PPManage/AddFile?PID=" + PID, 500, 300, '');
            //ShowIframe1("管理文件", "../PPManage/AddFile?PID=" + PID, 500, 300, '');
            ShowIframe1("管理文件", "../PPManage/InsertBiddingNew?PID=" + PID, 500, 300, '');
            
        }
        else {
            //window.parent.OpenDialog("管理文件", "../PPManage/AddFile?PID=" + PID, 500, 300, '');
            window.parent.OpenDialog("管理文件", "../PPManage/InsertBiddingNew?PID=" + PID, 500, 300, '');
            
        }
    });
});




function AddNewDD() {
    ShowIframe1("选择订单信息", "../PPManage/GetOrder", 1000, 450);
}

function addBasicDetail(DDID) { //增加货品信息行 //url: "GetByOrderID",


    $.ajax({
       
        url: "SelectGoodsDDID",
        type: "post",
        data: { DDID: DDID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                //交货方式
                var DeliveryMethod;
                //是否开发票
                var IsInvoice;
                //支付方式
                var PaymentMethod;
                //付款约定
                var PaymentAgreement;


                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>';
                    html += '<td ><input name="CP" type="checkbox" id="CPselect' + rowCount + '" /> </td>';
                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    //if (location.search.split('&')[0].split('=')[0] == "?texts") {
                    html += '<td ><lable class="labProductID " id="DDID' + rowCount + '">' + json[i].DDID + '</lable> </td>';
                    //}
                    //else {
                    //    html += '<td ><lable class="labProductID "   id="DDID' + rowCount + '">无</lable> </td>';
                    //}
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="SJAmount' + rowCount + '">' + json[i].ActualAmount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="COMNameC' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    html += '<td ><input type="text" id="SHAmount' + rowCount + '" style="width:70px;"/></td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }

            }
        }


    })
}
function XGSH(DDID) {
    $.ajax({
        url: "SelectSHDDID",
        type: "post",
        data: { DDID: DDID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            document.getElementById("ArrivalDate").value = json[0].ArrivalDate;
            document.getElementById("ArrivalDescription").value = json[0].ArrivalDescription;
            document.getElementById("ArrivalProcess").value = json[0].ArrivalProcess;
        }
    });
}
function AddSH() {


    var str = document.getElementById("GXInfo").innerHTML;
    if (str == "") {
        alert("请选择商品");
    }
    else {
        var arrivalprocess = document.getElementById("ArrivalProcess").value;

        var arrivaldescription = document.getElementById("ArrivalDescription").value;
        if (location.search.split('&')[0].split('=')[0] == "?SHID") {
            var shid = location.search.split('&')[0].split('=')[1];
        }
        else {
            var shid = $("#SHID").val();
        }

        var rownumber = "";
        var ddid = "";
        var did = "";
        var proname = "";
        var spec = "";
        var units = "";
        var amount = "";
        var supplier = "";
        var materialno = "";
        var shamount = "";
        var sjamount = "";
        var unitPriceNoTax = "";
        var totalNoTax = "";
        var tbody = document.getElementById("GXInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var RowNumber = document.getElementById("RowNumber" + i).innerHTML;
            var DDID = document.getElementById("DDID" + i).innerHTML;
            var DID = document.getElementById("DID" + i).innerHTML;
            var ProName = document.getElementById("ProName" + i).innerHTML;
            var Spec = document.getElementById("Spec" + i).innerHTML;
            var Units = document.getElementById("Units" + i).innerHTML;
            var Amount = document.getElementById("Amount" + i).innerHTML;
            var Supplier = document.getElementById("Supplier" + i).innerHTML;
            var MaterialNO = document.getElementById("MaterialNO" + i).innerHTML;

            var UnitPriceNoTax = document.getElementById("UnitPriceNoTax" + i).innerHTML;
            var TotalNoTax = document.getElementById("TotalNoTax" + i).innerHTML;
            var chk = document.getElementById('CPselect' + i);
            if (chk.checked) {
                var SHAmount = document.getElementById("SHAmount" + i).value;
            }
            else {
                var SHAmount = '0';
            }
            if (SHAmount == "") {
                alert("请输入收货数量");
                return;
            }
            if (parseInt(SHAmount) != SHAmount) {
                alert("数量填写整数"); return;
            }
            var SJAmount = document.getElementById("SJAmount" + i).innerHTML;
            if (parseInt(SHAmount) + parseInt(SJAmount) > parseInt(Amount)) {
                alert("收货数量超出总数量");
                return;
            }
            rownumber += RowNumber;
            ddid += DDID;
            did += DID;
            proname += ProName;
            spec += Spec;
            units += Units;
            amount += Amount;
            supplier += Supplier;
            materialno += MaterialNO;
            shamount += SHAmount;
            sjamount += SJAmount;
            unitPriceNoTax += UnitPriceNoTax;
            totalNoTax += TotalNoTax;
            if (i < tbody.rows.length - 1) {
                rownumber += ",";
                ddid += ",";
                did += ",";
                proname += ",";
                spec += ",";
                units += ",";
                amount += ",";
                supplier += ",";
                materialno += ",";
                shamount += ",";
                sjamount += ",";
                unitPriceNoTax += ",";
                totalNoTax += ",";
            }
            else {
                rownumber += "";
                ddid += "";
                did += "";
                proname += "";
                spec += "";
                units += "";
                amount += "";
                supplier += "";
                materialno += "";
                shamount += "";
                sjamount += "";
                unitPriceNoTax += "";
                totalNoTax += "";
            }


        }
        var arrivaldate = document.getElementById("ArrivalDate").value;
        if (arrivaldate == "") {
            alert("收货日期不可为空");
            return;
        }
    


        else {

            isConfirm = confirm("确定要收货吗")
            if (isConfirm == false) {
                return false;
            }
            else {
                $.ajax({
                    url: "InsertSH",
                    type: "Post",
                    data: {
                        rownumber: rownumber, ddid: ddid, shid: shid, proname: proname, spec: spec, materialno: materialno, units: units, amount: amount, supplier: supplier,
                        arrivalprocess: arrivalprocess, arrivaldate: arrivaldate, arrivaldescription: arrivaldescription, shamount: shamount, sjamount: sjamount, did: did,
                        unitPriceNoTax: unitPriceNoTax, totalNoTax: totalNoTax
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


}