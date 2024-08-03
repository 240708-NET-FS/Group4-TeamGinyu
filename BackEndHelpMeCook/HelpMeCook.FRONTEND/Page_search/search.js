import { makeIngredient } from "../Utils/ingredient.js"

const container_ingredients = document.querySelector('.ingredients-container')

document.addEventListener('DOMContentLoaded', (event) => {
    console.log('Login page loaded')

    container_ingredients.appendChild(makeIngredient('Some ingredient'))
    container_ingredients.appendChild(makeIngredient('Some ingredient'))
    container_ingredients.appendChild(makeIngredient('Some ingredient'))
    container_ingredients.appendChild(makeIngredient('Some ingredient'))
    container_ingredients.appendChild(makeIngredient('Some ingredient'))
    container_ingredients.appendChild(makeIngredient('Some ingredient'))
    container_ingredients.appendChild(makeIngredient('Some ingredient'))
    container_ingredients.appendChild(makeIngredient('Some ingredient'))
    container_ingredients.appendChild(makeIngredient('Some ingredient'))
    container_ingredients.appendChild(makeIngredient('Some ingredient'))



})