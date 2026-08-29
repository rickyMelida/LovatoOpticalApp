import { state } from '../Order/Order.State.js';
import { searchPatient, createPatient, showNewPatientForm } from '../Order/Order.Customer.js';

/* ---------- EXPOSE GLOBAL FUNCTIONS (inline onclick) ---------- */
window.searchPatient = searchPatient;
window.createPatient = createPatient;
window.showNewPatientForm = showNewPatientForm;

const openNewOrderWorkModal = () => {
    const modalEl = document.getElementById('newOrderWorkModal');
    if (!modalEl) return;

    const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
    modal.show();
};

const val = (id) => {
    const el = document.getElementById(id);
    return el ? (el.value || '').trim() : '';
};

const formatFecha = (date) => {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();
    return `${day}-${month}-${year}`;
};

const fillPrintArea = () => {
    const setText = (id, text) => {
        const el = document.getElementById(id);
        if (el) el.textContent = text;
    };

    const patient = state.order.patient;
    const patientName = patient ? (patient.name || '') : '';

    setText('p_numero', '');
    setText('p_fecha', formatFecha(new Date()));
    setText('p_cliente', patientName);
    setText('p_controlCliente', '');
    setText('p_nombrePaciente', patientName);
    setText('p_material', val('material'));
    setText('p_indice', val('indice'));
    setText('p_filtroTratamientos', val('filtroTratamientos'));

    ['esferico', 'cilindrico', 'eje', 'adicion', 'dnp', 'altura'].forEach(campo => {
        setText(`p_od_${campo}`, val(`od_${campo}`));
        setText(`p_oi_${campo}`, val(`oi_${campo}`));
    });

    setText('p_montaje', val('montaje'));
    setText('p_horizontal', val('horizontal'));
    setText('p_vertical', val('vertical'));
    setText('p_diagMayor', val('diagMayor'));
    setText('p_puente', val('puente'));
    setText('p_angPantoscopico', val('angPantoscopico'));
    setText('p_angPanoramico', val('angPanoramico'));
    setText('p_observaciones', val('observaciones'));
};

const handleWorkOrderForm = () => {
    const form = document.getElementById('workOrderForm');
    if (!form) return;

    form.addEventListener('submit', (event) => {
        event.preventDefault();
        event.stopPropagation();

        if (!form.checkValidity()) {
            form.classList.add('was-validated');
            return;
        }

        if (!state.order.patient) {
            form.classList.add('was-validated');
            alert('Debe seleccionar o registrar un cliente antes de imprimir la orden de trabajo.');
            return;
        }

        form.classList.add('was-validated');
        fillPrintArea();
        setTimeout(() => window.print(), 300);
    });
};

const init = () => {
    const btnNew = document.getElementById('btnNewOrderWork');
    if (btnNew) btnNew.addEventListener('click', openNewOrderWorkModal);

    handleWorkOrderForm();
};

init();
