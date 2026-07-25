import { DB_FRAMES, state } from './Order.State.js';
import { hideFeedback } from './Order.UI.js';
import { formatToGuarani } from '../Helper/Helper.js';

export const setFrameMode = async (mode) => {
    hideFeedback();

    document.getElementById('cardArmazonInventario').classList.toggle('selected', mode === 'inventory');
    document.getElementById('cardArmazonPropio').classList.toggle('selected', mode === 'own');

    document.getElementById('bloqueArmazonInventario').classList.toggle('d-none', mode !== 'inventory');
    document.getElementById('bloqueArmazonPropio').classList.toggle('d-none', mode !== 'own');

    state.order.isOwnFrame = mode === 'own';
    state.order.frame = null;

    if (mode === 'inventory') {
        await loadFrames();
    }
};

export const loadFrames = async () => {
    const select = document.getElementById('armazonSelect');

    if (select.options.length > 1) return;

    const frames = await getFramesAsync();

    frames.forEach(frame => {
        const option = document.createElement('option');

        option.value = frame.id;
        option.textContent = `${frame.name} — ${formatToGuarani(frame.salePrice)}`;

        select.appendChild(option);
    });
};

const getFramesAsync = async () => {
    const result = await fetch('/Catalog/GetFrames');

    if (result.status == 201)
        return null;

    return await result.json();
}

export const selectFrame = async () => {
    hideFeedback();

    const id   = document.getElementById('armazonSelect').value;
    const card = document.getElementById('armazonInfoCard');

    card.classList.add('d-none');
    card.classList.remove('alert-success', 'alert-danger');

    if (!id) {
        state.order.frame = null;
        return;
    }

    const frame = await getFrameById(id);

    if (frame.quantity <= 0) {
        state.order.frame = null;

        card.classList.add('alert-danger');
        card.classList.remove('d-none');
        card.innerHTML = `
            <strong>${frame.name}</strong><br>
            <span class="small">Sin stock disponible. Selecciona otro armazón.</span>
        `;

        return;
    }

    state.order.frame = frame;

    card.classList.add('alert-success');
    card.classList.remove('d-none');
    card.innerHTML = `
        <div class="d-flex justify-content-between align-items-center">
            <div>
                <strong>${frame.name}</strong><br>
                <span class="small">${formatToGuarani(frame.salePrice) }</span>
            </div>
            <span class="badge text-bg-success">${frame.quantity} en stock</span>
        </div>
    `;
};

const getFrameById = async (id) => {
    const result = await fetch(`/Catalog/GetFrameById?id=${id}`);

    if (result.status == 201)
        return null;

    return await result.json();
}
