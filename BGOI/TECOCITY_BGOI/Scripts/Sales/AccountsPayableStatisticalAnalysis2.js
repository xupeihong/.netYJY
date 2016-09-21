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
        url: "GetAccountsPayableStatisticalAnalysis2Table",
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