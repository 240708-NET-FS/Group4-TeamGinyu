import { api, spoonacular } from "../apisettings.js";
import { sendForm } from "./sendForm.js";

const btn_login = document.querySelector('#login-button');
const btn_register = document.querySelector('#register-button');
const form_element = document.querySelector('#submition-form')

document.addEventListener('DOMContentLoaded', (event) => {
    console.log('Login page loaded')
})

btn_login.addEventListener('click', (event) => {
    event.preventDefault()

    var email_form = document.getElementById("email_form").value;
    var password_form = document.getElementById("password_form").value;

    const clicked_element = event.target;

    const register_data = {
        "email": email_form,
        "password": password_form
    };

    sendForm(api.url +'/login', 'POST', register_data);
})