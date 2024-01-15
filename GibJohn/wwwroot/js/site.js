function setTheme() {
    let currentTheme = localStorage.getItem('theme');
    if (currentTheme === 'light') {
        localStorage.setItem('theme', 'dark');
    } else {
        localStorage.setItem('theme', 'light');
    }
    applyTheme();
}

function applyTheme() {
    var theme = localStorage.getItem('theme');
    if (theme === 'light') {
        document.documentElement.setAttribute('data-bs-theme', 'light');
    } else if (theme === 'dark') {
        document.documentElement.setAttribute('data-bs-theme', 'dark');
    }
}