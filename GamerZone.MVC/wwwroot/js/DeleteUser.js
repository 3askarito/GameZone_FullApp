$(document).ready(function () {
    $('.js-delete').on('click', function () {
        var btn = $(this);
        bootbox.confirm({
            message: '"Are you sure that you want to delete this user?',
            buttons: {
                confirm: {
                    label: 'Yes',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-secondary'
                }
            },
            callback: function (result) {
                if (result) {
                    $.ajax({
                        url: "/api/user/?userId=" + btn.data('id'),
                        method: "DELETE",
                        success: function () {
                            btn.parents('tr').fadeOut();
                            $('#alert').removeClass('d-none');
                            setTimeout(function () { $('#alert').addClass('d-none'); }, 3000);
                        },
                        error: function () {
                            alert("something Went Wrong!!!");
                        }
                    });
                }
            }
        });
        //var result = confirm("Are you sure that you want to delete that user?!");
        //if (result) {
        //    console.log(btn.data('id'));
        //}
    });
});