var curPage = 1;
var RcurPage = 1;
var WcurPage = 1;
var ScurPage = 1;
var OnePageCount = 5;
var oldSelID = 0;
var id = 0;
var ID = 0;
var DID = 0;
var LogID = 0;
var OID = 0;
var objData = '';
var SalesProduct = "";
var SpecsModels = "";
var ApplyMan = "";
var StartDate = "";
var EndDate = "";
var DcurPage = 1;
var PAID = 0;
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
    document.getElementById('div1').style.display = 'none';
    document.getElementById('div2').style.display = 'none';
    document.getElementById('div3').style.display = 'none';
    document.getElementById('RZDIV').style.display = 'none';
    LoadBasInfo();
    LoadDetail('');
    LoadRevoke('');
    $("#btnRecord").click(function () {
        window.parent.OpenDialog("样机申请", "../SalesRetail/ApplyProperty", 900, 500, '');
    });

    //$('#btnDetail').click(function () {
    //    var rowId = $("#list").jqGrid("getGridParam", "selrow");
    //    if (rowId == null) {
    //        alert("请选择申请记录...");
    //        return;
    //    }
    //    else {
    //        var PAID = jQuery("#list").jqGrid("getRowData", rowId).PAID;
    //        LoadDetail(PAID);
    //        //reload1(orderID);
    //        this.className = "btnTw";
    //        $("#div1").css("display", "");
    //    }
    //})
    LoadSPInfo('');
    LoadLog();
    $("#btnDetail").click(function () {
        this.className = "btnTw";
        $('#btnRevoke').attr("class", "btnTh");
        $('#btnSPInfo').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'block';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('div3').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'none';
    });
    $("#btnRevoke").click(function () {
        this.className = "btnTw";
        $('#btnDetail').attr("class", "btnTh");
        $('#btnSPInfo').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'block';
        document.getElementById('div3').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'none';
    });

    $("#btnPrint").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要打印的申请记录...");
            return;
        }
        else {
            var PAID = jQuery("#list").jqGrid("getRowData", rowId).PAID;
            window.showModalDialog("../SalesRetail/PrintPrototype?PAID=" + PAID, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    $("#btnPrintSP").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        var State = jQuery("#list").jqGrid("getRowData", rowId).State;
        if (State == "审批中") {
            alert("不能重复提交审批");
            return;
        }
        if (State == "审批完成") {
            alert("不能重复提交审批");
            return;
        }
        if (rowId == null) {
            alert("请选择要打印的申请记录...");
            return;
        }
        else {
            var PID = jQuery("#list").jqGrid("getRowData", rowId).PAID;
            window.showModalDialog("../SalesRetail/PrintSPInfo?PID=" + PID + "&TaskType=Prototype", window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    $("#btnSPInfo").click(function () {
        this.className = "btnTw";
        $('#btnDetail').attr("class", "btnTh");
        $('#btnRevoke').attr("class", "btnTh");
        $('#RZXX').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('div3').style.display = 'block';
        document.getElementById('RZDIV').style.display = 'none';
    });
    $("#RZXX").click(function () {
        this.className = "btnTw";
        $('#btnDetail').attr("class", "btnTh");
        $('#btnRevoke').attr("class", "btnTh");
        $('#btnSPInfo').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        document.getElementById('div2').style.display = 'none';
        document.getElementById('div3').style.display = 'none';
        document.getElementById('RZDIV').style.display = 'block';
    })


    $("#btnSP").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要进行审批的样机申请");
            return false;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowId).State ;
            if (State == "审批完成")
            {
                alert("审批已通过不能重复提交");
                return
            }
           
            var texts = jQuery("#list").jqGrid('getRowData', rowId).PAID + "@" + "样机审批" + "@" + "样机出撤管理";
            var PAID = jQuery("#list").jqGrid('getRowData', rowId).PAID;
            //window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
            window.parent.OpenDialog("提交审批", "../SalesRetail/ApprovalCommon?id=" + texts, 700, 500, '');
        }
    });

    $("#btnUpdate").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        if (rowId == null) {
            alert("请选择要修改的申请记录...");
            return;
        }
        else {
            var PAID = jQuery("#list").jqGrid("getRowData", rowId).PAID;
            window.parent.OpenDialog("修改样机申请记录", "../SalesRetail/UpdateProperty?PAID=" + PAID, 900, 500, '');
        }
    });

    $("#btnSearch").click(function () {
        curPage = 1;
        reload();
    });

    $("#btnCancel").click(function () {
        var rowId = $("#list").jqGrid("getGridParam", "selrow");
        var State = $("#list").jqGrid("getRowData", rowId).State;
        if (State == "审批完成") {
            alert("审批完成不能撤销");
            return;
        }
        if (rowId == null) {
            alert("请选择要撤销的申请记录...");
            return;
        }
        else {
            var PAID = jQuery("#list").jqGrid("getRowData", rowId).PAID;
            if (confirm("是否确定要撤销编号为" + PAID + "的申请记录?")) {
                $.ajax({
                    url: "DeletePrototype",
                    type: "post",
                    data: { PAID: PAID },
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
        SalesProduct = $("#SalesProduct").val();
        SpecsModels = $("#SpecsModels").val();
        ApplyMan = $("#ApplyMan").val();
        StartDate = $("#StartDate").val();
        EndDate = $("#EndDate").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetPrototypeGrid',
            datatype: 'json',
            postData: { curpage: curPage, rownum: OnePageCount, SalesProduct: SalesProduct, SpecsModels: SpecsModels, ApplyMan: ApplyMan, StartDate: StartDate, EndDate: EndDate },

        }).trigger("reloadGrid");
    }
}


function LoadBasInfo() {
    SalesProduct = $("#SalesProduct").val();
    SpecsModels = $("#SpecsModels").val();
    ApplyMan = $("#ApplyMan").val();
    StartDate = $("#StartDate").val();
    EndDate = $("#EndDate").val();
    jQuery("#list").jqGrid({
        url: 'GetPrototypeGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, SalesProduct: SalesProduct, SpecsModels: SpecsModels, ApplyMan: ApplyMan, StartDate: StartDate, EndDate: EndDate },
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
        colNames: ['申请编号', '上样产品', '撤样产品', '申请人', '申请日期', '', '商场名称', '活动说明', '状态', '所属单位'],
        colModel: [
        { name: 'PAID', index: 'PAID', width: 140 },
        { name: 'Sample', index: 'Sample', width: 200 },
        { name: 'RevokeInfo', index: 'RevokeInfo', width: 200 },
        { name: 'Applyer', index: 'Applyer', width: 150 },
        { name: 'ApplyDate', index: 'ApplyDate', width: 150 },
        { name: 'ProductID', index: 'ProductID', width: 100, hidden: true },
        { name: 'Malls', index: 'Malls', width: 150 },
        { name: 'Explain', index: 'Explain', width: 200 },
        { name: 'State', index: 'State', width: 120 },
        { name: 'UnitName', index: 'UnitName', width: 150 }
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var sample1 = "";
                var Sample = jQuery("#list").getCell(ids[i], "Sample");
                var arrSample = Sample.split(',');
                if (arrSample.length > 1) {
                    for (var j = 0; j < arrSample.length; j++) {
                        sample1 += arrSample[j] + "\n";
                    }
                    $("#list").jqGrid('setRowData', ids[i], { Sample: sample1 });
                }
                else {
                    $("#list").jqGrid('setRowData', ids[i], { Sample: Sample });
                }

                var revokeInfo1 = "";
                var RevokeInfo = jQuery("#list").getCell(ids[i], "RevokeInfo");
                var arrRevoke = RevokeInfo.split(',');
                if (arrRevoke.length > 1) {
                    for (var k = 0; k < arrRevoke.length; k++) {
                        revokeInfo1 += arrRevoke[k] + "\n";
                    }
                    $("#list").jqGrid('setRowData', ids[i], { RevokeInfo: revokeInfo1 });
                }
                else {
                    $("#list").jqGrid('setRowData', ids[i], { RevokeInfo: RevokeInfo });
                }

                var applyDate = jQuery("#list").getCell(ids[i], "ApplyDate");
                if (applyDate.indexOf("1900") > -1) {
                    applyDate = "";
                    $("#list").jqGrid('setRowData', ids[i], { ApplyDate: applyDate });
                }

            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            PAID = jQuery("#list").jqGrid("getRowData", rowid).PAID;
            //LogID = PAID;
            ////LoadDetail(PAID);
            ////LoadRevoke(PAID);
            //reload1(PAID, "0");
            //reload2(PAID, "1");
            //reload3(PAID);
            //reload7()
            select(rowid);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 146, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function LoadDetail(PAID) {
    var op = "0";

    $("#Detaillist").jqGrid('GridUnload');
    document.getElementById('div1').style.display = 'block';
    jQuery("#Detaillist").jqGrid({
        url: 'GetProtoDetailGrid',
        datatype: 'json',
        ansyc:false,
        postData: { PAID: PAID, Op: op, curpage: DcurPage, rownum: OnePageCount },
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
        colNames: ['', '', '上样物品', '规格型号', '产品大类', '生产厂家', '数量', '单价', '合计', '业务类型'],
        colModel: [
        { name: 'PAID', index: 'PAID', width: 90, hidden: true },
        { name: 'DID', index: 'DID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 150 },
        { name: 'SpecsModels', index: 'SpecsModels', width: 120 },
        { name: 'Ptype', index: 'Ptype', width: 120 },
        { name: 'Supplier', index: 'Supplier', width: 120 },
        { name: 'Amount', index: 'Amount', width: 80 },
        { name: 'UnitPrice', index: 'UnitPrice', width: 80 },
        { name: 'Total', index: 'Total', width: 80 },
        { name: 'BusinessType', index: 'BusinessType', width: 80 }
        ],
        pager: jQuery('#pager8'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        gridComplete: function () {
            var ids = jQuery("#Detaillist").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var ID = "" + id + "";
                $("#Detaillist").jqGrid('setRowData', ids[i], { ID: ID });
            }
        },
        onSelectRow: function (rowid, status) {

        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager8") {
                if (DcurPage == $("#Detaillist").getGridParam("lastpage"))
                    return;
                DcurPage = $("#Detaillist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager8") {
                DcurPage = $("#Detaillist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager8") {
                if (DcurPage == 1)
                    return;
                DcurPage = $("#Detaillist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager8") {
                DcurPage = 1;
            }
            else {
                DcurPage = $("#pager8:input").val();
            }
            reload1(PAID, op);
        },
        loadComplete: function () {
            $("#Detaillist").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#Detaillist").jqGrid("setGridWidth", $("#bor1").width() - 15, false);
        }
    });
}

function LoadRevoke(PAID) {
    var op = "1";
    $("#list2").jqGrid('GridUnload');
    jQuery("#list2").jqGrid({
        url: 'GetProtoDetailGrid',
        datatype: 'json',
        postData: { PAID: PAID, Op: op, curpage: WcurPage, rownum: OnePageCount },
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
        colNames: ['', '', '撤样物品', '规格型号', '产品大类', '生产厂家', '数量', '单价', '合计', '业务类型'],
        colModel: [
        { name: 'PAID', index: 'PAID', width: 90, hidden: true },
        { name: 'DID', index: 'DID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 150 },
        { name: 'SpecsModels', index: 'SpecsModels', width: 120 },
        { name: 'Ptype', index: 'Ptype', width: 120 },
        { name: 'Supplier', index: 'Supplier', width: 120 },
        { name: 'Amount', index: 'Amount', width: 80 },
        { name: 'UnitPrice', index: 'UnitPrice', width: 80 },
        { name: 'Total', index: 'Total', width: 80 },
        { name: 'BusinessType', index: 'BusinessType', width: 80 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        gridComplete: function () {
            var ids = jQuery("#list2").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var ID = "" + id + "";
                $("#list2").jqGrid('setRowData', ids[i], { ID: ID });
            }
        },
        onSelectRow: function (rowid, status) {

        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager2") {
                if (WcurPage == $("#list2").getGridParam("lastpage"))
                    return;
                WcurPage = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                WcurPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (WcurPage == 1)
                    return;
                WcurPage = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                WcurPage = 1;
            }
            else {
                WcurPage = $("#pager2 :input").val();
            }
            reload2(PAID, op);
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list2").jqGrid("setGridWidth", $("#bor2").width() - 15, false);
        }
    });
}

function select(rowid) {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    PAID = jQuery("#list").jqGrid('getRowData', rowid).PAID;
 //   PID = jQuery("#list").jqGrid('getRowData', rowid).offerPID;
 //   reload1(PAID, Op);
   
    reload1(PAID, "0");
    reload2(PAID, "1");
    reload3(PAID);
    reload7()

}

function reload1(PAID, Op) {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    PAID = jQuery("#list").jqGrid('getRowData', rowid).PAID;
    $("#Detaillist").jqGrid('setGridParam', {
        url: 'GetProtoDetailGrid',
        datatype: 'json',
     
        postData: { PAID: PAID, Op: Op, curpage: DcurPage, rownum: OnePageCount },
        loadComplete: function () {
            $("#Detaillist").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#Detaillist").jqGrid("setGridWidth", $("#bor1").width() - 15, false);
        }
    }).trigger("reloadGrid");
}

function reload2(PAID, Op) {
    var rowid = $("#list").jqGrid('getGridParam', 'selrow');
    PAID = jQuery("#list").jqGrid('getRowData', rowid).PAID;
    $("#list2").jqGrid('setGridParam', {
        url: 'GetProtoDetailGrid',
        datatype: 'json',
        postData: { PAID: PAID, Op: Op, curpage: WcurPage, rownum: OnePageCount },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list2").jqGrid("setGridWidth", $("#bor2").width() - 15, false);
        }
    }).trigger("reloadGrid");
}

function LoadSPInfo(PID) {
    var webkey = $('#webkey').val();
    var folderBack = $('#folderBack').val();
    jQuery("#list3").jqGrid({
        url: 'ConditionGrid',
        datatype: 'json',
        postData: { curpage: ScurPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },
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
        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (ScurPage == $("#list3").getGridParam("lastpage"))
                    return;
                ScurPage = $("#list3").getGridParam("pager3") + 1;
            }
            else if (pgButton == "last_pager1") {
                ScurPage = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (ScurPage == 1)
                    return;
                ScurPage = $("#list3").getGridParam("pager3") - 1;
            }
            else if (pgButton == "first_pager1") {
                ScurPage = 1;
            }
            else {
                ScurPage = $("#pager3 :input").val();
            }
            reload3(PID);
        },
        loadComplete: function () {
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 160, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 60, false);
        }
    });
}

function reload3(PID) {
    var webkey = $('#webkey').val();
    var folderBack = $('#folderBack').val();
    $("#list3").jqGrid('setGridParam', {
        url: 'ConditionGrid',
        datatype: 'json',
        postData: { curpage: ScurPage, rownum: OnePageCount, PID: PID, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}



function LoadLog() {
    jQuery("#RZlist").jqGrid({
        url: 'GetLogGrid',
        datatype: 'json',
        postData: { curpage: RcurPage, rownum: OnePageCount, ID: LogID },
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
        colNames: ['', '日志名称', '日志类型', '操作人', '操作单位', '操作时间'],
        colModel: [
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'LogContent', index: 'LogContent', width: 90 },
        { name: 'ProductType', index: 'ProductType', width: 90 },
        { name: 'Actor', index: 'Actor', width: 90 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'LogTime', index: 'LogTime', width: 100 }
        ],
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#FJXXlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                //var curRowData = jQuery("#FJXXlist").jqGrid('getRowData', id);
                //var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#FJXXlist").jqGrid('getRowData', id).PID + "' name='cb'/>";
                //jQuery("#FJXXlist").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            //ID = jQuery("#list").jqGrid('getRowData', rowid).EID//0812k
            //OrderID = jQuery("#list").jqGrid('getRowData', rowid).OrderID
            //select(rowid);
            //$("#Billlist tbody").html("");
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager6") {
                if (RcurPage == $("#RZlist").getGridParam("lastpage"))
                    return;
                RcurPage = $("#RZlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager6") {
                RcurPage = $("#RZlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager6") {
                if (RcurPage == 1)
                    return;
                RcurPage = $("#RZlist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager6") {
                RcurPage = 1;
            }
            else {
                RcurPage = $("#pager6 :input").val();
            }
            reload7();
        },
        loadComplete: function () {
            $("#RZlist").jqGrid("setGridHeight", $("#pageContent").height() + 80, false);
            $("#RZlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function reload7() {
    $("#RZlist").jqGrid('setGridParam', {
        url: 'GetLogGrid',
        datatype: 'json',
        postData: {
            curpage: RcurPage, rownum: OnePageCount, ID: LogID
        },
        //loadonce: false

    }).trigger("reloadGrid");//重新载入
}
