var curPage = 1;
var OnePageCount = 15;
var m = 0;
var ID = 0;
var RowId = 0;
var newRowID = "";
$(document).ready(function () {
    // $("#OfferTime").val("");
    //if (location.search != "") {
    //    ID = location.search.split('&')[0].split('=')[1];
    //}
    LoadProjectDetail();
    loadFile();
    $("#btnSaveOffer").click(function () {
        isConfirm = confirm("确定要报价吗");
        if (isConfirm == false) {
            return false;
        }
        else {
            //$("#uploadify").uploadify("upload", '*');
            //UpdateOffer();
            isConfirm = confirm("确定要报价吗")
            if (isConfirm == false) {
                return false;
            }
            else {
                $("#uploadify").uploadify("upload", '*');
                UpdateOffer();
            }
        }
      //  UpdateOffer();
    });
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
    $("#HJtxt").click(function () {
        HJ();
    });
    $("#uploadify").uploadify({
        uploader: 'InsertBiddingNew',           // 服务器端处理地址
        swf: '../Scripts/uploadify/uploadify.swf',    // 上传使用的 Flash

        width: 90,                          // 按钮的宽度
        height: 23,                         // 按钮的高度
        buttonText: "选择上传文件",                 // 按钮上的文字
        buttonCursor: 'hand',                // 按钮的鼠标图标

        fileObjName: 'Filedata',            // 上传参数名称
        formData: { 'RID': $("#BJID").val() },// 'Types': $("#StrType").val(), 'Comments': $("#StrComments").val()
        // 两个配套使用
        fileTypeExts: "*.xls;*.xlsx;*.jpg;*.png;*.doc;*.docx;*.pdf;*.txt;*.JPG;*.PNG,*.zip;*.rar;", // 扩展名
        fileTypeDesc: "请选择 xls xlsx jpg png doc docx pdf txt JPG PNG zip rar 文件", // 文件说明

        auto: false,                // 选择之后，自动开始上传
        multi: true,               // 是否支持同时上传多个文件

        onUploadStart: function (file) {
            //  debugger;
            if (m == -1) {
                //debugger;
                $("#uploadify").uploadify("stop");
            }
            //  alert(m);
            // m++;
            //var element = {};
            //element.RID = $("#Hidden").val();
            //element.Types = $("#StrType").val();
            //element.Comments = $("#StrComments").val();
            //$("#uploadify").uploadify('settings', 'formData', element);
        },
        onUploadComplete: function (fileObj) {
            //window.parent.frames["iframeRight"].reload();
            //setTimeout('parent.ClosePop()', 10);
        },
        onQueueComplete: function (queueData) {
            //  SaveOffer();
            //上传队列全部完成后执行的回调函数  
            window.parent.frames["iframeRight"].reload();
            setTimeout('parent.ClosePop()', 10);
        }
    });
});

//加载备案项目详细
function ShowPoject() {
    var PID = $("#PID").val();
    ShowIframe1("备案项目信息", "../SalesManage/ShowProject?PID=" + PID, 850, 450);
}

