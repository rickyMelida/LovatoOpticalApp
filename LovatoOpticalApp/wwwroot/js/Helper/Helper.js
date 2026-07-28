export function formatToGuarani(value) {
  return new Intl.NumberFormat('es-PY', {
    style: 'currency',
    currency: 'PYG',
    minimumFractionDigits: 0,
    maximumFractionDigits: 0,
  }).format(value);
}

export const aplicarFormatoGuarani = (input) => {
    const formatterGuarani = new Intl.NumberFormat("es-PY", {
        style: "currency",
        currency: "PYG",
        minimumFractionDigits: 0,
        maximumFractionDigits: 0,
    });

    const limpiarNumero = (valor) => valor.replace(/\D/g, "");

    input.addEventListener("blur", () => {
        const valorLimpio = limpiarNumero(input.value);

        if (!valorLimpio) {
            input.value = "";
            return;
        }

        input.value = formatterGuarani.format(Number(valorLimpio));
    });

    input.addEventListener("focus", () => {
        input.value = limpiarNumero(input.value);
    });
}

export const guaraniStringANumero = (valor) => {
    if (!valor) return 0;

    return Number(valor.replace(/\D/g, ""));
}