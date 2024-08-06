import { makeIngredientTag, makeIngredientSelection, makeRecipeBox } from "../Utils/ingredient.js"

// contains ingredient tag elements
let ingredientTags = []
// contains ingredient selection elements
let ingredientSelection = []

const tags_container = document.querySelector('.ingredients-tag-container')
const search_ingredient_button = document.querySelector('.search-ingredient-button')
const search_ingredient_input = document.querySelector('.search-ingredient-input')
const search_recipes_ingredients_button = document.querySelector('.search-recipes-ingredients-button')
const search_name_button = document.querySelector('.search-name-button')
const search_name_input = document.querySelector('.search-name-input')
const modal = document.querySelector('.ingredients-modal')
const modal_container_ingredients = document.querySelector('.ingredients-result-container')
const seach_type_selector = document.querySelector('.search-type')
const type_ingredient_container = document.querySelector('.type-ingredient-container')
const type_name_container = document.querySelector('.type-name-container')
const type_ingredient_submenu = document.querySelector('.type-ingredient-submenu')
const modal_button = document.querySelector('.modal-button-done')


const recipe_container = document.querySelector('.recipe-container')

// test
const test = document.querySelector('.recipe-tags')


document.addEventListener('DOMContentLoaded', (event) => {
    console.log('Login page loaded')

    //recipe_container.appendChild(makeRecipeBox('some recipe name', ['rep1', 'rep3', 'rep454']))

})



// change TYPE event.
// updates the UI to show the by ingredient or by name search options
seach_type_selector.addEventListener('change', (event) => {

    let searchType = event.target.value

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


// modal BUTTON click event
// closes the modal, updates ingredients
modal_button.addEventListener('click', () => {

    // close modal
    closeModal()


    
    // for each selected ingredient, add it to tags if not included already
    ingredientSelection.forEach(element => {
        let alreadyIncluded = false

        // if the element is not selected, return
        if(!element.classList.contains('is-selected')) return

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

// adds an ingredient to both the tags array and the HTML document
function addIngredientTag(name) {
    const newTag = makeIngredientTag(name)
    const newTagButton = newTag.querySelector('a')
    ingredientTags.push(newTag)
    tags_container.appendChild(newTag)

    newTagButton.addEventListener('click', () => {
        removeIngredientTag(newTag)
    })

}

// removes an ingredient from both the tags array and the HTML document
function removeIngredientTag(tagElement) {
    ingredientTags = ingredientTags.filter((item) => item !== tagElement)
    tags_container.removeChild(tagElement)
}

function openModal() {
    modal.classList.add('is-active')
    modal.querySelector('img').classList.remove('is-hidden')
}

function closeModal() {
    modal.classList.remove('is-active')

    // clear modal contents
    while(modal_container_ingredients.firstChild) {
        modal_container_ingredients.removeChild(modal_container_ingredients.firstChild)
    }
}


// search ingredient BUTTON event
// triggers an ingredient search to the spoonacular API
search_ingredient_button.addEventListener('click', searchIngredient)

// fetch spoonacular API for ingredients that match the input and return best X matches
async function searchIngredient() {

    const ingredient = search_ingredient_input.value;
    let fetchString = `https://api.spoonacular.com/food/ingredients/search?query=${ingredient}&number=5&sort=calories&sortDirection=desc&apiKey=bb8c79c0e34d4dca8fd0ef169d1426a4`;
    let ingredientsFoundData

    // open the modal
    openModal()

    // fetch the ingredients
    await fetch(fetchString, {method:"GET"})
    .then(response => {

        if(response.ok)
            return response.json()
    })
    .then(data => {
        ingredientsFoundData = data.results;
    })

    // clear the ingredientSelection array
    ingredientSelection = []

    // hide the modal's loading icon
    modal.querySelector('img').classList.add('is-hidden')

    // for each ingredient data, create an ingredient selection element and add it to the
    // ingredientSelection array
    ingredientsFoundData.forEach(i => {
        addIngredientSelection(i.name)
    });
}

// adds an ingredient selection to the ingredientSelection array and the document HTML
function addIngredientSelection(name) {
    // create ingredient selecion object
    const newIngredientSelection = makeIngredientSelection(name)

    // add it to selection array
    ingredientSelection.push(newIngredientSelection)
    // append it to document HTML
    modal_container_ingredients.appendChild(newIngredientSelection)

    // set up event listener for its close button
    newIngredientSelection.addEventListener('click', ()=> {
        if(newIngredientSelection.classList.contains('is-light')) {
            newIngredientSelection.classList.remove('is-light')
            newIngredientSelection.classList.add('is-selected')
        }
        else {
            newIngredientSelection.classList.add('is-light')
            newIngredientSelection.classList.remove('is-selected')
        }
    })
}

// search by ingredient EVENT
// performs a recipe search that includes all the ingredients we have as tags
search_recipes_ingredients_button.addEventListener('click', async () => {

    let ingredients = []
    let recipes = []

    // add each ingredient to the ingredients array
    // (spoonacular API doesnt allow white spaces on ingredient name searches, they
    // need to be replaced with '%20')
    ingredientTags.forEach(i => {
        let originalString = i.querySelector('span').textContent
        let parsedString = originalString.replace(/ /g, '%20')
        ingredients.push(parsedString)
    });

    let fetchString = `https://api.spoonacular.com/recipes/findByIngredients?ingredients=${ingredients.join(',+')}&apiKey=bb8c79c0e34d4dca8fd0ef169d1426a4`

    await fetch(fetchString, {method: "GET"})
    .then(result => {
        if(result.ok) return result.json()
    })
    .then(data => {
        recipes = data;
        console.log(data)
    })

    UpdateRecipeResults(recipes)
})

function UpdateRecipeResults(recipes) {
    while(recipe_container.firstChild) {
        recipe_container.removeChild(recipe_container.firstChild)
    }

    recipes.forEach(r => {
        let ingredientNames = []

        r.usedIngredients.forEach(i => {
            ingredientNames.push(i.name)
        });
        r.missedIngredients.forEach(i => {
            ingredientNames.push(i.name)
        });
        recipe_container.appendChild(makeRecipeBox(r.title, ingredientNames))
    });
}