document.addEventListener("DOMContentLoaded", function () {
  // Check if the user is authenticated
  const authToken = localStorage.getItem("authToken");
  if (!authToken) {
    window.location.href = "login.html";
  }

  // Add new product event
  document.getElementById("addNewProduct").addEventListener("click", createTableRow);

  // Form submission
  const invoiceForm = document.getElementById("new-invoice");
  invoiceForm.addEventListener("submit", function (event) {
    event.preventDefault();
    const formData = new FormData(invoiceForm);

    // Products from table rows
    const products = Array.from(document.getElementById("productsList").rows).map(row => {
      return {
        name: row.cells[0].textContent,
        quantity: parseFloat(row.cells[1].textContent),
        unit: row.cells[2].textContent,
        price: parseFloat(row.cells[3].textContent)
      };
    });

    // Invoice data
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
      paymentDate: new Date(formData.get("paymentDate")).toISOString()
    };

    // Sending invoice data
    fetch("http://propro.zzzz.lt:1027/invoice/create", {
      method: "POST",
      headers: {
        Authorization: `Bearer ${authToken}`,
        "Content-Type": "application/json"
      },
      body: JSON.stringify(invoiceData)
    })
    .then(response => {
      if (!response.ok) {
        throw new Error("Invoice creation failed");
      }
      return response.json();
    })
    .then(invoiceResponse => {
      const invoiceId = invoiceResponse.id;
      window.location.href = `index.html?invoiceId=${invoiceId}`;
    });
  });
});

function createTableRow() {
  const productsList = document.getElementById("productsList");
  const nameInput = document.getElementById("productName");
  const quantityInput = document.getElementById("productQuantity");
  const unitInput = document.getElementById("productQuantityType");
  const priceInput = document.getElementById("productPrice");

  if (validateInputs(nameInput, quantityInput, priceInput)) {
    const row = productsList.insertRow();
    row.insertCell(0).textContent = nameInput.value;
    row.insertCell(1).textContent = quantityInput.value;
    row.insertCell(2).textContent = unitInput.value;
    row.insertCell(3).textContent = priceInput.value;
    
    // Delete button
    const deleteCell = row.insertCell(4);
    const deleteButton = document.createElement("button");
    deleteButton.textContent = "Ištrinti";
    deleteButton.className = "btn btn-danger"; // Add your classes for styling
    deleteButton.onclick = function() {
      row.remove();
    };
    deleteCell.appendChild(deleteButton);

    nameInput.value = '';
    quantityInput.value = 1;
    unitInput.value = 'vnt.';
    priceInput.value = 0;
  }
}

function validateInputs(nameInput, quantityInput, priceInput) {
  let isValid = true;
  if (nameInput.value.trim() === "") {
    nameInput.style.border = "2px solid red";
    isValid = false;
  } else {
    nameInput.style.border = "";
  }

  if (quantityInput.value.trim() === "" || isNaN(quantityInput.value) || Number(quantityInput.value) < 0) {
    quantityInput.style.border = "2px solid red";
    isValid = false;
  } else {
    quantityInput.style.border = "";
  }

  if (priceInput.value.trim() === "" || isNaN(priceInput.value)) {
    priceInput.style.border = "2px solid red";
    isValid = false;
  } else {
    priceInput.style.border = "";
  }

  return isValid;
}
