var curPage = 1;
var OnePageCount = 20;
var webkey;
var oldSelID = 0;
var newSelID = 0;
$(document).ready(function () {
    //document.getElementById("ifrBackContent").src = "../COM_Approval/SubmitApproval?id=" + escape($("#textid").val()) + "&x=" + Math.random();
    jq();
    //GetCCInfo();

    $("#CopyUnit").change(function () {
        ChangeUnitList();
    });

    $("#btnAdd").click(function () {
        var cbVal = "";
        $('input[name=cbPer]:checkbox:checked').each(function () {
            cbVal += this.value + ",";
        });
        if (cbVal == "") {
            alert("请选择抄送人员..");
            return;
        }

        if (confirm("确认添加抄送人员吗？")) {
            AddCopyHtml();
        }
    });

    $("#btnSave").click(function () {
        var perVal = "";
        $('input[name=hdPerson]').each(function () {
            perVal += this.value + ",";
        });

        var one = confirm("确认提交审批信息吗？");
        if (one == false)
            return;
        else {
            $.ajax({
                url: "SaveApprovalInfo",
                type: "post",
                data: { data1: $("#webkey").val(), data2: $("#folderBack").val(), data3: $("#RelevanceID").val(), dataCbVal: perVal },
                dataType: "Json",
                success: function (data) {
                    if (data.success == "true") {
                        alert(data.Msg);
                        window.parent.frames["iframeRight"].reload();
                        setTimeout('parent.ClosePop()', 100);
                    }
                    else {
                        alert(data.Msg);
                        return;
                    }
                }
            });
        }
    });

    $("#btnCancle").click(function () {
        window.parent.ClosePop();
    })
});

function reload() {
    webkey = $('#webkey').val();
    $("#list").jqGrid('setGridParam', {
        url: '../COM_Approval/UMwebkeyGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, webkey: webkey },

    }).trigger("reloadGrid");
}

