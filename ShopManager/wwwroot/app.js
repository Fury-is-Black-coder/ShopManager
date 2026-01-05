const API = "/api/products";

//async function loadProducts(active = "") {
//    let url = API;
//    if (active !== "") url += `?active=${active}`;

//    const res = await fetch(url);
//    const data = await res.json();

//    const tbody = document.getElementById("products");
//    tbody.innerHTML = "";

//    data.forEach(p => {
//        tbody.innerHTML += `
//            <tr class="${p.isActive ? "" : "inactive"}">
//                <td>${p.name}</td>
//                <td>${p.price}</td>
//                <td>${p.sku}</td>
//                <td>${p.isActive ? "Aktywny" : "Nieaktywny"}</td>
//                <td>
//                    <button onclick="toggle('${p.id}', ${p.isActive})">
//                        ${p.isActive ? "Dezaktywuj" : "Aktywuj"}
//                    </button>
//                    <button onclick="removeProduct('${p.id}')">Usuń</button>
//                </td>
//            </tr>
//        `;
//    });
//}

async function loadProducts(active = "") {
    let url = API;
    if (active !== "") url += `?active=${active}`;

    const res = await fetch(url);
    const data = await res.json();

    const tbody = document.getElementById("products");
    tbody.innerHTML = "";

    data.forEach(p => {
        tbody.innerHTML += `
            <tr class="${p.isActive ? "" : "inactive"}">
                <td>
                    <input value="${p.name}" id="name-${p.id}">
                </td>
                <td>
                    <input type="number" step="0.01" value="${p.price}" id="price-${p.id}">
                </td>
                <td>${p.sku}</td>
                <td>${p.isActive ? "Aktywny" : "Nieaktywny"}</td>
                <td>
                    <button class="save" onclick="updateProduct('${p.id}')">Zapisz</button>
                    <button class="toggle" onclick="toggle('${p.id}', ${p.isActive})">
                        ${p.isActive ? "Dezaktywuj" : "Aktywuj"}
                    </button>
                    <button class="delete" onclick="removeProduct('${p.id}')">Usuń</button>

                </td>
            </tr>
        `;
    });
}


async function addProduct() {
    const nameInput = document.getElementById("name");
    const priceInput = document.getElementById("price");
    const skuInput = document.getElementById("sku");

    const nameValue = nameInput.value.trim();
    const priceValue = parseFloat(priceInput.value);
    const skuValue = skuInput.value.trim();

    if (!nameValue || !skuValue || isNaN(priceValue)) {
        alert("Uzupełnij wszystkie pola poprawnie");
        return;
    }

    const product = {
        name: nameValue,
        price: priceValue,
        sku: skuValue
    };

    const res = await fetch(API, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(product)
    });

    if (!res.ok) {
        const err = await res.text();
        alert("Błąd: " + err);
        return;
    }

    nameInput.value = "";
    priceInput.value = "";
    skuInput.value = "";

    loadProducts();
}

async function updateProduct(id) {
    const name = document.getElementById(`name-${id}`).value.trim();
    const price = parseFloat(document.getElementById(`price-${id}`).value);

    if (!name || isNaN(price)) {
        alert("Nieprawidłowe dane");
        return;
    }

    const res = await fetch(`${API}/${id}`, {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name, price })
    });

    if (!res.ok) {
        alert("Błąd zapisu");
        return;
    }

    loadProducts();
}

async function toggle(id, isActive) {
    const action = isActive ? "deactivate" : "activate";
    await fetch(`${API}/${id}/${action}`, { method: "PATCH" });
    loadProducts();
}

async function removeProduct(id) {
    await fetch(`${API}/${id}`, { method: "DELETE" });
    loadProducts();
}

loadProducts();
