
function setDataTablesSimple() {
    var tableMember = $('#dataTablesSimple').DataTable({
        responsive: true,
        "ordering": false,
        "oLanguage": {
            "sLengthMenu": "แสดงผล _MENU_ รายการ/หน้า",
            "sZeroRecords": "ไม่เจอข้อมูลที่ค้นหา",
            "sInfo": "แสดงรายการ _START_ ถึง _END_ ของทั้งหมด _TOTAL_ รายการ",
            "sInfoEmpty": "ไม่พบรายการ",
            "sInfoFiltered": "(จากทั้งหมด _MAX_ รายการ)",
            "sSearch": "ค้นหา :",
            "oPaginate": {
                "sFirst": "หน้าแรก",
                "sLast": "หน้าสุดท้าย",
                "sNext": "ถัดไป",
                "sPrevious": "ก่อนหน้า"
            }
        },
        "columnDefs": [{
            "targets": [1],
            "visible": false,
            "searchable": false
        }],
        preDrawCallback: function (settings) {
            var api = new $.fn.dataTable.Api(settings);
            var pagination = $(this)
                .closest('.dataTables_wrapper')
                .find('.dataTables_paginate');

            if (api.page.info().pages <= 1) {
                pagination.hide();
            }
            else {
                pagination.show();
            }
            //$('.dataTables_filter input').addClass('form-control').css({"width":"15em"});
            //$('.dataTables_length select').addClass("form-control");
        }
    });

    $('#dataTablesSimple tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            tableMember.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    return tableMember;
}
function setDataTables(tableId) {
    var tableMember = tableId.DataTable({
        responsive: true,
        "ordering": false,
        "oLanguage": {
            "sLengthMenu": "แสดงผล _MENU_ รายการ/หน้า",
            "sZeroRecords": "ไม่เจอข้อมูลที่ค้นหา",
            "sInfo": "แสดงรายการ _START_ ถึง _END_ ของทั้งหมด _TOTAL_ รายการ",
            "sInfoEmpty": "ไม่พบรายการ",
            "sInfoFiltered": "(จากทั้งหมด _MAX_ รายการ)",
            "sSearch": "ค้นหา :",
            "oPaginate": {
                "sFirst": "หน้าแรก",
                "sLast": "หน้าสุดท้าย",
                "sNext": "ถัดไป",
                "sPrevious": "ก่อนหน้า"
            }
        },
        "columnDefs": [{
            "targets": [1],
            "visible": false,
            "searchable": false
        }],
        preDrawCallback: function (settings) {
            var api = new $.fn.dataTable.Api(settings);
            var pagination = $(this)
                .closest('.dataTables_wrapper')
                .find('.dataTables_paginate');

            if (api.page.info().pages <= 1) {
                pagination.hide();
            }
            else {
                pagination.show();
            }
            //$('.dataTables_filter input').addClass('form-control').css({"width":"15em"});
            //$('.dataTables_length select').addClass("form-control");
        }
    });
    tableId.find('tbody').on('click', 'tr', function () {
        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected');
        } else {
            tableMember.$('tr.selected').removeClass('selected');
            $(this).addClass('selected');
        }
    });

    return tableMember;
}