var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    
    // 打印
    $("#btnPrint").click(function () {
        start = $('#start').val();
        end = $('#end').val();
        CustomerName = $('#CustomerName').val();
        Tel = $('#Tel').val();
        var url = "PrintSatisfactionStatistics?start=" + start + "&end=" + end + "&CustomerName=" + CustomerName + "&Tel="+Tel+"";
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });
})
function reload() {
    ProType = $('#ProType option:selected').text();
    PID = $('#PID').val();
    ProName = $('#ProName').val();
    Spec = $('#Spec option:selected').text();
    HouseID = $('#HouseID option:selected').text();

    if (ProType == "请选择") {
        ProType = "";
    }
    if (Spec == "请选择") {
        Spec = "";
    }
    if (HouseID == "请选择") {
        HouseID = "";
    }
    $("#list").jqGrid('setGridParam', {
        url: 'InstallStatisticsList',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ProType: ProType, PID: PID, ProName: ProName, Spec: Spec, HouseID: HouseID },

    }).trigger("reloadGrid");
}
function SearchYou1() {
    curRow = 0;
    curPage = 1;
    start = $('#start').val();
    end = $('#end').val();
    CustomerName = $('#CustomerName').val();
    Tel = $('#Tel').val();
    $("#list").jqGrid('setGridParam', {
        url: 'InstallStatisticsList',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, end: end, start: start, CustomerName: CustomerName, Tel: Tel
        },
        loadonce: false

    }).trigger("reloadGrid");//重新载入

}
function SearchYou() {
    curRow = 0;
    curPage = 1;
    start = $('#start').val();
    end = $('#end').val();
    CustomerName = $('#CustomerName').val();
    Tel = $('#Tel').val();
    //if (start == "") {
    //    alert("请选择开始时间");
    //    return;
    //}
    //if (end == "") {
    //    alert("请选择结束时间");
    //    return;
    //}
    for (var i = document.getElementById("DetailInfo").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("DetailInfo").removeChild(document.getElementById("DetailInfo").childNodes[i]);
    }
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "SatisfactionStatisticsList",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, end: end, start: start, CustomerName: CustomerName, Tel: Tel },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                var html = "";
                html = '<tr  id ="DetailInfo' + rowCount + '">';
                html += '<td rowspan="4"><lable class="cp' + rowCount + ' " id="cp' + rowCount + '">产品</lable> </td>';
                html += '</tr>';
                $("#DetailInfo").append(html);
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    if (i == 0) {
                    html = '<tr  id ="DetailInfo' + rowCount + '">';
                    html += '<td ><lable class="lablb' + rowCount + ' " id="lb' + rowCount + '">' + json[i].mc + '</lable> </td>';
                    html += '<td ><lable class="labkhxm' + rowCount + ' " id="khxm' + rowCount + '">' + json[i].cpzlfcmy + '</lable> </td>';
                    html += '<td ><lable class="labxh' + rowCount + ' " id="xh' + rowCount + '">' + json[i].cpzlmy + '</lable> </td>';
                    html += '<td ><lable class="labsl' + rowCount + ' " id="sl' + rowCount + '">' + json[i].cpzlyb + '</lable> </td>';
                    html += '<td ><lable class="labdj' + rowCount + ' " id="dj' + rowCount + '">' + json[i].cpzlbmy + '</lable> </td>';
                    html += '</tr>';
                    $("#DetailInfo").append(html);
                    }
                    if(i==1){
                        html = '<tr  id ="DetailInfo' + rowCount + '">';
                        html += '<td ><lable class="lablb' + rowCount + ' " id="lb' + rowCount + '">' + json[i].mc + '</lable> </td>';
                        html += '<td ><lable class="labkhxm' + rowCount + ' " id="khxm' + rowCount + '">' + json[i].cpzlfcmy + '</lable> </td>';
                        html += '<td ><lable class="labxh' + rowCount + ' " id="xh' + rowCount + '">' + json[i].cpzlmy + '</lable> </td>';
                        html += '<td ><lable class="labsl' + rowCount + ' " id="sl' + rowCount + '">' + json[i].cpzlyb + '</lable> </td>';
                        html += '<td ><lable class="labdj' + rowCount + ' " id="dj' + rowCount + '">' + json[i].cpzlbmy + '</lable> </td>';
                        html += '</tr>';
                        $("#DetailInfo").append(html);
                    }
                    if (i == 2) {
                        html = '<tr  id ="DetailInfo' + rowCount + '">';
                        html += '<td ><lable class="lablb' + rowCount + ' " id="lb' + rowCount + '">' + json[i].mc + '</lable> </td>';
                        html += '<td ><lable class="labkhxm' + rowCount + ' " id="khxm' + rowCount + '">' + json[i].cpzlfcmy + '</lable> </td>';
                        html += '<td ><lable class="labxh' + rowCount + ' " id="xh' + rowCount + '">' + json[i].cpzlmy + '</lable> </td>';
                        html += '<td ><lable class="labsl' + rowCount + ' " id="sl' + rowCount + '">' + json[i].cpzlyb + '</lable> </td>';
                        html += '<td ><lable class="labdj' + rowCount + ' " id="dj' + rowCount + '">' + json[i].cpzlbmy + '</lable> </td>';
                        html += '</tr>';
                        $("#DetailInfo").append(html);
                    }
                }
                html = '<tr  id ="DetailInfo' + rowCount + '">';
                html += '<td rowspan="4"><lable class="fw' + rowCount + ' " id="fw' + rowCount + '">服务</lable> </td>';
                html += '</tr>';
                $("#DetailInfo").append(html);
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    if (i == 3) {
                        html = '<tr  id ="DetailInfo' + rowCount + '">';
                        html += '<td ><lable class="lablb' + rowCount + ' " id="lb' + rowCount + '">' + json[i].mc + '</lable> </td>';
                        html += '<td ><lable class="labkhxm' + rowCount + ' " id="khxm' + rowCount + '">' + json[i].cpzlfcmy + '</lable> </td>';
                        html += '<td ><lable class="labxh' + rowCount + ' " id="xh' + rowCount + '">' + json[i].cpzlmy + '</lable> </td>';
                        html += '<td ><lable class="labsl' + rowCount + ' " id="sl' + rowCount + '">' + json[i].cpzlyb + '</lable> </td>';
                        html += '<td ><lable class="labdj' + rowCount + ' " id="dj' + rowCount + '">' + json[i].cpzlbmy + '</lable> </td>';
                        html += '</tr>';
                        $("#DetailInfo").append(html);
                    }
                    if (i == 4) {
                        html = '<tr  id ="DetailInfo' + rowCount + '">';
                        html += '<td ><lable class="lablb' + rowCount + ' " id="lb' + rowCount + '">' + json[i].mc + '</lable> </td>';
                        html += '<td ><lable class="labkhxm' + rowCount + ' " id="khxm' + rowCount + '">' + json[i].cpzlfcmy + '</lable> </td>';
                        html += '<td ><lable class="labxh' + rowCount + ' " id="xh' + rowCount + '">' + json[i].cpzlmy + '</lable> </td>';
                        html += '<td ><lable class="labsl' + rowCount + ' " id="sl' + rowCount + '">' + json[i].cpzlyb + '</lable> </td>';
                        html += '<td ><lable class="labdj' + rowCount + ' " id="dj' + rowCount + '">' + json[i].cpzlbmy + '</lable> </td>';
                        html += '</tr>';
                        $("#DetailInfo").append(html);
                    }
                    if (i == 5) {
                        html = '<tr  id ="DetailInfo' + rowCount + '">';
                        html += '<td ><lable class="lablb' + rowCount + ' " id="lb' + rowCount + '">' + json[i].mc + '</lable> </td>';
                        html += '<td ><lable class="labkhxm' + rowCount + ' " id="khxm' + rowCount + '">' + json[i].cpzlfcmy + '</lable> </td>';
                        html += '<td ><lable class="labxh' + rowCount + ' " id="xh' + rowCount + '">' + json[i].cpzlmy + '</lable> </td>';
                        html += '<td ><lable class="labsl' + rowCount + ' " id="sl' + rowCount + '">' + json[i].cpzlyb + '</lable> </td>';
                        html += '<td ><lable class="labdj' + rowCount + ' " id="dj' + rowCount + '">' + json[i].cpzlbmy + '</lable> </td>';
                        html += '</tr>';
                        $("#DetailInfo").append(html);
                    }
                }
                html = '<tr  id ="DetailInfo' + rowCount + '">';
                html += '<td rowspan="4"><lable class="dl' + rowCount + ' " id="dl' + rowCount + '">代理</lable> </td>';
                html += '</tr>';
                $("#DetailInfo").append(html);
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    if (i == 6) {
                        html = '<tr  id ="DetailInfo' + rowCount + '">';
                        html += '<td ><lable class="lablb' + rowCount + ' " id="lb' + rowCount + '">' + json[i].mc + '</lable> </td>';
                        html += '<td ><lable class="labkhxm' + rowCount + ' " id="khxm' + rowCount + '">' + json[i].cpzlfcmy + '</lable> </td>';
                        html += '<td ><lable class="labxh' + rowCount + ' " id="xh' + rowCount + '">' + json[i].cpzlmy + '</lable> </td>';
                        html += '<td ><lable class="labsl' + rowCount + ' " id="sl' + rowCount + '">' + json[i].cpzlyb + '</lable> </td>';
                        html += '<td ><lable class="labdj' + rowCount + ' " id="dj' + rowCount + '">' + json[i].cpzlbmy + '</lable> </td>';
                        html += '</tr>';
                        $("#DetailInfo").append(html);
                    }
                    if (i == 7) {
                        html = '<tr  id ="DetailInfo' + rowCount + '">';
                        html += '<td ><lable class="lablb' + rowCount + ' " id="lb' + rowCount + '">' + json[i].mc + '</lable> </td>';
                        html += '<td ><lable class="labkhxm' + rowCount + ' " id="khxm' + rowCount + '">' + json[i].cpzlfcmy + '</lable> </td>';
                        html += '<td ><lable class="labxh' + rowCount + ' " id="xh' + rowCount + '">' + json[i].cpzlmy + '</lable> </td>';
                        html += '<td ><lable class="labsl' + rowCount + ' " id="sl' + rowCount + '">' + json[i].cpzlyb + '</lable> </td>';
                        html += '<td ><lable class="labdj' + rowCount + ' " id="dj' + rowCount + '">' + json[i].cpzlbmy + '</lable> </td>';
                        html += '</tr>';
                        $("#DetailInfo").append(html);
                    }
                    if (i == 8) {
                        html = '<tr  id ="DetailInfo' + rowCount + '">';
                        html += '<td ><lable class="lablb' + rowCount + ' " id="lb' + rowCount + '">' + json[i].mc + '</lable> </td>';
                        html += '<td ><lable class="labkhxm' + rowCount + ' " id="khxm' + rowCount + '">' + json[i].cpzlfcmy + '</lable> </td>';
                        html += '<td ><lable class="labxh' + rowCount + ' " id="xh' + rowCount + '">' + json[i].cpzlmy + '</lable> </td>';
                        html += '<td ><lable class="labsl' + rowCount + ' " id="sl' + rowCount + '">' + json[i].cpzlyb + '</lable> </td>';
                        html += '<td ><lable class="labdj' + rowCount + ' " id="dj' + rowCount + '">' + json[i].cpzlbmy + '</lable> </td>';
                        html += '</tr>';
                        $("#DetailInfo").append(html);
                    }
                }
                
            }
        }
    })
}



