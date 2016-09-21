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
        var url = "PrintMaintenanceTaskStatistics?start=" + start + "&end=" + end + "&CustomerName=" + CustomerName + "Tel=" + Tel + "";
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
        url: 'MaintenanceTaskStatisticsList',
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
        url: 'MaintenanceTaskStatisticsList',
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
    if (start == "") {
        alert("请选择开始时间");
        return;
    }
    if (end == "") {
        alert("请选择结束时间");
        return;
    }
    for (var i = document.getElementById("DetailInfo").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("DetailInfo").removeChild(document.getElementById("DetailInfo").childNodes[i]);
    }
    $.ajax({
        url: "MaintenanceTaskStatisticsList",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, end: end, start: start, CustomerName: CustomerName, Tel: Tel },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '">';
                    html += '<td><lable class="laby' + rowCount + ' " id="CreateTime' + rowCount + '">' + json[i].CreateTime + '</lable> </td>';
                    html += '<td ><lable class="lablb' + rowCount + ' " id="lb' + rowCount + '">' + json[i].Customer + '</lable> </td>';
                    html += '<td ><lable class="labkhxm' + rowCount + ' " id="khxm' + rowCount + '">' + json[i].ContactName + '</lable> </td>';
                    html += '<td ><lable class="labxh' + rowCount + ' " id="xh' + rowCount + '">' + json[i].Address + '</lable> </td>';
                    html += '<td ><lable class="labsl' + rowCount + ' " id="sl' + rowCount + '">' + json[i].Tel + '</lable> </td>';
                    html += '<td ><lable class="labdj' + rowCount + ' " id="dj' + rowCount + '">' + json[i].DeviceType + '</lable> </td>';
                    html += '<td ><lable class="labbzsj' + rowCount + ' " id="bzsj' + rowCount + '">' + json[i].DeviceID + '</lable> </td>';
                    html += '<td ><lable class="labazsj' + rowCount + ' " id="azsj' + rowCount + '">' + json[i].EnableDate + '</lable> </td>';
                    html += '<td ><lable class="labazry' + rowCount + ' " id="azry' + rowCount + '">' + json[i].GuaranteePeriod + '</lable> </td>';
                    html += '<td ><lable class="labkhdh' + rowCount + ' " id="khdh' + rowCount + '">' + json[i].RepairDate + '</lable> </td>';
                    html += '<td ><lable class="labkhdz' + rowCount + ' " id="khdz' + rowCount + '">' + json[i].MaintenanceTime + '</lable> </td>';
                    html += '<td ><lable class="labfpsj' + rowCount + ' " id="fpsj' + rowCount + '">' + json[i].BXKNum + '</lable> </td>';
                    html += '<td ><lable class="labbz' + rowCount + ' " id="bz' + rowCount + '">' + json[i].MaintenanceName + '</lable> </td>';
                    html += '<td ><lable class="labqrkhmyd' + rowCount + ' " id="qrkhmyd' + rowCount + '">' + json[i].RepairTheUser + '</lable> </td>';
                    html += '<td ><lable class="labsfxyhsmbnshwp' + rowCount + ' " id="sfxyhsmbnshwp' + rowCount + '">' + json[i].MaintenanceRecord + '</lable> </td>';
                    html += '<td ><lable class="labsfcgzf' + rowCount + ' " id="sfcgzf' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labsfzdyhsyjzdsx' + rowCount + ' " id="sfzdyhsyjzdsx' + rowCount + '">' + json[i].Total + '</lable> </td>';
                    html += '<td ><lable class="labsfjsyhzydwp' + rowCount + ' " id="sfjsyhzydwp' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    html += '</tr>';
                    $("#DetailInfo").append(html);

                }

            }
        }
    })
}



