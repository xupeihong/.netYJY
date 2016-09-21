var m_selRowCSS = '';
var newRowID = '';

function Table(jsonTable, oddStyle, evenStyle, selStyle, arrListID, arrListType, arrNewElement, arrListInfo, listNewElementsID, listCheck) {
    this.jsonData = jsonTable;
    this.oddRowCSS = oddStyle;
    this.evenRowCSS = evenStyle;
    this.selRowCSS = selStyle;
    this.speciallist = arrListID;
    this.specialType = arrListType;
    this.specialNew = arrNewElement;
    this.specialContent = arrListInfo;
    this.specialElementsID = listNewElementsID;

    if (typeof Table.__initialized == "undefined") {
        Table.prototype.LoadTableTBody = function (TbodyID) {

            m_selRowCSS = this.selRowCSS;

            for (var i = 0; i < this.jsonData.length; i++) {

                var newTR = null;
                var newTR = document.createElement('tr');
                newTR.id = TbodyID + i;
                if (i % 2 == 0)
                    newTR.className = this.evenRowCSS;
                else
                    newTR.className = this.oddRowCSS;
                newTR.onclick = function () { Table.prototype.SelRow(this, m_selRowCSS) }

                var newIDTD = document.createElement('td');
                newIDTD.innerText = i + 1;
                newTR.appendChild(newIDTD);
                var keySign = 0;
                for (var key in this.jsonData[i]) {
                    var newTD = document.createElement('td');

                    //                    var newText = document.createTextNode(this.jsonData[i][key]);
                    //                    newTD.appendChild(newText);
                    if (this.speciallist != null) {
                        var specialTrue = false;
                        for (var j = 0; j < this.speciallist.length; j++) {
                            if (this.speciallist[j] == keySign) {
                                var getElement = this.LoadSpecialList(this.specialType[j], this.specialNew[j], this.specialContent[j], this.jsonData[i][key]);
                                //                                getElement.id = getElement.name + i;
                                //                                getElement.name = getElement.id;
                                //                                alert(this.specialElementsID[j])
                                getElement.id = this.specialElementsID[j] + i;
                                getElement.name = getElement.id;
                                if (this.specialNew[j] == "input") {
                                    getElement.type = this.specialType[j];
                                }

                                newTD.appendChild(getElement);

                                if (getElement.type == "text" && listCheck[j] == "y") {
                                    //var aa = document.getElementById(newTR.id);
                                    //newElement.onClick = "Selid('" + listNewElementsID[i] + "','" + newElement.id + "','div" + newElement.id + "','ul" + newElement.id + "','LoadInfo')";
                                    getElement.setAttribute("onclick", "Selid('" + listNewElementsID[j] + "','" + getElement.id + "','div" + getElement.id + "','ul" + getElement.id + "','LoadInfo')");
                                    getElement.setAttribute("onkeyup", "keyup('" + listNewElementsID[j] + "','" + getElement.id + "','div" + getElement.id + "','ul" + getElement.id + "','LoadInfo')");
                                    //newElement.setAttribute("onBlur", "$('#div" + newElement.id + "').hide();")
                                    //newTD.innerHTML += "<div id='div" + getElement.id + "' class='selDiv'><ul  style='list-style: none; color:black;' id='ul" + getElement.id + "'></ul></div>";
                                    var newDiv = document.createElement('div');
                                    newDiv.id = 'div' + getElement.id;
                                    newDiv.className = 'selDiv';
                                    var newUl = document.createElement('ul');
                                    newUl.id = 'ul' + getElement.id;
                                    newUl.style = 'list-style: none; color:black;';
                                    newDiv.appendChild(newUl);
                                    newTD.appendChild(newDiv);

                                }




                                specialTrue = true;
                                break;
                            }
                        }
                        if (specialTrue == false)
                            newTD.innerText = this.jsonData[i][key];
                    }
                    else {
                        newTD.innerText = this.jsonData[i][key];
                    }
                    newTR.appendChild(newTD);
                    keySign++;
                }
                document.getElementById(TbodyID).appendChild(newTR);
                if (i == 0)
                    this.SelRow(newTR, m_selRowCSS);
            }
        }

        Table.prototype.SelRow = function (curRow, selRowStyle) {
            //            var OldID = this.getCookieByID("CurRowIDT");
            //            alert(selRowStyle)
            var oldSelRow = null;
            //            if (OldID != "")
            //                oldSelRow = document.getElementById(OldID);
            if (newRowID != "")
                oldSelRow = document.getElementById(newRowID);
            var curElement = document.getElementById(curRow.id);
            var className = selRowStyle;

            if (!hasClass(curElement, className)) {
                addClass(curElement, className);
            }
            if (oldSelRow != null && newRowID != curRow.id) {
                if (hasClass(oldSelRow, className))
                    removeClass(oldSelRow, className);
            }
            //            this.setCookieRowID(curRow.id);
            newRowID = curRow.id;
        }

        Table.prototype.setCookieRowID = function (rowID) {
            document.cookie = "CurRowIDT=" + rowID + ";path=/";
        }

        Table.prototype.getCookieByID = function (Name) {
            var start = document.cookie.indexOf(Name + "=");
            var end = 0;
            var oldSelRow = null;
            if (start >= 0) {
                start = start + Name.length + 1;
                var end = document.cookie.indexOf(";", start);
                if (end == -1) end = document.cookie.length;
                return document.cookie.substring(start, end);
            }
            else
                return "";
        }

        Table.prototype.LoadSpecialList = function (type, newElement, content, curValue) {
            var newElement = document.createElement(newElement);
            newElement.style.width = "90%";
            var curIndex;
            if (type == "select") {
                //                var newElement = document.createElement("select");
                for (var i = 0; i < content.length; i++) {
                    newElement.options[i] = new Option(content[i].Text, content[i].ID);
                    if (content[i].Text == curValue) {
                        newElement.options[i].selected = true;
                    }
                }
            }
            else if (type == "text") {
                //                newElement = document.createElement("<input type='" + type + "'>");
                newElement.value = curValue;
            }
            return newElement;
        }

        Table.prototype.addNewRow = function (tableid, arrrowType, arrrowElement, arrrowData, tbodyID, listNewElementsID, listCheck) {

            var curtable = document.getElementById(tableid)
            var rowLength = curtable.rows.length;

            var newTR = document.createElement("tr");
            newTR.id = tbodyID + (rowLength - 1).toString();

            if ((rowLength - 1) % 2 == 0)
                newTR.className = this.evenRowCSS;
            else
                newTR.className = this.oddRowCSS;
            newTR.onclick = function () { Table.prototype.SelRow(this, m_selRowCSS) }

            var firstTD = document.createElement('td');
            firstTD.innerText = rowLength;
            newTR.appendChild(firstTD);

            for (var i = 0; i < arrrowType.length; i++) {
                var td = document.createElement("td");
                var newElement = document.createElement(arrrowElement[i]);
                newElement.id = listNewElementsID[i] + (rowLength - 1).toString();
                newElement.name = newElement.id;
                if (arrrowElement[i] == "input") {
                    newElement.type = arrrowType[i];
                }
                newElement.style.width = "90%";

                var curIndex;
                if (arrrowType[i] == "select") {
                    //                var newElement = document.createElement("select");
                    for (var j = 0; j < arrrowData[i].length; j++) {
                        newElement.options[j] = new Option(arrrowData[i][j].Text, arrrowData[i][j].ID);
                    }
                }
                td.appendChild(newElement);
                if (newElement.type == "text" && listCheck[i] == "y") {
                    //var aa = document.getElementById(newTR.id);
                    //newElement.onClick = "Selid('" + listNewElementsID[i] + "','" + newElement.id + "','div" + newElement.id + "','ul" + newElement.id + "','LoadInfo')";
                    newElement.setAttribute("onclick", "Selid('" + listNewElementsID[i] + "','" + newElement.id + "','div" + newElement.id + "','ul" + newElement.id + "','LoadInfo')");
                    newElement.setAttribute("onkeyup", "keyup('" + listNewElementsID[i] + "','" + newElement.id + "','div" + newElement.id + "','ul" + newElement.id + "','LoadInfo')");
                    //newElement.setAttribute("onBlur", "$('#div" + newElement.id + "').hide();")
                    td.innerHTML += "<div id='div" + newElement.id + "' class='selDiv'><ul  style='list-style: none; color:black;' id='ul" + newElement.id + "'></ul></div>";
                }

                newTR.appendChild(td);
            }
            document.getElementById(tbodyID).appendChild(newTR);
        }

        Table.prototype.addRow = function (tableid, tbodyID) {
            var isneed = false;
            var rowLength = document.getElementById(tableid).rows.length;
            //获得Table对象
            var tableObject = document.getElementById(tbodyID);
            //判断是否有必要添加新的输入行

            var CloneRow = document.getElementById(tbodyID + (rowLength - 2).toString());

            var NewRow = CloneRow.cloneNode(true); //深度复制
            //这里清空CloneRow输入的值
            NewRow.id = tbodyID + (rowLength - 1).toString();
            if ((rowLength - 1) % 2 == 0)
                NewRow.className = this.evenRowCSS;
            else
                NewRow.className = this.oddRowCSS;
            NewRow.onclick = function () { Table.prototype.SelRow(this, m_selRowCSS) }

            NewRow.cells[0].innerHTML = Number(NewRow.cells[0].innerHTML) + 1;
            var input = NewRow.getElementsByTagName("input");
            for (var i = 0; i < input.length; i++) {
                if (input[i].type == "text")
                    input[i].value = "";
                input[i].id = input[i].name + (rowLength - 1).toString();
                input[i].name = input[i].id;

            }
            var selects = NewRow.getElementsByTagName("select");

            for (var i = 0; i < selects.length; i++) {

                selects[i].id = selects[i].name + (rowLength - 1).toString();
                selects[i].name = selects[i].id;
            }

            tableObject.appendChild(NewRow);
        }

        Table.prototype.removeRow = function (tableid, tbodyID, typeNames) {
            //            var rowIndex = document.cookie.split('=')[1].substring(document.cookie.split('=')[1].length - 1);
            //            var curID = this.getCookieByID("CurRowIDT");
            var rowIndex = -1;
            if (newRowID != "")
                rowIndex = newRowID.replace(tbodyID, '');
            if (rowIndex != -1) {
                document.getElementById(tbodyID).deleteRow(rowIndex);

                if (rowIndex < document.getElementById(tbodyID).rows.length) {
                    for (var i = rowIndex; i < document.getElementById(tbodyID).rows.length; i++) {
                        var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                        tr.id = tbodyID + i;
                        tr.childNodes[0].innerHTML = parseInt(i) + 1;
                        for (var j = 1; j < tr.childNodes.length; j++) {
                            var td = tr.childNodes[j];
                            td.childNodes[0].id = typeNames[j - 1] + i;
                            td.childNodes[0].name = typeNames[j - 1] + i;

                        }
                    }
                }
                //                this.setCookieRowID("");
                if (document.getElementById(tbodyID).rows.length > 0) {

                    if (rowIndex == document.getElementById(tbodyID).rows.length)
                        this.SelRow(document.getElementById(tbodyID + (rowIndex - 1)), m_selRowCSS);
                    else
                        this.SelRow(document.getElementById(tbodyID + rowIndex), m_selRowCSS);
                }
                //            this.setCookieRowID("");
            }
        }
    }

    Table.__initialized = true;
}


