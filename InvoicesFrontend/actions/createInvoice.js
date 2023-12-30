var products = [];

document.addEventListener("DOMContentLoaded", function () {
  // Check if the user is authenticated
  const authToken = localStorage.getItem("authToken");

  if (!authToken) {
    window.location.href = "login.html";
  }

  // Add event for form submission
  const invoiceForm = document.getElementById("new-invoice");
  invoiceForm.addEventListener("submit", function (event) {
    event.preventDefault();

    const formData = new FormData(invoiceForm);

    // Convert local date to UTC before sending it to the server
    const localDate = new Date(formData.get("paymentDate"));
    const utcDate = new Date(
      Date.UTC(
        localDate.getUTCFullYear(),
        localDate.getUTCMonth(),
        localDate.getUTCDate(),
        localDate.getUTCHours(),
        localDate.getUTCMinutes(),
        localDate.getUTCSeconds()
      )
    );

    const invoiceData = {
      name: formData.get("name"),
      buyer: {
        name: formData.get("buyerName"),
        code: formData.get("buyerRegistrationCode"),
        vat: formData.get("buyerVAT"),
        address: formData.get("buyerAddress"),
        email: formData.get("buyerEmail"),
        phone: formData.get("buyerPhone"),
      },
      discount: parseFloat(formData.get("discount")),
      products: products,
      paymentDate: utcDate.toISOString(),
    };

    // PROD: http://propro.zzzz.lt:1027/invoice/create
    // DEV: https://localhost:8080/invoice/create
    fetch("http://propro.zzzz.lt:1027/invoice/create", {
      method: "POST",
      headers: {
        Authorization: `Bearer ${authToken}`,
        "Content-Type": "application/json",
      },
      Accept: "application/json",
      body: JSON.stringify(invoiceData),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Invoice creation failed");
        }

        return response.json();
      })
      .then((invoiceResponse) => {
        // Handle successful response
        const invoiceId = invoiceResponse.id;
        window.location.href = `index.html?invoiceId=${invoiceId}`;
      });
  });
});

document.addEventListener('DOMContentLoaded', function () {
  var addNewProduct = document.getElementById("addNewProduct");
  addNewProduct.addEventListener("click", createTableRow);
});

function createTableRow() {
  var newRow = document.createElement("tr");

  // Product name creation and validation
  var cell1 = document.createElement("td");
  var nameInput = document.getElementById("products[][name]");
  var nameValue = nameInput.value;
  if (nameValue.trim() === "") {
    nameInput.style.border = "2px solid red";
    var nameErrorMessage = document.getElementById("error-message-name");
    if (!nameErrorMessage) {
      nameErrorMessage = document.createElement("div");
      nameErrorMessage.id = "error-message-name";
      nameErrorMessage.style.color = "red";
      nameInput.parentNode.appendChild(nameErrorMessage);
    }
    nameErrorMessage.textContent = "Įveskite produkto pavadinimą.";
    return;
  }
  nameInput.style.border = "";
  var existingNameErrorMessage = document.getElementById("error-message-name");
  if (existingNameErrorMessage) {
    existingNameErrorMessage.remove();
  }
  cell1.textContent = nameValue;
  newRow.appendChild(cell1);

  // Product quantity creation and validation
  var cell2 = document.createElement("td");
  var quantityInput = document.getElementById("products[][quantity]");
  var quantityValue = quantityInput.value;
  if (
    quantityValue.trim() === "" ||
    !Number.isInteger(Number(quantityValue)) ||
    Number(quantityValue) < 0
  ) {
    quantityInput.style.border = "2px solid red";
    var errorMessage = document.getElementById("error-message-quantity");
    if (!errorMessage) {
      errorMessage = document.createElement("div");
      errorMessage.id = "error-message-quantity";
      errorMessage.style.color = "red";
      errorMessage.style.width = "115px";
      quantityInput.parentNode.appendChild(errorMessage);
    }
    errorMessage.textContent = "Įveskite teisingą produkto kiekį.";
    return;
  }
  quantityInput.style.border = "";
  var existingErrorMessage = document.getElementById("error-message-quantity");
  if (existingErrorMessage) {
    existingErrorMessage.remove();
  }
  cell2.textContent = quantityValue;
  newRow.appendChild(cell2);

  // Product quantity tipe creation
  var cell3 = document.createElement("td");
  cell3.textContent =
    document.getElementById("products[][quantity_type]").value || "";
  newRow.appendChild(cell3);

  // Product price creation and validation
  var cell4 = document.createElement("td");
  var priceInput = document.getElementById("products[][price]");
  var priceValue = priceInput.value;
  if (!isNaN(priceValue) && priceValue.trim() !== "") {
    priceInput.style.border = "";
    var existingPriceErrorMessage = document.getElementById(
      "error-message-price"
    );
    if (existingPriceErrorMessage) {
      existingPriceErrorMessage.remove();
    }
    cell4.textContent = priceValue;
  } else {
    priceInput.style.border = "2px solid red";
    var priceErrorMessage = document.getElementById("error-message-price");
    if (!priceErrorMessage) {
      priceErrorMessage = document.createElement("div");
      priceErrorMessage.id = "error-message-price";
      priceErrorMessage.style.color = "red";
      priceErrorMessage.style.width = "115px";
      priceInput.parentNode.appendChild(priceErrorMessage);
    }
    priceErrorMessage.textContent = "Įveskite produkto kainą.";
    return;
  }
  newRow.appendChild(cell4);

  // Product price calculation
  var cell5 = document.createElement("td");
  var product = Number(quantityValue) * Number(priceValue);
  cell5.textContent = product;
  newRow.appendChild(cell5);

  // Add product details to the products array
  var product = {
    name: nameValue,
    quantity: parseFloat(quantityValue),
    unit: document.getElementById("products[][quantity_type]").value || '',
    price: parseFloat(priceValue),
  }
  products.push(product);

  // Creating a working delete button
  var cell6 = document.createElement("td");
  var deleteButton = document.createElement("a");
  deleteButton.className = "btn btn-danger";
  deleteButton.textContent = "Ištrinti";
  deleteButton.addEventListener("click", function () {
    newRow.remove();
  });
  cell6.appendChild(deleteButton);
  newRow.appendChild(cell6);
  document.getElementById("productsList").appendChild(newRow);
}

