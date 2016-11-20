$(document).ready(function () {
    $(".del").click(function () {
        var excursionTypeId = parseInt($(this).attr("excursionTypeId"));
        if (confirm("Вы уверенны, что хотите удалить данный вид экскурсии?")) {
            var model = {
                Integer: excursionTypeId
            };
            $.ajax({
                type: "POST",
                url: "/Home/DeleteExcursionType",
                data: model,
                success: function (data) {
                    if (data)
                        $("#" + excursionTypeId).remove();
                    else
                        $("#excursionTypeTable").replaceWith("<h3>В базе данных отсутствует искомая информация о местоположениях...</h3>");
                }
            });
        }
    });
});