function jq() {
    webkey = $('#webkey').val();
    jQuery("#list").jqGrid({
        url: '../COM_Approval/UMwebkeyGrid',
        datatype: 'json',
        postData: { curpage: curPage, rownum: OnePageCount, webkey: webkey },
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
        colNames: ['', '职务', '审批人员', '审批类型', '审批级别', '审批方式', '人数'],
        colModel: [
        { name: 'IDCheck', index: 'Id', width: 20, hidden: true },
        { name: 'Duty', index: 'Duty', width: 120 },
        { name: 'UserName', index: 'UserName', width: 100 },
        { name: 'BuType', index: 'BuType', width: 120 },
        { name: 'AppLevel', index: 'AppLevel', width: 100 },
        { name: 'AppTypeDesc', index: 'AppTypeDesc', width: 100 },
        { name: 'Num', index: 'Num', width: 100 },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '审批人员表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs'); //alert(ids);
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
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
            $("#list").jqGrid("setGridHeight", $("#divMainListContent").height() - 250, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function selChange(rowid) {
    if ($('input[id=c' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSelID != 0) {
            $('input[id=c' + oldSelID + ']').prop("checked", false);
        }
        $('input[id=c' + rowid + ']').prop("checked", true);
        $("#list").setSelection(rowid)
    }
}

function ChangeUnitList() {
    $("#divPerson").html('');
    var sltUnit = $("#CopyUnit").val();
    if (sltUnit == "") {
        return;
    }
    else {
        $.post("GetPersonInfo?unitId=" + sltUnit, function (data) {
            var jsonHtml = "";
            if (data != null) {
                var objTask = JSON.parse(data);
                for (var i = 0; i < objTask.length; i++) {
                    var cbVal = objTask[i]["UserId"] + "@" + objTask[i]["DeptId"] + "@" + objTask[i]["UserName"];
                    jsonHtml += "<input type='checkbox' name='cbPer' value=" + cbVal + " />" + objTask[i]["UserName"] + " </br>";
                }

                $("#divPerson").append(jsonHtml);
            }
        })
    }
}

function AddCopyHtml() {
    var cbVal = "";
   
    $('input[name=cbPer]:checkbox:checked').each(function () {
        cbVal += this.value + ",";
    });
    var PerHtml = "";
    var arrCb = new Array();
    arrCb = cbVal.split(',');
    var tbody = document.getElementById("Personlist");
    var arrCb1 = arrCb;
    var s = arrCb.length - 1;
    for (var i = 0; i < arrCb1.length - 1; i++) {
        var arrPer = arrCb[i].split('@');
        for (var j = 0; j< tbody.rows.length; j++) {
            var Productid = document.getElementsByName("hdPerson");
            str = Productid[j].value.split("-");
            //if (cbVal.contains(str[0])) {
            //    cbVal.remove(str[0]);
            //}
            //if (cbVal.contain(str[1])) {
            //    cbVal.remove(str[1]);
            //}
            if (arrPer[0] == str[0] || arrPer[1] == str[1])
            {
                arrCb.splice(i, 1);
                continue;
              //  arrCb[i].remove(str[0]);
            }
        }
    }

    //
   // Personlist
   // var tbody = document.getElementById("Personlist");
   // for (var i = 0; i < tbody.rows.length; i++) {
    //    var Productid = document.getElementsByName("hdPerson");
  //}
    //
   // $("#Personlist").html("");
        for (var i = 0; i < arrCb.length - 1; i++) {
            var arrPer = arrCb[i].split('@');
            var str = "";
            //for (var j = Productid.length-1; j > 0; j--) {
            //    str = Productid[j].value.split("-");
               
            // //   var hd = document.getElementById("hdPerson"+i).value;
          
            //    if (arrPer[1] == str[1] && arrPer[0] == str[0]) {
            //        break;  continue;
            //    }

            //}
            //if (arrPer[1] == str[1] && arrPer[0] == str[0]) {
            //    continue;
            //}
            PerHtml += "<tr><td style='text-align:center;'><input type='hidden' id='hdPerson' name='hdPerson' value=" + arrPer[0] + "-" + arrPer[1] + " />" + arrPer[2] + "</td></tr>";
        }

    //
    //for (var i = 0; i < arrCb.length - 1; i++) {
    //    var arrPer = arrCb[i].split('@');
    //    PerHtml += "<tr><td style='text-align:center;'><input type='hidden' name='hdPerson' value=" + arrPer[0] + "-" + arrPer[1] + " />" + arrPer[2] + "</td></tr>";
    //}
    $("#Personlist").append(PerHtml);
}


function GetCCInfo() {
    $("#loadlist").html('');
    $.post("GetCopyInfo", function (data) {
        var objTask = JSON.parse(data);
        var jsonHtml = " <tr style='text-align: center; background-color: #88c9e9; font: bold; margin-top: 20px; font-size: 24px;'>"
                     + "<td colspan='2'>抄送人员</td></tr>";
        for (var i = 0; i < objTask.length; i++) {
            if (i % 2 == 0) {
                var cbVal = objTask[i]["UserId"] + "@" + objTask[i]["DeptId"];
                if (i == objTask.length - 1 && objTask.length % 2 != 0) {
                    jsonHtml += "<tr><td style='width:50%;' colspan='2'><input type='checkbox' name='cb' value=" + cbVal + ">" + objTask[i]["UserName"] + "</td></tr>";
                }
                else {
                    jsonHtml += "<tr><td style='width:50%;'><input type='checkbox' name='cb' value=" + cbVal + ">" + objTask[i]["UserName"] + "</td>"
                    + "<td style='width:50%;'><input type='checkbox' name='cb' value=" + objTask[i + 1]["UserId"] + "@" + objTask[i + 1]["DeptId"] + ">" + objTask[i + 1]["UserName"] + "</td></tr>";
                }
            }
        }

        $("#loadlist").append(jsonHtml);
    });
}