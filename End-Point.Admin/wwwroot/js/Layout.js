document.addEventListener("DOMContentLoaded", function (event) {

    const showNavbar = (toggleId, navId, bodyId, headerId) => {
        const toggle = document.getElementById(toggleId),
            nav = document.getElementById(navId),
            bodypd = document.getElementById(bodyId),
            headerpd = document.getElementById(headerId)

        // Validate that all variables exist
        if (toggle && nav && bodypd && headerpd) {
            toggle.addEventListener('click', () => {
                // show navbar
                nav.classList.toggle('show')
                // change icon
                toggle.classList.toggle('bx-x')
                // add padding to body
                bodypd.classList.toggle('body-pd')
                // add padding to header
                headerpd.classList.toggle('body-pd')
            })
        }
    }

    showNavbar('header-toggle', 'nav-bar', 'body-pd', 'header')

    document.addEventListener('DOMContentLoaded', function ()
    {
        const navLinks = document.querySelectorAll('.nav_link');
        navLinks.forEach(link => {
            link.addEventListener('click', function () {
                navLinks.forEach(link => link.classList.remove('active'));
                this.classList.add('active');
            });
        });
    });

    // Your code to run since DOM is loaded and ready
});