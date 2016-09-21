var curPage = 1;
var OnePageCount = 6;
var Customer;
var FollowPerson;
var oldSelID = 0;
var RWID;
var curPage1 = 1;
var OnePageCount1 = 6;
var newRowID;
var curPage2 = 1;
var OnePageCount2 = 4;


$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    jq();
    jq1("");
    jq2("");
    //jq3("");
    document.getElementById('bor1').style.display = 'block';
    document.getElementById('bor2').style.display = 'none';
    //document.getElementById('bor3').style.display = 'none';

    $("#DetailXX").click(function () {
        this.className = "btnTw";
        $('#BillXX').attr("class", "btnTh");
        //$('#SPXX').attr("class", "btnTh");
        document.getElementById('bor1').style.display = 'block';
        document.getElementById('bor2').style.display = 'none';
        //document.getElementById('bor3').style.display = 'none';
    });
    $("#BillXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        //$('#SPXX').attr("class", "btnTh");
        document.getElementById('bor1').style.display = 'none';
        document.getElementById('bor2').style.display = 'block';
        //document.getElementById('bor3').style.display = 'none';
    });
    //$("#SPXX").click(function () {
    //    this.className = "btnTw";
    //    $('#DetailXX').attr("class", "btnTh");
    //    $('#BillXX').attr("class", "btnTh");
    //    document.getElementById('bor1').style.display = 'none';
    //    document.getElementById('bor2').style.display = 'none';
    //    document.getElementById('bor3').style.display = 'block';
    //});

    $("#DaYin").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).LLID;
        if (rowid == null) {
            alert("请在列表中选择一条数据");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).LLID;
            var url = "PrintLL?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:500px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    $("#TJShenPi").click(function () {

        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var LLID = jQuery("#list").jqGrid('getRowData', rowid).LLID;
        if (rowid == null) {
            alert("请选择要审批的领料单");
            return;
        }
        else {
            $.ajax({
                async: false,
                url: "getTJLL",
                type: "post",
                single: true,
                data: { LLID: LLID },
                dataType: "json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            if (json[i].name == "新建" || json[i].name == "审批未通过") {
                                var PID = jQuery("#list").jqGrid('getRowData', rowid).LLID;
                                var texts = PID + "@" + "领料单审批";
                                window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
                            }
                            else {
                                alert("该任务单正在审批或审批已通过");
                                return;
                            }
                        }
                    }
                }
            })
        }
    })

    $('#ShenPi').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var LLID = jQuery("#list").jqGrid('getRowData', rowid).LLID;
        if (rowid == null) {
            alert("请选择要进行审批的条目");
            return;
        }
        else {
            $.ajax({
                async: false,
                url: "getPDLLSP",
                type: "post",
                single: true,
                data: { LLID: LLID },
                dataType: "json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        var SPID = jQuery("#list").jqGrid('getRowData', rowid).SPID;
                        var PID = jQuery("#list").jqGrid('getRowData', rowid).LLID;
                        var state = jQuery("#list").jqGrid('getRowData', rowid).State;
                        if (state == $("#Nostate").val()) {
                            alert("审批不通过，不能进行审批了");
                            return;
                        }
                        $.ajax({
                            url: "../COM_Approval/JudgeAppDisable",
                            type: "post",
                            data: { data1: $("#webkey").val(), data2: SPID },
                            dataType: "Json",
                            success: function (data) {
                                if (data.success == "true") {
                                    var bol = data.intblo;
                                    if (bol == "-1") {
                                        alert("您没有审批权限，不能进行审批操作");
                                        return;
                                    }
                                    if (bol == "1") {
                                        alert("您已经审批完成，不能进行审批操作");
                                        return;
                                    }
                                    if (bol == "2") {
                                        alert("审批过程还没有进行到您这一步，不能进行审批操作");
                                        return;
                                    }
                                    var texts = $("#webkey").val() + "@" + SPID + "@" + PID;
                                    window.parent.OpenDialog("审批", "../COM_Approval/Approval?id=" + texts, 500, 400, '');
                                }
                                else {
                                    return;
                                }
                            }
                        });
                    }
                    else {
                        alert("该领料单还未提交审批不能进行审批");
                        return;
                    }
                }
            })
        }
    })

    $("#SC").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要上传图片的领料单");
            return;
        } else {
            window.parent.parent.OpenDialog("上传文件", "../ProduceManage/SCMaterialForm?OId=" + Model.LLID + "&ID=领料单", 400, 200);
        }
    })

    $("#CK").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要上传图片的领料单");
            return;
        } else {
            window.parent.parent.OpenDialog("查看文件", "../ProduceManage/CKMaterialForm?OId=" + Model.LLID, 500, 500);
        }
    })
})


