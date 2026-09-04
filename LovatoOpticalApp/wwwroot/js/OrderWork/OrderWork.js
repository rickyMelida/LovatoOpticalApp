import { state } from '../Order/Order.State.js';
import { searchPatient, createPatient, showNewPatientForm } from '../Order/Order.Customer.js';

/* ---------- EXPOSE GLOBAL FUNCTIONS (inline onclick) ---------- */
window.searchPatient = searchPatient;
window.createPatient = createPatient;
window.showNewPatientForm = showNewPatientForm;

const loadNextIndex = async () => {
    const input = document.getElementById('indice');
    if (!input) return;

    input.value = '';
    input.placeholder = 'Cargando...';
    try {
        const response = await fetch('/OrderWork/GetNextIndex', {
            headers: { 'Accept': 'application/json' }
        });
        if (!response.ok) throw new Error(`HTTP ${response.status}`);
        const data = await response.json();
        input.value = data?.nextIndex ?? '';
    } catch {
        input.value = '';
    } finally {
        input.placeholder = 'Índice';
    }
};

const openNewOrderWorkModal = () => {
    const modalEl = document.getElementById('newOrderWorkModal');
    if (!modalEl) return;

    const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
    modal.show();
    loadNextIndex();
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

const buildRequestDto = () => ({
    material: val('material'),
    index: val('indice'),
    treatmentNotes: val('filtroTratamientos'),

    od_ESF: val('od_esferico'),
    od_CIL: val('od_cilindrico'),
    od_AXIS: val('od_eje'),
    od_ADD: val('od_adicion'),
    od_DNP: val('od_dnp'),
    od_HEIGHT: val('od_altura'),

    oi_ESF: val('oi_esferico'),
    oi_CIL: val('oi_cilindrico'),
    oi_AXIS: val('oi_eje'),
    oi_ADD: val('oi_adicion'),
    oi_DNP: val('oi_dnp'),
    oi_HEIGHT: val('oi_altura'),

    mounting: val('montaje'),
    horizontal: val('horizontal'),
    vertical: val('vertical'),
    majorDiagonal: val('diagMayor'),
    bridge: val('puente'),
    pantoscopicAngle: val('angPantoscopico'),
    panoramicAngle: val('angPanoramico'),
    observations: val('observaciones'),
});

const saveWorkOrder = async () => {
    const response = await fetch('/OrderWork/Create', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(buildRequestDto()),
    });

    if (!response.ok) {
        let message = 'No se pudo guardar la orden de trabajo.';
        try {
            const err = await response.json();
            if (err && err.message) message = err.message;
        } catch { /* ignore */ }
        throw new Error(message);
    }

    return response.json();
};

const showFeedback = (message, title, icon) => {
    if (typeof Swal !== 'undefined') {
        return Swal.fire({ icon, title, text: message, showConfirmButton: true });
    }
    alert(`${title}\n${message}`);
    return Promise.resolve();
};

const handleWorkOrderForm = () => {
    const form = document.getElementById('workOrderForm');
    if (!form) return;

    // El submit del form (botón "Imprimir") solo imprime, no guarda.
    form.addEventListener('submit', (event) => {
        event.preventDefault();
        event.stopPropagation();

        if (!form.checkValidity()) {
            form.classList.add('was-validated');
            return;
        }

        form.classList.add('was-validated');
        fillPrintArea();
        setTimeout(() => window.print(), 300);
    });
};

