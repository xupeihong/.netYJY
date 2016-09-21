var DID;
var rowCount;
$(document).ready(function () {
    $("#ProOutTime").val("");
    $("#btnSave").click(function () {
        var ID = $("#ID").val();
        var CreateUnitID = $("#CreateUnitID").val();
        var Inspector = $("#Inspector").val();

        //var Handlers = $("#Handlers").val();
        //var UserID = $("#UserID").val();
        //出库仓库

        var IsHouseIDonechu = $("#IsHouseIDonechu").val();
        var IsHouseIDoneru = $("#IsHouseIDoneru").val();

        var IsHouseIDtwochu = $("#IsHouseIDtwochu").val();
        var IsHouseIDtworu = $("#IsHouseIDtworu").val();

        var ReasonRemark = $("#ReasonRemark").val();
        var Remark = $("#Remark").val();
        var Count = $("#Amount").val();
        var CreateUser = $("#CreateUser").val();

        if (CreateUnitID == "" || CreateUnitID == null) {
            alert("申请人不能为空");
            return;
        }
        if (Inspector == "" || Inspector == null) {
            alert("单据日期不能为空");
            return;
        }

        var MainContent = "";
        var PID = "";
        var ProName = "";
        var SpecsModels = "";
        var UnitName = "";
        var StockOutCount = "";
        var UnitPrice = "";
        var TotalAmount = "";
        var Manufacturer = "";
        var Remark = "";
        var Price2 = "";
        var NOPrice = "";

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = tbody.getElementsByTagName("tr")[i].cells[0].innerText;//document.getElementById("RowNumber" + i).innerHTML;
            var pID = tbody.getElementsByTagName("tr")[i].cells[1].innerText;//document.getElementById("PID" + i).innerHTML;
            var proName = tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("ProName" + i).innerHTML;
            var specsModels = tbody.getElementsByTagName("tr")[i].cells[3].innerText;//document.getElementById("Spec" + i).innerHTML;
            var unitName = tbody.getElementsByTagName("tr")[i].cells[4].innerText;//document.getElementById("Units" + i).innerHTML;
            var stockOutCount = document.getElementById("Amount" + i).value;
            var unitPrice = tbody.getElementsByTagName("tr")[i].cells[6].innerText;// document.getElementById("UnitPrice" + i).innerHTML;
            var price2 = tbody.getElementsByTagName("tr")[i].cells[7].innerText;//document.getElementById("Price2" + i).innerHTML;
            var totalAmount = document.getElementById("Total" + i).value;//tbody.getElementsByTagName("tr")[i].cells[8].innerText;//
            var noprice = document.getElementById("NOPrice" + i).value;//tbody.getElementsByTagName("tr")[i].cells[9].innerText;//
            var manufacturer = tbody.getElementsByTagName("tr")[i].cells[10].innerText;//document.getElementById("Manufacturer" + i).innerHTML;
            var remark = tbody.getElementsByTagName("tr")[i].cells[11].innerText;//document.getElementById("Remark" + i).innerHTML;
            MainContent += mainContent;
            PID += pID;
            ProName += proName;
            SpecsModels += specsModels;
            UnitName += unitName;
            StockOutCount += stockOutCount;
            UnitPrice += unitPrice;
            TotalAmount += totalAmount;
            Manufacturer += manufacturer;
            Remark += remark;
            Price2 += price2;
            NOPrice += noprice;

            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                PID += ",";
                ProName += ",";
                SpecsModels += ",";
                UnitName += ",";
                StockOutCount += ",";
                UnitPrice += ",";
                TotalAmount += ",";
                Manufacturer += ",";
                Remark += ",";
                Price2 += ",";
                NOPrice += ",";

            }
            else {
                MainContent += "";
                PID += "";
                ProName += "";
                SpecsModels += "";
                UnitName += "";
                StockOutCount += "";
                UnitPrice += "";
                TotalAmount += "";
                Manufacturer += "";
                Remark += "";
                Price2 += "";
                NOPrice += "";
            }
        }
        $.ajax({
            url: "SaveAllocationSheet",
            type: "Post",
            data: {
                ID: ID, CreateUnitID: CreateUnitID, Inspector: Inspector, IsHouseIDonechu: IsHouseIDonechu, ReasonRemark: ReasonRemark, IsHouseIDoneru: IsHouseIDoneru,
                Remark: Remark, CreateUser: CreateUser, Count: Count, IsHouseIDtwochu: IsHouseIDtwochu, IsHouseIDtworu: IsHouseIDtworu,
                MainContent: MainContent, PID: PID, ProName: ProName, SpecsModels: SpecsModels, UnitName: UnitName, StockOutCount: StockOutCount,
                UnitPrice: UnitPrice, TotalAmount: TotalAmount, Manufacturer: Manufacturer, Remark: Remark, Price2: Price2, NOPrice: NOPrice
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
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
});
function Houselist() {
    var DeptId = $("#BMID").val();
    $.ajax({
        url: "GetUserName",
        type: "post",
        data: { DeptId: DeptId },
        dataType: "json",
        success: function (data) {
            var items = "";
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    items += "<option value='" + json[i].HouseID + "'>" + json[i].HouseName + "</option>";
                }
            }
            $("#Handlers").html(items);
        }
    })

}
function addBasicDetail(PID) { //增加货品信息行
    //判断重复数据
    var tbody = document.getElementById("DetailInfo");
    if (tbody.rows.length == 0) {
      
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
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
                        html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                        html += '<td ><lable class="labPrice2' + rowCount + ' " id="Price2' + rowCount + '">' + json[i].Price2 + '</lable> </td>';
                        html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                        html += '<td ><input type="text" id="NOPrice' + rowCount + '" style="width:60px;"/></td>';
                        html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                        html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '</tr>'
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
                        html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td ><lable class="labUnits' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" id="Amount' + rowCount + '" style="width:30px;" onblur="OnBlurAmount(this);"/></td>';
                        html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
                        html += '<td ><lable class="labPrice2' + rowCount + ' " id="Price2' + rowCount + '">' + json[i].Price2 + '</lable> </td>';
                        html += '<td ><input type="text" id="Total' + rowCount + '" style="width:60px;"/></td>';
                        html += '<td ><input type="text" id="NOPrice' + rowCount + '" style="width:60px;"/></td>';
                        html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                        html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                    GetAmount();
                }
            }
        })
    }
}
function AddNewOut() {
    // ShowIframe1("选择货品信息", "../InventoryManage/ChangeBasicOut", 500, 350);
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
var newRowID;
function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");

}
function DelRow() {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var tbodyID = "DetailInfo";
        var rowIndex = -1;
        var typeNames = ["RowNumber", "PID", "OrderContent", "Specifications", "Unit", "Amount", "UnitPrice", "Total", "Supplier", "Remark", 'PID'];

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

                            var oldid = typeNames[k] + (parseInt(i) + 1);
                            var newid = typeNames[k] + i;

                            var reg = new RegExp(oldid, "g");

                            html = html.replace(reg, newid);

                        }
                        tr1.html(html);
                    }
                    $("#RowNumber" + i).html(parseInt(i) + 1);
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
        GetCount();
        $("#DetailInfo tr").removeAttr("class");
    }
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
function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    // var strRow = newCount.charAt(newCount.length - 1);
    var strRow = newCount.substring(6);//Amount
    var AmountC = $("#Count" + strRow).val();
    if (parseInt(AmountC) < parseInt(Count)) {
        alert("所填的数量已超过此物品数量，请重新填写...");
        $("#" + newCount).val("0");
        $("#Total" + strRow).val("0");
        $("#NOPrice" + strRow).val("0");
        return;
    }
    //var strU = "#UnitPrice" + strRow;
    //var strU2 = "#Price2" + strRow;

    //var strUnitPrice = $(strU).text();

    //var strUnitPrice = document.getElementById("DetailInfo");
    var strUnitPrice = document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[6].innerText;
    // var strPrice2 = document.getElementById("DetailInfo");
    var strPrice2 = document.getElementById("DetailInfo").getElementsByTagName("tr")[strRow].cells[7].innerText;
    // var strPrice2 = $(strU2).text();
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    if (isNaN(strTotal)) {
        $("#Total" + strRow).val("");
    }
    else {
        $("#Total" + strRow).val(strTotal);
    }
    var strTotal2 = parseFloat(Count) * parseFloat(strPrice2);
    if (isNaN(strTotal2)) {
        $("#NOPrice" + strRow).val("");
    }
    else {
        $("#NOPrice" + strRow).val(strTotal2);
    }
    GetAmount();
    GetCount();
}
function changcollege(va) {
    $("#chuonetd").show();
    $("#chuonetdw").show();
    $("#chutwotd").show();
    $("#chutwotdw").show();
    $("#ruonetd").show();
    $("#ruonetdw").show();
    $("#rutwotd").show();
    $("#rutwotdw").show();

    document.getElementById("IsHouseIDonechu").options.length = 0;
    document.getElementById("IsHouseIDtwochu").options.length = 0;
    document.getElementById("IsHouseIDoneru").options.length = 0;
    document.getElementById("IsHouseIDtworu").options.length = 0;

    document.getElementById("IsHouseIDonechu").options.add(new Option("请选择", "0"));
    document.getElementById("IsHouseIDtwochu").options.add(new Option("请选择", "0"));
    document.getElementById("IsHouseIDoneru").options.add(new Option("请选择", "0"));
    document.getElementById("IsHouseIDtworu").options.add(new Option("请选择", "0"));
    $.ajax({
        url: "GetHouseIDoneNew",
        type: "post",
        data: { va: va },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var IsHouseIDonechu = document.getElementById("IsHouseIDonechu");
                    IsHouseIDonechu.add(new Option(json[i].HouseName, json[i].HouseID));
                    var IsHouseIDoneru = document.getElementById("IsHouseIDoneru");
                    IsHouseIDoneru.add(new Option(json[i].HouseName, json[i].HouseID));
                }
            }
        }
    })
    $.ajax({
        url: "GetHouseIDtwoNew",
        type: "post",
        data: { va: va },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    var IsHouseIDtwochu = document.getElementById("IsHouseIDtwochu");
                    IsHouseIDtwochu.add(new Option(json[i].HouseName, json[i].HouseID));

                    var IsHouseIDtworu = document.getElementById("IsHouseIDtworu");
                    IsHouseIDtworu.add(new Option(json[i].HouseName, json[i].HouseID));
                }
            }
        }
    })
}
//出库一级显示隐藏出库二级
function chuone() {
    $("#chutwotd").hide();
    $("#chutwotdw").hide();

    // $("#ruonetd").show();
}
//出库二级级显示隐藏出库一级
function chutwo() {
    $("#chuonetd").hide();
    $("#chuonetdw").hide();
}
//入库二级级显示隐藏出库一级
function rutwo() {
    $("#ruonetd").hide();
    $("#ruonetdw").hide();
}
//出库一级显示隐藏出库二级
function ruone() {
    $("#rutwotd").hide();
    $("#rutwotdw").hide();
}