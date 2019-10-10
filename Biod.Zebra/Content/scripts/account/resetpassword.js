(function () {
  let $message = $('.submit-message'); 
  $('form').on('submit', submit);
  
  function validate() {
    const password = $('#Password').val();
    const confirmPassword = $('#ConfirmPassword').val();
    
    if (password !== confirmPassword) {
      showMessage('Passwords do not match', true);
      return false;
    }
    
    return true;
  }
  
  function submit(e) {
    e.preventDefault();

    let $inputs = $('form input.toggleable');
    $inputs.prop('disabled', true);
    showMessage('');
    
    if (!validate()) {
      $inputs.prop('disabled', false);
      return;
    }
    
    $.ajax({
      url: window.baseUrl + '/mvcapi/user/resetpassword' + window.location.search,
      method: 'POST',
      data: {
        Email: $('#Email').val(),
        Password: $('#Password').val(),
        ConfirmPassword: $('#ConfirmPassword').val(),
        __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val()
      },
      success: () => {
        showMessage(`Password successfully updated. <a href="${window.baseUrl}">Go to login page.</a>`);
      },
      error: (jqXHR) => {
        showMessage(jqXHR.responseText, true);
        $inputs.prop('disabled', false);
      }
    });
  }
  
  function showMessage(message, isError = false) {
    $message.html(message.replace('\\n', '<br />')).addClass(isError ? 'error' : 'success').removeClass(isError ? 'success' : 'error');
  }
})();