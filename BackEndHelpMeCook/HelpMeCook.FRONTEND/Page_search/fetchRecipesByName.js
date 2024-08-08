// given a query, fetch a list of ingredients that best matches it
export async function fetchRecipesByName(query) {

    let recipes = []
    let fetchString = `https://api.spoonacular.com/recipes/complexSearch?query=${query.replace(/ /g, '%20')}&maxFat=25&number=2&apiKey=bb8c79c0e34d4dca8fd0ef169d1426a4`

    await fetch(fetchString, {method: "GET"})
    .then(result => {
        if(result.ok) return result.json()
    })
    .then(data => {
        recipes = data.results
    })

    return recipes
}