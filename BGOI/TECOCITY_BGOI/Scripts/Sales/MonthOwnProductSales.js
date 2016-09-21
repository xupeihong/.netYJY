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
    LoadMonthOwnProductSalesTable();
    

})


function LoadMonthOwnProductSalesTable() {
    $.ajax({
        url: "GetMonthOwnProductSalesTable",
        type: "post",
        data: { data1: start, data2: end },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                $("#StaTab").html(data.strSb);
                $("#BotomTab").html(data.BotomTab);
                $("#BotomTab2").html(data.LJTab);
                $("#BotomTab3").html(data.BotomTab4);
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