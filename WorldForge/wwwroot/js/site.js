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