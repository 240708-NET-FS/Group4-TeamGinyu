import { makeIngredientTag, makeIngredientResult } from "../Utils/ingredient.js"

let ingredientTags = []
let ingredientSelection = []

const tags_container = document.querySelector('.ingredients-tag-container')
const search_ingredient_button = document.querySelector('.search-ingredient-button')
const input_search_ingredient = document.querySelector('.search-ingredient-input')
const modal = document.querySelector('.ingredients-modal')

const modal_container_ingredients = document.querySelector('.ingredients-result-container')


const seach_type_selector = document.querySelector('.search-type')
const type_ingredient_container = document.querySelector('.type-ingredient-container')
const type_name_container = document.querySelector('.type-name-container')
const type_ingredient_submenu = document.querySelector('.type-ingredient-submenu')
const modal_button = document.querySelector('.modal-button-done')





document.addEventListener('DOMContentLoaded', (event) => {
    console.log('Login page loaded')
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


// When modal button clicked: take selected ingredients and add them to tags array
modal_button.addEventListener('click', () => {

    // close modal
    modal.classList.remove('is-active')
    
    // for each selected ingredient, add it to tags if not included already
    ingredientSelection.forEach(element => {

        // if the element is not selected, return
        if(!element.classList.contains('is-selected')) return

        console.log('passing: ' + element.textContent)

        let alreadyIncluded = false

        // check if that ingredient is already added to the list
        for(let i = 0; i < ingredientTags.length; i++) {

            console.log(element.textContent)

            if(ingredientTags[i].querySelector('span').textContent === element.textContent) {

                alreadyIncluded = true
                break
            }
        }


        // if not added, add to the ingredientTags
        if(alreadyIncluded === false)
            addIngredientTag(element.textContent)
    });
})

// adds an ingredient to the tags
function addIngredientTag(name) {
    const newTag = makeIngredientTag(name)
    const newTagButton = newTag.querySelector('a')
    ingredientTags.push(newTag)
    tags_container.appendChild(newTag)

    newTagButton.addEventListener('click', () => {
        removeIngredientTag(newTag)
    })

}

function removeIngredientTag(tagElement) {
    ingredientTags = ingredientTags.filter((item) => item !== tagElement)
    tags_container.removeChild(tagElement)
}



// when we click on search ingredient, call searchIngredient
search_ingredient_button.addEventListener('click', searchIngredient)

// fetch spoonacular API for ingredients that match the input and return best X matches
async function searchIngredient() {

    const ingredient = input_search_ingredient.value;
    let fetchString = `https://api.spoonacular.com/food/ingredients/search?query=${ingredient}&number=5&sort=calories&sortDirection=desc&apiKey=bb8c79c0e34d4dca8fd0ef169d1426a4`;
    let ingredients

    // remove all ingredient results from the modal
    while(modal_container_ingredients.firstChild) {
        modal_container_ingredients.removeChild(modal_container_ingredients.firstChild)
    }

    // open the modal
    modal.classList.add('is-active')

    // fetch the ingredients
    await fetch(fetchString, {method:"GET"})
    .then(response => {

        if(response.ok)
            return response.json()
    })
    .then(data => {
        ingredients = data.results;
    })

    ingredientSelection = []

    ingredients.forEach(i => {
        ingredientSelection.push(makeIngredientResult(i.name))
        modal_container_ingredients.appendChild(ingredientSelection[ingredientSelection.length - 1])
    });

    ingredientSelection.forEach(element => {
        element.addEventListener('click', ()=> {
            if(element.classList.contains('is-light')) {
                element.classList.remove('is-light')
                element.classList.add('is-selected')
            }
            else {
                element.classList.add('is-light')
            }
        })
    });
}