$(document).ready(function () {
    if (location.search != "") {
        LLID = location.search.split('&')[0].split('=')[1];
    }

    var HouseID = $("#HouseID").val();
    document.getElementById("HouseIDs").value = HouseID;
    $("#LLID").attr("value", LLID);
    LoadMaterialForm();
    $("#btnSaveOrder").click(function () {
        SaveUpdateMaterialForm();
    });


    function AddMaterial() {
        ShowIframe1("物品选择", "../ProduceManage/AddMaterial", 500, 350);
    }

    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });
});

function SaveUpdateMaterialForm() {
    var RWID = $("#RWID").val();
    var HouseID = $("#HouseIDs").val();
    if (RWID == null||RWID=="")
    {
        var LLID = $("#LLID").val();
        var ID = $("#ID").val();
        var MaterialDepartment = $("#MaterialDepartment").val();
        var CreateTime = $("#CreateTime").val();
        var OrderContent = $("#OrderContent").val();
        var SpecsModels = $("#SpecsModels").val();
        var MaterialTime = $("#MaterialTime").val();
        var Amount = document.getElementById("Amount" + 0).value;
        var RWIDDID = $("#RWIDDID").val();
        if (Amount == "" || Amount == null) {
            alert("数量不能为空！");
            return;
        }
        
        var MainContent = "";
        var DID = "";
        var OrderContents = "";
        var SpecsModelss = "";
        var Specifications = "";
        var Manufacturer = "";
        var OrderUnit = "";
        var OrderNum = "";
        var Technology = "";
        var Remark = "";

        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var mainContent = document.getElementById("RowNumber" + i).innerHTML;
            var dID = document.getElementById("DID" + i).innerHTML;
            var orderContents = document.getElementById("OrderContents" + i).value;
            var specsModelss = document.getElementById("SpecsModelss" + i).value;
            //var specifications = document.getElementById("Specifications" + i).value;
            var manufacturer = document.getElementById("Manufacturer" + i).value;
            if (manufacturer == "" || manufacturer == null) {
                alert("单台数量不能为空！");
                return;
            }
            var orderUnit = document.getElementById("OrderUnit" + i).value;
            var orderNum = document.getElementById("OrderNum" + i).value;
            if (orderNum == "" || orderNum == null) {
                alert("领出数量不能为空！");
                return;
            }
            var technology = document.getElementById("Technology" + i).value;
            var remark = document.getElementById("Remark" + i).value;


            MainContent += mainContent;
            DID += dID;
            OrderContents += orderContents;
            SpecsModelss += specsModelss;
            //Specifications += specifications;
            Manufacturer += manufacturer;
            OrderUnit += orderUnit;
            OrderNum += orderNum;
            Technology += technology;
            Remark += remark;

            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                DID += ",";
                OrderContents += ",";
                SpecsModelss += ",";
                //Specifications += ",";
                Manufacturer += ",";
                OrderUnit += ",";
                OrderNum += ",";
                Technology += ",";
                Remark += ",";
            }
            else {
                MainContent += "";
                DID += "";
                OrderContents += "";
                SpecsModelss += "";
                //Specifications += "";
                Manufacturer += "";
                OrderUnit += "";
                OrderNum += "";
                Technology += "";
                Remark += "";
            }
        }
   
    $.ajax({
        url: "SaveUpdateMaterialTask",
        type: "Post",
        data: {
            LLID: LLID, ID: ID, MaterialDepartment: MaterialDepartment,
            CreateTime: CreateTime, OrderContent: OrderContent, SpecsModels: SpecsModels, MaterialTime: MaterialTime,RWIDDID:RWIDDID,Amount:Amount,
            MainContent: MainContent, DID: DID, OrderContents: OrderContents, SpecsModelss: SpecsModelss,
            //Specifications: Specifications,
            Manufacturer: Manufacturer, OrderUnit: OrderUnit, OrderNum: OrderNum, Technology: Technology, Remark: Remark,
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                alert("修改领料单成功！");
                window.parent.frames["iframeRight"].Search();
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                alert("修改领料单失败-" + data.msg);
            }
        }
    });
}
    else {

    var LLID = $("#LLID").val();
    var RWID = $("#RWID").val();
    var ID = $("#ID").val();
    var MaterialDepartment = $("#MaterialDepartment").val();
    var CreateTime = $("#CreateTime").val();
    var OrderContent = $("#OrderContent").val();
    var SpecsModels = $("#SpecsModels").val();
    var MaterialTime = $("#MaterialTime").val();
    var Amount = document.getElementById("Amount" + 0).value;
    var RWIDDID = $("#RWIDDID").val();
    var HouseID = $("#HouseID").val();
    if (Amount == "" || Amount == null)
    {
        alert("数量不能为空！");
        return;
    }
   

    $.ajax({
        url: "GetTaskNum",
        type: "post",
        single: true,
        data: { LLID: LLID,RWIDDID:RWIDDID,RWID:RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (Number(Amount) <= (Number(json[i].OrderNum)+Number(json[i].Amount))) {
                        getsave(LLID,RWID,ID,MaterialDepartment,CreateTime,OrderContent,SpecsModels,MaterialTime,Amount);
                        return true;
                    }
                    else {
                        alert("生成领料单数量不能大于任务单数量,剩余未领料数量为'"+json[i].OrderNum+"'");
                        return;
                    }
                }
            }
        }
    })
    }
}


