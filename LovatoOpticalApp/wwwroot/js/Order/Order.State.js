/* ---------- SIMULATED DATA ---------- */
export const DB_PATIENTS = [
    {
        id: 1,
        documentId: '12345678',
        name: 'Juan Pérez',
        phone: '0991 234 567',
        email: 'juan.perez@mail.com'
    }
];

export const DB_PRESCRIPTIONS = {
    1: {
        od: '-2.00 / -0.50 / 90Â°',
        oi: '-1.75 / -0.25 / 85Â°',
        date: '2026-05-10',
        optometrist: 'Dra. Salazar'
    }
};

export const DB_FRAMES = [
    { id: 'a1', name: 'Ray-Ban Aviator RB3025', stock: 4, price: 89.0 },
    { id: 'a2', name: 'Vogue VO5051',           stock: 0, price: 54.0 },
    { id: 'a3', name: 'Oakley Holbrook',         stock: 2, price: 97.5 }
];

export const DB_LENSES = [
    { id: 'c1', name: 'Monofocal antirreflejo', price: 15000 },
    { id: 'c2', name: 'Bifocal',                price: 220000 },
    { id: 'c3', name: 'Progresivo premium',      price: 350000 }
];

export const DB_ACCESSORIES = [
    { id: 'x1', name: 'Estuche rígido',   price: 25000 },
    { id: 'x2', name: 'Paño de limpieza', price: 5000 },
    { id: 'x3', name: 'Cordón  deportivo', price: 6000 }
];

/* ---------- STATE ---------- */
export const state = {
    currentStep: 1,
    order: {
        patient: null,
        prescription: null,
        isOwnFrame: false,
        frame: null,
        lens: null,
        accessories: [],
        deposit: 0,
        paymentMethod: '',
        paymentReference: ''
    }
};