function Selid(actionid, selid, divid, ulid, jsfun) {

    $.ajax({
        url: "../SalesManage/GetConfigInfo",
        type: "post",
        data: { TaskType: actionid, condition: $('#' + selid).val() },
        dataType: "json",
        success: function (data) {
            if (data.success == true) {
                //                alert(data.datas);
                var unit = data.datas.split(',');
                var X = $('#' + selid).offset().top + 21;

                var Y = $('#' + selid).offset().left;
                $("#" + divid).attr("style", "left:" + Y + "px;top:" + X + "px;width:" + $("#" + selid).width() + "px;");
                $("#" + divid).show();
                //                document.getElementById(divid).style.display = "block";
                $("#" + ulid + " li").remove();
                //                alert(unit.length)
                for (var i = 0; i < unit.length; i++) {
                    $("#" + ulid).append('<li style="cursor:pointer;margin-left:0px; width:' + $("#" + selid).width() + 'px;height:20px;list-style-type:none;"><span onclick="' + jsfun + '(this.innerText,\'' + selid + '\',\'' + divid + '\');" style="margin-left:8px; color:black; width:' + $("#" + selid).width() + 'px; height:20px;display:block;"><nobr>' + unit[i].split('-')[0] + '</nobr></span>');
                }
            }
            else {
                $("#" + divid).hide();
            }
        }
    });
}

