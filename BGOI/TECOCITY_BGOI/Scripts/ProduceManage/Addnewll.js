var n;
var j;



$(document).ready(function () {
    var date = new Date();
    var yy = date.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = date.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = date.getDate();
    if (dd < 10) dd = '0' + dd;
    $("#CreateTime").val(yy + "-" + MM + "-" + dd);

    //document.getElementById('bor').style.display = 'block';


    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })


    $("#btnSave").click(function () {
        var LLID = $("#LLID").val();
        var ID = $("#ID").val();
        var MaterialDepartment = $("#MaterialDepartment").val();
        var CreateTime = $("#CreateTime").val();
        var OrderContents = $("#OrderContent").val();
        var SpecsModelss = $("#SpecsModels").val();
        var MaterialTime = $("#MaterialTime").val();
        var CreateUser = $("#CreateUser").val();

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
        var OrderNums = "";
        var Remarks = "";

        var tbody = document.getElementById("DetailInfo");
        if (tbody.rows.length > 0) {
            for (var i = 0; i < tbody.rows.length; i++) {
                var mainContent = document.getElementById("RowNumbers" + i).innerHTML;
                var pIDs = document.getElementById("PIDs" + i).innerHTML;
                var orderNums = document.getElementById("OrderNums" + i).innerHTML;
                if (orderNums == "" || orderNums == null) {
                    alert("数量不能为空！");
                    return;
                }
                var remarks = document.getElementById("Remarks" + i).value;
                var orderContent = document.getElementById("OrderContents" + i).innerHTML;
                var specsModels = document.getElementById("SpecsModelss" + i).innerHTML;
                var orderUnit = document.getElementById("OrderUnits" + i).innerHTML;

                getsave(mainContent, orderNums, remarks, pIDs, orderContent, specsModels, orderUnit, LLID, ID, MaterialDepartment, CreateTime, OrderContents, SpecsModelss, MaterialTime, CreateUser);
            }
            alert("添加成功");
            window.parent.frames["iframeRight"].Search();
            setTimeout('parent.ClosePop()', 100);
        }
        else {
            alert("请选择商品！");
            return;
        }
    })

})


function getsave(mainContent, orderNums, remarks, pIDs, orderContent, specsModels, orderUnit, LLID, ID, MaterialDepartment, CreateTime, OrderContents, SpecsModelss, MaterialTime, CreateUser) {
   

    $.ajax({
        url: "savesomematerial",
        type: "Post",
        data: {
            LLID: LLID, ID: ID, MaterialDepartment: MaterialDepartment, CreateTime: CreateTime, OrderContents: OrderContents, SpecsModelss: SpecsModelss,
            MaterialTime: MaterialTime, CreateUser: CreateUser,
            mainContent: mainContent, orderNums: orderNums, remarks: remarks, pIDs: pIDs, orderContent: orderContent, specsModels: specsModels, orderUnit: orderUnit
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                return true;
            }
            else {
                alert(data.msg);
                return;
            }
        }
    });
}



function gettask() {
    ShowIframe1("选择产品", "../COM_Approval/ChoseGoods", 900, 550);
}

