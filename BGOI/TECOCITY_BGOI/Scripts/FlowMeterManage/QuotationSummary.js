
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var j = 0;
$(document).ready(function () {
    
    var Request = GetRequest();
    var strRID = Request["RID"];
    $("#pageContent").height($(window).height());
    LoadQuotationList();


    // 打印 
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";


        window.print();
        document.getElementById("btnPrint").className = "btn";

    });
});



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



function LoadQuotationList() {


    var Request = GetRequest();
    var strRID = Request["RID"];
    var strCardType = Request["CardType"];
    $.ajax({
        url: "LoadQuotationList2",
        type: "post",
        data: {
            curpage: curPage, rownum: OnePageCount, RID: strRID,
            RepairID: "", CustomerName: "", CustomerAddr: "",
            MeterID:"", MeterName: "", Model: "",
            SS_Date: "", ES_Date: "", State: "",
            OrderDate: "", CardType: strCardType

        },
        dataType: "Json",
        success: function (obj) {
            
            var data = eval("(" + obj + ")");
            if (data.success == "false") {

                return; 
    
            } else {
                for (var i = 0; i < data.rows.length; i++) {
                    
                    var testTable = document.getElementById("list");
                    j++;
                    var newTr = testTable.insertRow();
                    newTr.id = "row" + j;

                
                    var newTd1 = newTr.insertCell();

                    newTd1.style.width = "200px";
                    newTd1.innerHTML = data.rows[i].RID;

               
                    var newTd2 = newTr.insertCell();

                    newTd2.style.width = "80px";
                    newTd2.innerHTML =data.rows[i].MeterName;


                    var newTd3 = newTr.insertCell();

                    newTd3.style.width = "80px";
                    newTd3.innerHTML = data.rows[i].Manufacturer;

                    var newTd4 = newTr.insertCell();

                    newTd4.style.width = "80px";


                    newTd4.innerHTML = data.rows[i].Model;
                 
                    var newTd5 = newTr.insertCell();

                    newTd5.style.width = "80px";

                    newTd5.innerHTML =data.rows[i].Caliber;

              
                    var newTd6 = newTr.insertCell();

                    newTd6.style.width = "80px";
                   
                    newTd6.innerHTML = data.rows[i].RecordNum;

                    var newTd7 = newTr.insertCell();

                    newTd7.style.width = "80px";
                    newTd7.innerHTML = data.rows[i].MeterID;

          
                    var newTd8 = newTr.insertCell();

                    newTd8.style.width = "80px";
                    newTd8.innerHTML = data.rows[i].PartsPrice;

                   
                    var newTd9 = newTr.insertCell();

                    newTd9.style.width = "80px";
                    newTd9.innerHTML = data.rows[i].qx;

                  
                    var newTd10 = newTr.insertCell();

                    newTd10.style.width = "80px";


                    newTd10.innerHTML = data.rows[i].dy;
                    
                    var newTd11 = newTr.insertCell();

                    newTd11.style.width = "80px";

                    newTd11.innerHTML = data.rows[i].jc;

                   
                    var newTd12 = newTr.insertCell();

                    newTd12.style.width = "80px";
                   
                    newTd12.innerHTML = data.rows[i].TotalPrice;
                }
            }
        }

    });
}