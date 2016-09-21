
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0, oldcerID = 0, oldProID = 0, oldSerID = 0, newSelID = 0, num = 0, oldAward = 0, oldPrice = 0;
var FileName, FileType, CName, CType, ProName, ProType, ServiceName, ServiceDesc, type, i, strSelect1;
var COMFactory;
var op = "";
$(document).ready(function () {

    $("#pageContent").height($(window).height());
    $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
    $("#list2").jqGrid("setGridWidth", $("#bor2").width() - 30, false);
    $("#list3").jqGrid("setGridWidth", $("#bor3").width() - 30, false);
    $("#ThreeCertity").attr("disabled", 'disabled');
    $("#two").hide();
    $("#three").hide();
    $("#four").hide();
    $("#five1").hide();
    $("#six").hide();
    jq();
    GetTime();
    jqCertifi();
    jqPro();
    jqServer();
    jqAward();
    jqPrice();
    $("#AddZiZhi").click(function () {
        var sid = $("#StrSid").val();
        if (sid == "" || sid == "undefined") {
            alert("请选择要添加资质的供应商");
            return;
        }
        window.parent.OpenDialog("新增资质", "../SuppliesManage/AddFileInfo?sid=" + sid, 550, 350, '');
    })

    $("#AddZhenShu").click(function () {
        var sid = $("#StrSid").val();
        if (sid == "" || sid == "undefined") {
            alert("请选择要添加证书的供应商");
            return;
        }
        window.parent.OpenDialog("新增证书", "../SuppliesManage/AddCertificate?sid=" + sid, 500, 350, '');
    })
    $("#AddPro").click(function () {
        var sid = $("#StrSid").val();
        if (sid == "" || sid == "undefined") {
            alert("请选择您要添加产品的供应商");
            return;
        }
        window.parent.OpenDialog("新增产品", "../SuppliesManage/AddPro?sid=" + sid, 500, 420, '');
    })
    $("#AddServer").click(function () {
        var sid = $("#StrSid").val();
        if (sid == "" || sid == "undefined") {
            alert("请选择您要添加服务的供应商");
            return;
        }
        window.parent.OpenDialog("新增服务", "../SuppliesManage/AddServer?sid=" + sid, 520, 250, '');
    })
    $("#AddWard").click(function () {
        var sid = $("#StrSid").val();
        if (sid == "" || sid == "undefined") {
            alert("请选择要上传曾获奖项的供应商");
            return;
        }
        window.parent.OpenDialog("上传曾获奖项", "../SuppliesManage/AddAwardInfo?sid=" + sid, 550, 150, '');
    })
    $("#AddPrice").click(function () {
        var sid = $("#StrSid").val();
        if (sid == "" || sid == "undefined") {
            alert("请选择要上传报价/比价单的供应商");
            return;
        }
        window.parent.OpenDialog("上传报价/比价单", "../SuppliesManage/AddPriceInfo?sid=" + sid, 550, 150, '');
    })
    loadPerson();

    $("#zizhi").click(function () {
        this.className = "btnTw";
        $('#zhenshu').attr("class", "btnTh");
        $('#product').attr("class", "btnTh");
        $('#server').attr("class", "btnTh");
        $('#Price').attr("class", "btnTh");
        $('#Ward').attr("class", "btnTh");

        $("#one").css("display", "");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five1").css("display", "none");
        $("#six").css("display", "none");

    });
    $("#zhenshu").click(function () {
        this.className = "btnTw";
        $('#zizhi').attr("class", "btnTh");
        $('#product').attr("class", "btnTh");
        $('#server').attr("class", "btnTh");
        $('#Price').attr("class", "btnTh");
        $('#Ward').attr("class", "btnTh");

        $("#two").css("display", "");
        $("#one").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five1").css("display", "none");
        $("#six").css("display", "none");

    });
    $("#product").click(function () {
        this.className = "btnTw";
        $('#zizhi').attr("class", "btnTh");
        $('#zhenshu').attr("class", "btnTh");
        $('#server').attr("class", "btnTh");
        $('#Price').attr("class", "btnTh");
        $('#Ward').attr("class", "btnTh");

        $("#three").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#four").css("display", "none");
        $("#five1").css("display", "none");
        $("#six").css("display", "none");

    });
    $("#server").click(function () {
        this.className = "btnTw";
        $('#zizhi').attr("class", "btnTh");
        $('#zhenshu').attr("class", "btnTh");
        $('#product').attr("class", "btnTh");
        $('#Price').attr("class", "btnTh");
        $('#Ward').attr("class", "btnTh");

        $("#four").css("display", "");
        $("#one").css("display", "none");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#five1").css("display", "none");
        $("#six").css("display", "none");

    });
    $("#Ward").click(function () {
        this.className = "btnTw";
        $('#zhenshu').attr("class", "btnTh");
        $('#product').attr("class", "btnTh");
        $('#server').attr("class", "btnTh");
        $('#Price').attr("class", "btnTh");
        $('#zizhi').attr("class", "btnTh");

        $("#five1").css("display", "");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#one").css("display", "none");
        $("#six").css("display", "none");

    })
    $("#Price").click(function () {
        this.className = "btnTw";
        $('#zhenshu').attr("class", "btnTh");
        $('#product').attr("class", "btnTh");
        $('#server').attr("class", "btnTh");
        $('#Ward').attr("class", "btnTh");
        $('#zizhi').attr("class", "btnTh");

        $("#six").css("display", "");
        $("#two").css("display", "none");
        $("#three").css("display", "none");
        $("#four").css("display", "none");
        $("#five1").css("display", "none");
        $("#one").css("display", "none");

    })
    $("#SuForm").submit(function () {
        var taleRow = document.getElementById("DetailInfo").rows.length;
        var options = {
            url: "InsertSuBas",
            data: { taleRow: taleRow },
            type: 'POST',
            async: false,
            success: function (data) {
                if (data.success == "true") {
                    $("#StrSid").val($("#Sid").val());
                    alert(data.Msg);//保存成功
                    $("#Save").attr('disabled', 'disabled');

                }
                else {
                    alert(data.Msg);
                    $("#Save").attr('disabled', false);
                }
            }
        };
        $("#SuForm").ajaxSubmit(options);
        return false;
    });
    $("#Save").click(function () {
        //var Relation = $('input[name=rela1]:checked').val();
        //alert(Relation);


        var res = "";
        var txtYW = "";
        $('input[name=guonei]:checkbox:checked').each(function () {   //美国，null,中国，韩国
            res += $(this).val() + ",";
        });
        var arrcbYw = res.split(',');
        for (var i = 0; i < arrcbYw.length - 1; i++) {
            txtYW += $("#txt" + arrcbYw[i]).val() + ",";
            if (res != "" && $("#txt" + arrcbYw[i]).val() == "") {
                alert("百分比不能为空"); break;
            }
        }
        document.getElementById("BusinessDistribute").value = res + ":" + txtYW;//汉字：数字
        var way = "";
        $('input[name=billway]:checkbox:checked').each(function () {
            way += $(this).val() + ",";
        });
        var arr = way.split(',');
        var txtbill = "";
        for (var i = 0; i < arr.length - 1; i++) {
            txtbill += $("#bill" + arr[i] + "").val() + ",";
            if ($("#bill" + arr[i] + "").val() == "" && way != "") {
                alert("开票方式百分比不能为空"); break;
            }
        }
        document.getElementById("BillingWay").value = way + ":" + txtbill;

        var sum;
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
        var COMCreateDate = document.getElementById("COMCreateDate").value;//输入的时间
        var arr = COMCreateDate.split('-');
        var time = new Date();
        var today = new Date();
        time.setFullYear(arr[0], arr[1] - 1, arr[2]);//输入的时间

        //var TaxRegistrationNo = document.getElementById("TaxRegistrationNo").value;
        //var BusinessLicenseNo = document.getElementById("BusinessLicenseNo").value;
        var ComAddress = document.getElementById("ComAddress").value;
        var COMLegalPerson = document.getElementById("COMLegalPerson").value;
        var COMFactoryAddress = document.getElementById("COMFactoryAddress").value;
        var COMFactoryArea = document.getElementById("COMFactoryArea").value;

        var OrganizationCode = document.getElementById("OrganizationCode").value;
        var COMGroup = document.getElementById("COMGroup").value;
        var RegisteredCapital = document.getElementById("RegisteredCapital").value;
        var IsCooperate = $("input[name='IsCooperate']:checked").val();

        var BankName = document.getElementById("BankName").value;
        var BankAccount = document.getElementById("BankAccount").value;
        var StaffNum = document.getElementById("StaffNum").value;//总人数
        var staff = parseInt(StaffNum);
        var EnterpriseType = document.getElementById("EnterpriseType").value;
        var Turnover = document.getElementById("Turnover").value;
        var DevelopStaffs = document.getElementById("DevelopStaffs").value;//研发人数
        sum += parseInt(DevelopStaffs);
        var intDevelop = parseInt(DevelopStaffs);
        var QAStaffs = document.getElementById("QAStaffs").value;//质量人数
        sum += parseInt(QAStaffs);
        var intQastaff = parseInt(QAStaffs);
        var ProduceStaffs = document.getElementById("ProduceStaffs").value;//生产人数
        sum += parseInt(ProduceStaffs);
        var intProduct = parseInt(ProduceStaffs);
        var ProductLineNum = document.getElementById("ProductLineNum").value;
        var WorkTime_Start = document.getElementById("WorkTime_Start").value;
        var WorkTime_End = document.getElementById("WorkTime_End").value;
        var WorkDay_Start = document.getElementById("WorkDay_Start").value;
        var WorkDay_End = document.getElementById("WorkDay_End").value;
        var BusinessScope = document.getElementById("BusinessScope").value;
        var Ranking = document.getElementById("Ranking").value;//名次
        var AnnualOutput = document.getElementById("AnnualOutput").value;
        var AnnualOutputValue = document.getElementById("AnnualOutputValue").value;
        var MainClient = document.getElementById("MainClient").value;
        var Achievement = document.getElementById("Achievement").value;
        //var Award = document.getElementById("Award").value;
        var Fax = document.getElementById("FAX").value;
        var CapitalUnit = document.getElementById("CapitalUnit").value;
        //拼接字符串
        var BusinessDistribute = $('input[name=guonei]:checkbox:checked').val();
        var bus = res + ":" + txtYW;
        var BillingWay = way + ":" + txtbill;
        var bill = $('input[name=billway]:checkbox:checked').val();
        var HasRegulation = $("input[name='HasRegulation']:checked").val();
        var IsrankingIn5 = $("input[name='IsrankingIn5']:checked").val();//排名前5
        var RankingType = $("input[name='RankingType']:checked").val();//排名类型

        var ScaleType = $('input[name=scale]:checked').val(); //$('input[name=scale]:checked').val(); //;//品种分类

        var QualityStandard = $('input[name=stand]:checkbox:checked').val();//$('input[name=stand]:checked').val();////$('input[name=stand]:checked').val();////执行标准
        var ThreeCertity = $("input[name='Certity']:checked").val();//三证合一是否

        var HasAuthorization = $("input[name='HasAuthorization']:checked").val();
        var HasDrawing = "";
        var AgentClass = "";
        var HasImportMaterial = "";
        var Relation = $('input[name=rela1]:checked').val();

        ////var fdepaetment = document.getElementById("FDepartment").value;
        //alert(FDepartment);
        //var depaetment = document.getElementById("Department").value;
        //var job = document.getElementById("Job").value;
        var phone = document.getElementById("Phone").value;
        var mobile = document.getElementById("Mobile").value;
        var email = document.getElementById("Email").value;
        var name = document.getElementById("PName").value;
        var FDepartment = document.getElementById("FDepartment").value;
        var Department = document.getElementById("Department").value;
        var Job = document.getElementById("Job").value;
        //var index = FDepartment.selectedIndex;
        //var fd = FDepartment.options[index].value;
        //alert(fd);
        //alert(Department);
        //alert(Job);
        // if (DeclareDate == "") { alert("填报日期不能为空"); return; }
        if (SupplierType == "") { alert("供应商类别不能为空"); return; }
        else if (COMShortName == "") { alert("公司简称不能为空"); return; }
        else if (COMNameC == "") { alert("公司中文名称不能为空"); return; }
        else if (COMCountry == "") { alert("所属国家不能为空"); return; }
        else if (COMArea == "") { alert("所属城市不能为空"); return; }
        else if (COMRAddress == "") { alert("公司注册地址不能为空"); return; }
        else if (time > today || COMCreateDate == "") { alert("创建日期不能为空或不能大于当前时间"); return; }
        else if (RegisteredCapital == 0) { alert("注册资金不能为空"); return; }
        else if (CapitalUnit == "") { alert("资金单位不能为空"); return; }
            //else if (ThreeCertity == "0") { alert("三证合一编号不能为空"); return; }
            //else if (ThreeCertity == "1") { alert("税务登记号,营业执照,组织机构代码不能为空"); return; }
        else if (Turnover == "") { alert("去年营业额不能为空"); return; }
        else if (ComAddress == "") { alert("公司办公地址不能为空"); return; }
        else if (COMLegalPerson == "") { alert("法人代表不能为空"); return; }
        else if (COMFactoryAddress == "") { alert("公司出厂/出货地址不能为空"); return; }

            //else if (TaxRegistrationNo == "") { alert("税务登记号不能为空"); return; }
            //else if (BusinessLicenseNo == "") { alert("营业执照不能为空"); return; }
            //else if (OrganizationCode == "") { alert("组织机构代码不能为空"); return; }

        else if (IsCooperate != 1 && IsCooperate != 0) { alert("是否合作过不能为空"); return; }//$('#IsCooperate').is(':checked') == false
        else if (BankName == "") { alert("开户行名称不能为空"); return; }
        else if (BankAccount == "") { alert("银行基本账号不能为空"); return; }
        else if (StaffNum == "") { alert("公司总人数不能为空"); return; }
        else if (EnterpriseType == "") { alert("企业类型不能为空"); return; }
        else if (BusinessDistribute != 0 && BusinessDistribute != 1 && BusinessDistribute != 2 && BusinessDistribute != 3 && BusinessDistribute != 4) { alert("业务分布不能为空"); return; }
        else if (bill != 0 && bill != 1 && bill != 2 && bill != 3) { alert("开票方式不能为空"); return; }
        else if (Relation == undefined) { alert("供需关系不能为空"); return; }
        else if (sum > staff || intDevelop > staff) { alert("研发人员数量不能大于公司总人数"); return; }//DevelopStaffs > StaffNum || sum > StaffNum || DevelopStaffs == ""
        else if (sum > staff || intQastaff > staff) { alert("质量人员数量不能大于公司总人数"); return; }
        else if (sum > staff || intProduct > staff) { alert("生产人员数量不能大于公司总人数"); return; }

        else if (BusinessScope == "") { alert("经营范围不能为空"); return; }

        else if (ScaleType == undefined) { alert("供应商规模和经营品种分类不能为空"); return; }
        else if (QualityStandard != 0 && QualityStandard != 1 && QualityStandard != 2 && QualityStandard != 3) { alert("产品质量执行标准不能为空"); return; }

        if (IsrankingIn5 == 0) {//如果是排名在前五的排名都必填，否则下面可以不填

            if (RankingType != 0 && RankingType != 1) {
                alert("排名类型不能为空");
                return;
            }
            if (Ranking == "") {
                alert("排名不能为空");
                return;
            }
        } else {
            RankingType = "";
            Ranking = "";

        }
        if (HasAuthorization == "0") {
            HasDrawing = $("input[name='HasDrawing']:checked").val();
            AgentClass = $('input[name=agent]:checkbox:checked').val();//document.getElementById("AgentClass").value;
            HasImportMaterial = $("input[name='HasImportMaterial']:checked").val();
            if (HasDrawing == "") {
                alert("是否有图纸不能为空");
                return;
            }
            if (AgentClass != 0 && AgentClass != 1 && AgentClass != 2) {
                alert("代理级别不能为空");
                return;
            }
            if (HasImportMaterial == "") {
                alert("产品进货证明不能为空");
                return;
            }
        }
        var leng = document.getElementById("DetailInfo").rows.length;
        if (leng == 0) {
            alert("还没有添加联系人，请先添加联系人后再点击保存");
            return;

        }
        if (FDepartment == "") {
            alert("职责部门不能为空"); return;
        }
        else if (name == "") {
            alert("联系人姓名不能为空"); return;
        }
        else if (Department == "") {
            alert("部门不能为空"); return;
        }
        else if (Job == "") {
            alert("职位不能为空"); return;
        }
        else if (phone == "") {
            alert("座机不能为空"); return;
        } else if (mobile == "") {
            alert("手机号不能为空"); return;
        } else if (email == "") {
            alert("邮箱不能为空"); return;
        }

        var phone = document.getElementById("Phone").value;
        var re = /^0\d{2,3}-?\d{7,8}$/;
        if (re.test(phone)) {

        } else {
            alert("座机号不正确(xxx-1234567)");
            return;
        }
        var sMobile = document.getElementById("Mobile").value;
        var re = /^1\d{10}$/;
        if (re.test(sMobile)) {

        } else {
            alert("输入的手机号不是11位");
            return;
        }
        var email = document.getElementById("Email").value;
        var re = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/
        if (re.test(email)) {
            var res = confirm("确定保存供应商信息吗？");
            if (res) {
                $("#SuForm").submit();
            } else {
                return;
            }
        } else {
            alert("邮箱格式错误(xx@xx.com/cn)");
            return;
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
    });
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

    ck_Function();
    $("#Add").click(function () {

        i++;
        num++;
        var NewTable = document.getElementById("DetailInfo");
        var NewTr = NewTable.insertRow();
        NewTr.id = "row" + i;
        //序号
        var newTD0 = NewTr.insertCell();
        newTD0.className = "textright";
        newTD0.style.width = "5%";
        newTD0.align = "center";
        newTD0.innerHTML = num;
        //职责部门
        var newTD1 = NewTr.insertCell();
        newTD1.className = "textright";
        newTD1.style.width = "10%";
        newTD1.align = "center";
        newTD1.innerHTML = strSelect1;
        var FDepartment = document.getElementById("FDepartment");
        FDepartment.setAttribute("name", "FDepartment");
        //姓名
        var newTD2 = NewTr.insertCell();
        newTD2.className = "textright";
        newTD2.style.width = "8%";
        newTD2.align = "center";
        newTD2.innerHTML = "<input type='text' name='PName' id='PName' style='width:70px;text-align:center;'/><span style='color: red;'>*</span>";
        //部门
        var newTD3 = NewTr.insertCell();
        newTD3.className = "textright";
        newTD3.style.width = "8%";
        newTD3.align = "center";
        newTD3.innerHTML = strSelect2;
        var Department = document.getElementById("Department");
        Department.setAttribute("name", "Department");
        //职位
        var newTD4 = NewTr.insertCell();
        newTD4.className = "textright";
        newTD4.style.width = "10%";
        newTD4.align = "center";
        newTD4.innerHTML = strSelect3;
        var Job = document.getElementById("Job");
        Job.setAttribute("name", "Job");
        //座机
        var newTD5 = NewTr.insertCell();
        newTD5.className = "textright";
        newTD5.style.width = "13%";
        newTD5.align = "center";
        newTD5.innerHTML = "<input type='text' id='Phone' name='Phone' style='width:100px;text-align:center;'/><span style='color: red;'>*</span>";
        //手机
        var newTD6 = NewTr.insertCell();
        newTD6.className = "textright";
        newTD6.style.width = "13%";
        //newTD6.align = "center";
        newTD6.innerHTML = "<input type='text' id='Mobile' name='Mobile' style='width:100px;text-align:center;'/><span style='color: red;'>*</span>";
        //邮箱
        var newTD8 = NewTr.insertCell();
        newTD8.className = "textright";
        newTD8.style.width = "13%";
        //newTD8.align = "center";
        newTD8.innerHTML = "<input type='text' id='Email' name='Email' style='width:125px;text-align:center;'/><span style='color: red;'>*</span>";
        //操作
        var newTD9 = NewTr.insertCell();
        newTD9.className = "textright";
        newTD9.style.width = "7%";
        newTD9.align = "center";
        newTD9.innerHTML = "<a onclick='deleteTr(" + NewTr.id + ")' style='color:blue;cursor:pointer;text-align:center;'>删除</a>";


    })
    $("#Reset").click(function () {
        var res = confirm("是否确认信息保存完毕");
        if (res) {
            window.location.reload();
            $("#Save").attr('disabled', false);
        }
        else
            return;
    })

})
function loadPerson() {
    //  职责部门 
    $.ajax({
        url: "GetContractSize",
        type: "post",
        data: { Type: "FDepartment" },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strSelect1 = "<select id='FDepartment' style='width:80px;text-align:center;'>";
                for (var i = 0; i < sel.length; i++) {
                    strSelect1 += "<option value=" + sel[i] + ">" + val[i] + "</option>";
                }
                strSelect1 += "</select><span style='color: red;'>*</span>";
            }
            else {
                return;
            }
        }
    });

    //  部门 
    $.ajax({
        url: "GetContractSize",
        type: "post",
        data: { Type: "Department" },
        dataType: "Json",
        success: function (data) {

            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strSelect2 = "<select id='Department' style='width:70px;text-align:center;'>";
                for (var i = 0; i < sel.length; i++) {
                    strSelect2 += "<option value=" + sel[i] + ">" + val[i] + "</option>";
                }
                strSelect2 += "</select> <span style='color: red;'>*</span>";
            }
            else {
                return;
            }

        }
    });

    //  岗位 
    $.ajax({
        url: "GetContractSize",
        type: "post",
        data: { Type: "Job" },
        dataType: "Json",
        success: function (data) {
            if (data.success == "true") {
                var strSel = data.Strid;
                var strVal = data.Strname;
                var sel = strSel.split(',');
                var val = strVal.split(',');
                strSelect3 = "<select id='Job' style='width:80px;text-align:center;'>";
                for (var i = 0; i < sel.length; i++) {
                    strSelect3 += "<option value=" + sel[i] + ">" + val[i] + "</option>";
                }
                strSelect3 += "</select> <span style='color: red;'>*</span>";
            }
            else {
                return;
            }
        }
    });
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
    //$('.ckb3').click(function () {
    //    var ckbname = this.name;
    //    if (this.checked) {
    //        var getId = document.getElementById(this.name);
    //        var res = ckbname;
    //        document.getElementById("IsrankingIn5").value = res;
    //        $('.ckb3').each(function () {
    //            if (this.name != ckbname) {
    //                $(this).attr("disabled", true);
    //            }
    //        });
    //    } else {
    //        $('.ckb3').attr("disabled", false);
    //    }
    //})
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
function deleteTr(obj) {
    var one = confirm("确认删除该行吗？");
    if (one == false)
        return;
    else {
        if (obj) obj.parentNode.removeChild(obj);
    }
}
//资质
function reloadPlanPro() {
    $("#list").jqGrid('setGridParam', {
        url: 'ProjectGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
        //loadComplete: function () {
        //    $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        //}
    }).trigger("reloadGrid");

}
//证书
function reloadPSer() {
    $("#list1").jqGrid('setGridParam', {
        url: 'CertificateGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
        //loadComplete: function () {
        //    $("#list1").jqGrid("setGridWidth", $("#bor1").width() - 30, false);
        //}
    }).trigger("reloadGrid");
}
//产品
function reloadpro() {
    $("#list2").jqGrid('setGridParam', {
        url: 'ProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
        //loadComplete: function () {
        //    $("#list2").jqGrid("setGridWidth", $("#bor2").width() - 30, false);
        //}
    }).trigger("reloadGrid");
}
//服务
function reloadServer() {

    $("#list3").jqGrid('setGridParam', {
        url: 'ServerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
        //loadComplete: function () {
        //    $("#list3").jqGrid("setGridWidth", $("#bor3").width() - 30, false);
        //}
    }).trigger("reloadGrid");
}
function reloadWard() {
    $("#list5").jqGrid('setGridParam', {
        url: 'AwardGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
    }).trigger("reloadGrid");
}
function reloadPrice() {
    $("#list6").jqGrid('setGridParam', {
        url: 'PriceGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
    }).trigger("reloadGrid");
}
function selChange(rowid) {
    if ($('input[id=c' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSelID != 0) {
            $('input[id=c' + oldSelID + ']').prop("checked", false);
        }
        $('input[id=c' + rowid + ']').prop("checked", true);
        $("#list").setSelection(rowid)
    }
}
function selChangecertifi(rowid) {
    if ($('input[id=c' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldcerID != 0) {
            $('input[id=c' + oldcerID + ']').prop("checked", false);
        }
        $('input[id=c' + rowid + ']').prop("checked", true);
        $("#list1").setSelection(rowid)
    }
}
function selPro(rowid) {
    if ($('input[id=c' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldProID != 0) {
            $('input[id=c' + oldProID + ']').prop("checked", false);
        }
        $('input[id=c' + rowid + ']').prop("checked", true);
        $("#list2").setSelection(rowid)
    }
}
function selServer(rowid) {
    if ($('input[id=c' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldSerID != 0) {
            $('input[id=c' + oldSerID + ']').prop("checked", false);
        }
        $('input[id=c' + rowid + ']').prop("checked", true);
        $("#list3").setSelection(rowid)
    }
}
function selAward(rowid) {
    if ($('input[id=e' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldAward != 0) {
            $('input[id=e' + oldAward + ']').prop("checked", false);
        }
        $('input[id=e' + rowid + ']').prop("checked", true);
        $("#list5").setSelection(rowid)
    }
}
function selPrice(rowid) {
    if ($('input[id=f' + rowid + ']').prop("checked") == 'checked') {
        return;
    }
    else {
        if (oldPrice != 0) {
            $('input[id=f' + oldPrice + ']').prop("checked", false);
        }
        $('input[id=f' + rowid + ']').prop("checked", true);
        $("#list6").setSelection(rowid)
    }
}
function jq() {
    jQuery("#list").jqGrid({
        url: 'ProjectGrid',  // ProjectGrid
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['', '序号', '资质类型', '资质具体项', '资质证书具体项', '其他项说明', '文档名称', '内部评审时资质文档名称', '文档类型'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'ID', index: 'ID', width: 50, hidden: true },
        { name: 'TypeO', index: 'TypeO', width: 150 },
        { name: 'FType', index: 'FType', width: 150 },
        { name: 'Item', index: 'Item', width: 150 },
        { name: 'ItemO', index: 'ItemO', width: 150 },
        { name: 'FFileName', index: 'FFileName', width: 200 },
        { name: 'MffileName', index: 'MffileName', width: 150, hidden: true },
        { name: 'FileType', index: 'FileType', width: 185, hidden: true },
        ],
        pager: jQuery('#pager'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '新增资质表',

        gridComplete: function () {
            var ids = jQuery("#list").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChange(" + id + ")' type='checkbox' value='" + jQuery("#list").jqGrid('getRowData', id).CID + "' name='cb'/>";
                jQuery("#list").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSelID != 0) {
                $('input[id=c' + oldSelID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSelID = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list").getGridParam("lastpage"))
                    return;
                curPage = $("#list").getGridParam("page") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list").getGridParam("page") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager :input").val();
            }
            reloadPlanPro();
        },
        loadComplete: function () {
            //$("#list").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqCertifi() {
    jQuery("#list1").jqGrid({
        url: 'CertificateGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['', '序号', '是否为计划性证书', '证书类型', '证书名称', '证书编号', '证书认证机构名称', '通过认证时间', '文档名称', '文档类型'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 50, hidden: true },
        { name: 'IsPlan', index: 'IsPlan', width: 100 },
        { name: 'CType', index: 'CType', width: 100 },
        { name: 'CName', index: 'CName', width: 120 },
        { name: 'CCode', index: 'CCode', width: 130 },
        { name: 'COrganization', index: 'COrganization', width: 150 },
        { name: 'CDate', index: 'CDate', width: 100 },
        { name: 'CFileName', index: 'CFileName', width: 150 },
        { name: 'FileType', index: 'FileType', width: 100, hidden: true },
        ],
        pager: jQuery('#pager1'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '新增证书表',
        gridComplete: function () {
            var ids = jQuery("#list1").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list1").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selChangecertifi(" + id + ")' type='checkbox' value='" + jQuery("#list1").jqGrid('getRowData', id).CID + "' name='cb'/>";
                jQuery("#list1").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldcerID != 0) {
                $('input[id=c' + oldcerID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldcerID = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list1").getGridParam("lastpage"))
                    return;
                curPage = $("#list1").getGridParam("page1") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list1").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list1").getGridParam("page1") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager1 :input").val();
            }
            reloadCertifi();
        },
        loadComplete: function () {
            // $("#list1").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list1").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPro() {
    jQuery("#list2").jqGrid({
        url: 'ProGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['', '序号', '产品分类', '产品编号', '产品名称', '规格型号', '单位', '详细说明', '参考价格', '产地', '文档名称'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 50, hidden: true },
        { name: 'SID', index: 'SID', width: 50, hidden: true },
        { name: 'Ptype', index: 'Ptype', width: 80 },
        { name: 'ProductID', index: 'ProductID', width: 100 },
        { name: 'ProductName', index: 'ProductName', width: 120 },
        { name: 'Standard', index: 'Standard', width: 130 },
        { name: 'MeasureUnit', index: 'MeasureUnit', width: 50 },
        { name: 'DetailDesc', index: 'DetailDesc', width: 150 },
        { name: 'Price', index: 'Price', width: 80 },
        { name: 'OriginPlace', index: 'OriginPlace', width: 100 },
        { name: 'FFileName', index: 'FFileName', width: 100 },
        ],
        pager: jQuery('#pager2'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '拟购产品',
        gridComplete: function () {
            var ids = jQuery("#list2").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list2").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selPro(" + id + ")' type='checkbox' value='" + jQuery("#list2").jqGrid('getRowData', id).CID + "' name='cb'/>";
                jQuery("#list2").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldProID != 0) {
                $('input[id=c' + oldProID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldProID = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list2").getGridParam("lastpage"))
                    return;
                curPage = $("#list2").getGridParam("page2") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list2").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list2").getGridParam("page2") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager2 :input").val();
            }
            reloadPro();
        },
        loadComplete: function () {
            // $("#list2").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list2").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqServer() {

    jQuery("#list3").jqGrid({
        url: 'ServerGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['', '序号', '服务名称', '服务描述', '用途', '文档名称'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 100, hidden: true },
        { name: 'SID', index: 'SID', width: 150, hidden: true },
        { name: 'ServiceName', index: 'ServiceName', width: 250 },
        { name: 'ServiceDesc', index: 'ServiceDesc', width: 250 },
        { name: 'Purpose', index: 'Purpose', width: 250 },
        { name: 'FFileName', index: 'FFileName', width: 250 },
        ],
        pager: jQuery('#pager3'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '拟购服务',
        gridComplete: function () {
            var ids = jQuery("#list3").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list3").jqGrid('getRowData', id);
                var curChk = "<input id='c" + id + "' onclick='selServer(" + id + ")' type='checkbox' value='" + jQuery("#list3").jqGrid('getRowData', id).CID + "' name='cb'/>";
                jQuery("#list3").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldSerID != 0) {
                $('input[id=c' + oldSerID + ']').prop("checked", false);
            }
            $('input[id=c' + rowid + ']').prop("checked", true);
            oldSerID = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list3").getGridParam("lastpage"))
                    return;
                curPage = $("#list3").getGridParam("page3") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list3").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list3").getGridParam("page3") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager3 :input").val();
            }
            reloadServer();
        },
        loadComplete: function () {
            // $("#list3").jqGrid("setGridHeight", $("#pageContent").height() / 2 - 180, false);
            $("#list3").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqAward() {
    jQuery("#list5").jqGrid({
        url: 'AwardGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['', '序号', '奖项名称', '上传时间'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 100, hidden: true },
        { name: 'SID', index: 'SID', width: 150, hidden: true },
        { name: 'Award', index: 'Award', width: 250 },
        { name: 'AwardTime', index: 'AwardTime', width: 250 },
        ],
        pager: jQuery('#pager5'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '曾获奖项',
        gridComplete: function () {
            var ids = jQuery("#list5").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list5").jqGrid('getRowData', id);
                var curChk = "<input id='e" + id + "' onclick='selAward(" + id + ")' type='checkbox' value='" + jQuery("#list5").jqGrid('getRowData', id).CID + "' name='cb'/>";
                jQuery("#list5").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldAward != 0) {
                $('input[id=e' + oldAward + ']').prop("checked", false);
            }
            $('input[id=e' + rowid + ']').prop("checked", true);
            oldAward = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list5").getGridParam("lastpage"))
                    return;
                curPage = $("#list5").getGridParam("page5") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list5").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list5").getGridParam("page5") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager5 :input").val();
            }
            reloadWard();
        },
        loadComplete: function () {
            $("#list5").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}
function jqPrice() {
    jQuery("#list6").jqGrid({
        url: 'PriceGrid',
        datatype: 'json',
        height: 150,
        postData: { curpage: curPage, rownum: OnePageCount, sid: $("#StrSid").val() },
        loadonce: false,
        mtype: 'POST',
        jsonReader: {
            root: function (obj) {
                var data = eval("(" + obj + ")");
                return data.rows;
            },
            page: function (obj) {
                var data = eval("(" + obj + ")");
                return data.page;
            },
            total: function (obj) {
                var data = eval("(" + obj + ")");
                return data.total;
            },
            records: function (obj) {
                var data = eval("(" + obj + ")");
                return data.records;
            },
            repeatitems: false
        },
        colNames: ['', '序号', '报价/比价单名称', '上传时间'],
        colModel: [
        { name: 'IDCheck', index: 'id', width: 100, hidden: true },
        { name: 'SID', index: 'SID', width: 150, hidden: true },
        { name: 'PriceName', index: 'PriceName', width: 250 },
        { name: 'PriceTime', index: 'PriceTime', width: 250 },
        ],
        pager: jQuery('#pager6'),
        pgbuttons: true,
        rowNum: OnePageCount,
        viewrecords: true,
        imgpath: 'themes/basic/images',
        caption: '报价/比价单',
        gridComplete: function () {
            var ids = jQuery("#list6").jqGrid('getDataIDs');
            for (var i = 0; i < ids.length; i++) {
                var id = ids[i];
                var curRowData = jQuery("#list6").jqGrid('getRowData', id);
                var curChk = "<input id='f" + id + "' onclick='selPrice(" + id + ")' type='checkbox' value='" + jQuery("#list6").jqGrid('getRowData', id).CID + "' name='cb'/>";
                jQuery("#list6").jqGrid('setRowData', ids[i], { IDCheck: curChk });
            }
        },
        onSelectRow: function (rowid, status) {
            if (oldPrice != 0) {
                $('input[id=f' + oldPrice + ']').prop("checked", false);
            }
            $('input[id=f' + rowid + ']').prop("checked", true);
            oldPrice = rowid;
        },
        onPaging: function (pgButton) {
            if (pgButton == "next_pager") {
                if (curPage == $("#list6").getGridParam("lastpage"))
                    return;
                curPage = $("#list6").getGridParam("page6") + 1;
            }
            else if (pgButton == "last_pager") {
                curPage = $("#list6").getGridParam("lastpage");
            }
            else if (pgButton == "prev_pager") {
                if (curPage == 1)
                    return;
                curPage = $("#list6").getGridParam("page6") - 1;
            }
            else if (pgButton == "first_pager") {
                curPage = 1;
            }
            else {
                curPage = $("#pager6 :input").val();
            }
            reloadPrice();
        },
        loadComplete: function () {
            $("#list6").jqGrid("setGridWidth", $("#bor").width() - 30, false);
        }
    });
}

function changes(op) {
    //if (op == "BDB") {
    //    var res = "";
    //    var txtYW = "";
    //    $('input[name=guonei]:checkbox:checked').each(function () {   //美国，null,中国，韩国
    //        res += $(this).val() + ",";
    //    });
    //    var arrcbYw = res.split(',');
    //    for (var i = 0; i < arrcbYw.length - 1; i++) {
    //        txtYW += $("#txt" + arrcbYw[i]).val() + ",";
    //        if (res != "" && $("#txt" + arrcbYw[i]).val() == "") {
    //            alert("百分比不能为空"); break;
    //        }
    //    }
    //    document.getElementById("BusinessDistribute").value = res + ":" + txtYW;//汉字：数字

    //}
    // if (op == "BW") {
    //var way = "";
    //$('input[name=billway]:checkbox:checked').each(function () {
    //    way += $(this).val() + ",";
    //});

    //var billway = document.getElementsByName("billway");
    //for (var i = 0; i < billway.length; i++) {
    //    billway[i] = new Array();
    //    if (billway[i].checked) {
    //        billway[i].value = $(this).val() + "," + $("#bill" + arr[i] + "").val() + ",";
    //    }
    //    alert(billway[i].value);
    //}

    //var arr = way.split(',');
    //var txtbill = "";
    //for (var i = 0; i < arr.length - 1; i++) {
    //    txtbill += $("#bill" + arr[i] + "").val() + ",";
    //    if ($("#bill" + arr[i] + "").val() == "" && way != "") {
    //        alert("开票方式百分比不能为空"); break;
    //    }
    //}
    //document.getElementById("BillingWay").value = way + ":" + txtbill;
    // }
    //供需关系
    if (op == "rla") {
        var rela = "";
        $('input[name=rela1]:checked').each(function () {
            rela += $(this).val();
        })
        document.getElementById("Relation").value = rela;
    }
    //经营品种分类
    if (op == "st") {
        var sal = "";
        $('input[name=scale]:checked').each(function () {
            sal += $(this).val();
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

//获取时间格式
function GetTime() {
    var mydate = new Date();

    var s = mydate.getFullYear() + "-";
    if (mydate.getMonth() < 10) {
        s += '0' + (mydate.getMonth() + 1) + "-";
    }
    else {
        s += (mydate.getMonth() + 1) + "-";
    }
    if (mydate.getDate() < 10) {
        s += '0' + mydate.getDate();


    } else {
        s += mydate.getDate();

    }
    document.getElementById("DeclareDate").value = s;

}
function getDateStr(date) {
    var month = date.getMonth() + 1;
    var strDate = date.getFullYear() + '-' + month + '-' + date.getDate();
    return strDate;
}
function myrefresh() {
    window.location.reload();
}
function checkMobile() {
    var sMobile = document.getElementById("Mobile").value;
    var re = /^1\d{10}$/;
    if (re.test(sMobile)) {

    } else {
        alert("输入的手机号不正确");
        return;
    }
}
function checkPhone() {
    var phone = document.getElementById("Phone").value;
    var re = /^0\d{2,3}-?\d{7,8}$/;
    if (re.test(phone)) {

    } else {
        alert("座机号不正确");
        return;
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
window.onload = function () {
    document.getElementsByTagName("body")[0].onkeydown = function () {

        //获取事件对象  
        var elem = event.relatedTarget || event.srcElement || event.target || event.currentTarget;

        if (event.keyCode == 8) {//判断按键为backSpace键  

            //获取按键按下时光标做指向的element  
            var elem = event.srcElement || event.currentTarget;

            //判断是否需要阻止按下键盘的事件默认传递  
            var name = elem.nodeName;

            if (name != 'INPUT' && name != 'TEXTAREA') {
                return _stopIt(event);
            }
            var type_e = elem.type.toUpperCase();
            if (name == 'INPUT' && (type_e != 'TEXT' && type_e != 'TEXTAREA' && type_e != 'PASSWORD' && type_e != 'FILE')) {
                return _stopIt(event);
            }
            if (name == 'INPUT' && (elem.readOnly == true || elem.disabled == true)) {
                return _stopIt(event);
            }
        }
    }

    function _stopIt(e) {
        if (e.returnValue) {
            e.returnValue = false;
        }
        if (e.preventDefault) {
            e.preventDefault();
        }

        return false;
    }
}













