import { api, spoonacular } from "../apisettings.js";
import { sendForm } from "./sendform.js";
// const api = {
//     url: "http://localhost:5224",
//     token: ""
//   };

const btn_login = document.querySelector('#login-button');
const btn_register = document.querySelector('#register-button');
const form_element = document.querySelector('#submition-form')

document.addEventListener('DOMContentLoaded', (event) => {
    
    // Retrieve the object from storage
    // var retrievedObject = localStorage.getItem('userObject');
    // console.log('retrievedObject: ', JSON.parse(retrievedObject));

    console.log('Login page loaded')
})

btn_register.addEventListener('click', (event) => {
    event.preventDefault()

    const clicked_element = event.target;
    console.log(event);

    var email_form = document.getElementById("email_form").value;
    var password_form = document.getElementById("password_form").value;
    var firstname_form = document.getElementById("firstname_form").value;
    var lastname_form = document.getElementById("lastname_form").value;

    // const register_data1 = {
    //     email: email_form,
    //     password: password_form
    // };

    const register_data2 = {
        username : email_form,
        password : password_form,
        firstName : firstname_form,
        lastName : lastname_form
    };

    sendForm(api.url +'/api/User/register', 'POST', register_data2);
})
// const register_data2 = {
//     "username": email_form,
//     "password": password_form,
//     "firstName": firstname_form,
//     "lastName": lastname_form
// };
  
// const requestOptions = {
//     method: 'POST',
//     headers: {
//         'Access-Control-Allow-Origin': '*',
//         'Access-Control-Allow-Headers': 'Content-Type, Authorization',
//         'Access-Control-Allow-Methods': '*',
//         'Content-Type': 'application/json',
//     },
//     body: JSON.stringify(register_data1),
// };
//     const requestOptions2 = {
//         method: 'POST',
//         headers: {
//             'Access-Control-Allow-Origin': '*',
//             'Access-Control-Allow-Headers': 'Content-Type, Authorization',
//             'Access-Control-Allow-Methods': '*',
//             'Content-Type': 'application/json',
//         },
//         body: JSON.stringify(register_data2),
//     };

//     // console.log(requestOptions2);

//     // Another request
//     fetch(api.url +'/api/User/register', requestOptions2)
//     .then(async res => {
//         if (!res.ok) {
//             console.log(res);
//             console.log(res['status'] +" == " +res['statusText']);
//             const reader = res.body.getReader();
//             const decoder = new TextDecoder("utf-8");
//             let data = "";
//             while (true) {
//                 const { done, value } = await reader.read();
    
//                 if (done) {
//                     break;
//                 }
    
//                 data += decoder.decode(value, { stream: true });
//             }
            
//             resJson = JSON.parse(data);
//             //
//             var dataAlert = "";
//             for (i in resJson) {
//                 //console.log("***" +resJson[i]['code'] +" = " +resJson[i]['description']);
//                 dataAlert += resJson[i]['description'] +"\n";
//             }
//             if(dataAlert.length>0)
//                 alert(dataAlert);

//             // throw new Error('Network response was not ok');
//         }
//         return res;
//     })
//     .then(data => {
//         console.log(data);
//         if(data['status']==200 && data['statusText']=="OK"){
//             // Alert successfully registered
//             //
//             //

//             // After clicking on okay btn redirect
//             window.location.href = "../Page_login/login.html";
//         }
//         // console.log(JSON.stringify(data, null, 2));
//     })
//     .catch(error => {
//         console.error('Error:', error);
//     });

//     // fetch(api.url +'/register', requestOptions)
//     // .then(response => {
//     //     if (!response.ok) {
//     //         console.log(response);
//     //         throw new Error('Network response was not ok');
//     //     }
//     //     return response;
//     // })
//     // .then(data => {
//     //     console.log(data);
//     //     if(data['status']==200 && data['statusText']=="OK"){
            
//     //     }
//     //     console.log(JSON.stringify(data, null, 2));
//     // })
//     // .catch(error => {
//     //     console.error('Error:', error);
//     // });

//     // if(clicked_element === btn_login)
//     //     sendForm('login_endpoint', 'GET?')
//     // else if (clicked_element === btn_register)
//     //     sendForm('/register', 'POST')
// })

// function sendForm(url, method) {
//     fetch(api.url + url, {
//         method: method
//     })
//     .then(response => {
//         //if(response.ok) return response.json()
//     })
// )}