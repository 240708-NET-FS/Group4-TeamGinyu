// given a list of ingredients, fetch for recipes that best match the ingredient
export async function fetchRecipesByIngredients(ingredients) {

    let recipes
    let fetchString = `https://api.spoonacular.com/recipes/findByIngredients?ingredients=${ingredients.join(',+')}&apiKey=bb8c79c0e34d4dca8fd0ef169d1426a4`

    await fetch(fetchString, {method: "GET"})
    .then(result => {
        if(result.ok) return result.json()
    })
    .then(data => {
        recipes = data;
    })

    return recipes
}