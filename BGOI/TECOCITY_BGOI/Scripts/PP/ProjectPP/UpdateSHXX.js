
var shuliang = "";
$(document).ready(function () {
    if (location.search.split('&')[0].split('=')[0] == "?SHID") {
        var SHID = location.search.split('&')[0].split('=')[1];
        addBasicDetail(SHID);
    }
});

function addBasicDetail(SHID) { //增加货品信息行
    var strPID = $("#SHID").val();
    $("#SHID").val(strPID + "," + SHID);
    $.ajax({
        url: "SelectSHXX",
        type: "post",
        data: { SHID: SHID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr>';
                    html += '<td ><input name="CP" type="checkbox" id="CPselect' + rowCount + '" /> </td>';
                    html += '<td ><lable class="labRowNumber " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="SHID">' + SHID + '</lable> </td>';
                    html += '<td style="display:none" ><lable class="labProductID " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '<td style="display:none" ><lable class="labProductID " id="Bak' + rowCount + '">' + json[i].Bak + '</lable> </td>';
                    html += '<td ><lable class="labProductID " id="ProName' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="MaterialNO' + rowCount + '">' + json[i].INID + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Units' + rowCount + '">' + json[i].Unit + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="Amount' + rowCount + '">' + json[i].Amount + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="DDAmount' + rowCount + '">' + json[i].DDAmount + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labSpecifications " id="DDAmounts' + rowCount + '">' + json[i].ActualAmount + '</lable> </td>';
            
                    html += '<td ><input type="text" id="ActualAmount' + rowCount + '" value="' + json[i].ActualAmount + '" style="width:150px;"/></td>';
                    html += '<td ><lable class="labSpecifications " id="Suppliers' + rowCount + '">' + json[i].COMNameC + '</lable> </td>';
                    html += '<td style="display:none"><lable class="labSpecifications " id="Supplier' + rowCount + '">' + json[i].Supplier + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="UnitPriceNoTax' + rowCount + '">' + json[i].UnitPriceNoTax + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications " id="TotalNoTax' + rowCount + '">' + json[i].TotalNoTax + '</lable> </td>';
                    html += '</tr>'
                    $("#GXInfo").append(html);
                    var Amount = document.getElementById("ActualAmount" + i).value;
                    shuliang += Amount;
                    if (i == json.length - 1) {
                        shuliang += "";
                    }
                    else {
                        shuliang += ",";
                    }
                }
                document.getElementById("ArrivalDate").value = json[0].ArrivalDate;
                document.getElementById("ArrivalDescription").value = json[0].ArrivalDescription;
                document.getElementById("ArrivalProcess").value = json[0].ArrivalProcess;
            }
        }


    })
}

function update() {

    var did = "";
    var bak = "";
    var actualamount = "";
    var str = document.getElementById("GXInfo").innerHTML;
    if (str == "") {
        alert("请选择商品");
    }
    else {



        var tbody = document.getElementById("GXInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var DID = document.getElementById("DID" + i).innerHTML;
            var Bak = document.getElementById("Bak" + i).innerHTML;
            //ben
            var DDAmount = document.getElementById("DDAmount" + i).innerHTML;
            //xie yin
            var DDAmounts = document.getElementById("ActualAmount" + i).innerHTML;
            //zong
            var Amounts = document.getElementById("Amount" + i).innerHTML;
            var chk = document.getElementById('CPselect' + i);
            //xie
            var ActualAmount = document.getElementById("ActualAmount" + i).value;
            if (chk.checked) {
                var zhAmount = parseInt(ActualAmount) - parseInt(DDAmounts);
                if (parseInt(DDAmount) + parseInt(zhAmount) >= parseInt(Amounts)) {
                    alert("输入数量大于总数量！！！");
                    return;
                }
            }
            else {
                var ActualAmount = document.getElementById("ActualAmount" + i).value;
            }
            if (ActualAmount == "") {
                alert("收货数量不可为空");
                return;
            }
            did += DID;
            bak += Bak;
            actualamount += ActualAmount;
            if (i < tbody.rows.length - 1) {
                did += ",";
                bak += ",";
                actualamount += ",";
            }
            else {
                did += "";
                bak += "";
                actualamount += "";
            }
        }
        var ArrivalDate = $("#ArrivalDate").val();
        if (ArrivalDate == "") {
            alert("收货日期不可以为空");
            return;
        }
        var ArrivalDescription = $("#ArrivalDescription").val();
        if (ArrivalDescription == "") {
            ArrivalDescription = "无";
        }
        var ArrivalProcess = $("#ArrivalProcess").val();
        var SHID = document.getElementById("SHID").innerHTML;
        isConfirm = confirm("确定要修改吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            $.ajax({
                url: "UpdateSH",
                type: "Post",
                data: {
                    arrivalDate: ArrivalDate, arrivalDescription: ArrivalDescription, arrivalProcess: ArrivalProcess, shid: SHID, shuliang: shuliang, did: did, bak: bak, actualamount: actualamount
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

    }

}
