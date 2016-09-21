var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    //$("#pageContent").height($(window).height());
    //$("#search").width($("#bor").width() - 30);
    // 打印
    $("#btnPrint").click(function () {
        start = $('#start').val();
        end = $('#end').val();
        CustomerName = $('#CustomerName').val();
        Tel = $('#Tel').val();
        var url = "PrintInstallStatistics?start=" + start + "&end=" + end + "&CustomerName=" + CustomerName + "&Tel=" + Tel + "";
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });
})

//function reload() {
//    ProType = $('#ProType option:selected').text();
//    PID = $('#PID').val();
//    ProName = $('#ProName').val();
//    Spec = $('#Spec option:selected').text();
//    HouseID = $('#HouseID option:selected').text();

//    if (ProType == "请选择") {
//        ProType = "";
//    }
//    if (Spec == "请选择") {
//        Spec = "";
//    }
//    if (HouseID == "请选择") {
//        HouseID = "";
//    }
//    $("#list").jqGrid('setGridParam', {
//        url: 'InstallStatisticsList',
//        datatype: 'json',
//        postData: { curpage: curPage, rownum: OnePageCount, ProType: ProType, PID: PID, ProName: ProName, Spec: Spec, HouseID: HouseID },

//    }).trigger("reloadGrid");
//}
//function SearchYou1() {
//    curRow = 0;
//    curPage = 1;
//    start = $('#start').val();
//    end = $('#end').val();
//    CustomerName = $('#CustomerName').val();
//    Tel = $('#Tel').val();
//    $("#list").jqGrid('setGridParam', {
//        url: 'InstallStatisticsList',
//        datatype: 'json',
//        postData: {
//            curpage: curPage, rownum: OnePageCount, end: end, start: start, CustomerName: CustomerName, Tel: Tel
//        },
//        loadonce: false

//    }).trigger("reloadGrid");//重新载入

//}
//function SearchYou() {
//    curRow = 0;
//    curPage = 1;
//    start = $('#start').val();
//    end = $('#end').val();
//    CustomerName = $('#CustomerName').val();
//    Tel = $('#Tel').val();
//    if (start == "") {
//        alert("请选择开始时间");
//        return;
//    }
//    if (end == "") {
//        alert("请选择结束时间");
//        return;
//    }
//    for (var i = document.getElementById("DetailInfo").childNodes.length - 1; i >= 0 ; i--) {
//        document.getElementById("DetailInfo").removeChild(document.getElementById("DetailInfo").childNodes[i]);
//    }
//    $.ajax({
//        url: "InstallStatisticsList",
//        type: "post",
//        data: { curpage: curPage, rownum: OnePageCount, end: end, start: start, CustomerName: CustomerName, Tel: Tel },
//        dataType: "json",
//        success: function (data) {
//            var json = eval(data.datas);
//            if (json.length > 0) {
//                for (var i = 0; i < json.length; i++) {
//                    rowCount = document.getElementById("DetailInfo").rows.length;
//                    var CountRows = parseInt(rowCount) + 1;
//                    var html = "";
//                    html = '<tr  id ="DetailInfo' + rowCount + '">';
//                    //if (i < json.length-1) {
//                    //    if (json[i].y == json[i + 1].y) {
//                    //        html += '<td  rowspan=' + rowCount + 1 + ' ><lable class="laby' + rowCount + ' " id="y' + rowCount + '">' + json[i].y + '</lable> </td>';//月
//                    //    } else {
//                    //        html += '<td><lable class="laby' + rowCount + ' " id="y' + rowCount + '">' + json[i].y + '</lable> </td>';//月
//                    //    }
//                    //} else {
//                    html += '<td><lable class="laby' + rowCount + ' " id="y' + rowCount + '">' + json[i].y + '</lable> </td>';//月
//                    //}
//                    html += '<td ><lable class="lablb' + rowCount + ' " id="lb' + rowCount + '">' + json[i].lb + '</lable> </td>';//类别
//                    html += '<td ><lable class="labkhxm' + rowCount + ' " id="khxm' + rowCount + '">' + json[i].khxm + '</lable> </td>';//姓名
//                    html += '<td ><lable class="labxh' + rowCount + ' " id="xh' + rowCount + '">' + json[i].xh + '</lable> </td>';//型号
//                    html += '<td ><lable class="labsl' + rowCount + ' " id="sl' + rowCount + '">' + json[i].sl + '</lable> </td>';//数量
//                    html += '<td ><lable class="labdj' + rowCount + ' " id="dj' + rowCount + '">' + json[i].dj + '</lable> </td>';//价格
//                    html += '<td ><lable class="labbzsj' + rowCount + ' " id="bzsj' + rowCount + '">' + json[i].bzsj + '</lable> </td>';//报装时间
//                    html += '<td ><lable class="labazsj' + rowCount + ' " id="azsj' + rowCount + '">' + json[i].azsj + '</lable> </td>';//安装时间
//                    html += '<td ><lable class="labazry' + rowCount + ' " id="azry' + rowCount + '">' + json[i].azry + '</lable> </td>';//安装人员
//                    html += '<td ><lable class="labkhdh' + rowCount + ' " id="khdh' + rowCount + '">' + json[i].khdh + '</lable> </td>';//联系方式
//                    html += '<td ><lable class="labkhdz' + rowCount + ' " id="khdz' + rowCount + '">' + json[i].khdz + '</lable> </td>';//地址
//                    html += '<td ><lable class="labfpsj' + rowCount + ' " id="fpsj' + rowCount + '">' + json[i].fpsj + '</lable> </td>';//发票/收据

