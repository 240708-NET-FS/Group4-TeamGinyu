import { fetchRecipesByName } from "../Page_search/fetchRecipesByName";

// Mocking the fetch function
global.fetch = jest.fn(() =>
    Promise.resolve({
        ok: true,
        status: 200,
        json: () => Promise.resolve({ results: [{ title: "recipe1" }, { title: "recipe2" }] }),
    })
);

it("returns a list of recipes", async () => {
    const res = await fetchRecipesByName("query");
    console.log("Returned recipes:", res);
    const expected = [{ title: "recipe1" }, { title: "recipe2" }];
    expect(res).toEqual(expected);
});
