import { spoonacular } from "../apisettings.js";

// given a name, returns a list of ingredients that best match the name
export async function fetchIngredientByName(name) {
    
    let fetchString = `https://api.spoonacular.com/food/ingredients/search?query=${name}&number=5&sort=calories&sortDirection=desc&apiKey=${spoonacular.key}`
    let ingredientsFoundData

    await fetch(fetchString, {method:"GET"})
    .then(response => {

        if(response.ok)
            return response.json()
    })
    .then(data => {
        ingredientsFoundData = data.results
    })

    return ingredientsFoundData
}