function  getsave(LLID,RWID,ID,MaterialDepartment,CreateTime,OrderContent,SpecsModels,MaterialTime,Amount){
    //详细表
    var HouseID = $("#HouseIDs").val();
    var MainContent = "";
    var PID = "";
    var OrderContents = "";
    var SpecsModelss = "";
    var Specifications = "";
    var Manufacturer = "";
    var OrderUnit = "";
    var OrderNum = "";
    var Technology = "";
    var Remark = "";
    var IdentitySharing = "";

    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var mainContent = document.getElementById("RowNumber" + i).innerHTML;
        var pID = document.getElementById("PID" + i).innerHTML;
        var orderContents = document.getElementById("OrderContents" + i).value;
        var specsModelss = document.getElementById("SpecsModelss" + i).value;
        //var specifications = document.getElementById("Specifications" + i).value;
        var manufacturer = document.getElementById("Manufacturer" + i).value;
        if (manufacturer == "" || manufacturer == null)
        {
            alert("单台数量不能为空！");
            return;
        }
        var orderUnit = document.getElementById("OrderUnit" + i).value;
        var orderNum = document.getElementById("OrderNum" + i).value;
        if (orderNum == "" || orderNum == null)
        {
            alert("领出数量不能为空！");
            return;
        }
        var technology = document.getElementById("Technology" + i).value;
        var remark = document.getElementById("Remark" + i).value;
        var identitySharing = document.getElementById("IdentitySharing" + i).innerHTML;


        MainContent += mainContent;
        PID += pID;
        OrderContents += orderContents;
        SpecsModelss += specsModelss;
        //Specifications += specifications;
        Manufacturer += manufacturer;
        OrderUnit += orderUnit;
        OrderNum += orderNum;
        Technology += technology;
        Remark += remark;
        IdentitySharing += identitySharing;

        if (i < tbody.rows.length - 1) {
            MainContent += ",";
            PID += ",";
            OrderContents += ",";
            SpecsModelss += ",";
            //Specifications += ",";
            Manufacturer += ",";
            OrderUnit += ",";
            OrderNum += ",";
            Technology += ",";
            Remark += ",";
            IdentitySharing += ",";
        }
        else {
            MainContent += "";
            PID += "";
            OrderContents += "";
            SpecsModelss += "";
            //Specifications += "";
            Manufacturer += "";
            OrderUnit += "";
            OrderNum += "";
            Technology += "";
            Remark += "";
            IdentitySharing += "";
        }
    }
    $.ajax({
        url: "SaveUpdateMaterialForm",
        type: "Post",
        data: {
        LLID: LLID, RWID: RWID, ID: ID, MaterialDepartment: MaterialDepartment,
        CreateTime: CreateTime, OrderContent: OrderContent, SpecsModels: SpecsModels, MaterialTime: MaterialTime,Amount:Amount,
        MainContent: MainContent, PID: PID, OrderContents: OrderContents, SpecsModelss: SpecsModelss,
        //Specifications: Specifications,
        Manufacturer: Manufacturer, OrderUnit: OrderUnit, OrderNum: OrderNum, Technology: Technology, Remark: Remark, IdentitySharing: IdentitySharing,
        HouseID: HouseID
    },
        async: false,
        success: function (data) {
            if (data.success == true) {
                alert("修改领料单成功！");
                window.parent.frames["iframeRight"].Search();
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                alert(data.Msg);
            }
        }
    });
}

function AddMaterial() {
    ShowIframe1("物品选择", "../ProduceManage/AddMaterial", 500, 350);
}

