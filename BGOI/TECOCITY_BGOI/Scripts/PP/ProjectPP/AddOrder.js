var RowId = 0;
$(document).ready(function () {

    if (location.search != "") {
        if (location.search.split('&')[0].split('=')[0] == "?XJID") {
            XJID = location.search.split('&')[0].split('=')[1];
            $.ajax({
                url: "SelectGoodsXJID",
                type: "post",
                data: { XJID: XJID },
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
                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].XXID + '</lable> </td>';
                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].Amount + '" onblur="OnBlurAmount(this);"  id="Amount' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].Supplier + '"  id="Supplier' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].NegotiatedPricingNoTax + '" onblur="OnBlurAmount(Amount' + rowCount + ');"  id="UnitPriceNoTax' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].TotalNegotiationNoTax + '"  id="TotalNoTax' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].GoodsUse + '"  id="GoodsUse' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].DrawingNum + '"  id="DrawingNum' + rowCount + '"> </td>';
                            html += '</tr>'
                            $("#GXInfo").append(html);
                        }
                    }
                }
            });
        }
        else if (location.search.split('&')[0].split('=')[0] == "?CID") {
            CID = location.search.split('&')[0].split('=')[1];
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

                            html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';

                            html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';

                            html += '<td ><input type="text" style="width:100px" value="' + json[i].Amount + '"  onblur="OnBlurAmount(this);" id="Amount' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].Supplier + '" onclick=CheckSupplier(this)  id="Supplier' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].UnitPriceNoTax + '" onblur="OnBlurAmount(Amount' + rowCount + ');"  id="UnitPriceNoTax' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].TotalNoTax + '"  id="TotalNoTax' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].GoodsUse + '"  id="GoodsUse' + rowCount + '"> </td>';
                            html += '<td ><input type="text" style="width:100px" value="' + json[i].DrawingNum + '"  id="DrawingNum' + rowCount + '"> </td>';
                            html += '</tr>'
                            $("#GXInfo").append(html);
                        }


                    }
                }
            });

        }

    }

    $("#upLoad").click(function () {
        var PID = $("#DDID").val();
        //window.parent.OpenDialog("管理文件", "../PPManage/AddFile?PID=" + PID, 500, 300, '');
        ShowIframe1("管理文件", "../PPManage/AddFile?PID=" + PID, 500, 300, '');
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

        var str = document.getElementById("GXInfo").innerHTML;
        if (str == "") {
            alert("请选择商品");
            return;
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


        var rownumber = "";
        var proname = "";
        var spec = "";
        var units = "";
        var amount = "";
        var supplier = "";
        var total = "";
        var totalnotax = "";
        var goodsuse = "";
        var materialno = "";
        var drawingNum = "";
        var tbody = document.getElementById("GXInfo");

        for (var i = 0; i < tbody.rows.length; i++) {



            if (location.search.split('&')[0].split('=')[1] != undefined) {
                //请购单id
                var CID = location.search.split('&')[0].split('=')[1];
            }
            else {
                var CID = "无";
            }
            var MaterialNO = document.getElementById("MaterialNO" + i).innerHTML;
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
            var Supplier = document.getElementById("Supplier" + i).value;
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
            //用途
            var GoodsUse = document.getElementById("GoodsUse" + i).value;
            if (GoodsUse == "") {
                var GoodsUse = "无";
            }
            var DrawingNum = document.getElementById("DrawingNum" + i).value;
            if (DrawingNum == "") {
                var DrawingNum = "无";

            }
            rownumber += RowNumber;
            proname += ProName;
            spec += Spec;
            units += Units;
            amount += Amount;
            supplier += Supplier;
            total += Total;
            totalnotax += TotalNoTax;
            goodsuse += GoodsUse;
            materialno += MaterialNO;
            drawingNum += DrawingNum;
            if (i < tbody.rows.length - 1) {
                rownumber += ",";
                proname += ",";
                spec += ",";
                units += ",";
                amount += ",";
                supplier += ",";
                total += ",";
                totalnotax += ",";
                goodsuse += ",";
                materialno += ",";
                drawingNum += ",";
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
                goodsuse += "";
                materialno += "";
                drawingNum += "";
            }

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
                    url: "InsertOrder",
                    type: "Post",
                    data: {
                        rownumber: rownumber, DDID: DDID, CID: CID, materialno: materialno, OrderDate: OrderDate, Begin: Begin, DeliveryMethod: DeliveryMethod, IsInvoice: IsInvoice,
                        PaymentMethod: PaymentMethod, OrderContacts: OrderContacts, PaymentAgreement: PaymentAgreement, ContractNO: ContractNO, proname: proname,
                        TheProject: TheProject, OrderContacts: OrderContacts, TaskNum: TaskNum, BusinessTypes: BusinessTypes,
                        spec: spec, units: units, amount: amount, supplier: supplier, total: total, totalnotax: totalnotax, goodsuse: goodsuse, drawingNum: drawingNum
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
        }
    });
});

