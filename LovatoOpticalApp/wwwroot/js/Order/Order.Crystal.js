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