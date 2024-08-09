import { fetchRecipesByIngredients } from "../Page_search/fetchRecipesByIngredient";


describe('tests for FetchRecipeByIngredient', () => {

    const fetchReturnOnSuccess = [
        {id: 125, title: 'Some Title', missedIngredients: ['ing1', 'ing2'], includedIngredients: ['ing3']},
        {id: 124, title: 'Some Title2', missedIngredients: ['ing1', 'ing2'], includedIngredients: ['ing3']},        
    ]
    // when the fetch returns a speciffic thing, the result of the function is a speciffic return
    it('provided a single ingredient, successfully fetches', async () => {

        // arrange
        // define what will the fetch request return
        global.fetch = jest.fn(() => Promise.resolve({
            ok: true,
            json: () => Promise.resolve(fetchReturnOnSuccess)
        }))

        // act
        // call the tested method
        const result = await fetchRecipesByIngredients(['ing1'])

        // assert
        // make sure the fetch was called once
        expect(fetch).toHaveBeenCalledTimes(1)
        // make sure the returned value of the tested method makes sense in relation to the arguments passed
        expect(result).toEqual(fetchReturnOnSuccess)
    })
})