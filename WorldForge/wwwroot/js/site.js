document.addEventListener("DOMContentLoaded", function () {
    const form = document.getElementById("createWorldForm");
    if (!form) return;

    const messageBox = document.getElementById("formMessage");

    form.addEventListener("submit", async function (e) {
        e.preventDefault();
        messageBox.innerHTML = "";

        // Oude serverfouten leegmaken
        document.querySelectorAll("[data-valmsg-for]").forEach(span => {
            span.textContent = "";
            span.classList.remove("field-validation-error");
            span.classList.add("field-validation-valid");
        });

        // Eerst client-side validatie uitvoeren
        if (!$(form).valid()) {
            return;
        }

        const data = {
            name: document.getElementById("Name").value.trim(),
            description: document.getElementById("Description").value.trim(),
            worldType: document.getElementById("WorldType").value,
            isPublic: document.getElementById("IsPublic").checked
        };

        try {
            const response = await fetch("/api/worlds", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            });

            const result = await response.json();

            if (response.ok) {
                messageBox.innerHTML = `<div class="alert alert-success">${result.message}</div>`;
                form.reset();
                $(form).validate().resetForm();
            } else {
                if (result.errors) {
                    for (const fieldName in result.errors) {
                        const span = document.querySelector(`[data-valmsg-for='${fieldName}']`);
                        if (span) {
                            span.textContent = result.errors[fieldName].join(" ");
                            span.classList.remove("field-validation-valid");
                            span.classList.add("field-validation-error");
                        }
                    }

                    messageBox.innerHTML = `<div class="alert alert-danger">Please fix the validation errors.</div>`;
                } else {
                    messageBox.innerHTML = `<div class="alert alert-danger">${result.message ?? "Something went wrong."}</div>`;
                }
            }
        } catch (error) {
            messageBox.innerHTML = `<div class="alert alert-danger">There was an error while connecting to the server.</div>`;
            console.error(error);
        }
    });
});