import {sendForm} from '../Page_register/sendform.js';
import {TextEncoder} from 'util';

describe('Register Test', () => {

    beforeEach(() => {
        consoleErrorSpy = jest.spyOn(global.console, 'error').mockImplementation(() => { });

        const mockReader = {
            read: jest.fn().mockResolvedValueOnce({ done: false, value: new TextEncoder().encode(JSON.stringify({ message: 'Form submitted successfully' })) })
                             .mockResolvedValueOnce({ done: true })  // End of the stream
        };

        // Fetch mock setup
        global.fetch = jest.fn(() =>
            Promise.resolve({
                ok: true,
                status: 200,
                statusText: "OK",
                body: {
                    getReader: () => mockReader,
                },
            })
        );
    });

    // Test case for successful form submission
    it('should submit the form successfully', async () => {
        const url = '/register';
        const method = 'POST';
        const data = {
            email: 'testuser@example.com',
            password: 'testpassword',
        };

        const response = await sendForm(url, method, data);

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
        // console.log(response['status']);
        expect(response.status).toBe(200);
        // expect(response.body).toBe('Form submitted successfully');
    });

    // Test case for error with form submission
    it('should handle error when form submission fails', async () => {
        fetch.mockImplementationOnce(() => Promise.reject('API failure'));
        const url = '/register';
        const method = 'POST';
        const data = {
            email: '',
            password: '',
        };

        await sendForm(url, method, data);
        expect(consoleErrorSpy).toHaveBeenCalled();
    });
});