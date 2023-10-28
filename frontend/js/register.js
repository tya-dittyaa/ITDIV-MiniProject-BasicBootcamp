document.addEventListener("DOMContentLoaded", function () {
    const nameInput = document.querySelector('input[name="name"]');
    const emailInput = document.querySelector('input[name="email"]');
    const passwordInput = document.querySelector('input[name="password"]');
    const confirmPasswordInput = document.querySelector('input[name="confirmPassword"]');
    const termsCheckbox = document.querySelector('input[name="terms"]');
    
    const nameRegex = /^[A-Za-z]+$/;
    const emailRegex = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    const passwordRegex = /^(?=.*[A-Z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).{8,}$/;

    function validateName(name){
        return name.match(nameRegex);
    }

    function validateEmail(email) {
        return email.match(emailRegex);
    }

    function validatePassword(password) {
        return password.match(passwordRegex);
    }

    function validateForm() {
        if(!validateName(nameInput.value)){
            alert("Invalid name!");
            return false;
        }

        if (!validateEmail(emailInput.value)) {
            alert("Invalid email address!");
            return false;
        }
        
        if (!validatePassword(passwordInput.value)) {
            alert("Password must contain at least 8 characters, one capital letter, one symbol, and one number.");
            return false;
        }

        if (passwordInput.value !== confirmPasswordInput.value) {
            alert("Passwords do not match!");
            return false;
        }

        if (!termsCheckbox.checked) {
            alert("Please agree to the terms and conditions.");
            return false;
        }

        return true;
    }

    const registrationForm = document.querySelector('form');

    registrationForm.addEventListener("submit", function (e) {
        if (!validateForm()) {
            e.preventDefault();
        }else {
            window.location.href = 'Main.html';
        }
    });
});
