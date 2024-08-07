function sendAPIGetToken(apiUrl, apiKey){
    // apiKey is the Bearer Token.
    const requestOptions = {
        method: 'GET',
        headers: {
            'Authorization': `Bearer ${apiKey}`,
        },
    };

    fetch(apiUrl, requestOptions)
    .then(response => {
        if(!response.ok) {
        throw new Error('Network response was not ok');
        }
        return response.json();
    })
    .then(data => {
        outputElement.textContent = JSON.stringify(data, null, 2);
    })
    .catch(error => {
        console.error('Error:', error);
    });
}