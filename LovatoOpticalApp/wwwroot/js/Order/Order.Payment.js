import { state } from './Order.State.js';

export const calculateTotal = () => {
    let total = state.order.lens ? state.order.lens.price : 0;

    if (state.order.frame) {
        total += state.order.frame.price;
    }

    state.order.accessories.forEach(accessory => {
        total += accessory.price;
    });

    return total;
};

export const updateDeposit = () => {
    const input = document.getElementById('anticipoInput');

    if (!input) return;

    state.order.deposit = parseFloat(input.value) || 0;
};

export const updatePaymentMethod = () => {
    const paymentSelect  = document.getElementById('metodoPagoSelect');
    const referenceInput = document.getElementById('referenciaPagoInput');
    const referenceBox = document.getElementById('referenciaPagoBox');

    if (!paymentSelect) return;

    state.order.paymentMethod    = paymentSelect.value;
    state.order.paymentReference = referenceInput ? referenceInput.value.trim() : '';

    const requiresReference = ['debit_card', 'credit_card', 'transfer'].includes(state.order.paymentMethod);

    referenceBox.classList.toggle('d-none', !requiresReference);

    if (!requiresReference && referenceInput) {
        referenceInput.value      = '';
        state.order.paymentReference = '';
    }
};

export const getPaymentMethodLabel = () => {
    const methods = {
        cash:         'Efectivo',
        debit_card:   'Tarjeta de débito',
        credit_card:  'Tarjeta de crédito',
        transfer:     'Transferencia bancaria',
        no_deposit:   'Sin anticipo'
    };

    return methods[state.order.paymentMethod] || 'No seleccionado';
};

export const validatePayment = () => {
    updateDeposit();
    updatePaymentMethod();

    const total = calculateTotal();

    if (state.order.deposit <= 0) {
        state.order.paymentMethod    = 'no_deposit';
        state.order.paymentReference = '';
        return { valid: true };
    }

    if (state.order.deposit > total) {
        return { valid: false, message: 'El anticipo no puede ser mayor al total de la orden.' };
    }

    if (!state.order.paymentMethod || state.order.paymentMethod === 'no_deposit') {
        return { valid: false, message: 'Selecciona un método de pago para registrar el anticipo.' };
    }

    const requiresReference = ['debit_card', 'credit_card', 'transfer'].includes(state.order.paymentMethod);

    if (requiresReference && !state.order.paymentReference) {
        return { valid: false, message: 'Ingresa la referencia o número de comprobante del pago.' };
    }

    return { valid: true };
};

export const getPaymentStatus = (total, deposit) => {
    if (deposit <= 0)        return 'Pendiente';
    if (deposit >= total)    return 'Pagado';
    return 'Parcial';
};