function AddMaterials(PID) {
    var IdentitySharing = "";
    var PID = PID;
    var tbody = document.getElementById("DetailInfo");
    if (tbody.rows.length == 0) {
        $.ajax({
            url: "GetMT",
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
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labDID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><input type="text" style="width:80px;" id="OrderContents' + rowCount + '" value="' + json[i].ProName + '"  > </td>';
                        html += '<td ><input type="text" style="width:80px;" id="SpecsModelss' + rowCount + '" value="' + json[i].Spec + '"  > </td>';//一次修改，图号和规格列合并
                        //html += '<td ><input type="text" style="width:80px;" id="Specifications' + rowCount + '" value="' + json[i].Specifications + '"  > </td>';
                        html += '<td ><input type="text" style="width:80px;" id="Manufacturer' + rowCount + '" onblur="OnBlur(this)"> </td>';
                        html += '<td ><input type="text" style="width:60px;" id="OrderUnit' + rowCount + '" value="' + json[i].Units + '"  > </td>';
                        html += '<td ><input type="text" style="width:60px;" id="OrderNum' + rowCount + '"   > </td>';
                        html += '<td ><input type="text" style="width:60px;" id="Technology' + rowCount + '"   > </td>';
                        html += '<td ><input type="text" style="width:60px;" id="Remark' + rowCount + '" value="' + json[i].Remark + '"  ><lable class="labRemark' + rowCount + ' " id="DID' + rowCount + '"style="display:none">' + json[i].DID + '</lable> </td>';
                        html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> <lable class="labRemark' + rowCount + ' " id="IdentitySharing' + rowCount + '"style="display:none">' + IdentitySharing + '</lable></td>';
                        html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                }
            }
        })
    }
    else {
        for (var i = 0; i < tbody.rows.length; i++) {
            var pID = document.getElementById("PID" + i).innerText;
            if (PID.replace(/[ ]/g, "") == pID.replace(/[ ]/g, "")) {
                return;
            }
        }
        $.ajax({
            url: "GetMT",
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
                        html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                        html += '<td ><lable class="labDID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><input type="text" style="width:80px;" id="OrderContents' + rowCount + '" value="' + json[i].ProName + '"  > </td>';
                        html += '<td ><input type="text" style="width:80px;" id="SpecsModelss' + rowCount + '" value="' + json[i].Spec + '"  > </td>';//一次修改，图号和规格列合并
                        //html += '<td ><input type="text" style="width:80px;" id="Specifications' + rowCount + '" value="' + json[i].Specifications + '"  > </td>';
                        html += '<td ><input type="text" style="width:80px;" id="Manufacturer' + rowCount + '"onblur="OnBlur(this)" > </td>';
                        html += '<td ><input type="text" style="width:60px;" id="OrderUnit' + rowCount + '" value="' + json[i].Units + '"  > </td>';
                        html += '<td ><input type="text" style="width:60px;" id="OrderNum' + rowCount + '"   > </td>';
                        html += '<td ><input type="text" style="width:60px;" id="Technology' + rowCount + '"   > </td>';
                        html += '<td ><input type="text" style="width:60px;" id="Remark' + rowCount + '" value="' + json[i].Remark + '"  ><lable class="labRemark' + rowCount + ' " id="DID' + rowCount + '"style="display:none">' + json[i].DID + '</lable> </td>';
                        html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a><lable class="labRemark' + rowCount + ' " id="IdentitySharing' + rowCount + '"style="display:none">' + IdentitySharing + '</lable> </td>';
                        html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                        html += '</tr>'
                        $("#DetailInfo").append(html);
                    }
                }
            }
        })
    }
}

