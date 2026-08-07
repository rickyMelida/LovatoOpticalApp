const colores = [
	{ nombre: "Blanco", hex: "#FFFFFF" },
	{ nombre: "Negro", hex: "#000000" },
	{ nombre: "Rojo", hex: "#FF0000" },
	{ nombre: "Verde", hex: "#00FF00" },
	{ nombre: "Azul", hex: "#0000FF" },
	{ nombre: "Amarillo", hex: "#FFFF00" },
	{ nombre: "Marrón", hex: "#8B4513" },
	{ nombre: "Gris", hex: "#808080" },
	{ nombre: "Dorado", hex: "#CDAA00" },
	{ nombre: "Plateado", hex: "#C0C0C0" },
	{ nombre: "Azul Marino", hex: "#1F3A5F" },
	{ nombre: "Verde Oliva", hex: "#6B8E23" },
	{ nombre: "Naranja", hex: "#FFA500" },
	{ nombre: "Rosa", hex: "#FFC0CB" },
	{ nombre: "Violeta", hex: "#8A2BE2" },
	{ nombre: "Beige", hex: "#F5F5DC" }
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