function CXLL() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要撤销的领料单");
        return;
    }
    else {
        var msg = "您真的撤销改领料单吗?";
        if (confirm(msg) == true) {
            var LLID = Model.LLID;
            var RWID = Model.RWID;
            $.ajax({
                type: "POST",
                url: "CXLL",
                data: { LLID: LLID, RWID: RWID },
                success: function (data) {
                    alert(data.Msg);
                    reload();
                },
                dataType: 'json'
            });
            return true;
        } else {
            return false;
        }
    }
}
//跳转到领料单修改页面
function UpdateMaterialForm() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要修改的领料单");
        return;
    } else {
        window.parent.parent.OpenDialog("修改领料单", "../ProduceManage/UpdateMaterialForm?LLID=" + Model.LLID, 800, 550);
    }
}

function Addnewll() {
    window.parent.parent.OpenDialog("新增领料单", "../ProduceManage/Addnewll", 1000, 600);
}

//跳转到领料单详情页面
function MaterialFormDetail() {
    var dataSel = jQuery("#list").jqGrid('getGridParam');
    var ids = dataSel.selrow;
    var Model = jQuery("#list").jqGrid('getRowData', ids);
    if (ids == null) {
        alert("请选择要操作的行");
        return;
    } else {
        window.parent.parent.OpenDialog("领料单详情", "../ProduceManage/MaterialFormDetail?LLID=" + Model.LLID, 800, 550);
    }
}
function jq() {
    var OrderUnit = $('#OrderUnit').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();

    jQuery("#list").jqGrid({
        url: 'Materialrequisitionlist',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderUnit: OrderUnit, OrderContent: OrderContent, SpecsModels: SpecsModels, Starts: Starts, Starte: Starte },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['序号', '领料单编号', '领料部门', '订货单位', '开单日期', '产品编号', '产品名称', '规格型号', '数量', '状态', '', ''],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'LLID', index: 'LLID', width: 120, align: "center" },
        { name: 'MaterialDepartment', index: 'MaterialDepartment', width: 120, align: "center" },
        { name: 'OrderUnit', index: 'OrderUnit', width: 120, align: "center" },
        { name: 'CreateTime', index: 'CreateTime', editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' }, width: 150, align: "center" },
        { name: 'PID', index: 'PID', width: 100, align: "center" },
        { name: 'OrderContent', index: 'OrderContent', width: 100, align: "center" },
        { name: 'SpecsModels', index: 'SpecsModels', width: 80, align: "center" },
        { name: 'Amount', index: 'Amount', width: 80, align: "center" },
        { name: 'State', index: 'State', width: 80, align: "center" },
        { name: 'RWID', index: 'RWID', width: 80, align: "center", hidden: true },
        { name: 'SPID', index: 'SPID', width: 80, align: "center", hidden: true }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;

            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {

                return;
            }
            else {
                var LLID = Model.LLID;
                reload1(LLID);
                LoadReceiveBill(LLID);
                reload3(Model.SPID)
            }
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager :input").val();
            }
            reload();
        },
        loadComplete: function () {
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 350, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    var OrderUnit = $('#OrderUnit').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();

    $("#list").jqGrid('setGridParam', {
        url: 'Materialrequisitionlist',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderUnit: OrderUnit, OrderContent: OrderContent, SpecsModels: SpecsModels, Starts: Starts, Starte: Starte },

    }).trigger("reloadGrid");//重新载入
}

//查询
function Search() {
    //判断开始日期
    var strDateStart = $('#Starts').val();
    var strDateEnd = $('#Starte').val();
    if (strDateStart == "" && strDateEnd == "") {

        getSearch();
    }
    else {
        var strSeparator = "-"; //日期分隔符
        var strDateArrayStart;
        var strDateArrayEnd;
        var intDay;
        strDateArrayStart = strDateStart.split(strSeparator);
        strDateArrayEnd = strDateEnd.split(strSeparator);
        var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
        var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);
        if (strDateS <= strDateE) {
            getSearch();
        }
        else {
            alert("开单截止日期不能小于开始日期！");
            $("#Starte").val("");
            return false;
        }


    }
}

