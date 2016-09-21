var i, strSelect1, strSelect2, strSelect3, strzhize;
var j = 1;
var num = 0, CountRows = 0;
var isConfirm = false;
var SID = "";
var i = 0;
var x = 0;
var rowCount = 0;
$(document).ready(function () {
    var IsrankingIn5 = $("input[name='IsrankingIn5']:checked").val();//排名前5
    if (IsrankingIn5 == "1") {
        $(".RankingType").attr('disabled', true);
        $("#Ranking").attr('disabled', true);
    }
    else {
        $(".RankingType").attr('disabled', false);
        $("#Ranking").attr('disabled', false);
    }
    var sup = $("#SupplierType").find("option:selected").text();
    if (sup == "其他") {
        $("#OtherType").attr('disabled', false);
    }
    else {
        $("#OtherType").attr('disabled', 'disabled');
    }
    var ThreeCertity = $("input[name='Certity']:checked").val();//是否三证合一
    if (ThreeCertity == "1") {
        $("#OrganizationCode").attr('disabled', false);
        $("#TaxRegistrationNo").attr('disabled', false);
        $("#BusinessLicenseNo").attr('disabled', false);
        //$("#ThreeCertity").attr("disabled", 'disabled');
    }
    else {
        $("#OrganizationCode").attr('disabled', 'disabled');
        $("#TaxRegistrationNo").attr('disabled', 'disabled');
        $("#BusinessLicenseNo").attr('disabled', 'disabled');
        //$("#ThreeCertity").attr("disabled", false);
    }
    if (location.search != "") {
        SID = location.search.split('&')[0].split('=')[1];
    }
    //GetTime();
    $("#hole").height($(window).height());
    var sel1, selment, seljob;
    var val1, valment, valjob;
    $.ajax({
        url: "GetContractPerson",
        type: "post",
        data: { sid: SID },
        dataType: "json",
        async: false,
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                //职责部门
                $.ajax({
                    url: "GetContractSize",
                    type: "post",
                    data: { Type: "FDepartment" },
                    dataType: "Json",
                    async: false,
                    success: function (data) {
                        if (data.success == "true") {
                            var strSel1 = data.Strid;
                            var strVal1 = data.Strname;
                            sel1 = strSel1.split(',');
                            val1 = strVal1.split(',');

                        }
                        else {
                            return;
                        }
                    }
                });
                //部门
                $.ajax({
                    url: "GetContractSize",
                    type: "post",
                    data: { Type: "Department" },
                    dataType: "Json",
                    async: false,
                    success: function (data) {
                        if (data.success == "true") {
                            var strSel2 = data.Strid;
                            var strVal2 = data.Strname;
                            selment = strSel2.split(',');
                            valment = strVal2.split(',');
                        }
                        else {
                            return;
                        }

                    }
                });
                //职位
                $.ajax({
                    url: "GetContractSize",
                    type: "post",
                    data: { Type: "Job" },
                    dataType: "Json",
                    async: false,
                    success: function (data) {
                        if (data.success == "true") {
                            var strSel3 = data.Strid;
                            var strVal3 = data.Strname;
                            seljob = strSel3.split(',');
                            valjob = strVal3.split(',');
                        }
                        else {
                            return;
                        }
                    }
                });

                for (var g = 0; g < json.length; g++) {

                    rowCount++;
                    var NewTable = document.getElementById("DetailInfo");
                    var NewTr = NewTable.insertRow();
                    NewTr.id = "row" + x;

                    rowCount = document.getElementById("DetailInfo").rows.length;
                    CountRows = parseInt(rowCount);
                    //序号
                    var newTD0 = NewTr.insertCell();
                    newTD0.className = "textright";
                    newTD0.style.width = "5%";
                    newTD0.align = "center";
                    newTD0.innerHTML = CountRows;
                    //职责部门
                    var newTD1 = NewTr.insertCell();
                    newTD1.className = "textright";
                    newTD1.style.width = "10%";
                    newTD1.align = "center";
                    newTD1.innerHTML = "<select id='FDepartment" + x + "' name='FDepartment" + x + "' style='width:100px;'></select>";
                    var FDepart = document.getElementById("FDepartment" + x);
                    for (var h = 0; h < sel1.length; h++) {
                        var opinonfdepart = document.createElement("option");
                        opinonfdepart.value = sel1[h];
                        if (sel1[h] == json[g].FDepartment) {
                            opinonfdepart.selected = true;
                        }
                        opinonfdepart.innerHTML = val1[h];

                        FDepart.appendChild(opinonfdepart);
                    }

                    //姓名
                    var newTD2 = NewTr.insertCell();
                    newTD2.className = "textright";
                    newTD2.style.width = "8%";
                    newTD2.align = "center";
                    newTD2.innerHTML = "<input type='text' name='PName" + x + "' id='PName" + x + "' value='" + json[g].PName + "' style='width:70px;text-align:center;'/><span style='color: red;'>*</span>";

                    //部门
                    var newTD3 = NewTr.insertCell();
                    newTD3.className = "textright";
                    newTD3.style.width = "8%";
                    newTD3.align = "center";
                    newTD3.innerHTML = "<select id='Department" + x + "' name='Department" + x + "' style='width:100px;'></select>";

                    var select2 = document.getElementById("Department" + x);
                    for (var j = 0; j < selment.length; j++) {
                        var selOPinons = document.createElement("option");
                        selOPinons.value = selment[j];
                        if (selment[j] == json[g].Department)
                            selOPinons.selected = true;
                        selOPinons.innerHTML = valment[j];//配置数据
                        select2.appendChild(selOPinons);
                    }

                    //职位
                    var newTD4 = NewTr.insertCell();
                    newTD4.className = "textright";
                    newTD4.style.width = "10%";
                    newTD4.align = "center";
                    newTD4.innerHTML = "<select id='Job" + x + "' name='Job" + x + "' style='width:100px;'></select>";
                    var Job = document.getElementById("Job" + x);
                    for (var i = 0; i < seljob.length; i++) {
                        var job = document.createElement("option");
                        job.value = seljob[i];
                        if (seljob[i] == json[g].Job) {
                            job.selected = true;
                        }
                        job.innerHTML = valjob[i];
                        Job.appendChild(job);
                    }

                    //座机
                    var newTD5 = NewTr.insertCell();
                    newTD5.className = "textright";
                    newTD5.style.width = "13%";
                    newTD5.align = "center";
                    newTD5.innerHTML = "<input type='text' id='Phone" + x + "' name='Phone" + x + "' value='" + json[g].Phone + "' style='width:100px;text-align:center;'/><span style='color: red;'>*</span>";

                    //手机
                    var newTD6 = NewTr.insertCell();
                    newTD6.className = "textright";
                    newTD6.style.width = "13%";
                    newTD6.align = "center";
                    newTD6.innerHTML = "<input type='text' id='Mobile" + x + "' name='Mobile" + x + "'  value='" + json[g].Mobile + "' style='width:100px;text-align:center;'/><span style='color: red;'>*</span>";

                    //邮箱
                    var newTD8 = NewTr.insertCell();
                    newTD8.className = "textright";
                    newTD8.style.width = "13%";
                    newTD8.align = "center";
                    newTD8.innerHTML = "<input type='text' id='Email" + x + "' name='Email" + x + "' value='" + json[g].Email + "' style='width:125px;text-align:center;'/><span style='color: red;'>*</span>";

                    //操作
                    var newTD9 = NewTr.insertCell();
                    newTD9.className = "textright";
                    newTD9.style.width = "7%";
                    newTD9.align = "center";
                    newTD9.innerHTML = "<a onclick='deleteTr(" + NewTr.id + ")' style='color:blue;cursor:pointer;text-align:center;'>删除</a>";

                    x++;
                }
            }
        }
    });

    $("#updateMsg").submit(function () {
        var taleRow = document.getElementById("DetailInfo").rows.length;
        var options = {
            url: "UpdateInfo",
            data: { taleRow: taleRow },
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    $("#StrSid").val($("#Sid").val());
                    alert(data.Msg);
                    window.parent.frames["iframeRight"].reload();
                    window.parent.frames["iframeRight"].reloadRefresh();
                    window.parent.ClosePop();
                }
                else {
                    alert(data.Msg);
                }
            }
        };
        $("#updateMsg").ajaxSubmit(options);
        return false;
    })
    $("#charge").click(function () {
        var DeclareDate = document.getElementById("DeclareDate").value;
        var SupplierType = document.getElementById("SupplierType").value;
        var OtherType = document.getElementById("OtherType").value;
        var COMNameC = document.getElementById("COMNameC").value;
        var COMShortName = document.getElementById("COMShortName").value;
        var COMWebsite = document.getElementById("COMWebsite").value;
        var COMArea = document.getElementById("COMArea").value;
        var COMNameE = document.getElementById("COMNameE").value;
        var COMCountry = document.getElementById("COMCountry").value;
        var COMRAddress = document.getElementById("COMRAddress").value;
        var COMCreateDate = document.getElementById("COMCreateDate").value;
        var TaxRegistrationNo = document.getElementById("TaxRegistrationNo").value;
        var BusinessLicenseNo = document.getElementById("BusinessLicenseNo").value;
        var ComAddress = document.getElementById("ComAddress").value;
        var COMLegalPerson = document.getElementById("COMLegalPerson").value;
        var COMFactoryAddress = document.getElementById("COMFactoryAddress").value;
        var COMFactoryArea = document.getElementById("COMFactoryArea").value;
        var OrganizationCode = document.getElementById("OrganizationCode").value;
        var COMGroup = document.getElementById("COMGroup").value;
        var RegisteredCapital = document.getElementById("RegisteredCapital").value;
        var CapitalUnit = document.getElementById("CapitalUnit").value;
        var IsCooperate = $("input[name='IsCooperate']:checked").val();
        var BankName = document.getElementById("BankName").value;
        var BankAccount = document.getElementById("BankAccount").value;
        var StaffNum = document.getElementById("StaffNum").value;
        var EnterpriseType = document.getElementById("EnterpriseType").value;
        var Turnover = document.getElementById("Turnover").value;
        var DevelopStaffs = document.getElementById("DevelopStaffs").value;
        var QAStaffs = document.getElementById("QAStaffs").value;
        var ProduceStaffs = document.getElementById("ProduceStaffs").value;
        var ProductLineNum = document.getElementById("ProductLineNum").value;
        var WorkTime_Start = document.getElementById("WorkTime_Start").value;
        var WorkTime_End = document.getElementById("WorkTime_End").value;
        var WorkDay_Start = document.getElementById("WorkDay_Start").value;
        var WorkDay_End = document.getElementById("WorkDay_End").value;
        var BusinessScope = document.getElementById("BusinessScope").value;
        var Ranking = document.getElementById("Ranking").value;
        var AnnualOutput = document.getElementById("AnnualOutput").value;
        var AnnualOutputValue = document.getElementById("AnnualOutputValue").value;
        var MainClient = document.getElementById("MainClient").value;
        var Achievement = document.getElementById("Achievement").value;
        //var Award = document.getElementById("Award").value;
        var Fax = document.getElementById("FAX").value;
        var CapitalUnit = document.getElementById("CapitalUnit").value;
        //拼接字符串
        var BusinessDistribute = $('input[name=guonei]:checkbox:checked').val();
        var BillingWay = $('input[name=billway]:checkbox:checked').val();
        var HasRegulation = $("input[name='HasRegulation']:checked").val();
        var IsrankingIn5 = $("input[name='IsrankingIn5']:checked").val();
        var RankingType = $("input[name='RankingType']:checked").val();
        var ScaleType = $('input[name=scale]:checkbox:checked').val();
        var QualityStandard = $('input[name=stand]:checkbox:checked').val();
        var HasAuthorization = $("input[name='HasAuthorization']:checked").val();
        var HasDrawing = $("input[name='HasDrawing']:checked").val();
        var AgentClass = $('input[name=agent]:checkbox:checked').val();
        var HasImportMaterial = $("input[name='HasImportMaterial']:checked").val();
        var Relation = $('input[name=rela1]:checkbox:checked').val();
        var phone = document.getElementById("Phone").value;
        var mobile = document.getElementById("Mobile").value;
        var email = document.getElementById("Email").value;
        var name = document.getElementById("PName").value;
        // var CreateTime = document.getElementById("CreateTime").value;

        //if (DeclareDate == "") { alert("填报日期不能为空"); return; }
        //if (SupplierType == "") {
        //    //可以编辑其他类别框
        //    alert("供应商类别不能为空");
        //    return;
        //}
        //else if (COMNameC == "") {
        //    alert("公司中文名称不能为空");
        //    return;
        //} else if (COMShortName == "") {
        //    alert("公司简称不能为空");
        //    return;
        //} else if (COMWebsite == "") {
        //    alert("公司网址不能为空");
        //    return;
        //} else if (COMArea == "") { alert("所属地区不能为空"); return; }
        //else if (COMCountry == "") {
        //    alert("所属国家不能为空"); return;
        //}
        //else if (COMRAddress == "") { alert("公司注册地址不能为空"); return; }
        //else if (COMCreateDate == "") { alert("创建日期不能为空"); return; }
        //else if (TaxRegistrationNo == "") { alert("税务登记号不能为空"); return; }
        //else if (BusinessLicenseNo == "") { alert("营业执照不能为空"); return; }
        //else if (ComAddress == "") { alert("公司办公地址不能为空"); return; }
        //else if (COMLegalPerson == "") { alert("法人代表不能为空"); return; }
        //else if (COMFactoryAddress == "") { alert("公司工厂/出货地址不能为空"); return; }
        //else if (OrganizationCode == "") { alert("组织机构代码不能为空"); return; }
        //else if (COMGroup == "") { alert("供应商集团名称不能为空"); return; }
        //else if (RegisteredCapital == 0) { alert("注册资金不能为空"); return; }
        //else if (CapitalUnit == "") { alert("资金单位不能为空"); return; }
        //else if (IsCooperate == "") { alert("是否合作过不能为空"); return; }
        //else if (BankName == "") { alert("开户行名称不能为空"); return; }
        //else if (BankAccount == "") { alert("银行基本账号不能为空"); return; }
        //else if (StaffNum == "") { alert("公司总人数不能为空"); return; }
        //else if (EnterpriseType == "") { alert("企业类型不能为空"); return; }
        //else if (BusinessDistribute != 0 && BusinessDistribute != 1 && BusinessDistribute != 2 && BusinessDistribute != 3) { alert("业务分布不能为空"); return; }
        //else if (BillingWay != 0 && BillingWay != 1 && BillingWay != 2) { alert("开票方式不能为空"); return; }
        //else if (Turnover == "") { alert("去年营业额不能为空"); return; }
        //else if (DevelopStaffs == "") { alert("研发人员数量不能为空"); return; }
        //else if (QAStaffs == "") { alert("质量人员数量不能为空"); return; }
        //else if (ProduceStaffs == "") { alert("生产人员数量不能为空"); return; }
        //else if (Relation != 0 && Relation != 1 && Relation != 2 && Relation != 3 && Relation != 4) { alert("供需关系不能为空"); return; }
        //else if (WorkTime_Start == "") { alert("工作开始时间不能为空"); return; }
        //else if (WorkTime_End == "") { alert("工作结束时间不能为空"); return; }
        //else if (WorkDay_Start == "") { alert("工作日开始时间不能为空"); return; }
        //else if (WorkDay_End == "") { alert("工作日结束时间不能为空"); return; }
        //else if (BusinessScope == "") { alert("经营范围不能为空"); return; }
        //else if (ScaleType != 0 && ScaleType != 1 && ScaleType != 2 && ScaleType != 3) { alert("经营品种不能为空"); return; }
        //else if (QualityStandard != 0 && QualityStandard != 1 && QualityStandard != 2 && QualityStandard != 3) { alert("产品质量执行标准不能为空"); return; }
        //if (IsrankingIn5 == "0") {
        //    RankingType = $("input[name='RankingType']:checked").val();
        //    Ranking = document.getElementById("Ranking").value;
        //    if (RankingType != 0 && RankingType != 2) {
        //        alert("排名类型不能为空");
        //        return;
        //    }
        //    if (Ranking == "") {
        //        alert("排名不能为空");
        //        return;
        //    }
        //}
        //if (HasAuthorization == "0") {
        //    HasDrawing = $("input[name='HasDrawing']:checked").val();
        //    AgentClass = $('input[name=agent]:checkbox:checked').val();
        //    HasImportMaterial = $("input[name='HasImportMaterial']:checked").val();
        //    if (HasDrawing == "") {
        //        alert("是否有图纸不能为空");
        //        return;
        //    }
        //    if (AgentClass != 0 && AgentClass != 1 && AgentClass != 2) {
        //        alert("代理级别不能为空");
        //        return;
        //    }
        //    if (HasImportMaterial == "") {
        //        alert("产品进货证明不能为空");
        //        return;
        //    }
        //}
        //var leng = document.getElementById("DetailInfo").rows.length;
        //if (leng == 0) {
        //    alert("还没有添加工作内容，请点击‘添加'，进行操作");
        //    return;
        //}
        var tbody = document.getElementById("DetailInfo");
        for (var i = 0; i < tbody.rows.length; i++) {
            var FDepartment = $("#FDepartment").val();
            var PName = $("#PName").val();
            var Department = $("#Department").val();
            var Job = $("#Job").val();
            var Phone = $("#Phone").val();
            var Mobile = $("#Mobile").val();
        }
        var res = confirm("确定保存供应商信息吗？");
        if (res) {
            $("#updateMsg").submit();
        } else {
            return;
        }
    });
    Load();
    LoadBillway();
    LoadRelation();
    LoadHasRegulation();
    LoadIsrankingIn5();
    LoadRankingType();
    LoadScaleType();
    LoadQualityStandard();
    LoadHasAuthorization();
    LoadHasDrawing();
    LoadAgentClass();
    LoadHasImportMaterial();
    LoadUnit(); //加载供应商联系人
    ck_Function();
    // LoadPerson();
    $(".IsrankingIn5").change(function () {
        var IsrankingIn5 = $("input[name='IsrankingIn5']:checked").val();//排名前5

        if (IsrankingIn5 == "1") {
            $(".RankingType").attr('disabled', true);
            $("#Ranking").attr('disabled', true);
        }
        else {
            $(".RankingType").attr('disabled', false);
            $("#Ranking").attr('disabled', false);
        }
    })
    $(".Certity").change(function () {
        var ThreeCertity = $("input[name='Certity']:checked").val();//是否三证合一

        if (ThreeCertity == "1") {
            $("#OrganizationCode").attr('disabled', false);
            $("#TaxRegistrationNo").attr('disabled', false);
            $("#BusinessLicenseNo").attr('disabled', false);
            $("#ThreeCertity").attr("disabled", 'disabled');
        }
        else {
            $("#OrganizationCode").attr('disabled', 'disabled');
            $("#TaxRegistrationNo").attr('disabled', 'disabled');
            $("#BusinessLicenseNo").attr('disabled', 'disabled');
            $("#ThreeCertity").attr("disabled", false);
        }
    });
    $("#SupplierType").change(function () {
        //默认其他可以编辑
        var sup = $("#SupplierType").find("option:selected").text();
        if (sup == "其他") {
            $("#OtherType").attr('disabled', false);
        }
        else {
            $("#OtherType").attr('disabled', 'disabled');
        }
    });
    $("#Add").click(function () {
        LoadUnit();
        //rowCount++;
        var NewTable = document.getElementById("DetailInfo");
        var NewTr = NewTable.insertRow();
        NewTr.id = "row" + i;

        rowCount = document.getElementById("DetailInfo").rows.length - 1;
        CountRows = parseInt(rowCount) + 1;
        //序号
        var newTD0 = NewTr.insertCell();
        newTD0.className = "textright";
        newTD0.style.width = "5%";
        newTD0.align = "center";
        newTD0.innerHTML = CountRows;
        //职责部门
        var newTD1 = NewTr.insertCell();
        newTD1.className = "textright";
        newTD1.style.width = "10%";
        newTD1.align = "center";
        newTD1.innerHTML = strSelect1;
        var FDepartment = document.getElementById("FDepartment" + rowCount);
        FDepartment.setAttribute("name", "FDepartment" + rowCount);
        //姓名
        var newTD2 = NewTr.insertCell();
        newTD2.className = "textright";
        newTD2.style.width = "8%";
        newTD2.align = "center";
        newTD2.innerHTML = "<input type='text' name='PName" + rowCount + "' id='PName" + rowCount + "' style='width:70px;text-align:center;'/><span style='color: red;'>*</span>";
        //部门
        var newTD3 = NewTr.insertCell();
        newTD3.className = "textright";
        newTD3.style.width = "8%";
        newTD3.align = "center";
        newTD3.innerHTML = strSelect2;
        var Department = document.getElementById("Department" + rowCount);
        Department.setAttribute("name", "Department" + rowCount);

        //职位
        var newTD4 = NewTr.insertCell();
        newTD4.className = "textright";
        newTD4.style.width = "10%";
        newTD4.align = "center";
        newTD4.innerHTML = strSelect3;
        var Job = document.getElementById("Job" + rowCount);
        Job.setAttribute("name", "Job" + rowCount);
        //座机
        var newTD5 = NewTr.insertCell();
        newTD5.className = "textright";
        newTD5.style.width = "13%";
        newTD5.align = "center";
        newTD5.innerHTML = "<input type='text' id='Phone" + rowCount + "' name='Phone" + rowCount + "' style='width:100px;text-align:center;'/><span style='color: red;'>*</span>";
        //手机
        var newTD6 = NewTr.insertCell();
        newTD6.className = "textright";
        newTD6.style.width = "13%";
        newTD6.align = "center";
        newTD6.innerHTML = "<input type='text' id='Mobile" + rowCount + "' name='Mobile" + rowCount + "' style='width:100px;text-align:center;'/><span style='color: red;'>*</span>";

        var newTD8 = NewTr.insertCell();
        newTD8.className = "textright";
        newTD8.style.width = "13%";
        newTD8.align = "center";
        newTD8.innerHTML = "<input type='text' id='Email" + rowCount + "' name='Email" + rowCount + "' style='width:125px;text-align:center;'/><span style='color: red;'>*</span>";
        //操作
        var newTD9 = NewTr.insertCell();
        newTD9.className = "textright";
        newTD9.style.width = "7%";
        newTD9.align = "center";
        newTD9.innerHTML = "<a onclick='deleteTr(" + NewTr.id + ")' style='color:blue;cursor:pointer;text-align:center;'>删除</a>";
        i++;
    })

})
function LoadUnit() {
    var count = document.getElementById("DetailInfo").rows.length;

    $.ajax({
        url: "GetContractSize",
        type: "post",
        data: { Type: "FDepartment" },
        dataType: "Json",
        async: false,
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strSelect1 = "<select id='FDepartment" + count + "' style='width:100px;text-align:center;'>";
                for (i = 0; i < sel.length; i++) {
                    strSelect1 += "<option value=" + sel[i] + " style='text-align:center;'>" + val[i] + "</option>";
                }
                strSelect1 += "</select>";
            }
            else {
                return;
            }
        }
    });
    $.ajax({
        url: "GetContractSize",
        type: "post",
        data: { Type: "Department" },
        dataType: "Json",
        async: false,
        success: function (data) {

            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strSelect2 = "<select id='Department" + count + "' style='width:100px;text-align:center;'>";
                for (var i = 0; i < sel.length; i++) {
                    strSelect2 += "<option value=" + sel[i] + ">" + val[i] + "</option>";
                }
                strSelect2 += "</select>";
            }
            else {
                return;
            }

        }
    });
    $.ajax({
        url: "GetContractSize",
        type: "post",
        data: { Type: "Job" },
        dataType: "Json",
        async: false,
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strSelect3 = "<select id='Job" + count + "' style='width:100px;'>";
                for (var i = 0; i < sel.length; i++) {
                    strSelect3 += "<option value=" + sel[i] + ">" + val[i] + "</option>";
                }
                strSelect3 += "</select>";
            }
            else {
                return;
            }
        }
    });
}
function changes(op) {
    if (op == "BDB") {
        var res = "";
        $('input[name=guonei]:checkbox:checked').each(function () {
            res += $(this).val() + ",";
        });
        var arrcbYw = res.split(',');
        var txtYW = "";
        for (var i = 0; i < arrcbYw.length - 1; i++) {
            txtYW += $("#txt" + arrcbYw[i]).val() + ",";
        }
        document.getElementById("BusinessDistribute").value = res + ":" + txtYW;//打印 汉字+数字
    }
    if (op == "BW") {
        var way = "";
        $('input[name=billway]:checkbox:checked').each(function () {
            way += $(this).val() + ",";
        });
        var arr = way.split(',');
        var txtbill = "";
        for (var i = 0; i < arr.length - 1; i++) {
            txtbill += $("#bill" + arr[i] + "").val() + ",";
        }
        document.getElementById("BillingWay").value = way + ":" + txtbill;
    }
    //供需关系
    if (op == "rla") {
        var rela = "";
        $('input[name=rela1]:checkbox:checked').each(function () {
            rela += $(this).val() + ",";
        })
        document.getElementById("Relation").value = rela;
    }
    //经营品种分类
    if (op == "st") {
        var sal = "";
        $('input[name=scale]:checkbox:checked').each(function () {
            sal += $(this).val() + ",";
        })
        document.getElementById("ScaleType").value = sal;
    }
    //产品质量执行标准
    if (op == "qsd") {
        var stand = "";
        $('input[name=stand]:checkbox:checked').each(function () {
            stand += $(this).val() + ",";
        })
        document.getElementById("QualityStandard").value = stand;
    }
    //产品代理级别
    if (op == "acl") {
        var agent = "";
        $('input[name=agent]:checkbox:checked').each(function () {
            agent += $(this).val() + ",";
        })
        document.getElementById("AgentClass").value = agent;
    }
}
function ck_Function() {
    $('.ckb2').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("HasRegulation").value = res;
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
            document.getElementById("IsrankingIn5").value = res;
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
            document.getElementById("RankingType").value = res;
            $('.ckb4').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb4').attr("disabled", false);
        }
    })
    $('.ckb7').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("HasAuthorization").value = res;
            $('.ckb7').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb7').attr("disabled", false);
        }
    })
    $('.ckb8').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("HasDrawing").value = res;
            $('.ckb8').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb8').attr("disabled", false);
        }
    })
    $('.ckb10').click(function () {
        var ckbname = this.name;
        if (this.checked) {
            var getId = document.getElementById(this.name);
            var res = ckbname;
            document.getElementById("HasImportMaterial").value = res;
            $('.ckb10').each(function () {
                if (this.name != ckbname) {
                    $(this).attr("disabled", true);
                }
            });
        } else {
            $('.ckb10').attr("disabled", false);
        }
    })
}


