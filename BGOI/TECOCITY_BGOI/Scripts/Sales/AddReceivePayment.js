$(document).ready(function () {
    if (location.search != "") {
        OrderID = location.search.split('&')[0].split('=')[1];
        ContractID = location.search.split('&')[1].split('=')[1];
    }
   // document.getElementById('JE').style.display = 'block';
   // document.getElementById('ZP').style.display = 'none';
    $("#btnSaveOrder").click(function () {
        isConfirm = confirm("确定要回款吗")
        if (isConfirm == false) {
            return false;
        }
        else {
            $("#uploadify").uploadify("upload", '*');
            SaveReceivePayment();
        }
    })
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    });

    $("#Methods").change(function () {
        var method = $("#Methods").val();
        if (method == "CB1")
        {
          //  document.getElementById('JE').style.display = 'block';
           // document.getElementById('ZP').style.display = 'none';
        } else if (method == "CB2") {
            //支票
           // document.getElementById('JE').style.display = 'none';
            //document.getElementById('ZP').style.display = 'block';
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
var m = 0;
function SaveReceivePayment()
{
    if ($('.field-validation-error').length == 0) {
        var RID = $("#RID").val();
        var OID = $("#OrderID").val();
        var Methods = $("#Methods").val();
        var Amount = $("#Amount").val();
        var ChequeID = $("#ChequeID").val();
        var PaymentUnit = $("#PaymentUnit").val();
        var DebtAmount = $("#DebtAmount").val();
        var Remark = $("#Remark").val();

        if (Methods == "CB2" && ChequeID == "")
        {
            alert("支票号不能为空");
            return;
        }
        if (Amount <= 0 || Amount == "")
        {
            alert("回款金额不能为0");
            return;
        }
        if (Amount > DebtAmount)
        {
            alert("回款金额不能超过欠款金额");
            return;
        }
        if (PaymentUnit == "")
        {
            alert("单位不能为空");
            return;
        }
        //$("#drpProvince").change(function () { $.getJSON("/Persons/GetCities/" + $(this).val(), null ,
        //    function (data) { $.each(data, function (i, item) { item[“”])//这是返回的数据 }); }); });

        $.ajax({
            url: "SaveReceivePayment",
            type: "Post",
            data: {
                RID: RID, OrderID: OID,ContractID:ContractID, Methods: Methods, Amount: Amount, ChequeID: ChequeID, PaymentUnit: PaymentUnit, Remark: Remark
            },
           // async: false,
            success: function (data) {
                if (data.success == true) {
                    window.parent.frames["iframeRight"].reload();
                    alert("生成回款！");
                    window.parent.ClosePop();
                }
                else {
                    alert("生成回款失败-" + data.Msg);
                }
            }
        });
    }
}

function GetTotal()
{
    var orderid = $("#OrderID").val();
    $.ajax({
        url: "GetReceivePaymentTotal",
        type: "post",
        data: { OrderID: orderid },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                   // alert(json[i]["Total"]);
                    $("#Total").val(json[i]["Total"]);
                    $("#DebtAmount").val(json[i]["DebtAmount"]);
                    $("#HKAmount").val(json[i]["HKAmount"]);
                }

            }
        }

    })
}