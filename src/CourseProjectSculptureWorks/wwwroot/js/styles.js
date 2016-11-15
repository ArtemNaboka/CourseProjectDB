$(document).ready(function () {
    $(".del").click(function () {
        var styleId = parseInt($(this).attr("styleId"));
        if (confirm("Вы уверенны, что хотите удалить данный стиль?")) {
            var model = {
                Integer: styleId
            };
            $.ajax({
                type: "POST",
                url: "/Home/DeleteStyle",
                data: model,
                success: function (data) {
                    if (data)
                        $("#" + styleId).remove();
                    else
                        $("#styleTable").replaceWith("<h3>В базе данных отсутствует искомая информация о стилях скульптуры...</h3>");
                }
            });
        }
    });
});