function Load() {
    var Buss = $("#BusinessDistribute").val();
    var arrBuss = new Array();
    arrBuss = Buss.split(':');
    var strCbBuss = arrBuss[0];//汉字
    var strPerBuss = arrBuss[1];//数字

    var arrcbbuss = strCbBuss.split(',');//0,2,3
    var arrPerBuss = strPerBuss.split(',');//23,23,33
    for (var i = 0; i < arrcbbuss.length ; i++) {
        var aa = document.getElementsByName("guonei");
        var per = document.getElementsByName("txtbuss");
        for (var j = 0; j < aa.length; j++) {
            if (aa[j].value == arrcbbuss[i]) { //汉字
                aa[j].checked = true;
                per[j].value = arrPerBuss[i];
            }
            //if (per[j].id == "txt" + aa[j].value ) {
            //    per[j].value = arrPerBuss[j]; //百分比
            //}
        }

    }

}
function LoadBillway() {
    var Buss = $("#BillingWay").val();
    var arrBuss = new Array();
    arrBuss = Buss.split(':');
    var strCbBuss = arrBuss[0];//汉字
    var strPerBuss = arrBuss[1];//数字
    var arrcbbuss = strCbBuss.split(',');
    var arrPerBuss = strPerBuss.split(',');
    for (var i = 0; i < arrcbbuss.length ; i++) {
        var aa = document.getElementsByName("billway");
        var per = document.getElementsByName("txtbill");
        for (var j = 0; j < aa.length; j++) {
            if (aa[j].value == arrcbbuss[i]) {
                aa[j].checked = true;
                per[j].value = arrPerBuss[i];
            }
            //if (per[j].id == "bill" + aa[j].value) {
            //    per[j].value = arrPerBuss[j];
            //}
        }
    }
}

