import { makeIngredientTag, makeIngredientResult } from "../Utils/ingredient.js"

let ingredients = []

const ingredients_tag_container = document.querySelector('.ingredients-tag-container')
const search_ingredient_button = document.querySelector('.search-ingredient-button')
const input_search_ingredient = document.querySelector('.search-ingredient-input')
const ingredients_modal = document.querySelector('.ingredients-modal')

const ingredients_result_container = document.querySelector('.ingredients-result-container')


const seach_type_selector = document.querySelector('.search-type')
const type_ingredient_container = document.querySelector('.type-ingredient-container')
const type_name_container = document.querySelector('.type-name-container')
const type_ingredient_submenu = document.querySelector('.type-ingredient-submenu')




document.addEventListener('DOMContentLoaded', (event) => {
    console.log('Login page loaded')

    ingredients.push('some ing')
    ingredients.push('some ing')
    ingredients.push('some ing')
    ingredients.push('some ing')
    ingredients.push('some ing')
    ingredients.push('some ing')
    ingredients.push('some ing')
    ingredients.push('some ing')
    ingredients.push('some ing')


    ingredients.forEach(i => {
        //ingredients_tag_container.appendChild(makeIngredientTag(i))
    });

    //for(let i = 0; i < 40; i++)
    //    container_ingredients.appendChild(makeIngredientTag('Some ingredient'))
})




// handles the update in UI when selecting a search type
seach_type_selector.addEventListener('change', (event) => {

    let searchType = event.target.value

    console.log(searchType)

    if(searchType === 'By Ingredient') {
        type_name_container.classList.add('is-hidden')
        type_ingredient_container.classList.remove('is-hidden')
        type_ingredient_submenu.classList.remove('is-hidden')
    }
    else {
        type_ingredient_container.classList.add('is-hidden')
        type_ingredient_submenu.classList.add('is-hidden')
        type_name_container.classList.remove('is-hidden')
    }
})





// when we click on search ingredient, call searchIngredient
search_ingredient_button.addEventListener('click', searchIngredient)


// fetch spoonacular API for ingredients that match the input and return best X matches
async function searchIngredient() {

    const ingredient = input_search_ingredient.value;
    let fetchString = `https://api.spoonacular.com/food/ingredients/search?query=${ingredient}&number=5&sort=calories&sortDirection=desc&apiKey=bb8c79c0e34d4dca8fd0ef169d1426a4`;
    let ingredients

    // remove all ingredient results from the modal
    while(ingredients_result_container.firstChild) {
        ingredients_result_container.removeChild(ingredients_result_container.firstChild)
    }

    // open the modal
    ingredients_modal.classList.add('is-active')

    // fetch the ingredients
    await fetch(fetchString, {method:"GET"})
    .then(response => {

        if(response.ok)
            return response.json()
    })
    .then(data => {
        ingredients = data.results;
    })


    console.log(ingredients)

    ingredients.forEach(i => {
        ingredients_result_container.appendChild(makeIngredientResult(i.name))
    });
}