function getSearch() {
    curRow = 0;
    curPage = 1;

    var OrderUnit = $('#OrderUnit').val();
    var OrderContent = $('#OrderContent').val();
    var SpecsModels = $('#SpecsModels').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();

    $("#list").jqGrid('setGridParam', {
        url: 'Materialrequisitionlist',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, OrderUnit: OrderUnit, OrderContent: OrderContent, SpecsModels: SpecsModels, Starts: Starts, Starte: Starte },
        loadonce: false

    }).trigger("reloadGrid");//重新载入
}

//加载
function jq1(LLID) {
    var aa = $("#Rights").val();
    $.ajax({
        url: "ProMaterialFDetail",
        type: "post",
        single: true,
        data: { LLID: LLID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;//1
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRows(this)">'
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';
                    //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Specifications' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable class="labOrderNum' + rowCount + ' " id="OrderNum' + rowCount + '">' + json[i].OrderNum + '</lable> </td>';
                    html += '<td ><lable class="labTechnology' + rowCount + ' " id="Technology' + rowCount + '">' + json[i].Technology + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable><lable class="labRemark' + rowCount + ' " id="DID' + rowCount + '"style="display:none">' + json[i].DID + '</lable>  </td>';
                    html += ' @if ("' + aa + '".IndexOf(", 6, ") >= 0){<td > <a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>}';
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
}

function selRows(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}

//移除行代码
function deleteTr(curRow) {
    var one = confirm("确认删除");
    if (one == false)
        return;
    else {
        newRowID = curRow.id;
        $("#DetailInfo tr").removeAttr("class");
        $("#" + newRowID).attr("class", "RowClass");

        var a = "#" + newRowID
        ss = Number(a.substring(12, 13));
        //alert(ss);
        //字符串截取DetailInfo，要剩下的   int
        //和did的id组装成需要的did的id
        var DID = document.getElementById("DID" + ss).innerHTML;
        var tbodyID = "DetailInfo";
        var rowIndex = -1;  //一次修改，图号和规格合并，不显示规格
        var typeNames = ["RowNumber", "DID", "OrderContent", "SpecsModels", "Manufacturer", "OrderUnit", "OrderNum", "Technology", "Remark", "DetailInfo"];
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
                    selRows(document.getElementById(tbodyID + (rowIndex - 1)), '');
                else
                    selRows(document.getElementById(tbodyID + rowIndex), '');;
            }
        }
        $("#DetailInfo tr").removeAttr("class");

        $.ajax({
            type: "POST",
            url: "SCLLDetail",
            data: { DID: DID },
            success: function (data) {
                alert(data.Msg);

            },
            dataType: 'json'
        });
    }
}
//点击显示详情
function reload1(LLID) {
    var aa = $("#Rights").val();
    //给选中的行赋值为0
    for (var i = document.getElementById("DetailInfo").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("DetailInfo").removeChild(document.getElementById("DetailInfo").childNodes[i]);
    }
    $.ajax({
        url: "ProMaterialFDetail",
        type: "post",
        single: true,
        data: { LLID: LLID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRows(this)">'
                    html += '<td id="RowNumber' + rowCount + '">' + CountRows + '</td>';
                    html += '<td ><lable class="labDID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="OrderContent' + rowCount + '">' + json[i].OrderContent + '</lable> </td>';
                    html += '<td ><lable class="labSpecsModels' + rowCount + ' " id="SpecsModels' + rowCount + '">' + json[i].SpecsModels + '</lable> </td>';//一次修改  图号和规格合并
                    //html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Specifications' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><lable class="labManufacturer' + rowCount + ' " id="Manufacturer' + rowCount + '">' + json[i].Manufacturer + '</lable> </td>';
                    html += '<td ><lable class="labOrderUnit' + rowCount + ' " id="OrderUnit' + rowCount + '">' + json[i].OrderUnit + '</lable> </td>';
                    html += '<td ><lable class="labOrderNum' + rowCount + ' " id="OrderNum' + rowCount + '">' + json[i].OrderNum + '</lable> </td>';
                    html += '<td ><lable class="labTechnology' + rowCount + ' " id="Technology' + rowCount + '">' + json[i].Technology + '</lable> </td>';
                    html += '<td ><lable class="labRemark' + rowCount + ' " id="Remark' + rowCount + '">' + json[i].Remark + '</lable><lable class="labRemark' + rowCount + ' " id="DID' + rowCount + '"style="display:none">' + json[i].DID + '</lable> </td>';
                    //if (aa.indexOf("6") >= 0) {
                    //    html += ' <td ><a id="DetailInfo' + rowCount + '" onclick="deleteTr(this);" style="color:blue;cursor:pointer;text-decoration:underline;">删除</a> </td>';
                    //}
                    html += '</tr>'
                    $("#DetailInfo").append(html);
                }
            }
        }
    })
}


