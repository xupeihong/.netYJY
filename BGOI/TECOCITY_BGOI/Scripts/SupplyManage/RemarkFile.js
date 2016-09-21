$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        //window.parent.frames["iframeRight"].reloadPlanPro();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#UPFiles").click(function () {
        var FileName = $("#UploadFile").val();
        if (FileName.replace(/(\s*$)/g, '') != "") {
            var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            if (filetype.toLowerCase().indexOf('xlsx') < 0 && filetype.toLowerCase().indexOf('docx') < 0 && filetype.toLowerCase().indexOf('pic') < 0 && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('png') < 0 && filetype.toLowerCase().indexOf('gif') < 0) {
                alert("不支持该类型文件的上传，请上传word,pic,excel,jpg,png,gif格式文件");
                return;
            }
        }
        var one = confirm("确定要更新资质信息吗？");
        if (one) {
            document.forms[0].submit();
        } else { return; }
    })
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
    })
    $("#Ftype").change(function () {
        LoadThread();
    })
})
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