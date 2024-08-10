import { api, spoonacular } from "../apisettings.js";

let token = JSON.parse(localStorage.getItem('userObject')).accessToken;

// Menu Item Nutrition by ID Image
async function fetchImgMenuItemNutrition(id) {
    
    let fetchString = `${spoonacular.url}/food/menuItems/${id}/nutritionWidget.png?apiKey=${spoonacular.key}`;
    
    fetch(fetchString)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.blob();  // Convert the response to a Blob
    })
    .then(blob => {
        const reader = new FileReader();
        reader.onloadend = function() {
            const base64data = reader.result;
            // console.log('Base64 Image Data:', base64data);

            // You can now use base64data to display the image or send it elsewhere
            const img = document.getElementById('dashboard-img-1');
            img.src = base64data;
        };
        reader.readAsDataURL(blob);  // Convert the Blob to a Base64-encoded string
    })
    .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
    });

}

async function fetchMenuItem(query){
    if(query != ""){
        query = "query=" + query +"&";
    }else{
        query = "query=chicke&";
    }
    let fetchString = `${spoonacular.url}/food/menuItems/search?${query}number=2&apiKey=${spoonacular.key}`;

    try {
        const response = await fetch(fetchString, {method:"GET"});
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return await response.json();  // Convert to JSON
    } catch (error) {
        console.error('There was a problem with the fetch operation:', error);
    }
}

// Get Recipe Card 
async function fetchGetRecipeCard() {
    // https://api.spoonacular.com/recipes/complexSearch
    let fetchString = `${spoonacular.url}/recipes/complexSearch?apiKey=${spoonacular.key}`;
    
    fetch(fetchString)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();  // Convert the response to a json
    })
    .then(data => {
        const img = document.getElementById('dashboard-img-2');
        console.log(data['results'][0]);
        img.src = data['results'][0]['image'];
    })
    .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
    });
}

// https://api.spoonacular.com/recipes/69095/tasteWidget.png
// Recipe Taste by ID Image
async function fetchRecipeTaste(id) {
    if(id == ""){
        id = "69095";
    }
    let fetchString = `${spoonacular.url}/recipes/${id}/tasteWidget.png?apiKey=${spoonacular.key}`;
    
    fetch(fetchString)
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        console.log(response);
        return response;  // Convert the response to a blob
    })
    .then(response => {
        const reader = response.body.getReader(); // Get a reader from the stream
        const decoder = new TextDecoder('utf-8'); // Decode the stream into text

        let result = '';

        // Function to process each chunk
        function readChunk() {
            reader.read().then(({ done, value }) => {
                if (done) {
                    console.log('Stream complete');
                    // console.log('Full result:', result); // Full content of the stream
                    return result.blob();
                }

                // Decode and append the chunk to the result
                result += decoder.decode(value, { stream: true });

                // Process the next chunk
                readChunk();
            });
        }

        // Start reading the first chunk
        readChunk();
    })
    .then(blob => {
        const reader = new FileReader();
        reader.onloadend = function() {
            const base64data = reader.result;  // Get the Base64 string
            console.log('Base64:', base64data);
        };
        reader.readAsDataURL(blob);  // Convert the Blob to a Base64 string
    })
    .catch(error => {
        console.error('There was a problem with the fetch operation:', error);
    });
}

// Get all users
async function getAllUsers() {
    let fetchString = `${api.url}/api/User/users`;
    await fetch(fetchString)  // Replace with the actual API endpoint
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();  // Parse the response as JSON
        })
        .then(data => {
            console.log('Fetched data:', data);  // Handle the JSON data
            // data.length;
            document.getElementById('card-users-count').innerHTML = data.length;
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
        });
}


// Get all recipes.
async function getAllRecipes() {
    const fetchString = `${api.url}/api/Recipe/recipes`
    await fetch(fetchString, {
            method: 'GET',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Headers': 'Content-Type, Authorization',
                'Access-Control-Allow-Methods': '*',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        })  // Replace with the actual API endpoint
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();  // Parse the response as JSON
        })
        .then(data => {
            console.log('Fetched data AllRecipes:', data);  // Handle the JSON data
            document.getElementById('card-recipes-count').innerHTML = data.length;
            return data;
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
        });
        //
}

document.addEventListener('DOMContentLoaded', async (event) => {
    console.log('Loading data from spoonacular API');
    var menuItem = await fetchMenuItem("");
    console.log(menuItem['menuItems'][1]['id']);
    var imgMenuItem = fetchImgMenuItemNutrition(menuItem['menuItems'][1]['id']);
    console.log(imgMenuItem);
    console.log('');
    fetchGetRecipeCard();
    console.log('');
    // fetchRecipeTaste('');
    console.log();
    // Get all users
    getAllUsers();
    //
    getAllRecipes();
    

});