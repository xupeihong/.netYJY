
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
    $("#StaTab").width($("#pageContent").width() - 20);
    $("#StaTab").height($("#pageContent").height() - 150);
    $("#tabStatisticTotal").width($("#StaTab").width() - 20);
    //var str1 = "<table id=\"T\" class=\"statitab\" style=\"margin-left:10px;margin-top:5px;line-height:25px;\"><tr  class=\"left\"><td style=\"width:15%\">工程编号</td><td style=\"width:15%\">工程名称</td><td style=\"width:10%\">立项时间</td><td style=\"width:10%\">项目负责人</td><td style=\"width:10%\">工程状态</td><td style=\"width:10%\">合同金额(万元)</td><td style=\"width:10%\">已付金额(万元)</td><td style=\"width:15%\">未付金额(万元)</td></tr></table>";

    LoadTitleF1();
    //$("#StaTab").html(str1);
    $("#Sign").html("汇总说明:");
    //jq();
    $('#CX').click(function () {
        var ProID = $('#ProID').val();
        var Pname = $('#Pname').val();
        var start = $('#StartDate').val();
        var end = $('#EndDate').val();
        var Principal = $('#Principal').val();

        $.ajax({
            url: "AmountStatisticsTable",
            type: "post",
            data: { data1: start, data2: end, data3: Pname, data4: ProID, data5: Principal },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    //alert(data.strSb);
                    $("#tabbody").html(data.strSb);
                    FixTable("tabStatisticTotal", 1, $("#StaTab").width(), $("#pageContent").height() - 150);
                    $("#Sign").html(data.strSign);
                }
                else {
                    return;
                }
            }
        });
    })
})
function LoadTitleF1() {
    var tr1 = $("#line1");
    tr1.html('');
    var th1 = $('<td style="width:15%">工程编号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td style="width:15%">工程名称</td>');
    th1.appendTo(tr1);
    var th1 = $('<td style="width:10%">立项时间</td>');
    th1.appendTo(tr1);
    var th1 = $('<td style="width:10%">项目负责人</td>');
    th1.appendTo(tr1);
    var th1 = $('<td style="width:10%">工程状态</td>');
    th1.appendTo(tr1);
    var th1 = $('<td style="width:10%">合同金额(万元)</td>');
    th1.appendTo(tr1);
    var th1 = $('<td style="width:10%">已付金额(万元)</td>');
    th1.appendTo(tr1);
    var th1 = $('<td style="width:10%">未付金额(万元)</td>');
    th1.appendTo(tr1);
    var th1 = $('<td style="width:10%">发票金额(万元)</td>');
    th1.appendTo(tr1);
}


// 导出到excel
function ToEXCEL(tableid) {
    var a = document.getElementById(tableid).rows.length;
    if (document.getElementById(tableid).rows.length <= 1) {
        alert("当前没有导出到EXCEL的数据");
        return;
    }
    //if (navigator.userAgent.indexOf("MSIE") < 0) {
    //    alert('请用ie浏览器进行表格导出');
    //    return;
    //}
    var divIn = document.getElementById(tableid).innerHTML;
    if (divIn != "") {
        var oXL = null;
        try {
            oXL = GetObject("", "Excel.Application");
        }
        catch (E) {
            try {
                oXL = new ActiveXObject("Excel.Application");
            }
            catch (E2) {
                alert("请确认:\n1.Microsoft Excel 已安装.\n2.Internet 选项=>安全=>设置 \"允许ActiveX控件\"");
                return;
            }
        }
    }
    var curTbl = document.getElementById(tableid);
    //创建AX对象excel 
    var oWB = oXL.Workbooks.Add();
    //获取workbook对象 
    var oSheet = oWB.Worksheets(1);

    oSheet.Rows(1 + ":" + 1).RowHeight = 24;

    oSheet.Columns('A:A').ColumnWidth = 20;
    oSheet.Columns('B:B').ColumnWidth = 20;
    oSheet.Columns('C:C').ColumnWidth = 20;
    oSheet.Columns('D:D').ColumnWidth = 10;
    oSheet.Columns('E:E').ColumnWidth = 10;
    oSheet.Columns('F:F').ColumnWidth = 10;
    oSheet.Columns('G:G').ColumnWidth = 10;
    oSheet.Columns('H:H').ColumnWidth = 8;
    oSheet.Columns('I:I').ColumnWidth = 8;
    oSheet.Columns('J:J').ColumnWidth = 8;
    oSheet.Columns('K:K').ColumnWidth = 8;
    oSheet.Columns('L:L').ColumnWidth = 8;
    oSheet.Columns('M:M').ColumnWidth = 8;
    oSheet.Columns('N:N').ColumnWidth = 8;
    oSheet.Columns('O:O').ColumnWidth = 8;
    oSheet.Columns('P:P').ColumnWidth = 8;
    oSheet.Columns('Q:Q').ColumnWidth = 8;
    oSheet.Columns('R:R').ColumnWidth = 8;
    oSheet.Columns('S:S').ColumnWidth = 8;
    oSheet.Columns('T:T').ColumnWidth = 8;
    oSheet.Columns('U:U').ColumnWidth = 8;
    oSheet.Columns('V:V').ColumnWidth = 8;
    oSheet.Columns('W:W').ColumnWidth = 8;
    oSheet.Columns('X:X').ColumnWidth = 8;

    oSheet.PageSetup.LeftMargin = 1.4 / 0.035;         //页边距 左2厘米
    oSheet.PageSetup.RightMargin = 1.2 / 0.035;      //页边距 右3厘米，
    oSheet.PageSetup.TopMargin = 1.8 / 0.035;        //页边距 上4厘米，
    oSheet.PageSetup.BottomMargin = 1.5 / 0.035;   //页边距 下5厘米

    //激活当前sheet 
    var sel = document.body.createTextRange();
    sel.moveToElementText(curTbl);
    //把表格中的内容移到TextRange中 
    sel.select();
    //全选TextRange中内容      
    sel.execCommand("Copy");
    //复制TextRange中内容  
    oSheet.Paste();
    //粘贴到活动的EXCEL中    

    var sheetR = oSheet.UsedRange.Rows.Count;
    var sheetC = oSheet.UsedRange.Columns.Count;

    oSheet.Range(oSheet.Cells(1, 1), oSheet.Cells(sheetR, sheetC)).Borders.Weight = 2;
    oSheet.Range(oSheet.Cells(1, 1), oSheet.Cells(sheetR, sheetC)).Font.Size = 10;
    oSheet.Range(oSheet.Cells(1, 1), oSheet.Cells(1, sheetC)).HorizontalAlignment = 3;

    oXL.Visible = true;
    //设置excel可见属性
}


