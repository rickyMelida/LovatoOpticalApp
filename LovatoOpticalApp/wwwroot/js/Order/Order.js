import { updateStepper } from './Order.UI.js';
import { searchPatient, createPatient } from './Order.Customer.js';
import { setPrescriptionMode, fetchCurrentPrescription, createPrescription } from './Order.Recipe.js';
import { setFrameMode, selectFrame } from './Order.Frame.js';
//import { selectLens, toggleAccessory } from './Order.Crystal.js';
import { handlerCrystalForm } from "./Order.Crystal.js";
import { updateDeposit, updatePaymentMethod } from './Order.Payment.js';
import { buildSummary, confirmOrder } from './Order.Confirm.js';
import { goToStep } from './Order.Nav.js';

/* ---------- EXPOSE GLOBAL FUNCTIONS (inline onclick) ---------- */
window.searchPatient       = searchPatient;
window.createPatient        = createPatient;

window.setPrescriptionMode        = setPrescriptionMode;
window.fetchCurrentPrescription = fetchCurrentPrescription;
window.createPrescription          = createPrescription;

window.setFrameMode       = setFrameMode;
window.selectFrame   = selectFrame;

//window.selectLens        = selectLens;
//window.toggleAccessory      = toggleAccessory;
window.updateDeposit    = updateDeposit;

window.updatePaymentMethod = updatePaymentMethod;

window.buildSummary     = buildSummary;
window.confirmOrder       = confirmOrder;

window.goToStep          = goToStep;

/* ---------- INIT ---------- */
updateStepper();
handlerCrystalForm();

