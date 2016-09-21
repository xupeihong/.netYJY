var curPage = 1;
var OnePageCount = 15;
var Customer;
var FollowPerson;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    // 打印
    $("#btnPrint").click(function () {
        start = $('#start').val();
        end = $('#end').val();
        CustomerName = $('#CustomerName').val();
        Tel = $('#Tel').val();
        var url = "PrintSatisfactionStatistics?start=" + start + "&end=" + end + "&CustomerName=" + CustomerName + "&Tel=" + Tel + "";
        window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    });
})

