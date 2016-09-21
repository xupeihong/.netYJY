
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    LoadTitle();
    LoadCustomerAnaly();
    LoadTitle2();
    LoadCustomerTotal();
})

// 加载表格标题
function LoadTitle() {
    var tr1 = $("#line1");
    tr1.html('');
    var td = '';
    td = $('<td>运行时间</td>');
    td.appendTo(tr1);
    td = $('<td>维修费高于2千元的故障</td>');
    td.appendTo(tr1);
    td = $('<td>直接影响计量的故障</td>');
    td.appendTo(tr1);
    td = $('<td>不同年限故障合计</td>');
    td.appendTo(tr1);
    td = $('<td>总数</td>');
    td.appendTo(tr1);
}

// 加载表格标题
function LoadTitle2() {
    var tr1 = $("#line2");
    tr1.html('');
    var td = '';
    td = $('<td>项目</td>');
    td.appendTo(tr1);
    td = $('<td>台数</td>');
    td.appendTo(tr1);
    td = $('<td>百分比</td>');
    td.appendTo(tr1);
}

// 加载数据列表
function LoadCustomerAnaly() {
    $.ajax({
        url: "LoadCustomerAnaly",
        type: "post",
        data: {},
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "false") {
                if (data.Msg != "")
                    alert(data.Msg);
            }
            else
                FillData(data);
        }
    })
}

function FillData(data) {
    if (data.Customer != "") {
        var tab = $("#tabDetail");
        tab.html('');
        var datas = eval("(" + data.Customer + ")");
        $.each(datas.Customer, function (i, n) {
            var tr = $('<tr></tr>');
            var td = '';
            for (var key in n) {
                var keyValue = n[key];
                td = $('<td>' + keyValue + '</td>');
                td.appendTo(tr);
            }
            tr.appendTo(tab);
        })
        mc("tabDetail", 0, tab[0].rows.length - 1, 0);
    }
    else
        return;
}

// 合并单元格
function mc(tableId, startRow, endRow, col) {
    var tb = document.getElementById(tableId);
    if (col >= tb.rows[0].cells.length) {
        return;
    }
    if (col == 0) {
        endRow = tb.rows.length - 1;
    }
    for (var i = startRow; i < endRow; i++) {
        if (tb.rows[startRow].cells[col].innerHTML == tb.rows[i + 1].cells[0].innerHTML) {
            tb.rows[i + 1].removeChild(tb.rows[i + 1].cells[0]);
            tb.rows[startRow].cells[col].rowSpan = (tb.rows[startRow].cells[col].rowSpan | 0) + 1;
            if (i == endRow - 1 && startRow != endRow) {
                mc(tableId, startRow, endRow, col + 1);
            }
        }
        else {
            mc(tableId, startRow, i + 0, col + 1);
            startRow = i + 1;
        }
    }
}

// 加载汇总数据列表
function LoadCustomerTotal() {
    $.ajax({
        url: "LoadCustomerTotal",
        type: "post",
        data: {},
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            if (data.success == "false") {
                if (data.Msg != "")
                    alert(data.Msg);
            }
            else
                FillData2(data);
        }
    })
}

function FillData2(data) {
    if (data.CustomerTotal != "") {
        var tab = $("#tabTotal");
        tab.html('');
        var datas = eval("(" + data.CustomerTotal + ")");
        $.each(datas.CustomerTotal, function (i, n) {
            var tr = $('<tr></tr>');
            var td = '';
            for (var key in n) {
                var keyValue = n[key];
                td = $('<td>' + keyValue + '</td>');
                td.appendTo(tr);
            }
            tr.appendTo(tab);
        })
        LoadFusioncharts(data);
    }
    else
        return;
}

//
function LoadFusioncharts(data) {
    var list = new Array();
    var arr1 = new Array();  // 维修费高于2千元的故障
    var arr2 = new Array();; // 直接影响计量的故障 
    var arr3 = new Array();;  // 故障合计
    var arr4 = new Array();;  // 总数
    //
    var datas = eval("(" + data.CustomerTotal + ")");
    $.each(datas.CustomerTotal, function (i, n) {
        for (var key in n) {
            var keyValue = '';
            if (n['项目'] == "维修费高于2千元的故障") {
                keyValue = n['百分比'];
                keyValue = keyValue.split('%')[0];
                arr1[0] = Math.round(keyValue);
            }
            else if (n['项目'] == "直接影响计量的故障") {
                keyValue = n['百分比'];
                keyValue = keyValue.split('%')[0];
                arr2[0] = Math.round(keyValue);
            }
            else if (n['项目'] == "故障合计") {
                keyValue = n['百分比'];
                keyValue = keyValue.split('%')[0];
                arr3[0] = Math.round(keyValue);
            }
            else if (n['项目'] == "总数") {
                keyValue = n['百分比'];
                keyValue = keyValue.split('%')[0];
                arr4[0] = Math.round(keyValue);
            }
            else
                return;
        }
    })

    var chart;
    $(function () {
        chart = new Highcharts.Chart({
            chart: {
                renderTo: 'divchart'
            },
            title: {
                text: ''
            },
            xAxis: {//x轴
                categories: null, //x轴类别
                tickmarkPlacement: 'on',
                labels: {
                    enabled: false
                }
            },
            yAxis: [{// y轴
                title: { text: '' }, // y轴标题
                lineWidth: 1,// 基线宽度                
                tooltip: {
                    valueSuffix: '%'
                }
            }],
            tooltip: {
                formatter: function () {//格式化鼠标滑向图表数据点时显示的提示框 
                    var s;
                    s = this.y;
                    return s;
                }
            },
            labels: {//图表标签 
                items: [{
                    html: '',
                    style: {
                        left: '40px',
                        top: '8px',
                        color: 'black'
                    }
                }]
            },
            exporting: {
                enabled: false  //设置导出按钮不可用 
            },
            plotOptions: {
                column: {
                    pointWidth: 35,
                    dataLabels: {
                        enabled: true, //是否显示数据标签 
                        color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                    }
                }
            },
            series: [{//数据列
                type: 'column',
                name: '维修费高于2千元的故障',
                color: '#ef3d15',
                data: arr1
            }, {
                type: 'column',
                name: '直接影响计量的故障',
                color: '#efe528',
                data: arr2
            }, {
                type: 'column',
                name: '故障合计',
                color: '#3f96ea',
                data: arr3
            }, {
                type: 'column',
                name: '总数',
                color: '#39d96d',
                data: arr4
            }],
            center: [100, 80],
            size: 100,
            showInLegend: false,
            dataLabels: {
                enabled: false //显示饼状图数据标签 
            }
        })
    })

}

//
function ToEXCEL(tableid) {
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
        var curTbl = "<table>" + divIn + "</table>";
        window.clipboardData.setData("Text", curTbl);

        //创建AX对象excel 
        var oWB = oXL.Workbooks.Add();
        //获取workbook对象 
        var oSheet = oWB.Worksheets(1);
        oSheet.Columns('A:A').ColumnWidth = 10;
        oSheet.Columns('B:B').ColumnWidth = 10;
        oSheet.Columns('C:C').ColumnWidth = 15;
        oSheet.Columns('D:D').ColumnWidth = 15;
        oSheet.Columns('E:E').ColumnWidth = 15;
        oSheet.Columns('F:F').ColumnWidth = 15;
        oSheet.Columns('G:G').ColumnWidth = 15;

        oSheet.Paste();
        //粘贴到活动的EXCEL中       
        oXL.Visible = true;
        //设置excel可见属性
    }
}