function AddNewBasic() {
    ShowIframe1("选择货品信息", "../PPManage/ChangeBasic", 800, 500);
}

function CheckSupplier(rowid) {
    RowId = rowid.id;
    ShowIframe1("选择供货商信息", "../PPManage/Supplier", 500, 350);
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
    rowCount = document.getElementById("GXInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
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
                    var html = "";
                    html = '<tr  id ="GXInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labProductID' + rowCount + ' " id="PIDS' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text" style="width:100px" value="0"   onblur="OnBlurAmount(this);" id="Amount' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"   onclick=CheckSupplier(this)  id="Supplier' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px" value="0"  onblur="OnBlurAmount(Amount' + rowCount + ');"   id="UnitPriceNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"    id="TotalNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"    id="GoodsUse' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px"  id="DrawingNum' + rowCount + '"> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
            }
        }
    })
}

function OnBlurAmount(rowcount) //求金额和
{
    var newCount = rowcount.id;
    var Count = $("#" + newCount).val();
    var strRow = newCount.charAt(newCount.length - 1);

    var strU = "#UnitPriceNoTax" + strRow;
    var strUnitPrice = $(strU).val();
    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);

    $("#TotalNoTax" + strRow).val(strTotal);

    GetAmount();
}
function GetAmount() {  //获取总数金额
    $("#AmountM").val("");
    var Amount = 0;
    var tbody = document.getElementById("GXInfo");
    for (var i = 1; i < tbody.rows.length; i++) {
        var totalAmount = document.getElementById("TotalNoTax" + i).value;
        if (totalAmount == "" || totalAmount == null) {
            totalAmount = "0";
        }
        Amount = Amount + parseFloat(totalAmount);

        $("#AmountM").val(Amount);
    }
}

function DelRow() {
    var tbodyID = "GXInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "ProName", "Spec", "MaterialNO", "PIDS", "Units", "Amount", "Supplier", "UnitPriceNoTax", "TotalNoTax", "GoodsUse", "Remark"];

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
        for (var row = 2; row <= rowCount; row++) {
            var tr = $('<tr></tr>');
            for (var col = 1; col <= colCount - 0; col++) {
                var td = '';
                var empty = objsheet.cells(row, 1).Value;
                if (empty == "" || empty == undefined || empty == null)
                    return;
                else {
                    var value = objsheet.cells(row, col).Value;
                    if (value == "" || value == undefined || value == null)
                        td = $('<td></td>');
                    else
                        td = $('<td>' + value + '</td>');
                    td.appendTo(tr);
                }
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

function Save() {
    var record;
    var tab = $("#tabStatistic");
    record = tab[0].rows.length;
    if (record == 0) {
        alert("请重新上传文件");
        return false;
    }
    else {
        $("#shangcuan").hide();
        UpLoadPlandata();
        return true;
    }
}


// 将数据保存到数据库中
function UpLoadPlandata() {
    $("#shangcuan").hide();
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
                if (col == countCol - 1) {
                    strs += "'" + tab[0].rows[n].cells[col].innerText + "'";
                }
                else {
                    strs += "'" + tab[0].rows[n].cells[col].innerText + "',";
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
                    str += "'" + tab[0].rows[i].cells[col].innerText + "'";
                }
                else {
                    str += "'" + tab[0].rows[i].cells[col].innerText + "',";
                }
            }
        }
        if (arr != "" && arr != null) {
            str += strs + "!";//每条完整数据用！分隔
        }
    }
    str = str.substring(0, str.length - 1);

    //订购时间
    var OrderDate = $("#OrderDate").val();
    if (OrderDate == "") {
        alert("订购时间不可为空");
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






