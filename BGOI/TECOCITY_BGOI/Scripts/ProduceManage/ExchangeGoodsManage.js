$(document).ready(function () {
    LoadExchangeGoods("");
    LoadExchangeDetail("");
    jq3("");

    document.getElementById('bor1').style.display = 'block';
    document.getElementById('bor2').style.display = 'none';
   
    $("#DetailXX").click(function () {
        this.className = "btnTw";
        $('#BillXX').attr("class", "btnTh");
        document.getElementById('bor1').style.display = 'block';
        document.getElementById('bor2').style.display = 'none';
    });
    $("#BillXX").click(function () {
        this.className = "btnTw";
        $('#DetailXX').attr("class", "btnTh");
        document.getElementById('bor1').style.display = 'none';
        document.getElementById('bor2').style.display = 'block';
    });

    $("#btnSearch").click(function () {
        GetSearchExchangGoods();
    })
    //退换货检验
    $("#btnCheck").click(function () {
        window.parent.parent.OpenDialog("退货检验", "../ProduceManage/ExchangeCheckGoods", 800, 450);
    });

    $("#TJShenPi").click(function () {

        var rowid = $("#list").jqGrid('getGridParam', 'selrow');
        var TID = jQuery("#list").jqGrid('getRowData', rowid).TID;
        if (rowid == null) {
            alert("请选择要审批的检验单");
            return;
        }
        else {
            $.ajax({
                async: false,
                url: "getSPPD",
                type: "post",
                single: true,
                data: { TID: TID },
                dataType: "json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        for (var i = 0; i < json.length; i++) {
                            if (json[i].name == "新建" || json[i].name == "审批未通过") {
                                var TID = jQuery("#list").jqGrid('getRowData', rowid).TID
                                var texts = TID + "@" + "退货检验单审批";
                                window.parent.OpenDialog("提交审批", "../ProduceManage/SubmitApproval?id=" + texts, 700, 500, '');
                            }
                            else {
                                alert("该检验单正在审批或审批已通过");
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
        var TID = jQuery("#list").jqGrid('getRowData', rowid).TID;
        if (rowid == null) {
            alert("请选择要进行审批的条目");
            return;
        }
        else {
            $.ajax({
                async: false,
                url: "getPDSPCK",
                type: "post",
                single: true,
                data: { TID: TID },
                dataType: "json",
                success: function (data) {
                    var json = eval(data.datas);
                    if (json.length > 0) {
                        var SPID = jQuery("#list").jqGrid('getRowData', rowid).PID;
                        var PID = jQuery("#list").jqGrid('getRowData', rowid).TID;
                        var state = jQuery("#list").jqGrid('getRowData', rowid).StateDesc;
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
                                    window.parent.OpenDialog("审批", "../ProduceManage/Approval?id=" + texts, 500, 400, '');
                                }
                                else {
                                    return;
                                }
                            }
                        });
                    }
                    else {
                        alert("该任务还未提交审批不能进行审批");
                        return;
                    }
                }
            })
        }
    })

});
var ID = 0;
var OrderID = '';
var curPage = 1;
var OnePageCount = 5;
var DcurPage = 1;
var DOnePageCount = 5;
var RcurPage = 1;
var ROnePageCount = 5;
var oldSelID = 0;
var curPage2 = 1;
var OnePageCount2 = 5;
function LoadExchangeGoods()
{
    var TID = $("#TID").val();
    var StartDate = $('#Start').val();
    var EndDate = $('#End').val();
    var State = $("input[name='State']:checked").val();

    jQuery("#list").jqGrid({
        url: 'getExchangeCheck',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, TID: TID, StartDate: StartDate, EndDate: EndDate, State: State },
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
        colNames: ['序号', '退换货编号', '退货检验编号', '检验日期', '退货方式', '检验说明','状态',''],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
        { name: 'TID', index: 'TID', width: 200 },
        { name: 'EID', index: 'EID', width: 200 },
        { name: 'ChangeDate', index: 'ChangeDate', width: 100 },
        { name: 'Brokerage', index: 'Brokerage', width: 100 },
        { name: 'CheckDescription', index: 'CheckDescription', width: 100 },
        { name: 'StateDesc', index: 'StateDesc', width: 100 },
        { name: 'PID', index: 'PID', width: 100 ,hidden:true}
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        //caption: '销售表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
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
            var dataSel = jQuery("#list").jqGrid('getGridParam');
            var ids = dataSel.selrow;
            var Model = jQuery("#list").jqGrid('getRowData', ids);
            if (ids == null) {
                return;
            }
            else {
                var TID = Model.TID;
                var pid = Model.PID;
                reload1(TID);
                reload3(pid);
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
            $("#list").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 15, false);
        }
    });
}


