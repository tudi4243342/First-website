const API_URL = "http://localhost:5152/api/fruits"; // Adjust port based on your running Visual Studio project

// Function for index.html (Customer View)
async function loadCatalog() {
    const response = await fetch(API_URL);
    const users = await response.json();

    let html = "<ul>";
    users.forEach(user => {
        html += `<li><strong>${user.name}</strong> [ID: ${user.id}]</li>`;
    });
    html += "</ul>";

    document.getElementById("catalog").innerHTML = html;
}

// Function for admin.html (Moderator View)
async function updateName(id, name) {
    const response = await fetch(`${API_URL}/${id}?name=${name}`, {
        method: 'PUT'
    });

    if (response.ok) {
        alert("name updated successfully in database!");
    } else {
        alert("Failed to update name. Verify user ID.");
    }
}