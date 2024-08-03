import { makeIngredient } from "../Utils/ingredient.js"

const container_ingredients = document.querySelector('.ingredients-container')
const btn_search_ingredient = document.querySelector('.search-ingredient-button')
const input_search_ingredient = document.querySelector('.search-ingredient-input')



document.addEventListener('DOMContentLoaded', (event) => {
    console.log('Login page loaded')

    for(let i = 0; i < 40; i++)
        container_ingredients.appendChild(makeIngredient('Some ingredient'))
})


btn_search_ingredient.addEventListener('click', searchIngredient)


// fetches ingredients from the spoonacular api
function searchIngredient() {
    // get whats on the input
    const input = input_search_ingredient.value;
    const words = input.split(' ')
    let fetchString = 'https://api.spoonacular.com/food/ingredients/search?query=';

    words.forEach(w => {
        // if the word contains less than two characters, return (spoonacular API min is 3 chars per ingredient name)
        if(w.length < 2) return;

        
    });

    console.log(`searching for: ` + words.length)
}