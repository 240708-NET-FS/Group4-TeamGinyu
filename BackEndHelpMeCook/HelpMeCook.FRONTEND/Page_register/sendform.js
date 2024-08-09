export async function sendForm(url, method, register_data) {
    try {
        const response = await fetch(url, {
            method: method,
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Headers': 'Content-Type, Authorization',
                'Access-Control-Allow-Methods': '*',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(register_data),
        });
        
        const reader = response.body.getReader();
        const decoder = new TextDecoder('utf-8');
        let data = '';

        while (true) {
            const { done, value } = await reader.read();

            if (done) {
                break;
            }
            data += decoder.decode(value, { stream: true });
        }

        if (!response.ok) {
            let dataAlert = '';
            try {
                    const resJson = JSON.parse(data);
                    for (const i in resJson) {
                        dataAlert += resJson[i]['description'] + '\n';
                    }
                } 
            catch (error) {
                dataAlert = 'Invalid email/password.';
            }
            console.log(response);
            console.log(response['status'] + ' == ' + response['statusText']);
            if (dataAlert.length > 0) {
                alert(dataAlert);
            }
        } else if (response['status'] == 200 && response['statusText'] == 'OK') {
            console.log(response);
            if (response['status'] == 200 && response['statusText'] == 'OK') {
                window.location.href = '../Page_login/login.html';
            }
            return response;
        }

        }
        catch (error) {
        console.error(error);
    } 
}