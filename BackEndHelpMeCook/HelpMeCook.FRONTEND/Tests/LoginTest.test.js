import { sendForm } from "../Page_login/sendForm.js";
import { TextEncoder, TextDecoder } from 'util';

describe('Login Test', () => {
    let localStorageSpy;
    // let consoleErrorSpy;
    let consoleSpy;

    beforeEach(() => {
        // global.localStorage = {
        //     setItem: jest.fn(),
        //     getItem: jest.fn(),
        //     removeItem: jest.fn(),
        //     clear: jest.fn(),
        // };

        // consoleErrorSpy = jest.spyOn(global.console, 'error').mockImplementation(() => {});

        consoleSpy = jest.spyOn(console, 'error').mockImplementation(() => {});

        // Polyfill TextEncoder and TextDecoder
        global.TextEncoder = TextEncoder;
        global.TextDecoder = TextDecoder;

        // Mock fetch
        global.fetch = jest.fn(() =>
            Promise.resolve({
                ok: true,
                status: 200,
                statusText: 'ok',
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
        
       // Reset all mocks before each test
    //    jest.resetAllMocks();
        
    });

    afterEach(() => {
        // Restore all mocks and spies after each test
        jest.restoreAllMocks();
    });


    it('should handle successful form submission', async () => {
        const url = '/login';
        const method = 'POST';
        const data = { email: 'test@example.com', password: 'password123' };

        // Spy on localStorage.setItem
        localStorageSpy = jest.spyOn(global.localStorage, 'setItem');

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
        // expect(localStorageSpy).toHaveBeenCalled();
        // expect(localStorageSpy).toHaveBeenCalledWith('userObject', JSON.stringify({ key: 'value' }));

        // Restore localStorage.setItem
        // localStorageSpy.mockRestore();
    });

    it('should handle error form submission', async () => {
        fetch.mockImplementationOnce(() => Promise.reject("API failure"));
        const url = '/login';
        const method = 'POST';
        const data = { email: 'test@example.com', password: 'password123' };
        // const consoleLogSpy = jest.spyOn(global.console, 'error');

        let res = await sendForm(url, method, data);

        console.log(res);
        
        expect(consoleSpy).toHaveBeenCalled();

        // Cleanup
        // consoleErrorSpy.mockRestore();
    });
});

// import { consoleThis } from "../TestingFolder/TestingFunction";

// describe('Simple Jest Test', () => {
//     let consoleSpy;

//     beforeEach(() => {
//         consoleSpy = jest.spyOn(console, 'log').mockImplementation(() => {});
//     });

//     afterEach(() => {
//         consoleSpy.mockRestore();
//     });

//     it('should spy on console.log', () => {
//         consoleThis();
//         expect(consoleSpy).toHaveBeenCalledWith('Hello, world!');
//     });
// });

