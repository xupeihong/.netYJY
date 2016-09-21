$(document).ready(function () {
    DDID = location.search.split('&')[0].split('=')[1];

    loadFile(DDID);
    $("#uploadify").uploadify({
        uploader: 'InsertBiddingNewS',           // 服务器端处理地址
        swf: '../Scripts/uploadify/uploadify.swf',    // 上传使用的 Flash

        width: 90,                          // 按钮的宽度
        height: 23,                         // 按钮的高度
        buttonText: "选择上传文件",                 // 按钮上的文字
        buttonCursor: 'hand',                // 按钮的鼠标图标

        fileObjName: 'Filedata',            // 上传参数名称
        formData: { 'RID': DDID },
        // 两个配套使用
        fileTypeExts: "*.xls;*.xlsx;*.jpg;*.png;*.doc;*.docx;*.pdf;*.txt;*.JPG;*.PNG,*.zip;*.rar;", // 扩展名
        fileTypeDesc: "请选择 xls xlsx jpg png doc docx pdf txt JPG PNG zip rar 文件", // 文件说明

        auto: false,                // 选择之后，自动开始上传
        multi: true,               // 是否支持同时上传多个文件
        onUploadStart: function (file) {
            var element = {};
            element.RID = DDID;
          
            $("#uploadify").uploadify('settings', 'formData', element);
        },
        onUploadComplete: function (fileObj) {
            // 单个文件传完之后
        },
        onQueueComplete: function (queueData) {
            //上传队列全部完成后执行的回调函数  
            //window.parent.frames["iframeRight"].reload();
            alert("上传成功");
            setTimeout('parent.ClosePop()', 10);
        }

    })

    // 确定按钮  类型为 button 
    $("#charge").click(function () {
        isConfirm = confirm("确定要保存吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            $("#uploadify").uploadify("upload", '*');// 上传队列所有文件 
        }
    })

})

function loadFile(DDID) {
    document.getElementById("unit").innerHTML = "";
    var InforNo = "";
    $.ajax({
        url: "GetFile",
        type: "post",
        data: { data1: DDID },
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
                    Banding.innerHTML += Name[i] + "<a style='color:blue;cursor:hand;' onclick='DownloadFile(\"" + cross + "\")'>下载</a></br>";
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
