$(document).ready(function () {
    //$("#ApplyTime").val('');
    $("#StartTime").val('');
    $("#EndTime").val('');
    var msg = $("#msg").val();
    if (msg == "保存成功") {
        window.parent.frames["iframeRight"].reload();
        $("#Progress").html(msg);
        window.parent.ClosePop();
    }

    $("#btnSave").click(function () {
        //var FileName = $("#UploadFile").val();
        //if (FileName.replace(/(\s*$)/g, '') != "") {
        //    var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
        //    if (filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('docx') < 0
        //        && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('xlsx') < 0) {
        //        alert("不支持该类型文件的上传，请上传word/excel文件");
        //        return;
        //    }
        //}
        var Applyer = $("#Applyer").val();
        if (Applyer == "") {
            m = -1;
            alert("申请人不能为空");
            return;
        }
        var ActionTitle = $("#ActionTitle").val();
        if (ActionTitle == "") {
            m = -1;
            alert("活动标题不能为空");
            return;
        }
        var StartTime = $("#StartTime").val();
        if (StartTime == "")
        {
            m = -1;
            alert("活动执行时间不能为空");
            return;
        }
        var EndTime = $("#EndTime").val();
        if (EndTime == "") {
            m = -1;
            alert("活动执行时间不能为空");
            return;
        }
        var ActionProject = $("#ActionProject").val();
        if (ActionProject == "")
        {
            m = -1;
            alert("活动金额不能为空");
            return;
        }
        var a = confirm("确定保存促销活动信息？")
        if (a == false)
            return;
        else {
            $("#uploadify").uploadify("upload", '*');
            document.forms[0].submit();
        }
    })

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