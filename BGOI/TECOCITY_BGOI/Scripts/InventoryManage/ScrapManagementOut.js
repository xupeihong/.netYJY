var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    //$("#ScrapTime").val("");
    $("#btnSave").click(function () {

        var ListScrapID = $("#ListScrapID").val();
        var SubjectID = $("#SubjectID").val();
        var ReasonRemark = $("#ReasonRemark").val();
        var ScrapTime = $("#ScrapTime").val();
        var HouseID = $("#HouseID").val();
        var Handlers = $("#Handlers").val();
        var Handling = $("#Handling").val();
        var Amount = $("#AmountM").val();
        var Count = $("#Count").val();
        if (SubjectID == "" || SubjectID == null) {
            alert("科目不能为空");
            return;
        }
        if (ListScrapID == "" || ListScrapID == null) {
            alert("报废单号不能为空");
            return;
        }
        if (ScrapTime == "" || ScrapTime == null) {
            alert("报废时间不能为空");
            return;
        }
        if (HouseID == "" || HouseID == null) {
            alert("仓库不能为空");
            return;
        }
        if (Handlers == "" || Handlers == null) {
            alert("经手人不能为空");
            return;
        }

        var MainContent = "";
        var PID = "";
        var ScrapCount = "";
        var TotalAmount = "";
        var Manufacturer = "";
        var Remark = "";

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = tbody.getElementsByTagName("tr")[i].cells[0].innerText;//document.getElementById('RowNumber' + i).innerHTML;
            var pID = tbody.getElementsByTagName("tr")[i].cells[1].innerText;// document.getElementById("PID" + i).innerHTML;
            var scrapCount = document.getElementById("Amount" + i).value;
            var totalAmount = document.getElementById("Total" + i).value;
            var manufacturer = tbody.getElementsByTagName("tr")[i].cells[8].innerText;// document.getElementById("Manufacturer" + i).innerHTML;
            var PRemark = tbody.getElementsByTagName("tr")[i].cells[9].innerText;// document.getElementById("Remark" + i).innerHTML;

            MainContent += mainContent;
            PID += pID;
            ScrapCount += scrapCount;
            TotalAmount += totalAmount;
            Manufacturer += manufacturer;
            Remark += PRemark;

            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                PID += ",";
                ScrapCount += ",";
                TotalAmount += ",";
                Manufacturer += ",";
                Remark += ",";
            }
            else {
                MainContent += "";
                PID += "";
                ScrapCount += "";
                TotalAmount += "";
                Manufacturer += "";
                Remark += "";
            }
        }

        $.ajax({
            url: "SaveScrapManagement",
            type: "Post",
            data: {
                ListScrapID: ListScrapID, SubjectID: SubjectID, ReasonRemark: ReasonRemark, ScrapTime: ScrapTime, HouseID: HouseID,
                Handlers: Handlers, PID: PID, Handling: Handling, MainContent: MainContent, Remark: Remark,Count:Count,
                ScrapCount: ScrapCount, TotalAmount: TotalAmount, Manufacturer: Manufacturer, Amount: Amount
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("添加成功");
                    setTimeout('parent.ClosePop()', 10);
                    window.parent.frames["iframeRight"].reload();
                }
                else {
                    alert(data.Msg);
                }
            }
        });
    });
});
function addBasicDetail(PID) { //增加货品信息行
    //判断重复数据
    var tbody = document.getElementById("DetailInfo");
    if (tbody.rows.length == 0) {
        //rowCount = document.getElementById("DetailInfo").rows.length;
        //var CountRows = parseInt(rowCount) + 1;
        //var strPID = $("#PID").val();
        //$("#PID").val(strPID + "," + PID);
        $.ajax({
            url: "GetBasicDetail",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">';
                        //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';

                        html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
                        html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].Price2 + '</lable> </td>';

                        html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                        html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                        html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                        html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '</tr>';
                        $("#DetailInfo").append(html);
                    }
                    GetAmount();
                }
            }
        })
    } else {
        var strPID = PID.replace("'", "").replace("'", "");
        var obj2 = strPID.split(",");
        for (var i = 0; i < tbody.rows.length; i++) {
            var pID = tbody.getElementsByTagName("tr")[i].cells[1].innerText;
            for (var j = 0; j < obj2.length; j++) {
                var newpid = obj2[j].replace("'", "").replace("'", "");
                if (newpid.replace(/[ ]/g, "") == pID.replace(/[ ]/g, "")) {
                    return;
                }
            }
        }
       
        //var strPID = $("#PID").val();
        //$("#PID").val(strPID + "," + PID);
        $.ajax({
            url: "GetBasicDetail",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">';
                        //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';

                        html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
                        html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].Price2 + '</lable> </td>';

                        html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                        html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                        html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                        html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '</tr>';
                        $("#DetailInfo").append(html);
                    }
                    GetAmount();
                }
            }
        })
    }
  
}
function AddNewBasic() { //弹出选择货品信息页面
    ShowIframe1("选择货品信息", "../InventoryManage/ChangeBasic", 760, 500);
}
function GetAmount() {  //获取总数金额
    $("#AmountM").val("");
    var Amount = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var totalAmount = document.getElementById("Total" + i).value;
        if (totalAmount == "" || totalAmount == null) {
            totalAmount = "0";
        }
        Amount = Amount + parseFloat(totalAmount);

        $("#AmountM").val(Amount);
    }
}
function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
function DelRow() { //删除货品信息行
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var tbodyID = "DetailInfo";
        var rowIndex = -1;
        var typeNames = ["RowNumber", "PID", "ProName", "Spec", "Units", "Amount", "UnitPrice", "Total", "Manufacturer", "Remark"];

        if (newRowID != "")
            rowIndex = newRowID.replace(tbodyID, '');
        if (rowIndex != -1) {
            document.getElementById(tbodyID).deleteRow(rowIndex);

            if (rowIndex < $("#" + tbodyID + " tr").length) {
                for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
                    var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                    var tr1 = $("#" + tbodyID + (parseInt(i) + 1));
                    tr.id = tbodyID + i;
                    for (var j = 0; j < tr.childNodes.length; j++) {
                        var html = tr1.html();
                        for (var k = 0; k < typeNames.length; k++) {

                            var olPID = typeNames[k] + (parseInt(i) + 1);
                            var newid = typeNames[k] + i;

                            var reg = new RegExp(olPID, "g");

                            html = html.replace(reg, newid);

                        }
                        tr1.html(html);
                    }
                    document.getElementById("RowNumber" + i).innerHTML = parseInt(i) + 1;
                    // $("#RowNumber" + i).html(parseInt(i) + 1);
                }
            }
            if (document.getElementById(tbodyID).rows.length > 0) {
                if (rowIndex == document.getElementById(tbodyID).rows.length)
                    selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
                else
                    selRow(document.getElementById(tbodyID + rowIndex), '');;
            }
        }
        GetAmount();
    }
}
function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    //var strRow = newCount.charAt(newCount.length - 1);
    var strRow = newCount.substring(6);//Amount
    //var strU = "#UnitPrice" + strRow;
    //var strUnitPrice = $(strU).text();
    var strUnitPrice = document.getElementById("DetailInfo").getElementsByTagName("tr")[0].cells[6].innerText;
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    $("#Total" + strRow).val(strTotal);
    GetAmount();
    GetCount();
}

function GetCount() {
    $("#Count").val("");
    var count = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var totalcount = document.getElementById("Amount" + i).value;
        if (totalcount == "" || totalcount == null) {
            totalcount = "0";
        }
        count = count + parseInt(totalcount);
        $("#Count").val(count);
    }
}