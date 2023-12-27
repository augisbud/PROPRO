document.addEventListener("DOMContentLoaded", function () {
  // Check if the user is authenticated
  const authToken = sessionStorage.getItem("authToken");

  if (!authToken) {
    window.location.href = "login.html";
  }

  // Add event for form submission
  const invoiceForm = document.getElementById("new-invoice");
  invoiceForm.addEventListener("submit", function (event) {
    event.preventDefault();

    const formData = new FormData(invoiceForm);

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
        products: [
          {
            name: formData.get("products[][name]"),
            quantity: parseFloat(formData.get("products[][quantity]")),
            unit: formData.get("products[][quantity_type]"),
            price: parseFloat(formData.get("products[][price]")),
          },
        ],
      };

    // PROD: http://propro.zzzz.lt:1027/invoice/create
    // DEV: https://localhost:8080/invoice/create
    fetch("https://localhost:8080/invoice/create", {
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
        console.log("ID: ", invoiceId);
        window.location.href = `index.html?invoiceId=${invoiceId}`;
      });
  });
});
