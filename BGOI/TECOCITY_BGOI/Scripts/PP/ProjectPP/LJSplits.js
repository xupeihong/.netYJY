$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#pageContent").width($(window).width() - 5);
    var ddid = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "SelectSplitLJ",
        type: "post",
        data: { DDID: ddid },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html += "<tr>";
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumbers' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJCPIDs' + rowCount + '">' + json[i].LJCPID + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJPids' + rowCount + '">' + json[i].Name + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJNamess' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJSpes' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    //html += '<td style="display:none"><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';

                    //html += '<td ><input type="text" name="' + json[i].LJCPID + '' + json[i].OrderContent + '-' + rowCount + '" style="width:100px" value=' + json[i].sum2 + ' id="LJNumss' + rowCount + '"></td>';

                    html += '<td ><lable class="labOrderContent' + rowCount + ' " name="' + json[i].LJCPID + '' + json[i].OrderContent + '-' + rowCount + '" style="width:100px"  id="LJNumss' + rowCount + '">' + json[i].sum2 + '</lable> </td>';

                    //html += '<td style="display:none" ><input type="text" style="width:100px" value=' + json[i].sum2 + ' id="Nums' + rowCount + '"></td>';
                    //html += '<td style="display:none" ><lable class="labProductID' + rowCount + ' " id="Nums' + rowCount + '">' + json[i].sum2 + '</lable> </td>';
                    //html += '<td ><lable class="labProductID' + rowCount + ' " id="LJUnitPrice' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJZJ' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJPrice2' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    //html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJZJ2' + rowCount + '">' + json[i].Total + '</lable> </td>';
                    //html += '<td >  <input type="button"  id="bnSplirt' + rowCount + '" onclick="FunSplirt(this)" class="btn" value="拆分" /></td>'; name="' + json[i].LJCPID + '' + json[i].OrderContent + '-' + rowCount + '' + rowCount + '"
                    html += '<td > <a  id="bnSplirt' + rowCount + '" style="color : blue" onclick="FunSplirt(this);">拆分</a></td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
            }
        }
    });

    $("#btnSubmit").click(function () {
        var ddid = location.search.split('&')[0].split('=')[1];
        var rownumber = "";
        var ljcpid = "";
        var ljpid = "";
        var ljnames = "";
        var ljspes = "";
        var supplier = "";
        var ljnums = "";
        var ljunits = "";
        var ljunitprice = "";
        var ljzj = "";
        var ljprice2 = "";
        var ljzj2 = "";
        var tbody = document.getElementById("GXInfo1");

        for (var i = 0; i < tbody.rows.length; i++) {


            var RowNumber = document.getElementById("RowNumber" + i).innerHTML;
            var LJCPID = document.getElementById("LJCPID" + i).innerHTML;
            var LJPid = document.getElementById("LJPid" + i).innerHTML;
            var LJNames = document.getElementById("LJNames" + i).innerHTML;
            var LJSpes = document.getElementById("LJSpes" + i).innerHTML;
            var Supplier = document.getElementById("Supplier" + i).innerHTML;
            var LJNums = document.getElementById("LJNums" + i).value;
            var LJUnits = document.getElementById("LJUnits" + i).innerHTML;
            var LJUnitPrice = document.getElementById("LJUnitPrice" + i).innerHTML;
            var LJZJ = document.getElementById("LJZJ" + i).innerHTML;
            var LJPrice2 = document.getElementById("LJPrice2" + i).innerHTML;
            var LJZJ2 = document.getElementById("LJZJ2" + i).innerHTML;


            rownumber += RowNumber;
            ljcpid += LJCPID;
            ljpid += LJPid;
            ljnames += LJNames;
            ljspes += LJSpes;
            supplier += Supplier;
            ljnums += LJNums;
            ljunits += LJUnits;
            ljunitprice += LJUnitPrice;
            ljzj += LJZJ;
            ljprice2 += LJPrice2;
            ljzj2 += LJZJ2;

            if (i < tbody.rows.length - 1) {
                rownumber += ",";
                ljcpid += ",";
                ljpid += ",";
                ljnames += ",";
                ljspes += ",";
                supplier += ",";
                ljnums += ",";
                ljunits += ",";
                ljunitprice += ",";
                ljzj += ",";
                ljprice2 += ",";
                ljzj2 += ",";
            }
            else {
                rownumber += "";
                ljcpid += "";
                ljpid += "";
                ljnames += "";
                ljspes += "";
                supplier += "";
                ljnums += "";
                ljunits += "";
                ljunitprice += "";
                ljzj += "";
                ljprice2 += "";
                ljzj2 += "";
            }

        }



        isConfirm = confirm("确定要拆分吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            $.ajax({
                url: "LJSplitsInsert",
                type: "Post",
                data: {
                    Rownumber: rownumber, Ljcpid: ljcpid, Ljpid: ljpid, Ljnames: ljnames, Ljspes: ljspes, Supplier: supplier, Ljnums: ljnums, Ljunits: ljunits,
                    Ljunitprice: ljunitprice, Ljzj: ljzj,
                    Ljprice2: ljprice2, Ljzj2: ljzj2, DDID: ddid
                },
                async: false,
                success: function (data) {
                    if (data.success == true) {
                        alert("成功");
                        setTimeout('parent.ClosePop()', 100);
                    }
                    else {
                        alert("失败");
                    }
                }
            });
        }
    });
});