var array = new Array();
var chengpinid = new Array();
var a = new Array();
function loadProduct(Name, Spc, Pid, Num, Units,UnitPrice, Price2) {
    $("#Name").val(Name);
    $("#Spc").val(Spc);
    $("#Pid").val(Pid);
    $("#Num").val(Num);
    $("#Units").val(Units);
    $("#UnitPrice").val(UnitPrice);
    $("#Price2").val(Price2);

    Name = Name.split('$');
    Spc = Spc.split('$');
    Pid = Pid.split('$');
    Num = Num.split('$');
    Units = Units.split('$');
    

    //var pidone = Pid[0];

    for (var i = document.getElementById("DetailInfo").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("DetailInfo").removeChild(document.getElementById("DetailInfo").childNodes[i]);
    }
  
    if (Name.length > 0) {
        for (var i = 0; i < Name.length; i++) {
            rowCount = document.getElementById("DetailInfo").rows.length;
            var CountRows = parseInt(rowCount) + 1;
            var html = "";
            html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">';
            html += '<td id="RowNumbers' + rowCount + '">' + CountRows + '</td>';
            html += '<td ><lable class="labPID' + rowCount + ' " id="PIDs' + rowCount + '">' + Pid[i] + '</lable> </td>';
            html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContents' + rowCount + '">' + Name[i] + '</lable> </td>';
            html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModelss' + rowCount + '">' + Spc[i] + '</lable> </td>';
            html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnits' + rowCount + '">' + Units[i] + '</lable> </td>';
            html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderNums' + rowCount + '">' + Num[i] + '</lable> </td>';
            //html += '<td ><input type="text" id="OrderNums' + rowCount + '" style="text-align:center;" value="' + Num[i] + '"/></td>';
            html += '<td ><input type="text" id="Remarks' + rowCount + '"style="width:160px;text-align:center"/></td>';
            html += '</tr>';
            $("#DetailInfo").append(html);
            array.push(Name[i] + Spc[i]);
            chengpinid.push(Pid[i]);
            a.push(Num[i]);
            GetLJ(Pid[i], Num[i]);
        }
    }

    function GetLJ(Pid, Num) {
        $.ajax({
            url: "GetMaterial",
            type: "post",
            data: { Pid: Pid },
            dataType: "Json",
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
                        html += '<td ><lable class="labPID' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Number + '</lable> </td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].Units + '</lable> </td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="OrderNum' + rowCount + '">' + json[i].Number*Num + '</lable> </td>';
                        //html += '<td ><lable class="labPID' + rowCount + ' " id="Technology' + rowCount + '">' +  + '</lable> </td>';
                        html += '<td ><lable class="labPID' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                        //html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                        html += '</tr>';
                        $("#DetailInfos").append(html);
                       
                    }
                }
                else {
                    return;
                }
                unique(array);
            }
        });
    }

}


