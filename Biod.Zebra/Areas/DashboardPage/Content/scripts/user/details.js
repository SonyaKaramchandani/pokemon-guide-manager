(function(userId) {
  $('#useradmin-details #resend-registration-email')
    .on('click', function(e) {
      e.preventDefault();

      kendo.ui.progress($('.admin-settings'), true);
      $.ajax({
        url: window.baseUrl + '/mvcapi/user/sendregistrationemail?userId=' + userId,
        method: 'POST',
        error: (jqXHR) => {
          $('#useradmin-details #message').text(jqXHR.responseText);
        },
        success: () => {
          $('#useradmin-details #message').text('Email successfully sent!');
        },
        complete: () => {
          kendo.ui.progress($('.admin-settings'), false);
        }
      });
    });
})(window.UserAdmin.Details.userId);