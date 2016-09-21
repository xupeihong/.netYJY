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
    $("#StaTab").height($("#pageContent").height() - 50);
    var str1 = "<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\">" +
       "<tr  class=\"left\"><td style=\"width:10%\">序号号</td><td style=\"width:15%\">合同编号</td><td style=\"width:15%\">摘要</td><td style=\"width:15%\">产品名称</td><td style=\"width:15%\">产品数量</td><td style=\"width:15%\">欠款总额</td><td style=\"width:15%\">产品状态</td></tr>";
    str1 += "<tr class=\"staleft\"  style=\"color:red\">" + "<td style=\"width:4%\">合计</td>" +
        "<td style=\"width:10%\"></td>" +
         "<td style=\"width:15%\"></td>" +
          "<td style=\"width:15%\"></td>" +
           "<td style=\"width:15%\"></td>" +
                "<td style=\"width:15%\"></td>" +
   "<td style=\"width:20%\"></td>" +
   
    "</tr></table>"
    $("#StaTab").html(str1);
    $("#Sign").html("汇总说明:");
    //jq();
    $('#CX').click(function () {
        var Pname = $('#Pname').val();
        var start = $('#StartDate').val();
        var end = $('#EndDate').val();

        $.ajax({
            url: "StatisticsManageTable",
            type: "post",
            data: { data1: start, data2: end, data3: Pname },
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