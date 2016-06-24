$.fn.clearValidation = function(){var v = $(this).validate();$('[name]',this).each(function(){v.successList.push(this);v.showErrors();});v.resetForm();v.reset();};

function setTableMembers(tableId) {
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
        }, bAutoWidth: false,
        "columnDefs": [
            { "targets": [0], "width": "15", "className": "dt-center" },
            { "targets": [1], "visible": false, "searchable": false },
            { "targets": [2], "width": "50", "className": "dt-center" },
            { "targets": [3], "width": "150" },
            { "targets": [4], "width": "50", "className": "dt-center" },
            { "targets": [6], "width": "50", "className": "dt-center" },
            { "targets": [7], "width": "50", "className": "dt-center" },
            { "targets": [8], "width": "50" },
            { "targets": [9], "width": "50" },
        ], fixedColumns: true,
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
        }, bAutoWidth: false,
        "columnDefs": [
            { "targets": [0], "width": "50", "className": "dt-center" },
            { "targets": [1], "visible": false, "searchable": false }
        ], fixedColumns: true,
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

function setTableProductDetailsAsTableList(tableId) {
    var tableMember = tableId.DataTable({
        responsive: true,
        "ordering": false,
        "bLengthChange": false,
        "pageLength": 5,
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
        }, bAutoWidth: false,
        "columnDefs": [
            { "targets": [0], "width": "50", "className": "dt-center" },
            { "targets": [1], "width": "80", "className": "dt-center" },
            { "targets": [3], "width": "50", "className": "dt-center" }
        ],fixedColumns: true,
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