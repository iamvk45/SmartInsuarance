function BindTableRate() {
    $.ajax({
        url: '/AdminAjaxRequest/BindSubmenu',
        type: 'POST',
        dataType: "text",
        success: function (response) {
            $("#tbody").html(response);
        }
    });

}
function BindCuverLiceTempTable(Id) {
    debugger;
    var iTypid = 2;
    $.ajax({
        url: '/AdminAjaxRequest/BindCuverLiceTempTable?Id=' + Id + '&iTypid=' + iTypid,
        type: 'POST',
        dataType: "text",
        success: function (response) {
           
            $("#tbody").html(response);
            var rowCount = $('#tbody tr').length;
           

            if (rowCount != 0) {
                $("#iValidityId").attr("readonly", "readonly");
            }
            else {
                $("#iValidityId").prop("disabled", false);
            }
            var id = $("#iValidityId").val();
          
        }
    });

}
//function BindSubmenus(MenuId, Type,MstId) {
//    alert("hshd");
//    var Model = {
//        MenuId: MenuId,
//        Type: Type,
//        LicMstId: MstId
//    }
//    $.ajax({
//        headers: {
//            'Accept': 'application/json',
//            'Content-Type': 'application/json'
//        },
//        url: "/Master/GetSelectMenuSubmenulist",
//        dataType: 'json',
//        type: 'Post',
//        cache: false,
//        data: JSON.stringify(Model),
//        success: function (data) {

//            var submenuList = data.Data;
//            //console.table(submenuList)
//            if (MenuId != 0) {
//                var selectBody = "<option value=0>Select Submenus</option> ";
//            }
//            else {
//                var selectBody = "<option value=0>Select Menus</option> ";
//            }
//            submenuList.forEach(item => {

//                selectBody += `<option value=${item.Id}>${item.Text}</option>`

//            });
//            if (MenuId != 0) {
//                $('#iSubMnuId').html('')
//                $('#iSubMnuId').append(selectBody);
//            }
//            else {
//                $('#iMnuId').html('')
//                $('#iMnuId').append(selectBody);
//                $('#iSubMnuId').html('<option value=0>Select Submenus</option>');
//            }
//        },
//        error: function (d) {
//            console.log(d);
//        }
//    });

//}

