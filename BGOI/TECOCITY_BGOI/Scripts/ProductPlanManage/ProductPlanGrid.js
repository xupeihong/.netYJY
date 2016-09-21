var curPage = 1;
var OnePageCount = 10;
var Customer;
var FollowPerson;
var oldSelID = 0;
var JHID;
var curPage1 = 1;
var curPage2 = 1;
var OnePageCount1 = 10;
var newRowID;
var curPage3 = 1;
OnePageCount2 = 4;


$(document).ready(function () {
    $("#pageContent").height($(window).height());
    $("#search").width($("#bor").width() - 30);
    //aa();
    $("#ZD").click(function () {
        window.parent.OpenDialog("制定计划单", "../ProductPlanManage/ProductPlanAdd", 1090, 540, '');
    });
    $("#Update").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        var name = Model.name;
        if (name != "新建" && name != "审批未通过") {
            alert("该计划单无法修改！");
            return;
        }
        if (ids == null) {
            alert("请选择要修改的计划单");
            return;
        } else {
            window.parent.OpenDialog("修改计划单", "../ProductPlanManage/UpdatePlan?JHID=" + Model.JHID, 1090, 540, '');
        }
    });

    $("#DaYin").click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var rID = jQuery("#list").jqGrid('getRowData', rowid).JHID;
        if (rowid == null) {
            alert("请选择要打印的计划单");
            return;
        }
        else {
            var texts = jQuery("#list").jqGrid('getRowData', rowid).JHID;
            var url = "PrintJH?Info=" + escape(texts);
            window.showModalDialog(url, window, "dialogWidth:700px;dialogHeight:700px;status:no;resizable:yes;edge:sunken;location:no;");
        }
    });

    $("#SC").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要上传图片的随工单");
            return;
        } else {
            window.parent.parent.OpenDialog("上传文件", "../ProduceManage/SCMaterialForm?OId=" + Model.JHID + "&ID=计划单", 400, 200);
        }
    })

    $("#CK").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要上传图片的随工单");
            return;
        } else {
            window.parent.parent.OpenDialog("查看文件", "../ProduceManage/CKMaterialForm?OId=" + Model.JHID, 500, 500);
        }
    })

    $("#CHEX").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要撤销的计划单");
            return;
        }
        else {
            var msg = "您真的确定要撤销吗?";
            if (confirm(msg) == true) {
                var JHID = Model.JHID;
                $.ajax({
                    type: "POST",
                    url: "CXJH",
                    data: { JHID: JHID },
                    success: function (data) {
                        alert(data.Msg);
                        getSearch();
                    },
                    dataType: 'json'
                });
                return true;
            } else {
                return false;
            }
        }
    });

    $("#sp").click(function () {
        var dataSel = jQuery("#list").jqGrid('getGridParam');
        var ids = dataSel.selrow;
        var Model = jQuery("#list").jqGrid('getRowData', ids);
        if (ids == null) {
            alert("请选择要提交审批的计划单");
            return;
        } else {
            var name = Model.name;
            if (name != "新建" && name != "审批未通过") {
                alert("该计划单已提交审批！");
                return;
            }
            var texts = Model.JHID + "@" + "生产计划审批";
            window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
        }
        //    var texts = "JH-20151030001" + "@" + "生产计划审批";
        //    window.parent.OpenDialog("提交审批", "../COM_Approval/SubmitApproval?id=" + texts, 700, 500, '');
    })

    $('#spl').click(function () {
        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        if (rowid == null) {
            alert("请选择要进行审批的条目");
            return;
        }
        else {
            var SPID = jQuery("#list").jqGrid('getRowData', rowid).SPID;
            var PID = jQuery("#list").jqGrid('getRowData', rowid).JHID;
            var state = jQuery("#list").jqGrid('getRowData', rowid).state;
            //alert(state);
            if (state == 0) {
                alert(" 该计划单未提交审批，无法进行审批！");
                return;
            }

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
    })

    document.getElementById('div1').style.display = 'block';
    //document.getElementById('RZJ').style.display = 'none';
    document.getElementById('getRZ').style.display = 'none';

    $("#DetailXX").click(function () {
        // LoadDetail(ID);
        this.className = "btnTw";
        //$('#BillXX').attr("class", "btnTh");
        //$('#QQJQdiv').attr("class", "btnTh");
        $('#RZ').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'block';
        //document.getElementById('RZJ').style.display = 'none';
        document.getElementById('getRZ').style.display = 'none';
    });

    //$("#QQJQdiv").click(function () {
    //    this.className = "btnTw";
    //    //$('#DetailXX').attr("class", "btnTh");
    //    $('#DetailXX').attr("class", "btnTh");
    //    $('#RZ').attr("class", "btnTh");
    //    document.getElementById('div1').style.display = 'none';
    //    document.getElementById('RZJ').style.display = 'block';
    //    document.getElementById('getRZ').style.display = 'none';
    //});

    $("#RZ").click(function () {
        this.className = "btnTw";
        //$('#DetailXX').attr("class", "btnTh");
        $('#DetailXX').attr("class", "btnTh");
        //$('#QQJQdiv').attr("class", "btnTh");
        document.getElementById('div1').style.display = 'none';
        //document.getElementById('RZJ').style.display = 'none';
        document.getElementById('getRZ').style.display = 'block';
    });

    //获取年份
    var mydate = new Date();
    var a = parseInt(mydate.getFullYear());
    var b = parseInt(mydate.getFullYear()) + 1;
    var c = parseInt(mydate.getFullYear()) + 2;
    //jQuery("#Type2").append("<option value=" + a + ">" + a + "</option>");
    //jQuery("#Type2").append("<option value=" + b + ">" + b + "</option>");
    //jQuery("#Type2").append("<option value=" + c + ">" + c + "</option>");

    //jQuery("#Type").append("<option value=" + a + ">" + a + "</option>");
    //jQuery("#Type").append("<option value=" + b + ">" + b  + "</option>");
    //jQuery("#Type").append("<option value=" + c + ">" + c + "</option>");
    jq();
    var JHID = "";
    jq1("");
    jq2("");
    jq3("");
})



