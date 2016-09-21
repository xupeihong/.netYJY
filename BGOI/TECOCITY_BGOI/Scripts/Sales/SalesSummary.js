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
       "<tr  class=\"left\"><td style=\"width:10%\">序号号</td><td style=\"width:20%\">姓名</td><td style=\"width:20%\">总额</td><td style=\"width:20%\">已收款</td><td style=\"width:20%\">欠款</td><td style=\"width:20%\">回款率</td></tr>";
    str1 += "<tr class=\"staleft\"  style=\"color:red\">" + "<td style=\"width:4%\">合计</td>" +
        "<td style=\"width:10%\"></td>" +
         "<td style=\"width:20%\"></td>" +
          "<td style=\"width:20%\"></td>" +
           "<td style=\"width:20%\"></td>" +
                "<td style=\"width:20%\"></td>" +
    "</tr></table>"
    $("#StaTab").html(str1);
 //   $("#Sign").html("汇总说明:开始日期-结束日期，销售总额xxx元，已收款xxxx元");
    //jq();
    $('#CX').click(function () {
        var Pname = $('#Pname').val();
        var start = $('#StartDate').val();
        var end = $('#EndDate').val();

        $.ajax({
            url: "SalesSummaryTable",
            type: "post",
            data: { StartDate: start, EndDate: end, data3: Pname },
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