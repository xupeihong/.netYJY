var curPage = 1;
var FcunPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
var id = 0;
var ID = 0;
var DID = 0;
var OID = 0;
var objData = '';
var PID = "";
var ActionTitle = "";
var Applyer = "";
var StartDate = "";
var EndDate = "";
$(document).ready(function () {
    var userRole = $("#UserRole").val();
    var ExJob = $("#ExJob").val();
    if (userRole.indexOf(",4,") != "-1" && ExJob == "") {
        $("#divOperate").css("display", "block");
    }
    else {
        $("#divOperate").css("display", "none");
    }

    $("#pageContent").height($(window).height());
    LoadBasInfo();
    LoadSPInfo('');
    //  LoadDetail('');
    LoadFJ();
    $("#btnRecord").click(function () {
        window.parent.OpenDialog("促销活动申请", "../SalesRetail/ApplyPromotion", 900, 500, '');
    });

    $("#btnPrintSP").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要打印的申请记录...");
            return;
        }
        else {
            var PID = jQuery("#list").jqGrid("getRowData", rowId).PID;
            window.showModalDialog("../SalesRetail/PrintSPInfo?PID=" + PID + "&TaskType=Promotion", window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    $("#btnUpdate").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要修改的促销申请记录...");
            return;
        }
        else {
            var PID = jQuery("#list").jqGrid("getRowData", rowId).PID;
            window.parent.OpenDialog("修改活动申请", "../SalesRetail/UpdatePromotion?PID=" + PID, 900, 500, '');
        }
    });

    $("#btnSearch").click(function () {
        reload();
    });

    $("#btnSP").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要进行审批的促销活动申请");
            return false;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowId).State;
            if (State == "审批中")
            {
                alert("审批中的不能重复提交");
                return;
            }
            if (State == "审批完成") {
                alert("审批完成的不能重复提交");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowId).PID + "@" + "促销审批" + "@" + "促销活动";
            var PAID = jQuery("#list").jqGrid('getRowData', rowId).PID;


            //window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
            window.parent.OpenDialog("提交审批", "../SalesRetail/ApprovalCommon?id=" + texts, 700, 500, '');
        }
    });

    $("#btnDetail").click(function () {
        this.className = "btnTw";
        $('#btnSPInfo').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
    });

    $("#btnSPInfo").click(function () {
        this.className = "btnTw";
        $('#btnDetail').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'block';
    });

    $("#btnCancel").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要撤销的促销申请记录...");
            return;
        }
        else {
            var PID = jQuery("#list").jqGrid("getRowData", rowId).PID;
            if (confirm("是否确定要撤销编号为" + PID + "的促销申请记录?")) {
                $.ajax({
                    url: "DeletePromotion",
                    type: "post",
                    data: { PID: PID },
                    dataType: "Json",
                    success: function (data) {
                        if (data.success == "true") {
                            alert(data.Msg);
                            reload();
                        }
                        else {
                            alert(data.Msg);
                            return;
                        }
                    }
                });
            }
        }
    });
});

function reload() {
    if ($('.field-validation-error').length == 0) {
        ActionTitle = $("#ActionTitle").val();
        Applyer = $("#Applyer").val();
        StartDate = $("#StartDate").val();
        EndDate = $("#EndDate").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetPromotionGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, ActionTitle: ActionTitle, Applyer: Applyer, StartDate: StartDate, EndDate: EndDate },

        }).trigger("reloadGrid");
    }
}

