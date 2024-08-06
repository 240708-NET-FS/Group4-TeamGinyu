function makeIngredientSelection(name) {
    const button = document.createElement('button')
    button.classList.add('button', 'is-info', 'is-light')
    button.innerHTML = name
    return button
}

// returns an ingredient tag
function makeIngredientTag(name) {

    const div = document.createElement('div')
    div.classList.add('tags', 'has-addons', 'nowrap', 'nomargin')

    const span = document.createElement('span')
    span.classList.add('tag', 'is-info')
    span.textContent = name

    const a = document.createElement('a')
    a.classList.add('tag', 'is-delete')

    div.appendChild(span)
    div.appendChild(a)

    return div
}

/* 
    <div class="recipe-container">
        <div class="box recipe-box">
            <p class="recipe-title">
                Ingredient Title bla sdsdsds sd sdsdsdsdsd
            </p>
            <div class="recipe-tags">

            </div>
        </div>
    </div>
*/

function makeRecipeBox(recipeName, ingredientNames) {
    
    const recipe_box = document.createElement('div')
    recipe_box.classList.add('box', 'recipe-box')
    const recipe_title = document.createElement('p')
    recipe_title.classList.add('recipe-title')
    const recipe_tags = document.createElement('div')
    recipe_tags.classList.add('recipe-tags')
    
    ingredientNames.forEach(n => {
        recipe_tags.appendChild(makeIngredientTagNonClosable(n))
    });

    recipe_title.textContent = recipeName

    recipe_box.appendChild(recipe_title)
    recipe_box.appendChild(recipe_tags)

    return recipe_box
}

// returns an ingredient tag (without a closing button)
function makeIngredientTagNonClosable(name) {
    //<span class="tag is-info">Info</span>
    const span = document.createElement('span')
    span.classList.add('tag', 'is-info')
    span.textContent = name

    return span
}

export { makeIngredientTag, makeIngredientSelection, makeRecipeBox }