$(document).ready(function () {
    $(':checkbox[id=a]').each(function () {
        $(this).click(function () {
            if ($(this).attr('checked')) {
                $(':checkbox[id=a]').removeAttr('checked');
                $(this).attr('checked', 'checked');
            }
        });
    });
    jq();
    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })
    $("#btnSave").click(function () {
        var OrderNums = "";
        var DID = "";
        var tbody = document.getElementById("DetailInfo")
        for (var i = 0; i < tbody.rows.length; i++) {
            var ch = document.getElementById("a" + i);
            if (ch.checked) {
                var a = document.getElementById("RowNumbers" + i).innerHTML;
                var orderNums = document.getElementById("OrderNums" + i).value;
                if (orderNums == "" || orderNums == null) {
                    alert("数量不能为空!");
                    break;
                }
                var dID = document.getElementById("DIDs" + i).innerHTML;

                if (orderNums > 0) {
                    OrderNums += orderNums;
                    DID += dID;

                    getunm(OrderNums, DID);
                }
                else {
                    alert("输入数量不能小于或等于0")
                    return;
                }
            }
        }
        if (DID == "") {
            alert("请选择产品！");
            return;
        }
    });

})

function getsave(OrderNums, DID) {
    var LLID = $("#LLID").val();
    var RWID = $("#RWID").val();
    var ID = $("#ID").val();
    var MaterialDepartment = $("#MaterialDepartment").val();
    var CreateTime = $("#CreateTime").val();
    var OrderContents = $("#OrderContent").val();
    var SpecsModelss = $("#SpecsModels").val();
    var MaterialTime = $("#MaterialTime").val();
    var CreateUser = $("#CreateUser").val();
    var HouseID = $("#HouseIDs").val();


    if (HouseID == "" || HouseID == null)
    {
        alert("库房不能为空");
        return;
    }
    if (ID == "" || ID == null) {
        alert("编号不能为空");
        return;
    }
    if (CreateTime == "" || CreateTime == null) {
        alert("开单日期不能为空");
        return;
    }
    if (OrderContents == "" || OrderContents == null) {
        alert("领料人不能为空");
        return;
    }
    if (MaterialTime == "" || MaterialTime == null) {
        alert("领料日期不能为空");
        return;
    }

    var MainContent = "";
    var PID = "";
    var OrderContent = "";
    var SpecsModels = "";
    var Specifications = "";
    var Manufacturer = "";
    var OrderUnit = "";
    var OrderNum = "";
    var Technology = "";
    var Remark = "";
    var IdentitySharing = "";

    var tbody = document.getElementById("DetailInfos");
    if (tbody.rows.length == 0)
    {
        alert("请添加零件！");
        return;
    }
    for (var i = 0; i < tbody.rows.length; i++) {
        var mainContent = document.getElementById("RowNumber" + i).innerHTML;
        var pID = document.getElementById("PID" + i).innerHTML;
        var orderContent = document.getElementById("OrderContent" + i).innerHTML;
        var specsModels = document.getElementById("SpecsModels" + i).innerHTML;

        var manufacturer = document.getElementById("Manufacturer" + i).value;
        if (manufacturer == "" || manufacturer == null) {
            alert("单台数量不能为空");
            return;
        }
        var orderUnit = document.getElementById("OrderUnit" + i).value;
        var orderNum = document.getElementById("OrderNum" + i).value;
        if (orderNum == "" || orderNum == null) {
            alert("领出数量不能为空");
            return;
        }
        var technology = document.getElementById("Technology" + i).value;
        var remark = document.getElementById("Remark" + i).value;
        var identitySharing = document.getElementById("IdentitySharing" + i).innerHTML;

        MainContent += mainContent;
        PID += pID;
        OrderContent += orderContent;
        SpecsModels += specsModels;

        Manufacturer += manufacturer;
        OrderUnit += orderUnit;
        OrderNum += orderNum;
        Technology += technology;
        Remark += remark;
        IdentitySharing += identitySharing;


        if (i < tbody.rows.length - 1) {
            MainContent += "&";
            PID += "&";
            OrderContent += "&";
            SpecsModels += "&";

            Manufacturer += "&";
            OrderUnit += "&";
            OrderNum += "&";
            Technology += "&";
            Remark += "&";
            IdentitySharing += "&";

        }
        else {
            MainContent += "";
            PID += "";
            OrderContent += "";
            SpecsModels += "";

            Manufacturer += "";
            OrderUnit += "";
            OrderNum += "";
            Technology += "";
            Remark += "";
            IdentitySharing += "";
        }
    }
    $.ajax({
        url: "SaveMaterialFDetailIn",
        type: "Post",
        data: {
            RWID: RWID, LLID: LLID, ID: ID, PID: PID, MaterialDepartment: MaterialDepartment, CreateTime: CreateTime,
            OrderContents: OrderContents, SpecsModelss: SpecsModelss, MaterialTime: MaterialTime, CreateUser: CreateUser,
            MainContent: MainContent, OrderContent: OrderContent, SpecsModels: SpecsModels,
            Manufacturer: Manufacturer, OrderUnit: OrderUnit, OrderNum: OrderNum,
            Technology: Technology, Remark: Remark, OrderNums: OrderNums, DID: DID, IdentitySharing: IdentitySharing,
            HouseID: HouseID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                alert("添加成功");
                window.parent.frames["iframeRight"].Search();
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                alert(data.Msg);
            }
        }
    });
}

