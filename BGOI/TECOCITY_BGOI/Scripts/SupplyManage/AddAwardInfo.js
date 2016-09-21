$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reloadWard();
        window.parent.ClosePop();
    }

    //$("#uploadify").uploadify({
    //    uploader: 'InsertBiddingNew',           // 服务器端处理地址
    //    swf: '../Scripts/uploadify/uploadify.swf',    // 上传使用的 Flash

    //    width: 90,                          // 按钮的宽度
    //    height: 23,                         // 按钮的高度
    //    buttonText: "选择上传文件",                 // 按钮上的文字
    //    buttonCursor: 'hand',                // 按钮的鼠标图标

    //    fileObjName: 'Filedata',            // 上传参数名称
    //    formData: { 'SID': $("#SID").val() },
    //    // 两个配套使用
    //    fileTypeExts: "*.xls;*.xlsx;*.jpg;*.png;*.doc;*.docx;*.pdf;*.txt;*.JPG;*.PNG,*.zip;*.rar;", // 扩展名
    //    fileTypeDesc: "请选择 xls xlsx jpg png doc docx pdf txt JPG PNG zip rar 文件", // 文件说明

    //    auto: false,                // 选择之后，自动开始上传
    //    multi: true,               // 是否支持同时上传多个文件
    //    onUploadStart: function (file) {
    //        var element = {};
    //        element.SID = $("#SID").val();
    //        //element.Types = $("#StrType").val();
    //        //element.Comments = $("#StrComments").val();
    //        //$("#uploadify").uploadify('settings', 'formData', element);
    //    },
    //    onUploadComplete: function (fileObj) {
    //        // 单个文件传完之后
    //    },
    //    onQueueComplete: function (queueData) {
    //        //上传队列全部完成后执行的回调函数  
    //        //window.parent.frames["iframeRight"].reloadWard();
    //       // setTimeout('parent.ClosePop()', 10);
    //    }

   // })
   
    $("#hole").height($(window).height());
    $("#sure1").click(function () {
        //var FileName = $("#UploadFile").val();
        //if (FileName.replace(/(\s*$)/g, '') != "") {
        //    var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
        //    if (filetype.toLowerCase().indexOf('xlsx') < 0 && filetype.toLowerCase().indexOf('docx') < 0 && filetype.toLowerCase().indexOf('pic') < 0 && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('png') < 0 && filetype.toLowerCase().indexOf('gif') < 0) {
        //        alert("不支持该类型文件的上传，请上传word,pic,excel,png,jpg,gif格式文件");
        //        return;
        //    }
        //}
        //layer.open({
        //    type: 1,
        //    skin: 'layui-layer-demo', //样式类名
        //    closeBtn: 0, //不显示关闭按钮
        //    shift: 2,
        //    shadeClose: true, //开启遮罩关闭
        //    content: '数据提交中，请稍等...'
        //});
        var res = confirm("确定要保存曾获奖项信息吗？");
        if (res) {
           // $("#uploadify").uploadify("upload", '*');// 上传队列所有文件
            document.forms[0].submit();
        }
        else {
            return;
        }
    });
})