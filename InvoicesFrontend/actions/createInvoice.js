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
      .then((data) => {
        // Handle successful response
        console.log("Invoice created:", data);
        // Redirect or perform any other actions as needed ... TODO
      });
  });
});
