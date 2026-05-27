import { test, expect } from '@playwright/test';

test('User can succesfully login', async ({ page }) => {
    // Go to the login page
    await page.goto('https://localhost:7018/Account/Login');

    // Fill in the login form
    await page.getByLabel(/email|gebruikersnaam|username/i).fill('test@example.com');
    await page.getByLabel(/password|wachtwoord/i).fill('Test123!');

    // Press the login button
    await page.getByRole('button', { name: /login|log in|inloggen/i }).click();

    // Check if the URL has changed to a page that indicates a successful login
    await expect(page).toHaveURL(/MyWorld|World|Dashboard|Home/);

    // Check if the page contains elements that are only visible to logged-in users
    await expect(page.getByText(/logout|uitloggen|my worlds|mijn werelden/i)).toBeVisible();
});
