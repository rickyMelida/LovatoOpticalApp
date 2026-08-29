import { state } from './Order.State.js';
import { showFeedback, hideFeedback, updateStepper } from './Order.UI.js';
import { buildSummary } from './Order.Confirm.js';
import { handlerValidations } from './Order.Validations.js';

const validateStep = (n) => {
    if (n === 1 && !state.order.patient) {
        showFeedback('Busca o registra al cliente antes de continuar.');
        return false;
    }

    if (n === 2 && !state.order.prescription) {
        showFeedback('Selecciona u obtén una receta antes de continuar.');
        return false;
    }

    if (n === 3 && !state.order.isOwnFrame && !state.order.frame) {
        showFeedback('Selecciona un armazón antes de continuar.');
        return false;
    }

	if (n === 5 && !state.order.crystal) {
		showFeedback('Debe de cargar los datos del cristal antes de continuar.');
		return false;
	}

	return true;
};

export const goToStep = (dir) => {
	const destination = state.currentStep + dir;

	hideFeedback();
	handlerValidations(destination);

	if (dir === 1 && !validateStep(state.currentStep)) return;
	if (destination < 1 || destination > 6) return;

	document.getElementById(`panel-${state.currentStep}`).classList.remove('active');

	state.currentStep = destination;

	document.getElementById(`panel-${state.currentStep}`).classList.add('active');

	document.getElementById('btnAtras').disabled = state.currentStep === 1;
	document.getElementById('btnSiguiente').classList.toggle('d-none', state.currentStep === 6);

	updateStepper();

	if (state.currentStep === 6) buildSummary();
};