//供需关系
function LoadRelation() {
    var relation = $("#Relation").val();//拿到值需分割循环赋值
    var arrre = new Array();
    arrre = relation.split(',');//分割成多个4个多一个逗号
    for (var i = 0; i < arrre.length; i++) {
        var Judge = document.getElementsByName("rela1");//5个checkbox
        for (var j = 0; j < Judge.length; j++) {
            if (Judge[j].value == arrre[i]) {
                Judge[j].checked = true;
            }
        }
    }
}
function LoadHasRegulation() {
    var regulation = $("#HasRegulation").val();
    var Judge = document.getElementsByName("0");
    for (var i = 0; i < Judge.length; i++) {
        if (Judge[i].value == regulation[i]) {
            Judge[i].checked = true;
        } else {
            Judge[i].checked = true;
        }
    }
}
function LoadIsrankingIn5() {
    var isrank5 = $("#IsrankingIn5").val();
    var Judge = document.getElementsByName("0");
    for (var i = 0; i < Judge.length; i++) {
        if (Judge[i].value == isrank5[i]) {
            Judge[i].checked = true;
        } else {
            Judge[i].checked = true;
        }
    }
}
function LoadRankingType() {
    var ranktype = $("#RankingType").val();
    var Judge = document.getElementsByName("0");
    for (var i = 0; i < Judge.length; i++) {
        if (Judge[i].value == ranktype[i]) {
            Judge[i].checked = true;
        } else {
            Judge[i].checked = true;
        }
    }
}
//经营产品分类
function LoadScaleType() {
    var scaltype = $("#ScaleType").val();
    var arrtype = new Array();
    arrtype = scaltype.split(',');
    for (var i = 0; i < arrtype.length; i++) {
        var Judge = document.getElementsByName("scale");
        for (var j = 0; j < Judge.length; j++) {
            if (Judge[j].value == arrtype[i]) {
                Judge[j].checked = true;
            }
        }
    }
}
//产品质量执行标准
function LoadQualityStandard() {
    var stand = $("#QualityStandard").val();
    var arrstand = new Array();
    arrstand = stand.split(',');
    for (var j = 0; j < arrstand.length; j++) {
        var Judge = document.getElementsByName("stand");
        for (var i = 0; i < Judge.length; i++) {
            if (Judge[i].value == arrstand[j]) {
                Judge[i].checked = true;
            }
            //else {
            //    Judge[i].checked = false;
            //}
        }
    }
}
function LoadHasAuthorization() {
    var author = $("#HasAuthorization").val();
    var Judge = document.getElementsByName("0");
    for (var i = 0; i < Judge.length; i++) {
        if (Judge[i].value == author[i]) {
            Judge[i].checked = true;
        } else {
            Judge[i].checked = true;
        }
    }
}
function LoadHasDrawing() {
    var hasdrowing = $("#HasDrawing").val();
    var Judge = document.getElementsByName("0");
    for (var i = 0; i < Judge.length; i++) {
        if (Judge[i].value == hasdrowing[i]) {
            Judge[i].checked = true;
        } else {
            Judge[i].checked = true;
        }
    }
}
//代理级别
function LoadAgentClass() {
    var agentclass = $("#AgentClass").val();
    var arrcalss = new Array();
    arrcalss = agentclass.split(',');
    for (var i = 0; i < arrcalss.length; i++) {
        var Judge = document.getElementsByName("agent");
        for (var j = 0; j < Judge.length; j++) {
            if (Judge[j].value == arrcalss[i]) {
                Judge[j].checked = true;
            }
            //else {
            //    Judge[j].checked = false;
            //}
        }
    }
}
function LoadHasImportMaterial() {
    var importMater = $("#HasImportMaterial").val();
    var Judge = document.getElementsByName("0");
    for (var i = 0; i < Judge.length; i++) {
        if (Judge[i].value == importMater[i]) {
            Judge[i].checked = true;
        } else {
            Judge[i].checked = true;
        }
    }
}
function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}
//时间格式
function GetTime() {
    var mydate = document.getElementById("DeclareDate").value;//.substring(0, 10);
    //alert(mydate);
    var s = mydate.getFullYear() + "-";
    if (mydate.getMonth() < 10) {
        s += '0' + (mydate.getMonth() + 1) + "-";
    }
    if (mydate.getDate() < 10)
        s += '0' + mydate.getDate();
    document.getElementById("DeclareDate").value = year + "-" + month1 + "-" + day;

}
function deleteTr(obj) {
    var one = confirm("确认删除该行吗？");
    if (one == false)
        return;
    else {
        if (obj) obj.parentNode.removeChild(obj);
    }
}

