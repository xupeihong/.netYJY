$(document).ready(function () {
    if (location.search != "") {
        RKID = location.search.split('&')[0].split('=')[1];
        alert(RKID);
        addBasicDetail();
    }
});
function addBasicDetail() { //增加货品信息行
    RKID = location.search.split('&')[0].split('=')[1];
    $.ajax({
        url: "RKXQ",
        type: "post",
        data: { RKID: RKID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);

            var html = "";
            html = "<tr>";
            html = '<td ><lable class="labSpecifications" id="RKID">' + json[0].RKID + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Rkdate">' + json[0].Rkdate + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="CKID">' + json[0].CKID + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="RKInstructions">' + json[0].RKInstructions + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Handler">' + json[0].Handler + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="RKType">' + json[0].RKType + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="UnitID">' + json[0].UnitID + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="State">' + json[0].State + '</lable> </td>';
            html += '</tr>'
            $("#GXInfo").append(html);

            var html = "";
            html = "<tr>";
            html += '<td ><lable class="labSpecifications" id="OrderContent">' + json[0].OrderContent + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Specifications">' + json[0].Specifications + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Supplier">' + json[0].Supplier + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Unit">' + json[0].Unit + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Amount">' + json[0].Amount + '</lable> </td>';
            html += '<td ><lable class="labSpecifications" id="Bak">' + json[0].Bak + '</lable> </td>';
            html += '</tr>'
            $("#GXInfo1").append(html);
        }

    })
}