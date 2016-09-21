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
       "<tr  class=\"left\"><td style=\"width:10%\">序号号</td><td style=\"width:5%\">合同编号</td><td style=\"width:5%\">物资编号</td><td style=\"width:5%\">订货单位</td><td style=\"width:5%\">订货内容</td><td style=\"width:5%\">规格型号</td>"
    +"<td style=\"width:10%\">单位</td><td style=\"width:10%\">数量</td><td style=\"width:10%\">单价</td><td style=\"width:10%\">合计</td>" +
    "<td style=\"width:10%\">技术指标要求</td><td style=\"width:10%\">订单下发日期</td>" +
    "<td style=\"width:10%\">交（提)货日期</td><td style=\"width:10%\">收款金额</td>" +
    "<td style=\"width:10%\">收款日期</td><td style=\"width:10%\">发票情况</td>" +
    "<td style=\"width:10%\">支出费用</td><td style=\"width:10%\">出厂价</td><td style=\"width:10%\">利润</td><td style=\"width:10%\">备注</td>" +
       "</tr>";
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
                                                "<td style=\"width:10%\"></td>" +
                                                 "<td style=\"width:10%\"></td>" +
                                                  "<td style=\"width:10%\"></td>" +
                                                     "<td style=\"width:10%\"></td>" +

    "</tr></table>"
    $("#StaTab").html(str1);
    $("#Sign").html("汇总说明:开始日期-结束日期");
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