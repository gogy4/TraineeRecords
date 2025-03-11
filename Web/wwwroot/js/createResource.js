async function postData(url, data) {
    let response = await fetch(url, {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(data)
    });
    return response;
}

document.addEventListener("DOMContentLoaded", function () {
    document.querySelector("#createResourceForm").addEventListener("submit", async function (event) {
        event.preventDefault();

        var newResource = document.getElementById("resourceName").value.trim();
        var resourceType = document.getElementById("resourceType").value;

        if (!newResource) {
            alert(resourceType === "Direction" ? "Введите направление стажировки!" : "Введите проект для стажировки!");
            return;
        }

        let url = resourceType === "Direction"
            ? '/CreateNewResource/AddInternshipDirection'
            : '/CreateNewResource/AddCurrentProject';

        try {
            let response = await postData(url, newResource);

            if (response.ok) {
                const resource = await response.json();
                document.getElementById("resourceId").value = resource.id;
                document.querySelector("#createResourceForm").submit();
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
        } catch (error) {
            alert("Ошибка при обращении к серверу");
        }
    });
});