function getunm(OrderNums, DID) {
    if (location.search != "") {
        RWID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "getTnum",
        type: "post",
        single: true,
        data: { DID: DID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (parseInt(OrderNums) <= parseInt(json[i].OrderNum)) {
                        getsave(OrderNums, DID);
                        return OrderNums;
                    }
                    else {
                        alert("生成领料单数量不能大于任务单数量");
                        return false;
                    }
                }
            }
        }
    })
}

function jq() { //增加产品信息行
    if (location.search != "") {
        RWID = location.search.split('&')[0].split('=')[1];
    }
    $("#RWID").attr("value", RWID);

    $.ajax({
        url: "GetMaterialForm",
        type: "post",
        single: true,
        data: { RWID: RWID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td><input  type="radio"  name="chen"  id="a' + rowCount + '"onclick="getKC()";/></td>'
                    html += '<td id="RowNumbers' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PIDs' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContents' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModelss' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnits' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><input type="text" id="OrderNums' + rowCount + '" style="width:190px;" value="' + json[i].OrderNum + '"onblur="OnBlur(this)"/></td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remarks' + rowCount + '">' + json[i].Remark + '</lable><lable class="labOrderUnit' + rowCount + ' " id="DIDs' + rowCount + '" style="display:none">' + json[i].DID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }

        }
    })
}


function getKC() {
    var tbody = document.getElementById("DetailInfo")
    for (var i = 0; i < tbody.rows.length; i++) {
        var ch = document.getElementById("a" + i);
        if (ch.checked) {
            var PID = document.getElementById("PIDs" + i).innerHTML;
            var rowcount = document.getElementById("OrderNums" + i).value;
        }
    }
    for (var i = document.getElementById("DetailInfos").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("DetailInfos").removeChild(document.getElementById("DetailInfos").childNodes[i]);
    }
    $.ajax({
        url: "GetMaterial",
        type: "post",
        data: { PID: PID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfos").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    //html += '<td ><input type="text" style="width:80px;" id="PID' + rowCount + '" value="' + json[i].PID + '"  > </td>';
                    //html += '<td ><input type="text" style="width:80px;" id="OrderContent' + rowCount + '" value="' + json[i].ProName + '"  > </td>';
                    //html += '<td ><input type="text" style="width:100px;" id="SpecsModels' + rowCount + '" value="' + json[i].Spec + '"  > </td>';
                    html += '<td ><input type="text" id="Manufacturer' + rowCount + '" style="width:60px;" value="' + json[i].Number + '"onblur="OnBlurAmount(this)"/></td>';

                    html += '<td ><input type="text" style="width:60px;" id="OrderUnit' + rowCount + '" value="' + json[i].Units + '"  > </td>';
                    html += '<td ><input type="text" id="OrderNum' + rowCount + '" style="width:60px;" /></td>';
                    html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:60px;" /></td>';
                    html += '<td ><input type="text" id="Remark' + rowCount + '" style="width:60px;" /></td>';
                    html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a><lable class="labPID' + rowCount + ' " id="IdentitySharing' + rowCount + '" style="display:none">' + json[i].IdentitySharing + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfos").append(html);
                }
                var tbody = document.getElementById("DetailInfos");
                for (var i = 0; i < tbody.rows.length; i++) {
                    var newCount = rowcount;
                    var Count = newCount;

                    var strRow = i;
                    var strUnitPrice = document.getElementById("Manufacturer" + i).value;
                    var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
                    $("#OrderNum" + strRow).val(strTotal);
                }
            }
        }
    })


}

