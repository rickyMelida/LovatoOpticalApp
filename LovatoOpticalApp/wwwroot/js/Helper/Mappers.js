export const mapCrystalTypeToEnum = {
	"SingleVision": 1,
	"Blank": 2,
	"BifocalKriptop": 3,
	"BifocalFloptop": 4,
	"Multifocal": 5,
	"BlankSingleVision": [1, 2],
	"Bifocal": [3, 4],
}

export const mapProductTypeToEnum = {
	"Frame": 1,
	"Crystal": 2,
	"Accessory": 3
};

export const mapProductTypeToString = {
	1: "Frame",
	2: "Crystal",
	3: "Accessory"
};

export const mapFrameTypeToString = {
	0: "Hilo",
	1: "Al Tornillo",
	2: "Aro Completo"
};

export const mapFrameMaterialToString = {
	0: "Metal",
	1: "Acetato",
	2: "Titanio",
	3: "Plástico"
};

export const mapProductModal = {
	1: "newFrameModal",
	2: "newCrystalModal",
	3: "newAccessoryModal"
};