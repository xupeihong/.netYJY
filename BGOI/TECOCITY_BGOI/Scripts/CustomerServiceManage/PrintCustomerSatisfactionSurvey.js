
$(document).ready(function () {
    jq();//加载数据
    // 打印 
    $("#btnPrint").click(function () {
        //alert(1);
        document.getElementById("btnPrint").className = "Noprint";
        $("#ReportContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#ReportContent").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });
})

function jq() {
    var Info = "";
    if (location.search != "") {
        Info = location.search.split('&')[0].split('=')[1];
    }
    var DCID = Info;
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "GetCustomerSatisfactionSurveyList",
        type: "Post",
        data: {
            Info: Info
        },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labOrderForm' + rowCount + ' " id="OrderForm' + rowCount + '">' + json[i].OrderForm + '</lable> </td>';
                    html += '<td ><lable class="labAmount' + rowCount + ' " id="Amount' + rowCount + '">' + json[i].Num + '</lable> </td>';
                    html += '<td ><lable class="labContractDate' + rowCount + ' " id="ContractDate' + rowCount + '">' + json[i].OrderDate.substr(0,9) + '</lable> </td>';
                    //html += '<td colspan="2"><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
                    html += '<td style="display:none;"><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows += 1;
                }
            }
        }
    });

    $.ajax({
        url: "UpSurveyList",
        type: "post",
        data: { DCID: DCID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            for (var i = 0; i < json.length; i++) {
                var ProductQuality = json[i].ProductQuality;
                if (ProductQuality == "0") {
                    $(':radio[name=ProductQuality][value=0]').attr('checked', true);
                } else if (ProductQuality == "1") {
                    $(':radio[name=ProductQuality][value=1]').attr('checked', true);
                } else if (ProductQuality == "2") {
                    $(':radio[name=ProductQuality][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=ProductQuality][value=3]').attr('checked', true);
                }

                var ProductQrice = json[i].ProductQrice;
                if (ProductQrice == "0") {
                    $(':radio[name=ProductQrice][value=0]').attr('checked', true);
                } else if (ProductQrice == "1") {
                    $(':radio[name=ProductQrice][value=1]').attr('checked', true);
                } else if (ProductQrice == "2") {
                    $(':radio[name=ProductQrice][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=ProductQrice][value=3]').attr('checked', true);
                }

                var ProductDelivery = json[i].ProductDelivery;
                if (ProductDelivery == "0") {
                    $(':radio[name=ProductDelivery][value=0]').attr('checked', true);
                } else if (ProductDelivery == "1") {
                    $(':radio[name=ProductDelivery][value=1]').attr('checked', true);
                } else if (ProductDelivery == "2") {
                    $(':radio[name=ProductDelivery][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=ProductDelivery][value=3]').attr('checked', true);
                }

                var CustomerServiceSurvey = json[i].CustomerServiceSurvey;
                if (CustomerServiceSurvey == "0") {
                    $(':radio[name=CustomerServiceSurvey][value=0]').attr('checked', true);
                } else if (CustomerServiceSurvey == "1") {
                    $(':radio[name=CustomerServiceSurvey][value=1]').attr('checked', true);
                } else if (CustomerServiceSurvey == "2") {
                    $(':radio[name=CustomerServiceSurvey][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=CustomerServiceSurvey][value=3]').attr('checked', true);
                }

                var SupplySurvey = json[i].SupplySurvey;
                if (SupplySurvey == "0") {
                    $(':radio[name=SupplySurvey][value=0]').attr('checked', true);
                } else if (SupplySurvey == "1") {
                    $(':radio[name=SupplySurvey][value=1]').attr('checked', true);
                } else if (SupplySurvey == "2") {
                    $(':radio[name=SupplySurvey][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=SupplySurvey][value=3]').attr('checked', true);
                }

                var LeakSurvey = json[i].LeakSurvey;
                if (LeakSurvey == "0") {
                    $(':radio[name=LeakSurvey][value=0]').attr('checked', true);
                } else if (LeakSurvey == "1") {
                    $(':radio[name=LeakSurvey][value=1]').attr('checked', true);
                } else if (LeakSurvey == "2") {
                    $(':radio[name=LeakSurvey][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=LeakSurvey][value=3]').attr('checked', true);
                }

                var AgencySales = json[i].AgencySales;
                if (AgencySales == "0") {
                    $(':radio[name=AgencySales][value=0]').attr('checked', true);
                } else if (AgencySales == "1") {
                    $(':radio[name=AgencySales][value=1]').attr('checked', true);
                } else if (AgencySales == "2") {
                    $(':radio[name=AgencySales][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=AgencySales][value=3]').attr('checked', true);
                }

                var AgencyConsultation = json[i].AgencyConsultation;
                if (AgencyConsultation == "0") {
                    $(':radio[name=AgencyConsultation][value=0]').attr('checked', true);
                } else if (AgencyConsultation == "1") {
                    $(':radio[name=AgencyConsultation][value=1]').attr('checked', true);
                } else if (AgencyConsultation == "2") {
                    $(':radio[name=AgencyConsultation][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=AgencyConsultation][value=3]').attr('checked', true);
                }

                var AgencySpareParts = json[i].AgencySpareParts;
                if (AgencySpareParts == "0") {
                    $(':radio[name=AgencySpareParts][value=0]').attr('checked', true);
                } else if (AgencySpareParts == "1") {
                    $(':radio[name=AgencySpareParts][value=1]').attr('checked', true);
                } else if (AgencySpareParts == "2") {
                    $(':radio[name=AgencySpareParts][value=2]').attr('checked', true);
                } else {
                    $(':radio[name=AgencySpareParts][value=3]').attr('checked', true);
                }
            }
        }
    })



}
function deleteTr(date) {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var newCount = date.id;
        var strRow = newCount.charAt(newCount.length - 1);
        // $("#DetailInfo" + strRow).parent().parent().remove();
        $("#DetailInfo" + strRow).closest('tr').remove();
    }
}
