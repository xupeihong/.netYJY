var m_selRowCSS = '';
var newRowID = '';

function Table(jsonTable, oddStyle, evenStyle, selStyle, arrListID, arrListType, arrNewElement, arrListInfo, listNewElementsID, listCheck, TaskType) {
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
                                //getElement.name = getElement.id;
                                getElement.name = this.specialElementsID[j];
                                //if (arrListType[i] == "text" && listCheck[i] == "y") {
                                //    getElement.setAttribute("onclick", "Selid('" + parseInt(this.jsonData.length - 1) + "','" + listNewElementsID[i] + "','" + TaskType + "')");
                                //}

                                if (this.specialNew[j] == "input") {
                                    getElement.type = this.specialType[j];
                                }

                                newTD.appendChild(getElement);
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
            newElement.style.width = "94%";

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

        Table.prototype.addNewRow = function (tableid, arrrowType, arrrowElement, arrrowData, tbodyID, listNewElementsID, listCheck, TaskType) {

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
                //newElement.name = newElement.id;
                newElement.name = listNewElementsID[i];
                if (arrrowElement[i] == "input") {
                    newElement.type = arrrowType[i];
                }
                newElement.style.width = "94%";

                var curIndex;
                if (arrrowType[i] == "select") {
                    //                var newElement = document.createElement("select");
                    for (var j = 0; j < arrrowData[i].length; j++) {
                        newElement.options[j] = new Option(arrrowData[i][j].Text, arrrowData[i][j].ID);
                    }
                }
                if (arrrowType[i] == "select" && TaskType == "Internal") {
                    //                var newElement = document.createElement("select");
                    for (var j = 0; j < arrrowData[i].length; j++) {
                        newElement.options[j] = new Option(arrrowData[i][j].Text, arrrowData[i][j].ID);
                        if (arrrowData[i][j].Text == "安装后付款") {
                            newElement.options[j].style.color = "red";
                        }
                    }

                    newElement.setAttribute("onchange", "ChangePayWay('" + parseInt(rowLength - 1) + "')");
                }
                td.appendChild(newElement);
                if (newElement.type == "text" && listCheck[i] == "y") {
                    //newElement.value = "请选择...";
                    newElement.setAttribute("value", "请选择...");
                    //var aa = document.getElementById(newTR.id);
                    newElement.setAttribute("onclick", "Selid('" + parseInt(rowLength - 1) + "','" + listNewElementsID[i] + "','" + TaskType + "')");
                    //newElement.onclick = "Selid('" + parseInt(rowLength - 1) + "','" + listNewElementsID[i] + "')";
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
                            //td.childNodes[0].name = typeNames[j - 1] + i;
                            td.childNodes[0].name = typeNames[j - 1];
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


//function Selid(actionid, selid, divid, ulid, jsfun) {
//    $.ajax({
//        url: "../SalesManage/GetConfigInfo",
//        type: "post",
//        data: { TaskType: actionid },
//        dataType: "json",
//        success: function (data) {
//            if (data.success == true) {
//                //                alert(data.datas);
//                var unit = data.datas.split(',');
//                $("#" + divid).show();
//                //                document.getElementById(divid).style.display = "block";
//                $("#" + ulid + " li").remove();
//                //                alert(unit.length)
//                for (var i = 0; i < unit.length; i++) {
//                    $("#" + ulid).append("<li style='cursor:pointer;margin-left:-40px; width:150px;height:20px;list-style-type:none;'><span onclick='" + jsfun + "(this.innerText," + selid + "," + divid + ");' style='margin-left:8px; color:black; width:150px; height:20px;display:block;'>" + unit[i].split('-')[0] + "</span>");
//                }
//            }
//            else {
//                $("#" + divid).hide();
//            }
//        }
//    });
//}

function Selid(id, listElemnt, TaskType) {
    var filed = listElemnt + id;
    document.getElementById(filed).value = "";
    //var returnInfo = ShowIframe1("选择货品信息", "../SalesRetail/GetProduct", 500, 500);
    var returnInfo = window.showModalDialog("../SalesRetail/GetProduct", "", "dialogWidth:850px;dialogHeight:500px;status:no;scroll:no;resizable:yes;edge:sunken;location:no;");

    if (returnInfo == undefined)
        returnInfo = "";
    else {
        var arrReturn = returnInfo.split(',');
        if (TaskType == "Order") {
            document.getElementById(filed).value = arrReturn[0];
            document.getElementById("OrderContent" + id).value = arrReturn[0];
            document.getElementById("SpecsModels" + id).value = arrReturn[2];
            document.getElementById("UnitPrice" + id).value = arrReturn[3];
        }
        else if (TaskType == "Internal" || TaskType == "Shoppe") {
            document.getElementById("ProductID" + id).value = arrReturn[1];
            document.getElementById("OrderContent" + id).value = arrReturn[0];
            document.getElementById("SpecsModels" + id).value = arrReturn[2];
        }
        else if (TaskType == "Sample") {
            document.getElementById("ProductID" + id).value = arrReturn[1];
            document.getElementById("OrderContent" + id).value = arrReturn[0];
            document.getElementById("SpecsModels" + id).value = arrReturn[2];
            document.getElementById("Total" + id).value = arrReturn[3];
        }
        else if (TaskType == "RevokeInfo") {
            document.getElementById("txtProductID" + id).value = arrReturn[1];
            document.getElementById("txtOrderContent" + id).value = arrReturn[0];
            document.getElementById("txtSpecsModels" + id).value = arrReturn[2];
            document.getElementById("txtTotal" + id).value = arrReturn[3];
        }
    }
}

function ChangePayWay(id) {
    //var PayVal = $("#PayWay" + id).find("option:selected").text();
    //if (PayVal == "安装后付款") {
    //    //$("#PayWay" + id).css("color", "red");
    //    //$("#PayWay" + id + "option[text='jquery']").attr("css:color", "red");
    //    var ad = $("#PayWay" + id).get(0).options[2].text;
    //    $("#PayWay" + id).get(0).options[2].color = "red";
    //}
    //var optionSelect = document.getElementById("PayWay" + id);
    //for (var i = 0; i < optionSelect.options.length; i++) {
    //    if (optionSelect.options[i].selected == true && optionSelect.options[i].value == "Pa3") {
    //        optionSelect.innerHTML = "<span style='color:red'>" + optionSelect.options[i].innerText + "</span>";
    //    }
    //}
}

function LoadInfo(listInfo, vid, divid) {
    $("#" + vid.id + "").val(listInfo);
    $("#" + divid.id + "").hide();
}