function LoadInfo(listInfo, vid, divid) {
    $("#" + vid).val(listInfo);
    $("#" + divid).hide();
    if (vid.indexOf('SampleName') >= 0) {
        loadBasisAndItem(vid);
    }
    if (vid.indexOf("ClienName") >= 0) {

        loadClienInfo(vid);
    }
}

function AddTableRow(json, selRowStyle) {
    var curtable = $("#SampleInfo");
    var rowLength = $("#Samplecontent > tr").length;
    var newTR = document.createElement("tr");
    newTR.id = "Samplecontent" + rowLength.toString();
    if (rowLength % 2 == 0)
        newTR.className = "bhs";
    else
        newTR.className = "bwhite";
    newTR.onclick = function () {
        selRow(this, selRowStyle);
    }
    var newTD = document.createElement("td");
    var newTable = document.createElement("table");
    newTable.style.width = "100%";
    var data = eval(json);

    for (var i = 0; i < data.length; i++) {
        var newTableTR = document.createElement("tr");
        for (var j = 0; j < data[i].names.length; j++) {
            var td = document.createElement("td");
            if (data[i].listTdWidth[j] != "") {
                td.style.width = data[i].listTdWidth[j];
            }
            if (data[i].listColSpan != "") {
                if (data[i].listColSpan[j] > 1)
                    td.colSpan = data[i].listColSpan[j].toString();
            }

            if (data[i].names[j] == "name") {
                td.innerText = data[i].listElementdID[j];
                td.align = "center";

            } else if (data[i].names[j] == "element") {
                var newElement = document.createElement(data[i].listElements[j]);
                newElement.id = data[i].listElementdID[j] + rowLength.toString();
                newElement.name = newElement.id;
                if (data[i].listElements[j] == "input") {
                    newElement.type = data[i].listType[j];
                }
                if (data[i].listElements[j] == "textarea") {
                    newElement.rows = data[i].listType[j];
                }
                newElement.style.width = "90%";
                td.appendChild(newElement);
                if (newElement.type == "text" && data[i].listChenks[j] == "y") {
                    newElement.setAttribute("onclick", "Selid('" + data[i].listElementdID[j] + "','" + newElement.id + "','div" + newElement.id + "','ul" + newElement.id + "','LoadInfo')")
                    newElement.setAttribute("onkeyup", "keyup('" + data[i].listElementdID[j] + "','" + newElement.id + "','div" + newElement.id + "','ul" + newElement.id + "','LoadInfo')")
                    td.innerHTML += "<div id='div" + newElement.id + "' class='selDiv'><ul style='list-style:none;color:black;' id='ul" + newElement.id + "'></ul></div>";
                }
            }
            newTableTR.appendChild(td);
        }
        newTable.appendChild(newTableTR);
    }

    newTD.appendChild(newTable);
    newTR.appendChild(newTD);
    document.getElementById("Samplecontent").appendChild(newTR);
}

