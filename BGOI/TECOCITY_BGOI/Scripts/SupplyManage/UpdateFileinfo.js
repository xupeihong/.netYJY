$(document).ready(function () {
    loadFile();
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadPlanPro();
        window.parent.ClosePop();
    }
    $("#hole").height($(window).height());
    $("#UPFiles").click(function () {

        if ($("#Typeo").val() == "") {
            alert("资质类型不能为空"); return;
        }
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
        var selcet = $("#Typeo").find("option:selected").text();
        if (selcet == "公司") {
            $("#Ftype").attr('disabled', 'disabled');
        } else {
            $("#Ftype").attr('disabled', false);
        }
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
function loadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("Sid").value;
   // var timeout = document.getElementById("FTimeOut").value;//
    var fid = $("#FID").val();//唯一编号
    var filename = $("#Ffilename").val();//资质文件名称
   
    $.ajax({
        url: "GetFile",
        type: "post",
        data: { data1: InforNo, timeOut: fid,filename:filename },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var id = data.id.split('@');
                var Code = data.File.split('@');
                var Name = data.Name.split('@');
                var Type = data.Type.split('@');
                var Banding = document.getElementById("unit");
                if (Code == "" && Name == "") {
                    Banding.innerHTML = "";
                    return;
                }
                for (var i = 0; i < Code.length; i++) {
                    var cross = id[i] + "/" + Name[i] + "/" + Code[i] + "/" + Type[i];
                    Banding.innerHTML += Name[i]+ "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='deleteFile(\"" + cross + "\")'>删除</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='DownloadFile(\"" + cross + "\")'>下载</a><br/>";
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
    window.open("DownLoad?id=" + id);
}
function deleteFile(id) {
    var one = confirm("确实要删除这条条目吗")
    if (one == false)
        return;
    else {
        $.ajax({
            url: "deleteFile",
            type: "post",
            data: { data1: id },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                   // loadFile();
                    window.parent.frames["iframeRight"].reloadPlanPro();
                    document.getElementById("unit").innerHTML = "";
                    $("#Ffilename").val("");
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
    }
}