// Crotto Plinius - Minimal Vanilla JS

document.addEventListener("DOMContentLoaded", () => {
    // Smooth scrolling for category pills
    const categoryLinks = document.querySelectorAll(".category-nav a");
    categoryLinks.forEach(link => {
        link.addEventListener("click", (e) => {
            const targetId = link.getAttribute("href");
            if (targetId && targetId.startsWith("#")) {
                const targetEl = document.querySelector(targetId);
                if (targetEl) {
                    e.preventDefault();
                    targetEl.scrollIntoView({ behavior: "smooth", block: "start" });
                }
            }
        });
    });
});
