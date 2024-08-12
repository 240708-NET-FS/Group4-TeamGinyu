import { api } from "../apisettings.js";

let token = JSON.parse(localStorage.getItem('userObject')).accessToken;

async function getAllRecipes() {
    const fetchString = `${api.url}/api/User/users`
    let result = "";
    try {
        let res = await fetch(fetchString, {
            method: 'GET',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Headers': 'Content-Type, Authorization',
                'Access-Control-Allow-Methods': '*',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });

        if (res.ok) {
            const reader = res.body.getReader();
            const decoder = new TextDecoder("utf-8");

            while (true) {
                const { done, value } = await reader.read();
                if (done) break;
                result += decoder.decode(value, { stream: true });
            }

        } else {
            console.error("Response was not okay.");
            return []
        }

        return JSON.parse(result);

    } catch (error) {
        console.error("Fetch error: " + error);
    }

}

function populateTable(userRecipes) {
    const tableBody = document.getElementById("recipeTableBody");
    tableBody.innerHTML = '';

    userRecipes.forEach(user => {

        user.recipes.forEach(recipe => {
            const row = document.createElement('tr');
            row.id = `recipe-${recipe.recipeID}`;

            row.innerHTML = `
            <td>${user.lastName} ${user.firstName}</td>
            <td>${recipe.recipeName}</td>
            <td>${recipe.recipeNumber}</td>
            <td>
            <button id="button-${recipe.recipeID}" class="button is-normal is-responsive is-success is-rounded" onclick="addRecipe('${recipe.recipeName}', ${recipe.recipeNumber}, '${row.id}')">Add</button>
            </td>
        `;

            tableBody.appendChild(row);
        });
    });
}

document.addEventListener('DOMContentLoaded', async () => {
    const recipes = await getAllRecipes();
    
    populateTable(recipes);
});

async function addRecipe(name, recipeNumber, elementId) {
    const fetchString = `${api.url}/api/Recipe`
    const body = {
        recipeName: name,
        recipeNumber: recipeNumber
    }
    // run fetch
    fetch(fetchString, {
        method: 'POST',
        headers: {
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Headers': 'Content-Type, Authorization',
            'Access-Control-Allow-Methods': '*',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(body)
    })
        .then(response => {
            console.log('response: ', response)
            console.log(JSON.stringify(body));
            if (response.ok) {
                const button = document.getElementById(elementId);
                button.disabled = true;

                // Change the styles to make it look gray
                button.style.backgroundColor = 'gray';
                button.style.color = 'white';
                button.style.cursor = 'not-allowed'; // Change cursor to indicate it's disabled
                button.style.opacity = '0.6'; // Optionally, reduce the opacity

            } else {
                alert("You already have this recipe in your storage.");
            }
        })
        .catch(err => {
            // else, un-disable the button
            console.error("Fetch error: ", err);
        });
}

window.addRecipe = addRecipe;