$(document).ready(function () {
    if (location.search != "") {
        EID= location.search.split('&')[0].split('=')[1];
    }
    LoadExCheck();

    //LoadReceiveBill();
   
    $("#btnSaveExCheck").click(function () {
        SaveExCheck();
    })
  
    $("#btnExit").click(function () {
        window.parent.ClosePop();
    })
});
var TID = '';
var curPage = 1;
var OnePageCount = 15;
var oldSelID = 0;
//获取退货和换货的物品数据
function LoadExCheck()
{
    $.ajax({

        url: "GetExcAndReturnDetailByEID",
        type: "post",
        data: { EID: EID },
        dataType: "json",
        success: function (data) {

            var json = eval(data.datas);
            if (json.length > 0) {
                for (var i = 0; i < json.length; i++) {
                    rowCount = i;
                    var html = "";
                    html = '<tr  id ="DetailInfo' + rowCount + '" onclick="selRow(this)">'
                    html += '<td ><input id="Check' + rowCount + '" type="checkbox" name="cb"/><lable class="labRowNumber' + rowCount + ' " id="RowNumber' + rowCount + '">' + (i + 1) + '</lable> </td>';
                    html += '<td ><lable class="labOrderContent' + rowCount + ' " id="ProductID' + rowCount + '">' + json[i].ProductID + '</lable> </td>';
                    html += '<td ><lable class="labSpecifications' + rowCount + ' " id="Spec' + rowCount + '">' + json[i].Specifications + '</lable> </td>';
                    html += '<td ><input type="text"  id="PackWreck' + rowCount + '"  /></td>';
                    html += '<td ><input type="text"  id="FeatureWreck' + rowCount + '"/></td>';
                    html += '<td ><input type="text"  id="Componments' + rowCount + '" /></td>';
                    html += '<td ><input type="text"  id="Quality' + rowCount + '" /></td>';
                    html += '<td ><lable id="Remark' + rowCount + '">' + json[i].Remark + '</lable> </td>';
                    //html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="ShipGoodsID' + rowCount + '">' + json[i].OrderID + '</lable> </td>';
                    html += '<td style="display:none;"><lable class="labDID' + rowCount + ' " id="DID' + rowCount + '">' + json[i].DID + '</lable> </td>';
                    html += '</tr>'
                    $("#DetailInfo").append(html);

                }


            }
        }
    })
}
function SaveExCheck()
{
   // var OID = OrderID;
    var EID = $("#EID").val();
    var TID = $("#TID").val();
    var ChangeDate = $("#ChangeDate").val();
    var Brokerage = $("#Brokerage").val();
    var CheckDescription = $("#CheckDescription").val()
    var IsApproval1 = $("#IsApproval1").val();
   
    var ProductID = "";
   // var OrderContent = "";
    var Specifications = "";
    var PackWreck = "";
    var FeatureWreck = "";
    var Componments = "";
    var Quality = "";
    var Remark = "";
    var DID = "";

    var aa = "";
    var cbNum = document.getElementsByName("cb");
    for (var i = 0; i < cbNum.length; i++) {
        var cbid = "";
        if (cbNum[i].checked == true) {
            cbid = cbNum[i].id;
            aa += cbid.substring(5) + ",";
        }
    }
    var salesNum = "";
    var arr1 = aa.split(',');
    for (var i = 0; i < arr1.length - 1; i++) {
        //salesNum += document.getElementById("Amount" + arr1[i]).value + ",";
        var Productid = document.getElementById("ProductID" + arr1[i]).innerHTML;
       // var mainContent = document.getElementById("OrderContent" + arr1[i]).innerHTML;
        var specsModels = document.getElementById("Spec" + arr1[i]).innerHTML;
        var packwreck = document.getElementById("PackWreck" + arr1[i]).value;
        var featurewreck = document.getElementById("FeatureWreck" + arr1[i]).value;
        var componments = document.getElementById("Componments" + arr1[i]).value;
        var quality = document.getElementById("Quality" + arr1[i]).value;
        var remark = document.getElementById("Remark" + arr1[i]).innerHTML;
        var did = document.getElementById("DID"+arr1[i]).innerHTML;
        ProductID += Productid;
       // OrderContent += mainContent;
        Specifications += specsModels;
        PackWreck += packwreck;
        FeatureWreck += featurewreck;
        Componments += componments;
        Quality += quality;
        Remark += remark;
        DID += did;
        if (i < arr1.length - 1) {
            ProductID += ",";
          //  OrderContent += ",";
            Specifications += ",";
            PackWreck += ",";
            FeatureWreck += ",";
            Componments += ",";
            Quality += ",";
            Remark += ",";
            DID += ",";
        } else {
            ProductID += "";
            OrderContent += "";
            SpecsModels += "";
            PackWreck += "";
            FeatureWreck += "";
            Componments += "";
            Quality += "";
            Remark += "";
            DID += "";
        }
    }

    isConfirm = confirm("是否保存退货检验")
    if (isConfirm == false) {
        return false;
    }
    $.ajax({
        url: "SaveExChangeCheck",
        type: "Post",
        data: {
            //	CheckDescription，IsApproval1, OrderContent,Specifications,PackWreck ,, FeatureWreck , Componments , Quality , Remark 
            TID: TID, EID: EID, ChangeDate: ChangeDate, Brokerage: Brokerage, CheckDescription: CheckDescription, IsApproval1: IsApproval1,ProductID:ProductID,
             Specifications: Specifications, PackWreck: PackWreck,
            FeatureWreck: FeatureWreck, Componments: Componments, Quality: Quality, Remark: Remark,DID:DID
        },
        async: false,
        success: function (data) {
            if (data.success == true) {
                window.parent.frames["iframeRight"].reload();
                alert("生成退换货检验！");

                window.parent.ClosePop();
            }
            else {
                alert("生成换货检验失败-" + data.Msg);
            }
        }
    });
}

function selRow(curRow) {
    newRowID = curRow.id;
    $("#DetailInfo tr").removeAttr("class");
    $("#" + newRowID).attr("class", "RowClass");
}