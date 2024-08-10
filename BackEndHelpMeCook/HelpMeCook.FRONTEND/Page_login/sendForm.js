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
         console.log("logginggg");
        const reader = response.body.getReader();
        const decoder = new TextDecoder("utf-8");
        let data = "";

        while (true) {
            const { done, value } = await reader.read();
            if (done) break;
            data += decoder.decode(value, { stream: true });
        }

        if (!response.ok) {
            let dataAlert = "";
            try {
                const resJson = JSON.parse(data);
                resJson.forEach(item => {
                    dataAlert += item['description'] + "\n";
                });
            } catch (e) {
                dataAlert = "Incorrect email/password."; // Default error message if parsing fails
            }
            
            if (dataAlert.length > 0) {
                alert(dataAlert);
            }
        } else if (response.status === 200 && response.statusText === "OK") {
            // Store the object in local storage
            localStorage.setItem('userObject', data);
            
            // Redirect to home page
            window.location.href = "../Page_home/home.html";
        }

    } catch (error) {
        console.error(error);
    }
}

