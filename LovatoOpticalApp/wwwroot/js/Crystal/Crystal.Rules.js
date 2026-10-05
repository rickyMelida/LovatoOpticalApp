// ============================================================
// UTILIDADES DE POTENCIA ÓPTICA
// ============================================================

function parseOpticalPower(value) {
	if (value == null) return null;

	const raw = String(value).trim().toUpperCase();

	// Plano / cero
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
	// +200
	// -175
	// +025
	// 200
	// -2.00
	// +0.25
	// .25
	// -.75
	const normalized = raw.replace(",", ".");

	// Formato decimal:
	// -1.75
	// +0.50
	// .25
	// -.75
	if (normalized.includes(".")) {
		const number = Number(normalized);

		if (Number.isNaN(number)) {
			throw new Error(`Potencia inválida: ${value}`);
		}

		// Internamente trabajamos en centésimas de dioptría.
		// Ejemplo:
		// -1.75 -> -175
		// +0.50 -> +50
		return Math.round(number * 100);
	}

	// Formato óptico:
	// +200
	// -175
	// +025
	// 200
	const match = normalized.match(/^([+-]?)(\d{1,4})$/);

	if (!match) {
		throw new Error(`Potencia inválida: ${value}`);
	}

	const sign = match[1] === "-" ? -1 : 1;
	const digits = match[2];

	return sign * Number(digits);
}


// ============================================================
// EJE
// ============================================================

