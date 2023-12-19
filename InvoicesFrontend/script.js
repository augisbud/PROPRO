document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('form')

    form.addEventListener('submit', function (event) {
        event.preventDefault();

        const username = document.getElementById('username').value;
        const password = document.getElementById('password').value;

        const data = {
            email: username,
            password: password
        };

        // Making a POST request using Fetch API
        fetch('http://propro.zzzz.lt:1027/authentication/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            Accept: "application/json",
            body: JSON.stringify(data),
        })
            .then(response => {
                if(!response.ok){
                    throw new Error('Authentication failed');
                }
                return response.json();
            })
            .then(result => {
                console.log(result.token);
                // Store the token in session storage
                sessionStorage.setItem('authToken', result.token)

                // Redirect to home page
                window.location.href = 'index.html';
            })
            .catch(error => {
                console.log('Error:', error);
            });
    });
});