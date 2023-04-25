(function ($) {
  "use strict";
  $(document).ready(function () {
      function check_login_form(id_form) {
          // var check_form = 0;
          // var check_form_email = 0;
          // var check_form_pwd = 0;
          $(id_form)
              .find("input")
              .each(function () {
                  let err_span = $(this).siblings(`span[data-err-for="${this.id}"]`);
                  let form_group = $(this).parent();
                  let val = $(this).val().trim();// Validate Email
                  const email_regex =
                      /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
                  if (val.length == "") {
                      form_group.addClass("err");
                      form_group.removeClass("success");
                      $(err_span).html("");
                      $(err_span).html("Email và mật khẩu không được để trống!");
                      // check_form = 1;
                  } else {
                      // Validate length
                      switch ($(this).attr("type")) {
                          case "password":
                              // check password length
                              if (val.length < 8) {
                                  $("#error-message").text("Mật khẩu không hợp lệ!");
                                  // check_form_pwd += 1;
                              } else {
                                  form_group.removeClass("err");
                                  form_group.addClass("success");
                                  $(err_span).html("");
                                  // check_form_pwd = 0;
                              }
                              break;
                          case "email":
                              // check for valid email format
                              if (!email_regex.test(val)) {
                                  form_group.addClass("err");
                                  form_group.removeClass("success");
                                  $(err_span).html("");
                                  $(err_span).html("Email không hợp lệ!");
                                  // check_form_email += 1;
                              } else {
                                  form_group.removeClass("err");
                                  form_group.addClass("success");
                                  $(err_span).html("");
                                  // check_form_email = 0;
                              }
                      }
                  }
              });
          // if (check_form_email > 0 || check_form_pwd > 0) {
          //     check_form = 1;
          // }
          // return check_form;
      }
      var id_form = "#signin-form";
      var signin_btn = $(id_form).find("button");
      $(signin_btn).click(function (e) {
          check_login_form("#signin-form")
          // if (check_login_form("#signin-form") > 0) {
          //     console.log('lỗi');
          // } else {
          //     console.log('Pass');
          // }
          e.preventDefault();
      });
  });
})(window.jQuery);
