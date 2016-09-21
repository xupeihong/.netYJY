





var j = 0;
$(document).ready(function () {

    LoadCheckDataList();
    var Request = GetRequest();
    var id = Request["id"];
    $("#ID").val(id);
})



function Voluation(t) {
    var j = $("#ID").val();
    window.parent.$("#ProName" + j).val($(t)[0].childNodes['0'].innerHTML);
    window.parent.$("#Spec" + j).val($(t)[0].childNodes['1'].innerHTML);
    window.parent.$("#Units" + j).val($(t)[0].childNodes['2'].innerHTML);
  
}


function LoadCheckDataList() {

    $.ajax({
        url: "GetComponent",
        type: "post",
        data: {},
        dataType: "Json",
        success: function (obj) {
            if (obj.success == "false") {

                return;
            }
            else {

                var data = eval("(" + obj.data + ")");
                var str = ""
                for (var k = 1 ; k <= data.rows.length; k++) {

                    if (k == 1) {
                        str += "<tr>"
                    }
                    str += "<td onclick='Voluation(this)'><lable id='ProName" + k + "' >" + data.rows[k - 1]["ProName"] + "</lable>"
                    str += "<lable id='Spec" + k + "' style='display:none' >" + data.rows[k-1]["Spec"] + "</lable>"
                    str += "<lable id='Units" + k + "' style='display:none'>" + data.rows[k-1]["Units"] + "</lable></td>";

                    if (k % 4 == 0&&k!=0) {
                        if (k == data.rows.length)
                            str += "</tr>";
                        else
                            str += "</tr><tr>";
                    }

                }

                document.getElementById('list').innerHTML = str;
            }
        }
    });
}


function GetRequest() {

    var url = location.search; //获取url中"?"符后的字串

    var theRequest = new Object();

    if (url.indexOf("?") != -1) {

        var str = url.substr(1);

        strs = str.split("&");

        for (var i = 0; i < strs.length; i++) {

            theRequest[strs[i].split("=")[0]] = unescape(strs[i].split("=")[1]);

        }

    }

    return theRequest;

}