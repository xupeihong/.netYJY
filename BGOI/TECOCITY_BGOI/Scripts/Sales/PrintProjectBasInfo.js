$(document).ready(function () {
    if (location.search != "") {
        ID = location.search.split('&')[0].split('=')[1];
    }
    $("#PrintArea table td").attr("style", "border: 1px solid #000;");
    //LoadProjectDetial();
   // var tableHtml = $("#Htable").val();
    //$("#PrintArea").append(tableHtml);
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";
       // $("#PrintArea").attr("style", "width: 100%;margin-top:10px;border: 3px solid #000 ！important;")

        $("#PrintArea").attr("style", "width:100%;margin-top:10px;page-break-after: always;height :1000px;")
        $("table td").attr("style", "border: 1px solid #000")
        pagesetup("0.3", "0.3", "0.3", "0.3");
        window.print();
        //wb.ExecWB(7, 1)
        document.getElementById("btnPrint").className = "btn";
      //  $("#PrintArea").attr("style", "margin: 0 auto; width: 90%;margin-top:10px;page-break-after: always;height :1000px;")
    });

});
var curPage = 1;
var OnePageCount = 15;
function LoadProjectDetial() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetPrintProjectDetail",
        type: "post",
        data: { ID: ID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length <= 6) {
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '">'
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td style="display:none;" ><lable class="labAmount' + rowCount + '  id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable></td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].XID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                        html += '<td ><lable class="labAmount' + rowCount + '  id="Unit' + rowCount + '">' + json[i].Unit + '</lable></td>';
                        html += '<td ><lable class="labAmount' + rowCount + '  id="Amount' + rowCount + '">' + json[i].Amount + '</lable></td>';
                        html += '<td ><lable class="labAmount' + rowCount + '  id="Remark' + rowCount + '">' + json[i].Remark + '</lable></td>';
                        html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                        CountRows += 1;
                    }


                }
            }
            else
            {
                var count = json.length % 6;
                if (count > 0)
                    count = json.length / 6 + 1;
                else
                    count = json.length / 6;
                var Title = $("#PrintArea").innerHTML;
                rowCount = document.getElementById("DetailInfo").rows.length;
                var CountRows = parseInt(rowCount) + 1;
                for (var i = 0; i < count; i++)
                {
                  //  sb1 = new StringBuilder();
                    var a = 6 * i;
                    var length = 6 * (i + 1);
                    if (length > json.length)
                        length = 6 * i + json.length % 6;
                    for (var j = a; j < length; j++)
                    {
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '">'
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td style="display:none;" ><lable class="labAmount' + rowCount + '  id="ProductID' + rowCount + '">' + json[j].ProductID + '</lable></td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProductID' + rowCount + '">' + json[j].XID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[j].OrderContent + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[j].Specifications + '</lable> </td>';
                        html += '<td ><lable class="labAmount' + rowCount + '  id="Unit' + rowCount + '">' + json[j].Unit + '</lable></td>';
                        html += '<td ><lable class="labAmount' + rowCount + '  id="Amount' + rowCount + '">' + json[j].Amount + '</lable></td>';
                        html += '<td ><lable class="labAmount' + rowCount + '  id="Remark' + rowCount + '">' + json[j].Remark + '</lable></td>';
                        html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[j].PID + '</lable> </td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                        CountRows += 1;

                    }
                    if ((length - a) < 6)
                    {


                    }
                    html += Title;
                }
            }
        }
    })
}

function LoadShipments() {//GetShipment
    rowCount = document.getElementById("Shipment").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetOrdersInfo",
        type: "post",
        data: { Orderid: ID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            //if (json.length > 0) {


            //    for (var i = 0; i < json.length; i++) {
            var html = "";
            html = '<tr  id ="DetailInfo' + rowCount + '">'
            html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">合计：人民币（大写）   </lable> </td>';
            html += '<td colspan="3"><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].Total + '</lable> </td>';
            html += '</tr>'

            html = '<tr  id ="DetailInfo' + rowCount + '">'
            html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">付款方式：   </lable> </td>';
            html += '<td colspan="3"><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].PayWay + '</lable> </td>';
            html += '</tr>'
            html = '<tr  id ="DetailInfo' + rowCount + '">'
            html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">产品保修期：   </lable> </td>';
            html += '<td colspan="3"><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].Guarantee + '</lable> </td>';
            html += '</tr>'
            html += '<tr rowspan="2">'
            html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">供方</lable> </td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">单  位(章)</lable> </td>';
            html += '<td ><lable class="labSpec' + rowCount + '  id="Amount' + rowCount + '">' + json[i].Provider + '</lable></td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">负责人</lable> </td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].ProvidManager + '</lable> </td>';
            html += '</tr>'
            html += '<tr>'
            html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">需方</lable> </td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">单  位(章)</lable> </td>';
            html += '<td ><lable class="labSpec' + rowCount + '  id="Amount' + rowCount + '">' + json[i].Demand + '</lable></td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">负责人</lable> </td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].DemandManager + '</lable> </td>';
            html += '</tr>'
            $("#Shipment").append(html);

            //    }


            //}
        }
    })
}