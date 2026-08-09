import { state } from './Order.State.js';

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
        console.log('Crystal form data:', state.order.crystal);
    });
};

const getValue = (name) => {
	const form = document.getElementById('crystalForm');

	const field = form.elements.namedItem(name);
	return field && 'value' in field ? field.value.trim() : '';
};

export const getCrystalData = () => {

    return {
        DescriptionOD: getValue('DescriptionOD'),
        PurchasePriceOD: getValue('PurchasePriceOD'),
        SalePriceOD: getValue('SalePriceOD'),
        DescriptionOI: getValue('DescriptionOI'),
        PurchasePriceOI: getValue('PurchasePriceOI'),
        SalePriceOI: getValue('SalePriceOI')
    };
};