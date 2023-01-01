function BindTableRate() {
    $.ajax({
        url: GetglobalDomain() + '/AdminAjaxRequest/BindSubmenu',
        type: 'POST',
        dataType: "text",
        success: function (response) {
            $("#tbody").html(response);
        }
    });
    
}

function BindPackageFunction(Id, Licenseid) {
    $.ajax({
        url: GetglobalDomain() + '/AdminAjaxRequest/BindPackageFunction?Id=' + Id + '&Licenseid=' + Licenseid,
        type: 'POST',
        dataType: "text",
        success: function (response) {
            $("#tbody").html(response);
        }
    });

}
function BindPackageFeature(Id) {
    $.ajax({
        url: GetglobalDomain() + '/AdminAjaxRequest/BindPackageFeature?Id=' + Id,
        type: 'POST',
        dataType: "text",
        success: function (response) {
            $("#tbody").html(response);
        }
    });

}
function BindFuncPackage() {
    debugger;
    var iFk_PackMstId = $("#iPK_PackageMstId").val();
    $.ajax({
        url: GetglobalDomain() + '/AdminAjaxRequest/BindFuncPackage?iFk_PackMstId=' + iFk_PackMstId,
        type: 'POST',
        dataType: "text",
        success: function (response) {
            $("#tbody").html(response);
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

