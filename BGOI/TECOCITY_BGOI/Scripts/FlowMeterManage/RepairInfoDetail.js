var j = 0;
$(document).ready(function () {
  
    // 加载部件更换记录
    $.ajax({
        url: "RepairDevice",
        type: "post",
        data: { RID: $("#StrRID").val() },
        dataType: "Json",
        success: function (data) {
            if (data.success == "false") {

                return;
            }
            else {

                var strBakName = data.BakName;
                var strBakType = data.BakType;
                var strBakNum = data.BakNum;
                var strMeasure = data.Measure;
                //
                var BakName = strBakName.split(',');
                var BakType = strBakType.split(',');
                var BakNum = strBakNum.split(',');
                var Measure = strMeasure.split(',');

                for (var k = 0 ; k < BakName.length; k++) {
                    j++;
                    var testTable = document.getElementById("taskList");
                    var newTr = testTable.insertRow();
                    newTr.id = "row" + j;

                    //部件名称 
                    var newTd1 = newTr.insertCell();
                    newTd1.className = "textright";
                    newTd1.style.width = "200px";
                    newTd1.innerHTML = BakName[k];

                    //规格型号
                    var newTd2 = newTr.insertCell();
                    newTd2.className = "textright";
                    newTd2.style.width = "80px";
                    newTd2.innerHTML = BakType[k];

                    //单位
                    var newTd3 = newTr.insertCell();
                    newTd3.className = "textright";
                    newTd3.style.width = "80px";
                    newTd3.innerHTML = Measure[k];

                    //数量
                    var newTd4 = newTr.insertCell();
                    newTd4.className = "textright";
                    newTd4.style.width = "80px";
                    newTd4.innerHTML = BakNum[k];


                }
            }
        }
    });

})