//再添加
function CheckDetail() {
    ShowIframe1("选择货品信息", "../SalesManage/ChangeProduct", 850, 450);
}
function addBasicDetail(PID) { //增加货品信息行

    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //var strPID = $("#ProductID").val();
    //$("#ProductID").val(strPID + "," + ProductID);
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
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + '" style="width:60px;" id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " style="width:60px;" id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable style="width:60px;" class="labSpecifications' + rowCount + '" id="Spec' + rowCount + '" >' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labUnit' + rowCount + '" id="Units' + rowCount + '" style="width:60px;" >' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text"  onblur=XJ()  id="Amount' + rowCount + '"  style="width:30px;" /></td>';
                    html += '<td ><input type="text" style="width:100px;" onclick=CheckSupplier()  id="Supplier' + rowCount + '" /> </td>';
                    html += '<td ><input type="text" style="width:100px;"  onblur=XJ() id="UnitPrice' + rowCount + '" /></td>';
                    html += '<td ><input type="text" style="width:100px;" readonly="readonly" id="txtTotal' + rowCount + '"  /> </td>';
                    //

                    html += '<td ><input type="text" style="width:100px;" class="labRemark' + rowCount + ' " id="Remark' + rowCount + '" value="' + json[i].Remark + '"/> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '" />' + json[i].PID + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="XID' + rowCount + '"></lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
                    rowCount += 1;
                }


            }
        }
    })
}
function LoadProjectDetail() {
    var BJID = $("#BJID").val();
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    //  var PID = ID;
    $.ajax({
        url: "GetOfferInfoGrid",
        type: "post",
        data: { BJID: BJID },
        dataType: "json",
        ansyc: false,
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {


                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">';
                    html += '<td ><lable class="labRowNumber' + rowCount + '"  id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                    html += '<td ><input type="text" class="labProductID' + rowCount + '" id="ProductID' + rowCount + '" style="width:90px;" value="' + json[i].ProductID + '" style="width:90px;"/> </td>';
                    html += '<td ><input type="text" class="labOrderContent' + rowCount + '" id="ProName' + rowCount + '" style="width:90px;" value="' + json[i].OrderContent + '"/> </td>';
                    html += '<td ><input type="text" class="labSpecifications' + rowCount + '" id="Spec' + rowCount + '" style="width:90px;" value="' + json[i].Specifications + '"/> </td>';
                    html += '<td ><input type="text" class="labUnit' + rowCount + '" id="Units' + rowCount + '" style="width:90px;" value="' + json[i].Unit + '"/> </td>';
                    html += '<td ><input type="text"  onblur=XJ(this)  id="Amount' + rowCount + '" value="' + json[i].Amount + '" style="width:90px;"/></td>';
                    html += '<td ><input type="text"  style="width:90px;" onclick=CheckSupplier()  id="Supplier' + rowCount + '" value="' + json[i].Supplier + '"> </td>';
                    html += '<td ><input type="text"   style="width:90px;" onblur=XJ(this) id="UnitPrice' + rowCount + '" value="' + json[i].UintPrice + '"></td>';
                    html += '<td ><input type="text"  style="width:90px;"  readonly="readonly" id="txtTotal' + rowCount + '" value="' + json[i].Total + '" /> </td>';
                    html += '<td ><input type="text" style="width:90px;" class="labRemark' + rowCount + ' " id="Remark' + rowCount + '" value="' + json[i].Remark + '" /> </td>';
                    html += '<td style="display:none;"><lable class="labXID' + rowCount + '" id="XID' + rowCount + '">' + json[i].XID + '</lable> </td>';
                    html += '</tr>';
                    $("#DetailInfo").append(html);

                }


            }
        }
    })
}
function selRow(curRow) {
    newRowID = curRow.id;
    RowId = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

//选择供应商
function CheckSupplier() {
    //  RowId = rowid.id;
    ShowIframe1("选择货品信息", "../SalesManage/Supplier", 500, 520);
}

function addSupplier(SID) {
    var rownumber = RowId.substr(RowId.length - 1, 1);
    var ProduID = document.getElementById("ProductID" + rownumber).innerHTML;
    $.ajax({
        url: "GetSupplier",
        type: "post",
        data: { SID: SID },
        dataType: "json",
        ansyc: false,
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                $("#Supplier" + rownumber).val(json[0].COMNameC);
                var ProID = document.getElementById("ProductID" + rownumber).innerHTML;
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
                            //$("#Supplier" + rownumber).val(json[0].COMNameC);
                            $("#UnitPrice" + rownumber).val(json[0].price);
                            XJ();
                            HJ();
                        }
                    }
                });
                //  $("#UnitPrice" + rownumber).val(json[0].Price);
                //  XJ();
            }
        }
    });
    //var s = RowId.substr(RowId.length - 1, RowId.length);
    //var ProID = document.getElementById("ProductID" + s).innerHTML;
    //var SupID = document.getElementById("Supplier" + s).value;
    //$.ajax({
    //    url: "GetProductPrice",
    //    type: "post",
    //    data: { ProID: ProID, SupID: SupID },
    //    dataType: "json",
    //    //ansyc: true,
    //    success: function (data) {

    //        var json = eval(data.datas);
    //        if (json.length > 0) {
    //            for (var i = 0; i < json.length; i++) {
    //                $("#UnitPrice" + s).val(json[i].price);
    //            }
    //            XJ();
    //        }
    //    }
    //});
    //setTimeout('XJ()', 100);

}

