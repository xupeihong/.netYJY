var RowId = 0;
$(document).ready(function () {
    //style = "display:none"
    if (location.search != "") {
        CID = location.search.split('&')[0].split('=')[1];
        var list = new Array();
        list = CID.split("-");

        if (list[0] == "QG") {

            $.ajax({
                url: "SelectGoodsQGID",
                type: "post",
                data: { CID: CID },
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
                            html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                            html += '<td style="display:none" ><lable class="labProductID' + rowCount + ' " id="PIDS' + rowCount + '">' + json[i].INID + '</lable> </td>';
                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';

                            html += '<td ><input type="text" style="width:100px" value="' + json[i].Amount + '"  onblur="OnBlurAmount(this,' + rowCount + ');" id="Amount' + rowCount + '"> </td>';

                            html += '<td ><lable class="labOrderContent' + rowCount + ' " id="supplierss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';

                            html += '<td  style="display:none"><lable class="labOrderContent' + rowCount + ' " id="supplier' + rowCount + '">' + json[i].SID + '</lable> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPriceNoTax + '"  onblur="OnBlurAmount(Amount' + rowCount + ',' + rowCount + ');"   id="UnitPriceNoTax' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].TotalNoTax + '"   id="TotalNoTax' + rowCount + '"> </td>';

                            html += '<td ><input type="text" style="width:100px" value="' + json[i].DrawingNum + '"    id="Price2' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].GoodsUse + '"  id="TotalTax' + rowCount + '"> </td>';
                            html += '</tr>'
                            $("#GXInfo").append(html);
                        }
                    }
                }
            })
        }
    }



    $("#upLoad").click(function () {
        var PID = $("#DDID").val();
        window.parent.OpenDialog("管理文件", "../PPManage/AddFile?PID=" + PID, 500, 300, '');
    });

    $("#DH").click(function () {
        var text = $("#xzrwlx").val();
        if (text == "工程销售") {
            ShowIframe1("选择任务单号", "../PPManage/OrderInfoManage", 1095, 400);
        }
        if (text == "家用产品销售") {
            ShowIframe1("选择任务单号", "../PPManage/SalesRetailManage", 1095, 400);
        }
        if (text == "工程项目") {
            ShowIframe1("选择任务单号", "../PPManage/General", 1095, 400);
        }
        if (text == "") {
            alert("请选择业务类型");
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
        if (GoodsType == "")
        {
            alert("请选择产品类型");
            return;
        }

        var str = document.getElementById("GXInfo").innerHTML;
        if (str == "") {
            alert("请选择商品");
            return;
        }

        var DDID = $("#DDID").val();

        //所属项目
        var TheProject = $("#TheProject").val();
        if (TheProject == "") {
            var TheProject = "无";

        }
        //请购人
        var OrderContacts = $("#OrderContacts").val();

            if (location.search.split('&')[0].split('=')[1] != undefined) {
                //请购单id
                var CID = location.search.split('&')[0].split('=')[1];
            }
            else {
                var CID = "无";
            }

            var rownumber = "";
            var pids = "";
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



 
            //序号
            var RowNumber = document.getElementById("RowNumber" + i).innerHTML;

            var Pids = document.getElementById("PIDS" + i).innerHTML;
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
            pids += Pids;
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
                pids += ",";
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
                pids += "";
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
                if (Contract == "")
                {
                    alert("请选择合同");
                    return;
                }
                if (Contract == 1) {
                    ContractNoReason = $("#ContractNoReason").val();
                }
                else
                {
                    ContractNoReason = "无"
                }
                SaleUnitPrice = $("#SaleUnitPrice").val();
                ContractTotal = $("#ContractTotal").val();
                FKexplain = $("#FKexplain").val();
                ProjectHK = $("#ProjectHK").val();
            }


            isConfirm = confirm("确定要添加吗")
            if (isConfirm == false) {
                return false;
            }
            else {

                $.ajax({
                    url: "ErInsertOrder",
                    type: "Post",
                    data: {
                        theproject:TheProject, goodstype:GoodsType, rownumber: rownumber, ddid: DDID, cid: CID, orderDate: OrderDate, begin: Begin, proname: proname, spec: spec, units: units, amount: amount, supplier: supplier, total: total, totalnotax: totalnotax, price2: price2, totaltax: totaltax,
                        stocksituation: StockSituation, projectpeople: ProjectPeople, contract: Contract, tsix: Tsix, contractnoreason: ContractNoReason, saleunitprice: SaleUnitPrice, contracttotal: ContractTotal, fkexplain: FKexplain, Projecthk: ProjectHK,pidss:pids

                    },
                    async: false,
                    success: function (data) {
                        if (data.success == true) {
                            if (location.search.split('&')[0].split('=')[0] != "") {
                                window.parent.frames["iframeRight"].reload();
                            }
                            alert("成功");
                        }
                        else {
                            alert("失败");
                        }
                    }
                });

            
        }
    });


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
});

