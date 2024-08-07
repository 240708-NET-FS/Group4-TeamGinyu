// import { api, spoonacular } from "./appsettings.js";
const api = {
    url: "http://localhost:5224",
    token: ""
  };
// console.log(api.url);

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

    // if(clicked_element === btn_login)
    //     sendForm('login_endpoint', 'GET?')
    // else if (clicked_element === btn_register)
    //     sendForm('register_endpoint', 'POST')
})

function sendForm(url, method, register_data) {
    fetch(url, {
        method: method,
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Headers': 'Content-Type, Authorization',
            'Access-Control-Allow-Methods': '*',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(register_data),
    })
    .then(response => {
        if (!response.ok) {
            console.log(response);
            throw new Error('Network response was not ok');
        }
        return response;
    })
    .then(async res => {
        console.log(res);
        if(res['status']==200 && res['statusText']=="OK"){
            console.log(res['body']);
            const reader = res.body.getReader();
            const decoder = new TextDecoder("utf-8");
            let data = "";
            while (true) {
                const { done, value } = await reader.read();
    
                if (done) {
                    break;
                }
    
                data += decoder.decode(value, { stream: true });
            }

            // Put the object into storage
            localStorage.setItem('userObject', data);

            // Retrieve the object from storage
            // var retrievedObject = localStorage.getItem('testObject');
            // console.log('retrievedObject: ', JSON.parse(retrievedObject));
    
            // At this point, data contains the complete response
            console.log(JSON.parse(data));

            // Redirect
            // window.location.href = "../Page_login/login.html";
            alert(JSON.parse(data));
        }
        // console.log(JSON.stringify(data, null, 2));
    })
    .catch(error => {
        console.error('Error:', error);
    });
}

async function fetchData(url) {
    try {
        const response = await fetch(url);
 
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
 
        const reader = response.body.getReader();
        const decoder = new TextDecoder("utf-8");
        let data = "";
        while (true) {
            const { done, value } = await reader.read();
 
            if (done) {
                break;
            }
 
            data += decoder.decode(value, { stream: true });
        }
 
        // At this point, data contains the complete response
        console.log(data);
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}