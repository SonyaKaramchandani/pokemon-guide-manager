(function () {
  let location = null;
  let $resetPassword = $('#useradmin-create #passwordreset');
  let resetPassword = $resetPassword.prop('checked');
  
  $('#useradmin-create #location')
    .autocomplete({
      source: window.baseUrl + '/api/geoname/city',
      minLength: 3,
      search: () => {
        $('#spinner').show();
      },
      select: (_, ui) => {
        location = ui.item.key;
      },
      response: (_, ui) => {
        if (ui.content.length === 0) {
          ui.content.push({
            key: 0,
            value: "No results found"
          });
        }
        $('#spinner').hide();
      }
    })
    .blur(function() {
      if (!location) {
        $(this).autocomplete("search", $(this).val());
      }
    })
    .autocomplete("instance")._renderItem = (ul, item) => {
    const styleStr = (item.key === 0) ? " style='pointer-events:none;opacity:0.6;height:200px'" : "";
    return $("<li" + styleStr + ">")
      .append("<div id='" + item.key + "'>" + item.value + "</div>")
      .appendTo(ul);
    };

  $resetPassword
    .on('change', function() {
      resetPassword = $(this).prop('checked');
      displayPasswordSection(!resetPassword);
    });
  
  $('#useradmin-create form').on('submit', submit);
  
  function displayPasswordSection(display = false) {
    if (display) {
      $('.password-section').show();
      $('.password-section input[type=password]').attr('required', 'required');
    } else {
      $('.password-section').hide();
      $('.password-section input[type=password]').removeAttr('required');
    }
  }
  
  function submit(e) {
    e.preventDefault();
    
    kendo.ui.progress($('.admin-settings'), true);
    $.ajax({
      url: window.baseUrl + '/mvcapi/user/create',
      method: 'POST',
      data: {
        FirstName: document.getElementById('firstname').value,
        LastName: document.getElementById('lastname').value,
        RoleNames: [document.getElementById('role').value],
        Organization: document.getElementById('organization').value || null,
        PhoneNumber: document.getElementById('phonenumber').value || null,
        Email: document.getElementById('email').value,
        LocationGeonameId: location,
        ResetPasswordRequired: resetPassword,
        Password: !resetPassword && document.getElementById('password').value || null,
        ConfirmPassword: !resetPassword && document.getElementById('confirmpassword').value || null
      },
      error: (jqXHR) => {
        $('#useradmin-create #errormessage').text(jqXHR.responseText);
        kendo.ui.progress($('.admin-settings'), false);
      },
      success: () => {
        window.location = window.baseUrl + '/DashboardPage/UserAdmin/Index';
      }
    });
  }
})();