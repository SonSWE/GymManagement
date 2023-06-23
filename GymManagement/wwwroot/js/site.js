// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var _countSpinLoading = 0; // Dem so lan func dc goi
function SpinLoading($create) {
    $create = $create || false;
    if ($create) {
        _countSpinLoading++;
        $('#divLoader').css('display', 'block');
    } else {
        _countSpinLoading--;
        if (_countSpinLoading <= 0) {
            _countSpinLoading = 0;
            $('#divLoader').css('display', 'none');
        }
    }
}