//添加非常规报价物品
function addOfferF() {
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var html = "";
    html = '<tr  id ="DetailInfo' + rowCount + '" onclick=selRow(this)>'
    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
    html += '<td ><input class="labProductID' + rowCount + ' " style="width:90px;" id="ProductID' + rowCount + '"/> </td>';
    html += '<td ><input class="labProName' + rowCount + ' " style="width:90px;" id="ProName' + rowCount + '" /></td>';
    html += '<td ><input class="labSpec' + rowCount + ' " style="width:90px;" id="Spec' + rowCount + '"/></td>';
    html += '<td ><input class="labUnits' + rowCount + ' " style="width:90px;" id="Units' + rowCount + '"/></td>';
    html += '<td ><input type="text"  onblur=XJ(this) style="width:90px;" id="Amount' + rowCount + '" style="width:90px;"/></td>';
    //html += '<td ><lable class="labUnitPrice' + rowCount + ' " id="UnitPrice' + rowCount + '">' + json[i].UnitPrice + '</lable> </td>';
    //html += '<td ><lable class="labManufacturer' + rowCount + ' " readonly="readonly" id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
    html += '<td ><input type="text"  style="width:90px;" id="Supplier' + rowCount + '"> </td>';
    html += '<td ><input type="text" onblur=XJ(this) style="width:90px;" id="UnitPrice' + rowCount + '"> </td>';
    html += '<td ><input type="text" id="txtTotal' + rowCount + '" readonly="readonly"  style="width:90px;" /></td>';
    html += '<td ><input class="labRemark' + rowCount + ' "  style="width:90px;" id="Remark' + rowCount + '"/></td>';
    html += '<td style="display:none;"><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '"></lable> </td>';
    html += '</tr>'
    CountRows = CountRows + 1;
    rowCount += 1;
    $("#DetailInfo").append(html);

}

function GetSupplierPrice() {
    var s = RowId.substr(RowId.length - 1, RowId.length);
    var ProID = document.getElementById("ProductID" + s).innerHTML;
    var SupID = document.getElementById("Supplier" + s).value;
    $.ajax({
        url: "GetProductPrice",
        type: "post",
        data: { ProID: ProID, SupID: SupID },
        dataType: "json",
        // ansyc: false,
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#UnitPrice" + s).val(json[i].price);
                }
            }
        }
    });
    XJ();
}
function UpdateOffer() {
    //报价主表
    var PID = $("#PID").val();
    var BJID = $("#BJID").val();
    var OfferTitle = $("#OfferTitle").val();
    var OfferTime = $("#OfferTime").val();
    var FKYD = $("#FKYD").val();
    var Description = $("#Description").val();
    var OfferContacts = $("#OfferContacts").val();
    var ISF = $("#ISF").val();
    if (OfferTitle == "" || OfferTitle == null) {
        m = -1;
        alert("报价标题不能为空");
        return;
    }
   
    if (OfferTime == "" || OfferTime == null) {
        m = -1;
        alert("报价时间不能为空");
        return;
    }
    var OfferTotal = $("#Total").val();
    if (OfferTotal == "0" || OfferTotal == null) {
        m = -1;
        alert("报价单价不正确");
        return;
    }
    var Customer = $("#Customer").val();
    var CustomerTel = $("#CustomerTel").val();
    //报价详细表
    var ProductID = "";
    var OrderContent = "";
    var SpecsModels = "";
    var Supplier = "";
    var Unit = "";
    var Amount = "";
    var UnitPrice = "";
    var Remark = "";
    var XID = "";
    var txtTotal = "";
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var Productid = document.getElementById("ProductID" + i).value;
        var mainContent = document.getElementById("ProName" + i).value;
        var specsModels = document.getElementById("Spec" + i).value;
        var unit = document.getElementById("Units" + i).value;
        var salesNum = document.getElementById("Amount" + i).value;
        var supplier = document.getElementById("Supplier" + i).value;
        var uitiprice = document.getElementById("UnitPrice" + i).value;
        var remark = document.getElementById("Remark" + i).value;
       // var xid = document.getElementById("XID" + i).innerHTML;
        var total = document.getElementById("txtTotal" + i).value;
        ID += parseInt(i + 1);
        ProductID += Productid;
        OrderContent += mainContent;
        SpecsModels += specsModels;
        Unit += unit;
        Amount += salesNum;
        Supplier += supplier;
        UnitPrice += uitiprice;
        Remark += remark;
      //  XID += xid;
        txtTotal += total;
        if (i < tbody.rows.length - 1) {
         //   ID += ",";
            ProductID += ",";
            OrderContent += ",";
            SpecsModels += ",";
            Unit += ",";
            Amount += ",";
            Supplier += ",";
            UnitPrice += ",";
            Remark += ",";
           // XID += ",";
            txtTotal += ",";
        }
        else {
           // ID += "";
            ProductID += "";
            OrderContent += "";
            SpecsModels += "";
            Unit += "";
            Amount += "";
            Supplier += "";
            UnitPrice += "";
            Remark += "";
           // XID += "";
            txtTotal += "";
        }
    }
    $.ajax({
        url: "SaveUpdateOffer",
        type: "Post",
        data: {
            PID: PID, BJID: BJID, OfferTitle: OfferTitle, OfferTime: OfferTime, FKYD: FKYD, Description: Description,ISF:ISF,
            OfferContacts: OfferContacts,Customer:Customer,CustomerTel:CustomerTel,
            XID: XID, ProductID: ProductID, OrderContent: OrderContent, SpecsModels: SpecsModels, Unit: Unit, Amount: Amount, Supplier: Supplier, UnitPrice: UnitPrice, Remark: Remark, txtTotal: txtTotal
        },
        //async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("修改完成！");
                window.parent.ClosePop();
            }
            else {
                alert("修改失败");
            }
        }
    });
}

