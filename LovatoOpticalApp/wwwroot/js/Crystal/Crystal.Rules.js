function normalizeAxis(axis) {
    if (axis == null) return null;

    let normalized = Number(axis) % 180;

    if (normalized <= 0) {
        normalized += 180;
    }

    return normalized;
}

function transposePrescription(prescription) {
    const sphere = Number(prescription.sphere);
    const cylinder = Number(prescription.cylinder || 0);
    const axis = prescription.axis != null ? normalizeAxis(prescription.axis) : null;

    if (cylinder === 0) {
        return {
            sphere,
            cylinder: 0,
            axis: null,
        };
    }

    return {
        sphere: sphere + cylinder,
        cylinder: -cylinder,
        axis: normalizeAxis(axis + 90),
    };
}

function toNegativeCylinder(prescription) {
    const cylinder = Number(prescription.cylinder || 0);

    if (cylinder > 0) {
        return transposePrescription(prescription);
    }

    return {
        sphere: Number(prescription.sphere),
        cylinder,
        axis: cylinder === 0 ? null : normalizeAxis(prescription.axis),
    };
}

function roundToQuarter(value) {
    return Math.round(value * 4) / 4;
}

function areAxesEquivalent(axis1, axis2) {
    return normalizeAxis(axis1) === normalizeAxis(axis2);
}

function sameAstigmatism(distance, near) {
    const d = toNegativeCylinder(distance);
    const n = toNegativeCylinder(near);

    const sameCylinder = roundToQuarter(d.cylinder) === roundToQuarter(n.cylinder);

    if (d.cylinder === 0 && n.cylinder === 0) {
        return sameCylinder;
    }

    const sameAxis = areAxesEquivalent(d.axis, n.axis);

    return sameCylinder && sameAxis;
}

function calculateAdd(distance, near) {
    const d = toNegativeCylinder(distance);
    const n = toNegativeCylinder(near);

    if (!sameAstigmatism(d, n)) {
        throw new Error(
            "La graduación de cerca no parece ser solo una adición sobre la de lejos. Revisar cilindro/eje."
        );
    }

    return roundToQuarter(n.sphere - d.sphere);
}

export function calculatePrescriptionSummary(recipe) {
    const distanceOD = toNegativeCylinder(recipe.distance.OD);
    const distanceOI = toNegativeCylinder(recipe.distance.OI);

    const addOD = calculateAdd(recipe.distance.OD, recipe.near.OD);
    const addOI = calculateAdd(recipe.distance.OI, recipe.near.OI);

    const result = {
        OD: distanceOD,
        OI: distanceOI,
        ADD_OD: addOD,
        ADD_OI: addOI,
    };

    if (addOD === addOI) {
        result.ADD = addOD;
        delete result.ADD_OD;
        delete result.ADD_OI;
    }

    return result;
}

function formatPower(value) {
    const number = Number(value);

    if (number > 0) {
        return `+${number.toFixed(2)}`;
    }

    return number.toFixed(2);
}

function formatEyePrescription(prescription) {
    const sphere = formatPower(prescription.sphere);
    const cylinder = formatPower(prescription.cylinder);

    if (prescription.cylinder === 0) {
        return sphere;
    }

    return `${sphere} ${cylinder} x ${prescription.axis}`;
}

export function formatSummary(summary) {
    const lines = [];

    lines.push(`OD: ${formatEyePrescription(summary.OD)}`);
    lines.push(`OI: ${formatEyePrescription(summary.OI)}`);

    if (summary.ADD != null) {
        lines.push(`ADD: ${formatPower(summary.ADD)}`);
    } else {
        lines.push(`ADD OD: ${formatPower(summary.ADD_OD)}`);
        lines.push(`ADD OI: ${formatPower(summary.ADD_OI)}`);
    }

    return lines.join("\n");
}

const recipe = {
    distance: {
        OD: {
            sphere: -2.00,
            cylinder: -0.75,
            axis: 180,
        },
        OI: {
            sphere: -1.50,
            cylinder: -1.25,
            axis: 170,
        },
    },
    near: {
        OD: {
            sphere: 0.00,
            cylinder: -0.75,
            axis: 180,
        },
        OI: {
            sphere: 0.50,
            cylinder: -1.25,
            axis: 170,
        },
    },
};

const summary = calculatePrescriptionSummary(recipe);

console.log(summary);
console.log(formatSummary(summary));