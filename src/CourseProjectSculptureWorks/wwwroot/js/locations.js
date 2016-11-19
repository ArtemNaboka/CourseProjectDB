$(document).ready(function () {
    $(".del").click(function () {
        var locationId = parseInt($(this).attr("locationId"));
        if (confirm("Вы уверенны, что хотите удалить данное местоположение?")) {
            var model = {
                Integer: locationId
            };
            $.ajax({
                type: "POST",
                url: "/Home/DeleteLocation",
                data: model,
                success: function (data) {
                    if (data)
                        $("#" + locationId).remove();
                    else
                        $("#locationTable").replaceWith("<h3>В базе данных отсутствует искомая информация о местоположениях...</h3>");
                }
            });
        }
    });
});