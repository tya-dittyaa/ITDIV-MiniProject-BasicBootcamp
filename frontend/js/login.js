const loginForm = document.getElementById('login-form');

loginForm.addEventListener('submit', async (e) => {
    e.preventDefault();

    const email = loginForm.querySelector('input[name="email"]').value;
    const password = loginForm.querySelector('input[name="password"]').value;

    const data = {
        email: email,
        password: password
    };

    const response = await fetch('/api/User', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });

    if (response.status === 200) {
        // Successful login
        const responseJson = await response.json();
        alert('Login successful!');
        // You can redirect to the dashboard.html or perform other actions here.
    } else if (response.status === 404) {
        // Handle errors, e.g., email not found or wrong password
        const errorResponse = await response.json();
        alert(errorResponse.errorMessage);
    } else {
        // Handle other errors
        alert('An error occurred');
    }
});