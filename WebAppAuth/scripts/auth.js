$(document).ready(function() {

  //Custom checkboxes

  $('input[type="checkbox"]').iCheck({
    checkboxClass: 'icheckbox_minimal-blue',
    radioClass: 'iradio_minimal-blue'
  });

  // TODO: Переписать, чтобы работало

  //Form Error Messages
  var formGroup = $('.form-group');
  formGroup.find('input').focus(function() {
    formGroup.find('.field-validation-error').animate({height: 'hide'}, 500);
  });

  var formValidate = $('.form-validate');

  formValidate.validate({

    rules: {
      password: {
        required: true
      },
      ConfirmPassword: {
        required: true
      },
      emailLogin: {
        required: true,
        email: true
      },
      name: {
        required: true,
         maxlength: 16
      },
      surname: {
        required: true,
        maxlength: 26
      }
    },
    messages: {
      emailLogin: {
        required: "Пожалуйста, введите Email",
        email: true
      },
      ConfirmPassword: {
        required: "Пожалуйста, повторите пароль"
      }
    }
  });



});
