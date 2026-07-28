function normalizeAxis(axis) {
    if (axis == null || axis === "") return null;

    let value = Number(axis);

    if (Number.isNaN(value)) {
        throw new Error(`Eje inválido: ${axis}`);
    }

    value = value % 180;

    if (value <= 0) {
        value += 180;
    }

    return value;
}

function parseEyePrescription(eye) {
    const sphere = parseOpticalPower(eye.sphere);
    const cylinder = parseOpticalPower(eye.cylinder || "000");
    const axis = cylinder === 0 ? null : normalizeAxis(eye.axis);

    return {
        sphere,
        cylinder,
        axis,
    };
}

function transposeEyePrescription(eye) {
    if (eye.cylinder === 0) {
        return {
            sphere: eye.sphere,
            cylinder: 0,
            axis: null,
        };
    }

    return {
        sphere: eye.sphere + eye.cylinder,
        cylinder: -eye.cylinder,
        axis: normalizeAxis(eye.axis + 90),
    };
}

function toNegativeCylinder(eye) {
    if (eye.cylinder > 0) {
        return transposeEyePrescription(eye);
    }

    return eye;
}

function sameAstigmatism(distanceEye, nearEye) {
    const distance = toNegativeCylinder(distanceEye);
    const near = toNegativeCylinder(nearEye);

    const sameCylinder = distance.cylinder === near.cylinder;

    if (distance.cylinder === 0 && near.cylinder === 0) {
        return sameCylinder;
    }

    const sameAxis = normalizeAxis(distance.axis) === normalizeAxis(near.axis);

    return sameCylinder && sameAxis;
}

function calculateAddForEye(distanceEyeRaw, nearEyeRaw) {
    const distanceEye = toNegativeCylinder(parseEyePrescription(distanceEyeRaw));
    const nearEye = toNegativeCylinder(parseEyePrescription(nearEyeRaw));

    if (!sameAstigmatism(distanceEye, nearEye)) {
        throw new Error(
            "La receta de cerca no parece ser solamente una adición sobre la receta de lejos."
        );
    }

    return nearEye.sphere - distanceEye.sphere;
}

export function calculatePrescriptionSummary(recipe) {
    const distanceOD = toNegativeCylinder(parseEyePrescription(recipe.distance.OD));
    const distanceOI = toNegativeCylinder(parseEyePrescription(recipe.distance.OI));

    const addOD = calculateAddForEye(recipe.distance.OD, recipe.near.OD);
    const addOI = calculateAddForEye(recipe.distance.OI, recipe.near.OI);

    const result = {
        OD: distanceOD,
        OI: distanceOI,
    };

    if (addOD === addOI) {
        result.ADD = addOD;
    } else {
        result.ADD_OD = addOD;
        result.ADD_OI = addOI;
    }

    return result;
}

function formatEyePrescription(eye) {
    const sphere = formatOpticalPower(eye.sphere);

    if (eye.cylinder === 0) {
        return sphere;
    }

    const cylinder = formatOpticalPower(eye.cylinder);

    return `${sphere} ${cylinder} ${eye.axis}`;
}

export function formatPrescriptionSummary(summary) {
    const lines = [];

    lines.push(`<span class="small"><strong>OD:</strong> ${formatEyePrescription(summary.OD)}</span>`);
    lines.push(`<span class="small"><strong>OI:</strong> ${formatEyePrescription(summary.OI)}</span>`);

    if (summary.ADD != null) {
        lines.push(`<span class="small"><strong>ADD:</strong> ${formatOpticalPower(summary.ADD)}</span>`);
    } else {
        lines.push(`<span class="small"><strong>ADD OD:</strong> ${formatOpticalPower(summary.ADD_OD)}</span>`);
        lines.push(`<span class="small"><strong>ADD OI:</strong> ${formatOpticalPower(summary.ADD_OI)}</span>`);
    }

    return lines.join("\n");
}

function parseOpticalPower(value) {
    if (value == null) return null;

    const raw = String(value).trim().toUpperCase();

    if (
        raw === "" ||
        raw === "PL" ||
        raw === "PLANO" ||
        raw === "PLAN" ||
        raw === "0" ||
        raw === "000" ||
        raw === "+000" ||
        raw === "-000"
    ) {
        return 0;
    }

    // Acepta formatos como:
    // +200, -175, +025, 200, -2.00, +0.25, .25, -.75
    const normalized = raw.replace(",", ".");

    // Si viene con punto decimal, por ejemplo -1.75
    if (normalized.includes(".")) {
        const number = Number(normalized);

        if (Number.isNaN(number)) {
            throw new Error(`Potencia inválida: ${value}`);
        }

        return Math.round(number * 100);
    }

    // Si viene en formato óptica: +200, -175, +025
    const match = normalized.match(/^([+-]?)(\d{1,4})$/);

    if (!match) {
        throw new Error(`Potencia inválida: ${value}`);
    }

    const sign = match[1] === "-" ? -1 : 1;
    const digits = match[2];

    return sign * Number(digits);
}

function formatOpticalPower(power) {
    if (power == null) return "";

    const sign = power < 0 ? "-" : "+";
    const absolute = Math.abs(power);

    return `${sign}${String(absolute).padStart(3, "0")}`;
}

function formatDiopters(power) {
    if (power == null) return "";

    const sign = power > 0 ? "+" : "";
    return `${sign}${(power / 100).toFixed(2)}`;
}

function isQuarterStep(power) {
    return power % 25 === 0;
}

/*
    const recipe = {
        distance: {
            OD: {
                sphere: "-200",
                cylinder: "-075",
                axis: "180",
            },
            OI: {
                sphere: "-150",
                cylinder: "-125",
                axis: "170",
            },
        },
        near: {
            OD: {
                sphere: "000",
                cylinder: "-075",
                axis: "180",
            },
            OI: {
                sphere: "+050",
                cylinder: "-125",
                axis: "170",
            },
        },
    };

    const summary = calculatePrescriptionSummary(recipe);

    console.log(formatPrescriptionSummary(summary));
*/