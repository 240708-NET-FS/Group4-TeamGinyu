
export function sendForm(url, method, register_data) {
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
    .then(async res => {
        if (!res.ok) {
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
            
            if(data.length>0 && Array.isArray(data)){
                resJson = JSON.parse(data);
                var dataAlert = "";
                for (i in resJson) {
                    dataAlert += resJson[i]['description'] +"\n";
                }
                if(dataAlert.length>0)
                    alert(dataAlert);
            }else{
                alert("Incorrect email/password."); // data['title']
            }

            // throw new Error('Network response was not ok');
        }
        return res;
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
            window.location.href = "../Page_home/home.html";
            //alert(JSON.parse(data));
        }
        // console.log(JSON.stringify(data, null, 2));
    })
    .catch(error => {
        console.log('Error:');
        console.log(error);
    });
}