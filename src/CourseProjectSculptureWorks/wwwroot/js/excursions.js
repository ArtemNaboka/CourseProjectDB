$(document).ready(function () {
    $(".del").click(function () {
        var excursionId = parseInt($(this).attr("excursionId"));
        if (confirm("Вы уверенны, что хотите удалить данную экскурсию?")) {
            var model = {
                Integer: excursionId
            };
            $.ajax({
                type: "POST",
                url: "/Home/DeleteExcursion",
                data: model,
                success: function (data) {
                    if (data)
                        $("#" + excursionId).remove();
                    else
                        $("#locationTable").replaceWith("<h3>В базе данных отсутствует искомая информация о местоположениях...</h3>");
                }
            });
        }
    });
});