//供需关系解释说明
function Show(obj) {
    var X = $("#" + obj).offset().top + 21;
    var Y = $("#" + obj).offset().left;
    if (obj == "0") {
        $("#sh").html("具有很强的产品开发能力,与之的采购业务对本公司非常重要，采购的产品处于寡头垄断市场");
        $("#sh").attr("style", "left:" + Y + "px;top:" + X + "px;width:" + $("#sh").width() + "px; position:absolute;background-color:#ddd6d6;");
    }
    else if (obj == "1") {
        $("#sh").html("采购业务对本公司和供应商都非常重要，已合作多年");
        $("#sh").attr("style", "left:" + Y + "px;top:" + X + "px;width:" + $("#sh").width() + "px; position:absolute;background-color:#ddd6d6;");
    }
    else if (obj == "2") {
        $("#sh").html("采购业务对供应商非常重要，对本公司却不是十分重要");
        $("#sh").attr("style", "left:" + Y + "px;top:" + X + "px;width:" + $("#sh").width() + "px; position:absolute;background-color:#ddd6d6;");
    }
    else if (obj == "3") {
        $("#sh").html("采购业务对供应商来说无关紧要，对于本公司来说却非常重要");
        $("#sh").attr("style", "left:" + Y + "px;top:" + X + "px;width:" + $("#sh").width() + "px; position:absolute;background-color:#ddd6d6;");
    }
    else {
        $("#sh").html("采购业务对本公司和供应商不重要，供应商可以很方便选择更换");
        $("#sh").attr("style", "left:" + Y + "px;top:" + X + "px;width:" + $("#sh").width() + "px; position:absolute;background-color:#ddd6d6;");
    }

}
function Noshow(tq) {
    if (tq == "0")
        document.getElementById("sh").style.display = 'none';
    else if (tq == "1")
        document.getElementById("sh").style.display = 'none';
    else if (tq == "2")
        document.getElementById("sh").style.display = 'none';
    else if (tq == "3")
        document.getElementById("sh").style.display = 'none';
    else
        document.getElementById("sh").style.display = 'none';
}







