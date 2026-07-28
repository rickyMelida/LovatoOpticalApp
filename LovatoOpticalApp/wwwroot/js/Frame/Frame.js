import { initFrameForm } from './Frame.Form.js';
import { aplicarFormatoGuarani } from '../Helper/Helper.js'

const frameCode = document.getElementById("frameCode");
const frameCodeHelp = document.getElementById('frameCodeHelp');

// Formato esperado: "GU2872 069 54-17 140"
// Partes: [Código armazón] [Color] [Ancho-Alto] [Largo patilla]
const FRAME_CODE_REGEX = /^([A-Za-z0-9]+)(?:\s+([A-Za-z0-9]+))?(?:\s+(\d+)-(\d+))?(?:\s+(\d+))?$/;

const parseFrameCode = (value) => {
    const trimmed = value.trim();
    if (!trimmed) {
        return '<span class="text-muted">Ingrese el código del armazón. Ej: <em>GU2872 069 54-17 140</em></span>';
    }

    const match = trimmed.match(FRAME_CODE_REGEX);
    if (!match) {
        return '<span class="text-danger"><i class="bi bi-exclamation-circle me-1"></i>Formato inválido. Esperado: <em>CÓDIGO COLOR ANCHO-ALTO PATILLA</em> &mdash; Ej: <em>GU2872 069 54-17 140</em></span>';
    }

    const [, codigo, color, ancho, alto, patilla] = match;
    const parts = [];

    parts.push(`<span class="badge bg-primary me-1">${codigo}</span> Código del armazón`);

    if (color !== undefined) {
        parts.push(`<span class="badge bg-secondary me-1">${color}</span> Color`);
    }
    if (ancho !== undefined && alto !== undefined) {
        parts.push(`<span class="badge bg-success me-1">${ancho}-${alto}</span> Calibre (ancho <strong>${ancho} mm</strong> &bull; alto <strong>${alto} mm</strong>)`);
    }
    if (patilla !== undefined) {
        parts.push(`<span class="badge bg-warning text-dark me-1">${patilla}</span> Largo de patilla <strong>${patilla} mm</strong>`);
    }

    return parts.join('<span class="text-muted mx-1">|</span>');
}


export const initFrame = () => {
	initFrameForm();
	
	frameCode.addEventListener('input', (e) => {
		frameCodeHelp.innerHTML = parseFrameCode(e.target.value);
    });

    document.querySelectorAll(".input-guarani").forEach(aplicarFormatoGuarani);
}