import { DB_PRESCRIPTIONS, state } from './Order.State.js';
import { showFeedback, hideFeedback } from './Order.UI.js';
import { dateDayMonthYear } from '../Common/DateFormats.js';
import { crystalShortFormatt } from '../Common/CrystalFormatt.js';
import { enableLargeButton } from '../Common/ButtonEvents.js';

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

export const createPrescription = () => {
    hideFeedback();

    const date         = document.getElementById('fechaReceta').value;
    const optometrist  = document.getElementById('optometrista').value.trim();

    if (!date || !optometrist) {
        showFeedback('Completa la fecha de la receta y el nombre del optometrista.');
        return;
    }

    const odSphere   = document.getElementById('odEsfera').value.trim()   || 'sin dato';
    const odCylinder = document.getElementById('odCilindro').value.trim() || 'sin dato';
    const odAxis      = document.getElementById('odEje').value.trim()      || 'sin dato';

    const oiSphere   = document.getElementById('oiEsfera').value.trim()   || 'sin dato';
    const oiCylinder = document.getElementById('oiCilindro').value.trim() || 'sin dato';
    const oiAxis      = document.getElementById('oiEje').value.trim()      || 'sin dato';

    state.order.prescription = {
        od: `${odSphere} / ${odCylinder} / ${odAxis}`,
        oi: `${oiSphere} / ${oiCylinder} / ${oiAxis}`,
        date,
        optometrist
    };

    showFeedback('Receta guardada correctamente. Puedes continuar.', 'success');
};
