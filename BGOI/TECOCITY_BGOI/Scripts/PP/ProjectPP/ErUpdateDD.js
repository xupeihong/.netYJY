$(document).ready(function () {


    $("#GoodsType").bind("change", function () {
        var a = $("#GoodsType").val();
        if (a == 0) {
            $("#tablelist").css("display", "");
            $("#tablelists").css("display", "none");
        }
        else {
            $("#tablelist").css("display", "none");
            $("#tablelists").css("display", "");
        }

    });

    $("#Contract").bind("change", function () {
        var a = $("#Contract").val();

        if (a == 0) {
            $("#HTyes").css("display", "");
            $("#HTno").css("display", "none");
        }
        else if (a == 1) {
            $("#HTyes").css("display", "none");
            $("#HTno").css("display", "");
        }
        else {
            $("#HTyes").css("display", "none");
            $("#HTno").css("display", "none");
        }
    });
    $("#upLoad").click(function () {
        var texts = location.search.split('&')[0].split('=')[1];
        var PID = $("#SHID").val();
        if (texts != undefined) {
            //window.parent.OpenDialog("管理文件", "../PPManage/AddFile?PID=" + PID, 500, 300, '');
            ShowIframe1("管理文件", "../PPManage/AddFile?PID=" + PID, 500, 300, '');
        }
        else {
            window.parent.OpenDialog("管理文件", "../PPManage/AddFile?PID=" + PID, 500, 300, '');
        }
    });
    $("#btnSubmit").click(function () {

        //订购时间
        var OrderDate = $("#OrderDate").val();
        if (OrderDate == "") {
            alert("订购时间不可为空");
            return;
        }

        var GoodsType = $("#GoodsType").val();
        if (GoodsType == "") {
            alert("请选择产品类型");
            return;
        }

        var str = document.getElementById("GXInfo").innerHTML;
        if (str == "") {
            alert("请选择商品");
            return;
        }

        var DDID = location.search.split('&')[0].split('=')[1];

        //所属项目
        var TheProject = $("#TheProject").val();
        if (TheProject == "") {
            var TheProject = "无";

        }
        //请购人
        var OrderContacts = $("#OrderContacts").val();


        var rownumber = "";
        var proname = "";
        var spec = "";
        var units = "";
        var amount = "";
        var supplier = "";
        var total = "";
        var totalnotax = "";
        var goodsuse = "";
        var price2 = "";
        var totaltax = "";
        var tbody = document.getElementById("GXInfo");

        for (var i = 0; i < tbody.rows.length; i++) {



            if (location.search.split('&')[0].split('=')[1] != undefined) {
                //请购单id
                var CID = location.search.split('&')[0].split('=')[1];
            }
            else {
                var CID = "无";
            }

            //序号
            var RowNumber = document.getElementById("RowNumber" + i).innerHTML;
            //商品名称
            var ProName = document.getElementById("ProName" + i).innerHTML;
            //商品型号
            var Spec = document.getElementById("Spec" + i).innerHTML;
            //单位
            var Units = document.getElementById("Units" + i).innerHTML;
            //数量
            var Amount = document.getElementById("Amount" + i).value;
            if (Amount == "") {
                alert("数量不可为空");
                return;
            }
            //供货商
            var Supplier = document.getElementById("supplier" + i).innerHTML;
            if (Supplier == "") {
                Supplier = "无";
            }
            //单价
            var Total = document.getElementById("UnitPriceNoTax" + i).value;
            if (Total == "") {
                alert("单价不可为空");
                return;
            }
            //总价
            var TotalNoTax = document.getElementById("TotalNoTax" + i).value;
            if (TotalNoTax == "") {
                alert("总价不可为空");
                return;
            }
            //税前单价
            var Price2 = document.getElementById("Price2" + i).value;
            if (Price2 == "") {
                alert("税前单价不可为空");
                return;
            }
            //税前总价
            var TotalTax = document.getElementById("TotalTax" + i).value;
            if (TotalTax == "") {
                alert("税前总价不可为空");
                return;

            }

            rownumber += RowNumber;
            proname += ProName;
            spec += Spec;
            units += Units;
            amount += Amount;
            supplier += Supplier;
            total += Total;
            totalnotax += TotalNoTax;

            price2 += Price2;
            totaltax += TotalTax;
            if (i < tbody.rows.length - 1) {
                rownumber += ",";
                proname += ",";
                spec += ",";
                units += ",";
                amount += ",";
                supplier += ",";
                total += ",";
                totalnotax += ",";
                price2 += ",";
                totaltax += ",";
            }
            else {
                rownumber += " ";
                proname += " ";
                spec += " ";
                units += "";
                amount += " ";
                supplier += " ";
                total += " ";
                totalnotax += " ";
                price2 += "";
                totaltax += "";
            }
        }
        var StockSituation = "";
        var Begin = "";
        var TheProject = "";
        var ProjectPeople = "";
        var Contract = "";
        var Tsix = "";
        var ContractNoReason = "";
        var SaleUnitPrice = "";
        var ContractTotal = "";
        var FKexplain = "";
        var ProjectHK = "";
        if (GoodsType == 0) {
            StockSituation = $("#StockSituations").val();
            Begin = $("#Begins").val();
        }
        else {
            StockSituation = $("#StockSituation").val();
            Begin = $("#Begin").val();
            TheProject = $("#TheProject").val();
            ProjectPeople = $("#ProjectPeople").val();
            Contract = $("#Contract").val();
            Tsix = $("#Tsix").val();
            if (Contract == "") {
                alert("请选择合同");
                return;
            }
            if (Contract == 1) {
                ContractNoReason = $("#ContractNoReason").val();
            }
            else {
                ContractNoReason = "无"
            }
            SaleUnitPrice = $("#SaleUnitPrice").val();
            ContractTotal = $("#ContractTotal").val();
            FKexplain = $("#FKexplain").val();
            ProjectHK = $("#ProjectHK").val();
        }


        isConfirm = confirm("确定要修改吗")
        if (isConfirm == false) {
            return false;
        }
        else {

            $.ajax({
                url: "ErDDUpdate",
                type: "Post",
                data: {
                    theproject: TheProject, goodstype: GoodsType, rownumber: rownumber, ddid: DDID, cid: CID, orderDate: OrderDate, begin: Begin, proname: proname, spec: spec, units: units, amount: amount, supplier: supplier, total: total, totalnotax: totalnotax, price2: price2, totaltax: totaltax,
                    stocksituation: StockSituation, projectpeople: ProjectPeople, contract: Contract, tsix: Tsix, contractnoreason: ContractNoReason, saleunitprice: SaleUnitPrice, contracttotal: ContractTotal, fkexplain: FKexplain, Projecthk: ProjectHK

                },
                async: false,
                success: function (data) {
                    if (data.success == true) {
                        window.parent.frames["iframeRight"].reload();
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

    if (location.search != "") {
        DDID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "ErSelectGoodsDDID",
        type: "post",
        data: { DDID: DDID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="GXInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';;
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><input type="text" style="width:100px" value="' + json[i].Amount + '"  onblur="OnBlurAmount(this);" id="Amount' + rowCount + '"> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Suppliersss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td  style="display:none"><lable class="labOrderContent' + rowCount + ' " id="supplier' + rowCount + '">' + json[i].SID + '</lable> </td>';
                    html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPriceNoTax + '"  onblur="OnBlurAmount(Amount' + rowCount + ');"   id="UnitPriceNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"  value="' + json[i].TotalNoTax + '"   id="TotalNoTax' + rowCount + '"> </td>';

                    html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPrice + '"    id="Price2' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"  value="' + json[i].Total + '"  id="TotalTax' + rowCount + '"> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                  
                }
               
                document.getElementById("OrderDate").value = json[0].OrderDate;
                document.getElementById("GoodsType").value = json[0].GoodsType;
                if (json[0].GoodsType == "0") {
                    $("#tablelist").css("display", "");
                    $("#tablelists").css("display", "none");
                    document.getElementById("StockSituations").value = json[0].StockSituation;
                    if (json[0].DeliveryLimit != "0001/1/1 0:00:00") {
                        document.getElementById("Begins").value = json[0].DeliveryLimit;
                    }
                }
                else {
                    $("#tablelist").css("display", "none");
                    $("#tablelists").css("display", "");

                    if (json[0].DeliveryLimit != "0001/1/1 0:00:00") {
                        document.getElementById("Begin").value = json[0].DeliveryLimit;
                    }

                    document.getElementById("StockSituation").value = json[0].StockSituation;
                    document.getElementById("TheProject").value = json[0].TheProject;
                    document.getElementById("ProjectPeople").value = json[0].ProjectPeople;
                    document.getElementById("Contract").value = json[0].Contract;
                    if (json[0].Contract == "0")
                    {
                        $("#HTyes").css("display", "");
                        $("#HTno").css("display", "none");
                    }
                    else
                    {
                        $("#HTyes").css("display", "none");
                        $("#HTno").css("display", "");
    document.getElementById("ContractNoReason").value = json[0].ContractNoReason;
                    }
                    document.getElementById("Tsix").value = json[0].Tsix;
                
                    document.getElementById("SaleUnitPrice").value = json[0].SaleUnitPrice;

                    document.getElementById("ContractTotal").value = json[0].ContractTotal;
                    document.getElementById("FKexplain").value = json[0].FKexplain;
                    document.getElementById("ProjectHK").value = json[0].ProjectHK;
                }

            }

        }

    });


});

function AddNewBasic() {
    ShowIframe1("选择货品信息", "../PPManage/ChangeBasic?DD", 800, 500);
}
function OnBlurAmount(rowcount) //求金额和
{
    
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    var strRow = newCount.charAt(newCount.length - 1);

    var strU = "#UnitPriceNoTax" + strRow;
    var strUnitPrice = $(strU).val();

    var strP = "#Price2" + strRow;
    var strPrice2 = $(strP).val();

    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    var TotalTax = parseFloat(Count) * parseFloat(strPrice2);

    $("#TotalNoTax" + strRow).val(strTotal);
    $("#TotalTax" + strRow).val(TotalTax);
}
function DelRow() {
    var tbodyID = "GXInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "ProName", "Spec", "Units", "Amount", "supplier", "supplierss", "UnitPriceNoTax", "TotalNoTax", "Price2", "TotalTax"];

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


}
function addBasicDetail(PID) { //增加货品信息行
 
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
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="GXInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="PIDS' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';

                    html += '<td ><input type="text" style="width:100px" value="0"   onblur="OnBlurAmount(this);" id="Amount' + rowCount + '"> </td>';

                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="supplierss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';

                    html += '<td  style="display:none"><lable class="labOrderContent' + rowCount + ' " id="supplier' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPrice + '"  onblur="OnBlurAmount(Amount' + rowCount + ');"   id="UnitPriceNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"    id="TotalNoTax' + rowCount + '"> </td>';

                    html += '<td ><input type="text" style="width:100px" value="' + json[i].Price2 + '"    id="Price2' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"   id="TotalTax' + rowCount + '"> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
            }
        }
    })
}

var oldid;
function selRow(curRow) {
    if (oldid != null) {
        if (document.getElementById(oldid) != undefined) {
            document.getElementById(oldid).style.backgroundColor = 'white';
        }
    }
    document.getElementById("myTable").style.backgroundColor = 'white';
    newRowID = curRow.id;
    document.getElementById(newRowID).style.backgroundColor = '#EEEEF2';
    oldid = newRowID;
}