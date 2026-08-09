import { state } from './Order.State.js';
import { getCrystalData } from './Order.Crystal.js';


export const handlerValidations = (step) => {
	console.log({ step });
	switch (step) {
		case 5:
			validateCrystalForm();
			break;
		default:
			break;
	}
}

const validateCrystalForm = () => {
	const form = document.getElementById('crystalForm');

	if (!form) return;

	if (!form.checkValidity()) {
		form.classList.add('was-validated');
		return;
	}

	state.order.crystal = getCrystalData();
}