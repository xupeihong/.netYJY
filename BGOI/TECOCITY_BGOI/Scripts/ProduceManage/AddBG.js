$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg == "保存成功") {
        alert(msg);
        setTimeout('parent.ClosePop()', 100);
    }
    if (msg == "保存失败") {
        alert(msg);
    }

    var date = new Date();
    var yy = date.getYear();
    if (yy < 1900) yy = yy + 1900;
    var MM = date.getMonth() + 1;
    if (MM < 10) MM = '0' + MM;
    var dd = date.getDate();
    if (dd < 10) dd = '0' + dd;
    $("#uploadtime").val(yy+"-"+MM+"-"+dd);

    jq();

    function check(obj) {
        $('.checkall input').each(function () {
            if (this != obj)
                $(this).attr("checked", false);
            else {
                if ($(this).prop("checked"))
                    $(this).attr("checked", true);
                else
                    $(this).attr("checked", false);
            }
        });
    }


    $("#addbg").click(function () {
        rowCount = document.getElementById("DetailInfos").rows.length;
        var CountRows = parseInt(rowCount) + 1;
        var a = '报告类型:';
        var b = '检验报告:';
        $.ajax({
            url: "GetBGType",
            type: "post",
            single: true,
            //data: { RWID: RWID },
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
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + b + '</lable> </td>';
                    html += '<td ><input type="file" name="file1" id="FileName' + rowCount + '" /></td>';
                    html += '<td ><a id="DetailInfos' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                    //html += '<td id="RowNumbers' + rowCount + '"style="display:none">' + CountRows + '</td>';
                    html += '</tr>'
                    $("#DetailInfos").append(html);
                }
                for (var i = 0; i < json.length; i++) {
                    jQuery("#Type" + rowCount).append("<option value=" + json[i].Text + ">" + json[i].Text + "</option>");
                }
            }
        })
    });

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

      

        var MainContent = "";
        var DID = "";
        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
                var ch = document.getElementById("a" + i);
                if(ch.checked){
                var mainContent = document.getElementById("RowNumber" + i).innerHTML;
                var dID = document.getElementById("DID" + i).innerHTML;


                MainContent += mainContent;
                DID += dID;

                if (i < tbody.rows.length - 1) {
                    MainContent += ",";
                    DID += "";
                }
                else {
                    MainContent += "";
                    DID += "";
                }
            }

        }
       
        if (DID == "")
        {
            alert("请选择产品！");
            return;
        }
           

            var tbody = document.getElementById("DetailInfos");
            for (var i = 0; i < tbody.rows.length; i++) {

                var mainContents = document.getElementById("RowNumbers" + i).innerHTML;
                var type = document.getElementById("Type" + i).value;
                var file = $("#FileName" + i).val();
              
                if(file=="")
                {
                    alert("请选择上传文件！");
                    return;
                }
              
            }

       
        $("#DID").val(DID);
       
        
        document.forms[0].submit();
    });




})



function jq() { //增加产品信息行
    //   var RWID = "";
    if (location.search != "") {
        RWID = location.search.split('&')[0].split('=')[1];
        $("#RWID").val(RWID);
    }
    var RWID = $("#RWID").val();
    $.ajax({
        url: "GetReportInfoselect",
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
                    html += '<td><input  type="radio"  name="chen"  id="a' + rowCount + '"/></td>'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable class="labOrderNum' + rowCount + ' " id="OrderNum' + rowCount + '">' + json[i].OrderNum + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remarks' + rowCount + '">' + json[i].Remark + '</lable><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '" style="display:none">' + json[i].DID + '</lable>  </td>';
                
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
function deleteTr(date) {

    var TYpe;
    var FileName;

    newRowID = date.id;
    var a = newRowID.substring(11, 12);
    var tbody = document.getElementById("DetailInfos");
    for (var i = 0; i < tbody.rows.length; i++) {
        TYpe = document.getElementById("Type" + a).innerText;
        FileName = document.getElementById("FileName" + a).innerHTML;
    }
    var one = confirm("确认删除'"+TYpe+"'类型的'"+FileName+"'报告吗？");
    if (one == false)
        return;
    else {
        var tbodyID = "DetailInfos";
        var rowIndex = -1;
        var typeNames = ["RowNumbers","PID", "Type", "SpecsModels", "FileName", "DetailInfos"];
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
    }
}

function submitInfo() {
    var options = {
        url: "SaveFileInfoIn",
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