function OnBlurAmount(rowcount) //求领出数量
{
    var tbody = document.getElementById("DetailInfos");
    for (var i = 0; i < tbody.rows.length; i++) {
        var newCount = rowcount.id;
        var n = Number(newCount.substring(12, 13));
        var Count = document.getElementById("Manufacturer" + n).value;

        var strRow = n;

        var tbody = document.getElementById("DetailInfo")
        for (var m = 0; m < tbody.rows.length; m++) {
            var ch = document.getElementById("a" + m);
            if (ch.checked) {
                var strUnitPrice = document.getElementById("OrderNums" + m).value;
            }
        }
        var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
        $("#OrderNum" + strRow).val(strTotal);
    }
}

function OnBlur(rowcount) //求领出数量
{
    var tbody = document.getElementById("DetailInfo");
    for (var i = 0; i < tbody.rows.length; i++) {
        var ch = document.getElementById("a" + i);
        if (ch.checked) {
            var strUnitPrice = document.getElementById("OrderNums" + i).value;
            var tbodys = document.getElementById("DetailInfos")
            for (var m = 0; m < tbodys.rows.length; m++) {
                var newCount = rowcount.id;
                var n = Number(newCount.substring(9, 10));
                var Count = document.getElementById("Manufacturer" + m).value;
                var strRow = m;

                var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
                $("#OrderNum" + strRow).val(strTotal);
            }
        }
    }
}

