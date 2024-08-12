import { spoonacular } from "../apisettings.js";

const mainContent = document.querySelector('.main-content')

document.addEventListener('DOMContentLoaded', async (event) => {

    const url = new URL(window.location.href)
    const params = new URLSearchParams(url.search)
    

    let recipeId = params.get('recipeId')


    let title = document.createElement('h3')
    title.textContent = params.get('recipeName')
    mainContent.appendChild(title)

    let fetchString = `https://api.spoonacular.com/recipes/${recipeId}/analyzedInstructions?apiKey=${spoonacular.key}`

    let content = []

    await fetch(fetchString, {method: 'GET'})
    .then(response => {
        if(response.ok)
            return response.json()
    })
    .then(data => {
        data[0].steps.forEach(step => {
            console.log(step.step)
            content.push(step.step)
            //content += step.step + '\n'
        });
    })

    content.forEach(line => {
        let p = document.createElement('p')
        p.textContent = line
        mainContent.appendChild(p)
    });
})