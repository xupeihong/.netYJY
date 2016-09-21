var l = 0;
var j = 0;
var w = 0;
$(document).ready(function () {

    LoadQuotationList();
    LoadChangeBakList();

    // 打印 
    $("#DY").click(function () {

        document.getElementById("DY").className = "Noprint";

        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("DY").className = "btn";
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
                    if (data.rows[k].Type != "维修") {
                        l++;
                        var testTable = document.getElementById("taskList");
                        var newTr = testTable.insertRow();
                        newTr.id = "row" + l;
                       
                        //类型 
                        var newTd1 = newTr.insertCell();

                        newTd1.style.width = "200px";
                        newTd1.innerHTML = data.rows[k].Type;

                        //原价
                        var newTd2 = newTr.insertCell();

                        newTd2.style.width = "80px";

                        newTd2.innerHTML = data.rows[k].UnitPrice
                        w +=parseFloat(data.rows[k].UnitPrice);

                    }
                }
            }
        }
    });
}

//加载更换零件列表
function LoadChangeBakList() {
    var Request = GetRequest();
    var RID = Request["RID"];
    $.ajax({
        url: "ServicingAccessory",
        type: "post",
        data: { RID: RID },
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
                var testTable = document.getElementById("taskList2");
                for (var k = 0 ; k < BakName.length; k++) {
                    j++;

                    var newTr = testTable.insertRow();
                    newTr.id = "row" + j;

                    //部件名称 
                    var newTd1 = newTr.insertCell();
                    newTd1.className = "textright";
                    newTd1.style.width = "150px";
                    newTd1.innerHTML = BakName[k];

                    //规格型号
                    var newTd2 = newTr.insertCell();
                    newTd2.className = "textright";
                    newTd2.style.width = "150px";
                    newTd2.innerHTML = BakType[k];

                    //数量
                    var newTd3 = newTr.insertCell();
                    newTd3.className = "textright";
                    newTd3.style.width = "80px";
                    newTd3.innerHTML = BakNum[k];

                    //单价
                    var newTd4 = newTr.insertCell();
                    newTd4.className = "textright";
                    newTd4.style.width = "80px";


                    newTd4.innerHTML = UnitPrice[k];
                    //价格
                    var newTd5 = newTr.insertCell();
                    newTd5.className = "textright";
                    newTd5.style.width = "80px";

                    newTd5.innerHTML = TotalPrice[k];
                    total += parseFloat(TotalPrice[k]);
                    //备注
                    var newTd6 = newTr.insertCell();
                    newTd6.className = "textright";
                    newTd6.style.width = "80px";

                    newTd6.innerHTML = Comments[k];
                }

                j++;

                var newTr = testTable.insertRow();
                newTr.id = "row" + j;

                var newTd1 = newTr.insertCell();
                newTd1.className = "textright";
                newTd1.style.width = "150px";
                newTd1.innerHTML = "总价";
                total += parseFloat(w);

                var newTd2 = newTr.insertCell();
                newTd2.className = "textright";
                newTd2.setAttribute("colspan", 5);

                newTd2.innerHTML = total;
            }
        }
    });
}

