import { aplicarFormatoGuarani } from '../Helper/Helper.js';
import { showModal } from "../Common/ModalEvents.js";
import { loadCrystalPrices } from "./Crystal.Prices.js"

const btnLoadCrystalPrices = document.getElementById("btnLoadCrystalPrices");
const buttons = document.querySelectorAll("#typeCrystalSelectorModal .btn");

buttons.forEach(button => {
	button.addEventListener("click", async () => {
		const selectedType = button.getAttribute("data-crystal");
		const modalSelected = document.getElementById(selectedType);
		
		showTypeCrystalSelectorModal(selectedType);
		hideTypeCrystalSelectorModal("typeCrystalSelectorModal");
		loadCrystalPrices(modalSelected);

	});
});

function showTypeCrystalSelectorModal(elementId) {
	const typeCrystalSelectorModal = new bootstrap.Modal(document.getElementById(elementId));
	typeCrystalSelectorModal.show();
}

function hideTypeCrystalSelectorModal(elementId) {
	const typeCrystalSelectorModal = bootstrap.Modal.getInstance(document.getElementById(elementId));
	if (typeCrystalSelectorModal) {
		typeCrystalSelectorModal.hide();
	}
}

btnLoadCrystalPrices.addEventListener("click", () => showModal("typeCrystalSelectorModal"));