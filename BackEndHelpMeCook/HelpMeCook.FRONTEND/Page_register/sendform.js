export async function sendForm(url, method) {
    try {
        const requestOptions2 = {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Headers': 'Content-Type, Authorization',
                'Access-Control-Allow-Methods': '*',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(register_data2),
        };

        const res = await fetch(api.url + '/api/User/register', requestOptions2);
        if (!res.ok) {
            console.log(res);
            console.log(res['status'] + ' == ' + res['statusText']);
            const reader = res.body.getReader();
            const decoder = new TextDecoder('utf-8');
            let data = '';
            while (true) {
                const { done, value } = await reader.read();

                if (done) {
                    break;
                }

                data += decoder.decode(value, { stream: true });
            }

            const resJson = JSON.parse(data);
            let dataAlert = '';
            for (const i in resJson) {
                dataAlert += resJson[i]['description'] + '\n';
            }
            if (dataAlert.length > 0) {
                alert(dataAlert);
            }
        } else {
            console.log(res);
            if (res['status'] == 200 && res['statusText'] == 'OK') {
                window.location.href = '../Page_login/login.html';
            }
        }
    } catch (error) {
        console.error(error);
    }
}