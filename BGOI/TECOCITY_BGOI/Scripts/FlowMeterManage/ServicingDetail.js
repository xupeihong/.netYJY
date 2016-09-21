var j = 0;
var w = 0;
$(document).ready(function () {
   
    $("#pageContent").height($(window).height());
    var Request = GetRequest();
    var rid = Request["RID"];
    LoadQuotationList();
      LoadChangeBakList();
    
    // 打印 
      $("#btnPrint").click(function () {
       
          $("#pageContent").height($("#ReportContent").height()+50);
        document.getElementById("btnPrint").className = "Noprint";

        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#pageContent").height($(window).height());
    });





})



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



//加载报价单列表
function LoadQuotationList() {
    var Request = GetRequest();
    var RID = Request["RID"];
    $.ajax({
        url: "LoadQuotationList",
        type: "post",
        data: { RID: RID, curpage: "1", rownum: 15 },
        dataType: "Json",
        success: function (obj) {
            var data = eval("(" + obj + ")");
            if (data.success == "false") {

                return;
            }
            else {



                for (var k = 0 ; k < data.rows.length; k++) {
                 
                    switch (data.rows[k].Type) {
                        case "清洗":
                            $("#qx").text(data.rows[k].UnitPrice);
                            w += parseFloat(data.rows[k].UnitPrice);
                            break;
                        case "出厂标定":
                            $("#ccbd").text(data.rows[k].UnitPrice);
                            w += parseFloat(data.rows[k].UnitPrice);
                            break;
                        case "维修":
                            $("#wx").text(data.rows[k].UnitPrice);
                            w += parseFloat(data.rows[k].UnitPrice);
                            break;
                        case "代理标定":
                            $("#dlbd").text(data.rows[k].UnitPrice);
                            w += parseFloat(data.rows[k].UnitPrice);
                            break;
                        case "检测":
                            $("#jc").text(data.rows[k].UnitPrice);
                            w += parseFloat(data.rows[k].UnitPrice);
                            break;
                        case "实流标定":
                            $("#slbd").text(data.rows[k].UnitPrice);
                            w += parseFloat(data.rows[k].UnitPrice);
                            break;
                        case "其他":
                            $("#qt").text(data.rows[k].UnitPrice);
                            w += parseFloat(data.rows[k].UnitPrice);
                            break;


                    }


                }
            }
        }
    });
}

// 加载部件更换记录
function LoadChangeBakList() {
    var Request = GetRequest();
    var rid = Request["RID"];

    $.ajax({
        url: "ServicingAccessory",
        type: "post",
        data: { RID: rid },
        dataType: "Json",
        success: function (data) {
            if (data.success == "false") {

                return;
            }
            else {
                var total = 0;
                var strBakName = data.BakName;
                var strBakType = data.BakType;
                var strBakNum = data.BakNum;

                var strUnitPrice = data.UnitPrice;


                var BakName = strBakName.split(',');
                var BakType = strBakType.split(',');
                var BakNum = strBakNum.split(',');
                var UnitPrice = strUnitPrice.split(',');
                var TotalPrice = data.TotalPrice.split(',');
                var Comments = data.Comments.split('@');
                var testTable = document.getElementById("taskList");
               
                for (var k = 0 ; k < BakName.length; k++) {
                    j++;
                    var newTr = testTable.insertRow();
                    newTr.id = "row" + j;

                    //部件名称 
                    var newTd1 = newTr.insertCell();
               
                    newTd1.style.width = "200px";
                    newTd1.innerHTML = BakName[k];

                    //规格型号
                    var newTd2 = newTr.insertCell();
                    
                    newTd2.style.width = "80px";
                    newTd2.innerHTML = BakType[k];

                    //数量
                    var newTd3 = newTr.insertCell();
                 
                    newTd3.style.width = "80px";
                    newTd3.innerHTML = BakNum[k];

                    //单价
                    var newTd4 = newTr.insertCell();
                 
                    newTd4.style.width = "80px";

                  
                    newTd4.innerHTML = UnitPrice[k];
                    //价格
                    var newTd5 = newTr.insertCell();
                
                    newTd5.style.width = "80px";
                   
                    newTd5.innerHTML = TotalPrice[k];
                  
                    //备注
                    var newTd6 = newTr.insertCell();
                  
                    newTd6.style.width = "80px";
                    newTd6.setAttribute("colspan", 3);
                    newTd6.innerHTML =   Comments[k] 
                }
                j++;
                var newTr1 = testTable.insertRow();
                newTr.id = "row" + j;
                var newTd1 = newTr1.insertCell();
                newTd1.style.width = "200px";
                newTd1.innerHTML = "其他";
                var newTd2 = newTr1.insertCell();
                newTd2.setAttribute("colspan", 7);
                newTd2.style.width = "80px";
                newTd2.innerHTML = "<label></label>";
                
                j++;
                var newTr2 = testTable.insertRow();
                newTr.id = "row" + j;
                var newTd1 = newTr2.insertCell();
                newTd1.style.width = "200px";
                newTd1.innerHTML = "小计";
                var newTd2 = newTr2.insertCell();
                newTd2.setAttribute("colspan", 7);
                newTd2.style.width = "80px";
                newTd2.innerHTML = document.getElementById("wx").innerText;


                var r = j + 2;
               
                $("#Total").text(w)
                
                $("#firsttd").attr("Rowspan", r);
            }
        }
    });
}