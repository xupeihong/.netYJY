$(document).ready(function () {
    //ShowDiv();
    LoadBasInfo();
    LoadDetail();
    LoadProjectInfosBill();
    LoadLog();
    document.getElementById('WPXX').style.display = 'block';
    document.getElementById('XGSJ').style.display = 'none';
    document.getElementById('RZDiv').style.display = 'none';
    $("#btnRecord").click(function () {
        window.parent.OpenDialog("新增备案", "../SalesManage/RecordInfo", 1000, 550, '');
    });
    $("#btnRecordF").click(function () {
        window.parent.OpenDialog("新增非标备案", "../SalesManage/RecordInfoF", 1000, 550, '');
    });


    $("#btnPrice").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要报价的备案信息");
            return;
        }
          var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        if (ISF == "1") {
            alert("不能添加常规报价");
            return;
        }
        //var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var PPstate = jQuery("#list").jqGrid('getRowData', rowid).PPstate;
        if (PPstate == 2) {
            alert("不能重复报价");
            return;
        }
        window.parent.OpenDialog("新增报价单", "../SalesManage/AddOffer?BJID=" + ID, 1000, 550, '');
    });
    $("#btnPriceF").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要报价的备案信息");
            return;
        }
        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        if (ISF != "1") {
            alert("不能添加常规订单");
            return;
        }
        // var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var PPstate = jQuery("#list").jqGrid('getRowData', rowid).PPstate;
        if (PPstate == 2) {
            alert("不能重复报价");
            return;
        }
        window.parent.OpenDialog("新增非标报价单", "../SalesManage/AddOfferF?BJID=" + ID, 1000, 550, '');
    });

    $("#btnOrder").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("选择要生成订单的备案");
            return;
        }
        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        if (ISF == "1") {
            alert("不能添加常规订单");
            return;
        }
        window.parent.OpenDialog("新增订单", "../SalesManage/AddOrder?ID=" + ID, 1000, 550, '');
        //reload();
    });
    $("#btnOrderF").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("选择要生成订单的备案");
            return;
        }
        // var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var ISF = jQuery("#list").jqGrid('getRowData', rowid).ISF;
        if (ISF != "1") {
            alert("不能添加非常规订单");
            return;
        }
        window.parent.OpenDialog("新增非常规订单", "../SalesManage/AddOrderF?ID=" + ID, 1000, 550, '');
        reload();
    });
    $("#btnDetail").click(function () {
        this.className = "btnTw";
        $('#btnBill').attr("class", "btnTh");
        document.getElementById('WPXX').style.display = 'block';
        document.getElementById('XGSJ').style.display = 'none';
    });
    $("#btnBill").click(function () {
        this.className = "btnTw";
        $('#btnDetail').attr("class", "btnTh");
        document.getElementById('WPXX').style.display = 'none';
        document.getElementById('XGSJ').style.display = 'block';
    });

    $("#btnSP").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择新增订单的备案项目");
            return;
        }
        else {
            var texts = ID + "@" + "备案审批";
            window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
        }
    });
    $("#btnContract").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行签订合同的项目单");
            return;
        }
        else {
            var State = jQuery("#list").jqGrid('getRowData', rowid).PPstate;
            if (State <= "1") {
                alert("该项目还没有报价，不能进行签订合同操作");
                return;
            }
            var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
            window.parent.OpenDialog("签订合同", "../SalesManage/AddContract?id=" + texts, 800, 600, '');
        }

    })

    //$("#btnExport").click(function () {
    //    var ProjectName = $('#PlanName').val();
    //    var PlanID = $('#PlanID').val();
    //    // var RecordContent = $('#RecordContent').val();
    //    var MainContent = $("#MainContent").val();
    //    var SpecsModels = $('#SpecsModels').val(); //$('#SpecsModels').click.Text();
    //    var BelongArea = $('#BelongArea').val();
    //    var StartDate = $('#StartDate').val();
    //    var EndDate = $('#EndDate').val();
    //    var Manager = $('#Manager').val();
    //    var State = $("input[name='State']:checked").val();
    //    var HState = $("input[name='HState']:checked").val();
    //    $.ajax({
    //        url: "RecordToExcel",
    //        type: "post",
    //        data: { curPage: curPage, ProjectName: ProjectName, PlanID: PlanID, MainContent: MainContent, SpecsModels: SpecsModels, BelongArea: BelongArea, StartDate: StartDate, EndDate: EndDate, Manager: Manager, State: State, HState: HState },
    //        dataType: "json",
    //        async: false,
    //        success: function (data)
    //        {
    //        }
    //    });
    //})

    $("#PrintProject").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要打印的备案");
            return;
        }
        window.showModalDialog("../SalesManage/PrintProjectBasInfo?PID=" + ID, window, "dialogWidth:800px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
    })

    $("#UploadContract").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("您还没选择要上传合同的备案");
            return;
        }
        var texts = jQuery("#list").jqGrid('getRowData', rowid).PID;
        window.parent.OpenDialog("上传合同窗口", "../SalesManage/AddFile?id=" + texts, 500, 300, '');
    })

    $("#btnSearch").click(function () {
        SearchOut();
    })
});
function ShowDetail() {
    document.getElementById('div1').style.display = 'block';
    document.getElementById('div2').style.display = 'none';
}

