document.addEventListener("DOMContentLoaded", function () {
  localStorage.removeItem("authToken");

  const formLogin = document.querySelector("#login-form");
  const errorContainer = document.querySelector("#error-container");

  // API call for login
  formLogin.addEventListener("submit", function (event) {
    event.preventDefault();

    errorContainer.innerHTML = "";

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;

    const data = {
      userName: username,
      password: password,
    };

    // Making a POST request using Fetch API
    // PROD: http://propro.zzzz.lt:1027/authentication/login
    // DEV: https://localhost:8080/authentication/login
    fetch("http://propro.zzzz.lt:1027/authentication/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      Accept: "application/json",
      body: JSON.stringify(data),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Authentication failed");
        }
        return response.json();
      })
      .then((result) => {
        console.log(result.token);
        // Store the token in local storage
        localStorage.setItem("authToken", result.token);

        // Redirect to home page
        window.location.href = "index.html";
      })
      .catch((error) => {
        displayErrorMessage("Netinkamas prisijungimo vardas arba slaptažodis");
      });
  });

  function displayErrorMessage(message) {
    const errorMessage = document.createElement("div");
    errorMessage.textContent = message;
    errorMessage.style.color = "red";
    errorMessage.style.marginTop = "10px";
    errorContainer.appendChild(errorMessage);
  }
});
