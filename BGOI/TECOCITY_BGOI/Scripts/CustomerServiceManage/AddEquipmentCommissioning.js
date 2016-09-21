var PID;
var rowCount;
var newRowID;

$(document).ready(function () {
    tick();
    $("#ScrapTime").val("");
    $("#btnSave").click(function () {
        var TRID = $("#TRID").val();
        var UserName = $("#UserName").val();
        var Address = $("#Address").val();
        var ContactPerson = $("#ContactPerson").val();
        var Tel = $("#Tel").val();
        var ConstructionUnit = $("#ConstructionUnit").val();
        var CUnitPer = $("#CUnitPer").val();
        var CUnitTel = $("#CUnitTel").val();
        var EquManType = $("input[name='EquManType']:checked").val();
        var UnitName = $("#UnitName").val();
        var UnitTel = $("#UnitTel").val();
        var UnitPer = $("#UnitPer").val();
        //多选
        var str = document.getElementsByName("ProductForm");
        var objarray = str.length;
        var ProductForm = "";
        for (i = 0; i < objarray; i++) {
            if (str[i].checked == true) {
                //alert(str[i].value);
                ProductForm += str[i].value + ",";
            }
        }
        // var ProductForm = $("input[name='ProductForm']:checked").val();
        var Gas = $("input[name='Gas']:checked").val();
        var UserType = $("input[name='UserType']:checked").val();
        var DebPerson = $("#DebPerson").val();

        var DebTime = $("#DebTime").val();

        var PowerNumber = $("#PowerNumber").val();
        var PowerTime = $("#PowerTime").val();
        var PowerInitialP = $("#PowerInitialP").val();
        var StepDownNumber = $("#StepDownNumber").val();
        var StepDownTime = $("#StepDownTime").val();
        var StepDownInitialP = $("#StepDownInitialP").val();
        var InletPreP1 = $("#InletPreP1").val();
        var BleedingpreP1 = $("#BleedingpreP1").val();
        var PowerExportPreP2 = $("#PowerExportPreP2").val();
        var PowerOffPrePb = $("#PowerOffPrePb").val();
        var PowerCutOffPrePq = $("#PowerCutOffPrePq").val();
        var SDExportPreP2 = $("#SDExportPreP2").val();
        var SDPowerOffPrePb = $("#SDPowerOffPrePb").val();
        var SDCutOffPrePq = $("#SDCutOffPrePq").val();
        var FieldFailure = $("#FieldFailure").val();
        var CreateTime = $("#localtime").text();
        var CreateUser = $("#CreateUser").val();
        var Remark = $("#Remark").val();
        var type = 1;//添加

        var ProName = "";
        var PID = "";
        var SpecsModels = "";

        //产品信息
        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            //var proName = tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("ProName" + i).innerHTML;
            //var pID = tbody.getElementsByTagName("tr")[i].cells[1].innerText;//document.getElementById("PID" + i).innerHTML;
            //var spec = tbody.getElementsByTagName("tr")[i].cells[3].innerText;//document.getElementById("Spec" + i).innerHTML;

            var proName = document.getElementById("ProName" + i).value;// tbody.getElementsByTagName("tr")[i].cells[2].innerText;//document.getElementById("ProName" + i).innerHTML;
            var pID = document.getElementById("ProductID" + i).value;// tbody.getElementsByTagName("tr")[i].cells[1].innerText;//document.getElementById("PID" + i).innerHTML;
            var spec = document.getElementById("Spec" + i).value;//tbody.getElementsByTagName("tr")[i].cells[3].innerText;//document.getElementById("Spec" + i).innerHTML;


            ProName += proName;
            PID += pID;
            SpecsModels += spec;
            if (i < tbody.rows.length - 1) {
                ProName += ",";
                PID += ",";
                SpecsModels += ",";
            }
            else {
                ProName += "";
                PID += "";
                SpecsModels += "";
            }
        }

        $.ajax({
            url: "SaveEquipmentCommissioning",
            type: "Post",
            data: {
                TRID: TRID, UserName: UserName, Address: Address, ContactPerson: ContactPerson, Tel: Tel, ConstructionUnit: ConstructionUnit,
                CUnitPer: CUnitPer, CUnitTel: CUnitTel, EquManType: EquManType, UnitName: UnitName, UnitTel: UnitTel,
                UnitPer: UnitPer, ProductForm: ProductForm, ProductForm: ProductForm, Gas: Gas, UserType: UserType,
                DebPerson: DebPerson, DebTime: DebTime, PowerNumber: PowerNumber, PowerTime: PowerTime, PowerInitialP: PowerInitialP,
                StepDownNumber: StepDownNumber, StepDownTime: StepDownTime, StepDownInitialP: StepDownInitialP, InletPreP1: InletPreP1,
                BleedingpreP1: BleedingpreP1, PowerExportPreP2: PowerExportPreP2, PowerOffPrePb: PowerOffPrePb, PowerCutOffPrePq: PowerCutOffPrePq,
                SDExportPreP2: SDExportPreP2, SDPowerOffPrePb: SDPowerOffPrePb, SDCutOffPrePq: SDCutOffPrePq, FieldFailure: FieldFailure,
                CreateTime: CreateTime, CreateUser: CreateUser, Remark: Remark, type: type,
                ProName: ProName, PID: PID, SpecsModels: SpecsModels
            },
            async: false,
            success: function (data) {
                if (data.success == true) {
                    alert("保存成功");
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
function DelRow() { //删除货品信息行
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
}
function showLocale(objD) {
    var str, colorhead, colorfoot;
    var yy = objD.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = objD.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = objD.getDate();
    if (dd < 10) dd = '0' + dd;
    var hh = objD.getHours();
    if (hh < 10) hh = '0' + hh;
    var mm = objD.getMinutes();
    if (mm < 10) mm = '0' + mm;
    var ss = objD.getSeconds();
    if (ss < 10) ss = '0' + ss;
    var ww = objD.getDay();
    if (ww == 0) colorhead = "<font color=\"#333333\">";
    if (ww > 0 && ww < 6) colorhead = "<font color=\"#333333\">";
    if (ww == 6) colorhead = "<font color=\"#333333\">";
    if (ww == 0) ww = "星期日";
    if (ww == 1) ww = "星期一";
    if (ww == 2) ww = "星期二";
    if (ww == 3) ww = "星期三";
    if (ww == 4) ww = "星期四";
    if (ww == 5) ww = "星期五";
    if (ww == 6) ww = "星期六";
    colorfoot = "</font>"
    str = colorhead + yy + "年" + MM + "月" + dd + "日" + " " + hh + ":" + mm + ":" + ss + " " + ww + colorfoot;
    return (str);
}
function tick() {
    var today;
    today = new Date();
    document.getElementById("localtime").innerHTML = showLocale(today);
    window.setTimeout("tick()", 1000);
}
function GaiBian() {
    var DDL = $("#OrderContent").val();
    $.ajax({
        url: "GetProSpec",
        type: "post",
        data: { DDL: DDL },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#SpecsModels").val(json[i].Spec);
                    $("#PID").val(json[i].PID);
                }
            }
        }
    })
}
function CheckDetail() {
    // ShowIframe1("选择货品信息", "../CustomerService/ChangeProduct", 500, 400);
    ShowIframe1("选择货品信息", "../InventoryManage/ChangeBasic", 500, 500);
}
function addBasicDetail(PID) { //增加货品信息行
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
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
                    html = '<tr id ="DetailInfo' + rowCount + '" onclick="selRow(this)">';
                    //html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labProductID' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labProName' + rowCount + ' " id="ProName' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
                    html += '</tr>';
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
    //加载报修
    $.ajax({
        url: "GetPIDBaoDetail",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                $("#BX").show();
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfoi").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfoi' + rowCount + '" onclick="ChaKanBX(this)">'
                    html += '<td ><lable class="labBXID' + rowCount + ' " id="BXID' + rowCount + '">' + json[i].BXID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfoi").append(html);
                }
            }
        }
    })
}
function addKonghang() { //增加空行
    rowCount = document.getElementById("DetailInfo").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    var html = "";
    html = '<tr id ="DetailInfo' + rowCount + '" onclick="selRow(this)">';
    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
    html += '<td ><input type="text" id="ProductID' + rowCount + '" style="width:100px;"/></td>';
    html += '<td ><input type="text" id="ProName' + rowCount + '" style="width:100px;"/></td>';
    html += '<td ><input type="text" id="Spec' + rowCount + '" style="width:230px;"/></td>';
    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:bule;cursor:pointer;">删除</a> </td>';
    html += '</tr>';
    $("#DetailInfo").append(html);

    var html1 = "";
    html1 = '<tr  id ="DetailInfoi' + rowCount + '" onclick="ChaKanBX(this)">'
    html1 += '<td ><input type="text" id="BXID' + rowCount + '" style="width:50px;"/></td>';
    html1 += '</tr>'
    $("#DetailInfoi").append(html1);

}


function deleteTr(date) {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        var tbodyID = "DetailInfo";
        var newRowIDnew = date.id;
        var rowIndex = -1;
        var typeNames = ["RowNumber", "ProName", "Spec", "DetailInfo", "ProductID"];

        if (newRowIDnew != "")
            rowIndex = newRowIDnew.replace(tbodyID, '');
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
                    document.getElementById("RowNumber" + i).innerHTML = parseInt(i) + 1;
                    // $("#RowNumber" + i).html(parseInt(i) + 1);
                }
            }
            if (document.getElementById(tbodyID).rows.length > 0) {
                if (rowIndex == document.getElementById(tbodyID).rows.length)
                    selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
                else
                    selRow(document.getElementById(tbodyID + rowIndex), '');
            }
        }
        //GetAmount();
        $("#DetailInfo tr").removeAttr("class");
    }
}
function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");

}
//查看报修详细
function ChaKanBX(va) {
    var type = 1;//查看
    newRowID = va.id;
    var tbody = document.getElementById("DetailInfoi");
    var BXID = tbody.getElementsByTagName("tr")[newRowID].cells[0].innerText;
    var url = "PrintMaintenanceTaskList?Info='" + BXID + "'&type=" + type;
    window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
}

