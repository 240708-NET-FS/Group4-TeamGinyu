import { api } from "../apisettings.js";


async function getRecipesByUser() {
    const fetchString = `${api.url}/api/Recipe/recipe/user`
    let result = "";
    let token = JSON.parse(localStorage.getItem('userObject')).accessToken;
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

        return result;

    } catch (error) {
        console.error("Fetch error: " + error);
    }

}

async function deleteRecipe(id) {
    const fetchString = `${api.url}/api/Recipe/recipe/${id}`
    let result = "";
    let token = JSON.parse(localStorage.getItem('userObject')).accessToken;

    try {
        let res = await fetch(fetchString, {
            method: 'DELETE',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Headers': 'Content-Type, Authorization',
                'Access-Control-Allow-Methods': '*',
                'Content-Type': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }
        });

        if (res.ok) {
            document.getElementById(`recipe-${id}`).remove();
            
        } else 
        {
            console.error("Error when trying to delete id ", id);
        }
    } catch (error) {
        console.error("Fetch error: ", error);
    }
}

function populateTable(recipes) {
    const tableBody = document.getElementById("recipeTableBody");
    tableBody.innerHTML = '';

    recipes.forEach(recipe => {
        const row = document.createElement('tr');
        row.id = `recipe-${recipe.recipeID}`;

        row.innerHTML = `
            <td>${recipe.recipeNumber}</td>
            <td>${recipe.recipeName}</td>
            <td>${recipe.createdDate}</td>
            <td>
            <!-- <button>Edit</button>  -->
                <button onclick="deleteRecipe(${recipe.recipeID})">Delete</button>
            </td>
        `;

        tableBody.appendChild(row);
    });
}

document.addEventListener('DOMContentLoaded', async () => {
    const recipes = await getRecipesByUser();
    let recipesArray = JSON.parse(recipes);
    console.log(recipesArray);
    populateTable(recipesArray);
});

window.deleteRecipe = deleteRecipe;