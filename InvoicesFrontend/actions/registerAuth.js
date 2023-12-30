document.addEventListener("DOMContentLoaded", function () {
  const formRegister = document.querySelector("#register-form");
  const errorContainer = document.querySelector("#error-container");

  // API call for register
  formRegister.addEventListener("submit", function (event) {
    event.preventDefault();

    errorContainer.innerHTML = "";

    const username = document.getElementById("username").value;
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const rePassword = document.getElementById("re-password").value;

    if (password !== rePassword) {
      displayErrorMessage("Pateikti slaptažodžiai nesutampa");
      return;
    }

    const data = {
      email: email,
      userName: username,
      password: password,
    };

    // Making a POST request using Fetch API
    // PROD: http://propro.zzzz.lt:1027/authentication/register
    fetch("http://propro.zzzz.lt:1027/authentication/register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      Accept: "application/json",
      body: JSON.stringify(data),
    })
      .then((response) => {
        if (!response.ok) {
          return response.json().then((error) => {
            throw new Error(error.message || "Registration failed");
          });

        }
        return response.json();
      })
      .then((result) => {
        console.log(result.token);
        // Store the token in local storage
        localStorage.setItem("authToken", result.token);

        // Redirect to home page
        window.location.href = "login.html";
      })
      .catch((error) => {
        console.log("Error:", error);
        displayErrorMessage(error.message || "Registration failed");
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