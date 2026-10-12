import { aplicarFormatoGuarani } from '../Helper/Helper.js';
import { showModal } from "../Common/ModalEvents.js";

document.querySelectorAll(".input-guarani").forEach(aplicarFormatoGuarani);
const btnLoadCrystalPrices = document.getElementById("btnLoadCrystalPrices");

/*const lista = document.getElementById("multifocales-lista");
const template = document.getElementById("fila-multifocal-template");
const mensajeVacio = document.getElementById("multifocales-vacio");

function actualizarFilas() {
	[...lista.rows].forEach((fila, indice) => {
		fila.querySelector(".multifocal-marca").name = `multifocales[${indice}].Marca`;
		fila.querySelector(".multifocal-gama").name = `multifocales[${indice}].Gama`;
		fila.querySelector(".multifocal-precio").name = `multifocales[${indice}].Precio`;
	});
	mensajeVacio.hidden = lista.rows.length > 0;
}

document.getElementById("agregar-multifocal").addEventListener("click", () => {
	const fila = template.content.firstElementChild.cloneNode(true);
	aplicarFormatoGuarani(fila.querySelector(".multifocal-precio"));
	lista.appendChild(fila);
	actualizarFilas();
});

lista.addEventListener("click", (event) => {
	const boton = event.target.closest(".quitar-multifocal");
	if (!boton) return;

	boton.closest("tr").remove();
	actualizarFilas();
});*/

btnLoadCrystalPrices.addEventListener("click", async () => {
	showModal("typeCrystalSelectorModal");

})