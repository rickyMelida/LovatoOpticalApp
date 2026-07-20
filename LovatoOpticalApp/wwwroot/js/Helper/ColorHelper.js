const colores = [
	{ nombre: "Blanco", hex: "#FFFFFF" },
	{ nombre: "Negro", hex: "#000000" },
	{ nombre: "Rojo", hex: "#FF0000" },
	{ nombre: "Verde", hex: "#00FF00" },
	{ nombre: "Azul", hex: "#0000FF" },
	{ nombre: "Amarillo", hex: "#FFFF00" },
	// ... agregá más
];

function hexToRgb(hex) {
	hex = hex.replace('#', '');
	const num = parseInt(hex, 16);
	return { r: (num >> 16) & 255, g: (num >> 8) & 255, b: num & 255 };
}

function distancia(c1, c2) {
	return Math.sqrt(
		(c1.r - c2.r) ** 2 +
		(c1.g - c2.g) ** 2 +
		(c1.b - c2.b) ** 2
	);
}

export function getColorName(hex) {
	const rgb = hexToRgb(hex);
	let masCercano = colores[0];
	let menorDistancia = Infinity;

	for (const color of colores) {
		const d = distancia(rgb, hexToRgb(color.hex));
		if (d < menorDistancia) {
			menorDistancia = d;
			masCercano = color;
		}
	}

	return masCercano.nombre;
}