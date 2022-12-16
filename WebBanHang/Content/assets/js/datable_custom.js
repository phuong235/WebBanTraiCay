$(document).ready(function () {
    $('#example').DataTable({
        "columnDefs": [{
            "targets": 'no-sort',
            "orderable": false,
        }],
        "language": {
            "paginate": {
                "previous": "Trước",
                "next": "Sau"
            },
            "search": "<strong>Tìm kiếm:</strong>",
            "lengthMenu": 'Hiển thị <select>' +
                '<option value="6" selected>6</option>' +
                '<option value="20">12</option>' +
                '<option value="-1">Tất cả</option>' +
                '</select> bản ghi'


        },
        "pageLength": 6,
        "bDestroy": true

    });
});