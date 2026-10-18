import { mapCrystalTypeToEnum } from '../Helper/Mappers.js';
import { aplicarFormatoGuarani } from '../Helper/Helper.js';

export const loadCrystalPrices = async (modalSelected) => {
	const dataCrystalType = modalSelected.getAttribute("data-crystal-type");
	const crystalTypeEnum = mapCrystalTypeToEnum[dataCrystalType];
	const isMultifocal = crystalTypeEnum === mapCrystalTypeToEnum["Multifocal"];

	if (isMultifocal) {
		const response = await getMultifocalPrices();
		renderMultifocalPrices(response);
		console.log({ multifocales: response });

		return;
	}

	const promises = crystalTypeEnum.map(type => getCrystalPriceByType(type));

	try {
		const crystalPricesMap = {
			"BlankSingleVision": setSimpleVisionAndBlankPrices,
			"Bifocal": setBifocalPrices
		}

		const prices = await Promise.all(promises);
		await crystalPricesMap[dataCrystalType](prices);
		document.querySelectorAll(".input-guarani").forEach(aplicarFormatoGuarani);

	} catch (error) {
		console.error("Error al obtener los precios de los cristales:", error);
	}

}

const setSimpleVisionAndBlankPrices = async (prices) => {
	const simpleVisionInput = document.getElementById("prec_simple");
	const blankInput = document.getElementById("prec_blanco");

	const simpleVisionData = prices.find(e => e.type == 1);
	const blankData = prices.find(e => e.type == 2);

	const simpleVisionCurrentPrice = simpleVisionData.prices.reduce((max, item) =>
		new Date(item.effectivityDate) > new Date(max.effectivityDate) ? item : max
	);

	const blankCurrentPrice = blankData.prices.reduce((max, item) =>
		new Date(item.effectivityDate) > new Date(max.effectivityDate) ? item : max
	);

	simpleVisionInput.value = simpleVisionCurrentPrice.price;
	blankInput.value = blankCurrentPrice.price;
}

const setBifocalPrices = async (prices) => {
	const bifocalKriptoInput = document.getElementById("prec_bifocal_kriptop");
	const bifocalFlaptopInput = document.getElementById("prec_bifocal_flaptop");

	const bifocalKriptoData = prices.find(e => e.type == 3);
	const bifocalFlaptopData = prices.find(e => e.type == 4);

	const bifocalKriptoCurrentPrice = bifocalKriptoData.prices.reduce((max, item) =>
		new Date(item.effectivityDate) > new Date(max.effectivityDate) ? item : max
	);

	const bifocalFlaptopPrice = bifocalFlaptopData.prices.reduce((max, item) =>
		new Date(item.effectivityDate) > new Date(max.effectivityDate) ? item : max
	);



	bifocalKriptoInput.value = bifocalKriptoCurrentPrice.price;
	bifocalFlaptopInput.value = bifocalFlaptopPrice.price;
}

const renderMultifocalPrices = (prices) => {
	const tbody = document.getElementById("multifocales-lista");
	tbody.innerHTML = ""; // limpiar antes de rellenar

	if (!prices || prices.length === 0) {
		tbody.innerHTML = `
                <tr>
                    <td colspan="4" class="text-center text-muted">No hay resultados</td>
                </tr>`;
		return;
	}

	const fragment = document.createDocumentFragment();

	prices.forEach(item => {
		const tr = document.createElement("tr");

		const tdMarca = document.createElement("td");
		tdMarca.textContent = item.name;

		const tdDescripcion = document.createElement("td");
		tdDescripcion.textContent = item.description;

		const tdPrecio = document.createElement("td");
		tdPrecio.textContent = new Intl.NumberFormat("es-PY", {
			style: "currency",
			currency: "PYG",
			maximumFractionDigits: 0
		}).format(/*item.precio*/300000);

		const tdAcciones = document.createElement("td");
		tdAcciones.className = "text-center";
		tdAcciones.innerHTML = `
			<button type="button" class="btn btn-outline-warning editar-multifocal" aria-label="Editar multifocal">
				<i class="bi bi-pen"></i>
			</button>
		`;

		tr.append(tdMarca, tdDescripcion, tdPrecio, tdAcciones);
		fragment.appendChild(tr);
	});

	tbody.appendChild(fragment);
}

export const newMultifocalBrand = (data = {}) => {
	const tbody = document.getElementById("multifocales-lista");
	const template = document.getElementById("fila-multifocal-template");
	// Clonar el contenido del template
    const fragment = template.content.cloneNode(true);
    const fila = fragment.querySelector("tr");

    // Rellenar valores si se pasan (útil para cargar registros existentes)
    fila.querySelector(".multifocal-marca").value = data.marca ?? "";
    fila.querySelector(".multifocal-description").value = data.descripcion ?? "";
    fila.querySelector(".multifocal-precio").value = data.precio ?? "";

    tbody.appendChild(fragment);

    // Llevar el foco al primer input de la nueva fila
    fila.querySelector(".multifocal-marca").focus();
}

const getCrystalPriceByType = async (type) => {
	const response = await fetch(`/Crystal/GetCrystalPricesByType?crystalType=${type}`);
	if (!response.ok)
		throw new Error('Failed to fetch crystal price');

	return await response.json();
}

const getMultifocalPrices = async () => {
	const response = await fetch(`/Crystal/GetMultifocalPrices`);
	if (!response.ok)
		throw new Error('Failed to fetch crystal price');

	return await response.json();
}