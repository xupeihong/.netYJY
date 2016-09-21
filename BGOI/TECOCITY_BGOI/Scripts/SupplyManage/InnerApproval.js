$(document).ready(function () {
    loadPrice();
    $('.supname').click(function () {
        var rela = "";
        $('input[name=supname]:text:input').each(function () {
            rela += $(this).val() + ",";
        })
        document.getElementById("UnReviewDesc").value = rela;

    })
    ck_Function();
    // $("#hole").height($(window).height());
    IsUnView();
    IsView();
    $("#one").hide();
    $("#two").hide();
    $("#three").hide();
    $("#four").hide();
    $("#five").hide();
    $("#six").hide();
    $("#eight").hide();
    $("#Free").hide();
    $("#Eva1").hide();
    $("#Eva2").hide();
    $("#Eva3").hide();
    $("#bot").hide();
    $("#Eva4").hide();
    $("#Eva5").hide();
    $("#kaocha").hide();
    $("#innerapp").hide();
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reload();
        window.parent.ClosePop();
    }

    $("#innerapp").click(function () {
        var IsUnReview = $("input:radio[name='IsUnReview']:checked").val();
        var UnReviewUnit = $('input[name=ulut]:checkbox:checked').val();
        var URConfirmUser = $("input:radio[name='URConfirmUser']:checked").val();
        var IsURInnerUnit = $("input:radio[name='IsURInnerUnit']:checked").val();
        var FileName = $("#UploadFile").val();
        var DeclareUser = $("#DeclareUser").val();
        var BussinessUser = $("#BussinessUser").val();
        var TecolUser = $("#TecolUser").val();
        var BuyUser = $("#BuyUser").val();
        var SaleUser = $("#SaleUser").val();
        var ChargeUser = $("#ChargeUser").val();
        var UploadFile = $("#UploadFile").val();//资质
        var UploadFile1 = $("#UploadFile1").val();//比价单
        var UnReviewDesc = $("#UnReviewDesc").val();
        var MFFileName = $("#MFFileName").val();
        var pricename = $("#PriceName").val();
        var rela = "";
        var tval = "";
        $('input[name=supname]:text:input').each(function () {
            tval += $(this).val();
            rela += $(this).val() + ",";
        })
        //alert(URConfirmUser);
        //alert(tval);
        // alert(UnReviewDesc);
        // alert(IsURInnerUnit);
        //alert(UnReviewUnit);
        //alert(tval.length);


        var IsUnreviewUser = document.getElementById("IsUnreviewUser").value;
        if (IsUnReview == "0" && URConfirmUser != "0" && URConfirmUser != "1" && IsUnreviewUser == "" && IsURInnerUnit != "0" && IsURInnerUnit != "1") {
            alert("是否是集团供应商、客户指定免评供应商、免评供应商内部单位至少选一项填写");
            return;
        }

        if (URConfirmUser == "1" && tval == "" && UnReviewUnit == undefined) {
            alert("客户指定免评审供应商描述或免评审供应商是否为内部单位任选一项填写");
            return;
        }
        if (URConfirmUser == "1" && tval.length < 4 && tval.length > 0) {
            alert("请将客户指定免评审供应商描述填写完整"); return;
        }

        else {
            if (IsUnReview == "0" && UploadFile == "" && MFFileName == "") { alert("免评资质证明不能为空"); return; }
            if (IsUnReview == "1" && UploadFile1 == "" && pricename == "") { alert("报价/比价单不能为空"); return; }
            if (FileName.replace(/(\s*$)/g, '') != "") {
                var filetype = (/\.[^\.]+$/.exec(FileName)).toString();
                if (filetype.toLowerCase().indexOf('xlsx') < 0 && filetype.toLowerCase().indexOf('docx') < 0 && filetype.toLowerCase().indexOf('pic') < 0 && filetype.toLowerCase().indexOf('xls') < 0 && filetype.toLowerCase().indexOf('doc') < 0 && filetype.toLowerCase().indexOf('png') < 0 && filetype.toLowerCase().indexOf('jpg') < 0 && filetype.toLowerCase().indexOf('gif') < 0) {
                    alert("不支持该类型文件的上传，请上传word,pic,excel,png,jpg,gif格式文件");
                }
            }
            if (UploadFile1.replace(/(\s*$)/g, '') != "") {
                var pricetype = (/\.[^\.]+$/.exec(UploadFile1)).toString();
                if (pricetype.toLowerCase().indexOf('xlsx') < 0 && pricetype.toLowerCase().indexOf('docx') < 0 && pricetype.toLowerCase().indexOf('pic') < 0 && pricetype.toLowerCase().indexOf('xls') < 0 && pricetype.toLowerCase().indexOf('doc') < 0 && pricetype.toLowerCase().indexOf('png') < 0 && pricetype.toLowerCase().indexOf('jpg') < 0 && pricetype.toLowerCase().indexOf('gif') < 0) {
                    alert("不支持该类型文件的上传，请上传word,pic,excel,png,jpg,gif格式文件");
                }
            }
            //if (URConfirmUser == '1') {
            //var rela = "";
            //var val = "";
            //$('input[name=supname]:text:input').each(function () {
            //    //alert(rela);

            //    val = $(this).val();
            //    rela += $(this).val() + ",";
            //})
            //if (val == "") { //,,,,
            //    alert("客户指定免评审供应商描述必须填写完整");
            //}
            //else {
            var res = confirm("确定要保存内部评审吗？");
            if (res) {
                document.forms[0].submit();
            }
            else {
                return;
            }
            // }
            //}

            //  document.getElementById("UnReviewDesc").value = rela;

        }
    })
});
function ck_Function() {
    $('.ckb1').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            if (res == '1') {
                $("#kaocha").hide();
            } else {
                $("#kaocha").show();
            }
            document.getElementById("Evaluation5").value = res;
            $('.ckb1').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb1').attr("disabled", false);
        }
    })
    $('.ckb2').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("IsUnReview").value = res;
            $('.ckb2').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb2').attr("disabled", false);
        }
    })
    $('.ckb3').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            if (res == '1') {
                $("#ManName").hide();
            } else { $("#ManName").show(); }
            document.getElementById("IsUnreviewUser").value = res;
            $('.ckb3').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb3').attr("disabled", false);
        }
    })

    $('.ckb4').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("IsURInnerUnit").value = res;
            $('.ckb4').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb4').attr("disabled", false);
        }
    })
}
function change(op) {
    if (op == "urut") {
        var rela = "";
        $('input[name=ulut]:checkbox:checked').each(function () {
            rela += $(this).val() + ",";
        })
        document.getElementById("UnReviewUnit").value = rela;
    }
}
function IsView() {
    var Evaluation5 = $("input[name='Evaluation5']:checked").val();
    if (Evaluation5 == '0') {
        $("#kaocha").show();
    }
    else {
        $("#kaocha").hide();

    }
}
function IsUnView() {
    var IsUnReview = $("input[name='IsUnReview']:checked").val();
    if (IsUnReview == '0') { //是免评供应商
        $("#one").show();
        $("#two").show();
        $("#three").show();
        $("#four").show();
        $("#five").show();
        $("#six").hide();
        $("#eight").hide();
        $("#Free").show();
        $("#bot").hide();
        $("#kaocha").hide();
        $("#Eva1").hide();
        $("#Eva2").hide();
        $("#Eva3").hide();
        $("#Eva4").hide();
        $("#Eva5").hide();
        $("#innerapp").show();
        //$("#hole").attr({ width: "700", height: "450" });
        //$("#title").attr({ width: "700", height: "350" });
        // $("#tabInfo").attr({ width: "700", height: "650" });

    }
    else {
        $("#kaocha").show();
        $("#Eva1").show();
        $("#Eva2").show();
        $("#Eva3").show();
        $("#Eva4").show();
        $("#Eva5").show();
        $("#four").hide();
        $("#one").hide();
        $("#two").hide();
        $("#three").hide();
        $("#five").hide();
        $("#eight").show();
        $("#six").show();
        $("#Free").hide();
        $("#innerapp").show();
        $("#bot").show();
        // $("#hole").attr({ width: "700", height: "650" });
        //$("#title").attr({  height: "23" });
    }
}
function loadPrice() {
    document.getElementById("unit").innerHTML = "";
    var InforNo = document.getElementById("Sid").value;
    // var timeout = document.getElementById("FTimeOut").value;//
    //var fid = $("#FID").val();//唯一编号
    var filename = $("#PriceName").val();//资质文件名称

    $.ajax({
        url: "GetPriceFile",
        type: "post",
        data: { data1: InforNo, pricename: filename },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var id = data.id.split('@');
                // var Code = data.File.split('@');
                var Name = data.Name.split('@');
                // var Type = data.Type.split('@');
                var Banding = document.getElementById("unit");
                if (Name == "") {
                    Banding.innerHTML = "";
                    return;
                }
                for (var i = 0; i < Name.length; i++) {
                    var cross = id[i] + "/" + Name[i]; //+ "/" + Code[i] + "/" + Type[i];
                    Banding.innerHTML += Name[i];
                    //+ "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='deleteFile(\"" + cross + "\")'>删除</a>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<a style='color:blue;cursor:hand;' onclick='DownloadFile(\"" + cross + "\")'>下载</a><br/>";
                }
            }
            else {
                alert(data.Msg);
                return;
            }
        }
    });

}