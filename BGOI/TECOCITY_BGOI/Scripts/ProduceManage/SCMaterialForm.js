$(document).ready(function () {
    var msg = $("#msg").val();
    //alert(msg)
    if (msg != "") {
        //window.parent.frames["iframeRight"].reload();
        alert(msg);
        setTimeout('parent.ClosePop();', 100);
       
    }
    //$("#SCMaterialForm").submit(function () {
    //    var options = {
    //        url: "InsertFile",
    //        data: {},   
    //        type: "post",
    //        async: false,
    //        resetForm: true,
    //        dataType: "json",
    //        success: function (data) {
    //           // alert("")
    //            if (data.success == true) {
    //                //window.parent.frames["iframeRight"].reload();
    //                alert("保存成功");
    //                setTimeout('parent.ClosePop();', 10);
    //            }

    //            else
    //                alert("保存失败！-" + data.Err)
    //        }
    //    };
    //    $("#SCMaterialForm").ajaxSubmit(options);
    //    return false;
    //})
})
function seav() {
    var fileElem = document.getElementById('fileElem').files
    if (fileElem.length <= 0) {
        alert("请选择要上传的文件");
        return;
    }
    for (var i = 0; i < fileElem.length; i++) {
        //alert(fileElem[i].name)
        var name = fileElem[i].name;
        var extStart = name.lastIndexOf(".");
        var ext = name.substring(extStart, name.length).toUpperCase();
        //alert(ext);
        if (ext != ".BMP" && ext != ".PNG" && ext != ".GIF" && ext != ".JPG" && ext != ".JPEG") {
            alert("上传文件格式限于png,gif,jpeg,jpg格式");
            return;
        }
    }


    $("#SCMaterialForm").submit();

}