function LoadMaterialForm() {
   
    $.ajax({
        url: "GetMaterialFormDetail",
        type: "post",
        data: { LLID: LLID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><input type="text" style="width:80px;" id="OrderContents' + rowCount + '" value="' + json[i].OrderContent + '"  > </td>';
                    html += '<td ><input type="text" style="width:80px;" id="SpecsModelss' + rowCount + '" value="' + json[i].SpecsModels + '"  > </td>';//一次修改，图号和规格列合并
                    //html += '<td ><input type="text" style="width:80px;" id="Specifications' + rowCount + '" value="' + json[i].Specifications + '"  > </td>';
                    html += '<td ><input type="text" style="width:80px;" id="Manufacturer' + rowCount + '" value="' + json[i].Manufacturer + '" onblur="OnBlur(this)" > </td>';
                    html += '<td ><input type="text" style="width:60px;" id="OrderUnit' + rowCount + '" value="' + json[i].OrderUnit + '"  > </td>';
                    html += '<td ><input type="text" style="width:60px;" id="OrderNum' + rowCount + '" value="' + json[i].OrderNum + '"  > </td>';
                    html += '<td ><input type="text" style="width:60px;" id="Technology' + rowCount + '" value="' + json[i].Technology + '"  > </td>';
                    html += '<td ><input type="text" style="width:60px;" id="Remark' + rowCount + '" value="' + json[i].Remark + '"  ><lable class="labRemark' + rowCount + ' " id="DID' + rowCount + '"style="display:none">' + json[i].DID + '</lable> </td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a><lable class="labRemark' + rowCount + ' " id="IdentitySharing' + rowCount + '"style="display:none">' + json[i].IdentitySharing + '</lable> </td>';
                    html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                    CountRows = CountRows + 1;
                }
            }
        }
    })

    $.ajax({
        url: "GetMaterialFormTaskdetail",
        type: "post",
        data: { LLID: LLID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfos").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PIDss' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PIDss' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PIDss' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PIDss' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><input type="text" style="width:100px;" id="Amount' + rowCount + '" value="' + json[i].Amount + '"onblur="OnBlurAmount(this);"> </td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PIDss' + rowCount + '">' + json[i].Remark + '</lable><lable class="labRemark' + rowCount + ' " id="DIDs' + rowCount + '"style="display:none">' + json[i].DID + '</lable>  </td>';
                    html += '</tr>'
                    $("#DetailInfos").append(html);
                    CountRows = CountRows + 1;
                }
            }
        }
    })
}

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

function selRows(curRow) {
    newRowID = curRow.id;
    $("#DetailInfos tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

function OnBlurAmount(rowcount) //求领出数量
{
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var newCount = rowcount.id;
        var Count = $("#" + newCount).val();

        var strRow = i;
        var strUnitPrice = document.getElementById("Manufacturer" + i).value;
        var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
        $("#OrderNum" + strRow).val(strTotal);
    }
}
function OnBlur(rowcount) //求领出数量
{
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var newCount = rowcount.id;
        var n = Number(newCount.substring(12, 13));
        var Count = document.getElementById("Manufacturer" + n).value;

        var strRow = n;

        var tbody = document.getElementById("DetailInfos")
        for (var m = 0; m < tbody.rows.length; m++) {
           
            
                var strUnitPrice = document.getElementById("Amount" + m).value;
           
        }
        var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
        $("#OrderNum" + strRow).val(strTotal);
    }
}


function deleteTr(curRow) {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        newRowID = curRow.id;
        $("#DetailInfo tr").removeAttr("class");
        $("#" + newRowID).attr("class", "RowClass");

        var a = "#" + newRowID
        ss = Number(a.substring(11, 12));
        //alert(ss);
        //字符串截取DetailInfo，要剩下的   int
        //和did的id组装成需要的did的id
        var DID = document.getElementById("DID" + ss).innerHTML;


        var tbodyID = "DetailInfo";
        var rowIndex = -1;
        var typeNames = ["RowNumber", "PID", "OrderContents", "SpecsModelss", "Manufacturer", "OrderUnit", "OrderNum", "Technology", "Remark", "DID", "DetailInfo", "FinishCount", "IdentitySharing"];
        if (newRowID != "")
            rowIndex = newRowID.replace(tbodyID, '');
        if (rowIndex != -1) {
            document.getElementById(tbodyID).deleteRow(rowIndex);
            //var a = $("#" + tbodyID + " tr").length;
            if (rowIndex < $("#" + tbodyID + " tr").length) {
                for (var i = rowIndex; i < $("#" + tbodyID + " tr").length; i++) {
                    // var b = parseInt(i);
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
                        tr1.html(html);//.toLocaleLowerCase());//replace('TD','td'));
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
        $("#DetailInfo tr").removeAttr("class");
    }
}

function getAllNum() {
    var HouseID = $("#HouseIDs").val();
    $("#KCSL").show();

    var tbody = document.getElementById("DetailInfo");
    for (var m = 0; m < tbody.rows.length; m++) {
        var pID = document.getElementById("PID" + m).innerHTML;
        $("#FinishCount" + m).show();
        $.ajax({
            async: false,
            single: true,
            url: "getKCnum",
            type: "post",
            data: { PID: pID, HouseID: HouseID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        if (json[i].FinishCount == "" || json[i].FinishCount == null) {
                            json[i].FinishCount = 0;
                        }
                        document.getElementById("FinishCount" + m).innerHTML = json[i].FinishCount;
                    }
                }
                else {
                    document.getElementById("FinishCount" + m).innerHTML = 0;
                }
            }
        })
    }
}