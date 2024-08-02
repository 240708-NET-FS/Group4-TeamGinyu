function makeIngredient(name) {

    const div = document.createElement('div')
    div.classList.add('tags', 'has-addons')

    const span = document.createElement('span')
    span.classList.add('tag', 'is-danger')
    span.textContent = name

    const a = document.createElement('a')
    a.classList.add('tag', 'is-delete')

    div.appendChild(span)
    div.appendChild(a)

    return div
}

export { makeIngredient }