function jq2(LLID) {
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadLLs",
        type: "post",
        data: { LLID: LLID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {

                $("#ReceiveBill").val("");
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html += '<tr>'
                    var s = json[i].ID.substr(0, 2);
                    if (s == "LL") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '" style="width:33%">领料单信息</lable> </td>';
                    }
                    html += '<td ><lable class="labID' + rowCount + ' " id="ID' + rowCount + '" style="width:33%">' + json[i].ID + '</lable> </td>';
                    html += '<td ><a href="#" style="color:blue;width:33%" onclick=GetXX("' + json[i].ID + '")>详情</a></td>';
                    html += '</tr>'
                    $("#ReceiveBill").append(html);
                }
            }
        }
    })
}
function LoadReceiveBill(LLID) {
    for (var i = document.getElementById("ReceiveBill").childNodes.length - 1; i >= 0 ; i--) {
        document.getElementById("ReceiveBill").removeChild(document.getElementById("ReceiveBill").childNodes[i]);
    }
    rowCount = document.getElementById("ReceiveBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "LoadLLs",
        type: "post",
        data: { LLID: LLID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {

                $("#ReceiveBill").val("");
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html += '<tr>'
                    var s = json[i].ID.substr(0, 2);
                    if (s == "LL") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '" style="width:33%">领料单信息</lable> </td>';
                    }
                    html += '<td ><lable class="labID' + rowCount + ' " id="ID' + rowCount + '" style="width:33%">' + json[i].ID + '</lable> </td>';
                    html += '<td ><a href="#" style="color:blue;width:33%" onclick=GetXX("' + json[i].ID + '")>详情</a></td>';
                    html += '</tr>'
                    $("#ReceiveBill").append(html);
                }


            }
        }
    })
}

function GetXX(SDI) {
    var id = SDI;
    window.parent.parent.OpenDialog("详细", "../ProduceManage/LoadLL?ID=" + id, 800, 450);
}

function reload3(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list3").jqGrid('setGridParam', {
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, PID: jhid, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}

function jq3(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list3").jqGrid({
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: jhid, webkey: webkey, folderBack: folderBack },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['', '职务', '姓名', '审批方式', '人数', '审批情况', '审批意见', '审批时间', '备注'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'Job', index: 'Job', width: 100, align: "center" },
        { name: 'UserName', index: 'UserName', width: 100, align: "center" },
        { name: 'AppTypeDesc', index: 'AppTypeDesc', width: 100, align: "center" },
        { name: 'Num', index: 'Num', width: 100, align: "center" },
        { name: 'stateDesc', index: 'stateDesc', width: 100, align: "center" },
        { name: 'Opinion', index: 'Opinion', width: $("#bor").width() - 920, align: "center" },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 150, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'Remark', index: 'Remark', width: 200, align: "center" },
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                //var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                //jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (curPage3 == $("#list3").getGridParam("lastpage3"))
                    return;
                curPage3 = $("#list3").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                curPage3 = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (curPage3 == 1)
                    return;
                curPage3 = $("#list3").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                curPage3 = 1;
            }
            else {
                curPage3 = $("#pager3 :input").val();
            }
            reload3();
        },
        loadComplete: function () {
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() - 490, false);
            $("#list3").jqGrid("setGridWidth", $("#bor3").width() - 30, false);
        }
    });
}

