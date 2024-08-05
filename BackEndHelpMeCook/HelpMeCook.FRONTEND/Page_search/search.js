import { makeIngredient } from "../Utils/ingredient.js"

const container_ingredients = document.querySelector('.ingredients-container')
const btn_search_ingredient = document.querySelector('.search-ingredient-button')
const input_search_ingredient = document.querySelector('.search-ingredient-input')

const btn_search_by_ingredient = document.querySelector('.search-by-ingredient-button')
const btn_search_by_name = document.querySelector('.search-by-name-button')
const content_search_by_ingredient = document.querySelector('.search-by-ingredient-content')
const content_search_by_name = document.querySelector('.search-by-name-content')



document.addEventListener('DOMContentLoaded', (event) => {
    console.log('Login page loaded')

    for(let i = 0; i < 40; i++)
        container_ingredients.appendChild(makeIngredient('Some ingredient'))
})


btn_search_by_ingredient.addEventListener('click', () => {
    console.log('BY INGREDIENT')
    btn_search_by_ingredient.classList.add('is-active')
    btn_search_by_name.classList.remove('is-active')

    content_search_by_name.classList.add('is-hidden')
    content_search_by_ingredient.classList.remove('is-hidden')
})

btn_search_by_name.addEventListener('click', () => {
    console.log('BY NAME')

    btn_search_by_ingredient.classList.remove('is-active')
    btn_search_by_name.classList.add('is-active')

    content_search_by_ingredient.classList.add('is-hidden')
    content_search_by_name.classList.remove('is-hidden')
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