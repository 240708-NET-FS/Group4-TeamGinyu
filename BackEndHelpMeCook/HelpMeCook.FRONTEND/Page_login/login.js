const btn_login = document.querySelector('#login-button');
const btn_register = document.querySelector('#register-button');
const form_element = document.querySelector('#submition-form')

document.addEventListener('DOMContentLoaded', (event) => {
    console.log('Login page loaded')
})

btn_register.addEventListener('click', (event) => {
    event.preventDefault()

    const email = form_element.querySelector('.input-email').value
    const password = form_element.querySelector('.input-password').value

    const data = {
        email: email,
        password: password
    }

    fetch('http://localhost:5224/register', {
        method: 'POST',
        headers: {'Content-Type':'application/json'},
        body: JSON.stringify(data)
    })
    .then(response => {
        return response.json()
    })
    .then(data => {
        console.log(data)
    })
})

btn_login.addEventListener('click', (event) => {
    event.preventDefault()

    const email = form_element.querySelector('.input-email').value
    const password = form_element.querySelector('.input-password').value

    const data = {
        email: email,
        password: password
    }

    fetch('http://localhost:5224/login', {
        method: 'POST',
        headers: {'Content-Type':'application/json'},
        body: JSON.stringify(data)
    })
    .then(response => {
        return response.json()
    })
    .then(data => {
        console.log(data)
    })
})