function selRow(curRow, selRowStyle) {
    var oldSelRow = null;
    //            if (OldID != "")
    //                oldSelRow = document.getElementById(OldID);
    if (newRowID != "")
        oldSelRow = document.getElementById(newRowID);
    var curElement = document.getElementById(curRow.id);
    var className = selRowStyle;

    if (!hasClass(curElement, className)) {
        addClass(curElement, className);
    }
    if (oldSelRow != null && newRowID != curRow.id) {
        if (hasClass(oldSelRow, className))
            removeClass(oldSelRow, className);
    }
    //            this.setCookieRowID(curRow.id);
    newRowID = curRow.id;
}


function removeRow(tableid, tbodyID, typeNames) {

    if (newRowID != "")
        rowIndex = newRowID.replace(tbodyID, '');
    if (rowIndex != -1) {
        document.getElementById(tbodyID).deleteRow(rowIndex);

        if (rowIndex < $("#" + tbodyID + " > tr").length) {
            for (var i = rowIndex; i < $("#" + tbodyID + " > tr").length; i++) {
                var tr = document.getElementById(tbodyID + (parseInt(i) + 1));
                tr.id = tbodyID + i;
                if (i % 2 == 0)
                    tr.className = "bhs";
                else
                    tr.className = "bwhite";
                for (var j = 0; j < tr.childNodes.length; j++) {
                    //var childtr = tr.childNodes[0].childNodes[0].childNodes[j];
                    //alert(tr.childNodes[j].innerHTML);
                    //alert(i);
                    var html = tr.childNodes[j].innerHTML;
                    for (var k = 0; k < typeNames.length; k++) {

                        var oldid = typeNames[k] + (parseInt(i) + 1);
                        var newid = typeNames[k] + i;
                        //alert(oldid+"===="+newid)
                        var reg = new RegExp(oldid, "g");
                        // for (var l = 0; l < html.replace(oldid).length ; l++) {
                        html = html.replace(reg, newid);
                        //}



                    }

                    tr.childNodes[j].innerHTML = html;
                }


            }
        }

        //                this.setCookieRowID("");
        if (document.getElementById(tbodyID).rows.length > 0) {

            if (rowIndex == document.getElementById(tbodyID).rows.length)
                selRow(document.getElementById(tbodyID + (rowIndex - 1)), '');
            else
                selRow(document.getElementById(tbodyID + rowIndex), '');
        }
        //            this.setCookieRowID("");
    }
}