function AddMaterial() {
    ShowIframe1("物品选择", "../ProduceManage/AddMaterial", 600, 350);
}
function AddMaterials(PID) {
    var IdentitySharing = "";
    var tbody = document.getElementById("DetailInfos");
    if (tbody.rows.length == 0) {
        var strPID = $("#PID").val();
        $("#PID").val(strPID + "," + PID);
        
            $.ajax({
                url: "GetMT",
                type: "post",
                data: { PID: PID },
                dataType: "json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            rowCount = document.getElementById("DetailInfos").rows.length;
                            var CountRows = parseInt(rowCount) + 1;
                            var html = "";
                            html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
                            html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                            html += '<td ><lable class="labPID' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                            html += '<td ><lable class="labPID' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                            //html += '<td ><input type="text" style="width:80px;" id="PID' + rowCount + '" value="' + json[i].PID + '"  > </td>';
                            //html += '<td ><input type="text" style="width:80px;" id="OrderContent' + rowCount + '" value="' + json[i].ProName + '"  > </td>';

                            //html += '<td ><input type="text" style="width:100px;" id="SpecsModels' + rowCount + '" value="' + json[i].Spec + '"  > </td>';
                            html += '<td ><input type="text" id="Manufacturer' + rowCount + '" style="width:60px;"onblur="OnBlurAmount(this)"/></td>';
                            html += '<td ><input type="text" style="width:60px;" id="OrderUnit' + rowCount + '" value="' + json[i].Units + '"  > </td>';
                            html += '<td ><input type="text" id="OrderNum' + rowCount + '" style="width:60px;" /></td>';
                            html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:60px;" /></td>';
                            html += '<td ><input type="text" id="Remark' + rowCount + '" style="width:60px;" /></td>';
                            html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a><lable class="labPID' + rowCount + ' " id="IdentitySharing' + rowCount + '" style="display:none">' + IdentitySharing + '</lable> </td>';
                            html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                            html += '</tr>'
                            $("#DetailInfos").append(html);
                        }
                    }
                }
            })
        }
   
    else {
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
        var strPID = $("#PID").val();
        $("#PID").val(strPID + "," + PID);
        $.ajax({
            url: "GetMT",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfos").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        //html += '<td ><input type="text" style="width:80px;" id="PID' + rowCount + '" value="' + json[i].PID + '"  > </td>';
                        //html += '<td ><input type="text" style="width:80px;" id="OrderContent' + rowCount + '" value="' + json[i].ProName + '"  > </td>';

                        //html += '<td ><input type="text" style="width:100px;" id="SpecsModels' + rowCount + '" value="' + json[i].Spec + '"  > </td>';
                        html += '<td ><input type="text" id="Manufacturer' + rowCount + '" style="width:60px;"onblur="OnBlurAmount(this)"/></td>';
                        html += '<td ><input type="text" style="width:60px;" id="OrderUnit' + rowCount + '" value="' + json[i].Units + '"  > </td>';
                        html += '<td ><input type="text" id="OrderNum' + rowCount + '" style="width:60px;" /></td>';
                        html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:60px;" /></td>';
                        html += '<td ><input type="text" id="Remark' + rowCount + '" style="width:60px;" /></td>';
                        html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a><lable class="labPID' + rowCount + ' " id="IdentitySharing' + rowCount + '" style="display:none">' + IdentitySharing + '</lable> </td>';
                        html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                        html += '</tr>'
                        $("#DetailInfos").append(html);
                    }
                }
            }
        })
    }
    
}

    function selRow(curRow) {
        newRowID = curRow.id;
        var m = Number(newRowID.substring(10, 11))
        var ch = document.getElementById("a" + m);
        ch.checked = true;
        $("#DetailInfo tr").removeAttr("class");
        $("#" + newRowID).attr("class", "RowClass");

        var tbody = document.getElementById("DetailInfo")
        for (var i = 0; i < tbody.rows.length; i++) {
            var ch = document.getElementById("a" + i);
            if (ch.checked) {
                var PID = document.getElementById("PIDs" + i).innerHTML;
                var rowcount = document.getElementById("OrderNums" + i).value;
            }
        }
        for (var i = document.getElementById("DetailInfos").childNodes.length - 1; i >= 0 ; i--) {
            document.getElementById("DetailInfos").removeChild(document.getElementById("DetailInfos").childNodes[i]);
        }
        var IdentitySharing = "";
        $.ajax({
            url: "GetMaterial",
            type: "post",
            data: { PID: PID },
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                if (json.length > 0) {
                    for (var i = 0; i < json.length; i++) {
                        rowCount = document.getElementById("DetailInfos").rows.length;
                        var CountRows = parseInt(rowCount) + 1;
                        var html = "";
                        html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
                        html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                        //html += '<td ><input type="text" style="width:80px;" id="PID' + rowCount + '" value="' + json[i].PID + '"  > </td>';
                        //html += '<td ><input type="text" style="width:80px;" id="OrderContent' + rowCount + '" value="' + json[i].ProName + '"  > </td>';
                        //html += '<td ><input type="text" style="width:100px;" id="SpecsModels' + rowCount + '" value="' + json[i].Spec + '"  > </td>';
                        html += '<td ><input type="text" id="Manufacturer' + rowCount + '" style="width:60px;" value="' + json[i].Number + '"onblur="OnBlurAmount(this)"/></td>';

                        html += '<td ><input type="text" style="width:60px;" id="OrderUnit' + rowCount + '" value="' + json[i].Units + '"  > </td>';
                        html += '<td ><input type="text" id="OrderNum' + rowCount + '" style="width:60px;" /></td>';
                        html += '<td ><input type="text" id="Technology' + rowCount + '" style="width:60px;" /></td>';
                        html += '<td ><input type="text" id="Remark' + rowCount + '" style="width:60px;" /></td>';
                        html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a><lable class="labPID' + rowCount + ' " id="IdentitySharing' + rowCount + '" style="display:none">' + json[i].IdentitySharing + '</lable> </td>';
                        html += '<td id="FinishCount' + rowCount + '"style="display:none"><lable class="labOrderUnits' + rowCount + ' " id="FinishCount' + rowCount + '"style="display:none"></lable></td>';
                        html += '</tr>'
                        $("#DetailInfos").append(html);
                    }
                    var tbody = document.getElementById("DetailInfos");
                    for (var i = 0; i < tbody.rows.length; i++) {
                        var newCount = rowcount;
                        var Count = newCount;

                        var strRow = i;
                        var strUnitPrice = document.getElementById("Manufacturer" + i).value;
                        var strTotal = parseFloat(Count) * parseFloat(strUnitPrice);
                        $("#OrderNum" + strRow).val(strTotal);
                    }
                }
            }
        })


    }


    function selRows(curRow) {
        newRowID = curRow.id;
        $("#DetailInfos tr").removeAttr("class");
        $("#" + newRowID).attr("class", "RowClass");
    }
    function deleteTr(curRow) {
        newRowID = curRow.id;
        var one = confirm("确认删除");
        if (one == false)
            return;
        else {
            var tbodyID = "DetailInfos";
            var rowIndex = -1;
            var typeNames = ["RowNumber", "PID", "OrderContent", "SpecsModels", "Manufacturer", "OrderUnit", "OrderNum", "Technology", "Remark", "DetailInfos"];
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
                        var c = parseInt(i) + 1;
                        document.getElementById("RowNumber" + i).innerHTML = parseInt(i) + 1;
                        // $("#RowNumber" + i).html(parseInt(i) + 1);
                    }
                }
                if (document.getElementById(tbodyID).rows.length > 0) {
                    if (rowIndex == document.getElementById(tbodyID).rows.length)
                        selRows(document.getElementById(tbodyID + (rowIndex - 1)), '');
                    else
                        selRows(document.getElementById(tbodyID + rowIndex), '');
                }
            }
            $("#DetailInfos tr").removeAttr("class");
        }
    }


    function getAllNum() {
        var HouseID = $("#HouseIDs").val();
        $("#KCSL").show();

        var tbody = document.getElementById("DetailInfos");
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
                            if (json[i].FinishCount == "" || json[i].FinishCount == null)
                            {
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


