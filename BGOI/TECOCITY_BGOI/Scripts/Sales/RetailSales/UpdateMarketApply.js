$(document).ready(function () {
    LoadFile();
    var msg = $("#msg").val();
    if (msg == "保存成功") {
        window.parent.frames["iframeRight"].reload();
        $("#Progress").html(msg);
        window.parent.ClosePop();
    }

    $("#btnSave").click(function () {
        var FileName = $("#UploadFile").val();

       // var FileName = $("#UploadFile").val();
        var ApplyTime = $("#ApplyTime").val();
        var ApplyType = $("#ApplyType").val();
        var ApplyTitle = $("#ApplyTitle").val();
        var Manager = $("#Manager").val();
        if (ApplyTime == "") {
            alert("申请日期不能为空");
            return;
        }
        if (ApplyType == "") {
            alert("类型不能为空");
            return;
        }
        if (ApplyTitle == "") {
            alert("名称不能为空");
            return;
        }
        if (Manager == "") {
            alert("申请人不能为空");
            return;
        }
        //if (FileName.replace(/(\s*$)/g, '') != "") {
        //    var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
        //    //if (filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('docx') < 0
        //    //    && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('xlsx') < 0) {
        //    //    alert("不支持该类型文件的上传，请上传word/excel文件");
        //    //    return;
        //    //}
        //}
        var a = confirm("确定修改市场销售信息？")
        if (a == false)
            return;
        else {
            $("#uploadify").uploadify("upload", '*');
            document.forms[0].submit();
        }
    });

    $("#btnCancel").click(function () {
        window.parent.ClosePop();
    })


    $("#uploadify").uploadify({
        uploader: '../SalesManage/InsertBiddingNew',           // 服务器端处理地址
        swf: '../Scripts/uploadify/uploadify.swf',    // 上传使用的 Flash

        width: 90,                          // 按钮的宽度
        height: 23,                         // 按钮的高度
        buttonText: "选择上传文件",                 // 按钮上的文字
        buttonCursor: 'hand',                // 按钮的鼠标图标

        fileObjName: 'Filedata',            // 上传参数名称
        formData: { 'RID': $("#PID").val() },// 'Types': $("#StrType").val(), 'Comments': $("#StrComments").val()
        // 两个配套使用
        fileTypeExts: "*.xls;*.xlsx;*.jpg;*.png;*.doc;*.docx;*.pdf;*.txt;*.JPG;*.PNG,*.zip;*.rar;", // 扩展名
        fileTypeDesc: "请选择 xls xlsx jpg png doc docx pdf txt JPG PNG zip rar 文件", // 文件说明

        auto: false,                // 选择之后，自动开始上传
        multi: true,               // 是否支持同时上传多个文件

        onUploadStart: function (file) {
            //  debugger;
            if (m == -1) {
                //debugger;
                $("#uploadify").uploadify("stop");
            }
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
            window.parent.frames["iframeRight"].reload();
            setTimeout('parent.ClosePop()', 10);
        }
    });
})

var m = 0;


//修改上传的文件
function LoadFile() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("PID").value;
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

//function LoadFile() {
//    document.getElementById("OldFile").innerHTML = "";
//    var PID = document.getElementById("PID").value;
//    var SalesType = "Market";

//    $.ajax({
//        url: "GetPromotionFile",
//        type: "post",
//        data: { PID: PID, SalesType: SalesType },
//        dataType: "Json",
//        success: function (data) {
//            if (data.success == "true") {
//                //var fileName = data.FileName.split('@');
//                //var fileInfo = data.FileInfo.split('@');

//                var arrFileName = new Array();
//                arrFileName = data.FileName.split(',');;
//                var arrFileInfo = new Array();
//                arrFileInfo = data.FileInfo.split(',');

//                var Banding = document.getElementById("OldFile");
//                if (data.FileName == "") {
//                    Banding.innerHTML = "";
//                    return;
//                }
//                for (var i = 0; i < arrFileInfo.length - 1; i++) {
//                    var cross = arrFileName[i] + "/" + arrFileInfo[i];
//                    if (arrFileName[i] != "")
//                        Banding.innerHTML += arrFileName[i] + "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='DownloadFile(\"" + PID + "\",\"" + arrFileName[i] + "\")'>下  载</a><br/>";
//                }
//            }
//            else {
//                alert(data.Msg);
//                return;
//            }
//        }
//    });
//}

//function DownloadFile(PID, FileName) {
//    window.open("DownLoadFile?PID=" + PID + "&FileName=" + FileName + "&FileType=Market");
//}