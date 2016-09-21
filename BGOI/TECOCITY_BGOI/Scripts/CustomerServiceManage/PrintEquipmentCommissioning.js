
$(document).ready(function () {
    jq();//加载数据
    // 打印 
    $("#btnPrint").click(function () {
        //alert(1);
        document.getElementById("btnPrint").className = "Noprint";
        $("#ReportContent").attr("style", "width: 100%;margin-top:10px")
        pagesetup("0.3", "0", "0.3", "0.3");
        window.print();
        document.getElementById("btnPrint").className = "btn";
        $("#ReportContent").attr("style", "margin: 0 auto; width: 90%;margin-top:10px")
    });
})

function jq() {
    if (location.search != "") {
        TRID = location.search.split('&')[0].split('=')[1];
    }
    //主表
    $.ajax({
        url: "GetEquipmentDebugging",
        type: "post",
        data: { TRID: TRID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    $("#DebTime").val(json[i].DebTime);
                    //设备管理方式
                    var EquManType = json[i].EquManType;
                    if (EquManType == "0") {
                        $(':radio[name=EquManType][value=0]').attr('checked', true);
                    } else if (EquManType == "1") {
                        $(':radio[name=EquManType][value=1]').attr('checked', true);
                    } else if (EquManType == "2") {
                        $(':radio[name=EquManType][value=2]').attr('checked', true);
                    } else if (EquManType == "3") {
                        $(':radio[name=EquManType][value=3]').attr('checked', true);
                    } else {
                        $(':radio[name=EquManType][value=4]').attr('checked', true);
                    }
                    //气种
                    var Gas = json[i].Gas;
                    if (Gas == "0") {
                        $(':radio[name=Gas][value=0]').attr('checked', true);
                    } else if (Gas == "1") {
                        $(':radio[name=Gas][value=1]').attr('checked', true);
                    } else if (Gas == "2") {
                        $(':radio[name=Gas][value=2]').attr('checked', true);
                    } else if (Gas == "3") {
                        $(':radio[name=Gas][value=3]').attr('checked', true);
                    } else if (Gas == "4") {
                        $(':radio[name=Gas][value=4]').attr('checked', true);
                    } else if (Gas == "5") {
                        $(':radio[name=Gas][value=5]').attr('checked', true);
                    } else {
                        $(':radio[name=Gas][value=6]').attr('checked', true);
                    }
                    //用户类别
                    var UserType = json[i].UserType;
                    if (UserType == "0") {
                        $(':radio[name=UserType][value=0]').attr('checked', true);
                    } else if (UserType == "1") {
                        $(':radio[name=UserType][value=1]').attr('checked', true);
                    } else if (UserType == "2") {
                        $(':radio[name=UserType][value=2]').attr('checked', true);
                    } else if (UserType == "3") {
                        $(':radio[name=UserType][value=3]').attr('checked', true);
                    } else {
                        $(':radio[name=UserType][value=4]').attr('checked', true);
                    }

                }
            }
        }
    })
    //副表
    $.ajax({
        url: "UpDebuggingSituation",
        type: "post",
        data: { TRID: TRID },
        dataType: "json",
        success: function (data) {
            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = document.getElementById("DetailInfo").rows.length;
                    var CountRows = parseInt(rowCount) + 1;
                    //产品名称  产品编号  规格型号
                    var html = "";
                    html = '<tr id ="DetailInfo' + rowCount + '" onclick="selRow(this)">';
                    html += '<td ><lable class="labPID' + rowCount + ' " id="PID' + rowCount + '">' + json[i].PID + '</lable> </td>';
                    html += '<td ><lable class="labSpec' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Spec + '</lable> </td>';
                    html += '</tr>';
                    $("#DetailInfo").append(html);
                    //上台（前台）
                    $("#PowerTime").val(json[i].PowerTime);
                    //下台（前台）
                    $("#StepDownTime").val(json[i].StepDownTime);
                    //设备形式
                    var ProductForm = json[i].ProductForm.split(",");
                    for (var i = 0; i < ProductForm.length; i++) {
                        if (ProductForm[i] == "0") {
                            $("input:checkbox[value='0']").attr('checked', true);
                        } else if (ProductForm[i] == "1") {
                            $("input:checkbox[value='1']").attr('checked', true);
                        } else if (ProductForm[i] == "2") {
                            $("input:checkbox[value='2']").attr('checked', true);
                        } else if (ProductForm[i] == "3") {
                            $("input:checkbox[value='3']").attr('checked', true);
                        } else if (ProductForm[i] == "4") {
                            $("input:checkbox[value='4']").attr('checked', true);
                        } else {
                        }
                    }



                }
            }
        }
    })
}

