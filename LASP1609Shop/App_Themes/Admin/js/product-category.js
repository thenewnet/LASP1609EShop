$(document).ready(function () {
    var successMessage = $("#cphContent_hdSuccessMessage").val();
    if (successMessage != null && successMessage != undefined && successMessage != "") {
        showSuccess(successMessage);
    }
    var errorMessage = $("#cphContent_hdErrorMessage").val();
    if (errorMessage != null && errorMessage != undefined && errorMessage != "") {
        showError(errorMessage);
    }
})