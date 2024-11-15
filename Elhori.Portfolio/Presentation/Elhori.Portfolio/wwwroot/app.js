window.ShowToast = (message, bgClass) => {
    const toastElement = document.createElement('div');
    toastElement.classList.add('toast', bgClass);
    toastElement.setAttribute('role', 'alert');
    toastElement.setAttribute('aria-live', 'assertive');
    toastElement.setAttribute('aria-atomic', 'true');
    toastElement.innerHTML = `
    <div class="toast-body" style="color: white;">
        ${message}
    </div>`
    ;

    document.getElementById('toastContainer').appendChild(toastElement);

    const toast = new bootstrap.Toast(toastElement);
    toast.show();

    setTimeout(() => {
        toastElement.remove();
    }, 5000);
};
