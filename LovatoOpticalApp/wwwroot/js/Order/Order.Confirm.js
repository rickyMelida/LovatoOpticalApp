import { state } from './Order.State.js';
import { calculateTotal, updateDeposit, updatePaymentMethod, getPaymentMethodLabel, validatePayment, getPaymentStatus } from './Order.Payment.js';

export const buildSummary = () => {
    updateDeposit();

    const total        = calculateTotal();
    const balance        = Math.max(total - state.order.deposit, 0);
    const paymentStatus   = getPaymentStatus(total, state.order.deposit);
    const frameLabel = state.order.isOwnFrame ? 'Propio del cliente' : state.order.frame.name;
    const accessoriesLabel = state.order.accessories.length
        ? state.order.accessories.map(x => x.name).join(', ')
        : 'Ninguno';

    document.getElementById('resumenOrden').innerHTML = `
        <div class="card border">
            <div class="card-body">
                <h3 class="h6 mb-3">Resumen de la orden</h3>

                <div class="row g-3 small">
                    <div class="col-md-6">
                        <div class="text-muted">Cliente</div>
                        <strong>${state.order.patient.name}</strong>
                    </div>

                    <div class="col-md-6">
                        <div class="text-muted">Documento</div>
                        <strong>${state.order.patient.ciRuc}</strong>
                    </div>

                    <div class="col-md-6">
                        <div class="text-muted">Receta</div>
                        <strong>${state.order.prescription.date}</strong>
                    </div>

                    <div class="col-md-6">
                        <div class="text-muted">Armazón</div>
                        <strong>${frameLabel}</strong>
                    </div>

                    <div class="col-md-6">
                        <div class="text-muted">Cristal</div>
                        <strong>${state.order.lens.name}</strong>
                    </div>

                    <div class="col-md-6">
                        <div class="text-muted">Accesorios</div>
                        <strong>${accessoriesLabel}</strong>
                    </div>
                </div>

                <hr>

                <div class="d-flex justify-content-between">
                    <span>Total</span>
                    <strong>$${total.toFixed(2)}</strong>
                </div>

                <div class="d-flex justify-content-between">
                    <span>Anticipo</span>
                    <strong>$${state.order.deposit.toFixed(2)}</strong>
                </div>

                <div class="d-flex justify-content-between">
                    <span>balance pendiente</span>
                    <strong class="text-danger">$${balance.toFixed(2)}</strong>
                </div>

                <div class="d-flex justify-content-between mt-2">
                    <span>Estado de pago</span>
                    <span class="badge ${paymentStatus === 'Pagado' ? 'text-bg-success' : paymentStatus === 'Parcial' ? 'text-bg-warning' : 'text-bg-secondary'}">
                        ${paymentStatus}
                    </span>
                </div>
            </div>
        </div>
    `;

    const paymentSelect    = document.getElementById('metodoPagoSelect');
    const referenceInput = document.getElementById('referenciaPagoInput');

    document.getElementById('errorOrden').classList.add('d-none');

    if (state.order.deposit <= 0) {
        paymentSelect.value         = 'no_deposit';
        paymentSelect.disabled      = true;
        state.order.paymentMethod     = 'no_deposit';

        referenceInput.value      = '';
        state.order.paymentReference = '';
    } else {
        paymentSelect.disabled = false;

        if (state.order.paymentMethod && state.order.paymentMethod !== 'no_deposit') {
            paymentSelect.value = state.order.paymentMethod;
        } else {
            paymentSelect.value     = '';
            state.order.paymentMethod = '';
        }
    }

    updatePaymentMethod();
};

export const confirmOrder = () => {
    const errorBox   = document.getElementById('errorOrden');
    const errorText = document.getElementById('errorOrdenTexto');
    const simulate    = document.getElementById('simularError').checked;

    errorBox.classList.add('d-none');

    const paymentValidation = validatePayment();

    if (!paymentValidation.valid) {
        errorText.textContent = paymentValidation.message;
        errorBox.classList.remove('d-none');
        return;
    }

    if (simulate || (state.order.frame && state.order.frame.stock <= 0)) {
        errorText.textContent =
            'No fue posible completar la orden: stock insuficiente o fallo al guardar en base de datos. Se realizó rollback de la transacción.';
        errorBox.classList.remove('d-none');
        return;
    }

    const total           = calculateTotal();
    const balance           = Math.max(total - state.order.deposit, 0);
    const paymentStatus      = getPaymentStatus(total, state.order.deposit);
    const paymentMethodLabel = getPaymentMethodLabel();
    const orderNumber     = 'OT-' + Math.floor(100000 + Math.random() * 899999);
    const date            = new Date().toLocaleString('es-EC', { dateStyle: 'medium', timeStyle: 'short' });
    const frameLabel      = state.order.isOwnFrame ? 'Propio' : state.order.frame.name;

    document.getElementById('ticketBody').innerHTML = `
        <div class="text-center">
            <div class="fw-bold fs-6">ÓPTICA VISIÓN+</div>
            <div class="text-muted small">Comprobante de orden</div>
        </div>

        <hr>

        <div>N° Orden: <strong>${orderNumber}</strong></div>
        <div>Fecha: ${date}</div>
        <div>Estado: Creada</div>

        <hr>

        <div>Cliente: ${state.order.patient.name}</div>
        <div>Documento: ${state.order.patient.documentId}</div>
        <div>Armazón: ${frameLabel}</div>
        <div>Cristal: ${state.order.lens.name}</div>
        ${state.order.accessories.map(x => `<div>Accesorio: ${x.name}</div>`).join('')}

        <hr>

        <div class="d-flex justify-content-between">
            <strong>Total</strong>
            <strong>$${total.toFixed(2)}</strong>
        </div>

        <div class="d-flex justify-content-between">
            <span>Anticipo</span>
            <span>$${state.order.deposit.toFixed(2)}</span>
        </div>

        <div class="d-flex justify-content-between">
            <span>Saldo</span>
            <span>$${balance.toFixed(2)}</span>
        </div>

        <hr>

        <div>Método pago: ${paymentMethodLabel}</div>
        ${state.order.paymentReference ? `<div>Referencia: ${state.order.paymentReference}</div>` : ''}
        <div>Estado pago: ${paymentStatus}</div>

        <hr>

        <div class="text-center text-muted small">
            Gracias por confiar en Óptica Visión+
        </div>
    `;

    const modal = new bootstrap.Modal(document.getElementById('ticketModal'));
    modal.show();
};
