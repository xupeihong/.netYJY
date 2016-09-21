
$(document).ready(function () {
    var msg = $("#msg").val();

    if (msg == "修改成功") {
        alert(msg);
        setTimeout('parent.ClosePop()', 100);
    }
    if (msg == "修改失败") {
        alert(msg);
    }

    if (location.search != "") {
        BGID = location.search.split('&')[0].split('=')[1];
        DID = location.search.split('&')[1].split('=')[1];

        $("#BGID").attr("value", BGID);
    }
  

    var date = new Date();
    var yy = date.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = date.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = date.getDate();
    if (dd < 10) dd = '0' + dd;
    $("#uploadtime").val(yy + "-" + MM + "-" + dd);
    jq();


    $("#Cancel").click(function () {
        window.parent.ClosePop();
    })

    $("#btnSave").click(function () {
        
        var BGID = $("#BGID").val();
        var RWID = $("#RWID").val();
        var uploadtime = $("#uploadtime").val();
        var Remarks = $("#Remarks").val();
        var CreatePerson = $("#CreatePerson").val();
     


        if (uploadtime == "" || uploadtime == null) {
            alert("上传日期不能为空");
            return;
        }
        var tbody = document.getElementById("DetailInfos");
        var m = $("#bbb").val();
        for (var i = m; i < tbody.rows.length; i++) {
            var mainContents = document.getElementById("RowNumbers" + i).innerHTML;
            var type = document.getElementById("Type" + i).value;
            var file = $("#FileName" + i).val();
            
            //alert(file);
            if (file == "") {
                alert("请选择上传文件！");
                return;
            }
        }
        $("#DID").val(DID);
        var bbb = $("#bbb").val();
        document.forms[0].submit();
    });

    $("#addbg").click(function () {
        rowCount = document.getElementById("DetailInfos").rows.length;
        var CountRows = parseInt(rowCount) + 1;
        var a = '报告类型:';
        var c = '检验报告:';
        var n='0';
        $.ajax({
            url: "GetBGType",
            type: "post",
            single: true,
            dataType: "json",
            success: function (data) {
                var json = eval(data.datas);
                var json = eval(data.datas);
                if (json.length > 0) {
                    var html = "";
                    html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
                    html += '<td id="RowNumbers' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labPID" id="PID">' + a + '</lable> </td>';
                    html += '<td ><select id="Type' + rowCount + '" name="Type' + rowCount + '"></select></td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + c + '</lable> </td>';
                    html += '<td ><input type="file" name="file1" id="FileName' + rowCount + '" /></td>';
                    html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> <lable class="labDID' + rowCount + ' " id="DID' + rowCount + '" style="display:none">' + n + '</lable></td>';
                    html += '</tr>' 
                    $("#DetailInfos").append(html);
                }
                for (var i = 0; i < json.length; i++) {
                    jQuery("#Type" + rowCount).append("<option value=" + json[i].Text + ">" + json[i].Text + "</option>");
                }
                
               $("#q").val(1);
            }

        })
    })
})


var BGID = "";
var DID = "";
function jq() { //增加产品信息行
    if (location.search != "") {
        BGID = location.search.split('&')[0].split('=')[1];
        DID = location.search.split('&')[1].split('=')[1];

        $("#BGID").attr("value", BGID);
    }
    var RWID = $("#RWID").val();
    var a = '报告类型:';
    var b = '检验报告:';
    $.ajax({
        url: "getFileInfo",
        type: "post",
        single: true,
        data: { BGID: BGID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            var json = eval(data.datas);
            var MS="";
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfos").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfos' + rowCount + '" onclick="selRows(this)">'
                    html += '<td id="RowNumbers' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + a + '</lable> </td>';
                    html += '<td ><lable class="labType' + rowCount + ' " id="Types' + rowCount + '">' + json[i].Type + '</lable> </td>';
                    jQuery("#Type" + rowCount).append("<option value=" + json[i].Type + ">" + json[i].Type + "</option>");
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + b + '</lable> </td>';
                    html += '<td ><lable class="labFileName' + rowCount + ' " id="FileNames' + rowCount + '">' + json[i].FileName + '</lable></td>';
                    html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '" style="display:none">' + json[i].DID + '</lable></td>';
                    html += '</tr>'
                    $("#DetailInfos").append(html);
                }
                
            }
            b = document.getElementById("DetailInfos").rows.length;
            $("#bbb").val(b);
            $("#q").val(0);
        }
           
    })
    $.ajax({
        url: "GetReportInfos",
        type: "post",
        single: true,
        data: { DID: DID },
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
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable class="labOrderNum' + rowCount + ' " id="OrderNum' + rowCount + '">' + json[i].OrderNum + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remarks' + rowCount + '">' + json[i].Remark + '</lable> <lable class="labDID' + rowCount + ' " id="DIDs' + rowCount + '" style="display:none">' + json[i].DID + '</lable></td>';
                   
                    html += '</tr>'
                    $("#DetailInfo").append(html);
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


function deleteTr(curRow) {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        newRowID = curRow.id;
        $("#DetailInfos tr").removeAttr("class");
        $("#" + newRowID).attr("class", "RowClass");

        var a = "#" + newRowID
        ss =Number(a.substring(12, 13));
     
        //字符串截取DetailInfo，要剩下的   int
        //和did的id组装成需要的did的id
        var DID = document.getElementById("DID" + ss).innerHTML;

        if (DID == "0")
        {
            var tbodyID = "DetailInfos";
            var rowIndex = -1;
            var typeNames = ["PID", "Type", "SpecsModels", "FileNames", "DetailInfos", "RowNumbers"];
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
                        document.getElementById("RowNumbers" + i).innerHTML = parseInt(i) + 1;
                        // $("#RowNumber" + i).html(parseInt(i) + 1);
                    }
                }
                if (document.getElementById(tbodyID).rows.length > 0) {
                    if (rowIndex == document.getElementById(tbodyID).rows.length)
                        selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
                    else
                        selRow(document.getElementById(tbodyID + rowIndex), '');;
                }
            }
            $("#DetailInfos tr").removeAttr("class");
            b = document.getElementById("DetailInfos").rows.length;
            $("#bbb").val(b);
            $("#q").val(1);
        }
        else
        {
            var tbodyID = "DetailInfos";
            var rowIndex = -1;
            var typeNames = ["PID", "Type", "SpecsModels", "FileNames", "DetailInfos", "RowNumbers"];
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
                        document.getElementById("RowNumbers" + i).innerHTML = parseInt(i) + 1;
                        // $("#RowNumber" + i).html(parseInt(i) + 1);
                    }
                }
                if (document.getElementById(tbodyID).rows.length > 0) {
                    if (rowIndex == document.getElementById(tbodyID).rows.length)
                        selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
                    else
                        selRow(document.getElementById(tbodyID + rowIndex), '');;
                }
            }
            $("#DetailInfos tr").removeAttr("class");

            b = document.getElementById("DetailInfos").rows.length;
            $("#bbb").val(b);
            $("#q").val(1);

            $.ajax({
                type: "POST",
                url: "SCBG",
                data: { DID: DID },
                success: function (data) {
                    alert(data.Msg);

                },
                dataType: 'json'
            });
        }
        return true;
    }
}

function submitInfo() {
    var options = {
        url: "SaveUpdateFileInfoIn",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            //alert(data.success)
            if (data.success) {
                // window.parent.frames["iframeRight"].reload();
                alert("aaaa");
                setTimeout('parent.ClosePop()', 100);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#from1").ajaxSubmit(options);
    return false;
}


function returnConfirm() {
    return false;
}