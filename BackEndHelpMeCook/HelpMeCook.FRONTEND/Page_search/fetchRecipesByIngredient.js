import { spoonacular } from "../apisettings.js";

// given a list of ingredients, fetch for recipes that best match the ingredient
export async function fetchRecipesByIngredients(ingredients) {

    let recipes
    let fetchString = `https://api.spoonacular.com/recipes/findByIngredients?ingredients=${ingredients.join(',+')}&apiKey=${spoonacular.key}`

    await fetch(fetchString, {method: "GET"})
    .then(result => {
        if(result.ok) return result.json()
    })
    .then(data => {
        recipes = data;
    })

    return recipes
}