function jq() {
    //var RWID = $('#RWID').val();
    var Name = $('#Name').val();
    var Specifications = $('#Specifications').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();
    var Type = $('#Type').val();
    var Type1 = $('#Type1').val();
    var Type2 = $('#Type2').val();
    var Type3 = $('#Type3').val();
    var State = $("#State").val();

    jQuery("#list").jqGrid({
        url: 'GetProductPlan',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, Name: Name, Specifications: Specifications, Starts: Starts, Starte: Starte, Type: Type, Type1: Type1, Type2: Type2, Type3: Type3, State: State },
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
        colNames: ['计划单号', '制定日期', '计划月份', '制定人', '说明', '状态', '审批号', ''],
        colModel: [
        { name: 'JHID', index: 'JHID', width: 150, align: "center" },
        { name: 'Specifieddate', index: 'Specifieddate', width: 140, align: "center" },
        { name: 'Plannedmonth', index: 'Plannedmonth', width: 140, align: "center" },
        { name: 'Formulation', index: 'Formulation', width: 180, align: "center" },
        { name: 'Remarks', index: 'Remarks', width: 380, align: "center" },
        { name: 'name', index: 'name', width: 80, align: "center",hidden:true },
        { name: 'SPID', index: 'SPID', width: 80, align: "center", hidden: true },
        { name: 'state', index: 'state', width: 80, align: "center", hidden: true }
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
                JHID = Model.JHID;
                var spid = Model.SPID;
                reload1();
                reload2(spid);
                reload3(JHID);

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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() - 400, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload() {
    var Name = $('#Name').val();
    var Specifications = $('#Specifications').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();
    var Type = $('#Type').val();
    var Type1 = $('#Type1').val();
    var Type2 = $('#Type2').val();
    var Type3 = $('#Type3').val();
    var State = $("#State").val();

    $("#list").jqGrid('setGridParam', {
        url: 'GetProductPlan',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, Name: Name, Specifications: Specifications, Starts: Starts, Starte: Starte, Type: Type, Type1: Type1, Type2: Type2, Type3: Type3, State: State },

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
            alert("指定日期截止日期不能小于开始日期！");
            $("#Starte").val("");
            return false;
        }


    }
}

function getSearch() {
    curRow = 0;
    curPage = 1;

    var Name = $('#Name').val();
    var Specifications = $('#Specifications').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();
    var Type = $('#Type').val();
    var Type1 = $('#Type1').val();
    var Type2 = $('#Type2').val();
    var Type3 = $('#Type3').val();
    var State = $("#State").val();

    $("#list").jqGrid('setGridParam', {
        url: 'GetProductPlan',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, Name: Name, Specifications: Specifications, Starts: Starts, Starte: Starte, Type: Type, Type1: Type1, Type2: Type2, Type3: Type3, State: State },
        loadonce: false

    }).trigger("reloadGrid");//重新载入

    reload1("");
    reload2("");
}


function jq1(JHID) {
    var Name = $('#Name').val();
    var Specifications = $('#Specifications').val();
    var Starts = $('#Starts').val();
    var Starte = $('#Starte').val();
    var Type = $('#Type').val();
    var Type1 = $('#Type1').val();
    var Type2 = $('#Type2').val();
    var Type3 = $('#Type3').val();


    jQuery("#list1").jqGrid({
        url: 'GetProductPlanGrid',
        datatype: 'json',
        postData: { curpage1: curPage1, rownum: OnePageCount1, JHID: JHID },
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
        colNames: ['序号', '产品名称', '规格型号', '库存成品', '库存半成品', '在线生产数量', '库存成品零件数(套)', '已下单尚未供货零件数(套)', '合计', '下月计划生产数', '下月需求零件数', '备注'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },

        { name: 'Name', index: 'Name', width: 140, align: "center" },
        { name: 'Specifications', index: 'Specifications', width: 140, align: "center" },
        { name: 'Finishedproduct', index: 'Finishedproduct', width: 80, align: "center" },
        { name: 'finishingproduct', index: 'finishingproduct', width: 80, align: "center" },
        { name: 'OnlineCount', index: 'OnlineCount', width: 80, align: "center" },
        { name: 'Spareparts', index: 'Spareparts', width: 80, align: "center" },
        { name: 'notavailable', index: 'notavailable', width: 80, align: "center" },
        { name: 'Total', index: 'Total', width: 80, align: "center" },
        { name: 'plannumber', index: 'plannumber', width: 80, align: "center" },
        { name: 'demandnumber', index: 'demandnumber', width: 80, align: "center" },
        { name: 'Remarks', index: 'Remarks', width: 80, align: "center" }
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
                if (curPage1 == $("#list1").getGridParam("lastpage"))
                    return;
                curPage1 = $("#list1").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage1 = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (curPage1 == 1)
                    return;
                curPage1 = $("#list1").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage1 = 1;
            }
            else {
                curPage1 = $("#pager1 :input").val();
            }
            reload1()
        },
        loadComplete: function () {
            $("#list1").jqGrid("setGridHeight", $("#pageContent").height() - 450, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function reload1() {
    //var curPage1 = 1;
    //var OnePageCount1 = 4;
    $("#list1").jqGrid('setGridParam', {
        url: 'GetProductPlanGrid',
        datatype: 'json',
        postData: { curpage1: curPage1, rownum: OnePageCount1, JHID: JHID },

    }).trigger("reloadGrid");
}



function reload2(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list2").jqGrid('setGridParam', {
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount, PID: jhid, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}

function jq2(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list2").jqGrid({
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount, PID: jhid, webkey: webkey, folderBack: folderBack },
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
            if (pgButton == "next_pager1") {
                if (curPage2 == $("#list2").getGridParam("lastpage"))
                    return;
                curPage2 = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                curPage2 = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage2 == 1)
                    return;
                curPage2 = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                curPage2 = 1;
            }
            else {
                curPage2 = $("#pager2 :input").val();
            }
            reload2();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height() - 460, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}


function reload3(JHID) {
    $("#list3").jqGrid('setGridParam', {
        url: '../ProduceManage/getRZ',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, RWID: JHID },

    }).trigger("reloadGrid");
}

function jq3(JHID) {
    jQuery("#list3").jqGrid({
        url: '../ProduceManage/getRZ',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, RWID: JHID },
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
        colNames: ['', '编号', '操作内容', '操作结果', '操作时间', '操作人'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'YYCode', index: 'YYCode', width: 200, align: "center" },
        { name: 'Content', index: 'Content', width: 200, align: "center" },
        { name: 'YYType', index: 'YYType', width: 200, align: "center" },
        { name: 'LogTime', index: 'LogTime', width: 200, align: "center", editable: false, formatter: "date", formatoptions: { srcformat: 'Y-m-d H:i:s', newformat: 'Y-m-d ' } },
        { name: 'Actor', index: 'Actor', width: 200, align: "center" }
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批情况表',

        gridComplete: function () {
            var ids = jQuery("#list3").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list3").jqGrid('getRowData', id);
            }
        },


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
            $("#list3").jqGrid("setGridHeight", $("#pageContent").height() - 470, false);
            $("#list3").jqGrid("setGridWidth", $("#bor3").width() + 900, false);
        }
    });
}