function reload1(TID) {
    $("#Detaillist").jqGrid('setGridParam', {
        url: 'getExchangeCheckDetail',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount,TID:TID }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

function reload() {
    var TID = $("#TID").val();
    var StartDate = $('#Start').val();
    var EndDate = $('#End').val();
    var State = $("input[name='State']:checked").val();

    $("#list").jqGrid('setGridParam', {
        url: 'getExchangeCheck',
        datatype: 'json',
        postData: { curpage: DcurPage, rownum: DOnePageCount, TID: TID, StartDate: StartDate, EndDate: EndDate, State: State }, //,jqType:JQtype

    }).trigger("reloadGrid");
}

function GetSearchExchangGoods() {
    var StartDate = $('#Start').val();
    var EndDate = $('#End').val();
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
            $("#EndDate").val("");
            return false;
        }
    }


}

function GetSearchData() {
    if ($('.field-validation-error').length == 0) {
        var TID = $("#TID").val();
        var StartDate = $('#Start').val();
        var EndDate = $('#End').val();
        var State = $("input[name='State']:checked").val();
        $("#list").jqGrid('setGridParam', {
            url: 'getExchangeCheck',
            datatype: 'json',
            postData: {
                curpage: 1, rownum: OnePageCount, TID: TID,
                StartDate: StartDate,
                EndDate: EndDate, State: State
            },
            loadonce: false

        }).trigger("reloadGrid");//重新载入
    }

}
//退货详细
function LoadExchangeDetail(TID) {
    jQuery("#Detaillist").jqGrid({
        url: 'getExchangeCheckDetail',
        datatype: 'json',
        postData: { TID: TID, curpage: DcurPage, rownum: DOnePageCount },
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
        colNames: ['序号',  '退货检验编号', '规格型号','货品编号', '包装折损情况', '外观折损情况', '零配件少损情况', '质量情况'],
        colModel: [
        { name: 'RowNumber', index: 'RowNumber', width: 50, align: "center" },
       
        { name: 'TDID', index: 'TDID', width: 200 },
        { name: 'SpecsModels', index: 'SpecsModels', width: 100 },
        { name: 'ProductID', index: 'ProductID', width: 100 },
        { name: 'PackWreck', index: 'PackWreck', width: 100 },
        { name: 'FeatureWreck', index: 'FeatureWreck', width: 100 },
        { name: 'Componments', index: 'Componments', width: 100 },
        { name: 'Quality', index: 'Quality', width: 100 }
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '退货主表',

        gridComplete: function () {
            var ids = jQuery("#Detaillist").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                id = ids[i];
                var curRowData = jQuery("#Detaillist").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#Detaillist").jqGrid('getRowData', id).PID + "' name='cb'/>";
                jQuery("#Detaillist").jqGrid('setRowData', ids[i], { IDCheck: curChk });
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
                if (DcurPage == $("#Detaillist").getGridParam("lastpage"))
                    return;
                DcurPage = $("#Detaillist").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager1") {
                DcurPage = $("#Detaillist").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager1") {
                if (DcurPage == 1)
                    return;
                DcurPage = $("#Detaillist").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager1") {
                DcurPage = 1;
            }
            else {
                DcurPage = $("#pager1 :input").val();
            }
            reload1();
        },
        loadComplete: function () {
            $("#Detaillist").jqGrid("setGridHeight", $("#pageContent").height() + 100, false);
            $("#Detaillist").jqGrid("setGridWidth", $("#bor1").width() - 15, false);
        }
    });
}

function reload3(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    $("#list2").jqGrid('setGridParam', {
        url: '../COM_Approval/ConditionGrid',
        datatype: 'json',
        postData: { curpage: curPage2, rownum: OnePageCount2, PID: jhid, webkey: webkey, folderBack: folderBack },

    }).trigger("reloadGrid");
}

function jq3(jhid) {
    //PID = $('#PID').val();
    webkey = $('#webkey').val();
    folderBack = $('#folderBack').val();
    jQuery("#list2").jqGrid({
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
            if (pgButton == "next_pager2") {
                if (curPage2 == $("#list2").getGridParam("lastpage2"))
                    return;
                curPage2 = $("#list2").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager2") {
                curPage2 = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager2") {
                if (curPage2 == 1)
                    return;
                curPage2 = $("#list2").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager2") {
                curPage2 = 1;
            }
            else {
                curPage2 = $("#pager2 :input").val();
            }
            reload3();
        },
        loadComplete: function () {
            $("#list2").jqGrid("setGridHeight", $("#pageContent").height()+100, false);
            $("#list2").jqGrid("setGridWidth", $("#bor2").width() - 30, false);
        }
    });
}