//删除的时候
function DeleteRow() {
    var tbodyID = "DetailInfo";
    var rowIndex = -1;
    //var typeNames = ["RowNumber", "ProductID", "ProName", "Spec", "Units", "Amount", "Supplier", "UnitPrice", "Total", "Remark", "XID"];
    var typeNames = ["RowNumber", "ProductID", "ProName", "Spec", "Units", "Amount", "Supplier", "UnitPrice", "txtTotal", "Remark", ];
    if (newRowID == "") {
        alert("请选择删除的数据信息");
        return;
    }


    var rowIndex = -1;
    if (newRowID != "")
        rowIndex = newRowID.replace(tbodyID, '');
    if (rowIndex != -1) {
        document.getElementById(tbodyID).deleteRow(rowIndex);

        if (rowIndex < document.getElementById(tbodyID).rows.length) {
            for (var i = rowIndex; i < document.getElementById(tbodyID).rows.length; i++) {
                var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                tr.id = tbodyID + i;
                tr.childNodes[0].innerHTML = parseInt(i) + 1;
                for (var j = 1; j < tr.childNodes.length; j++) {
                    var td = tr.childNodes[j];
                    td.childNodes[0].id = typeNames[j] + i;
                    td.childNodes[0].name = typeNames[j] + i;

                }
            }
        }
        if (document.getElementById(tbodyID).rows.length > 0) {

            if (rowIndex == document.getElementById(tbodyID).rows.length)
                selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
            else
                selRow(document.getElementById(tbodyID + rowIndex), '');
        }
    }

    HJ();
}

function HJ() {
    var Total = 0;
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        //  var Subtotal = $("#txtTotal" + i).val();
        var Subtotal = document.getElementById("txtTotal" + i).value;
        if (Subtotal == "" || Subtotal == null) {
            Subtotal = "0";
        }
        Total = Total + parseFloat(Subtotal);


    }
    $("#Total").val(Total);
}

function XJ(rowid) {
    RowId = rowid.id;
    var Total = 0;
    var a = RowId.split('Amount');
    var b = RowId.split('UnitPrice');
    var Total = 0;
    var s = "";
    //s = a[1];
    if (a.length == 2) {
        s = a[1];
    }
    if (b.length == 2) {
        s = b[1];
    }
    var tbody = document.getElementById("DetailInfo");
    //  for (var i = 0; i < tbody.rows.length; i++) {
    var Amount = document.getElementById("Amount" + s).value;
    var UnitPrice = document.getElementById("UnitPrice" + s).value;
    if (Amount == "" || Amount == null) {
        Amount = "0";
    }
    if (UnitPrice == "" || UnitPrice == null) {
        UnitPrice = "0.00";
    }
    Total = parseFloat(Amount) * parseFloat(UnitPrice);

    $("#txtTotal" + s).val(Total);
    //}
    HJ();
}

//修改上传的文件
function loadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("BJID").value;
    $.ajax({
        url: "GetUploadFile",
        type: "post",
        data: { data1: InforNo },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var id = data.id.split('@');
                var Code = data.File.split('@');
                var Name = data.Name.split('@');
                var Banding = document.getElementById("unit");
                if (Code == "") {
                    Banding.innerHTML = "";
                    return;
                }
                for (var i = 0; i < Code.length; i++) {
                    var cross = id[i] + "/" + Name[i] + "/" + Code[i];
                    Banding.innerHTML += Name[i] + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='deleteFile(\"" + cross + "\")'>删除</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='DownloadFile(\"" + id[i] + "\")'>下载</a><br/>";
                }
            }
            else {
                alert(data.Msg);
                return;
            }
        }
    });
}
function DownloadFile(id) {
    window.open("DownLoad2?id=" + id);
}
function deleteFile(id) {
    var one = confirm("确实要删除这条条目吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deleteFile",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    loadFile();
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
    }
}