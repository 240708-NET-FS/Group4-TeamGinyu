document.addEventListener('DOMContentLoaded', (event) => {
    console.log('Check for token');
    var retrievedObject = localStorage.getItem('userObject');
    try {
        var resJson = JSON.parse(retrievedObject);
        if(resJson['accessToken'].length < 5 && resJson['expiresIn'].length < 5 
            && resJson['refreshToken'].length < 5 && resJson['tokenType'].length < 5){
            window.location.href = "../Page_login/login.html";
        }
    } catch (error) {
        console.log(error);
        window.location.href = "../Page_login/login.html";
    }
})