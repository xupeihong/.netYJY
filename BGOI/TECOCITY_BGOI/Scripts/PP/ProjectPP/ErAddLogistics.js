
$(document).ready(function () {
    $("#btnSubmit").click(function () {
        var ID = $("#ID").val();
        //授权公司
        var SQCompany = $("#SQCompany").val();
        if (SQCompany == "") {
            alert("授权公司不可为空");
            return;
        }

        //提货公司
        var THCompany = $("#THCompany").val();
        if (THCompany == "") {
            alert("提货公司不可为空");
            return;
        }
        var rownumber = "";
        var proname = "";
        var spec = "";
        var amount = "";
        var tbody = document.getElementById("GXInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var RowNumber = document.getElementById("RowNumber" + i).innerHTML;
            //商品名称
            var ProName = document.getElementById("ProName" + i).innerHTML;
            //商品型号
            var Spec = document.getElementById("Spec" + i).innerHTML;
            //数量
            var Amount = document.getElementById("Amount" + i).value;
            if (Amount == "") {
                alert("数量不可为空");
                return;
            }
            rownumber += RowNumber;
            proname += ProName;
            spec += Spec;

            amount += Amount;

            if (i < tbody.rows.length - 1) {
                rownumber += ",";
                proname += ",";
                spec += ",";
                amount += ",";

            }
            else {
                rownumber += " ";
                proname += " ";
                spec += " ";
                amount += " ";
            }
        }
        //收货地址
        var SHaddress = $("#SHaddress").val();
        if (SHaddress == "") {
            alert("收获地址不可为空");
            return;
        }
        //收获联系人
        var SHContacts = $("#SHContacts").val();
        if (SHContacts == "") {
            alert("收获联系人不可为空");
            return;
        }
        //收货电话
        var SHTel = $("#SHTel").val();
        if (SHTel == "") {
            alert("收货人电话不可为空");
            return;
        }
        //发货人
        var FHConsignor = $("#FHConsignor").val();
        if (FHConsignor == "") {
            alert("发货人不可为空");
            return;
        }
        //发货人电话
        var FHTel = $("#FHTel").val();
        if (FHTel == "") {
            alert("发货人电话不可为空");
            return;
        }
        //发货人传真
        var FHFax = $("#FHFax").val();
        if (FHFax == "") {
            alert("发货人传真不可为空");
            return;
        }
        //物流公司联系人
        var LogisticsS = $("#LogisticsS").val();
        if (LogisticsS == "") {
            alert("物流公司联系人不可为空");
            return;
        }
        //物流公司联系人电话
        var LogisticsSTel = $("#LogisticsSTel").val();
        if (LogisticsSTel == "") {
            alert("物流公司联系人电话不可为空");
            return;
        }

        //物流公司联系人传真
        var LogisticsSFax = $("#LogisticsSFax").val();
        if (LogisticsSFax == "") {
            alert("物流公司联系人传真不可为空");
            return;
        }
        isConfirm = confirm("确定要添加吗")
        if (isConfirm == false) {
            return false;
        }
        else {

            $.ajax({
                url: "InsertWL",
                type: "Post",
                data: {
                    id: ID, rownumber: rownumber, proname: proname, spec: spec, amount: amount, sqcompany: SQCompany, thcompany: THCompany, shaddress: SHaddress, shcontacts: SHContacts, shtel: SHTel, fhconsignor: FHConsignor, fhtel: FHTel, fhfax: FHFax, logisticss: LogisticsS, logisticsstel: LogisticsSTel, logisticssfax: LogisticsSFax,
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
});


function AddNewBasic() {
    window.parent.OpenDialog("选择货品信息", "../PPManage/ChangeBasic", 800, 500);
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
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("GXInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="GXInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><input type="text" style="width:100px"   id="Amount' + rowCount + '"> </td>';
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

function DelRow() {
    var tbodyID = "GXInfo";
    var rowIndex = -1;
    var typeNames = ["RowNumber", "ProName", "Spec", "Amount"];

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