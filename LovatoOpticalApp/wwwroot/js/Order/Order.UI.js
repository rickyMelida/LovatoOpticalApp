import { state } from './Order.State.js';

export const showFeedback = (message, type = 'warning') => {
    const feedback = document.getElementById('feedbackGlobal');

    feedback.className = `alert alert-${type}`;
    feedback.textContent = message;
    feedback.classList.remove('d-none');

    window.scrollTo({ top: 0, behavior: 'smooth' });
};

export const hideFeedback = () => {
    document.getElementById('feedbackGlobal').classList.add('d-none');
};

export const updateStepper = () => {
    document.querySelectorAll('.step-item').forEach(item => {
        const step = Number(item.dataset.step);
        const badge = item.querySelector('.step-badge');
        const label = item.querySelector('.small');

        badge.className = 'badge rounded-circle step-badge mb-2';
        label.className = 'small fw-semibold';

        if (step < state.currentStep) {
            badge.classList.add('text-bg-success');
            badge.innerHTML = '<i class="bi bi-check-lg"></i>';
            label.classList.add('text-success');
        } else if (step === state.currentStep) {
            badge.classList.add('text-bg-primary');
            badge.textContent = step;
            label.classList.add('text-primary');
        } else {
            badge.classList.add('text-bg-secondary');
            badge.textContent = step;
            label.classList.add('text-muted');
        }
    });
};
