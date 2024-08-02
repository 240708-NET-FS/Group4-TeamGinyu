const btn_login = document.querySelector('#login-button');
const btn_register = document.querySelector('#register-button');
const form_element = document.querySelector('#submition-form')

document.addEventListener('DOMContentLoaded', (event) => {
    console.log('Login page loaded')
})

form_element.addEventListener('click', (event) => {
    event.preventDefault()

    const clicked_element = event.target

    if(clicked_element === btn_login)
        sendForm('login_endpoint', 'GET?')
    else if (clicked_element === btn_register)
        sendForm('register_endpoint', 'POST')
})

function sendForm(url, method) {
    fetch(url, {
        method:method
    })
    .then(response => {
        //if(response.ok) return response.json()
    })
}