$(document).ready(function () {
    $("#ContractInfo").submit(function () {
        var options = {
            url: "AddNewContracte",
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
        $("#ContractInfo").ajaxSubmit(options);
        return false;
    });
    $("#charge").click(function () {
        var sid = document.getElementById("Sid").value;//提交保存必须得加上
        var FDepartment = document.getElementById("Fdepartment").value;
        var PName = document.getElementById("Pname").value;
        var Department = document.getElementById("Department").value;
        var Job = document.getElementById("Job").value;
        var Phone = document.getElementById("Phone").value;
        var Mobile = document.getElementById("Mobile").value;
        if (FDepartment == "") {
            alert("职能部门不能为空");
            return;
        } else if (PName == "") { alert("联系人不能为空"); return; }
        else if (Department == "") { alert("部门不能为空"); return; }
        else if (Job == "") { alert("职位不能为空"); return; }
        else if (Phone == "") { alert("座机不能为空"); return; }
        else if (Mobile == "") { alert("手机不能为空"); return; }
        else {
            var res = confirm("确定提交联系人信息吗？");
            if (res) {
                $("#ContractInfo").submit();
            } else {
                return;
            }

        }

    });
});

