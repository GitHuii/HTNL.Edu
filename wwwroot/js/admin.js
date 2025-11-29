// Toggle Sidebar for Mobile
document.addEventListener('DOMContentLoaded', function () {
    const toggleBtn = document.getElementById('toggleSidebar');
    const sidebar = document.querySelector('.admin-sidebar');

    if (toggleBtn) {
        toggleBtn.addEventListener('click', function () {
            sidebar.classList.toggle('active');
        });
    }

    // Close sidebar when clicking outside on mobile
    document.addEventListener('click', function (event) {
        if (window.innerWidth <= 968) {
            const isClickInsideSidebar = sidebar.contains(event.target);
            const isClickOnToggle = toggleBtn.contains(event.target);

            if (!isClickInsideSidebar && !isClickOnToggle && sidebar.classList.contains('active')) {
                sidebar.classList.remove('active');
            }
        }
    });

    // ============================
    //        ACTIVE NAV ITEM
    // ============================
    // Active nav item (fixed)
    const currentPath = window.location.pathname.toLowerCase();
    const navItems = document.querySelectorAll('.nav-item');

    navItems.forEach(item => {
        const href = item.getAttribute('href');
        if (!href) return;

        const link = href.toLowerCase();

        // 🔥 Không được auto-active cho link "/" (Về trang chủ)
        if (link === "/") return;

        // Nếu href = "/admin" → active đúng "/admin"
        if (link === "/admin") {
            if (currentPath === "/admin") {
                item.classList.add('active');
            }
            return;
        }

        // Các menu khác so khớp chính xác
        if (currentPath.startsWith(link)) {
            item.classList.add('active');
        }
    });


    // Auto-hide success alerts after 5 seconds
    const alerts = document.querySelectorAll('.alert');
    alerts.forEach(alert => {
        setTimeout(() => {
            alert.style.opacity = '0';
            alert.style.transition = 'opacity 0.5s';
            setTimeout(() => alert.remove(), 500);
        }, 5000);
    });
});
