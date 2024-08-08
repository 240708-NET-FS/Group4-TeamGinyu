import { sendForm } from "../Page_login/sendForm.js";
import { TextEncoder, TextDecoder } from 'util';

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

// beforeEach(() => {
//     global.localStorage = {
//         setItem: jest.fn(),
//         getItem: jest.fn(),
//         removeItem: jest.fn(),
//         clear: jest.fn(),
//     };
// });

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

    // expect(localStorage.setItem).toHaveBeenCalledWith('userObject', JSON.stringify({ key: 'value' }));
});
