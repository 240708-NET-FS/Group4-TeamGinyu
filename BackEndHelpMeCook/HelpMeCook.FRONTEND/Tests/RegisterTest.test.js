import {sendForm} from '../Page_register/sendform.js';
import {TextEncoder} from 'util';

describe('Register Test', () => {

    beforeEach(() => {
        consoleErrorSpy = jest.spyOn(global.console, 'error').mockImplementation(() => { });

        // Fetch mock setup
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
    // Test case for successful form submission
    it('should submit the form successfully', async () => {
        const form = {
            username: 'testuser',
            password: 'testpassword',
            email: 'testuser@example.com',
        };

        const encodedForm = new TextEncoder().encode(JSON.stringify(form));

        const response = await sendForm(encodedForm);

        expect(response.status).to.equal(200);
        expect(response.body.message).to.equal('Form submitted successfully');
    });

    // Test case for form submission with missing fields
    it('should return an error when required form fields are missing', async () => {
        const form = {
            username: 'testuser',
            password: 'testpassword',
        };

        const encodedForm = new TextEncoder().encode(JSON.stringify(form));

        const response = await sendForm(encodedForm);

        expect(response.status).to.equal(400);
        expect(response.body.error).to.equal('Missing required form fields');
    });

    // Test case for form submission with an existing username
    it('should return an error when submitting form with an existing username', async () => {
        const form = {
            username: 'existinguser',
            password: 'testpassword',
            email: 'existinguser@example.com',
        };

        const encodedForm = new TextEncoder().encode(JSON.stringify(form));

        const response = await sendForm(encodedForm);

        expect(response.status).to.equal(409);
        expect(response.body.error).to.equal('Username already exists');
    });

    // Test case for form submission with an existing email
    it('should return an error when submitting form with an existing email', async () => {
        const form = {
            username: 'testuser',
            password: 'testpassword',
            email: 'existinguser@example.com',
        };

        const encodedForm = new TextEncoder().encode(JSON.stringify(form));

        const response = await sendForm(encodedForm);

        expect(response.status).to.equal(409);
        expect(response.body.error).to.equal('Email already exists');
    });

});