function setTheme(theme) {
    localStorage.setItem("theme", theme);
    applyTheme();
}

function getTheme() {
    localStorage.getItem("theme");
}

function applyTheme() {
    let theme = getTheme();
    if (theme === "dark") {
        document.documentElement.setAttribute("data-bs-theme", "dark");
    }
    else {
        document.documentElement.setAttribute("data-bs-theme", "light");
    }
}