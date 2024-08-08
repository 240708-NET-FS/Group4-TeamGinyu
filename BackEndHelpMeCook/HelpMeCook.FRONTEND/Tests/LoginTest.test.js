import { sendForm } from "../Page_login/sendForm.js";
import { TextEncoder } from 'util';

describe('Login Test', () => {
    let localStorageSpy;

    beforeEach(() => {

        localStorageSpy = jest.spyOn(Storage.prototype, 'setItem').mockImplementation(() => { });
        consoleErrorSpy = jest.spyOn(global.console, 'error').mockImplementation(() => { });
     

        // Polyfill TextEncoder and TextDecoder
        global.TextEncoder = TextEncoder;

        // Mock fetch
        global.fetch = jest.fn(() =>
            Promise.resolve({
                ok: true,
                status: 200,
                statusText: "OK",
                body: {
                    getReader: () => ({
                        read: () => Promise.resolve({
                            done: true,
                            value: new TextEncoder().encode(JSON.stringify({ key: 'value' }))
                        })
                    })
                }
            })
        );

    });

    afterEach(() => {
        // Restore all mocks and spies after each test
        jest.restoreAllMocks();
    });


    it('should handle successful form submission', async () => {
        const url = '/login';
        const method = 'POST';
        const data = { email: 'test@example.com', password: 'password123' };

        await sendForm(url, method, data);


        // Check if fetch was called with the correct arguments
        expect(fetch).toHaveBeenCalledWith(url, {
            method,
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Headers': 'Content-Type, Authorization',
                'Access-Control-Allow-Methods': '*',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data),
        });

        // Check if localStorage.setItem was called
        expect(localStorageSpy).toHaveBeenCalledTimes(1);

    });

    it('should handle error form submission', async () => {
        fetch.mockImplementationOnce(() => Promise.reject("API failure"));
        const url = '/login';
        const method = 'POST';
        const data = { email: 'test@example.com', password: 'password123' };

        await sendForm(url, method, data);

        expect(consoleErrorSpy).toHaveBeenCalled();
    });
});