const handleSaveButton = () => {
    const btnSave = document.getElementById('btnSaveOrderWork');
    const form = document.getElementById('workOrderForm');
    if (!btnSave || !form) return;

    btnSave.addEventListener('click', async () => {
        if (!form.checkValidity()) {
            form.classList.add('was-validated');
            await showFeedback('Complete los datos requeridos antes de guardar.', 'Datos incompletos', 'warning');
            return;
        }

        if (!state.order.patient) {
            form.classList.add('was-validated');
            await showFeedback(
                'Debe seleccionar o registrar un cliente antes de guardar la orden de trabajo.',
                'Cliente requerido',
                'warning'
            );
            return;
        }

        form.classList.add('was-validated');

        const originalHtml = btnSave.innerHTML;
        setButtonLoading(btnSave, true, originalHtml);
        try {
            const result = await saveWorkOrder();
            await showFeedback(
                `La orden de trabajo se guardó correctamente${result?.id ? ` (ID: ${result.id})` : ''}.`,
                'Orden guardada',
                'success'
            );

            const modalEl = document.getElementById('newOrderWorkModal');
            if (modalEl) {
                const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
                modal.hide();
            }

            gridState.pageNumber = 1;
            await loadOrderWorks();
        } catch (err) {
            await showFeedback(
                err.message || 'No se pudo guardar la orden de trabajo.',
                'Error al guardar',
                'error'
            );
        } finally {
            setButtonLoading(btnSave, false);
        }
    });
};

const gridState = { pageNumber: 1, pageSize: 10 };

const escapeHtml = (value) => {
    if (value == null) return '';
    return String(value)
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');
};

const formatDateTime = (iso) => {
    if (!iso) return '';
    const d = new Date(iso);
    if (Number.isNaN(d.getTime())) return '';
    return `${formatFecha(d)} ${String(d.getHours()).padStart(2, '0')}:${String(d.getMinutes()).padStart(2, '0')}`;
};

const renderGrid = (paged) => {
    const container = document.getElementById('orderWorkGridContainer');
    if (!container) return;

    const items = paged?.items ?? [];
    const totalCount = paged?.totalCount ?? 0;
    const pageNumber = paged?.pageNumber ?? 1;
    const pageSize = paged?.pageSize ?? gridState.pageSize;
    const totalPages = paged?.totalPages ?? Math.max(1, Math.ceil(totalCount / pageSize));

    if (totalCount === 0) {
        container.innerHTML = `
            <div class="row">
                <div class="col-md-12">
                    <div class="alert alert-light text-center" role="alert">
                        No se encontraron ordenes de trabajo.
                    </div>
                </div>
            </div>`;
        return;
    }

    let index = (pageNumber - 1) * pageSize + 1;
    const rows = items.map(item => `
        <tr>
            <th scope="row">${index++}</th>
            <td>${escapeHtml(formatDateTime(item.createdAt))}</td>
            <td>${escapeHtml(item.material)}</td>
            <td>${escapeHtml(item.index)}</td>
            <td>${escapeHtml(item.mounting)}</td>
            <td>${escapeHtml(item.state)}</td>
            <td class="text-center">
                <a href="#" class="view-orderwork" data-id="${escapeHtml(item.id)}" title="Ver detalles">
                    <i class="bi bi-eye text-primary"></i>
                </a>
            </td>
            <td class="text-center">
                <a href="#" class="reprint-orderwork" data-id="${escapeHtml(item.id)}" title="Reimprimir">
                    <i class="bi bi-printer text-success"></i>
                </a>
            </td>
        </tr>
    `).join('');

    let pagination = '';
    if (totalPages > 1) {
        const pageItems = [];
        for (let p = 1; p <= totalPages; p++) {
            pageItems.push(`
                <li class="page-item ${p === pageNumber ? 'active' : ''}">
                    <a class="page-link" href="#" data-page="${p}">${p}</a>
                </li>`);
        }
        pagination = `
            <div class="col-md-12">
                <nav aria-label="Page navigation">
                    <ul class="pagination justify-content-center">
                        <li class="page-item ${pageNumber > 1 ? '' : 'disabled'}">
                            <a class="page-link" href="#" data-page="${pageNumber - 1}">Anterior</a>
                        </li>
                        ${pageItems.join('')}
                        <li class="page-item ${pageNumber < totalPages ? '' : 'disabled'}">
                            <a class="page-link" href="#" data-page="${pageNumber + 1}">Siguiente</a>
                        </li>
                    </ul>
                </nav>
            </div>`;
    }

    container.innerHTML = `
        <div class="row">
            <div class="col-md-12">
                <div class="table-responsive">
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">Fecha</th>
                                <th scope="col">Material</th>
                                <th scope="col">Índice</th>
                                <th scope="col">Montaje</th>
                                <th scope="col">Estado</th>
                                <th scope="col" class="text-center">Detalles</th>
                                <th scope="col" class="text-center">Reimprimir</th>
                            </tr>
                        </thead>
                        <tbody>${rows}</tbody>
                    </table>
                </div>
            </div>
            ${pagination}
        </div>`;

    container.querySelectorAll('a.page-link[data-page]').forEach(link => {
        link.addEventListener('click', (event) => {
            event.preventDefault();
            const parent = link.parentElement;
            if (parent && parent.classList.contains('disabled')) return;

            const page = Number(link.dataset.page);
            if (!Number.isFinite(page) || page < 1) return;

            gridState.pageNumber = page;
            loadOrderWorks();
        });
    });

    container.querySelectorAll('a.view-orderwork').forEach(link => {
        link.addEventListener('click', (event) => {
            event.preventDefault();
            const id = link.dataset.id;
            if (id) showOrderWorkDetail(id);
        });
    });

    container.querySelectorAll('a.reprint-orderwork').forEach(link => {
        link.addEventListener('click', (event) => {
            event.preventDefault();
            const id = link.dataset.id;
            if (id) reprintOrderWork(id);
        });
    });
};

