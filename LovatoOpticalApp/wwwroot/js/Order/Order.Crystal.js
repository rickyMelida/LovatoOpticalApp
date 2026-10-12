﻿import { state } from './Order.State.js';
import { guaraniStringANumero } from '../Helper/Helper.js';
import { showPrescriptionInfo } from './Order.Recipe.js';

export const initializeCrystalStep = () => {
	const card = document.getElementById('crystalRecipe');
	showPrescriptionInfo(card);
}

const amountEyesSelect = document.getElementById('amountEyes');
amountEyesSelect.addEventListener('change', handleEyeSideChange);

function handleEyeSideChange() {
	const selectedValue = this.value;
	const showBoth = selectedValue === 'ambos' || selectedValue === '';
	
	showCrystalEyeSide(selectedValue, showBoth);

}


const showCrystalEyeSide = (eyeSide, showBoth) => {
	const eyeToHide = eyeSide === 'od' ? 'oi' : 'od';

	const crystalToHide = document.getElementById(`crystal-${eyeToHide}-section`);
	const crystalToShow = document.getElementById(`crystal-${eyeSide}-section`);

	if (!showBoth) {
		crystalToHide.style.display = 'none';
		crystalToShow.style.display = 'block';
	}else{
		crystalToHide.style.display = 'block';
		crystalToShow.style.display = 'block';
	}
}

const handlerCrystalForm = () => {
    const form = document.getElementById('crystalForm');
	
    if (!form) {
        return;
    }
	
    form.addEventListener('submit', function (event) {
		event.preventDefault();
        event.stopPropagation();

        if (!form.checkValidity()) {
            form.classList.add('was-validated');
            return;
        }

        state.order.crystal = getCrystalData();
    });
};

const getValue = (name) => {
	const form = document.getElementById('crystalForm');

	const field = form.elements.namedItem(name);
	return field && 'value' in field ? field.value.trim() : '';
};

export const getCrystalData = () => {

    return {
		NameOD: getValue('NameOD'),
        DescriptionOD: getValue('DescriptionOD'),
        PurchasePriceOD: guaraniStringANumero(getValue('PurchasePriceOD')),
        SalePriceOD: guaraniStringANumero(getValue('SalePriceOD')),
		NameOI: getValue('NameOI'),
        DescriptionOI: getValue('DescriptionOI'),
        PurchasePriceOI: guaraniStringANumero(getValue('PurchasePriceOI')),
        SalePriceOI: guaraniStringANumero(getValue('SalePriceOI'))
    };
};