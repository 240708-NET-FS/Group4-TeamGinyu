// given a name, returns a list of ingredients that best match the name
export async function fetchIngredientByName(name) {
    
    let fetchString = `https://api.spoonacular.com/food/ingredients/search?query=${name}&number=5&sort=calories&sortDirection=desc&apiKey=bb8c79c0e34d4dca8fd0ef169d1426a4`
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