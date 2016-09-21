$(document).ready(function () {
    var msg = $("#msg").val();
    if (msg != "") {
        alert(msg);
        window.parent.frames["iframeRight"].reload();
        window.parent.ClosePop();
    }
    ck_function();
    $("#hole").height($(window).height());
    $("#sure").click(function () {
        if ($("#GainDate").val() == "") {
            alert("获得用户时间不能为空"); return;
        }
        if ($("#DeclareUnit").val() == "") {
            alert("填报单位不能为空"); return;
        }
        if ($("#DeclareUser").val() == "") {
            alert("填报人不能为空"); return;
        }
        if ($("#ChargeUser").val() == "") {
            alert("负责人不能为空"); return;
        }
        if ($("#ShareUnits").val() == "") {
            alert("共享单位不能为空"); return;
        }
        if ($("#CName").val() == "") {
            alert("客户名称不能为空"); return;
        }
        if ($("#CShortName").val() == "") {
            alert("客户简称不能为空"); return;
        }
        if ($("#Industry").val() == "") {
            alert("所属行业不能为空"); return;
        }
        if ($("#StaffSize").val() == "") {
            alert("人员规模不能为空"); return;
        }
        if ($("#Products").val() == "") {
            alert("意向产品不能为空"); return;
        }
        if ($("#Phone").val() == "") {
            alert("客户座机不能为空"); return;
        }
        if ($("#FAX").val() == "") {
            alert("传真不能为空"); return;
        }
        if ($("#ZipCode").val() == "") {
            alert("邮编不能为空"); return;
        }
        if ($("#COMWebsite").val() == "") {
            alert("公司网址不能为空"); return;
        }
        if ($("#Province").val() == "") {
            alert("所属省份不能为空"); return;
        }
        if ($("#City").val() == "") {
            alert("所属城市不能为空"); return;
        }
        if ($("#CType").val() == "") {
            alert("客户类别不能为空"); return;
        }
        if ($("#CClass").val() == "") {
            alert("客户等级不能为空"); return;
        }
        if ($("#CSource").val() == "") {
            alert("客户来源不能为空"); return;
        }
        if ($("#CRelation").val() == "") {
            alert("客户关系不能为空"); return;
        }
        if ($("#Maturity").val() == "") {
            alert("成熟度不能为空"); return;
        }
        if ($("#State").val() == "") {
            alert("客户状态不能为空"); return;
        }
        var res = confirm("确定要保存客户信息吗？");
        if (res) {
            document.forms[0].submit();
        }
        else {
            return;
        }
    });
})
function ck_function() {
    $('.ckb1').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("IsShare").value = res;
            $('.ckb1').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb1').attr("disabled", false);
        }
    })
}
function changes(op) {
    if (op == "unit") {
        var rela = "";
        $('input[name=rela1]:checkbox:checked').each(function () {
            rela += $(this).val() + ",";
        })
        document.getElementById("ShareUnits").value = rela;
    }
}