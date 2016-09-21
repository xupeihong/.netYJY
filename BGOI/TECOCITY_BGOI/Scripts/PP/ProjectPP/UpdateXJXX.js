$(document).ready(function () {
    XJID = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "SelectGoodsXJID",
        type: "post",
        data: { XJID: XJID },
        dataType: "json",
        ansyc: false,
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
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="XXID' + rowCount + '">' + json[i].XXID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><input type="text"  style="width:70px" id="Amount' + rowCount + '" value="' + json[i].Amount + '" width="30px" onblur="OnBlurAmount(this);" /> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="supplierss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td  style="display:none"><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].SID + '</lable> </td>';
                    html += '<td ><input type="text" style="width:100px" value="' + json[i].NegotiatedPricingNoTax + '" onblur="OnBlurAmount(Amount' + rowCount + ');"  id="UnitPriceNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px" value="' + json[i].TotalNegotiationNoTax + '" id="TotalNoTax' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px" value="' + json[i].DrawingNum + '"  id="Price2' + rowCount + '"> </td>';
                    html += '<td ><input type="text" style="width:100px" value="' + json[i].GoodsUse + '"  id="TotalTax' + rowCount + '"> </td>';
                   
                    html += '</tr>'
                    $("#GXInfo").append(html);
                }
                document.getElementById("InquiryTitle").value = json[0].InquiryTitle;
                document.getElementById("InquiryDate").value = json[0].InquiryDate;
                document.getElementById("ContractCondition").value = json[0].ContractCondition;
                document.getElementById("InquiryExplain").value = json[0].InquiryExplain;
                document.getElementById("OrderContacts").value = json[0].OrderContacts;
            }
        }
    });

    $("#btnSubmit").click(function () {

        var str = document.getElementById("GXInfo").innerHTML;


        //请购单id
        var XJID = location.search.split('&')[0].split('=')[1];
        var ContractCondition = $("#ContractCondition").val();
        //请购说明
        var InquiryExplain = $("#InquiryExplain").val();
        //请购人
        var OrderContacts = $("#OrderContacts").val();
        //询价标题
        var InquiryTitle = $("#InquiryTitle").val();
        if (InquiryTitle == "") {
            alert("询价标题不可为空");
            return;
        }

        var InquiryDate = $("#InquiryDate").val();
        if (InquiryDate == "") {
            alert("询价日期不可为空");
            return;
        }

        var str = document.getElementById("GXInfo").innerHTML;

        if (str == "") {
            alert("请选择商品");
            return;
        }
        //var Suppliers = $("#Suppliers").val();
        //if (Suppliers == "") {
        //    alert("供货商不可为空");
        //    return;
        //}
        var rownumber = "";
        var proname = "";
        var xxid = "";
        var spec = "";
        var units = "";
        var amount = "";
        var supplier = "";
        var total = "";
    
        var totalNegotiationNoTax = "";
        var drawingNum = "";
        var goodsuse = "";
        var tbody = document.getElementById("GXInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
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
            var Supplier = document.getElementById("Supplier" + i).innerHTML;
            if (Supplier == "") {
                var Supplier = "无";
            }
            //谈判定价
            var Total = document.getElementById("UnitPriceNoTax" + i).value;
            if (Total == "") {
                alert("定价不可为空");
                return;
            }
            //谈判总价
            var TotalNegotiationNoTax = document.getElementById("TotalNoTax" + i).value;
            if (TotalNegotiationNoTax == "") {
                alert("总价不可为空");
                return;
            }
      
 



            var DrawingNum = document.getElementById("Price2" + i).value;
            if (DrawingNum == "") {
                var DrawingNum = "无";
            }
            var XXID = document.getElementById("XXID" + i).innerHTML;
            var GoodsUse = document.getElementById("TotalTax" + i).value;
            if (GoodsUse == "") {
                var GoodsUse = "无";
            }
            rownumber += RowNumber;
            proname += ProName;
            xxid += XXID;
            spec += Spec;
            units += Units;
            amount += Amount;
            supplier += Supplier;
            total += Total;
            totalNegotiationNoTax += TotalNegotiationNoTax;
      
            drawingNum += DrawingNum;
            goodsuse += GoodsUse;
            if (i < tbody.rows.length - 1) {
                rownumber += ",";
                proname += ",";
                xxid += ",";
                spec += ",";
                units += ",";
                amount += ",";
                supplier += ",";
                total += ",";
         
                totalNegotiationNoTax += ",";
                drawingNum += ",";
                goodsuse += ",";
            }
            else {
                rownumber += " ";
                proname += " ";
                xxid += "";
                spec += " ";
                units += "";
                amount += " ";
                supplier += " ";
                total += " ";
               
                totalNegotiationNoTax += "";
                drawingNum += " ";
                goodsuse += "";
            }
        }
            isConfirm = confirm("确定要修改吗")
            if (isConfirm == false) {
                return false;
            }
            else {
                $.ajax({
                    url: "UpdateXJ",
                    type: "Post",
                    data: {
                        XJID: XJID, rownumber: rownumber,  InquiryDate: InquiryDate, ContractCondition: ContractCondition, InquiryExplain: InquiryExplain, OrderContacts: OrderContacts, proname: proname,
                        xxid: xxid, spec: spec, units: units, amount: amount, supplier: supplier, total: total,   InquiryTitle: InquiryTitle, totalNegotiationNoTax: totalNegotiationNoTax, drawingNum: drawingNum
                        , goodsuse: goodsuse
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
});


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
 

 

 
 

function AddNewBasic() {
    ShowIframe1("选择货品信息", "../PPManage/ChangeBasic?XJ", 800, 500);
}

function addBasicDetail(PID) { //增加货品信息行

   

    $.ajax({
        url: "GetBasicDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                $("#myTable GXInfo").html("");
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="GXInfo' + rowCount + '"  onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="XXID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                   
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Units' + rowCount + '">' + json[i].Units + '</lable> </td>';
                    html += '<td ><input type="text" style="width:100px" value="0"   onblur="OnBlurAmount(this);" id="Amount' + rowCount + '"> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="supplierss' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td  style="display:none"><lable class="labOrderContent' + rowCount + ' " id="Supplier' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
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

function DelRow() {
    var tbodyID = "GXInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "ProName", "XXID", "Spec", "Units", "Amount","supplierss", "Supplier", "UnitPriceNoTax", "TotalNoTax", "Price2", "TotalTax"];

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

    $("#GXInfo tr").removeAttr("class");

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