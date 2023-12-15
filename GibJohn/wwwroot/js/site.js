function setTheme(theme) {
    localStorage.setItem("theme", theme);
    applyTheme();
}

function getTheme() {
    let currentTheme;
    if (localStorage.length === 0) {
        currentTheme = setTheme("dark");
    } else {
        currentTheme = localStorage.getItem('theme');
    }
    return currentTheme;

}

function applyTheme() {
    var theme = getTheme();
    if (theme === "light") {
        document.documentElement.setAttribute('data-bs-theme', 'light');
    }
    else if (theme === "dark") {
        document.documentElement.setAttribute('data-bs-theme', 'dark');
    }
}