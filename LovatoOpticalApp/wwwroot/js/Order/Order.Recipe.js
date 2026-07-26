import { DB_PRESCRIPTIONS, state } from './Order.State.js';
import { showFeedback, hideFeedback } from './Order.UI.js';
import { dateDayMonthYear } from '../Common/DateFormats.js';
import { crystalShortFormatt } from '../Common/CrystalFormatt.js';
import { enableLargeButton } from '../Common/ButtonEvents.js';
import { buildRecipePayload } from '../Customer/Customer.Form.js';
import { enableButton } from '../Common/ButtonEvents.js';

export const setPrescriptionMode = (mode) => {
    hideFeedback();

    document.getElementById('cardRecetaExistente').classList.toggle('selected', mode === 'existing');
    document.getElementById('cardRecetaNueva').classList.toggle('selected', mode === 'new');

    document.getElementById('bloqueRecetaExistente').classList.toggle('d-none', mode !== 'existing');
    document.getElementById('bloqueRecetaNueva').classList.toggle('d-none', mode !== 'new');

    state.order.prescription = null;
};

export const fetchCurrentPrescription = async () => {
    const card = document.getElementById('recetaVigenteCard');
    const btnGetVigentRecipe = document.getElementById('btn-get-vigent-recipe');
    const btnOriginalContent = `<i class="bi bi-arrow-repeat me-1"></i>Obtener receta vigente`;

    enableLargeButton(btnGetVigentRecipe, true);

    const prescription = await getRecipeAsync(state.order.patient.id);

    card.classList.remove('d-none', 'alert-success', 'alert-danger');

    if (prescription) {
        state.order.prescription = prescription;

        card.classList.add('alert-success');
        card.innerHTML = `
            <strong>Receta vigente encontrada</strong><br>
            <span class="small">${crystalShortFormatt(prescription, 'OD')} · ${crystalShortFormatt(prescription, 'OI') }</span><br>
            <span class="small">Fecha: ${dateDayMonthYear(prescription.prescriptionIssueDate)} · ${prescription.optometrist}</span>
        `;
    } else {
        card.classList.add('alert-danger');
        card.innerHTML = `
            <strong>Este paciente no tiene receta vigente.</strong>
            <span class="small">Carga una nueva receta para continuar.</span>
        `;
    }

    enableLargeButton(btnGetVigentRecipe, false, btnOriginalContent);
};

const getRecipeAsync = async (customerId) => {
    const result = await fetch(`/Customer/GetLastRecipe?customerId=${customerId}`);

    if (result.status == 204)
        return null;

    return await result.json();
}

export const createPrescription = async () => {
    hideFeedback();
    const form = document.getElementById('createRecipe');
    const btnCreateRecipe = document.getElementById('btnCreateRecipe');

    enableButton(btnCreateRecipe, true);

    const recipeRequest = buildRecipePayload(form);
    const request = { ...recipeRequest, CustomerId: state.order.patient.id }
    const result = await createRecipeAsync(request);

    if (!result.status && result.status != 201) {
        showFeedback('Hubo un error al crear la receta.');
        enableButton(btnCreateRecipe, false);
        return;
    }

    state.order.prescription = { ...request };

    showFeedback(`${result.message} Puedes continuar.`, 'success');
    enableButton(btnCreateRecipe, false);
};

const createRecipeAsync = async (request) => {
    const result = await fetch('/Customer/CreateRecipe', {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(request)
    });


    return await result.json();
}
