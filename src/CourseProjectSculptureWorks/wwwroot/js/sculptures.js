$(document).ready(function () {
    $(".del").click(function () {
        var sculptureId = parseInt($(this).attr("sculptureId"));
        if (confirm("Вы уверенны, что хотите удалить данную скульптуру?")) {
            var model = {
                Integer: sculptureId
            };
            $.ajax({
                type: "POST",
                url: "/Home/DeleteSculpture",
                data: model,
                success: function (data) {
                    if (data)
                        $("#" + sculptureId).remove();
                    else
                        $("#sculptureTable").replaceWith("<h3>В базе данных отсутствует искомая информация о скульптурах...</h3>");
                }
            });
        }
    });
});