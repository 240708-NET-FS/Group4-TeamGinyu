function makeIngredientResult(name) {
    const p = document.createElement('p')
    p.innerHTML = name

    return p
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

export { makeIngredientTag, makeIngredientResult }