const fetchOrderWorkById = async (id) => {
    const response = await fetch(`/OrderWork/GetById?id=${encodeURIComponent(id)}`, {
        headers: { 'Accept': 'application/json' }
    });
    if (!response.ok) {
        let msg = `HTTP ${response.status}`;
        try {
            const err = await response.json();
            if (err && err.message) msg = err.message;
        } catch { /* ignore */ }
        throw new Error(msg);
    }
    return response.json();
};

const fillDetailModal = (data) => {
    const set = (id, value) => {
        const el = document.getElementById(id);
        if (el) el.textContent = value ?? '';
    };

    console.log({data})

    set('d_id', data.id);
    set('d_createdAt', formatDateTime(data.createdAt));
    set('d_state', data.state);
    set('d_material', data.material);
    set('d_index', data.index);
    set('d_treatmentNotes', data.treatmentNotes);

    set('d_od_esf', data.od_ESF);
    set('d_od_cil', data.od_CIL);
    set('d_od_axis', data.od_AXIS);
    set('d_od_add', data.od_ADD);
    set('d_od_dnp', data.od_DNP);
    set('d_od_height', data.od_HEIGHT);

    set('d_oi_esf', data.oi_ESF);
    set('d_oi_cil', data.oi_CIL);
    set('d_oi_axis', data.oi_AXIS);
    set('d_oi_add', data.oi_ADD);
    set('d_oi_dnp', data.oi_DNP);
    set('d_oi_height', data.oi_HEIGHT);

    set('d_mounting', data.mounting);
    set('d_horizontal', data.horizontal);
    set('d_vertical', data.vertical);
    set('d_majorDiagonal', data.majorDiagonal);
    set('d_bridge', data.bridge);
    set('d_pantoscopicAngle', data.pantoscopicAngle);
    set('d_panoramicAngle', data.panoramicAngle);
    set('d_observation', data.observation);
};

const showOrderWorkDetail = async (id) => {
    try {
        const data = await fetchOrderWorkById(id);
        fillDetailModal(data);

        const btnReprint = document.getElementById('btnReprintFromDetail');
        if (btnReprint) btnReprint.dataset.id = id;

        const modalEl = document.getElementById('orderWorkDetailModal');
        if (modalEl) bootstrap.Modal.getOrCreateInstance(modalEl).show();
    } catch (err) {
        await showFeedback(err.message || 'No se pudo obtener el detalle.', 'Error', 'error');
    }
};

