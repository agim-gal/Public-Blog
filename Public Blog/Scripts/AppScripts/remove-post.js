function OnSuccess(data) {
    var target = $("#removeLink_" + data.idPost);
    if (data.request == "ok") {
        target.empty();
        target.append("удалено");
        $("#post_" + data.idPost).hide("slow");
    } else {
        target.append("<p style=\"color: red\">" + data.request + "</p>");
    }
}