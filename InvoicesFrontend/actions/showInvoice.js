document.addEventListener("DOMContentLoaded", function () {
  // Check if the user is authenticated
  const authToken = sessionStorage.getItem("authToken");

  if (!authToken) {
    window.location.href = "login.html";
  }

  // Retrieve invoice id from query params
  const searchParams = new URLSearchParams(window.location.search);
  const paramInvoiceId = searchParams.get("invoiceId");
  console.log("LALA: ", paramInvoiceId);

  // fetch invoice by id
  // PROD: http://propro.zzzz.lt:1027/invoice/{}
  // DEV: https://localhost:8080/invoice/{}
  fetch(`https://localhost:8080/invoice/${paramInvoiceId}`, {
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

        // Convert creationDate to a simpler format
        const creationDate = new Date(invoiceDTO.creationDate);
        const formattedCreationDate = creationDate.toISOString().split('T')[0];

        document.getElementById("invoiceID").innerText = invoiceDTO.id;
        document.getElementById("buyerName").innerText = invoiceDTO.buyer.name;
        document.getElementById("buyerRegCode").innerText = invoiceDTO.buyer.code;
        document.getElementById("buyerVAT").innerText = invoiceDTO.buyer.vat;
        document.getElementById("buyerPhone").innerText = invoiceDTO.buyer.phone;
        document.getElementById("buyerAdress").innerText = invoiceDTO.buyer.address;
        document.getElementById("buyerMail").innerText = invoiceDTO.buyer.email;
        document.getElementById("invoiceDate").innerText = formattedCreationDate;
        // document.getElementById("dueDate").innerText = invoiceDTO;
        // document.getElementById("").innerText = invoiceDTO;
    })
});
