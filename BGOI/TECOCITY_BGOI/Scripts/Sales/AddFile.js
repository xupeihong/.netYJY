$(document).ready(function () {
    loadFile();
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        //document.getElementById("unit").innerHTML = "";
        //loadFile();
    }
    $("#hole").height($(window).height());
    $("#upLoad").click(function () {
        //var FileName = $("#UploadFile").val();
        //if (FileName.replace(/(\s*$)/g, '') == "") {
        //    alert("请选择要上传的文件");
        //    return;
        //}
        var one = confirm("确定上传文件吗");
        if (one == false)
            return;
        else {
            $("#uploadify").uploadify("upload", '*');
            //var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            //if (filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('pdf') < 0 && filetype.toLowerCase().indexOf('doc') < 0&& filetype.toLowerCase().indexOf('jpg') < 0  && filetype.toLowerCase().indexOf('docx') < 0) {
            //    alert("不支持该类型文件的上传，请上传jpg,doc,docx,pdf格式文件");
            //    return;
            //}
            //var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
            //if (filetype.toLowerCase().indexOf('docx') < 0 && filetype.toLowerCase().indexOf('doc') < 0) {
            //    alert("不支持该类型文件的上传，请上传docx,doc格式文件");
            //    return;
            //}

           // document.forms[0].submit();
           
           // window.parent.ClosePop();
        }
    })

    $("#uploadify").uploadify({
        uploader: 'InsertBiddingNew',           // 服务器端处理地址
        swf: '../Scripts/uploadify/uploadify.swf',    // 上传使用的 Flash

        width: 90,                          // 按钮的宽度
        height: 23,                         // 按钮的高度
        buttonText: "选择上传文件",                 // 按钮上的文字
        buttonCursor: 'hand',                // 按钮的鼠标图标

        fileObjName: 'Filedata',            // 上传参数名称
        formData: { 'RID': $("#StrCID").val() },// 'Types': $("#StrType").val(), 'Comments': $("#StrComments").val()
        // 两个配套使用
        fileTypeExts: "*.xls;*.xlsx;*.jpg;*.png;*.doc;*.docx;*.pdf;*.txt;*.JPG;*.PNG,*.zip;*.rar;", // 扩展名
        fileTypeDesc: "请选择 xls xlsx jpg png doc docx pdf txt JPG PNG zip rar 文件", // 文件说明

        auto: false,                // 选择之后，自动开始上传
        multi: true,               // 是否支持同时上传多个文件

        onUploadStart: function (file) {
            //  debugger;
            //if (m == -1) {
            //    //debugger;
            //    $("#uploadify").uploadify("stop");
            //}
            //  alert(m);
            // m++;
            //var element = {};
            //element.RID = $("#Hidden").val();
            //element.Types = $("#StrType").val();
            //element.Comments = $("#StrComments").val();
            //$("#uploadify").uploadify('settings', 'formData', element);
        },
        onUploadComplete: function (fileObj) {
            //window.parent.frames["iframeRight"].reload();
            //setTimeout('parent.ClosePop()', 10);
        },
        onQueueComplete: function (queueData) {
            //  SaveOffer();
            //上传队列全部完成后执行的回调函数  
            loadFile();
            window.parent.frames["iframeRight"].reload();
            setTimeout('parent.ClosePop()', 10);
        }
    });
})

function loadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("StrCID").value;
    $.ajax({
        url: "GetFile",
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
    window.parent.frames["iframeRight"].reload();
}
function DownloadFile(id) {
    window.open("DownLoad2?id=" + id);
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
                    loadFile();
                }
                else {
                    alert(data.Msg);
                    return;
                }
            }
        });
    }
}