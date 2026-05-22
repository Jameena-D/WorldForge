// Add event listener to handle form submission for creating a world
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

// Add event listener to handle adding new blocks to sections
document.addEventListener("DOMContentLoaded", function () {
    // Select all buttons with the class "add-block"
    document.querySelectorAll(".add-block").forEach(button => {
        button.addEventListener("click", function () {
            const sectionIndex = button.dataset.section;
            const sectionDiv = button.closest(".section");

            const blockCount = sectionDiv.querySelectorAll(".block").length;

            const newBlock = document.createElement("div");
            newBlock.classList.add("block");

            const nameLabel = document.createElement("label");
            nameLabel.innerText = "Name";
            const nameInput = document.createElement("input");
            nameInput.name = `Sections[${sectionIndex}].Blocks[${blockCount}].Name`;
            nameInput.classList.add("form-control");

            const contentLabel = document.createElement("label");
            contentLabel.innerText = "Content";
            const contentTextarea = document.createElement("textarea");
            contentTextarea.name = `Sections[${sectionIndex}].Blocks[${blockCount}].Content`;
            contentTextarea.classList.add("form-control");

            newBlock.appendChild(nameLabel);
            newBlock.appendChild(nameInput);
            newBlock.appendChild(contentLabel);
            newBlock.appendChild(contentTextarea);

            sectionDiv.insertBefore(newBlock, button);
        });
    });
});

// Add event listener to handle showing the confirmation modal for delete/unpublish actions
document.addEventListener('DOMContentLoaded', function () {
    var confirmModal = document.getElementById('confirmModal');
    confirmModal.addEventListener('show.bs.modal', function (event) {
        var btn = event.relatedTarget;
        var action = btn.getAttribute('data-action');
        var worldId = btn.getAttribute('data-world-id');
        var worldName = btn.getAttribute('data-world-name');

        var form = document.getElementById('confirmForm');
        var confBtn = document.getElementById('confirmBtn');
        var title = document.getElementById('confirmModalTitle');
        var body = document.getElementById('confirmModalBody');

        if (action === 'delete') {
            title.textContent = 'Delete World';
            body.innerHTML = '<p>Are you sure you want to <strong>permanently delete</strong> the world <em>"' + worldName + '"</em>? This cannot be undone.</p>';
            form.action = '/Admin/DeleteWorld';
            confBtn.style.background = 'linear-gradient(135deg, #c0392b, #a93226)';
            confBtn.textContent = 'Delete';
        } else {
            title.textContent = 'Make World Non-Public';
            body.innerHTML = '<p>This will remove <em>"' + worldName + '"</em> from the public listing. The world owner will retain access to it in their private worlds.</p>';
            form.action = '/Admin/UnpublishWorld';
            confBtn.style.background = 'linear-gradient(135deg, #e67e22, #d35400)';
            confBtn.textContent = 'Make Non-Public';
        }

        // Inject the worldId hidden input
        var existing = form.querySelector('input[name="worldId"]');
        if (existing) existing.remove();
        var hidden = document.createElement('input');
        hidden.type = 'hidden';
        hidden.name = 'worldId';
        hidden.value = worldId;
        form.prepend(hidden);
    });
});