const fillPrintAreaFromData = (data) => {
    const setText = (id, text) => {
        const el = document.getElementById(id);
        if (el) el.textContent = text ?? '';
    };

    setText('p_numero', data.id ?? '');
    setText('p_fecha', formatDateTime(data.createdAt));
    setText('p_cliente', '');
    setText('p_controlCliente', '');
    setText('p_nombrePaciente', '');
    setText('p_material', data.material);
    setText('p_indice', data.index);
    setText('p_filtroTratamientos', data.treatmentNotes);

    setText('p_od_esferico', data.od_ESF);
    setText('p_od_cilindrico', data.od_CIL);
    setText('p_od_eje', data.od_AXIS);
    setText('p_od_adicion', data.od_ADD);
    setText('p_od_dnp', data.od_DNP);
    setText('p_od_altura', data.od_HEIGHT);

    setText('p_oi_esferico', data.oi_ESF);
    setText('p_oi_cilindrico', data.oi_CIL);
    setText('p_oi_eje', data.oi_AXIS);
    setText('p_oi_adicion', data.oi_ADD);
    setText('p_oi_dnp', data.oi_DNP);
    setText('p_oi_altura', data.oi_HEIGHT);

    setText('p_montaje', data.mounting);
    setText('p_horizontal', data.horizontal);
    setText('p_vertical', data.vertical);
    setText('p_diagMayor', data.majorDiagonal);
    setText('p_puente', data.bridge);
    setText('p_angPantoscopico', data.pantoscopicAngle);
    setText('p_angPanoramico', data.panoramicAngle);
    setText('p_observaciones', data.observation);
};

const reprintOrderWork = async (id) => {
    try {
        const data = await fetchOrderWorkById(id);
        fillPrintAreaFromData(data);
        setTimeout(() => window.print(), 300);
    } catch (err) {
        await showFeedback(err.message || 'No se pudo reimprimir la orden.', 'Error', 'error');
    }
};

const handleDetailModalReprint = () => {
    const btn = document.getElementById('btnReprintFromDetail');
    if (!btn) return;
    btn.addEventListener('click', () => {
        const id = btn.dataset.id;
        if (id) reprintOrderWork(id);
    });
};

const showGridLoader = () => {
    const container = document.getElementById('orderWorkGridContainer');
    if (!container) return;
    container.innerHTML = `
        <div class="row">
            <div class="col-md-12 text-center py-5">
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Cargando...</span>
                </div>
                <div class="mt-2 text-muted">Cargando ordenes de trabajo...</div>
            </div>
        </div>`;
};

const setButtonLoading = (button, isLoading, originalHtml) => {
    if (!button) return;
    if (isLoading) {
        button.dataset.originalHtml = originalHtml ?? button.innerHTML;
        button.innerHTML = `
            <div class="spinner-border spinner-border-sm text-light" role="status">
                <span class="visually-hidden">Guardando...</span>
            </div>`;
        button.disabled = true;
    } else {
        if (button.dataset.originalHtml) {
            button.innerHTML = button.dataset.originalHtml;
            delete button.dataset.originalHtml;
        }
        button.disabled = false;
    }
};

const loadOrderWorks = async () => {
    const container = document.getElementById('orderWorkGridContainer');
    showGridLoader();
    try {
        const url = `/OrderWork/GetAll?pageNumber=${gridState.pageNumber}&pageSize=${gridState.pageSize}`;
        const response = await fetch(url, { headers: { 'Accept': 'application/json' } });
        if (!response.ok) throw new Error(`HTTP ${response.status}`);

        const paged = await response.json();
        renderGrid(paged);
    } catch (err) {
        if (container) {
            container.innerHTML = `
                <div class="row">
                    <div class="col-md-12">
                        <div class="alert alert-danger text-center" role="alert">
                            No se pudieron cargar las ordenes de trabajo. ${escapeHtml(err.message || '')}
                        </div>
                    </div>
                </div>`;
        }
    }
};

const init = () => {
    const btnNew = document.getElementById('btnNewOrderWork');
    if (btnNew) btnNew.addEventListener('click', openNewOrderWorkModal);

    handleWorkOrderForm();
    handleSaveButton();
    handleDetailModalReprint();
    loadOrderWorks();
};

init();
