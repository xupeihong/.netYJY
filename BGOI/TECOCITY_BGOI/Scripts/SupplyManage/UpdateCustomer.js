var i = 0;
var j = 1;

$(document).ready(function () {
    $("#hole").height($(window).height());
    ck_function();
    $("#updateCustomer").submit(function () {
        var options = {
            url: "UpdateCusInfo",
            data: {},
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    alert(data.Msg);
                    window.parent.frames["iframeRight"].reload();
                    window.parent.ClosePop();
                }
                else {
                    alert(data.Msg);
                }
            }
        };
        $("#updateCustomer").ajaxSubmit(options);
        return false;
    })
    $("#UPCustome").click(function () {
        var GainDate = document.getElementById("GainDate").value;
        var DeclareUnit = document.getElementById("DeclareUnit").value;
        var DeclareUser = document.getElementById("DeclareUser").value;
        var ChargeUser = document.getElementById("ChargeUser").value;
        var IsShare = document.getElementById("IsShare").value;
        var ShareUnits = document.getElementById("ShareUnits").value;
        var CName = document.getElementById("CName").value;
        var CShortName = document.getElementById("CShortName").value;
        var Industry = document.getElementById("Industry").value;
        var StaffSize = document.getElementById("StaffSize").value;
        var Products = document.getElementById("Products").value;
        var Phone = document.getElementById("Phone").value;
        var FAX = document.getElementById("FAX").value;
        var ZipCode = document.getElementById("ZipCode").value;
        var COMWebsite = document.getElementById("COMWebsite").value;
        var ComAddress = document.getElementById("ComAddress").value;
        var Province = document.getElementById("Province").value;
        var City = document.getElementById("City").value;
        var ClientDesc = document.getElementById("ClientDesc").value;
        var Remark = document.getElementById("Remark").value;
        var CType = document.getElementById("CType").value;
        var CClass = document.getElementById("CClass").value;
        var CSource = document.getElementById("CSource").value;
        var CRelation = document.getElementById("CRelation").value;
        var Maturity = document.getElementById("Maturity").value;
        var State = document.getElementById("State").value;
        if (GainDate == "") {
            alert("客户时间不能为空");
            return;
        }
        if (IsShare == "") {
            alert("是否共享不能为空");
            return;
        }
        if (CName == "") {
            alert("客户名称不能为空");
            return;
        }
        if (Industry == "") {
            alert("所属行业不能为空");
            return;
        }
        if (Products == "") {
            alert("意向产品不能为空");
            return;
        }
        if (CType == "") {
            alert("客户类别不能为空");
            return;
        }
        if (State == "") {
            alert("状态不能为空");
            return;
        }
        var res = confirm("确定保存客户信息吗？");
        if (res) {
            $("#updateCustomer").submit();

        } else {
            return;
        }
    });
    Load();
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
function Load() {
    var isshare = $("#IsShare").val();
    var Judge = document.getElementsByName("0");
    for (var i = 0; i < Judge.length; i++) {
        if (Judge[i].value == isshare[i]) {
            Judge[i].checked = true;
        } else {
            Judge[i].checked = true;
        }
    }
    var ShareUnits = $("#ShareUnits").val();
    var arrre = new Array();
    arrre = ShareUnits.split(',');//分割成多个4个多一个逗号
    for (var i = 0; i < arrre.length; i++) {
        var Judge = document.getElementsByName("rela1");//5个checkbox
        for (var j = 0; j < Judge.length; j++) {
            if (Judge[j].value == arrre[i]) {
                Judge[j].checked = true;
            }
        }
    }
}
function change(op) {
    if (op == "unit") {
        var rela = "";
        $('input[name=rela1]:checkbox:checked').each(function () {
            rela += $(this).val() + ",";
        })
        document.getElementById("ShareUnits").value = rela;
    }
}