function FunSplirt(rowCount) {
    if (document.getElementById("btnSubmit").style.display == "none") {
        alert("请输入正确的数量！！！");
        return;
    }
    else {
        $("#btnSubmit").css("display", "none")
    }

    var ary = rowCount.id.split('t');
    $("#bnSplirt" + ary[1]).css("display", "none");
    var name = document.getElementById('LJNamess' + ary[1]).innerHTML;
    var cpid = document.getElementById('LJCPIDs' + ary[1]).innerHTML;
    var CFAmount = document.getElementById('LJNumss' + ary[1]).innerHTML;
    $.ajax({
        url: "SelectSplitLJxq",
        type: "post",
        data: { Name: name, CPID: cpid },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo1").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html += "<tr>";
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJCPID' + rowCount + '">' + document.getElementById('LJCPIDs' + ary[1]).innerHTML + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJPid' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJNames' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJSpes' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><input type="text"  name="' + document.getElementById('LJCPIDs' + ary[1]).innerHTML + '' + json[i].ProName + '-' + rowCount + '" id="LJNums' + rowCount + '" style="width:100px" onblur="fun(this)" value="0" > </td>';

                    html += '<td style="display:none"><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';

                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJUnits' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="LJUnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    //var a = $("#LJNums'" + rowCount + "'").val() * json[i].UnitPrice;style="display:none" 
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJZJ' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJPrice2' + rowCount + '">' + json[i].Price2 + '</lable> </td>';

                    //var b = $("#LJNums'" + rowCount + "'").val() * json[i].Price2;
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="LJZJ2' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';

                    html += '<td style="display:none" ><lable class="labOrderContent' + rowCount + ' " id="CFAmount' + rowCount + '">' + CFAmount + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo1").append(html);
                }
            }
        }
    });
}
var Bnum = 0;
var bbb = 0;
function fun(order) {
    $("#btnSubmit").css("display", "none")
    var Num = 0;
    var CAmount = 0;
    var id = order.name;// 传过来的id 
    var inText = id.split('-')[0];
    var tbody = document.getElementById("GXInfo1");
    var tds = tbody.getElementsByTagName("tr");
    // 获取的所有input的 id值 
    var checks = document.getElementsByTagName("input");
    for (var j = 0; j < checks.length; j++) {
        var sID = checks[j].name;// 循环出的id
        if (sID.indexOf(inText) >= 0) {// 遍历ID前面部分与点击传递来的id前面部分一样 则累加
            Num += parseInt(document.getElementById('LJNums' + sID.split('-')[1]).value);
            CAmount = parseInt(document.getElementById('CFAmount' + sID.split('-')[1]).innerHTML);
        }
    }
    if (Num == CAmount) {
        $("#btnSubmit").css("display", "")
    }
}