function normalizeAxis(axis) {
	if (axis == null || axis === "") {
		return null;
	}

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


// ============================================================
// PARSEAR UNA RECETA DE UN OJO
// ============================================================

function isEmptyEye(eye) {
	if (!eye) {
		return true;
	}

	return (
		eye.sphere == null &&
		eye.cylinder == null &&
		eye.axis == null
	);
}


function parseEyePrescription(eye) {
	if (isEmptyEye(eye)) {
		return null;
	}

	const sphere = parseOpticalPower(
		eye.sphere ?? "000"
	);

	const cylinder = parseOpticalPower(
		eye.cylinder ?? "000"
	);

	// Si no hay cilindro, el eje no tiene sentido.
	const axis =
		cylinder === 0
			? null
			: normalizeAxis(eye.axis);

	return {
		sphere,
		cylinder,
		axis,
	};
}


// ============================================================
// TRANSPOSICIÓN
//
// Ejemplo:
//
// +1.00 +0.50 × 90
//
// se convierte en:
//
// +1.50 -0.50 × 180
// ============================================================

function transposeEyePrescription(eye) {
	if (!eye) {
		return null;
	}

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


// ============================================================
// NORMALIZAR A CILINDRO NEGATIVO
// ============================================================

function toNegativeCylinder(eye) {
	if (!eye) {
		return null;
	}

	if (eye.cylinder > 0) {
		return transposeEyePrescription(eye);
	}

	return eye;
}


// ============================================================
// COMPROBAR SI DOS RECETAS TIENEN EL MISMO ASTIGMATISMO
//
// Para poder convertir:
//
// LEJOS + CERCA
//
// en:
//
// LEJOS + ADD
//
// el cilindro y el eje deben ser iguales
// después de normalizar ambas recetas.
// ============================================================

function sameAstigmatism(distanceEye, nearEye) {
	if (!distanceEye || !nearEye) {
		return false;
	}

	const distance = toNegativeCylinder(distanceEye);
	const near = toNegativeCylinder(nearEye);

	// El cilindro debe ser igual
	if (distance.cylinder !== near.cylinder) {
		return false;
	}

	// Si ninguno tiene cilindro,
	// no necesitamos comparar ejes.
	if (
		distance.cylinder === 0 &&
		near.cylinder === 0
	) {
		return true;
	}

	// Si tienen cilindro, el eje debe coincidir.
	return (
		normalizeAxis(distance.axis) ===
		normalizeAxis(near.axis)
	);
}


// ============================================================
// CALCULAR ADD
//
// ADD = esfera de cerca - esfera de lejos
//
// Ejemplo:
//
// Lejos: -2.00
// Cerca: +0.00
//
// ADD = 0.00 - (-2.00)
//     = +2.00
// ============================================================

function calculateAdd(distanceEye, nearEye) {
	if (!distanceEye || !nearEye) {
		return null;
	}

	if (!sameAstigmatism(distanceEye, nearEye)) {
		return null;
	}

	const distance = toNegativeCylinder(distanceEye);
	const near = toNegativeCylinder(nearEye);

	const add = near.sphere - distance.sphere;

	// Una ADD convencional para cerca
	// normalmente debe ser positiva.
	if (add <= 0) {
		return null;
	}

	return add;
}


// ============================================================
// PROCESAR UN OJO
//
// Determina qué tipo de información tenemos:
//
// empty
// distance-only
// near-only
// distance-plus-add
// distance-and-near
// ============================================================

function processEye(distanceRaw, nearRaw) {
	const distance = toNegativeCylinder(
		parseEyePrescription(distanceRaw)
	);

	const near = toNegativeCylinder(
		parseEyePrescription(nearRaw)
	);

	// --------------------------------------------------------
	// No hay receta
	// --------------------------------------------------------

	if (!distance && !near) {
		return {
			distance: null,
			near: null,
			add: null,
			mode: "empty",
		};
	}

	// --------------------------------------------------------
	// Solo visión lejana
	// --------------------------------------------------------

	if (distance && !near) {
		return {
			distance,
			near: null,
			add: null,
			mode: "distance-only",
		};
	}

	// --------------------------------------------------------
	// Solo visión cercana
	// --------------------------------------------------------

	if (!distance && near) {
		return {
			distance: null,
			near,
			add: null,
			mode: "near-only",
		};
	}

	// --------------------------------------------------------
	// Existen ambas
	// --------------------------------------------------------

	const add = calculateAdd(distance, near);

	// Se puede representar como:
	//
	// LEJOS + ADD
	//

	if (add != null) {
		return {
			distance,
			near,
			add,
			mode: "distance-plus-add",
		};
	}

	// --------------------------------------------------------
	// Existen ambas, pero no pueden reducirse a ADD
	// --------------------------------------------------------

	return {
		distance,
		near,
		add: null,
		mode: "distance-and-near",
	};
}


// ============================================================
// CALCULAR EL RESUMEN COMPLETO
// ============================================================

export function calculatePrescriptionSummary(recipe) {
	const distance = recipe?.distance ?? {};
	const near = recipe?.near ?? {};

	const OD = processEye(
		distance.OD,
		near.OD
	);

	const OI = processEye(
		distance.OI,
		near.OI
	);

	const result = {
		OD,
		OI,
	};

	// --------------------------------------------------------
	// Buscar ADD válidas
	// --------------------------------------------------------

	const odHasAdd = OD.mode === "distance-plus-add";
	const oiHasAdd = OI.mode === "distance-plus-add";

	// Ambos ojos tienen ADD
	if (odHasAdd && oiHasAdd) {

		// Misma ADD para ambos ojos
		if (OD.add === OI.add) {
			result.ADD = OD.add;
		}

		// ADD diferente para cada ojo
		else {
			result.ADD_OD = OD.add;
			result.ADD_OI = OI.add;
		}
	}

	// Solo OD tiene ADD
	else if (odHasAdd) {
		result.ADD_OD = OD.add;
	}

	// Solo OI tiene ADD
	else if (oiHasAdd) {
		result.ADD_OI = OI.add;
	}

	return result;
}


// ============================================================
// FORMATEAR POTENCIA
//
// Internamente:
//
// -200 = -2.00
// -50  = -0.50
// 200  = +2.00
//
// Formato óptico:
//
// -200
// -050
// +200
// ============================================================

function formatOpticalPower(power) {
	if (power == null) {
		return "";
	}

	const sign = power < 0 ? "-" : "+";
	const absolute = Math.abs(power);

	return `${sign}${String(absolute).padStart(3, "0")}`;
}


// ============================================================
// FORMATEAR UN OJO
// ============================================================

function formatEyePrescription(eye) {
	if (!eye) {
		return "";
	}

	const sphere = formatOpticalPower(
		eye.sphere
	);

	// Sin cilindro
	if (eye.cylinder === 0) {
		return sphere;
	}

	const cylinder = formatOpticalPower(
		eye.cylinder
	);

	return `${sphere} ${cylinder} ${eye.axis}`;
}


// ============================================================
// FORMATEAR LA RECETA COMPLETA
// ============================================================

export function formatPrescriptionSummary(summary) {
	const lines = [];

	// --------------------------------------------------------
	// OD
	// --------------------------------------------------------

	if (summary.OD.mode === "distance-only") {

		lines.push(
			`<span class="small"><strong>OD:</strong> ` +
			`${formatEyePrescription(summary.OD.distance)}</span>`
		);
	}

	else if (summary.OD.mode === "near-only") {

		lines.push(
			`<span class="small"><strong>OD CERCA:</strong> ` +
			`${formatEyePrescription(summary.OD.near)}</span>`
		);
	}

	else if (summary.OD.mode === "distance-plus-add") {

		lines.push(
			`<span class="small"><strong>OD:</strong> ` +
			`${formatEyePrescription(summary.OD.distance)}</span>`
		);
	}

	else if (summary.OD.mode === "distance-and-near") {

		lines.push(
			`<span class="small"><strong>OD LEJOS:</strong> ` +
			`${formatEyePrescription(summary.OD.distance)}</span>`
		);

		lines.push(
			`<span class="small"><strong>OD CERCA:</strong> ` +
			`${formatEyePrescription(summary.OD.near)}</span>`
		);
	}


	// --------------------------------------------------------
	// OI
	// --------------------------------------------------------

	if (summary.OI.mode === "distance-only") {

		lines.push(
			`<span class="small"><strong>OI:</strong> ` +
			`${formatEyePrescription(summary.OI.distance)}</span>`
		);
	}

	else if (summary.OI.mode === "near-only") {

		lines.push(
			`<span class="small"><strong>OI CERCA:</strong> ` +
			`${formatEyePrescription(summary.OI.near)}</span>`
		);
	}

	else if (summary.OI.mode === "distance-plus-add") {

		lines.push(
			`<span class="small"><strong>OI:</strong> ` +
			`${formatEyePrescription(summary.OI.distance)}</span>`
		);
	}

	else if (summary.OI.mode === "distance-and-near") {

		lines.push(
			`<span class="small"><strong>OI LEJOS:</strong> ` +
			`${formatEyePrescription(summary.OI.distance)}</span>`
		);

		lines.push(
			`<span class="small"><strong>OI CERCA:</strong> ` +
			`${formatEyePrescription(summary.OI.near)}</span>`
		);
	}


	// --------------------------------------------------------
	// ADD
	// --------------------------------------------------------

	if (summary.ADD != null) {

		lines.push(
			`<span class="small"><strong>ADD:</strong> ` +
			`${formatOpticalPower(summary.ADD)}</span>`
		);
	}

	else {

		if (summary.ADD_OD != null) {
			lines.push(
				`<span class="small"><strong>ADD OD:</strong> ` +
				`${formatOpticalPower(summary.ADD_OD)}</span>`
			);
		}

		if (summary.ADD_OI != null) {
			lines.push(
				`<span class="small"><strong>ADD OI:</strong> ` +
				`${formatOpticalPower(summary.ADD_OI)}</span>`
			);
		}
	}

	return lines.join("\n");
}


// ============================================================
// EJEMPLO REAL
//
// RECETA DE LA IMAGEN QUE ME MOSTRASTE
//
// Visión lejana:
//
// OD: PLANO -0.50 × 65
// OI: PLANO -0.50 × 85
//
// Visión cercana:
//
// NO INDICADA
// ============================================================

/*const recipe = {
	distance: {
		OD: {
			sphere: "PLANO",
			cylinder: "-050",
			axis: "65",
		},

		OI: {
			sphere: "PLANO",
			cylinder: "-050",
			axis: "85",
		},
	},

	// La receta original NO tiene graduación de cerca.
	near: {
		OD: null,
		OI: null,
	},
};*/


// ============================================================
// PROCESAR
// ============================================================

//const summary = calculatePrescriptionSummary(recipe);


// ============================================================
// MOSTRAR
// ============================================================

/*console.log("SUMMARY:");
console.log(summary);

console.log("\nFORMATTED:");
console.log(
	formatPrescriptionSummary(summary)
);*/