function AddNewBasic() {
    //window.parent.OpenDialog("选择货品信息", "../PPManage/ChangeBasic", 800, 500);
    window.parent.OpenDialog("选择货品信息", "../InventoryManage/ChangeBasic?id=2&type=QG", 550, 500);
}

function CheckSupplier(rowid) {
    RowId = rowid.id;
    ShowIframe1("选择供货商信息", "../PPManage/UpdateSupplier", 500, 350);
}
function Getid(id) {
    document.getElementById("TaskNum").value = id;
}

function addSupplier(SID) {
    var rownumber = RowId.substr(RowId.length - 1, 1);
    //var ProID = document.getElementById("PIDS" + rownumber).innerHTML;
    $.ajax({
        url: "GetSupplier",
        type: "post",
        data: { SID: SID },
        dataType: "json",
        ansyc: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#Supplier" + rownumber).val(json[0].COMNameC);

                    var ProID = document.getElementById("MaterialNO" + rownumber).innerHTML;
                    var SupID = document.getElementById("Supplier" + rownumber).value;
                    $.ajax({
                        url: "GetProductPrice",
                        type: "post",
                        data: { ProID: ProID, SupID: SupID },
                        dataType: "json",
                        ansyc: false,
                        success: function (data) {

                            var json = eval(data.datas);
                            if (json.length > 0) {

                                $("#UnitPriceNoTax" + rownumber).val(json[0].price);
                            }
                        }
                    });
                }
            }
        }
    });
}

