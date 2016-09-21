
var curPage = 1;
var OnePageCount = 30;
var PID;
var RelenvceID;
var Type = "工程立项审批";
var ProID;
var Pname;
var start;
var end;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#pageContent").width() - 30);

    $("#StaTab").height($("#pageContent").height() - 150);
    var str1 = "<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:20px;\"><tr  class=\"left\"><td style=\"width:20%\">工程编号</td><td style=\"width:20%\">工程名称</td><td style=\"width:10%\">立项时间</td><td style=\"width:10%\">项目负责人</td><td style=\"width:20%\">工程状态</td><td style=\"width:20%\">欠款金额(万元)</td></tr></table>";

    $("#StaTab").html(str1);
    $("#Sign").html("汇总说明:");
    //jq();
    $('#CX').click(function () {
        var ProID = $('#ProID').val();
        var Pname = $('#Pname').val();
        var start = $('#StartDate').val();
        var end = $('#EndDate').val();
        var Principal = $('#Principal').val();

        $.ajax({
            url: "DebtStatisticsTable",
            type: "post",
            data: { data1: start, data2: end, data3: Pname, data4: ProID, data5: Principal },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    //alert(data.strSb);
                    $("#StaTab").html(data.strSb);
                    $("#Sign").html(data.strSign);
                }
                else {
                    return;
                }
            }
        });
    })
})