var n = "";
    function unique(arr) {

        var result = new Array();
        for (var i = 0; i < arr.length; i++) {
            isRepeated = false;
            for (var j = i + 1; j < arr.length; j++) {
                if (arr[i] == arr[j]) {
                    isRepeated = true;
                    break;
                }
            }
            if (!isRepeated) {
                result.push(arr[i]);
            }
        }
        $("#supplier").empty();
        $("#bor").empty();
        for (var i = 0; i < result.length; i++) {
            $("#lingjian").css("display", "")
            var PiD = chengpinid[i];
            var Num = a[i];
            var n = PiD + "$" + Num;
         
            if (i == 0) {
                $("#supplier").append(" <input  id='supplier" + i + "'class='btnTw' type='button' name='" + n + "" + i + "' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
                $("#bor").append("  <div id='lingjian" + i + "'style='overflow-y: scroll; border: 1px solid #707070;    margin-left: 10px; height: 100px'> <table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>    <th style='width: 5%;' class='th'>序号</th><th style='width: 10%;' class='h'>零件编码</th><th style='width: 10%;' class='th'>零件名称</th><th style='width: 15%;' class='th'>图号或规格</th><th style='width: 10%;' class='th'>单台数量 </th><th style='width: 10%;' class='th'>单位</th><th style='width: 10%;' class='th'>领出数量</th> <th style='width: 10%;' class='th'>备注</th></tr> <tbody id='DetailInfoss" + i + "'></tbody></table></div>");
                var tbody = "DetailInfoss" + 0;

                var PID = chengpinid[0];
                var Num = a[0];
                $.ajax({
                    url: "GetMaterial",
                    type: "post",
                    async: false,
                    data: { PID: PID },
                    dataType: "Json",
                    success: function (data) {
                        var json = eval(data.datas);
                        if (json.length > 0) {
                            for (var i = 0; i < json.length; i++) {
                                rowCount = document.getElementById("" + tbody + "").rows.length;
                                var CountRows = parseInt(rowCount) + 1;
                                var html = "";
                                html += "<tr>";
                                html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                                html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                                html += '<td ><lable class="labPID' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                                html += '<td ><lable class="labPID' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                                html += '<td ><lable class="labPID' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Number + '</lable> </td>';
                                html += '<td ><lable class="labPID' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].Units + '</lable> </td>';
                                html += '<td ><lable class="labPID' + rowCount + ' " id="OrderNum' + rowCount + '">' + json[i].Number * Num + '</lable> </td>';
                                //html += '<td ><lable class="labPID' + rowCount + ' " id="Technology' + rowCount + '">' +  + '</lable> </td>';
                                html += '<td ><lable class="labPID' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                                //html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                                html += '</tr>'
                                $("#" + tbody + "").append(html);
                            }
                        }
                        else {
                            return;
                        }
                    }
                });
            }
            else {
                $("#supplier").append(" <input  id='supplier" + i + "'' class='btnTh' type='button'name='" + n + "" + i + "' onclick=Suppliers(this," + result.length + ") value='" + result[i] + "' />");
                $("#bor").append("  <div id='lingjian" + i + "' style='overflow-y: scroll; border: 1px solid #707070;   display:none; margin-left: 10px; height: 100px'><table   cellpadding='0' cellspacing='0' class='tabInfo'>  <tr align='center' valign='middle'>   <th style='width: 5%;' class='th'>序号</th><th style='width: 10%;' class='h'>零件编码</th><th style='width: 10%;' class='th'>零件名称</th><th style='width: 15%;' class='th'>图号或规格</th><th style='width: 10%;' class='th'>单台数量 </th><th style='width: 10%;' class='th'>单位</th><th style='width: 10%;' class='th'>领出数量</th> <th style='width: 10%;' class='th'>备注</th></tr> <tbody id='DetailInfoss" + i + "'></tbody></table> </div>");
            }
        }

    }
    function Suppliers(rowid, lenght) {
        var dd = rowid.id;
        var id = rowid.name;
        var PIDNUM = id.substring(0, 10);
        var i = id.substring(10, 11);
        var n = "name" + [i];
        var mn = PIDNUM.split("$");
        var PID = mn[0];
        var Num = mn[1];
       

        var tbody = "";
        for (var i = 0; i < lenght; i++) {
            if (dd == "supplier" + i) {
                $("#supplier" + i).attr("class", "btnTw");
                $("#lingjian" + i).css("display", "");
                tbody = "DetailInfoss" + i;
            }
            else {
                $("#supplier" + i).attr("class", "btnTh");
                $("#lingjian" + i).css("display", "none");
            }
        }
            $.ajax({
                url: "GetMaterial",
                type: "post",
                async: false,
                data: { PID: PID },
                dataType: "Json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            rowCount = document.getElementById("" + tbody + "").rows.length;
                            var CountRows = parseInt(rowCount) + 1;
                            var html = "";
                            html += "<tr>";
                            html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                            html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                            html += '<td ><lable class="labPID' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].ProName + '</lable> </td>';
                            html += '<td ><lable class="labPID' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                            html += '<td ><lable class="labPID' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Number + '</lable> </td>';
                            html += '<td ><lable class="labPID' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].Units + '</lable> </td>';
                            html += '<td ><lable class="labPID' + rowCount + ' " id="OrderNum' + rowCount + '">' + json[i].Number * Num + '</lable> </td>';
                            //html += '<td ><lable class="labPID' + rowCount + ' " id="Technology' + rowCount + '">' +  + '</lable> </td>';
                            html += '<td ><lable class="labPID' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                            //html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                            html += '</tr>'
                            $("#" + tbody + "").append(html);
                        }
                    }
                    else {
                        return;
                    }
                }
            });

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


