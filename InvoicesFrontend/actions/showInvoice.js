document.addEventListener("DOMContentLoaded", function () {
  // Check if the user is authenticated
  const authToken = sessionStorage.getItem("authToken");

  if (!authToken) {
    window.location.href = "login.html";
  }

  // Retrieve invoice id from query params
  const searchParams = new URLSearchParams(window.location.search);
  const paramInvoiceId = searchParams.get("invoiceId");

  // fetch invoice by id
  // PROD: http://propro.zzzz.lt:1027/invoice/{}
  // DEV: https://localhost:8080/invoice/{}
  fetch(`http://propro.zzzz.lt:1027/invoice/${paramInvoiceId}`, {
    method: "GET",
    headers: {
      Authorization: `Bearer ${authToken}`,
      "Content-Type": "application/json",
    },
    Accept: "application/json",
  })
    .then((response) => {
        if (!response.ok) {
            throw new Error("Invoice creation failed");
        }

        return response.json();
    })
    .then((invoiceDTO) => {
        var total = 0;
        var grandTotal = 0;
        const taxRate = 0.21;

        // Convert creationDate to a simpler format
        const creationDate = new Date(invoiceDTO.creationDate);
        const formattedCreationDate = creationDate.toISOString().split('T')[0];

        const paymentDate = new Date(invoiceDTO.paymentDate);
        const formattedPaymentDate = paymentDate.toISOString().split('T')[0];

        // Display buyers information
        document.getElementById("invoiceID").innerText = invoiceDTO.id;
        document.getElementById("buyerName").innerText = invoiceDTO.buyer.name;
        document.getElementById("buyerRegCode").innerText = invoiceDTO.buyer.code;
        document.getElementById("buyerVAT").innerText = invoiceDTO.buyer.vat;
        document.getElementById("buyerPhone").innerText = invoiceDTO.buyer.phone;
        document.getElementById("buyerAdress").innerText = invoiceDTO.buyer.address;
        document.getElementById("buyerMail").innerText = invoiceDTO.buyer.email;
        document.getElementById("invoiceDate").innerText = formattedCreationDate;
        document.getElementById("dueDate").innerText = formattedPaymentDate;

        // Display products
        const productsTable = document.getElementById("productsTableBody");

        invoiceDTO.products.forEach((product, index) => {
          const newRow = productsTable.insertRow(-1);

          newRow.insertCell(0).textContent = index + 1;
          newRow.insertCell(1).textContent = product.name;
          newRow.insertCell(2).textContent = product.unit;
          newRow.insertCell(3).textContent = product.quantity;
          newRow.insertCell(4).textContent = product.price;
          newRow.insertCell(5).textContent = product.quantity * product.price;

          total += product.price;
        });

        const tax = total * taxRate;
        grandTotal = total + tax;

        // Display overall information
        document.getElementById("total").innerText = total;
        document.getElementById("tax").innerText = tax;
        document.getElementById("grandTotal").innerText = grandTotal;
    })
});