function SetStyle(op) {
    if (op == "TW") {
        //$('#btnBill').css("background-color", "#ededed");
        //$('#btnDetail').css("background-color", "white");
        $('#btnDetail').attr("class", "btnTw");
        $('#btnBill').attr("class", "btnTh");
        $('#btnRZ').attr("class", "btnTh");
        document.getElementById('WPXX').style.display = 'block';
        document.getElementById('XGSJ').style.display = 'none';
        document.getElementById('RZDiv').style.display = 'none';
    }
    else if (op == "TH") {
        //$('#btnBill').css("background-color", "white");
        //$('#btnDetail').css("background-color", "#ededed");
        $('#btnBill').attr("class", "btnTw");
        $('#btnDetail').attr("class", "btnTh");
        $('#btnRZ').attr("class", "btnTh");
        document.getElementById('WPXX').style.display = 'none';
        document.getElementById('XGSJ').style.display = 'block';
        document.getElementById('RZDiv').style.display = 'none';
    }
    else if (op == "RZ") {
        $('#btnRZ').attr("class", "btnTw");
        $('#btnBill').attr("class", "btnTh");
        $('#btnDetail').attr("class", "btnTh");
        document.getElementById('RZDiv').style.display = 'block';
        document.getElementById('WPXX').style.display = 'none';
        document.getElementById('XGSJ').style.display = 'none';

    }
}

var curPage = 1;
var Dcurpage = 1;
var OnePageCount = 5;
var DOnePageCount = 5;
var RcurPage = 1;
var ROnePageCount = 5;
var oldSelID = 0;
var id = 0;
var ID = '';
var DID = 0;
var OID = 0;

function reload() {
    var ProjectName = $('#PlanName').val();
    var PlanID = $('#PlanID').val();
    // var RecordContent = $('#RecordContent').val();
    var MainContent = $("#MainContent").val();
    var SpecsModels = $('#SpecsModels').val(); //$('#SpecsModels').click.Text();
    var BelongArea = $('#BelongArea').val();
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    var Manager = $('#Manager').val();
    var State = $("input[name='State']:checked").val();
    var HState = $("input[name='HState']:checked").val();
    $("#list").jqGrid('setGridParam', {
        url: 'GetSearchData', //'GetSalesGrid',
        datatype: 'json',
        postData: {
            curpage: curPage, rownum: OnePageCount, PlanName: ProjectName, PlanID: PlanID,
            MainContent: MainContent, SpecsModels: SpecsModels, BelongArea: BelongArea, StartDate: StartDate,
            EndDate: EndDate, Manager: Manager, State: State, HState: HState
        },

    }).trigger("reloadGrid");
}


