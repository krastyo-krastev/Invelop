// e2e/account-settings.spec.ts
import { test, expect } from '@playwright/test';

test('navigate from list to add new contact', async ({ page }) => {
  await page.goto('/contacts');
  const elem1 = page.getByTestId('contacts-list-h2-title'); 
  await expect(elem1).toBeVisible();

  // Get Add Contact button 
  const ctrl = page.getByTestId('contacts-list-button-add-contact');
  await expect(ctrl).toBeVisible();
  ctrl.click();

  // Get page head/title
  const elem2 = page.getByTestId('contacts-form-h2-title') 
  await expect(elem2).toBeVisible();

  // Check url
  await expect(page).toHaveURL('/contacts/new');
});

test('navigate from new contact page back to list page', async ({ page }) => {
  await page.goto('/contacts/new');
  const elem1 = page.getByTestId('contacts-form-h2-title'); 
  await expect(elem1).toBeVisible();

  // Get Add Contact button 
  const ctrl = page.getByTestId('contacts-form-button-cancel');
  await expect(ctrl).toBeVisible();
  ctrl.click();

  // Get page head/title
  const elem2 = page.getByTestId('contacts-list-h2-title') 
  await expect(elem2).toBeVisible();

  // Check url
  await expect(page).toHaveURL('/contacts');    
});