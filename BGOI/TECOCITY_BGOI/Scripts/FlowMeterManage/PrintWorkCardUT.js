
$(document).ready(function () {
    //
    var ChangePlace = $("#StrChangePlace").val();
    if (ChangePlace == 0) {
        $("input[name=ChangePlace]:eq(0)").attr("checked", 'checked');
    }
    else if (ChangePlace == 1) {
        $("input[name=ChangePlace]:eq(1)").attr("checked", 'checked');
    }
    else {
        $("input[name=ChangePlace]:eq(2)").attr("checked", 'checked');
    }
    //
    var Check1 = $("#StrCheck1").val();
    if (Check1 == 0) {
        $("input[name=Check1]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check1]:eq(1)").attr("checked", 'checked');
    }
    var Check2 = $("#StrCheck2").val();
    if (Check2 == 0) {
        $("input[name=Check2]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check2]:eq(1)").attr("checked", 'checked');
    }
    var Check3 = $("#StrCheck3").val();
    if (Check3 == 0) {
        $("input[name=Check3]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check3]:eq(1)").attr("checked", 'checked');
    }
    var Check4 = $("#StrCheck4").val();
    if (Check4 == 0) {
        $("input[name=Check4]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check4]:eq(1)").attr("checked", 'checked');
    }
    var Check5 = $("#StrCheck5").val();
    if (Check5 == 0) {
        $("input[name=Check5]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check5]:eq(1)").attr("checked", 'checked');
    }
    var Check6 = $("#StrCheck6").val();
    if (Check6 == 0) {
        $("input[name=Check6]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check6]:eq(1)").attr("checked", 'checked');
    }
    var Check7 = $("#StrCheck7").val();
    if (Check7 == 0) {
        $("input[name=Check7]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check7]:eq(1)").attr("checked", 'checked');
    }
    var Check8 = $("#StrCheck8").val();
    if (Check8 == 0) {
        $("input[name=Check8]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=Check8]:eq(1)").attr("checked", 'checked');
    }
    //
    var RepairContent1 = $("#StrRepairContent1").val();
    if (RepairContent1 == 0) {
        $("input[name=RepairContent1]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=RepairContent1]:eq(1)").attr("checked", 'checked');
    }
    var RepairContent2 = $("#StrRepairContent2").val();
    if (RepairContent2 == 0) {
        $("input[name=RepairContent2]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=RepairContent2]:eq(1)").attr("checked", 'checked');
    }
    var RepairContent3 = $("#StrRepairContent3").val();
    if (RepairContent3 == 0) {
        $("input[name=RepairContent3]:eq(0)").attr("checked", 'checked');
    }
    else {
        $("input[name=RepairContent3]:eq(1)").attr("checked", 'checked');
    }

    // 打印 
    $("#btnPrint").click(function () {
        document.getElementById("btnPrint").className = "Noprint";

        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";

    });

})

