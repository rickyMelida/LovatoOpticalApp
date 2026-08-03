import { DB_PATIENTS, state } from './Order.State.js';
import { showFeedback, hideFeedback } from './Order.UI.js';
import { enableLargeButton } from '../Common/ButtonEvents.js';
import { buildCustomerPayload } from "../Customer/Customer.Form.js";
import { createCustomer } from "../Customer/Customer.Rules.js";
import { enableButton } from '../Common/ButtonEvents.js';

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

export const createPatient = async () => {
    hideFeedback();
	const form = document.getElementById("newCustomerForm");
	const btnCreatePatient = document.getElementById("btnCreateCustomer");

	enableButton(btnCreatePatient, true);
	
	if (!form.checkValidity()) {
		form.reportValidity();
		enableButton(btnCreatePatient, false);
		return;
	}
	
	const customerPayload = buildCustomerPayload();
	const newPatient = await createCustomer(customerPayload);

    state.order.patient = newPatient;

    document.getElementById('formNuevoPaciente').classList.add('d-none');
    document.getElementById('pacienteNoEncontrado').classList.add('d-none');

    document.getElementById('pacNombre').textContent = newPatient.name;
    document.getElementById('pacDoc').textContent    = newPatient.ciRuc;

    document.getElementById('pacienteEncontrado').classList.remove('d-none');
};
