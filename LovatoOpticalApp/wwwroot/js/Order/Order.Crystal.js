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
        
        fillPrintArea();

        setTimeout(() => window.print(), 300);

        state.order.lens = getCrystalOrderWork();

        console.log({ state })
    });
}
    
const getCrystalOrderWork = () => {
    return {
        Material: document.getElementById('material').value,
        Index: document.getElementById('indice').value,
        TreatmentNotes: document.getElementById('filtroTratamientos').value,

        // Graduación Ojo Derecho (OD)
        OD_ESF: document.getElementById('od_esferico').value,
        OD_CIL: document.getElementById('od_cilindrico').value,
        OD_AXIS: document.getElementById('od_eje').value,
        OD_ADD: document.getElementById('od_adicion').value,
        OD_DNP: document.getElementById('od_dnp').value,
        OD_HEIGHT: document.getElementById('od_altura').value,

        // Graduación Ojo Izquierdo (OI)
        OI_ESF: document.getElementById('oi_esferico').value,
        OI_CIL: document.getElementById('oi_cilindrico').value,
        OI_AXIS: document.getElementById('oi_eje').value,
        OI_ADD: document.getElementById('oi_adicion').value,
        OI_DNP: document.getElementById('oi_dnp').value,
        OI_HEIGHT: document.getElementById('oi_altura').value,

        // Medidas del armazón
        Mounting: document.getElementById('montaje').value,
        Horizontal: document.getElementById('horizontal').value,
        Vertical: document.getElementById('vertical').value,
        MajorDiagonal: document.getElementById('diagMayor').value,
        Bridge: document.getElementById('puente').value,
        PantoscopicAngle: document.getElementById('angPantoscopico').value,
        PanoramicAngle: document.getElementById('angPanoramico').value,
        Observations: document.getElementById('angPanoramico').value,
    }
}

const val = (id) => {
    const missedField = {
        numero: "1234",
        fecha: new Date(),
        cliente: state.order.patient.name,
        controlCliente: "Excelente",
        nombrePaciente: state.order.patient.name
    }

    if (missedField.hasOwnProperty(id))
        return missedField[id];
    

    const el = document.getElementById(id);

    return el ? (el.value || '').trim() : '';
}

const formatFecha = (date) => {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();

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