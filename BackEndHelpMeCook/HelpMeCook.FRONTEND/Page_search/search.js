import { makeIngredient } from "../ingredient.js"

document.addEventListener('DOMContentLoaded', (event) => {
    console.log('Login page loaded')

    document.body.appendChild(makeIngredient('test'))
})