import { DB_PATIENTS, state } from './Order.State.js';
import { showFeedback, hideFeedback } from './Order.UI.js';
import { enableLargeButton } from '../Common/ButtonEvents.js'

export const searchPatient = async () => {
    hideFeedback();

    const documentId = document.getElementById('docInput').value.trim();
    const btnSearch = document.getElementById('btn-search');


    const found    = document.getElementById('pacienteEncontrado');
    const notFound  = document.getElementById('pacienteNoEncontrado');
    const newPatientForm     = document.getElementById('formNuevoPaciente');

    found.classList.add('d-none');
    notFound.classList.add('d-none');
    newPatientForm.classList.add('d-none');

    if (!documentId) {
        showFeedback('Ingresa un documento para buscar al cliente.');
        return;
    }
    enableLargeButton(btnSearch, true);

    const patient = await findCustomerByDocument(documentId);//DB_PATIENTS.find(x => x.documentId === documentId);

    if (patient) {
        state.order.patient = patient;

        document.getElementById('pacNombre').textContent = patient.name;
        document.getElementById('pacDoc').textContent = patient.ciRuc;

        found.classList.remove('d-none');
    } else {
        state.order.patient = null;
        notFound.classList.remove('d-none');
        newPatientForm.classList.remove('d-none');
    }

    enableLargeButton(btnSearch, false);
};

const findCustomerByDocument = async (documentId) => {
    const result = await fetch(`/Customer/GetCustomerByDoc?doc=${documentId}`);

    if (result.status == 204)
        return null;
    
    return await result.json();
}

export const createPatient = () => {
    hideFeedback();

    const documentId    = document.getElementById('docInput').value.trim();
    const name = document.getElementById('nuevoNombre').value.trim();

    if (!documentId) {
        showFeedback('Ingresa el documento del cliente.');
        return;
    }

    if (!name) {
        showFeedback('Ingresa el nombre completo del cliente.');
        return;
    }

    const newPatient = {
        id: DB_PATIENTS.length + 100,
        documentId,
        name,
        phone: document.getElementById('nuevoTelefono').value.trim(),
        email:    document.getElementById('nuevoEmail').value.trim()
    };

    DB_PATIENTS.push(newPatient);
    state.order.patient = newPatient;

    document.getElementById('formNuevoPaciente').classList.add('d-none');
    document.getElementById('pacienteNoEncontrado').classList.add('d-none');

    document.getElementById('pacNombre').textContent = newPatient.name;
    document.getElementById('pacDoc').textContent    = newPatient.documentId;

    document.getElementById('pacienteEncontrado').classList.remove('d-none');
};
