﻿$(document).ready(function () {
    $(".del").click(function () {
        var startLocationId = parseInt($(this).attr("startLocationId"));
        var finishLocationId = parseInt($(this).attr("finishLocationId"));
        if (confirm("Вы уверенны, что хотите удалить данное перемещение?")) {
            var model = {
                startLocationId: startLocationId,
                finishLocationId: finishLocationId
            };
            $.ajax({
                type: "POST",
                url: "/Home/DeleteTransfer",
                data: model,
                success: function (data) {
                    if (data)
                        $(this).parents('tr').remove();
                    else
                        $("#locationTable").replaceWith("<h3>В базе данных отсутствует искомая информация о перемещениях...</h3>");
                }
            });
        }
    });
});