function addBasicDetail(PID) { //增加货品信息行
    var tbody = document.getElementById("GXInfo");
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
                        rowCount = document.getElementById("GXInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="GXInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td style="display:none" ><lable class="labProductID' + rowCount + ' " id="PIDS' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" style="width:100px" value="0"   onblur="OnBlurAmount(this,' + rowCount + ');" id="Amount' + rowCount + '"> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="supplierss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td  style="display:none"><lable class="labOrderContent' + rowCount + ' " id="supplier' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                        html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPrice + '"  onblur="OnBlurAmount(Amount' + rowCount + ',' + rowCount + ');"   id="UnitPriceNoTax' + rowCount + '"> </td>';
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
    else {
        var array = new Array();
        var array = PID.split(',');
        for (var i = 0; i < tbody.rows.length; i++) {
            var pID = tbody.getElementsByTagName("tr")[i].cells[3].innerText;
            for (var n = 0; n < array.length; n++) {
                if (array[n].replace("'", "").replace("'", "").trim() == pID.trim()) {
                    alert("所选商品有重复！！！");
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
                        rowCount = document.getElementById("GXInfo").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="GXInfo' + rowCount + '" onclick="selRow(this)">'
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="PIDS' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><input type="text" style="width:100px" value="0"   onblur="OnBlurAmount(this,' + rowCount + ');" id="Amount' + rowCount + '"> </td>';
                        html += '<td ><lable class="labOrderContent' + rowCount + ' " id="supplierss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                        html += '<td  style="display:none"><lable class="labOrderContent' + rowCount + ' " id="supplier' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                        html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPrice + '"  onblur="OnBlurAmount(Amount' + rowCount + ',' + rowCount + ');"   id="UnitPriceNoTax' + rowCount + '"> </td>';
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
}

//function OnBlurAmount(rowcount) //求金额和
//{ 
//    var newCount = rowcount.id;
//    var Count = $("#" + newCount).val();
//    var strRow = newCount.charAt(newCount.length - 1);

//    var strU = "#UnitPriceNoTax" + strRow;
//    var strUnitPrice = $(strU).val();

//    var strP = "#Price2" + strRow;
//    var strPrice2 = $(strP).val();

//    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
//    var TotalTax = parseFloat(Count) * parseFloat(strPrice2);

//    $("#TotalNoTax" + strRow).val(strTotal);
//    $("#TotalTax" + strRow).val(TotalTax);
//}

function OnBlurAmount(rowcount, count) //求金额和
{

    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    var strRow = newCount.charAt(newCount.length - 1);

    var strU = "#UnitPriceNoTax" + count;
    var strUnitPrice = $(strU).val();

    var strP = "#Price2" + count;
    var strPrice2 = $(strP).val();

    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
    var TotalTax = parseFloat(Count) * parseFloat(strPrice2);

    $("#TotalNoTax" + count).val(strTotal);
    $("#TotalTax" + count).val(TotalTax);
}
function GetAmount() {  //获取总数金额
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

    //if (newRowID != "")
    //    rowIndex = newRowID.replace(tbodyID, '');
    //if (rowIndex != -1) {
    //    document.getElementById(tbodyID).deleteRow(rowIndex);
    //    if (rowIndex < $("#" + tbodyID + " tr").length) {
    //        for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
    //            var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
    //            var tr1 = $("#" + tbodyID + (parseInt(i) + 1));
    //            tr.id = tbodyID + i;
    //            for (var j = 0; j < tr.childNodes.length; j++) {
    //                var html = tr1.html();
    //                for (var k = 0; k < typeNames.length; k++) {
    //                    var oldid = typeNames[k] + (parseInt(i) + 1);
    //                    var newid = typeNames[k] + i;
    //                    var reg = new RegExp(oldid, "g");
    //                    html = html.replace(reg, newid);
    //                }
    //                tr1.html(html);
    //            }
    //            $("#RowNumber" + i).html(parseInt(i) + 1);
    //        }
    //    }
    //    if (document.getElementById(tbodyID).rows.length > 0) {
    //        if (rowIndex == document.getElementById(tbodyID).rows.length)
    //            selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
    //        else
    //            selRow(document.getElementById(tbodyID + rowIndex), '');;
    //    }
    //}

    if (newRowID != "")
        rowIndex = newRowID.replace(tbodyID, '');
    if (rowIndex != -1) {
        document.getElementById(tbodyID).deleteRow(rowIndex);

        if (rowIndex < document.getElementById(tbodyID).rows.length) {
            for (var i = rowIndex; i < document.getElementById(tbodyID).rows.length; i++) {
                var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                tr.id = tbodyID + i;
                //tr.childNodes[0].innerHTML = parseInt(i) + 1;

                for (var j = 0; j < tr.childNodes.length; j++) {
                    var td = tr.childNodes[j];
                    td.childNodes[0].id = typeNames[j] + i;
                    td.childNodes[0].name = typeNames[j] + i;
                }
                $("#RowNumber" + i).html(parseInt(i) + 1);
            }
        }
        if (document.getElementById(tbodyID).rows.length > 0) {

            if (rowIndex == document.getElementById(tbodyID).rows.length)
                selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
            else
                selRow(document.getElementById(tbodyID + rowIndex), '');
        }
    }
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


function getDetailData() {
    var path = $("#txtPath").val();
    if (path == "") {
        alert("请选择文件路径");
        return;
    }
    else
        $("#shangcuan").show();
    readEx(path);
}

function readEx(path) {
    var ExcelSheet;
    var wb;

    try {
        ExcelSheet = new ActiveXObject("Excel.Application");
        wb = ExcelSheet.Workbooks.open(path);
        var objsheet = '';
        var num = 0;
        wb.worksheets(1).select();
        objsheet = wb.ActiveSheet;
        var colCount = objsheet.UsedRange.Columns.Count;
        var rowCount = objsheet.UsedRange.Rows.Count;
        var tab = $("#tabStatistic");
        tab.html('');

        for (var row = 2; row <= 4; row++) {
            var tr = $('<tr></tr>');
            for (var col = 1; col <= colCount - 0; col++) {
                var td = '';
                //var empty = objsheet.cells(row, 1).Value;
                //if (empty == "" || empty == undefined || empty == null)
                //    return;
                //else {
                var value = objsheet.cells(row, col).Value;
                if (value == "" || value == undefined || value == null)
                    td = $('<td></td>');
                else
                    td = $('<td>' + value + '</td>');
                td.appendTo(tr);
                //}
            }
            tr.appendTo(tab);
        }

        for (var row = 5; row <= rowCount; row++) {
            var tr = $('<tr></tr>');
            for (var col = 1; col <= colCount - 0; col++) {
                var td = '';
                var empty = objsheet.cells(row, 1).Value;
                //if (empty == "" || empty == undefined || empty == null)
                //    return;
                //else {
                var value = objsheet.cells(row, col).Value;
                if (value == undefined)
                    td = $('<td><span style="display:none">无</span></td>');
                else
                    td = $('<td>' + value + '</td>');
                td.appendTo(tr);
                //}
            }
            tr.appendTo(tab);
        }
    }
    catch (e) {
        if (ExcelSheet != undefined) {
            alert('Error happened : ' + e);
            ExcelSheet.Quit();
        }
        return '';
    }
}

//function Save() {
//    var record;
//    var tab = $("#tabStatistic");
//    record = tab[0].rows.length;
//    if (record == 0) {
//        alert("请重新上传文件");
//        return false;
//    }
//    else {
//        //$("#shangcuan").hide();
//        UpLoadPlandata();
//        return true;
//    }
//}


// 将数据保存到数据库中
function UpLoadPlandata() {
    //$("#shangcuan").hide();
    //编号/货品库类型/货品名称/物料号/规格型号/单价（含税）/不含税价格/	单位/厂家，生产厂商，供应商	/备注/详细说明/物品类型
    var tab = $("#tabStatistic");
    var countRow = tab[0].rows.length;
    var countCol = tab[0].rows[0].cells.length;
    var str = ''; // 将所有值拼接成需要的字符串
    var strs = '';
    for (var n = 0; n < 3; n++) {

        //var arr1 = tab[0].rows[0].cells[1].innerText;
        for (var col = 0; col < countCol; col++) {
            var arr = tab[0].rows[n].cells[col].innerText;
            if (arr != "" && arr != null) {
                if (parseInt(col) == parseInt(countCol) - 1) {
                    strs += "" + tab[0].rows[n].cells[col].innerText + "";
                }
                else {
                    strs += "" + tab[0].rows[n].cells[col].innerText + ",";
                }
            }
        }
    }
    for (var i = 4; i < countRow; i++) {

        //var arr1 = tab[0].rows[0].cells[1].innerText;
        for (var col = 0; col < countCol; col++) {
            var arr = tab[0].rows[i].cells[col].innerText;
            if (arr != "" && arr != null) {
                if (col == countCol - 1) {
                    str += "" + tab[0].rows[i].cells[col].innerText + ",";
                }
                else {
                    str += "" + tab[0].rows[i].cells[col].innerText + ",";
                }
            }

        }
        str += strs + "!";//每条完整数据用！分隔
    }
    str = str.substring(0, str.length - 1);

    //订购时间
    var OrderDate = $("#OrderDate").val();
    if (OrderDate == "") {
        alert("订购时间不可为空");
        return;
    }


    var strss = document.getElementById("tabStatistic").innerHTML;
    if (strss == "") {
        alert("请导入商品");
        return;
    }
    //任务单号
    var TaskNum = $("#TaskNum").val();
    if (TaskNum == "") {
        TaskNum = "无";
    }
    //选择业务类型
    var BusinessTypes = $("#xzrwlx").val();
    if (BusinessTypes == "") {
        BusinessTypes = "无";
    }


    var DDID = $("#DDID").val();
    //交货期限
    var Begin = $("#Begin").val();

    //合同编号
    var ContractNO = $("#ContractNO").val();
    if (ContractNO == "") {
        var ContractNO = "无";

    }
    //所属项目
    var TheProject = $("#TheProject").val();
    if (TheProject == "") {
        var TheProject = "无";

    }
    //请购人
    var OrderContacts = $("#OrderContacts").val();
    //交货方式
    var DeliveryMethod = $("#DeliveryMethod").val();
    if (DeliveryMethod == "") {
        alert("交货方式不可为空");
        return;
    }
    //发票
    var IsInvoice = $("#IsInvoice").val();
    if (IsInvoice == "") {
        alert("发票信息不可为空");
        return;
    }
    //支付方式
    var PaymentMethod = $("#PaymentMethod").val();
    if (PaymentMethod == "") {
        alert("支付方式不可为空");
        return;
    }
    //付款约定
    var PaymentAgreement = $("#PaymentAgreement").val();
    if (PaymentAgreement == "") {
        alert("付款约定不可为空");
        return;
    }
    isConfirm = confirm("确定要添加吗")
    if (isConfirm == false) {
        return false;
    }
    else {
        $.ajax({
            url: "SavePlanData",
            type: "post",
            data: { strData: str, orderdate: OrderDate, tasknum: TaskNum, businesstypes: BusinessTypes, ddid: DDID, begin: Begin, contractno: ContractNO, theproject: TheProject, ordercontacts: OrderContacts, deliverymethod: DeliveryMethod, isinvoice: IsInvoice, paymentmethod: PaymentMethod, paymentagreement: PaymentAgreement },
            dataType: "json",
            async: false, //是否异步
            success: function (data) {
                if (data.success == "false") {
                    if (data.Msg != "") {

                        alert(data.Msg);
                    }
                }
                else {
                    alert('货品数据保存成功');
                }
            }
        })
    }
}
function LoadTitleF1() {
    var tr1 = $("#line1");
    tr1.html('');

    //加载第一行
    var th1 = $('<td>序号</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>名称</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>图纸或规格</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>单位</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>计划采购量</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>单价</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>金额</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>税前单价</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>税前总价</td>');
    th1.appendTo(tr1);
    var th1 = $('<td>结账方式</td>');
    th1.appendTo(tr1);
    var th1 = $('<td  style="width:150px;">备注</td>');
    th1.appendTo(tr1);
}






