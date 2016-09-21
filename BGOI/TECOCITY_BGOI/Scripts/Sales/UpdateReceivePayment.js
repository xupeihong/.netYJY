$(document).ready(function () {
    if (location.search != "") {
        OrderID = location.search.split('&')[0].split('=')[1];
    }
    loadFile();
    //document.getElementById('JE').style.display = 'block';
   // document.getElementById('ZP').style.display = 'none';
    $("#btnSaveOrder").click(function () {
        isConfirm = confirm("确定要修改回款吗");
        if (isConfirm == false) {
            return false;
        }
        else {
            //$("#uploadify").uploadify("upload", '*');
            //UpdateOffer();
            
                $("#uploadify").uploadify("upload", '*');
                SaveReceivePayment();
            
        }
       
    })
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });

    $("#Methods").change(function () {
        var method = $("#Methods").val();
        if (method == "M01") {
            document.getElementById('JE').style.display = 'block';
            document.getElementById('ZP').style.display = 'none';
        } else if (method == "M02") {
            //支票
            document.getElementById('JE').style.display = 'none';
            document.getElementById('ZP').style.display = 'block';
        }
        //else if ($("#Methods").val() == "汇款") {
        //    //汇款

        //}
    });

    $("#uploadify").uploadify({
        uploader: 'InsertBiddingNew',           // 服务器端处理地址
        swf: '../Scripts/uploadify/uploadify.swf',    // 上传使用的 Flash

        width: 90,                          // 按钮的宽度
        height: 23,                         // 按钮的高度
        buttonText: "选择上传文件",                 // 按钮上的文字
        buttonCursor: 'hand',                // 按钮的鼠标图标

        fileObjName: 'Filedata',            // 上传参数名称
        formData: { 'RID': $("#RID").val() },// 'Types': $("#StrType").val(), 'Comments': $("#StrComments").val()
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
});
function SaveReceivePayment() {
    if ($('.field-validation-error').length == 0) {
        var RID = $("#RID").val();
        var OID = $("#OrderID").val();
        var Mothods = $("#Mothods").val();
        var Amount = $("#Amount").val();
        var ChequeID = $("#ChequeID").val();
        var PaymentUnit = $("#PaymentUnit").val();
        var Remark = $("#Remark").val();
        //$("#drpProvince").change(function () { $.getJSON("/Persons/GetCities/" + $(this).val(), null ,
        //    function (data) { $.each(data, function (i, item) { item[“”])//这是返回的数据 }); }); });

        $.ajax({
            url: "SaveUpdateReceivePayment",
            type: "Post",
            data: {
                RID: RID, OID: OID, Mothods: Mothods, Amount: Amount, ChequeID: ChequeID, PaymentUnit: PaymentUnit, Remark: Remark
            },
           // async: false,
            success: function (data) {
                window.parent.frames["iframeRight"].reload();
                if (data.success == true) {
                    alert("修改回款！");
                    window.parent.ClosePop();
                }
                else {
                    alert("修改回款失败-" + data.msg);
                }
            }
        });
    }
}


//修改上传的文件
function loadFile() {
    document.getElementById("unit").innerHTML = "";
  //  $("#RID").val();
    var InforNo = document.getElementById("RID").value;
    $.ajax({
        url: "GetUploadFile",
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