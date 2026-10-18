import { mapCrystalTypeToEnum } from '../Helper/Mappers.js';
import { aplicarFormatoGuarani } from '../Helper/Helper.js';

export const loadCrystalPrices = async (modalSelected) => {
	const dataCrystalType = modalSelected.getAttribute("data-crystal-type");
	const crystalTypeEnum = mapCrystalTypeToEnum[dataCrystalType];
	const isMultifocal = crystalTypeEnum === mapCrystalTypeToEnum["Multifocal"];

	if (isMultifocal) {
		console.log("Tipo de cristal seleccionado:", dataCrystalType, "Enum:", crystalTypeEnum);
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

const getCrystalPriceByType = async (type) => {
	const response = await fetch(`/Crystal/GetCrystalPricesByType?crystalType=${type}`);
	if (!response.ok)
		throw new Error('Failed to fetch crystal price');

	return await response.json();
}