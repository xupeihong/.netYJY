var isConfirm = false;
$(document).ready(function () {
    $("#pageContent").height($(window).height());

    $("#uploadify").uploadify({
        uploader: 'InsertBiddingFile',           // 服务器端处理地址
        swf: '../Scripts/uploadify/uploadify.swf',    // 上传使用的 Flash

        width: 90,                          // 按钮的宽度
        height: 23,                         // 按钮的高度
        buttonText: "选择上传文件",                 // 按钮上的文字
        buttonCursor: 'hand',                // 按钮的鼠标图标

        fileObjName: 'Filedata',            // 上传参数名称
        formData: { 'PID': $("#StrPID").val(), 'sid': $("#StrBidID").val() },// 'Types': $("#StrType").val(), 'Comments': $("#StrComments").val()
        // 两个配套使用
        fileTypeExts: "*.xls;*.xlsx;*.jpg;*.png;*.doc;*.docx;*.pdf;*.txt;*.JPG;*.PNG,*.zip;*.rar;", // 扩展名
        fileTypeDesc: "请选择 xls xlsx jpg png doc docx pdf txt JPG PNG zip rar 文件", // 文件说明

        auto: false,                // 选择之后，自动开始上传
        multi: true,               // 是否支持同时上传多个文件

        onUploadStart: function (file) {
            var element = {};
            element.PID = $("#StrPID").val();
            element.sid = $("#StrBidID").val();
            $("#uploadify").uploadify('settings', 'formData', element);
        },
        onUploadComplete: function (fileObj) {
            //window.parent.frames["iframeRight"].reload();
            //setTimeout('parent.ClosePop()', 10);
        },
        onQueueComplete: function (queueData) {
            //SaveOffer();
            //上传队列全部完成后执行的回调函数  
            //window.parent.frames["iframeRight"].reload();
            setTimeout('parent.ClosePop()', 10);
        }
    });

    $("#upload").click(function () {
        var one = confirm("确定要上传文件吗")
        if (one == false) {
            return false;
        }
        else {
            $("#uploadify").uploadify("upload", '*');// 上传队列所有文件 
        }
    })

    $("#charge").click(function () {

        if ($("#StrPview").val() == "") {
            alert("概述不能为空");
            return;
        }
        if ($("#StrBiddingTime").val() == "") {
            alert("投标时间不能为空");
            return;
        }
        isConfirm = confirm("确定要保存投标内容吗")
        if (isConfirm == false) {
        return false;
         }
        else
            submitInfo();

    })
})

function returnConfirm() {
    return isConfirm;
}
function submitInfo() {
    var options = {
        url: "InsertBidding",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                document.getElementById("charge").style.display = "none";
                document.getElementById("upload").style.display = "";
                window.parent.frames["iframeRight"].reload();
                window.parent.frames["iframeRight"].reload5();
                alert(data.Msg);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}