//                    html += '<td ><lable class="labxxsqd' + rowCount + ' " id="xxsqd' + rowCount + '">' + json[i].xxsqd + '</lable> </td>';//销售渠道
//                    html += '<td ><lable class="labfgs' + rowCount + ' " id="fgs' + rowCount + '">' + json[i].fgs + '</lable> </td>';//分公司


//                    html += '<td ><lable class="labbz' + rowCount + ' " id="bz' + rowCount + '">' + json[i].bz + '</lable> </td>';//备注
//                    html += '<td ><lable class="labqrkhmyd' + rowCount + ' " id="qrkhmyd' + rowCount + '">' + json[i].qrkhmyd + '</lable> </td>';//确认客户满意度
//                    html += '<td ><lable class="labsfxyhsmbnshwp' + rowCount + ' " id="sfxyhsmbnshwp' + rowCount + '">' + json[i].sfxyhsmbnshwp + '</lable> </td>';//是否向用户说明包装内所含物品
//                    html += '<td ><lable class="labsfcgzf' + rowCount + ' " id="sfcgzf' + rowCount + '">' + json[i].sfcgzf + '</lable> </td>';//工作服
//                    html += '<td ><lable class="labsfzdyhsyjzdsx' + rowCount + ' " id="sfzdyhsyjzdsx' + rowCount + '">' + json[i].sfzdyhsyjzdsx + '</lable> </td>';//是否指导用户使用及指导事项
//                    html += '<td ><lable class="labsfjsyhzydwp' + rowCount + ' " id="sfjsyhzydwp' + rowCount + '">' + json[i].sfjsyhzydwp + '</lable> </td>';//是否接收用户赠与的物品
//                    html += '<td ><lable class="labsfsf' + rowCount + ' " id="sfsf' + rowCount + '">' + json[i].sfsf + '</lable> </td>';//是否收费
//                    html += '<td ><lable class="labgzwchsfzhqjgz' + rowCount + ' " id="gzwchsfzhqjgz' + rowCount + '">' + json[i].gzwchsfzhqjgz + '</lable> </td>';//工作完成后是否做好清洁工作
//                    html += '<td ><lable class="labkhsfydazdbqz' + rowCount + ' " id="khsfydazdbqz' + rowCount + '">' + json[i].khsfydazdbqz + '</lable> </td>';//客户是否阅读安装单并签字
//                    html += '</tr>';
//                    $("#DetailInfo").append(html);

//                }

//            }
//        }
//    })
//}



