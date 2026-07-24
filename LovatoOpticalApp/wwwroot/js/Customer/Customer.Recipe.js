import { showModal } from '../Common/ModalEvents.js'
import { buildRecipePayload, resetForm } from './Customer.Form.js';
import { enableButton } from '../Common/ButtonEvents.js';

export const handlerCustomer = () => {
    const addRecipeButton = document.querySelectorAll('.add-recipe');
    const btnSaveRecipe = document.getElementById('btnSaveRecipe');
    const form = document.getElementById("newRecipeForm");
    let customerId = null;

    addRecipeButton.forEach(button => {
        button.addEventListener('click', async (event) => {
            event.preventDefault();
            customerId = button.getAttribute('id');

            showModal("newRecipeModal");
        })
    });

    btnSaveRecipe.addEventListener('click', async (e) => {
        e.preventDefault();
        enableButton(btnSaveRecipe, true);

        const recipePayload = buildRecipePayload(form);
        const result = await saveRecipe(recipePayload, customerId);

        showAlert(result.message, "Receta Agregada", "success")
            .then(() => {
                resetForm(form, btnSaveRecipe);
                hideModal("newRecipeModal");
            })
    })
}

const saveRecipe = async (recipePayload, customerId) => {
    const request = { ...recipePayload, customerId };
   
    const result = await fetch('/Customer/CreateRecipe', {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(request)
    });

    return result.json();
}