function LoadTableTBody(json, selRowStyle, jsonTable) {
    for (var k = 0; k < jsonTable.length; k++) {

        var curtable = $("#SampleInfo");
        //var rowLength = $("#Samplecontent > tr").length;
        var newTR = document.createElement("tr");
        newTR.id = "Samplecontent" + k.toString();
        if (k % 2 == 0)
            newTR.className = "bhs";
        else
            newTR.className = "bwhite";
        newTR.onclick = function () {
            selRow(this, selRowStyle);
        }
        var newTD = document.createElement("td");
        var newTable = document.createElement("table");
        newTable.style.width = "100%";
        var data = eval(json);

        for (var i = 0; i < data.length; i++) {
            var newTableTR = document.createElement("tr");
            for (var j = 0; j < data[i].names.length; j++) {
                var td = document.createElement("td");
                if (data[i].listTdWidth[j] != "") {
                    td.style.width = data[i].listTdWidth[j];
                }
                if (data[i].listColSpan != "") {
                    if (data[i].listColSpan[j] > 1)
                        td.colSpan = data[i].listColSpan[j].toString();
                }

                if (data[i].names[j] == "name") {
                    td.innerText = data[i].listElementdID[j];
                    td.align = "center";

                } else if (data[i].names[j] == "element") {
                    var newElement = document.createElement(data[i].listElements[j]);
                    newElement.id = data[i].listElementdID[j] + k.toString();
                    newElement.name = newElement.id;



                    if (data[i].listElements[j] == "input") {
                        newElement.type = data[i].listType[j];
                    }
                    if (data[i].listElements[j] == "textarea") {
                        newElement.rows = data[i].listType[j];
                    }
                    newElement.style.width = "90%";
                    if (data[i].listElements[j] == "input" || data[i].listElements[j] == "textarea") {
                        newElement.value = jsonTable[k][data[i].listElementdID[j]];
                    }
                    td.appendChild(newElement);
                    if (newElement.type == "text" && data[i].listChenks[j] == "y") {
                        newElement.setAttribute("onclick", "Selid('" + data[i].listElementdID[j] + "','" + newElement.id + "','div" + newElement.id + "','ul" + newElement.id + "','LoadInfo')")
                        newElement.setAttribute("onkeyup", "keyup('" + data[i].listElementdID[j] + "','" + newElement.id + "','div" + newElement.id + "','ul" + newElement.id + "','LoadInfo')")
                        //td.innerHTML += "<div id='div" + newElement.id + "' class='selDiv'><ul style='list-style:none;color:black;' id='ul" + newElement.id + "'></ul></div>";
                        var newDiv = document.createElement('div');
                        newDiv.id = 'div' + newElement.id;
                        newDiv.className = 'selDiv';
                        var newUl = document.createElement('ul');
                        newUl.id = 'ul' + newElement.id;
                        newUl.style = 'list-style: none; color:black;';
                        newDiv.appendChild(newUl);
                        td.appendChild(newDiv);



                    }
                }
                newTableTR.appendChild(td);
            }
            newTable.appendChild(newTableTR);
        }

        newTD.appendChild(newTable);
        newTR.appendChild(newTD);
        document.getElementById("Samplecontent").appendChild(newTR);
    }
}




function changeTestType(rowsNumber) {
    rowsNumber = rowsNumber.split("TestType")[1]
    var TestTypeId = "TestType"
    var ChildTestTypeId = "ChildTestType";
    if (rowsNumber != "") {
        TestTypeId += rowsNumber;
        ChildTestTypeId += rowsNumber;
    }
    $.ajax({
        url: "GetChildTestType",
        type: "post",
        data: { FatherTestType: $("#" + TestTypeId).val() },
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            $("#" + ChildTestTypeId).find("option").remove();
            for (var i = 0; i < data.length; i++) {
                $("#" + ChildTestTypeId).append("<option value='" + data[i].Value + "'>" + data[i].Text + "</option>");
            }

        }
    });
}


