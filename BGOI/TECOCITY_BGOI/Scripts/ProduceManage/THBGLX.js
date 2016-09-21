$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();

    $("#addContent").click(function () {
        rowCount = document.getElementById("DetailInfo").rows.length;
        var CountRows = parseInt(rowCount) + 1;
                var html = "";
                html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRows(this)">'
                html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumbers' + rowCount + '">' + CountRows + '</lable> </td>';
                html += '<td ><input type="text'+rowCount+'" id="Text' + rowCount + '" style="width:25%;" /></td>';
                html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                html += '</tr>'
                $("#DetailInfo").append(html);
    });

    $("#aa").click(function () {
        window.parent.ClosePop();
    })

    $("#charge").click(function () {
        if (location.search != "") {
            SID = location.search.split('&')[0].split('=')[1];
        }
        var MainContent="";
        var Text = "";
        var tbody = document.getElementById("DetailInfo");
        var m = $("#bbb").val();
        if (m == tbody.rows.length) {
            alert("请点击添加类型！");
            return;
        }
        for (var i = m; i < tbody.rows.length; i++) {
            var mainContent = document.getElementById("RowNumbers" + i).innerHTML;
            var text = document.getElementById("Text" + i).value;
            //判断是否有该类型
            getText(SID,text,mainContent,i);
           
        }
    })
})
   

function SaveBGLX(SID, text, mainContent,i)
{
    var MainContent = "";
    var Text = "";
    var tbody = document.getElementById("DetailInfo");
    if (text == "") {
        alert("请输入文档的类型！");
        return false;
    }
    else {
        var msg = "确定保存文档类型吗?";
        if (confirm(msg) == true) {
            Text += text;
            MainContent += mainContent;

            if (i < tbody.rows.length - 1) {
                MainContent += ",";
                Text += ",";
            }
            else {
                MainContent += "";
                Text += "";
            }
            $.ajax({
                url: "SaveBGLX",
                type: "Post",
                data: {
                    MainContent: MainContent, Text: Text, SID: SID
                },
                async: false,
                success: function (data) {
                    if (data.success == true) {
                        alert("保存成功");
                        window.parent.frames["iframeRight"].reload();
                        setTimeout('parent.ClosePop()', 100);
                    }
                    else {
                        alert(data.msg);
                    }
                }
            }); return true;
        } 
   else{
    return false;
    }
}
}
function getText(SID, text, mainContent,i)
{
    if (location.search != "") {
        SID = location.search.split('&')[0].split('=')[1];
    }
    $.ajax({
        url: "getText",
        type: "post",
        single: true,
        data: { SID: SID,text:text },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    if (text == json[i].Text) {
                        alert("已有此类型，请重新输入！");
                        return false;
                    }
                    else {
                        SaveBGLX(SID, text, mainContent);
                        return text;
                    }
                }
            }
            else {
                SaveBGLX(SID, text, mainContent,i);
            }
        }
    })
}
function jq() {
    if (location.search != "") {
        SID = location.search.split('&')[0].split('=')[1];
    }
   
    $.ajax({
        url: "THBGLXs",
        type: "post",
        single: true,
        data: {SID:SID},
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;//1
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRows(this)">'
                    html += '<td ><lable class="labRowNumber' + rowCount + ' " id="RowNumbers' + rowCount + '">' + CountRows + '</lable> </td>';
                    html += '<td ><lable class="labProcess' + rowCount + ' " id="Text' + rowCount + '">' + json[i].Text + '</lable> </td>';
                    html += '<td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
            b = document.getElementById("DetailInfo").rows.length;
            $("#bbb").val(b);
        }
    })
}

function selRows(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
//删除选中行，有就撤销，没有就移除
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
        var Text = document.getElementById("Text" + ss).innerHTML;

        if (Text != "") {
            var tbodyID = "DetailInfo";
            var rowIndex = -1;
            var typeNames = ["RowNumbers", "Text", "DetailInfo"];
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
                        selRows(document.getElementById(tbodyID + (rowIndex - 1)), '');
                    else
                        selRows(document.getElementById(tbodyID + rowIndex), '');;
                }
            }
            $("#DetailInfo tr").removeAttr("class");

            b = document.getElementById("DetailInfo").rows.length;
            $("#bbb").val(b);
            $.ajax({
                type: "POST",
                url: "SCBGLX",
                data: { Text: Text },
                success: function (data) {
                    alert(data.Msg);

                },
                dataType: 'json'
            });
            window.parent.frames["iframeRight"].reload();
            //window.parent.ClosePop();
        }
      
        else {
            var tbodyID = "DetailInfo";
            var rowIndex = -1;
            var typeNames = ["RowNumbers", "Text", "DetailInfo"];
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
                        selRows(document.getElementById(tbodyID + (rowIndex - 1)), '');
                    else
                        selRows(document.getElementById(tbodyID + rowIndex), '');;
                }
            }
            $("#DetailInfo tr").removeAttr("class");
               b = document.getElementById("DetailInfo").rows.length;
               $("#bbb").val(b);
               window.parent.frames["iframeRight"].reload();
               //window.parent.ClosePop();
        }
        return true;
    
    }
}