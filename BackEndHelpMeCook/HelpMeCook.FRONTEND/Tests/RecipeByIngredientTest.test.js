import { fetchIngredientByName } from "../Page_search/fetchIngredientByName";


describe('Fetch Ingredient By Name test', () => {

    global.fetch = jest.fn(() =>
        Promise.resolve({
            ok: true,
            json: () => Promise.resolve({
                results: [
                    { id: 1111, name: 'friedOnion', image: 'onion.png' }
                ]
            })
        })
    );

    it('Fetch ingredients by name', async () => {
        const fromFetch =
            [{
                id: 1111,
                name: 'friedOnion',
                image: 'onion.png'
            }];

        const res = await fetchIngredientByName('onion');

        expect(res).toEqual(fromFetch);
    });
});
