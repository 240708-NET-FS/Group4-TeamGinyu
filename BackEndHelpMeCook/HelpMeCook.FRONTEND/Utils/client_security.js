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

    //
    console.log('Fn');
    document.getElementById('logout').addEventListener('click', function(event) {
        event.preventDefault(); // Prevent default link behavior
        logOut(); // Call your logout function
    });
    function logOut(){
        // Delete userObject Local storage
        localStorage.removeItem('userObject');
        console.log('LocalStorage userObject destroyed...');
        window.location.href='../Page_login/login.html';
        // Clear all items
        // localStorage.clear();
    }
});