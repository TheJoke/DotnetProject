$(document).ready(function () {
    $('#articleDropdown').change(function () {
        var articleId = $(this).val();
        if (articleId) {
            $.ajax({
                url: '/Reclamations/GetArticleName',
                type: 'GET',
                data: { id: articleId },
                success: function (data) {
                    $('#articleName').text(data);
                },
                error: function () {
                    $('#articleName').text('Error fetching article name');
                }
            });
        } else {
            $('#articleName').text('');
        }
    });
});
