import { test, expect } from '@playwright/test';

const username = process.env.MY_USERNAME ?? '';
const password = process.env.MY_PASSWORD ?? '';

test('should login, create a message and show the created message', async ({ page }) => {
  await page.goto('http://localhost:5173');
  const loginButton = page.getByText(/log in/i);

  await loginButton.click();
  const emailField = page.getByPlaceholder('yours@example.com');
  const passwordField = page.getByPlaceholder('your password');
  await emailField.fill(username);
  await passwordField.fill(password);
  await passwordField.press('Enter');
  await page.waitForLoadState();
  
  await expect(page.getByText(/log out/i)).toHaveCount(1);
  const messageField = page.getByLabel('Melding:');
  await messageField.fill(` melding som skal renses.`);
  await messageField.press('Enter');
  
  const newMessage = page.getByText(/Melding som skal renses - .*/);
  await expect(newMessage).toHaveCount(1);
});