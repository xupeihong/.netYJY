
var curPage = 1;
var OnePageCount = 15;
var j = 0;
$(document).ready(function () {

    $("#pageContent").height($(window).height());
    LoadFileList();

})

function LoadFileList() {

    var Request = GetRequest();
    var strRID = Request["rid"];
    $.ajax({
        url: "LoadFileList",
        type: "post",
        data: { curpage: curPage, rownum: OnePageCount, RID: strRID },
        dataType: "Json",
        success: function (obj) {
            var data = eval("(" + obj + ")");

            for (var i = 0; i < data.rows.length; i++) {


                var testTable = document.getElementById("list");
                j++;
                var newTr = testTable.insertRow();
                newTr.id = "row" + j;


                var newTd1 = newTr.insertCell();

                newTd1.style.width = "200px";
                newTd1.innerHTML = data.rows[i].ID;


                var newTd2 = newTr.insertCell();

                newTd2.style.width = "80px";
                newTd2.innerHTML = data.rows[i].FileName;


                var newTd3 = newTr.insertCell();

                newTd3.style.width = "80px";
                newTd3.innerHTML = data.rows[i].TypeText;

                var newTd4 = newTr.insertCell();

                newTd4.style.width = "80px";


                newTd4.innerHTML = data.rows[i].CreatePerson;

                var newTd5 = newTr.insertCell();
                newTd5.setAttribute("colspan", 5);


                newTd5.innerHTML = "<a  onclick='DownloadFile(\"" + data.rows[i].ID + "\")'>下载</a>";

            }
        }

    });
}

function DownloadFile(id) {
    window.open("DownLoad?id=" + id);
}







function GetRequest() {

    var url = location.search; //获取url中"?"符后的字串

    var theRequest = new Object();

    if (url.indexOf("?") != -1) {

        var str = url.substr(1);

        strs = str.split("&");

        for (var i = 0; i < strs.length; i++) {

            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);

        }

    }

    return theRequest;

}