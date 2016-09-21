
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    LoadDetail();

})

//
function LoadDetail() {
    $.ajax({
        url: "GetMeterImg",
        type: "post",
        data: { Meters: $("#Hide").val() },
        dataType: "Json",
        loadonce: false,
        success: function (data) {
            if (data.success == "true") {
                $("#divTab").html(data.strSb);
                var Infos = data.strInfo;
                LoadFusioncharts(Infos);
            }
            else {
                return;
            }
        }
    });
}

// 曲线图
function LoadFusioncharts(Infos) {
    var list = new Array();
    var arrFlow = new Array();//流量点
    var arrPre = new Array();// 维修前
    var arrAft = new Array();// 维修后
    var arrTypes = new Array();// 检测方式 高低频？
    //
    var strList = Infos.split('&');
    var Flows = strList[0];
    var Pre = strList[1];
    var Aft = strList[2];
    var Types = strList[3];
    var Count = Flows.split('@').length;
    if (Count == 1) {
        var arr1 = Flows.split(',');
        var arr2 = Pre.split(',');
        var arr3 = Aft.split(',');
        var arr4 = Types;
        for (var i = 0; i < arr1.length; i++) {
            var keyValue = arr1[i];
            arr1[i] = parseFloat(keyValue);
            keyValue = arr2[i];
            arr2[i] = parseFloat(keyValue);
            keyValue = arr3[i];
            arr3[i] = parseFloat(keyValue);
            LoadPic(Count, arr1, arr2, arr3, arr4);
        }

    }
}

function LoadPic(Count, arr1, arr2, arr3, arr4) {
    var strTitle = "";
    if (arr4.split('-')[1] == "高频")
        strTitle = "仪表系数";
    else
        strTitle = "示值误差";
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
                title: { text: "流量值" },
                categories: arr1, //x轴类别
                tickmarkPlacement: 'on'
            },
            yAxis: [{// y轴
                title: { text: strTitle }, // y轴标题
                lineWidth: 1// 基线宽度
            }
            ],
            stackLabels: {
                enabled: true,
                style: {
                    fontWeight: 'bold',
                    color: 'Red' || 'Blue'
                }
            },
            tooltip: {
                formatter: function () {//格式化鼠标滑向图表数据点时显示的提示框 
                    var s;
                    if (this.point.name) { // the pie chart
                        s = '' + this.point.name + ': ' + this.y + ' fruits';
                    } else {
                        s = '' + this.x + ': ' + this.y;
                    }
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
                spline: {
                    dataLabels: {
                        enabled: true,
                        color: 'black'
                    }
                }
            },
            series: [{
                type: 'spline',
                name: '维修前',
                color: '#AA4643',
                data: arr2
            }, {
                type: 'spline',
                name: '维修后',
                color: '#003399',
                data: arr3
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