function LoadBasInfo() {
    ActionTitle = $("#ActionTitle").val();
    Applyer = $("#Applyer").val();
    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();
    jQuery("#list").jqGrid({
        url: 'GetPromotionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, ActionTitle: ActionTitle, Applyer: Applyer, StartDate: StartDate, EndDate: EndDate },
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
        colNames: ['申请编号', '活动主题', '活动金额', '促销活动位置', '宣传方式', '活动参与人员', '申请人', '活动负责人', '备注', '状态', '所属单位'],
        colModel: [
        { name: 'PID', index: 'PID', width: 120 },
        { name: 'ActionTitle', index: 'ActionTitle', width: 150 },
        { name: 'ActionProject', index: 'ActionProject', width: 150 },
        { name: 'Position', index: 'Position', width: 180 },
        { name: 'ActionStyle', index: 'ActionStyle', width: 150 },
        { name: 'PurPose', index: 'PurPose', width: 220 },
        { name: 'Applyer', index: 'Applyer', width: 120 },
        { name: 'Manager', index: 'Manager', width: 120 },
        { name: 'Remark', index: 'Remark', width: 200 },
        { name: 'State', index: 'State', width: 120 },
        { name: 'UnitName', index: 'UnitName', width: 120 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            var PID = jQuery("#list").jqGrid("getRowData", rowid).PID;
            reload3(PID);
            //LoadDetail(PID);
            reloadFJ(PID)
            //LoadDetail(orderID);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function LoadDetail(PID) {
    var SalesType = "Promotion";
    $.post("../SalesRetail/GetRightsFiled?PID=" + PID + "&TaskType=" + SalesType, function (data1) {
        if (data1 == "1") {
            $.ajax({
                url: "GetPromotionFile",
                type: "post",
                data: { PID: PID, SalesType: SalesType },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        var arrFileName = new Array();
                        arrFileName = data.FileName.split(',');
                        var arrFileInfo = new Array();
                        arrFileInfo = data.FileInfo.split(',');
                        var html1 = "";

                        if (data.FileName == "") {
                            $("#bor1").html("");
                            return;
                        }
                        for (var i = 0; i < arrFileInfo.length - 1; i++) {
                            $("#bor1").html("");
                            var cross = arrFileName[i] + "/" + arrFileInfo[i];
                            if (arrFileName[i] != "") {
                                html1 += "<table style='width: 98%;' class='tabInfo'>"
                                html1 += "<tr><td style='width:65%;'>" + arrFileName[i] + "</td><td style='width:35%;text-align:center'><a style='color:blue;cursor:hand;' onclick='DownloadFile(\"" + PID + "\",\"" + arrFileName[i] + "\")'>下  载</a></td></tr>"
                                html1 += "</table>"
                            }
                        }
                        $("#bor1").html(html1);
                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });
        }
    });
}

function LoadSPInfo(PID) {
    var webkey = $('#webkey').val();
    var folderBack = $('#folderBack').val();
    jQuery("#list2").jqGrid({
        url: 'ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },
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
        { name: 'Job', index: 'Job', width: 100 },
        { name: 'UserName', index: 'UserName', width: 100 },
        { name: 'AppTypeDesc', index: 'AppTypeDesc', width: 100 },
        { name: 'Num', index: 'Num', width: 100 },
        { name: 'stateDesc', index: 'stateDesc', width: 100 },
        { name: 'Opinion', index: 'Opinion', width: $("#bor").width() - 920 },
        { name: 'ApprovalTime', index: 'ApprovalTime', width: 150 },
        { name: 'Remark', index: 'Remark', width: 200 },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list2").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                //var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                //jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (curPage == $("#list2").getGridParam("lastpage"))
                    return;
                curPage = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reload3(PID);
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

function reload3(PID) {
    var webkey = $('#webkey').val();
    var folderBack = $('#folderBack').val();
    $("#list2").jqGrid('setGridParam', {
        url: 'ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}


function DownloadFile(PID, FileName) {
    window.open("DownLoadFile?PID=" + PID + "&FileName=" + FileName + "&FileType=Promotion");
}

////附件
function LoadFJ() {
    jQuery("#list4").jqGrid({
        url: '../SalesManage/GetUploadFileGrid',
        datatype: 'json',
        postData: { curpage: FcunPage, rownum: 5, CID: PID },
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
        colNames: ['', '编号', '文件', '操作人', '操作时间', '操作', '操作'],
        colModel: [
            { name: 'ID', index: 'ID', width: 90, hidden: true },
        { name: 'CID', index: 'CID', width: 90 },
        { name: 'FileName', index: 'FileName', width: 90 },
        { name: 'CreateUser', index: 'CreateUser', width: 90 },
        { name: 'CreateTime', index: 'CreateTime', width: 90 },
        { name: 'IDCheck', index: 'Id', width: 50 },
        { name: 'deCheck', index: 'Id', width: 50 }
        ],
        pager: jQuery('#pager4'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        // caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list4").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list4").jqGrid('getRowData', id);
                var curChk = "<a style='color:blue;cursor:pointer' onclick=\"deleteFile('" + jQuery("#list4").jqGrid('getRowData', id).ID + "')\">删除</a>";
                var curChk1 = "<a style='color:blue;cursor:pointer' onclick=\"DownloadFile('" + jQuery("#list4").jqGrid('getRowData', id).ID + "')\">下载</a>";
                jQuery("#list4").jqGrid('setRowData', ids[i], { IDCheck: curChk, deCheck: curChk1 });
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
            if (pgButton == "next_pager4") {
                if (curPage == $("#list4").getGridParam("lastpage"))
                    return;
                curPage = $("#list4").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager4") {
                curPage = $("#list4").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager4") {
                if (curPage == 1)
                    return;
                curPage = $("#list4").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager4") {
                curPage = 1;
            }
            else {
                curPage = $("#pager4 :input").val();
            }
            reloadFJ();
        },
        loadComplete: function () {
            $("#list4").jqGrid("setGridHeight", 250, false);
            $("#list4").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}


function reloadFJ(PID) {
    $("#list4").jqGrid('setGridParam', {
        url: '../SalesManage/GetUploadFileGrid',
        datatype: 'json',
        postData: { CID: PID, curpage: FcunPage, rownum: 5 }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

function LoadFile() {
    // document.getElementById("unit").innerHTML = "";
    var InforNo = jQuery("#list").jqGrid("getRowData", oldSelID).PID;
    //var InforNo = document.getElementById("PID").value;
    $.ajax({
        url: "../SalesManage/GetUploadFile",
        type: "post",
        data: { data1: InforNo },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var id = data.id.split('@');
                var Code = data.File.split('@');
                var Name = data.Name.split('@');
                var Banding = document.getElementById("unit");
                if (Code == "") {
                    Banding.innerHTML = "";
                    return;
                }
                for (var i = 0; i < Code.length; i++) {
                    var cross = id[i] + "/" + Name[i] + "/" + Code[i];
                    Banding.innerHTML += Name[i] + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='deleteFile(\"" + cross + "\")'>删除</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='DownloadFile(\"" + id[i] + "\")'>下载</a><br/>";
                }
            }
            else {
                alert(data.Msg);
                return;
            }
        }
    });
}
function DownloadFile(id) {
    window.open("../SalesManage/DownLoad2?id=" + id);
}
function deleteFile(id) {
    var one = confirm("确实要删除这条条目吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "../SalesManage/deleteFile",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    reloadFJ();
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
    }
}

