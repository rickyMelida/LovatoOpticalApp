import { state } from './Order.State.js';

export const handlerCrystalForm = () => {
    const form = document.getElementById('crystalForm');

    form.addEventListener('submit', function (event) {
        event.preventDefault();
        event.stopPropagation();

        if (!form.checkValidity()) {
            form.classList.add('was-validated');
            return;
        }

        form.classList.add('was-validated');

        console.log({state})

        fillPrintArea();

        setTimeout(() => window.print(), 300);
    });
}
const val = (id) => {
    const missedField = {
        numero: "1234",
        fecha: new Date(),
        cliente: state.order.patient.name,
        controlCliente: "Excelente",
        nombrePaciente: state.order.patient.name
    }

    if (missedField.hasOwnProperty(id)) {
        return missedField[id];
    }



    const el = document.getElementById(id);
    return el ? (el.value || '').trim() : '';
}

const formatFecha = (date) => {
    const day = String(date.getDate()).padStart(2, '0');       // 01-31
    const month = String(date.getMonth() + 1).padStart(2, '0'); // 01-12
    const year = date.getFullYear();                            // yyyy

    return `${day}-${month}-${year}`;
}


const fillPrintArea = () => {
    document.getElementById('p_numero').textContent = val('numero');
    document.getElementById('p_fecha').textContent = formatFecha(val('fecha'));
    document.getElementById('p_cliente').textContent = val('cliente');
    document.getElementById('p_controlCliente').textContent = val('controlCliente');
    document.getElementById('p_nombrePaciente').textContent = val('nombrePaciente');
    document.getElementById('p_material').textContent = val('material');
    document.getElementById('p_indice').textContent = val('indice');
    document.getElementById('p_filtroTratamientos').textContent = val('filtroTratamientos');

    ['esferico', 'cilindrico', 'eje', 'adicion', 'dnp', 'altura'].forEach(campo => {
        document.getElementById(`p_od_${campo}`).textContent = val(`od_${campo}`);
        document.getElementById(`p_oi_${campo}`).textContent = val(`oi_${campo}`);
    });

    document.getElementById('p_montaje').textContent = val('montaje');
    document.getElementById('p_horizontal').textContent = val('horizontal');
    document.getElementById('p_vertical').textContent = val('vertical');
    document.getElementById('p_diagMayor').textContent = val('diagMayor');
    document.getElementById('p_puente').textContent = val('puente');
    document.getElementById('p_angPantoscopico').textContent = val('angPantoscopico');
    document.getElementById('p_angPanoramico').textContent = val('angPanoramico');
    document.getElementById('p_observaciones').textContent = val('observaciones');
}