$(document).ready(function () {
    $('a[data-toggle="tab"]').click(function () {
        var activeTab = $(this).attr('href').substring(1);
        $('#activeTab').val(activeTab);
        $('#searchForm').submit();
    });

    $('.toggle-trainees').click(function () {
        var id = $(this).data('id');
        var list = $('#trainees-' + id);

        if (list.is(':visible')) {
            list.hide();
        } else {
            list.show();
        }
    });

    $('select[name="pageSize"]').change(function () {
        $('#paginationForm').submit();
    });

    $('.pagination-buttons button').click(function () {
        var page = $(this).val();
        var activeTab = $('#activeTabPagination').val();
        var searchQuery = $('input[name="searchQuery"]').val();
        var sortOrder = $('input[name="sortOrder"]').val();
        $('#currentPage').val(page);
        $('#paginationForm').submit();
    });
});

function updatePageSizeFromSelect() {
    var pageSize = document.getElementById('pageSizeSelect').value;
    document.getElementById('pageSizeInput').value = pageSize;
    document.getElementById('pageSizeHidden').value = pageSize;
    document.getElementById('paginationForm').submit();
}

function updatePageSizeFromInput() {
    var pageSize = document.getElementById('pageSizeInput').value;
    document.getElementById('pageSizeSelect').value = pageSize;
    document.getElementById('pageSizeHidden').value = pageSize;
    document.getElementById('paginationForm').submit();
}
