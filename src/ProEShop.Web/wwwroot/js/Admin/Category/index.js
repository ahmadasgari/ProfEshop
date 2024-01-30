$.get(`${location.pathname}?handler=GetDataTable`, function (data, status) {
    
    
    if (status == 'success') {
        console.log(status);
        $('.read-data-table').html(data);
    }
    else {
        showToastr('error', 'خطایی به وجود آمد، لطفا مجددا تلاش نمایید');
    }
});