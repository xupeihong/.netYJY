var isConfirm = false;
var type;
$(document).ready(function () {
    $("#hole").height($(window).height());

    $("#uploadify").uploadify({
        uploader: 'InsertProjectCCashBackFile',           // 服务器端处理地址
        swf: '../Scripts/uploadify/uploadify.swf',    // 上传使用的 Flash

        width: 90,                          // 按钮的宽度
        height: 23,                         // 按钮的高度
        buttonText: "选择上传文件",                 // 按钮上的文字
        buttonCursor: 'hand',                // 按钮的鼠标图标

        fileObjName: 'Filedata',            // 上传参数名称
        formData: { 'PID': $("#StrCID").val(), 'CBID': $("#StrCBID").val() },// 'Types': $("#StrType").val(), 'Comments': $("#StrComments").val()
        // 两个配套使用
        fileTypeExts: "*.xls;*.xlsx;*.jpg;*.png;*.doc;*.docx;*.pdf;*.txt;*.JPG;*.PNG,*.zip;*.rar;", // 扩展名
        fileTypeDesc: "请选择 xls xlsx jpg png doc docx pdf txt JPG PNG zip rar 文件", // 文件说明

        auto: false,                // 选择之后，自动开始上传
        multi: true,               // 是否支持同时上传多个文件

        onUploadStart: function (file) {
            var element = {};
            element.PID = $("#StrCID").val();
            element.CBID = $("#StrCBID").val();
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
            $("#uploadify").uploadify("upload",'*');// 上传队列所有文件 
        }
    })

    $("#charge").click(function () {
        if ($("#StrCBMethod").val() == "")
        {
            alert("回款方式不能为空");
            return;
        }
        if ($("#StrCBMoney").val() == 0) {
            alert("回款金额不能为零");
            return;
        }
        if ($("#StrCBDate").val() == "") {
            alert("回款日期不能为空");
            return;
        }
        $.ajax({
            url: "CheckProjectMoney",
            type: "post",
            data: { data1: $("#StrCID").val(), data2: $("#StrCBMoney").val() },
            dataType: "Json",
            success: function (data) {
                if (data.success == "true") {
                    if ($('input:radio[name="IsReturn"]:checked').val() == null) {
                        alert("请选择是否符合约定的回款进度");
                        return;

                    }
                    var RceMoney = $("#StrReceiptMoney").val();
                    var CBMoney = $("#StrCBMoney").val();
                    if (RceMoney > CBMoney)
                    {
                        alert("发票金额不能大于回款金额");
                        return;
                    }
                    isConfirm = confirm("确定保存回款记录吗")
                    if (isConfirm == false) {
                        return false;
                    }
                    else {
                        submitInfo();
                        //$("#uploadify").uploadify("upload",'*');// 上传队列所有文件 
                    }
                }
                else {
                    alert("回款金额已超过项目合同额，不能进行回款");
                    return;
                }
            }
        });
    })

})

function returnConfirm() {
    return false;
}

function submitInfo() {
    $("#StrIsReturn").val($('input:radio[name="IsReturn"]:checked').val());
    type = $("#type").val();
    var options = {
        url: "InsertProjectCCashBack",
        data: {},
        type: 'POST',
        async: false,
        success: function (data) {
            if (data.success == "true") {
                document.getElementById("charge").style.display = "none";
                document.getElementById("upload").style.display = "";
                window.parent.frames["iframeRight"].reload();
                window.parent.frames["iframeRight"].reload7();
                alert(data.Msg);
                //setTimeout('parent.ClosePop()', 100);
            }
            else {
                //alert(data.Msg);
            }
        }
    };
    $("#ProjectformInfo").ajaxSubmit(options);
    return false;
}
