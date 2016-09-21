$(document).ready(function () {
 
    if ( location.search.split('&')[0].split('=')[0] == "?SHIDXQ") {
 
        var SHID = location.search.split('&')[0].split('=')[1];
   
        addBasicDetail(SHID);
        //XGSH(SHID);
    }
});
function addBasicDetail(DDID) { //增加货品信息行
    //rowCount = document.getElementById("GXInfo").rows.length;
    //var CountRows = parseInt(rowCount) + 1;
    var strPID = $("#DDID").val();
    $("#DDID").val(strPID + "," + DDID);
    $.ajax({
        url: "SelectSHXX",
        type: "post",
        data: { SHID: DDID },
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


                //if (json[0].DeliveryMethod == 0)
                //    DeliveryMethod = "物料派送";
                //else if (json[0].DeliveryMethod == 1)
                //    DeliveryMethod = "供货方送货";
                //else if (json[0].DeliveryMethod == 2)
                //    DeliveryMethod = "采购费自提";

                //if (json[0].IsInvoice == 0)
                //    IsInvoice = "是";
                //else
                //    IsInvoice = "否";

                //if (json[0].PaymentMethod == 0)
                //    PaymentMethod = "现金";
                //else if (json[0].PaymentMethod == 1)
                //    PaymentMethod = "银行转账";
                //else if (json[0].PaymentMethod == 2)
                //    PaymentMethod = "在线支付";

                //if (json[0].PaymentAgreement == 0)
                //    PaymentAgreement = "货到付款";
                //else if (json[0].PaymentAgreement == 1)
                //    PaymentAgreement = "贷款预付";
                //else if (json[0].PaymentAgreement == 2)
                //    PaymentAgreement = "分期付款";

                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>';
                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="DDID' + rowCount + '">' + json[i].INID + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].MaterialNO + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].ActualAmount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }

                document.getElementById("ArrivalDate").innerHTML = json[0].ArrivalDate;
                document.getElementById("ArrivalDescription").innerHTML = json[0].ArrivalDescription;
                document.getElementById("ArrivalProcess").innerHTML = json[0].ArrivalProcess;
            }
        }


    })
}
function XGSH(DDID) {
    $.ajax({
        url: "SelectSHDDID",
        type: "post",
        data: { SHID: DDID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            document.getElementById("ArrivalDate").innerHTML = json[0].ArrivalDate;
            document.getElementById("ArrivalDescription").innerHTML = json[0].ArrivalDescription;
            document.getElementById("ArrivalProcess").innerHTML = json[0].UserName;

        }
    });
}