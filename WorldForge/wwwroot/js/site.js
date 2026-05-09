document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("createWorldForm");
    if (!form) return;

    form.addEventListener("submit", function () {
        const messageBox = document.getElementById("formMessage");

        if (messageBox) {
            messageBox.innerHTML = "";
        }
        //MVC form submission will handle the rest, so we don't need to do anything else here.
    });
});