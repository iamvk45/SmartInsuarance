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
