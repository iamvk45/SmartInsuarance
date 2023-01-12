$(".ChangeStatus").change(function () {
    debugger;
    var id = $(this).data("id");
    var item = id.split('-');
    var type = parseInt($(this).val());
   
    var Params = JSON.stringify(
        {
            TableId: item[0],
            type: type,
            Id: item[1],
            Guid:''
        });

    $.ajax({
        url: GetglobalDomain() + "/Role/ChangeStatus",
        type: 'POST',
        dataType: "json",
        contentType: "application/json",
        data: Params,
        success: function (response) {
            debugger;
            if (response.isvalid == 1) {
                var msg = 'Status Changes Successfully';
                if (type == 1) {
                    $("#" + id).val(0);
                    /* $(this).val('0');*/
                    msg = 'Status De-Active Successfully';
                }
                else {
                    $("#" + id).val(1);
                    /* $(this).val('1');*/
                    msg = 'Status Active Successfully';
                }
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: msg,
                    showConfirmButton: false,
                    timer: 3000
                })


            }






        }
    });

});
function DeleteData(Id,table) {
    var Id = Id;

    /*  var TypeId = $(this).attr("name");*/
    $.confirm({

        title: 'Remove Confirmation',
        content: 'Are you sure to remove record',
        type: 'red',
        typeAnimated: true,
        animation: 'rotate',
        closeAnimation: 'scale',
        buttons: {
            Confirm: {
                text: 'YES',
                btnClass: 'btn-red',
                action: function () {



                    var Params = JSON.stringify({
                        TableId: table,
                        Id: Id,
                        type: 2


                    });
                    /* ShowBALoader("LoaderBASinup1");*/
                    $.ajax({
                        url: GetglobalDomain() + "/Role/ChangeStatus",
                        type: 'POST',
                        dataType: "json",
                        data: Params,
                        contentType: "application/json",
                        success: function (response) {


                            debugger;
                            if (response.IsValid = 1) {
                                Swal.fire({
                                    position: 'center',
                                    icon: 'success',
                                    title: 'Delete Successfully',
                                    showConfirmButton: false,
                                    timer: 3000
                                })
                                $("#tr_" + Id).remove();
                            }

                        },

                    });

                }
            },
            close: {
                text: 'NO',
                action: function () { }
            }
        }
    });
}
$(".ChangeStatus1").change(function () {
    debugger;
    var id = $(this).data("id");
    var item = id.split('_');
    var type = parseInt($(this).val());

    var Params = JSON.stringify(
        {
            TableId: item[0],
            type: type,
            Id: 0,
            Guid: item[1]
        });

    $.ajax({
        url: GetglobalDomain() + "/Role/ChangeStatus",
        type: 'POST',
        dataType: "json",
        contentType: "application/json",
        data: Params,
        success: function (response) {
            debugger;
            if (response.isvalid == 1) {
                var msg = 'Status Changes Successfully';
                if (type == 1) {
                    $("#" + id).val(0);
                    /* $(this).val('0');*/
                    msg = 'Status De-Active Successfully';
                }
                else {
                    $("#" + id).val(1);
                    /* $(this).val('1');*/
                    msg = 'Status Active Successfully';
                }
                Swal.fire({
                    position: 'center',
                    icon: 'success',
                    title: msg,
                    showConfirmButton: false,
                    timer: 3000
                })


            }






        }
    });

});

