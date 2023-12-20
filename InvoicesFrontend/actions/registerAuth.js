document.addEventListener("DOMContentLoaded", function () {
  const formRegister = document.querySelector("#register-form");

  // API call for register
  formRegister.addEventListener("submit", function (event) {
    event.preventDefault();

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const rePassword = document.getElementById("re-password").value;

    if (password !== rePassword) {
      // Display error message
      const errorMessage = document.createElement("div");
      errorMessage.textContent = "Pateikti slaptažodžiai nesutampa";
      errorMessage.style.color = "red";
      errorMessage.style.marginTop = "10px"
      formRegister.appendChild(errorMessage);
      return;
    }

    const data = {
      email: username,
      password: password,
    };

    // Making a POST request using Fetch API
    // PROD: http://propro.zzzz.lt:1027/authentication/login
    fetch("https://localhost:8080/authentication/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      Accept: "application/json",
      body: JSON.stringify(data),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Registration failed");
        }
        return response.json();
      })
      .then((result) => {
        console.log(result.token);
        // Store the token in session storage
        sessionStorage.setItem("authToken", result.token);

        // Redirect to home page
        window.location.href = "login.html";
      })
      .catch((error) => {
        console.log("Error:", error);
      });
  });
});