function LoadBasInfo() {
    jQuery("#list").jqGrid({
        url: 'GetSalesGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount },
        loadonce: false,
        mtype: 'POST',
        ansyc: true,
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
        colNames: ['', '备案日期', '项目名称', '工程编号', '产品名称', '规格型号', '业务负责人', '所属部门', '所属区域', '渠道来源', '进度', '备案人', '', '', ''],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'RecordDate', index: 'RecordDate', width: 100, formatter: "date", formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' } },
        { name: 'PlanName', index: 'PlanName', width: 80 },
        { name: 'PlanID', index: 'PlanID', width: 80 },
        { name: 'OrderContent', index: 'OrderContent', width: 150 },
        { name: 'Specifications', index: 'Specifications', width: 150 },
        { name: 'WorkChief', index: 'WorkChief', width: 70 },
        { name: 'UNitName', index: 'UNitName', width: 70 },
        { name: 'BelongArea', index: 'BelongArea', width: 50 },
        { name: 'ChannelsFrom', index: 'ChannelsFrom', width: 100 },
        { name: 'Pstate', index: 'Pstate', width: 100 },
        { name: 'Manager', index: 'Manager', width: 100 },
         { name: 'PPstate', index: 'PPstate', width: 100, hidden: true },
        { name: 'State', index: 'State', width: 100, hidden: true },
         { name: 'ISF', index: 'ISF', width: 100, hidden: true },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        //gridComplete: function () {
        //    //var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
        //    //for (var i = 0; i < ids.length; i++) {
        //    //    id = ids[i];
        //    //    var curRowData = jQuery("#list").jqGrid('getRowData', id);
        //    //    var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
        //    //    jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    //}//1110k



        //},

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var order1 = "";
                var order2 = "";
                var OrderContent = jQuery("#list").getCell(ids[i], "OrderContent");
                var Specification = jQuery("#list").getCell(ids[i], "Specifications");
                //var ProductID = jQuery("#list").getCell(ids[i], "ProductID");
                var arrContent = OrderContent.split(',');
                var arrSpecification = Specification.split(',');
                //var arrProductID = ProductID.split(',');
                if (arrContent.length > 1) {
                    for (var j = 0; j < arrContent.length; j++) {
                        order1 += arrContent[j] + "\n";
                    }
                    $("#list").jqGrid('setRowData', ids[i], { OrderContent: order1 });
                }
                else {
                    $("#list").jqGrid('setRowData', ids[i], { OrderContent: OrderContent });
                }
                if (arrSpecification.length > 1) {
                    for (var j = 0; j < arrSpecification.length; j++) {
                        order2 += arrSpecification[j] + "\n";
                    }
                    $("#list").jqGrid('setRowData', ids[i], { Specifications: order2 });
                } else {
                    $("#list").jqGrid('setRowData', ids[i], { Specifications: Specification });
                }
                //if (arrProductID.length > 1) {
                //    for (var j = 0; j < arrProductID.length; j++) {
                //        order3 += arrProductID[j] + "\n";
                //    }
                //    $("#list").jqGrid('setRowData', ids[i], { ProductID: order3 });
                //} else {
                //    $("#list").jqGrid('setRowData', ids[i], { ProductID: ProductID });
                //}
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);

            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            ID = jQuery("#list").jqGrid('getRowData', rowid).PID//0812k
            // reload1(DID);
            select(rowid);
            //  $("#Billlist tbody").html("");
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 200, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}

function select(rowid) {
    ID = jQuery("#list").jqGrid('getRowData', rowid).PID;
    Dcurpage = 1;
    reload1();
    LoadProjectInfosBill();
    reload3();
}
function reload12() {
    $("#Detaillist").jqGrid('setGridParam', {
        url: 'GetDetailGrid',
        datatype: 'json',
        loadonce: true,
        mtype: 'POST',
        postData: { ID: ID, curpage: Dcurpage, rownum: DOnePageCount }, //,jqType:JQtype
    }).trigger("reloadGrid");
}
//加载物品详细List
function LoadDetailaa() {
    jQuery("#Detaillist").jqGrid({
        url: 'GetDetailGrid',
        datatype: 'json',
        postData: { ID: ID, curpage: Dcurpage, rownum: DOnePageCount },
        loadonce: true,
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
        colNames: ['', '', '', '物品名称', '规格型号', '单位', '数量'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'XID', index: 'XID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'Amount', index: 'Amount', width: 80 }

        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: DOnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#Detaillist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#Detaillist").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            DID = jQuery("#loadlist").jqGrid('getRowData', rowid).PID;
            // LoadOrdersInfo();

        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (Dcurpage == $("#Detaillist").getGridParam("lastpager1"))
                    return;
                Dcurpage = $("#Detaillist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                Dcurpage = $("#Detaillist").getGridParam("lastpager1");
            }
            else if (pgButton == "prev_pager1") {
                if (Dcurpage == 1)
                    return;
                Dcurpage = $("#Detaillist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                Dcurpage = 1;
            }
            else {
                Dcurpage = $("#pager1 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#Detaillist").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#Detaillist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}
function LoadDetail() {

    jQuery("#Detaillist").jqGrid({
        url: 'GetDetailGrid',
        datatype: 'json',
        postData: { ID: ID, curpage: Dcurpage, rownum: DOnePageCount },
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
        colNames: ['', '', '', '物品名称', '规格型号', '单位', '数量'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'XID', index: 'XID', width: 90, hidden: true },
        { name: 'OrderContent', index: 'OrderContent', width: 90 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Unit', index: 'Unit', width: 80 },
        { name: 'Amount', index: 'Amount', width: 80 }

        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager1") {
                if (Dcurpage == $("#Detaillist").getGridParam("lastpage"))
                    return;
                Dcurpage = $("#Detaillist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                Dcurpage = $("#Detaillist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (Dcurpage == 1)
                    return;
                Dcurpage = $("#Detaillist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                Dcurpage = 1;
            }
            else {
                Dcurpage = $("#pager1 :input").val();
            }
            reload1()
        },
        loadComplete: function () {
            $("#Detaillist").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#Detaillist").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload1() {
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#Detaillist").jqGrid('setGridParam', {
        url: 'GetDetailGrid',
        datatype: 'json',
        postData: { ID: ID, curpage: Dcurpage, rownum: DOnePageCount },

    }).trigger("reloadGrid");
}

//查询
function SearchOut() {
    var StartDate = $('#StartDate').val();
    var EndDate = $('#EndDate').val();
    if (StartDate == "" && EndDate == "") {
        GetSearchData();
    }
    else {
        var strSeparator = "-"; //日期分隔符
        var strDateArrayStart;
        var strDateArrayEnd;
        var intDay;
        strDateArrayStart = StartDate.split(strSeparator);
        strDateArrayEnd = EndDate.split(strSeparator);
        var strDateS = new Date(strDateArrayStart[0] + "/" + strDateArrayStart[1] + "/" + strDateArrayStart[2]);
        var strDateE = new Date(strDateArrayEnd[0] + "/" + strDateArrayEnd[1] + "/" + strDateArrayEnd[2]);
        if (strDateS <= strDateE) {
            GetSearchData();
        }
        else {
            alert("截止日期不能小于开始日期！");
            $("#End").val("");
            return false;
        }
    }


}
function GetSearchData() {
    if ($('.field-validation-error').length == 0) {
        var ProjectName = $('#PlanName').val();
        var PlanID = $('#PlanID').val();
        // var RecordContent = $('#RecordContent').val();
        var MainContent = $("#MainContent").val();
        var SpecsModels = $('#SpecsModels').val(); //$('#SpecsModels').click.Text();
        var BelongArea = $('#BelongArea').val();
        var StartDate = $('#StartDate').val();
        var EndDate = $('#EndDate').val();
        var Manager = $('#Manager').val();
        var State = $("input[name='State']:checked").val();
        var HState = $("input[name='HState']:checked").val();
        $("#list").jqGrid('setGridParam', {
            url: 'GetSearchData',
            datatype: 'json',
            postData: {
                curpage: 1, rownum: OnePageCount, PlanName: ProjectName, PlanID: PlanID,
                MainContent: MainContent, SpecsModels: SpecsModels, BelongArea: BelongArea, StartDate: StartDate,
                EndDate: EndDate, Manager: Manager, State: State, HState: HState
            },
            loadonce: false

        }).trigger("reloadGrid");//重新载入

    }
}
//加载相关单据List
function LoadBill() {
    var PID = ID;
    //  $('#loadlist').hide();
    document.getElementById('div1').style.display = 'none';
    document.getElementById('div2').style.display = 'block';
    // $("#loadlist").attr("style", "display:none;");
    jQuery("#Orderlist").jqGrid({
        url: 'GetOrderInfoGrid',
        datatype: 'json',
        postData: { PID: PID, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['', '', '', '订单所属单位部门', '签订日期', '订货单位', '订货单位联系人', '订货单位联系电话', '订货单位地址', ],
        colModel: [
        //{ name: 'IDCheck', index: 'Id', width: 20 },
        { name: 'PID', index: 'PID', width: 90, hidden: true },
        { name: 'OrderID', index: 'OrderID', width: 90, hidden: true },
        { name: 'ContractID', index: 'ContractID', width: 90, hidden: true },

        { name: 'UnitID', index: 'UnitID', width: 100 },
        { name: 'ContractDate', index: 'ContractDate', width: 80 },
        { name: 'OrderUnit', index: 'OrderUnit', width: 80 },
        { name: 'OrderContactor', index: 'OrderContactor', width: 80 },//
        { name: 'OrderTel', index: 'OrderTel', width: 80 },// OrderAddress
        { name: 'OrderAddress', index: 'OrderAddress', width: 80 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        //gridComplete: function () {
        //    var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
        //    for (var i = 0; i < ids.length; i++) {
        //        var id = ids[i];
        //        var curRowData = jQuery("#list").jqGrid('getRowData', id);
        //        var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).PID + "' name='cb'/>";
        //        jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
        //    }

        //},
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
            OID = jQuery("#Orderlist").jqGrid('getRowData', rowid).PID;
            LoadOrdersInfo();
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#loadlist").getGridParam("lastpage"))
                    return;
                curPage = $("#loadlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#loadlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#loadlist").getGridParam("page") - 1;
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
            $("#loadlist").jqGrid("setGridHeight", $("#pageContent").height() + 10, false);
            $("#loadlist").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });

}
//相关单据详细
function LoadProjectInfosBill() {
    rowCount = document.getElementById("ProjectInfoBill").rows.length;
    var CountRows = parseInt(rowCount) + 1;
    $.ajax({
        url: "ProjectBasInfoRelBill",
        type: "post",
        data: { ID: ID },
        dataType: "json",
        success: function (data) {
              $("#ProjectInfoBill").html("");
            var json = eval(data.datas);
            if (json.length > 0) {
                $("#ProjectInfoBill").html("");
                //  $("#Billlist th").html("");
                var html0 = '<tr><th style="width: 5%;" class="th">描述</th><th style="width: 5%;" class="th">编号</th><th style="width: 5%;" class="th">操作</th></tr>'
                // $("#ProjectInfoBill").append(html0);
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    //html += '<tr><th style="width: 5%;" class="th">描述</th><th style="width: 5%;" class="th">编号</th><th style="width: 5%;" class="th">操作</th></tr>'
                    html += '<tr>'
                    var s = json[i].ID.substr(0, 2);
                    if (s == "BA") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">备案基本信息</lable> </td>';
                    }
                    if (s == "BJ") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">报价信息</lable> </td>';
                    }
                    if (s == "DH") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">订货信息</lable> </td>';
                    }
                    if (s == "FH") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">发货信息</lable> </td>';
                    }
                    if (s == "HK") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">回款信息</lable> </td>';
                    }
                    if (s == "TH") {
                        html += '<td ><lable class="labMSID' + rowCount + ' " id="MSID' + rowCount + '">退换信息</lable> </td>';
                    }


                    html += '<td ><lable class="labID' + rowCount + ' " id="ID' + rowCount + '">' + json[i].ID + '</lable> </td>';

                    html += '<td ><a href="#" style="color:blue" onclick=GetXX("' + json[i].ID + '")>详情</a></td>';
                    html += '</tr>'
                    $("#ProjectInfoBill").append(html);
                }


            }
        }
    })
}
function GetXX(SDI) {
    var id = SDI;
    var s = id.substr(0, 2);
    if (s == "BA") { window.parent.parent.OpenDialog("详细", "../SalesManage/ProjectBill?ID=" + id, 800, 450); }
    else if (s == "BJ") { window.parent.parent.OpenDialog("详细", "../SalesManage/OfferBill?ID=" + id,800, 450); }
    else if (s == "DH") { window.parent.parent.OpenDialog("详细", "../SalesManage/OrdersInfoBill?ID=" + id, 800, 450); }
    else if (s == "FH") { window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 800, 450); }
    else if (s == "HK") { window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 800, 450); }
    else if (s == "TH") { window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 800, 450); }
    //window.parent.parent.OpenDialog("详细", "../SalesManage/LoadReceivePaymentXX?ID=" + id, 500, 450);
}

//根据报价单BJID获取报价单详细新信息
function GetBJData(id) {
    var Xid = id;
    jQuery('#BJlist').jqGrid({
        url: 'GetBJXXGrid',
        datatype: 'json',
        postData: { Xid: Xid, curpage: curPage, rownum: OnePageCount },
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
        colNames: ['报价单号', '细项编号', '物料编码', '产品名称', '规格型号', '生成厂家', '单位', '进货数量', '含税定价', '含税金额', '时间', '报价人'],
        colModel: [
        { name: 'BJID', index: 'BJID', width: 150 },
        { name: 'XID', index: 'XID', width: 100 },
        { name: 'ProductID', index: 'ProductID', width: 100 },
        { name: 'OrderContent', index: 'OrderContent', width: 100 },
        { name: 'Specifications', index: 'Specifications', width: 100 },
        { name: 'Supplier', index: 'Supplier', width: 100 },
        { name: 'Unit', index: 'Unit', width: 100 },
        { name: 'Amount', index: 'Amount', width: 100 },
        { name: 'UnitPrice', index: 'UnitPrice', width: 100 },
        { name: 'Total', index: 'Total', width: 100 },
        { name: 'CreateTime', index: 'CreateTime', width: 100 },
        { name: 'CreateUser', index: 'CreateUser', width: 100 }
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '报价详细数据',
        gridComplete: function () {
            var ids = jQuery("#BJlist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var Model = jQuery("#BJlist").jqGrid('getRowData', id);
                Up_Down = "<a href='#' style='color:blue' onclick='DownloadFile(" + id + ")'  >详情</a>";
                jQuery("#BJlist").jqGrid('setRowData', ids[i], { DID: Up_Down });

            }
        },
        //onSelectRow: function (rowid, status) {
        //    if (oldSelID != 0) {
        //        $('input[id=c' + oldSelID + ']').prop("checked", false);
        //    }
        //    $('input[id=c' + rowid + ']').prop("checked", true);
        //    oldSelID = rowid;
        //    //DID = jQuery("#loadlist").jqGrid('getRowData', rowid).PID;
        //},

        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#loadlist").getGridParam("lastpage"))
                    return;
                curPage = $("#loadlist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#loadlist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#loadlist").getGridParam("page") - 1;
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
            $("#loadlist").jqGrid("setGridHeight", $("#pageContent").height() + 10, false);
            $("#loadlist").jqGrid("setGridWidth", $("#div1").width() + 250, false);
        }
    });

}



function reload2() {
    $("#Orderlist").jqGrid('setGridParam', {
        url: 'ProjectBasInfoRelBill',
        datatype: 'json',
        postData: { ID: ID, curpage: curPage, rownum: OnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}



function reload3() {
    $("#LogList").jqGrid('setGridParam', {
        url: 'GetLogGrid',
        datatype: 'json',
        postData: { ID: ID, curpage: RcurPage, rownum: ROnePageCount }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

//操作日志

function LoadLog() {
    jQuery("#LogList").jqGrid({
        url: 'GetLogGrid',
        datatype: 'json',
        postData: { ID: ID, curpage: RcurPage, rownum: ROnePageCount },
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
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '',

        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }

        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
        },

        onPaging: function (pgButton) {
            if (pgButton == "next_pager3") {
                if (RcurPage == $("#LogList").getGridParam("lastpage"))
                    return;
                RcurPage = $("#LogList").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager3") {
                RcurPage = $("#LogList").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager3") {
                if (RcurPage == 1)
                    return;
                RcurPage = $("#LogList").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager3") {
                RcurPage = 1;
            }
            else {
                RcurPage = $("#pager3 :input").val();
            }
            reload3();
        },
        loadComplete: function () {
            $("#LogList").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#LogList").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
