async function postData(url, data) {
    let response = await fetch(url, {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(data)
    });

    return response;
}

document.getElementById("addInternshipDirectionName").addEventListener("click", async function () {
    var newDirection = document.getElementById("newInternshipDirectionName").value.trim();
    if (newDirection) {
        let response = await postData('/CreateNewResource/AddInternshipDirection', newDirection);
        if (response.ok) {
            var select = document.getElementById("internshipDirectionName");
            var option = document.createElement("option");
            option.value = newDirection;
            option.textContent = newDirection;
            select.appendChild(option);
            select.value = newDirection;
            document.getElementById("newInternshipDirectionName").value = "";
        } else {
            try {
                let errorText = await response.text();

                let match = errorText.match(/System\.ArgumentException: (.+)/);
                if (match && match[1]) {
                    errorText = match[1];
                }

                alert(errorText || "Ошибка при добавлении ресурса");
            } catch (e) {
                alert("Ошибка при обработке ответа сервера");
            }
        }
    }
});

document.getElementById("addCurrentProjectName").addEventListener("click", async function () {
    var newProject = document.getElementById("newCurrentProjectName").value.trim();
    if (newProject) {
        let response = await postData('/CreateNewResource/AddCurrentProject', newProject);
        if (response.ok) {
            var select = document.getElementById("currentProjectName");
            var option = document.createElement("option");
            option.value = newProject;
            option.textContent = newProject;
            select.appendChild(option);
            select.value = newProject;
            document.getElementById("newCurrentProjectName").value = "";
        } else {
            try {
                let errorText = await response.text();

                let match = errorText.match(/System\.ArgumentException: (.+)/);
                if (match && match[1]) {
                    errorText = match[1];
                }

                alert(errorText || "Ошибка при добавлении ресурса");
            } catch (e) {
                alert("Ошибка при обработке ответа сервера");
            }
        }
    }
});

