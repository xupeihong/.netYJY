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
       "<tr  class=\"left\"><td rowspan=\"2\" style=\"width:5%\">序号号</td>" +
       "<td rowspan=\"2\" style=\"width:5%\">规格型号</td><td rowspan=\"2\" style=\"width:5%\">单位</td>" +
       "<td colspan=\"2\" style=\"width:5%\">X月</td>" +
       "<td colspan=\"8\" style=\"width:5%\">年度累计</td><td colspan=\"3\" style=\"width:5%\">产品状态</td></tr>";

    str1 += "<td style=\"width:5%\">销售数量</td><td style=\"width:5%\">合同额</td>"+"<td style=\"width:5%\">数量</td><td style=\"width:10%\">销售均价</td><td style=\"width:10%\">合同额</td><td style=\"width:10%\">更换线圈后直接成本</td><td style=\"width:10%\">C-C型直接成本</td><td style=\"width:10%\">累计成本</td><td style=\"width:10%\">毛利润</td>" +
       "<td style=\"width:20%\">直接成本占销售价比例</td>" +
     
      " <td style=\"width:10%\">已发货数量</td><td style=\"width:10%\">待发货数量</td><td style=\"width:10%\">生产中数量</td></tr>";;
    str1 += "<tr class=\"staleft\"  style=\"color:red\">" + "<td style=\"width:4%\">合计</td>" +
        "<td style=\"width:10%\"></td>" +
         
          "<td style=\"width:10%\"></td>" +
           "<td style=\"width:10%\"></td>" +
                "<td style=\"width:10%\"></td>" +
                  "<td style=\"width:10%\"></td>" +
           "<td style=\"width:10%\"></td>" +
                "<td style=\"width:10%\"></td>" +
                "<td style=\"width:10%\"></td>" +
         "<td style=\"width:10%\"></td>" +
          "<td style=\"width:10%\"></td>" +
           "<td style=\"width:10%\"></td>" +
                "<td style=\"width:10%\"></td>" +
                  "<td style=\"width:10%\"></td>" +
           "<td style=\"width:10%\"></td>" +
                "<td style=\"width:10%\"></td>" +
    "</tr></table>"
    $("#StaTab").html(str1);
    $("#Sign").html("汇总说明:");
    //jq();
    $('#CX').click(function () {
        var Pname = $('#Pname').val();
        var start = $('#StartDate').val();
        var end = $('#EndDate').val();

        $.ajax({
            url: "GetCutOffSalesSummaryTable",
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