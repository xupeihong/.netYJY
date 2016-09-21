
$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadPlanPro();
        // window.parent.frames["iframeRight"].reload();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#sure1").click(function () {

        if ($("#Typeo").val() == "") {
            alert("资质类型不能为空"); return;
        }
        //if ($("#Item").val() == "") {
        //    alert("资质证书具体项不能为空"); return;
        //}
        if ($("#Ffilename").val() == "") {
            alert("文档名称不能为空"); return;
        }
        if ($("#Filetype").val() == "") {
            alert("文档类型不能为空"); return;
        }

        var FileName = $("#UploadFile").val();
        if (FileName.replace(/(\s*$)/g, '') != "") {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf("xlsx") < 0 && filetype.toLowerCase().indexOf("xls") < 0 && filetype.toLowerCase().indexOf("docx") < 0 && filetype.toLowerCase().indexOf("doc") < 0 && filetype.toLowerCase().indexOf("pic") < 0 && filetype.toLowerCase().indexOf("png") < 0 && filetype.toLowerCase().indexOf("jpg") < 0 && filetype.toLowerCase().indexOf("gif") < 0) {
                alert("不支持该类型文件的上传，请上传word,excel,pic,png,jpg,gif格式文件");
                return;
            }
        }
        var res = confirm("确定要保存资质信息吗？");
        if (res) {
            document.forms[0].submit();
            window.parent.frames["iframeRight"].reloadPlanPro();
            
        }
        else {
            return;
        }
    });
   

    $("#Item").change(function () {
        var selcet = $("#Item").find("option:selected").text();
        if (selcet == "其他") {
            $("#Itemo").attr('disabled', false);
        } else {
            $("#Itemo").attr('disabled', 'disabled');
        }
    })
    $("#Typeo").change(function () {
        LoadSecond();  //一级选中加载 二级
        LoadCompare();
        var selcet = $("#Typeo").find("option:selected").text();

        if (selcet == "公司") {
            $("#Ftype").attr('disabled', 'disabled');
            $("#Item").attr('disabled', false);
        } else {
            $("#Ftype").attr('disabled', false);
            $("#Item").attr('disabled', false);
        }
    })
    $("#Ftype").change(function () {
        LoadThread();
        var selcet = $("#Ftype").find("option:selected").text();

        if (selcet == "其他") {
            $("#Item").attr('disabled', 'disabled');
            $("#Itemo").attr('disabled',false);
        } else {
            $("#Item").attr('disabled', false);
        }
    })


});

function LoadSecond() {
    $("#Ftype").empty();
    $.ajax({
        url: "GetSecond",
        type: "POST",
        data: { SelFirst: $("#Typeo").val() },
        dataType: "json",
        async: false, //是否异步
        success: function (json) {
            if (json != "") {
                var jsonData = JSON.parse(json);    //有值
                $.each(jsonData.SubType, function (key, items) {
                    $("#Ftype").append("<option value='" + items.SID + "'>" + items.text + "</option>");
                });
            }
            else {
                $("#Ftype").append("<option value=''></option>");
            }
        }
    });
}
function LoadThread() {
    $("#Item").empty();
    $.ajax({
        url: "GetThread",
        type: "POST",
        data: { SelSecond: $("#Ftype").val() },
        dataType: "json",
        async: false, //是否异步
        success: function (json) {
            if (json != "") {
                var jsonData = JSON.parse(json);    //有值
                $.each(jsonData.SubType, function (key, items) {
                    $("#Item").append("<option value='" + items.SID + "'>" + items.text + "</option>");
                });
            }
            else {
                $("#Item").append("<option value=''></option>");
            }
        }
    });
}
function LoadCompare() {
    $("#Item").empty();
    $.ajax({
        url: "GetCom",
        type: "POST",
        data: { SelComd: $("#Typeo").val() },
        dataType: "json",
        async: false, //是否异步
        success: function (json) {
            if (json != "") {
                var jsonData = JSON.parse(json);    //有值
                $.each(jsonData.SubType, function (key, items) {
                    $("#Item").append("<option value='" + items.SID + "'>" + items.text + "</option>");
                });
            }
            else {
                $("#Item").append("<option value=''></option>");
            }
        }
    });
}

