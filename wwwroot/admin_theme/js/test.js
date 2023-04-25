// (function ($) {
//     "use strict";
//     $(document).ready(function () {
//         function check_login_form(id_form) {
//             let email = $(id_form).find("input[type=email]");
//             let pass = $(id_form).find("input[type=password]");
//             const email_regex =
//                 /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
//             if (email.val().trim() == "") {
//                 email.parent().addClass("err");
//                 email.parent().removeClass("success");
//                 $(email).siblings(`span[data-valmsg-for="Email"]`).html("");
//                 $(email).siblings(`span[data-valmsg-for="Email"]`).html("Email và mật khẩu không được để trống!");
//                 console.log($(email).siblings(`span[data-valmsg-for="Email"]`));
//             } else {
//                 if (!email_regex.test(email.val().trim())) {
//                     email.parent().addClass("err");
//                     email.parent().removeClass("success");
//                     $(email).siblings(`span[data-valmsg-for="Email"]`).html("");
//                     $(email).siblings(`span[data-valmsg-for="Email"]`).html("Email không hợp lệ!");
//                     console.log($(email).siblings(`span[data-valmsg-for="Email"]`));
//                 }
//                 else {
//                     email.parent().removeClass("err");
//                     email.parent().addClass("success");
//                     $(email).siblings(`span[data-valmsg-for="Email"]`).html("");
//                 }
//             }
//             if (pass.val().trim() == "") {
//                 pass.parent().addClass("err");
//                 pass.parent().removeClass("success");
//                 $(pass).siblings(`span[data-valmsg-for="Email"]`).html("");
//                 $(pass).siblings(`span[data-valmsg-for="Email"]`).html("Email và mật khẩu không được để trống!");
//                 console.log($(pass).siblings(`span[data-valmsg-for="Email"]`));
//             } else {
//                 if (!email_regex.test(pass.val().trim())) {
//                     pass.parent().addClass("err");
//                     pass.parent().removeClass("success");
//                     $(pass).siblings(`span[data-valmsg-for="Email"]`).html("");
//                     $(pass).siblings(`span[data-valmsg-for="Email"]`).html("Email không hợp lệ!");
//                     console.log($(pass).siblings(`span[data-valmsg-for="Email"]`));
//                 }
//                 else {
//                     pass.parent().removeClass("err");
//                     pass.parent().addClass("success");
//                     $(pass).siblings(`span[data-valmsg-for="Email"]`).html("");
//                 }
//             }
//         }

//         var id_form = "#signin-form";
//         var signin_btn = $(id_form).find("button");
//         $(signin_btn).click(function (e) {
//             check_login_form("#signin-form");
//             e.preventDefault();
//         });
//     });
// })(window.jQuery);