function changeChildTestType(rowsNumber) {
    rowsNumber = rowsNumber.split("ChildTestType")[1]
    var ChildTestTypeId = "ChildTestType";
    var TestItemId = "TestItem";
    if (rowsNumber != "") {
        ChildTestTypeId += rowsNumber;
        TestItemId += rowsNumber;
    }
    $.ajax({
        url: "GetTestItem",
        type: "post",
        data: { TID: $("#" + ChildTestTypeId).val() },
        dataType: "json",
        async: false, //是否异步
        success: function (data) {
            $("#" + TestItemId).find("option").remove();
            for (var i = 0; i < data.length; i++) {
                $("#" + TestItemId).append("<option value='" + data[i].Value + "'>" + data[i].Text + "</option>");
            }

        }
    });
}

function AddTestItem(rowsNumber) {
    rowsNumber = rowsNumber.split("TestItem")[1];
    var TestingItemId = "TestingItem";
    var TestItemId = "TestItem";
    if (rowsNumber != "") {
        TestingItemId += rowsNumber;
        TestItemId += rowsNumber;
    }
    var TestingItem = $("#" + TestingItemId).val();
    var TestItems = $("#TestingItems").val();
    var TestItemText = $("#" + TestItemId + "   option:selected").text();
    if (TestingItem.indexOf(TestItemText) < 0) {
        if (TestingItem == "")
            $("#" + TestingItemId).val(TestItemText);
        else
            $("#" + TestingItemId).val(TestingItem + "," + TestItemText);
    }
    if (TestItems.indexOf(TestItemText) < 0) {
        if (TestItems == "")
            $("#TestingItems").val(TestItemText);
        else
            $("#TestingItems").val(TestItems + "," + TestItemText);
    }



}

function loadBasisAndItem(vid) {
    $.ajax({
        url: "../MandateInfo/GetBasisAndItem",
        type: "post",
        data: { SampleName: $('#' + vid).val() },
        dataType: "json",
        success: function (data) {
            if (data.success == true) {
                var josn = eval(data.datas);
                var rowNumber = vid.split("SampleName")[1];

                var TestingBasis = $("#TestingBasis").val();// TestingBasis0
                var STestingBasis = $("#TestingBasis" + rowNumber).val();

                //var TestingBasisID = "";
                var TestItems = $("#TestingItems").val();
                var STestItems = $("#TestingItem" + rowNumber).val();
                //var TestItemIDs = "";

                for (var i = 0; i < josn.length; i++) {
                    if (TestingBasis.indexOf(josn[i].TestingBasis) < 0) {
                        (TestingBasis == "") ? TestingBasis = josn[i].TestingBasis : TestingBasis += ',' + josn[i].TestingBasis;
                        //(TestingBasisID == "") ? TestingBasisID = josn[i].TestingBasisId : TestingBasisID += '，' + josn[i].TestingBasisId;
                    }
                    if (STestingBasis.indexOf(josn[i].TestingBasis) < 0) {
                        (STestingBasis == "") ? STestingBasis = josn[i].TestingBasis : STestingBasis += ',' + josn[i].TestingBasis;
                    }

                    if (TestItems.indexOf(josn[i].ItemContent) < 0) {
                        (TestItems == "") ? TestItems = josn[i].ItemContent : TestItems += ',' + josn[i].ItemContent;
                        //(TestItemIDs == "") ? TestItemIDs = josn[i].ItemId : TestItemIDs += '，' + josn[i].ItemId;
                    }
                    if (STestItems.indexOf(josn[i].ItemContent) < 0) {
                        (STestItems == "") ? STestItems = josn[i].ItemContent : STestItems += ',' + josn[i].ItemContent;
                        //(TestItemIDs == "") ? TestItemIDs = josn[i].ItemId : TestItemIDs += '，' + josn[i].ItemId;
                    }

                }

                $("#TestingBasis").val(TestingBasis)
                //预留依据ID赋值

                $("#TestingBasis" + rowNumber).val(STestingBasis)
                $("#TestingItems").val(TestItems);
                $("#TestingItem" + rowNumber).val(STestItems)



            }
            else {

            }
        }
    });
}