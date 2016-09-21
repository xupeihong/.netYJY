var curPage = 1;
var OnePageCount = 30;
var PID;
var RelenvceID;
var Type = "";
var ProID;
var Pname;
var start;
var end;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#pageContent").width() - 30);
    $("#StaTab").height($("#pageContent").height() - 300);
    $("#BotomTab").height($("#pageContent").height() - 300);
    var str1 = "<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">" +
       "<tr  class=\"left\"><td rowspan=\"2\"  style=\"width:10%\">部门</td><td rowspan=\"2\" style=\"width:10%\">当月</td>" +
       "<td  rowspan=\"2\" style=\"width:5%\">上月</td>" +
        "<td colspan=\"2\" style=\"width:5%\">环比</td><td  rowspan=\"2\" style=\"width:5%\">去年同期</td>" +
         "<td colspan=\"2\" style=\"width:5%\">环比</td>" +
       "<td rowspan=\"2\" style=\"width:5%\">占全年合同累计额%</td><td rowspan=\"2\" style=\"width:5%\">备注</td></tr>";
    str1 += "<tr class=\"staleft\" >" +
      "<td style=\"width:5%\">变动</td><td style=\"width:5%\">变动%</td>" +
       "<td style=\"width:5%\">变动</td><td style=\"width:5%\">变动%</td></tr>";
    str1 += "<tr class=\"staleft\"  style=\"color:red\">" + "<td style=\"width:4%\">合计</td>" +
        "<td style=\"width:10%\"></td>" +
         "<td style=\"width:10%\"></td>" +
          "<td style=\"width:10%\"></td>" +
           "<td style=\"width:10%\"></td>" +
                "<td style=\"width:10%\"></td>" +
                  "<td style=\"width:10%\"></td>" +
           "<td style=\"width:10%\"></td>" +
                "<td style=\"width:10%\"></td>" +
                 "<td style=\"width:10%\">" +
    "</tr></table>"
   // $("#StaTab").html(str1);
    // $("#Sign").html("汇总说明:");
    //jq();


    LoadContractStaticalAnalysis();
    // LoadContractNowStaticalAnalysis();
    $('#CX').click(function () {
        //var Pname = $('#Pname').val();
        var start = $('#StartDate').val();
        var end = $('#EndDate').val();

        $.ajax({
            url: "GetContractStatisticalAnalysisTable",
            type: "post",
            data: { data1: start, data2: end },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    //alert(data.strSb);
                    $("#StaTab").html(data.strSb);
                    // $("#Sign").html(data.strSign);
                }
                else {
                    return;
                }
            }
        });
    })


})


function LoadContractStaticalAnalysis() {
    $.ajax({
        url: "GetReceivableAccountTable",
        type: "post",
        data: { data1: start, data2: end },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                $("#StaTab").html(data.strSb);
                $("#BotomTab").html(data.BotomTab);
                $("#BotomTab2").html(data.LJTab);
            }
            else {
                return;
            }
        }
    });

}


function LoadContractNowStaticalAnalysis() {
    $.ajax({
        url: "GetContractNowStatisticalAnalysisTable",
        type: "post",
        data: { data1: start, data2: end },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                $("#BotomTab").html(data.BotomTab);
                //$("#Sign").html(data.strSign);
            }
            else {
                return;
            }
        }
    });
}